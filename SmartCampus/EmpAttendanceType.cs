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
    public partial class EmpAttendanceType : UserControl
    {
        public event EventHandler btn1Click;
        public Button clickedButton;

        public EmpAttendanceType()
        {
            InitializeComponent();
        }

        private void viewAtt_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = viewAtt;
                this.btn1Click(this, e);
            }
        }

        private void viewCO_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = viewCO;
                this.btn1Click(this, e);
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (this.btn1Click != null)
            {
                clickedButton = back;
                this.btn1Click(this, e);
            }
        }
    }
}
