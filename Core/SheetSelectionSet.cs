using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if C3D2021
using ACSMCOMPONENTS24Lib;
#elif C3D2020
using ACSMCOMPONENTS23Lib;
#endif
using Autodesk.DesignScript.Runtime;
using AXDBLib;
using Dynamo.Core;

namespace AMR.dynSSetMgr
{
    /// <summary>
    /// Represents a subset.
    /// </summary>
    [IsVisibleInDynamoLibrary(true)]
    public class SheetSelectionSet
    {
        AcSmSheetSelSet _curSelSet;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        internal SheetSelectionSet(AcSmSheetSelSet baseObject)
        {
            _curSelSet = baseObject;
        }

        /// <summary>
        /// Create a new Sheet Selection Set.
        /// </summary>
        /// <param name="sheetSet">The Sheet Set to contain the Selection Set.</param>
        /// <param name="name">The name of the Sheet Selection Set.</param>
        /// <returns>The new Sheet Selection Set.</returns>
        public static SheetSelectionSet BySheetSetAndName(SheetSet sheetSet, string name)
        {
            AcSmSheetSelSet newSelSet = null;
            sheetSet.BaseObject.GetSheetSelSets().Add(name, name, ref newSelSet);
            return new SheetSelectionSet(newSelSet);
        }

        /// <summary>
        /// Get an existing Sheet Selection Set by index.
        /// </summary>
        /// <param name="sheetSet">The containing Sheet Set.</param>
        /// <param name="index">The index number of the Selection Set.</param>
        /// <returns>The selected Sheet Selection Set.</returns>
        public static SheetSelectionSet BySheetSetIndex(SheetSet sheetSet, int index)
        {
            if ((sheetSet != null) && (index >=0) && (index <= sheetSet.Sheets.Count))
            {
                return sheetSet.SheetSelectionSets[index];
            }
            else { return null; }
        }


        #endregion


        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo
        /// <summary>
        /// Get the Sheet Selection Set name.
        /// </summary>
        public string Name
        { get => _curSelSet.GetName(); }

        /// <summary>
        /// Get the Sheets in the Sheet Selection Set
        /// </summary>
        public IList<Sheet> Sheets
        {
            get
            {
                IList<Sheet> sheets = new List<Sheet>();
                IAcSmEnumComponent sheetEnum = _curSelSet.GetEnumerator();
                sheetEnum.Reset();
                IAcSmComponent curItem = sheetEnum.Next();
                while (curItem != null)
                {
                    if (curItem.GetTypeName() == "AcSmSheet")
                    { sheets.Add(new Sheet((AcSmSheet)curItem)); }
                    curItem = sheetEnum.Next();
                }
                return sheets;
            }
        }

        #endregion



        #region publicMethods
        // These will show up in the Actions category
        /// <summary>
        /// Set the name of the Sheet Selection Set.
        /// </summary>
        /// <param name="name">The new name to assign the Sheet Selection Set.</param>
        /// <returns>The updated Sheet Selection Set</returns>
        public SheetSelectionSet SetName(string name)
        {
            if ((name != "") || (name != null))
            {
                if (Database.LockDatabase(new Database(_curSelSet.GetDatabase()), true))
                {
                    _curSelSet.SetName(name);
                }
                Database.LockDatabase(new Database(_curSelSet.GetDatabase()), false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Add a sheet to the Sheet Selection Set
        /// </summary>
        /// <param name="newSheet">The Sheet to add.</param>
        /// <returns>The updated Sheet Selection Set.</returns>
        public SheetSelectionSet AddSheet(Sheet newSheet)
        {
            if (newSheet != null)
            {
                if (Database.LockDatabase(new Database(_curSelSet.GetDatabase()), true))
                {
                    _curSelSet.Add(newSheet.BaseObject);
                }
                Database.LockDatabase(new Database(_curSelSet.GetDatabase()), false);
                return this;
            }
            else { return null; }
        }

        // remove sheet
        /// <summary>
        /// Remove a sheet from the Sheet Selection Set
        /// </summary>
        /// <param name="removeSheet">The Sheet to remove.</param>
        /// <returns>The updated Sheet Selection Set.</returns>
        public SheetSelectionSet RemoveSheet(Sheet removeSheet)
        {
            if (removeSheet != null)
            {
                if (Database.LockDatabase(new Database(_curSelSet.GetDatabase()), true))
                {
                    _curSelSet.Remove(removeSheet.BaseObject);
                }
                Database.LockDatabase(new Database(_curSelSet.GetDatabase()), false);
                return this;
            }
            else { return null; }
        }


        #endregion
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmSheetSelSet BaseObject
        { get => _curSelSet; }

        public override string ToString()
        {
            return "Sheet Selection Set: ( " + _curSelSet.GetName() + " )" ;
        }

    }
}
