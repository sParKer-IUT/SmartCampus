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
    public partial class EnterMarksMain : UserControl
    {
        public event EventHandler enterMarksMainBtnClicked;
        public Button clickedButton;

        string server;
        string database;
        string uid;
        string password;
        MySqlConnection connection;

        public EnterMarksMain()
        {
            InitializeComponent();
            cbxExamType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSemester.DropDownStyle = ComboBoxStyle.DropDownList;

            cbxExamType.SelectedIndex = 0;
            cbxSemester.SelectedIndex = 0;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            clickedButton = btnEnter;
            
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                string query = "INSERT INTO marks (Class, ID, Year, Semester, SubjectCode, Exam, Marks) VALUES ('"+EnterMarksInfo.selectedClassNumber+"', '"+tbxID.Text+"', '"+DateTime.Now.Year+"', '"+cbxSemester.SelectedItem.ToString()+"', '"+EnterMarksInfo.selectedSubjectCode+"', '"+cbxExamType.SelectedItem.ToString().GetExamCode()+"', '"+tbxMarks.Text+"');";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command;
                MySqlDataReader reader;

                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();

                MessageBox.Show("Year: "+DateTime.Now.Year+" Class: "+EnterMarksInfo.selectedClassNumber.GetClassName()+" ID: "+tbxID.Text+" Semester: Subject: Exam: Marks: inserted");

                command.Dispose();
                reader.Dispose();
                connection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(enterMarksMainBtnClicked != null)
            {
                clickedButton = btnBack;
                enterMarksMainBtnClicked(this, e);
            }
        }
    }
}
