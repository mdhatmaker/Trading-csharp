using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using TTAPI;
using TT.TradeCo;
using TT.TradeCo.Controls;
using Util;

namespace LevelTrader
{
    public partial class LevelTraderForm : Form
    {
        private List<string> _labels = new List<string>();
        private List<int> _tags = new List<int>();
        private SortedDictionary<int, KeyValuePair<bool, int> > _orderBook;
        private List<Instrument> _loadedInstruments = new List<Instrument>();
        private Dictionary<int, List<TimeAndSalesEntry>> _timeAndSales = new Dictionary<int, List<TimeAndSalesEntry>>();
        private Dictionary<int, Quote> _storedQuote = new Dictionary<int, Quote>();
        private Dictionary<int, Quote> _nextStoredQuote = new Dictionary<int, Quote>();

        private TTAPI.Trader tt = new TTAPI.Trader();
        private TTAPI.OrderManager om;

        public delegate void StrategyUpdateHandler(Instrument instrument, bool isBuy, List<int> legs);
        public event StrategyUpdateHandler StrategyAdded;
        public event StrategyUpdateHandler StrategyRemoved;

        private PortfolioManager _pm;

        private double _tickIncrement;
        private bool _isBuying = false;
        private bool _isSelling = false;
        private List<int> _strategyLegs = new List<int>();

        static private Bitmap _cursorBuy = null;
        static private Bitmap _cursorSell = null;
        static private Bitmap _cursorStop = null;
        static private Bitmap _cursorProfit = null;

        private enum CursorType { BUY, SELL, STOP, PROFIT, DEFAULT };

        private SettingsForm _settings = new SettingsForm();

        //private int _previousTradePrice;

        private Instrument _instrument;

        private bool _soundOn = true;

        /*
        [DllImport("Kernel32.dll", SetLastError = true)]
        static extern Boolean Beep(UInt32 frequency, UInt32 duration);
        */

        public LevelTraderForm(Instrument instrument, PortfolioManager pm)
        {
            InitializeComponent();

            om = new TTAPI.OrderManager(tt);
            
            _pm = pm;

            _MDTraderControl.OrderHover += _MDTraderControl_OrderHover;
            _MDTraderControl.SendOrder += _MDTraderControl_SendOrder;
            _MDTraderControl.ChangeOrdersAtLevel += _MDTraderControl_ChangeOrdersAtLevel;
            _MDTraderControl.DeleteOrdersAtTagLevel += _MDTraderControl_DeleteOrdersAtTagLevel;
            _MDTraderControl.LeftBtnDown += _MDTraderControl_LeftBtnDown;

            tt.InstrumentFound += new TTAPI.Trader.InstrumentFoundHandler(tt_InstrumentFound);
            tt.QuoteUpdate += new TTAPI.Trader.QuoteUpdateHandler(tt_QuoteUpdate);
            tt.TradeUpdate += new TTAPI.Trader.TradeUpdateHandler(tt_TradeUpdate);
            tt.DepthUpdate += new TTAPI.Trader.DepthUpdateHandler(tt_DepthUpdate);

            om.FillNotify += new OrderManager.FillHandler(tt_Fill);

            SetInstrument(instrument);
        }

        public void SetInstrument(Instrument instrument)
        {
            _orderBook = new SortedDictionary<int, KeyValuePair<bool, int>>();

            _instrument = instrument;
            int instrumentID = tt.AddInstrument(_instrument, true);

            if (_loadedInstruments.Contains(instrument))
            {
                displayStatus("");
                _tickIncrement = instrument.TickIncrement;
                this.Text = string.Format("LevelTrader: {0}", instrument.Contract);
                recenterMDTrader();
                tsControl1.Clear();
                tsControl1.Populate(_timeAndSales[instrument.ID]);
            }
            else
            {
                displayStatus("Opening instrument {0} {1}...", _instrument.Product, _instrument.Contract);
                tsControl1.Clear();
                _timeAndSales[_instrument.ID] = new List<TimeAndSalesEntry>();
            }
        }

        private void tt_InstrumentFound(object source, Instrument instrument)
        {
            if (_loadedInstruments.Contains(instrument))
                return;

            // Once found, the Contract property should be valid, so display it.
            this.Text = string.Format("LevelTrader: {0}", instrument.Contract);

            _instrument = instrument;

            Quote mostRecentQuote = tt.MostRecentQuote(instrument);
            
            _storedQuote[instrument.ID] = mostRecentQuote;
            _nextStoredQuote[instrument.ID] = mostRecentQuote;

            _tickIncrement = instrument.TickIncrement;

            recenterMDTrader();

            displayStatus("");

            _loadedInstruments.Add(instrument);
        }

        private void recenterMDTrader()
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
                double price = i * _tickIncrement;
                _labels.Add(price.ToString());
                _tags.Add(i);
            }

            populateMDTraderPrices();
        }

        private void tt_QuoteUpdate(object source, Quote quote)
        {
            if (!_nextStoredQuote.ContainsKey(quote.InstrumentID))
                _nextStoredQuote[quote.InstrumentID] = quote;

            _storedQuote[quote.InstrumentID] = _nextStoredQuote[quote.InstrumentID];
            _nextStoredQuote[quote.InstrumentID] = quote;

            if (quote.InstrumentID != _instrument.ID)
                return;

            _MDTraderControl.SetLTP(quote.LastPrice);
        }

        private void tt_TradeUpdate(object source, Quote quote)
        {
            Instrument instrument = tt.GetInstrument(quote.InstrumentID);
            string price = string.Format("{0}", quote.LastPrice * instrument.TickIncrement);
            int quantity = quote.LastQty;

            int bidPrice;
            int askPrice;
            if (!_storedQuote.ContainsKey(quote.InstrumentID))
            {
                bidPrice = quote.BidPrice;
                askPrice = quote.AskPrice;
            }
            else
            {
                bidPrice = _storedQuote[quote.InstrumentID].BidPrice;
                askPrice = _storedQuote[quote.InstrumentID].AskPrice;
            }

            Color color = Color.Black;
            if (quote.LastPrice <= bidPrice)
                color = Color.Red;
            else if (quote.LastPrice >= askPrice)
                color = Color.Blue;
            else
            {
                if (quote.LastPrice <= _nextStoredQuote[quote.InstrumentID].BidPrice)
                {
                    color = Color.Red;
                    bidPrice = _nextStoredQuote[quote.InstrumentID].BidPrice;
                    askPrice = _nextStoredQuote[quote.InstrumentID].AskPrice;
                }
                else if (quote.LastPrice >= _nextStoredQuote[quote.InstrumentID].AskPrice)
                {
                    color = Color.Blue;
                    bidPrice = _nextStoredQuote[quote.InstrumentID].BidPrice;
                    askPrice = _nextStoredQuote[quote.InstrumentID].AskPrice;
                }
                else
                    color = Color.Black;
            }

            TimeAndSalesEntry tsEntry = new TimeAndSalesEntry(price, quantity, color);

            _timeAndSales[quote.InstrumentID].Add(tsEntry);

            // We want to keep the size of our time-and-sales lists down to a reasonable number.
            const int MAX_TSENTRY_COUNT = 100;
            if (_timeAndSales[quote.InstrumentID].Count > MAX_TSENTRY_COUNT)
                _timeAndSales[quote.InstrumentID].RemoveAt(0);

            if (quote.InstrumentID != _instrument.ID)
                return;

            if (quote.LastPrice <= bidPrice)
            {
                playDownSound(quote.LastQty);
                tsControl1.InsertTrade(tsEntry);
            }
            else if (quote.LastPrice >= askPrice)
            {
                playUpSound(quote.LastQty);
                tsControl1.InsertTrade(tsEntry);
            }
            else
            {
                tsControl1.InsertTrade(tsEntry);
            }

            _storedQuote[quote.InstrumentID] = _nextStoredQuote[quote.InstrumentID];
            _nextStoredQuote[quote.InstrumentID] = quote;
        }

        private void tt_DepthUpdate(object source, int instrumentID, Dictionary<double,int> bidDepth, Dictionary<double,int> askDepth)
        {
            if (instrumentID != _instrument.ID)
                return;

            TradingTechnologies.MDTrader.Depth.Snapshot depthSnapshot = new TradingTechnologies.MDTrader.Depth.Snapshot();

            foreach (int price in bidDepth.Keys)
            {
                int quantity = bidDepth[price];
                int tickPrice = (int) (price / _tickIncrement);
                depthSnapshot.BidList.Add(new TradingTechnologies.MDTrader.Depth.Item(tickPrice, quantity));
            }

            foreach (int price in askDepth.Keys)
            {
                int quantity = askDepth[price];
                int tickPrice = (int)(price / _tickIncrement);
                depthSnapshot.AskList.Add(new TradingTechnologies.MDTrader.Depth.Item(tickPrice, quantity));
            }

            _MDTraderControl.AcceptDepth(depthSnapshot);
        }

        private void tt_Fill(object source, Fill fill)
        {
            System.Console.WriteLine("Fill received: {0} {1}", fill.Quantity, fill.Instrument);
            displayStatus("Fill received: {0} {1}", fill.Quantity, fill.Instrument);

            if (!fill.Instrument.Equals(_instrument))
                return;

            int tag = fill.Price;

            //_mdtVerticalControlPanel1.NetPos = om.NetPosition(_instrument);

            if (!_orderBook.ContainsKey(tag))
                return;
            
            KeyValuePair<bool, int> pair = _orderBook[tag];

            _orderBook.Remove(tag);
            _MDTraderControl.RemoveWorkingExecAtPriceLevel(tag);

            int orderQuantity = pair.Value;

            if (fill.Quantity < orderQuantity)
            {
                pair = new KeyValuePair<bool,int>(pair.Key, orderQuantity - fill.Quantity);
                _orderBook.Add(tag, pair);
                _MDTraderControl.SetWorkingExecAtPriceLevel(tag, pair.Key, pair.Value.ToString());
            }

            
        }

        void _MDTraderControl_LeftBtnDown(TradingTechnologies.MDTrader.MouseButtonEventArgs e)
        {
            // Buy column clicked.
            if (e.ColIndex == 2)
            {
                e.Cancel = true;

                if (_isBuying == true) return;

                if (_isSelling == true)
                {
                    // Stop must be greater than entry for sell strategy.
                    if (_strategyLegs.Count == 1 && e.Tag <= _strategyLegs[0])
                        return;

                    // Profit target must be less than entry for sell strategy.
                    if (_strategyLegs.Count == 2 && e.Tag >= _strategyLegs[0])
                        return;
                    
                    _strategyLegs.Add(e.Tag);   // second leg added is stop; third leg added is profit target
                    if (_strategyLegs.Count == 3)
                    {
                        _isSelling = false;
                        _MDTraderControl.SetWorkingExecAtPriceLevel(e.Tag, false, "$");
                        SetCursor(CursorType.DEFAULT);
                        AddStrategy(_instrument, false, _strategyLegs);
                        displayStatus("SELL strategy complete.");
                    }
                    else
                    {
                        _MDTraderControl.SetWorkingExecAtPriceLevel(e.Tag, false, "X");
                        SetCursor(CursorType.PROFIT);
                        displayStatus("SELL strategy: Select profit target price");
                    }
                }
                else
                {
                    _isBuying = true;
                    _strategyLegs.Clear();
                    _strategyLegs.Add(e.Tag);   // first leg added is the entry price
                    _MDTraderControl.SetWorkingExecAtPriceLevel(e.Tag, true, "B");
                    SetCursor(CursorType.STOP);
                    displayStatus("BUY strategy: Select stop price");
                }

                System.Console.WriteLine(e.Tag);
            }
            // Sell column clicked.
            else if (e.ColIndex == 4)
            {
                e.Cancel = true;

                if (_isSelling == true) return;

                if (_isBuying == true)
                {
                    // Stop must be less than entry for buy strategy.
                    if (_strategyLegs.Count == 1 && e.Tag >= _strategyLegs[0])
                        return;

                    // Profit target must be greater than entry for buy strategy.
                    if (_strategyLegs.Count == 2 && e.Tag <= _strategyLegs[0])
                        return;

                    _strategyLegs.Add(e.Tag);   // second leg added is stop; third leg added is profit target
                    if (_strategyLegs.Count == 3)
                    {
                        _isBuying = false;
                        _MDTraderControl.SetWorkingExecAtPriceLevel(e.Tag, true, "$");
                        SetCursor(CursorType.DEFAULT);
                        AddStrategy(_instrument, true, _strategyLegs);
                        displayStatus("BUY strategy complete.");
                    }
                    else
                    {
                        _MDTraderControl.SetWorkingExecAtPriceLevel(e.Tag, true, "X");
                        SetCursor(CursorType.PROFIT);
                        displayStatus("BUY strategy: Select profit target price");
                    }
                }
                else
                {
                    _isSelling = true;
                    _strategyLegs.Clear();
                    _strategyLegs.Add(e.Tag);   // first leg added is the entry price
                    _MDTraderControl.SetWorkingExecAtPriceLevel(e.Tag, false, "S");
                    SetCursor(CursorType.STOP);
                    displayStatus("SELL strategy: Select stop price");
                }

                System.Console.WriteLine(e.Tag);
            }
        }

        void _MDTraderControl_DeleteOrdersAtTagLevel(int tag)
        {
            if (_orderBook.ContainsKey(tag))
                _orderBook.Remove(tag);

            om.DeleteOrdersAtPrice(tag);

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
            /*if ( _mdtVerticalControlPanel1.OrderQty == 0 )
                return;*/

            if (_orderBook.ContainsKey(tag))
            {
                _orderBook.Remove(tag);
            }

            //int orderQuantity = _mdtVerticalControlPanel1.OrderQty;
            int orderQuantity = 1;
            OrderSide buySell = bIsBuy ? OrderSide.BUY : OrderSide.SELL;
            om.SendLimitOrder(_instrument, buySell, orderQuantity, tag);

            KeyValuePair<bool, int> pair = new KeyValuePair<bool, int>( bIsBuy, orderQuantity);
            _orderBook.Add(tag, pair);

            _MDTraderControl.SetWorkingExecAtPriceLevel(tag, bIsBuy, orderQuantity.ToString());
        }

        void _MDTraderControl_OrderHover(bool active, int tag)
        {
            // This is here because MDT has a bug that it doesn't
            // check for the OrderHover handler being null or not.
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
            recenterMDTrader();
        }

        private void playUpSound(int tradeQuantity)
        {
            if (_soundOn == false) return;

            if (tradeQuantity >= _settings.LargeSize)
                Sound.PlayWAV("up2.wav");
            else if (tradeQuantity >= _settings.SmallSize)
                Sound.PlayWAV("up1.wav");
        }

        private void playDownSound(int tradeQuantity)
        {
            if (_soundOn == false) return;

            if (tradeQuantity >= _settings.LargeSize)
                Sound.PlayWAV("down2.wav");
            else if (tradeQuantity >= _settings.SmallSize)
                Sound.PlayWAV("down1.wav");
        }

        private void _MDTraderControl_DoubleClick(object sender, EventArgs e)
        {
            populateMDTraderPrices();
        }

        private void tsbSound_Click(object sender, EventArgs e)
        {
            if (_soundOn == true)
            {
                _soundOn = false;
                tsbSound.Image = LevelTrader.Properties.Resources.AudioHS;
            }
            else
            {
                _soundOn = true;
                tsbSound.Image = LevelTrader.Properties.Resources.BackgroundSoundHS;
            }
        }

        private void LevelTraderForm_Load(object sender, EventArgs e)
        {

        }

        private void tsbCenter_Click(object sender, EventArgs e)
        {
            recenterMDTrader();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            _settings.Show();
        }

        private void tsbTimeAndSales_Click(object sender, EventArgs e)
        {
            if (tsbTimeAndSales.Checked == true)
            {
                tsControl1.Visible = true;
                _MDTraderControl.Width -= tsControl1.Width;
                this.Width += tsControl1.Width;
            }
            else
            {
                tsControl1.Visible = false;
                _MDTraderControl.Width += tsControl1.Width;
                this.Width -= tsControl1.Width;
            }
        }

        private void SetCursor(CursorType cursorType)
        {
            const string cursor_file1 = "leveltrader_buy.bmp";
            const string cursor_file2 = "leveltrader_sell.bmp";
            const string cursor_file3 = "leveltrader_stop.bmp";
            const string cursor_file4 = "leveltrader_profit.bmp";

            if (cursorType == CursorType.DEFAULT)
            {
                _MDTraderControl.ResetCursor();
                return;
            }

            if (_cursorBuy == null)
            {
                _cursorBuy = new Bitmap(Application.StartupPath + "\\" + cursor_file1);
                _cursorSell = new Bitmap(Application.StartupPath + "\\" + cursor_file2);
                _cursorStop = new Bitmap(Application.StartupPath + "\\" + cursor_file3);
                _cursorProfit = new Bitmap(Application.StartupPath + "\\" + cursor_file4);
            }

            // create any bitmap
            //Bitmap b = new Bitmap(55, 25);

            //Bitmap b = new Bitmap(filename);
            //Graphics g = Graphics.FromImage(b);

            // do whatever you wish
            //g.DrawString("myText", this.Font, Brushes.Blue, 0, 0);

            // this is the trick!
            Bitmap b = null;
            switch (cursorType)
            {
                case CursorType.BUY: 
                    b = _cursorBuy;
                    break;
                case CursorType.SELL:
                    b = _cursorSell;
                    break;
                case CursorType.STOP:
                    b = _cursorStop;
                    break;
                case CursorType.PROFIT:
                    b = _cursorProfit;
                    break;
            }

            _MDTraderControl.SetCursor(b, Color.White);

            /*
            b.MakeTransparent(Color.White);
            
            IntPtr ptr = b.GetHicon();
            Cursor c = new Cursor(ptr);
            
            //c.HotSpot.X = 0;
            //c.HotSpot.Y = 0;

            // attach cursor to the form
            this.Cursor = c;
            //_MDTraderControl.Cursor = c;
            */

        }

        private void AddStrategy(Instrument instrument, bool isBuy, List<int> legs)
        {
            Strategy strategy = new Strategy(isBuy, legs[0], legs[1], legs[2], instrument.TickIncrement);
            _pm.AddStrategy(strategy);
            if (StrategyAdded != null) StrategyAdded(instrument, isBuy, legs);
        }

     }   // LevelTraderForm
}