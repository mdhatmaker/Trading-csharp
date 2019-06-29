using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TT.TradeCo;

namespace LevelTrader
{
    public partial class FormulaOptionsForm : Form
    {
        public delegate void ParameterChangeHandler(PortfolioParameters parameters);
        public event ParameterChangeHandler ParameterChange;

        private PortfolioParameters _parameters = new PortfolioParameters();

        public FormulaOptionsForm()
        {
            InitializeComponent();
        }

        private void FormulaOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            copyFormToSettings();
            this.Hide();
            if (ParameterChange != null)
                ParameterChange(_parameters);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            copySettingsToForm();
            this.Hide();
        }

        public PortfolioParameters Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        private void copySettingsToForm()
        {
            txtCoreMarket.Text = _parameters.CoreMarket;
            txtBeginningEquity.Text = _parameters.BeginningEquity.ToString();
            txtConstantDollar.Text = _parameters.ConstantDollarRisk.ToString();
            txtFixedQuantity.Text = _parameters.FixedQuantity.ToString();
        }

        private void copyFormToSettings()
        {
            _parameters.CoreMarket = txtCoreMarket.Text;
            _parameters.BeginningEquity = double.Parse(txtBeginningEquity.Text);
            _parameters.ConstantDollarRisk = double.Parse(txtConstantDollar.Text);
            _parameters.FixedQuantity = int.Parse(txtFixedQuantity.Text);
        }

    }   // FormulaOptionsForm
}