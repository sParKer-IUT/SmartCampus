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
    public partial class TeacherPayment : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        //for checking payment is valid or not
        public bool proceed;

        bool connected = false;

        //For Database connection
        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;

        //for the UI and calculation
        private DateTime joinDate;
        private int[] years;
        private String[] months;
        private int currentYear;
        private int currentMonth;
        public static int selectedYear;
        public static int selectedMonth;
        private int index;
        private int i;

        //for Database operations
        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;

        public TeacherPayment()
        {
            InitializeComponent();
        }

        private void TeacherPayment_Load(object sender, EventArgs e)
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
                ComboID.ValueMember = "id";
                ComboID.DisplayMember = "id";
                ComboID.DataSource = dt;

                sc.Dispose();
                reader.Dispose();
                connected = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                connected = false;
            }
        }

        private void ComboID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Paymentselecttchrdeptid.thisID = ComboID.SelectedValue.ToString();
        }

        private void ComboDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            Paymentselecttchrdeptid.thisdept = ComboDept.SelectedValue.ToString();
            sc = new MySqlCommand("select id from employee_info where department='" + ComboDept.SelectedValue.ToString() + "' order by id;", connection);
            reader = sc.ExecuteReader();

            dt = new DataTable();
            dt.Load(reader);
            ComboID.ValueMember = "id";
            ComboID.DisplayMember = "id";
            ComboID.DataSource = dt;

            sc = new MySqlCommand("select * from employee_info where department='" + Paymentselecttchrdeptid.thisdept + "'and id='" + Paymentselecttchrdeptid.thisID + "';", connection);
            reader = sc.ExecuteReader();
            reader.Read();
            joinDate = (DateTime)reader["JoinDate"];
            reader.Close();
            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;

            months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
            ComboMonth.DataSource = months;
            ComboMonth.SelectedIndex = currentMonth - 1;

            index = 0;
            years = new int[currentYear - joinDate.Year + 1];
            for (i = currentYear; i >= joinDate.Year; i--)
            {
                years[index] = i;
                index++;
            }
            ComboYear.DataSource = years;

            sc.Dispose();
            reader.Dispose();
        }

        private void Proceed_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            if (selectedYear < joinDate.Year)
            {
                proceed = false;
                MessageBox.Show("Invalid Year");
            }
            else if (selectedYear == joinDate.Year && selectedMonth < joinDate.Month)
            {
                proceed = false;
                MessageBox.Show("Invalid Month");
            }
            else
            {
                try
                {
                    sc = new MySqlCommand("select * from emp_payment where id = '" + Paymentselecttchrdeptid.thisID + "' and year = " + selectedYear + " and month = " + selectedMonth + ";", connection);
                    reader = sc.ExecuteReader();

                    if (reader.Read())
                    {
                        proceed = false;
                        MessageBox.Show(Paymentselecttchrdeptid.thisID + " is already PAID!!! for " + months[selectedMonth - 1] + " " + selectedYear.ToString(), "Payment Acknowledgement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        /* selectedYear = (int)ComboYear.SelectedValue;
                         Paymentselecttchrdeptid.paymentYear = selectedYear;
                         selectedMonth = (int)ComboMonth.SelectedIndex + 1;
                         Paymentselecttchrdeptid.paymentMonth = selectedMonth;
                         Paymentselecttchrdeptid.thisdept = ComboDept.SelectedValue.ToString();
                         Paymentselecttchrdeptid.thisID = ComboID.SelectedValue.ToString();*/
                        proceed = true;
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


            
            if (this.btn1Click != null)
            {
                clickedButton = Proceed;
                this.btn1Click(this, e);
            }
        }

        private void ComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedYear = (int)ComboYear.SelectedValue;
            Paymentselecttchrdeptid.paymentYear = selectedYear;
        }

        private void ComboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMonth = ComboMonth.SelectedIndex + 1;
            Paymentselecttchrdeptid.paymentMonth = selectedMonth;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }
    }

    public class Paymentselecttchrdeptid
    {
        public static String thisdept;
        public static String thisID;
        public static int paymentYear;
        public static int paymentMonth;
    }
}
