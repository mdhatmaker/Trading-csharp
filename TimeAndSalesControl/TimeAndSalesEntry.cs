using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TT.TradeCo.Controls
{
    public struct TimeAndSalesEntry
    {
        public string Price;
        public int Quantity;
        public Color Color;

        public TimeAndSalesEntry(string price, int quantity, Color color)
        {
            Price = price;
            Quantity = quantity;
            Color = color;
        }
    }   // TimeAndSalesEntry
}
