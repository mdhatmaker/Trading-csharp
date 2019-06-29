using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI
{
    public struct Quote
    {
        public int InstrumentID;
        public int BidQty;
        public int BidPrice;
        public int AskPrice;
        public int AskQty;
        public int LastPrice;
        public int LastQty;
        public int Volume;
        public int NetChange;

        public Quote(int instrumentID, int bidQty, int bidPrice, int askPrice, int askQty, int lastPrice, int lastQty, int volume, int netChange)
        {
            InstrumentID = instrumentID;
            BidQty = bidQty;
            BidPrice = bidPrice;
            AskPrice = askPrice;
            AskQty = askQty;
            LastPrice = lastPrice;
            LastQty = lastQty;
            Volume = volume;
            NetChange = netChange;
        }
    }   // Quote
}
