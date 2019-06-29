using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TTAPI
{
    public class Trader
    {
        XTAPI.TTInstrNotify _notify = new XTAPI.TTInstrNotifyClass();
        XTAPI.TTDropHandler _dragDrop = new XTAPI.TTDropHandlerClass();

        private const int TRUE = 1, FALSE = 0;

        private Dictionary<Instrument, int> _instrumentToID = new Dictionary<Instrument, int>();
        private Dictionary<int, Instrument> _idToInstrument = new Dictionary<int, Instrument>();
        private Dictionary<string, Instrument> _nameToInstrument = new Dictionary<string, Instrument>();

        private Dictionary<int, int> _previousVolume = new Dictionary<int, int>();

        private Dictionary<Instrument, Quote> _mostRecentQuote = new Dictionary<Instrument, Quote>();

        private Quote _quote = new Quote();

        //private int _previousLastPrice;
        //private int _previousLTQ;

        public delegate void QuoteUpdateHandler(object source, Quote quote);
        public event QuoteUpdateHandler QuoteUpdate;

        public delegate void TradeUpdateHandler(object source, Quote quote);
        public event TradeUpdateHandler TradeUpdate;

        public delegate void DepthUpdateHandler(object source, int instrumentID, Dictionary<double,int> bidDepth, Dictionary<double,int> askDepth);
        public event DepthUpdateHandler DepthUpdate;

        public delegate void InstrumentFoundHandler(object source, Instrument instrument);
        public event InstrumentFoundHandler InstrumentFound;

        public delegate void DragDropHandler(object source, Instrument instrument);
        public event DragDropHandler DragDrop;

        public Trader()
        {
            _notify.OnNotifyFound += new XTAPI._ITTInstrNotifyEvents_OnNotifyFoundEventHandler(OnNotifyFound);
            _notify.OnNotifyUpdate += new XTAPI._ITTInstrNotifyEvents_OnNotifyUpdateEventHandler(OnNotifyUpdate);
            _notify.OnNotifyDepthData += new XTAPI._ITTInstrNotifyEvents_OnNotifyDepthDataEventHandler(OnNotifyDepth);

            _dragDrop.OnNotifyDrop += new XTAPI._ITTDropHandlerEvents_OnNotifyDropEventHandler(OnNotifyDrop);

            _notify.EnablePriceUpdates = TRUE;
            _notify.EnableDepthUpdates = TRUE;
            _notify.UpdateFilter = "BidQty,Bid&,Ask&,AskQty,Last&,LastQty,Volume&";
        }

        public int GetInstrumentID(Instrument instrument)
        {
            return _instrumentToID[instrument];
        }

        public Instrument GetInstrument(int quoteID)
        {
            return _idToInstrument[quoteID];
        }

        public Instrument GetInstrumentFromName(string name)
        {
            if (_nameToInstrument.ContainsKey(name))
                return _nameToInstrument[name];
            else
                return null;
        }

        public int AddInstrument(Instrument instrument, bool marketDepth)
        {
            _instrumentToID[instrument] = instrument.ID;
            _idToInstrument[instrument.ID] = instrument;
            _nameToInstrument[instrument.Name] = instrument;

            _previousVolume[instrument.ID] = -1;

            _notify.AttachInstrument(instrument.TTInstrument);

            instrument.RequestPriceData(marketDepth);

            return instrument.ID;
        }

        public Quote MostRecentQuote(Instrument instrument)
        {
            return _mostRecentQuote[instrument];
        }

        public void RegisterDropWindow(Form form)
        {
            _dragDrop.RegisterDropWindow(form.Handle.ToInt32());
        }

        public void OnNotifyDrop()
        {
            foreach (XTAPI.TTInstrObj dropInstrument in _dragDrop)
            {
                Instrument instrument = new Instrument(dropInstrument);
                if (DragDrop != null) DragDrop(this, instrument);
            }

            _dragDrop.Reset();
        }

        public void OnNotifyFound(XTAPI.TTInstrNotify pNotify, XTAPI.TTInstrObj pInstr)
        {
            // Indicate that the instrument is open.
            int id = pInstr.GetHashCode();

            getLiveData(pInstr);

            Instrument instrument = _idToInstrument[id];

            instrument.Open();

            if (InstrumentFound != null) InstrumentFound(this, instrument);
        }

        public void OnNotifyUpdate(XTAPI.TTInstrNotify pNotify, XTAPI.TTInstrObj pInstr)
        {
            //int instrIndex = pInstr.GetHashCode();
            getLiveData(pInstr);
        }

        public void OnNotifyDepth(XTAPI.TTInstrNotify pNotify, XTAPI.TTInstrObj pInstr)
        {
            const int DEPTH_SIZE = 5;

            Dictionary<double,int> bidDepth = new Dictionary<double,int>();
            Dictionary<double,int> askDepth = new Dictionary<double,int>();
            object[,] depth;

            depth = (object[,]) pInstr.get_Get(string.Format("BidDepth({0})", DEPTH_SIZE));
            for (int i=0; i<DEPTH_SIZE; i++)
            {
                if (depth[i, 0] != null)
                {
                    double price = double.Parse(depth[i, 0].ToString());
                    bidDepth.Add(price, (int)depth[i, 1]);
                }
            }

            depth = (object[,]) pInstr.get_Get(string.Format("AskDepth({0})", DEPTH_SIZE));
            for (int i=0; i<DEPTH_SIZE; i++)
            {
                if (depth[i, 0] != null)
                {
                    double price = double.Parse(depth[i, 0].ToString());
                    askDepth.Add(price, (int)depth[i, 1]);
                }
            }

            int instrumentID = pInstr.GetHashCode();

            if (DepthUpdate != null) DepthUpdate(this, instrumentID, bidDepth, askDepth);
        }

        private void getLiveData(XTAPI.TTInstrObj pInstr)
        {
            object[] data = pInstr.get_Get("BidQty,Bid&,Ask&,AskQty,Last&,LastQty,Volume&,Change&") as object[];
            _quote.InstrumentID = pInstr.GetHashCode();
            _quote.BidQty = (data[0] == null ? 0 : (int) data[0]);
            _quote.BidPrice = (data[1] == null ? 0 : (int) data[1]);
            _quote.AskPrice = (data[2] == null ? 0 : (int) data[2]);
            _quote.AskQty = (data[3] == null ? 0 : (int) data[3]);
            _quote.LastPrice = (data[4] == null ? 0 : (int) data[4]);
            _quote.LastQty = (data[5] == null ? 0 : (int) data[5]);
            _quote.Volume = (data[6] == null ? 0 : (int) data[6]);
            _quote.NetChange = (data[7] == null ? 0 : (int) data[7]);

            /*// Deal with the accumulated LTQ problem of the CME.
            if (pInstr.Exchange.Equals("CME"))
            {
                if (_previousLastPrice == _quote.LastPrice)
                {
                    int accLTQ = (int)data[5];
                    _quote.LastQty = accLTQ - _previousLTQ;
                    _previousLTQ = accLTQ;
                }
                else
                {
                    _quote.LastQty = (int)data[5];

                    _previousLastPrice = _quote.LastPrice;
                    _previousLTQ = _quote.LastQty;
                }
            }
            else
            {
                _quote.LastQty = (int)data[5];
            }
            */

            if (QuoteUpdate != null) QuoteUpdate(this, _quote);

            // If the volume has changed, then a trade has occurred.
            if (_quote.Volume != _previousVolume[_quote.InstrumentID])
            {
                _quote.LastQty = _quote.Volume - _previousVolume[_quote.InstrumentID];
                if (TradeUpdate != null && _previousVolume[_quote.InstrumentID] != -1) TradeUpdate(this, _quote);
                _previousVolume[_quote.InstrumentID] = _quote.Volume;
            }

            _mostRecentQuote[_idToInstrument[_quote.InstrumentID]] = _quote;
        }

    }   // Trader
}
