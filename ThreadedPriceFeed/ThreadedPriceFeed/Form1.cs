using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

using ThreadedPriceFeed.XTAPIx;

namespace ThreadedPriceFeed
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		// overriden 'notify map' that is hard wired 
		// too our update function (Form1.UpdateDisplay)
		class InstrFeed : AsyncInstrNotifyMap
		{
			// form too tell about the update
			Form1			m_frm;

			// cached invoke data
			MethodInvoker	m_OnUpdate;
			IAsyncResult	m_OnUpdateResult;

			public InstrFeed(Form1 frm)
			{
				// save the form
				m_frm=frm;

				// construct our updater
				m_OnUpdate=new MethodInvoker(m_frm.UpdateDisplay);
			}

			// overriden dispose so we can 'disable' our updates before calling down
			new public void Dispose()
			{
				// remove our window reference (so we don't allow through any more calls)
				m_frm=null;

				// call our parent
				base.Dispose();
			}

			// callback from the XTAPI (same thread as the original callback)
			override protected void OnNotifyFound(InstrObj instr)
			{
				// change our cpation
				m_frm.Text=instr.m_InstrObj.Contract;
	
				// treat this as a simple 'update
				OnNotifyUpdate(instr);
			}
			// callback from the XTAPI (same thread as the original callback)
			override protected void OnNotifyUpdate(InstrObj instr)
			{
				if (m_frm!=null && (m_OnUpdateResult==null || m_OnUpdateResult.IsCompleted))
				{
					// we have a window + (no prexisting BeginInvoke OR last BeginInvoke has been proccessed)
					// start another update
					m_OnUpdateResult=m_frm.BeginInvoke(m_OnUpdate);
				}
			}
		};

		XTAPI.TTDropHandlerClass	m_DropHandler;
		InstrFeed					m_InstrNotify;
		InstrObj					m_InstrObj;

		private System.Windows.Forms.Label lblValue1;
		private System.Windows.Forms.Label lblValue2;
		private System.Windows.Forms.Label lblValue3;
		private System.Windows.Forms.Label lblValue4;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			// init drag and drop 
			m_DropHandler=new XTAPI.TTDropHandlerClass();
			m_DropHandler.OnNotifyDrop+=new XTAPI._ITTDropHandlerEvents_OnNotifyDropEventHandler(m_DropHandler_OnNotifyDrop);
			m_DropHandler.RegisterDropWindow((int)Handle);

			// init the notify stuff
			m_InstrNotify=new InstrFeed(this);

			if (true)
			{
				// startup our secondary thread
				m_InstrNotify.StartXTAPIThd();
			} 
			else 
			{
				// ELSE init out XTAPI notifiy object (using THIS thread)
				m_InstrNotify.XTAPIInit();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (m_InstrNotify!=null)
				{
					// make sure our price feed has been derefed
					m_InstrNotify.Dispose();
					m_InstrNotify=null;
				}
				if (m_DropHandler!=null)
				{
					// deref this NOW
					m_DropHandler=null;
				}
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblValue1 = new System.Windows.Forms.Label();
			this.lblValue2 = new System.Windows.Forms.Label();
			this.lblValue3 = new System.Windows.Forms.Label();
			this.lblValue4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblValue1
			// 
			this.lblValue1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblValue1.Location = new System.Drawing.Point(8, 16);
			this.lblValue1.Name = "lblValue1";
			this.lblValue1.Size = new System.Drawing.Size(96, 24);
			this.lblValue1.TabIndex = 0;
			this.lblValue1.Tag = "BidQty";
			this.lblValue1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblValue2
			// 
			this.lblValue2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblValue2.Location = new System.Drawing.Point(112, 16);
			this.lblValue2.Name = "lblValue2";
			this.lblValue2.Size = new System.Drawing.Size(96, 24);
			this.lblValue2.TabIndex = 1;
			this.lblValue2.Tag = "Bid";
			this.lblValue2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblValue3
			// 
			this.lblValue3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblValue3.Location = new System.Drawing.Point(216, 16);
			this.lblValue3.Name = "lblValue3";
			this.lblValue3.Size = new System.Drawing.Size(96, 24);
			this.lblValue3.TabIndex = 2;
			this.lblValue3.Tag = "Ask";
			this.lblValue3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblValue4
			// 
			this.lblValue4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblValue4.Location = new System.Drawing.Point(320, 16);
			this.lblValue4.Name = "lblValue4";
			this.lblValue4.Size = new System.Drawing.Size(96, 24);
			this.lblValue4.TabIndex = 3;
			this.lblValue4.Tag = "AskQty";
			this.lblValue4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 69);
			this.Controls.Add(this.lblValue4);
			this.Controls.Add(this.lblValue3);
			this.Controls.Add(this.lblValue2);
			this.Controls.Add(this.lblValue1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			{
				// create/run our window (restrict the 
				// scope so our form has gone before we GC
				Application.Run(new Form1());
			}

			// make sure we have collected BEFORE we let .NET unload anything
			System.GC.Collect();
			System.GC.WaitForPendingFinalizers();
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// close our feed before our window goes away
			m_InstrNotify.Dispose();
			m_InstrNotify=null;
		}
	
		private void m_DropHandler_OnNotifyDrop()
		{
			if (m_InstrObj!=null)
			{
				// disconnect this first
				m_InstrNotify.DelInstr(m_InstrObj);
				m_InstrObj=null;
			}

			// clear our display
			UpdateDisplay(null);
			Text="";

			// get the dropped instrument
			XTAPI.TTInstrObj tmpInstr=(XTAPI.TTInstrObj)m_DropHandler[1];

			// setup our the new one
			m_InstrObj=new InstrObj();
			m_InstrObj.m_sExchange=tmpInstr.Exchange;
			m_InstrObj.m_sProduct=tmpInstr.Product;
			m_InstrObj.m_sProdType=tmpInstr.ProdType;
			m_InstrObj.m_sSeriesKey=tmpInstr.SeriesKey;

			// attach it 
			m_InstrNotify.AddInstr(m_InstrObj);

			// clear the handler
			m_DropHandler.Reset();
		}
		private void UpdateDisplay()
		{
			UpdateDisplay(m_InstrObj.m_InstrObj);
		}

		private void UpdateDisplay(XTAPI.TTInstrObj pInstr)
		{
			if (pInstr!=null)
			{
				// we have an instrument, get the values
				object o=(object[])pInstr.get_Get("BidQty,Bid,Ask,AskQty");
				if (o.GetType().IsArray)
				{
					// looks like a type
					object[] aV=(object[])o;

					if (aV[0]!=null)	lblValue1.Text=aV[0].ToString(); else lblValue1.Text="";
					if (aV[1]!=null)	lblValue2.Text=aV[1].ToString(); else lblValue2.Text="";
					if (aV[2]!=null)	lblValue3.Text=aV[2].ToString(); else lblValue3.Text="";
					if (aV[3]!=null)	lblValue4.Text=aV[3].ToString(); else lblValue4.Text="";

					// quit here
					return;
				}
			} 
			// all else fails, clear everything
			lblValue1.Text="N/A";
			lblValue2.Text="N/A";
			lblValue3.Text="N/A";
			lblValue4.Text="N/A";
		}
	}
}
