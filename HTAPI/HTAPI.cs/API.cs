using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XTAPI;

namespace HTAPI
{
    public class API
    {

        #region Constants
        private const int TRUE = 1, FALSE = 0;
        private const int DEPTH_SIZE = 5;
        #endregion

        #region Delegates and Events
        public delegate void NotifyInstrument(Instrument instrument);
        public delegate void DepthUpdate(Instrument instrument, DepthLevel[] bidDepth, DepthLevel[] askDepth);
        public delegate void InsideMarketUpdate(Instrument instrument, Quote quote);
        public delegate void NotifyErrorMessage(string message);
        public delegate void OrderFill(Instrument instrument, Fill fill);
        public delegate void OrderStatusUdpate(Instrument instrument, OrderStatus orderStatus, Order order, int qty, string message, object details);
        public delegate void OrderSetUpdate(OrderSet orderSet);
        public delegate void NotifyDrop(string nickname, int dropID);
        public event NotifyInstrument OnInstrumentFound;
        public event NotifyInstrument OnInstrumentNotFound;
        public event DepthUpdate OnDepthUpdate;
        public event InsideMarketUpdate OnInsideMarketUpdate;
        public event NotifyErrorMessage OnNotifyErrorMessage;
        public event OrderFill OnOrderFill;
        public event OrderStatusUdpate OnOrderStatusUpdate;
        public event OrderSetUpdate OnOrderSetUpdate;
        public event NotifyDrop OnNotifyDrop;
        #endregion

        #region Singleton design pattern
        private static API _instance = new API();
        static public API Instance { get { return _instance; } }
        #endregion

        #region Declare the XTAPI objects
        private TTGate _ttGate = null;
        private TTInstrNotify _notify = null;
        private TTOrderSet _ttOrderSet = null;
        private TTOrderSet _buyOrders = null;
        private TTOrderSet _sellOrders = null;
        private TTDropHandler _drop1 = null;
        private TTDropHandler _drop2 = null;
        #endregion

        #region Define class-level variables
        //private bool _orderSetIsOpen = false;

        private Dictionary<string, Instrument> _instruments = new Dictionary<string, Instrument>();
        private Dictionary<string, TTInstrObj> _ttInstruments = new Dictionary<string, TTInstrObj>();
        private Dictionary<string, Instrument> _ttToInstrument = new Dictionary<string, Instrument>();
        private Dictionary<Instrument, TTInstrObj> _InstrumentToTT = new Dictionary<Instrument,TTInstrObj>();
        private Dictionary<Instrument, TTOrderSet> _instrTTOrderSets = new Dictionary<Instrument, TTOrderSet>();
        private Dictionary<string, Instrument> _orderSetInstruments = new Dictionary<string, Instrument>();

        private List<string> _customerNames = new List<string>();

        private string _bidDepthValue;
        private string _askDepthValue;

        private Instrument _dropInstrument1, _dropInstrument2;
        //private TTInstrObj _ttDropInstrument;
        #endregion

        #region Constructor and Destructor
        private API()
        {
            _ttGate = new TTGate();
            _ttGate.OnExchangeMessage += new _ITTGateEvents_OnExchangeMessageEventHandler(gateway_OnExchangeMessage);
            _ttGate.OnExchangeStateUpdate += new _ITTGateEvents_OnExchangeStateUpdateEventHandler(gateway_OnExchangeStateUpdate);
            _ttGate.OnSessionRollMessage += new _ITTGateEvents_OnSessionRollMessageEventHandler(gateway_OnSessionRollMessage);
            _ttGate.OnStatusUpdate += new _ITTGateEvents_OnStatusUpdateEventHandler(gateway_OnStatusUpdate);

            _notify = new TTInstrNotify();
            _notify.OnNotifyFound += new _ITTInstrNotifyEvents_OnNotifyFoundEventHandler(notify_OnNotifyFound);
            _notify.OnNotifyNotFound += new _ITTInstrNotifyEvents_OnNotifyNotFoundEventHandler(notify_OnNotifyNotFound);
            _notify.OnNotifyDepthData += new _ITTInstrNotifyEvents_OnNotifyDepthDataEventHandler(notify_OnNotifyDepthData);
            _notify.OnNotifyUpdate += new _ITTInstrNotifyEvents_OnNotifyUpdateEventHandler(notify_OnNotifyUpdate);
            _notify.OnOrderSetUpdate += new _ITTInstrNotifyEvents_OnOrderSetUpdateEventHandler(notify_OnOrderSetUpdate);
            _notify.EnablePriceUpdates = TRUE;
            _notify.EnableDepthUpdates = TRUE;

            // Set up multiple drop handlers
            _drop1 = new TTDropHandler();
            _drop1.OnNotifyDrop += new _ITTDropHandlerEvents_OnNotifyDropEventHandler(drop_OnNotifyDrop1);
            _drop2 = new TTDropHandler();
            _drop2.OnNotifyDrop += new _ITTDropHandlerEvents_OnNotifyDropEventHandler(drop_OnNotifyDrop2);

            // Set the Depth levels to "0" which will return all available depth.
            _bidDepthValue = "BidDepth(0)#";
            _askDepthValue = "AskDepth(0)#";

            _ttOrderSet = new TTOrderSet();
            _ttOrderSet.EnableOrderFillData = TRUE;
            // Enable the orderSet to send an order
            _ttOrderSet.EnableOrderSend = TRUE;
            _ttOrderSet.EnableOrderSetUpdates = TRUE;
            _ttOrderSet.EnableOrderUpdateData = TRUE;
            // Enable the orderSet to retrieve open P&L
            _ttOrderSet.EnableFillCaching = TRUE;

            // Subscribe to the fill events
            _ttOrderSet.OnOrderFillData += new _ITTOrderSetEvents_OnOrderFillDataEventHandler(TTOrderSet_OnOrderFillData);
            _ttOrderSet.OnOrderFillBlockEnd += new _ITTOrderSetEvents_OnOrderFillBlockEndEventHandler(TTOrderSet_OnOrderFillBlockEnd);
            _ttOrderSet.OnOrderFillBlockStart += new _ITTOrderSetEvents_OnOrderFillBlockStartEventHandler(TTOrderSet_OnOrderFillBlockStart);

            // Subscribe to order status events
            _ttOrderSet.OnOrderSubmitted += new _ITTOrderSetEvents_OnOrderSubmittedEventHandler(TTOrderSet_OnOrderSubmitted);
            _ttOrderSet.OnOrderUpdated += new _ITTOrderSetEvents_OnOrderUpdatedEventHandler(TTOrderSet_OnOrderUpdated);
            _ttOrderSet.OnOrderDeleted += new _ITTOrderSetEvents_OnOrderDeletedEventHandler(TTOrderSet_OnOrderDeleted);
            _ttOrderSet.OnOrderHeld += new _ITTOrderSetEvents_OnOrderHeldEventHandler(TTOrderSet_OnOrderHeld);
            _ttOrderSet.OnOrderRejected += new _ITTOrderSetEvents_OnOrderRejectedEventHandler(TTOrderSet_OnOrderRejected);
            _ttOrderSet.OnOrderActionRejected += new _ITTOrderSetEvents_OnOrderActionRejectedEventHandler(TTOrderSet_OnOrderActionRejected);
            _ttOrderSet.OnOrderSetUpdate += new _ITTOrderSetEvents_OnOrderSetUpdateEventHandler(TTOrderSet_OnOrderSetUpdate);

            _buyOrders = new TTOrderSet();
            TTOrderSelector buySelector = new TTOrderSelector();
            buySelector.AddTest("BuySell", "B");
            _buyOrders.OrderSelector = buySelector;
            _buyOrders.Open(0);

            _sellOrders = new TTOrderSet();
            TTOrderSelector sellSelector = new TTOrderSelector();
            sellSelector.AddTest("BuySell", "S");
            _sellOrders.OrderSelector = sellSelector;
            _sellOrders.Open(0);

            //_notify.UpdateFilter = "BidQty,Bid&,Ask&,AskQty,Last&,LastQty,Volume&";
        }

        ~API()
        {
            ShutdownAPI();
        }
        #endregion

        #region XTAPI Event Notifications
        void notify_OnOrderSetUpdate(TTInstrNotify pNotify, TTInstrObj pInstr, TTOrderSet pOrderSet)
        {
            UpdatePLInformation(pInstr);
        }

        void TTOrderSet_OnOrderSetUpdate(TTOrderSet pOrderSet)
        {
            /*int i = _buyOrders.Count;
            i = _sellOrders.Count;
            List<Order> buyOrders = GetOrders(_buyOrders);
            List<Order> sellOrders = GetOrders(_sellOrders);
            */
            //Instrument instrument = _instruments[pOrderSet.Alias];
            OrderSet orderSet = GetOrderSet(pOrderSet);

            if (OnOrderSetUpdate != null) OnOrderSetUpdate(orderSet);
        }

        void TTOrderSet_OnOrderActionRejected(TTOrderObj pNewOrderObj, TTOrderObj pOldOrderObj, string sSiteOrderKey, enumOrderNotifyState eOrderState, enumOrderAction eOrderAction, int rejectQty, string sRejectMessage)
        {
            Order order = TTOrderToOrder(pNewOrderObj);

            Object details = new Object();
            if (OnOrderStatusUpdate != null) OnOrderStatusUpdate(order.Instrument, OrderStatus.ACTION_REJECTED, order, rejectQty, sRejectMessage, details);
        }

        void TTOrderSet_OnOrderUpdated(TTOrderObj pNewOrderObj, TTOrderObj pOldOrderObj, string sSiteOrderKey, enumOrderNotifyState eOrderState, enumOrderAction eOrderAction, int updQty, string sOrderType, string sOrderTraits)
        {
            Order order = TTOrderToOrder(pNewOrderObj);

            Object details = new Object();
            if (OnOrderStatusUpdate != null) OnOrderStatusUpdate(order.Instrument, OrderStatus.UPDATED, order, updQty, "", details);
        }

        void TTOrderSet_OnOrderSubmitted(TTOrderObj pNewOrderObj, TTOrderObj pOldOrderObj, string sSiteOrderKey, enumOrderAction eOrderAction, int wrkQty, string sOrderType, string sOrderTraits)
        {
            Order order = TTOrderToOrder(pNewOrderObj);

            Object details = new Object();
            if (OnOrderStatusUpdate != null) OnOrderStatusUpdate(order.Instrument, OrderStatus.SUBMITTED, order, wrkQty, "", details);
        }

        void TTOrderSet_OnOrderRejected(TTOrderObj pRejectedOrderObj)
        {
            Order order = TTOrderToOrder(pRejectedOrderObj);

            Object details = new Object();
            if (OnOrderStatusUpdate != null) OnOrderStatusUpdate(order.Instrument, OrderStatus.REJECTED, order, int.MinValue, "", details);
        }

        void TTOrderSet_OnOrderHeld(TTOrderObj pNewOrderObj, TTOrderObj pOldOrderObj, string sSiteOrderKey, enumOrderAction eOrderAction, int ordQty)
        {
            Order order = TTOrderToOrder(pNewOrderObj);

            Object details = new Object();
            if (OnOrderStatusUpdate != null) OnOrderStatusUpdate(order.Instrument, OrderStatus.HELD, order, ordQty, "", details);
        }

        void TTOrderSet_OnOrderDeleted(TTOrderObj pNewOrderObj, TTOrderObj pOldOrderObj, string sSiteOrderKey, enumOrderNotifyState eOrderState, enumOrderAction eOrderAction, int delQty)
        {
            Order order = TTOrderToOrder(pNewOrderObj);

            Object details = new Object();
            if (OnOrderStatusUpdate != null) OnOrderStatusUpdate(order.Instrument, OrderStatus.DELETED, order, delQty, "", details);
        }

        void notify_OnNotifyUpdate(TTInstrNotify pNotify, TTInstrObj pInstr)
        {
            UpdatePLInformation(pInstr);
            FireInsideMarketUpdate(pInstr);
        }

        /// <summary>
        /// This function displays is called for every
        /// change to the instrument depth.
        /// </summary>
        /// <param name="pNotify">TTInstrNotify object</param>
        /// <param name="pInstr">TTInstrObj object</param>
        void notify_OnNotifyDepthData(XTAPI.TTInstrNotify pNotify, XTAPI.TTInstrObj pInstr)
        {

            // Obtain the bid depth (Level based on user selection).
            Array dataArrayBid = (Array)pInstr.get_Get(_bidDepthValue);

            List<DepthLevel> bidDepthList = new List<DepthLevel>();
            DepthLevel[] bidDepth = null;
            // Test if depth exists.
            if (dataArrayBid != null)
            {
                // Iterate through the depth array.
                for (int i = 0; i <= dataArrayBid.GetUpperBound(0); i++)
                {
                    // Break out of FOR LOOP if index value is null.
                    if (dataArrayBid.GetValue(i, 0) == null)
                        break;

                    double price = double.Parse((string)dataArrayBid.GetValue(i, 0));
                    int qty = (int)dataArrayBid.GetValue(i, 1);
                    bidDepthList.Add(new DepthLevel(new Price(price), qty));
                    // Update the bid depth list box.
                    //lboBidDepth.Items.Add("BidPrice: " + dataArrayBid.GetValue(i, 0) + " | " + " BidQty: " + dataArrayBid.GetValue(i, 1));
                }
            }
            bidDepth = new DepthLevel[bidDepthList.Count];
            bidDepthList.CopyTo(bidDepth);

            // Obtain the ask depth (Level based on user selection).
            Array dataArrayAsk = (Array)pInstr.get_Get(_askDepthValue);

            List<DepthLevel> askDepthList = new List<DepthLevel>();
            DepthLevel[] askDepth = null;
            // Test if depth exists.
            if (dataArrayAsk != null)
            {
                // Iterate through the depth array.
                for (int i = 0; i <= dataArrayAsk.GetUpperBound(0); i++)
                {
                    // Break out of FOR LOOP if index value is null.
                    if (dataArrayAsk.GetValue(i, 0) == null)
                        break;

                    double price = double.Parse((string)dataArrayAsk.GetValue(i, 0));
                    int qty = (int)dataArrayAsk.GetValue(i, 1);
                    askDepthList.Add(new DepthLevel(new Price(price), qty));
                    // Update the bid depth list box.
                    //lboAskDepth.Items.Add(" AskPrice: " + dataArrayAsk.GetValue(i, 0) + " | " + " AskQty: " + dataArrayAsk.GetValue(i, 1));
                }
            }
            askDepth = new DepthLevel[askDepthList.Count];
            askDepthList.CopyTo(askDepth);

            if (OnDepthUpdate != null) OnDepthUpdate(GetInstrument(pInstr), bidDepth, askDepth);
        }

        /*
        void notify_OnNotifyDepthData(TTInstrNotify pNotify, TTInstrObj pInstr)
        {
            Dictionary<int, Quote> bidDepth = new Dictionary<int, Quote>();
            Dictionary<int, Quote> askDepth = new Dictionary<int, Quote>();
            object[,] depth;

            depth = (object[,])pInstr.get_Get(string.Format("BidDepth({0})", DEPTH_SIZE));
            for (int i = 0; i < DEPTH_SIZE; i++)
            {
                if (depth[i, 0] != null)
                {
                    double price = double.Parse(depth[i, 0].ToString());
                    Quote quote = new Quote();
                    quote.LastPrice = new Price(price);
                    bidDepth.Add((int)depth[i, 1], quote);
                }
            }

            depth = (object[,])pInstr.get_Get(string.Format("AskDepth({0})", DEPTH_SIZE));
            for (int i = 0; i < DEPTH_SIZE; i++)
            {
                if (depth[i, 0] != null)
                {
                    double price = double.Parse(depth[i, 0].ToString());
                    Quote quote = new Quote();
                    quote.LastPrice = new Price(price);
                    askDepth.Add((int)depth[i, 1], quote);
                }
            }

            int instrumentID = pInstr.GetHashCode();

            if (OnDepthUpdate != null) OnDepthUpdate(GetInstrument(pInstr), bidDepth, askDepth);

            if (OnInsideMarketUpdate != null) OnInsideMarketUpdate(GetInstrument(pInstr), new Quote());
 
        }
        */

        void notify_OnNotifyNotFound(TTInstrNotify pNotify, TTInstrObj pInstr)
        {
            string nickname = GetNickname(pInstr);
            Instrument instrument = _instruments[nickname];
            instrument.Alias = pInstr.Alias;
            instrument.Contract = pInstr.Contract;
            instrument.Exchange = pInstr.Exchange;
            instrument.ProdType = pInstr.ProdType;
            instrument.Product = pInstr.Product;
            instrument.SeriesKey = pInstr.SeriesKey; 
            
            if (OnInstrumentNotFound != null)
                OnInstrumentNotFound(instrument);
        }

        void notify_OnNotifyFound(TTInstrNotify pNotify, TTInstrObj pInstr)
        {
            string nickname = GetNickname(pInstr);
            Instrument instrument = _instruments[nickname];
            instrument.Alias = pInstr.Alias;
            instrument.Contract = pInstr.Contract;
            instrument.Exchange = pInstr.Exchange;
            instrument.ProdType = pInstr.ProdType;
            instrument.Product = pInstr.Product;
            instrument.SeriesKey = pInstr.SeriesKey;
            instrument.TickPrice = (double) pInstr.get_TickPrice(0, 1, "#");
            instrument.PointValue = (double)pInstr.get_Get("PointValue"); 

            _ttToInstrument[instrument.SeriesKey] = instrument;

            /*if (!_instrOrderSets.ContainsKey(instrument))
            {
                _instrOrderSets[instrument] = CreateTTOrderSet(nickname);
                _instrOrderSets[instrument].Open(TRUE);
                //_orderSetInstruments[nickname] = instrument;
            }

            pInstr.OrderSet = _instrOrderSets[instrument];*/

            /*if (_orderSetIsOpen == false)
            {
                // Set the Net Limits to false.
                _ttOrderSet.Set("NetLimits", false);
                // Open the TTOrderSet with send orders enabled.
                _ttOrderSet.Open(TRUE);

                _orderSetIsOpen = true;
            }*/

            // Attach the instrument to a TTOrderSet object
            //pInstr.OrderSet = _ttOrderSet;

            instrument.NetPos = (int)pInstr.get_Get("NetPos");
            Console.WriteLine("Initial netpos: {0}", instrument.NetPos);

            if (OnInstrumentFound != null)
                OnInstrumentFound(instrument);
        }

        /// <summary>
        /// This function is called for every fill update.
        /// Obtain the fill information by calling the Get()
        /// properties from the TTFillObj passed as an argument.   
        /// </summary>
        /// <param name="pFillObj">XTAPI Fill Object</param>
        private void TTOrderSet_OnOrderFillData(TTFillObj pFillObj)
        {
            // Update the Status Bar text.
            //sbaStatus.Text = "Fill Received.";

            Fill fill = new Fill();
            
            // TODO: Why does the TTFillObj not return a valid TTInstrObj?
            /*TTInstrObj tto = (TTInstrObj)pFillObj.Instrument;
            fill.Instrument = _ttToInstrument[tto];*/

            // Retrieve the fill information using the TTFillObj Get Properties.
            //Array fillData = (Array)pFillObj.get_Get("SeriesKey,SiteOrderKey,BuySell,Qty,Price,FillType,FFT1,FFT2,FFT3,Instr,InstrAlias,DateExec,TimeExec$");
            Array fillData = (Array)pFillObj.get_Get("SeriesKey,SiteOrderKey,BuySell,Qty,Price,FillType,FFT1,FFT2,FFT3,DateExec,TimeExec$");

            fill.Instrument = GetInstrumentFromSeriesKey((string)fillData.GetValue(0));
            
            if (fill.Instrument == null)
            {
                if (OnNotifyErrorMessage != null) OnNotifyErrorMessage(string.Format("WARNING: Fill for unspecified instrument {0} {1}", (string)fillData.GetValue(9), (string)fillData.GetValue(10)));
            }
            else
            {
                TTInstrObj pInstr = _InstrumentToTT[fill.Instrument];

                /*fill.Instrument.NetPos = (int)pInstr.get_Get("NetPos");
                Console.WriteLine("{0} NetPos = {1}", fill.Instrument.FormattedName, fill.Instrument.NetPos); 
                fill.Instrument.NetPos = (int)_instrTTOrderSets[fill.Instrument].get_Get("NetPos");
                Console.WriteLine("{0} NetPos = {1}", fill.Instrument.FormattedName, fill.Instrument.NetPos);
                Console.WriteLine("-----------");*/

                fill.OrderKey = (string)fillData.GetValue(1);
                fill.Side = Util.ToEnum<OrderSide>((string)fillData.GetValue(2));
                fill.Quantity = (int)fillData.GetValue(3);
                fill.Price = double.Parse((string)fillData.GetValue(4));
                fill.FillType = Util.ToEnum<FillType>((string)fillData.GetValue(5));
                fill.FFT1 = (string)fillData.GetValue(6);
                fill.FFT2 = (string)fillData.GetValue(7);
                fill.FFT3 = (string)fillData.GetValue(8);
                fill.DateExec = (DateTime)fillData.GetValue(9);
                fill.TimeExec = (string)fillData.GetValue(10);
                
                // Print the fill information to the screen.
                /*txtFillData.Text += (string)fillData.GetValue(0) + ",  ";
                txtFillData.Text += (string)fillData.GetValue(1) + ",  ";
                txtFillData.Text += Convert.ToString(fillData.GetValue(2)) + ",  ";
                txtFillData.Text += (string)fillData.GetValue(3) + ",  ";
                txtFillData.Text += (string)fillData.GetValue(4) + ",  ";
                txtFillData.Text += (string)fillData.GetValue(5) + "\r\n";*/

                if (OnOrderFill != null) OnOrderFill(fill.Instrument, fill);
            }
            //ShowGetAttrDescriptions(pFillObj);
        }

        /// <summary>
        /// This function is called when a set of fills
        /// is about to be sent.  
        /// </summary>
        private void TTOrderSet_OnOrderFillBlockStart()
        {
            // Update the text box.
            //txtFillData.Text += "FillBlockStart\r\n";

        }

        /// <summary>
        /// This function is called when a set of fills
        /// has been sent. 
        /// </summary>
        private void TTOrderSet_OnOrderFillBlockEnd()
        {
            // Update the text box.
            //txtFillData.Text += "FillBlockEnd\r\n";

        }

        void drop_OnNotifyDrop1()
        {
            TTInstrObj tti = null;
            string nickname = "DROP1";

            RemoveInstrument(nickname);

            foreach (TTInstrObj instr in _drop1)
            {
                tti = instr;
            }
            
            if (tti != null)
            {
                _dropInstrument1 = new Instrument(nickname, tti);
            }

            _drop1.Reset();

            if (OnNotifyDrop != null) OnNotifyDrop(nickname, 1);
        }

        void drop_OnNotifyDrop2()
        {
            TTInstrObj tti = null;
            string nickname = "DROP2";

            RemoveInstrument(nickname);

            foreach (TTInstrObj instr in _drop2)
            {
                tti = instr;
            }

            if (tti != null)
            {
                _dropInstrument2 = new Instrument(nickname, tti);
            }

            _drop2.Reset();

            if (OnNotifyDrop != null) OnNotifyDrop(nickname, 2);
        }

        void gateway_OnStatusUpdate(int lHintMask, string sText)
        {
            //throw new NotImplementedException();
        }

        void gateway_OnSessionRollMessage(string sExchange, string sMessage, enumSessionRollState eMessage)
        {
            //throw new NotImplementedException();
        }

        void gateway_OnExchangeStateUpdate(string sExchange, string sText, int bOpened, int bServerUp)
        {
            //throw new NotImplementedException();
        }

        void gateway_OnExchangeMessage(string sExchange, string sTimeStamp, string sInfo, string sText)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region Private Helper Methods
        private void UpdatePLInformation(XTAPI.TTInstrObj pInstr)
        {
            try
            {
                // Get the P/L and Net Position per instrument.
                Array data = (Array)pInstr.get_Get("PL#,NetPos");
                double profit = (double)data.GetValue(0);
                int netPos = (int)data.GetValue(1);
                //double tickPrice = (double)pInstr.get_TickPrice(0, 1, "#");

                Instrument instrument = _ttToInstrument[pInstr.SeriesKey];
                instrument.PL = profit * instrument.PointValue;
                instrument.NetPos = netPos;

                //Console.WriteLine("PL$: {0}    NetPos: {1}", (string)data.GetValue(0), Convert.ToString(data.GetValue(1)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private TTOrderSet CreateTTOrderSet(string alias)
        {
            TTOrderSet orderSet = new TTOrderSet();
            orderSet.Alias = alias;
            orderSet.EnableOrderFillData = TRUE;
            // Enable the orderSet to send orders
            orderSet.EnableOrderSend = TRUE;
            orderSet.EnableOrderSetUpdates = TRUE;
            orderSet.EnableOrderUpdateData = TRUE;
            // Enable the orderSet to retrieve open P&L
            orderSet.EnableFillCaching = TRUE;

            // Subscribe to the fill events
            orderSet.OnOrderFillData += new _ITTOrderSetEvents_OnOrderFillDataEventHandler(TTOrderSet_OnOrderFillData);
            orderSet.OnOrderFillBlockEnd += new _ITTOrderSetEvents_OnOrderFillBlockEndEventHandler(TTOrderSet_OnOrderFillBlockEnd);
            orderSet.OnOrderFillBlockStart += new _ITTOrderSetEvents_OnOrderFillBlockStartEventHandler(TTOrderSet_OnOrderFillBlockStart);

            // Subscribe to order status events
            orderSet.OnOrderSubmitted += new _ITTOrderSetEvents_OnOrderSubmittedEventHandler(TTOrderSet_OnOrderSubmitted);
            orderSet.OnOrderUpdated += new _ITTOrderSetEvents_OnOrderUpdatedEventHandler(TTOrderSet_OnOrderUpdated);
            orderSet.OnOrderDeleted += new _ITTOrderSetEvents_OnOrderDeletedEventHandler(TTOrderSet_OnOrderDeleted);
            orderSet.OnOrderHeld += new _ITTOrderSetEvents_OnOrderHeldEventHandler(TTOrderSet_OnOrderHeld);
            orderSet.OnOrderRejected += new _ITTOrderSetEvents_OnOrderRejectedEventHandler(TTOrderSet_OnOrderRejected);
            orderSet.OnOrderActionRejected += new _ITTOrderSetEvents_OnOrderActionRejectedEventHandler(TTOrderSet_OnOrderActionRejected);
            orderSet.OnOrderSetUpdate += new _ITTOrderSetEvents_OnOrderSetUpdateEventHandler(TTOrderSet_OnOrderSetUpdate);

            orderSet.Set("NetLimits", FALSE);

            return orderSet;
        }
        
        private void FireInsideMarketUpdate(TTInstrObj pInstr)
        {
            try
            {
                // Retrieve the instrument information using the TTInstrObj Get Properties.
                // NOTE: For simplicity, the Exchange, Product, ProdType and Contract is redundant.
                //Array data = (Array)pInstr.get_Get("Bid#,BidQty#,Ask#,AskQty#,Last#,LastQty#,LTPDirection,PL.[USD],OpenPL$");
                Array data = (Array)pInstr.get_Get("Bid#,BidQty#,Ask#,AskQty#,Last#,LastQty#,LTPDirection,PL#,OpenPL#,Volume&");

                Quote quote = new Quote();
                quote.Instrument = GetInstrument(pInstr);

                quote.BidPrice = (double)data.GetValue(0);
                quote.BidQty = (int)((double)data.GetValue(1));
                quote.AskPrice = (double)data.GetValue(2);
                quote.AskQty = (int)((double)data.GetValue(3));
                quote.LastPrice = (double)data.GetValue(4);
                quote.LastQty = (int)((double)data.GetValue(5));
                quote.Direction = LTPDirection.UNCHANGED;
                if (data.GetValue(6) != null)
                    quote.Direction = (LTPDirection)((int)data.GetValue(6));
                //string plStr = (string)data.GetValue(7);
                if (data.GetValue(7) == null)
                    quote.PL = double.NaN;
                else
                    quote.PL = (double)data.GetValue(7);
                //string openPLStr = (string)data.GetValue(8);
                quote.OpenPL = (int)data.GetValue(8);
                quote.Volume = (int)data.GetValue(9);

                if (OnInsideMarketUpdate != null) OnInsideMarketUpdate(quote.Instrument, quote);
                //Application.DoEvents();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Order TTOrderToOrder(TTOrderObj tto)
        {
            Order order = new Order();
            TTInstrObj ttInstrument = tto.Instrument as TTInstrObj;
            order.Instrument = _ttToInstrument[ttInstrument.SeriesKey];

            order.OrderKey = (string)tto.get_Get("SiteOrderKey");
            order.Side = Util.ToEnum<OrderSide>((string)tto.get_Get("BuySell"));
            order.Quantity = (int)tto.get_Get("Qty");
            order.OrderType = Util.ToEnum<OrderType>((string)tto.get_Get("OrderType"));
            order.Price = (double)tto.get_Get("Price#");
            order.StopPrice = (double)tto.get_Get("Stop#");
            order.Account = (string)tto.get_Get("Account");

            return order;
        }

        private void PopulateCustomerNames()
        {
            // Obtain the available customer names
            XTAPI.TTOrderProfile TTOrderProfile = new XTAPI.TTOrderProfile();
            Array customers = (Array)TTOrderProfile.Customers;

            // Populate the customer customer names array with strings
            for (int i = 0; i < customers.Length; i++)
                _customerNames.Add(customers.GetValue(i).ToString());
        }

        private string GetNickname(TTInstrObj ttInstrument)
        {
            string result = null;
            foreach (string nickname in _ttInstruments.Keys)
            {
                if (_ttInstruments[nickname].Equals(ttInstrument))
                {
                    result = nickname;
                    break;
                }
            }
            return result;
        }

        private void NotifyError(string errMessage)
        {
            if (OnNotifyErrorMessage != null) OnNotifyErrorMessage(errMessage);
        }

        private void ShowGetAttrDescriptions(TTFillObj ttObj)
        {
            string attrDesc;

            string[] attributes = (string[]) ttObj.ReadProperties;

            foreach (string getAttr in attributes)
            {
                attrDesc = ttObj.get_ReadProperties(getAttr) as string;
                Console.WriteLine("{0} : {1}", getAttr, attrDesc);
            }

        }

        private Instrument GetInstrumentFromSeriesKey(string seriesKey)
        {
            Instrument result = null;

            foreach (Instrument instrument in _instruments.Values)
            {
                if (instrument.SeriesKey != null && instrument.SeriesKey.Equals(seriesKey))
                {
                    result = instrument;
                    break;
                }
            }
            return result;
        }

        OrderSet GetOrderSet(TTOrderSet orderSet)
        {
            OrderSet os = new OrderSet(orderSet.Alias);
            try
            {
                for (int i = 1; i <= orderSet.Count; ++i)
                {
                    TTOrderObj tto = (TTOrderObj)orderSet[i];

                    Order order = new Order(tto);
                    os.Add(order);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return os;
        }
        #endregion

        #region Internal helper methods
        internal Instrument GetInstrument(TTInstrObj tti)
        {
            if (_ttToInstrument.ContainsKey(tti.SeriesKey))
                return _ttToInstrument[tti.SeriesKey];
            else
                return null;
        }
        #endregion

        #region Public methods
        public void FindInstrument(Instrument instrument, string nickname, TTInstrObj tti)
        {
            /*// If the exchange for this instrument is not up, then generate an InstrumentNotFound event.
            ExchangeStatus exchangeStatus = GetExchangeStatus(tti.Exchange);
            if (exchangeStatus != ExchangeStatus.ALL_SERVERS_UP)
            {
                Console.WriteLine("Exchange error: Exchange status for {0} is {1}", tti.Exchange, exchangeStatus.ToString());
                if (OnInstrumentNotFound != null)
                    OnInstrumentNotFound(instrument);
                return;
            }*/

            // Store both the TTInstrObj and the Instrument in a dictionary with the nickname as the key
            if (_ttInstruments.ContainsKey(nickname))
            {
                throw new ArgumentException(string.Format("An instrument with the nickname '{0}' already exists.", nickname));
            }
            _ttInstruments[nickname] = tti;
            instrument.Nickname = nickname;
            _instruments[nickname] = instrument;
            // Create dictionaries to lookup eithr a TTInstrObj or an Instrument (when given the other)
            //_ttToInstrument[tti.SeriesKey] = instrument;
            _InstrumentToTT[instrument] = tti;

            // Create an order set for each instrument
            /*TTOrderSet orderSet = new TTOrderSet();
            orderSet.EnableOrderSend = 1;
            orderSet.EnableOrderFillData = 1;
            orderSet.EnableFillCaching = 1;
            //orderSet.OnOrderFillData += new _ITTOrderSetEvents_OnOrderFillDataEventHandler(m_orderSet_OnOrderFillData);
            orderSet.Set("NetLimits", false);
            
            orderSet.Open(TRUE);

            // Attach the instrument to a TTOrderSet object
            tti.OrderSet = orderSet;
            _instrOrderSets[instrument] = orderSet;*/
            
            _notify.AttachInstrument(tti);
            tti.Open(TRUE); // enable Market Depth:  1 - true, 0 - false

            /*if (!_instrOrderSets.ContainsKey(instrument))
            {
                _instrOrderSets[instrument] = CreateTTOrderSet(nickname);
                _instrOrderSets[instrument].Open(TRUE);
                //_orderSetInstruments[nickname] = instrument;
            }*/

            _instrTTOrderSets[instrument] = CreateTTOrderSet(nickname);
            _instrTTOrderSets[instrument].OrderSelector = tti.CreateOrderSelector;
            tti.OrderSet = _instrTTOrderSets[instrument];
            tti.OrderSet.Open(TRUE);

            // Instantiate the TTOrderSet object.
            //_ttOrderSet = new XTAPI.TTOrderSetClass();
            // Set the TTOrderSelector to the Instrument so that P/L per contract
            // will be displayed.  Otherwise the overall P/L per user will be displayed.
            //_ttOrderSet.OrderSelector = tti.CreateOrderSelector;
            // Attach the TTOrderSet to the TTInstrObj.
            //tti.OrderSet = _ttOrderSet;
            // Open the TTOrderSet.
            //tti.OrderSet.Open(0);
        }

        public void FindInstrument(Instrument instrument, string nickname, string exchange, string product, string contract, string prodType)
        {
            TTInstrObj tti = new TTInstrObj();
            tti.Exchange = exchange;
            tti.Product = product;
            tti.Contract = contract;
            tti.ProdType = prodType;

            FindInstrument(instrument, nickname, tti);
        }

        public void FireInsideMarketUpdate(Instrument instrument)
        {
            FireInsideMarketUpdate(_InstrumentToTT[instrument]);
        }

        public void RemoveInstrument(string nickname)
        {
            // Store both the TTInstrObj and the Instrument in a dictionary with the nickname as the key
            if (_ttInstruments.ContainsKey(nickname))
            {
                TTInstrObj tti = _ttInstruments[nickname];
                Instrument instrument = _instruments[nickname];
                _ttInstruments.Remove(nickname);
                _instruments.Remove(nickname);

                _InstrumentToTT.Remove(instrument);
                _instrTTOrderSets.Remove(instrument);
                tti.OrderSet = null;

                _notify.DetachInstrument(tti);
                tti = null;
            }
        }

        public ExchangeStatus GetExchangeStatus(string exchSymbol)
        {
            ExchangeStatus result = ExchangeStatus.NO_SERVERS_UP; ;
            string priceServer;
            string orderServer;
            string fillServer;
            string serverList;

            priceServer = "Available." + exchSymbol + ".Price";
            orderServer = "Available." + exchSymbol + ".Order";
            fillServer = "Available." + exchSymbol + ".Fill";
            serverList = priceServer + "," + orderServer + "," + fillServer;
            Array data = (Array)_ttGate.get_Get(serverList);

            int priceServerAvailable = (int)data.GetValue(0);
            int orderServerAvailable = (int)data.GetValue(1);
            int fillServerAvailable = (int)data.GetValue(2);

            if (priceServerAvailable == TRUE && orderServerAvailable == TRUE && fillServerAvailable == TRUE)
                result = ExchangeStatus.ALL_SERVERS_UP;
            else if (priceServerAvailable == FALSE && orderServerAvailable == FALSE && fillServerAvailable == FALSE)
                result = ExchangeStatus.NO_SERVERS_UP;
            else
                result = ExchangeStatus.SOME_SERVERS_UP;

            return result;
        }

        // The string returned is the site order key
        public string SendOrder(Instrument instrument, string customerName, OrderSide side, int qty, OrderType orderType, Price limitPrice, Price stopPrice, out int submittedQuantity)
        {
            // Initialize the submittedQuantity value to zero.
            submittedQuantity = 0;
            // We'll return the orderKey to be used for subsequent order modification, etc.
            string lastOrderSiteOrderKey = null;

            // Set the TTInstrObj to the TTOrderProfile.
            XTAPI.TTOrderProfile TTOrderProfile = new XTAPI.TTOrderProfile();
            TTOrderProfile.Instrument = _InstrumentToTT[instrument];

            string buySell = side.ToString();
            /*// Determine whether this is a buy or sell order based on the quantity (negative quantity = sell)
            string buySell;
            if (qty < 0)
            {
                buySell = "Sell";
                qty = Math.Abs(qty);
            }
            else if (qty > 0)
            {
                buySell = "Buy";
            }
            else
            {
                NotifyError("Cannot send an order with a zero quantity.");
                return null;
            }*/

            try
            {
                // Set the customer default property (e.g. "<Default>").
                if (customerName == null) customerName = "<Default>";
                TTOrderProfile.Customer = customerName;

                // Set for Buy or Sell.
                TTOrderProfile.Set("BuySell", buySell);

                // Set the quantity.
                TTOrderProfile.Set("Qty", qty.ToString());

                switch (orderType)
                {
                    case OrderType.MARKET:
                        TTOrderProfile.Set("OrderType", "M");
                        break;
                    case OrderType.LIMIT:
                        TTOrderProfile.Set("OrderType", "L");
                        TTOrderProfile.Set("Limit$", limitPrice.ToString());
                        break;
                    case OrderType.STOP_MARKET:
                        TTOrderProfile.Set("OrderType", "M");
                        // Set the order restriction to "S" for a stop order.
                        TTOrderProfile.Set("OrderRestr", "S");
                        // Set the stop price.
                        TTOrderProfile.Set("Stop$", stopPrice.ToString());
                        break;
                    case OrderType.STOP_LIMIT:
                        TTOrderProfile.Set("OrderType", "L");
                        // Set the order restriction to "S" for a stop order.
                        TTOrderProfile.Set("OrderRestr", "S");
                        // Set the limit price.
                        TTOrderProfile.Set("Limit$", limitPrice.ToString());
                        // Set the stop price.
                        TTOrderProfile.Set("Stop$", stopPrice.ToString());
                        break;
                }

                // Send the order by submitting the TTOrderProfile through the TTOrderSet.
                submittedQuantity = _ttOrderSet.get_SendOrder(TTOrderProfile);

                lastOrderSiteOrderKey = (string)TTOrderProfile.get_GetLast("SiteOrderKey");
            }
            catch (Exception e)
            {
                NotifyError(e.Message.ToString());
            }

            return lastOrderSiteOrderKey;
        }

        public string SendOrder(string instrumentNickname, string customerName, OrderSide side, int qty, OrderType orderType, Price limitPrice, Price stopPrice, out int submittedQuantity)
        {
            return SendOrder(GetInstrument(instrumentNickname), customerName, side, qty, orderType, limitPrice, stopPrice, out submittedQuantity);
        }

        public string SendBuyOrder(string instrumentNickname, string customerName, int qty, OrderType orderType, Price limitPrice, Price stopPrice, out int submittedQuantity)
        {
            return SendOrder(GetInstrument(instrumentNickname), customerName, OrderSide.BUY, qty, orderType, limitPrice, stopPrice, out submittedQuantity);
        }

        public string SendSellOrder(string instrumentNickname, string customerName, int qty, OrderType orderType, Price limitPrice, Price stopPrice, out int submittedQuantity)
        {
            return SendOrder(GetInstrument(instrumentNickname), customerName, OrderSide.SELL, qty, orderType, limitPrice, stopPrice, out submittedQuantity);
        }

        public void DeleteOrder(string orderKey, out int deletedQuantity)
        {
            // Record the quantity deleted.
            deletedQuantity = 0;

            // Test if the TTOrderSet contains orders.
            if (_ttOrderSet.Count <= 0)
            {
                NotifyError("There are no orders in the TTOrderSet to delete!");
                return;
            }

            // Obtain the TTOrderObj using the SiteOrderKey.
            TTOrderObj tempOrderObj = (TTOrderObj)_ttOrderSet.get_SiteKeyLookup(orderKey);

            // Invoke the Delete order property.
            deletedQuantity = tempOrderObj.DeleteOrder();
        }

        public void DeleteAllOrders(out int deletedQuantity)
        {
            // Record the quantity deleted.
            deletedQuantity = 0;

            // Test if the TTOrderSet contains orders.
            if (_ttOrderSet.Count <= 0)
            {
                NotifyError("There are no orders in the TTOrderSet to delete!");
                return;
            }
            
            // Delete all of the orders.
            deletedQuantity = _ttOrderSet.get_DeleteOrders(System.Type.Missing, null, null, 0, null);
        }

        // If you do NOT want to change one of the parameters, use:
        // int.MinValue for qty
        // null for limitPrice or stopPrice
        public void ModifyOrder(string orderKey, int qty, Price limitPrice, Price stopPrice, UpdateOrderType updateOrderType)
        {
            // Test if the TTOrderSet contains orders.
            if (_ttOrderSet.Count <= 0)
            {
                NotifyError("There are no orders in the TTOrderSet to modify!");
                return;
            }

            // Obtain the TTOrderObj of the last order using the saved SiteOrderKey.
            TTOrderObj tempOrderObj = (TTOrderObj)_ttOrderSet.get_SiteKeyLookup(orderKey);

            // Obtain the TTOrderProfile from the last order.
            TTOrderProfile tempOrderProfile = tempOrderObj.CreateOrderProfile;

            // Set the new price and quantity.
            if (limitPrice != null) tempOrderProfile.Set("Limit$", limitPrice.ToString());
            if (stopPrice != null) tempOrderProfile.Set("Stop$", stopPrice.ToString());
            if (qty != int.MinValue) tempOrderProfile.Set("Qty", qty.ToString());

            // Update Order as change or cancel/replace (0 - change, 1 - cancel/replace).
            _ttOrderSet.UpdateOrder(tempOrderProfile, (int)updateOrderType);
        }

        public Instrument GetInstrument(string nickname)
        {
            return _instruments[nickname];
        }

        public void RegisterDropWindow(int hWnd, int dropID)
        {
            switch (dropID)
            {
                case 1:
                    _drop1.RegisterDropWindow(hWnd);
                    break;
                case 2:
                    _drop2.RegisterDropWindow(hWnd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("dropID");
            }
        }

        public OrderSet CreateOrderSet(string nickname, OrderSelector selector)
        {
            TTOrderSet ttos = CreateTTOrderSet(nickname);
            ttos.Open(TRUE);

            TTOrderSet os = new TTOrderSet();
            TTOrderSelector ttSelect = CreateTTOrderSelector(selector);
            os.OrderSelector = ttSelect;
            os.Open(FALSE);

            OrderSet orderSet = GetOrderSet(os);

            return orderSet;
        }

        private TTOrderSelector CreateTTOrderSelector(OrderSelector selector)
        {
            TTOrderSelector ttSelect = new TTOrderSelector();
            
            // OrderSelector determines if all tests must be true (AND) or
            // if any of the tests can be true (OR).
            if (selector.TestLogic == LogicOperator.AND)
                ttSelect.AllMatchesRequired = TRUE;
            else
                ttSelect.AllMatchesRequired = FALSE;

            foreach (KeyValuePair<string, string> pair in selector)
            {
                ttSelect.AddTest(pair.Key, pair.Value);
            }

            return ttSelect;
        }

        public void ShutdownAPI()
        {
            try
            {
                _ttGate.XTAPITerminate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

    } //class

} //namespace
