namespace SpreadPanelControlTest
{
    partial class SpreadToolForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.spreadLeg3 = new SpreadPanelControl.SpreadPanelControl();
            this.spreadLeg2 = new SpreadPanelControl.SpreadPanelControl();
            this.spreadLeg1 = new SpreadPanelControl.SpreadPanelControl();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(110, 783);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 28);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(203, 783);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // spreadLeg3
            // 
            this.spreadLeg3.Active = true;
            this.spreadLeg3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadLeg3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spreadLeg3.Location = new System.Drawing.Point(5, 540);
            this.spreadLeg3.Month = 0;
            this.spreadLeg3.Name = "spreadLeg3";
            this.spreadLeg3.Size = new System.Drawing.Size(278, 239);
            this.spreadLeg3.TabIndex = 2;
            // 
            // spreadLeg2
            // 
            this.spreadLeg2.Active = true;
            this.spreadLeg2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadLeg2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spreadLeg2.Location = new System.Drawing.Point(3, 294);
            this.spreadLeg2.Month = 0;
            this.spreadLeg2.Name = "spreadLeg2";
            this.spreadLeg2.Size = new System.Drawing.Size(278, 240);
            this.spreadLeg2.TabIndex = 1;
            // 
            // spreadLeg1
            // 
            this.spreadLeg1.Active = true;
            this.spreadLeg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadLeg1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spreadLeg1.Location = new System.Drawing.Point(3, 48);
            this.spreadLeg1.Month = 0;
            this.spreadLeg1.Name = "spreadLeg1";
            this.spreadLeg1.Size = new System.Drawing.Size(278, 240);
            this.spreadLeg1.TabIndex = 0;
            // 
            // SpreadToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 817);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.spreadLeg3);
            this.Controls.Add(this.spreadLeg2);
            this.Controls.Add(this.spreadLeg1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpreadToolForm";
            this.Text = "SpreadTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadToolForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private SpreadPanelControl.SpreadPanelControl spreadLeg1;
        private SpreadPanelControl.SpreadPanelControl spreadLeg2;
        private SpreadPanelControl.SpreadPanelControl spreadLeg3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}

