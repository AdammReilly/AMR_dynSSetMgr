using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#if C3D2021
using ACSMCOMPONENTS24Lib;
#elif C3D2020
using ACSMCOMPONENTS23Lib;
#endif
using Autodesk.DesignScript.Runtime;
using AXDBLib;

namespace AMR.dynSSetMgr
{
    /// <summary>
    /// Represents the file containing a Sheet Set.
    /// </summary>
    public partial class Database
    {
        internal AcSmDatabase _curDatabase;

        [IsVisibleInDynamoLibrary(false)]
        internal AcSmDatabase BaseObject
        { get => _curDatabase; }

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category

        /// <summary>
        /// The file that stores the sheet set.
        /// </summary>
        /// <param name="dbFromSSMgr">The Sheet Set Manager object.</param>
        [IsVisibleInDynamoLibrary(false)]
        internal Database(AcSmDatabase dbFromSSMgr)
        {
            _curDatabase = dbFromSSMgr;
        }

        /// <summary>
        /// Open a .dst file.
        /// </summary>
        /// <param name="sheetSetManager">The sheet set manager.</param>
        /// <param name="filename">Full path and name of the .dst file.</param>
        /// <returns>Database in the .dst file.</returns>
        public static Database ByFilename(SheetSetMgr sheetSetManager, string filename)
        {
            return sheetSetManager.OpenDatabase(filename, false);
        }


        #endregion
        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo

        /// <summary>
        /// Get the filename of the database.
        /// </summary>
        public string Filename
        { get => _curDatabase.GetFileName(); }

        /// <summary>
        /// Get the database name.
        /// </summary>
        public string Name
        { get => _curDatabase.GetName(); }

        #endregion

        #region publicMethods
        // These will show up in the Actions category


        #endregion

        #region AcSmDatabase_for_reference
        // db.Clear();
        [IsVisibleInDynamoLibrary(false)]
        internal void Clear()
        {
            _curDatabase.Clear();
        }
        // db.FindObject(hand);
        [IsVisibleInDynamoLibrary(false)]
        internal IAcSmPersist FindObject(string hand)
        {
            return _curDatabase.FindObject(hand);
        }
        // db.GetCustomPropertyBag();
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmCustomPropertyBag CustomPropertyBag
        {
            get => _curDatabase.GetCustomPropertyBag();
        }
        // db.GetDatabase();
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmDatabase GetInternalDatabase()
        {
            return _curDatabase;
        }
        // db.GetDbVersion();
        [IsVisibleInDynamoLibrary(false)]
        internal string DatabaseVersion
        {
            get => _curDatabase.GetDbVersion();
        }
        // db.GetDesc();
        // db.SetDesc(desc);
        [IsVisibleInDynamoLibrary(false)]
        internal string Description
        {
            get => _curDatabase.GetDesc();
            set
            {
                if (LockDatabase(this, true))
                {
                    _curDatabase.SetDesc(value);
                    LockDatabase(this, false);
                }
            }
        }
        // db.GetDirectlyOwnedObjects(out objects);
        [IsVisibleInDynamoLibrary(false)]
        internal Array GetDirectlyOwnedObjects()
        {
            Array objects;
            _curDatabase.GetDirectlyOwnedObjects(out objects);
            return objects;
        }
        // db.GetEnumerator();
        [IsVisibleInDynamoLibrary(false)]
        internal IAcSmEnumPersist Enumerator
        {
            get => _curDatabase.GetEnumerator();
        }
        // db.GetFileName();
        // db.SetFileName(newVal);
        [IsVisibleInDynamoLibrary(false)]
        internal string FileName
        {
            get => _curDatabase.GetFileName();
            set
            {
                try
                {
                    if (LockDatabase(this, true))
                    {
                        // this line causes an error, as if the database isn't locked.
                        _curDatabase.SetFileName(value);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    LockDatabase(this, false);
                }
            }
        }
        // db.GetIsDirty();
        [IsVisibleInDynamoLibrary(false)]
        internal bool IsDirty
        {
            get => _curDatabase.GetIsDirty();
        }
        // db.GetIsTemporary();
        // db.SetIsTemporary();
        [IsVisibleInDynamoLibrary(false)]
        internal bool IsTemporary
        {
            get => _curDatabase.GetIsTemporary();
            set
            {
                if (LockDatabase(this, true))
                {
                    _curDatabase.SetIsTemporary();
                    LockDatabase(this, false);
                }
            }
        }
        // db.AcSmCustomPropertyBag();
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmCustomPropertyBag GetCustomPropertyBag()
        {
            return _curDatabase.GetCustomPropertyBag();
        }

        // db.GetLockOwnerInfo(pstrUserName, pstrMachineName);
        [IsVisibleInDynamoLibrary(false)]
        internal void GetLockOwnerInfo()
        {
            // this would be a prime candidate for returning multi values
            // need to read up on that!
            string pstrUserName;
            string pstrMachineName;
            _curDatabase.GetLockOwnerInfo(out pstrUserName, out pstrMachineName);
        }
        // db.GetLockStatus();
        [IsVisibleInDynamoLibrary(false)]
        internal AcSmLockStatus LockStatus
        {
            get => _curDatabase.GetLockStatus();
        }
        // db.GetNewObjectId(hand, out cookie);
        [IsVisibleInDynamoLibrary(false)]
        internal int GetNewObjectId(string hand)
        {
            int idCookie;
            _curDatabase.GetNewObjectId(hand, out idCookie);
            return idCookie;
        }
        // db.GetObjectId();
        [IsVisibleInDynamoLibrary(false)]
        internal IAcSmObjectId ObjectId
        {
            get => _curDatabase.GetObjectId();
        }
        // db.GetOwner();
        // db.SetOwner(pOwner);
        [IsVisibleInDynamoLibrary(false)]
        internal IAcSmPersist Owner
        {
            get => _curDatabase.GetOwner();
            set
            {
                if (LockDatabase(this, true))
                {
                    _curDatabase.SetOwner(value);
                    LockDatabase(this, false);
                }
            }
        }
        // db.GetSheetSet();
        [IsVisibleInDynamoLibrary(false)]
        internal SheetSet SheetSet
        {
            get => new SheetSet(_curDatabase.GetSheetSet());
        }
        // db.GetTemplateDstFileName();
        [IsVisibleInDynamoLibrary(false)]
        internal string TemplateDstFileName
        {
            get => _curDatabase.GetTemplateDstFileName();
        }
        // db.GetTypeName();
        [IsVisibleInDynamoLibrary(false)]
        internal string TypeName
        {
            get => _curDatabase.GetTypeName();
        }
        // db.InitNew(pOwner);
        [IsVisibleInDynamoLibrary(false)]
        internal void InitNew(IAcSmPersist pOwner)
        {
            _curDatabase.InitNew(pOwner);
        }
        // db.Load(pFiler);
        [IsVisibleInDynamoLibrary(false)]
        internal void Load(AcSmDSTFiler pFiler)
        {
            _curDatabase.Load(pFiler);
        }
        // db.LoadFromFile(templateDstFileName);
        [IsVisibleInDynamoLibrary(false)]
        internal void LoadFromFile(string templateDstFileName = "0")
        {
            _curDatabase.LoadFromFile(templateDstFileName);
        }
        // db.LockDb(pObject);
        [IsVisibleInDynamoLibrary(false)]
        internal void LockDatabase(object pObject)
        {
            _curDatabase.LockDb(pObject);
        }
        // db.NotifyRegisteredEventHandlers(eventcode, comp);
        [IsVisibleInDynamoLibrary(false)]
        internal void NotifyRegisteredEventHandlers(AcSmEvent eventCode, IAcSmPersist comp)
        {
            _curDatabase.NotifyRegisteredEventHandlers(eventCode, comp);
        }
        // db.Register(eventHandler);
        [IsVisibleInDynamoLibrary(false)]
        internal int RegisterEventHandler(IAcSmEvents eventHandler)
        {
            return _curDatabase.Register(eventHandler);
        }
        // db.RegisterOwner(idcookie, pObject, pOwner);
        [IsVisibleInDynamoLibrary(false)]
        internal void RegisterOwner(int idCookie, IAcSmPersist pObject, IAcSmPersist pOwner)
        {
            _curDatabase.RegisterOwner(idCookie, pObject, pOwner);
        }
        // db.Save(pFiler);
        [IsVisibleInDynamoLibrary(false)]
        internal void Save(AcSmDSTFiler pFiler)
        {
            _curDatabase.Save(pFiler);
        }
        // db.UnlockDb(pObject, bCommit);
        [IsVisibleInDynamoLibrary(false)]
        internal void UnlockDatabase(object pObject, bool commit = true)
        {
            _curDatabase.UnlockDb(pObject, commit);
        }
        // db.Unregister(cookie);
        [IsVisibleInDynamoLibrary(false)]
        internal void UnregisterEventHandler(int cookie)
        {
            _curDatabase.Unregister(cookie);
        }

        // db.UpdateInMemoryDwgHints();
        [IsVisibleInDynamoLibrary(false)]
        internal void UpdateInMemoryDwgHints()
        {
            _curDatabase.UpdateInMemoryDwgHints();
        }
        #endregion


        /// <summary>
        /// Locks or unlocks a database
        /// </summary>
        /// <param name="database">The database object to be locked.</param>
        /// <param name="lockFlag">true to lock, false to unlock</param>
        /// <returns>Returns if the lock was successful.</returns>
        /// <search>Sheet Set, Lock, Database</search>
        [IsVisibleInDynamoLibrary(false)]
        public static bool LockDatabase(Database database, bool lockFlag)
        {
            AcSmDatabase acDatabase = database.BaseObject;
            bool dbLock = false;
            // if the lockFlag is true, then attempt to lock the database, otherwise attempt to unlock it
            if ((lockFlag == true) && (acDatabase.GetLockStatus() == AcSmLockStatus.AcSmLockStatus_UnLocked))
            {
                acDatabase.LockDb(acDatabase);
                if (acDatabase.GetLockStatus() == AcSmLockStatus.AcSmLockStatus_Locked_Local)
                {
                    dbLock = true;
                }
            }
            else if ((lockFlag == false) && (acDatabase.GetLockStatus() == AcSmLockStatus.AcSmLockStatus_Locked_Local))
            {
                acDatabase.UnlockDb(acDatabase);
                dbLock = true;
            }
            else
            {
                dbLock = false;
            }
            return dbLock;
        }

        /// <summary>
        /// Format the name of this object
        /// </summary>
        /// <returns>A string representing the name of this object.</returns>
        public override string ToString()
        {
            return "Database: ( " + System.IO.Path.GetFileName(this.FileName) + " )";
        }
    }
}
