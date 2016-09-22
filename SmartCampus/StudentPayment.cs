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
    public partial class StudentPayment : UserControl
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

        //for the UI and calculation
        private DateTime admDate;
        private int[] years;
        private String[] months;
        private int currentYear;
        private int currentMonth;
        public static int selectedYear;
        public static int selectedMonth;
        private int index;
        private int i;

        bool connected = false;

        //for Database operations
        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;

        public StudentPayment()
        {

            InitializeComponent();

            //ComboStdID.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /*Loader of this UC
         *connect the database
         *take distinct classes for combobox
         *take the choosen class's students for combobox
        */
        private void StudentPayment_Load(object sender, EventArgs e)
        {
            try
            {
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;

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

                sc.Dispose();
                reader.Dispose();
                connected = true;
            }
            catch(Exception ex)
            {
                connected = false;
            }
            //connection.Close();
        }

        //combobox's current ID tracker
        private void ComboStdID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Paymentselectclassid.thisID = ComboStdID.SelectedValue.ToString();
        }

        /*combobox's current class tracker
         *when a class changes it again connects to the database and take the ids of the selected class to the combobox
         *and also set the month and year for the comboboxes checking from the db for current id selected
        */
        private void ComboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();*/
            Paymentselectclassid.thisclass = ComboClass.SelectedValue.ToString();
            
            sc = new MySqlCommand("select id from student_info where class='" + ComboClass.SelectedValue.ToString() + "' order by id;", connection);
            reader = sc.ExecuteReader();

            dt = new DataTable();
            dt.Load(reader);
            ComboStdID.ValueMember = "id";
            ComboStdID.DisplayMember = "id";
            ComboStdID.DataSource = dt;

            sc = new MySqlCommand("select * from student_info where class='" + Paymentselectclassid.thisclass + "'and id='" + Paymentselectclassid.thisID + "';", connection);
            reader = sc.ExecuteReader();
            reader.Read();
            admDate = (DateTime)reader["adm_date"];
            reader.Close();
            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;

            months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
            ComboMonth.DataSource = months;
            ComboMonth.SelectedIndex = currentMonth-1;

            index = 0;
            years = new int[currentYear - admDate.Year + 1];
            for (i = currentYear; i >= admDate.Year; i--)
            {
                years[index] = i;
                index++;
            }
            ComboYear.DataSource = years;

            sc.Dispose();
            reader.Dispose();
            //connection.Close();
        }

        //check already paid or not and valid month and year selected or not
        //also load UC PaymentReceipt
        private void Proceed_Click(object sender, EventArgs e)
        {
            if(!connected)return;

            if (selectedYear < admDate.Year)
            {
                proceed = false;
                MessageBox.Show("Invalid Year");
            }
            else if (selectedYear == admDate.Year && selectedMonth < admDate.Month)
            {
                proceed = false;
                MessageBox.Show("Invalid Month");
            }
            else
            {
                try
                {
                    sc = new MySqlCommand("select * from std_payment where id = '" + Paymentselectclassid.thisID + "' and year = " + selectedYear + " and month = " + selectedMonth + ";", connection);
                    reader = sc.ExecuteReader();

                    if (reader.Read())
                    {
                        proceed = false;
                        MessageBox.Show(Paymentselectclassid.thisID + " is already PAID!!! for " + months[selectedMonth - 1] + " " + selectedYear.ToString(), "Payment Acknowledgement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        proceed = true;
                        //reader.Close();
                        //connection.Close();
                    }
                    sc.Dispose();
                    reader.Dispose();
                }
                catch (Exception ex)
                {
                    proceed = false;
                    MessageBox.Show(ex.Message);
                }
            }

            try
            {
                sc = new MySqlCommand("select * from student_info where id = '" + Paymentselectclassid.thisID + "' and class = '" + Paymentselectclassid.thisclass + "';", connection);
                reader = sc.ExecuteReader();
                if (!reader.Read())
                {
                    proceed = false;
                    MessageBox.Show("Invalid ID!!!");
                }

                sc.Dispose();
                reader.Dispose();
            }
            catch(Exception ex)
            {
                proceed = false;
                MessageBox.Show(ex.Message);
            }

            if (this.btn1Click != null)
            {
                clickedButton = Proceed;
                this.btn1Click(this, e);
            }
            //connection.Close();
        }

        //to go back to UC Accounts
        private void Back_Click(object sender, EventArgs e)
        {
            //reader.Close();
            //connection.Close();
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        //combobox's current Month tracker
        private void ComboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMonth = ComboMonth.SelectedIndex + 1;
            Paymentselectclassid.paymentMonth = selectedMonth;
        }

        //combobox's current Year tracker
        private void ComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedYear = (int)ComboYear.SelectedValue;
            Paymentselectclassid.paymentYear = selectedYear;
        }

        private void ComboStdID_TextUpdate(object sender, EventArgs e)
        {
            Paymentselectclassid.thisID = ComboStdID.Text;
            //MessageBox.Show(ComboStdID.Text);
        }
    }

    /*
     * this class is used for the next UC PaymentReceipt to keep track of current student's ID,month,year and class
     * Consists of 4 static variables
    */
    public class Paymentselectclassid
    {
        public static String thisclass;
        public static String thisID;
        public static int paymentYear;
        public static int paymentMonth;
    }
}
