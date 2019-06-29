using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;

namespace FillsDeluxe
{

    public class FillBasic
    {
        public string Time { get; set; }
        public string ShortTime { get; set; }
        public string Identifiers { get; set; }
        public BuySell BuySell { get; set; }
        public Quantity Quantity { get; set; }
        public string InstrumentName { get; set; }
        public Price Price { get; set; }

        public FillBasic(Fill fill)
        {
            //fill.FillType // partial or full
            //fill.SpreadId // ID of spread associated with this fill

            TimeZoneInfo cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(fill.TransactionDateTime, cst);

            DateTime time = localTime;
            string timeStr = time.ToString("H:mm:ss.fff");
            string shortTimeStr = time.ToString("hh:mm:ss tt");

            Time = timeStr;
            ShortTime = shortTimeStr;
            Identifiers = GetFillIdenifiers(fill);
            BuySell = fill.BuySell;
            Quantity = fill.Quantity;
            InstrumentName = ParseInstrumentKey(fill.InstrumentKey.ToString());
            Price = fill.MatchPrice;
        }

        string GetFillIdenifiers(Fill fill)
        {
            StringBuilder sb = new StringBuilder();

            // Start of Day
            if (fill.IsStartOfDay)  // S
                sb.Append("S ");

            // Open/Close
            if (fill.OpenClose == OpenClose.Open)  // O
                sb.Append("O ");
            else if (fill.OpenClose == OpenClose.Close)  // C
                sb.Append("C ");
            else if (fill.OpenClose == OpenClose.Manual)  // M
                sb.Append("M ");
            else if (fill.OpenClose == OpenClose.StartOfDay)  // (nothing - we already handle start of day)
                ;
            else
            {
                sb.Append("? ");
                Console.WriteLine("OpenClose: " + fill.OpenClose.ToString());
            }

            // Autospreader
            if (fill.IsAutospreaderLegFill)  //ASL
                sb.Append("ASL ");
            if (fill.IsAutospreaderSyntheticFill)  //ASS
                sb.Append("ASS ");
            if (fill.IsHedge)  //H
                sb.Append("H ");
            if (fill.IsQuoting)  //Q
                sb.Append("Q ");

            // ETS
            if (fill.IsExchangeSpreadLegFill)  //ETS
                sb.Append("ETS ");

            // SSE
            if (fill.IsSseFill)  //SSE
                sb.Append("SSE ");
            if (fill.IsSseChildFill)  //SSC
                sb.Append("SSC ");

            return sb.ToString();
        }

        string ParseInstrumentKey(string key)
        {
            string result = key;
            int i = key.IndexOf("(FUTURE)");

            if (i >= 0)
            {
                int i2 = key.IndexOf(" ");
                result = key.Substring(i2, (i - i2)).Trim();
            }

            string st = key.Substring(i + 8).Trim();
            if (key.StartsWith("CME"))
            {
                if (st.Length > 5)
                {
                    char chMonth = st[4];
                    char chYear = st[5];

                    int iMonth = (int)chMonth - 64;
                    int iYear = (int)chYear - 65;   // we subtract 1 extra here because 'A' represents the year 2000
                    if (iMonth >= 1 && iMonth <= 12 && iYear >= 0 && iYear <= 99)
                    {
                        DateTime dt = new DateTime(iYear, iMonth, 1);
                        string expiry = dt.ToString("MMMyy");
                        result += "  " + expiry;
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + Time + " ");
            sb.Append(" " + Identifiers + " ");
            sb.Append(" " + BuySell.ToString() + " ");
            sb.Append(" " + Quantity.ToInt().ToString() + " ");
            sb.Append(" " + InstrumentName + " ");
            sb.Append("@ " + Price + " ");
            return (sb.ToString());
        }
    } // class

} // namespace
