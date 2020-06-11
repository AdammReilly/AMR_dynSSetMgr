using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACSMCOMPONENTS24Lib;
using Autodesk.DesignScript.Runtime;
using AXDBLib;
using Dynamo.Graph.Nodes;

namespace AMR.dynSSetMgr
{
    /// <summary>
    /// Represents a sheet in the set.
    /// </summary>
    [IsVisibleInDynamoLibrary(true)]
    public class Sheet
    {
        AcSmSheet _curSheet;
        IAcSmSheet2 _curSheet2;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        internal Sheet(AcSmSheet rootSheetObject)
        {
            _curSheet = rootSheetObject;
            _curSheet2 = (IAcSmSheet2)rootSheetObject;
        }
        /// <summary>
        /// Get an existing sheet in the set.
        /// </summary>
        /// <param name="sheetSet">Sheet Set to access.</param>
        /// <param name="index">Index number of the sheet to get.</param>
        /// <returns>Sheet object.</returns>
        public static Sheet BySheetSetIndex(SheetSet sheetSet, int index)
        {
            if (sheetSet != null)
            { return sheetSet.Sheets[index]; }
            else { return null; }
        }

        /// <summary>
        /// Create and add a new sheet to the beginning of a sheet set.
        /// Sheet Set must have a default template and sheet location set.
        /// </summary>
        /// <param name="sheetSet">The sheet set to add the sheet to.</param>
        /// <param name="filename">The name of the new sheet.</param>
        /// <param name="desc">The optional description of the new sheet.</param>
        /// <returns>The newly added Sheet.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Sheet AddToSheetSet(SheetSet sheetSet, string filename, string desc = "")
        {
            Sheet retSheet = null;
            if (sheetSet != null)
            {
                Database database = sheetSet.Database;
                try
                {
                    if (Database.LockDatabase(database, true))
                    {
                        // create the sheet
                        AcSmSheet sheet = database.BaseObject.GetSheetSet().AddNewSheet(filename, desc);
                        // add the sheet to the sheet set
                        database.BaseObject.GetSheetSet().InsertComponent(sheet, null);
                        retSheet = new Sheet(sheet);

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Database.LockDatabase(database, false);
                }
            }
            return retSheet;
        }

        /// <summary>
        /// Create and add a new sheet before a provided sheet in the sheet set.
        /// Sheet Set must have a default template and sheet location set.
        /// </summary>
        /// <param name="sheetSet">The sheet set to receive the new sheet.</param>
        /// <param name="filename">The name of the new sheet.</param>
        /// <param name="indexSheet">An existing sheet in the set to put the new sheet before.</param>
        /// <param name="desc">The optional description of the new sheet.</param>
        /// <returns>The newly added Sheet.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Sheet AddToSheetSetBefore(SheetSet sheetSet, string filename, Sheet indexSheet, string desc = "")
        {
            Sheet retSheet = null;
            if (sheetSet != null)
            {
                Database database = sheetSet.Database;
                try
                {
                    if (Database.LockDatabase(database, true))
                    {
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        // create the sheet
                        AcSmSheet sheet = database.BaseObject.GetSheetSet().AddNewSheet(filename, desc);
                        // add the sheet to the sheet set
                        database.BaseObject.GetSheetSet().InsertComponent(sheet, exSheet);
                        retSheet = new Sheet(sheet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Database.LockDatabase(database, false);
                }
            }
            return retSheet;
        }

        /// <summary>
        /// Create and add a new sheet after a provided sheet in the sheet set.
        /// Sheet Set must have a default template and sheet location set.
        /// </summary>
        /// <param name="sheetSet">The sheet set to receive the new sheet.</param>
        /// <param name="filename">The name of the new sheet.</param>
        /// <param name="indexSheet">An existing sheet in the set to put the new sheet after.</param>
        /// <param name="desc">The optional description of the new sheet.</param>
        /// <returns>The newly added Sheet.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Sheet AddToSheetSetAfter(SheetSet sheetSet, string filename, Sheet indexSheet, string desc = "")
        {
            Sheet retSheet = null;
            if (sheetSet != null)
            {
                Database database = sheetSet.Database;
                try
                {
                    if (Database.LockDatabase(database, true))
                    {
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        // create the sheet
                        AcSmSheet sheet = database.BaseObject.GetSheetSet().AddNewSheet(filename, desc);
                        // add the sheet to the sheet set
                        database.BaseObject.GetSheetSet().InsertComponentAfter(sheet, exSheet);
                        retSheet = new Sheet(sheet);

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Database.LockDatabase(database, false);
                }
            }
            return retSheet;
        }

        /// <summary>
        /// Create and add a new sheet to the beginning of a subset.
        /// Sheet Set must have a default template and sheet location set.
        /// </summary>
        /// <param name="subset">SubSet to receive the new sheet.</param>
        /// <param name="filename">The filename of the new sheet.</param>
        /// <param name="desc">The optional description of the new sheet.</param>
        /// <returns>The newly added Sheet.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Sheet AddToSubSet(SubSet subset, string filename, string desc = "")
        {
            Sheet retSheet = null;
            if (subset != null)
            {
                Database database = subset.Database;
                try
                {
                    if (Database.LockDatabase(database, true))
                    {

                        // add the new sheet to the subset
                        AcSmSheet sheet = subset.BaseObject.AddNewSheet(filename, desc);
                        // add the sheet to the subset
                        subset.BaseObject.InsertComponent(sheet, null);
                        retSheet = new Sheet(sheet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Database.LockDatabase(database, false);
                }
            }
            return retSheet;
        }

        /// <summary>
        /// Create and add a new sheet to a subset after a sheet.
        /// Sheet Set must have a default template and sheet location set.
        /// </summary>
        /// <param name="subset">The SubSet to receive the new sheet.</param>
        /// <param name="filename">The filename of the new sheet.</param>
        /// <param name="indexSheet">The sheet to insert after.</param>
        /// <param name="desc">The optional description of the new sheet.</param>
        /// <returns>The newly added Sheet.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Sheet AddToSubSetAfter(SubSet subset, string filename, Sheet indexSheet, string desc = "")
        {
            Sheet retSheet = null;
            if (subset != null)
            {
                Database database = subset.Database;
                try
                {
                    if (Database.LockDatabase(database, true))
                    {
                        // get the base object of the index sheet
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        // add the new sheet to the subset
                        AcSmSheet sheet = subset.BaseObject.AddNewSheet(filename, desc);
                        // add the sheet to the subset
                        subset.BaseObject.InsertComponentAfter(sheet, exSheet);
                        retSheet = new Sheet(sheet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Database.LockDatabase(database, false);
                }
            }
            return retSheet;
        }

        /// <summary>
        /// Create and add a new sheet to a subset before a sheet.
        /// Sheet Set must have a default template and sheet location set.
        /// </summary>
        /// <param name="subset">The SubSet to receive the new sheet.</param>
        /// <param name="filename">The filename of the new sheet.</param>
        /// <param name="indexSheet">The sheet to insert before.</param>
        /// <param name="desc">The optional description of the new sheet.</param>
        /// <returns>The newly added Sheet.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Sheet AddToSubSetBefore(SubSet subset, string filename, Sheet indexSheet, string desc = "")
        {
            Sheet retSheet = null;
            if (subset != null)
            {
                Database database = subset.Database;
                try
                {
                    if (Database.LockDatabase(database, true))
                    {
                        // get the base object of the index sheet
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        // add the new sheet to the subset
                        AcSmSheet sheet = subset.BaseObject.AddNewSheet(filename, desc);
                        // add the sheet to the subset
                        subset.BaseObject.InsertComponent(sheet, exSheet);
                        retSheet = new Sheet(sheet);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    Database.LockDatabase(database, false);
                }
            }
            return retSheet;
        }

        /// <summary>
        /// Import an existing sheet file to the beginning of the sheet set.
        /// </summary>
        /// <param name="sheetSet">The sheet set to receive the new sheet.</param>
        /// <param name="filename">The file and path of the existing sheet.</param>
        /// <param name="layoutName">The paperspace layout name.</param>
        /// <returns></returns>
        /// [IsVisibleInDynamoLibrary(true)]
        public static Sheet ImportLayoutToSheetSet(SheetSet sheetSet, string filename, string layoutName)
        {
            Sheet retSheet = null;
            if (sheetSet != null)
            {
                try
                {
                    if (Database.LockDatabase(sheetSet.Database, true))
                    {
                        // create a layout reference object
                        AcSmAcDbLayoutReference layoutReference = new AcSmAcDbLayoutReference();
                        // initialize the reference to the sheet set
                        layoutReference.InitNew(sheetSet.BaseObject);
                        // set the layout and drawing file to the inputs
                        layoutReference.SetFileName(filename);
                        layoutReference.SetName(layoutName);
                        // import the sheet into the sheet set
                        AcSmSheet newSheet;
                        newSheet = sheetSet.BaseObject.ImportSheet(layoutReference);
                        sheetSet.BaseObject.InsertComponent(newSheet, null);
                        retSheet = new Sheet(newSheet);
                    }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retSheet;
        }

        /// <summary>
        /// Import an existing sheet file to a sheet set after a sheet.
        /// </summary>
        /// <param name="sheetSet">The sheet set to receive the new sheet.</param>
        /// <param name="filename">The file and path of the existing sheet.</param>
        /// <param name="layoutName">The paperspace layout name.</param>
        /// <param name="indexSheet">The sheet to insert after.</param>
        /// <returns></returns>
        /// [IsVisibleInDynamoLibrary(true)]
        public static Sheet ImportLayoutToSheetSetAfter(SheetSet sheetSet, string filename, string layoutName, Sheet indexSheet)
        {
            Sheet retSheet = null;
            if (sheetSet != null)
            {
                try
                {
                    if (Database.LockDatabase(sheetSet.Database, true))
                    {
                        // create a layout reference object
                        AcSmAcDbLayoutReference layoutReference = new AcSmAcDbLayoutReference();
                        // initialize the reference to the sheet set
                        layoutReference.InitNew(sheetSet.BaseObject);
                        // set the layout and drawing file to the inputs
                        layoutReference.SetFileName(filename);
                        layoutReference.SetName(layoutName);
                        // import the sheet into the sheet set
                        AcSmSheet newSheet;
                        newSheet = sheetSet.BaseObject.ImportSheet(layoutReference);
                        // get the base object of the index sheet
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        sheetSet.BaseObject.InsertComponentAfter(newSheet, exSheet);
                        retSheet = new Sheet(newSheet);
                    }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retSheet;
        }

        /// <summary>
        /// Import an existing sheet file to a sheet set before a sheet.
        /// </summary>
        /// <param name="sheetSet">The sheet set to receive the new sheet.</param>
        /// <param name="filename">The file and path of the existing sheet.</param>
        /// <param name="layoutName">The paperspace layout name.</param>
        /// <param name="indexSheet">The sheet to insert before.</param>
        /// <returns></returns>
        /// [IsVisibleInDynamoLibrary(true)]
        public static Sheet ImportLayoutToSheetSetBefore(SheetSet sheetSet, string filename, string layoutName, Sheet indexSheet)
        {
            Sheet retSheet = null;
            if (sheetSet != null)
            {
                try
                {
                    if (Database.LockDatabase(sheetSet.Database, true))
                    {
                        // create a layout reference object
                        AcSmAcDbLayoutReference layoutReference = new AcSmAcDbLayoutReference();
                        // initialize the reference to the sheet set
                        layoutReference.InitNew(sheetSet.BaseObject);
                        // set the layout and drawing file to the inputs
                        layoutReference.SetFileName(filename);
                        layoutReference.SetName(layoutName);
                        // import the sheet into the sheet set
                        AcSmSheet newSheet;
                        newSheet = sheetSet.BaseObject.ImportSheet(layoutReference);
                        // get the base object of the index sheet
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        sheetSet.BaseObject.InsertComponent(newSheet, exSheet);
                        retSheet = new Sheet(newSheet);
                    }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retSheet;
        }

        /// <summary>
        /// Import an existing sheet file to the beginning of a subset.
        /// </summary>
        /// <param name="subset">The subset to receive the new sheet.</param>
        /// <param name="filename">The file and path of the existing sheet.</param>
        /// <param name="layoutName">The paperspace layout name.</param>
        /// <returns></returns>
        /// [IsVisibleInDynamoLibrary(true)]
        public static Sheet ImportLayoutToSubSet(SubSet subset, string filename, string layoutName)
        {
            Sheet retSheet = null;
            if (subset != null)
            {
                try
                {
                    if (Database.LockDatabase(subset.Database, true))
                    {
                        // create a layout reference object
                        AcSmAcDbLayoutReference layoutReference = new AcSmAcDbLayoutReference();
                        // initialize the reference to the sheet set
                        layoutReference.InitNew(subset.BaseObject);
                        // set the layout and drawing file to the inputs
                        layoutReference.SetFileName(filename);
                        layoutReference.SetName(layoutName);
                        // import the sheet into the sheet set
                        AcSmSheet newSheet;
                        newSheet = subset.BaseObject.ImportSheet(layoutReference);
                        subset.BaseObject.InsertComponent(newSheet, null);
                        retSheet = new Sheet(newSheet);
                    }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(subset.Database, false); }
            }
            return retSheet;
        }

        /// <summary>
        /// Import an existing sheet file to a subset after a sheet.
        /// </summary>
        /// <param name="subset">The subset to receive the new sheet.</param>
        /// <param name="filename">The file and path of the existing sheet.</param>
        /// <param name="layoutName">The paperspace layout name.</param>
        /// <param name="indexSheet">The sheet to insert after.</param>
        /// <returns></returns>
        /// [IsVisibleInDynamoLibrary(true)]
        public static Sheet ImportLayoutToSubSetAfter(SubSet subset, string filename, string layoutName, Sheet indexSheet)
        {
            Sheet retSheet = null;
            if (subset != null)
            {
                try
                {
                    if (Database.LockDatabase(subset.Database, true))
                    {
                        // create a layout reference object
                        AcSmAcDbLayoutReference layoutReference = new AcSmAcDbLayoutReference();
                        // initialize the reference to the sheet set
                        layoutReference.InitNew(subset.BaseObject);
                        // set the layout and drawing file to the inputs
                        layoutReference.SetFileName(filename);
                        layoutReference.SetName(layoutName);
                        // import the sheet into the sheet set
                        AcSmSheet newSheet;
                        newSheet = subset.BaseObject.ImportSheet(layoutReference);
                        // get the base object of the index sheet
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        subset.BaseObject.InsertComponentAfter(newSheet, exSheet);
                        retSheet = new Sheet(newSheet);
                    }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(subset.Database, false); }
            }
            return retSheet;
        }

        /// <summary>
        /// Import an existing sheet file to a subset before a sheet.
        /// </summary>
        /// <param name="subset">The subset to receive the new sheet.</param>
        /// <param name="filename">The file and path of the existing sheet.</param>
        /// <param name="layoutName">The paperspace layout name.</param>
        /// <param name="indexSheet">The sheet to insert before.</param>
        /// <returns></returns>
        /// [IsVisibleInDynamoLibrary(true)]
        public static Sheet ImportLayoutToSubSetBefore(SubSet subset, string filename, string layoutName, Sheet indexSheet)
        {
            Sheet retSheet = null;
            if (subset != null)
            { try
                {
                    if (Database.LockDatabase(subset.Database, true))
                    {
                        // create a layout reference object
                        AcSmAcDbLayoutReference layoutReference = new AcSmAcDbLayoutReference();
                        // initialize the reference to the sheet set
                        layoutReference.InitNew(subset.BaseObject);
                        // set the layout and drawing file to the inputs
                        layoutReference.SetFileName(filename);
                        layoutReference.SetName(layoutName);
                        // import the sheet into the sheet set
                        AcSmSheet newSheet;
                        newSheet = subset.BaseObject.ImportSheet(layoutReference);
                        // get the base object of the index sheet
                        AcSmSheet exSheet = indexSheet.BaseObject;
                        subset.BaseObject.InsertComponent(newSheet, exSheet);
                        retSheet = new Sheet(newSheet);
                    }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(subset.Database, false); }
            }
            return retSheet;
        }

        #endregion

        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmSheet BaseObject
        { get => _curSheet; }

        /// <summary>
        /// Get the name of the sheet.
        /// </summary>
        /// <returns>Returns the sheet name. A combination of number and title.</returns>
        public string Name
        {
            get => _curSheet.GetName();
        }

        /// <summary>
        /// Get the sheet description.
        /// </summary>
        public string Description
        {
            get => _curSheet.GetDesc();
            
        }

        /// <summary>
        /// Get the root database of the sheet.
        /// </summary>
        /// <returns>The Database object that contains the sheet.</returns>
        public Database Database
        {
            get
            {
                if (_curSheet != null)
                {
                    if (_curSheet.GetDatabase() != null)
                    { return new Database(_curSheet.GetDatabase()); }
                    else { return null; }
                }
                else { return null; }
            }
        }

        /// <summary>
        /// Get the sheet number.
        /// </summary>
        public string Number
        {
            get => _curSheet.GetNumber();
        }

        /// <summary>
        /// Get the sheet title.
        /// </summary>
        public string Title
        {
            get => _curSheet.GetTitle();
        }

        /// <summary>
        /// Get the 'Do Not Plot' setting for the sheet.
        /// </summary>
        public bool DoNotPlot
        { get => _curSheet.GetDoNotPlot(); }

        /// <summary>
        /// Get the Revision Number property value.
        /// </summary>
        public string RevisionNumber
        {
            get => _curSheet2.GetRevisionNumber();
            
        }
        
        /// <summary>
        /// Get the Revision Date property value.
        /// </summary>
        public string RevisionDate
        {
            get => _curSheet2.GetRevisionDate();
        }

        /// <summary>
        /// Get the Issue Purpose property value.
        /// </summary>
        public string IssuePurpose
        {
            get => _curSheet2.GetIssuePurpose();
        }

        /// <summary>
        /// Get the layout name for the sheet.
        /// </summary>
        public string LayoutName
        {
            get => _curSheet.GetLayout().GetName();
        }

        /// <summary>
        /// Get the category assigned to the sheet.
        /// </summary>
        public string Category
        { get => _curSheet2.GetCategory(); }

        /// <summary>
        /// Get the custom properties for the sheet.
        /// </summary>
        public IList<CustomProperty> CustomProperties
        {
            get
            {
                IList<CustomProperty> customProps = new List<CustomProperty>();
                try
                {
                    IAcSmEnumProperty propEnum = _curSheet.GetCustomPropertyBag().GetPropertyEnumerator();
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

        /// <summary>
        /// Get the Sheet Views in the Sheet.
        /// </summary>
        public IList<SheetView> SheetViews
        {
            get
            {
                IList<SheetView> views = new List<SheetView>();
                AcSmSheetViews sheetViews = _curSheet.GetSheetViews();
                IAcSmEnumSheetView sVEnum = sheetViews.GetEnumerator();
                sVEnum.Reset();
                AcSmSheetView viewItem = sVEnum.Next();
                while (viewItem != null)
                {
                    views.Add(new SheetView(viewItem));
                    viewItem = sVEnum.Next();
                }
                return views;
            }
        }

        #endregion

        #region publicMethods
        // These will show up in the Actions category
        /// <summary>
        /// Set the sheet description.
        /// </summary>
        /// <param name="description">The new description value.</param>
        /// <returns>The modified Sheet.</returns>
        public Sheet SetDescription(string description)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet.SetDesc(description);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }
        /// <summary>
        /// Set the sheet number.
        /// </summary>
        /// <param name="number">The number to set.</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetNumber(string number)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet.SetNumber(number);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the sheet title.
        /// </summary>
        /// <param name="title">The new sheet title.</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetTitle(string title)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet.SetTitle(title);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the 'Do Not Plot' setting for the sheet.
        /// </summary>
        /// <param name="value">The boolean value of the setting.</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetDoNotPlot(bool value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet.SetDoNotPlot(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }
        
        /// <summary>
        /// Set the Revision Number property value.
        /// </summary>
        /// <param name="value">The new Revision Number value.</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetRevisionNumber(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet2.SetRevisionNumber(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Revision Date property value.
        /// </summary>
        /// <param name="value">The new Revision Date value.</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetRevisionDate(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet2.SetRevisionDate(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Issue Purpose property value
        /// </summary>
        /// <param name="value">The new Issue Purpose value</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetIssuePurpose(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet2.SetIssuePurpose(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the category for the sheet.
        /// </summary>
        /// <param name="value">The category to assign.</param>
        /// <returns>The updated sheet.</returns>
        public Sheet SetCategory(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheet2.SetCategory(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        #endregion


        /// <summary>
        /// Format the name of this object
        /// </summary>
        /// <returns>A string representing the name of this object.</returns>
        public override string ToString()
        {
            return "Sheet: ( " + this.Name + " )";
        }


    }
}
