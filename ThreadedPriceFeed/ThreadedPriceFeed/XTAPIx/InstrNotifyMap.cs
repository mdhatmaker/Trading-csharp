using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ThreadedPriceFeed.XTAPIx
{
	/// <summary>
	/// Summary description for InstrNotify.
	/// </summary>
	abstract public class InstrNotifyMap : InstrMap, IDisposable
	{
		// XTAPI instrument notify class
		XTAPI.TTInstrNotify	m_InstrNotify;

		public InstrNotifyMap()
		{
		}
		public void XTAPIInit()
		{
			if (m_InstrNotify==null)
			{
				// hook up our notify stuff
				m_InstrNotify=new XTAPI.TTInstrNotifyClass();
				m_InstrNotify.OnNotifyFound+=new XTAPI._ITTInstrNotifyEvents_OnNotifyFoundEventHandler(m_InstrNotify_OnNotifyFound);
				m_InstrNotify.OnNotifyUpdate+=new XTAPI._ITTInstrNotifyEvents_OnNotifyUpdateEventHandler(m_InstrNotify_OnNotifyUpdate);
			}
		}

		public void Dispose()
		{
			if (m_InstrNotify!=null)
			{
				// we have an Notify object, we need to detach all of the instruments

				// stop updates 
				m_InstrNotify.EnablePriceUpdates=0;
				m_InstrNotify.EnableDepthUpdates=0;

				// remove all of our instruments
				foreach (DictionaryEntry DE in this)
				{
					// cast the enty
					InstrObj tmpInstr=(InstrObj)DE.Value;

					// attempt to detach it 
					tmpInstr.CloseConnection(m_InstrNotify);
				}

				// clear our data
				Clear();

				// deref our Notify object
				m_InstrNotify=null;
			}
		}

		override public bool AddInstr(InstrObj instr)
		{
			if (m_InstrNotify!=null && base.AddInstr(instr))
			{
				// just added this, ask it too connect too our notify object
				instr.InitConnection(m_InstrNotify);

				// worked
				return true;
			}
			// already existed
			return false;
		}
		override public bool DelInstr(InstrObj instr)
		{
			if (m_InstrNotify!=null && base.DelInstr(instr))
			{
				// detach our instrument
				instr.CloseConnection(m_InstrNotify);

				// worked
				return true;
			}
			// did not exist
			return false;
		}
		private void m_InstrNotify_OnNotifyFound(XTAPI.TTInstrNotify pNotify, XTAPI.TTInstrObj pInstr)
		{
			// match up our instrument too our InstrObj class
			InstrObj instr=Find(pInstr.GetHashCode());
			if (instr!=null)
			{
				// found the instrument, call our notify
				OnNotifyFound(instr);
			}
		}

		private void m_InstrNotify_OnNotifyUpdate(XTAPI.TTInstrNotify pNotify, XTAPI.TTInstrObj pInstr)
		{
			// match up our instrument too our InstrObj class
			InstrObj instr=Find(pInstr.GetHashCode());
			if (instr!=null)
			{
				// found the instrument, call our notify
				OnNotifyUpdate(instr);
			}
		}
		protected abstract void OnNotifyFound(InstrObj instr);
		protected abstract void OnNotifyUpdate(InstrObj instr);
	}
}
