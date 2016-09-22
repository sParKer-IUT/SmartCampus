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
using DGVPrinterHelper;

namespace SmartCampus
{
    public partial class EmpDailyAttendance : UserControl
    {
        public event EventHandler btn1Click;


        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlConnection connection;

        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;

        public int SelectedYear;
        public int SelectedMonth;
        public int SelectedDate;
        private int currentYear;
        private int currentMonth;
        private int index;
        private int i;
        private String[] months;
        private int[] years;
        private int daysInMonth;
        int[] days;

        string type;        //"in" or "out"

        string printSubtitle = "";

        public EmpDailyAttendance(string t)
        {
            InitializeComponent();
            type = t;
            if (type.Equals("in"))
            {
                rbPresent.Enabled = rbAbsent.Enabled = true;
            }
            else if (type.Equals("out"))
            {
                rbPresent.Enabled = rbAbsent.Enabled = false;
            }
        }

        private void EmpDailyAttendance_Load(object sender, EventArgs e)
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            sc = new MySqlCommand("select MIN(JoinDate) from employee_info;", connection);
            reader = sc.ExecuteReader();
            reader.Read();
            DateTime jDate;
            jDate = (DateTime)reader[0];
            reader.Close();
            currentYear = DateTime.Now.Year;
            currentMonth = DateTime.Now.Month;

            months = System.Globalization.DateTimeFormatInfo.InvariantInfo.MonthNames;
            ComboMonth.DataSource = months;
            ComboMonth.SelectedIndex = currentMonth - 1;

            index = 0;
            years = new int[currentYear - jDate.Year + 1];
            for (i = currentYear; i >= jDate.Year; i--)
            {
                years[index] = i;
                index++;
            }
            ComboYear.DataSource = years;

            sc.Dispose();
            reader.Dispose();
            
            SelectedMonth = ComboMonth.SelectedIndex + 1;
            SelectedYear = (int)ComboYear.SelectedValue;
            GetDaysInMonth();
            List<empData> Data = new List<empData>();
            string tempTime = "%" + ComboDay.SelectedItem.ToString() + "-" + months[SelectedMonth-1].Substring(0, 3) + "-" + ComboYear.SelectedValue.ToString().Substring(ComboYear.SelectedValue.ToString().Length - 2) + "%";
            string q = null;
            if(type.Equals("in")) q = "SELECT a.id,a.name,b.time FROM employee_info a, dailyattendance b WHERE a.card_id=b.id and b.time like '" + tempTime + "';";
            else if (type.Equals("out")) q = "SELECT a.id,a.name,b.time FROM employee_info a, dailycheckout b WHERE a.card_id=b.id and b.time like '" + tempTime + "';";
            sc = new MySqlCommand(q, connection);
            reader = sc.ExecuteReader();
            while(reader.Read())
            {
                Data.Add(new empData() { Name = reader[1].ToString(), ID = reader[0].ToString(), Time = reader[2].ToString().Substring(reader[2].ToString().Length - 10)});
            }
            dataGridView1.DataSource = Data;
            connection.Close();
        }
        void updateGrid()
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            List<empData> Data = new List<empData>();
            string tempTime = "%" + ComboDay.SelectedItem.ToString() + "-" + months[SelectedMonth - 1].Substring(0, 3) + "-" + ComboYear.SelectedValue.ToString().Substring(ComboYear.SelectedValue.ToString().Length - 2) + "%";
            string q = null;
            if (type.Equals("in"))
            {
                printSubtitle = "Present List"; 
                q = "SELECT a.id,a.name,b.time FROM employee_info a, dailyattendance b WHERE a.card_id=b.id and b.time like '" + tempTime + "';";
            }
            else if (type.Equals("out"))
            {
                printSubtitle = "Checkout List"; 
                q = "SELECT a.id,a.name,b.time FROM employee_info a, dailycheckout b WHERE a.card_id=b.id and b.time like '" + tempTime + "';";
            }

            string query = q;

            if (rbAbsent.Checked)
            {
                printSubtitle = "Absent List";
                query = "SELECT id, name FROM `employee_info` WHERE card_id NOT IN (SELECT id FROM `dailyattendance` WHERE time like '" + tempTime + "');";
            }

            printSubtitle += " of Employees in " + tempTime.ToString();
            
            sc = new MySqlCommand(query, connection);
            reader = sc.ExecuteReader();
            while (reader.Read())
            {
                string cintime;
                if (!rbAbsent.Checked) cintime = reader[2].ToString().Substring(reader[2].ToString().Length - 10);
                else cintime = "--";
                Data.Add(new empData() { Name = reader[1].ToString(), ID = reader[0].ToString(), Time = cintime });
            }
            dataGridView1.DataSource = Data;
            connection.Close();
        }

        private void GetDaysInMonth()
        {
            if (SelectedYear < 1 || SelectedYear > 9999) SelectedYear = 2001;

            daysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
            days = new int[daysInMonth];
            index = 0;
            for (i = 1; i <= daysInMonth; i++)
            {
                days[index] = i;
                index++;
            }
            ComboDay.DataSource = days;
            ComboDay.SelectedIndex = DateTime.Now.Day - 1;
        }

        private void ComboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedYear = (int)ComboYear.SelectedValue;
            GetDaysInMonth();
        }

        private void ComboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMonth = ComboMonth.SelectedIndex + 1;
            GetDaysInMonth();
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = back;
                this.btn1Click(this, e);
            }
        }

        private void Show_Click(object sender, EventArgs e)
        {
            updateGrid();
        }

        private void print_Click(object sender, EventArgs e)
        {
            /*PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Test Page Print";
                printDocument1.Print();
            }*/

            DGVPrinter printer = new DGVPrinter();

            printer.Title = "Shotabdi Biddyapith";

            printer.SubTitle = printSubtitle;

            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;

            printer.PageNumberInHeader = false;

            printer.PorportionalColumns = true;
            printer.ColumnWidth = DGVPrinter.ColumnWidthSetting.Porportional;
            
            printer.ColumnWidths.Add(dataGridView1.Columns[0].Name, 300);
            printer.ColumnWidths.Add(dataGridView1.Columns[1].Name, 200);
            printer.ColumnWidths.Add(dataGridView1.Columns[2].Name, 200);

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.Footer = "";

            printer.FooterSpacing = 15;


            //printer.PrintPreviewDataGridView(dataGridView1);
            printer.PrintDataGridView(dataGridView1);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*PaintEventArgs myPaintArgs = new PaintEventArgs(e.Graphics, new Rectangle(new Point(0, 0), this.Size));
            this.InvokePaint(dataGridView1, myPaintArgs);*/
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
    }

    class empData
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Time { get; set; }
    }
}
