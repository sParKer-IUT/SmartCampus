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
    public partial class StudentInfoEdit : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        private string server;
        private string database;
        private string uid;
        private string password;
        //private int total;
        private MySqlConnection connection;
        MySqlDataReader reader;
        MySqlCommand sc;
        public static String name;
        public bool save;
        DateTime tempdob;
        DateTime tempadm;
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

        public StudentInfoEdit()
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
            /*pictureBox1.Image = pictureBox1.Image;
            webcam.Stop();
            Helper.SaveImageCaptureStdEdit(pictureBox1.Image);
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
                sfd.FileName = StudentInfoEdit.name;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    current.Save(sfd.FileName);
                    StudentInfoEdit.savedImagePath = sfd.FileName;
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
            pictureBox1.Image = null;*/
            Capture.Enabled = true;
            Capture.Visible = true;
            Browse.Enabled = true;
            Browse.Visible = true;

            thrashOldCamera();
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


        void generateValues()
        {
            StdEditedInfo.name = stdName.Text.ToString();
            StdEditedInfo.fName = fathersName.Text.ToString();
            StdEditedInfo.mName = mothersName.Text.ToString();
            StdEditedInfo.mocup = mothersOcupation.Text.ToString();
            StdEditedInfo.focup = fathersOcupation.Text.ToString();
            StdEditedInfo.sex = sex.Text.ToString();
            StdEditedInfo.nationality = Nationality.Text.ToString();
            StdEditedInfo.religion = religion.Text.ToString();
            StdEditedInfo.sClass = cls.SelectedItem.ToString();
            StdEditedInfo.sec = section.Text.ToString();
            StdEditedInfo.dob = dob.Value;
            StdEditedInfo.admdate = tempadm;
            StdEditedInfo.prsntaddress = presentAddress.Text.ToString();
            StdEditedInfo.peraddress = permanentAddress.Text.ToString();
            StdEditedInfo.lclgurdian = localGurdian.Text.ToString();
            StdEditedInfo.land = landMob.Text.ToString();
            StdEditedInfo.fmbl = fatherMob.Text.ToString();
            StdEditedInfo.mmbl = motherMob.Text.ToString();
            StdEditedInfo.gmbl = gurdianMob.Text.ToString();
            StdEditedInfo.fnid = fatherNatID.Text.ToString();
            StdEditedInfo.mnid = motherNatID.Text.ToString();
            StdEditedInfo.gnid = gurdianNatID.Text.ToString();
            StdEditedInfo.hobby = Hobby.Text.ToString();
            StdEditedInfo.cocurricular = Cocurricular.Text.ToString();
            StdEditedInfo.bloodgrp = ComboBlood.Text.ToString();
            StdEditedInfo.cid = cardID.Text.ToString();
            if (cls.Text.ToString() == "9")
            {
                StdEditedInfo.group = ComboGrp.Text.ToString();
            }
            else
            {
                //MessageBox.Show(cls.Text);
                //StdEditedInfo.group = null;
            }
            StdEditedInfo.email = email.Text.ToString();
            if (land.Checked) StdEditedInfo.msgmbl = landMob.Text.ToString();
            else if (father.Checked) StdEditedInfo.msgmbl = fatherMob.Text.ToString();
            else if (mother.Checked) StdEditedInfo.msgmbl = motherMob.Text.ToString();
            else if (gurdian.Checked) StdEditedInfo.msgmbl = gurdianMob.Text.ToString();

            StdEditedInfo.photo = StudentInfoEdit.savedImagePath;
            //MessageBox.Show(StdEditedInfo.photo);
        }
        void SavetoDB()
        {
            server = "localhost";
            database = "shotabdi";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            string query = "update student_info set id=@id,name=@name,fathers_name=@fname,mothers_name=@mname,sex=@sex,nationality=@nation,fathers_ocupation=@focup,mothers_ocupation=@mocup,dob=@DOB,adm_date=@ADM ,religion=@religion,class=@cls,sec=@sec,local_gurdian=@lclgurdn,present_address=@prsntaddress,permanent_address=@peraddress,nat_id_father=@fnid,nat_id_mother=@mnid,nat_id_gurdian=@gnid,phn_land=@land,phn_father=@fmbl,phn_mother=@mmbl,phn_gurdian=@gmbl,messaging_number=@msgmbl,hobby=@hobby,cocurricular=@cocur,st_group=@group,bloodgrp=@blood,email=@email,photo=@photo,card_id=@cid where id=@id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", StdDBselectclassid.thisID);
            cmd.Parameters.AddWithValue("@name", StdEditedInfo.name);
            cmd.Parameters.AddWithValue("@fname", StdEditedInfo.fName);
            cmd.Parameters.AddWithValue("@mname", StdEditedInfo.mName);
            cmd.Parameters.AddWithValue("@sex", StdEditedInfo.sex);
            cmd.Parameters.AddWithValue("@nation", StdEditedInfo.nationality);
            cmd.Parameters.AddWithValue("@focup", StdEditedInfo.focup);
            cmd.Parameters.AddWithValue("@mocup", StdEditedInfo.mocup);
            cmd.Parameters.AddWithValue("@DOB", StdEditedInfo.dob);
            cmd.Parameters.AddWithValue("@ADM", StdEditedInfo.admdate);
            cmd.Parameters.AddWithValue("@religion", StdEditedInfo.religion);
            cmd.Parameters.AddWithValue("@cls", StdEditedInfo.sClass);
            cmd.Parameters.AddWithValue("@sec", StdEditedInfo.sec);
            cmd.Parameters.AddWithValue("@lclgurdn", StdEditedInfo.lclgurdian);
            cmd.Parameters.AddWithValue("@prsntaddress", StdEditedInfo.prsntaddress);
            cmd.Parameters.AddWithValue("@peraddress", StdEditedInfo.peraddress);
            cmd.Parameters.AddWithValue("@fnid", StdEditedInfo.fnid);
            cmd.Parameters.AddWithValue("@mnid", StdEditedInfo.mnid);
            cmd.Parameters.AddWithValue("@gnid", StdEditedInfo.gnid);
            cmd.Parameters.AddWithValue("@land", StdEditedInfo.land);
            cmd.Parameters.AddWithValue("@fmbl", StdEditedInfo.fmbl);
            cmd.Parameters.AddWithValue("@mmbl", StdEditedInfo.mmbl);
            cmd.Parameters.AddWithValue("@gmbl", StdEditedInfo.gmbl);
            cmd.Parameters.AddWithValue("@msgmbl", StdEditedInfo.msgmbl);
            cmd.Parameters.AddWithValue("@hobby", StdEditedInfo.hobby);
            cmd.Parameters.AddWithValue("@cocur", StdEditedInfo.cocurricular);
            cmd.Parameters.AddWithValue("@group", StdEditedInfo.group);
            cmd.Parameters.AddWithValue("@blood", StdEditedInfo.bloodgrp);
            cmd.Parameters.AddWithValue("@email", StdEditedInfo.email);
            cmd.Parameters.AddWithValue("@photo", StdEditedInfo.photo);
            cmd.Parameters.AddWithValue("@cid", StdEditedInfo.cid);
            StdDBselectclassid.thisclass = StdEditedInfo.sClass;
            //MessageBox.Show(query);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Data Entried", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmd.Dispose();
            connection.Close();
            savedImagePath = "";
        }
        private void Save_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to save this information?", "Confirmation!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                save = true;
            }
            else
            {
                save = false;
                return;
            }
            try
            {
                generateValues();
                if (StdEditedInfo.name != "" && StdEditedInfo.fName != "" && StdEditedInfo.mName != "" && StdEditedInfo.focup != "" && StdEditedInfo.mocup != "" && StdEditedInfo.sex != "" && StdEditedInfo.religion != "" && StdEditedInfo.sClass != "" && StdEditedInfo.sec != "" && StdEditedInfo.prsntaddress != "" && StdEditedInfo.peraddress != "" && StdEditedInfo.lclgurdian != "" && StdEditedInfo.fmbl != "" && StdEditedInfo.mmbl != "" && StdEditedInfo.gmbl != "" && StdEditedInfo.bloodgrp != "" && StdEditedInfo.photo != "")
                {
                    //MessageBox.Show(StdEditedInfo.group);
                    if (cls.Text.ToString() == "9" && StdEditedInfo.group == "")
                    {
                        MessageBox.Show("Please enter all required fields(*)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        save = false;
                    }
                    else
                    {
                        SavetoDB();
                        /*sc.Dispose();
                        reader.Dispose();*/
                        save = true;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter all required fields(*)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    save = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                save = false;
            }
            if (this.btn1Click != null)
            {
                clickedButton = Save;
                this.btn1Click(this, e);
            }
        }

        private void StudentInfoEdit_Load(object sender, EventArgs e)
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
                sc = new MySqlCommand("select * from student_info where class='" + StdDBselectclassid.thisclass + "'and id='" + StdDBselectclassid.thisID + "';", connection);
                reader = sc.ExecuteReader();
                reader.Read();
                tempdob = (DateTime)reader["dob"];
                tempadm = (DateTime)reader["adm_date"];
                StdEditedInfo.name = reader["name"].ToString();
                StdEditedInfo.fName = reader["fathers_name"].ToString();
                StdEditedInfo.mName = reader["mothers_name"].ToString();
                StdEditedInfo.mocup = reader["mothers_ocupation"].ToString();
                StdEditedInfo.focup = reader["fathers_ocupation"].ToString();
                StdEditedInfo.sex = reader["sex"].ToString();
                StdEditedInfo.nationality = reader["nationality"].ToString();
                StdEditedInfo.religion = reader["religion"].ToString();
                StdEditedInfo.sClass = reader["class"].ToString();
                StdEditedInfo.sec = reader["sec"].ToString();
                StdEditedInfo.dob = tempdob;
                StdEditedInfo.admdate = tempadm;
                StdEditedInfo.prsntaddress = reader["present_address"].ToString();
                StdEditedInfo.peraddress = reader["permanent_address"].ToString();
                StdEditedInfo.lclgurdian = reader["local_gurdian"].ToString();
                StdEditedInfo.land = reader["phn_land"].ToString();
                StdEditedInfo.fmbl = reader["phn_father"].ToString();
                StdEditedInfo.mmbl = reader["phn_mother"].ToString();
                StdEditedInfo.gmbl = reader["phn_gurdian"].ToString();
                StdEditedInfo.fnid = reader["nat_id_father"].ToString();
                StdEditedInfo.mnid = reader["nat_id_mother"].ToString();
                StdEditedInfo.gnid = reader["nat_id_gurdian"].ToString();
                StdEditedInfo.hobby = reader["hobby"].ToString();
                StdEditedInfo.cocurricular = reader["cocurricular"].ToString();
                StdEditedInfo.bloodgrp = reader["bloodgrp"].ToString();
                StdEditedInfo.group = reader["st_group"].ToString();
                StdEditedInfo.msgmbl = reader["messaging_number"].ToString();
                StdEditedInfo.cid = reader["card_id"].ToString();
                savedImagePath = reader["photo"].ToString();
                stdName.Text = reader["name"].ToString();
                fathersName.Text = reader["fathers_name"].ToString();
                mothersName.Text = reader["mothers_name"].ToString();
                fathersOcupation.Text = reader["fathers_ocupation"].ToString();
                mothersOcupation.Text = reader["mothers_ocupation"].ToString();
                dob.Value = tempdob;
                sex.SelectedIndex = setsex(reader["sex"].ToString());
                Nationality.Text = reader["nationality"].ToString();
                religion.Text = reader["religion"].ToString();
                cls.SelectedIndex = setcls(reader["class"].ToString());
                section.SelectedIndex = setsec(reader["sec"].ToString());
                presentAddress.Text = reader["present_address"].ToString();
                permanentAddress.Text = reader["permanent_address"].ToString();
                localGurdian.Text = reader["local_gurdian"].ToString();
                landMob.Text = reader["phn_land"].ToString();
                fatherMob.Text = reader["phn_father"].ToString();
                motherMob.Text = reader["phn_mother"].ToString();
                gurdianMob.Text = reader["phn_gurdian"].ToString();
                fatherNatID.Text = reader["nat_id_father"].ToString();
                motherNatID.Text = reader["nat_id_mother"].ToString();
                gurdianNatID.Text = reader["nat_id_gurdian"].ToString();
                email.Text = reader["email"].ToString();
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = reader["photo"].ToString();
                Cocurricular.Text = reader["cocurricular"].ToString();
                Hobby.Text = reader["hobby"].ToString();
                cardID.Text = reader["card_id"].ToString();
                if (cls.SelectedItem.ToString() == "9")
                    ComboGrp.SelectedIndex = setgrp(reader["st_group"].ToString());
                ComboBlood.SelectedIndex = setblood(reader["bloodgrp"].ToString());
                //AdmDate.Text = tempadm.ToShortDateString();
                msg = reader["messaging_number"].ToString();
                // MessageBox.Show(msg + " " + fatherMob.Text + " " + motherMob.ToString() + " " + gurdianMob.ToString() + " " + landMob.ToString());
                if (msg == fatherMob.Text)
                {
                    father.Checked = true;
                    mother.Checked = false;
                    gurdian.Checked = false;
                    land.Checked = false;
                }
                else if (msg == motherMob.Text)
                {
                    father.Checked = false;
                    mother.Checked = true;
                    gurdian.Checked = false;
                    land.Checked = false;
                }
                else if (msg == gurdianMob.Text)
                {
                    father.Checked = false;
                    mother.Checked = false;
                    gurdian.Checked = true;
                    land.Checked = false;
                }
                else if (msg == landMob.Text)
                {
                    father.Checked = false;
                    mother.Checked = false;
                    gurdian.Checked = false;
                    land.Checked = true;
                }
                sc.Dispose();
                reader.Dispose();
                name = stdName.Text;

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
        private int setcls(String str)
        {
            int x=0;
            if (str == "1")
                x = 0;
            else if (str == "2")
                x = 1;
            else if (str == "3")
                x = 2;
            else if (str == "4")
                x = 3;
            else if (str == "5")
                x = 4;
            else if (str == "6")
                x = 5;
            else if (str == "7")
                x = 6;
            else if (str == "8")
                x = 7;
            else if (str == "9")
                x = 8;
            else if (str == "Nursery")
                x = 9;
            else if (str == "Infantry")
                x = 10;
            else if (str == "Kg-1")
                x = 11;
            return x;
        }
        private int setsex(String str)
        {
            int x = 0;
            if (str == "Male")
                x = 0;
            else if (str == "Female")
                x = 1;
            return x;
        }
        private int setsec(String str)
        {
            int x = 0;
            if (str == "A")
                x = 0;
            else if (str == "B")
                x = 1;
            else if (str == "C")
                x = 2;
            else if (str == "D")
                x = 3;
            return x;
        }
        private int setgrp(String str)
        {
            int x = 0;
            if (str == "Science")
                x = 0;
            else if (str == "Arts")
                x = 1;
            else if (str == "Commerce")
                x = 2;
            return x;
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
                ComboGrp.Text = "";
            }
        }

        private void stdName_TextChanged(object sender, EventArgs e)
        {
            name = stdName.Text.ToString();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        private void ComboBlood_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                    string query = "delete from student_info where id = '" + StdDBselectclassid.thisID.ToString() + "';";
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
    public class StdEditedInfo
    {
        public static String name;
        public static String fName;
        public static String mName;
        public static String focup;
        public static String mocup;
        public static String sex;
        public static String nationality;
        public static String religion;
        //public static String sId;
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
