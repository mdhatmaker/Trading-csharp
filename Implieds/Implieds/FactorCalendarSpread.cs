using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Strategy.Models.TestProjects
{
    public class FactorCalendarSpread : CalendarSpread
    {
        public FactorCalendarSpread(uint frontMonthContractIndex, uint backMonthContractIndex, uint etsInstrumentId, PricingType pricingType = PricingType.None, uint initBidQty = 0, int initBidPrice = int.MinValue, int initAskPrice = int.MinValue, uint initAskQty = 0)
            : base(frontMonthContractIndex, backMonthContractIndex, etsInstrumentId, pricingType, initBidQty, initBidPrice, initAskPrice, initAskQty)
        {
            
        }

        /*
        public int CalculatedBidPrice
        {
            get
            {
                if (PricingType == PricingType.Model)               // Model pricing
                    return roundBidToMarket(rawCalculatedBid) + getBidEdgeAdjustment();
                else if (PricingType == PricingType.MarketMaker)    // MM pricing
                    return marketMakerBidPrice + getBidEdgeAdjustment();
                else
                    return int.MinValue;    // if PricingType is NONE
            }
        }

        public int CalculatedAskPrice
        {
            get
            {
                if (PricingType == PricingType.Model)               // Model pricing
                    return roundAskToMarket(rawCalculatedAsk) + getAskEdgeAdjustment();
                else if (PricingType == PricingType.MarketMaker)    // MM pricing
                    return marketMakerAskPrice + getAskEdgeAdjustment();
                else
                    return int.MinValue;    // if PricingType is NONE
            }
        }

        private double rawCalculatedBid
        {
            get
            {
                double bidFront = Factor.GetCalculatedContractBid(FrontMonthContractIndex);
                double bidBack = Factor.GetCalculatedContractBid(BackMonthContractIndex);
                Console.WriteLine("braw: {0}  braw: {1}", bidFront, bidBack);
                return bidFront - bidBack;
            }
        }

        private double rawCalculatedAsk
        {
            get
            {
                double askFront = Factor.GetCalculatedContractAsk(FrontMonthContractIndex);
                double askBack = Factor.GetCalculatedContractAsk(BackMonthContractIndex);
                Console.WriteLine("araw: {0}  araw: {1}", askFront, askBack);
                return askFront - askBack;
            }
        }

        private int getBidEdgeAdjustment()
        {
            return (int)-(ExtraBidEdge + ExtraBidEdgeOverride + getBidBackoff());    // this will return a negative value so you can ADD it to the BID
        }

        private int getAskEdgeAdjustment()
        {
            return (int)(ExtraAskEdge + ExtraAskEdgeOverride + getAskBackoff());     // this will return a positive value so you can ADD it to the ASK
        }

        // Go through each Factor, check to see if there is a backoff amount for the bid
        private double getBidBackoff()
        {
            double result = 0.0;
            for (int i = 0; i < Factor.Count; ++i)
            {
                double backoff = Factor.Factors[i].GetBidBackoff(FrontMonthContractIndex, BackMonthContractIndex);
                if (backoff != 0)
                {
                    result = backoff;
                    break;
                }
            }
            return result;
        }

        // Go through each Factor, check to see if there is a backoff amount for the ask
        private double getAskBackoff()
        {
            double result = 0.0;
            for (int i = 0; i < Factor.Count; ++i)
            {
                double backoff = Factor.Factors[i].GetAskBackoff(FrontMonthContractIndex, BackMonthContractIndex);
                if (backoff != 0)
                {
                    result = backoff;
                    break;
                }
            }
            return result;
        }

        private int marketMakerBidPrice
        {
            get
            {
                double bidRatio = (double)_market.BidQty / (_market.BidQty + _market.AskQty);
                if (_market.BidQty >= MarketMakerQty && bidRatio >= MarketMakerRatio)
                    return _market.BidPrice;
                else
                    return _market.BidPrice - 1;
            }
        }

        private int marketMakerAskPrice
        {
            get
            {
                double askRatio = (double)_market.AskQty / (_market.BidQty + _market.AskQty);
                if (_market.AskQty >= MarketMakerQty && askRatio >= MarketMakerRatio)
                    return _market.AskPrice;
                else
                    return _market.AskPrice + 1;
            }
        }
        */

    } // END OF CLASS FactorCalendarSpread
} // END OF NAMESPACE
