using System;
using System.Collections.Generic;
using System.Text;

namespace HTAPI
{
    public class Quote
    {
        public Instrument Instrument;
        public int BidQty;
        public Price BidPrice;
        public Price AskPrice;
        public int AskQty;
        public Price LastPrice;
        public int LastQty;
        public LTPDirection Direction = LTPDirection.UNCHANGED;
        public double PL = 0.0;
        public double OpenPL = 0.0;
        public int Volume { get; set; }
        
        public Quote()
        {

        }

        public Quote(Instrument instrument, Price bidPrice, int bidQty, Price askPrice, int askQty, Price lastPrice, int lastQty)
        {
            Instrument = instrument;
            BidQty = bidQty;
            BidPrice = bidPrice;
            AskPrice = askPrice;
            AskQty = askQty;
            LastPrice = lastPrice;
            LastQty = lastQty;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} : {3} {4}", Instrument.FormattedName, BidQty, BidPrice, AskPrice, AskQty);
        }
    } //class

} //namespace