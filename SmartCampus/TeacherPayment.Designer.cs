namespace SmartCampus
{
    partial class TeacherPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeacherPayment));
            this.label3 = new System.Windows.Forms.Label();
            this.ComboMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboYear = new System.Windows.Forms.ComboBox();
            this.Back = new System.Windows.Forms.Button();
            this.Proceed = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboDept = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(753, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 37);
            this.label3.TabIndex = 20;
            this.label3.Text = "Month";
            // 
            // ComboMonth
            // 
            this.ComboMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMonth.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboMonth.FormattingEnabled = true;
            this.ComboMonth.Location = new System.Drawing.Point(760, 326);
            this.ComboMonth.Name = "ComboMonth";
            this.ComboMonth.Size = new System.Drawing.Size(202, 38);
            this.ComboMonth.TabIndex = 19;
            this.ComboMonth.SelectedIndexChanged += new System.EventHandler(this.ComboMonth_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(388, 286);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 37);
            this.label4.TabIndex = 18;
            this.label4.Text = "Year";
            // 
            // ComboYear
            // 
            this.ComboYear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboYear.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboYear.FormattingEnabled = true;
            this.ComboYear.Location = new System.Drawing.Point(395, 326);
            this.ComboYear.Name = "ComboYear";
            this.ComboYear.Size = new System.Drawing.Size(202, 38);
            this.ComboYear.TabIndex = 17;
            this.ComboYear.SelectedIndexChanged += new System.EventHandler(this.ComboYear_SelectedIndexChanged);
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
            this.Back.Location = new System.Drawing.Point(737, 488);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(225, 89);
            this.Back.TabIndex = 16;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Proceed
            // 
            this.Proceed.BackColor = System.Drawing.Color.Gold;
            this.Proceed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Proceed.FlatAppearance.BorderSize = 0;
            this.Proceed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.Proceed.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Proceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Proceed.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Proceed.Location = new System.Drawing.Point(395, 488);
            this.Proceed.Name = "Proceed";
            this.Proceed.Size = new System.Drawing.Size(225, 89);
            this.Proceed.TabIndex = 15;
            this.Proceed.Text = "Proceed";
            this.Proceed.UseVisualStyleBackColor = false;
            this.Proceed.Click += new System.EventHandler(this.Proceed_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(753, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 37);
            this.label2.TabIndex = 14;
            this.label2.Text = "ID";
            // 
            // ComboID
            // 
            this.ComboID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboID.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboID.FormattingEnabled = true;
            this.ComboID.Location = new System.Drawing.Point(760, 193);
            this.ComboID.Name = "ComboID";
            this.ComboID.Size = new System.Drawing.Size(202, 38);
            this.ComboID.TabIndex = 13;
            this.ComboID.SelectedIndexChanged += new System.EventHandler(this.ComboID_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(388, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 37);
            this.label1.TabIndex = 12;
            this.label1.Text = "Department";
            // 
            // ComboDept
            // 
            this.ComboDept.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDept.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboDept.FormattingEnabled = true;
            this.ComboDept.Location = new System.Drawing.Point(395, 193);
            this.ComboDept.Name = "ComboDept";
            this.ComboDept.Size = new System.Drawing.Size(202, 38);
            this.ComboDept.TabIndex = 11;
            this.ComboDept.SelectedIndexChanged += new System.EventHandler(this.ComboDept_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 50F);
            this.label12.ForeColor = System.Drawing.Color.DeepPink;
            this.label12.Location = new System.Drawing.Point(399, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(545, 89);
            this.label12.TabIndex = 214;
            this.label12.Text = "Teacher Payment";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(1120, 655);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 50);
            this.pictureBox2.TabIndex = 215;
            this.pictureBox2.TabStop = false;
            // 
            // TeacherPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboMonth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboYear);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Proceed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboDept);
            this.Name = "TeacherPayment";
            this.Size = new System.Drawing.Size(1350, 730);
            this.Load += new System.EventHandler(this.TeacherPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboYear;
        public System.Windows.Forms.Button Back;
        public System.Windows.Forms.Button Proceed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboDept;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox2;


    }
}
