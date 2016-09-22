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
    public partial class EnterMarks : UserControl
    {
        public event EventHandler enterMarksBtnClick;
        public Button clickedButton;

        string server;
        string database;
        string uid;
        string password;
        MySqlConnection connection;

        public EnterMarks()
        {
            InitializeComponent();
            cbxClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSubject.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void EnterMarks_Load(object sender, EventArgs e)
        {
            cbxClass.SelectedIndex = 0;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            if (enterMarksBtnClick != null)
            {
                clickedButton = Submit;
                enterMarksBtnClick(this, e);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (enterMarksBtnClick != null)
            {
                clickedButton = Back;
                enterMarksBtnClick(this, e);
            }
        }

        private void cbxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnterMarksInfo.selectedClassNumber = cbxClass.SelectedItem.ToString().GetClassNumber();

            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                string query = "SELECT SubjectCode FROM subjects WHERE Class = '" + cbxClass.SelectedItem.ToString().GetClassNumber() + "' order by SubjectCode;";
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command;
                MySqlDataReader reader;

                command = new MySqlCommand(query, connection);
                reader = command.ExecuteReader();

                cbxSubject.Items.Clear();
                while (reader.Read())
                {
                    string a = (string)reader[0];
                    cbxSubject.Items.Add(a.GetSubjectName());
                }
                if (cbxSubject.Items.Count != 0) cbxSubject.SelectedIndex = 0;

                command.Dispose();
                reader.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnterMarksInfo.selectedSubjectCode = cbxSubject.SelectedItem.ToString().GetSubjectCode();
        }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public class EnterMarksInfo
    {
        public static int selectedClassNumber;
        public static string selectedSubjectCode;
    }
}
