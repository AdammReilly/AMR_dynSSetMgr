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
    /// <summary>
    /// An object representing the Sheet Set Manager.
    /// </summary>
    [IsVisibleInDynamoLibrary(true)]
    public class SheetSetMgr
    {
        private IAcSmSheetSetMgr _sheetSetMgr;

        #region constructors
        internal SheetSetMgr()
        {
            _sheetSetMgr = new AcSmSheetSetMgr();
        }

        /// <summary>
        /// Get the Sheet Set Manager object.
        /// </summary>
        /// <returns>A Sheet Set Manager object.</returns>
        public static SheetSetMgr SheetSetManager()
        {
            return new SheetSetMgr();
        }

        #endregion

        #region properties
        /// <summary>
        /// Lists each of the open sheet set databases.
        /// </summary>
        /// <returns>A list of open Databases.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public IList<Database> Databases
        {
            get
            {
                // create a list to put the databases into 
                IList<Database> databases = new List<Database>();
                // get the database enumerator
                IAcSmEnumDatabase enumDatabase = _sheetSetMgr.GetDatabaseEnumerator();
                enumDatabase.Reset();
                IAcSmPersist item = enumDatabase.Next();
                if (item != null)
                {
                    while (item != null)
                    {
                        databases.Add(new Database((AcSmDatabase)item));
                        item = enumDatabase.Next();
                    }
                }
                return databases;
            }
        }


        #endregion

        #region publicMethods

        /// <summary>
        /// Close the selected Sheet Set database.
        /// </summary>
        /// <param name="database">The Database to close.</param>
        [IsVisibleInDynamoLibrary(true)]
        public void Close(Database database)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            AcSmDatabase dbToClose = database.BaseObject;
            ssMgr.Close(dbToClose);
            dbToClose = null;
        }

        /// <summary>
        /// Close all open Databases.
        /// </summary>
        /// <param name="doIt">Boolean choice to commit this command.</param>
        [IsVisibleInDynamoLibrary(true)]
        public void CloseAll(bool doIt = true)
        {
            if (doIt)
            {
                IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
                ssMgr.CloseAll();
                
            }
        }

        /// <summary>
        /// Create a new Sheet Set file.
        /// </summary>
        /// <param name="filename">The new Sheet Set .dst file name and path.</param>
        /// <param name="templatefilename">A template .dst file name and path.</param>
        /// <param name="alwaysCreate">Allow overwriting of an existing file.</param>
        /// <returns>Database object of the new .dst file.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public Database CreateDatabase(string filename, string templatefilename = "", bool alwaysCreate = true)
        {
            if (filename != "")
            {
                return new Database(_sheetSetMgr.CreateDatabase(filename, templatefilename, alwaysCreate));
            }
            else { return null; }
        }

        /// <summary>
        /// Looks for the Sheet Set file currently opened in the Sheet Set Manager palette.
        /// </summary>
        /// <param name="filename">The .dst file name</param>
        /// <returns>Database object if found, null if not found.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public Database FindOpenDatabase(string filename)
        {
            if (_sheetSetMgr.FindOpenDatabase(filename) != null)
            {
                return new Database(_sheetSetMgr.FindOpenDatabase(filename));
            }
            else
            { return null; }
        }

        /// <summary>
        /// Opens a Sheet Set .dst file.
        /// </summary>
        /// <param name="filename">Path and name of the .dst file to open.</param>
        /// <param name="bFailIfAlreadyOpen">Throw an error if the .dst is already open.</param>
        /// <returns>A Database of the Sheet Set .dst file.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public Database OpenDatabase(string filename, bool bFailIfAlreadyOpen = false)
        {
            return new Database(_sheetSetMgr.OpenDatabase(filename, bFailIfAlreadyOpen));
        }


        [IsVisibleInDynamoLibrary(false)]
        public SheetSet GetParentSheetSet(string filename, string layoutname)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            AcSmSheetSet sheetSet;
            ssMgr.GetParentSheetSet(filename, layoutname, out sheetSet);
            SheetSet output = new SheetSet(sheetSet);
            return output;
        }

        [IsVisibleInDynamoLibrary(false)]
        public AcSmSheet GetSheetFromLayout(AcadObject pAcDbLayout)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            AcSmSheet sheet;
            ssMgr.GetSheetFromLayout(pAcDbLayout, out sheet);
            return sheet;
        }

        [IsVisibleInDynamoLibrary(false)]
        public int Register(IAcSmEvents eventHandler)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            return ssMgr.Register(eventHandler);
        }
        // ssMgr.Unregister(cookie);
        [IsVisibleInDynamoLibrary(false)]
        public void Unregister(int cookie)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            ssMgr.Unregister(cookie);
        }


        #endregion


        /// <summary>
        /// Format the name of this object
        /// </summary>
        /// <returns>A string representing the name of this object.</returns>
        public override string ToString()
        {
            return "Sheet Set Manager";
        }
    }
}
