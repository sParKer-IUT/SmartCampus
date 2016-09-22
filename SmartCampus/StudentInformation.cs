using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using Spire.Doc;
using iTextSharp.text.pdf;
using System.Drawing.Printing; 

namespace SmartCampus
{
    public partial class StudentInformation : UserControl
    {
        public event EventHandler btnClick;

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

        bool connected = false;

        public StudentInformation()
        {
            InitializeComponent();
        }

        private void StudentInformation_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;
                string msg;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                sc = new MySqlCommand("select * from student_info where class='" + StdDBselectclassid.thisclass + "'and id='" + StdDBselectclassid.thisID + "';", connection);
                reader = sc.ExecuteReader();
                reader.Read();
                DateTime tempdob = (DateTime)reader["dob"];
                DateTime tempadm = (DateTime)reader["adm_date"];
                if (reader["name"].ToString() != "") StdName.Text = reader["name"].ToString();
                if (reader["fathers_name"].ToString() != "") FathersName.Text = reader["fathers_name"].ToString();
                if (reader["mothers_name"].ToString() != "") MothersName.Text = reader["mothers_name"].ToString();
                if (reader["fathers_ocupation"].ToString() != "") Focup.Text = reader["fathers_ocupation"].ToString();
                if (reader["mothers_ocupation"].ToString() != "") Mocup.Text = reader["mothers_ocupation"].ToString();
                if (reader["dob"].ToString() != "") Dob.Text = tempdob.ToShortDateString();
                if (reader["sex"].ToString() != "") Sex.Text = reader["sex"].ToString();
                if (reader["nationality"].ToString() != "") Nationality.Text = reader["nationality"].ToString();
                if (reader["religion"].ToString() != "") Religion.Text = reader["religion"].ToString();
                if (reader["class"].ToString() != "") StdClass.Text = reader["class"].ToString();
                if (reader["sec"].ToString() != "") Section.Text = reader["sec"].ToString();
                if (reader["present_address"].ToString() != "") PresentAdd.Text = reader["present_address"].ToString();
                if (reader["permanent_address"].ToString() != "") PermanentAdd.Text = reader["permanent_address"].ToString();
                if (reader["local_gurdian"].ToString() != "") Lclgur.Text = reader["local_gurdian"].ToString();
                if (reader["phn_land"].ToString() != "") Land.Text = reader["phn_land"].ToString();
                if (reader["phn_father"].ToString() != "") Fmob.Text = reader["phn_father"].ToString();
                if (reader["phn_mother"].ToString() != "") Mmob.Text = reader["phn_mother"].ToString();
                if (reader["phn_gurdian"].ToString() != "") Gmob.Text = reader["phn_gurdian"].ToString();
                if (reader["nat_id_father"].ToString() != "") Fnat.Text = reader["nat_id_father"].ToString();
                if (reader["nat_id_mother"].ToString() != "") Mnat.Text = reader["nat_id_mother"].ToString();
                if (reader["nat_id_gurdian"].ToString() != "") Gnat.Text = reader["nat_id_gurdian"].ToString();
                if (reader["email"].ToString() != "") Email.Text = reader["email"].ToString();
                if (reader["photo"].ToString() != "") pictureBox1.ImageLocation = reader["photo"].ToString();
                if (reader["cocurricular"].ToString() != "") Cocur.Text = reader["cocurricular"].ToString();
                if (reader["hobby"].ToString() != "") Hobby.Text = reader["hobby"].ToString();
                if (reader["st_group"].ToString() != "") Group.Text = reader["st_group"].ToString();
                if (reader["bloodgrp"].ToString() != "") Blood.Text = reader["bloodgrp"].ToString();
                if (reader["adm_date"].ToString() != "") AdmDate.Text = tempadm.ToShortDateString();
                msg = reader["messaging_number"].ToString();
                Msg.Text = msg;

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
            if (this.btnClick != null)
            {
                clickedButton = Back;
                this.btnClick(this, e);
            }
        }

        private void PrintPDF(string path)
        {
            using (Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument())
            {
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

                    using (PrintDocument printDoc = doc.PrintDocument)
                    {
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
            }
        }

        Dictionary<string, string> GetReplaceDictionary()
        {
            Dictionary<string, string> replaceDict = new Dictionary<string, string>();
            replaceDict.Clear();
            replaceDict.Add("#name#", StdName.Text.Trim());
            replaceDict.Add("#fathersname#", FathersName.Text.Trim());
            replaceDict.Add("#mothersname#", MothersName.Text.Trim());
            replaceDict.Add("#dob#", Dob.Text.Trim());
            replaceDict.Add("#religion#", Religion.Text.Trim());
            replaceDict.Add("#nationality#", Nationality.Text.Trim());
            replaceDict.Add("#sex#", Sex.Text.Trim());
            replaceDict.Add("#focup#", Focup.Text.Trim());
            replaceDict.Add("#mocup#", Mocup.Text.Trim());
            replaceDict.Add("#presentaddress#", PresentAdd.Text.Trim());
            replaceDict.Add("#permanentaddress#", PermanentAdd.Text.Trim());
            replaceDict.Add("#admdate#", AdmDate.Text.Trim());
            replaceDict.Add("#class#", StdClass.Text.Trim());
            replaceDict.Add("#section#", Section.Text.Trim());
            replaceDict.Add("#lclguardian#", Lclgur.Text.Trim());
            replaceDict.Add("#landphn#", Land.Text.Trim());
            replaceDict.Add("#fphn#", Fmob.Text.Trim());
            replaceDict.Add("#mphn#", Mmob.Text.Trim());
            replaceDict.Add("#gphn#", Gmob.Text.Trim());
            replaceDict.Add("#msgphn#", Msg.Text.Trim());
            replaceDict.Add("#fnid#", Fnat.Text.Trim());
            replaceDict.Add("#mnid#", Mnat.Text.Trim());
            replaceDict.Add("#gnid#", Gnat.Text.Trim());
            replaceDict.Add("#email#", Email.Text.Trim());
            replaceDict.Add("#bldgrp#", Blood.Text.Trim());
            replaceDict.Add("#grp#", Group.Text.Trim());
            replaceDict.Add("#hobby#", Hobby.Text.Trim());
            replaceDict.Add("#cocur#", Cocur.Text.Trim());

            return replaceDict;
        }  

        private void Edit_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            if (this.btnClick != null)
            {
                clickedButton = Edit;
                this.btnClick(this, e);
            }
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            if (!connected) return;

            //initialize word object  
            using (var document = new Document())
            {
                document.LoadFromFile(@"..\..\Images\AddmissionForm.docx");
                //get strings to replace  
                Dictionary<string, string> dictReplace = new Dictionary<string, string>(GetReplaceDictionary());
                //Replace text  
                foreach (KeyValuePair<string, string> kvp in dictReplace)
                {
                    document.Replace(kvp.Key, kvp.Value, true, true);
                }

                document.SaveToFile(@"..\..\Images\AddmissionForm1.pdf", Spire.Doc.FileFormat.PDF);
                document.Close();
                dictReplace.Clear();
            }
            using (Stream inputPdfStream = new FileStream(@"..\..\Images\AddmissionForm1.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream(pictureBox1.Image == pictureBox1.ErrorImage ? @"..\..\Images\smart office.png" : pictureBox1.ImageLocation, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(@"..\..\Images\test.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                image.ScalePercent(15, 24);
                //image.ScaleToFit(120, 120);
                image.SetAbsolutePosition(470, 540);
                pdfContentByte.AddImage(image);
                stamper.Close();
                reader.Close();
            }
            PrintPDF(@"..\..\Images\test.pdf");
        }

    }
}
