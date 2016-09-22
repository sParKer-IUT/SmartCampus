namespace SmartCampus
{
    partial class EmpAttendanceType
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
            this.viewCO = new System.Windows.Forms.Button();
            this.viewAtt = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // viewCO
            // 
            this.viewCO.BackColor = System.Drawing.Color.Gold;
            this.viewCO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.viewCO.FlatAppearance.BorderSize = 0;
            this.viewCO.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.viewCO.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.viewCO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewCO.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewCO.Location = new System.Drawing.Point(938, 348);
            this.viewCO.Name = "viewCO";
            this.viewCO.Size = new System.Drawing.Size(240, 61);
            this.viewCO.TabIndex = 75;
            this.viewCO.Text = "View Check Out";
            this.viewCO.UseVisualStyleBackColor = false;
            this.viewCO.Click += new System.EventHandler(this.viewCO_Click);
            // 
            // viewAtt
            // 
            this.viewAtt.BackColor = System.Drawing.Color.Gold;
            this.viewAtt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.viewAtt.FlatAppearance.BorderSize = 0;
            this.viewAtt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.viewAtt.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.viewAtt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.viewAtt.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewAtt.Location = new System.Drawing.Point(938, 225);
            this.viewAtt.Name = "viewAtt";
            this.viewAtt.Size = new System.Drawing.Size(240, 61);
            this.viewAtt.TabIndex = 76;
            this.viewAtt.Text = "View Attendance";
            this.viewAtt.UseVisualStyleBackColor = false;
            this.viewAtt.Click += new System.EventHandler(this.viewAtt_Click);
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.Gold;
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.FlatAppearance.BorderSize = 0;
            this.back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back.Location = new System.Drawing.Point(938, 475);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(240, 61);
            this.back.TabIndex = 77;
            this.back.Text = "Back";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SmartCampus.Properties.Resources.icon_Teacher_attendanceReport;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(173, 168);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(446, 447);
            this.pictureBox1.TabIndex = 80;
            this.pictureBox1.TabStop = false;
            // 
            // EmpAttendanceType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.viewCO);
            this.Controls.Add(this.viewAtt);
            this.Controls.Add(this.back);
            this.Name = "EmpAttendanceType";
            this.Size = new System.Drawing.Size(1350, 730);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button viewCO;
        public System.Windows.Forms.Button viewAtt;
        public System.Windows.Forms.Button back;
    }
}
