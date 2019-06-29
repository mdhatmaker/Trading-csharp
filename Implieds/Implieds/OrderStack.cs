using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive;
using System.Reactive.Linq;

using Hydra.Strategy.Interfaces;

namespace Hydra.Strategy.Models.TestProjects
{
    public class OrderStack
    {
        private uint _instrumentId;

        private OrderSide _orderSide;           // are these orders bids or offers
        private int _insidePrice;               // this OrderStack contains orders starting at this price level
        private uint _stackLevelCount;          // levels deep (number of price levels) to quote
        private uint _quantityPerLevel;         // quantity to quote at each price level in the stack
        private HydraManager _hydraMgr;         // we will use this HydraManager to manipulate orders in this OrderStack

        private Dictionary<int, List<IOrder>> _orders = new Dictionary<int, List<IOrder>>(); // orders that exist at each price level

        private bool _isQuotingActive;          // are we currently quoting (submitting orders to the market)
        private OrderType _orderType;           // type of orders in this stack (limit, market, etc.)
        private TimeInForceType _orderTIF;      // TimeInForce for the orders in this stack

        public uint InstrumentId { get { return _instrumentId; } }
        public OrderSide OrderSide { get { return _orderSide; } }
        public uint StackLevelCount { get { return _stackLevelCount; } }
        public uint QuantityPerLevel { get { return _quantityPerLevel; } }
        public bool IsQuotingActive { get { return _isQuotingActive; } }
        public int InsidePrice                  // the price closest to the inside market (highest bid/lowest offer)
        {
            get
            {
                return _insidePrice;
            }
        }

        public OrderStack(IObservable<IFill> fillSource, uint instrumentId, OrderSide side, int topLevelPrice, uint stackLevelCount, uint quantityPerLevel, HydraManager hydraMgr, OrderType orderType = OrderType.Limit, TimeInForceType orderTIF = TimeInForceType.Day)
        {
            _instrumentId = instrumentId;
            _orderSide = side;
            _insidePrice = topLevelPrice;
            _stackLevelCount = stackLevelCount;
            _quantityPerLevel = quantityPerLevel;
            _hydraMgr = hydraMgr;
            _orderType = orderType;
            _orderTIF = orderTIF;

            _isQuotingActive = false;

            IObserver<IFill> obsvr = Observer.Create<IFill>(
                fill => rxFill(fill),
                ex => Console.WriteLine("OnError: {0}", ex.Message),
                () => Console.WriteLine("OnCompleted"));
            var filterSource = from source in fillSource
                               where source.InstrumentId == InstrumentId
                               select source;
            filterSource.Subscribe(obsvr);
        }

        public List<IOrder> GetOrdersAtPrice(int price)
        {
            if (_orders.ContainsKey(price))
                return _orders[price];
            else
                return null;
        }

        public int GetPriceAtLevel(uint level)
        {
            var query = from key in _orders.Keys
                        orderby key ascending
                        select key;

            int[] indexedKeys = query.ToArray();
            if (indexedKeys.Length > 0 && level < indexedKeys.Length)
                return indexedKeys[level];
            else
                return int.MinValue;
        }

        public List<IOrder> this[uint index]
        {
            get
            {
                int priceAtIndex = GetPriceAtLevel(index);
                if (priceAtIndex == int.MinValue)
                    return new List<IOrder>();
                else
                    return _orders[priceAtIndex];
            }
        }

        // A multiplier based on the order side (Buy = 1; Sell = -1)
        // So we can use side as a multiplier and then "price -= 1" would move one level AWAY from the inside market and
        // "price += 1" would move one level TOWARD the inside market
        private int side
        {
            get { return _orderSide == OrderSide.Buy ? 1 : -1; }
        }

        private void startQuoting(int topLevelPrice)
        {
            if (_isQuotingActive == true || topLevelPrice == int.MinValue) return;     // don't start quoting (sending orders) more than once

            _insidePrice = topLevelPrice;

            for (int i = 0; i < _stackLevelCount; ++i)
            {
                int price = _insidePrice - (i * side);
                submitOrderAtPrice(price);
            }
            _isQuotingActive = true;
        }

        public void StopQuoting()
        {
            for (uint i = 0; i < _stackLevelCount; ++i)
            {
                deleteAllOrdersAtLevel(i);
            }
            _isQuotingActive = false;
        }

        private void deleteAllOrdersAtLevel(uint level)
        {
            var ordersAtPrice = this[level];
            foreach (var order in ordersAtPrice)
            {
                deleteOrder(order);
            }
        }

        private void deleteOrder(IOrder order)
        {
            _hydraMgr.ExecuteCommand(new Hydra.Strategy.Commands.HydraCancelCommand(order));
        }

        private IOrder submitOrderAtPrice(int price)
        {
            return submitOrderAtPrice(price, _quantityPerLevel);
        }

        private IOrder submitOrderAtPrice(int price, uint quantity)
        {
            uint hydraId = _hydraMgr.HydraId;
            uint modelId = _hydraMgr.GetClientStrategyId();

            IOrder hydraOrder = _hydraMgr.CreateOrder(_instrumentId, (byte)_orderSide, price, quantity, modelId, hydraId, _orderType, 0, 0, _orderTIF);

            _hydraMgr.ExecuteCommand(new Hydra.Strategy.Commands.HydraSubmitCommand(hydraOrder));

            return hydraOrder;
        }

        public void UpdateInsidePrice(int updatedPrice)
        {
            if (_isQuotingActive == false)
                startQuoting(updatedPrice);
            else
                updatePrice(updatedPrice);
        }

        private void updatePrice(int updatedPrice)
        {
            if (_isQuotingActive == false || updatedPrice == _insidePrice) return;  // if price hasn't changed, then nothing to see here...

            int priceChange = updatedPrice - _insidePrice;      // TODO: need to allow for larger price moves (larger than StackLevels)
            for (uint i = 0; i < priceChange; ++i)              // the price change (previous to updated) determines how many orders we need to move
            {
                deleteAllOrdersAtLevel(i);
                int price = GetPriceAtLevel(i);
                price -= (int)_stackLevelCount * side;         // move this order to the back of the stack
                submitOrderAtPrice(price);
            }

            _insidePrice = updatedPrice;
        }

        private void rxFill(IFill fill)
        {
            Console.WriteLine("rxFill: {0}", fill.InstrumentId);
        }

    } // END OF CLASS OrderStack
} // END OF NAMESPACE
