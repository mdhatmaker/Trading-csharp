using System;
using System.Collections.Generic;
using System.Text;

namespace HTAPI
{
    public class Fill
    {
        public Instrument Instrument;
        public string OrderKey;
        public OrderSide Side; 
        public int Quantity;
        public Price Price;
        public string Account;
        public FillType FillType;
        public string FFT1;
        public string FFT2;
        public string FFT3;
        public DateTime DateExec { get; set; }
        public string TimeExec { get; set; }

        public Fill()
        {
        }

        public override string  ToString()
        {
            // sample date and time formatting: "The current date and time: {0:MM/dd/yy H:mm:ss zzz}"
            return string.Format("{0:MM/dd/yy} {1} {2} {3} {4} {5}", DateExec, TimeExec, Instrument.FormattedName, Side, Quantity, Price);
        }
    } //class
} //namespace
