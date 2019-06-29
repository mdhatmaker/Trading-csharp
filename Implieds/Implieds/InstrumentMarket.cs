using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using Hydra.Strategy.Interfaces;

namespace Hydra.Strategy.Models.TestProjects
{
    public interface IMarket
    {
        InstrumentMarket Market { get; }
    }

    public class InstrumentMarket
    {
        public uint InstrumentId { get; private set; }

        private IDepth _depth = null;

        private uint _ltpQty = 0;
        private int _ltpPrice;

        public uint BidQty { get { return _bidQty; } }
        public int BidPrice { get { return _bidPrice; } }
        public int AskPrice { get { return _askPrice; } }
        public uint AskQty { get { return _askQty; } }

        private uint _bidQty;
        private int _bidPrice;
        private int _askPrice;
        private uint _askQty;

        public InstrumentMarket(IObservable<IDepth> depthSource, uint instrumentId, uint bidQty = 0, int bidPrice = int.MinValue, int askPrice = int.MinValue, uint askQty = 0)
        {
            InstrumentId = instrumentId;

            _bidQty = bidQty;
            _bidPrice = bidPrice;
            _askPrice = askPrice;
            _askQty = askQty;

            IObserver<IDepth> obsvr = Observer.Create<IDepth>(
                depth => rxDepth(depth),
                ex => Console.WriteLine("OnError: {0}", ex.Message),
                () => Console.WriteLine("OnCompleted"));
            var filterSource = from source in depthSource
                               where source.InstrumentId == InstrumentId
                               select source;
            filterSource.Subscribe(obsvr);
        }

        private void rxDepth(IDepth revisedDepth)
        {
            Console.WriteLine("rxDepth: {0}", revisedDepth.InstrumentId);

            if (_depth == null)
                _depth = revisedDepth.Clone();
            else
                revisedDepth.CopyTo(_depth);

            if (_depth.Bids.Levels() > 0)
            {
                _bidPrice = _depth.Bids.PriceAt(0);
                _bidQty = _depth.Bids.QuantityAt(0);
            }
            else
            {
                _bidPrice = int.MinValue;
                _bidQty = 0;
            }

            if (_depth.Asks.Levels() > 0)
            {
                _askPrice = _depth.Asks.PriceAt(0);
                _askQty = _depth.Asks.QuantityAt(0);
            }
            else
            {
                _askPrice = int.MinValue;
                _askQty = 0;
            }
        }

        public void ProcessLTPUpdate(int price, uint qty)
        {
            _ltpPrice = price;
            _ltpQty = qty;
        }

        public double Midpoint
        {
            get
            {
                return (double)_bidQty / (_bidQty + _askQty) * _askPrice + (double)_askQty / (_bidQty + _askQty) * _bidPrice;
            }
        }

        public bool HasBid
        {
            get
            {
                return (_bidPrice != int.MinValue);
            }
        }

        public bool HasAsk
        {
            get
            {
                return (_askPrice != int.MinValue);
            }
        }

    } // END OF CLASS InstrumentMarket


} // END OF NAMESPACE
