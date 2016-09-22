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
    public partial class EmployeeInfoShow : UserControl
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

        public EmployeeInfoShow()
        {
            InitializeComponent();
        }

        private void EmployeeInfoShow_Load(object sender, EventArgs e)
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
                sc = new MySqlCommand("select * from employee_info where department='" + EmpDBselectdeptid.thisDept + "'and id='" + EmpDBselectdeptid.thisID + "';", connection);
                reader = sc.ExecuteReader();
                reader.Read();
                DateTime tempdob = (DateTime)reader["date_of_birth"];
                DateTime tempadm = (DateTime)reader["JoinDate"];
                if (reader["name"].ToString() != "") name.Text = reader["Name"].ToString();
                if (reader["fathers_name"].ToString() != "") fname.Text = reader["fathers_name"].ToString();
                if (reader["mothers_name"].ToString() != "") mname.Text = reader["mothers_name"].ToString();
                if (reader["date_of_birth"].ToString() != "") dob.Text = tempdob.ToShortDateString();
                if (reader["sex"].ToString() != "") sex.Text = reader["sex"].ToString();
                if (reader["nationality"].ToString() != "") nat.Text = reader["nationality"].ToString();
                if (reader["religion"].ToString() != "") rel.Text = reader["religion"].ToString();
                if (reader["department"].ToString() != "") dept.Text = reader["department"].ToString();
                if (reader["present_address"].ToString() != "") preadd.Text = reader["present_address"].ToString();
                if (reader["permanent_address"].ToString() != "") peradd.Text = reader["permanent_address"].ToString();
                if (reader["phn"].ToString() != "") mob.Text = reader["phn"].ToString();
                if (reader["nationalid"].ToString() != "") natid.Text = reader["nationalid"].ToString();
                if (reader["email"].ToString() != "") email.Text = reader["email"].ToString();
                if (reader["photo"].ToString() != "") pictureBox1.ImageLocation = reader["photo"].ToString();
                if (reader["blood"].ToString() != "") bgrp.Text = reader["blood"].ToString();
                if (reader["joindate"].ToString() != "") jdate.Text = tempadm.ToShortDateString();

                sc.Dispose();
                reader.Dispose();
                connected = true;
            }
            catch (Exception ex)
            {
                connected = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (btnClick != null)
            {
                clickedButton = Edit;
                btnClick(this, e);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (btnClick != null)
            {
                clickedButton = Back;
                btnClick(this, e);
            }
        }
    }
}
