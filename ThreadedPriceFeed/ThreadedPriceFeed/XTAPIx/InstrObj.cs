using System;
using System.Runtime.InteropServices;

namespace ThreadedPriceFeed.XTAPIx
{
	/// <summary>
	/// Summary description for InstrObj.
	/// </summary>
	public class InstrObj : IDisposable
	{
		// ID for this instrument
		public int m_iInstrObj;

		// instrument definition stuff
		public String	m_sExchange;
		public String	m_sProduct;
		public String	m_sProdType;
		public String	m_sContract;
		public String	m_sSeriesKey;

		public bool	m_bNeedDepth = false;

		// XTAPI instrument object itself
		public XTAPI.TTInstrObj m_InstrObj;

		public InstrObj()
		{
		}

		public void Dispose()
		{
			if (m_InstrObj!=null)
			{
				// deref the object
				m_InstrObj=null;
			}
		}

		// HashCode is actually generated off of the COM instrument itself
		// (so we can use the COM object to find this object)
		public override int GetHashCode()
		{
			if (m_InstrObj==null)
			{
				// no object as yet, we need to initialise one
				InitInstrObj();
			}

			// return the ID
			return m_iInstrObj;
		}
		public bool IsComplete()
		{
			// sanity check our data
			return 
			(
				m_sExchange!=null && 
				m_sProduct!=null && 
				m_sProdType!=null && 
				(m_sContract!=null || m_sSeriesKey!=null)
			);
		}
		// create the COM object from this internal data
		protected bool InitInstrObj()
		{
			if (IsComplete())
			{
				if (m_InstrObj==null)
				{
					// no instrument yet, create one
					m_InstrObj=new XTAPI.TTInstrObj();

					// generate a unique ID
					m_iInstrObj=m_InstrObj.GetHashCode();

					// setup the data
					m_InstrObj.Exchange	= m_sExchange;
					m_InstrObj.Product	= m_sProduct;
					m_InstrObj.ProdType	= m_sProdType;
					m_InstrObj.Contract	= m_sContract;
					m_InstrObj.SeriesKey= m_sSeriesKey;					

					// attempt to open it
					m_InstrObj.Open(m_bNeedDepth?1:0);
				}
				// probably worked !!!
				return true;
			}
			// not enough data !!!
			return false;
		}
		virtual public bool InitConnection(XTAPI.TTInstrNotify InstrNotify)
		{
			try
			{
				// connect this instrument
				InstrNotify.AttachInstrument(m_InstrObj);
			} 
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			return true;
		}
		virtual public bool CloseConnection(XTAPI.TTInstrNotify InstrNotify)
		{
			if (m_InstrObj!=null)
			{
				// detach our instrument
				InstrNotify.DetachInstrument(m_InstrObj);
			}
			// always worked
			return true;
		}
	}
}
