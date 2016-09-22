namespace SmartCampus
{
    partial class StudentDatabase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentDatabase));
            this.label1 = new System.Windows.Forms.Label();
            this.stdInfo = new System.Windows.Forms.Button();
            this.Payment = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.ComboClass = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboStdID = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Attendance = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DeepPink;
            this.label1.Location = new System.Drawing.Point(435, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(562, 89);
            this.label1.TabIndex = 4;
            this.label1.Text = "Student Database";
            // 
            // stdInfo
            // 
            this.stdInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stdInfo.BackColor = System.Drawing.Color.Gold;
            this.stdInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stdInfo.FlatAppearance.BorderSize = 0;
            this.stdInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.stdInfo.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.stdInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stdInfo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stdInfo.Location = new System.Drawing.Point(878, 167);
            this.stdInfo.Name = "stdInfo";
            this.stdInfo.Size = new System.Drawing.Size(310, 83);
            this.stdInfo.TabIndex = 5;
            this.stdInfo.Text = "Student Information";
            this.stdInfo.UseVisualStyleBackColor = false;
            this.stdInfo.Click += new System.EventHandler(this.stdInfo_Click);
            // 
            // Payment
            // 
            this.Payment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Payment.BackColor = System.Drawing.Color.Gold;
            this.Payment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Payment.FlatAppearance.BorderSize = 0;
            this.Payment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.Payment.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Payment.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Payment.Location = new System.Drawing.Point(878, 325);
            this.Payment.Name = "Payment";
            this.Payment.Size = new System.Drawing.Size(310, 83);
            this.Payment.TabIndex = 7;
            this.Payment.Text = "Payment";
            this.Payment.UseVisualStyleBackColor = false;
            this.Payment.Click += new System.EventHandler(this.Payment_Click);
            // 
            // Back
            // 
            this.Back.BackColor = System.Drawing.Color.Gold;
            this.Back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Back.FlatAppearance.BorderSize = 0;
            this.Back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.Back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Back.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Back.Location = new System.Drawing.Point(878, 500);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(310, 83);
            this.Back.TabIndex = 8;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // ComboClass
            // 
            this.ComboClass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboClass.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboClass.FormattingEnabled = true;
            this.ComboClass.Location = new System.Drawing.Point(325, 225);
            this.ComboClass.Name = "ComboClass";
            this.ComboClass.Size = new System.Drawing.Size(243, 38);
            this.ComboClass.TabIndex = 71;
            this.ComboClass.SelectedIndexChanged += new System.EventHandler(this.ComboClass_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(141, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 45);
            this.label2.TabIndex = 70;
            this.label2.Text = "Class:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(141, 454);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 45);
            this.label3.TabIndex = 73;
            this.label3.Text = "Student ID:";
            // 
            // ComboStdID
            // 
            this.ComboStdID.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboStdID.FormattingEnabled = true;
            this.ComboStdID.Location = new System.Drawing.Point(325, 461);
            this.ComboStdID.Name = "ComboStdID";
            this.ComboStdID.Size = new System.Drawing.Size(243, 38);
            this.ComboStdID.TabIndex = 72;
            this.ComboStdID.SelectedIndexChanged += new System.EventHandler(this.ComboStdID_SelectedIndexChanged);
            this.ComboStdID.TextChanged += new System.EventHandler(this.ComboStdID_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(1120, 655);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 50);
            this.pictureBox2.TabIndex = 74;
            this.pictureBox2.TabStop = false;
            // 
            // Attendance
            // 
            this.Attendance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Attendance.BackColor = System.Drawing.Color.Gold;
            this.Attendance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Attendance.FlatAppearance.BorderSize = 0;
            this.Attendance.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.Attendance.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Attendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Attendance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Attendance.Location = new System.Drawing.Point(553, 289);
            this.Attendance.Name = "Attendance";
            this.Attendance.Size = new System.Drawing.Size(310, 83);
            this.Attendance.TabIndex = 6;
            this.Attendance.Text = "Attendance";
            this.Attendance.UseVisualStyleBackColor = false;
            this.Attendance.Visible = false;
            this.Attendance.Click += new System.EventHandler(this.Attendence_Click);
            // 
            // StudentDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboStdID);
            this.Controls.Add(this.ComboClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Payment);
            this.Controls.Add(this.Attendance);
            this.Controls.Add(this.stdInfo);
            this.Controls.Add(this.label1);
            this.Name = "StudentDatabase";
            this.Size = new System.Drawing.Size(1350, 730);
            this.Load += new System.EventHandler(this.StudentDatabase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button stdInfo;
        public System.Windows.Forms.Button Payment;
        public System.Windows.Forms.Button Back;
        private System.Windows.Forms.ComboBox ComboClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboStdID;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Button Attendance;
    }
}
