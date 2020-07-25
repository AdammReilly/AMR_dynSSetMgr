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
    public class SubSet
    {
        AcSmSubset _curSubSet;
        IAcSmSubset2 _curSubSet2;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        internal SubSet(AcSmSubset subsetFromSSMgr)
        {
            _curSubSet = subsetFromSSMgr;
            _curSubSet2 = (IAcSmSubset2)subsetFromSSMgr;
        }

        /// <summary>
        /// Get the subset by the index number provided.
        /// </summary>
        /// <param name="sheetSet">Sheet Set to search.</param>
        /// <param name="index">Index number to select.</param>
        /// <returns>The selected subset.</returns>
        public static SubSet BySheetSetIndex(SheetSet sheetSet, int index)
        {
            if (sheetSet != null)
            { return sheetSet.Subsets[index]; }
            else { return null; }
        }

        /// <summary>
        /// Create and add a new Subset to the Sheet Set.
        /// </summary>
        /// <param name="sheetSet">The sheet set to contain the new subset.</param>
        /// <param name="name">The name of the new subset.</param>
        /// <param name="description">The optional description of the new subset.</param>
        /// <param name="newSheetDWTLayout">The optional layout tab of an alterante DWT to create new sheets from.</param>
        /// <param name="newSheetDWTLocation">The optional location of an alternate DWT to create new sheets from.</param>
        /// <param name="newSheetLocation">An optional new location to create new sheets for this subset.</param>
        /// <param name="promptForDWT">Optional setting to force the user to select a template with each new sheet.</param>
        /// <returns>The new subset.</returns>
        public static SubSet CreateNewSubset(SheetSet sheetSet, string name, string description = "", string newSheetLocation = "", string newSheetDWTLocation = "", string newSheetDWTLayout = "", bool promptForDWT = false)
        {
            SubSet retVal = null;
            if ((sheetSet != null) || (name != null) || (name != ""))
            {
                try
                {// lock the database
                    if (Database.LockDatabase(sheetSet.Database, true))
                    {
                        //create the new subset
                        AcSmSubset newSubset = (AcSmSubset)sheetSet.BaseObject.CreateSubset(name, description);
                        // get thet folder the sheet set is stored in
                        IAcSmFileReference sheetSetFolder = sheetSet.BaseObject.GetNewSheetLocation();

                        // create a file reference
                        IAcSmFileReference fileReference = newSubset.GetNewSheetLocation();
                        // check if a new path was provided, if not, default to the sheet set location
                        if (newSheetLocation != "")
                        { fileReference.SetFileName(newSheetLocation); }
                        else { fileReference.SetFileName(sheetSetFolder.GetFileName()); }
                        // set the location for new sheets added to the subset
                        newSubset.SetNewSheetLocation(fileReference);

                        // check if a default DWT location and name was provided
                        if ((newSheetDWTLocation != "") && (newSheetDWTLayout != ""))
                        {
                            // create a reference to a layout reference object
                            AcSmAcDbLayoutReference layoutReference = newSubset.GetDefDwtLayout();
                            layoutReference.SetFileName(newSheetDWTLocation);
                            layoutReference.SetName(newSheetDWTLayout);
                            // set the layout reference for the subset
                            newSubset.SetDefDwtLayout(layoutReference);
                        }

                        // set the prompt for template option
                        newSubset.SetPromptForDwt(promptForDWT);

                        // create our version of a subset
                        retVal = new SubSet((AcSmSubset)newSubset);

                    }
                    // if it couldn't be locked, fail
                    else { return null; }
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
                finally
                {
                    // unlock the database
                    Database.LockDatabase(sheetSet.Database, false);
                }
            }
            return retVal;
        }

        #endregion


        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo

        /// <summary>
        /// Get the Subset Name.
        /// </summary>
        public string Name
        {
            get => _curSubSet.GetName();
        }

        /// <summary>
        /// Get the Subset Description.
        /// </summary>
        public string Description
        {
            get => _curSubSet.GetDesc();
        }

        /// <summary>
        /// Get the database that the subset is within.
        /// </summary>
        [IsVisibleInDynamoLibrary(false)]
        public Database Database
        {
            get => new Database(_curSubSet.GetDatabase());
        }

        /// <summary>
        /// Get all the sheets in the subset.
        /// </summary>
        public IList<Sheet> Sheets
        {
            get
            {
                IList<Sheet> sheets = new List<Sheet>();
                try
                {
                    IAcSmEnumComponent sheetEnum = _curSubSet.GetSheetEnumerator();
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
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                return sheets;
            }
        }

        /// <summary>
        /// Get the location at which new sheets will be created.
        /// </summary>
        public string NewSheetLocation
        {
            get
            {
                IAcSmFileReference fileRef = _curSubSet.GetNewSheetLocation();
                if (fileRef != null)
                { return fileRef.GetFileName(); }
                else { return ""; }
            }
        }

        /// <summary>
        /// Get the filename and path of the template used for the subset.
        /// </summary>
        public string NewSheetTemplate
        {
            get
            {
                IAcSmFileReference fileRef = _curSubSet.GetDefDwtLayout();
                return fileRef.GetFileName();
            }
        }

        /// <summary>
        /// Get the Prompt For Template setting.
        /// </summary>
        public bool PromptForTemplate
        { get => _curSubSet.GetPromptForDwt(); }

        /// <summary>
        /// Get the subset setting to disable publishing of the entire set.
        /// </summary>
        public bool PublishSheets
        {
            get => _curSubSet2.GetOverrideSheetPublish();
        }

        #endregion


        #region publicMethods
        // These will show up in the Actions category
        /// <summary>
        /// Set the SubSet publish override setting.
        /// </summary>
        /// <param name="publish">Set to publish or not.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet SetPublishSheets(bool publish)
        {
            try
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    _curSubSet2.SetOverrideSheetPublish(publish);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return this;
        }

        /// <summary>
        /// Set the name of the SubSet.
        /// </summary>
        /// <param name="newName">The new name to assign the subset.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet SetName(string newName)
        {
            if (Database.LockDatabase(this.Database, true))
            { _curSubSet.SetName(newName); }
            Database.LockDatabase(this.Database, false);
            return this;
        }

        /// <summary>
        /// Set the location for new sheets to be created.
        /// </summary>
        /// <param name="filePath">The folder path for new sheets.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet SetNewSheetLocation(string filePath)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                // create a file reference
                IAcSmFileReference fileReference = _curSubSet.GetNewSheetLocation();
                // check if a new path was provided, if not, default to the sheet set location
                if (filePath != "")
                { fileReference.SetFileName(filePath); }
                else
                {
                    // get thet folder the sheet set is stored in
                    IAcSmFileReference sheetSetFolder = _curSubSet.GetDatabase().GetSheetSet().GetNewSheetLocation();
                    fileReference.SetFileName(sheetSetFolder.GetFileName());
                }
                // set the location for new sheets added to the subset
                _curSubSet.SetNewSheetLocation(fileReference);
            }
            Database.LockDatabase(this.Database, false);
            return this;
        }

        /// <summary>
        /// Set the SubSet new sheet template.
        /// </summary>
        /// <param name="newSheetDWTLocation">File and path to the .dwt file.</param>
        /// <param name="newSheetDWTLayout">Layout name in the .dwt file.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet SetNewSheetTemplate(string newSheetDWTLocation, string newSheetDWTLayout)
        {
            // check if a default DWT location and name was provided
            if ((newSheetDWTLocation != "") && (newSheetDWTLayout != ""))
            {
                // if the .dwt file exists, continue on
                if (File.Exists(newSheetDWTLocation))
                {
                    if (Database.LockDatabase(this.Database, true))
                    {
                        // create a reference to a layout reference object
                        AcSmAcDbLayoutReference layoutReference = _curSubSet.GetDefDwtLayout();
                        layoutReference.SetFileName(newSheetDWTLocation);
                        layoutReference.SetName(newSheetDWTLayout);
                        // set the layout reference for the subset
                        _curSubSet.SetDefDwtLayout(layoutReference);
                    }
                    Database.LockDatabase(this.Database, false);
                }
            }
            return this;
        }

        /// <summary>
        /// Set the Prompt for Template setting.
        /// </summary>
        /// <param name="newPrompt">The value to assign to the setting.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet SetPromptForTemplate(bool newPrompt)
        {
            if (Database.LockDatabase(this.Database, true))
            {
                _curSubSet.SetPromptForDwt(newPrompt);
            }
            Database.LockDatabase(this.Database, false);
            return this;
        }

        /// <summary>
        /// Move an existing sheet into the subset before another sheet, or to the top.
        /// </summary>
        /// <param name="sheet">Existing sheet.</param>
        /// <param name="indexSheet">Sheet to insert before. Can be null for start of SubSet.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet MoveSheet(Sheet sheet, Sheet indexSheet = null)
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
                    _curSubSet.InsertComponent(sheet.BaseObject, index);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return this;
        }

        /// <summary>
        /// Move an existing sheet into the subset, after a sheet.
        /// </summary>
        /// <param name="sheet">Existing sheet.</param>
        /// <param name="indexSheet">Sheet to insert after.</param>
        /// <returns>The updated SubSet.</returns>
        public SubSet MoveSheetAfter(Sheet sheet, Sheet indexSheet)
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
                    _curSubSet.InsertComponentAfter(sheet.BaseObject, index);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return this;
        }

        /// <summary>
        /// Remove a sheet from the SubSet.
        /// </summary>
        /// <param name="sheet">A Sheet to be removed.</param>
        /// <returns>The Sheet that was removed.</returns>
        public Sheet RemoveSheet(Sheet sheet)
        {
            try
            {
                if (Database.LockDatabase(this.Database, true))
                {
                    _curSubSet.RemoveSheet(sheet.BaseObject);
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
            finally { Database.LockDatabase(this.Database, false); }
            return sheet;
        }


        #endregion

        /// <summary>
        /// Get the original AcSmSubset object.
        /// </summary>

        [IsVisibleInDynamoLibrary(false)]
        internal AcSmSubset BaseObject
        { get => _curSubSet; }


        /// <summary>
        /// Format the name of this object
        /// </summary>
        /// <returns>A string representing the name of this object.</returns>
        public override string ToString()
        {
            return "SubSet: ( " + this.Name + " )";
        }
    }
}
