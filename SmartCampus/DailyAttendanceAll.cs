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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;

namespace SmartCampus
{
    public partial class DailyAttendanceAll : UserControl
    {
        public event EventHandler btn1Click;
        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlConnection connection;

        System.Globalization.CultureInfo oldCI;
        public DailyAttendanceAll()
        {
            InitializeComponent();
        }

        void SetNewCurrentCulture()
        {
            oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        //reset Current Culture back to the originale
        void ResetCurrentCulture()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        Dictionary<string, string> ReadExcelSheet(string filename)
        {
            Dictionary<string, string> attInfo = new Dictionary<string, string>();

            Excel.Workbook inputWorkBook;
            Excel.Sheets inputWorkSheets;
            Excel.Worksheet inputWSheet1;
            Excel.Application inputXL;

            inputXL = new Excel.Application();
            inputXL.Visible = false;
            inputXL.DisplayAlerts = false;
            inputWorkBook = inputXL.Workbooks.Open(filename, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            //string newFile = Path.GetDirectoryName(filename) + @"\" + Path.GetFileNameWithoutExtension(filename) + ".xlsx";
            //inputWorkBook.SaveAs(newFile);

            //inputWorkBook.Close(true, Missing.Value, Missing.Value);
            //inputWSheet1 = null;
            //inputWorkBook = null;
            //inputXL.Quit();

            //GC.WaitForPendingFinalizers();
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            //inputXL = new Excel.Application();
            //inputXL.Visible = false;
            //inputXL.DisplayAlerts = false;
            //inputWorkBook = inputXL.Workbooks.Open(newFile, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);


            //Get all the sheets in the workbook
            inputWorkSheets = inputWorkBook.Worksheets;

            //Get the allready exists sheet
            inputWSheet1 = (Excel.Worksheet)inputWorkSheets.get_Item(1);

            Excel.Range inputRange = inputWSheet1.UsedRange;
            for (int i = 1; i <= inputRange.Rows.Count; i++)
            {
                var value1 = (inputWSheet1.Cells[i, 1] as Excel.Range).Value;
                var value2 = (inputWSheet1.Cells[i, 2] as Excel.Range).Value;

                string cardID = value1.ToString();

                /*IF only Time
                DateTime conv = DateTime.FromOADate(value2);
                string time = conv.ToString();
                */

                /*If date time*/
                string time = value2.ToString();

                //Console.WriteLine("Card ID: " + value1.ToString());
                //Console.WriteLine("Time: " + time);

                attInfo[cardID] = time;
            }

            inputWorkBook.Close(true, Missing.Value, Missing.Value);
            inputWSheet1 = null;
            inputWorkBook = null;
            inputXL.Quit();

            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            return attInfo;
        }

        private void UploadAtt_Click(object sender, EventArgs e)
        {
            try
            {
                SetNewCurrentCulture();
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Select an excel sheet";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Dictionary<string, string> attInfo = ReadExcelSheet(fd.FileName);

                    int c = 0;

                    foreach (KeyValuePair<string, string> kvp in attInfo)
                    {
                        if (c == 0)
                        {
                            c++;
                            continue;
                        }
                        //Console.WriteLine("Card ID: " + kvp.Key);
                        //Console.WriteLine("Time: " + kvp.Value);
                        string query = "insert into dailyattendance values(@id,@time);";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@id", kvp.Key);
                        cmd.Parameters.AddWithValue("@time", kvp.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ResetCurrentCulture();
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = back;
                this.btn1Click(this, e);
            }
        }

        private void stdAtt_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = stdAtt;
                this.btn1Click(this, e);
            }
        }

        private void empAtt_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = empAtt;
                this.btn1Click(this, e);
            }
        }

        private void UploadCO_Click(object sender, EventArgs e)
        {
            try
            {
                SetNewCurrentCulture();
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                OpenFileDialog fd = new OpenFileDialog();
                fd.Title = "Select an excel sheet";
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    Dictionary<string, string> attInfo = ReadExcelSheet(fd.FileName);

                    int c = 0;

                    foreach (KeyValuePair<string, string> kvp in attInfo)
                    {
                        if (c == 0)
                        {
                            c++;
                            continue;
                        }
                        //Console.WriteLine("Card ID: " + kvp.Key);
                        //Console.WriteLine("Time: " + kvp.Value);
                        string query = "insert into dailycheckout values(@id,@time);";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@id", kvp.Key);
                        cmd.Parameters.AddWithValue("@time", kvp.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ResetCurrentCulture();
        }
    }
}
