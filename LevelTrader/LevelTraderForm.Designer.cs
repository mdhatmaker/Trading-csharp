namespace LevelTrader
{
    partial class LevelTraderForm
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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsControl1 = new TT.TradeCo.Controls.TSControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSound = new System.Windows.Forms.ToolStripButton();
            this.tsbTimeAndSales = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsLabelSound = new System.Windows.Forms.ToolStripStatusLabel();
            this._MDTraderControl = new TradingTechnologies.MDTrader.MDTraderControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(271, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsControl1
            // 
            this.tsControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tsControl1.Location = new System.Drawing.Point(229, 28);
            this.tsControl1.Name = "tsControl1";
            this.tsControl1.Size = new System.Drawing.Size(88, 566);
            this.tsControl1.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCenter,
            this.toolStripSeparator1,
            this.tsbSound,
            this.tsbTimeAndSales,
            this.tsbSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(317, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbCenter
            // 
            this.tsbCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCenter.Image = global::LevelTrader.Properties.Resources.TileWindowsHorizontallyHS;
            this.tsbCenter.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbCenter.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbCenter.Name = "tsbCenter";
            this.tsbCenter.Size = new System.Drawing.Size(23, 22);
            this.tsbCenter.Text = "Center";
            this.tsbCenter.ToolTipText = "Re-center the grid";
            this.tsbCenter.Click += new System.EventHandler(this.tsbCenter_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSound
            // 
            this.tsbSound.Checked = true;
            this.tsbSound.CheckOnClick = true;
            this.tsbSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSound.Image = global::LevelTrader.Properties.Resources.BackgroundSoundHS;
            this.tsbSound.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbSound.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbSound.Name = "tsbSound";
            this.tsbSound.Size = new System.Drawing.Size(23, 22);
            this.tsbSound.Text = "Sound";
            this.tsbSound.ToolTipText = "Toggle sound on/off";
            this.tsbSound.Click += new System.EventHandler(this.tsbSound_Click);
            // 
            // tsbTimeAndSales
            // 
            this.tsbTimeAndSales.Checked = true;
            this.tsbTimeAndSales.CheckOnClick = true;
            this.tsbTimeAndSales.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbTimeAndSales.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTimeAndSales.Image = global::LevelTrader.Properties.Resources.Expiration;
            this.tsbTimeAndSales.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTimeAndSales.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbTimeAndSales.Name = "tsbTimeAndSales";
            this.tsbTimeAndSales.Size = new System.Drawing.Size(23, 22);
            this.tsbTimeAndSales.Text = "Time && Sales";
            this.tsbTimeAndSales.ToolTipText = "Enable/disable the time and sales display";
            this.tsbTimeAndSales.Click += new System.EventHandler(this.tsbTimeAndSales_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSettings.Image = global::LevelTrader.Properties.Resources.PropertiesHS;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsbSettings.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(23, 22);
            this.tsbSettings.Text = "Settings";
            this.tsbSettings.ToolTipText = "View/Change application settings";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsLabelSound
            // 
            this.tsLabelSound.Image = global::LevelTrader.Properties.Resources.BackgroundSoundHS;
            this.tsLabelSound.Name = "tsLabelSound";
            this.tsLabelSound.Size = new System.Drawing.Size(16, 17);
            // 
            // _MDTraderControl
            // 
            this._MDTraderControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._MDTraderControl.ForceMonoRendering = false;
            this._MDTraderControl.Location = new System.Drawing.Point(0, 28);
            this._MDTraderControl.Name = "_MDTraderControl";
            this._MDTraderControl.Size = new System.Drawing.Size(223, 566);
            this._MDTraderControl.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 602);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(317, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(302, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LevelTraderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 624);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._MDTraderControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tsControl1);
            this.Name = "LevelTraderForm";
            this.Text = "LevelTrader";
            this.Load += new System.EventHandler(this.LevelTraderForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel tsLabelSound;
        private TT.TradeCo.Controls.TSControl tsControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSound;
        private System.Windows.Forms.ToolStripButton tsbCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripButton tsbTimeAndSales;
        private TradingTechnologies.MDTrader.MDTraderControl _MDTraderControl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

    }
}

