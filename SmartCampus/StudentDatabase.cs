using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SmartCampus
{
    public partial class StudentDatabase : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        //for checking payment is valid or not
        public bool proceed;

        //For Database connection
        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;

        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;

        bool connected = false;

        public StudentDatabase()
        {
            InitializeComponent();
        }
        private void StudentDatabase_Load(object sender, EventArgs e)
        {
            try
            {
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;
                proceed = false;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                sc = new MySqlCommand("select distinct class from student_info order by class", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboClass.ValueMember = "class";
                ComboClass.DisplayMember = "class";
                ComboClass.DataSource = dt;

                sc = new MySqlCommand("select id from student_info where class='" + ComboClass.SelectedValue.ToString() + "' order by id;", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboStdID.ValueMember = "id";
                ComboStdID.DisplayMember = "id";
                ComboStdID.DataSource = dt;
                connected = true;
                sc.Dispose();
                reader.Dispose();

                if(ComboClass.Items.Count > 0) ComboClass.SelectedIndex = 0;
                if (ComboStdID.Items.Count > 0) ComboStdID.SelectedIndex = 0;

                StdDBselectclassid.thisclass = ComboClass.SelectedValue.ToString();
                StdDBselectclassid.thisID = ComboStdID.SelectedValue.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connected = false;
            }
        }
        private void ComboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!connected) return;
            try
            {
                StdDBselectclassid.thisclass = ComboClass.SelectedValue.ToString();

                sc = new MySqlCommand("select id from student_info where class='" + ComboClass.SelectedValue.ToString() + "' order by id;", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboStdID.ValueMember = "id";
                ComboStdID.DisplayMember = "id";
                ComboStdID.DataSource = dt;

                sc.Dispose();
                reader.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboStdID_SelectedIndexChanged(object sender, EventArgs e)
        {
            StdDBselectclassid.thisID = ComboStdID.SelectedValue.ToString();
        }

        private void stdInfo_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            try
            {
                sc = new MySqlCommand("select * from student_info where id = '" + StdDBselectclassid.thisID + "' and class = '" + StdDBselectclassid.thisclass + "';", connection);
                reader = sc.ExecuteReader();
                if (!reader.Read())
                {
                    proceed = false;
                    MessageBox.Show("Invalid ID!!!");
                }
                else
                {
                    proceed = true;
                }
                sc.Dispose();
                reader.Dispose();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            if (this.btn1Click != null)
            {
                clickedButton = stdInfo;
                this.btn1Click(this, e);
            }
        }

        private void Attendence_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            try
            {
                sc = new MySqlCommand("select * from student_info where id = '" + StdDBselectclassid.thisID + "' and class = '" + StdDBselectclassid.thisclass + "';", connection);
                reader = sc.ExecuteReader();
                if (!reader.Read())
                {
                    proceed = false;
                    MessageBox.Show("Invalid ID!!!");
                }
                else
                {
                    proceed = true;
                }
                sc.Dispose();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (this.btn1Click != null)
            {
                clickedButton = Attendance;
                this.btn1Click(this, e);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        private void Payment_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            try
            {
                sc = new MySqlCommand("select * from student_info where id = '" + StdDBselectclassid.thisID + "' and class = '" + StdDBselectclassid.thisclass + "';", connection);
                reader = sc.ExecuteReader();
                if (!reader.Read())
                {
                    proceed = false;
                    MessageBox.Show("Invalid ID!!!");
                }
                else
                {
                    proceed = true;
                }
                sc.Dispose();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (this.btn1Click != null)
            {
                clickedButton = Payment;
                this.btn1Click(this, e);
            }
        }

        

        private void ComboStdID_TextChanged(object sender, EventArgs e)
        {
            StdDBselectclassid.thisID = ComboStdID.Text;
        }
    }
    public class StdDBselectclassid
    {
        public static String thisclass;
        public static String thisID;
    }
}
