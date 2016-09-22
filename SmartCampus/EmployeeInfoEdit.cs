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
using Touchless.Vision.Camera;

namespace SmartCampus
{
    public partial class EmployeeInfoEdit : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlCommand command;
        MySqlCommand sc;
        private MySqlDataReader reader;

        private int total;
        private MySqlConnection connection;
        public static String name;
        public bool save;

        DateTime tempdob;
        DateTime tempjoin;

        WebCam webcam;

        public static string savedImagePath;

        #region webcam variables

        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        private Camera CurrentCamera
        {
            get
            {
                return comboBoxCameras.SelectedItem as Camera;
            }
        }

        #endregion

        public EmployeeInfoEdit()
        {
            InitializeComponent();
        }

        private int setblood(String str)
        {
            int x = 0;
            if (str == "A+")
                x = 0;
            else if (str == "A-")
                x = 1;
            else if (str == "B+")
                x = 2;
            else if (str == "B-")
                x = 3;
            else if (str == "O+")
                x = 4;
            else if (str == "O-")
                x = 5;
            else if (str == "AB+")
                x = 6;
            else if (str == "AB-")
                x = 7;
            return x;
        }

        private void EmployeeInfoEdit_Load(object sender, EventArgs e)
        {
            try
            {
                save = false;

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
                tempdob = (DateTime)reader["date_of_birth"];
                tempjoin = (DateTime)reader["joindate"];
                empName.Text = EmployeeEditedInfo.name = reader["name"].ToString();
                fathersName.Text = EmployeeEditedInfo.fName = reader["fathers_name"].ToString();
                mothersName.Text = EmployeeEditedInfo.mName = reader["mothers_name"].ToString();
                EmployeeEditedInfo.sex = reader["sex"].ToString();
                Nationality.Text = EmployeeEditedInfo.nationality = reader["nationality"].ToString();
                religion.Text = EmployeeEditedInfo.religion = reader["religion"].ToString();
                dob.Value = EmployeeEditedInfo.dob = tempdob;
                EmployeeEditedInfo.joindate = tempjoin;
                presentAddress.Text = EmployeeEditedInfo.prsntaddress = reader["present_address"].ToString();
                permanentAddress.Text = EmployeeEditedInfo.peraddress = reader["permanent_address"].ToString();
                empMob.Text = EmployeeEditedInfo.mbl = reader["phn"].ToString();
                empNatID.Text = EmployeeEditedInfo.nid = reader["nationalid"].ToString();
                EmployeeEditedInfo.bloodgrp = reader["blood"].ToString();
                savedImagePath = EmployeeEditedInfo.photo = reader["photo"].ToString();
                EmployeeEditedInfo.department = reader["department"].ToString();
                email.Text = EmployeeEditedInfo.email = reader["email"].ToString();

                cardID.Text = EmployeeEditedInfo.cid = reader["card_id"].ToString();

                sex.SelectedIndex = EmployeeEditedInfo.sex == "Male" ? 0 : 1;
                ComboBlood.SelectedIndex = setblood(EmployeeEditedInfo.bloodgrp);

                cbxCategory.SelectedIndex = EmployeeEditedInfo.department == "Service" ? 0 : 1;

                label7.Enabled = cbxDepartment.Enabled = cbxCategory.SelectedIndex == 0 ? false : true;

                if (!DesignMode)
                {
                    // Refresh the list of available cameras
                    comboBoxCameras.Items.Clear();
                    foreach (Camera cam in CameraService.AvailableCameras)
                        comboBoxCameras.Items.Add(cam);

                    if (comboBoxCameras.Items.Count > 0)
                        comboBoxCameras.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg";
            openFileDialog1.Title = "Select a Cursor File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                savedImagePath = openFileDialog1.FileName;
            }
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            /*pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            webcam = new WebCam();
            webcam.InitializeWebCam(ref pictureBox1);
            webcam.Start();*/

            Snap.Enabled = true;
            Snap.Visible = true;
            Cancel.Enabled = true;
            Cancel.Visible = true;
            Capture.Enabled = false;
            Capture.Visible = false;
            Browse.Enabled = false;
            Browse.Visible = false;

            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void Snap_Click(object sender, EventArgs e)
        {
            /*pictureBox1.Image = pictureBox1.Image;
            webcam.Stop();
            Helper.SaveImageCaptureRecruit(pictureBox1.Image);
            Snap.Enabled = false;
            Snap.Visible = false;
            Cancel.Enabled = false;
            Cancel.Visible = false;
            Capture.Enabled = true;
            Capture.Visible = true;
            Browse.Enabled = true;
            Browse.Visible = true;*/

            if (_frameSource == null)
                return;

            Bitmap current = (Bitmap)_latestFrame.Clone();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = ".Jpg";// Default file extension
                sfd.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension
                sfd.FileName = EmployeeRecruit.name;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    current.Save(sfd.FileName);
                    EmployeeRecruit.savedImagePath = sfd.FileName;
                    Snap.Enabled = false;
                    Snap.Visible = false;
                    Cancel.Enabled = false;
                    Cancel.Visible = false;
                    Capture.Enabled = true;
                    Capture.Visible = true;
                    Browse.Enabled = true;
                    Browse.Visible = true;
                    thrashOldCamera();

                    pictureBox1.ImageLocation = sfd.FileName;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }

            current.Dispose();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Snap.Enabled = false;
            Snap.Visible = false;
            Cancel.Enabled = false;
            Cancel.Visible = false;
            /*webcam.Stop();
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = null;
             * */
            Capture.Enabled = true;
            Capture.Visible = true;
            Browse.Enabled = true;
            Browse.Visible = true;

            thrashOldCamera();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                generateValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
                return;
            }
            if (EmployeeEditedInfo.name != "" && EmployeeEditedInfo.fName != "" && EmployeeEditedInfo.mName != "" && EmployeeEditedInfo.religion != "" && EmployeeEditedInfo.nationality != "" && EmployeeEditedInfo.prsntaddress != "" && EmployeeEditedInfo.peraddress != "" && EmployeeEditedInfo.mbl != "" && EmployeeEditedInfo.nid != "" && EmployeeEditedInfo.photo != "")
            {
                try
                {
                    SavetoDB();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please enter all required fields(*)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
                return;
            }
            if (this.btn1Click != null)
            {
                clickedButton = Save;
                this.btn1Click(this, e);
            }
        }

        #region webcam functions
        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBox1.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrashOldCamera();
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBox1.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                //originalstatement: e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
                e.Graphics.DrawImage(_latestFrame, 0, 0, pictureBox1.Width, pictureBox1.Height);
            }
        }

        private void startCapturing()
        {
            try
            {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 320;
                _frameSource.Camera.CaptureHeight = 240;
                _frameSource.Camera.Fps = 50;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBox1.Paint += new PaintEventHandler(drawLatestImage);
                //cameraPropertyValue.Enabled = 
                _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        private void generateValues()
        {
            EmployeeEditedInfo.name = empName.Text;
            EmployeeEditedInfo.fName = fathersName.Text;
            EmployeeEditedInfo.mName = mothersName.Text;
            EmployeeEditedInfo.sex = sex.SelectedItem.ToString();
            EmployeeEditedInfo.nationality = Nationality.Text;
            EmployeeEditedInfo.religion = religion.Text;
            EmployeeEditedInfo.dob = dob.Value;
            EmployeeEditedInfo.joindate = DateTime.Now.Date;
            EmployeeEditedInfo.prsntaddress = presentAddress.Text;
            EmployeeEditedInfo.peraddress = permanentAddress.Text;
            EmployeeEditedInfo.mbl = empMob.Text;
            if (cbxDepartment.Enabled)
            {
                EmployeeEditedInfo.eCategory = cbxDepartment.SelectedItem.ToString().Substring(0, 2).ToUpper();
                EmployeeEditedInfo.department = cbxDepartment.SelectedItem.ToString();
            }
            else
            {
                EmployeeEditedInfo.eCategory = "SR";
                EmployeeEditedInfo.department = "Service";
            }
            getSerial();
            EmployeeEditedInfo.bloodgrp = ComboBlood.SelectedItem.ToString();
            EmployeeEditedInfo.nid = empNatID.Text;
            EmployeeEditedInfo.photo = savedImagePath;
            EmployeeEditedInfo.email = email.Text;
            EmployeeEditedInfo.cid = cardID.Text;
            EmployeeEditedInfo.eId = EmployeeEditedInfo.eCategory + DateTime.Now.Year.ToString().Substring(2, 2) + getSerial().PadLeft(2, '0');
        }

        private string getSerial()
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            string dept = EmployeeEditedInfo.department;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            command = new MySqlCommand("select count(*) from employee_info where department='" + dept + "';", connection);
            reader = command.ExecuteReader();
            reader.Read();
            total = Convert.ToInt32(reader["count(*)"]) + 1;
            command.Dispose();
            reader.Dispose();
            connection.Close();
            return total.ToString();
        }

        private void SavetoDB()
        {
            try
            {
                server = "localhost";
                database = "shotabdi";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE employee_info set department=@dept, Name=@name, Fathers_Name=@fname, Mothers_name=@mname, Sex=@sex, date_of_birth=@dob, Nationality=@nat, NationalID=@natid, present_address=@presaddr, permanent_address=@peraddr, phn=@phn, JoinDate=@jdate, email=@email, photo=@photo, blood=@bld, religion=@rel, card_id=@cid where id=@id;";

                //MessageBox.Show(EmployeeInfo.eId + " " + EmployeeInfo.department + " " + EmployeeInfo.name + " " + EmployeeInfo.fName + " " + EmployeeInfo.sex + " " + EmployeeInfo.dob + " " + EmployeeInfo.nationality + " " + EmployeeInfo.nid + " " + EmployeeInfo.prsntaddress + " " + EmployeeInfo.peraddress + " " + EmployeeInfo.mbl + " " + EmployeeInfo.joindate + " " + EmployeeInfo.email + " " + EmployeeInfo.photo);

                command = new MySqlCommand(query, connection);
                //command.Parameters.AddWithValue("@id", EmployeeEditedInfo.eId);
                command.Parameters.AddWithValue("@id", EmpDBselectdeptid.thisID);
                command.Parameters.AddWithValue("@dept", EmployeeEditedInfo.department);
                command.Parameters.AddWithValue("@name", EmployeeEditedInfo.name);
                command.Parameters.AddWithValue("@fname", EmployeeEditedInfo.fName);
                command.Parameters.AddWithValue("@mname", EmployeeEditedInfo.mName);
                command.Parameters.AddWithValue("@sex", EmployeeEditedInfo.sex);
                command.Parameters.AddWithValue("@dob", EmployeeEditedInfo.dob);
                command.Parameters.AddWithValue("@nat", EmployeeEditedInfo.nationality);
                command.Parameters.AddWithValue("@natid", EmployeeEditedInfo.nid);
                command.Parameters.AddWithValue("@presaddr", EmployeeEditedInfo.prsntaddress);
                command.Parameters.AddWithValue("@peraddr", EmployeeEditedInfo.peraddress);
                command.Parameters.AddWithValue("@phn", EmployeeEditedInfo.mbl);
                command.Parameters.AddWithValue("@jdate", EmployeeEditedInfo.joindate);
                command.Parameters.AddWithValue("@email", EmployeeEditedInfo.email);
                command.Parameters.AddWithValue("@photo", EmployeeEditedInfo.photo);
                command.Parameters.AddWithValue("@bld", EmployeeEditedInfo.bloodgrp);
                command.Parameters.AddWithValue("@rel", EmployeeEditedInfo.religion);
                command.Parameters.AddWithValue("@cid", EmployeeEditedInfo.cid);

                command.ExecuteNonQuery();
                MessageBox.Show("Data Entried", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                save = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            command.Dispose();
            reader.Dispose();
            connection.Close();
            savedImagePath = "";
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCategory.SelectedIndex == 1)
            {
                label7.Enabled = true;
                cbxDepartment.Enabled = true;
                cbxDepartment.SelectedIndex = 0;
            }
            else
            {
                label7.Enabled = false;
                cbxDepartment.Enabled = false;
            }
        }

        private void empName_TextChanged(object sender, EventArgs e)
        {
            name = empName.Text;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            PasswordForm pass = new PasswordForm();
            if (pass.ShowDialog() == DialogResult.OK)
            {
                if (AllPasswords.inputPass.Equals(AllPasswords.masterPass))
                {
                    server = "localhost";
                    database = "shotabdi";
                    uid = "root";
                    password = "";
                    string connectionString;
                    connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                    string query = "delete from employee_info where id = '" + EmpDBselectdeptid.thisID + "';";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    connection.Close();

                    if (this.btn1Click != null)
                    {
                        clickedButton = delete;
                        this.btn1Click(this, e);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password!!!");
                }
            }
        }

    }

    public class EmployeeEditedInfo
    {
        public static String name;
        public static String fName;
        public static String mName;
        public static String sex;
        public static String nationality;
        public static String religion;
        public static String eId;
        public static String eCategory;
        public static DateTime dob;
        public static DateTime joindate;
        public static String prsntaddress;
        public static String peraddress;
        public static String mbl;
        public static String department;
        public static String bloodgrp;
        public static String nid;
        public static String photo;
        public static String email;
        public static String cid;
    }
}
