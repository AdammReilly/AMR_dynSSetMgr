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
    [IsVisibleInDynamoLibrary(false)]
    public class AMRSheet
    {
        AcSmSheet _curSheet;

        public AMRSheet(AcSmSheet rootSheetObject)
        {
            _curSheet = rootSheetObject;
        }

        public AcSmSheet BaseObject()
        { return _curSheet; }

        public string Name
        {
            get => _curSheet.GetName();
            set => _curSheet.SetName(value);
        }

        public string Description
        {
            get => _curSheet.GetDesc();
            set => _curSheet.SetDesc(value);
        }

	}
}
