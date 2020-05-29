using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ACSMCOMPONENTS24Lib;
using Autodesk.DesignScript.Runtime;
using AXDBLib;

namespace AMR.dynSSetMgr
{
    [IsVisibleInDynamoLibrary(false)]
    public class AMRSheetSet
    {
        AcSmSheetSet _curSheetSet;

        [IsVisibleInDynamoLibrary(false)]
        public AMRSheetSet(AcSmSheetSet sheetSetFromSSMgr)
        {
            _curSheetSet = sheetSetFromSSMgr;
        }
        [IsVisibleInDynamoLibrary(false)]
        public AcSmSheetSet BaseObject()
        {
            return _curSheetSet;
        }
        public string Name
        {
            get => _curSheetSet.GetName();
            set => _curSheetSet.SetName(value);
        }
        public string Description
        {
            get => _curSheetSet.GetDesc();
            set => _curSheetSet.SetDesc(value);
        }

        public AMRSheet AddNewSheet(string name, string desc = "")
        {
            AMRDatabase database = new AMRDatabase(_curSheetSet.GetDatabase());
            AMRSheet retSheet = null;
            database.LockDatabase(true);
            try
            {
                retSheet = new AMRSheet(_curSheetSet.AddNewSheet(name, desc));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                database.LockDatabase(false);
            }
            return retSheet;
        }
        public void Clear()
        {
            _curSheetSet.Clear();
        }
        public AMRDatabase GetDatabase()
        {
            return new AMRDatabase(_curSheetSet.GetDatabase());
        }

        public bool IsDirty
        { get => _curSheetSet.GetIsDirty(); }

    }
}
