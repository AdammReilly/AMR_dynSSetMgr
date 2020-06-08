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
    /// <summary>
    /// A Sheet Set, containing Sheets, Subsets, Custom Properties and other data.
    /// </summary>
    [IsVisibleInDynamoLibrary(true)]
    public class SheetSet
    {
        private AcSmSheetSet _curSheetSet;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        [IsVisibleInDynamoLibrary(false)]
        internal SheetSet(AcSmSheetSet sheetSetFromSSMgr)
        {
            _curSheetSet = sheetSetFromSSMgr;
        }
        /// <summary>
        /// Get the Sheet Set from the database.
        /// </summary>
        /// <param name="database">The Database to read the Sheet Set out of.</param>
        /// <returns>The Sheet Set from the database.</returns>
        public static SheetSet ByDatabase(Database database)
        {
            if (database != null)
            {
                return new SheetSet(database.SheetSet.BaseObject);
            }
            else { return null; }
        }

        // Create new Sheet Set from Filename

        #endregion

        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo

        [IsVisibleInDynamoLibrary(false)]
        internal AcSmSheetSet BaseObject
        {
            get => _curSheetSet;
        }

        /// <summary>
        /// Get the Sheet Set name.
        /// </summary>
        public string Name
        {
            get => _curSheetSet.GetName();
        }
        /// <summary>
        /// Gets the Sheet Set description.
        /// </summary>
        public string Description
        {
            get => _curSheetSet.GetDesc();
        }
        /// <summary>
        /// Get all the sheets in the sheet set.
        /// </summary>
        public IList<Sheet> Sheets
        {
            get
            {
                IList<Sheet> sheets = new List<Sheet>();
                IAcSmEnumComponent sheetEnum = _curSheetSet.GetSheetEnumerator();
                sheetEnum.Reset();
                IAcSmPersist item = sheetEnum.Next();
                while (item != null)
                {
                    if (item.GetTypeName() == "AcSmSheet")
                    {
                        sheets.Add(new Sheet((AcSmSheet)item));
                    }
                    item = sheetEnum.Next();
                }
                return sheets;
            }
        }

        /// <summary>
        /// Get the database containing the sheet set.
        /// </summary>
        /// <returns>A Database object.</returns>
        public Database Database
        {
            get
            {
                if (_curSheetSet.GetDatabase() != null)
                {
                    if (_curSheetSet.GetDatabase() != null)
                    { return new Database(_curSheetSet.GetDatabase()); }
                    else { return null; }
                }
                else { return null; }
            }
        }

        /// <summary>
        /// Get the subsets.
        /// </summary>
        public IList<SubSet> Subsets
        {
            get
            {
                IList<SubSet> subsets = new List<SubSet>();
                try
                {
                    IAcSmEnumComponent sheetEnum = _curSheetSet.GetSheetEnumerator();
                    sheetEnum.Reset();
                    IAcSmPersist item = sheetEnum.Next();
                    while (item != null)
                    {
                        if (item.GetTypeName() == "AcSmSubset")
                        {
                            subsets.Add(new SubSet((AcSmSubset)item));
                        }
                        item = sheetEnum.Next();
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                return subsets; 
            }
        }

        /// <summary>
        /// Get a list of all the custom properties in the set.
        /// </summary>
        public IList<CustomProperty> CustomProperties
        {
            get
            {
                IList<CustomProperty> customProps = new List<CustomProperty>();
                try
                {
                    IAcSmEnumProperty propEnum = _curSheetSet.GetCustomPropertyBag().GetPropertyEnumerator();
                    propEnum.Reset();
                    string propName;
                    AcSmCustomPropertyValue propValue;
                    propEnum.Next(out propName, out propValue);
                    while ((propName != null) || (propValue != null))
                    {
                        customProps.Add(new CustomProperty(propName, propValue.GetValue(), (int)propValue.GetFlags()));
                        propEnum.Next(out propName, out propValue);
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                return customProps;
            }
        }

        // Get Model Views (custom object?)

        // Get Sheet Views (custom object?)

        // Get Label Block Views (custom object?)

        // Get Callout Blocks (custom object?)

        // Get Page Setup Overrides file

        // Get Project Number

        // Get Project name

        // Get Project Phase

        // Get Project Milestone

        // Get Sheet Storage Location

        // Get Sheet Creation Template

        // Get prompt for template

        #endregion

        #region publicMethods
        // These will show up in the Actions category

        /// <summary>
        /// Set the Sheet Set description.
        /// </summary>
        /// <param name="value">The new Description value.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetDescription(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet.SetDesc(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }

        }

        /// <summary>
        /// Set the Sheet Set Name.
        /// </summary>
        /// <param name="value">The new sheet set name.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetName(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet.SetName(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }

        }


        // Set Model Views (custom object?)

        // Set Sheet Views (custom object?)

        // Set Label Block Views (custom object?)

        // Set Callout Blocks (custom object?)

        // Set Page Setup Overrides file

        // Set Project Number

        // Set Project name

        // Set Project Phase

        // Set Project Milestone

        // Set Sheet Storage Location

        // Set Sheet Creation Template

        // Set prompt for template

        // Remove Subset

        // Remove Sheet

        #endregion

        /// <summary>
        /// Format the name of this object
        /// </summary>
        /// <returns>A string representing the name of this object.</returns>
        public override string ToString()
        {
            return "Sheet Set: ( " + this.Name + " )";
        }

    }
}
