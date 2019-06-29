using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpreadPanelControlTest
{
    public partial class SpreadToolForm : Form
    {
        public SpreadToolForm()
        {
            InitializeComponent();

            ResetSpreadLegs();

            for (int i = 600; i < 900; i += 5)
            {
                spreadLeg1.AddStrike(i);
                spreadLeg2.AddStrike(i);
                spreadLeg3.AddStrike(i);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            ResetSpreadLegs();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void SpreadToolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = true;
        }

        private void ResetSpreadLegs()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is SpreadPanelControl.SpreadPanelControl)
                {
                    SpreadPanelControl.SpreadPanelControl leg = ctrl as SpreadPanelControl.SpreadPanelControl;
                    leg.Reset();
                }
            }

            spreadLeg1.Side = SpreadPanelControl.SpreadPanelControl.BuySell.BUY;
            spreadLeg2.Side = SpreadPanelControl.SpreadPanelControl.BuySell.SELL;
            spreadLeg3.Active = false;
        }
    } // class
} // namespace
