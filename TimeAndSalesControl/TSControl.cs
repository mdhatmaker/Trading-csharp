using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TT.TradeCo.Controls
{
    public partial class TSControl : UserControl
    {
        public TSControl()
        {
            InitializeComponent();
        }

        public void InsertTrade(TimeAndSalesEntry tsEntry)
        {
            string trade = string.Format("{0}\t{1}", tsEntry.Price, tsEntry.Quantity);

            rtbTimeSales.SelectionStart = 0;
            rtbTimeSales.SelectionLength = 0;
            rtbTimeSales.SelectedText = trade + "\n";
            rtbTimeSales.SelectionStart = 0;
            rtbTimeSales.SelectionLength = trade.Length;
            rtbTimeSales.SelectionColor = tsEntry.Color;
            //rtbTimeSales.AppendText(trade + "\n");
            //rtbTimeSales.Text.Insert(0, trade + "\n");
            //rtbTimeSales.Text = trade + "\n" + rtbTimeSales.Text;
            //rtbTimeSales.Select(0, trade.Length);
        }

        public void Clear()
        {
            rtbTimeSales.Clear();
        }

        public void Populate(List<TimeAndSalesEntry> tsEntryList)
        {
            foreach (TimeAndSalesEntry entry in tsEntryList)
                InsertTrade(entry);
        }

    }   // TSControl
}