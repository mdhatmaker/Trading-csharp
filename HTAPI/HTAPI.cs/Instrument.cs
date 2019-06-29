using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XTAPI;

namespace HTAPI
{
    public class Instrument
    {

        public string Nickname { get; set; }
        public string Alias { get; set; }
        public string Contract { get; set; }
        public string Exchange { get; set; }
        public GatewayType GatewayType { get; set; }
        public string ProdType { get; set; }
        public string Product { get; set; }
        public string SeriesKey { get; set; }
        public int NetPos { get; set; }
        public double PL { get; set; }
        public double TickPrice { get; set; }
        public double PointValue { get; set; }

        public string FormattedName
        {
            get
            {
                string result = "";
                if (Contract != null && Product != null)
                {
                    if (Contract.StartsWith(Product))
                        result = Contract;
                    else
                        result = Product + " " + Contract;
                }
                else if (Nickname != null)
                {
                    result = Nickname;
                }
                return result;
            }
        }

        public Instrument()
        {
        }

        public Instrument(string nickname, string exchange, string product, string contract, string prodType)
        {
            API.Instance.FindInstrument(this, nickname, exchange, product, contract, prodType);
        }

        public Instrument(string nickname, TTInstrObj tti)
        {
            API.Instance.FindInstrument(this, nickname, tti);
        }

        /// <summary>
        /// Take the P&L and adjust it based on the quote that is passed.
        /// The quote will provide bid, ask and last which will be used to
        /// modify our P&L (since API provides P&L based on last price).
        /// </summary>
        /// <param name="quote">Most recent quote for this instrument</param>
        /// <returns>P&L adjusted by looking at bid, ask and last prices of the quote provided</returns>
        public double AdjustedPL(Quote quote)
        {
            //double aPL = ((int)((quote.BidPrice + quote.AskPrice) / 2 / TickPrice) * TickPrice - quote.LastPrice) * PointValue * NetPos;
            double aPL;
            // Find the bid/ask average
            aPL = (quote.BidPrice + quote.AskPrice) / 2;
            // Round this to an actual price (use TickPrice) - this will
            // cut down on the "jumpiness" of the P&L
            aPL = ((int)(aPL / TickPrice)) * TickPrice;
            // Find the difference between the bid/ask average and the last price
            aPL = aPL - quote.LastPrice;
            // Adjust by this difference for EACH contract (net position)
            aPL = aPL * PointValue * NetPos;

            return Math.Round(PL+aPL, 2);
        }

        public override string ToString()
        {
            return FormattedName;
        }
    } //class
} //namespace
