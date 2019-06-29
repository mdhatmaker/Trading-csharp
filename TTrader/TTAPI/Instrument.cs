using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TTAPI
{
    public class Instrument
    {
        private const int TRUE = 1, FALSE = 0;

        private XTAPI.TTInstrObj _instrument;
        
        private double _tickIncrement;
        private int _id;
        private string _exchange;
        private string _productType;
        private string _product;
        private string _contract;
        private string _name;
        private XTAPI.TTOrderSelector _orderSelector;

        private bool _isOpen = false;

        public Instrument(XTAPI.TTInstrObj ttInstr)
        {
            _exchange = ttInstr.Exchange;
            _productType = ttInstr.ProdType;
            _product = ttInstr.Product;
            _contract = ttInstr.Contract;
            _name = _product + " " + _contract;

            _instrument = ttInstr;

            _instrument.Exchange = _exchange;
            _instrument.ProdType = _productType;
            _instrument.Product = _product;
            _instrument.Contract = _contract;

            _id = _instrument.GetHashCode();
        }

        public Instrument(string exchange, string productType, string product, string contract)
        {
            _exchange = exchange;
            _productType = productType;
            _product = product;
            _contract = contract;
            _name = _product + " " + _contract;

            _instrument = new XTAPI.TTInstrObjClass();

            _instrument.Exchange = exchange;
            _instrument.ProdType = productType;
            _instrument.Product = product;
            _instrument.Contract = contract;
            //_instrument.Name = _name;

            _id = _instrument.GetHashCode();
        }


        public int ID
        {
            get { return _id; }
        }

        public XTAPI.TTInstrObj TTInstrument
        {
            get { return _instrument; }
        }

        public double TickIncrement
        {
            get { return _tickIncrement; }
        }

        public string Exchange
        {
            get { return _exchange; }
        }

        public string ProductType
        {
            get { return _productType; }
        }

        public string Product
        {
            get { return _product; }
        }

        public string Contract
        {
            get { return _contract; }
        }

        public string Name
        {
            get { return _name; }
        }

        public void RequestPriceData(bool includeMarketDepth)
        {
            _instrument.Open(includeMarketDepth ? 1 : 0);
        }

        public void Open()
        {
            _isOpen = true;

            // The Contract property should now be valid, so retrieve it. 
            _contract = _instrument.Contract;

            // Retrieve the tick increment from the underlying TTInstrObj object.
            object obj = _instrument.get_TickPrice(1, 0, "#");
            _tickIncrement = (double)obj;

            System.Console.WriteLine("Open Instrument: {0}", _instrument.SeriesKey);

            _orderSelector = _instrument.CreateOrderSelector;
        }        

        public bool IsOpen
        {
            get { return _isOpen; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Instrument instrument = obj as Instrument;
            return instrument._id == _id;
        }

        public override string ToString()
        {
            return _name;
        }
        /*public void WaitForOpen()
        {
            const int SLEEP_DELAY = 200;
            const int TIMEOUT = 5 * (1000 / SLEEP_DELAY);

            int timeoutCount = 0;
            while (_isOpen == false && timeoutCount < TIMEOUT)     // timeout is 5 seconds
            {
                Thread.Sleep(SLEEP_DELAY);
                timeoutCount++;
            }

            if (timeoutCount >= TIMEOUT)
                throw new Exception(string.Format("Timeout occurred while attempting to open instrument: {0} {1} {2} {3}", _exchange, _productType, _product, _contract));
        }*/

            
    }   // Instrument
}
