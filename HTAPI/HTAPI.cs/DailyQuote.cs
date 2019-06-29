using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{
    public class DailyQuote
    {
        public Instrument Instrument;
        public Price LastPrice;
        public int LastQty;
        public int Volume;
        public Price NetChange;

        public DailyQuote()
        {

        }

        public DailyQuote(Instrument instrument, Price lastPrice, int lastQty, int volume, Price netChange)
        {
            Instrument = instrument;
            LastPrice = lastPrice;
            LastQty = lastQty;
            Volume = volume;
            NetChange = netChange;
        }

    } //class
} //namespace
