using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XTAPI;

namespace HTAPI
{
    public class Order
    {
        public Instrument Instrument;
        public string Contract;
        public string OrderKey;
        public OrderSide Side;
        public int Quantity;
        public OrderType OrderType;
        public Price Price;
        public Price StopPrice;
        public string Account;
        public string FFT1;
        public string FFT2;
        public string FFT3;
        public string Status;
        public string SeriesKey;

        public Order()
        {
        }

        public Order(TTOrderObj tto)
        {
            Array data = (Array)tto.get_Get("Time,Date,Contract,BuySell,Qty,Price,OrdStatus,SeriesKey,SiteOrderKey,Stop,OrderType,Acct,FFT1,FFT2,FFT3");
            DateTime timeExec = (DateTime)data.GetValue(0);
            DateTime dateExec = (DateTime)data.GetValue(1);
            string dateString = dateExec.Month + "/" + dateExec.Day + "/" + dateExec.Year;
            Contract = (string)data.GetValue(2);
            Side = ParseEnum.ParseOrderSide((string)data.GetValue(3));
            Quantity = (int)data.GetValue(4);
            Price = Double.Parse((string)data.GetValue(5));
            Status = (string)data.GetValue(6);
            SeriesKey = (string)data.GetValue(7);
            OrderKey = (string)data.GetValue(8);
            StopPrice = ParseEnum.ParsePrice(data.GetValue(9));
            OrderType = ParseEnum.ParseOrderType((string)data.GetValue(10), StopPrice);
            Account = (string)data.GetValue(11);
            FFT1 = (string)data.GetValue(12);
            FFT2 = (string)data.GetValue(13);
            FFT3 = (string)data.GetValue(14);

            Instrument = API.Instance.GetInstrument(tto.Instrument as TTInstrObj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>deleted quantity (int)</returns>
        public int Delete()
        {
            int deletedQuantity;
            API.Instance.DeleteOrder(OrderKey, out deletedQuantity);
            return deletedQuantity;
        }

        public bool IsStopOrder()
        {
            bool result = false;
            if (OrderType == OrderType.STOP_LIMIT || OrderType == OrderType.STOP_MARKET)
                result = true;
            return result;
        }

    } //class
} //namespace
