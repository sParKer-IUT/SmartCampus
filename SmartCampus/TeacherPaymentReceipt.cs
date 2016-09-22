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
using System.IO;
using Spire.Doc;
using iTextSharp.text.pdf;
using System.Diagnostics;
using Spire.Pdf;
using Spire.Pdf.Annotations;
using Spire.Pdf.Widget;
using System.Drawing.Printing;

namespace SmartCampus
{
    public partial class TeacherPaymentReceipt : UserControl
    {

        public event EventHandler btn1Click;

        public Button clickedButton;

        //for db connection
        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;

        //for db operations
        MySqlDataReader reader;
        MySqlCommand sc;

        private int less;
        private int total;
        private int salary;
        private int bonus;
        private int incentive;
        private int others;

        private DateTime date;


        private String[] months;
        //For checking valid payment and storing to db and printing
        public bool givepayment = false;

        bool connected = false;

        public TeacherPaymentReceipt()
        {
            InitializeComponent();
            SetVariables();
        }

        public void SetVariables()
        {
            //initializing variables
            givepayment = false;
            Total.Text = "0";
            salary = 0;
            bonus = 0;
            incentive = 0;
            less = 0;
            others = 0;
            Salary.Value = salary;
            Bonus.Value = bonus;
            Incentive.Value = incentive;
            Less.Value = less;
            Others.Value = others;
            total = 0;
        }

        private void TeacherPaymentReceipt_Load(object sender, EventArgs e)
        {
            SetVariables();
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;

            try
            {
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                sc = new MySqlCommand("select * from employee_info where department='" + Paymentselecttchrdeptid.thisdept + "'and id='" + Paymentselecttchrdeptid.thisID + "';", connection);
                reader = sc.ExecuteReader();
                reader.Read();
                empName.Text = reader["name"].ToString();
                empdept.Text = reader["department"].ToString();
                empID.Text = reader["id"].ToString();
                payyear.Text = TeacherPayment.selectedYear.ToString();

                months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;

                paymonth.Text = months[TeacherPayment.selectedMonth - 1];

                date = DateTime.Now;

                paydate.Text = date.ToShortDateString();

                sc.Dispose();
                reader.Dispose();
                connected = true;
            }
            catch(Exception ex)
            {
                connected = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            SetVariables();
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        private void Salary_ValueChanged(object sender, EventArgs e)
        {
            salary = (int)Salary.Value;
            total = salary + bonus + incentive + others - less;
            Total.Text = total.ToString();
        }

        private void Bonus_ValueChanged(object sender, EventArgs e)
        {
            bonus = (int)Bonus.Value;
            total = salary + bonus + incentive + others - less;
            Total.Text = total.ToString();
        }

        private void Incentive_ValueChanged(object sender, EventArgs e)
        {
            incentive = (int)Incentive.Value;
            total = salary + bonus + incentive + others - less;
            Total.Text = total.ToString();
        }

        private void Others_ValueChanged(object sender, EventArgs e)
        {
            others = (int)Others.Value;
            total = salary + bonus + incentive + others - less;
            Total.Text = total.ToString();
        }

        private void Less_ValueChanged(object sender, EventArgs e)
        {
            less = (int)Less.Value;
            total = salary + bonus + incentive + others - less;
            Total.Text = total.ToString();
        }

        private void Pay_Click(object sender, EventArgs e)
        {
            if (!connected) return;
            if (total > 0)
            {
                DialogResult dr = MessageBox.Show("Do you want to give this payment?", "Confirmation!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        givepayment = true;
                        MySqlCommand cmd = new MySqlCommand("insert into emp_payment values(@id,@year,@month,@date,@amount);", connection);
                        cmd.Parameters.AddWithValue("@id", Paymentselecttchrdeptid.thisID);
                        cmd.Parameters.AddWithValue("@year", Paymentselecttchrdeptid.paymentYear);
                        cmd.Parameters.AddWithValue("@month", Paymentselecttchrdeptid.paymentMonth);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@amount", total);
                        cmd.ExecuteNonQuery();
                        //initialize word object  
                        var document = new Document();

                        document.LoadFromFile(@"..\..\Images\TeacherPayment.docx");
                        //get strings to replace  
                        Dictionary<string, string> dictReplace = new Dictionary<string, string>(GetReplaceDictionary());
                        //Replace text  
                        foreach (KeyValuePair<string, string> kvp in dictReplace)
                        {
                            document.Replace(kvp.Key, kvp.Value, true, true);
                        }

                        document.SaveToFile(@"..\..\Images\TeacherPayment1.pdf", Spire.Doc.FileFormat.PDF);
                        document.Close();

                        PrintPDF(@"..\..\Images\TeacherPayment1.pdf");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        givepayment = false;
                    }
                }
                else
                {
                    givepayment = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid amount!!!!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                givepayment = false;
            }
            if (this.btn1Click != null)
            {
                clickedButton = Pay;
                this.btn1Click(this, e);
            }
        }

        private void PrintPDF(string path)
        {
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(path);

            PrintDialog dialogPrint = new PrintDialog();
            dialogPrint.AllowPrintToFile = true;
            dialogPrint.AllowSomePages = true;
            dialogPrint.PrinterSettings.MinimumPage = 1;
            dialogPrint.PrinterSettings.MaximumPage = doc.Pages.Count;
            dialogPrint.PrinterSettings.FromPage = 1;
            dialogPrint.PrinterSettings.ToPage = doc.Pages.Count;

            if (dialogPrint.ShowDialog() == DialogResult.OK)
            {
                doc.PrintFromPage = dialogPrint.PrinterSettings.FromPage;
                doc.PrintToPage = dialogPrint.PrinterSettings.ToPage;
                doc.PrinterName = dialogPrint.PrinterSettings.PrinterName;

                PrintDocument printDoc = doc.PrintDocument;
                dialogPrint.Document = printDoc;
                int exCount = 0;
                for (int i = 0; i < 1; i++)
                {
                    try
                    {
                        printDoc.Print();
                    }
                    catch (Exception ex)
                    {
                        if (!ex.GetType().ToString().Equals("System.ArgumentException"))
                        {
                            MessageBox.Show(ex.Message);
                            break;
                        }
                        exCount++;
                        i--;
                        if (exCount < 5)
                            continue;
                        else
                        {
                            MessageBox.Show("Printing Failed...");
                            break;
                        }
                    }
                }
            }
        }

        Dictionary<string, string> GetReplaceDictionary()
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Add("#name#", empName.Text.Trim());
            replaceDict.Add("#department#", empdept.Text.Trim());
            replaceDict.Add("#id#", empID.Text.Trim());
            replaceDict.Add("#month#", paymonth.Text.Trim());
            replaceDict.Add("#year#", payyear.Text.Trim());
            replaceDict.Add("#date#", paydate.Text.Trim());
            replaceDict.Add("#salary#", Salary.Value.ToString().Trim());
            replaceDict.Add("#bonus#", Bonus.Value.ToString().Trim());
            replaceDict.Add("#incentive#", Incentive.Value.ToString().Trim());
            replaceDict.Add("#others#", Others.Value.ToString().Trim());
            replaceDict.Add("#less#", Less.Value.ToString().Trim());
            replaceDict.Add("#total#", Total.Text.Trim());
            
            return replaceDict;
        } 
    }
}
