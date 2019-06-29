namespace FillsDeluxe
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnMessaging = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemTextMessageAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemProwlAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemAndroidAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemTextMessageBackup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemSendTestMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemMessagingSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnSounds = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemEnableSounds = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemSoundSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnHedger = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemEnableHedging = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemHedgerSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsitemAutoscroll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsitemGeneralSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbtnInfo = new System.Windows.Forms.ToolStripButton();
            this.tscomboFFT = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnMessaging,
            this.tsbtnSounds,
            this.tsbtnHedger,
            this.tsbtnSettings,
            this.toolStripSeparator1,
            this.tsbtnHelp,
            this.tsbtnInfo,
            this.tscomboFFT});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(711, 86);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnMessaging
            // 
            this.tsbtnMessaging.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemTextMessageAlert,
            this.tsitemProwlAlert,
            this.tsitemAndroidAlert,
            this.tsitemTextMessageBackup,
            this.tsitemSendTestMessage,
            this.tsitemMessagingSettings});
            this.tsbtnMessaging.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMessaging.Image")));
            this.tsbtnMessaging.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnMessaging.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnMessaging.Name = "tsbtnMessaging";
            this.tsbtnMessaging.Size = new System.Drawing.Size(77, 83);
            this.tsbtnMessaging.Text = "Messaging";
            this.tsbtnMessaging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemTextMessageAlert
            // 
            this.tsitemTextMessageAlert.CheckOnClick = true;
            this.tsitemTextMessageAlert.Name = "tsitemTextMessageAlert";
            this.tsitemTextMessageAlert.Size = new System.Drawing.Size(187, 22);
            this.tsitemTextMessageAlert.Text = "Text Message Alert";
            // 
            // tsitemProwlAlert
            // 
            this.tsitemProwlAlert.CheckOnClick = true;
            this.tsitemProwlAlert.Name = "tsitemProwlAlert";
            this.tsitemProwlAlert.Size = new System.Drawing.Size(187, 22);
            this.tsitemProwlAlert.Text = "iPhone Prowl Alert";
            // 
            // tsitemAndroidAlert
            // 
            this.tsitemAndroidAlert.CheckOnClick = true;
            this.tsitemAndroidAlert.Name = "tsitemAndroidAlert";
            this.tsitemAndroidAlert.Size = new System.Drawing.Size(187, 22);
            this.tsitemAndroidAlert.Text = "Android NMA Alert";
            // 
            // tsitemTextMessageBackup
            // 
            this.tsitemTextMessageBackup.CheckOnClick = true;
            this.tsitemTextMessageBackup.Enabled = false;
            this.tsitemTextMessageBackup.Name = "tsitemTextMessageBackup";
            this.tsitemTextMessageBackup.Size = new System.Drawing.Size(187, 22);
            this.tsitemTextMessageBackup.Text = "Text Message Backup";
            // 
            // tsitemSendTestMessage
            // 
            this.tsitemSendTestMessage.Name = "tsitemSendTestMessage";
            this.tsitemSendTestMessage.Size = new System.Drawing.Size(187, 22);
            this.tsitemSendTestMessage.Text = "Send Test Message";
            // 
            // tsitemMessagingSettings
            // 
            this.tsitemMessagingSettings.Name = "tsitemMessagingSettings";
            this.tsitemMessagingSettings.Size = new System.Drawing.Size(187, 22);
            this.tsitemMessagingSettings.Text = "Messaging Settings...";
            // 
            // tsbtnSounds
            // 
            this.tsbtnSounds.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemEnableSounds,
            this.tsitemSoundSettings});
            this.tsbtnSounds.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSounds.Image")));
            this.tsbtnSounds.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSounds.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSounds.Name = "tsbtnSounds";
            this.tsbtnSounds.Size = new System.Drawing.Size(77, 83);
            this.tsbtnSounds.Text = "Sounds";
            this.tsbtnSounds.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemEnableSounds
            // 
            this.tsitemEnableSounds.CheckOnClick = true;
            this.tsitemEnableSounds.Name = "tsitemEnableSounds";
            this.tsitemEnableSounds.Size = new System.Drawing.Size(153, 22);
            this.tsitemEnableSounds.Text = "Enable Sounds";
            // 
            // tsitemSoundSettings
            // 
            this.tsitemSoundSettings.Name = "tsitemSoundSettings";
            this.tsitemSoundSettings.Size = new System.Drawing.Size(153, 22);
            this.tsitemSoundSettings.Text = "Sound Settings";
            // 
            // tsbtnHedger
            // 
            this.tsbtnHedger.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemEnableHedging,
            this.tsitemHedgerSettings});
            this.tsbtnHedger.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHedger.Image")));
            this.tsbtnHedger.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnHedger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHedger.Name = "tsbtnHedger";
            this.tsbtnHedger.Size = new System.Drawing.Size(77, 83);
            this.tsbtnHedger.Text = "Hedger";
            this.tsbtnHedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemEnableHedging
            // 
            this.tsitemEnableHedging.CheckOnClick = true;
            this.tsitemEnableHedging.Name = "tsitemEnableHedging";
            this.tsitemEnableHedging.Size = new System.Drawing.Size(167, 22);
            this.tsitemEnableHedging.Text = "Enable Hedging";
            // 
            // tsitemHedgerSettings
            // 
            this.tsitemHedgerSettings.Name = "tsitemHedgerSettings";
            this.tsitemHedgerSettings.Size = new System.Drawing.Size(167, 22);
            this.tsitemHedgerSettings.Text = "Hedger Settings...";
            // 
            // tsbtnSettings
            // 
            this.tsbtnSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsitemAutoscroll,
            this.tsitemGeneralSettings});
            this.tsbtnSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSettings.Image")));
            this.tsbtnSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSettings.Name = "tsbtnSettings";
            this.tsbtnSettings.Size = new System.Drawing.Size(77, 83);
            this.tsbtnSettings.Text = "Settings";
            this.tsbtnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsitemAutoscroll
            // 
            this.tsitemAutoscroll.Checked = true;
            this.tsitemAutoscroll.CheckOnClick = true;
            this.tsitemAutoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsitemAutoscroll.Name = "tsitemAutoscroll";
            this.tsitemAutoscroll.Size = new System.Drawing.Size(168, 22);
            this.tsitemAutoscroll.Text = "Autoscroll";
            // 
            // tsitemGeneralSettings
            // 
            this.tsitemGeneralSettings.Name = "tsitemGeneralSettings";
            this.tsitemGeneralSettings.Size = new System.Drawing.Size(168, 22);
            this.tsitemGeneralSettings.Text = "General Settings...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 86);
            // 
            // tsbtnHelp
            // 
            this.tsbtnHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnHelp.Image")));
            this.tsbtnHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnHelp.Name = "tsbtnHelp";
            this.tsbtnHelp.Size = new System.Drawing.Size(68, 83);
            this.tsbtnHelp.Text = "Help";
            this.tsbtnHelp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbtnInfo
            // 
            this.tsbtnInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnInfo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnInfo.Image")));
            this.tsbtnInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnInfo.Name = "tsbtnInfo";
            this.tsbtnInfo.Size = new System.Drawing.Size(68, 83);
            this.tsbtnInfo.Text = "Info";
            this.tsbtnInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tscomboFFT
            // 
            this.tscomboFFT.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tscomboFFT.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tscomboFFT.Name = "tscomboFFT";
            this.tscomboFFT.Size = new System.Drawing.Size(121, 86);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 262);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnMessaging;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnSounds;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnHedger;
        private System.Windows.Forms.ToolStripDropDownButton tsbtnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox tscomboFFT;
        private System.Windows.Forms.ToolStripButton tsbtnInfo;
        private System.Windows.Forms.ToolStripButton tsbtnHelp;
        private System.Windows.Forms.ToolStripMenuItem tsitemTextMessageAlert;
        private System.Windows.Forms.ToolStripMenuItem tsitemProwlAlert;
        private System.Windows.Forms.ToolStripMenuItem tsitemAndroidAlert;
        private System.Windows.Forms.ToolStripMenuItem tsitemTextMessageBackup;
        private System.Windows.Forms.ToolStripMenuItem tsitemSendTestMessage;
        private System.Windows.Forms.ToolStripMenuItem tsitemMessagingSettings;
        private System.Windows.Forms.ToolStripMenuItem tsitemEnableSounds;
        private System.Windows.Forms.ToolStripMenuItem tsitemSoundSettings;
        private System.Windows.Forms.ToolStripMenuItem tsitemEnableHedging;
        private System.Windows.Forms.ToolStripMenuItem tsitemHedgerSettings;
        private System.Windows.Forms.ToolStripMenuItem tsitemAutoscroll;
        private System.Windows.Forms.ToolStripMenuItem tsitemGeneralSettings;
    }
}