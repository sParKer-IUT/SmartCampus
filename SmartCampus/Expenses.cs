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
    public partial class Expenses : UserControl
    {

        public event EventHandler btn1Click;

        public Button clickedButton;

        //for checking payment is valid or not
        public bool save;

        //For Database connection
        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;

        //for the UI and calculation
        private DateTime Date;
        private int[] years;
        private String[] months;
        private int currentYear;
        private int currentMonth;
        public static int selectedYear;
        public static int selectedMonth;
        private int index;
        private int i;
        private int house;
        private int electricity;
        private int gas;
        private int internet;
        private int entertainment;
        private int decoration;
        private int transport;
        private int stationary;
        private int ad;
        private int others;
        private int total;
        

        //for Database operations
        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;

        public Expenses()
        {
            InitializeComponent();
        }


        private void Expenses_Load(object sender, EventArgs e)
        {
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            save = false;
            house = 0;
            electricity = 0;
            gas = 0;
            internet = 0;
            entertainment = 0;
            decoration = 0;
            transport = 0;
            stationary = 0;
            ad = 0;
            others = 0;
            total = 0;

            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;
            Date = DateTime.Now;
            months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
            ComboMonth.DataSource = months;
            ComboMonth.SelectedIndex = currentMonth - 1;
            
            index = 0;
            years = new int[currentYear - 2015 + 1];
            for (i = currentYear; i >= 2015; i--)
            {
                years[index] = i;
                index++;
            }

            ComboYear.DataSource = years;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (total > 0)
            {
                try
                {
                    sc = new MySqlCommand("select * from expenses where year = " + ComboYear.SelectedValue + " and month = " + (ComboMonth.SelectedIndex + 1) + ";", connection);
                    reader = sc.ExecuteReader();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    save = false;
                    return;
                }
                if (reader.Read())
                {
                    save = false;
                    MessageBox.Show(ComboYear.SelectedValue + " year " + months[ComboMonth.SelectedIndex] + " month's Expenses is already updated ", "Expenses Acknowledgement", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sc.Dispose();
                    reader.Dispose();
                }
                else
                {
                    sc.Dispose();
                    reader.Dispose();
                    DialogResult dr = MessageBox.Show("Do you want to Save?", "Confirmation!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        save = true;
                        MySqlCommand cmd = new MySqlCommand("insert into expenses values(@year,@month,@date,@amount);", connection);
                        cmd.Parameters.AddWithValue("@year", ComboYear.SelectedValue);
                        cmd.Parameters.AddWithValue("@month", (ComboMonth.SelectedIndex + 1));
                        cmd.Parameters.AddWithValue("@date", Date);
                        cmd.Parameters.AddWithValue("@amount", total);
                        cmd.ExecuteNonQuery();

                        //initialize word object  
                        var document = new Document();

                        document.LoadFromFile(@"..\..\Images\Expenses.docx");
                        //get strings to replace  
                        Dictionary<string, string> dictReplace = new Dictionary<string,string>(GetReplaceDictionary());
                        //Replace text  
                        foreach (KeyValuePair<string, string> kvp in dictReplace)
                        {
                            document.Replace(kvp.Key, kvp.Value, true, true);
                        }

                        document.SaveToFile(@"..\..\Images\Expenses1.pdf", Spire.Doc.FileFormat.PDF);
                        document.Close();

                        PrintPDF(@"..\..\Images\Expenses1.pdf");

                    }
                    else
                    {
                        save = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid total expenses", "Invalid Amount!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (this.btn1Click != null)
            {
                clickedButton = Save;
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
            replaceDict.Add("#year#", ComboYear.SelectedItem.ToString().Trim());
            replaceDict.Add("#month#", ComboMonth.SelectedItem.ToString().Trim());
            replaceDict.Add("#house#", House.Value.ToString().Trim());
            replaceDict.Add("#electricity#", Electricity.Value.ToString().Trim());
            replaceDict.Add("#gas#", Gas.Value.ToString().Trim());
            replaceDict.Add("#internet#", Internet.Value.ToString().Trim());
            replaceDict.Add("#entertain#", Entertain.Value.ToString().Trim());
            replaceDict.Add("#decor#", Decoration.Value.ToString().Trim());
            replaceDict.Add("#transport#", Transport.Value.ToString().Trim());
            replaceDict.Add("#stationary#", Stationary.Value.ToString().Trim());
            replaceDict.Add("#ad#", Ad.Value.ToString().Trim());
            replaceDict.Add("#others#", Others.Value.ToString().Trim());
            replaceDict.Add("#total#", Total.Text.Trim());

            return replaceDict;
        } 

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        private void House_ValueChanged(object sender, EventArgs e)
        {
            house = (int)House.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Electricity_ValueChanged(object sender, EventArgs e)
        {
            electricity = (int)Electricity.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Gas_ValueChanged(object sender, EventArgs e)
        {
            gas = (int)Gas.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Internet_ValueChanged(object sender, EventArgs e)
        {
            internet = (int)Internet.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Entertain_ValueChanged(object sender, EventArgs e)
        {
            entertainment = (int)Entertain.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Decoration_ValueChanged(object sender, EventArgs e)
        {
            decoration = (int)Decoration.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Transport_ValueChanged(object sender, EventArgs e)
        {
            transport = (int)Transport.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Stationary_ValueChanged(object sender, EventArgs e)
        {
            stationary = (int)Stationary.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Ad_ValueChanged(object sender, EventArgs e)
        {
            ad = (int)Ad.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }

        private void Others_ValueChanged(object sender, EventArgs e)
        {
            others = (int)Others.Value;
            total = house + electricity + gas + internet + entertainment + decoration + transport + stationary + ad + others;
            Total.Text = total.ToString();
        }
    }
}
