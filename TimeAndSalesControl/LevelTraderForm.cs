using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using TTAPI;
using Util;

namespace LevelTrader
{
    public partial class MainForm : Form
    {
        private List<string> _labels = new List<string>();
        private List<int> _tags = new List<int>();
        private SortedDictionary<int, KeyValuePair<bool, int> > _orderBook;

        private TTAPI.Trader tt = new TTAPI.Trader();

        private double _tickIncrement;
        private bool _instrumentFound = false;

        private int _previousTradePrice;

        private Instrument _instrument;

        /*
        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern Boolean Beep(UInt32 frequency, UInt32 duration);
        */

        public MainForm()
        {
            InitializeComponent();

            _orderBook = new SortedDictionary<int, KeyValuePair<bool, int> >();

            _MDTraderControl.OrderHover += _MDTraderControl_OrderHover;
            _MDTraderControl.SendOrder += _MDTraderControl_SendOrder;
            _MDTraderControl.ChangeOrdersAtLevel += _MDTraderControl_ChangeOrdersAtLevel;
            _MDTraderControl.DeleteOrdersAtTagLevel += _MDTraderControl_DeleteOrdersAtTagLevel;

            tt.InstrumentFound += new TTAPI.Trader.InstrumentFoundHandler(tt_InstrumentFound);
            tt.QuoteUpdate += new TTAPI.Trader.QuoteUpdateHandler(tt_QuoteUpdate);
            tt.TradeUpdate += new TTAPI.Trader.TradeUpdateHandler(tt_TradeUpdate);
            tt.DepthUpdate += new TTAPI.Trader.DepthUpdateHandler(tt_DepthUpdate);

            _instrument = new Instrument("TTSIM", "FUTURE", "YM", "JUN05");
            int instrumentID = tt.AddInstrument(_instrument, true);

            displayStatus("Opening instrument {0} {1}...", _instrument.Product, _instrument.Contract);
        }

        private void tt_InstrumentFound(object source, Instrument instrument)
        {
            if (_instrumentFound == true) return;

            // Once found, the Contract property should be valid, so display it.
            this.Text = string.Format("LevelTrader: {0} {1}", instrument.Product, instrument.Contract);

            _instrument = instrument;

            Quote mostRecentQuote = tt.MostRecentQuote(instrument);

            _tickIncrement = instrument.TickIncrement;

            resizeMDTrader();

            displayStatus("");

            _instrumentFound = true;
        }

        private void resizeMDTrader()
        {
            Quote mostRecentQuote = tt.MostRecentQuote(_instrument);

            int rows = _MDTraderControl.Height / 13;

            int highPrice = mostRecentQuote.LastPrice + rows / 2;
            int lowPrice = mostRecentQuote.LastPrice - rows / 2;

            // Populate the MDTrader control with the price levels.
            _labels.Clear();
            _tags.Clear();
            for (int i = highPrice; i > lowPrice; i--)
            {
                _labels.Add(i.ToString());
                _tags.Add(i);
            }

            populateMDTraderPrices();
        }

        private void tt_QuoteUpdate(object source, Quote quote)
        {
            _MDTraderControl.SetLTP(quote.LastPrice);
            //_MDTraderControl.SetLTQ(quote.LastQty, 0);
        }

        private void tt_TradeUpdate(object source, Quote quote)
        {
            string price = string.Format("{0}", quote.LastPrice);
            int quantity = quote.LastQty;

            if (quote.LastPrice == quote.BidPrice)
            {
                playDownSound(quote.LastQty);
                tsControl1.InsertTrade(price, quantity, Color.Red);
            }
            else if (quote.LastPrice == quote.AskPrice)
            {
                playUpSound(quote.LastQty);
                tsControl1.InsertTrade(price, quantity, Color.Blue);
            }
            else
            {
                tsControl1.InsertTrade(price, quantity, Color.Black);
            }
        }

        private void tt_DepthUpdate(object source, Dictionary<int,int> bidDepth, Dictionary<int,int> askDepth)
        {
            TradingTechnologies.MDTrader.Depth.Snapshot depthSnapshot = new TradingTechnologies.MDTrader.Depth.Snapshot();

            foreach (int price in bidDepth.Keys)
            {
                int quantity = bidDepth[price];
                depthSnapshot.BidList.Add(new TradingTechnologies.MDTrader.Depth.Item(price, quantity));
            }

            foreach (int price in askDepth.Keys)
            {
                int quantity = askDepth[price];
                depthSnapshot.AskList.Add(new TradingTechnologies.MDTrader.Depth.Item(price, quantity));
            }

            _MDTraderControl.AcceptDepth(depthSnapshot);
        }

        void _MDTraderControl_DeleteOrdersAtTagLevel(int tag)
        {
            if (_orderBook.ContainsKey(tag))
                _orderBook.Remove(tag);

            _MDTraderControl.RemoveWorkingExecAtPriceLevel(tag);
        }

        void _MDTraderControl_ChangeOrdersAtLevel(int origTag, int newTag)
        {
            KeyValuePair<bool, int> pair = _orderBook[origTag];
            _orderBook.Remove(origTag);
            if (_orderBook.ContainsKey(newTag))
                _orderBook.Remove(newTag);

            _orderBook.Add(newTag, pair);

            _MDTraderControl.RemoveWorkingExecAtPriceLevel(origTag);
            _MDTraderControl.SetWorkingExecAtPriceLevel(newTag, pair.Key, pair.Value.ToString());
        }

        void _MDTraderControl_SendOrder(int tag, bool bIsBuy)
        {
            if ( _mdtVerticalControlPanel1.OrderQty == 0 )
                return;

            if (_orderBook.ContainsKey(tag))
            {
                _orderBook.Remove(tag);
            }

            KeyValuePair<bool, int> pair = new KeyValuePair<bool, int>( bIsBuy, _mdtVerticalControlPanel1.OrderQty);
            _orderBook.Add(tag, pair);

            _MDTraderControl.SetWorkingExecAtPriceLevel(tag, bIsBuy, _mdtVerticalControlPanel1.OrderQty.ToString());
        }

        void _MDTraderControl_OrderHover(bool active, int tag)
        {
            // This is here because MDT has a bug that it doesn't
            // check for the OrderHover handler being null or not.
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
        }

        private void populateMDTraderPrices()
        {
            _MDTraderControl.SetGridRows(_labels.ToArray(), _tags.ToArray());

            /*_labels.RemoveRange(0, 3);
            _tags.RemoveRange(0, 3);

            _labels.RemoveRange(_labels.Count-3, 3);
            _tags.RemoveRange(_tags.Count - 3, 3);

            _MDTraderControl2.SetGridRows(_labels.ToArray(), _tags.ToArray());*/
        }

        private void displayStatus(string msg, params object[] param)
        {
            statusLabel.Text = string.Format(msg, param);
            Application.DoEvents();
        }

        private void _MDTraderControl_Resize(object sender, EventArgs e)
        {
            resizeMDTrader();
        }

        private void playUpSound(int tradeQuantity)
        {
            if (tradeQuantity >= 10)
                Sound.PlayWAV("up2.wav");
            else
                Sound.PlayWAV("up1.wav");
        }

        private void playDownSound(int tradeQuantity)
        {
            if (tradeQuantity >= 10)
                Sound.PlayWAV("down2.wav");
            else
                Sound.PlayWAV("down1.wav");
        }

    }   // LevelTraderForm
}