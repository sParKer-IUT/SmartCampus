﻿namespace SmartCampus
{
    partial class EmpDailyAttendance
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
            this.Show = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.ComboDay = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ComboYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ComboMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.print = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.rbAbsent = new System.Windows.Forms.RadioButton();
            this.rbPresent = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Show
            // 
            this.Show.BackColor = System.Drawing.Color.Gold;
            this.Show.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Show.FlatAppearance.BorderSize = 0;
            this.Show.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.Show.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.Show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Show.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Show.Location = new System.Drawing.Point(347, 661);
            this.Show.Name = "Show";
            this.Show.Size = new System.Drawing.Size(183, 58);
            this.Show.TabIndex = 277;
            this.Show.Text = "Show";
            this.Show.UseVisualStyleBackColor = false;
            this.Show.Click += new System.EventHandler(this.Show_Click);
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
            this.back.Location = new System.Drawing.Point(825, 661);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(183, 58);
            this.back.TabIndex = 276;
            this.back.Text = "Back";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // ComboDay
            // 
            this.ComboDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDay.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboDay.FormattingEnabled = true;
            this.ComboDay.Location = new System.Drawing.Point(840, 58);
            this.ComboDay.Name = "ComboDay";
            this.ComboDay.Size = new System.Drawing.Size(120, 29);
            this.ComboDay.TabIndex = 275;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(599, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 21);
            this.label4.TabIndex = 274;
            this.label4.Text = "Month";
            // 
            // ComboYear
            // 
            this.ComboYear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboYear.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboYear.FormattingEnabled = true;
            this.ComboYear.Location = new System.Drawing.Point(375, 58);
            this.ComboYear.Name = "ComboYear";
            this.ComboYear.Size = new System.Drawing.Size(123, 29);
            this.ComboYear.TabIndex = 273;
            this.ComboYear.SelectedIndexChanged += new System.EventHandler(this.ComboYear_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(371, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 21);
            this.label3.TabIndex = 272;
            this.label3.Text = "Year:";
            // 
            // ComboMonth
            // 
            this.ComboMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMonth.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboMonth.FormattingEnabled = true;
            this.ComboMonth.Location = new System.Drawing.Point(603, 58);
            this.ComboMonth.Name = "ComboMonth";
            this.ComboMonth.Size = new System.Drawing.Size(123, 29);
            this.ComboMonth.TabIndex = 271;
            this.ComboMonth.SelectedIndexChanged += new System.EventHandler(this.ComboMonth_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(836, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 270;
            this.label1.Text = "Day:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(255, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(840, 489);
            this.dataGridView1.TabIndex = 269;
            // 
            // print
            // 
            this.print.BackColor = System.Drawing.Color.Gold;
            this.print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.print.FlatAppearance.BorderSize = 0;
            this.print.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.print.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.print.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.print.Location = new System.Drawing.Point(590, 661);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(183, 58);
            this.print.TabIndex = 278;
            this.print.Text = "Print";
            this.print.UseVisualStyleBackColor = false;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // rbAbsent
            // 
            this.rbAbsent.AutoSize = true;
            this.rbAbsent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rbAbsent.Location = new System.Drawing.Point(696, 610);
            this.rbAbsent.Name = "rbAbsent";
            this.rbAbsent.Size = new System.Drawing.Size(77, 17);
            this.rbAbsent.TabIndex = 281;
            this.rbAbsent.TabStop = true;
            this.rbAbsent.Text = "Absent List";
            this.rbAbsent.UseVisualStyleBackColor = true;
            // 
            // rbPresent
            // 
            this.rbPresent.AutoSize = true;
            this.rbPresent.Checked = true;
            this.rbPresent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.rbPresent.Location = new System.Drawing.Point(590, 610);
            this.rbPresent.Name = "rbPresent";
            this.rbPresent.Size = new System.Drawing.Size(80, 17);
            this.rbPresent.TabIndex = 282;
            this.rbPresent.TabStop = true;
            this.rbPresent.Text = "Present List";
            this.rbPresent.UseVisualStyleBackColor = true;
            // 
            // EmpDailyAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.rbAbsent);
            this.Controls.Add(this.rbPresent);
            this.Controls.Add(this.print);
            this.Controls.Add(this.Show);
            this.Controls.Add(this.back);
            this.Controls.Add(this.ComboDay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ComboYear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboMonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "EmpDailyAttendance";
            this.Size = new System.Drawing.Size(1350, 730);
            this.Load += new System.EventHandler(this.EmpDailyAttendance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Show;
        public System.Windows.Forms.Button back;
        private System.Windows.Forms.ComboBox ComboDay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ComboYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComboMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button print;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.RadioButton rbAbsent;
        private System.Windows.Forms.RadioButton rbPresent;

    }
}
