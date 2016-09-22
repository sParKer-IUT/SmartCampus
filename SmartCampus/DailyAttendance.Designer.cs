namespace SmartCampus
{
    partial class DailyAttendance
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
            this.viewAtt = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboClass = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.viewCO = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.viewAtt.Location = new System.Drawing.Point(944, 258);
            this.viewAtt.Name = "viewAtt";
            this.viewAtt.Size = new System.Drawing.Size(240, 61);
            this.viewAtt.TabIndex = 5;
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
            this.back.Location = new System.Drawing.Point(944, 508);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(240, 61);
            this.back.TabIndex = 5;
            this.back.Text = "Back";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(939, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 30);
            this.label2.TabIndex = 72;
            this.label2.Text = "Class:";
            // 
            // ComboClass
            // 
            this.ComboClass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboClass.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboClass.FormattingEnabled = true;
            this.ComboClass.Location = new System.Drawing.Point(944, 122);
            this.ComboClass.Name = "ComboClass";
            this.ComboClass.Size = new System.Drawing.Size(240, 38);
            this.ComboClass.TabIndex = 73;
            this.ComboClass.SelectedIndexChanged += new System.EventHandler(this.ComboClass_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SmartCampus.Properties.Resources.icon_Teacher_attendanceReport;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(179, 122);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(446, 447);
            this.pictureBox1.TabIndex = 74;
            this.pictureBox1.TabStop = false;
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
            this.viewCO.Location = new System.Drawing.Point(944, 381);
            this.viewCO.Name = "viewCO";
            this.viewCO.Size = new System.Drawing.Size(240, 61);
            this.viewCO.TabIndex = 5;
            this.viewCO.Text = "View Check Out";
            this.viewCO.UseVisualStyleBackColor = false;
            this.viewCO.Click += new System.EventHandler(this.viewCO_Click);
            // 
            // DailyAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ComboClass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.viewCO);
            this.Controls.Add(this.viewAtt);
            this.Controls.Add(this.back);
            this.Name = "DailyAttendance";
            this.Size = new System.Drawing.Size(1350, 730);
            this.Load += new System.EventHandler(this.DailyAttendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button viewAtt;
        public System.Windows.Forms.Button back;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboClass;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button viewCO;
    }
}
