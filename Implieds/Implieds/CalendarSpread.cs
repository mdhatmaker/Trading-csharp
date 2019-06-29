using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using Hydra.Strategy.Interfaces;

namespace Hydra.Strategy.Models.TestProjects
{
    public enum PricingType { None = 0, Model = 1, MarketMaker = 2 };

    public class CalendarSpread : IMarket
    {
        public static uint ExtraBidEdgeOverride = 0;
        public static uint ExtraAskEdgeOverride = 0;
        public static HydraManager HydraMgr
        {
            get { return _hydraMgr; }
            set
            {
                if (_hydraMgr == null)
                    _hydraMgr = value;
                else
                    throw new MemberAccessException("CalendarSpread.HydraMgr static property can only be set once");
            }
        }
        private static HydraManager _hydraMgr = null;
        public static IObservable<IFill> FillSource
        {
            get { return _fillSource; }
            set
            {
                if (_fillSource == null)
                    _fillSource = value;
                else
                    throw new MemberAccessException("CalendarSpread.Source static property can only be set once");
            }
        }
        private static IObservable<IFill> _fillSource = null;
        public static IObservable<IDepth> DepthSource
        {
            get { return _depthSource; }
            set
            {
                if (_depthSource == null)
                    _depthSource = value;
                else
                    throw new MemberAccessException("Contract.DepthSource static property can only be set once");
            }
        }
        private static IObservable<IDepth> _depthSource = null;

        public uint InstrumentId { get; private set; }

        public InstrumentMarket Market { get { return _market; } }
        protected InstrumentMarket _market;

        private OrderStack _bids;
        private OrderStack _asks;

        public uint FrontMonthContractIndex { get; private set; }
        public uint BackMonthContractIndex { get; private set; }

        public uint BidsWorkingQty { get { return _bids.QuantityPerLevel; } }
        public uint AsksWorkingQty { get { return _asks.QuantityPerLevel; } }
        public uint BidsStackLevelCount { get { return _bids.StackLevelCount; } }
        public uint AsksStackLevelCount { get { return _asks.StackLevelCount; } }

        public PricingType PricingType;

        public uint MarketMakerQty { get; private set; }
        public double MarketMakerRatio { get; private set; }
        public uint ExtraBidEdge { get; private set; }
        public uint ExtraAskEdge { get; private set; }

        public int MakeBestRepeatCount;    // how many times can I better the market before just joining best bid/offer
        public int RequoteSameLevelCount;  // how many times do I put orders at that same price level (I'm getting filled, but my model is still showing same bid/ask)


        public CalendarSpread(uint frontMonthContractIndex, uint backMonthContractIndex, uint etsInstrumentId, PricingType pricingType = PricingType.None, uint initBidQty = 0, int initBidPrice = int.MinValue, int initAskPrice = int.MinValue, uint initAskQty = 0)
        {
            InstrumentId = etsInstrumentId;
            _market = new InstrumentMarket(DepthSource, etsInstrumentId, initBidQty, initBidPrice, initAskPrice, initAskQty);

            FrontMonthContractIndex = frontMonthContractIndex;
            BackMonthContractIndex = backMonthContractIndex;

            PricingType = pricingType;

            MarketMakerQty = 0;
            MarketMakerRatio = 0.0;
            ExtraBidEdge = 0;
            ExtraAskEdge = 0;
        }

        // Not every CalendarSpread object will need the ability to submit quotes (bids/offers)
        // The order stacks to handle quoting are only created when/if this method is called
        public void InitializeOrderStacks(uint stackLevels, uint priceLevelWorkingQty, int bidPrice = int.MinValue, int askPrice = int.MinValue)
        {
            if (HydraMgr != null)
            {
                _bids = new OrderStack(_fillSource, InstrumentId, OrderSide.Buy, bidPrice, stackLevels, priceLevelWorkingQty, HydraMgr);
                _asks = new OrderStack(_fillSource, InstrumentId, OrderSide.Sell, askPrice, stackLevels, priceLevelWorkingQty, HydraMgr);
            }
        }

        public void UpdateQuotes(int bidPrice, int askPrice)        // Will either start the quoting or update the inside price
        {
            if (HydraMgr != null)
            {
                _bids.UpdateInsidePrice(bidPrice);
                _asks.UpdateInsidePrice(askPrice);
            }
        }

        public void StopQuotes()
        {
            if (HydraMgr != null)
            {
                _bids.StopQuoting();
                _asks.StopQuoting();
            }
        }

        public void SetExtraEdge(uint extraEdge)
        {
            ExtraBidEdge = extraEdge;
            ExtraAskEdge = extraEdge;
        }

        public void SetExtraEdge(uint extraBidEdge, uint extraAskEdge)
        {
            ExtraBidEdge = extraBidEdge;
            ExtraAskEdge = extraAskEdge;
        }

        public void SetMarketMakerParams(uint mmQty, double mmRatio)
        {
            MarketMakerQty = mmQty;
            MarketMakerRatio = mmRatio;
        }

        protected int roundBidToMarket(double rawBid)
        {
            if (_market.HasBid && rawBid > _market.BidPrice)
            {
                if (_market.AskPrice - _market.BidPrice > 1)
                    return _market.BidPrice + 1;
                else
                    return _market.BidPrice;
            }
            else
            {
                return (int)(_market.BidPrice - roundUp(_market.BidPrice - rawBid));
            }
        }

        protected int roundAskToMarket(double rawAsk)
        {
            if (_market.HasAsk && rawAsk < _market.AskPrice)
            {
                if (_market.AskPrice - _market.BidPrice > 1)
                    return _market.AskPrice - 1;
                else
                    return _market.AskPrice;
            }
            else
            {
                return (int)(_market.AskPrice - roundUp(_market.AskPrice - rawAsk));
            }
        }

        // Duplicates the (weird) Excel function ROUNDUP which always rounds AWAY from zero
        private double roundUp(double value)
        {
            return Math.Sign(value) * Math.Ceiling(Math.Abs(value));
        }


    } // END OF CLASS CalendarSpread
} // END OF NAMESPACE
