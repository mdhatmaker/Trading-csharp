using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HTAPI;

namespace HatMDTrader
{
    public partial class MDTraderControl : UserControl
    {
        HTAPI.API api = HTAPI.API.Instance;

        public MDTraderControl()
        {
            InitializeComponent();

            /*api.OnInstrumentFound += new API.NotifyInstrument(api_OnInstrumentFound);
            api.OnInstrumentNotFound += new API.NotifyInstrument(api_OnInstrumentNotFound);
            api.OnOrderSetUpdate += new API.OrderSetUpdate(api_OnOrderSetUpdate);
            api.OnOrderStatusUpdate += new API.OrderStatusUdpate(api_OnOrderStatusUpdate);
            //api.OnNotifyDrop += new API.NotifyDrop(api_OnNotifyDrop);
            api.OnInsideMarketUpdate += new API.InsideMarketUpdate(api_OnInsideMarketUpdate);
            api.OnDepthUpdate += new API.DepthUpdate(api_OnDepthUpdate);
            api.RegisterDropWindow(gridProductA.Handle.ToInt32(), 1);
            api.RegisterDropWindow(gridProductB.Handle.ToInt32(), 2);
            
            _rowsLookup = _rowsA;
            */
        }
    } // class
} // namespace
