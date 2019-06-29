using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HTAPI;

namespace LagTrader
{
    public partial class MainForm : Form
    {
        const int ROW_COUNT = 1000;
        const int COL_ORDERS = 0;
        const int COL_BID = 1;
        const int COL_PRICE = 2;
        const int COL_ASK = 3;
        const int COL_TRADE = 4;

        HTAPI.API api = HTAPI.API.Instance;

        double _midPriceA = double.NaN;
        double _lowPriceA = double.NaN;
        double _highPriceA = double.NaN;
        double _midPriceB = double.NaN;
        double _lowPriceB = double.NaN;
        double _highPriceB = double.NaN;

        Instrument _dropInstrA = null;
        Instrument _dropInstrB = null;
        int _dropID = 0;

        bool _scrollingA = false;
        bool _scrollingB = false;

        Dictionary<double, DataGridViewRow> _rowsA = new Dictionary<double, DataGridViewRow>();
        Dictionary<double, DataGridViewRow> _rowsB = new Dictionary<double, DataGridViewRow>();
        Dictionary<DataGridView, Dictionary<double, DataGridViewRow>> _rowsLookup = new Dictionary<DataGridView, Dictionary<double, DataGridViewRow>>();

        Quote _insideMarketA = null;
        Quote _insideMarketB = null;
        DepthLevel[] _bidDepthA = null;
        DepthLevel[] _askDepthA = null;
        DepthLevel[] _bidDepthB = null;
        DepthLevel[] _askDepthB = null;

        public MainForm()
        {
            InitializeComponent();

            api.OnInstrumentFound += new API.NotifyInstrument(api_OnInstrumentFound);
            api.OnInstrumentNotFound += new API.NotifyInstrument(api_OnInstrumentNotFound);
            api.OnOrderSetUpdate += new API.OrderSetUpdate(api_OnOrderSetUpdate);
            api.OnNotifyDrop += new API.NotifyDrop(api_OnNotifyDrop);
            api.OnInsideMarketUpdate += new API.InsideMarketUpdate(api_OnInsideMarketUpdate);
            api.OnDepthUpdate += new API.DepthUpdate(api_OnDepthUpdate);
            api.RegisterDropWindow(gridProductA.Handle.ToInt32(), 1);
            api.RegisterDropWindow(gridProductB.Handle.ToInt32(), 2);

            _rowsLookup[gridProductA] = _rowsA;
            _rowsLookup[gridProductB] = _rowsB;
        }

        void api_OnDepthUpdate(Instrument instrument, DepthLevel[] bidDepth, DepthLevel[] askDepth)
        {
            if (instrument.Nickname.Equals("DROP1"))
            {
                if (_bidDepthA != null)
                {
                    for (int i = 0; i < _bidDepthA.Length; i++)
                    {
                        SetCell(gridProductA, _bidDepthA[i].Price, COL_BID, "");
                    }
                }
                if (_askDepthA != null)
                {
                    for (int i = 0; i < _askDepthA.Length; i++)
                    {
                        SetCell(gridProductA, _askDepthA[i].Price, COL_ASK, "");
                    }
                }
                for (int i = 0; i < bidDepth.Length; i++)
                {
                    SetCell(gridProductA, bidDepth[i].Price, COL_BID, bidDepth[i].Qty);
                }
                for (int i = 0; i < askDepth.Length; i++)
                {
                    SetCell(gridProductA, askDepth[i].Price, COL_ASK, askDepth[i].Qty);
                }
                _bidDepthB = bidDepth;
                _askDepthB = askDepth;
            }
            /*else if (instrument.Nickname.Equals("DROP2"))
            {
                if (_bidDepthB != null)
                {
                    for (int i = 0; i < _bidDepthB.Length; i++)
                    {
                        SetCell(gridProductB, _bidDepthB[i].Price, COL_BID, "");
                    }
                }
                if (_askDepthB != null)
                {
                    for (int i = 0; i < _askDepthB.Length; i++)
                    {
                        SetCell(gridProductB, _askDepthB[i].Price, COL_ASK, "");
                    }
                }
                for (int i = 0; i < bidDepth.Length; i++)
                {
                    SetCell(gridProductB, bidDepth[i].Price, COL_BID, bidDepth[i].Qty);
                }
                for (int i = 0; i < askDepth.Length; i++)
                {
                    SetCell(gridProductB, askDepth[i].Price, COL_ASK, askDepth[i].Qty);
                }
                _bidDepthB = bidDepth;
                _askDepthB = askDepth;
            }*/
        }

        void SetCell(DataGridView grid, Price price, int column, object value)
        {
            Dictionary<double, DataGridViewRow> rows = _rowsLookup[grid];
            DataGridViewRow row = rows[price.ToDouble()];
            row.Cells[column].Value = value;
        }

        void api_OnInsideMarketUpdate(Instrument instrument, Quote quote)
        {
            if (_dropID == 0)
            {
                if (instrument.Nickname.Equals("DROP1"))
                {
                    if (_insideMarketA != null)
                    {
                        SetCell(gridProductA, _insideMarketA.BidPrice, COL_BID, "");
                        SetCell(gridProductA, _insideMarketA.AskPrice, COL_ASK, "");
                        SetCell(gridProductA, _insideMarketA.LastPrice, COL_TRADE, "");
                    }
                    SetCell(gridProductA, quote.BidPrice, COL_BID, quote.BidQty);
                    SetCell(gridProductA, quote.AskPrice, COL_ASK, quote.AskQty);
                    SetCell(gridProductA, quote.LastPrice, COL_TRADE, quote.LastQty);

                    _insideMarketA = quote;
                }
                else if (instrument.Nickname.Equals("DROP2"))
                {
                    if (_insideMarketB != null)
                    {
                        SetCell(gridProductB, _insideMarketB.BidPrice, COL_BID, "");
                        SetCell(gridProductB, _insideMarketB.AskPrice, COL_ASK, "");
                        SetCell(gridProductB, _insideMarketB.LastPrice, COL_TRADE, "");
                    }
                    SetCell(gridProductB, quote.BidPrice, COL_BID, quote.BidQty);
                    SetCell(gridProductB, quote.AskPrice, COL_ASK, quote.AskQty);
                    SetCell(gridProductB, quote.LastPrice, COL_TRADE, quote.LastQty);
                    
                    _insideMarketB = quote;
                }
                return;
            }
            
            double price = 0;
            DataGridView grid = null;
            Dictionary<double, DataGridViewRow> rows = null;
            double tickPrice = 0;

            if (_dropID == 1)
            {
                if (double.IsNaN(_midPriceA))
                {
                    tickPrice = _dropInstrA.TickPrice;

                    _midPriceA = quote.LastPrice.ToDouble();
                    _highPriceA = _midPriceA + (int)(ROW_COUNT / 2) * tickPrice;
                    _lowPriceA = _highPriceA - ROW_COUNT * tickPrice;

                    price = _highPriceA;
                    grid = gridProductA;
                    rows = _rowsA;
                }
            }
            else if (_dropID == 2)
            {
                if (double.IsNaN(_midPriceB))
                {
                    tickPrice = _dropInstrB.TickPrice;

                    _midPriceB = quote.LastPrice.ToDouble();
                    _highPriceB = _midPriceB + (int)(ROW_COUNT / 2) * tickPrice;
                    _lowPriceB = _highPriceB - ROW_COUNT * tickPrice;

                    price = _highPriceB;
                    grid = gridProductB;
                    rows = _rowsB;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("_dropID");
            }

            grid.Rows.Clear();
            rows.Clear();

            // Create a price format based on the number of decimal places.
            int decimalPlaces = 0;
            string tickPriceStr = tickPrice.ToString();
            string format = "00000000";
            if (tickPriceStr.EndsWith(".0") || !tickPriceStr.Contains("."))
            {
                format = "{0:0}";
            }
            else
            {
                int i = tickPriceStr.IndexOf(".");
                decimalPlaces = tickPriceStr.Length - i - 1;
                format = format.Substring(0, decimalPlaces);
                format = "{0:0." + format + "}";
            }

            for (int i = 0; i < ROW_COUNT; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(grid, "", "", string.Format(format, price), "", "");
                grid.Rows.Add(row);
                rows[price] = row;
                price -= tickPrice;
            }

            grid.FirstDisplayedScrollingRowIndex = (int)(ROW_COUNT / 2) - (int)(grid.DisplayedRowCount(false) / 2);

            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Height = 14;
            }

            _dropID = 0;
        }

        void api_OnNotifyDrop(string nickname, int dropID)
        {
            _dropID = dropID;
            switch (dropID)
            {
                case 1:
                    lblProductA.Text = "(retrieving dropped instrument...)";
                    break;
                case 2:
                    lblProductB.Text = "(retrieving dropped instrument...)";
                    break;
            }
        }

        void api_OnOrderSetUpdate(List<Order> buyOrders, List<Order> sellOrders)
        {
            return;

            orderSetView.Items.Clear();

            // Clear the bid/ask columns.
            foreach (DataGridViewRow row in gridProductA.Rows)
            {
                row.Cells[COL_BID].Value = "";
                row.Cells[COL_ASK].Value = "";
            }

            byte ch = 65;
            foreach (Order order in buyOrders)
            {
                if (order.Contract.Equals(_dropInstrA.Contract))
                {
                    string letter = ((char)ch).ToString();

                    ListViewItem item = new ListViewItem();
                    item.Text = letter;
                    item.SubItems.Add(order.Contract);
                    item.SubItems.Add(order.Side.ToString());
                    item.SubItems.Add(order.Quantity.ToString());
                    item.SubItems.Add(order.Price.ToString());

                    orderSetView.Items.Add(item);

                    DataGridViewRow row = _rowsA[order.Price.ToDouble()];
                    row.Cells[COL_BID].Value = row.Cells[COL_BID].Value.ToString() + letter;
                    ch++;
                }
            }

            foreach (Order order in sellOrders)
            {
                if (order.Contract.Equals(_dropInstrA.Contract))
                {
                    string letter = ((char)ch).ToString();

                    ListViewItem item = new ListViewItem();
                    item.Text = letter;
                    item.SubItems.Add(order.Contract);
                    item.SubItems.Add(order.Side.ToString());
                    item.SubItems.Add(order.Quantity.ToString());
                    item.SubItems.Add(order.Price.ToString());

                    orderSetView.Items.Add(item);

                    DataGridViewRow row = _rowsA[order.Price.ToDouble()];
                    row.Cells[COL_ASK].Value = row.Cells[COL_ASK].Value.ToString() + letter;

                    ch++;
                }
            }
        }

        void api_OnInstrumentNotFound(Instrument instrument)
        {
            switch (_dropID)
            {
                case 1:
                    lblProductA.Text = "Instrument not found";
                    break;
                case 2:
                    lblProductA.Text = "Instrument not found";
                    break;
            }
        }

        void api_OnInstrumentFound(Instrument instrument)
        {
            switch (_dropID)
            {
                case 1:
                    lblProductA.Text = instrument.Contract;
                    _dropInstrA = instrument;
                    _midPriceA = double.NaN;
                    break;
                case 2:
                    lblProductB.Text = instrument.Contract;
                    _dropInstrB = instrument;
                    _midPriceB = double.NaN;
                    break;
            }
        }

        private void gridProductA_Scroll(object sender, ScrollEventArgs e)
        {
            if (chkLockScroll.Checked == false || e.ScrollOrientation != ScrollOrientation.VerticalScroll || _scrollingA == true)
                return;

            int change = e.NewValue - e.OldValue;
            int topRow = gridProductB.FirstDisplayedScrollingRowIndex;
            
            _scrollingB = true;
            gridProductB.FirstDisplayedScrollingRowIndex = topRow + change;
            _scrollingB = false;
        }

        private void gridProductB_Scroll(object sender, ScrollEventArgs e)
        {
            if (chkLockScroll.Checked == false || e.ScrollOrientation != ScrollOrientation.VerticalScroll || _scrollingB == true)
                return;

            int change = e.NewValue - e.OldValue;
            int topRow = gridProductA.FirstDisplayedScrollingRowIndex;

            _scrollingA = true;
            gridProductA.FirstDisplayedScrollingRowIndex = topRow + change;
            _scrollingA = false;
        }

        private void b(object sender, DataGridViewCellEventArgs e)
        {

        }


    } // class
} // namespace
