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
    [IsVisibleInDynamoLibrary(true)]
    public static class SheetSetMgr
    {
        /// <summary>
        /// Close the selected Sheet Set database.
        /// </summary>
        /// <param name="database">The AMRDatabase to close.</param>
        [IsVisibleInDynamoLibrary(true)]
        public static void Close(Database database)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            AcSmDatabase dbToClose = database.BaseObject;
            ssMgr.Close(dbToClose);
            dbToClose = null;
        }

        /// <summary>
        /// Close all open AMRDatabases.
        /// </summary>
        /// <param name="doIt">Boolean choice to commit this command.</param>
        [IsVisibleInDynamoLibrary(true)]
        public static void CloseAll(bool doIt = true)
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
        /// <param name="templatefilename">A template .dst file.</param>
        /// <param name="alwaysCreate">Allow overwriting of an existing file.</param>
        /// <returns>AMRDatabase object of the .dst file.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Database CreateDatabase(string filename, string templatefilename = "0", bool alwaysCreate = true)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            return new Database(ssMgr.CreateDatabase(filename, templatefilename, alwaysCreate));
        }

        /// <summary>
        /// Looks for the Sheet Set file currently opened in the Sheet Set Manager palette.
        /// </summary>
        /// <param name="filename">The .dst file name</param>
        /// <returns>AMRDatabase object if found, null if not found.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Database FindOpenDatabase(string filename)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            if (ssMgr.FindOpenDatabase(filename) != null)
            {
                return new Database(ssMgr.FindOpenDatabase(filename));
            }
            else
            { return null; }
        }

        /// <summary>
        /// Lists each of the open sheet set databases.
        /// </summary>
        /// <returns>A list of AMRDatabases.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static IList<Database> GetDatabases()
        {
            // get the sheet set manager object
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            // create a list to put the databases into 
            IList<Database> amrDatabases = new List<Database>();

            // get the database enumerator
            IAcSmEnumDatabase enumDatabase = ssMgr.GetDatabaseEnumerator();
            enumDatabase.Reset();
            IAcSmPersist item = enumDatabase.Next();
            if (item != null)
            {
                while (item != null)
                {
                    amrDatabases.Add(new Database((AcSmDatabase)item));
                    item = enumDatabase.Next();
                }
            }
            return amrDatabases;
        }

        [IsVisibleInDynamoLibrary(true)]
        public static SheetSet GetParentSheetSet(string filename, string layoutname)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            AcSmSheetSet sheetSet;
            ssMgr.GetParentSheetSet(filename, layoutname, out sheetSet);
            SheetSet output = new SheetSet(sheetSet);
            return output;
        }

        [IsVisibleInDynamoLibrary(true)]
        public static AcSmSheet GetSheetFromLayout(AcadObject pAcDbLayout)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            AcSmSheet sheet;
            ssMgr.GetSheetFromLayout(pAcDbLayout, out sheet);
            return sheet;
        }

        /// <summary>
        /// Opens a Sheet Set .dst file.
        /// </summary>
        /// <param name="filename">Path and name of the .dst file to open.</param>
        /// <param name="bFailIfAlreadyOpen">Throw an error if the .dst is already open.</param>
        /// <returns>An AMRDatabase of the Sheet Set .dst file.</returns>
        [IsVisibleInDynamoLibrary(true)]
        public static Database OpenDatabase(string filename, bool bFailIfAlreadyOpen = false)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            return new Database(ssMgr.OpenDatabase(filename, bFailIfAlreadyOpen));
        }

        [IsVisibleInDynamoLibrary(true)]
        public static int Register(IAcSmEvents eventHandler)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            return ssMgr.Register(eventHandler);
        }
        // ssMgr.Unregister(cookie);
        [IsVisibleInDynamoLibrary(true)]
        public static void Unregister(int cookie)
        {
            IAcSmSheetSetMgr ssMgr = new AcSmSheetSetMgr();
            ssMgr.Unregister(cookie);
        }

    }
}
