namespace LevelTrader
{
    partial class LevelTraderMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelTraderMainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbManagementFormula = new System.Windows.Forms.ToolStripDropDownButton();
            this.itemConstantDollar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFixedQuantity = new System.Windows.Forms.ToolStripMenuItem();
            this.itemFixedFraction = new System.Windows.Forms.ToolStripMenuItem();
            this.itemKellysCriterion = new System.Windows.Forms.ToolStripMenuItem();
            this.itemOptimalF = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbFormulaOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbManagementFormula,
            this.tsbFormulaOptions});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(344, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbManagementFormula
            // 
            this.tsbManagementFormula.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemConstantDollar,
            this.itemFixedQuantity,
            this.itemFixedFraction,
            this.itemKellysCriterion,
            this.itemOptimalF});
            this.tsbManagementFormula.Image = global::LevelTrader.Properties.Resources.Data_Schema;
            this.tsbManagementFormula.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManagementFormula.Name = "tsbManagementFormula";
            this.tsbManagementFormula.Size = new System.Drawing.Size(103, 22);
            this.tsbManagementFormula.Text = "Mgmt Formula";
            // 
            // itemConstantDollar
            // 
            this.itemConstantDollar.Name = "itemConstantDollar";
            this.itemConstantDollar.Size = new System.Drawing.Size(159, 22);
            this.itemConstantDollar.Text = "Constant Dollar";
            this.itemConstantDollar.Click += new System.EventHandler(this.itemConstantDollar_Click);
            // 
            // itemFixedQuantity
            // 
            this.itemFixedQuantity.Name = "itemFixedQuantity";
            this.itemFixedQuantity.Size = new System.Drawing.Size(159, 22);
            this.itemFixedQuantity.Text = "Fixed Quantity";
            this.itemFixedQuantity.Click += new System.EventHandler(this.itemFixedQuantity_Click);
            // 
            // itemFixedFraction
            // 
            this.itemFixedFraction.Name = "itemFixedFraction";
            this.itemFixedFraction.Size = new System.Drawing.Size(159, 22);
            this.itemFixedFraction.Text = "Fixed Fraction";
            this.itemFixedFraction.Click += new System.EventHandler(this.itemFixedFraction_Click);
            // 
            // itemKellysCriterion
            // 
            this.itemKellysCriterion.Name = "itemKellysCriterion";
            this.itemKellysCriterion.Size = new System.Drawing.Size(159, 22);
            this.itemKellysCriterion.Text = "Kelly\'s Criterion";
            this.itemKellysCriterion.Click += new System.EventHandler(this.itemKellysCriterion_Click);
            // 
            // itemOptimalF
            // 
            this.itemOptimalF.Name = "itemOptimalF";
            this.itemOptimalF.Size = new System.Drawing.Size(159, 22);
            this.itemOptimalF.Text = "Optimal F";
            this.itemOptimalF.Click += new System.EventHandler(this.itemOptimalF_Click);
            // 
            // tsbFormulaOptions
            // 
            this.tsbFormulaOptions.Image = global::LevelTrader.Properties.Resources.otheroptions;
            this.tsbFormulaOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFormulaOptions.Name = "tsbFormulaOptions";
            this.tsbFormulaOptions.Size = new System.Drawing.Size(105, 22);
            this.tsbFormulaOptions.Text = "Formula Options";
            this.tsbFormulaOptions.Click += new System.EventHandler(this.tsbFormulaOptions_Click);
            // 
            // LevelTraderMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 114);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LevelTraderMainForm";
            this.Text = "LevelTrader";
            this.TopMost = true;
            this.DragLeave += new System.EventHandler(this.LevelTraderMainForm_DragLeave);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.LevelTraderMainForm_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton tsbManagementFormula;
        private System.Windows.Forms.ToolStripMenuItem itemConstantDollar;
        private System.Windows.Forms.ToolStripMenuItem itemFixedQuantity;
        private System.Windows.Forms.ToolStripMenuItem itemFixedFraction;
        private System.Windows.Forms.ToolStripMenuItem itemKellysCriterion;
        private System.Windows.Forms.ToolStripMenuItem itemOptimalF;
        private System.Windows.Forms.ToolStripButton tsbFormulaOptions;

    }
}