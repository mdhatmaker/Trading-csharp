using System;
using System.Collections.Generic;
using System.Text;

namespace TTAPI
{
    public struct Fill
    {
        public int Quantity;
        public int Price;
        public OrderSide Side;
        public Instrument Instrument;
        public string Account;
        public string OrderNumber;

    }   // Fill
}
