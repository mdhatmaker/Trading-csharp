namespace GridApp
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridProduct = new System.Windows.Forms.DataGridView();
            this.ColTheoBid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAsk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTheoAsk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPanel = new System.Windows.Forms.Panel();
            this.lblProduct = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.colOrderLetter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderSide = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderRule = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colRuleDelay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuleTicks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRulePercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorkspaceAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutOrderManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).BeginInit();
            this.GridPanel.SuspendLayout();
            this.status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridProduct
            // 
            this.gridProduct.AllowUserToAddRows = false;
            this.gridProduct.AllowUserToDeleteRows = false;
            this.gridProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.gridProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColTheoBid,
            this.ColBid,
            this.ColPrice,
            this.ColAsk,
            this.ColTheoAsk});
            this.gridProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridProduct.Location = new System.Drawing.Point(0, 0);
            this.gridProduct.Name = "gridProduct";
            this.gridProduct.ReadOnly = true;
            this.gridProduct.RowHeadersVisible = false;
            this.gridProduct.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridProduct.Size = new System.Drawing.Size(532, 425);
            this.gridProduct.TabIndex = 0;
            // 
            // ColTheoBid
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ColTheoBid.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColTheoBid.HeaderText = "TheoBid";
            this.ColTheoBid.Name = "ColTheoBid";
            this.ColTheoBid.ReadOnly = true;
            this.ColTheoBid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColBid
            // 
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ColBid.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColBid.HeaderText = "Bid";
            this.ColBid.Name = "ColBid";
            this.ColBid.ReadOnly = true;
            // 
            // ColPrice
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColPrice.DefaultCellStyle = dataGridViewCellStyle13;
            this.ColPrice.HeaderText = "Price";
            this.ColPrice.Name = "ColPrice";
            this.ColPrice.ReadOnly = true;
            // 
            // ColAsk
            // 
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ColAsk.DefaultCellStyle = dataGridViewCellStyle14;
            this.ColAsk.HeaderText = "Ask";
            this.ColAsk.Name = "ColAsk";
            this.ColAsk.ReadOnly = true;
            // 
            // ColTheoAsk
            // 
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ColTheoAsk.DefaultCellStyle = dataGridViewCellStyle15;
            this.ColTheoAsk.HeaderText = "TheoAsk";
            this.ColTheoAsk.Name = "ColTheoAsk";
            this.ColTheoAsk.ReadOnly = true;
            // 
            // GridPanel
            // 
            this.GridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GridPanel.Controls.Add(this.gridProduct);
            this.GridPanel.Location = new System.Drawing.Point(12, 59);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(532, 425);
            this.GridPanel.TabIndex = 1;
            // 
            // lblProduct
            // 
            this.lblProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(12, 30);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(532, 27);
            this.lblProduct.TabIndex = 2;
            this.lblProduct.Text = "(drop product to begin)";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1});
            this.status.Location = new System.Drawing.Point(0, 677);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(559, 22);
            this.status.TabIndex = 4;
            this.status.Text = "statusStrip1";
            // 
            // statusLabel1
            // 
            this.statusLabel1.AutoSize = false;
            this.statusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(199, 17);
            this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gridOrders
            // 
            this.gridOrders.AllowUserToAddRows = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            this.gridOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrderLetter,
            this.colOrderContract,
            this.colOrderSide,
            this.colOrderQty,
            this.colOrderPrice,
            this.colOrderRule,
            this.colRuleDelay,
            this.colRuleTicks,
            this.colRulePercent,
            this.colOrderKey});
            this.gridOrders.Location = new System.Drawing.Point(12, 494);
            this.gridOrders.Name = "gridOrders";
            this.gridOrders.RowHeadersVisible = false;
            this.gridOrders.Size = new System.Drawing.Size(532, 180);
            this.gridOrders.TabIndex = 5;
            // 
            // colOrderLetter
            // 
            this.colOrderLetter.HeaderText = "A-Z";
            this.colOrderLetter.Name = "colOrderLetter";
            this.colOrderLetter.ReadOnly = true;
            this.colOrderLetter.Width = 30;
            // 
            // colOrderContract
            // 
            this.colOrderContract.HeaderText = "Contract";
            this.colOrderContract.Name = "colOrderContract";
            this.colOrderContract.ReadOnly = true;
            this.colOrderContract.Width = 80;
            // 
            // colOrderSide
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colOrderSide.DefaultCellStyle = dataGridViewCellStyle16;
            this.colOrderSide.HeaderText = "Side";
            this.colOrderSide.Name = "colOrderSide";
            this.colOrderSide.ReadOnly = true;
            this.colOrderSide.Width = 45;
            // 
            // colOrderQty
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colOrderQty.DefaultCellStyle = dataGridViewCellStyle17;
            this.colOrderQty.HeaderText = "Qty";
            this.colOrderQty.Name = "colOrderQty";
            this.colOrderQty.ReadOnly = true;
            this.colOrderQty.Width = 35;
            // 
            // colOrderPrice
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colOrderPrice.DefaultCellStyle = dataGridViewCellStyle18;
            this.colOrderPrice.HeaderText = "Price";
            this.colOrderPrice.Name = "colOrderPrice";
            this.colOrderPrice.ReadOnly = true;
            this.colOrderPrice.Width = 60;
            // 
            // colOrderRule
            // 
            this.colOrderRule.HeaderText = "Rule";
            this.colOrderRule.Name = "colOrderRule";
            this.colOrderRule.Width = 140;
            // 
            // colRuleDelay
            // 
            this.colRuleDelay.HeaderText = "Delay";
            this.colRuleDelay.Name = "colRuleDelay";
            this.colRuleDelay.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRuleDelay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRuleDelay.Width = 40;
            // 
            // colRuleTicks
            // 
            this.colRuleTicks.HeaderText = "Ticks";
            this.colRuleTicks.Name = "colRuleTicks";
            this.colRuleTicks.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRuleTicks.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRuleTicks.Width = 40;
            // 
            // colRulePercent
            // 
            this.colRulePercent.HeaderText = "%";
            this.colRulePercent.Name = "colRulePercent";
            this.colRulePercent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRulePercent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRulePercent.Width = 40;
            // 
            // colOrderKey
            // 
            this.colOrderKey.HeaderText = "Order Key";
            this.colOrderKey.Name = "colOrderKey";
            this.colOrderKey.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(559, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.saveWorkspaceToolStripMenuItem,
            this.saveWorkspaceAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // saveWorkspaceToolStripMenuItem
            // 
            this.saveWorkspaceToolStripMenuItem.Name = "saveWorkspaceToolStripMenuItem";
            this.saveWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveWorkspaceToolStripMenuItem.Text = "&Save Workspace";
            // 
            // saveWorkspaceAsToolStripMenuItem
            // 
            this.saveWorkspaceAsToolStripMenuItem.Name = "saveWorkspaceAsToolStripMenuItem";
            this.saveWorkspaceAsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveWorkspaceAsToolStripMenuItem.Text = "Save Workspace &As...";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutOrderManagerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutOrderManagerToolStripMenuItem
            // 
            this.aboutOrderManagerToolStripMenuItem.Name = "aboutOrderManagerToolStripMenuItem";
            this.aboutOrderManagerToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.aboutOrderManagerToolStripMenuItem.Text = "&About Order Manager...";
            this.aboutOrderManagerToolStripMenuItem.Click += new System.EventHandler(this.aboutOrderManagerToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 699);
            this.Controls.Add(this.gridOrders);
            this.Controls.Add(this.status);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.GridPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Order Manager";
            ((System.ComponentModel.ISupportInitialize)(this.gridProduct)).EndInit();
            this.GridPanel.ResumeLayout(false);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridProduct;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTheoBid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAsk;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTheoAsk;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderLetter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderSide;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderPrice;
        private System.Windows.Forms.DataGridViewComboBoxColumn colOrderRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleDelay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuleTicks;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRulePercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderKey;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorkspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorkspaceAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutOrderManagerToolStripMenuItem;
    }
}

