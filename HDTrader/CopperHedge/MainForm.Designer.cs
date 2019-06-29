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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle50 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle51 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle46 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle47 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle48 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle49 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatusProduct1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusProduct2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusNetPL = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitBottom = new System.Windows.Forms.SplitContainer();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.splitOrders = new System.Windows.Forms.SplitContainer();
            this.orderSetViewA = new System.Windows.Forms.ListView();
            this.colOrderLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderContract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderSide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderSetViewB = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelBottomInput = new System.Windows.Forms.Panel();
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
            this.listMessage = new System.Windows.Forms.ListView();
            this.colMessages = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelGrids = new System.Windows.Forms.Panel();
            this.splitGrids = new System.Windows.Forms.SplitContainer();
            this.gridProductA = new System.Windows.Forms.DataGridView();
            this.ColOrdersA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBidA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriceA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAskA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTradeA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridProductB = new System.Windows.Forms.DataGridView();
            this.ColOrdersB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBidB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriceB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAskB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTradeB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTopInput = new System.Windows.Forms.Panel();
            this.numScrollA = new System.Windows.Forms.NumericUpDown();
            this.numScrollB = new System.Windows.Forms.NumericUpDown();
            this.chkLockScroll = new System.Windows.Forms.CheckBox();
            this.lblProductB = new System.Windows.Forms.Label();
            this.lblProductA = new System.Windows.Forms.Label();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sendFillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).BeginInit();
            this.splitBottom.Panel1.SuspendLayout();
            this.splitBottom.Panel2.SuspendLayout();
            this.splitBottom.SuspendLayout();
            this.panelOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitOrders)).BeginInit();
            this.splitOrders.Panel1.SuspendLayout();
            this.splitOrders.Panel2.SuspendLayout();
            this.splitOrders.SuspendLayout();
            this.panelBottomInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPositionOffset)).BeginInit();
            this.panelGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGrids)).BeginInit();
            this.splitGrids.Panel1.SuspendLayout();
            this.splitGrids.Panel2.SuspendLayout();
            this.splitGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductB)).BeginInit();
            this.panelTopInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollB)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(778, 24);
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
            this.instrumentsToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.toolStripSeparator1,
            this.sendFillsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // instrumentsToolStripMenuItem
            // 
            this.instrumentsToolStripMenuItem.Name = "instrumentsToolStripMenuItem";
            this.instrumentsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.instrumentsToolStripMenuItem.Text = "&Instruments...";
            this.instrumentsToolStripMenuItem.Click += new System.EventHandler(this.instrumentsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusProduct1,
            this.lblStatusProduct2,
            this.lblStatusNetPL});
            this.statusStrip1.Location = new System.Drawing.Point(0, 664);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(778, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusProduct1
            // 
            this.lblStatusProduct1.AutoSize = false;
            this.lblStatusProduct1.Name = "lblStatusProduct1";
            this.lblStatusProduct1.Size = new System.Drawing.Size(208, 17);
            this.lblStatusProduct1.Text = "(netpos and PL$)";
            this.lblStatusProduct1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusProduct2
            // 
            this.lblStatusProduct2.AutoSize = false;
            this.lblStatusProduct2.Name = "lblStatusProduct2";
            this.lblStatusProduct2.Size = new System.Drawing.Size(208, 17);
            this.lblStatusProduct2.Text = "(netpos and PL$)";
            this.lblStatusProduct2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatusNetPL
            // 
            this.lblStatusNetPL.AutoSize = false;
            this.lblStatusNetPL.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusNetPL.Name = "lblStatusNetPL";
            this.lblStatusNetPL.Size = new System.Drawing.Size(118, 17);
            this.lblStatusNetPL.Text = "(net PL$)";
            this.lblStatusNetPL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 24);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.panelGrids);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitBottom);
            this.splitMain.Size = new System.Drawing.Size(778, 640);
            this.splitMain.SplitterDistance = 354;
            this.splitMain.TabIndex = 26;
            // 
            // splitBottom
            // 
            this.splitBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBottom.Location = new System.Drawing.Point(0, 0);
            this.splitBottom.Name = "splitBottom";
            this.splitBottom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitBottom.Panel1
            // 
            this.splitBottom.Panel1.Controls.Add(this.panelOrders);
            // 
            // splitBottom.Panel2
            // 
            this.splitBottom.Panel2.Controls.Add(this.listMessage);
            this.splitBottom.Size = new System.Drawing.Size(778, 282);
            this.splitBottom.SplitterDistance = 140;
            this.splitBottom.TabIndex = 24;
            // 
            // panelOrders
            // 
            this.panelOrders.Controls.Add(this.splitOrders);
            this.panelOrders.Controls.Add(this.panelBottomInput);
            this.panelOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrders.Location = new System.Drawing.Point(0, 0);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(778, 140);
            this.panelOrders.TabIndex = 23;
            // 
            // splitOrders
            // 
            this.splitOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitOrders.Location = new System.Drawing.Point(0, 24);
            this.splitOrders.Name = "splitOrders";
            // 
            // splitOrders.Panel1
            // 
            this.splitOrders.Panel1.Controls.Add(this.orderSetViewA);
            // 
            // splitOrders.Panel2
            // 
            this.splitOrders.Panel2.Controls.Add(this.orderSetViewB);
            this.splitOrders.Size = new System.Drawing.Size(778, 116);
            this.splitOrders.SplitterDistance = 388;
            this.splitOrders.TabIndex = 22;
            // 
            // orderSetViewA
            // 
            this.orderSetViewA.AllowDrop = true;
            this.orderSetViewA.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrderLetter,
            this.colStatus,
            this.colOrderContract,
            this.colOrderSide,
            this.colOrderQuantity,
            this.colOrderPrice});
            this.orderSetViewA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderSetViewA.Location = new System.Drawing.Point(0, 0);
            this.orderSetViewA.Name = "orderSetViewA";
            this.orderSetViewA.Size = new System.Drawing.Size(388, 116);
            this.orderSetViewA.TabIndex = 5;
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
            // orderSetViewB
            // 
            this.orderSetViewB.AllowDrop = true;
            this.orderSetViewB.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.orderSetViewB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderSetViewB.Location = new System.Drawing.Point(0, 0);
            this.orderSetViewB.Name = "orderSetViewB";
            this.orderSetViewB.Size = new System.Drawing.Size(386, 116);
            this.orderSetViewB.TabIndex = 11;
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
            // panelBottomInput
            // 
            this.panelBottomInput.Controls.Add(this.chkDelay);
            this.panelBottomInput.Controls.Add(this.numDelay);
            this.panelBottomInput.Controls.Add(this.chkUpdateInsideMkt);
            this.panelBottomInput.Controls.Add(this.chkUpdateDepth);
            this.panelBottomInput.Controls.Add(this.chkDeleteOnly);
            this.panelBottomInput.Controls.Add(this.lblPositionOffset);
            this.panelBottomInput.Controls.Add(this.lblPositionOffsetText2);
            this.panelBottomInput.Controls.Add(this.numPositionOffset);
            this.panelBottomInput.Controls.Add(this.lblPositionOffsetText1);
            this.panelBottomInput.Controls.Add(this.chkRequote);
            this.panelBottomInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBottomInput.Location = new System.Drawing.Point(0, 0);
            this.panelBottomInput.Name = "panelBottomInput";
            this.panelBottomInput.Size = new System.Drawing.Size(778, 24);
            this.panelBottomInput.TabIndex = 21;
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
            1000,
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
            // listMessage
            // 
            this.listMessage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMessages});
            this.listMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMessage.Location = new System.Drawing.Point(0, 0);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(778, 138);
            this.listMessage.TabIndex = 17;
            this.listMessage.UseCompatibleStateImageBehavior = false;
            this.listMessage.View = System.Windows.Forms.View.Details;
            // 
            // colMessages
            // 
            this.colMessages.Text = "Messages";
            this.colMessages.Width = 721;
            // 
            // panelGrids
            // 
            this.panelGrids.Controls.Add(this.splitGrids);
            this.panelGrids.Controls.Add(this.panelTopInput);
            this.panelGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrids.Location = new System.Drawing.Point(0, 0);
            this.panelGrids.Name = "panelGrids";
            this.panelGrids.Size = new System.Drawing.Size(778, 354);
            this.panelGrids.TabIndex = 26;
            // 
            // splitGrids
            // 
            this.splitGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGrids.Location = new System.Drawing.Point(0, 33);
            this.splitGrids.Name = "splitGrids";
            // 
            // splitGrids.Panel1
            // 
            this.splitGrids.Panel1.Controls.Add(this.gridProductA);
            // 
            // splitGrids.Panel2
            // 
            this.splitGrids.Panel2.Controls.Add(this.gridProductB);
            this.splitGrids.Size = new System.Drawing.Size(778, 321);
            this.splitGrids.SplitterDistance = 390;
            this.splitGrids.TabIndex = 26;
            // 
            // gridProductA
            // 
            this.gridProductA.AllowUserToAddRows = false;
            this.gridProductA.AllowUserToDeleteRows = false;
            this.gridProductA.AllowUserToResizeRows = false;
            this.gridProductA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.gridProductA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProductA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrdersA,
            this.ColBidA,
            this.ColPriceA,
            this.ColAskA,
            this.ColTradeA});
            dataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle41.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle41.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProductA.DefaultCellStyle = dataGridViewCellStyle41;
            this.gridProductA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProductA.Location = new System.Drawing.Point(0, 0);
            this.gridProductA.Name = "gridProductA";
            this.gridProductA.ReadOnly = true;
            dataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle42.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle42.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle42.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle42.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle42.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductA.RowHeadersDefaultCellStyle = dataGridViewCellStyle42;
            this.gridProductA.RowHeadersVisible = false;
            dataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridProductA.RowsDefaultCellStyle = dataGridViewCellStyle43;
            this.gridProductA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProductA.ShowCellToolTips = false;
            this.gridProductA.Size = new System.Drawing.Size(390, 321);
            this.gridProductA.TabIndex = 1;
            // 
            // ColOrdersA
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColOrdersA.DefaultCellStyle = dataGridViewCellStyle36;
            this.ColOrdersA.HeaderText = "Orders";
            this.ColOrdersA.Name = "ColOrdersA";
            this.ColOrdersA.ReadOnly = true;
            this.ColOrdersA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColBidA
            // 
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle37.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.White;
            this.ColBidA.DefaultCellStyle = dataGridViewCellStyle37;
            this.ColBidA.HeaderText = "Bid";
            this.ColBidA.Name = "ColBidA";
            this.ColBidA.ReadOnly = true;
            // 
            // ColPriceA
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.White;
            this.ColPriceA.DefaultCellStyle = dataGridViewCellStyle38;
            this.ColPriceA.HeaderText = "Price";
            this.ColPriceA.Name = "ColPriceA";
            this.ColPriceA.ReadOnly = true;
            // 
            // ColAskA
            // 
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle39.ForeColor = System.Drawing.Color.Black;
            this.ColAskA.DefaultCellStyle = dataGridViewCellStyle39;
            this.ColAskA.HeaderText = "Ask";
            this.ColAskA.Name = "ColAskA";
            this.ColAskA.ReadOnly = true;
            // 
            // ColTradeA
            // 
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColTradeA.DefaultCellStyle = dataGridViewCellStyle40;
            this.ColTradeA.HeaderText = "Trade";
            this.ColTradeA.Name = "ColTradeA";
            this.ColTradeA.ReadOnly = true;
            // 
            // gridProductB
            // 
            this.gridProductB.AllowUserToAddRows = false;
            this.gridProductB.AllowUserToDeleteRows = false;
            this.gridProductB.AllowUserToResizeRows = false;
            this.gridProductB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle44.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle44.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle44;
            this.gridProductB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProductB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrdersB,
            this.ColBidB,
            this.ColPriceB,
            this.ColAskB,
            this.ColTradeB});
            dataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle50.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle50.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle50.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridProductB.DefaultCellStyle = dataGridViewCellStyle50;
            this.gridProductB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProductB.Location = new System.Drawing.Point(0, 0);
            this.gridProductB.Name = "gridProductB";
            this.gridProductB.ReadOnly = true;
            dataGridViewCellStyle51.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle51.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle51.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle51.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductB.RowHeadersDefaultCellStyle = dataGridViewCellStyle51;
            this.gridProductB.RowHeadersVisible = false;
            this.gridProductB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProductB.ShowCellToolTips = false;
            this.gridProductB.Size = new System.Drawing.Size(384, 321);
            this.gridProductB.TabIndex = 1;
            // 
            // ColOrdersB
            // 
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColOrdersB.DefaultCellStyle = dataGridViewCellStyle45;
            this.ColOrdersB.HeaderText = "Orders";
            this.ColOrdersB.Name = "ColOrdersB";
            this.ColOrdersB.ReadOnly = true;
            this.ColOrdersB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColBidB
            // 
            dataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle46.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle46.ForeColor = System.Drawing.Color.White;
            this.ColBidB.DefaultCellStyle = dataGridViewCellStyle46;
            this.ColBidB.HeaderText = "Bid";
            this.ColBidB.Name = "ColBidB";
            this.ColBidB.ReadOnly = true;
            // 
            // ColPriceB
            // 
            dataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle47.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle47.ForeColor = System.Drawing.Color.White;
            this.ColPriceB.DefaultCellStyle = dataGridViewCellStyle47;
            this.ColPriceB.HeaderText = "Price";
            this.ColPriceB.Name = "ColPriceB";
            this.ColPriceB.ReadOnly = true;
            // 
            // ColAskB
            // 
            dataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle48.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle48.ForeColor = System.Drawing.Color.Black;
            this.ColAskB.DefaultCellStyle = dataGridViewCellStyle48;
            this.ColAskB.HeaderText = "Ask";
            this.ColAskB.Name = "ColAskB";
            this.ColAskB.ReadOnly = true;
            // 
            // ColTradeB
            // 
            dataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColTradeB.DefaultCellStyle = dataGridViewCellStyle49;
            this.ColTradeB.HeaderText = "Trade";
            this.ColTradeB.Name = "ColTradeB";
            this.ColTradeB.ReadOnly = true;
            // 
            // panelTopInput
            // 
            this.panelTopInput.Controls.Add(this.numScrollA);
            this.panelTopInput.Controls.Add(this.numScrollB);
            this.panelTopInput.Controls.Add(this.chkLockScroll);
            this.panelTopInput.Controls.Add(this.lblProductB);
            this.panelTopInput.Controls.Add(this.lblProductA);
            this.panelTopInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopInput.Location = new System.Drawing.Point(0, 0);
            this.panelTopInput.Name = "panelTopInput";
            this.panelTopInput.Size = new System.Drawing.Size(778, 33);
            this.panelTopInput.TabIndex = 25;
            // 
            // numScrollA
            // 
            this.numScrollA.Location = new System.Drawing.Point(297, 7);
            this.numScrollA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numScrollA.Name = "numScrollA";
            this.numScrollA.Size = new System.Drawing.Size(34, 20);
            this.numScrollA.TabIndex = 13;
            this.numScrollA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numScrollB
            // 
            this.numScrollB.Location = new System.Drawing.Point(425, 7);
            this.numScrollB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numScrollB.Name = "numScrollB";
            this.numScrollB.Size = new System.Drawing.Size(34, 20);
            this.numScrollB.TabIndex = 12;
            this.numScrollB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkLockScroll
            // 
            this.chkLockScroll.AllowDrop = true;
            this.chkLockScroll.AutoSize = true;
            this.chkLockScroll.Location = new System.Drawing.Point(346, 10);
            this.chkLockScroll.Name = "chkLockScroll";
            this.chkLockScroll.Size = new System.Drawing.Size(73, 17);
            this.chkLockScroll.TabIndex = 11;
            this.chkLockScroll.Text = "lock scroll";
            this.chkLockScroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkLockScroll.UseVisualStyleBackColor = true;
            // 
            // lblProductB
            // 
            this.lblProductB.AllowDrop = true;
            this.lblProductB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductB.Location = new System.Drawing.Point(380, 0);
            this.lblProductB.Name = "lblProductB";
            this.lblProductB.Size = new System.Drawing.Size(376, 27);
            this.lblProductB.TabIndex = 10;
            this.lblProductB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblProductA
            // 
            this.lblProductA.AllowDrop = true;
            this.lblProductA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductA.Location = new System.Drawing.Point(3, 0);
            this.lblProductA.Name = "lblProductA";
            this.lblProductA.Size = new System.Drawing.Size(376, 27);
            this.lblProductA.TabIndex = 9;
            this.lblProductA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // sendFillsToolStripMenuItem
            // 
            this.sendFillsToolStripMenuItem.Checked = true;
            this.sendFillsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sendFillsToolStripMenuItem.Name = "sendFillsToolStripMenuItem";
            this.sendFillsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sendFillsToolStripMenuItem.Text = "&Send Fills";
            this.sendFillsToolStripMenuItem.Click += new System.EventHandler(this.sendFillsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 686);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copper Hedger";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitBottom.Panel1.ResumeLayout(false);
            this.splitBottom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitBottom)).EndInit();
            this.splitBottom.ResumeLayout(false);
            this.panelOrders.ResumeLayout(false);
            this.splitOrders.Panel1.ResumeLayout(false);
            this.splitOrders.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitOrders)).EndInit();
            this.splitOrders.ResumeLayout(false);
            this.panelBottomInput.ResumeLayout(false);
            this.panelBottomInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPositionOffset)).EndInit();
            this.panelGrids.ResumeLayout(false);
            this.splitGrids.Panel1.ResumeLayout(false);
            this.splitGrids.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGrids)).EndInit();
            this.splitGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProductA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductB)).EndInit();
            this.panelTopInput.ResumeLayout(false);
            this.panelTopInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScrollB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusProduct1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusProduct2;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusNetPL;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel panelGrids;
        private System.Windows.Forms.SplitContainer splitGrids;
        private System.Windows.Forms.DataGridView gridProductA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdersA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBidA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPriceA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAskA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTradeA;
        private System.Windows.Forms.DataGridView gridProductB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdersB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBidB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPriceB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAskB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTradeB;
        private System.Windows.Forms.Panel panelTopInput;
        private System.Windows.Forms.NumericUpDown numScrollA;
        private System.Windows.Forms.NumericUpDown numScrollB;
        private System.Windows.Forms.CheckBox chkLockScroll;
        private System.Windows.Forms.Label lblProductB;
        private System.Windows.Forms.Label lblProductA;
        private System.Windows.Forms.SplitContainer splitBottom;
        private System.Windows.Forms.Panel panelOrders;
        private System.Windows.Forms.SplitContainer splitOrders;
        private System.Windows.Forms.ListView orderSetViewA;
        private System.Windows.Forms.ColumnHeader colOrderLetter;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colOrderContract;
        private System.Windows.Forms.ColumnHeader colOrderSide;
        private System.Windows.Forms.ColumnHeader colOrderQuantity;
        private System.Windows.Forms.ColumnHeader colOrderPrice;
        private System.Windows.Forms.ListView orderSetViewB;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Panel panelBottomInput;
        private System.Windows.Forms.CheckBox chkDelay;
        private System.Windows.Forms.NumericUpDown numDelay;
        private System.Windows.Forms.CheckBox chkUpdateInsideMkt;
        private System.Windows.Forms.CheckBox chkUpdateDepth;
        private System.Windows.Forms.CheckBox chkDeleteOnly;
        private System.Windows.Forms.Label lblPositionOffset;
        private System.Windows.Forms.Label lblPositionOffsetText2;
        private System.Windows.Forms.NumericUpDown numPositionOffset;
        private System.Windows.Forms.Label lblPositionOffsetText1;
        private System.Windows.Forms.CheckBox chkRequote;
        private System.Windows.Forms.ListView listMessage;
        private System.Windows.Forms.ColumnHeader colMessages;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sendFillsToolStripMenuItem;
    }
}

