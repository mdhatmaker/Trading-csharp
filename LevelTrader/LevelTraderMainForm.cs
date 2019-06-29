using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TTAPI;
using TT.TradeCo;
using TT.TradeCo.Controls;

namespace LevelTrader
{
    public partial class LevelTraderMainForm : Form
    {
        private TTAPI.Trader tt = new TTAPI.Trader();

        private LevelTraderForm _mainForm = null;
        private FormulaOptionsForm _formulaOptionsForm;
        private List<TT.TradeCo.Controls.PortfolioContractControl> _contracts = new List<TT.TradeCo.Controls.PortfolioContractControl>();

        private PortfolioManager _pm = new PortfolioManager();

        private IManagementFormula _mfConstantDollar;
        private IManagementFormula _mfFixedQuantity;

        private int _contractCount = 0;

        public LevelTraderMainForm()
        {
            InitializeComponent();

            tt.DragDrop += new TTAPI.Trader.DragDropHandler(tt_DragDrop);
            tt.RegisterDropWindow(this);

            _formulaOptionsForm = new FormulaOptionsForm();

            _formulaOptionsForm.ParameterChange += new FormulaOptionsForm.ParameterChangeHandler(formulaOptionsForm_ParameterChange);

            _mfConstantDollar =  new MFConstantDollar((int) _formulaOptionsForm.Parameters.ConstantDollarRisk);
            _mfFixedQuantity = new MFFixedQuantity(_formulaOptionsForm.Parameters.FixedQuantity);
        }

        private void formulaOptionsForm_ParameterChange(PortfolioParameters parameters)
        {
            _pm.Parameters = parameters;
        }

        private void tt_DragDrop(object sender, Instrument instrument)
        {
            if (_mainForm == null)
            {
                _mainForm = new LevelTraderForm(instrument, _pm);
                _mainForm.StrategyAdded += new LevelTraderForm.StrategyUpdateHandler(levelTrader_StrategyAdded);
                //_mainForm.StrategyRemoved += new PortfolioManager.StrategyUpdateHandler(levelTrader_StrategyRemoved);
                _mainForm.Show();
                _mainForm.Location = new Point(this.Location.X, this.Location.Y + this.Height + 5);
                _mainForm.TopMost = true;
                _mainForm.TopMost = false;
            }
            else
            {
                _mainForm.SetInstrument(instrument);
            }

            int x = 5 + 123 * _contractCount;
            int y = 26;
            TT.TradeCo.Controls.PortfolioContractControl contractControl = new TT.TradeCo.Controls.PortfolioContractControl(instrument, x, y); 
            this.Controls.Add(contractControl);
            contractControl.ContractClick += new TT.TradeCo.Controls.PortfolioContractControl.ContractClickHandler(contractControl_ContractClick);
            _contracts.Add(contractControl);
            highlightControl(contractControl);
            _contractCount++;
        }

        private void LevelTraderMainForm_DragEnter(object sender, DragEventArgs e)
        {
            this.Cursor = Cursors.Cross;
        }

        private void LevelTraderMainForm_DragLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void tsbFormulaOptions_Click(object sender, EventArgs e)
        {
            _formulaOptionsForm.Show();
        }

        private void removeChecks()
        {
            itemConstantDollar.Checked = false;
            itemFixedQuantity.Checked = false;
            itemFixedFraction.Checked = false;
            itemKellysCriterion.Checked = false;
            itemOptimalF.Checked = false;
        }

        private void itemConstantDollar_Click(object sender, EventArgs e)
        {
            removeChecks();
            itemConstantDollar.Checked = true;

            _pm.ManagementFormula = _mfConstantDollar;
        }

        private void itemFixedQuantity_Click(object sender, EventArgs e)
        {
            removeChecks();
            itemFixedQuantity.Checked = true;

            _pm.ManagementFormula = _mfFixedQuantity;
        }

        private void itemFixedFraction_Click(object sender, EventArgs e)
        {
            removeChecks();
            itemFixedFraction.Checked = true;
        }

        private void itemKellysCriterion_Click(object sender, EventArgs e)
        {
            removeChecks();
            itemKellysCriterion.Checked = true;
        }

        private void itemOptimalF_Click(object sender, EventArgs e)
        {
            removeChecks();
            itemOptimalF.Checked = true;
        }

        private void contractControl_ContractClick(object source, Instrument instrument)
        {
            highlightControl(source as TT.TradeCo.Controls.PortfolioContractControl);

            _mainForm.SetInstrument(instrument);
        }

        private void highlightControl(TT.TradeCo.Controls.PortfolioContractControl selected)
        {
            foreach (TT.TradeCo.Controls.PortfolioContractControl control in _contracts)
            {
                control.Highlight = false;
            }

            selected.Highlight = true;
        }

        private void levelTrader_StrategyAdded(Instrument instrument, bool isBuy, List<int> legs)
        {
            foreach (TT.TradeCo.Controls.PortfolioContractControl contract in _contracts)
            {
                if (contract.Instrument.ID == instrument.ID)
                {
                    contract.StrategyCount++;
                }
            }
        }


    }   // LevelTraderMainForm
}