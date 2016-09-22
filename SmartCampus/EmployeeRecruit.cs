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
    public partial class EmployeeRecruit : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        private MySqlCommand command;
        private MySqlDataReader reader;

        private int total;
        private MySqlConnection connection;
        public static String name;
        public bool save;

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

        public EmployeeRecruit()
        {
            InitializeComponent();

        }

        private void EmplyeeRecruit_Load(object sender, EventArgs e)
        {
            try
            {
                save = false;
                cbxCategory.SelectedIndex = 0;
                ComboBlood.SelectedIndex = 0;
                sex.SelectedIndex = 0;
                dob.Value = DateTime.Now.Date;
                cbxDepartment.Enabled = false;
                label7.Enabled = false;

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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
                return;
            }
            if (EmployeeInfo.name != "" && EmployeeInfo.fName != "" && EmployeeInfo.mName != "" && EmployeeInfo.religion != "" && EmployeeInfo.nationality != "" && EmployeeInfo.prsntaddress != "" && EmployeeInfo.peraddress != "" && EmployeeInfo.mbl != "" && EmployeeInfo.nid != "" && EmployeeInfo.photo != "")
            {
                try
                {
                    SavetoDB();
                }
                catch(Exception ex)
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
            EmployeeInfo.name = empName.Text;
            EmployeeInfo.fName = fathersName.Text;
            EmployeeInfo.mName = mothersName.Text;
            EmployeeInfo.sex = sex.SelectedItem.ToString();
            EmployeeInfo.nationality = Nationality.Text;
            EmployeeInfo.religion = religion.Text;
            EmployeeInfo.dob = dob.Value;
            EmployeeInfo.joindate = DateTime.Now.Date;
            EmployeeInfo.prsntaddress = presentAddress.Text;
            EmployeeInfo.peraddress = permanentAddress.Text;
            EmployeeInfo.mbl = empMob.Text;
            if (cbxDepartment.Enabled)
            {
                EmployeeInfo.eCategory = cbxDepartment.SelectedItem.ToString().Substring(0, 2).ToUpper();
                EmployeeInfo.department = cbxDepartment.SelectedItem.ToString();
            }
            else
            {
                EmployeeInfo.eCategory = "SR";
                EmployeeInfo.department = "Service";
            }
            getSerial();
            EmployeeInfo.bloodgrp = ComboBlood.SelectedItem.ToString();
            EmployeeInfo.nid = empNatID.Text;
            EmployeeInfo.photo = savedImagePath;
            EmployeeInfo.email = email.Text;
            EmployeeInfo.cid = cardID.Text;
            EmployeeInfo.eId = EmployeeInfo.eCategory + DateTime.Now.Year.ToString().Substring(2,2) + getSerial().PadLeft(2, '0');
        }

        private string getSerial()
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            string dept = EmployeeInfo.department;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            command = new MySqlCommand("select count(*) from employee_info where department='" + dept + "';", connection);
            reader = command.ExecuteReader();
            reader.Read();
            total = Convert.ToInt32(reader["count(*)"])+1;
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

                string query = "INSERT INTO employee_info (ID, department, Name, Fathers_Name, Mothers_name, Sex, date_of_birth, Nationality, NationalID, present_address, permanent_address, phn, JoinDate, email, photo, blood, religion, card_id) VALUES (@id, @dept, @name, @fname, @mname, @sex, @dob, @nat, @natid, @presaddr, @peraddr, @phn, @jdate, @email, @photo, @bld, @rel, @cid);";

                //MessageBox.Show(EmployeeInfo.eId + " " + EmployeeInfo.department + " " + EmployeeInfo.name + " " + EmployeeInfo.fName + " " + EmployeeInfo.sex + " " + EmployeeInfo.dob + " " + EmployeeInfo.nationality + " " + EmployeeInfo.nid + " " + EmployeeInfo.prsntaddress + " " + EmployeeInfo.peraddress + " " + EmployeeInfo.mbl + " " + EmployeeInfo.joindate + " " + EmployeeInfo.email + " " + EmployeeInfo.photo);

                command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", EmployeeInfo.eId);
                command.Parameters.AddWithValue("@dept", EmployeeInfo.department);
                command.Parameters.AddWithValue("@name", EmployeeInfo.name);
                command.Parameters.AddWithValue("@fname", EmployeeInfo.fName);
                command.Parameters.AddWithValue("@mname", EmployeeInfo.mName);
                command.Parameters.AddWithValue("@sex", EmployeeInfo.sex);
                command.Parameters.AddWithValue("@dob", EmployeeInfo.dob);
                command.Parameters.AddWithValue("@nat", EmployeeInfo.nationality);
                command.Parameters.AddWithValue("@natid", EmployeeInfo.nid);
                command.Parameters.AddWithValue("@presaddr", EmployeeInfo.prsntaddress);
                command.Parameters.AddWithValue("@peraddr", EmployeeInfo.peraddress);
                command.Parameters.AddWithValue("@phn", EmployeeInfo.mbl);
                command.Parameters.AddWithValue("@jdate", EmployeeInfo.joindate);
                command.Parameters.AddWithValue("@email", EmployeeInfo.email);
                command.Parameters.AddWithValue("@photo", EmployeeInfo.photo);
                command.Parameters.AddWithValue("@bld", EmployeeInfo.bloodgrp);
                command.Parameters.AddWithValue("@rel", EmployeeInfo.religion);
                command.Parameters.AddWithValue("@cid", EmployeeInfo.cid);

                command.ExecuteNonQuery();
                MessageBox.Show("Data Entried", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                save = true;
            }
            catch(Exception ex)
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
            if(cbxCategory.SelectedIndex == 1)
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

        private void ComboBlood_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cardID_TextChanged(object sender, EventArgs e)
        {

        }

        private void fathersName_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class EmployeeInfo
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
