using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpreadPanelControl
{

    public partial class SpreadPanelControl : UserControl
    {
        public enum BuySell { NONE, BUY, SELL };

        public bool Active
        {
            get { return chkActive.Checked; }
            set { chkActive.Checked = value; }
        }

        public int Month
        {
            get { return listMonth.SelectedIndex + 1; }
            set
            {
                if (value > 0)
                    listMonth.SetSelected(value - 1, true);
            }
        }

        public int Year
        {
            get { return int.Parse(listYear.SelectedItem.ToString()); }
            set
            {
                object select = null;
                foreach (object item in listYear.Items)
                {
                    if (int.Parse(item.ToString()) == value)
                        select = item;
                }

                if (select != null)
                    listYear.SelectedItem = select;
            }
        }

        public BuySell Side
        {
            get
            {
                BuySell result = SpreadPanelControl.BuySell.NONE;

                if (radioBuy.Checked == true)
                    result = SpreadPanelControl.BuySell.BUY;
                else if (radioSell.Checked == true)
                    result = SpreadPanelControl.BuySell.SELL;

                return result;
            }
            set
            {
                if (value == BuySell.BUY)
                    radioBuy.Checked = true;
                else if (value == BuySell.SELL)
                    radioSell.Checked = true;

                ColorBackground();
            }
        }

        public SpreadPanelControl()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            Active = true;
            listMonth.ClearSelected();
            listYear.ClearSelected();
            this.BackColor = Color.FromKnownColor(KnownColor.Control);
        }

        private void listMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listYear.SelectedIndex < 0)
            {
                int currentMonth = DateTime.Now.Month;
                int currentYear = DateTime.Now.Year;
                int selectedMonth = listMonth.SelectedIndex + 1;
                if (selectedMonth < currentMonth)
                    Year = currentYear + 1;
                else
                    Year = currentYear;
            }

        }

        private void radioBuy_CheckedChanged(object sender, EventArgs e)
        {
            ColorBackground();
        }

        private void radioSell_CheckedChanged(object sender, EventArgs e)
        {
            ColorBackground();
        }
        
        private void ColorBackground()
        {
            if (radioBuy.Checked == true)
                this.BackColor = Color.LightBlue;
            else
                this.BackColor = Color.IndianRed;

        }

        public void AddStrike(int strike)
        {
            ListViewItem item = new ListViewItem("C" + strike.ToString());
            item.SubItems.Add(strike.ToString() + "P");
            lviewStrike.Items.Add(item);
        }

    } // class
} // namespace
