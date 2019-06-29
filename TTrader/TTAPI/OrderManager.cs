using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI
{
    public enum OrderSide { BUY, SELL };

    public class OrderManager
    {
        private TTAPI.Trader tt;

        private XTAPI.TTOrderSet _orderSet = new XTAPI.TTOrderSetClass();
        private XTAPI.TTOrderSelector _selector = new XTAPI.TTOrderSelector();

        private Dictionary<Instrument, int> _position = new Dictionary<Instrument,int>();

        public delegate void FillHandler(object source, Fill fill);
        public event FillHandler FillNotify;

        public delegate void OrderSummaryUpdateHandler(object source, OrderSummary summary);
        public event OrderSummaryUpdateHandler OrderSummaryUpdateNotify;

        private const int TRUE = 1, FALSE = 0;

        private const string POSITION_VALUES = "NetCnt,NetWrk,NetPos,NetTicks,BuyCnt,BuyWrk,SellCnt,SellWrk";

        public OrderManager(TTAPI.Trader traderObject)
        {
            tt = traderObject;

            _orderSet.Set("NetLimits", FALSE);
            _selector.AllMatchesRequired = TRUE;
            _orderSet.OrderSelector = _selector;
            _orderSet.EnableOrderFillData = TRUE;
            _orderSet.EnableOrderSend = TRUE;
            _orderSet.Open(TRUE);

            _orderSet.OnOrderFillData += new XTAPI._ITTOrderSetEvents_OnOrderFillDataEventHandler(OnOrderFillData);
            _orderSet.OnOrderSetUpdate += new XTAPI._ITTOrderSetEvents_OnOrderSetUpdateEventHandler(OnOrderSetUpdate);
        }

        public void AddAccount(string account)
        {
            _selector.AddTest("Acct", account);
        }

        public void SendLimitOrder(Instrument instrument, OrderSide buySell, int quantity, int price)
        {
            XTAPI.TTOrderProfile orderProfile = new XTAPI.TTOrderProfileClass();

            orderProfile.Instrument = instrument.TTInstrument;

            string side = (buySell == OrderSide.BUY ? "Buy" : "Sell");

            orderProfile.Set("OrderType", "L");
            orderProfile.Set("BuySell$", side);
            orderProfile.Set("Qty", quantity.ToString());
            orderProfile.Set("Price", price.ToString());

            int quantitySent = _orderSet.get_SendOrder(orderProfile);
        }

        public void DeleteOrdersAtPrice(int price)
        {
            // Delete BUY orders.
            _orderSet.get_DeleteOrders(TRUE, price, price, TRUE, null);
            // Delete SELL orders.
            _orderSet.get_DeleteOrders(FALSE, price, price, TRUE, null);
        }

        public void OnOrderFillData(XTAPI.TTFillObj pFillObj)
        {
            object[] data = pFillObj.get_Get("Qty,Price&,IsBuy,Instr$,Acct,OrderNo") as object[];
            Fill fill = new Fill();
            fill.Quantity = (int)data[0];
            fill.Price = (int)data[1];
            int isBuy = (int)data[2];
            fill.Side = isBuy == TRUE ? OrderSide.BUY : OrderSide.SELL;
            string name = (string)data[3];
            fill.Instrument = tt.GetInstrumentFromName(name.ToUpper());
            fill.Account = (string)data[4];
            fill.OrderNumber = (string)data[5];

            if (fill.Instrument == null)
                return;

            System.Console.WriteLine("FILL: {0} {1}", fill.Quantity, fill.Instrument);

            int quantity = fill.Quantity;
            if (fill.Side == OrderSide.SELL)
                quantity = -quantity;

            if (_position.ContainsKey(fill.Instrument))
            {
                int pos = _position[fill.Instrument];
                _position[fill.Instrument] = pos + quantity;
            }
            else
            {
                _position[fill.Instrument] = quantity;
            }

            if (FillNotify != null) FillNotify(this, fill);
        }

        public void OnOrderSetUpdate(XTAPI.TTOrderSet pOrderSetObj)
        {
            object[] data = _orderSet.get_Get(POSITION_VALUES) as object[];

            OrderSummary summary = new OrderSummary();

            summary.WorkingOrderCount = (int)data[0];
            summary.WorkingQuantity = (int)data[1];
            summary.NetPosition = (int)data[2];
            summary.NetTicks = (int)data[3];
            summary.BuyOrderCount = (int)data[4];
            summary.BuyOrderQuantity = (int)data[5];
            summary.SellOrderCount = (int)data[6];
            summary.SellOrderQuantity = (int)data[7];

            System.Console.WriteLine("Working orders: {0}", summary.WorkingOrderCount);

            if (OrderSummaryUpdateNotify != null) OrderSummaryUpdateNotify(this, summary);
        }

        public int NetPosition(Instrument instrument)
        {
            return _position[instrument];
        }

    }   // OrderManager
}
