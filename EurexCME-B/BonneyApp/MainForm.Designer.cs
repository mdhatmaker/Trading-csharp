namespace LagTrader
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridProductA = new System.Windows.Forms.DataGridView();
            this.GridPanelA = new System.Windows.Forms.Panel();
            this.lblProductA = new System.Windows.Forms.Label();
            this.orderSetView = new System.Windows.Forms.ListView();
            this.colOrderLetter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderContract = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderSide = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOrderPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDelay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTicks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProductB = new System.Windows.Forms.Label();
            this.GridPanelB = new System.Windows.Forms.Panel();
            this.gridProductB = new System.Windows.Forms.DataGridView();
            this.chkLockScroll = new System.Windows.Forms.CheckBox();
            this.ColOrdersB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBidB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriceB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAskB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTradeB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColOrdersA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBidA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriceA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAskA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTradeA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductA)).BeginInit();
            this.GridPanelA.SuspendLayout();
            this.GridPanelB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridProductB)).BeginInit();
            this.SuspendLayout();
            // 
            // gridProductA
            // 
            this.gridProductA.AllowUserToAddRows = false;
            this.gridProductA.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridProductA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProductA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrdersA,
            this.ColBidA,
            this.ColPriceA,
            this.ColAskA,
            this.ColTradeA});
            this.gridProductA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProductA.Location = new System.Drawing.Point(0, 0);
            this.gridProductA.Name = "gridProductA";
            this.gridProductA.ReadOnly = true;
            this.gridProductA.RowHeadersVisible = false;
            this.gridProductA.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProductA.Size = new System.Drawing.Size(372, 680);
            this.gridProductA.TabIndex = 0;
            this.gridProductA.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.b);
            this.gridProductA.Scroll += new System.Windows.Forms.ScrollEventHandler(this.gridProductA_Scroll);
            // 
            // GridPanelA
            // 
            this.GridPanelA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GridPanelA.Controls.Add(this.gridProductA);
            this.GridPanelA.Location = new System.Drawing.Point(12, 35);
            this.GridPanelA.Name = "GridPanelA";
            this.GridPanelA.Size = new System.Drawing.Size(372, 680);
            this.GridPanelA.TabIndex = 1;
            // 
            // lblProductA
            // 
            this.lblProductA.AllowDrop = true;
            this.lblProductA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductA.Location = new System.Drawing.Point(8, 5);
            this.lblProductA.Name = "lblProductA";
            this.lblProductA.Size = new System.Drawing.Size(376, 27);
            this.lblProductA.TabIndex = 2;
            this.lblProductA.Text = "(drop watch contract)";
            this.lblProductA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // orderSetView
            // 
            this.orderSetView.AllowDrop = true;
            this.orderSetView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderSetView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colOrderLetter,
            this.colOrderContract,
            this.colOrderSide,
            this.colOrderQuantity,
            this.colOrderPrice,
            this.colRule,
            this.colDelay,
            this.colTicks,
            this.colPercent});
            this.orderSetView.Location = new System.Drawing.Point(12, 721);
            this.orderSetView.Name = "orderSetView";
            this.orderSetView.Size = new System.Drawing.Size(752, 100);
            this.orderSetView.TabIndex = 3;
            this.orderSetView.UseCompatibleStateImageBehavior = false;
            this.orderSetView.View = System.Windows.Forms.View.Details;
            // 
            // colOrderLetter
            // 
            this.colOrderLetter.Text = "A-Z";
            this.colOrderLetter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colOrderLetter.Width = 35;
            // 
            // colOrderContract
            // 
            this.colOrderContract.Text = "Contract";
            this.colOrderContract.Width = 140;
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
            this.colOrderQuantity.Width = 40;
            // 
            // colOrderPrice
            // 
            this.colOrderPrice.Text = "Price";
            this.colOrderPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOrderPrice.Width = 65;
            // 
            // colRule
            // 
            this.colRule.Text = "Rule";
            this.colRule.Width = 120;
            // 
            // colDelay
            // 
            this.colDelay.Text = "Delay";
            // 
            // colTicks
            // 
            this.colTicks.Text = "Ticks";
            // 
            // colPercent
            // 
            this.colPercent.Text = "Percent";
            // 
            // lblProductB
            // 
            this.lblProductB.AllowDrop = true;
            this.lblProductB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductB.Location = new System.Drawing.Point(390, 5);
            this.lblProductB.Name = "lblProductB";
            this.lblProductB.Size = new System.Drawing.Size(376, 27);
            this.lblProductB.TabIndex = 5;
            this.lblProductB.Text = "(drop trade contract)";
            this.lblProductB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GridPanelB
            // 
            this.GridPanelB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.GridPanelB.Controls.Add(this.gridProductB);
            this.GridPanelB.Location = new System.Drawing.Point(394, 35);
            this.GridPanelB.Name = "GridPanelB";
            this.GridPanelB.Size = new System.Drawing.Size(372, 680);
            this.GridPanelB.TabIndex = 4;
            // 
            // gridProductB
            // 
            this.gridProductB.AllowUserToAddRows = false;
            this.gridProductB.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProductB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.gridProductB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProductB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrdersB,
            this.ColBidB,
            this.ColPriceB,
            this.ColAskB,
            this.ColTradeB});
            this.gridProductB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProductB.Location = new System.Drawing.Point(0, 0);
            this.gridProductB.Name = "gridProductB";
            this.gridProductB.ReadOnly = true;
            this.gridProductB.RowHeadersVisible = false;
            this.gridProductB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProductB.Size = new System.Drawing.Size(372, 680);
            this.gridProductB.TabIndex = 0;
            this.gridProductB.Scroll += new System.Windows.Forms.ScrollEventHandler(this.gridProductB_Scroll);
            // 
            // chkLockScroll
            // 
            this.chkLockScroll.AllowDrop = true;
            this.chkLockScroll.AutoSize = true;
            this.chkLockScroll.Location = new System.Drawing.Point(352, 12);
            this.chkLockScroll.Name = "chkLockScroll";
            this.chkLockScroll.Size = new System.Drawing.Size(73, 17);
            this.chkLockScroll.TabIndex = 6;
            this.chkLockScroll.Text = "lock scroll";
            this.chkLockScroll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkLockScroll.UseVisualStyleBackColor = true;
            // 
            // ColOrdersB
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColOrdersB.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColOrdersB.HeaderText = "Orders";
            this.ColOrdersB.Name = "ColOrdersB";
            this.ColOrdersB.ReadOnly = true;
            this.ColOrdersB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColOrdersB.Width = 80;
            // 
            // ColBidB
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColBidB.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColBidB.HeaderText = "Bid";
            this.ColBidB.Name = "ColBidB";
            this.ColBidB.ReadOnly = true;
            this.ColBidB.Width = 60;
            // 
            // ColPriceB
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColPriceB.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColPriceB.HeaderText = "Price";
            this.ColPriceB.Name = "ColPriceB";
            this.ColPriceB.ReadOnly = true;
            this.ColPriceB.Width = 80;
            // 
            // ColAskB
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ColAskB.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColAskB.HeaderText = "Ask";
            this.ColAskB.Name = "ColAskB";
            this.ColAskB.ReadOnly = true;
            this.ColAskB.Width = 60;
            // 
            // ColTradeB
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColTradeB.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColTradeB.HeaderText = "Trade";
            this.ColTradeB.Name = "ColTradeB";
            this.ColTradeB.ReadOnly = true;
            this.ColTradeB.Width = 60;
            // 
            // ColOrdersA
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColOrdersA.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColOrdersA.HeaderText = "Orders";
            this.ColOrdersA.Name = "ColOrdersA";
            this.ColOrdersA.ReadOnly = true;
            this.ColOrdersA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColOrdersA.Width = 80;
            // 
            // ColBidA
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColBidA.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColBidA.HeaderText = "Bid";
            this.ColBidA.Name = "ColBidA";
            this.ColBidA.ReadOnly = true;
            this.ColBidA.Width = 60;
            // 
            // ColPriceA
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColPriceA.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColPriceA.HeaderText = "Price";
            this.ColPriceA.Name = "ColPriceA";
            this.ColPriceA.ReadOnly = true;
            this.ColPriceA.Width = 80;
            // 
            // ColAskA
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ColAskA.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColAskA.HeaderText = "Ask";
            this.ColAskA.Name = "ColAskA";
            this.ColAskA.ReadOnly = true;
            this.ColAskA.Width = 60;
            // 
            // ColTradeA
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColTradeA.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColTradeA.HeaderText = "Trade";
            this.ColTradeA.Name = "ColTradeA";
            this.ColTradeA.ReadOnly = true;
            this.ColTradeA.Width = 60;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 833);
            this.Controls.Add(this.chkLockScroll);
            this.Controls.Add(this.lblProductB);
            this.Controls.Add(this.GridPanelB);
            this.Controls.Add(this.orderSetView);
            this.Controls.Add(this.lblProductA);
            this.Controls.Add(this.GridPanelA);
            this.Name = "MainForm";
            this.Text = "Lag Trader";
            ((System.ComponentModel.ISupportInitialize)(this.gridProductA)).EndInit();
            this.GridPanelA.ResumeLayout(false);
            this.GridPanelB.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridProductB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridProductA;
        private System.Windows.Forms.Panel GridPanelA;
        private System.Windows.Forms.Label lblProductA;
        private System.Windows.Forms.ListView orderSetView;
        private System.Windows.Forms.ColumnHeader colOrderContract;
        private System.Windows.Forms.ColumnHeader colOrderSide;
        private System.Windows.Forms.ColumnHeader colOrderQuantity;
        private System.Windows.Forms.ColumnHeader colOrderPrice;
        private System.Windows.Forms.ColumnHeader colOrderLetter;
        private System.Windows.Forms.ColumnHeader colRule;
        private System.Windows.Forms.ColumnHeader colDelay;
        private System.Windows.Forms.ColumnHeader colTicks;
        private System.Windows.Forms.ColumnHeader colPercent;
        private System.Windows.Forms.Label lblProductB;
        private System.Windows.Forms.Panel GridPanelB;
        private System.Windows.Forms.DataGridView gridProductB;
        private System.Windows.Forms.CheckBox chkLockScroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdersB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBidB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPriceB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAskB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTradeB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdersA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBidA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPriceA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAskA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTradeA;
    }
}

