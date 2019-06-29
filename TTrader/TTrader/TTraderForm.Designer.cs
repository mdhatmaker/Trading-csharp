namespace TTrader
{
    partial class TTraderForm
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
            this.lblBidSize = new System.Windows.Forms.Label();
            this.lblBidPrice = new System.Windows.Forms.Label();
            this.lblAskPrice = new System.Windows.Forms.Label();
            this.lblAskSize = new System.Windows.Forms.Label();
            this.lblLastPrice = new System.Windows.Forms.Label();
            this.lblLastSize = new System.Windows.Forms.Label();
            this.lblPriceChange = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblVPM = new System.Windows.Forms.Label();
            this.lblBADeltaChange = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblRatio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBidSize
            // 
            this.lblBidSize.Location = new System.Drawing.Point(12, 31);
            this.lblBidSize.Name = "lblBidSize";
            this.lblBidSize.Size = new System.Drawing.Size(43, 15);
            this.lblBidSize.TabIndex = 0;
            this.lblBidSize.Text = "(bidqty)";
            // 
            // lblBidPrice
            // 
            this.lblBidPrice.Location = new System.Drawing.Point(61, 31);
            this.lblBidPrice.Name = "lblBidPrice";
            this.lblBidPrice.Size = new System.Drawing.Size(43, 15);
            this.lblBidPrice.TabIndex = 1;
            this.lblBidPrice.Text = "(bid$)";
            // 
            // lblAskPrice
            // 
            this.lblAskPrice.Location = new System.Drawing.Point(130, 31);
            this.lblAskPrice.Name = "lblAskPrice";
            this.lblAskPrice.Size = new System.Drawing.Size(43, 15);
            this.lblAskPrice.TabIndex = 2;
            this.lblAskPrice.Text = "(ask$)";
            // 
            // lblAskSize
            // 
            this.lblAskSize.Location = new System.Drawing.Point(179, 31);
            this.lblAskSize.Name = "lblAskSize";
            this.lblAskSize.Size = new System.Drawing.Size(44, 15);
            this.lblAskSize.TabIndex = 3;
            this.lblAskSize.Text = "(askqty)";
            // 
            // lblLastPrice
            // 
            this.lblLastPrice.Location = new System.Drawing.Point(123, 58);
            this.lblLastPrice.Name = "lblLastPrice";
            this.lblLastPrice.Size = new System.Drawing.Size(43, 15);
            this.lblLastPrice.TabIndex = 5;
            this.lblLastPrice.Text = "(last$)";
            // 
            // lblLastSize
            // 
            this.lblLastSize.Location = new System.Drawing.Point(74, 58);
            this.lblLastSize.Name = "lblLastSize";
            this.lblLastSize.Size = new System.Drawing.Size(43, 15);
            this.lblLastSize.TabIndex = 4;
            this.lblLastSize.Text = "(lastqty)";
            // 
            // lblPriceChange
            // 
            this.lblPriceChange.Location = new System.Drawing.Point(179, 119);
            this.lblPriceChange.Name = "lblPriceChange";
            this.lblPriceChange.Size = new System.Drawing.Size(58, 15);
            this.lblPriceChange.TabIndex = 6;
            this.lblPriceChange.Text = "(price)";
            this.lblPriceChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblVPM
            // 
            this.lblVPM.Location = new System.Drawing.Point(-3, 117);
            this.lblVPM.Name = "lblVPM";
            this.lblVPM.Size = new System.Drawing.Size(58, 15);
            this.lblVPM.TabIndex = 7;
            this.lblVPM.Text = "(vpm)";
            this.lblVPM.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBADeltaChange
            // 
            this.lblBADeltaChange.Location = new System.Drawing.Point(179, 104);
            this.lblBADeltaChange.Name = "lblBADeltaChange";
            this.lblBADeltaChange.Size = new System.Drawing.Size(58, 15);
            this.lblBADeltaChange.TabIndex = 8;
            this.lblBADeltaChange.Text = "(badelta)";
            this.lblBADeltaChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lblRatio
            // 
            this.lblRatio.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRatio.Location = new System.Drawing.Point(72, 104);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(78, 27);
            this.lblRatio.TabIndex = 9;
            this.lblRatio.Text = "(ratio)";
            this.lblRatio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TTraderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 140);
            this.Controls.Add(this.lblRatio);
            this.Controls.Add(this.lblBADeltaChange);
            this.Controls.Add(this.lblVPM);
            this.Controls.Add(this.lblPriceChange);
            this.Controls.Add(this.lblLastPrice);
            this.Controls.Add(this.lblLastSize);
            this.Controls.Add(this.lblAskSize);
            this.Controls.Add(this.lblAskPrice);
            this.Controls.Add(this.lblBidPrice);
            this.Controls.Add(this.lblBidSize);
            this.Name = "TTraderForm";
            this.Text = "TTrader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBidSize;
        private System.Windows.Forms.Label lblBidPrice;
        private System.Windows.Forms.Label lblAskPrice;
        private System.Windows.Forms.Label lblAskSize;
        private System.Windows.Forms.Label lblLastPrice;
        private System.Windows.Forms.Label lblLastSize;
        private System.Windows.Forms.Label lblPriceChange;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblVPM;
        private System.Windows.Forms.Label lblBADeltaChange;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblRatio;
    }
}

