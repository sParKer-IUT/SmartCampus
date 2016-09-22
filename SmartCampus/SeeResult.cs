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
    public partial class SeeResult : UserControl
    {
        public event EventHandler seeResultBtnClick;
        public Button clickedButton;

        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dtOvr = new DataTable();

        string server;
        string database;
        string uid;
        string password;

        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;

        public SeeResult()
        {
            InitializeComponent();

            tabControlResult.TabPages[0].Controls.Add(dgvSem1);
            tabControlResult.TabPages[0].Controls.Add(lblGPA1);
            dgvSem1.Location = new Point(0,0);
            dgvSem1.Size = new Size(tabControlResult.TabPages[0].Size.Width, tabControlResult.TabPages[0].Size.Height - 20);
            lblGPA1.Location = new Point(dgvSem1.Size.Width - 120, dgvSem1.Size.Height + 5);

            tabControlResult.TabPages[1].Controls.Add(dgvSem2);
            tabControlResult.TabPages[1].Controls.Add(lblGPA2);
            dgvSem2.Location = new Point(0, 0);
            dgvSem2.Size = tabControlResult.TabPages[1].Size;
            lblGPA2.Location = new Point(dgvSem2.Size.Width - 120, dgvSem2.Size.Height + 5);

            tabControlResult.TabPages[2].Controls.Add(dgvSem3);
            tabControlResult.TabPages[2].Controls.Add(lblGPA3);
            dgvSem3.Location = new Point(0, 0);
            dgvSem3.Size = tabControlResult.TabPages[2].Size;
            lblGPA3.Location = new Point(dgvSem3.Size.Width - 120, dgvSem3.Size.Height + 5);
            
            tabControlResult.TabPages[3].Controls.Add(dgvOverall);
            tabControlResult.TabPages[3].Controls.Add(lblGPAOvr);
            dgvOverall.Location = new Point(0, 0);
            dgvOverall.Size = tabControlResult.TabPages[3].Size;
            lblGPAOvr.Location = new Point(dgvOverall.Size.Width - 120, dgvOverall.Size.Height + 5);

            //cbx_semester.SelectedIndex = 0;
            
            dt1.Columns.Add("Subject", typeof(string));
            dt1.Columns.Add("Quiz1", typeof(string));
            dt1.Columns.Add("Quiz2", typeof(string));
            dt1.Columns.Add("Semester Final", typeof(string));
            dt1.Columns.Add("q1+q2+final(80%)", typeof(string));
            dt1.Columns.Add("GP", typeof(string));

            dt2.Columns.Add("Subject", typeof(string));
            dt2.Columns.Add("Quiz1", typeof(string));
            dt2.Columns.Add("Quiz2", typeof(string));
            dt2.Columns.Add("Semester Final", typeof(string));
            dt2.Columns.Add("q1+q2+final(80%)", typeof(string));
            dt2.Columns.Add("GP", typeof(string));

            dt3.Columns.Add("Subject", typeof(string));
            dt3.Columns.Add("Quiz1", typeof(string));
            dt3.Columns.Add("Quiz2", typeof(string));
            dt3.Columns.Add("Semester Final", typeof(string));
            dt3.Columns.Add("q1+q2+final(80%)", typeof(string));
            dt3.Columns.Add("GP", typeof(string));

            dtOvr.Columns.Add("Subject", typeof(string));
            dtOvr.Columns.Add("1st Semester", typeof(string));
            dtOvr.Columns.Add("2nd Semester", typeof(string));
            dtOvr.Columns.Add("3rd Semester", typeof(string));
            dtOvr.Columns.Add("S1(25%) + S2(25%) + S3(50%)", typeof(string));
            dtOvr.Columns.Add("GP", typeof(string));
        }

        private void btn_seeResult_seeResult_Click(object sender, EventArgs e)
        {
            clickedButton = btn_seeResult_seeResult;

            string connectionString;
            string id;
            string year;
            //string semester;
            id = tbx_ID.Text;
            year = tbx_year.Text;
            //semester = (cbx_semester.SelectedIndex + 1).ToString();
            
            if(id == "" || year == "")
            {
                MessageBox.Show("Enter both ID and Year");
                return;
            }

            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            int yearDiff = Convert.ToInt32(year.Substring(2, 2)) - Convert.ToInt32(id.Substring(0, 2));
            if (yearDiff < 0)
            {
                MessageBox.Show(id + " wasn't a student of this institution in year " + year);
                return;
            }
            //int studentClass = id.Substring(2, 2).GetClassNumber() + yearDiff;
            
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();

                GetSemesterResult(id, year, 1);
                GetSemesterResult(id, year, 2);
                GetSemesterResult(id, year, 3);
                GetOverallResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        void GetSemesterResult(string id, string year, int semester)
        {
            DataTable dt = new DataTable();
            
            dt.Columns.Add("Subject", typeof(string));
            dt.Columns.Add("Quiz1", typeof(string));
            dt.Columns.Add("Quiz2", typeof(string));
            dt.Columns.Add("Semester Final", typeof(string));
            dt.Columns.Add("q1 + q2 + final(80%)", typeof(string));
            dt.Columns.Add("GP", typeof(string));

            command = new MySqlCommand("SELECT distinct SubjectCode FROM marks WHERE id='" + id + "' and year=" + year + " order by SubjectCode;", connection);
            reader = command.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("No data found");
                command.Dispose();
                reader.Dispose();
                return;
            }
            
            dt.Rows.Clear();
            dt.Rows.Add(reader[0].ToString().GetSubjectName(), "--", "--", "--", "--", "--");
            
            while (reader.Read())
            {
                dt.Rows.Add(reader[0].ToString().GetSubjectName(), "--", "--", "--", "--", "--");
            }

            //MessageBox.Show(marks.Count.ToString());
            
            command.Dispose();
            reader.Dispose();

            command = new MySqlCommand("SELECT SubjectCode, Exam, Marks FROM marks WHERE id='" + id + "' and year=" + year + " and semester=" + semester + " order by SubjectCode;", connection);
            reader = command.ExecuteReader();

            int i = 0;
            
            while (reader.Read())
            {
                if (reader[0].ToString().GetSubjectName() != dt.Rows[i][0])
                {
                    dt.Rows[i][4] = CalculateTotal(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                    dt.Rows[i][5] = dt.Rows[i][4].ToString().GetGradePoint();
                    i++;
                }
                if ((int)reader[1] == 1)
                {
                    dt.Rows[i][1] = reader[2].ToString();
                }
                else if ((int)reader[1] == 2)
                {
                    dt.Rows[i][2] = reader[2].ToString();
                }
                else
                {
                    dt.Rows[i][3] = reader[2].ToString();
                }
            }
            dt.Rows[i][4] = CalculateTotal(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
            dt.Rows[i][5] = dt.Rows[i][4].ToString().GetGradePoint();

            double t = 0;
            string gpa;
            foreach (DataRow r in dt.Rows)
            {
                if(r[5].ToString() == "--")
                {
                    t = -1;
                    break;
                }
                t += Convert.ToDouble(r[5].ToString());
            }
            if(t == -1)
            {
                gpa = "--";
            }
            else
            {
                gpa = (t / dt.Rows.Count).ToString();
            }

            if (semester == 1)
            {
                dt1 = dt;
                dgvSem1.DataSource = dt1;
                lblGPA1.Text = "GPA: " + gpa;
            }
            else if (semester == 2)
            {
                dt2 = dt;
                dgvSem2.DataSource = dt2;
                lblGPA2.Text = "GPA: " + gpa;
            }
            else if (semester == 3)
            {
                dt3 = dt;
                dgvSem3.DataSource = dt3;
                lblGPA3.Text = "GPA: " + gpa;
            }

            command.Dispose();
            reader.Dispose();
        }

        private void GetOverallResult()
        {
            int subCount = dt1.Rows.Count;
            int i;
            dtOvr.Clear();
            for(i = 0; i < subCount; i++)
            {
                string total = CalculateTotalFinal(dt1.Rows[i][4].ToString(), dt2.Rows[i][4].ToString(), dt3.Rows[i][4].ToString());
                dtOvr.Rows.Add(dt1.Rows[i][0].ToString(), dt1.Rows[i][4].ToString(), dt2.Rows[i][4].ToString(), dt3.Rows[i][4].ToString(), total, total.GetGradePoint());
            }
            dgvOverall.DataSource = dtOvr;

            double t = 0;
            foreach (DataRow r in dtOvr.Rows)
            {
                if (r[5].ToString() == "--")
                {
                    t = -1;
                    break;
                }
                t += Convert.ToDouble(r[5].ToString());
            }
            if (t == -1)
            {
                lblGPAOvr.Text = "--";
            }
            else
            {
                lblGPAOvr.Text = "GPA: "+(t / dtOvr.Rows.Count).ToString();
            }
        }

        private string CalculateTotal(string p1, string p2, string p3)
        {
            if(p1 == "--" || p2 == "--" || p2 == "--")
            {
                return "--";
            }
            double m = Convert.ToDouble(p3)*80/100;
            double total = Convert.ToDouble(p1) + Convert.ToDouble(p2) + m;
            return total.ToString();
        }

        private string CalculateTotalFinal(string p1, string p2, string p3)
        {
            if (p1 == "--" || p2 == "--" || p3 == "--")
            {
                return "--";
            }
            double m1 = Convert.ToDouble(p1) * 25 / 100;
            double m2 = Convert.ToDouble(p2) * 25 / 100;
            double m3 = Convert.ToDouble(p3) * 50 / 100;
            double total = m1 + m2 +m3;
            return total.ToString();
        }

        private void btn_seeResult_back_Click(object sender, EventArgs e)
        {
            if (seeResultBtnClick != null)
            {
                clickedButton = btn_seeResult_back;
                seeResultBtnClick(this, e);
            }
        }

        private void SeeResult_Load(object sender, EventArgs e)
        {
            
        }

        void AddedToPanel()
        {
            MessageBox.Show(this.Name);
        }
    }
}
