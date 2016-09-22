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
    public partial class ResultOption : UserControl
    {
        public event EventHandler resultOptionBtnClick;
        public Button clickedButton;

        public ResultOption()
        {
            InitializeComponent();
        }

        private void btnSeeIndividual_Click(object sender, EventArgs e)
        {
            if(resultOptionBtnClick != null)
            {
                clickedButton = btnSeeIndividual;
                resultOptionBtnClick(this, e);
            }
        }

        private void btnSeeClassResult_Click(object sender, EventArgs e)
        {
            if (resultOptionBtnClick != null)
            {
                clickedButton = btnSeeClassResult;
                resultOptionBtnClick(this, e);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (resultOptionBtnClick != null)
            {
                clickedButton = btnBack;
                resultOptionBtnClick(this, e);
            }
        }
    }
}
