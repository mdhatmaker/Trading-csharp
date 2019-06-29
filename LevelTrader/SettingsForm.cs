using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LevelTrader
{
    public partial class SettingsForm : Form
    {
        private int _smallSize = 1;
        private int _largeSize = 10;

        private const int MAX_ORDER_TYPE = 6;

        public SettingsForm()
        {
            InitializeComponent();

            populateFormFromSettings();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            populateSettingsFromForm();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            populateFormFromSettings();
            this.Hide();
        }

        private void populateFormFromSettings()
        {
            txtSmallSize.Text = _smallSize.ToString();
            txtLargeSize.Text = _largeSize.ToString();

        }

        private void populateSettingsFromForm()
        {
            _smallSize = int.Parse(txtSmallSize.Text);
            _largeSize = int.Parse(txtLargeSize.Text);
        }

        public int LargeSize
        {
            get { return _largeSize; }
        }

        public int SmallSize
        {
            get { return _smallSize; }
        }

    }   // SettingsForm
}