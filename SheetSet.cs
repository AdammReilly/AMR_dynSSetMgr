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
        private IAcSmSheetSet2 _curSheetSet2;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        [IsVisibleInDynamoLibrary(false)]
        internal SheetSet(AcSmSheetSet sheetSetFromSSMgr)
        {
            _curSheetSet = sheetSetFromSSMgr;
            _curSheetSet2 = (IAcSmSheetSet2)sheetSetFromSSMgr;
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

        /// <summary>
        /// Get the Page Setup Overrides file name
        /// </summary>
        public string PageSetupOverridesFile
        {
            get => _curSheetSet.GetAltPageSetups().GetFileName();
        }
            
        /// <summary>
        /// Get the project number value.
        /// </summary>
        public string ProjectNumber
        { get => _curSheetSet2.GetProjectNumber(); }

        /// <summary>
        /// Get the project name value.
        /// </summary>
        public string ProjectName
        { get => _curSheetSet2.GetProjectName(); }

        /// <summary>
        /// Get the project phase value.
        /// </summary>
        public string ProjectPhase
        { get => _curSheetSet2.GetProjectPhase(); }

        /// <summary>
        /// Get the project milestone value.
        /// </summary>
        public string ProjectMilestone
        { get => _curSheetSet2.GetProjectMilestone(); }

        /// <summary>
        /// Get the location new sheets are stored in.
        /// </summary>
        public string NewSheetLocation
        { get => _curSheetSet.GetNewSheetLocation().GetFileName(); }

        /// <summary>
        /// Get the new sheet .dwt template file name.
        /// </summary>
        public string NewSheetTemplateFilename
        { get => _curSheetSet.GetDefDwtLayout().GetFileName(); }
        
        /// <summary>
        /// Get the new sheet layout name in the .dwt file.
        /// </summary>
        public string NewSheetTemplateLayout
        { get => _curSheetSet.GetDefDwtLayout().GetName(); }

        /// <summary>
        /// Get the Prompt for Template setting.
        /// </summary>
        public bool PromptForTemplate
        { get => _curSheetSet.GetPromptForDwt(); }

        // Get Model Views (custom object?)

        // Get Sheet Views (custom object?)

        // Get Label Block Views (custom object?)

        // Get Callout Blocks (custom object?)

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

        /// <summary>
        /// Move an existing sheet to the sheet set before another sheet, or to the top.
        /// </summary>
        /// <param name="sheet">Existing sheet.</param>
        /// <param name="indexSheet">Sheet to insert before. Can be null for start of SubSet.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet MoveSheet(Sheet sheet, Sheet indexSheet = null)
        {
            IAcSmComponent index = null;
            if (indexSheet != null)
            { index = (IAcSmComponent)indexSheet.BaseObject; }
            try
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    IAcSmPersist owner = sheet.BaseObject.GetOwner();
                    if (owner.GetTypeName() == "AcSmSheetSet")
                    {
                        AcSmSheetSet sheetSet = (AcSmSheetSet)owner;
                        sheetSet.RemoveSheet(sheet.BaseObject);
                    }
                    else if (owner.GetTypeName() == "AcSmSubset")
                    {
                        AcSmSubset subSet = (AcSmSubset)owner;
                        subSet.RemoveSheet(sheet.BaseObject);
                    }
                    _curSheetSet.InsertComponent(sheet.BaseObject, index);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return this;
        }

        /// <summary>
        /// Move an existing sheet into the sheet set, after a sheet.
        /// </summary>
        /// <param name="sheet">Existing sheet.</param>
        /// <param name="indexSheet">Sheet to insert after.</param>
        /// <returns>The updated SheetSet.</returns>
        public SheetSet MoveSheetAfter(Sheet sheet, Sheet indexSheet)
        {
            IAcSmComponent index = (IAcSmComponent)indexSheet.BaseObject;
            try
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    IAcSmPersist owner = sheet.BaseObject.GetOwner();
                    if (owner.GetTypeName() == "AcSmSheetSet")
                    {
                        AcSmSheetSet sheetSet = (AcSmSheetSet)owner;
                        sheetSet.RemoveSheet(sheet.BaseObject);
                    }
                    else if (owner.GetTypeName() == "AcSmSubset")
                    {
                        AcSmSubset subSet = (AcSmSubset)owner;
                        subSet.RemoveSheet(sheet.BaseObject);
                    }
                    _curSheetSet.InsertComponentAfter(sheet.BaseObject, index);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return this;
        }

        /// <summary>
        /// Set the location for the page setup overrides file.
        /// </summary>
        /// <param name="filename">The folder path to the page setup overrides file.</param>
        /// <returns>The updated SheetSet.</returns>
        public SheetSet SetPageSetupOverride(string filename)
        {
            if ((filename != "") || (filename != null))
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    IAcSmFileReference fileRef = new AcSmFileReference();
                    fileRef.SetFileName(filename);
                    _curSheetSet.SetAltPageSetups(fileRef);
                    Database.LockDatabase(this.Database, false);
                    return this;
                }
                else { return null; }
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Sheet Set Project Number.
        /// </summary>
        /// <param name="value">The new Project Number value.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetProjectNumber(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet2.SetProjectNumber(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Sheet Set Project Name.
        /// </summary>
        /// <param name="value">The new Project Name value.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetProjectName(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet2.SetProjectName(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Sheet Set Project Phase.
        /// </summary>
        /// <param name="value">The new Project Phase value.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetProjectPhase(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet2.SetProjectPhase(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Sheet Set Project Milestone.
        /// </summary>
        /// <param name="value">The new Project Milestone value.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetProjectMilestone(string value)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet2.SetProjectMilestone(value);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Set the location for new sheets to be created in.
        /// </summary>
        /// <param name="folderName">The folder path to store new sheets.</param>
        /// <returns>The updated SheetSet.</returns>
        public SheetSet SetNewSheetLocation(string folderName)
        {
            if ((folderName != "") || (folderName != null))
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    IAcSmFileReference fileRef = _curSheetSet.GetNewSheetLocation();
                    fileRef.SetFileName(folderName);
                    _curSheetSet.SetNewSheetLocation(fileRef);
                    Database.LockDatabase(this.Database, false);
                    return this;
                }
                else { return null; }
            }
            else { return null; }
        }

        /// <summary>
        /// Set the template to create new sheets from.
        /// </summary>
        /// <param name="templatePath">The path to the template file.</param>
        /// <param name="layoutName">The layout name in the template to use for new sheets.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetNewSheetTemplate(string templatePath, string layoutName)
        {
            if ((templatePath != "") || (templatePath != null) || (layoutName != "") || (layoutName != null))
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    AcSmAcDbLayoutReference fileRef = new AcSmAcDbLayoutReference();
                    fileRef.SetFileName(templatePath);
                    fileRef.SetName(layoutName);
                    _curSheetSet.SetDefDwtLayout(fileRef);
                    Database.LockDatabase(this.Database, false);
                    return this;
                }
                else { return null; }
            }
            else { return null; }
        }

        /// <summary>
        /// Set the Prompt for Template option.
        /// </summary>
        /// <param name="prompt">Whether to prompt for a template or not.</param>
        /// <returns>The updated Sheet Set.</returns>
        public SheetSet SetPromptForTemplate(bool prompt)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSheetSet.SetPromptForDwt(prompt);
                Database.LockDatabase(this.Database, false);
                return this;
            }
            else { return null; }
        }

        /// <summary>
        /// Remove a subset from the Sheet Set.
        /// </summary>
        /// <param name="subset">A SubSet to be removed.</param>
        /// <returns>The SubSet that was removed.</returns>
        public SubSet RemoveSubSet(SubSet subset)
        {
            try
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    _curSheetSet.RemoveSubset(subset.BaseObject);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return subset;
        }

        /// <summary>
        /// Remove a sheet from the Sheet Set.
        /// </summary>
        /// <param name="sheet">A Sheet to be removed.</param>
        /// <returns>The Sheet that was removed.</returns>
        public Sheet RemoveSheet(Sheet sheet)
        {
            try
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    _curSheetSet.RemoveSheet(sheet.BaseObject);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return sheet;
        }

        // Set Model Views (custom object?)

        // Set Sheet Views (custom object?)

        // Set Label Block Views (custom object?)

        // Set Callout Blocks (custom object?)

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
