using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{
    public class BookDepth
    {
        public Instrument Instrument;
        public DepthLevel[] BidDepth;
        public DepthLevel[] AskDepth;

        public BookDepth()
        {
        }

        public BookDepth(Instrument instrument, DepthLevel[] BidDepth, DepthLevel[] AskDepth)
        {

        }

    } //class
} //namespace
