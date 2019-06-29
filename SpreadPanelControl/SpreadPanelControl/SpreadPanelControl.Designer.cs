namespace SpreadPanelControl
{
    partial class SpreadPanelControl
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
            this.listMonth = new System.Windows.Forms.ListBox();
            this.listYear = new System.Windows.Forms.ListBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.cboQuantity = new System.Windows.Forms.ComboBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblFraction = new System.Windows.Forms.Label();
            this.cboFraction = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioSell = new System.Windows.Forms.RadioButton();
            this.radioBuy = new System.Windows.Forms.RadioButton();
            this.lviewStrike = new System.Windows.Forms.ListView();
            this.columnCall = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listMonth
            // 
            this.listMonth.FormattingEnabled = true;
            this.listMonth.Items.AddRange(new object[] {
            "Jan",
            "Fe b",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec"});
            this.listMonth.Location = new System.Drawing.Point(2, 23);
            this.listMonth.Name = "listMonth";
            this.listMonth.Size = new System.Drawing.Size(33, 160);
            this.listMonth.TabIndex = 0;
            this.listMonth.SelectedIndexChanged += new System.EventHandler(this.listMonth_SelectedIndexChanged);
            // 
            // listYear
            // 
            this.listYear.FormattingEnabled = true;
            this.listYear.Items.AddRange(new object[] {
            "2010",
            "2011",
            "2012"});
            this.listYear.Location = new System.Drawing.Point(3, 189);
            this.listYear.Name = "listYear";
            this.listYear.Size = new System.Drawing.Size(32, 43);
            this.listYear.TabIndex = 1;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(3, 3);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 2;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // cboQuantity
            // 
            this.cboQuantity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboQuantity.FormattingEnabled = true;
            this.cboQuantity.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cboQuantity.Location = new System.Drawing.Point(162, 72);
            this.cboQuantity.Name = "cboQuantity";
            this.cboQuantity.Size = new System.Drawing.Size(51, 160);
            this.cboQuantity.TabIndex = 4;
            this.cboQuantity.Text = "1";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantity.Location = new System.Drawing.Point(159, 56);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(54, 13);
            this.lblQuantity.TabIndex = 5;
            this.lblQuantity.Text = "Quantity";
            // 
            // lblFraction
            // 
            this.lblFraction.AutoSize = true;
            this.lblFraction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFraction.Location = new System.Drawing.Point(219, 56);
            this.lblFraction.Name = "lblFraction";
            this.lblFraction.Size = new System.Drawing.Size(53, 13);
            this.lblFraction.TabIndex = 7;
            this.lblFraction.Text = "Fraction";
            // 
            // cboFraction
            // 
            this.cboFraction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboFraction.FormattingEnabled = true;
            this.cboFraction.Items.AddRange(new object[] {
            "-",
            "1/8",
            "1/4",
            "3/8",
            "1/2",
            "5/8",
            "3/4",
            "7/8"});
            this.cboFraction.Location = new System.Drawing.Point(222, 72);
            this.cboFraction.Name = "cboFraction";
            this.cboFraction.Size = new System.Drawing.Size(38, 134);
            this.cboFraction.TabIndex = 6;
            this.cboFraction.Text = "-";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioSell);
            this.panel1.Controls.Add(this.radioBuy);
            this.panel1.Location = new System.Drawing.Point(162, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 32);
            this.panel1.TabIndex = 13;
            // 
            // radioSell
            // 
            this.radioSell.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioSell.AutoSize = true;
            this.radioSell.Location = new System.Drawing.Point(53, 4);
            this.radioSell.Name = "radioSell";
            this.radioSell.Size = new System.Drawing.Size(34, 23);
            this.radioSell.TabIndex = 14;
            this.radioSell.TabStop = true;
            this.radioSell.Text = "Sell";
            this.radioSell.UseVisualStyleBackColor = true;
            this.radioSell.CheckedChanged += new System.EventHandler(this.radioSell_CheckedChanged);
            // 
            // radioBuy
            // 
            this.radioBuy.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioBuy.AutoSize = true;
            this.radioBuy.Location = new System.Drawing.Point(9, 4);
            this.radioBuy.Name = "radioBuy";
            this.radioBuy.Size = new System.Drawing.Size(35, 23);
            this.radioBuy.TabIndex = 13;
            this.radioBuy.TabStop = true;
            this.radioBuy.Text = "Buy";
            this.radioBuy.UseVisualStyleBackColor = true;
            this.radioBuy.CheckedChanged += new System.EventHandler(this.radioBuy_CheckedChanged);
            // 
            // lviewStrike
            // 
            this.lviewStrike.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCall,
            this.columnPut});
            this.lviewStrike.Location = new System.Drawing.Point(41, 23);
            this.lviewStrike.MultiSelect = false;
            this.lviewStrike.Name = "lviewStrike";
            this.lviewStrike.Size = new System.Drawing.Size(112, 209);
            this.lviewStrike.TabIndex = 15;
            this.lviewStrike.UseCompatibleStateImageBehavior = false;
            this.lviewStrike.View = System.Windows.Forms.View.Details;
            // 
            // columnCall
            // 
            this.columnCall.Text = "call";
            this.columnCall.Width = 40;
            // 
            // columnPut
            // 
            this.columnPut.Text = "put";
            this.columnPut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnPut.Width = 40;
            // 
            // SpreadPanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lviewStrike);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblFraction);
            this.Controls.Add(this.cboFraction);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.cboQuantity);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.listYear);
            this.Controls.Add(this.listMonth);
            this.Name = "SpreadPanelControl";
            this.Size = new System.Drawing.Size(272, 236);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listMonth;
        private System.Windows.Forms.ListBox listYear;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.ComboBox cboQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblFraction;
        private System.Windows.Forms.ComboBox cboFraction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioSell;
        private System.Windows.Forms.RadioButton radioBuy;
        private System.Windows.Forms.ListView lviewStrike;
        private System.Windows.Forms.ColumnHeader columnCall;
        private System.Windows.Forms.ColumnHeader columnPut;
    }
}
