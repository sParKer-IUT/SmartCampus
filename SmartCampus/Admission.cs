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
    public partial class Admission : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        private int total;
        private MySqlConnection connection;
        public static String name;
        public bool save;

        Bitmap bmp;

        WebCam webcam;

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

        public static string savedImagePath;

        public Admission()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            // Displays an OpenFileDialog so the user can select a Cursor.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files|*.jpg";
            openFileDialog1.Title = "Select a Image File";

            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .CUR file was selected, open it.
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                this.pictureBox1.ImageLocation = openFileDialog1.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                savedImagePath = openFileDialog1.FileName;
                //MessageBox.Show(savedImagePath);
            }
        }

        private void Capture_Click(object sender, EventArgs e)
        {
            /*pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            webcam = new WebCam();
            //pictureBox1.Image = null;
            webcam.InitializeWebCam(ref pictureBox1);
            webcam.Start();
            */
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
            //pictureBox1.Image = pictureBox1.Image;
            //webcam.Stop();
            //Helper.SaveImageCaptureAdmission(pictureBox1.Image);
            //webcam.Dispose();
            
            
            

            if (_frameSource == null)
                return;

            Bitmap current = (Bitmap)_latestFrame.Clone();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.DefaultExt = ".Jpg";// Default file extension
                sfd.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension
                sfd.FileName = Admission.name;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    current.Save(sfd.FileName);
                    Admission.savedImagePath = sfd.FileName;
                    Snap.Enabled = false;
                    Snap.Visible = false;
                    Cancel.Enabled = false;
                    Cancel.Visible = false;
                    Capture.Enabled = true;
                    Capture.Visible = true;
                    Browse.Enabled = true;
                    Browse.Visible = true;
                    thrashOldCamera();
                    
                    /*bmp = new Bitmap(Image.FromFile(sfd.FileName));
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(new Bitmap(sfd.FileName), new Point(0, 0));
                    }
                    pictureBox1.BackgroundImage = bmp;*/

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
            webcam.Dispose();
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = null;
            */
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
            if (StdAdmitedInfo.name != "" && StdAdmitedInfo.fName != "" && StdAdmitedInfo.mName != "" && StdAdmitedInfo.focup != "" && StdAdmitedInfo.mocup != "" && StdAdmitedInfo.sex != "" && StdAdmitedInfo.religion != "" && StdAdmitedInfo.sClass != "" && StdAdmitedInfo.sec != "" && StdAdmitedInfo.prsntaddress != "" && StdAdmitedInfo.peraddress != "" && StdAdmitedInfo.lclgurdian != "" && StdAdmitedInfo.fmbl != "" && StdAdmitedInfo.mmbl != "" && StdAdmitedInfo.gmbl != "" && StdAdmitedInfo.bloodgrp != "" && StdAdmitedInfo.photo != "")
            {
                if (cls.Text.ToString() == "9" && StdAdmitedInfo.group == "")
                {
                    MessageBox.Show("Please enter all required fields(*)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }
                else
                {
                    try
                    {
                        SavetoDB();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter all required fields(*)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
            }
            if (this.btn1Click != null)
            {
                clickedButton = Save;
                this.btn1Click(this, e);
            }
        }
        private void generateValues()
        {
            getSerial();
            StdAdmitedInfo.name = stdName.Text.ToString();
            StdAdmitedInfo.fName = fathersName.Text.ToString();
            StdAdmitedInfo.mName = mothersName.Text.ToString();
            StdAdmitedInfo.mocup = mothersOcupation.Text.ToString();
            StdAdmitedInfo.focup = fathersOcupation.Text.ToString();
            StdAdmitedInfo.sex = sex.Text.ToString();
            StdAdmitedInfo.nationality = Nationality.Text.ToString();
            StdAdmitedInfo.religion = religion.Text.ToString();
            StdAdmitedInfo.sClass = cls.Text.ToString();
            StdAdmitedInfo.sec = section.Text.ToString();
            StdAdmitedInfo.dob = dob.Value;
            StdAdmitedInfo.admdate = DateTime.Now.Date;
            StdAdmitedInfo.prsntaddress = presentAddress.Text.ToString();
            StdAdmitedInfo.peraddress = permanentAddress.Text.ToString();
            StdAdmitedInfo.lclgurdian = localGurdian.Text.ToString();
            StdAdmitedInfo.land = landMob.Text.ToString();
            StdAdmitedInfo.fmbl = fatherMob.Text.ToString();
            StdAdmitedInfo.mmbl = motherMob.Text.ToString();
            StdAdmitedInfo.gmbl = gurdianMob.Text.ToString();
            StdAdmitedInfo.fnid = fatherNatID.Text.ToString();
            StdAdmitedInfo.mnid = motherNatID.Text.ToString();
            StdAdmitedInfo.gnid = gurdianNatID.Text.ToString();
            StdAdmitedInfo.hobby = Hobby.Text.ToString();
            StdAdmitedInfo.cocurricular = Cocurricular.Text.ToString();
            StdAdmitedInfo.bloodgrp = ComboBlood.Text.ToString();
            StdAdmitedInfo.cid = cardID.Text.ToString();
            if (cls.Text.ToString() == "9")
            {
                StdAdmitedInfo.group = ComboGrp.Text.ToString();
            }
            else
            {
                StdAdmitedInfo.group = "";
            }
            StdAdmitedInfo.email = email.Text.ToString();
            if (land.Checked) StdAdmitedInfo.msgmbl = landMob.Text.ToString();
            else if (father.Checked) StdAdmitedInfo.msgmbl = fatherMob.Text.ToString();
            else if (mother.Checked) StdAdmitedInfo.msgmbl = motherMob.Text.ToString();
            else if (gurdian.Checked) StdAdmitedInfo.msgmbl = gurdianMob.Text.ToString();
            String str = StdAdmitedInfo.sClass;
            if (StdAdmitedInfo.sClass != "Nursery" && StdAdmitedInfo.sClass != "Infantry" && StdAdmitedInfo.sClass != "Kg-1")
            {
                str = "0" + str;
            }
            else if (StdAdmitedInfo.sClass == "Nursery")
            {
                str = "NR";
            }
            else if (StdAdmitedInfo.sClass == "Infantry")
            {
                str = "IN";
            }
            else if (StdAdmitedInfo.sClass == "Kg-1")
            {
                str = "K1";
            }
            //str = StdAdmitedInfo.sClass.GetClassCode();
            total++;
            StdAdmitedInfo.sId = GetLast(StdAdmitedInfo.admdate.Year.ToString(), 2) + str + total.ToString().PadLeft(3, '0');
            //StdAdmitedInfo.photo = pictureBox1.ImageLocation.ToString();
            StdAdmitedInfo.photo = savedImagePath; // "C:\\Users\\sParKer\\Desktop\\" + StdAdmitedInfo.name + ".jpg";
                    
        }
        private void SavetoDB()
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "insert into student_info values(@id,@name,@fname,@mname,@sex,@nation,@focup,@mocup,@DOB ,@ADM ,@religion,@cls,@sec,@lclgurdn,@prsntaddress,@peraddress,@fnid,@mnid,@gnid,@land,@fmbl,@mmbl,@gmbl,@msgmbl,@hobby,@cocur,@group,@blood,@email,@photo,@cid);";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", StdAdmitedInfo.sId);
            cmd.Parameters.AddWithValue("@name", StdAdmitedInfo.name);
            cmd.Parameters.AddWithValue("@fname", StdAdmitedInfo.fName);
            cmd.Parameters.AddWithValue("@mname", StdAdmitedInfo.mName);
            cmd.Parameters.AddWithValue("@sex", StdAdmitedInfo.sex);
            cmd.Parameters.AddWithValue("@nation", StdAdmitedInfo.nationality);
            cmd.Parameters.AddWithValue("@focup", StdAdmitedInfo.focup);
            cmd.Parameters.AddWithValue("@mocup", StdAdmitedInfo.mocup);
            cmd.Parameters.AddWithValue("@DOB", StdAdmitedInfo.dob);
            cmd.Parameters.AddWithValue("@ADM", StdAdmitedInfo.admdate);
            cmd.Parameters.AddWithValue("@religion", StdAdmitedInfo.religion);
            cmd.Parameters.AddWithValue("@cls", StdAdmitedInfo.sClass);
            cmd.Parameters.AddWithValue("@sec", StdAdmitedInfo.sec);
            cmd.Parameters.AddWithValue("@lclgurdn", StdAdmitedInfo.lclgurdian);
            cmd.Parameters.AddWithValue("@prsntaddress", StdAdmitedInfo.prsntaddress);
            cmd.Parameters.AddWithValue("@peraddress", StdAdmitedInfo.peraddress);
            cmd.Parameters.AddWithValue("@fnid", StdAdmitedInfo.fnid);
            cmd.Parameters.AddWithValue("@mnid", StdAdmitedInfo.mnid);
            cmd.Parameters.AddWithValue("@gnid", StdAdmitedInfo.gnid);
            cmd.Parameters.AddWithValue("@land", StdAdmitedInfo.land);
            cmd.Parameters.AddWithValue("@fmbl", StdAdmitedInfo.fmbl);
            cmd.Parameters.AddWithValue("@mmbl", StdAdmitedInfo.mmbl);
            cmd.Parameters.AddWithValue("@gmbl", StdAdmitedInfo.gmbl);
            cmd.Parameters.AddWithValue("@msgmbl", StdAdmitedInfo.msgmbl);
            cmd.Parameters.AddWithValue("@hobby", StdAdmitedInfo.hobby);
            cmd.Parameters.AddWithValue("@cocur", StdAdmitedInfo.cocurricular);
            cmd.Parameters.AddWithValue("@group", StdAdmitedInfo.group);
            cmd.Parameters.AddWithValue("@blood", StdAdmitedInfo.bloodgrp);
            cmd.Parameters.AddWithValue("@email", StdAdmitedInfo.email);
            cmd.Parameters.AddWithValue("@photo", StdAdmitedInfo.photo);
            cmd.Parameters.AddWithValue("@photo", StdAdmitedInfo.cid);
            //MessageBox.Show(query);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Entried","Confirmation",MessageBoxButtons.OK,MessageBoxIcon.Information);
            save = true;
            connection.Close();
            savedImagePath = "";
        }
        private void getSerial()
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            string sclass;
            sclass = cls.Text.ToString();
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand forname = new MySqlCommand("select count(*) from student_info where class='" + sclass + "';", connection);
            MySqlDataReader reader;
            reader = forname.ExecuteReader();
            reader.Read();
            total = Convert.ToInt32(reader["count(*)"]);
            forname.Dispose();
            reader.Dispose();
            connection.Close();
        }
        private string GetLast(string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }
        private void stdName_TextChanged(object sender, EventArgs e)
        {
            name = stdName.Text.ToString();
        }

        private void cls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cls.Text.ToString() == "9")
            {
                ComboGrp.Enabled = true;
                group.Visible = true;
            }
            else
            {
                ComboGrp.Enabled = false;
                group.Visible = false;
            }
        }

        private void Admission_Load(object sender, EventArgs e)
        {
            try
            {
                save = false;
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

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    comboBoxCameras.Items.Add(cam);
                Console.WriteLine(comboBoxCameras.Items.Count);
                if (comboBoxCameras.Items.Count > 0)
                    comboBoxCameras.SelectedIndex = 0;
            }
        }
    }
    public class StdAdmitedInfo
    {
        public static String name;
        public static String fName;
        public static String mName;
        public static String focup;
        public static String mocup;
        public static String sex;
        public static String nationality;
        public static String religion;
        public static String sId;
        public static String sClass;
        public static String sec;
        public static DateTime dob;
        public static DateTime admdate;
        public static String prsntaddress;
        public static String peraddress;
        public static String lclgurdian;
        public static String land;
        public static String fmbl;
        public static String mmbl;
        public static String gmbl;
        public static String msgmbl;
        public static String hobby;
        public static String cocurricular;
        public static String group;
        public static String bloodgrp;
        public static String fnid;
        public static String mnid;
        public static String gnid;
        public static String photo;
        public static String email;
        public static String cid;
    }
}
