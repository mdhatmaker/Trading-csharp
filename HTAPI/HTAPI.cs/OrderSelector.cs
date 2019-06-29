using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace HTAPI
{
    public class OrderSelector : IEnumerator, IEnumerable
    {
        private LogicOperator _testLogic;

        public LogicOperator TestLogic
        {
            get { return _testLogic; }
            set { _testLogic = value; }
        }

        private Dictionary<string, string> _tests = new Dictionary<string, string>();
        private int _index = 0;

        public void AddTest(string attribute, string value)
        {
            _tests[attribute] = value;
        }

        /// <summary>
        /// Remove all of the existing tests from the OrderSelector
        /// </summary>
        public void Clear()
        {
            _tests.Clear();
        }

        #region Implement interfaces for enumeration (for use with foreach loops)
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        public bool MoveNext()
        {
            _index++;
            return (_index < _tests.Count);
        }

        public void Reset()
        {
            _index = 0;
        }

        public object Current
        {
            get
            {
                KeyValuePair<string,string> pair = _tests.ElementAt(_index);
                return pair;
            }
        }
        #endregion
    } // class
} // namespace
