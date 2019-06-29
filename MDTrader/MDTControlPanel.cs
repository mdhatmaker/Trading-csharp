using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace TT.SP.Trading.Controls.MDTrader
{
	/// <summary>
	/// Summary description for MDTControlPanel.
	/// </summary>
	public class MDTControlPanel : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private System.Windows.Forms.Cursor _oldCursor;
        private bool _bMouseDown = false;
        private int _diffX = 0;
        private int _diffY = 0;
        private Point _grabOffset;

        private Syncfusion.Windows.Forms.Tools.GradientPanel panel1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.ComboBox comboBox1;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.Tools.GradientLabel gradientLabel1;
        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtn1;
        private System.Windows.Forms.NumericUpDown _orderQtyBtn;
        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtnClear;
        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtn5;
        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtn4;
        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtn3;
        private Syncfusion.Windows.Forms.ButtonAdv _qtyBtn2;
        

		public MDTControlPanel()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: AddOrUpdate any initialization after the InitializeComponent call

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this._qtyBtnClear = new Syncfusion.Windows.Forms.ButtonAdv();
            this._qtyBtn5 = new Syncfusion.Windows.Forms.ButtonAdv();
            this._qtyBtn4 = new Syncfusion.Windows.Forms.ButtonAdv();
            this._qtyBtn3 = new Syncfusion.Windows.Forms.ButtonAdv();
            this._qtyBtn2 = new Syncfusion.Windows.Forms.ButtonAdv();
            this._qtyBtn1 = new Syncfusion.Windows.Forms.ButtonAdv();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this._orderQtyBtn = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel1 = new Syncfusion.Windows.Forms.Tools.GradientLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._orderQtyBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.SystemColors.InactiveBorder, System.Drawing.SystemColors.ControlDark);
            this.panel1.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.panel1.BorderColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.gradientLabel1);
            this.panel1.Controls.Add(this.gradientPanel1);
            this.panel1.Controls.Add(this._qtyBtnClear);
            this.panel1.Controls.Add(this._qtyBtn5);
            this.panel1.Controls.Add(this._qtyBtn4);
            this.panel1.Controls.Add(this._qtyBtn3);
            this.panel1.Controls.Add(this._qtyBtn2);
            this.panel1.Controls.Add(this._qtyBtn1);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.numericUpDown2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this._orderQtyBtn);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 166);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint_2);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gradientPanel1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.BackwardDiagonal, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.ActiveCaption);
            this.gradientPanel1.BorderColor = System.Drawing.Color.Black;
            this.gradientPanel1.BorderSides = System.Windows.Forms.Border3DSide.Bottom;
            this.gradientPanel1.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(194, 16);
            this.gradientPanel1.TabIndex = 56;
            this.gradientPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.gradientPanel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.gradientPanel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            // 
            // _qtyBtnClear
            // 
            this._qtyBtnClear.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtnClear.BackColor = System.Drawing.SystemColors.ControlLight;
            this._qtyBtnClear.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtnClear.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtnClear.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._qtyBtnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtnClear.Location = new System.Drawing.Point(148, 134);
            this._qtyBtnClear.Name = "_qtyBtnClear";
            this._qtyBtnClear.Size = new System.Drawing.Size(44, 24);
            this._qtyBtnClear.TabIndex = 55;
            this._qtyBtnClear.Text = "CLR";
            this._qtyBtnClear.UseVisualStyle = false;
            this._qtyBtnClear.Click += new System.EventHandler(this._btnQtyClr_Click);
            // 
            // _qtyBtn5
            // 
            this._qtyBtn5.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtn5.BackColor = System.Drawing.SystemColors.ControlLight;
            this._qtyBtn5.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtn5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtn5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._qtyBtn5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtn5.Location = new System.Drawing.Point(102, 134);
            this._qtyBtn5.Name = "_qtyBtn5";
            this._qtyBtn5.Size = new System.Drawing.Size(44, 24);
            this._qtyBtn5.TabIndex = 54;
            this._qtyBtn5.Text = "5";
            this._qtyBtn5.UseVisualStyle = false;
            this._qtyBtn5.Click += new System.EventHandler(this.OnQtyBtnClick);
            // 
            // _qtyBtn4
            // 
            this._qtyBtn4.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtn4.BackColor = System.Drawing.SystemColors.ControlLight;
            this._qtyBtn4.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtn4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtn4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._qtyBtn4.ForeColor = System.Drawing.SystemColors.ControlText;
            this._qtyBtn4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtn4.Location = new System.Drawing.Point(148, 108);
            this._qtyBtn4.Name = "_qtyBtn4";
            this._qtyBtn4.Size = new System.Drawing.Size(44, 24);
            this._qtyBtn4.TabIndex = 53;
            this._qtyBtn4.Text = "50";
            this._qtyBtn4.UseVisualStyle = false;
            this._qtyBtn4.Click += new System.EventHandler(this.OnQtyBtnClick);
            // 
            // _qtyBtn3
            // 
            this._qtyBtn3.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtn3.BackColor = System.Drawing.SystemColors.ControlLight;
            this._qtyBtn3.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtn3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtn3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._qtyBtn3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtn3.Location = new System.Drawing.Point(102, 108);
            this._qtyBtn3.Name = "_qtyBtn3";
            this._qtyBtn3.Size = new System.Drawing.Size(44, 24);
            this._qtyBtn3.TabIndex = 52;
            this._qtyBtn3.Text = "2";
            this._qtyBtn3.UseVisualStyle = false;
            this._qtyBtn3.Click += new System.EventHandler(this.OnQtyBtnClick);
            // 
            // _qtyBtn2
            // 
            this._qtyBtn2.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtn2.BackColor = System.Drawing.SystemColors.ControlLight;
            this._qtyBtn2.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtn2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtn2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._qtyBtn2.ForeColor = System.Drawing.SystemColors.ControlText;
            this._qtyBtn2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtn2.Location = new System.Drawing.Point(148, 82);
            this._qtyBtn2.Name = "_qtyBtn2";
            this._qtyBtn2.Size = new System.Drawing.Size(44, 24);
            this._qtyBtn2.TabIndex = 51;
            this._qtyBtn2.Text = "10";
            this._qtyBtn2.UseVisualStyle = false;
            this._qtyBtn2.Click += new System.EventHandler(this.OnQtyBtnClick);
            // 
            // _qtyBtn1
            // 
            this._qtyBtn1.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.WindowsXP;
            this._qtyBtn1.BackColor = System.Drawing.SystemColors.ControlLight;
            this._qtyBtn1.ComboEditBackColor = System.Drawing.Color.Empty;
            this._qtyBtn1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._qtyBtn1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._qtyBtn1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._qtyBtn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._qtyBtn1.Location = new System.Drawing.Point(102, 82);
            this._qtyBtn1.Name = "_qtyBtn1";
            this._qtyBtn1.Size = new System.Drawing.Size(44, 24);
            this._qtyBtn1.TabIndex = 50;
            this._qtyBtn1.Text = "1";
            this._qtyBtn1.UseVisualStyle = false;
            this._qtyBtn1.Click += new System.EventHandler(this.OnQtyBtnClick);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button8.Enabled = false;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.button8.Location = new System.Drawing.Point(50, 108);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(44, 24);
            this.button8.TabIndex = 49;
            this.button8.Text = "IOC";
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button9.Enabled = false;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.button9.Location = new System.Drawing.Point(4, 108);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(44, 24);
            this.button9.TabIndex = 48;
            this.button9.Text = "SM";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.button7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button7.Enabled = false;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.button7.Location = new System.Drawing.Point(50, 82);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(44, 24);
            this.button7.TabIndex = 47;
            this.button7.Text = "SL";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Yellow;
            this.button6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.button6.Location = new System.Drawing.Point(4, 82);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(44, 24);
            this.button6.TabIndex = 46;
            this.button6.Text = "Limit";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.numericUpDown2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.numericUpDown2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.numericUpDown2.Location = new System.Drawing.Point(4, 136);
            this.numericUpDown2.Maximum = new System.Decimal(new int[] {
                                                                           1000000,
                                                                           0,
                                                                           0,
                                                                           0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown2.TabIndex = 17;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown2.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUpDown2.Value = new System.Decimal(new int[] {
                                                                         123,
                                                                         0,
                                                                         0,
                                                                         0});
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.comboBox1.Location = new System.Drawing.Point(4, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 24);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "<Default>";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // _orderQtyBtn
            // 
            this._orderQtyBtn.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._orderQtyBtn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._orderQtyBtn.Cursor = System.Windows.Forms.Cursors.Arrow;
            this._orderQtyBtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this._orderQtyBtn.Location = new System.Drawing.Point(100, 48);
            this._orderQtyBtn.Maximum = new System.Decimal(new int[] {
                                                                         1000000,
                                                                         0,
                                                                         0,
                                                                         0});
            this._orderQtyBtn.Name = "_orderQtyBtn";
            this._orderQtyBtn.Size = new System.Drawing.Size(92, 26);
            this._orderQtyBtn.TabIndex = 5;
            this._orderQtyBtn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0)));
            this.gradientLabel1.BackgroundColor = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.ForwardDiagonal, System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0))), System.Drawing.Color.Maroon);
            this.gradientLabel1.BorderSides = ((System.Windows.Forms.Border3DSide)((((System.Windows.Forms.Border3DSide.Left | System.Windows.Forms.Border3DSide.Top) 
                | System.Windows.Forms.Border3DSide.Right) 
                | System.Windows.Forms.Border3DSide.Bottom)));
            this.gradientLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.gradientLabel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gradientLabel1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.gradientLabel1.ForeColor = System.Drawing.Color.White;
            this.gradientLabel1.Location = new System.Drawing.Point(4, 48);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(92, 28);
            this.gradientLabel1.TabIndex = 58;
            this.gradientLabel1.Text = "1002";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MDTControlPanel
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.Name = "MDTControlPanel";
            this.Size = new System.Drawing.Size(198, 166);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._orderQtyBtn)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        private void panel1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        
        }

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _bMouseDown = true;

            _grabOffset = System.Windows.Forms.Cursor.Position;

            Rectangle rrr = this.Parent.RectangleToScreen( Bounds );
            _diffX = _grabOffset.X - rrr.X;
            _diffY = _grabOffset.Y - rrr.Y;
        }

        private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _bMouseDown = false;
            Cursor.Current = _oldCursor;
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if ( _bMouseDown )
            {
                if ( Cursor.Current != Cursors.Hand )
                {
                    _oldCursor = Cursors.Hand;
                    Cursor.Current = Cursors.Hand;
                }

                Point p = Parent.PointToClient(System.Windows.Forms.Cursor.Position);
                
                Parent.SuspendLayout();

                

                int newLeft = p.X - _diffX;
                int newTop = p.Y - _diffY;
                
                // Snap-to the left 
                if ( Left > 10 && newLeft <=20 )
                {
                    Point ptr = System.Windows.Forms.Cursor.Position;
                    ptr.X -= newLeft;
                    System.Windows.Forms.Cursor.Position = ptr;
                    newLeft = 0;
                }

                // Snap-to the Top
                if ( Top > 20 && newTop <=20 )
                {
                    Point ptr = System.Windows.Forms.Cursor.Position;
                    ptr.Y -= newTop;
                    System.Windows.Forms.Cursor.Position = ptr;
                    
                    newTop = 0;
                }

                if ( newTop < 0 )
                    newTop = 0;

                if ( newLeft < 0 )
                    newLeft = 0 ;

                
                if ( Top + Height < Parent.ClientRectangle.Height -20 
                    && newTop + Height >= Parent.ClientRectangle.Height -20 )
                {
                    int adjustedTop = Parent.ClientRectangle.Bottom - this.Height;
                    int movedYAmount = adjustedTop - newTop;

                    Point ptr = System.Windows.Forms.Cursor.Position;
                    ptr.Y += movedYAmount;
                    System.Windows.Forms.Cursor.Position = ptr;
                        
                    newTop = adjustedTop;

//                    this.Anchor = AnchorStyles.Bottom;
                }
                else
                {
//                    this.Anchor = AnchorStyles.None;
                }

                if ( newTop + Height > Parent.ClientRectangle.Height )
                    newTop = Parent.ClientRectangle.Height - Height;

                if ( newLeft + Width > Parent.ClientRectangle.Width )
                    newLeft = Parent.ClientRectangle.Width - Height;


                Left = newLeft;
                Top = newTop;

                Parent.ResumeLayout();
                Parent.Refresh(); 

                
            }
        }

        private void panel1_Paint_1(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }

        private void panel1_Paint_2(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        
        }

        private void _btnQtyClr_Click(object sender, System.EventArgs e)
        {
            _orderQtyBtn.Value = 0;
        }

        private void OnQtyBtnClick(object sender, System.EventArgs e)
        {
            _orderQtyBtn.Value += System.Int32.Parse( (sender as System.Windows.Forms.Button).Text );

            /*
            int val = _orderQtyBtn.Value;
            val -= System.Int32.Parse( (sender as System.Windows.Forms.Button).Text );

            if ( val < 0 )
                val = 0;
    
            _orderQtyBtn.Value = val;
*/
        }



	}
}
