namespace CopperHedge
{
    partial class InstrumentForm
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
            this.instrumentGrid = new System.Windows.Forms.DataGridView();
            this.colExchange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContract = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProdType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.instrumentGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // instrumentGrid
            // 
            this.instrumentGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.instrumentGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instrumentGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colExchange,
            this.colProduct,
            this.colContract,
            this.colProdType});
            this.instrumentGrid.Location = new System.Drawing.Point(12, 12);
            this.instrumentGrid.Name = "instrumentGrid";
            this.instrumentGrid.Size = new System.Drawing.Size(516, 94);
            this.instrumentGrid.TabIndex = 1;
            // 
            // colExchange
            // 
            this.colExchange.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colExchange.HeaderText = "Exchange";
            this.colExchange.Name = "colExchange";
            this.colExchange.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colExchange.Width = 61;
            // 
            // colProduct
            // 
            this.colProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colProduct.HeaderText = "Product";
            this.colProduct.Name = "colProduct";
            this.colProduct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProduct.Width = 50;
            // 
            // colContract
            // 
            this.colContract.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colContract.HeaderText = "Contract";
            this.colContract.Name = "colContract";
            this.colContract.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colContract.Width = 53;
            // 
            // colProdType
            // 
            this.colProdType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colProdType.HeaderText = "ProdType";
            this.colProdType.Name = "colProdType";
            this.colProdType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colProdType.Width = 59;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(356, 125);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(453, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // InstrumentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 160);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.instrumentGrid);
            this.Name = "InstrumentsForm";
            this.Text = "Instruments";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InstrumentsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.instrumentGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView instrumentGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExchange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContract;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProdType;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}