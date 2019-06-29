using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using XTAPI;
using Db4objects.Db4o;

namespace HTAPI
{
    public class HistoricalAPI
    {
        public int TrackingInstrumentCount { get { return _trackingInstruments.Count; } }
        public int RecordCount { get { return _recordCount; } }

        HTAPI.API _api;
        IObjectContainer _db;
        List<Instrument> _trackingInstruments = new List<Instrument>();
        int _recordCount = 0;

        public HistoricalAPI(string historicalDBFilename, HTAPI.API api)
        {
            _api = api;

            _db = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), historicalDBFilename);

            _api.OnInsideMarketUpdate += new API.InsideMarketUpdate(api_OnInsideMarketUpdate);
            _api.OnNotifyErrorMessage += new API.NotifyErrorMessage(api_OnNotifyErrorMessage);
        }

        void api_OnNotifyErrorMessage(string message)
        {
            Console.WriteLine("ERROR: " + message);
        }

        void api_OnInsideMarketUpdate(Instrument instrument, Quote quote)
        {
            if (!_trackingInstruments.Contains(instrument))
                return;

            HistoricalQuote historicalQuote = new HistoricalQuote(DateTime.Now, quote);
            _db.Store(historicalQuote);

            _recordCount++;

            //Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5} {6}", instrument.Nickname, quote.BidPrice, quote.BidQty, quote.AskPrice, quote.AskQty, quote.LastPrice, quote.LastQty));
        }

        public void TrackInstrument(Instrument instrument)
        {
            _trackingInstruments.Add(instrument);
        }

        public Dictionary<DateTime,Quote> RetrieveAll()
        {
            Dictionary<DateTime, Quote> result = new Dictionary<DateTime, Quote>();

            // Retrieve all HistoricalQuotes QBE
            HistoricalQuote proto = new HistoricalQuote(DateTime.MinValue, null);
            IObjectSet queryResults = _db.QueryByExample(proto);
            //IObjectSet result = db.QueryByExample(typeof(Pilot))

            HistoricalQuote hq;
            while (queryResults.HasNext())
            {
                hq = (HistoricalQuote)queryResults.Next();
                result[hq.DateTime] = hq.Quote;
            }

            return result;
        }
    } //class

    public class HistoricalQuote
    {
        public DateTime DateTime;
        public Quote Quote;

        public HistoricalQuote(DateTime dateTime, Quote quote)
        {
            DateTime = dateTime;
            Quote = quote;
        }
    } //class

} //namespace
