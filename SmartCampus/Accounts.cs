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
    public partial class Accounts : UserControl
    {
        public event EventHandler btn1Click;

        public Button clickedButton;

        public Accounts()
        {
            InitializeComponent();
        }

        //to load the UC StudentPayment
        private void stdPayment_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = stdPayment;
                this.btn1Click(this, e);
            }
        }

        //to load the UC TeacherPayment
        private void tchrPayment_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = tchrPayment;
                this.btn1Click(this, e);
            }
        }

        //to go back to UC MainMenu
        private void Back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Back;
                this.btn1Click(this, e);
            }
        }

        //to load the UC Spendings
        private void Spendings_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = Spendings;
                this.btn1Click(this, e);
            }
        }

        private void PaymentRate_Click(object sender, EventArgs e)
        {
            PasswordForm pass = new PasswordForm();
            if (pass.ShowDialog() == DialogResult.OK)
            {
                if (AllPasswords.inputPass.Equals(AllPasswords.masterPass))
                {
                    if (this.btn1Click != null)
                    {
                        clickedButton = PaymentRate;
                        this.btn1Click(this, e);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Password!!!");
                }
            }
        }

        private void Accounts_Load(object sender, EventArgs e)
        {

        }
    }
}
