namespace SmartCampus
{
    partial class StdPaymentStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StdPaymentStatus));
            this.label3 = new System.Windows.Forms.Label();
            this.ComboMonth = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.paylbl = new System.Windows.Forms.Label();
            this.paydate = new System.Windows.Forms.Label();
            this.amount = new System.Windows.Forms.Label();
            this.amountlbl = new System.Windows.Forms.Label();
            this.Back = new System.Windows.Forms.Button();
            this.Check = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(94, 362);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 37);
            this.label3.TabIndex = 14;
            this.label3.Text = "Month";
            // 
            // ComboMonth
            // 
            this.ComboMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMonth.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboMonth.FormattingEnabled = true;
            this.ComboMonth.Location = new System.Drawing.Point(101, 402);
            this.ComboMonth.Name = "ComboMonth";
            this.ComboMonth.Size = new System.Drawing.Size(202, 38);
            this.ComboMonth.TabIndex = 13;
            this.ComboMonth.SelectedIndexChanged += new System.EventHandler(this.ComboMonth_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(94, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 37);
            this.label4.TabIndex = 16;
            this.label4.Text = "Year";
            // 
            // ComboYear
            // 
            this.ComboYear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboYear.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboYear.FormattingEnabled = true;
            this.ComboYear.Location = new System.Drawing.Point(101, 254);
            this.ComboYear.Name = "ComboYear";
            this.ComboYear.Size = new System.Drawing.Size(202, 38);
            this.ComboYear.TabIndex = 15;
            this.ComboYear.SelectedIndexChanged += new System.EventHandler(this.ComboYear_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DeepPink;
            this.label1.Location = new System.Drawing.Point(435, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(496, 89);
            this.label1.TabIndex = 17;
            this.label1.Text = "Payment Status";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Segoe UI", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.ForeColor = System.Drawing.Color.Red;
            this.status.Location = new System.Drawing.Point(622, 271);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(483, 128);
            this.status.TabIndex = 18;
            this.status.Text = "UNPAID!!!";
            // 
            // paylbl
            // 
            this.paylbl.AutoSize = true;
            this.paylbl.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paylbl.ForeColor = System.Drawing.Color.Gold;
            this.paylbl.Location = new System.Drawing.Point(600, 214);
            this.paylbl.Name = "paylbl";
            this.paylbl.Size = new System.Drawing.Size(189, 37);
            this.paylbl.TabIndex = 19;
            this.paylbl.Text = "Payment Date:";
            this.paylbl.Visible = false;
            // 
            // paydate
            // 
            this.paydate.AutoSize = true;
            this.paydate.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paydate.ForeColor = System.Drawing.Color.Gold;
            this.paydate.Location = new System.Drawing.Point(791, 214);
            this.paydate.Name = "paydate";
            this.paydate.Size = new System.Drawing.Size(129, 37);
            this.paydate.TabIndex = 20;
            this.paydate.Text = "12-12-15";
            this.paydate.Visible = false;
            // 
            // amount
            // 
            this.amount.AutoSize = true;
            this.amount.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amount.ForeColor = System.Drawing.Color.Gold;
            this.amount.Location = new System.Drawing.Point(1072, 214);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(32, 37);
            this.amount.TabIndex = 22;
            this.amount.Text = "0";
            this.amount.Visible = false;
            // 
            // amountlbl
            // 
            this.amountlbl.AutoSize = true;
            this.amountlbl.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.amountlbl.ForeColor = System.Drawing.Color.Gold;
            this.amountlbl.Location = new System.Drawing.Point(946, 214);
            this.amountlbl.Name = "amountlbl";
            this.amountlbl.Size = new System.Drawing.Size(118, 37);
            this.amountlbl.TabIndex = 21;
            this.amountlbl.Text = "Amount:";
            this.amountlbl.Visible = false;
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
            this.Back.Location = new System.Drawing.Point(306, 537);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(171, 83);
            this.Back.TabIndex = 23;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = false;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // Check
            // 
            this.Check.BackColor = System.Drawing.Color.Gold;
            this.Check.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Check.FlatAppearance.BorderSize = 0;
            this.Check.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.Check.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Check.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Check.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Check.Location = new System.Drawing.Point(101, 537);
            this.Check.Name = "Check";
            this.Check.Size = new System.Drawing.Size(171, 83);
            this.Check.TabIndex = 24;
            this.Check.Text = "Check";
            this.Check.UseVisualStyleBackColor = false;
            this.Check.Click += new System.EventHandler(this.Check_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(1120, 655);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 50);
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // StdPaymentStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.Check);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.amount);
            this.Controls.Add(this.amountlbl);
            this.Controls.Add(this.paydate);
            this.Controls.Add(this.paylbl);
            this.Controls.Add(this.status);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboMonth);
            this.Name = "StdPaymentStatus";
            this.Size = new System.Drawing.Size(1350, 730);
            this.Load += new System.EventHandler(this.StdPaymentStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label paylbl;
        private System.Windows.Forms.Label paydate;
        private System.Windows.Forms.Label amount;
        private System.Windows.Forms.Label amountlbl;
        public System.Windows.Forms.Button Back;
        public System.Windows.Forms.Button Check;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
