 /*****************************************************************************\
  *                                                                           *
  *                    Unpublished Work Copyright (c) 2005-2006               *
  *                  Trading Technologies International, Inc.                 *
  *                       All Rights Reserved Worldwide                       *
  *                                                                           *
  *          * * *   S T R I C T L Y   P R O P R I E T A R Y   * * *          *
  *                                                                           *
  * WARNING:  This program (or document) is unpublished, proprietary property *
  * of Trading Technologies International, Inc. and  is  to be  maintained in *
  * strict confidence. Unauthorized reproduction,  distribution or disclosure *
  * of this program (or document),  or any program (or document) derived from *
  * it is  prohibited by  State and Federal law, and by local law  outside of *
  * the U.S.                                                                  *
  *                                                                           *
  *****************************************************************************/

using System;
using System.Text;
using System.Collections.Generic;

namespace TT.SP.Trading.Controls.MDTrader.Depth
{
    public struct Item
    {
        #region PRIVATE MEMBERS
        private int _price;
        private int _qty;
        #endregion

        #region CTOR
        public Item(int price, int quantity)
        {
            // Depth quantity should always be greater than zero.  If it's not, then you've got bad data.
            //
            // ewb.. BrokerTec sends 0 quantity when entering "workup" mode.
            // System.Diagnostics.Debug.Assert( quantity > 0 );
            _price = price;
            _qty = quantity;
        }
        #endregion

        #region PROPERTIES
        public int Price
        {
            get { return _price; }
        }

        public int Qty
        {
            get { return _qty; }
        }
        #endregion

        #region PUBLIC METHODS
        public override Boolean Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(Item))
                return false;

            Item i = (Item)obj;

            return (i._price == _price && i._qty == _qty);            
        }

        public override int GetHashCode()
        {
            return _price ^ _qty;
        }

        public static Boolean operator==(Item i1, Item i2)
        {
            return ((i1._price == i2._price) && (i1._qty == i2._qty));
        }

        public static Boolean operator !=(Item i1, Item i2)
        {
            return !(i1 == i2);
        }
        #endregion
    }

    public class Snapshot
    {
        public Snapshot()
        {
            _bidList = new List<Depth.Item>();
            _askList = new List<Depth.Item>();
        }

        private List<Depth.Item> _bidList;
        private List<Depth.Item> _askList;




        #region Properties
  
        public List<Depth.Item> BidList
        {
            get { return _bidList; }
        }

        public List<Depth.Item> AskList
        {
            get { return _askList; }
        }
        #endregion
    }
}
