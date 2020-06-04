using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACSMCOMPONENTS24Lib;
using Autodesk.DesignScript.Runtime;
using AXDBLib;

namespace AMR.dynSSetMgr
{
    public partial class Sheet
    {
        AcSmSheet _curSheet;

        internal Sheet(AcSmSheet rootSheetObject)
        {
            _curSheet = rootSheetObject;
        }

        internal AcSmSheet BaseObject()
        { return _curSheet; }

        internal string Name
        {
            get => _curSheet.GetName();
            set => _curSheet.SetName(value);
        }

        internal string Description
        {
            get => _curSheet.GetDesc();
            set => _curSheet.SetDesc(value);
        }

        internal Database GetDatabase()
        {
            return new Database(_curSheet.GetDatabase());
        }
	}
}
