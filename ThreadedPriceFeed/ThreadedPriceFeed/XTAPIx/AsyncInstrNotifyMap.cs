using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace ThreadedPriceFeed.XTAPIx
{
	/// <summary>
	/// Summary description for AsyncInstrNotifyMap.
	/// </summary>
	abstract public class AsyncInstrNotifyMap : InstrNotifyMap
	{
		// context switch delegate
		delegate bool InvokeHandler(InstrObj instr);

		// worker thread
		Thread	m_XTAPIThd;

		// Message loop context
		ApplicationContext	m_ThdContext;

		// context switch stuff
		Form			m_frmInvoke;
		InvokeHandler	m_CallAddInstr;
		InvokeHandler	m_CallDelInstr;

		public AsyncInstrNotifyMap()
		{
			// cache some context switch delegates
			m_CallAddInstr=new InvokeHandler(OnAddInstr);
			m_CallDelInstr=new InvokeHandler(OnDelInstr);
		}

		new public void Dispose()
		{
			if (m_XTAPIThd!=null)
			{
				// make sure our thread is dead
				ShutDown();
			} 
			else 
			{
				// call our parent
				base.Dispose();
			}
		}

		public void StartXTAPIThd()
		{
			if (m_XTAPIThd==null || !m_XTAPIThd.IsAlive)
			{
				// not running or stopped, create a new one
				m_XTAPIThd=new Thread(new ThreadStart(OnThreadStart));
				m_XTAPIThd.TrySetApartmentState( System.Threading.ApartmentState.STA);

				// start it running
				m_XTAPIThd.Start();
			}
		}
		public void ShutDown()
		{
			if (m_XTAPIThd!=null && m_XTAPIThd.IsAlive)
			{
				// tell the context to quit
				m_ThdContext.ExitThread();

				// wait for termination
				m_XTAPIThd.Join();
			}
		}
		void OnThreadStart()
		{
			// starting point for our handler
			m_ThdContext=new ApplicationContext();

			// create our notify stuff (as we are in the right thread now)
			XTAPIInit();

			// create ourselves a generic (hidden window)
			m_frmInvoke=new Form();

			// force creation of the window itself
			IntPtr hWnd=m_frmInvoke.Handle;

			// start the message pump for us (this blocks)
			Application.Run(m_ThdContext);

			// tidy up our map stuff
			base.Dispose();
		}
		override public bool AddInstr(InstrObj instr)
		{
			if (m_XTAPIThd!=null)
			{
				// make the call using our hidden window
				object[] args=new object[1];
				args[0]=instr;
				return (bool)m_frmInvoke.Invoke(m_CallAddInstr,args);
			} 
			else 
			{
				// no thread, just call through
				return base.AddInstr(instr);
			}
		}
		bool OnAddInstr(InstrObj instr)
		{
			// just call through
			return base.AddInstr(instr);
		}
		override public bool DelInstr(InstrObj instr)
		{
			if (m_XTAPIThd!=null)
			{
				// make the call using our hidden window
				object[] args=new object[1];
				args[0]=instr;
				return (bool)m_frmInvoke.Invoke(m_CallDelInstr,args);
			} 
			else 
			{
				// no thread, just call through
				return base.AddInstr(instr);
			}
		}
		bool OnDelInstr(InstrObj instr)
		{
			// just call through
			return base.DelInstr(instr);
		}
	}
}
