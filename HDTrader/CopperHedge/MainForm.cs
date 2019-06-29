using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HTAPI;
using TT.Hatmaker;
using System.Threading;
using System.Media;

namespace CopperHedge
{
    public partial class MainForm : Form
    {
        #region Constants
        const int ROW_COUNT = 1000;
        const int COL_ORDERS = 0;
        const int COL_BID = 1;
        const int COL_PRICE = 2;
        const int COL_ASK = 3;
        const int COL_TRADE = 4;
        #endregion

        #region Define class-level variables
        HTAPI.API api = HTAPI.API.Instance;

        double _midPriceA = double.NaN;
        double _lowPriceA = double.NaN;
        double _highPriceA = double.NaN;
        double _midPriceB = double.NaN;
        double _lowPriceB = double.NaN;
        double _highPriceB = double.NaN;

        double _adjustedPL_A = 0;
        double _adjustedPL_B = 0;

        Instrument _dropInstrA = null;
        Instrument _dropInstrB = null;
        int _dropID = 0;

        bool _scrollingA = false;
        bool _scrollingB = false;
        bool _modifyingDeleteOrders = false;

        CellFormat _formatPriceNormal = null;
        CellFormat _formatPriceBid = null;
        CellFormat _formatPriceAsk = null;

        Dictionary<double, DataGridViewRow> _rowsA = new Dictionary<double, DataGridViewRow>();
        Dictionary<double, DataGridViewRow> _rowsB = new Dictionary<double, DataGridViewRow>();
        Dictionary<DataGridView, Dictionary<double, DataGridViewRow>> _rowsLookup = new Dictionary<DataGridView, Dictionary<double, DataGridViewRow>>();
        List<Order> _activeCAOrders = new List<Order>();
        List<string> _requotedCAOrderKeys = new List<string>();
        Dictionary<DateTime, Order> _ordersToDelete = new Dictionary<DateTime, Order>();

        Quote _insideMarketA = null;
        Quote _insideMarketB = null;
        DepthLevel[] _bidDepthA = null;
        DepthLevel[] _askDepthA = null;
        DepthLevel[] _bidDepthB = null;
        DepthLevel[] _askDepthB = null;

        InstrumentForm _instrumentForm = new InstrumentForm();
        PropertyForm _propertyForm = new PropertyForm();
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeComponent();

            api.OnInstrumentFound += new API.NotifyInstrument(api_OnInstrumentFound);
            api.OnInstrumentNotFound += new API.NotifyInstrument(api_OnInstrumentNotFound);
            api.OnOrderSetUpdate += new API.OrderSetUpdate(api_OnOrderSetUpdate);
            api.OnOrderStatusUpdate += new API.OrderStatusUdpate(api_OnOrderStatusUpdate);
            //api.OnNotifyDrop += new API.NotifyDrop(api_OnNotifyDrop);
            api.OnInsideMarketUpdate += new API.InsideMarketUpdate(api_OnInsideMarketUpdate);
            api.OnDepthUpdate += new API.DepthUpdate(api_OnDepthUpdate);
            api.OnOrderFill += new API.OrderFill(api_OnOrderFill);
            
            api.RegisterDropWindow(gridProductA.Handle.ToInt32(), 1);
            api.RegisterDropWindow(gridProductB.Handle.ToInt32(), 2);

            _rowsLookup[gridProductA] = _rowsA;
            _rowsLookup[gridProductB] = _rowsB;

            InitializeCellFormats();
        }
        #endregion

        #region API event handlers
        void api_OnOrderFill(Instrument instrument, Fill fill)
        {
            listMessage.Items.Insert(0, "FILL: " + fill.ToString());

            if (sendFillsToolStripMenuItem.Checked == true && _dropInstrA != null && _dropInstrB != null)
            {
                double PL = _adjustedPL_A + _adjustedPL_B;
                ProwlAPI.Priority priority = ProwlAPI.Priority.NORMAL;
                if (Math.Abs(PL) >= 8000) priority = ProwlAPI.Priority.HIGH;
                string description = string.Format("Fill   CA: {0}  HG: {1}   ${2:0.}", _dropInstrA.NetPos, _dropInstrB.NetPos, PL);
                ProwlAPI.Send(description, fill.ToString(), priority);
            }
        }

        void api_OnOrderStatusUpdate(Instrument instrument, OrderStatus orderStatus, Order order, int qty, string message, object details)
        {
            throw new NotImplementedException();
        }

        void api_OnDepthUpdate(Instrument instrument, DepthLevel[] bidDepth, DepthLevel[] askDepth)
        {
            if (chkUpdateDepth.Checked == false)
                return;

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
                _bidDepthA = bidDepth;
                _askDepthA = askDepth;
            }
            else if (instrument.Nickname.Equals("DROP2"))
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
            }
        }

        void api_OnInsideMarketUpdate(Instrument instrument, Quote quote)
        {
            // Display the net position and PL$ info in the status bar
            if (instrument.Nickname.Equals("DROP1"))
            {
                _adjustedPL_A = instrument.AdjustedPL(quote);
                //lblStatusProduct1.Text = string.Format("{0}  pos: {1}   PL$: {2}", instrument.FormattedName, instrument.NetPos, _adjustedPL_A);
                lblStatusProduct1.Text = string.Format("{0}  pos: {1}", instrument.FormattedName, instrument.NetPos);
            }
            else if (instrument.Nickname.Equals("DROP2"))
            {
                _adjustedPL_B = instrument.AdjustedPL(quote);
                //lblStatusProduct2.Text = string.Format("{0}  pos: {1}   PL$: {2}", instrument.FormattedName, instrument.NetPos, _adjustedPL_B);
                lblStatusProduct2.Text = string.Format("{0}  pos: {1}", instrument.FormattedName, instrument.NetPos);
            }

            // Calculate and display the net P&L for the two products
            double netPL = _adjustedPL_A + _adjustedPL_B;
            lblStatusNetPL.Text = string.Format("${0:0.}", netPL);
            // Display the P&L in red or green depending on its value
            if (netPL < 0)
                lblStatusNetPL.ForeColor = Color.Red;
            else
                lblStatusNetPL.ForeColor = Color.Green;

            // If the user has chosen to NOT update the inside market, then just return
            if (chkUpdateInsideMkt.Checked == false)
                return;

            if (instrument.Nickname.Equals("DROP1"))
            {
                if (double.IsNaN(_midPriceA))
                {
                    InitializeGridPrices(1, quote);
                }

                if (_insideMarketA != null)
                {
                    SetCell(gridProductA, _insideMarketA.BidPrice, COL_BID, "");
                    SetCell(gridProductA, _insideMarketA.AskPrice, COL_ASK, "");
                    SetCell(gridProductA, _insideMarketA.LastPrice, COL_TRADE, "");
                }
                SetCell(gridProductA, quote.BidPrice, COL_BID, quote.BidQty);
                SetCell(gridProductA, quote.AskPrice, COL_ASK, quote.AskQty);
                SetCell(gridProductA, quote.LastPrice, COL_TRADE, quote.LastQty);

                FormatInsideMarketCells(_rowsA, _insideMarketA, quote);
                _insideMarketA = quote;
            }
            else if (instrument.Nickname.Equals("DROP2"))
            {
                if (double.IsNaN(_midPriceB))
                {
                    InitializeGridPrices(2, quote);
                }

                if (_insideMarketB != null)
                {
                    SetCell(gridProductB, _insideMarketB.BidPrice, COL_BID, "");
                    SetCell(gridProductB, _insideMarketB.AskPrice, COL_ASK, "");
                    SetCell(gridProductB, _insideMarketB.LastPrice, COL_TRADE, "");
                }
                SetCell(gridProductB, quote.BidPrice, COL_BID, quote.BidQty);
                SetCell(gridProductB, quote.AskPrice, COL_ASK, quote.AskQty);
                SetCell(gridProductB, quote.LastPrice, COL_TRADE, quote.LastQty);

                FormatInsideMarketCells(_rowsB, _insideMarketB, quote);
                _insideMarketB = quote;
            }

            return;
        }

        void api_OnOrderSetUpdate(OrderSet orderSet)
        {
            if (orderSet.Nickname.Equals("DROP1"))
            {
                orderSetViewA.Items.Clear();
                _activeCAOrders.Clear();
            }
            else if (orderSet.Nickname.Equals("DROP2"))
                orderSetViewB.Items.Clear();
            else
            {
                // right now we only handle the order sets created for the instruments, so just
                // return if this OrderSet is not the one for either of these instruments.
                return;
            }

            // Clear the bid/ask columns.
            /*foreach (DataGridViewRow row in gridProductA.Rows)
            {
                row.Cells[COL_BID].Value = "";
                row.Cells[COL_ASK].Value = "";
            }*/

            byte chA = 65;
            byte chB = 65;
            foreach (Order order in orderSet)
            {
                ListViewItem item = new ListViewItem();

                item.SubItems.Add(order.Status);
                item.SubItems.Add(order.Contract);
                item.SubItems.Add(order.Side.ToString());
                item.SubItems.Add(order.Quantity.ToString());
                item.SubItems.Add(order.Price.ToString());
                if (order.Instrument != null && order.Status.Equals("OK") && order.Instrument.Equals(_dropInstrA))
                {
                    string letter = ((char)chA).ToString();
                    item.Text = letter;
                    orderSetViewA.Items.Add(item);
                    chA++;

                    if (order.Status.Equals("OK"))
                    {
                        _activeCAOrders.Add(order);
                    }
                }
                if (order.Instrument != null && order.Instrument.Equals(_dropInstrB))
                {
                    string letter = ((char)chB).ToString();
                    item.Text = letter;
                    orderSetViewB.Items.Add(item);
                    chB++;
                }                
            }

            // If the user has selected to activate the requoting mechanism, then check the
            // CA orders to see if there is anything to do.
            if (chkRequote.Checked == true)
            {
                HandleCAOrders();
            }
        }

        void api_OnInstrumentNotFound(Instrument instrument)
        {
            switch (_dropID)
            {
                case 1:
                    lblProductA.Text = "Instrument not found";
                    _dropID = 2;
                    break;
                case 2:
                    lblProductB.Text = "Instrument not found";
                    break;
            }
        }

        void api_OnInstrumentFound(Instrument instrument)
        {
            switch (_dropID)
            {
                case 1:
                    lblProductA.Text = instrument.FormattedName;
                    _dropInstrA = instrument;
                    _midPriceA = double.NaN;
                    _dropID = 2;
                    break;
                case 2:
                    lblProductB.Text = instrument.FormattedName;
                    _dropInstrB = instrument;
                    _midPriceB = double.NaN;
                    break;
            }
        }
        #endregion

        #region UI event handlers
        private void gridProductA_Scroll(object sender, ScrollEventArgs e)
        {
            if (chkLockScroll.Checked == false || e.ScrollOrientation != ScrollOrientation.VerticalScroll || _scrollingA == true)
                return;

            int change = (e.NewValue - e.OldValue) / (int)numScrollA.Value;
            int topRow = gridProductB.FirstDisplayedScrollingRowIndex;
            
            _scrollingB = true;
            try
            {
                gridProductB.FirstDisplayedScrollingRowIndex = topRow + (int)numScrollB.Value * change;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            _scrollingB = false;
        }

        private void gridProductB_Scroll(object sender, ScrollEventArgs e)
        {
            if (chkLockScroll.Checked == false || e.ScrollOrientation != ScrollOrientation.VerticalScroll || _scrollingB == true)
                return;

            int change = (e.NewValue - e.OldValue) / (int)numScrollB.Value;
            int topRow = gridProductA.FirstDisplayedScrollingRowIndex;

            _scrollingA = true;
            try
            {
                gridProductA.FirstDisplayedScrollingRowIndex = topRow + (int)numScrollA.Value * change;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            _scrollingA = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadInstruments();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelEventArgs e2 = new CancelEventArgs();
            Application.Exit(e2);
        }

        private void instrumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _instrumentForm.Show(this);
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _propertyForm.Show(this);
        }

        private void sendFillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sendFillsToolStripMenuItem.Checked = !sendFillsToolStripMenuItem.Checked;
        }

        private void chkDeleteOnly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckOrdersToDelete();
        }
        #endregion

        #region Private helper methods
        private void FormatInsideMarketCells(Dictionary<double, DataGridViewRow> rows, Quote existingMarketQuote, Quote quote)
        {
            // Highlight the bid/offer of the inside market
            if (existingMarketQuote != null)
            {
                FormatCell(rows, existingMarketQuote.BidPrice, _formatPriceNormal);
                FormatCell(rows, existingMarketQuote.AskPrice, _formatPriceNormal);
            }
            existingMarketQuote = quote;
            FormatCell(rows, existingMarketQuote.BidPrice, _formatPriceBid);
            FormatCell(rows, existingMarketQuote.AskPrice, _formatPriceAsk);
        }

        void InitializeGridPrices(int gridID, Quote quote)
        {
            double price = 0;
            DataGridView grid = null;
            Dictionary<double, DataGridViewRow> rows = null;
            double tickPrice = 0;

            if (gridID == 1)
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
            else if (gridID == 2)
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
                row.Height = 12;
            }
        }

        void LoadInstruments()
        {
            string[] settings;

            _dropID = 1;
            //_dropInstrA = new Instrument("DROP1", "LME", "CA", "CA 3M", "FUTURE");

            settings = _instrumentForm.GetSettings(0);
            if (settings == null)
            {
                ExitApp("Cannot load settings from file:\n\n" + _instrumentForm.SettingsFile);
                return;
            }
            lblProductA.Text = string.Format("(retrieving {0}...)", settings[1]);

            _dropInstrA = new Instrument("DROP1", settings[0], settings[1], settings[2], settings[3]);

            do
            {
                Application.DoEvents();
                Thread.Sleep(15);
            } while (_dropID == 1);

            _dropID = 2;
            //_dropInstrB = new Instrument("DROP2", "CME-C", "HG", "May12", "FUTURE");

            settings = _instrumentForm.GetSettings(1);
            if (settings == null)
            {
                ExitApp("Cannot load settings from file:\n\n" + _instrumentForm.SettingsFile);
                return;
            }
            lblProductB.Text = string.Format("(retrieving {0}...)", settings[1]);

            _dropInstrB = new Instrument("DROP2", settings[0], settings[1], settings[2], settings[3]);
        }

        private void SetCell(DataGridView grid, Price price, int column, object value)
        {
            Dictionary<double, DataGridViewRow> rows = _rowsLookup[grid];
            if (rows.ContainsKey(price.ToDouble()))
            {
                DataGridViewRow row = rows[price.ToDouble()];
                row.Cells[column].Value = value;
            }
            else
            {
                Console.WriteLine("SetCell could not find key '{0}'", price.ToDouble());
            }
        }

        private void ExitApp(string msg)
        {
            MessageBox.Show(this, msg, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Application.Exit();
        }

        private void DeleteOrder(Order order)
        {
            int deletedQuantity = order.Delete();
            if (deletedQuantity != 0)
            {
                SoundPlayer sound = new SoundPlayer(@"C:\tt\sounds\honk.wav");
                sound.Play();
                string st = string.Format("Order key '{0}' : Deleted = {1}    {2} {3} {4} {5}", order.OrderKey, deletedQuantity, order.Contract, order.Side, order.Quantity, order.Price);
                if (chkDeleteOnly.Checked == false)
                {
                    int submittedQuantity;
                    string orderKey = api.SendOrder(order.Instrument, null, order.Side, order.Quantity, order.OrderType, order.Price, order.StopPrice, out submittedQuantity);
                    _requotedCAOrderKeys.Add(orderKey);
                    st += "        " + string.Format("Order key '{0}' : Submitted = {1}    {2} {3} {4} {5}", orderKey, submittedQuantity, order.Contract, order.Side, order.Quantity, order.Price);
                }
                listMessage.Items.Insert(0, st);
            }
        }

        private void HandleCAOrders()
        {
            foreach (Order order in _activeCAOrders)
            {
                if (!_requotedCAOrderKeys.Contains(order.OrderKey))
                {
                    _requotedCAOrderKeys.Add(order.OrderKey);
                    if (chkDelay.Checked == true)
                    {
                        DateTime currentTime = DateTime.Now;
                        DateTime deleteTime = currentTime.AddMilliseconds((double)numDelay.Value);
                        AddOrderToDeleteList(order, deleteTime);
                    }
                    else
                    {
                        DeleteOrder(order);
                    }
                }
            }
        }

        private void AddOrderToDeleteList(Order order, DateTime deleteTime)
        {
            _modifyingDeleteOrders = true;
            _ordersToDelete[deleteTime] = order;
            _modifyingDeleteOrders = false;
        }

        // Check the current time vs the times of orders waiting to be deleted.
        private void CheckOrdersToDelete()
        {
            if (_modifyingDeleteOrders == true)
                return;

            List<DateTime> deletedOrders = new List<DateTime>();

            foreach (DateTime dt in _ordersToDelete.Keys)
            {
                DateTime currentTime = DateTime.Now;
                if (currentTime.CompareTo(dt) > 0)
                {
                    DeleteOrder(_ordersToDelete[dt]);
                    deletedOrders.Add(dt);
                }
            }
            foreach (DateTime dt in deletedOrders)
            {
                _ordersToDelete.Remove(dt);
            }
        }

        void FormatCell(Dictionary<double,DataGridViewRow> rows, double price, CellFormat format)
        {
            DataGridViewRow row = rows[price];
            DataGridViewCellStyle style = row.Cells[COL_PRICE].Style;
            style.BackColor = format.BackColor;
            style.ForeColor = format.TextColor;
        }

        void InitializeCellFormats()
        {
            _formatPriceNormal = new CellFormat();
            _formatPriceBid = new CellFormat();
            _formatPriceAsk = new CellFormat();

            _formatPriceNormal.BackColor = Color.Gray;
            _formatPriceNormal.TextColor = Color.White;

            _formatPriceBid.BackColor = Color.Blue;
            _formatPriceBid.TextColor = Color.White;

            _formatPriceAsk.BackColor = Color.Red;
            _formatPriceAsk.TextColor = Color.White;
        }
        #endregion

    } // class
    
    public class CellFormat
    {
        public Color BackColor { get; set; }
        public Color TextColor { get; set; }
    }
} // namespace
