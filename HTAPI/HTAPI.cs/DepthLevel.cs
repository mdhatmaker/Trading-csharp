using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{
    public class DepthLevel
    {
        public Price Price;
        public int Qty;

        public DepthLevel()
        {
        }

        public DepthLevel(Price price, int qty)
        {
            Price = price;
            Qty = qty;
        }

    } //class
} //namespace
