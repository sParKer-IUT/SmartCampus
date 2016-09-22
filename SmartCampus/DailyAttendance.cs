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
    public partial class DailyAttendance : UserControl
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

        public DailyAttendance()
        {
            InitializeComponent();
        }

        
        


        

        private void viewAtt_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = viewAtt;
                this.btn1Click(this, e);
            }
            /*server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();

            sc = new MySqlCommand("SELECT a.id,a.name,b.time FROM student_info a, dailyattendance b WHERE a.class = @cls and a.card_id=b.id and b.time=@tim", connection);
            sc.Parameters.AddWithValue("@cls", AttendanceShow.sClass);
            //sc.Parameters.AddWithValue("@tim", Convert.ToString(AttendanceShow.time));
            reader = sc.ExecuteReader();*/
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = back;
                this.btn1Click(this, e);
            }
        }

        

        private void DailyAttendance_Load(object sender, EventArgs e)
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

            AttendanceShow.sClass = ComboClass.SelectedValue.ToString();

            sc.Dispose();
            reader.Dispose();

            /*sc = new MySqlCommand("select MIN(adm_date) from student_info where class='" + AttendanceShow.sClass + "';", connection);
            reader = sc.ExecuteReader();
            reader.Read();
            DateTime admDate;
            admDate = (DateTime)reader[0];
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
            connection.Close();
            SelectedMonth = ComboMonth.SelectedIndex + 1;
            SelectedYear = (int)ComboYear.SelectedValue;
            */
        }


        private void ComboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            AttendanceShow.sClass = ComboClass.SelectedValue.ToString();
        }

        private void viewCO_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = viewCO;
                this.btn1Click(this, e);
            }
        }
    }
    public class AttendanceShow
    {
        public static String sClass;
    }
}
