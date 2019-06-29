/*****************************************************************************\
*                                                                           *
*                    Unpublished Work Copyright (c) 2006                    *
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
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using TT.SP.Trading;
using TT.SP.Trading.MarketData.Live;
using TT.SP.Trading.Execution;
using TT.SP.Trading.Tick;

using TT.SP.Trading.Controls.Instruments;

namespace TT.SP.Trading.Controls.MDTrader
{
    public partial class AutoSpreader : UserControl
    {
        #region PRIVATE MEMBERS
        #endregion

        #region CTORS
        public AutoSpreader()
        {
            InitializeComponent();
        }
        #endregion

        #region PRIVATE THINGS
        #endregion

        #region PUBLIC THINGS
        public void RegisterOrderServiceRange(IOrderServiceListProvider orderSvcProvider)
        {
            spmdTraderControl1.RegisterOrderServiceRange(orderSvcProvider);
        }
        public void RegisterOrderService(IOrderService svc)
        {
            spmdTraderControl1.RegisterOrderService(svc);
        }
        public void Clear()
        {
            spmdTraderControl1.Clear();
        }
        public void Setup(
            ITickCalculator tickCalc,
            InsideMarketPublisher insidePublisher,
            DepthPublisher depthPublisher)
        {
            spmdTraderControl1.Setup(
                tickCalc,
                insidePublisher,
                depthPublisher);
        }
        public void MonitorOrder(IOrderDriver order)
        {
            spmdTraderControl1.TrackOrder(order);
        }
        #endregion

    }
}
