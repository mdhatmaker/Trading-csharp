using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{
    public class Price
    {
        private double PriceDouble = double.NaN;

        public Price(double price)
        {
            PriceDouble = price;
        }

        static public implicit operator Price(double d)
        {
            Price price = new Price(d);
            return price;
        }

        static public implicit operator Price(string s)
        {
            Price price = new Price(double.Parse(s));
            return price;
        }

        static public implicit operator double(Price price)
        {
            return price.PriceDouble;
        }

        public double ToDouble()
        {
            return PriceDouble;
        }

        public override string ToString()
        {
            if (PriceDouble != double.NaN) return PriceDouble.ToString();
            return "";
        }
    } //class
} //namespace
