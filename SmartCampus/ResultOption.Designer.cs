namespace SmartCampus
{
    partial class ResultOption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultOption));
            this.btnSeeIndividual = new System.Windows.Forms.Button();
            this.btnSeeClassResult = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSeeIndividual
            // 
            this.btnSeeIndividual.BackColor = System.Drawing.Color.Gold;
            this.btnSeeIndividual.FlatAppearance.BorderSize = 0;
            this.btnSeeIndividual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.btnSeeIndividual.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.btnSeeIndividual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeeIndividual.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeeIndividual.Location = new System.Drawing.Point(774, 241);
            this.btnSeeIndividual.Name = "btnSeeIndividual";
            this.btnSeeIndividual.Size = new System.Drawing.Size(313, 60);
            this.btnSeeIndividual.TabIndex = 0;
            this.btnSeeIndividual.Text = "Individual Result";
            this.btnSeeIndividual.UseVisualStyleBackColor = false;
            this.btnSeeIndividual.Click += new System.EventHandler(this.btnSeeIndividual_Click);
            // 
            // btnSeeClassResult
            // 
            this.btnSeeClassResult.BackColor = System.Drawing.Color.Gold;
            this.btnSeeClassResult.FlatAppearance.BorderSize = 0;
            this.btnSeeClassResult.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.btnSeeClassResult.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.btnSeeClassResult.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeeClassResult.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeeClassResult.Location = new System.Drawing.Point(774, 363);
            this.btnSeeClassResult.Name = "btnSeeClassResult";
            this.btnSeeClassResult.Size = new System.Drawing.Size(313, 60);
            this.btnSeeClassResult.TabIndex = 0;
            this.btnSeeClassResult.Text = "Class Result";
            this.btnSeeClassResult.UseVisualStyleBackColor = false;
            this.btnSeeClassResult.Click += new System.EventHandler(this.btnSeeClassResult_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Gold;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DeepPink;
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(774, 491);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(313, 60);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 50F);
            this.label12.ForeColor = System.Drawing.Color.DeepPink;
            this.label12.Location = new System.Drawing.Point(605, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(218, 89);
            this.label12.TabIndex = 211;
            this.label12.Text = "Result";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(274, 241);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(393, 310);
            this.pictureBox1.TabIndex = 212;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(1120, 655);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 50);
            this.pictureBox2.TabIndex = 213;
            this.pictureBox2.TabStop = false;
            // 
            // ResultOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSeeClassResult);
            this.Controls.Add(this.btnSeeIndividual);
            this.Name = "ResultOption";
            this.Size = new System.Drawing.Size(1350, 730);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnSeeIndividual;
        public System.Windows.Forms.Button btnSeeClassResult;
        public System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
