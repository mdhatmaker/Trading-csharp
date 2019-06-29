namespace CopperHedge
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblProductA = new System.Windows.Forms.Label();
            this.orderSetViewA = new System.Windows.Forms.ListView();
            this.colOrderLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderContract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderSide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProductB = new System.Windows.Forms.Label();
            this.chkLockScroll = new System.Windows.Forms.CheckBox();
            this.numScrollB = new System.Windows.Forms.NumericUpDown();
            this.numScrollA = new System.Windows.Forms.NumericUpDown();
            this.orderSetViewB = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GridsPanel = new System.Windows.Forms.Panel();
            this.GridPanelB = new System.Windows.Forms.Panel();
            this.gridProductB = new System.Windows.Forms.DataGridView();
            this.ColOrdersB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBidB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriceB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAskB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTradeB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPanelA = new System.Windows.Forms.Panel();
            this.listMessage = new System.Windows.Forms.ListView();
            this.colMessages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.chkDelay = new System.Windows.Forms.CheckBox();
            this.numDelay = new System.Windows.Forms.NumericUpDown();
            this.chkUpdateInsideMkt = new System.Windows.Forms.CheckBox();
            this.chkUpdateDepth = new System.Windows.Forms.CheckBox();
            this.chkDeleteOnly = new System.Windows.Forms.CheckBox();
            this.lblPositionOffset = new System.Windows.Forms.Label();
            this.lblPositionOffsetText2 = new System.Windows.Forms.Label();
            this.numPositionOffset = new System.Windows.Forms.NumericUpDown();
            this.lblPositionOffsetText1 = new System.Windows.Forms.Label();
            this.chkRequote = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mdTraderControl1 = new HatMDTrader.MDTraderControl();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollA)).BeginInit();
            this.GridsPanel.SuspendLayout();
            this.GridPanelB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductB)).BeginInit();
            this.GridPanelA.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.inputPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPositionOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductA
            // 
            this.lblProductA.AllowDrop = true;
            this.lblProductA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductA.Location = new System.Drawing.Point(8, 36);
            this.lblProductA.Name = "lblProductA";
            this.lblProductA.Size = new System.Drawing.Size(376, 27);
            this.lblProductA.TabIndex = 2;
            this.lblProductA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // orderSetViewA
            // 
            this.orderSetViewA.AllowDrop = true;
            this.orderSetViewA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderSetViewA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrderLetter,
            this.colStatus,
            this.colOrderContract,
            this.colOrderSide,
            this.colOrderQuantity,
            this.colOrderPrice});
            this.orderSetViewA.Location = new System.Drawing.Point(12, 446);
            this.orderSetViewA.Name = "orderSetViewA";
            this.orderSetViewA.Size = new System.Drawing.Size(372, 122);
            this.orderSetViewA.TabIndex = 3;
            this.orderSetViewA.UseCompatibleStateImageBehavior = false;
            this.orderSetViewA.View = System.Windows.Forms.View.Details;
            // 
            // colOrderLetter
            // 
            this.colOrderLetter.Text = "A-Z";
            this.colOrderLetter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colOrderLetter.Width = 30;
            // 
            // colStatus
            // 
            this.colStatus.Text = "TT Status";
            this.colStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // colOrderContract
            // 
            this.colOrderContract.Text = "Contract";
            this.colOrderContract.Width = 110;
            // 
            // colOrderSide
            // 
            this.colOrderSide.Text = "Side";
            this.colOrderSide.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colOrderSide.Width = 50;
            // 
            // colOrderQuantity
            // 
            this.colOrderQuantity.Text = "Qty";
            this.colOrderQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOrderQuantity.Width = 30;
            // 
            // colOrderPrice
            // 
            this.colOrderPrice.Text = "Price";
            this.colOrderPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOrderPrice.Width = 65;
            // 
            // lblProductB
            // 
            this.lblProductB.AllowDrop = true;
            this.lblProductB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductB.Location = new System.Drawing.Point(391, 36);
            this.lblProductB.Name = "lblProductB";
            this.lblProductB.Size = new System.Drawing.Size(376, 27);
            this.lblProductB.TabIndex = 5;
            this.lblProductB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkLockScroll
            // 
            this.chkLockScroll.AllowDrop = true;
            this.chkLockScroll.AutoSize = true;
            this.chkLockScroll.Location = new System.Drawing.Point(351, 46);
            this.chkLockScroll.Name = "chkLockScroll";
            this.chkLockScroll.Size = new System.Drawing.Size(73, 17);
            this.chkLockScroll.TabIndex = 6;
            this.chkLockScroll.Text = "lock scroll";
            this.chkLockScroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkLockScroll.UseVisualStyleBackColor = true;
            // 
            // numScrollB
            // 
            this.numScrollB.Location = new System.Drawing.Point(430, 43);
            this.numScrollB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numScrollB.Name = "numScrollB";
            this.numScrollB.Size = new System.Drawing.Size(34, 20);
            this.numScrollB.TabIndex = 7;
            this.numScrollB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numScrollA
            // 
            this.numScrollA.Location = new System.Drawing.Point(302, 43);
            this.numScrollA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numScrollA.Name = "numScrollA";
            this.numScrollA.Size = new System.Drawing.Size(34, 20);
            this.numScrollA.TabIndex = 8;
            this.numScrollA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // orderSetViewB
            // 
            this.orderSetViewB.AllowDrop = true;
            this.orderSetViewB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderSetViewB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.orderSetViewB.Location = new System.Drawing.Point(394, 446);
            this.orderSetViewB.Name = "orderSetViewB";
            this.orderSetViewB.Size = new System.Drawing.Size(372, 122);
            this.orderSetViewB.TabIndex = 9;
            this.orderSetViewB.UseCompatibleStateImageBehavior = false;
            this.orderSetViewB.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "A-Z";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "TT Status";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Contract";
            this.columnHeader3.Width = 110;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Side";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Qty";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader5.Width = 30;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Price";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 65;
            // 
            // GridsPanel
            // 
            this.GridsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridsPanel.Controls.Add(this.GridPanelB);
            this.GridsPanel.Controls.Add(this.GridPanelA);
            this.GridsPanel.Location = new System.Drawing.Point(12, 69);
            this.GridsPanel.Name = "GridsPanel";
            this.GridsPanel.Size = new System.Drawing.Size(754, 346);
            this.GridsPanel.TabIndex = 15;
            // 
            // GridPanelB
            // 
            this.GridPanelB.Controls.Add(this.gridProductB);
            this.GridPanelB.Dock = System.Windows.Forms.DockStyle.Right;
            this.GridPanelB.Location = new System.Drawing.Point(382, 0);
            this.GridPanelB.Name = "GridPanelB";
            this.GridPanelB.Size = new System.Drawing.Size(372, 346);
            this.GridPanelB.TabIndex = 6;
            // 
            // gridProductB
            // 
            this.gridProductB.AllowUserToAddRows = false;
            this.gridProductB.AllowUserToDeleteRows = false;
            this.gridProductB.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProductB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProductB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrdersB,
            this.ColBidB,
            this.ColPriceB,
            this.ColAskB,
            this.ColTradeB});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProductB.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridProductB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProductB.Location = new System.Drawing.Point(0, 0);
            this.gridProductB.Name = "gridProductB";
            this.gridProductB.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductB.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gridProductB.RowHeadersVisible = false;
            this.gridProductB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProductB.Size = new System.Drawing.Size(372, 346);
            this.gridProductB.TabIndex = 0;
            // 
            // ColOrdersB
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColOrdersB.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColOrdersB.HeaderText = "Orders";
            this.ColOrdersB.Name = "ColOrdersB";
            this.ColOrdersB.ReadOnly = true;
            this.ColOrdersB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColOrdersB.Width = 80;
            // 
            // ColBidB
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.ColBidB.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColBidB.HeaderText = "Bid";
            this.ColBidB.Name = "ColBidB";
            this.ColBidB.ReadOnly = true;
            this.ColBidB.Width = 60;
            // 
            // ColPriceB
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.ColPriceB.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColPriceB.HeaderText = "Price";
            this.ColPriceB.Name = "ColPriceB";
            this.ColPriceB.ReadOnly = true;
            this.ColPriceB.Width = 80;
            // 
            // ColAskB
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.ColAskB.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColAskB.HeaderText = "Ask";
            this.ColAskB.Name = "ColAskB";
            this.ColAskB.ReadOnly = true;
            this.ColAskB.Width = 60;
            // 
            // ColTradeB
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColTradeB.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColTradeB.HeaderText = "Trade";
            this.ColTradeB.Name = "ColTradeB";
            this.ColTradeB.ReadOnly = true;
            this.ColTradeB.Width = 60;
            // 
            // GridPanelA
            // 
            this.GridPanelA.Controls.Add(this.mdTraderControl1);
            this.GridPanelA.Dock = System.Windows.Forms.DockStyle.Left;
            this.GridPanelA.Location = new System.Drawing.Point(0, 0);
            this.GridPanelA.Name = "GridPanelA";
            this.GridPanelA.Size = new System.Drawing.Size(372, 346);
            this.GridPanelA.TabIndex = 5;
            // 
            // listMessage
            // 
            this.listMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listMessage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessages});
            this.listMessage.Location = new System.Drawing.Point(12, 574);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(754, 97);
            this.listMessage.TabIndex = 16;
            this.listMessage.UseCompatibleStateImageBehavior = false;
            this.listMessage.View = System.Windows.Forms.View.Details;
            // 
            // colMessages
            // 
            this.colMessages.Text = "Messages";
            this.colMessages.Width = 721;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(779, 24);
            this.menuStrip.TabIndex = 17;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instrumentsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // instrumentsToolStripMenuItem
            // 
            this.instrumentsToolStripMenuItem.Name = "instrumentsToolStripMenuItem";
            this.instrumentsToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.instrumentsToolStripMenuItem.Text = "&Instruments";
            this.instrumentsToolStripMenuItem.Click += new System.EventHandler(this.instrumentsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // inputPanel
            // 
            this.inputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.inputPanel.Controls.Add(this.chkDelay);
            this.inputPanel.Controls.Add(this.numDelay);
            this.inputPanel.Controls.Add(this.chkUpdateInsideMkt);
            this.inputPanel.Controls.Add(this.chkUpdateDepth);
            this.inputPanel.Controls.Add(this.chkDeleteOnly);
            this.inputPanel.Controls.Add(this.lblPositionOffset);
            this.inputPanel.Controls.Add(this.lblPositionOffsetText2);
            this.inputPanel.Controls.Add(this.numPositionOffset);
            this.inputPanel.Controls.Add(this.lblPositionOffsetText1);
            this.inputPanel.Controls.Add(this.chkRequote);
            this.inputPanel.Location = new System.Drawing.Point(12, 420);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(754, 24);
            this.inputPanel.TabIndex = 18;
            // 
            // chkDelay
            // 
            this.chkDelay.AutoSize = true;
            this.chkDelay.Location = new System.Drawing.Point(83, 3);
            this.chkDelay.Name = "chkDelay";
            this.chkDelay.Size = new System.Drawing.Size(51, 17);
            this.chkDelay.TabIndex = 25;
            this.chkDelay.Text = "delay";
            this.chkDelay.UseVisualStyleBackColor = true;
            // 
            // numDelay
            // 
            this.numDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numDelay.Location = new System.Drawing.Point(139, 3);
            this.numDelay.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numDelay.Name = "numDelay";
            this.numDelay.Size = new System.Drawing.Size(52, 20);
            this.numDelay.TabIndex = 24;
            this.numDelay.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // chkUpdateInsideMkt
            // 
            this.chkUpdateInsideMkt.AutoSize = true;
            this.chkUpdateInsideMkt.Checked = true;
            this.chkUpdateInsideMkt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateInsideMkt.Location = new System.Drawing.Point(552, 4);
            this.chkUpdateInsideMkt.Name = "chkUpdateInsideMkt";
            this.chkUpdateInsideMkt.Size = new System.Drawing.Size(109, 17);
            this.chkUpdateInsideMkt.TabIndex = 22;
            this.chkUpdateInsideMkt.Text = "update inside mkt";
            this.chkUpdateInsideMkt.UseVisualStyleBackColor = true;
            // 
            // chkUpdateDepth
            // 
            this.chkUpdateDepth.AutoSize = true;
            this.chkUpdateDepth.Location = new System.Drawing.Point(667, 4);
            this.chkUpdateDepth.Name = "chkUpdateDepth";
            this.chkUpdateDepth.Size = new System.Drawing.Size(89, 17);
            this.chkUpdateDepth.TabIndex = 21;
            this.chkUpdateDepth.Text = "update depth";
            this.chkUpdateDepth.UseVisualStyleBackColor = true;
            // 
            // chkDeleteOnly
            // 
            this.chkDeleteOnly.AutoSize = true;
            this.chkDeleteOnly.Location = new System.Drawing.Point(0, 4);
            this.chkDeleteOnly.Name = "chkDeleteOnly";
            this.chkDeleteOnly.Size = new System.Drawing.Size(77, 17);
            this.chkDeleteOnly.TabIndex = 20;
            this.chkDeleteOnly.Text = "delete only";
            this.chkDeleteOnly.UseVisualStyleBackColor = true;
            this.chkDeleteOnly.CheckedChanged += new System.EventHandler(this.chkDeleteOnly_CheckedChanged);
            // 
            // lblPositionOffset
            // 
            this.lblPositionOffset.AutoSize = true;
            this.lblPositionOffset.Location = new System.Drawing.Point(524, 5);
            this.lblPositionOffset.Name = "lblPositionOffset";
            this.lblPositionOffset.Size = new System.Drawing.Size(13, 13);
            this.lblPositionOffset.TabIndex = 19;
            this.lblPositionOffset.Text = "0";
            // 
            // lblPositionOffsetText2
            // 
            this.lblPositionOffsetText2.Location = new System.Drawing.Point(462, 5);
            this.lblPositionOffsetText2.Name = "lblPositionOffsetText2";
            this.lblPositionOffsetText2.Size = new System.Drawing.Size(65, 19);
            this.lblPositionOffsetText2.TabIndex = 18;
            this.lblPositionOffsetText2.Text = "pos offset:";
            this.lblPositionOffsetText2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numPositionOffset
            // 
            this.numPositionOffset.Location = new System.Drawing.Point(422, 3);
            this.numPositionOffset.Name = "numPositionOffset";
            this.numPositionOffset.Size = new System.Drawing.Size(35, 20);
            this.numPositionOffset.TabIndex = 17;
            // 
            // lblPositionOffsetText1
            // 
            this.lblPositionOffsetText1.AutoSize = true;
            this.lblPositionOffsetText1.Location = new System.Drawing.Point(300, 7);
            this.lblPositionOffsetText1.Name = "lblPositionOffsetText1";
            this.lblPositionOffsetText1.Size = new System.Drawing.Size(116, 13);
            this.lblPositionOffsetText1.TabIndex = 16;
            this.lblPositionOffsetText1.Text = "if pos offset is less than";
            // 
            // chkRequote
            // 
            this.chkRequote.AutoSize = true;
            this.chkRequote.Location = new System.Drawing.Point(206, 6);
            this.chkRequote.Name = "chkRequote";
            this.chkRequote.Size = new System.Drawing.Size(97, 17);
            this.chkRequote.TabIndex = 15;
            this.chkRequote.Text = "requote spread";
            this.chkRequote.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mdTraderControl1
            // 
            this.mdTraderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdTraderControl1.Location = new System.Drawing.Point(0, 0);
            this.mdTraderControl1.Name = "mdTraderControl1";
            this.mdTraderControl1.Size = new System.Drawing.Size(372, 346);
            this.mdTraderControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 674);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.GridsPanel);
            this.Controls.Add(this.orderSetViewB);
            this.Controls.Add(this.numScrollA);
            this.Controls.Add(this.numScrollB);
            this.Controls.Add(this.chkLockScroll);
            this.Controls.Add(this.lblProductB);
            this.Controls.Add(this.orderSetViewA);
            this.Controls.Add(this.lblProductA);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HatMDTrader Test";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numScrollB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollA)).EndInit();
            this.GridsPanel.ResumeLayout(false);
            this.GridPanelB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProductB)).EndInit();
            this.GridPanelA.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPositionOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductA;
        private System.Windows.Forms.ListView orderSetViewA;
        private System.Windows.Forms.ColumnHeader colOrderContract;
        private System.Windows.Forms.ColumnHeader colOrderSide;
        private System.Windows.Forms.ColumnHeader colOrderQuantity;
        private System.Windows.Forms.ColumnHeader colOrderPrice;
        private System.Windows.Forms.ColumnHeader colOrderLetter;
        private System.Windows.Forms.Label lblProductB;
        private System.Windows.Forms.CheckBox chkLockScroll;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.NumericUpDown numScrollB;
        private System.Windows.Forms.NumericUpDown numScrollA;
        private System.Windows.Forms.ListView orderSetViewB;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel GridsPanel;
        private System.Windows.Forms.Panel GridPanelB;
        private System.Windows.Forms.DataGridView gridProductB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdersB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBidB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPriceB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAskB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTradeB;
        private System.Windows.Forms.Panel GridPanelA;
        private System.Windows.Forms.ListView listMessage;
        private System.Windows.Forms.ColumnHeader colMessages;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Label lblPositionOffset;
        private System.Windows.Forms.Label lblPositionOffsetText2;
        private System.Windows.Forms.NumericUpDown numPositionOffset;
        private System.Windows.Forms.Label lblPositionOffsetText1;
        private System.Windows.Forms.CheckBox chkRequote;
        private System.Windows.Forms.CheckBox chkDeleteOnly;
        private System.Windows.Forms.CheckBox chkUpdateDepth;
        private System.Windows.Forms.CheckBox chkUpdateInsideMkt;
        private System.Windows.Forms.CheckBox chkDelay;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.Timer timer1;
        private HatMDTrader.MDTraderControl mdTraderControl1;
    }
}

