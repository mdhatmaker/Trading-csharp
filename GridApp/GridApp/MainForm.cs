using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HTAPI;

namespace GridApp
{
    public partial class MainForm : Form
    {
        #region Constants
        const int ROW_COUNT = 500;
        
        // Price grid column indexes
        const int COL_THEOBID = 0;
        const int COL_BID = 1;
        const int COL_PRICE = 2;
        const int COL_ASK = 3;
        const int COL_THEOASK = 4;
        
        // Order grid column indexes
        const int COL_OLETTER = 0;
        const int COL_OCONTRACT = 1;
        const int COL_OSIDE = 2;
        const int COL_OQTY = 3;
        const int COL_OPRICE = 4;
        const int COL_ORULE = 5;
        const int COL_ODELAY = 6;
        const int COL_OTICKS = 7;
        const int COL_OPERCENT = 8;
        const int COL_OKEY = 9;
        #endregion

        #region Define class-level variables
        HTAPI.API api = HTAPI.API.Instance;

        double _midPrice = double.NaN;
        double _lowPrice = double.NaN;
        double _highPrice = double.NaN;

        string _priceFormat = "{0}";

        double _insideBid = double.NaN;
        double _insideAsk = double.NaN;

        CellFormat _formatPriceNormal = new CellFormat();
        CellFormat _formatPriceBid = new CellFormat();
        CellFormat _formatPriceAsk = new CellFormat();

        Instrument _dropInstr = null;

        Dictionary<double, DataGridViewRow> _rows = new Dictionary<double, DataGridViewRow>();
        #endregion

        public MainForm()
        {
            InitializeComponent();

            api.OnInstrumentFound += new API.NotifyInstrument(api_OnInstrumentFound);
            api.OnInstrumentNotFound += new API.NotifyInstrument(api_OnInstrumentNotFound);
            api.OnOrderSetUpdate += new API.OrderSetUpdate(api_OnOrderSetUpdate);
            api.OnNotifyDrop += new API.NotifyDrop(api_OnNotifyDrop);
            api.OnInsideMarketUpdate += new API.InsideMarketUpdate(api_OnInsideMarketUpdate);

            api.RegisterDropWindow(this.Handle.ToInt32(), 1);

            InitializeCellFormats();
        }

        #region Handle menu selections
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            api.ShutdownAPI();
            Application.Exit();
        }

        private void aboutOrderManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog(this);
        }
        #endregion

        #region API event handlers
        void api_OnInsideMarketUpdate(Instrument instrument, Quote quote)
        {
            // See if a new instrument has been dropped (if so we need
            // to reset the price grid)
            if (double.IsNaN(_midPrice))
            {
                ResetPriceGrid(quote);
            }
            
            // Highlight the bid/offer of the inside market
            if (!double.IsNaN(_insideBid))
                FormatCell(_insideBid, _formatPriceNormal);
            _insideBid = quote.BidPrice;
            FormatCell(_insideBid, _formatPriceBid);
            if (!double.IsNaN(_insideAsk))
                FormatCell(_insideAsk, _formatPriceNormal);
            _insideAsk = quote.AskPrice;
            FormatCell(_insideAsk, _formatPriceAsk);
            
            Console.WriteLine("{0} {1} {2}", instrument.NetPos, quote.PL, quote.OpenPL);
        }

        void api_OnNotifyDrop(string nickname, int dropID)
        {
            lblProduct.Text = "(retrieving instrument...)";
        }

        void api_OnOrderSetUpdate(OrderSet orderSet)
        {
            //gridOrders.Rows.Clear();

            // Test out the OrderSelector API code:
            OrderSelector select = new OrderSelector();
            select.AddTest("IsBuy", "True");
            OrderSet os = api.CreateOrderSet("buys", select);

            // Clear the bid/ask columns.
            foreach (DataGridViewRow row in gridProduct.Rows)
            {
                row.Cells[COL_BID].Value = "";
                row.Cells[COL_ASK].Value = "";
            }

            int buyOrderCount = 0;
            int sellOrderCount = 0;

            foreach (Order order in orderSet)
            {
                if (order.Contract.Equals(_dropInstr.Contract) && order.Status.Equals("OK"))
                {
                    string letter;
                    int index = OrderGridRowIndex(order);
                    if (index < 0)
                    {
                        letter = FirstAvailableLetter();

                        DataGridViewRow orderRow = new DataGridViewRow();
                        orderRow.CreateCells(gridOrders, letter, order.Contract, order.Side, order.Quantity, PriceString(order.Price), null, null, null, null, order.OrderKey);
                        AddRules(orderRow.Cells[COL_ORULE]);
                        gridOrders.Rows.Add(orderRow);
                    }
                    else
                    {
                        letter = GetOrderCell(index, COL_OLETTER);
                        SetOrderCell(index, COL_OPRICE, order.Price);
                    }
                    DataGridViewRow row = _rows[order.Price.ToDouble()];
                    if (order.IsStopOrder())
                        letter = "(" + letter + ")";
                    row.Cells[COL_BID].Value = row.Cells[COL_BID].Value.ToString() + letter;

                    if (order.Side == OrderSide.BUY)
                        buyOrderCount++;
                    else if (order.Side == OrderSide.SELL)
                        sellOrderCount++;
                }
            }
            /*
            foreach (Order order in sellOrders)
            {
                if (order.Contract.Equals(_dropInstr.Contract) && order.Status.Equals("OK"))
                {
                    string letter;
                    int index = OrderGridRowIndex(order);
                    if (index < 0)
                    {
                        letter = FirstAvailableLetter();

                        DataGridViewRow orderRow = new DataGridViewRow();
                        orderRow.CreateCells(gridOrders, letter, order.Contract, order.Side, order.Quantity, PriceString(order.Price), null, null, null, null, order.OrderKey);
                        AddRules(orderRow.Cells[COL_ORULE]);
                        gridOrders.Rows.Add(orderRow);
                    }
                    else
                    {
                        letter = GetOrderCell(index, COL_OLETTER);
                        SetOrderCell(index, COL_OPRICE, order.Price);
                    }
                    DataGridViewRow row = _rows[order.Price.ToDouble()];
                    if (order.IsStopOrder())
                        letter = "(" + letter + ")";
                    row.Cells[COL_ASK].Value = row.Cells[COL_ASK].Value.ToString() + letter; 
                    sellOrderCount++;
                }
            }*/

            statusLabel1.Text = string.Format("{0} orders ({1} buy  {2} sell)", buyOrderCount + sellOrderCount, buyOrderCount, sellOrderCount);
        }

        void api_OnInstrumentNotFound(Instrument instrument)
        {
            lblProduct.Text = "Error: Instrument not found.";
        }

        void api_OnInstrumentFound(Instrument instrument)
        {
            lblProduct.Text = instrument.Contract;
            _dropInstr = instrument;
            _midPrice = double.NaN;
            _insideBid = double.NaN;
            _insideAsk = double.NaN;
            gridOrders.Rows.Clear();
            statusLabel1.Text = "";
            api.FireInsideMarketUpdate(instrument);
        }
        #endregion

        #region Helper methods
        string PriceString(double price)
        {
            return string.Format(_priceFormat, price);
        }

        string PriceString(Price price)
        {
            return PriceString(price.ToDouble());
        }

        void AddRules(DataGridViewCell cell)
        {
            DataGridViewComboBoxCell combo = cell as DataGridViewComboBoxCell;
            combo.Items.Add("( none )");
            combo.Items.Add("Improvement");
            combo.Items.Add("Retrenching");
            combo.Value = combo.Items[0];
        }

        int OrderGridRowIndex(Order order)
        {
            int result = -1;
            foreach (DataGridViewRow row in gridOrders.Rows)
            {
                if (row.Cells[COL_OKEY].Value.Equals(order.OrderKey))
                    result = row.Index;
            }
            return result;
        }

        string FirstAvailableLetter()
        {
            // First make a list of all letters in use in the grid
            List<string> lettersInUse = new List<string>();
            foreach (DataGridViewRow row in gridOrders.Rows)
            {
                lettersInUse.Add(row.Cells[COL_OLETTER].Value.ToString());
            }

            byte ch = 65;
            string letter;
            while (true)
            {
                letter = ((char)ch).ToString();
                if (!lettersInUse.Contains(letter))
                    break;
                ch++;
            }

            return letter;
        }

        void ResetPriceGrid(Quote quote)
        {
            gridProduct.Rows.Clear();
            _rows.Clear();

            _midPrice = quote.LastPrice.ToDouble();
            _highPrice = _midPrice + (int)(ROW_COUNT / 2) * _dropInstr.TickPrice;
            _lowPrice = _highPrice - ROW_COUNT * _dropInstr.TickPrice;

            string st = _dropInstr.TickPrice.ToString();

            if (st.IndexOf('.') >= 0)
            {
                int i = st.IndexOf('.');
                int decimalPlaces = st.Length - i - 1;
                _priceFormat = "{0:0." + "0000000000".Substring(0, decimalPlaces) + "}";
            }
            else
            {
                _priceFormat = "{0}";
            }

            double price = _highPrice;
            for (int i = 0; i < ROW_COUNT; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(gridProduct, "", "", PriceString(price), "", "");
                gridProduct.Rows.Add(row);
                _rows[price] = row;
                price -= _dropInstr.TickPrice;
            }

            gridProduct.FirstDisplayedScrollingRowIndex = (int)(ROW_COUNT / 2) - (int)(gridProduct.DisplayedRowCount(false) / 2);

            foreach (DataGridViewRow row in gridProduct.Rows)
            {
                row.Height = 16;
            }
        }
        
        void FormatCell(double price, CellFormat format)
        {
            DataGridViewRow row = _rows[price];
            DataGridViewCellStyle style = row.Cells[COL_PRICE].Style;
            style.BackColor = format.BackColor;
            style.ForeColor = format.TextColor;
        }

        void InitializeCellFormats()
        {
            _formatPriceNormal.BackColor = Color.White;
            _formatPriceNormal.TextColor = Color.Black;

            _formatPriceBid.BackColor = Color.Blue;
            _formatPriceBid.TextColor = Color.White;

            _formatPriceAsk.BackColor = Color.Red;
            _formatPriceAsk.TextColor = Color.White;
        }

        string GetOrderCell(int rowIndex, int columnIndex)
        {
            return gridOrders.Rows[rowIndex].Cells[columnIndex].Value.ToString();
        }

        void SetOrderCell(int rowIndex, int columnIndex, object value)
        {
            gridOrders.Rows[rowIndex].Cells[columnIndex].Value = value;
        }
        #endregion

    } // class

    public class CellFormat
    {
        public Color BackColor { get; set; }
        public Color TextColor { get; set; }
    }

} // namespace
