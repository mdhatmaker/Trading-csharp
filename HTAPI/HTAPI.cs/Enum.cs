using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{

    public enum GatewayType { PFX, LEGACY, UNKNOWN };
    public enum GatewayStatus
    {
        INIT, XT_CONNECT, XT_CLOSED, XT_EXCHANGE, XT_CUSTLIST, XT_UNAVAILABLE, XT_AVAILABLE,
        XTAPI_SERVERMODE, CURRENCY_TABLE, LOAD_WORKSPACE, SAVE_WORKSPACE, TT_TRADER_MODE, CLOSE_ALL_WINDOWS
    };
    public enum OrderSide { BUY = 'B', SELL = 'S' };
    public enum UpdateOrderType { CHANGE = 0, CANCEL_REPLACE = 1 };
    public enum FillType { FULL = 'F', PARTIAL = 'P' };
    public enum OrderStatus { SUBMITTED, UPDATED, HELD, DELETED, REJECTED, ACTION_REJECTED };
    public enum OrderType { STOP_MARKET, STOP_LIMIT, OCO = 'B', CMO = 'C', LIMIT = 'L', MARKET = 'M', BATCH = 'O', QUOTE = 'Q', BEST_LIMIT = 'S', MARKET_TO_LIMIT = 'T', CROSS = 'X' };
    public enum LTPDirection { DOWN = -1, UNCHANGED = 0, UP = 1 };
    public enum ExchangeStatus { NO_SERVERS_UP = 0, SOME_SERVERS_UP = 1, ALL_SERVERS_UP = 2 };
    public enum LogicOperator { AND, OR };

    public class ParseEnum
    {
        public static Price ParsePrice(object o)
        {
            Price result = null;

            string st = (string) o;
            if (!st.Trim().Equals(""))
            {
                result = double.Parse(st);
            }
            return result;
        }

        public static OrderSide ParseOrderSide(string s)
        {
            OrderSide result;

            if (s.ToUpper().StartsWith("B"))
                result = OrderSide.BUY;
            else if (s.ToUpper().StartsWith("S"))
                result = OrderSide.SELL;
            else
                throw new ArgumentException();

            return result;
        }

        public static OrderType ParseOrderType(string s, Price stopPrice)
        {
            OrderType result;

            if (s.ToUpper().Equals("B"))
                result = OrderType.OCO;
            else if (s.ToUpper().Equals("C"))
                result = OrderType.CMO;
            else if (s.ToUpper().Equals("L"))
            {
                if (stopPrice != null)
                    result = OrderType.STOP_LIMIT;
                else
                    result = OrderType.LIMIT;
            }
            else if (s.ToUpper().Equals("M"))
            {
                if (stopPrice != null)
                    result = OrderType.STOP_MARKET;
                else
                    result = OrderType.MARKET;
            }
            else if (s.ToUpper().Equals("O"))
                result = OrderType.BATCH;
            else if (s.ToUpper().Equals("Q"))
                result = OrderType.QUOTE;
            else if (s.ToUpper().Equals("S"))
                result = OrderType.BEST_LIMIT;
            else if (s.ToUpper().Equals("T"))
                result = OrderType.MARKET_TO_LIMIT;
            else if (s.ToUpper().Equals("X"))
                result = OrderType.CROSS;
            else
                throw new ArgumentException();

            return result;
        }
    } // class
} //namespace
