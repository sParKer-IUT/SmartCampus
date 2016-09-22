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
    public partial class SetPaymentRate : UserControl
    {

        public event EventHandler btn1Click;

        public Button clickedButton;

        //for checking payment is valid or not
        public bool proceed;

        //For Database connection
        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlConnection connection;

        //for Database operations
        MySqlDataReader reader;
        MySqlCommand sc;
        DataTable dt;

        public SetPaymentRate()
        {
            InitializeComponent();
        }

        private void SetPaymentRate_Load(object sender, EventArgs e)
        {
            try
            {
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;

                proceed = false;

                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                sc = new MySqlCommand("select class from std_payment_rate order by class", connection);
                reader = sc.ExecuteReader();

                dt = new DataTable();
                dt.Load(reader);
                ComboClass.ValueMember = "class";
                ComboClass.DisplayMember = "class";
                ComboClass.DataSource = dt;

                sc = new MySqlCommand("select * from std_payment_rate where class='" + ComboClass.SelectedValue.ToString() + "';", connection);
                reader = sc.ExecuteReader();

                reader.Read();

                Tution.Value = (int)reader[1];
                Due.Value = (int)reader[2];
                Admission.Value = (int)reader[3];
                DueFine.Value = (int)reader[4];
                ReAdm.Value = (int)reader[5];
                Absent.Value = (int)reader[6];
                Session.Value = (int)reader[7];
                Exam.Value = (int)reader[8];
                RegFee.Value = (int)reader[9];
                CenterFee.Value = (int)reader[10];
                Caution.Value = (int)reader[11];
                ID.Value = (int)reader[12];
                Msg.Value = (int)reader[13];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sc.Dispose();
            reader.Dispose();
        }

        private void ComboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sc = new MySqlCommand("select * from std_payment_rate where class='" + ComboClass.SelectedValue.ToString() + "';", connection);
                reader = sc.ExecuteReader();

                reader.Read();

                Tution.Value = (int)reader[1];
                Due.Value = (int)reader[2];
                Admission.Value = (int)reader[3];
                DueFine.Value = (int)reader[4];
                ReAdm.Value = (int)reader[5];
                Absent.Value = (int)reader[6];
                Session.Value = (int)reader[7];
                Exam.Value = (int)reader[8];
                RegFee.Value = (int)reader[9];
                CenterFee.Value = (int)reader[10];
                Caution.Value = (int)reader[11];
                ID.Value = (int)reader[12];
                Msg.Value = (int)reader[13];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sc.Dispose();
            reader.Dispose();
        }

        private void Proceed_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to Set this rate?", "Confirmation!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("update std_payment_rate set tuition=@ttn,due=@due,admitionfee=@adm,duefine=@duefine,readfee=@readfee,absentfee=@absent,session=@session,exam=@exam,regfee=@reg,centerfee=@center,caution=@caution,id=@id,msg=@msg where class=@cls;", connection);
                    cmd.Parameters.AddWithValue("@ttn", (int)Tution.Value);
                    cmd.Parameters.AddWithValue("@due", (int)Due.Value);
                    cmd.Parameters.AddWithValue("@adm", (int)Admission.Value);
                    cmd.Parameters.AddWithValue("@duefine", (int)DueFine.Value);
                    cmd.Parameters.AddWithValue("@readfee", (int)ReAdm.Value);
                    cmd.Parameters.AddWithValue("@absent", (int)Absent.Value);
                    cmd.Parameters.AddWithValue("@session", (int)Session.Value);
                    cmd.Parameters.AddWithValue("@exam", (int)Exam.Value);
                    cmd.Parameters.AddWithValue("@reg", (int)RegFee.Value);
                    cmd.Parameters.AddWithValue("@center", (int)CenterFee.Value);
                    cmd.Parameters.AddWithValue("@caution", (int)Caution.Value);
                    cmd.Parameters.AddWithValue("@id", (int)ID.Value);
                    cmd.Parameters.AddWithValue("@msg", (int)Msg.Value);
                    cmd.Parameters.AddWithValue("@cls", ComboClass.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Payment Rate Updated");
                    proceed = true;
                    //print should be here
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    proceed = false;
                }
            }
            else
            {
                proceed = false;
            }
            if (this.btn1Click != null)
            {
                clickedButton = Proceed;
                this.btn1Click(this, e);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }
    }
}
