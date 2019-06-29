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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

using Syncfusion.Drawing;
using Syncfusion.Windows.Forms.Tools;

namespace TT.SP.Trading.Controls.MDTrader
{
    /// <summary>
    /// The MDTrader control panel for features like accounts, quantities, etc.
    /// </summary>
    public class MDTVerticalControlPanel : UserControl
    {
        #region PRIVATE MEMBERS
        private Dictionary<GradientPanel, Size> _snapPanels = new Dictionary<GradientPanel, Size>();
        private System.Windows.Forms.Cursor _oldCursor;
        private bool _bMouseDown;
        private int _diffX;
        private int _diffY;
        private Point _grabOffset;
        private int _netPos = Int32.MinValue;
        private RenderMode _renderMode = RenderMode.Color;
        private Point _topCornerLocation;
        private Point _firstTopCornerLocation;
        private Boolean _firstTimeSettingLocation;
        private Boolean _visibility;
        private Boolean _firstVisibility;
        private Boolean _firstTimeSettingVisibility;
        private int _defaultOrderQty;

        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtnClear;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _panel1;
        private TT.SP.Trading.Controls.MDTrader.SmartNumericUpDown _orderQtyBtn;
        private Syncfusion.Windows.Forms.Tools.GradientLabel _netPosLabel;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _panel5;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _panel2;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _panel3;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _headerPanel;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _header4;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _header2;
        private Syncfusion.Windows.Forms.Tools.GradientPanel _header3;
        private System.Windows.Forms.ComboBox accountComboBox;        
        private Syncfusion.Windows.Forms.ButtonAdv _closeButtonAdv;
        private EditableIntegerButton _editableIntegerButton1;
        private EditableIntegerButton _editableIntegerButton2;
        private EditableIntegerButton _editableIntegerButton5;
        private EditableIntegerButton _editableIntegerButton10;
        private EditableIntegerButton _editableIntegerButton50;
        #endregion

        #region EVENTS
        public event EventHandler<EventArgs> CloseButtonClicked;
        public event EventHandler<MouseEventArgs> ActivateControlPanelMenu;
        #endregion

        #region CTOR
        public MDTVerticalControlPanel()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // Set default value for netPos, via the property setter, to avoid
            // seeing the "NetPos" string when the panel is 1st shown.
            NetPos = 0;

            _snapPanels.Add(_panel1, _panel1.Size);
            _snapPanels.Add(_panel2, _panel2.Size);
            _snapPanels.Add(_panel3, _panel3.Size);
            _snapPanels.Add(_panel5, _panel5.Size);

            _header_Click(_header2, null);
            _header_Click(_header3, null);

            _closeButtonAdv.Click += CloseButtonOnClick;
            
            _editableIntegerButton1.Value = 1;
            _editableIntegerButton2.Value = 2;
            _editableIntegerButton5.Value = 5;
            _editableIntegerButton10.Value = 10;
            _editableIntegerButton50.Value = 50;

            _editableIntegerButton1.QuantityChanged += EditableIntegerButtonQuantityChanged;
            _editableIntegerButton2.QuantityChanged += EditableIntegerButtonQuantityChanged;
            _editableIntegerButton5.QuantityChanged += EditableIntegerButtonQuantityChanged;
            _editableIntegerButton10.QuantityChanged += EditableIntegerButtonQuantityChanged;
            _editableIntegerButton50.QuantityChanged += EditableIntegerButtonQuantityChanged;

            _panel1.MouseDown += MDTVerticalControlPanelMouseDown; 
            _panel2.MouseDown += MDTVerticalControlPanelMouseDown;
            _panel3.MouseDown += MDTVerticalControlPanelMouseDown;
            _panel5.MouseDown += MDTVerticalControlPanelMouseDown;
            _netPosLabel.MouseDown += MDTVerticalControlPanelMouseDown;
            accountComboBox.MouseDown += MDTVerticalControlPanelMouseDown;
            _orderQtyBtn.MouseDown += MDTVerticalControlPanelMouseDown;
        }
        #endregion

        #region public properties
        public int OrderQty
        {
            get { return (int)_orderQtyBtn.Value; }
            set { _orderQtyBtn.Value = value; }
        }

        public int DefaultOrderQty
        {
            get { return _defaultOrderQty; }
            set { _defaultOrderQty = value; }
        }

        public string Account
        {
            get { return accountComboBox.Text; }
        }

        /// <summary>
        /// Set the net position. 
        /// Updates the netpos text and control colors.
        /// </summary>
        public int NetPos
        {
            get { return _netPos; }
            set
            {
                if (_netPos != value)
                {
                    _netPos = value;
                    _netPosLabel.Text = _netPos == 0 ? "Flat" : _netPos.ToString(CultureInfo.InvariantCulture);
                    _renderState.UpdateColors(this, State.Hint.NetPos);
                }
            }
        } 

        public string[] CustomerList
        {
            set
            {
                if (value == null)
                    throw new ArgumentException("The CustomerList cannot be null.");

                bool bFound = false;
                foreach (string s in value)
                {
                    if (s.CompareTo(accountComboBox.Text) == 0)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (!bFound)
                    accountComboBox.Text = MDTVerticalControlPanel.DefaultAccountName;

                accountComboBox.Items.Clear();
                accountComboBox.Items.AddRange(value);
            }
        }

        /// <summary>
        /// Set the rendering mode of the control to Mono or Color
        /// </summary>
        public RenderMode RenderMode
        {
            set
            {
                if (_renderMode != value)
                {
                    _renderMode = value;
                    _renderState = (_renderMode == RenderMode.Color) ? StateColor._instance : StateMono._instance;
                    _renderState.UpdateColors(this, State.Hint.All);
                }
            }
        }

        public static String DefaultAccountName
        {
            get { return "<Default>"; }
        }

        public static Int32 MinimumDefaultQuantity
        {
            get { return 1; }
        }

        public static Int32 MaximumDefaultQuantity
        {
            get { return 99999999; }
        }

        public Boolean FirstTimeSettingVisibleSetting
        {
            get { return _firstTimeSettingVisibility; }
            set { _firstTimeSettingVisibility = value; }
        }

        public Boolean FirstVisibility
        {
            get { return _firstVisibility; }
            set { _firstVisibility = value; }
        }

        public Boolean Visibility
        {
            get { return _visibility; }
            set
            {
                if (_firstTimeSettingVisibility)
                {
                    FirstVisibility = value;
                    _firstTimeSettingVisibility = false;
                }

                _visibility = value;
            }
        }

        /// <summary>
        /// This property is needed to make sure that the original location of the panel is stored away as the values
        /// gets overwritten by the memento code later in the call stack. See the SetMemento method in the frmChart code
        /// for an explanation.
        /// </summary>
        public Boolean FirstTimeSettingLocation
        {
            get { return _firstTimeSettingLocation; }
            set { _firstTimeSettingLocation = value; }
        }

        public Point FirstTopCornerLocation
        {
            get { return _firstTopCornerLocation; }
            set { _firstTopCornerLocation = value; }
        }

        public Point TopCornerLocation
        {
            get { return _topCornerLocation; }
            set 
            {
                if (_firstTimeSettingLocation)
                {
                    FirstTopCornerLocation = value;
                    _firstTimeSettingLocation = false;
                }

                _topCornerLocation = value;
            }
        }

        public Int64 Button1Quantity
        {
            get { return _editableIntegerButton1.Value; }
            set 
            {
                _editableIntegerButton1.Text = value.ToString(CultureInfo.InvariantCulture);
                _editableIntegerButton1.Value = value; 
            }
        }

        public Int64 Button2Quantity
        {
            get { return _editableIntegerButton2.Value; }
            set 
            {
                _editableIntegerButton2.Text = value.ToString(CultureInfo.InvariantCulture);
                _editableIntegerButton2.Value = value; 
            }
        }

        public Int64 Button5Quantity
        {
            get { return _editableIntegerButton5.Value; }
            set 
            {
                _editableIntegerButton5.Text = value.ToString(CultureInfo.InvariantCulture);
                _editableIntegerButton5.Value = value;
            }
        }

        public Int64 Button10Quantity
        {
            get { return _editableIntegerButton10.Value; }
            set 
            {
                _editableIntegerButton10.Text = value.ToString(CultureInfo.InvariantCulture);
                _editableIntegerButton10.Value = value; 
            }
        }

        public Int64 Button50Quantity
        {
            get { return _editableIntegerButton50.Value; }
            set 
            {
                _editableIntegerButton50.Text = value.ToString(CultureInfo.InvariantCulture);
                _editableIntegerButton50.Value = value; 
            }
        }

        public Boolean OrderQuantitiesHidden
        {
            get { return IsHeaderControlHidden(_header2); }
            set 
            {
                if (value)
                    MinimizeHeaderControl(_header2);
                else
                    MaximizeHeaderControl(_header2);
            }
        }

        public Boolean OrderTypesHidden
        {
            get { return IsHeaderControlHidden(_header3); }
            set
            {
                if (value)
                    MinimizeHeaderControl(_header3);
                else
                    MaximizeHeaderControl(_header3);
            }
        }

        public Boolean AccountsHidden
        {
            get { return IsHeaderControlHidden(_header4); }
            set
            {
                if (value)
                    MinimizeHeaderControl(_header4);
                else
                    MaximizeHeaderControl(_header4);
            }
        }

        public static Int32 SnapToAmount
        {
            get { return 20; }
        }

        public static Int32 HeaderControlHeightOffset
        {
            get { return 2; }
        }
        #endregion

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._panel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._netPosLabel = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this._headerPanel = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._closeButtonAdv = new Syncfusion.Windows.Forms.ButtonAdv();
            this._panel5 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this._header4 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._panel2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._editableIntegerButton5 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton50 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton2 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton10 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton1 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._header2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._qtyBtnClear = new Syncfusion.Windows.Forms.ButtonAdv();
            this._panel3 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._header3 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this._editableIntegerButton1 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._orderQtyBtn = new TT.SP.Trading.Controls.MDTrader.SmartNumericUpDown();
            this._editableIntegerButton10 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton2 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton50 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            this._editableIntegerButton5 = new TT.SP.Trading.Controls.MDTrader.EditableIntegerButton();
            ((System.ComponentModel.ISupportInitialize)(this._panel1)).BeginInit();
            this._panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._headerPanel)).BeginInit();
            this._headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._panel5)).BeginInit();
            this._panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._header4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panel2)).BeginInit();
            this._panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._header2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._panel3)).BeginInit();
            this._panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._header3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._orderQtyBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // _panel1
            // 
            this._panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this._panel1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.SystemColors.InactiveBorder, System.Drawing.SystemColors.ControlDark);
            this._panel1.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
            this._panel1.BorderColor = System.Drawing.Color.Black;
            this._panel1.Controls.Add(this._netPosLabel);
            this._panel1.Controls.Add(this._orderQtyBtn);
            this._panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._panel1.Location = new System.Drawing.Point(0, 16);
            this._panel1.Name = "_panel1";
            this._panel1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this._panel1.Size = new System.Drawing.Size(100, 54);
            this._panel1.TabIndex = 4;
            // 
            // _netPosLabel
            // 
            this._netPosLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._netPosLabel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.Maroon);
            this._netPosLabel.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top)
                        | System.Windows.Forms.Border3DSide.Right)
                        | System.Windows.Forms.Border3DSide.Bottom)));
            this._netPosLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this._netPosLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._netPosLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._netPosLabel.ForeColor = System.Drawing.Color.White;
            this._netPosLabel.Location = new System.Drawing.Point(2, 2);
            this._netPosLabel.Name = "_netPosLabel";
            this._netPosLabel.Size = new System.Drawing.Size(92, 22);
            this._netPosLabel.TabIndex = 77;
            this._netPosLabel.Text = "netpos";
            this._netPosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _orderQtyBtn
            // 
            this._orderQtyBtn.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._orderQtyBtn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._orderQtyBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._orderQtyBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._orderQtyBtn.Location = new System.Drawing.Point(2, 26);
            this._orderQtyBtn.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this._orderQtyBtn.Name = "_orderQtyBtn";
            this._orderQtyBtn.Size = new System.Drawing.Size(92, 22);
            this._orderQtyBtn.TabIndex = 76;
            this._orderQtyBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _headerPanel
            // 
            this._headerPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._headerPanel.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.BackwardDiagonal, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.ActiveCaption);
            this._headerPanel.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
            this._headerPanel.BorderColor = System.Drawing.Color.White;
            this._headerPanel.BorderSingle = System.Windows.Forms.ButtonBorderStyle.Outset;
            this._headerPanel.Controls.Add(this._closeButtonAdv);
            this._headerPanel.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this._headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headerPanel.Location = new System.Drawing.Point(0, 0);
            this._headerPanel.Name = "_headerPanel";
            this._headerPanel.Size = new System.Drawing.Size(100, 16);
            this._headerPanel.TabIndex = 5;
            this._headerPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this._headerPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this._headerPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            // 
            // _closeButtonAdv
            // 
            this._closeButtonAdv.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._closeButtonAdv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this._closeButtonAdv.BorderStyleAdv = Syncfusion.Windows.Forms.ButtonAdvBorderStyle.None;
            this._closeButtonAdv.ComboEditBackColor = System.Drawing.Color.Empty;
            this._closeButtonAdv.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._closeButtonAdv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this._closeButtonAdv.FlatAppearance.BorderSize = 0;
            this._closeButtonAdv.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this._closeButtonAdv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(50)))), ((int)(((byte)(0)))));
            this._closeButtonAdv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButtonAdv.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._closeButtonAdv.ForeColor = System.Drawing.Color.White;
            this._closeButtonAdv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._closeButtonAdv.IsMouseDown = false;
            this._closeButtonAdv.Location = new System.Drawing.Point(83, -4);
            this._closeButtonAdv.Name = "_closeButtonAdv";
            this._closeButtonAdv.Size = new System.Drawing.Size(14, 21);
            this._closeButtonAdv.TabIndex = 0;
            this._closeButtonAdv.Text = "X";
            this._closeButtonAdv.UseVisualStyle = false;
            this._closeButtonAdv.UseVisualStyleBackColor = false;
            // 
            // _panel5
            // 
            this._panel5.BackColor = System.Drawing.SystemColors.ControlDark;
            this._panel5.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.SystemColors.InactiveBorder, System.Drawing.SystemColors.ControlDark);
            this._panel5.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
            this._panel5.BorderColor = System.Drawing.Color.Black;
            this._panel5.Controls.Add(this.accountComboBox);
            this._panel5.Controls.Add(this._header4);
            this._panel5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this._panel5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._panel5.Location = new System.Drawing.Point(0, 228);
            this._panel5.Name = "_panel5";
            this._panel5.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._panel5.Size = new System.Drawing.Size(100, 42);
            this._panel5.TabIndex = 3;
            // 
            // accountComboBox
            // 
            this.accountComboBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.accountComboBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accountComboBox.Location = new System.Drawing.Point(2, 12);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(92, 22);
            this.accountComboBox.TabIndex = 77;
            this.accountComboBox.Text = "<Default>";
            // 
            // _header4
            // 
            this._header4.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.SystemColors.Highlight, System.Drawing.SystemColors.HotTrack);
            this._header4.Border3DStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this._header4.BorderColor = System.Drawing.Color.Black;
            this._header4.Cursor = System.Windows.Forms.Cursors.HSplit;
            this._header4.Dock = System.Windows.Forms.DockStyle.Top;
            this._header4.Location = new System.Drawing.Point(2, 0);
            this._header4.Name = "_header4";
            this._header4.Size = new System.Drawing.Size(92, 10);
            this._header4.TabIndex = 76;
            this._header4.Click += new System.EventHandler(this._header_Click);
            // 
            // _panel2
            // 
            this._panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this._panel2.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.SystemColors.InactiveBorder, System.Drawing.SystemColors.ControlDark);
            this._panel2.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
            this._panel2.BorderColor = System.Drawing.Color.Black;
            this._panel2.Controls.Add(this._editableIntegerButton5);
            this._panel2.Controls.Add(this._editableIntegerButton50);
            this._panel2.Controls.Add(this._editableIntegerButton2);
            this._panel2.Controls.Add(this._editableIntegerButton10);
            this._panel2.Controls.Add(this._editableIntegerButton1);
            this._panel2.Controls.Add(this._header2);
            this._panel2.Controls.Add(this._qtyBtnClear);
            this._panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._panel2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._panel2.Location = new System.Drawing.Point(0, 70);
            this._panel2.Name = "_panel2";
            this._panel2.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._panel2.Size = new System.Drawing.Size(100, 92);
            this._panel2.TabIndex = 2;
            // 
            // _editableIntegerButton5
            // 
            this._editableIntegerButton5.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton5.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton5.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton5.IsMouseDown = false;
            this._editableIntegerButton5.Location = new System.Drawing.Point(2, 64);
            this._editableIntegerButton5.Name = "_editableIntegerButton5";
            this._editableIntegerButton5.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton5.TabIndex = 84;
            this._editableIntegerButton5.Text = "5";
            this._editableIntegerButton5.UseVisualStyle = false;
            this._editableIntegerButton5.UseVisualStyleBackColor = false;
            this._editableIntegerButton5.Value = ((long)(1));
            // 
            // _editableIntegerButton50
            // 
            this._editableIntegerButton50.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton50.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton50.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton50.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton50.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton50.IsMouseDown = false;
            this._editableIntegerButton50.Location = new System.Drawing.Point(49, 39);
            this._editableIntegerButton50.Name = "_editableIntegerButton50";
            this._editableIntegerButton50.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton50.TabIndex = 83;
            this._editableIntegerButton50.Text = "50";
            this._editableIntegerButton50.UseVisualStyle = false;
            this._editableIntegerButton50.UseVisualStyleBackColor = false;
            this._editableIntegerButton50.Value = ((long)(1));
            // 
            // _editableIntegerButton2
            // 
            this._editableIntegerButton2.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton2.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton2.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton2.IsMouseDown = false;
            this._editableIntegerButton2.Location = new System.Drawing.Point(2, 39);
            this._editableIntegerButton2.Name = "_editableIntegerButton2";
            this._editableIntegerButton2.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton2.TabIndex = 82;
            this._editableIntegerButton2.Text = "2";
            this._editableIntegerButton2.UseVisualStyle = false;
            this._editableIntegerButton2.UseVisualStyleBackColor = false;
            this._editableIntegerButton2.Value = ((long)(1));
            // 
            // _editableIntegerButton10
            // 
            this._editableIntegerButton10.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton10.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton10.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton10.IsMouseDown = false;
            this._editableIntegerButton10.Location = new System.Drawing.Point(49, 13);
            this._editableIntegerButton10.Name = "_editableIntegerButton10";
            this._editableIntegerButton10.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton10.TabIndex = 81;
            this._editableIntegerButton10.Text = "10";
            this._editableIntegerButton10.UseVisualStyle = false;
            this._editableIntegerButton10.UseVisualStyleBackColor = false;
            this._editableIntegerButton10.Value = ((long)(1));
            // 
            // _editableIntegerButton1
            // 
            this._editableIntegerButton1.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton1.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton1.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton1.IsMouseDown = false;
            this._editableIntegerButton1.Location = new System.Drawing.Point(2, 13);
            this._editableIntegerButton1.Name = "_editableIntegerButton1";
            this._editableIntegerButton1.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton1.TabIndex = 80;
            this._editableIntegerButton1.Text = "1";
            this._editableIntegerButton1.UseVisualStyle = false;
            this._editableIntegerButton1.UseVisualStyleBackColor = false;
            this._editableIntegerButton1.Value = ((long)(1));
            // 
            // _header2
            // 
            this._header2.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.SystemColors.Highlight, System.Drawing.SystemColors.HotTrack);
            this._header2.Border3DStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this._header2.BorderColor = System.Drawing.Color.Black;
            this._header2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this._header2.Dock = System.Windows.Forms.DockStyle.Top;
            this._header2.Location = new System.Drawing.Point(2, 0);
            this._header2.Name = "_header2";
            this._header2.Size = new System.Drawing.Size(92, 10);
            this._header2.TabIndex = 78;
            this._header2.Click += new System.EventHandler(this._header_Click);
            // 
            // _qtyBtnClear
            // 
            this._qtyBtnClear.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtnClear.BackColor = System.Drawing.SystemColors.Control;
            this._qtyBtnClear.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtnClear.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtnClear.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._qtyBtnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtnClear.IsMouseDown = false;
            this._qtyBtnClear.Location = new System.Drawing.Point(49, 64);
            this._qtyBtnClear.Name = "_qtyBtnClear";
            this._qtyBtnClear.Size = new System.Drawing.Size(45, 24);
            this._qtyBtnClear.TabIndex = 77;
            this._qtyBtnClear.Text = "Clear";
            this._qtyBtnClear.UseVisualStyle = false;
            this._qtyBtnClear.UseVisualStyleBackColor = false;
            this._qtyBtnClear.Click += new System.EventHandler(this._qtyBtnClear_Click);
            // 
            // _panel3
            // 
            this._panel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this._panel3.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.SystemColors.InactiveBorder, System.Drawing.SystemColors.ControlDark);
            this._panel3.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
            this._panel3.BorderColor = System.Drawing.Color.Black;
            this._panel3.Controls.Add(this._header3);
            this._panel3.Controls.Add(this.button9);
            this._panel3.Controls.Add(this.button7);
            this._panel3.Controls.Add(this.button1);
            this._panel3.Controls.Add(this.button2);
            this._panel3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this._panel3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._panel3.Location = new System.Drawing.Point(0, 162);
            this._panel3.Name = "_panel3";
            this._panel3.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._panel3.Size = new System.Drawing.Size(100, 66);
            this._panel3.TabIndex = 1;
            // 
            // _header3
            // 
            this._header3.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.SystemColors.Highlight, System.Drawing.SystemColors.HotTrack);
            this._header3.Border3DStyle = System.Windows.Forms.Border3DStyle.SunkenInner;
            this._header3.BorderColor = System.Drawing.Color.Black;
            this._header3.Cursor = System.Windows.Forms.Cursors.HSplit;
            this._header3.Dock = System.Windows.Forms.DockStyle.Top;
            this._header3.Location = new System.Drawing.Point(2, 0);
            this._header3.Name = "_header3";
            this._header3.Size = new System.Drawing.Size(92, 10);
            this._header3.TabIndex = 76;
            this._header3.Click += new System.EventHandler(this._header_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button9.Enabled = false;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(2, 38);
            this.button9.Name = "button9";
            this.button9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button9.Size = new System.Drawing.Size(45, 24);
            this.button9.TabIndex = 74;
            this.button9.Text = "SM";
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button7.Enabled = false;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(49, 12);
            this.button7.Name = "button7";
            this.button7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button7.Size = new System.Drawing.Size(45, 24);
            this.button7.TabIndex = 73;
            this.button7.Text = "SL";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Yellow;
            this.button1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(2, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 24);
            this.button1.TabIndex = 72;
            this.button1.Text = "Limit";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(49, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(45, 24);
            this.button2.TabIndex = 75;
            this.button2.Text = "IOC";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // _editableIntegerButton1
            // 
            this._editableIntegerButton1.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton1.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton1.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton1.IsMouseDown = false;
            this._editableIntegerButton1.Location = new System.Drawing.Point(2, 13);
            this._editableIntegerButton1.Name = "_editableIntegerButton1";
            this._editableIntegerButton1.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton1.TabIndex = 80;
            this._editableIntegerButton1.Text = "1";
            this._editableIntegerButton1.UseVisualStyle = false;
            this._editableIntegerButton1.UseVisualStyleBackColor = false;
            // 
            // _orderQtyBtn
            // 
            this._orderQtyBtn.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._orderQtyBtn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._orderQtyBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._orderQtyBtn.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._orderQtyBtn.Location = new System.Drawing.Point(2, 26);
            this._orderQtyBtn.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this._orderQtyBtn.Name = "_orderQtyBtn";
            this._orderQtyBtn.Size = new System.Drawing.Size(92, 22);
            this._orderQtyBtn.TabIndex = 76;
            this._orderQtyBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // _editableIntegerButton10
            // 
            this._editableIntegerButton10.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton10.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton10.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton10.IsMouseDown = false;
            this._editableIntegerButton10.Location = new System.Drawing.Point(49, 13);
            this._editableIntegerButton10.Name = "_editableIntegerButton10";
            this._editableIntegerButton10.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton10.TabIndex = 81;
            this._editableIntegerButton10.Text = "10";
            this._editableIntegerButton10.UseVisualStyle = false;
            this._editableIntegerButton10.UseVisualStyleBackColor = false;
            // 
            // _editableIntegerButton2
            // 
            this._editableIntegerButton2.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton2.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton2.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton2.IsMouseDown = false;
            this._editableIntegerButton2.Location = new System.Drawing.Point(2, 39);
            this._editableIntegerButton2.Name = "_editableIntegerButton2";
            this._editableIntegerButton2.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton2.TabIndex = 82;
            this._editableIntegerButton2.Text = "2";
            this._editableIntegerButton2.UseVisualStyle = false;
            this._editableIntegerButton2.UseVisualStyleBackColor = false;
            // 
            // _editableIntegerButton50
            // 
            this._editableIntegerButton50.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton50.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton50.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton50.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton50.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton50.IsMouseDown = false;
            this._editableIntegerButton50.Location = new System.Drawing.Point(49, 39);
            this._editableIntegerButton50.Name = "_editableIntegerButton50";
            this._editableIntegerButton50.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton50.TabIndex = 83;
            this._editableIntegerButton50.Text = "50";
            this._editableIntegerButton50.UseVisualStyle = false;
            this._editableIntegerButton50.UseVisualStyleBackColor = false;
            // 
            // _editableIntegerButton5
            // 
            this._editableIntegerButton5.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._editableIntegerButton5.BackColor = System.Drawing.SystemColors.Control;
            this._editableIntegerButton5.ComboEditBackColor = System.Drawing.Color.Empty;
            this._editableIntegerButton5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._editableIntegerButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._editableIntegerButton5.IsMouseDown = false;
            this._editableIntegerButton5.Location = new System.Drawing.Point(2, 64);
            this._editableIntegerButton5.Name = "_editableIntegerButton5";
            this._editableIntegerButton5.Size = new System.Drawing.Size(45, 23);
            this._editableIntegerButton5.TabIndex = 84;
            this._editableIntegerButton5.Text = "5";
            this._editableIntegerButton5.UseVisualStyle = false;
            this._editableIntegerButton5.UseVisualStyleBackColor = false;
            // 
            // MDTVerticalControlPanel
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._panel5);
            this.Controls.Add(this._panel3);
            this.Controls.Add(this._panel2);
            this.Controls.Add(this._panel1);
            this.Controls.Add(this._headerPanel);
            this.Name = "MDTVerticalControlPanel";
            this.Size = new System.Drawing.Size(100, 270);
            ((System.ComponentModel.ISupportInitialize)(this._panel1)).EndInit();
            this._panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._headerPanel)).EndInit();
            this._headerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._panel5)).EndInit();
            this._panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._header4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panel2)).EndInit();
            this._panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._header2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._panel3)).EndInit();
            this._panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._header3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._orderQtyBtn)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Clicking on the header of a section will cause that section
        /// (panel) to shrink to a minimum size and the other
        /// sections ( panels ) will follow upwards, as they
        /// are docked to the top.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _header_Click(object sender, EventArgs e)
        {
            if (!(sender is Control))
                return;

            GradientPanel headerControl = sender as GradientPanel;

            if (headerControl == null)
                throw new InvalidOperationException("The incoming header control must be of type GrandientPanel.");

            if (IsHeaderControlHidden(headerControl))
                MaximizeHeaderControl(headerControl);
            else
                MinimizeHeaderControl(headerControl);
        }

        /// <summary>
        /// Begin tracking the movement of the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            _bMouseDown = true;

            _grabOffset = Cursor.Position;

            Rectangle rrr = Parent.RectangleToScreen(Bounds);

            // Diff X and Y tell us how much offset there was from the
            // mouse pointer when it grabbed the header/bar compared
            // to the upper left corner of this control, expressed
            // in screen coordinates.  We'll use this later to
            // determine where to move this control's window
            // as the mouse moves around.
            _diffX = _grabOffset.X - rrr.X;
            _diffY = _grabOffset.Y - rrr.Y;  
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            _bMouseDown = false;
            Cursor.Current = _oldCursor;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_bMouseDown)
            {
                if (Cursor.Current != Cursors.Hand)
                {
                    _oldCursor = Cursors.Hand;
                    Cursor.Current = Cursors.Hand;
                }

                Point p = Parent.PointToClient(Cursor.Position);

                Parent.SuspendLayout();

                int newLeft = p.X - _diffX;
                int newRight = p.X + _diffX;
                int newTop = p.Y - _diffY;

                // Snap-to the left 
                if (Left > MDTVerticalControlPanel.SnapToAmount && newLeft <= MDTVerticalControlPanel.SnapToAmount)
                {
                    Point ptr = Cursor.Position;
                    ptr.X -= newLeft;
                    Cursor.Position = ptr;
                    newLeft = 0;

                    Anchor = AnchorStyles.None;
                    Anchor = AnchorStyles.Left;
                }
 
                // Snap-to the right
                // TBD....
 
                // Snap-to the Top
                if (Top > MDTVerticalControlPanel.SnapToAmount && newTop <= MDTVerticalControlPanel.SnapToAmount)
                {
                    Point ptr = Cursor.Position;
                    ptr.Y -= newTop;
                    Cursor.Position = ptr;
                    newTop = 0;

                    Anchor = AnchorStyles.None;
                    Anchor = AnchorStyles.Top;
                }

                if (newTop < 0)
                    newTop = 0;

                if (newLeft < 0)
                    newLeft = 0;

                // Snap-to-the Bottom
                if (Top + Height < Parent.ClientRectangle.Height - MDTVerticalControlPanel.SnapToAmount
                    && newTop + Height >= Parent.ClientRectangle.Height - MDTVerticalControlPanel.SnapToAmount)
                {
                    int adjustedTop = Parent.ClientRectangle.Bottom - this.Height;
                    int movedYAmount = adjustedTop - newTop;

                    Point ptr = Cursor.Position;
                    ptr.Y += movedYAmount;
                    Cursor.Position = ptr;

                    newTop = adjustedTop;

                    Anchor = AnchorStyles.None;
                    Anchor = AnchorStyles.Bottom;
                }

                if (newTop + Height > Parent.ClientRectangle.Height)
                    newTop = Parent.ClientRectangle.Height - Height;

                if (newLeft + Width > Parent.ClientRectangle.Width)
                    newLeft = Parent.ClientRectangle.Width - Width;

                Left = newLeft;
                Top = newTop;

                Parent.ResumeLayout();

                // DON'T do the following:
                //       Parent.Refresh();   // no no no no no
            }
        }

        /// <summary>
        /// All quantity buttons on the form map to this method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnQtyBtnMouseUp(Int64 quantity, MouseEventArgs e)
        {
            switch (e.Button)
            {
                // LEFT button increases the qty
                case System.Windows.Forms.MouseButtons.Left:
                    if (_orderQtyBtn.Value + quantity <= _orderQtyBtn.Maximum)
                        _orderQtyBtn.Value += quantity;
                    break;

                // RIGHT button decreases the qty
                case System.Windows.Forms.MouseButtons.Right:
                    if (_orderQtyBtn.Value - quantity > _orderQtyBtn.Minimum)
                        _orderQtyBtn.Value -= quantity;
                    break;
            }
        }

        /// <summary>
        /// Set next trade qty value to zero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _qtyBtnClear_Click(object sender, EventArgs e)
        {
            _orderQtyBtn.ResetText();
            _orderQtyBtn.Value = 0;
        }

        /// <summary>
        /// Provides an mechanism to fire this objects click event based upon the close button click event.
        /// </summary>
        /// <param name="sender">The MDTraderVerticalControlPanel object which contains the parameters.</param>
        /// <param name="e">The EventArgs that provides the event data.</param>
        private void CloseButtonOnClick(object sender, EventArgs e)
        {
            if (CloseButtonClicked != null)
                CloseButtonClicked(sender, e);
        }

        /// <summary>
        /// Sink the quantity changed event from the editable integer buttons.
        /// </summary>
        /// <param name="sender">The EditableIntegerButton object which contains the parameters.</param>
        /// <param name="e">The IntegerButtonEventArgs that provides the event data.</param>
        private void EditableIntegerButtonQuantityChanged(object sender, IntegerButtonEventArgs e)
        {
            OnQtyBtnMouseUp(e.Value, e.MouseEventArgs);
        }

        /// <summary>
        /// Fire the active control panel menu event if anyone is hooked to the event.
        /// </summary>
        /// <param name="sender">The MDTraderVerticalControlPanel object which contains the parameters.</param>
        /// <param name="e">The MouseEventArgs that provides the event data.</param>
        private void DoActivateControlPanelMenu(object sender, MouseEventArgs e)
        {
            if (ActivateControlPanelMenu != null)
                ActivateControlPanelMenu(sender, e);
        }

        /// <summary>
        /// Fire the active control panel menu event if the right mouse button is clicked on the control.
        /// </summary>
        /// <param name="sender">The MDTraderVerticalControlPanel object which contains the parameters.</param>
        /// <param name="e">The MouseEventArgs that provides the event data.</param>
        private void MDTVerticalControlPanelMouseDown(object sender, MouseEventArgs e)
        {
            if (button2.Bounds.Contains(e.X, e.Y) || button7.Bounds.Contains(e.X, e.Y) || button9.Bounds.Contains(e.X, e.Y))
                return;

            if (e.Button == MouseButtons.Right)
            {
                // Take the cursor position on the screen and translate that into the coordinates that are in the
                // coordinate space of this control.  If this is not done the menu placement will be in pseudo-
                // random locations around this control and will not actually follow the mouse.
                Point p = PointToClient(Cursor.Position);
                MouseEventArgs mea = new MouseEventArgs(e.Button, e.Clicks, p.X, p.Y, e.Delta);
                DoActivateControlPanelMenu(sender, mea);
            }
        }

        /// <summary>
        /// Determine if the header control is hidden or visible.
        /// </summary>
        /// <param name="headerControl">The GradientPanel which represents the header to click on for changing the visible state.</param>
        /// <returns>True if the control is visible and false otherwise.</returns>
        private static Boolean IsHeaderControlHidden(GradientPanel headerControl)
        {
            GradientPanel parentControl = headerControl.Parent as GradientPanel;

            return (parentControl.Height == headerControl.Height + MDTVerticalControlPanel.HeaderControlHeightOffset);
        }

        /// <summary>
        /// Attempt to minimize the panel associated with the header which was clicked.
        /// </summary>
        /// <param name="headerControl">The GradientPanel which represents the header control.</param>
        private void MinimizeHeaderControl(GradientPanel headerControl)
        {
            GradientPanel parentControl = headerControl.Parent as GradientPanel;
            Size parentOrigSize = _snapPanels[parentControl];
            int hDiff = parentOrigSize.Height - (headerControl.Height + MDTVerticalControlPanel.HeaderControlHeightOffset);

            if (parentControl.Height != headerControl.Height + MDTVerticalControlPanel.HeaderControlHeightOffset)
            {
                parentControl.Height = headerControl.Height + MDTVerticalControlPanel.HeaderControlHeightOffset;
                Height -= hDiff;
            }
        }

        /// <summary>
        /// Attempt to maximize the panel associated with the header which was clicked.
        /// </summary>
        /// <param name="headerControl">The GradientPanel which represents the header control.</param>
        private void MaximizeHeaderControl(GradientPanel headerControl)
        {
            GradientPanel parentControl = headerControl.Parent as GradientPanel;
            Size parentOrigSize = _snapPanels[parentControl];
            int hDiff = parentOrigSize.Height - (headerControl.Height + MDTVerticalControlPanel.HeaderControlHeightOffset);

            if (parentControl.Height == headerControl.Height + MDTVerticalControlPanel.HeaderControlHeightOffset)
            {
                parentControl.Height = parentOrigSize.Height;
                Height += hDiff;
            }
        }
        #endregion

        #region PROTECTED METHODS
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Provides a way to keep the panel docked to the side of the parent window if applicable.
        /// </summary>
        public void OnResize()
        {
            OnMouseMove(this, null);
        }

        /// <summary>
        /// Provide a way to restore the settings that the memento sets on saving or restoring a workspace.
        /// </summary>
        public void RestoreSettings()
        {
            _topCornerLocation = _firstTopCornerLocation;
            Visible = _firstVisibility;
        }
        #endregion

        #region Rendering States
        /// <summary>
        /// Base class for stateful control styles
        /// </summary>
        private abstract class State
        {
            public enum Hint
            {
                All, /* status AND net position changed */
                Status, /* status changed */
                NetPos, /* net position changed */
            }

            /// <summary>
            /// Update colors for all control panel elements
            /// </summary>
            /// <param name="controlPanel"></param>
            /// <param name="hint"></param>
            public void UpdateColors(MDTVerticalControlPanel controlPanel, Hint hint)
            {
                if (hint == Hint.All || hint == Hint.Status)
                    UpdateGeneralColors(controlPanel);
                else if (hint == Hint.All || hint == Hint.NetPos)
                    UpdateNetPositionColors(controlPanel);
            }

            /// <summary>
            /// Update everything except net position
            /// </summary>
            /// <param name="controlPanel"></param>
            protected abstract void UpdateGeneralColors(MDTVerticalControlPanel controlPanel);

            /// <summary>
            /// Update only net position controls
            /// </summary>
            /// <param name="controlPanel"></param>
            protected abstract void UpdateNetPositionColors(MDTVerticalControlPanel controlPanel);
        }

        /// <summary>
        /// Mono rendering when the instrument is down or untradable
        /// </summary>
        private class StateMono : State
        {
            public static State _instance = new StateMono();

            protected override void UpdateGeneralColors(MDTVerticalControlPanel cp)
            {
                cp._panel1.Enabled =
                cp._panel2.Enabled =
                cp._panel3.Enabled =
                cp._panel5.Enabled = false;

                // gradiant control panel colors
                RenderStyle.Mono.Apply(cp._headerPanel);
                RenderStyle.Mono.Apply(cp._header2);
                RenderStyle.Mono.Apply(cp._header3);
                RenderStyle.Mono.Apply(cp._header4);
            }

            protected override void UpdateNetPositionColors(MDTVerticalControlPanel cp)
            {
                if (cp._netPos == 0)
                    RenderStyle.NetposFlatMono.Apply(cp._netPosLabel);
                else if (cp._netPos > 0)
                    RenderStyle.NetposLongMono.Apply(cp._netPosLabel);
                else
                    RenderStyle.NetposShortMono.Apply(cp._netPosLabel);
            }
        }

        /// <summary>
        /// Colorful rendering when the instrument is enabled and tradable.
        /// </summary>
        private class StateColor : State
        {
            public static State _instance = new StateColor();

            protected override void UpdateGeneralColors(MDTVerticalControlPanel cp)
            {
                cp._panel1.Enabled =
                cp._panel2.Enabled =
                cp._panel3.Enabled =
                cp._panel5.Enabled = true;

                // gradiant control panel colors
                RenderStyle.HeaderPanel.Apply(cp._headerPanel);
                RenderStyle.SubHeader.Apply(cp._header2);
                RenderStyle.SubHeader.Apply(cp._header3);
                RenderStyle.SubHeader.Apply(cp._header4);
            }

            protected override void UpdateNetPositionColors(MDTVerticalControlPanel cp)
            {
                if (cp._netPos == 0)
                    RenderStyle.NetposFlat.Apply(cp._netPosLabel);
                else if (cp._netPos > 0)
                    RenderStyle.NetposLong.Apply(cp._netPosLabel);
                else
                    RenderStyle.NetposShort.Apply(cp._netPosLabel);
            }
        }

        private State _renderState = StateColor._instance;
        #endregion // Rendering States
    }

    #region SmartNumericUpDown
    /// <summary>
    /// The purpose of this class is to provide a numeric edit control
    /// with up/down arrows that really only allows the user to enter
    /// 0-9 and won't exceed the Maximum or Minimum
    /// </summary>
    public class SmartNumericUpDown : System.Windows.Forms.NumericUpDown
    {
        /// <summary>
        /// Returns true if all the text in the control is selected
        /// </summary>
        /// <returns></returns>
        protected bool IsAllSelected()
        {
            // Suxks, but we have to dive into the Controls[] to get
            // the underlying TextBox, since the conrol doesn't
            // expose things like SelectionLength to us.
            TextBox tb = null;
            if (Controls != null
                && Controls.Count > 1 && Controls[1] is TextBox)
            {
                tb = (TextBox)Controls[1];
            }

            bool bAllSelected = false;
            if (tb != null)
                bAllSelected = (tb.SelectionLength == Text.Length);

            return bAllSelected;
        }

        /// <summary>
        /// Intercept key down messages to massage where the carrot
        /// goes.  The control wants to put the carrot at the front
        /// of the number a lot of times, but we always want it
        /// at the back so the user can keep typing and deleteing.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Delete || e.KeyData == Keys.Back)
                && (IsAllSelected() || Text.Length == 1))
            {
                Value = 0;
                Select(Text.Length, 0);
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Only allow:  0123456789 and things
            // like CTRL-X
            if (System.Char.IsDigit(e.KeyChar)
                || System.Char.IsControl(e.KeyChar))
            {
                e.Handled = false;   // let the regular processing get it
            }
            else
            {
                e.Handled = true;    // we are blocking the key
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// We're looking out for situations where the control
        /// is putting the cursor at the front of the string,
        /// but we want it at the end.  i.e., when there is only one
        /// character after you've deleted from the end of the
        /// string with backspace.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (Text.Length == 1)
            {
                Select(Text.Length, 0);
            }

            if (Value >= Maximum)
            {
                Select(Text.Length, 0);
            }

            base.OnTextChanged(e);
        }
    }
    #endregion

    #region IntegerButtonEventArgs
    public class IntegerButtonEventArgs : EventArgs
    {
        #region PRIVATE MEMBERS
        private Int64 _value;
        private MouseEventArgs _mouseEventArgs;
        #endregion

        #region CTOR
        public IntegerButtonEventArgs(Int64 value, MouseEventArgs e)
        {
            _value = value;
            _mouseEventArgs = e;
        }
        #endregion

        #region PROPERTIES
        public Int64 Value
        {
            get { return _value; }
        }

        public MouseEventArgs MouseEventArgs
        {
            get { return _mouseEventArgs; }
        }
        #endregion
    }
    #endregion

    #region EditableIntegerButton
    public class EditableIntegerButton : Syncfusion.Windows.Forms.ButtonAdv
    {
        #region EVENTS
        public event EventHandler<IntegerButtonEventArgs> QuantityChanged;
        #endregion

        #region PRIVATE MEMBERS
        private IntegerTextBox _textBox = new IntegerTextBox();
        private Boolean _lastCharacterIsControl;
        #endregion

        #region CTOR
        public EditableIntegerButton()
        {
            // Button
            Size = new Size(EditableIntegerButton.DefaultWidth, EditableIntegerButton.DefaultHeight);
            MouseDown += ButtonMouseDown;
            MouseHover += ButtonMouseHover;
            MouseUp += ButtonMouseUp;
            KeyDown += ButtonKeyDown;

            // TextBox
            _textBox.MinValue = EditableIntegerButton.MinValue;
            _textBox.MaxValue = EditableIntegerButton.MaxValue;
            _textBox.Location = new Point(4, 4);
            _textBox.Size = new Size(Width - 8, Height);
            _textBox.Visible = false;
            _textBox.IntegerValue = 1;
            _textBox.TextAlign = HorizontalAlignment.Center;
            _textBox.Border3DStyle = Border3DStyle.Flat;
            _textBox.BorderStyle = BorderStyle.None;
            _textBox.ContextMenu = new ContextMenu();
            _textBox.KeyDown += TextBoxKeyDown;
            _textBox.MouseLeave += TextBoxMouseLeave;
            _textBox.Validated += TextBoxValidated;
            Controls.Add(_textBox);
        }
        #endregion

        #region PROPERTIES
        public static Int32 DefaultWidth
        {
            get { return 45; }
        }

        public static Int32 DefaultHeight
        {
            get { return 23; }
        }

        public static Int64 MinValue
        {
            get { return 1; }
        }

        public static Int64 MaxValue
        {
            get { return 99999; }
        }

        public Int64 Value
        {
            get { return _textBox.IntegerValue; }
            set 
            {
                if (value < MinValue)
                    value = MinValue;

                if (value > MaxValue)
                    value = MaxValue;

                _textBox.IntegerValue = value; 
            }
        }
        #endregion

        #region BUTTON MEMBERS
        /// <summary>
        /// If the last character pressed is the control character and the right mouse button is clicked then begin editing.
        /// </summary>
        /// <param name="sender">The ButtonAdv which contains the parameters.</param>
        /// <param name="e">The MouseEventArgs that provides the event data.</param>
        void ButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (_lastCharacterIsControl && e.Button == MouseButtons.Right)
                BeginEdit();
        }

        /// <summary>
        /// Fire the quantity changed event on release of the mouse.
        /// </summary>
        /// <param name="sender">The ButtonAdv which contains the parameters.</param>
        /// <param name="e">The MouseEventArgs that provides the event data.</param>
        void ButtonMouseUp(object sender, MouseEventArgs e)
        {
            IntegerButtonEventArgs ibea = new IntegerButtonEventArgs(_textBox.IntegerValue, e);

            if (QuantityChanged != null)
                QuantityChanged(this, ibea);
        }

        /// <summary>
        /// Key track of the last character pressed internally.
        /// </summary>
        /// <param name="sender">The ButtonAdv which contains the parameters.</param>
        /// <param name="e">The KeyEventArgs that provides the event data.</param>
        void ButtonKeyDown(object sender, KeyEventArgs e)
        {
            _lastCharacterIsControl = (e.Control) ? true : false;
        }

        /// <summary>
        /// Give the button focus when hovering over it.
        /// </summary>
        /// <param name="sender">The ButtonAdv which contains the parameters.</param>
        /// <param name="e">The EventArgs that provides the event data.</param>
        void ButtonMouseHover(object sender, EventArgs e)
        {
            Focus();
        }
        #endregion

        #region TEXTBOX MEMBERS
        /// <summary>
        /// If a key is pressed in the text box and it is determined to be the "Return" key then exit the edit session.
        /// </summary>
        /// <param name="sender">The TextBox which contains the parameters.</param>
        /// <param name="e">The KeyEventArgs that provides the event data.</param>
        void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                EndEdit();
        }

        /// <summary>
        /// If the mouse is moved out of the text box then exit the edit session.
        /// </summary>
        /// <param name="sender">The TextBox which contains the parameters.</param>
        /// <param name="e">The EventArgs that provides the event data.</param>
        void TextBoxMouseLeave(object sender, EventArgs e)
        {
            EndEdit();
        }

        /// <summary>
        /// Make sure that the value is in bounds.
        /// </summary>
        /// <param name="sender">The TextBox which contains the parameters.</param>
        /// <param name="e">The EventArgs that provides the event data.</param>
        void TextBoxValidated(object sender, EventArgs e)
        {
            if (_textBox.IntegerValue < MinValue)
                _textBox.IntegerValue = MinValue;

            if (_textBox.IntegerValue > MaxValue)
                _textBox.IntegerValue = MaxValue;
        }
        #endregion

        #region PRIVATE MEMBERS
        /// <summary>
        /// Call this method to begin the edit process for the text within the button.
        /// </summary>
        private void BeginEdit()
        {
            // Make the text box invisible to the user since the edit operation is starting.
            _textBox.Visible = true;

            // Give focus to the text box.
            _textBox.Focus();

            // This is here as I believe there is a bug in the Syncfusion code as at times
            // the context menu would pop-up even though this same operation is done in the
            // constructor to prevent the pop-up.
            _textBox.ContextMenu = new ContextMenu();

            // Set the cursor to let the user know they can edit the text.
            Cursor = Cursors.IBeam;

            // Reset the fact that the last character is not a control character.
            _lastCharacterIsControl = false;
        }

        /// <summary>
        /// Call this method to end the edit process for the text within the button.
        /// </summary>
        private void EndEdit()
        {
            // Make the text box invisible to the user since the edit operation is complete.
            _textBox.Visible = false;

            // Set the cursor to let the user know they the edit operation is complete.
            Cursor = Cursors.Arrow;

            // Reset the fact that the last character is not a control character.
            _lastCharacterIsControl = false;

            // Make sure the button retains focus.
            Focus();

            // Assign the button text the value of the last text box entry of the edit operation.
            // DO THIS STEP LAST:  After the visibility is set and the focus is changed, above, the
            // edit control validation will fire, which will do the final check to minValue/MaxValue.
            // If you don't do this last you'll find that some out of range values ( like zero ) could
            // sneak into the button label.  PCR 46703
            Text = _textBox.IntegerValue.ToString(CultureInfo.InvariantCulture);

        }
        #endregion
    }
    #endregion
}
