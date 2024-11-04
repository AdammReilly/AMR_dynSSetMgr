using System;
using System.Collections.Generic;
using System.Diagnostics;
#if C3D2025
using ACSMCOMPONENTS25Lib;
#elif C3D2023 || C3D2024
using ACSMCOMPONENTS24Lib;
#elif C3D2022
using ACSMCOMPONENTS24Lib;
#elif C3D2021
using ACSMCOMPONENTS24Lib;
#elif C3D2020
using ACSMCOMPONENTS23Lib;
#endif
using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;

namespace AMR.dynSSetMgr
{
    /// <summary>
    /// Represents a custom property in the set.
    /// </summary>
    [IsVisibleInDynamoLibrary(true)]
    public class CustomProperty
    {
        string _customPropName;
        string _customPropValue;
        int _curPropFlag;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        internal CustomProperty(string propName, string propValue, int propFlag)
        {
            _customPropName = propName;
            _customPropValue = propValue;
            _curPropFlag = propFlag;
        }
        internal CustomProperty(string propName, object propValue, int propFlag)
        {
            _customPropName = propName;
            _customPropValue = propValue.ToString();
            _curPropFlag = propFlag;
        }

        /// <summary>
        /// Create a Custom Property with the provided name and value.
        /// </summary>
        /// <param name="propName">The name of the property.</param>
        /// <param name="propValue">The value to assign the property.</param>
        /// <returns>A Custom Property object.</returns>
        public static CustomProperty ByNameValue(string propName, string propValue)
        {
            return new CustomProperty(propName, propValue, 0);
        }

        /// <summary>
        /// Get a custom property from the sheet set
        /// </summary>
        /// <param name="sheetSet">The Sheet Set to search.</param>
        /// <param name="propertyName">The name to look for.</param>
        /// <returns>The Custom Property.</returns>
        public static CustomProperty ByName(SheetSet sheetSet, string propertyName)
        {
            CustomProperty retVal = null;
            AcSmCustomPropertyValue propValue = sheetSet.BaseObject.GetCustomPropertyBag().GetProperty(propertyName);
            retVal = new CustomProperty(propertyName, propValue.GetValue(), (int)propValue.GetFlags());
            return retVal;
        }

        #endregion


        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo

        /// <summary>
        /// Get the name of the custom property
        /// </summary>
        public string Name
        { get => _customPropName; }

        /// <summary>
        /// Get the value of the custom property
        /// </summary>
        public string Value
        { get => _customPropValue; }

        /// <summary>
        /// Gets the type of property flag.
        /// </summary>
        public string Type
        {
            get
            {
                if (_curPropFlag == (int)PropertyFlags.CUSTOM_SHEETSET_PROP)
                { return "SheetSet"; }
                else if (_curPropFlag == (int)PropertyFlags.CUSTOM_SHEET_PROP)
                { return "Sheet"; }
                else { return ""; }
            }
        }

        #endregion


        #region publicMethods
        // These will show up in the Actions category

        /// <summary>
        /// Get the name and value separately
        /// </summary>
        /// <returns>The Name and Value as separate ports.</returns>
        [MultiReturn(new[] { "Name", "Value" })]
        [NodeCategory("Query")]
        public Dictionary<string, string> Properties()
        {
            return new Dictionary<string, string>
            {   { "Name", _customPropName },
                { "Value", _customPropValue  }
            };
        }

        /// <summary>
        /// Set the value of a custom property
        /// </summary>
        /// <param name="sheetSet">The sheet set containing the custom property.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns>The updated Custom Property.</returns>
        public CustomProperty EditValue(SheetSet sheetSet, string value)
        {
            CustomProperty retVal = null;
            if (sheetSet != null)
            {
                try
                {// get the custom property bag from the sheet set
                    AcSmCustomPropertyBag propBag = sheetSet.BaseObject.GetCustomPropertyBag();
                    // iterate through the properties looking for this property
                    IAcSmEnumProperty propEnum = propBag.GetPropertyEnumerator();
                    propEnum.Reset();
                    AcSmCustomPropertyValue curPropVal = null;
                    string curPropName = "";
                    propEnum.Next(out curPropName, out curPropVal);
                    while (curPropVal != null)
                    {
                        if (curPropName == _customPropName)
                        {
                            if (Database.LockDatabase(sheetSet.Database, true))
                            {// set the value
                                curPropVal.SetValue(value);
                                // set the return object
                                retVal = new CustomProperty(curPropName, value, (int)curPropVal.GetFlags());
                            }
                        }
                        propEnum.Next(out curPropName, out curPropVal);
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retVal;
        }

        /// <summary>
        /// Set the value of a custom property associated with a specific sheet.
        /// </summary>
        /// <param name="sheetSet">The sheet set containing the custom property.</param>
        /// <param name="sheet">The sheet containing the property to edit.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns>The updated Custom Property.</returns>
        public CustomProperty EditSheetValue(SheetSet sheetSet, Sheet sheet, string value)
        {
            CustomProperty retVal = null;
            if (sheetSet != null)
            {
                try
                {// get the custom property bag from the sheet set
                    AcSmCustomPropertyBag propBag = sheet.BaseObject.GetCustomPropertyBag();
                    // iterate through the properties looking for this property
                    IAcSmEnumProperty propEnum = propBag.GetPropertyEnumerator();
                    propEnum.Reset();
                    AcSmCustomPropertyValue curPropVal = null;
                    string curPropName = "";
                    propEnum.Next(out curPropName, out curPropVal);
                    while (curPropVal != null)
                    {
                        if (curPropVal.GetFlags() == PropertyFlags.CUSTOM_SHEET_PROP)
                        {
                            if (curPropName == _customPropName)
                            {
                                if (Database.LockDatabase(sheetSet.Database, true))
                                {// set the value
                                    curPropVal.SetValue(value);
                                    // set the return object
                                    retVal = new CustomProperty(curPropName, value, (int)curPropVal.GetFlags());
                                }
                            }
                        }
                        propEnum.Next(out curPropName, out curPropVal);
                    }
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retVal;
        }

        /// <summary>
        /// Add the Custom Property to the sheet set as a Sheet Set property.
        /// </summary>
        /// <param name="sheetSet">The sheet set to update.</param>
        /// <returns>The new custom property.</returns>
        public CustomProperty AddAsSheetSetProperty(SheetSet sheetSet)
        {
            CustomProperty retVal = null;
            if (sheetSet != null)
            {
                try
                {
                    if (Database.LockDatabase(sheetSet.Database, true))
                    {
                        // create a reference to the sheet set as a Persist object
                        IAcSmPersist persistSheetSet = sheetSet.BaseObject;
                        // create a new custom value
                        AcSmCustomPropertyValue pValue = new AcSmCustomPropertyValue();
                        // initialize the value with the sheet set
                        pValue.InitNew(persistSheetSet);
                        // set the Sheet Set flag
                        pValue.SetFlags(PropertyFlags.CUSTOM_SHEETSET_PROP);
                        // Set the value
                        pValue.SetValue(_customPropValue);
                        // get the props Bag from the sheet set
                        AcSmCustomPropertyBag ssPropBag = sheetSet.BaseObject.GetCustomPropertyBag();
                        // set the property to the bag
                        ssPropBag.SetProperty(_customPropName, pValue);
                        _curPropFlag = (int)PropertyFlags.CUSTOM_SHEETSET_PROP;
                        retVal = this;
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retVal;
        }

        /// <summary>
        /// Add the Custom Property to the sheet set as a Sheet property.
        /// </summary>
        /// <param name="sheetSet">The sheet set to update.</param>
        /// <returns>The new custom property.</returns>
        public CustomProperty AddAsSheetProperty(SheetSet sheetSet)
        {
            CustomProperty retVal = null;
            if (sheetSet != null)
            {
                try
                {
                    if (Database.LockDatabase(sheetSet.Database, true))
                    {
                        // create a reference to the sheet set as a Persist object
                        IAcSmPersist persistSheetSet = sheetSet.BaseObject;
                        // create a new custom value
                        AcSmCustomPropertyValue pValue = new AcSmCustomPropertyValue();
                        // initialize the value with the sheet set
                        pValue.InitNew(persistSheetSet);
                        // set the Sheet Set flag
                        pValue.SetFlags(PropertyFlags.CUSTOM_SHEET_PROP);
                        // Set the value
                        pValue.SetValue(_customPropValue);
                        // get the props Bag from the sheet set
                        AcSmCustomPropertyBag ssPropBag = sheetSet.BaseObject.GetCustomPropertyBag();
                        // set the property to the bag
                        ssPropBag.SetProperty(_customPropName, pValue);
                        _curPropFlag = (int)PropertyFlags.CUSTOM_SHEET_PROP;
                        retVal = this;
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
                finally { Database.LockDatabase(sheetSet.Database, false); }
            }
            return retVal;
        }


        #endregion


        /// <summary>
        /// Format the name of this object
        /// </summary>
        /// <returns>A string representing the name of this object.</returns>
        public override string ToString()
        {
            return "Custom " + this.Type + " Property: ( " + _customPropName + " | " + _customPropValue + " )";
        }
    }
}
