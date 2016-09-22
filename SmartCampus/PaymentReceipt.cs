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
    public partial class PaymentReceipt : UserControl
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

        //rates for taking payment (now temporarily fixed)
        private int tution=100;
        private int due=100;
        private int admfee=1000;
        private int duefine=20;
        private int readmfee=500;
        private int absentfee=50;
        private int session=1200;
        private int examfee=300;
        private int regfee=400;
        private int centerfee=500;
        private int caution=100;
        private int idcost=250;
        private int msg=50;
        //for calculation
        private int subtotal;
        private int extra;
        private int total;
        //for extra payment options
        private int computer;
        private int lab;
        private int cocur;
        private int transport;
        private int hostel;
        private int others;

        private DateTime date;


        private String[] months;
        //For checking valid payment and storing to db and printing
        public bool takepayment = false;


        public PaymentReceipt()
        {
            InitializeComponent();
            SetVariables();
        }

        //Sets all the UI and calculation variables
        public void SetVariables()
        {
            //initializing variables
            takepayment = false;
            checkBoxtution.Checked = false;
            checkBoxdue.Checked = false;
            checkBoxadmfee.Checked = false;
            checkBoxduefine.Checked = false;
            checkBoxreadmfee.Checked = false;
            checkBoxabsent.Checked = false;
            checkBoxsession.Checked = false;
            checkBoxexamfee.Checked = false;
            checkBoxregfee.Checked = false;
            checkBoxcenter.Checked = false;
            checkBoxcaution.Checked = false;
            checkBoxidcost.Checked = false;
            checkBoxmsg.Checked = false;
            Tution.Text = "0";
            Due.Text = "0";
            Admfee.Text = "0";
            Duefine.Text = "0";
            Readmfee.Text = "0";
            Absentfee.Text = "0";
            Session.Text = "0";
            Xm.Text = "0";
            Regfee.Text = "0";
            Centerfee.Text = "0";
            Caution.Text = "0";
            IDc.Text = "0";
            Msg.Text = "0";
            Total.Text = "0";
            Subtotal.Text = "0";
            Extra.Text = "0";
            computer = 0;
            lab = 0;
            cocur = 0;
            transport = 0;
            hostel = 0;
            others = 0;
            Computer.Value = computer;
            Lab.Value = lab;
            Cocur.Value = cocur;
            Trans.Value = transport;
            Hostel.Value = hostel;
            Others.Value = others;
            subtotal = 0;
            extra = 0;
            total = 0;
        }

        /*Loader of this UC
         *connects to the db and set the basic info of student and payment in the UI
        */
        private void PaymentReceipt_Load(object sender, EventArgs e)
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
                SetVariables();
                sc = new MySqlCommand("select * from student_info where class='" + Paymentselectclassid.thisclass + "'and id='" + Paymentselectclassid.thisID + "';", connection);
                reader = sc.ExecuteReader();
                reader.Read();
                stdName.Text = reader["name"].ToString();
                stdclass.Text = reader["class"].ToString();
                stdsec.Text = reader["sec"].ToString();
                payyear.Text = StudentPayment.selectedYear.ToString();

                months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;

                paymonth.Text = months[StudentPayment.selectedMonth - 1];

                date = DateTime.Now;

                paydate.Text = date.ToShortDateString();

                sc.Dispose();
                reader.Dispose();


                sc = new MySqlCommand("select * from std_payment_rate where class='" + Paymentselectclassid.thisclass + "';", connection);
                Console.WriteLine("select * from std_payment_rate where class='" + Paymentselectclassid.thisclass + "';");
                reader = sc.ExecuteReader();
                reader.Read();

                tution = (int)reader[1];
                due = (int)reader[2];
                admfee = (int)reader[3];
                duefine = (int)reader[4];
                readmfee = (int)reader[5];
                absentfee = (int)reader[6];
                session = (int)reader[7];
                examfee = (int)reader[8];
                regfee = (int)reader[9];
                centerfee = (int)reader[10];
                caution = (int)reader[11];
                idcost = (int)reader[12];
                msg = (int)reader[13];
                sc.Dispose();
                reader.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Check payment is correct or not and set the takepayment variable
        private void Paid_Click(object sender, EventArgs e)
        {
            if (total != 0 && subtotal != 0)
            {
                try
                {
                    DialogResult dr = MessageBox.Show("Do you want to take this payment?", "Confirmation!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        takepayment = true;
                        MySqlCommand cmd = new MySqlCommand("insert into std_payment values(@id,@year,@month,@date,@amount);", connection);
                        cmd.Parameters.AddWithValue("@id", Paymentselectclassid.thisID);
                        cmd.Parameters.AddWithValue("@year", Paymentselectclassid.paymentYear);
                        cmd.Parameters.AddWithValue("@month", Paymentselectclassid.paymentMonth);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@amount", subtotal + extra);
                        cmd.ExecuteNonQuery();

                        //initialize word object  
                        var document = new Document();

                        document.LoadFromFile(@"..\..\Images\StdPayment.docx");
                        //get strings to replace  
                        Dictionary<string, string> dictReplace = new Dictionary<string, string>(GetReplaceDictionary());
                        //Replace text  
                        foreach (KeyValuePair<string, string> kvp in dictReplace)
                        {
                            document.Replace(kvp.Key, kvp.Value, true, true);
                        }

                        document.SaveToFile(@"..\..\Images\StdPayment1.pdf", Spire.Doc.FileFormat.PDF);
                        document.Close();

                        PrintPDF(@"..\..\Images\StdPayment1.pdf");
                    }
                    else
                    {
                        takepayment = false;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    takepayment = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid amount!!!!","Check",MessageBoxButtons.OK,MessageBoxIcon.Error);
                takepayment = false;
            }
            if (this.btn1Click != null)
            {
                clickedButton = TakePayment;
                this.btn1Click(this, e);
            }
            //SetVariables();
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
            replaceDict.Add("#name#", stdName.Text.Trim());
            replaceDict.Add("#class#", stdclass.Text.Trim());
            replaceDict.Add("#sec#", stdsec.Text.Trim());
            replaceDict.Add("#month#", paymonth.Text.Trim());
            replaceDict.Add("#year#", payyear.Text.Trim());
            replaceDict.Add("#date#", paydate.Text.Trim());
            replaceDict.Add("#tuition#", Tution.Text.Trim());
            replaceDict.Add("#due#", Due.Text.Trim());
            replaceDict.Add("#adm#", Admfee.Text.Trim());
            replaceDict.Add("#duefine#", Duefine.Text.Trim());
            replaceDict.Add("#readm#", Readmfee.Text.Trim());
            replaceDict.Add("#abs#", Absentfee.Text.Trim());
            replaceDict.Add("#session#", Session.Text.Trim());
            replaceDict.Add("#exam#", Xm.Text.Trim());
            replaceDict.Add("#reg#", Regfee.Text.Trim());
            replaceDict.Add("#center#", Centerfee.Text.Trim());
            replaceDict.Add("#caution#", Caution.Text.Trim());
            replaceDict.Add("#id#", IDc.Text.Trim());
            replaceDict.Add("#mess#", Msg.Text.Trim());
            replaceDict.Add("#com#", Computer.Value.ToString().Trim());
            replaceDict.Add("#lab#", Lab.Value.ToString().Trim());
            replaceDict.Add("#cocur#", Cocur.Value.ToString().Trim());
            replaceDict.Add("#trans#", Trans.Value.ToString().Trim());
            replaceDict.Add("#hostel#", Hostel.Value.ToString().Trim());
            replaceDict.Add("#other#", Others.Value.ToString().Trim());
            replaceDict.Add("#sub#", Subtotal.Text.Trim());
            replaceDict.Add("#extras#", Extra.Text.Trim());
            replaceDict.Add("#total#", Total.Text.Trim());

            return replaceDict;
        }  

        //These functions are for keeping track of UI changes
        private void Computer_ValueChanged(object sender, EventArgs e)
        {
            computer = (int)Computer.Value;
            extra = computer + lab + cocur + transport + hostel + others;
            Extra.Text = extra.ToString();
        }

        private void Lab_ValueChanged(object sender, EventArgs e)
        {
            lab = (int)Lab.Value;
            extra = computer + lab + cocur + transport + hostel + others;
            Extra.Text = extra.ToString();
        }

        private void Cocur_ValueChanged(object sender, EventArgs e)
        {
            cocur = (int)Cocur.Value;
            extra = computer + lab + cocur + transport + hostel + others;
            Extra.Text = extra.ToString();
        }

        private void Trans_ValueChanged(object sender, EventArgs e)
        {
            transport = (int)Trans.Value;
            extra = computer + lab + cocur + transport + hostel + others;
            Extra.Text = extra.ToString();
        }

        private void Hostel_ValueChanged_1(object sender, EventArgs e)
        {
            hostel = (int)Hostel.Value;
            extra = computer + lab + cocur + transport + hostel + others;
            Extra.Text = extra.ToString();
        }

        private void Others_ValueChanged(object sender, EventArgs e)
        {
            others = (int)Others.Value;
            extra = computer + lab + cocur + transport + hostel + others;
            Extra.Text = extra.ToString();
        }

        private void checkBoxtution_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxtution.Checked == true)
            {
                Tution.Text = tution.ToString();
                total += tution;
                subtotal += tution;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Tution.Text = "0";
                total -= tution;
                subtotal -= tution;
                Total.Text = (total+extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxdue_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxdue.Checked == true)
            {
                Due.Text = due.ToString();
                total += due;
                subtotal += due;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Due.Text = "0";
                total -= due;
                subtotal -= due;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }
        private void Extra_TextChanged(object sender, EventArgs e)
        {
            Total.Text = (total + extra).ToString();
        }

        private void checkBoxadmfee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxadmfee.Checked == true)
            {
                Admfee.Text = admfee.ToString();
                total += admfee;
                subtotal += admfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else 
            {
                Admfee.Text = "0";
                total -= admfee;
                subtotal -= admfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxduefine_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxduefine.Checked == true)
            {
                Duefine.Text = duefine.ToString();
                total += duefine;
                subtotal += duefine;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Duefine.Text = "0";
                total -= duefine;
                subtotal -= duefine;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxreadmfee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxreadmfee.Checked == true)
            {
                Readmfee.Text = readmfee.ToString();
                total += readmfee;
                subtotal += readmfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Readmfee.Text = "0";
                total -= readmfee;
                subtotal -= readmfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxabsent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxabsent.Checked == true)
            {
                Absentfee.Text = absentfee.ToString();
                total += absentfee;
                subtotal += absentfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Absentfee.Text = "0";
                total -= absentfee;
                subtotal -= absentfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxsession_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxsession.Checked == true)
            {
                Session.Text = session.ToString();
                total += session;
                subtotal += session;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Session.Text = "0";
                total -= session;
                subtotal -= session;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxexamfee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxexamfee.Checked == true)
            {
                Xm.Text = examfee.ToString();
                total += examfee;
                subtotal += examfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Xm.Text = "0";
                total -= examfee;
                subtotal -= examfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxregfee_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxregfee.Checked == true)
            {
                Regfee.Text = regfee.ToString();
                total += regfee;
                subtotal += regfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Regfee.Text = "0";
                total -= regfee;
                subtotal -= regfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxcenter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxcenter.Checked == true)
            {
                Centerfee.Text = centerfee.ToString();
                total += centerfee;
                subtotal += centerfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Centerfee.Text = "0";
                total -= centerfee;
                subtotal -= centerfee;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxcaution_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxcaution.Checked == true)
            {
                Caution.Text = caution.ToString();
                total += caution;
                subtotal += caution;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Caution.Text = "0";
                total -= caution;
                subtotal -= caution;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxidcost_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxidcost.Checked == true)
            {
                IDc.Text = idcost.ToString();
                total += idcost;
                subtotal += idcost;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                IDc.Text = "0";
                total -= idcost;
                subtotal -= idcost;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        private void checkBoxmsg_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxmsg.Checked == true)
            {
                Msg.Text = msg.ToString();
                total += msg;
                subtotal += msg;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
            else
            {
                Msg.Text = "0";
                total -= msg;
                subtotal -= msg;
                Total.Text = (total + extra).ToString();
                Subtotal.Text = subtotal.ToString();
            }
        }

        //to go back to UC StudentPayment
        private void Back_Click(object sender, EventArgs e)
        {
            SetVariables();
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }
    }
}
