namespace TT.TradeCo.Controls
{
    partial class TSControl
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
            this.rtbTimeSales = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbTimeSales
            // 
            this.rtbTimeSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTimeSales.Location = new System.Drawing.Point(0, 0);
            this.rtbTimeSales.Name = "rtbTimeSales";
            this.rtbTimeSales.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbTimeSales.Size = new System.Drawing.Size(83, 285);
            this.rtbTimeSales.TabIndex = 0;
            this.rtbTimeSales.Text = "";
            // 
            // TSControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtbTimeSales);
            this.Name = "TSControl";
            this.Size = new System.Drawing.Size(83, 285);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbTimeSales;

    }
}
