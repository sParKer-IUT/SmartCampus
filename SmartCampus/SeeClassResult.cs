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
    public partial class SeeClassResult : UserControl
    {
        public event EventHandler seeClassResultBtnClick;
        public Button ClickedButton;

        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dtOvr = new DataTable();

        DataTable dtTotalMarks = new DataTable();

        string server;
        string database;
        string uid;
        string password;

        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;

        List<string> Students = new List<string>();

        Dictionary<CustomKey, double> marksTemp = new Dictionary<CustomKey,double>();

        public SeeClassResult()
        {
            InitializeComponent();

            dtTotalMarks.Columns.Add("ID", typeof(string));
            dtTotalMarks.Columns.Add("Marks", typeof(string));

            cbx_class.SelectedIndex = 0;
            //cbx_semester.SelectedIndex = 0;

            tabControlResult.TabPages[0].Controls.Add(dgvSem1);
            dgvSem1.Location = new Point(0, 0);
            dgvSem1.Size = new Size(tabControlResult.TabPages[0].Size.Width, tabControlResult.TabPages[0].Size.Height - 20);
            
            tabControlResult.TabPages[1].Controls.Add(dgvSem2);
            dgvSem2.Location = new Point(0, 0);
            dgvSem2.Size = tabControlResult.TabPages[1].Size;
            
            tabControlResult.TabPages[2].Controls.Add(dgvSem3);
            dgvSem3.Location = new Point(0, 0);
            dgvSem3.Size = tabControlResult.TabPages[2].Size;
            
            //tabControlResult.TabPages[3].Controls.Add(dgvOverall);
            //dgvOverall.Location = new Point(0, 0);
            //dgvOverall.Size = tabControlResult.TabPages[3].Size;

            dt1.Columns.Add("Serial", typeof(string));
            dt1.Columns.Add("ID", typeof(string));
            dt1.Columns.Add("Total Marks", typeof(string));
            dt1.Columns.Add("GPA", typeof(string));

            dt2.Columns.Add("Serial", typeof(string));
            dt2.Columns.Add("ID", typeof(string));
            dt2.Columns.Add("Total Marks", typeof(string));
            dt2.Columns.Add("GPA", typeof(string));

            dt3.Columns.Add("Serial", typeof(string));
            dt3.Columns.Add("ID", typeof(string));
            dt3.Columns.Add("Total Marks", typeof(string));
            dt3.Columns.Add("GPA", typeof(string));

            dtOvr.Columns.Add("Serial", typeof(string));
            dtOvr.Columns.Add("ID", typeof(string));
            dtOvr.Columns.Add("Total Marks", typeof(string));
            dtOvr.Columns.Add("GPA", typeof(string));
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if(seeClassResultBtnClick != null)
            {
                ClickedButton = btnBack;
                seeClassResultBtnClick(this, e);
            }
        }

        private void btnSeeResult_Click(object sender, EventArgs e)
        {
            ClickedButton = btnSeeResult;
            marksTemp.Clear();
            string connectionString;
            string selectedClass;
            string year;
            string semester;

            selectedClass = cbx_class.SelectedItem.ToString();
            year = tbx_year.Text;
            //semester = (cbx_semester.SelectedIndex + 1).ToString();

            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";

            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                
                dt1 = GetSemesterResult(selectedClass, year, 1);
                dgvSem1.DataSource = dt1;

                dt2 = GetSemesterResult(selectedClass, year, 2);
                dgvSem2.DataSource = dt2;

                dt3 = GetSemesterResult(selectedClass, year, 3);
                dgvSem3.DataSource = dt3;

                foreach(var pair in marksTemp)
                {
                    Console.WriteLine(pair.Key.id + " " + pair.Key.subject + " " + pair.Value);
                }
                //GetOverallResult(selectedClass, year);
                //dgvOverall.DataSource = dtOvr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private DataTable GetSemesterResult(string cls, string year, int semester)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Serial", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Total Marks", typeof(string));
            dt.Columns.Add("GPA", typeof(string));

            string query = "SELECT * from result WHERE class="+cls.GetClassNumber()+" and year="+year+" and semester="+semester+";";
            
            command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();

            dt.Clear();
            while(reader.Read())
            {
                dt.Rows.Add("", reader["ID"].ToString(), reader["TotalMarks"].ToString(), reader["GPA"].ToString());
            }

            command.Dispose();
            reader.Dispose();

            if(dt.Rows.Count == 0)
            {
                if (CheckAllStudents(cls, semester))
                {
                    //MessageBox.Show(Students.Count.ToString());
                    foreach (string sid in Students)
                    {
                        string[] arr = GenerateSemesterResult(cls, sid, Convert.ToInt32(year), Convert.ToInt32(semester));
                        dt.Rows.Add(arr);
                    }
                }
                else
                {
                    MessageBox.Show("Marks of all students of class "+cls+", semester "+semester+" are not in the database");
                    return dt;
                }
            }
            dt.DefaultView.Sort = "GPA DESC, Total Marks DESC";
            dt = dt.DefaultView.ToTable();

            int i = 1;
            foreach(DataRow r in dt.Rows)
            {
                r[0] = i;
                i++;
            }

            return dt;

        }

        bool CheckAllStudents(string cls, int semester)
        {
            string query1 = "SELECT id from student_info WHERE class = '"+cls+"';";
            string query2 = "SELECT count(distinct id) as total from marks WHERE class = "+cls.GetClassNumber()+" and year = "+DateTime.Now.Year+" and semester = "+semester+";";

            command = new MySqlCommand(query1, connection);
            reader = command.ExecuteReader();

            Students.Clear();

            while(reader.Read())
            {
                Students.Add(reader["id"].ToString());
            }

            int t1 = Students.Count;

            command.Dispose();
            reader.Dispose();

            command = new MySqlCommand(query2, connection);
            reader = command.ExecuteReader();

            reader.Read();
            int t2 = Convert.ToInt32(reader[0].ToString());

            command.Dispose();
            reader.Dispose();

            //MessageBox.Show(t1+" "+t2);

            return t1==t2?true:false;
        }

        string[] GenerateSemesterResult(string cls, string id, int year, int semester)
        {
            string[] arr = {"--","--","--","--"};
            
            DataTable dt = new DataTable();

            dt.Columns.Add("Subject", typeof(string));
            dt.Columns.Add("Quiz1", typeof(string));
            dt.Columns.Add("Quiz2", typeof(string));
            dt.Columns.Add("Semester Final", typeof(string));
            dt.Columns.Add("q1 + q2 + final(80%)", typeof(string));
            dt.Columns.Add("GP", typeof(string));

            command = new MySqlCommand("SELECT distinct SubjectCode FROM marks WHERE class=" + cls.GetClassNumber() + " and id='" + id + "' and year=" + year + " order by SubjectCode;", connection);
            reader = command.ExecuteReader();
            if (!reader.Read())
            {
                MessageBox.Show("No data found");
                command.Dispose();
                reader.Dispose();
                return arr;
            }

            dt.Rows.Clear();
            dt.Rows.Add(reader[0].ToString().GetSubjectName(), "--", "--", "--", "--", "--");
            //marksTemp.Add(new CustomKey(id, reader[0].ToString().GetSubjectName()), 0);

            while (reader.Read())
            {
                dt.Rows.Add(reader[0].ToString().GetSubjectName(), "--", "--", "--", "--", "--");
                //marksTemp.Add(new CustomKey(id, reader[0].ToString().GetSubjectName()), 0);
            }

            command.Dispose();
            reader.Dispose();

            command = new MySqlCommand("SELECT SubjectCode, Exam, Marks FROM marks WHERE class=" + cls.GetClassNumber() + " and id='" + id + "' and year=" + year + " and semester=" + semester + " order by SubjectCode;", connection);
            reader = command.ExecuteReader();

            int i = 0;
            
            while (reader.Read())
            {
                if (reader[0].ToString().GetSubjectName() != dt.Rows[i][0])
                {
                    dt.Rows[i][4] = CalculateTotal(dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                    dt.Rows[i][5] = dt.Rows[i][4].ToString().GetGradePoint();
                    //CustomKey k = new CustomKey(id, dt.Rows[i][0].ToString().GetSubjectCode());
                    //double d = Convert.ToDouble(dt.Rows[i][4].ToString());
                    //bool check = false;
                    /*if (marksTemp.Count == 0)
                    {
                        Console.WriteLine("Empty");
                        marksTemp.Add(k, SemesterPercentage(d, semester));
                    }
                    else
                    {
                        foreach(KeyValuePair<CustomKey, double> p in marksTemp)
                        {
                            if(p.Key.Equals(k))
                            {
                                Console.WriteLine("Matched");
                            }
                            else
                            {
                                marksTemp.Add(k, SemesterPercentage(d, semester));
                            }
                        }
                        /*if (marksTemp.ContainsKey(k))
                        {
                            marksTemp[k] += SemesterPercentage(d, semester);
                        }
                        else
                        {
                            marksTemp.Add(k, SemesterPercentage(d, semester));
                        }
                    }*/
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

            command.Dispose();
            reader.Dispose();

            double t = 0;
            double gpa = -1;
            double tm = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (r[5].ToString() == "--")
                {
                    t = -1;
                    tm = -1;
                    break;
                }
                t += Convert.ToDouble(r[5].ToString());
                tm += Convert.ToDouble(r[4].ToString());
            }
            if (t == -1)
            {
                MessageBox.Show("Insufficient data");
            }
            else
            {
                gpa = (t / dt.Rows.Count);
                //command = new MySqlCommand("INSERT INTO result (Class, Year, Semester, ID, TotalMarks, GPA) VALUES(" + cls.GetClassNumber() + ", " + year + ", " + semester + ", '" + id + "', " + tm + ", " + gpa + ");", connection);
                //reader = command.ExecuteReader();
            }

            //command.Dispose();
            //reader.Dispose();

            arr[0] = "";
            arr[1] = id;
            
            if (tm == -1) arr[2] = "--";
            else arr[2] = tm.ToString();

            if (gpa == -1) arr[3] = "--";
            else arr[3] = gpa.ToString();

            return arr;
        }

        private string CalculateTotal(string p1, string p2, string p3)
        {
            if (p1 == "--" || p2 == "--" || p2 == "--")
            {
                return "--";
            }
            double m = Convert.ToDouble(p3) * 80 / 100;
            double total = Convert.ToDouble(p1) + Convert.ToDouble(p2) + m;
            return total.ToString();
        }

        private double SemesterPercentage(double d, int s)
        {
            double val;
            if(s==1 || s==2)
            {
                val = d * 25 / 100;
            }
            else
            {
                val = d * 50 / 100;
            }
            return val;
        }

        private string CalculateTotalFinal(string p1, string p2, string p3)
        {
            if (p1 == "--" || p2 == "--" || p2 == "--")
            {
                return "--";
            }
            double m1 = Convert.ToDouble(p1) * 25 / 100;
            double m2 = Convert.ToDouble(p2) * 25 / 100;
            double m3 = Convert.ToDouble(p3) * 50 / 100;
            double total = m1 + m2 + m3;
            return total.ToString();
        }

        private void GetOverallResult(string cls, string year)
        {
            dtOvr.Clear();

            if (dt1.Rows.Count == 0 || dt2.Rows.Count == 0 || dt3.Rows.Count == 0)
            {
                MessageBox.Show("Result of all semester are not in the database");
                return;
            }
            CheckAllStudents(cls, 1);
            //MessageBox.Show(Students.Count.ToString());
            foreach (string sid in Students)
            {
                string[] arr1 = new string[4];
                string[] arr2 = new string[4];
                string[] arr3 = new string[4];
                
                arr1 = GenerateSemesterResult(cls, sid, Convert.ToInt32(year), 1);
                arr2 = GenerateSemesterResult(cls, sid, Convert.ToInt32(year), 2);
                arr3 = GenerateSemesterResult(cls, sid, Convert.ToInt32(year), 3);
                
                int i; 
                double tm=0, gp=0;
                for(i = 0; i < arr1.Length; i++)
                {
                    tm += ((Convert.ToDouble(arr1[2])*25/100) + (Convert.ToDouble(arr2[2])*25/100) + (Convert.ToDouble(arr3[2])*50/100));
                    gp = Convert.ToDouble(tm.ToString().GetGradePoint());
                }
                double gpa = gp / arr1.Length;

                dtOvr.Rows.Add("", arr1[1], tm.ToString(), gpa.ToString());
            }
            dtOvr.DefaultView.Sort = "GPA DESC, Total Marks DESC";
            dtOvr = dtOvr.DefaultView.ToTable();
        }
    }
    class CustomKey
    {
        public string id;
        public string subject;
        
        public CustomKey(string i, string s)
        {
            id = i;
            subject = s;
        }
    }
}
