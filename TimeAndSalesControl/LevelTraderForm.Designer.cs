namespace LevelTrader
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
            this._MDTraderControl = new TradingTechnologies.MDTrader.MDTraderControl();
            this._mdtVerticalControlPanel1 = new TradingTechnologies.MDTrader.MDTVerticalControlPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsLabelSound = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsControl1 = new TimeAndSalesControl.TSControl();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MDTraderControl
            // 
            this._MDTraderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._MDTraderControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._MDTraderControl.ForceMonoRendering = false;
            this._MDTraderControl.Location = new System.Drawing.Point(106, 1);
            this._MDTraderControl.Name = "_MDTraderControl";
            this._MDTraderControl.Size = new System.Drawing.Size(230, 429);
            this._MDTraderControl.TabIndex = 1;
            this._MDTraderControl.Resize += new System.EventHandler(this._MDTraderControl_Resize);
            // 
            // _mdtVerticalControlPanel1
            // 
            this._mdtVerticalControlPanel1.BackColor = System.Drawing.SystemColors.Control;
            this._mdtVerticalControlPanel1.Location = new System.Drawing.Point(0, 1);
            this._mdtVerticalControlPanel1.Name = "_mdtVerticalControlPanel1";
            this._mdtVerticalControlPanel1.Size = new System.Drawing.Size(100, 136);
            this._mdtVerticalControlPanel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.tsLabelSound});
            this.statusStrip1.Location = new System.Drawing.Point(0, 433);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(338, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(307, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsLabelSound
            // 
            this.tsLabelSound.Image = global::SimpleExample.Properties.Resources.BackgroundSoundHS;
            this.tsLabelSound.Name = "tsLabelSound";
            this.tsLabelSound.Size = new System.Drawing.Size(16, 17);
            // 
            // tsControl1
            // 
            this.tsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tsControl1.Location = new System.Drawing.Point(0, 143);
            this.tsControl1.Name = "tsControl1";
            this.tsControl1.Size = new System.Drawing.Size(100, 287);
            this.tsControl1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 455);
            this.Controls.Add(this.tsControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._mdtVerticalControlPanel1);
            this.Controls.Add(this._MDTraderControl);
            this.Name = "MainForm";
            this.Text = "MDTrader Example";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TradingTechnologies.MDTrader.MDTraderControl _MDTraderControl;
        private TradingTechnologies.MDTrader.MDTVerticalControlPanel _mdtVerticalControlPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tsLabelSound;
        private TimeAndSalesControl.TSControl tsControl1;

    }
}

