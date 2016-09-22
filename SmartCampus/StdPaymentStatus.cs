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
    public partial class StdPaymentStatus : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        public bool connected = false;

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

        //for Database operations
        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;
        public StdPaymentStatus()
        {
            InitializeComponent();
        }

        private void StdPaymentStatus_Load(object sender, EventArgs e)
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
                sc = new MySqlCommand("select * from student_info where class='" + StdDBselectclassid.thisclass + "'and id='" + StdDBselectclassid.thisID + "';", connection);
                reader = sc.ExecuteReader();
                reader.Read();
                admDate = (DateTime)reader["adm_date"];
                reader.Close();
                currentYear = DateTime.Now.Year;
                currentMonth = DateTime.Now.Month;

                months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
                ComboMonth.DataSource = months;
                ComboMonth.SelectedIndex = currentMonth - 1;

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
                status.Visible = false;
                connected = true;
            }
            catch(Exception ex)
            {
                connected = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            if (selectedYear <= admDate.Year)
            {
                if (selectedMonth < admDate.Month)
                {
                    //proceed = false;
                    MessageBox.Show("Not valid Month for payment Check","Warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        sc = new MySqlCommand("select * from std_payment where id = '" + StdDBselectclassid.thisID + "' and year = " + selectedYear + " and month = " + selectedMonth + ";", connection);
                        reader = sc.ExecuteReader();

                        if (reader.Read())
                        {
                            //proceed = false;
                            //MessageBox.Show(Paymentselectclassid.thisID + " is already PAID!!! for " + months[selectedMonth - 1] + " " + selectedYear.ToString(), "Payment Acknowledgement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            status.Text = "PAID!!!";
                            status.ForeColor = Color.LawnGreen;
                            status.Visible = true;
                            paylbl.Visible = true;
                            DateTime dt = (DateTime)reader["payment_date"];
                            paydate.Text = dt.ToShortDateString();
                            paydate.Visible = true;
                            amountlbl.Visible = true;
                            amount.Text = reader["amount"].ToString();
                            amount.Visible = true;
                            amountlbl.Visible = true;
                        }
                        else
                        {
                            status.Text = "UNPAID!!!";
                            status.ForeColor = Color.Red;
                            status.Visible = true;
                            paylbl.Visible = false;
                            //DateTime dt = (DateTime)reader["payment_date"];
                            //paydate.Text = dt.ToShortDateString();
                            paydate.Visible = false;
                            amount.Visible = false;
                            amountlbl.Visible = false;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    sc.Dispose();
                    reader.Dispose();
                }
            }
            else
            {
                status.Text = "UNPAID!!!";
                status.ForeColor = Color.Red;
                status.Visible = true;
                paylbl.Visible = false;
                //DateTime dt = (DateTime)reader["payment_date"];
                //paydate.Text = dt.ToShortDateString();
                paydate.Visible = false;
                amount.Visible = false;
                amountlbl.Visible = false;
            }
            if (this.btn1Click != null)
            {
                clickedButton = Check;
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

        private void ComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedYear = (int)ComboYear.SelectedValue;
        }

        private void ComboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMonth = ComboMonth.SelectedIndex + 1;
        }
    }
}
