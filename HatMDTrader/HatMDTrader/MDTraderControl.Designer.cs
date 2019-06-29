namespace HatMDTrader
{
    partial class MDTraderControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridTrader = new System.Windows.Forms.DataGridView();
            this.ColOrdersA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColBidA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColPriceA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColAskA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTradeA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridTrader)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTrader
            // 
            this.gridTrader.AllowUserToAddRows = false;
            this.gridTrader.AllowUserToDeleteRows = false;
            this.gridTrader.AllowUserToResizeRows = false;
            this.gridTrader.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTrader.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTrader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTrader.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColOrdersA,
            this.ColBidA,
            this.ColPriceA,
            this.ColAskA,
            this.ColTradeA});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTrader.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridTrader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTrader.Location = new System.Drawing.Point(0, 0);
            this.gridTrader.Name = "gridTrader";
            this.gridTrader.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTrader.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.gridTrader.RowHeadersVisible = false;
            this.gridTrader.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridTrader.Size = new System.Drawing.Size(364, 530);
            this.gridTrader.TabIndex = 1;
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
            // 
            // ColBidA
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            this.ColBidA.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColBidA.HeaderText = "Bid";
            this.ColBidA.Name = "ColBidA";
            this.ColBidA.ReadOnly = true;
            // 
            // ColPriceA
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            this.ColPriceA.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColPriceA.HeaderText = "Price";
            this.ColPriceA.Name = "ColPriceA";
            this.ColPriceA.ReadOnly = true;
            // 
            // ColAskA
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.ColAskA.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColAskA.HeaderText = "Ask";
            this.ColAskA.Name = "ColAskA";
            this.ColAskA.ReadOnly = true;
            // 
            // ColTradeA
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ColTradeA.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColTradeA.HeaderText = "Trade";
            this.ColTradeA.Name = "ColTradeA";
            this.ColTradeA.ReadOnly = true;
            // 
            // MDTraderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridTrader);
            this.Name = "MDTraderControl";
            this.Size = new System.Drawing.Size(364, 530);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTrader;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColOrdersA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColBidA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColPriceA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColAskA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTradeA;
    }
}
