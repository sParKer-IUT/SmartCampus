using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCampus
{
    public partial class ResultMain : UserControl
    {
        public event EventHandler resultMainBtnClick;
        public Button clickedButton;

        public ResultMain()
        {
            InitializeComponent();
        }

        private void btn_resultMain_seeResult_Click(object sender, EventArgs e)
        {
            if(resultMainBtnClick != null)
            {
                clickedButton = btn_resultMain_seeResult;
                resultMainBtnClick(this, e);
            }
        }

        private void btn_resultMain_enterMarks_Click(object sender, EventArgs e)
        {
            PasswordForm pass = new PasswordForm();
            if (pass.ShowDialog() == DialogResult.OK)
            {
                if (AllPasswords.inputPass.Equals(AllPasswords.academicPass) || AllPasswords.inputPass.Equals(AllPasswords.masterPass))
                {
                    if (resultMainBtnClick != null)
                    {
                        clickedButton = btn_resultMain_enterMarks;
                        resultMainBtnClick(this, e);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password!!!");
                }
            }
            
        }

        private void btn_resultMain_back_Click(object sender, EventArgs e)
        {
            if (resultMainBtnClick != null)
            {
                clickedButton = btn_resultMain_back;
                resultMainBtnClick(this, e);
            }
        }
    }
}