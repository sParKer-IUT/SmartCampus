using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCampus
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AllPasswords.inputPass = passTextBox.Text;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {

        }

    }

    public class AllPasswords
    {
        public static string inputPass;
        public static string accountsPass = "sb_acc";
        public static string masterPass = "sb_master";
        public static string academicPass = "sb_aca";
    }
}
