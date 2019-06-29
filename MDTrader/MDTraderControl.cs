/*****************************************************************************\
 *                                                                           *
 *                Unpublished Work Copyright (c) 2005 - 2006                 *
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
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Windows.Forms;

using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;

using TT.SP.Trading.Controls.Misc;
using TT.SP.Trading.Controls.MDTrader;
using TT.SP.Trading.Execution;

namespace TT.SP.Trading.Controls.MDTrader
{
    #region MDTraderControl
    /// <summary>
    /// Grid control that provides MDTrader features and functionality.
    /// </summary>
    public class MDTraderControl : UserControl
    {
        #region delegate types
        public delegate void ChangeOrdersAtLevelEventHandler(int origTag, int newTag);
        public delegate void SendOrderEventHandler(int tag, bool bIsBuy);
        public delegate void OrderHoverEventHandler(bool active, int tag);
        public delegate void RecenterEventHandler();
        public delegate void ToggleMarkerEventHandler(int tag);
        public delegate void ShiftScrollEventHandler(int amount);
        public delegate void DeleteOrdersAtTagLevelEventHandler(int tag);
        public delegate void SendStopEventHandler(int tag, Side side);
        #endregion

        #region events
        public event ChangeOrdersAtLevelEventHandler ChangeOrdersAtLevel;
        public event SendOrderEventHandler SendOrder;
        public event OrderHoverEventHandler OrderHover;
        public event RecenterEventHandler Recenter;
        public event ToggleMarkerEventHandler ToggleMarker;
        public event ShiftScrollEventHandler ShiftScroll;
        public event DeleteOrdersAtTagLevelEventHandler DeleteOrdersAtTagLevel;
        public event EventHandler<EventArgs> DeleteAllWorkingBids;
        public event EventHandler<EventArgs> DeleteAllWorkingOffers;
        public event EventHandler<EventArgs> DeleteAllWorkingOrders;
        public event EventHandler<EventArgs> TradeOut;
        public event EventHandler<MouseEventArgs> PriceColumnClicked;
        public event SendStopEventHandler SendStopEvent;

        #endregion

        #region enum
        public enum LastTradedPriceDirection { NoChange, TradedUp, TradedDown };
        #endregion

        #region private members
        private PerformanceGridControl _grid;

        private MDTraderControlProperties _properties;

        private String[] _rowLabels;
        private int[] _rowTags;

        /// <summary>
        /// PriceToRowMap keeps a mapping of the rows such that
        /// a row can be easily looked up for a given price.  The
        /// price is 
        /// </summary>
        private Dictionary<int, int> _tagToRowMap = new Dictionary<int, int>();

        /// <summary>
        /// Depth Update Timer is used as an update cycle for non critical
        /// depth updates.  Depth updates are deferred when the best
        /// price level of the bid/ask remains the same, and the best
        /// qty at the best price level remains the same.  If the qty or
        /// price at the best level is updated then depth is rendered
        /// immediately.
        /// </summary>
        private Timer _depthUpdateTimer = new Timer();

        /// <summary>
        /// To prevent blocking the chart's redraw, we defer 
        /// and wait before we re-render the price
        /// levels of the grid when they are changed.
        /// </summary>
        /// 
        private bool _bRowUpdatePending;

        private bool _bOrderDragInProgress;
        private GridStyleInfo _orderDragInProgressStyle;
        private int _orderDragOrigTag = Int32.MinValue;
        private int _orderDragNewTag = Int32.MinValue;

        /// <summary>
        /// StyleInfo for Well Known grid areas
        /// </summary>
        private GridStyleInfo _buyColStyle = new GridStyleInfo();
        private GridStyleInfo _buyColStyleDisabled = new GridStyleInfo();
        private GridStyleInfo _buyColStyleCurrent;
        private GridStyleInfo _sellColStyle = new GridStyleInfo();
        private GridStyleInfo _sellColStyleDisabled = new GridStyleInfo();
        private GridStyleInfo _sellColStyleCurrent;
        private GridStyleInfo _priceColStyle = new GridStyleInfo();
        private GridStyleInfo _priceColStyleDisabled = new GridStyleInfo();
        private GridStyleInfo _priceColStyleCurrent;
        //private GridStyleInfo _ltpStyle = new GridStyleInfo();
        private GridStyleInfo _orderColStyle = new GridStyleInfo();
        private GridStyleInfo _orderColStyleDisabled = new GridStyleInfo();
        private GridStyleInfo _orderColStyleCurrent;

        private GridStyleInfo _bidHilightStyle = new GridStyleInfo();
        private GridStyleInfo _bidHilightStyleDisabled = new GridStyleInfo();
        private GridStyleInfo _bidHilightStyleCurrent;

        private GridStyleInfo _askHilightStyle = new GridStyleInfo();
        private GridStyleInfo _askHilightStyleDisabled = new GridStyleInfo();
        private GridStyleInfo _askHilightStyleCurrent;

        // ******************************************************************************
        // we can have a special style for N number of items at the 'front' of the depth
        private GridStyleInfo _frontOfBidDepthStyle = new GridStyleInfo();
        private GridStyleInfo _frontOfAskDepthStyle = new GridStyleInfo();
        private int _frontOfBidDepthRenderCount = -1;
        private int _frontOfAskDepthRenderCount = -1;
        // ******************************************************************************

        private GridBorder _whiteBorder = new GridBorder(GridBorderStyle.Solid, Color.White, GridBorderWeight.Thin);
        private GridBorder _blackBorder = new GridBorder(GridBorderStyle.Solid, Color.Black, GridBorderWeight.Thin);

        /// <summary>
        /// Cached update values for diffing and perf checking
        /// </summary>
        private int _lastLTP = TT_CONSTANTS.TT_INVALID_PRICE;
        private LastTradedPriceDirection _lastTradeDirection;
        private int _lastLTQ = -1;
        private int _lastLTQAccum = -1;
        private int _averagePrice = TT_CONSTANTS.TT_INVALID_PRICE;
        private bool _averagePriceIsBuy = true;

        private int _pendingLastLTP = -1;
        private LastTradedPriceDirection _pendingLastTradeDirection;
        private int _pendingLastLTQ = -1;
        private int _pendingLastLTQAccum = -1;

        private bool _pendingLTPUpdate;        

        private int _currentHoverCol = -1;
        private int _highlightRowTag;
        private int _lastHoverRow = int.MinValue;
        private int _lastHoverCol = int.MinValue;

        private bool _bForceMonoRendering;
        private bool _bMonoRendering = true;

        private Depth.Snapshot _lastDepthUpdate;
        private Depth.Snapshot _pendingDepthUpdate;

        private Dictionary<int, KeyValuePair<string, bool>> _tagToWorkingExecutedString = new Dictionary<int, KeyValuePair<string, bool>>();

        private int _rowOffset;
        private int _currentMouseRowIdx;
        private int _prevMouseRowIdx;

        private MethodInvoker _renderDepthInvoker;
        private MethodInvoker _lastPrcQtyUpdatedInvoker;

        // RJS. 10/4/2007... this wasn't working properly for big fonts, just got rid of it for now
        // cell model for rendering decimal places
        //private DecimalCellModel _decimalCellModel;

        private Color _highlightColor;
        private Color _shadowColor;
        private Brush _shadowBrush;
        private SizeF _columnWidthPaddingSize;

        private Boolean _displayLTQ;
        private Boolean _displayAccumLTQ;
        private Double _rowHeightFactor;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip contextMenuStripMain;
        private ToolStripMenuItem propertiesToolStripMenuItem;

        private Boolean _disposed;
        #endregion

        #region CTORS
        public MDTraderControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            SuspendLayout();
            DeserializeProperties();

            // readonly values
            _lastTradedPriceNoChangeBackColor = Color.White;
            _lastTradedPriceTradedUpBackColor = Color.Green;
            _lastTradedPriceTradedDownBackColor = Color.Red;

            // Properties
            _displayLTQ = true;
            _displayAccumLTQ = true;

            base.Font = _properties.Font;
            _rowHeightFactor = DefaultRowHeightFactor;

            // Non-designer initialization 
            _grid.ColCount = MaxColumnCount;
            _grid.RowCount = 2;

            _grid.HScrollBehavior = GridScrollbarMode.Disabled;
            _grid.VScrollBehavior = GridScrollbarMode.Disabled;

            _grid.CommandStack.Enabled = false;
            _grid.ReadOnly = true; // prevents user AND program from updating
            _grid.AllowDragSelectedCols = true;
            _grid.AllowDragSelectedRows = false;
            _grid.AllowSelection = GridSelectionFlags.Column | GridSelectionFlags.AlphaBlend;
            _grid.AlphaBlendSelectionColor = Color.Black;
            _grid.SuspendRecordUndo();
            _grid.Model.Options.NumberedColHeaders = false;

            _grid.CommandStack.Enabled = false; //Turn off Undo buffer. 

            _buyColStyle.TextColor = System.Drawing.Color.White;
            _buyColStyle.BackColor = System.Drawing.Color.DarkBlue;
            _buyColStyle.VerticalAlignment = GridVerticalAlignment.Middle;
            _buyColStyle.HorizontalAlignment = GridHorizontalAlignment.Right;
            _buyColStyle.Font.Facename = base.Font.Name;
            _buyColStyle.Font.Bold = true;
            _buyColStyle.Font.Size = base.Font.Size;
            _buyColStyle.Tag = "_buyColStyle";

            _buyColStyleDisabled.CopyFrom(_buyColStyle);
            _buyColStyleDisabled.TextColor = Color.White;
            _buyColStyleDisabled.BackColor = Color.FromArgb(16, 20, 16);
            //we will be disable initially
            _buyColStyleCurrent = _buyColStyleDisabled;

            _sellColStyle.TextColor = System.Drawing.Color.Black;
            _sellColStyle.BackColor = System.Drawing.Color.DarkRed;
            _sellColStyle.VerticalAlignment = GridVerticalAlignment.Middle;
            _sellColStyle.HorizontalAlignment = GridHorizontalAlignment.Right;
            _sellColStyle.Font.Facename = base.Font.Name;
            _sellColStyle.Font.Bold = true;
            _sellColStyle.Font.Size = base.Font.Size;
            _sellColStyle.Tag = "_sellColStyle";

            _sellColStyleDisabled.CopyFrom(_sellColStyle);
            _sellColStyleDisabled.TextColor = Color.FromArgb(132, 130, 132);
            _sellColStyleDisabled.BackColor = Color.FromArgb(49, 48, 49);

            //disabled to start
            _sellColStyleCurrent = _sellColStyleDisabled;

            _priceColStyle.TextColor = System.Drawing.Color.White;
            _priceColStyle.BackColor = System.Drawing.Color.Gray;
            _priceColStyle.Font.Facename = base.Font.Name;
            _priceColStyle.Font.Bold = true;
            _priceColStyle.Font.Size = base.Font.Size;
            _priceColStyle.VerticalAlignment = GridVerticalAlignment.Middle;
            _priceColStyle.HorizontalAlignment = GridHorizontalAlignment.Center;
            _priceColStyle.Tag = "_priceColStyle";

            _priceColStyleDisabled.CopyFrom(_priceColStyle);
            _priceColStyleDisabled.TextColor = Color.White;
            _priceColStyleDisabled.BackColor = Color.FromArgb(99, 101, 99);

            //disabled to start
            _priceColStyleCurrent = _priceColStyleDisabled;

            //_ltpStyle.TextColor = System.Drawing.Color.White;
            //_ltpStyle.BackColor = System.Drawing.Color.Green;

            _orderColStyle.TextColor = System.Drawing.Color.White;
            _orderColStyle.BackColor = System.Drawing.Color.LightGray;
            _orderColStyle.Font.Facename = base.Font.Name;
            _orderColStyle.Font.Bold = true;
            _orderColStyle.Font.Size = base.Font.Size;
            _orderColStyle.VerticalAlignment = GridVerticalAlignment.Middle;
            _orderColStyle.HorizontalAlignment = GridHorizontalAlignment.Center;
            _orderColStyle.Tag = "_orderColStyle";

            _orderColStyleDisabled.CopyFrom(_orderColStyle);
            _orderColStyleDisabled.TextColor = Color.LightGray;
            _orderColStyleDisabled.BackColor = Color.Gray;

            //disabled to start
            _orderColStyleCurrent = _orderColStyleDisabled;

            _bidHilightStyle.CopyFrom(_buyColStyle);
            _bidHilightStyle.BackColor = System.Drawing.Color.Blue;
            _bidHilightStyle.Tag = null;

            _bidHilightStyleDisabled.CopyFrom(_bidHilightStyle);
            _bidHilightStyleDisabled.BackColor = System.Drawing.Color.FromArgb(57, 56, 57);
            _bidHilightStyleDisabled.TextColor = Color.White;
            //disabled to start
            _bidHilightStyleCurrent = _bidHilightStyleDisabled;

            _askHilightStyle.CopyFrom(_sellColStyle);
            _askHilightStyle.BackColor = System.Drawing.Color.Red;
            _askHilightStyle.Tag = null;

            _askHilightStyleDisabled.CopyFrom(_askHilightStyle);
            _askHilightStyleDisabled.BackColor = System.Drawing.Color.FromArgb(66, 65, 66);
            _askHilightStyleDisabled.TextColor = Color.FromArgb(132, 130, 130);
            //disabled to start
            _askHilightStyleCurrent = _askHilightStyleDisabled;

            _frontOfBidDepthStyle.CopyFrom(_bidHilightStyle);
            _frontOfBidDepthStyle.BackColor = Color.Green;
            _frontOfBidDepthStyle.Tag = null;
            
            _frontOfAskDepthStyle.CopyFrom(_askHilightStyle);
            _frontOfAskDepthStyle.BackColor = Color.Green;
            _frontOfAskDepthStyle.Tag = null;

            GridModelHideRowColsIndexer hiddenIndexer = _grid.HideCols;
            hiddenIndexer[0] = true;
            hiddenIndexer = _grid.HideRows;

            BuyColumnPosition = _properties.BuyColPosition;
            SellColumnPosition = _properties.SellColPosition;
            PriceColumnPosition = _properties.PriceColPosition;
            OrderColumnPosition = _properties.OrderColPosition;

            BuyColumnWidth = _properties.BuyColWidth;
            SellColumnWidth = _properties.SellColWidth;
            PriceColumnWidth = _properties.PriceColWidth;
            OrderColumnWidth = _properties.OrderColWidth;

            // Timers...
            _depthUpdateTimer.Tick += new EventHandler(OnTimer);
            _depthUpdateTimer.Interval = DepthTimerInterval;

            // Handlers...         
            _grid.Click                 += _grid_Click;
            _grid.GridControlMouseUp    += _grid_GridControlMouseUp;
            _grid.CellHitTest           += _grid_CellHitTest;
            _grid.CellMouseDown         += _grid_CellMouseDown;
            _grid.CellMouseUp           += _grid_CellMouseUp;
            _grid.CellMouseHoverEnter   += _grid_CellMouseHoverEnter;
            _grid.CellMouseHoverLeave   += _grid_CellMouseHoverLeave;
            _grid.MouseLeave            += _grid_MouseLeave;
            _grid.ColsMoved             += _grid_ColsMoved;
            _grid.MouseWheel            += _grid_MouseWheel;
            _grid.MouseMove             += _grid_MouseMove;
            _grid.ResizingColumns       += _grid_ResizingColumns;
            _grid.PrepareViewStyleInfo  += _grid_PrepareViewStyleInfo;
            _grid.DoubleClick           += _grid_DoubleClick;
            _grid.KeyDown               += _grid_KeyDown;
            _grid.ColWidthsChanged += new GridRowColSizeChangedEventHandler(_grid_ColWidthsChanged);

            _grid.RowHeights.SetRange(0, 0, HeaderRowHeight);
            _grid.MinResizeColSize = MinimumColumnWidth;

            _renderDepthInvoker = RenderDepth;
            _lastPrcQtyUpdatedInvoker = LastPrcQtyUpdated;

            _highlightColor = SystemColors.ControlLightLight;
            _shadowColor = SystemColors.ControlDarkDark;
            _shadowBrush = new SolidBrush(_shadowColor);

            using (Graphics g = Graphics.FromHwnd(_grid.Handle))
                _columnWidthPaddingSize = g.MeasureString(ColumnWidthPadding, base.Font);
            ResumeLayout();
        }

        void _grid_KeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                SafeInvoke_Recenter();
            }
        }

        private void RefreshCellStyles()
        {
            _grid.ChangeCells(GridRangeInfo.Col(BuyColumnPosition), _buyColStyleCurrent);
            _grid.ChangeCells(GridRangeInfo.Col(SellColumnPosition), _sellColStyleCurrent);
            _grid.ChangeCells(GridRangeInfo.Col(PriceColumnPosition), _priceColStyleCurrent);
            _grid.ChangeCells(GridRangeInfo.Col(OrderColumnPosition), _orderColStyleCurrent);
        }

        public void SetColorsDisabled()
        {
            _grid.IgnoreReadOnly = true;
            _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);
            try
            {
                _lastTradedPriceNoChangeBackColor = Color.DarkGray;
                _lastTradedPriceTradedUpBackColor = Color.DarkGray;
                _lastTradedPriceTradedDownBackColor = Color.DarkGray;

                _sellColStyleCurrent = _sellColStyleDisabled;
                _buyColStyleCurrent = _buyColStyleDisabled;
                _priceColStyleCurrent = _priceColStyleDisabled;
                _orderColStyleCurrent = _orderColStyleDisabled;
                _bidHilightStyleCurrent = _bidHilightStyleDisabled;
                _askHilightStyleCurrent = _askHilightStyleDisabled;
                RefreshCellStyles();

                _frontOfBidDepthStyle.BackColor = Color.DarkGray;
                _frontOfAskDepthStyle.BackColor = Color.DarkGray;

                _highlightColor = SystemColors.ControlLightLight;
                _shadowColor = SystemColors.ControlDarkDark;
                _shadowBrush = new SolidBrush(_shadowColor);

                int row = RowFromTag(_lastLTP);
                if (row > 0)
                {
                    GridStyleInfo gsi = _grid[row, PriceColumnPosition];
                    gsi.BackColor = Color.DarkGray;
                    gsi.TextColor = Color.LightGray;
                }
            }
            finally
            {
                _grid.EndUpdate();
            }
            _grid.IgnoreReadOnly = false;
            RenderRefreshLastDepth();
            //_grid.Invalidate();
        }

        public void SetColorsEnabled()
        {
            _grid.IgnoreReadOnly = true;
            _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);
            try
            {
                _lastTradedPriceNoChangeBackColor = Color.White;
                _lastTradedPriceTradedUpBackColor = Color.Green;
                _lastTradedPriceTradedDownBackColor = Color.Red;

                _buyColStyleCurrent = _buyColStyle;
                _sellColStyleCurrent = _sellColStyle;
                _priceColStyleCurrent = _priceColStyle;
                _orderColStyleCurrent = _orderColStyle;
                _askHilightStyleCurrent = _askHilightStyle;
                _bidHilightStyleCurrent = _bidHilightStyle;

                RefreshCellStyles();

                _frontOfBidDepthStyle.BackColor = Color.Green;
                _frontOfAskDepthStyle.BackColor = Color.Green;

                _highlightColor = SystemColors.ControlLightLight;
                _shadowColor = SystemColors.ControlDarkDark;
                _shadowBrush = new SolidBrush(_shadowColor);

                int row = RowFromTag(_lastLTP);
                if (row > 0)
                {
                    GridStyleInfo gsi = _grid[row, PriceColumnPosition];
                    switch (_lastTradeDirection)
                    {
                        case LastTradedPriceDirection.NoChange:
                            gsi.BackColor = _lastTradedPriceNoChangeBackColor;
                            gsi.TextColor = System.Drawing.Color.Black;  // hack
                            break;
                        case LastTradedPriceDirection.TradedDown:
                            gsi.BackColor = _lastTradedPriceTradedDownBackColor;
                            gsi.TextColor = Color.White;
                            break;
                        case LastTradedPriceDirection.TradedUp:
                            gsi.BackColor = _lastTradedPriceTradedUpBackColor;
                            gsi.TextColor = Color.White;
                            break;
                    }
                }
            }
            finally
            {
                _grid.EndUpdate();
            }
            _grid.IgnoreReadOnly = false;
            RenderRefreshLastDepth();
            //_grid.Invalidate();
        }

        private void RenderRefreshLastDepth()
        {
            //if (_pendingDepthUpdate != null)
            //{
            //    RenderDepth();
            //}

            _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);
            try
            {
                _grid.IgnoreReadOnly = true;   // allows program to still change cells

                //change cell interior to disabled/enabled interior
                if (_lastDepthUpdate != null)
                {
                    for (int idx = 0; idx < _lastDepthUpdate.BidList.Count; idx++)
                    {
                        Depth.Item di = _lastDepthUpdate.BidList[idx];
                        int row = RowFromTag(di.Price);
                        if (row != -1)
                        {
                            GridStyleInfo gsi = _grid[row, BuyColumnPosition];
                            gsi.Interior = _buyColStyleCurrent.Interior;
                        }
                    }

                    for (int idx = 0; idx < _lastDepthUpdate.AskList.Count; idx++)
                    {
                        Depth.Item di = _lastDepthUpdate.AskList[idx];
                        int row = RowFromTag(di.Price);
                        if (row != -1)
                        {
                            GridStyleInfo gsi = _grid[row, SellColumnPosition];
                            gsi.Interior = _sellColStyleCurrent.Interior;
                        }
                    }
                }
            }
            finally
            {
                //_lastDepthUpdate = _pendingDepthUpdate;
                //_pendingDepthUpdate = null;
                _grid.IgnoreReadOnly = false;

                _grid.EndUpdate(false);
            }
        }

        void _grid_ColWidthsChanged(object sender, GridRowColSizeChangedEventArgs e)
        {
            for (int i = e.From; i <= e.To; i++)
            {
                if (i == BuyColumnPosition)
                    _properties.BuyColWidth = BuyColumnWidth;
                else if (i == SellColumnPosition)
                    _properties.SellColWidth = SellColumnWidth;
                else if (i == OrderColumnPosition)
                    _properties.OrderColWidth = OrderColumnWidth;
                else if (i == PriceColumnPosition)
                    _properties.PriceColWidth = PriceColumnWidth;
            }
        }
        #endregion

        #region properties
        internal MDTraderControlProperties Properties
        {
            [DebuggerStepThrough]
            get { return _properties; }
        }

        public static string DefaultFontFacename { get { return "Arial"; } }
        public static float DefaultFontSize { get { return 11; } }

        public bool ForceMonoRendering
        {
            get { return _bForceMonoRendering; }
            set
            {
                if (value != _bForceMonoRendering)
                {
                    _bForceMonoRendering = value;
                    Refresh();
                    Update();
                }
            }
        }

        public void InvokePaint(PaintEventArgs e, bool bForceMonoRendering)
        {
            bool bForceMonoRenderingPrev = _bForceMonoRendering;

            _bForceMonoRendering = bForceMonoRendering;

            InvokePaint(_grid, e);

            ForceMonoRendering = bForceMonoRenderingPrev;
        }

        private bool MonoRendering
        {
            get { return _bMonoRendering; }
            set
            {
                if (value != _bMonoRendering)
                {
                    _bMonoRendering = value;
                    Refresh();
                }
            }
        }

        public int MinimumRowHeight
        {
            get { return (int)System.Math.Ceiling(_priceColStyle.Font.GdipFont.GetHeight() * RowHeightFactor); }
        }

        private bool IsHighlightRowActive()
        {
            return (_highlightRowTag > 0);
        }

        private int HighlightRowTag
        {
            get
            {
                return _highlightRowTag;
            }

            set
            {
                if (_highlightRowTag == value)
                    return;  // hasn't changed

                _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);

                if (_highlightRowTag != -1)
                {
                    // Force redraw of old highlight row
                    int oldRow = RowFromTag(_highlightRowTag);
                    if (oldRow > 0)
                        _grid.RefreshRange(GridRangeInfo.Row(oldRow));
                }

                if (value != -1)
                {
                    // Force redraw of highlight at new level
                    int newRow = RowFromTag(value);
                    if (newRow > 0)
                        _grid.RefreshRange(GridRangeInfo.Row(newRow));
                }
                _grid.EndUpdate(false);

                _highlightRowTag = value;

                if (_highlightRowTag == -1)
                    _currentHoverCol = -1;

                if (OrderHover != null)
                {
                    if (IsHighlightRowActive())
                        OrderHover(true, HighlightRowTag);
                    else
                        OrderHover(false, System.Int32.MinValue);
                }
            }
        }

        public Int32 RowCount
        {
            get { return _grid.RowCount; }
        }

        /// <summary>
        /// Gets or sets the position of the buy column.
        /// </summary>
        public Int32 BuyColumnPosition
        {
            get { return _properties.BuyColPosition; }
            set
            {
                if (value > MaxColumnCount)
                    throw new ArgumentException("Buy column position cannot be greater than the maximum column count: " +
                        MaxColumnCount, "value");

                _properties.BuyColPosition = value;
                _grid.ColStyles[value] = _buyColStyleCurrent;
            }
        }
        /// <summary>
        /// Gets or set the position of the sell column.
        /// </summary>
        public Int32 SellColumnPosition
        {
            get { return _properties.SellColPosition; }
            set
            {
                if (value > MaxColumnCount)
                    throw new ArgumentException("Sell column position cannot be greater than the maximum column count: " +
                        MaxColumnCount, "value");

                _properties.SellColPosition = value;
                _grid.ColStyles[value] = _sellColStyleCurrent;
            }
        }
        /// <summary>
        /// Gets or sets the position of the price column.
        /// </summary>
        public Int32 PriceColumnPosition
        {
            get { return _properties.PriceColPosition; }
            set
            {
                if (value > MaxColumnCount)
                    throw new ArgumentException("Price column position cannot be greater than the maximum column count: " +
                        MaxColumnCount, "value");

                _properties.PriceColPosition = value;
                _grid.ColStyles[value] = _priceColStyleCurrent;
            }
        }
        /// <summary>
        /// Gets or sets the position of the order column.
        /// </summary>
        public Int32 OrderColumnPosition
        {
            get { return _properties.OrderColPosition; }
            set
            {
                if (value > MaxColumnCount)
                    throw new ArgumentException("Order column position cannot be greater than the maximum column count: " +
                        MaxColumnCount, "value");

                _properties.OrderColPosition = value;
                _grid.ColStyles[value] = _orderColStyleCurrent;
            }
        }

        /// <summary>
        /// Gets or sets the width of the buy column.
        /// </summary>
        public Int32 BuyColumnWidth
        {
            get { return _grid.Cols.Size.GetSize(BuyColumnPosition); }
            set
            {
                if (value != 0)
                {
                    _properties.BuyColWidth = value;
                    _grid.ChangeCells(GridRangeInfo.Col(BuyColumnPosition), _buyColStyleCurrent);
                    _grid.Cols.Size.SetSize(BuyColumnPosition, value);
                }
            }
        }
        /// <summary>
        /// Gets or sets the width of the sell column.
        /// </summary>
        public Int32 SellColumnWidth
        {
            get { return _grid.Cols.Size.GetSize(SellColumnPosition); }
            set
            {
                if (value != 0)
                {
                    _properties.SellColWidth = value;
                    _grid.ChangeCells(GridRangeInfo.Col(SellColumnPosition), _sellColStyleCurrent);
                    _grid.Cols.Size.SetSize(SellColumnPosition, value);
                }
            }
        }
        /// <summary>
        /// Gets or sets the width of the price column.
        /// </summary>
        public Int32 PriceColumnWidth
        {
            get { return _grid.Cols.Size.GetSize(PriceColumnPosition); }
            set
            {
                if (value != 0)
                {
                    _properties.PriceColWidth = value;
                    _grid.ChangeCells(GridRangeInfo.Col(PriceColumnPosition), _priceColStyleCurrent);
                    _grid.Cols.Size.SetSize(PriceColumnPosition, value);
                }
            }
        }
        /// <summary>
        /// Gets or sets the width of the order column.
        /// </summary>
        public Int32 OrderColumnWidth
        {
            get { return _grid.Cols.Size.GetSize(OrderColumnPosition); }
            set
            {
                if (value != 0)
                {
                    _properties.OrderColWidth = value;
                    _grid.ChangeCells(GridRangeInfo.Col(OrderColumnPosition), _orderColStyleCurrent);
                    _grid.Cols.Size.SetSize(OrderColumnPosition, value);
                }
            }
        }

        /// <summary>
        /// Get or sets a value indicating whether or not to display the last traded quantity.
        /// </summary>
        public Boolean DisplayLTQ
        {
            get { return _displayLTQ; }
            set { _displayLTQ = value; }
        }

        /// <summary>
        /// Get or sets a value indicating whether or not to display the accumulated last traded quantity.
        /// </summary>
        public Boolean DisplayAccumLTQ
        {
            get { return _displayAccumLTQ; }
            set { _displayAccumLTQ = value; }
        }

        /// <summary>
        /// Gets or sets a value used to determine the cell width multiplier.
        /// </summary>
        public Double RowHeightFactor
        {
            get { return _rowHeightFactor; }
            set { _rowHeightFactor = value; }
        }

        public Int32 MinimumColumnWidth
        {
            get { return Convert.ToInt32(HeaderRowHeight * (RowHeightFactor / 2)); }
        }

        #region STATIC PROPERTIES

        public static Int32 MaxColumnCount
        {
            get { return 4; }
        }
        public static Int32 DepthTimerInterval
        {
            get { return 300; }
        }
        
        public static Double DefaultRowHeightFactor
        {
            get { return 1.382; }
        }

        public static Char BrokerTecWideChar
        {
            get { return '+'; }
        }

        public static Int32 DeleteOrderRow
        {
            get { return 1; }
        }
        public static Int32 DeleteOrderGridHeaderHeight
        {
            get { return 7; }
        }
        public static Int32 HeaderRowHeight
        {
            get { return 15; }
        }

        public static String ColumnWidthPadding
        {
            get { return "X"; }
        }

        private Color _lastTradedPriceNoChangeBackColor;
        private Color _lastTradedPriceTradedUpBackColor;
        private Color _lastTradedPriceTradedDownBackColor;

        public static Color WorkingOrderBuyBackColor
        {
            get { return Color.SteelBlue; }
        }
        public static Color WorkingOrderSellBackColor
        {
            get { return Color.FromArgb(192, 12, 24); }
        }
        public static Color WorkingOrderDragBuyBackColor
        {
            get { return Color.LightSteelBlue; }
        }
        public static Color WorkingOrderDragSellBackColor
        {
            get { return Color.DarkSalmon; }
        }
        #endregion
        #endregion

        #region IDISPOSABLE MEMBERS
        /// <summary>
        /// Design pattern implementation of the derived class version of Dipose(bool).
        /// </summary>
        /// <param name="disposing">The indication of which type of dipose scenario is executing.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                try
                {
                    if (disposing)
                    {
                        if (_grid != null)
                            _grid.Dispose();

                        if (_orderDragInProgressStyle != null)
                            _orderDragInProgressStyle.Dispose();

                        if (_buyColStyle != null)
                            _buyColStyle.Dispose();

                        if (_buyColStyleDisabled != null)
                            _buyColStyleDisabled.Dispose();

                        if (_sellColStyle != null)
                            _sellColStyle.Dispose();

                        if (_sellColStyleDisabled != null)
                            _sellColStyleDisabled.Dispose();

                        if (_priceColStyle != null)
                            _priceColStyle.Dispose();

                        if (_priceColStyleDisabled != null)
                            _priceColStyleDisabled.Dispose();

                        //if (_ltpStyle != null)
                        //    _ltpStyle.Dispose();

                        if (_orderColStyle != null)
                            _orderColStyle.Dispose();

                        if (_orderColStyleDisabled != null)
                            _orderColStyleDisabled.Dispose();

                        if (_bidHilightStyle != null)
                            _bidHilightStyle.Dispose();

                        if (_bidHilightStyleDisabled != null)
                            _bidHilightStyleDisabled.Dispose();

                        if (_askHilightStyleDisabled != null)
                            _askHilightStyleDisabled.Dispose();

                        if (_askHilightStyle != null)
                            _askHilightStyle.Dispose();

                        if (_depthUpdateTimer != null)
                            _depthUpdateTimer.Dispose();

                        //if (_decimalCellModel != null)
                        //    _decimalCellModel.Dispose();
                        
                        if (_shadowBrush != null)
                            _shadowBrush.Dispose();

                        SerializeProperties();
                    }

                    _disposed = true;
                }
                finally
                {
                    base.Dispose(disposing);
                }
            }
        }
        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle1 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle2 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle3 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            Syncfusion.Windows.Forms.Grid.GridBaseStyle gridBaseStyle4 = new Syncfusion.Windows.Forms.Grid.GridBaseStyle();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._grid = new TT.SP.Trading.Controls.MDTrader.PerformanceGridControl();
            this.contextMenuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propertiesToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStrip1";
            this.contextMenuStripMain.Size = new System.Drawing.Size(135, 26);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // _grid
            // 
            this._grid.BackColor = System.Drawing.Color.Black;
            gridBaseStyle1.Name = "Row Header";
            gridBaseStyle1.StyleInfo.BaseStyle = "Header";
            gridBaseStyle1.StyleInfo.CellType = "Static";
            gridBaseStyle1.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Left;
            gridBaseStyle1.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(199)))), ((int)(((byte)(184))))), System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            gridBaseStyle2.Name = "Column Header";
            gridBaseStyle2.StyleInfo.BaseStyle = "Header";
            gridBaseStyle2.StyleInfo.CellType = "Header";
            gridBaseStyle2.StyleInfo.Clickable = true;
            gridBaseStyle2.StyleInfo.Enabled = true;
            gridBaseStyle2.StyleInfo.HorizontalAlignment = Syncfusion.Windows.Forms.Grid.GridHorizontalAlignment.Center;
            gridBaseStyle2.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.None, System.Drawing.SystemColors.WindowText, System.Drawing.Color.DarkGray);
            gridBaseStyle3.Name = "Standard";
            gridBaseStyle3.StyleInfo.CellType = "Static";
            gridBaseStyle3.StyleInfo.Clickable = false;
            gridBaseStyle3.StyleInfo.Enabled = false;
            gridBaseStyle3.StyleInfo.Font.Facename = "Tahoma";
            gridBaseStyle3.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.SystemColors.Window);
            gridBaseStyle4.Name = "Header";
            gridBaseStyle4.StyleInfo.Borders.Bottom = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle4.StyleInfo.Borders.Left = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle4.StyleInfo.Borders.Right = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle4.StyleInfo.Borders.Top = new Syncfusion.Windows.Forms.Grid.GridBorder(Syncfusion.Windows.Forms.Grid.GridBorderStyle.None);
            gridBaseStyle4.StyleInfo.CellType = "Header";
            gridBaseStyle4.StyleInfo.Font.Bold = true;
            gridBaseStyle4.StyleInfo.Interior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(234)))), ((int)(((byte)(216))))));
            gridBaseStyle4.StyleInfo.VerticalAlignment = Syncfusion.Windows.Forms.Grid.GridVerticalAlignment.Middle;
            this._grid.BaseStylesMap.AddRange(new Syncfusion.Windows.Forms.Grid.GridBaseStyle[] {
            gridBaseStyle1,
            gridBaseStyle2,
            gridBaseStyle3,
            gridBaseStyle4});
            this._grid.ColWidthEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridColWidth[] {
            new Syncfusion.Windows.Forms.Grid.GridColWidth(0, 35)});
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.HScrollBehavior = Syncfusion.Windows.Forms.Grid.GridScrollbarMode.Disabled;
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Name = "_grid";
            this._grid.RowHeightEntries.AddRange(new Syncfusion.Windows.Forms.Grid.GridRowHeight[] {
            new Syncfusion.Windows.Forms.Grid.GridRowHeight(0, 21)});
            this._grid.Size = new System.Drawing.Size(232, 255);
            this._grid.SmartSizeBox = false;
            this._grid.TabIndex = 0;
            this._grid.Text = "MDTraderControl (c) 2005-2008 TT";
            this._grid.ThemesEnabled = true;
            this._grid.UseGDI = false;
            this._grid.VScrollBehavior = Syncfusion.Windows.Forms.Grid.GridScrollbarMode.Disabled;
            // 
            // MDTraderControl
            // 
            this.Controls.Add(this._grid);
            this.Name = "MDTraderControl";
            this.Size = new System.Drawing.Size(232, 255);
            this.Load += new System.EventHandler(this.MDTraderControl_Load);
            this.Resize += new System.EventHandler(this.MDTraderControl_Resize);
            this.contextMenuStripMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depthUpdate"></param>
        public void AcceptDepth(Depth.Snapshot depthUpdate)
        {
            if (IsDisposed) return;

            Debug.Assert(depthUpdate != null, "Depthupdate parameter cannot be null");
            _pendingDepthUpdate = depthUpdate;
            BeginInvoke(_renderDepthInvoker);
        }
        /// <summary>
        /// 
        /// </summary>
        public void RenderDepth()
        {
            if (_pendingDepthUpdate == null)
                return;

            // If the user is busy resizing the
            // grid, don't render until the re-size timer kicks
            // in and the _bRowUpdatePending is cleared.
            if (_bRowUpdatePending)
                return;

            _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);
            try
            {
                _grid.IgnoreReadOnly = true;   // allows program to still change cells

                #region Clear out the cells in the grid that are no longer going
                // ***************************************************************************
                // Clear out the cells in the grid that are no longer going
                // to have market depth quantities displayed in them.  i.e.,
                // price levels that are no longer present should get blanked out
                // and their color should be restored back to the "normal" depth
                // column cell's appearance.
                if (_lastDepthUpdate != null)
                {
                    for (int idx = 0; idx < _lastDepthUpdate.BidList.Count; idx++)
                    {
                        Depth.Item di = _lastDepthUpdate.BidList[idx];

                        if (!_pendingDepthUpdate.BidList.Contains(di))
                        {
                            int row = RowFromTag(di.Price);
                            if (row != -1)
                            {
                                GridStyleInfo gsi = _grid[row, BuyColumnPosition];
                                gsi.Text = String.Empty;
                                gsi.Interior = _buyColStyleCurrent.Interior;
                            }
                        }
                    }

                    for (int idx = 0; idx < _lastDepthUpdate.AskList.Count; idx++)
                    {
                        Depth.Item di = _lastDepthUpdate.AskList[idx];

                        if (!_pendingDepthUpdate.AskList.Contains(di))
                        {
                            int row = RowFromTag(di.Price);
                            if (row != -1)
                            {
                                GridStyleInfo gsi = _grid[row, SellColumnPosition];
                                gsi.Text = String.Empty;
                                gsi.Interior = _sellColStyleCurrent.Interior;
                            }
                        }
                    }
                }
                // ***************************************************************************
                #endregion

                #region Highlight the new rows with depth available
                // ***************************************************************************
                // Highlight the new rows with depth available
                for (int idx = 0; idx < _pendingDepthUpdate.BidList.Count; idx++)
                {
                    Depth.Item di = _pendingDepthUpdate.BidList[idx];

                    int row = RowFromTag(di.Price);
                    if (row >= 0)
                    {
                        string qtyStr = di.Qty.ToString();
                        GridStyleInfo gsi = _grid[row, BuyColumnPosition];
                        if (gsi.Text != qtyStr)
                        {
                            gsi.Text = qtyStr;

                            if (idx > _frontOfBidDepthRenderCount - 1)
                                gsi.Interior = _bidHilightStyleCurrent.Interior;
                            else
                                gsi.Interior = _frontOfBidDepthStyle.Interior;
                        }
                    }
                }

                for (int idx = 0; idx < _pendingDepthUpdate.AskList.Count; idx++)
                {
                    Depth.Item di = _pendingDepthUpdate.AskList[idx];

                    int row = RowFromTag(di.Price);
                    if (row >= 0)
                    {
                        string qtyStr = di.Qty.ToString();
                        GridStyleInfo gsi = _grid[row, SellColumnPosition];
                        if (gsi.Text != qtyStr)
                        {
                            gsi.Text = qtyStr;

                            if (idx > _frontOfAskDepthRenderCount - 1)
                                gsi.Interior = _askHilightStyleCurrent.Interior;
                            else
                                gsi.Interior = _frontOfAskDepthStyle.Interior;
                        }
                    }
                }
                // ***************************************************************************
                #endregion
            }
            finally
            {
                _lastDepthUpdate = _pendingDepthUpdate;
                _pendingDepthUpdate = null;
                _grid.IgnoreReadOnly = false;

                _grid.EndUpdate(false);
            }
        }
        /// <summary>
        /// Call this when you know that BOTH LTQ and LTP have changed together
        /// </summary>
        /// <param name="price"></param>
        /// <param name="qty"></param>
        /// <param name="accumulated"></param>
        public void SetLastTradePriceAndQty(int price, int qty, int accumulated)
        {
            SetLTQ(qty, accumulated);
            SetLTP(price);
        }
        /// <summary>
        /// Call this when ONLY the LTP value has changed
        /// </summary>
        /// <param name="price"></param>
        public void SetLTP(int price)
        {
            if (IsDisposed) return;

            if ( (_pendingLastLTP == price) || (_pendingLastLTP == TT_CONSTANTS.TT_INVALID_PRICE) || (price == TT_CONSTANTS.TT_INVALID_PRICE) )
            {
                _pendingLastTradeDirection = LastTradedPriceDirection.NoChange;
            }
            else
            {
                if (price > _pendingLastLTP)
                    _pendingLastTradeDirection = LastTradedPriceDirection.TradedUp;
                else
                    _pendingLastTradeDirection = LastTradedPriceDirection.TradedDown;
            }

            _pendingLastLTP = price;            

            if (_pendingLTPUpdate == false)
            {
                _pendingLTPUpdate = true;
                if (!IsDisposed)
                    BeginInvoke(_lastPrcQtyUpdatedInvoker);
             }
        }
        /// <summary>
        /// Call this when ONLY the LTQ has changed.  Calls to this method will
        /// ALWAYS force the LTP background color to indicate "unchanged"
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="accumulated"></param>
        public void SetLTQ(int qty, int accumulated)
        {
            if (IsDisposed) return;

            _pendingLastTradeDirection = LastTradedPriceDirection.NoChange;

            _pendingLastLTQ = qty;
            _pendingLastLTQAccum = accumulated;

            if (_pendingLTPUpdate == false)
            {
                _pendingLTPUpdate = true;
                BeginInvoke(_lastPrcQtyUpdatedInvoker);
            }        
        }
        /// <summary>
        /// 
        /// </summary>
        public void ClearAveragePrice()
        {
            SetAveragePrice(TT_CONSTANTS.TT_INVALID_PRICE, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="averagePriceTag"></param>
        /// <param name="isBuy"></param>
        public void SetAveragePrice(int averagePriceTag, bool isBuy)
        {            
            // Invalidate the old price level's cell
            int oldRow = RowFromTag(_averagePrice);
            if (oldRow != -1)
                _grid.InvalidateRange(GridRangeInfo.Cell(oldRow, PriceColumnPosition));

            // Take on the new value
            _averagePrice = averagePriceTag;
            _averagePriceIsBuy = isBuy;

            // Invalidate the new value's cell
            int newRow = RowFromTag(_averagePrice);
            if (newRow != -1)
                _grid.InvalidateRange(GridRangeInfo.Cell(newRow, PriceColumnPosition));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="bIsBuy"></param>
        /// <param name="displayString"></param>
        public void SetWorkingExecAtPriceLevel(int tag, bool bIsBuy, string displayString)
        {
            _tagToWorkingExecutedString[tag] = new KeyValuePair<string, bool>(displayString, bIsBuy);

            int row = RowFromTag(tag);
            if (row != -1)
                _grid.InvalidateRange(GridRangeInfo.Cell(row, OrderColumnPosition));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveWorkingExecAtPriceLevel(int tag)
        {
            if (!_tagToWorkingExecutedString.ContainsKey(tag)) return;

            _tagToWorkingExecutedString.Remove(tag);

            int row = RowFromTag(tag);
            if (row != -1)
                _grid.InvalidateRange(GridRangeInfo.Cols(row, OrderColumnPosition));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="labels"></param>
        /// <param name="tags"></param>
        public void SetGridRows(System.String[] labels, int[] tags)
        {
            Debug.Assert(labels.Length == tags.Length, "arguments should be the same");
            if (labels.Equals(_rowLabels) && tags.Equals(_rowTags))
                return;
            if (labels.Equals(_rowLabels) && tags.Equals(_rowTags))
                return;

            //TODO - tpeterson - I think we may need to clear out the orders column here
            //to remove an order indicators from the previous target of the control

            _rowLabels = labels;
            _rowTags = tags;

            _tagToRowMap.Clear();
            MonoRendering = false;

            if (!_bRowUpdatePending)
            {
                _bRowUpdatePending = true;

                if (Handle != IntPtr.Zero)
                {
                    BeginInvoke(
                        (MethodInvoker)delegate()
                        {
                            OnGridRowInfoUpdatedHandler();
                        });
                }
            }
        }

        public int GetRecommendedRowHeight()
        {
            return Font.Height;
        }
        public int GetGridHeightMinusHeader()
        {
            return _grid.Height - HeaderRowHeight;
        }
        public int RowHeightForSpecifiedRowCount(int count)
        {
            Int32 returnValue = (int)System.Math.Ceiling((_grid.Height - HeaderRowHeight) / (double)count);
            //Debug.Assert(returnValue >= MinimumRowHeight);
            return returnValue;
        }
        
        public void SetFrontOfDepthCount(bool bidSide, int depthCount)
        {
            if (!bidSide)
                _frontOfAskDepthRenderCount = depthCount;
            else
                _frontOfBidDepthRenderCount = depthCount;
        }
        #endregion

        #region private methods

        private void RaiseDeleteAllWorkingBids()
        {
            if (DeleteAllWorkingBids != null)
            {
                DeleteAllWorkingBids(this, EventArgs.Empty);
            }
        }
        private void RaiseDeleteAllWorkingOffers()
        {
            if (DeleteAllWorkingOffers != null)
            {
                DeleteAllWorkingOffers(this, EventArgs.Empty);
            }
        }
        private void RaiseDeleteAllWorkingOrders()
        {
            if (DeleteAllWorkingOrders != null)
            {
                DeleteAllWorkingOrders(this, EventArgs.Empty);
            }
        }
        private void RaiseTradeOut()
        {
            if (TradeOut != null)
            {
                TradeOut(this, EventArgs.Empty);
            }
        }
        private void RaiseSendStopEvent(int tag, TT.SP.Trading.Execution.Side side)
        {
            if (SendStopEvent != null)
            {
                SendStopEvent(tag, side);
            }
        }


        
        private void DeserializeProperties()
        {
            if (DesignMode) return;

            string fileName = "MDTraderControlProperties.bin";
            try
            {
                if (!File.Exists(fileName))
                {
                    _properties = new MDTraderControlProperties();
                }
                else
                {
                    using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        _properties = (MDTraderControlProperties)new BinaryFormatter().Deserialize(fs);
                    }
                }

                _properties.FontChanged += new EventHandler(_properties_FontChanged);
            }
            catch (Exception)
            {
                _properties = new MDTraderControlProperties();
            }
        }
        private void SerializeProperties()
        {
            if (DesignMode) return;

            string fileName = "MDTraderControlProperties.bin";
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    new BinaryFormatter().Serialize(fs, _properties);
                }
            }
            catch (Exception)
            {
                _properties = new MDTraderControlProperties();
            }
        }

        private void LastPrcQtyUpdated()
        {
            if (_lastLTP != _pendingLastLTP)
            {
                ClearLastPrcQty(_lastLTP);
            }

            _lastLTP = _pendingLastLTP;
            _lastLTQ = _pendingLastLTQ;
            _lastLTQAccum = _pendingLastLTQAccum;
            _lastTradeDirection = _pendingLastTradeDirection;

            RenderLastPrcQty();

            _pendingLTPUpdate = false;
        }
        /// <summary>
        /// Given a price-tag we can get back the row that it is displayed at
        /// </summary>
        /// <param name="price"></param>
        /// <returns>A value of -1 indicates that no row is showing that price</returns>
        private int RowFromTag(int tag)
        {
            if (_tagToRowMap == null || _tagToRowMap.Count == 0)
                return -1; // throw new InvalidOperationException("Cannot get row from tag; _tagToRowMap not initialized");

            if (_bRowUpdatePending)
                return -1;

            int retval = -1;

            if (_tagToRowMap.TryGetValue(tag, out retval))
                return retval;
            else
                return -1;
        }
        private int TagFromRow(int gridRow)
        {
            if (_bRowUpdatePending || gridRow < 1)
                return System.Int32.MinValue;

            if (gridRow < 1 || _rowTags == null)
                return System.Int32.MinValue;

            int index = gridRow + _rowOffset;

            if (index >= _rowTags.Length)
                return System.Int32.MinValue;

            return _rowTags[index];
        }
        private void ClearLastPrcQty(int price)
        {
            if (_bRowUpdatePending)
                return;

            _grid.IgnoreReadOnly = true;
            _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);

            try
            {
                int row = RowFromTag(price);

                if (row >= 0)
                {
                    GridStyleInfo gsi = _grid[row, PriceColumnPosition];
                    gsi.CopyFrom(_priceColStyleCurrent);                    
                    //gsi.BackColor = System.Drawing.Color.Purple;//.Gray;
                    gsi.Text = _rowLabels[row - 1];
                }
            }
            finally
            {
                _grid.IgnoreReadOnly = false;
                _grid.EndUpdate(false);
            }
        }
        private void RenderLastPrcQty()
        {
            if (_bRowUpdatePending)
                return;

            _grid.IgnoreReadOnly = true;
            _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);

            try
            {
                int row = RowFromTag(_lastLTP);

                if (row > 0)
                {
                    GridStyleInfo gsi = _grid[row, PriceColumnPosition];

                    // If the user has elected not to show the last traded quantity as well as not showing the accumulated
                    // last traded quantity then leave the price column format unchanged.
                    if (!_displayLTQ && !_displayAccumLTQ)
                    {
                        gsi.Text = _rowLabels[row-1];
                    }
                    else
                    {
                        if (_lastLTQ != -1 && _lastLTQAccum != -1)
                        {
                            if (_displayLTQ && _displayAccumLTQ)
                                gsi.Text = _lastLTQ.ToString() + " : " + _lastLTQAccum.ToString();
                            else if (_displayLTQ && !_displayAccumLTQ)
                                gsi.Text = _lastLTQ.ToString();
                            else if (!_displayLTQ && _displayAccumLTQ)
                                gsi.Text = _lastLTQAccum.ToString();
                            else
                                gsi.Text = "";
                        }
                        else if (_lastLTQAccum != -1)
                        {
                            if (_displayAccumLTQ)
                                gsi.Text = _lastLTQAccum.ToString();
                            else
                                gsi.Text = "";
                        }
                        else if (_lastLTQ != -1)
                        {
                            if (_displayLTQ)
                                gsi.Text = _lastLTQ.ToString();
                            else
                                gsi.Text = "";
                        }
                    }

                    switch (_lastTradeDirection)
                    {
                        case LastTradedPriceDirection.NoChange:
                            gsi.BackColor = _lastTradedPriceNoChangeBackColor;
                            gsi.TextColor = System.Drawing.Color.Black;  // hack
                            break;
                        case LastTradedPriceDirection.TradedDown:
                            gsi.BackColor = _lastTradedPriceTradedDownBackColor;
                            break;
                        case LastTradedPriceDirection.TradedUp:
                            gsi.BackColor = _lastTradedPriceTradedUpBackColor;
                            break;
                    }
                }
            }
            finally
            {
                _grid.IgnoreReadOnly = false;
                _grid.EndUpdate(false);
            }
        }
        private void OnTimer(object sender, EventArgs e)
        {
            // handle pending depths
            _depthUpdateTimer.Enabled = false;
            RenderDepth();
        }
        private void OnGridRowInfoUpdatedHandler()
        {
            //_decimalCellModel.PlacesAfterDecimal = CountMaxDecimals();

            if (!_bRowUpdatePending)
                throw new InvalidOperationException("The row update pending flag must be set to true.");

            OnGridRowInfoUpdatedForStaticNumberOfRows();
        }
        /// <summary>
        /// Loop through the row labels and find the maximum number of characters on the
        /// right hand side of a decimal point
        /// </summary>
        /// <returns></returns>
        private int CountMaxDecimals()
        {
            int max = 0;

            foreach (string s in _rowLabels)
            {
                int pos = s.IndexOf('.');

                if (pos >= 0)
                    max = Math.Max(max, s.Length - pos - 1);
            }

            return max;
        }
        private void SafeInvoke_Recenter()
        {
            if (Recenter != null)
                Recenter();
        }
        private void OnGridRowInfoUpdatedForStaticNumberOfRows()
        {
            if (_rowLabels == null || _rowTags == null)
                return;

            try
            {
                _grid.IgnoreReadOnly = true;
                _grid.BeginUpdate(Syncfusion.Windows.Forms.BeginUpdateOptions.Invalidate);

                HighlightRowTag = -1;

                _grid.ClearCells(GridRangeInfo.Cols(BuyColumnPosition, BuyColumnPosition), true);
                _grid.ChangeCells(GridRangeInfo.Col(BuyColumnPosition), _buyColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

                _grid.ClearCells(GridRangeInfo.Cols(SellColumnPosition, SellColumnPosition), true);
                _grid.ChangeCells(GridRangeInfo.Col(SellColumnPosition), _sellColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

                _grid.ClearCells(GridRangeInfo.Cols(PriceColumnPosition, PriceColumnPosition), true);
                _grid.ChangeCells(GridRangeInfo.Col(PriceColumnPosition), _priceColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

                //The row count does not include headers, per Grid docs.  It will yield
                // exactly the set number of rows set, plus applicible header rows.
                _grid.RowCount = _rowTags.Length;

                // The height of the header, row 0, will remain fixed
                _grid.RowHeights.SetRange(0, 0, HeaderRowHeight);
                _rowOffset = -1;

                // Apply the row height (for row #1 ) to the grid.
                _grid.RowHeights.SetRange(1, _grid.RowCount, RowHeightForSpecifiedRowCount(_grid.RowCount));

                _tagToRowMap.Clear();
                for (int index = 0; index < _rowTags.Length; index++)
                {
                    _tagToRowMap.Add(_rowTags[index], index + 1);
                    _grid[index + 1, PriceColumnPosition].Text = _rowLabels[index];
                }
            }
            finally
            {
                _grid.IgnoreReadOnly = false;

                if (_pendingDepthUpdate == null)
                {
                    _pendingDepthUpdate = _lastDepthUpdate;
                }

                // Note to Self:  _depthUpdateTimer.Enabled = false should probably 
                // be added to RenderDepth().  
                _depthUpdateTimer.Enabled = false;
                _bRowUpdatePending = false;

                RenderDepth();
                RenderLastPrcQty();
                _grid.EndUpdate(false);
                //_grid.Update();
            }
        }
        private void ProcessRightBtnDown(int tag, GridCellMouseEventArgs e)
        {
            if (e.ColIndex == OrderColumnPosition && e.RowIndex > 0)
            {
                if (_tagToWorkingExecutedString.ContainsKey(tag))
                {
                    _orderDragInProgressStyle = new GridStyleInfo(_grid[e.RowIndex, e.ColIndex]);
                    ModifyStyleForWorkingOrder(e.RowIndex, _orderDragInProgressStyle);

                    _bOrderDragInProgress = true;
                    _orderDragOrigTag = tag;

                    System.Drawing.Color lesserClr = Color.FromArgb(
                        128, _orderDragInProgressStyle.BackColor.R,
                             _orderDragInProgressStyle.BackColor.G,
                             _orderDragInProgressStyle.BackColor.B);

                    _orderDragInProgressStyle.BackColor = lesserClr;
                    _orderDragInProgressStyle.CellAppearance = GridCellAppearance.Sunken;

                    _grid.InvalidateRange(GridRangeInfo.Cell(e.RowIndex, e.ColIndex));
                }
            }
            else if (e.ColIndex == PriceColumnPosition && e.RowIndex > 0)
            {                
                PriceColumnClicked(this, e.MouseEventArgs);
            }
            else if (e.ColIndex == BuyColumnPosition && e.RowIndex > 0)
            {
                RaiseSendOrder(tag, true);
            }
            else if (e.ColIndex == SellColumnPosition && e.RowIndex > 0)
            {
                RaiseSendOrder(tag, false);
            }
        }
        private void RaiseSendOrder(int tag, bool bVal)
        {
            if (SendOrder != null)
            {
                SendOrder(tag, bVal);
            }
        }
        private void ProcessLeftBtnDown(int tag, int col)
        {
            if (col == BuyColumnPosition)
            {
                RaiseSendOrder(tag, true);
            }
            else if (col == SellColumnPosition)
            {
                RaiseSendOrder(tag, false);
            }
            else if (col == PriceColumnPosition)
            {
                if (ToggleMarker != null)
                    ToggleMarker(tag);
            }
            else if (col == OrderColumnPosition)
            {
                if (DeleteOrdersAtTagLevel != null)
                    DeleteOrdersAtTagLevel(tag);
            }
        }

        private void ModifyCellForMonoMode(GridPrepareViewStyleInfoEventArgs e)
        {
            //int gamma = System.Math.Min( System.Math.Min( e.Style.BackColor.R, e.Style.BackColor.G), e.Style.BackColor.B );
            //e.Style.BackColor = Color.FromArgb(gamma,gamma,gamma);
            Color f = Color.Black;
            Color b = Color.White;

            if (e.ColIndex == BuyColumnPosition)
            {
                f = Color.Black;
                b = Color.White;
            }
            else if (e.ColIndex == SellColumnPosition)
            {
                f = Color.Black;
                b = Color.LightGray;
            }
            else if (e.ColIndex == PriceColumnPosition)
            {
                if (RowFromTag(_lastLTP) != e.RowIndex)
                {
                    f = Color.White;
                    b = Color.Black;
                }
                else
                {
                    f = Color.White;
                    b = Color.Gray;
                }
            }
            else if (e.ColIndex == OrderColumnPosition)
            {
                f = Color.Black;
                b = Color.White;
            }

            e.Style.BackColor = b;
            e.Style.TextColor = f;
        }
        private void ModifyStyleForWorkingOrder(int row, GridStyleInfo style)
        {
            int tag = TagFromRow(row);
            if (_tagToWorkingExecutedString.ContainsKey(tag))
            {
                KeyValuePair<string, bool> pair = _tagToWorkingExecutedString[tag];
                style.Text = pair.Key;
                bool bIsBuy = pair.Value;

                if (tag != _orderDragOrigTag)
                {
                    if (bIsBuy)
                        style.BackColor = WorkingOrderBuyBackColor;
                    else
                        style.BackColor = WorkingOrderSellBackColor;
                }
                else
                {
                    if (bIsBuy)
                        style.BackColor = WorkingOrderDragBuyBackColor;
                    else
                        style.BackColor = WorkingOrderDragSellBackColor;
                }

                style.CellAppearance = GridCellAppearance.Raised;
            }
        }
        private void ModifyStyleForAveragePrice(GridStyleInfo style)
        {
            if (_averagePriceIsBuy)
                style.BackColor = WorkingOrderBuyBackColor;
            else
                style.BackColor = WorkingOrderSellBackColor;
        }
        /// <summary>
        /// Provide a best fit column width based upon the cell text in the entire column specified.
        /// </summary>
        /// <param name="column">The Int32 that represents the column to resize.</param>
        private void AdjustColumnWidth(Int32 column)
        {
            float width = 0;
            Int32 rowCount = _grid.RowCount;
            SizeF size;

            using (Graphics g = Graphics.FromHwnd(_grid.Handle))
            {
                // If the order column is being adjusted then this is a special case as the text is not stored in the
                // grid but in a dictionary.
                if (column == OrderColumnPosition)
                {
                    foreach (KeyValuePair<String, Boolean> kvp in _tagToWorkingExecutedString.Values)
                    {
                        // This is an optimization to prevent a call to measure the string if there is actually no text.
                        if (kvp.Key.Length == 0)
                            continue;

                        size = g.MeasureString(ColumnWidthPadding + kvp.Key, base.Font);

                        if (size.Width > width)
                            width = size.Width;
                    }
                }
                else
                {
                    // Find the width of the largest string in the current column.
                    for (int rowIndex = 1; rowIndex < rowCount; rowIndex++)
                    {
                        GridStyleInfo gsi = _grid[rowIndex, column];

                        // This is an optimization to prevent a call to measure the string if there is actually no text.
                        if (gsi.Text.Length == 0)
                            continue;

                        size = g.MeasureString(ColumnWidthPadding + gsi.Text, base.Font);

                        if (size.Width > width)
                            width = size.Width;
                    }
                }
            }

            _grid.ColWidths[column] = Convert.ToInt32(width);
        }

        /// <summary>
        /// Propagate the font change to all of the relevant style and cell model objects.
        /// </summary>
        private void PropagateFontChange()
        {
            _grid.IgnoreReadOnly = true;
            _grid.BeginUpdate(BeginUpdateOptions.Invalidate);

            _buyColStyle.Font.Facename = base.Font.Name;
            _buyColStyle.Font.Bold = base.Font.Bold;
            _buyColStyle.Font.Size = base.Font.Size;

            _buyColStyleDisabled.Font.Facename = base.Font.Name;
            _buyColStyleDisabled.Font.Bold = Font.Bold;
            _buyColStyleDisabled.Font.Size = Font.Size;

            _grid.ChangeCells(GridRangeInfo.Col(BuyColumnPosition), _buyColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

            _sellColStyle.Font.Facename = base.Font.Name;
            _sellColStyle.Font.Bold = base.Font.Bold;
            _sellColStyle.Font.Size = base.Font.Size;

            _sellColStyleDisabled.Font.Facename = base.Font.Name;
            _sellColStyleDisabled.Font.Bold = base.Font.Bold;
            _sellColStyleDisabled.Font.Size = base.Font.Size;

            _grid.ChangeCells(GridRangeInfo.Col(SellColumnPosition), _sellColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

            _priceColStyle.Font.Facename = base.Font.Name;
            _priceColStyle.Font.Bold = base.Font.Bold;
            _priceColStyle.Font.Size = base.Font.Size;

            _priceColStyleDisabled.Font.Facename = base.Font.Name;
            _priceColStyleDisabled.Font.Bold = base.Font.Bold;
            _priceColStyleDisabled.Font.Size = base.Font.Size;

            _grid.ChangeCells(GridRangeInfo.Col(PriceColumnPosition), _priceColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

            _orderColStyle.Font.Facename = base.Font.Name;
            _orderColStyle.Font.Bold = base.Font.Bold;
            _orderColStyle.Font.Size = base.Font.Size;

            _orderColStyleDisabled.Font.Facename = base.Font.Name;
            _orderColStyleDisabled.Font.Bold = base.Font.Bold;
            _orderColStyleDisabled.Font.Size = base.Font.Size;

            _grid.ChangeCells(GridRangeInfo.Col(OrderColumnPosition), _orderColStyleCurrent, Syncfusion.Styles.StyleModifyType.Copy);

            _grid.EndUpdate(true);

            // probably not the right thing to do, but i know my particular client and it will reset itself properly
            // on the recenter event, so i'm just throwing this here to safe time... a font changed event may be better
            SafeInvoke_Recenter();
        }
        private void _properties_FontChanged(object sender, EventArgs e)
        {
            base.Font = _properties.Font;
            PropagateFontChange();
        }

        private void MDTraderControl_Load(object sender, EventArgs e)
        {
        }
        private void MDTraderControl_Resize(object sender, EventArgs e)
        {

        }
        
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmPropertyGridDisplay props = new frmPropertyGridDisplay("MD Trader Properties"))
            {
                props.SetPropertyGridObject(this._properties);
                props.ShowDialog(this);
            }
        }

        private void _grid_GridControlMouseUp(object sender, CancelMouseEventArgs e)
        {
            if (_bOrderDragInProgress || e.MouseEventArgs.Button != MouseButtons.Right) 
                return;

            int clickContext = _grid.HitTest(e.MouseEventArgs.Location);   
            // simply based on experimentation, if the clickContext is 
            // 0, then no row/column was clicked, if the clickContext is
            // 1, then a row/column was clicked
            if (clickContext == 0)
            {
                e.Cancel = true;
                contextMenuStripMain.Show(this, e.MouseEventArgs.Location);
            }
        }
        private void _grid_Click(object sender, EventArgs e)
        {
        }
        private void _grid_CellMouseDown(object sender, GridCellMouseEventArgs e)
        {
            // Check for the header row
            if (e.RowIndex == 0)
                return;
            
            int tag = TagFromRow(e.RowIndex);
            switch (e.MouseEventArgs.Button)
            {
                // LEFT button
                case MouseButtons.Left:
                    ProcessLeftBtnDown(tag, e.ColIndex);
                    break;

                case MouseButtons.Right:
                    ProcessRightBtnDown(tag, e);
                    break;

                // MIDDLE button
                case MouseButtons.Middle:
                    SafeInvoke_Recenter();
                    break;
            }
        }

        private void _grid_CellHitTest(object sender, GridCellHitTestEventArgs e)
        {
            // NOTE:  If you are doing 'regular' header selection for 
            // drag and drop you will want to add some code like this, else
            // your drag and drop won't work.  ( Since we handle it differently
            // this isn't applicable to us currently. )
            /* 
             *           if (  e.RowIndex == 0 || e.ColIndex == 0 )
             *              e.Result = 0; // We vote not to handle this test
             *           else
             *              e.Result = 1;
             * */

            /////////////////////////////////////////////////////////////////////////
            // http://syncfusion.com/Support/Forums/message.aspx?MessageID=21069
            //   "To get the CellMouseDown/CellMouseUp events to be raised, you need
            //    to handle the CellHitTest event, and return a non-zero result when
            //    the mouse is over a cell where you want to get these events."
            /////////////////////////////////////////////////////////////////////////
            e.Result = 1;
        }
        private void _grid_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mea = e as MouseEventArgs;
            if (mea == null) return;

            int rowIdx, colIdx;
            
            _grid.PointToRowCol(mea.Location, out rowIdx, out colIdx);

            if (colIdx == PriceColumnPosition)
                SafeInvoke_Recenter();
            
        }
        private void _grid_CellMouseUp(object sender, GridCellMouseEventArgs e)
        {
            if (_bOrderDragInProgress)
            {
                int rowIdx;
                int colIdx;
                _grid.PointToRowCol(new System.Drawing.Point(e.MouseEventArgs.X, e.MouseEventArgs.Y),
                        out rowIdx, out colIdx);

                if (rowIdx != -1 && colIdx != -1)
                {
                    _orderDragNewTag = TagFromRow(rowIdx);
                    if (ChangeOrdersAtLevel != null
                        && colIdx == OrderColumnPosition
                        && _orderDragOrigTag != _orderDragNewTag
                        && _orderDragOrigTag != Int32.MinValue
                        && _orderDragNewTag != Int32.MinValue)
                    {

                        ChangeOrdersAtLevel(_orderDragOrigTag, _orderDragNewTag);
                    }
                }
                _grid.InvalidateRange(GridRangeInfo.Cell(RowFromTag(_orderDragOrigTag), OrderColumnPosition));
                _grid.InvalidateRange(GridRangeInfo.Cell(_currentMouseRowIdx, OrderColumnPosition));

                _bOrderDragInProgress = false;
                _orderDragInProgressStyle = null;
                _orderDragOrigTag = Int32.MinValue;
                _orderDragNewTag = Int32.MinValue;
            }
        }
        private void _grid_CellMouseHoverEnter(object sender, GridCellMouseEventArgs e)
        {
            if (_rowTags == null)
                return;

            #region WORKAROUND for SyncFusion HoverEnterBug
            /// NOTE:  There seems to be a bug in Syncfusion's grid whereby we will
            ///        get this HoverEnter callback 2X when we enter the cell when
            ///        the mouse is coming from outside the grid.

            if (_lastHoverRow == e.RowIndex && _lastHoverCol == e.ColIndex)
                return;

            _lastHoverRow = e.RowIndex;
            _lastHoverCol = e.ColIndex;
            #endregion

            if (e.RowIndex == 0)
            {
                if (!_grid.IsMousePressed)
                {
                    _grid.RaiseCellClick(e.RowIndex, e.ColIndex, null);
                    _grid.Selections.Clear();
                    _grid.Selections.Add(GridRangeInfo.Col(e.ColIndex));
                }
            }
            else
            {
                _grid.Selections.Clear();
            }

            _currentHoverCol = e.ColIndex;
            if (_currentHoverCol == PriceColumnPosition)
            {
                HighlightRowTag = TagFromRow(e.RowIndex);
            }
            else
            {
                HighlightRowTag = -1;
            }
        }
        private void _grid_CellMouseHoverLeave(object sender, GridCellMouseEventArgs e)
        {
            #region WORKAROUND for SyncFusion HoverEnterBug
            _lastHoverRow = Int32.MinValue;
            _lastHoverCol = Int32.MinValue;
            #endregion
        }
        private void _grid_MouseLeave(object sender, EventArgs e)
        {
            #region WORKAROUND for SyncFusion HoverEnterBug
            _lastHoverRow = Int32.MinValue;
            _lastHoverCol = Int32.MinValue;
            #endregion

            HighlightRowTag = -1;
            _grid.Selections.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Remarks from Syncfusion docs:
        /// GridPrepareViewStyleInfoEventArgs is a custom event argument class used by the PrepareViewStyleInfo 
        /// event to allow custom formatting of a cell by changing its style object. Set the Cancel property true 
        /// if you want to avoid calling the associated cell renderer's object OnPrepareViewStyleInfo method.
        /// 
        /// Changes made to the style object will not be saved in the grid nor cached. This event is called 
        /// every time a portion of the grid is repainted and the specified cell belongs to the invalidated 
        /// region of the window that needs to be redrawn.
        /// 
        /// Changes to the style object done at this time will also not be reflected when accessing cells 
        /// though the models indexer. See QueryCellInfo.
        /// </remarks>
        private void _grid_PrepareViewStyleInfo(object sender, GridPrepareViewStyleInfoEventArgs e)
        {
            if (e.RowIndex == 0)
                return;

            if (e.ColIndex == OrderColumnPosition)
            {
                ModifyStyleForWorkingOrder(e.RowIndex, e.Style);

                if (_bOrderDragInProgress && e.RowIndex == _currentMouseRowIdx)
                {
                    e.Style.ModifyStyle(_orderDragInProgressStyle, Syncfusion.Styles.StyleModifyType.Copy);
                }
            }
            else if (e.ColIndex == PriceColumnPosition)
            {
                // If the Average Price of Open Position is not the same as the 
                // LTP we need to render it's background to indicate that price.
                if (_averagePrice != _lastLTP
                    && e.RowIndex == RowFromTag(_averagePrice))
                {
                    ModifyStyleForAveragePrice(e.Style);
                    e.Style.FloatCell = true;
                }
            }

            // If it's the highlighted row, then border it and lower the colors
            // as long as the user has the mouse hovering in the price column.
            // We don't hightlight for other columns.
            if (IsHighlightRowActive() && _currentHoverCol == PriceColumnPosition)
            {
                int row = RowFromTag(HighlightRowTag);

                if (row != -1 && e.RowIndex == row)
                {
                    // LEFT column, no matter what it is         
                    if (e.ColIndex == 1)
                        e.Style.Borders.Left = _whiteBorder;

                    // RIGHT column, no matter what it is
                    if (e.ColIndex == 4)
                        e.Style.Borders.Right = _blackBorder;

                    e.Style.Borders.Top = _whiteBorder;
                    e.Style.Borders.Bottom = _blackBorder;

                    // Calculate a suitable background color to highlight the cell
                    e.Style.BackColor = Color.FromArgb(3 * e.Style.BackColor.R / 4,
                                                        3 * e.Style.BackColor.G / 4,
                                                        3 * e.Style.BackColor.B / 4);
                }
            }

            // Finally, wipe the colors if need be
            if (ForceMonoRendering || MonoRendering)
                ModifyCellForMonoMode(e);
        }
        private void _grid_ColsMoved(object sender, GridRangeMovedEventArgs e)
        {
            System.Diagnostics.Debug.Assert(_grid.ColCount == 4);
            System.Diagnostics.Debug.Assert(_grid.RowCount >= 1);

            for (int i = 1; i <= 4; i++)
            {
                // Grab tag from 1st no header row.  All cells
                // in non-header rows have an identifying tag.  After
                // the re-arrange the grid will have the rows in the new
                // order and we'll grab the tags and re-assign the
                // members that track the columns.
                string tag = _grid[1, i].Tag as string;
                switch (tag)
                {
                    case "_buyColStyle":
                        BuyColumnPosition = i;
                        break;
                    case "_sellColStyle":
                        SellColumnPosition = i;
                        break;
                    case "_priceColStyle":
                        PriceColumnPosition = i;
                        break;
                    case "_orderColStyle":
                        OrderColumnPosition = i;
                        break;
                }
            }

            // After the user has completed the DnD of columns, we
            // will un-select the columns so that everything looks 
            // clean.  Otherwise, we'd be left with highlighted ( and inverted )
            // columns.
            _grid.Selections.Clear();
            _grid.InvalidateRange(GridRangeInfo.Cols(1, 4));
            _grid.Update();

        }
        private void _grid_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ShiftScroll != null)
                ShiftScroll(e.Delta / 120);
        }
        private void _grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (_bOrderDragInProgress)
            {
                int rowIndex, colIndex;
                _grid.PointToRowCol(new Point(e.X, e.Y), out rowIndex, out colIndex);

                // If the current cell has not actually changed, don't do anything
                if (_currentMouseRowIdx == rowIndex && colIndex == OrderColumnPosition)
                    return;

                // If the mouse went out of the order col, put the in-progress
                // order marker back to the original price level to indicate that
                // the user can't drop where his mous is.
                if (colIndex != OrderColumnPosition || rowIndex < 1)
                {
                    rowIndex = RowFromTag(_orderDragOrigTag);
                }

                _prevMouseRowIdx = _currentMouseRowIdx;
                _currentMouseRowIdx = rowIndex;

                _grid.InvalidateRange(GridRangeInfo.Cell(_prevMouseRowIdx, OrderColumnPosition));
                _grid.InvalidateRange(GridRangeInfo.Cell(_currentMouseRowIdx, OrderColumnPosition));
            }
        }
        /// <summary>
        /// Allow for adjustments when the columns are about to be resized.
        /// </summary>
        /// <param name="sender">The PerformanceGridControl object which contains the parameters.</param>
        /// <param name="e">The GridResizingColumnsEventArgs that provides the event data.</param>
        private void _grid_ResizingColumns(object sender, GridResizingColumnsEventArgs e)
        {
            if (e.Reason == GridResizeCellsReason.DoubleClick)
            {
                Int32 rowIndex = 0, colIndex = 0;
                _grid.PointToRowCol(e.Point, out rowIndex, out colIndex);

                // There are times when attemping to double-click the right most column header border that the column
                // index returned will be less than zero, i.e. -1. The grid thinks the mouse is outside the grid area 
                // so when this happens simply ignore the double-click.
                if (colIndex >= 0)
                    AdjustColumnWidth(colIndex);

                // Always cancel further processing when a double-click occurs, even if the column with was not adjusted.
                e.Cancel = true;
            }
        }
        #endregion

        [Serializable]
        internal class MDTraderControlProperties
        {
            [field:NonSerialized]
            public event EventHandler FontChanged;

            private Font _font;
            
            private int _buyColPosition;
            private int _priceColPosition;
            private int _sellColPosition;
            private int _orderColPosition;
            
            private int _buyColWidth;
            private int _sellColWidth;
            private int _orderColWidth;
            private int _priceColWidth;

            public MDTraderControlProperties()
            {
                _font = new Font(MDTraderControl.DefaultFontFacename, MDTraderControl.DefaultFontSize, FontStyle.Bold);
                _buyColPosition     = 1;
                _priceColPosition   = 2;
                _sellColPosition    = 3;
                _orderColPosition   = 4;

                _buyColWidth = 53;
                _sellColWidth = 53;
                _priceColWidth = 63;
                _orderColWidth = 57;
            }

            [Browsable(true)]
            public Font Font
            {
                [DebuggerStepThrough]
                get { return _font; }
                [DebuggerStepThrough]
                set { 
                    _font = value;
                    if (FontChanged != null) 
                        FontChanged(this, EventArgs.Empty);
                }
            }
            [Browsable(false)]
            public int BuyColPosition
            {
                [DebuggerStepThrough]
                get { return _buyColPosition; }
                [DebuggerStepThrough]
                set { _buyColPosition = value; }
            }
            [Browsable(false)]
            public int PriceColPosition
            {
                [DebuggerStepThrough]
                get { return _priceColPosition; }
                [DebuggerStepThrough]
                set { _priceColPosition = value; }
            }
            [Browsable(false)]
            public int SellColPosition
            {
                [DebuggerStepThrough]
                get { return _sellColPosition; }
                [DebuggerStepThrough]
                set { _sellColPosition = value; }
            }
            [Browsable(false)]
            public int OrderColPosition
            {
                [DebuggerStepThrough]
                get { return _orderColPosition; }
                [DebuggerStepThrough]
                set { _orderColPosition = value; }
            }
            [Browsable(false)]
            public int BuyColWidth
            {
                get { return _buyColWidth; }
                set { _buyColWidth = value; }
            }
            [Browsable(false)]
            public int SellColWidth
            {
                get { return _sellColWidth; }
                set { _sellColWidth = value; }
            }
            [Browsable(false)]
            public int PriceColWidth
            {
                get { return _priceColWidth; }
                set { _priceColWidth = value; }
            }
            [Browsable(false)]
            public int OrderColWidth
            {
                get { return _orderColWidth; }
                set { _orderColWidth = value; }
            }
        }
    }
    #endregion

    #region SUPPORT TYPES

    #region DefaultGridPushButtonCellModel
    [Serializable]
    public class DefaultGridPushButtonCellModel : GridPushButtonCellModel
    {
        #region PRIVATE MEMBERS
        private Int32 _columnIndex;
        private String _cellType;
        private Boolean _enabled;
        private DefaultCellRenderer _renderer;
        private Boolean _disposing;
        #endregion

        #region CTORS
        public DefaultGridPushButtonCellModel(GridModel grid, Int32 columnIndex, String cellType)
            : base(grid)
        {
            if (grid == null)
                throw new ArgumentNullException("grid");

            _columnIndex = columnIndex;
            _cellType = cellType;
            grid[1, _columnIndex].CellType = _cellType;
        }
        #endregion

        #region PROPERTIES
        protected DefaultCellRenderer Renderer
        {
            get { return _renderer; }
            set
            {
                if (value == null)
                    return;

                _renderer = value;
            }
        }
        public Boolean Enabled
        {
            get
            {
                if (_renderer != null)
                    return _renderer.Enabled;
                else
                    return _enabled;
            }
            set
            {
                _enabled = value;

                if (_renderer != null)
                    _renderer.Enabled = _enabled;
            }
        }
        public Font Font
        {
            get { return _renderer.Font; }
            set
            {
                if (_renderer != null)
                    _renderer.Font = value;
            }
        }
        #endregion

        #region PUBLIC METHODS
        public SizeF GetButtonTextWidth(Graphics g)
        {
            return _renderer.GetButtonTextWidth(g);
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a serialization information object with the data needed to serialize the AddVolumeAtPriceCommand. 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        /// <remarks>
        /// Serializing an object exposes its private and protected data. You should apply the SerializationFormatter permission to 
        /// the GetObjectData method to prevent any non-public data from being available to objects that do not have permission to 
        /// serialize and deserialize objects.
        /// 
        /// The GetObjectData method is declared in the ISerializable interface. This interface is implemented by types that provide
        /// custom serialization logic. GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
        /// is protected by a security check for SerializationFormatter security permission. If an implementation of 
        /// GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) is not protected 
        /// by the same security check, callers can call the implementation to bypass security on the interface and gain access to data 
        /// serialized by the type.
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// At deserialization time, this private constructor is called only after the data in the 
        /// <paramref name="SerializationInfo"/> has been deserialized by the 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream 
        /// associated with the AddVolumeAtPriceCommand.</param>
        protected DefaultGridPushButtonCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

        #region IDISPOSABLE MEMBERS
        /// <summary>
        /// Design pattern implementation of the derived class version of Dipose(bool).
        /// </summary>
        /// <param name="disposing">The indication of which type of dipose scenario is executing.</param> 
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!_disposing)
                {
                    if (disposing)
                    {
                        if (_renderer != null)
                            _renderer.Dispose();
                    }

                    _disposing = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion
    }
    #endregion

    #region DefaultCellRenderer
    public class DefaultCellRenderer : GridPushButtonCellRenderer
    {
        #region PRIVATE MEMBERS
        private Boolean _enabled;
        private GridStyleInfo _gridStyleInfoEnabled;
        private GridStyleInfo _gridStyleInfoDisabled;
        private Font _font;
        private Boolean _disposing;
        #endregion

        #region CTORS
        public DefaultCellRenderer(GridCellModelBase cellModel, GridControlBase grid)
            : base(grid, cellModel)
        {
            AddButton(new GridCellButton(this));
        }
        #endregion

        #region PROPERTIES
        public static GridCellAppearance GridButtonCellAppearance
        {
            get { return GridCellAppearance.Flat; }
        }

        public static GridHorizontalAlignment GridButtonHorizontalAlignment
        {
            get { return GridHorizontalAlignment.Center; }
        }

        public static GridVerticalAlignment GridButtonVerticalAlignment
        {
            get { return GridVerticalAlignment.Middle; }
        }

        protected GridStyleInfo GridStyleInfoEnabled
        {
            get { return _gridStyleInfoEnabled; }
            set
            {
                _gridStyleInfoEnabled = new GridStyleInfo(value);

                // This private font object is created for the sole purpose of having a font object from which the measure
                // text can be used when finding the button text width. The other alternative is to create a new font object
                // each time a call is made to find the button text width which seems wasteful.
                //
                // It is the responsibility of the derived class constructor to set this grid style and also the disabled style.
                _font = new Font(_gridStyleInfoEnabled.Font.Facename, _gridStyleInfoEnabled.Font.Size, FontStyle.Bold);
            }
        }

        protected GridStyleInfo GridStyleInfoDisabled
        {
            get { return _gridStyleInfoDisabled; }
            set { _gridStyleInfoDisabled = new GridStyleInfo(value); }
        }

        public Boolean Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public Font Font
        {
            get
            {
                return new Font(_gridStyleInfoEnabled.Font.Facename,
                                _gridStyleInfoEnabled.Font.Size);//,
                                //_gridStyleInfoEnabled.Font.FontStyle);
            }
            set
            {
                if (value == null)
                    return;

                // Per ENH 46424, the font size for the buttons cannot
                // go below 8pt, and it must be 2pt smaller than the font
                // from the main grid.
                _gridStyleInfoEnabled.Font.Facename = value.Name;
                _gridStyleInfoEnabled.Font.Size = Math.Max(8F, value.Size-2F);

                _gridStyleInfoDisabled.Font.Facename = value.Name;
                _gridStyleInfoDisabled.Font.Size = Math.Max(8F, value.Size-2F);

                // This private font object is created for the sole purpose of having a font object from which the measure
                // text can be used when finding the button text width. The other alternative is to create a new font object
                // each time a call is made to find the button text width which seems wasteful.
                _font = new Font(_gridStyleInfoEnabled.Font.Facename, _gridStyleInfoEnabled.Font.Size, FontStyle.Bold);
            }
        }
        #endregion

        #region PUBLIC METHODS
        public override void OnPrepareViewStyleInfo(GridPrepareViewStyleInfoEventArgs e)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            if (_enabled)
            {
                e.Style.BackColor = _gridStyleInfoEnabled.BackColor;
                e.Style.CellAppearance = _gridStyleInfoEnabled.CellAppearance;
                e.Style.Description = _gridStyleInfoEnabled.Description;
                e.Style.Font.Bold = _gridStyleInfoEnabled.Font.Bold;
                e.Style.Font.Facename = _gridStyleInfoEnabled.Font.Facename;
                e.Style.Font.Size = _gridStyleInfoEnabled.Font.Size;
                e.Style.HorizontalAlignment = _gridStyleInfoEnabled.HorizontalAlignment;
                e.Style.Tag = _gridStyleInfoEnabled.Tag;
                e.Style.TextColor = _gridStyleInfoEnabled.TextColor;
                e.Style.VerticalAlignment = _gridStyleInfoEnabled.VerticalAlignment;
                e.Style.WrapText = _gridStyleInfoEnabled.WrapText;
            }
            else
            {
                e.Style.BackColor = _gridStyleInfoDisabled.BackColor;
                e.Style.CellAppearance = _gridStyleInfoDisabled.CellAppearance;
                e.Style.Description = _gridStyleInfoDisabled.Description;
                e.Style.Font.Bold = _gridStyleInfoDisabled.Font.Bold;
                e.Style.Font.Facename = _gridStyleInfoDisabled.Font.Facename;
                e.Style.Font.Size = _gridStyleInfoDisabled.Font.Size;
                e.Style.HorizontalAlignment = _gridStyleInfoDisabled.HorizontalAlignment;
                e.Style.Tag = _gridStyleInfoDisabled.Tag;
                e.Style.TextColor = _gridStyleInfoDisabled.TextColor;
                e.Style.VerticalAlignment = _gridStyleInfoDisabled.VerticalAlignment;
                e.Style.WrapText = _gridStyleInfoDisabled.WrapText;
            }
        }

        public SizeF GetButtonTextWidth(Graphics g)
        {
            if (_font == null)
                throw new InvalidOperationException("Any class that derives from this class must call the GridStyleInfoEnabled " +
                                                    "and GridStyleInfoDisabled properties making sure that the font for the grid " + 
                                                    "styles is set properly.");

            return g.MeasureString(_gridStyleInfoEnabled.Description + MDTraderControl.ColumnWidthPadding, _font);
        }
        #endregion

        #region IDISPOSABLE MEMBERS
        /// <summary>
        /// Design pattern implementation of the derived class version of Dipose(bool).
        /// </summary>
        /// <param name="disposing">The indication of which type of dipose scenario is executing.</param> 
        /// <remarks>
        /// This methods gets called in a rather roundabout way. Because the renderer is a property that gets set in each
        /// class that inherits from this class, when the MDTraderControl dispose method calls dispose on the performance
        /// grid a series of events happen that lead to this method getting called. The series of method calls is (reading 
        /// the call flow from the bottom to the top):
        /// 
        ///         MDTrader.DefaultCellRenderer.Dispose(bool disposing = true)
        ///         Syncfusion.Windows.Forms.Grid.GridCellRendererBase.Dispose()
        ///         Syncfusion.Windows.Forms.Grid.GridCellRendererCollection.Clear()
        ///         Syncfusion.Windows.Forms.Grid.GridCellRendererCollection.Dispose(bool disposing = true)
        ///         Syncfusion.ComponentModel.NonFinalizeDisposable.Dispose()
        ///         Syncfusion.Windows.Forms.Grid.GridControlBase.Dispose(bool disposing = true)
        ///         Syncfusion.Windows.Forms.Grid.GridControlBaseImp.Dispose(bool disposing = true)
        ///         MDTrader.PerformanceGridControl.Dispose(bool disposing = true)
        /// 
        /// </remarks>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!_disposing)
                {
                    if (disposing)
                    {
                        _gridStyleInfoEnabled.Dispose();
                        _gridStyleInfoDisabled.Dispose();
                        _font.Dispose();
                    }

                    _disposing = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion
    }
    #endregion

    #region DeleteBuysCellRenderer
    public class DeleteBuysCellRenderer : DefaultCellRenderer
    {
        #region CTOR
        public DeleteBuysCellRenderer(GridCellModelBase cellModel, GridControlBase grid)
            : base(cellModel, grid)
        {
            GridStyleInfo gridStyleInfoEnabled = new GridStyleInfo();
            gridStyleInfoEnabled.BackColor = DeleteBuysCellModel.GridButtonEnabledBackColor;
            gridStyleInfoEnabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoEnabled.Description = DeleteBuysCellModel.GridButtonText;
            gridStyleInfoEnabled.Font.Bold = true;
            gridStyleInfoEnabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoEnabled.Font.Size = Math.Max(8,MDTraderControl.DefaultFontSize-2);
            gridStyleInfoEnabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoEnabled.Tag = DeleteBuysCellModel.GridButtonCellModelTag;
            gridStyleInfoEnabled.TextColor = DeleteBuysCellModel.GridButtonEnabledTextColor;
            gridStyleInfoEnabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoEnabled.WrapText = true;

            base.GridStyleInfoEnabled = gridStyleInfoEnabled;

            GridStyleInfo gridStyleInfoDisabled = new GridStyleInfo();
            gridStyleInfoDisabled.BackColor = DeleteBuysCellModel.GridButtonDisabledBackColor;
            gridStyleInfoDisabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoDisabled.Description = DeleteBuysCellModel.GridButtonText;
            gridStyleInfoDisabled.Font.Bold = false;
            gridStyleInfoDisabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoDisabled.Font.Size = Math.Max(8,MDTraderControl.DefaultFontSize-2);
            gridStyleInfoDisabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoDisabled.Tag = DeleteBuysCellModel.GridButtonCellModelTag;
            gridStyleInfoDisabled.TextColor = DeleteBuysCellModel.GridButtonDisabledTextColor;
            gridStyleInfoDisabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoDisabled.WrapText = true;

            base.GridStyleInfoDisabled = gridStyleInfoDisabled;
        }
        #endregion
    }
    #endregion

    #region DeleteBuysCellModel
    [Serializable]
    public class DeleteBuysCellModel : DefaultGridPushButtonCellModel
    {
        #region CTORS
        public DeleteBuysCellModel(GridModel grid, Int32 columnIndex, String cellType)
            : base(grid, columnIndex, cellType)
        {
        }
        #endregion

        #region PROPERTIES
        public static Color GridButtonEnabledBackColor
        {
            get { return Color.SteelBlue; }
        }

        public static Color GridButtonEnabledTextColor
        {
            get { return Color.White; }
        }

        public static Color GridButtonDisabledBackColor
        {
            get { return SystemColors.Control; }
        }

        public static Color GridButtonDisabledTextColor
        {
            get { return Color.DarkGray; }
        }

        public static String GridButtonCellModelTag
        {
            get { return "DeleteBuysCellModel"; }
        }

        public static String GridButtonCellType
        {
            get { return "DeleteBuys"; }
        }

        public static String GridButtonText
        {
            get { return "X Buys"; }
        }
        #endregion

        #region PUBLIC METHODS
        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            base.Renderer = new DeleteBuysCellRenderer(this, control);
            base.Renderer.Enabled = this.Enabled;
            return base.Renderer;
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a serialization information object with the data needed to serialize the AddVolumeAtPriceCommand. 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        /// <remarks>
        /// Serializing an object exposes its private and protected data. You should apply the SerializationFormatter permission to 
        /// the GetObjectData method to prevent any non-public data from being available to objects that do not have permission to 
        /// serialize and deserialize objects.
        /// 
        /// The GetObjectData method is declared in the ISerializable interface. This interface is implemented by types that provide
        /// custom serialization logic. GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
        /// is protected by a security check for SerializationFormatter security permission. If an implementation of 
        /// GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) is not protected 
        /// by the same security check, callers can call the implementation to bypass security on the interface and gain access to data 
        /// serialized by the type.
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// At deserialization time, this private constructor is called only after the data in the <paramref name="SerializationInfo"/> has been deserialized by the 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        protected DeleteBuysCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
    #endregion

    #region DeleteSellsCellRenderer
    public class DeleteSellsCellRenderer : DefaultCellRenderer
    {
        #region CTOR
        public DeleteSellsCellRenderer(GridCellModelBase cellModel, GridControlBase grid)
            : base(cellModel, grid)
        {
            GridStyleInfo gridStyleInfoEnabled = new GridStyleInfo();
            gridStyleInfoEnabled.BackColor = DeleteSellsCellModel.GridButtonEnabledBackColor;
            gridStyleInfoEnabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoEnabled.Description = DeleteSellsCellModel.GridButtonText;
            gridStyleInfoEnabled.Font.Bold = true;
            gridStyleInfoEnabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoEnabled.Font.Size = Math.Max(8, MDTraderControl.DefaultFontSize - 2);
            gridStyleInfoEnabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoEnabled.Tag = DeleteSellsCellModel.GridButtonCellModelTag;
            gridStyleInfoEnabled.TextColor = DeleteSellsCellModel.GridButtonEnabledTextColor;
            gridStyleInfoEnabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoEnabled.WrapText = true;

            base.GridStyleInfoEnabled = gridStyleInfoEnabled;

            GridStyleInfo gridStyleInfoDisabled = new GridStyleInfo();
            gridStyleInfoDisabled.BackColor = DeleteSellsCellModel.GridButtonDisabledBackColor;
            gridStyleInfoDisabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoDisabled.Description = DeleteSellsCellModel.GridButtonText;
            gridStyleInfoDisabled.Font.Bold = false;
            gridStyleInfoDisabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoDisabled.Font.Size = Math.Max(8, MDTraderControl.DefaultFontSize - 2);
            gridStyleInfoDisabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoDisabled.Tag = DeleteSellsCellModel.GridButtonCellModelTag;
            gridStyleInfoDisabled.TextColor = DeleteSellsCellModel.GridButtonDisabledTextColor;
            gridStyleInfoDisabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoDisabled.WrapText = true;

            base.GridStyleInfoDisabled = gridStyleInfoDisabled;
        }
        #endregion
    }
    #endregion

    #region DeleteSellsCellModel
    [Serializable]
    public class DeleteSellsCellModel : DefaultGridPushButtonCellModel
    {
        #region CTORS
        public DeleteSellsCellModel(GridModel grid, Int32 columnIndex, String cellType)
            : base(grid, columnIndex, cellType)
        {
        }
        #endregion

        #region PROPERTIES
        public static Color GridButtonEnabledBackColor
        {
            get { return Color.FromArgb(192, 12, 24); }
        }

        public static Color GridButtonEnabledTextColor
        {
            get { return Color.White; }
        }

        public static Color GridButtonDisabledBackColor
        {
            get { return SystemColors.Control; }
        }

        public static Color GridButtonDisabledTextColor
        {
            get { return Color.DarkGray; }
        }

        public static String GridButtonCellModelTag
        {
            get { return "DeleteSellsCellModel"; }
        }

        public static String GridButtonCellType
        {
            get { return "DeleteSells"; }
        }

        public static String GridButtonText
        {
            get { return "X Sells"; }
        }
        #endregion

        #region PUBLIC METHODS
        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            base.Renderer = new DeleteSellsCellRenderer(this, control);
            base.Renderer.Enabled = this.Enabled;
            return base.Renderer;
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a serialization information object with the data needed to serialize the AddVolumeAtPriceCommand. 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        /// <remarks>
        /// Serializing an object exposes its private and protected data. You should apply the SerializationFormatter permission to 
        /// the GetObjectData method to prevent any non-public data from being available to objects that do not have permission to 
        /// serialize and deserialize objects.
        /// 
        /// The GetObjectData method is declared in the ISerializable interface. This interface is implemented by types that provide
        /// custom serialization logic. GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
        /// is protected by a security check for SerializationFormatter security permission. If an implementation of 
        /// GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) is not protected 
        /// by the same security check, callers can call the implementation to bypass security on the interface and gain access to data 
        /// serialized by the type.
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// At deserialization time, this private constructor is called only after the data in the <paramref name="SerializationInfo"/> has been deserialized by the 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        protected DeleteSellsCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
    #endregion

    #region DeleteAllCellRenderer
    public class DeleteAllCellRenderer : DefaultCellRenderer
    {
        #region CTOR
        public DeleteAllCellRenderer(GridCellModelBase cellModel, GridControlBase grid)
            : base(cellModel, grid)
        {
            GridStyleInfo gridStyleInfoEnabled = new GridStyleInfo();
            gridStyleInfoEnabled.BackColor = DeleteAllCellModel.GridButtonEnabledBackColor;
            gridStyleInfoEnabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoEnabled.Description = DeleteAllCellModel.GridButtonText;
            gridStyleInfoEnabled.Font.Bold = true;
            gridStyleInfoEnabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoEnabled.Font.Size = Math.Max(8, MDTraderControl.DefaultFontSize - 2);
            gridStyleInfoEnabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoEnabled.Tag = DeleteAllCellModel.GridButtonCellModelTag;
            gridStyleInfoEnabled.TextColor = DeleteAllCellModel.GridButtonEnabledTextColor;
            gridStyleInfoEnabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoEnabled.WrapText = true;

            base.GridStyleInfoEnabled = gridStyleInfoEnabled;

            GridStyleInfo gridStyleInfoDisabled = new GridStyleInfo();
            gridStyleInfoDisabled.BackColor = DeleteAllCellModel.GridButtonDisabledBackColor;
            gridStyleInfoDisabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoDisabled.Description = DeleteAllCellModel.GridButtonText;
            gridStyleInfoDisabled.Font.Bold = false;
            gridStyleInfoDisabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoDisabled.Font.Size = Math.Max(8, MDTraderControl.DefaultFontSize - 2);
            gridStyleInfoDisabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoDisabled.Tag = DeleteAllCellModel.GridButtonCellModelTag;
            gridStyleInfoDisabled.TextColor = DeleteAllCellModel.GridButtonDisabledTextColor;
            gridStyleInfoDisabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoDisabled.WrapText = true;

            base.GridStyleInfoDisabled = gridStyleInfoDisabled;
        }
        #endregion
    }
    #endregion

    #region DeleteAllCellModel
    [Serializable]
    public class DeleteAllCellModel : DefaultGridPushButtonCellModel
    {
        #region CTORS
        public DeleteAllCellModel(GridModel grid, Int32 columnIndex, String cellType)
            : base(grid, columnIndex, cellType)
        {
        }
        #endregion

        #region PROPERTIES
        public static Color GridButtonEnabledBackColor
        {
            get { return Color.FromArgb(255, 140, 0); }
        }

        public static Color GridButtonEnabledTextColor
        {
            get { return Color.Black; }
        }

        public static Color GridButtonDisabledBackColor
        {
            get { return SystemColors.Control; }
        }

        public static Color GridButtonDisabledTextColor
        {
            get { return Color.DarkGray; }
        }

        public static String GridButtonCellModelTag
        {
            get { return "DeleteAllCellModel"; }
        }

        public static String GridButtonCellType
        {
            get { return "DeleteAll"; }
        }

        public static String GridButtonText
        {
            get { return "X  All"; }
        }
        #endregion

        #region PUBLIC METHODS
        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            base.Renderer = new DeleteAllCellRenderer(this, control);
            base.Renderer.Enabled = this.Enabled;
            return base.Renderer;
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a serialization information object with the data needed to serialize the AddVolumeAtPriceCommand. 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        /// <remarks>
        /// Serializing an object exposes its private and protected data. You should apply the SerializationFormatter permission to 
        /// the GetObjectData method to prevent any non-public data from being available to objects that do not have permission to 
        /// serialize and deserialize objects.
        /// 
        /// The GetObjectData method is declared in the ISerializable interface. This interface is implemented by types that provide
        /// custom serialization logic. GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
        /// is protected by a security check for SerializationFormatter security permission. If an implementation of 
        /// GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) is not protected 
        /// by the same security check, callers can call the implementation to bypass security on the interface and gain access to data 
        /// serialized by the type.
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// At deserialization time, this private constructor is called only after the data in the <paramref name="SerializationInfo"/> has been deserialized by the 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        protected DeleteAllCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
    #endregion

    #region TradeOutCellRenderer
    public class TradeOutCellRenderer : DefaultCellRenderer
    {
        #region CTOR
        public TradeOutCellRenderer(GridCellModelBase cellModel, GridControlBase grid)
            : base(cellModel, grid)
        {
            GridStyleInfo gridStyleInfoEnabled = new GridStyleInfo();
            gridStyleInfoEnabled.BackColor = TradeOutCellModel.GridButtonEnabledBackColor;
            gridStyleInfoEnabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoEnabled.Description = TradeOutCellModel.GridButtonText;
            gridStyleInfoEnabled.Font.Bold = true;
            gridStyleInfoEnabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoEnabled.Font.Size = Math.Max(8, MDTraderControl.DefaultFontSize - 2);
            gridStyleInfoEnabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoEnabled.Tag = TradeOutCellModel.GridButtonCellModelTag;
            gridStyleInfoEnabled.TextColor = TradeOutCellModel.GridButtonEnabledTextColor;
            gridStyleInfoEnabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoEnabled.WrapText = true;

            base.GridStyleInfoEnabled = gridStyleInfoEnabled;

            GridStyleInfo gridStyleInfoDisabled = new GridStyleInfo();
            gridStyleInfoDisabled.BackColor = TradeOutCellModel.GridButtonDisabledBackColor;
            gridStyleInfoDisabled.CellAppearance = DefaultCellRenderer.GridButtonCellAppearance;
            gridStyleInfoDisabled.Description = TradeOutCellModel.GridButtonText;
            gridStyleInfoDisabled.Font.Bold = false;
            gridStyleInfoDisabled.Font.Facename = MDTraderControl.DefaultFontFacename;
            gridStyleInfoDisabled.Font.Size = Math.Max(8, MDTraderControl.DefaultFontSize - 2);
            gridStyleInfoDisabled.HorizontalAlignment = DefaultCellRenderer.GridButtonHorizontalAlignment;
            gridStyleInfoDisabled.Tag = TradeOutCellModel.GridButtonCellModelTag;
            gridStyleInfoDisabled.TextColor = TradeOutCellModel.GridButtonDisabledTextColor;
            gridStyleInfoDisabled.VerticalAlignment = DefaultCellRenderer.GridButtonVerticalAlignment;
            gridStyleInfoDisabled.WrapText = true;

            base.GridStyleInfoDisabled = gridStyleInfoDisabled;
        }
        #endregion
    }
    #endregion

    #region TradeOutCellModel
    [Serializable]
    public class TradeOutCellModel : DefaultGridPushButtonCellModel
    {
        #region CTORS
        public TradeOutCellModel(GridModel grid, Int32 columnIndex, String cellType)
            : base(grid, columnIndex, cellType)
        {
        }
        #endregion

        #region PROPERTIES
        public static Color GridButtonEnabledBackColor
        {
            get { return Color.White; }
        }

        public static Color GridButtonEnabledTextColor
        {
            get { return Color.Black; }
        }

        public static Color GridButtonDisabledBackColor
        {
            get { return SystemColors.Control; }
        }

        public static Color GridButtonDisabledTextColor
        {
            get { return Color.DarkGray; }
        }

        public static String GridButtonCellModelTag
        {
            get { return "TradeOutCellModel"; }
        }

        public static String GridButtonCellType
        {
            get { return "TradeOut"; }
        }

        public static String GridButtonText
        {
            get { return "TradeOut"; }
        }
        #endregion

        #region PUBLIC METHODS
        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            base.Renderer = new TradeOutCellRenderer(this, control);
            base.Renderer.Enabled = this.Enabled;
            return base.Renderer;
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Populates a serialization information object with the data needed to serialize the AddVolumeAtPriceCommand. 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        /// <remarks>
        /// Serializing an object exposes its private and protected data. You should apply the SerializationFormatter permission to 
        /// the GetObjectData method to prevent any non-public data from being available to objects that do not have permission to 
        /// serialize and deserialize objects.
        /// 
        /// The GetObjectData method is declared in the ISerializable interface. This interface is implemented by types that provide
        /// custom serialization logic. GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)
        /// is protected by a security check for SerializationFormatter security permission. If an implementation of 
        /// GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) is not protected 
        /// by the same security check, callers can call the implementation to bypass security on the interface and gain access to data 
        /// serialized by the type.
        /// </remarks>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        /// <summary>
        /// At deserialization time, this private constructor is called only after the data in the <paramref name="SerializationInfo"/> has been deserialized by the 
        /// </summary>
        /// <param name="info">A SerializationInfo that holds the serialized data associated with the AddVolumeAtPriceCommand.</param>
        /// <param name="context">A StreamingContext that contains the source and destination of the serialized stream associated with the AddVolumeAtPriceCommand.</param>
        protected TradeOutCellModel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
    }
    #endregion
    
    #endregion
}
