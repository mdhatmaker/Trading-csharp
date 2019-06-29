using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{
    public class OrderSet : IEnumerator, IEnumerable
    {
        public string Nickname { get; set; }
        public int Count { get { return _orders.Count; } }

        private List<Order> _orders = new List<Order>();
        private int _index = 0;

        public OrderSet(string nickname)
        {
            Nickname = nickname;
        }

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        #region Implement interfaces for enumeration (for use with foreach loops)
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            _index++;
            return (_index < _orders.Count);
        }

        public void Reset()
        {
            _index = 0;
        }

        public object Current
        {
            get { return _orders[_index]; }
        }
        #endregion

    } //class
} //namespace
