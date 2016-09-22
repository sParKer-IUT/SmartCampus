namespace SmartCampus
{
    partial class StudentPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentPayment));
            this.ComboClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboStdID = new System.Windows.Forms.ComboBox();
            this.Proceed = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboYear = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboClass
            // 
            this.ComboClass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboClass.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboClass.FormattingEnabled = true;
            this.ComboClass.Location = new System.Drawing.Point(432, 239);
            this.ComboClass.Name = "ComboClass";
            this.ComboClass.Size = new System.Drawing.Size(202, 38);
            this.ComboClass.TabIndex = 0;
            this.ComboClass.SelectedIndexChanged += new System.EventHandler(this.ComboClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(425, 199);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Class";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(790, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 37);
            this.label2.TabIndex = 4;
            this.label2.Text = "Student ID";
            // 
            // ComboStdID
            // 
            this.ComboStdID.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboStdID.FormattingEnabled = true;
            this.ComboStdID.Location = new System.Drawing.Point(797, 239);
            this.ComboStdID.Name = "ComboStdID";
            this.ComboStdID.Size = new System.Drawing.Size(202, 38);
            this.ComboStdID.TabIndex = 3;
            this.ComboStdID.SelectedIndexChanged += new System.EventHandler(this.ComboStdID_SelectedIndexChanged);
            this.ComboStdID.TextUpdate += new System.EventHandler(this.ComboStdID_TextUpdate);
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
            this.Proceed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Proceed.Location = new System.Drawing.Point(432, 534);
            this.Proceed.Name = "Proceed";
            this.Proceed.Size = new System.Drawing.Size(225, 89);
            this.Proceed.TabIndex = 5;
            this.Proceed.Text = "Proceed";
            this.Proceed.UseVisualStyleBackColor = false;
            this.Proceed.Click += new System.EventHandler(this.Proceed_Click);
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
            this.Back.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Back.Location = new System.Drawing.Point(774, 534);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(225, 89);
            this.Back.TabIndex = 6;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(790, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 37);
            this.label3.TabIndex = 10;
            this.label3.Text = "Month";
            // 
            // ComboMonth
            // 
            this.ComboMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMonth.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboMonth.FormattingEnabled = true;
            this.ComboMonth.Location = new System.Drawing.Point(797, 372);
            this.ComboMonth.Name = "ComboMonth";
            this.ComboMonth.Size = new System.Drawing.Size(202, 38);
            this.ComboMonth.TabIndex = 9;
            this.ComboMonth.SelectedIndexChanged += new System.EventHandler(this.ComboMonth_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(425, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 37);
            this.label4.TabIndex = 8;
            this.label4.Text = "Year";
            // 
            // ComboYear
            // 
            this.ComboYear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboYear.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboYear.FormattingEnabled = true;
            this.ComboYear.Location = new System.Drawing.Point(432, 372);
            this.ComboYear.Name = "ComboYear";
            this.ComboYear.Size = new System.Drawing.Size(202, 38);
            this.ComboYear.TabIndex = 7;
            this.ComboYear.SelectedIndexChanged += new System.EventHandler(this.ComboYear_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 50F);
            this.label12.ForeColor = System.Drawing.Color.DeepPink;
            this.label12.Location = new System.Drawing.Point(443, 32);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(546, 89);
            this.label12.TabIndex = 213;
            this.label12.Text = "Student Payment";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(1120, 655);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 50);
            this.pictureBox2.TabIndex = 214;
            this.pictureBox2.TabStop = false;
            // 
            // StudentPayment
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
            this.Controls.Add(this.ComboStdID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboClass);
            this.ForeColor = System.Drawing.Color.Gold;
            this.Name = "StudentPayment";
            this.Size = new System.Drawing.Size(1350, 730);
            this.Load += new System.EventHandler(this.StudentPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboStdID;
        public System.Windows.Forms.Button Proceed;
        public System.Windows.Forms.Button Back;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboYear;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox2;


    }
}
