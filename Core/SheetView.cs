using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if C3D2023
using ACSMCOMPONENTS24Lib;
#elif C3D2022
using ACSMCOMPONENTS24Lib;
#elif C3D2021
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
    public class SheetView
    {
        AcSmSheetView _curSheetView;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        internal SheetView(AcSmSheetView baseObject)
        {
            _curSheetView = baseObject;
        }

        #endregion


        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo
        /// <summary>
        /// Get the sheet view name.
        /// </summary>
        public string Name
        {
            get => _curSheetView.GetName();
            
        }

        /// <summary>
        /// Get the sheet view number.
        /// </summary>
        public string Number
        { get => _curSheetView.GetNumber(); }

        /// <summary>
        /// Get the sheet view Category.
        /// </summary>
        public string Category
        { get => _curSheetView.GetCategory().GetName(); }

        #endregion



        #region publicMethods
        // These will show up in the Actions category

        /// <summary>
        /// Set the category for the sheet view.
        /// </summary>
        /// <param name="value">The name of the category to assign.</param>
        /// <returns>The updated sheet view.</returns>
        public SheetView SetCategory(string value)
        {
            if (Database.LockDatabase(new Database(_curSheetView.GetDatabase()), true))
            {
                // get the existing view categories
                AcSmViewCategories viewCats = _curSheetView.GetDatabase().GetSheetSet().GetViewCategories();
                // get the enumerator
                IAcSmEnumViewCategory viewEnum = viewCats.GetEnumerator();
                // setup a check
                bool exists = false;
                // reset the enumerator, just in case
                viewEnum.Reset();
                // get the first item
                AcSmViewCategory viewCategory = viewEnum.Next();
                // step thru the enumerator
                while (viewCategory != null)
                {
                    // if the current item is the category we wany
                    if (viewCategory.GetName() == value)
                    {
                        // set it to the catrgory
                        _curSheetView.SetCategory(viewCategory);
                        // mark the check as true
                        exists = true;
                    }
                }
                // if we couldn't find the category, lets make one
                if (exists == false)
                {
                    // create a new category and add it to the database
                    viewCategory.InitNew(_curSheetView.GetDatabase());
                    // set the name
                    viewCategory.SetName(value);
                    // apply it to the sheet view
                    _curSheetView.SetCategory(viewCategory);
                }
                Database.LockDatabase(new Database(_curSheetView.GetDatabase()), false);
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
            return "Sheet: ( " + _curSheetView.GetNumber() + " " + _curSheetView.GetName() + " )";
        }

    }
}
