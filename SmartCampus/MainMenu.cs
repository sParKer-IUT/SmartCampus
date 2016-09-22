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

namespace SmartCampus
{
    public partial class MainMenu : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        MySqlConnection connection;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void Accounts_Click(object sender, EventArgs e)
        {
            PasswordForm pass = new PasswordForm();
            if (pass.ShowDialog() == DialogResult.OK)
            {
                if (AllPasswords.inputPass.Equals(AllPasswords.accountsPass) || AllPasswords.inputPass.Equals(AllPasswords.masterPass))
                {
                    if (this.btn1Click != null)
                    {
                        clickedButton = Accounts;
                        this.btn1Click(this, e);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password!!!");
                }
            }
        }

        private void Admission_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Admission;
                this.btn1Click(this, e);
            }
        }

        private void StudentDB_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = StudentDB;
                this.btn1Click(this, e);
            }
        }

        private void Employee_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Employee;
                this.btn1Click(this, e);
            }
        }

        private void Result_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Result;
                this.btn1Click(this, e);
            }
        }

        private void DailyAtt_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = DailyAtt;
                this.btn1Click(this, e);
            }
        }

    }
}
