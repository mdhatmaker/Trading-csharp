using System;
using System.Collections;

namespace ThreadedPriceFeed.XTAPIx
{
	/// <summary>
	/// Summary description for InstrMap.
	/// </summary>
	public class InstrMap : DictionaryBase
	{
		public InstrMap()
		{
		}
		public InstrObj Find(int iInstID)
		{
			if (Dictionary.Contains(iInstID))
			{
				// we have it !!
				return (InstrObj)Dictionary[iInstID];
			}
			// not found 
			return null;
		}
		virtual public bool AddInstr(InstrObj Instr)
		{
			int iID=Instr.GetHashCode();
			if (!Dictionary.Contains(iID))
			{
				// did not contain this, add it
				Dictionary[iID]=Instr;

				// worked
				return true;
			}
			return false;
		}
		virtual public bool DelInstr(InstrObj Instr)
		{
			int iID=Instr.GetHashCode();
			if (Dictionary.Contains(iID))
			{
				// found it, remove it from our map
				Dictionary.Remove(iID);
				return true;
			}
			return false;
		}
	}
}
