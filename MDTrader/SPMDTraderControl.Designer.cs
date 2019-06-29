namespace TT.SP.Trading.Controls.MDTrader
{
    partial class SPMDTraderControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            ClearPublishers();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.panelRightDockRibbon = new System.Windows.Forms.Panel();
            this.panelLeftDockedRibbon = new System.Windows.Forms.Panel();
            this.panelBottomDockedRibbon = new System.Windows.Forms.Panel();
            this.panelTopDockedRibbon = new System.Windows.Forms.Panel();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ribbonWheelVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ribbonWheelMain = new TT.SP.Trading.Controls.Ribbon.RibbonWheel();
            this.mdTraderControlMain = new TT.SP.Trading.Controls.MDTrader.MDTraderControl();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.ribbonWheelMain);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.mdTraderControlMain);
            this.splitContainerMain.Panel2.Controls.Add(this.panelRightDockRibbon);
            this.splitContainerMain.Panel2.Controls.Add(this.panelLeftDockedRibbon);
            this.splitContainerMain.Panel2.Controls.Add(this.panelBottomDockedRibbon);
            this.splitContainerMain.Panel2.Controls.Add(this.panelTopDockedRibbon);
            this.splitContainerMain.Size = new System.Drawing.Size(342, 532);
            this.splitContainerMain.TabIndex = 6;
            // 
            // panelRightDockRibbon
            // 
            this.panelRightDockRibbon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRightDockRibbon.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRightDockRibbon.Location = new System.Drawing.Point(297, 40);
            this.panelRightDockRibbon.Name = "panelRightDockRibbon";
            this.panelRightDockRibbon.Size = new System.Drawing.Size(45, 398);
            this.panelRightDockRibbon.TabIndex = 4;
            // 
            // panelLeftDockedRibbon
            // 
            this.panelLeftDockedRibbon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLeftDockedRibbon.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeftDockedRibbon.Location = new System.Drawing.Point(0, 40);
            this.panelLeftDockedRibbon.Name = "panelLeftDockedRibbon";
            this.panelLeftDockedRibbon.Size = new System.Drawing.Size(45, 398);
            this.panelLeftDockedRibbon.TabIndex = 3;
            // 
            // panelBottomDockedRibbon
            // 
            this.panelBottomDockedRibbon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBottomDockedRibbon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottomDockedRibbon.Location = new System.Drawing.Point(0, 438);
            this.panelBottomDockedRibbon.Name = "panelBottomDockedRibbon";
            this.panelBottomDockedRibbon.Size = new System.Drawing.Size(342, 40);
            this.panelBottomDockedRibbon.TabIndex = 2;
            // 
            // panelTopDockedRibbon
            // 
            this.panelTopDockedRibbon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTopDockedRibbon.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopDockedRibbon.Location = new System.Drawing.Point(0, 0);
            this.panelTopDockedRibbon.Name = "panelTopDockedRibbon";
            this.panelTopDockedRibbon.Size = new System.Drawing.Size(342, 40);
            this.panelTopDockedRibbon.TabIndex = 1;
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ribbonWheelVisibleToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(184, 26);
            // 
            // ribbonWheelVisibleToolStripMenuItem
            // 
            this.ribbonWheelVisibleToolStripMenuItem.Checked = true;
            this.ribbonWheelVisibleToolStripMenuItem.CheckOnClick = true;
            this.ribbonWheelVisibleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ribbonWheelVisibleToolStripMenuItem.Name = "ribbonWheelVisibleToolStripMenuItem";
            this.ribbonWheelVisibleToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.ribbonWheelVisibleToolStripMenuItem.Text = "Ribbon Wheel Visible";
            this.ribbonWheelVisibleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.ribbonWheelVisibleToolStripMenuItem_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler( Timer_Tick );
            this.timer1.Enabled = true;
            this.timer1.Interval = 2500;
            // 
            // ribbonWheelMain
            // 
            this.ribbonWheelMain.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ribbonWheelMain.CausesValidation = false;
            this.ribbonWheelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonWheelMain.Location = new System.Drawing.Point(0, 0);
            this.ribbonWheelMain.Name = "ribbonWheelMain";
            this.ribbonWheelMain.Size = new System.Drawing.Size(342, 50);
            this.ribbonWheelMain.TabIndex = 5;
            // 
            // mdTraderControlMain
            // 
            this.mdTraderControlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mdTraderControlMain.DisplayAccumLTQ = false;
            this.mdTraderControlMain.DisplayLTQ = true;
            this.mdTraderControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdTraderControlMain.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.mdTraderControlMain.ForceMonoRendering = false;
            this.mdTraderControlMain.Location = new System.Drawing.Point(45, 40);
            this.mdTraderControlMain.Name = "mdTraderControlMain";
            this.mdTraderControlMain.RowHeightFactor = 1.382;
            this.mdTraderControlMain.Size = new System.Drawing.Size(252, 398);
            this.mdTraderControlMain.TabIndex = 0;
            this.mdTraderControlMain.OrderHover += new TT.SP.Trading.Controls.MDTrader.MDTraderControl.OrderHoverEventHandler(this.MDTrader_OrderHover);
            this.mdTraderControlMain.PriceColumnClicked += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.MDTrader_PriceColumnClicked);
            this.mdTraderControlMain.EnabledChanged += new System.EventHandler(this.MDTrader_EnabledChanged);
            this.mdTraderControlMain.DeleteAllWorkingOrders += new System.EventHandler<System.EventArgs>(this.DeleteAllOrdersAction);
            this.mdTraderControlMain.Recenter += new TT.SP.Trading.Controls.MDTrader.MDTraderControl.RecenterEventHandler(this.RecenterAction);
            this.mdTraderControlMain.DeleteOrdersAtTagLevel += new TT.SP.Trading.Controls.MDTrader.MDTraderControl.DeleteOrdersAtTagLevelEventHandler(this.MDTrader_DeleteAllOrdersAtLevel);
            this.mdTraderControlMain.TradeOut += new System.EventHandler<System.EventArgs>(this.MDTrader_TradeOut);
            this.mdTraderControlMain.SendOrder += new TT.SP.Trading.Controls.MDTrader.MDTraderControl.SendOrderEventHandler(this.MDTrader_SendOrderRequest);
            this.mdTraderControlMain.DeleteAllWorkingOffers += new System.EventHandler<System.EventArgs>(this.DeleteAllOffersAction);
            this.mdTraderControlMain.ChangeOrdersAtLevel += new TT.SP.Trading.Controls.MDTrader.MDTraderControl.ChangeOrdersAtLevelEventHandler(this.MDTrader_ChangeOrdersAtLevel);
            this.mdTraderControlMain.DeleteAllWorkingBids += new System.EventHandler<System.EventArgs>(this.DeleteAllBidsAction);
            this.mdTraderControlMain.ShiftScroll += new TT.SP.Trading.Controls.MDTrader.MDTraderControl.ShiftScrollEventHandler(this.MDTrader_ShiftScroll);
            // 
            // SPMDTraderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerMain);
            this.Name = "SPMDTraderControl";
            this.Size = new System.Drawing.Size(342, 532);
            this.Load += new System.EventHandler(this.SPMDTraderControl_Load);
            this.Resize += new System.EventHandler(this.SPMDTraderControl_Resize);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MDTraderControl mdTraderControlMain;
        private TT.SP.Trading.Controls.Ribbon.RibbonWheel ribbonWheelMain;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.Panel panelTopDockedRibbon;
        private System.Windows.Forms.Panel panelBottomDockedRibbon;
        private System.Windows.Forms.Panel panelRightDockRibbon;
        private System.Windows.Forms.Panel panelLeftDockedRibbon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem ribbonWheelVisibleToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}
