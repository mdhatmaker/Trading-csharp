using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HTAPI;

namespace APITest
{
    public partial class frmMain : Form
    {
        Instrument i1, i2, i3, i4, i5;

        string _buyOrderKey, _sellOrderKey;

        HTAPI.API api;

        public frmMain()
        {
            InitializeComponent();

            api = API.Instance;

            api.OnInstrumentFound += new API.NotifyInstrument(api_OnInstrumentFound);
            api.OnInstrumentNotFound += new API.NotifyInstrument(api_OnInstrumentNotFound);
            api.OnInsideMarketUpdate += new API.InsideMarketUpdate(api_OnInsideMarketUpdate);
            api.OnDepthUpdate += new API.DepthUpdate(api_OnDepthUpdate);
            api.OnNotifyErrorMessage += new API.NotifyErrorMessage(api_OnNotifyErrorMessage);
            api.OnOrderFill += new API.OrderFill(api_OnOrderFill);
            api.OnOrderStatusUpdate += new API.OrderStatusUdpate(api_OnOrderStatusUpdate);

            i1 = new Instrument("copper", "CME-C", "HG", "Dec11", "FUTURE");
            i2 = new Instrument("HO", "CME-C", "HO", "Oct11", "FUTURE");
            i3 = new Instrument("fake", "CME-C", "ZZZZ", "Dec11", "FUTURE");
            i4 = new Instrument("CA", "LME", "CA", "CA 3M", "FUTURE");
            i5 = new Instrument("gasoil", "ICE_IPE-B", "IPE e-Gas Oil", "Oct11", "FUTURE");

            listMessages.Items.Insert(0, "Waiting for instrument notification...");
        }

        void api_OnOrderStatusUpdate(Instrument instrument, OrderStatus orderStatus, Order order, int qty, string message, object details)
        {
            listMessages.Items.Insert(0, string.Format("ORDER STATUS: {0} {1} {2} {3}", orderStatus, instrument, qty.ToString(), message));
        }

        void api_OnOrderFill(Instrument instrument, Fill fill)
        {
            listMessages.Items.Insert(0, string.Format("FILL: {0} {1} {2} {3}", fill.Instrument.Nickname, fill.Side, fill.Quantity, fill.Price));
        }

        void api_OnNotifyErrorMessage(string message)
        {
            listMessages.Items.Insert(0, "ERROR: " + message);
        }

        void api_OnInsideMarketUpdate(Instrument instrument, Quote quote)
        {
            //Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6}", instrument.Nickname, quote.BidPrice, quote.BidQty, quote.AskPrice, quote.AskQty, quote.LastPrice, quote.LastQty));
        }

        void api_OnDepthUpdate(Instrument instrument, DepthLevel[] bidDepth, DepthLevel[] askDepth)
        {
            //Console.WriteLine(string.Format("{0}  {1}:{2} {3}:{4}", bidDepth.Length, bidDepth[0].Price, bidDepth[0].Qty, askDepth[0].Price, askDepth[0].Qty));
        }

        void api_OnInstrumentNotFound(Instrument instrument)
        {
            string errString = instrument.Exchange + " " + instrument.Product + " " + instrument.Contract + " Instrument Not Found!";
            listMessages.Items.Insert(0, errString);
        }

        void api_OnInstrumentFound(Instrument instrument)
        {
            string foundString = instrument.Exchange + " " + instrument.Product + " " + instrument.Contract + " Instrument sucessfully Found!";
            listMessages.Items.Insert(0, foundString);
        }

        private void btnSendTestOrder_Click(object sender, EventArgs e)
        {
            int submittedQuantity;
            _buyOrderKey = api.SendBuyOrder("crude", null, 1, OrderType.LIMIT, 8600, null, out submittedQuantity);
            _sellOrderKey = api.SendSellOrder("crude", null, 1, OrderType.LIMIT, 9200, null, out submittedQuantity);
        }

        private void btnDeleteAllOrders_Click(object sender, EventArgs e)
        {
            int deletedQuantity;
            api.DeleteAllOrders(out deletedQuantity);
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            int deletedQuantity;
            api.DeleteOrder(_buyOrderKey, out deletedQuantity);
        }

        private void btnModifyOrder_Click(object sender, EventArgs e)
        {
            api.ModifyOrder(_buyOrderKey, int.MinValue, 8550, null, UpdateOrderType.CHANGE);
        }

    } //class
} //namespace
