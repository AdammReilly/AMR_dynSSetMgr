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
    public partial class SheetSet
    {
        internal AcSmSheetSet _curSheetSet;

        [IsVisibleInDynamoLibrary(false)]
        internal SheetSet(AcSmSheetSet sheetSetFromSSMgr)
        {
            _curSheetSet = sheetSetFromSSMgr;
        }
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmSheetSet BaseObject()
        {
            return _curSheetSet;
        }
        internal string Name
        {
            get => _curSheetSet.GetName();
            set => _curSheetSet.SetName(value);
        }
        internal string Description
        {
            get => _curSheetSet.GetDesc();
            set => _curSheetSet.SetDesc(value);
        }

        internal Sheet AddNewSheet(string name, string desc = "")
        {
            Database database = new Database(_curSheetSet.GetDatabase());
            Sheet retSheet = null;
            database.LockDatabase(true);
            try
            {
                AcSmSheet newSheet = _curSheetSet.AddNewSheet(name, desc);
                _curSheetSet.InsertComponent(newSheet, null);
                retSheet = new Sheet(newSheet);
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
        internal void Clear()
        {
            _curSheetSet.Clear();
        }
        internal Database GetDatabase()
        {
            return new Database(_curSheetSet.GetDatabase());
        }

        internal bool IsDirty
        { get => _curSheetSet.GetIsDirty(); }

    }
}
