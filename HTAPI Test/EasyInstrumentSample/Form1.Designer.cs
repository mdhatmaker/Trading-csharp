namespace APITest
{
    partial class frmMain
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
            this.btnSendTestOrder = new System.Windows.Forms.Button();
            this.listMessages = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnDeleteAllOrders = new System.Windows.Forms.Button();
            this.btnModifyOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSendTestOrder
            // 
            this.btnSendTestOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendTestOrder.Location = new System.Drawing.Point(421, 12);
            this.btnSendTestOrder.Name = "btnSendTestOrder";
            this.btnSendTestOrder.Size = new System.Drawing.Size(109, 23);
            this.btnSendTestOrder.TabIndex = 0;
            this.btnSendTestOrder.Text = "Send Test Orders";
            this.btnSendTestOrder.UseVisualStyleBackColor = true;
            this.btnSendTestOrder.Click += new System.EventHandler(this.btnSendTestOrder_Click);
            // 
            // listMessages
            // 
            this.listMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listMessages.Location = new System.Drawing.Point(12, 53);
            this.listMessages.Name = "listMessages";
            this.listMessages.Size = new System.Drawing.Size(518, 179);
            this.listMessages.TabIndex = 1;
            this.listMessages.UseCompatibleStateImageBehavior = false;
            this.listMessages.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Message";
            this.columnHeader1.Width = 1000;
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.Location = new System.Drawing.Point(12, 12);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteOrder.TabIndex = 2;
            this.btnDeleteOrder.Text = "Delete Order";
            this.btnDeleteOrder.UseVisualStyleBackColor = true;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnDeleteAllOrders
            // 
            this.btnDeleteAllOrders.Location = new System.Drawing.Point(118, 12);
            this.btnDeleteAllOrders.Name = "btnDeleteAllOrders";
            this.btnDeleteAllOrders.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteAllOrders.TabIndex = 3;
            this.btnDeleteAllOrders.Text = "Delete All Orders";
            this.btnDeleteAllOrders.UseVisualStyleBackColor = true;
            this.btnDeleteAllOrders.Click += new System.EventHandler(this.btnDeleteAllOrders_Click);
            // 
            // btnModifyOrder
            // 
            this.btnModifyOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyOrder.Location = new System.Drawing.Point(306, 12);
            this.btnModifyOrder.Name = "btnModifyOrder";
            this.btnModifyOrder.Size = new System.Drawing.Size(109, 23);
            this.btnModifyOrder.TabIndex = 4;
            this.btnModifyOrder.Text = "Modify Order";
            this.btnModifyOrder.UseVisualStyleBackColor = true;
            this.btnModifyOrder.Click += new System.EventHandler(this.btnModifyOrder_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 243);
            this.Controls.Add(this.btnModifyOrder);
            this.Controls.Add(this.btnDeleteAllOrders);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.listMessages);
            this.Controls.Add(this.btnSendTestOrder);
            this.Name = "frmMain";
            this.Text = "HTAPI Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSendTestOrder;
        private System.Windows.Forms.ListView listMessages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnDeleteAllOrders;
        private System.Windows.Forms.Button btnModifyOrder;

    }
}

