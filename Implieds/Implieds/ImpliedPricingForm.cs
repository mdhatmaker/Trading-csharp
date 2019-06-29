using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Implieds
{
    public partial class ImpliedPricingForm : Form
    {
        public ImpliedPricingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog(this);

            if (openFileDialog1.FileName != null)
                LogHelper.ReadLog(openFileDialog1.FileName);
        }


    } // END OF CLASS ImpliedPricingForm
} // END OF NAMESPACE
