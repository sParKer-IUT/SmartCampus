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
    public partial class EmployeeOptions : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

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

        public EmployeeOptions()
        {
            InitializeComponent();
        }

        private void Recruit_Click(object sender, EventArgs e)
        {
            if(btn1Click != null)
            {
                clickedButton = Recruit;
                btn1Click(this, e);
            }
        }

        private void EmpInfo_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            try
            {
                sc = new MySqlCommand("select * from employee_info where id = '" + EmpDBselectdeptid.thisID + "' and department = '" + EmpDBselectdeptid.thisDept + "';", connection);
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
            if (btn1Click != null)
            {
                clickedButton = EmpInfo;
                btn1Click(this, e);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (btn1Click != null)
            {
                clickedButton = Back;
                btn1Click(this, e);
            }
        }

        private void EmployeeOptions_Load(object sender, EventArgs e)
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

                sc = new MySqlCommand("select distinct department from employee_info order by department", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboDept.ValueMember = "department";
                ComboDept.DisplayMember = "department";
                ComboDept.DataSource = dt;

                sc = new MySqlCommand("select id from employee_info where department='" + ComboDept.SelectedValue.ToString() + "' order by id;", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboEmpID.ValueMember = "id";
                ComboEmpID.DisplayMember = "id";
                ComboEmpID.DataSource = dt;
                connected = true;
                sc.Dispose();
                reader.Dispose();

                if (ComboDept.Items.Count > 0) ComboDept.SelectedIndex = 0;
                if (ComboEmpID.Items.Count > 0) ComboEmpID.SelectedIndex = 0;

                EmpDBselectdeptid.thisDept = ComboDept.SelectedValue.ToString();
                EmpDBselectdeptid.thisID = ComboEmpID.SelectedValue.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connected = false;
            }
        }

        private void ComboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!connected) return;
            try
            {
                EmpDBselectdeptid.thisDept = ComboDept.SelectedValue.ToString();

                sc = new MySqlCommand("select id from employee_info where department='" + ComboDept.SelectedValue.ToString() + "' order by id;", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboEmpID.ValueMember = "id";
                ComboEmpID.DisplayMember = "id";
                ComboEmpID.DataSource = dt;

                sc.Dispose();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpDBselectdeptid.thisID = ComboEmpID.SelectedValue.ToString();
        }

        private void ComboEmpID_TextChanged(object sender, EventArgs e)
        {
            EmpDBselectdeptid.thisID = ComboEmpID.SelectedValue.ToString();
        }
    }

    public class EmpDBselectdeptid
    {
        public static String thisDept;
        public static String thisID;
    }
}
