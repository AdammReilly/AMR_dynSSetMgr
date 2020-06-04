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
    public partial class Database
    {
       internal AcSmDatabase _curDatabase;

        [IsVisibleInDynamoLibrary(false)]
        internal AcSmDatabase BaseObject
        { get => _curDatabase; }

        [IsVisibleInDynamoLibrary(false)]
        internal Database(AcSmDatabase dbFromSSMgr)
        {
            _curDatabase = dbFromSSMgr;
        }

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
                if (LockDatabase(true))
                {
                    _curDatabase.SetDesc(value);
                    LockDatabase(false);
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
                    if (LockDatabase(true))
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
                    LockDatabase(false);
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
                if (LockDatabase(true))
                {
                    _curDatabase.SetIsTemporary();
                    LockDatabase(false);
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
        // db.GetName();
        // db.SetName(name);
        [IsVisibleInDynamoLibrary(false)]
        internal string Name
        {
            get => _curDatabase.GetName();
            set
            {
                if (LockDatabase(true))
                {
                    _curDatabase.SetName(value);
                    LockDatabase(false);
                }
            }
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
                if (LockDatabase(true))
                {
                    _curDatabase.SetOwner(value);
                    LockDatabase(false);
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

        /// <summary>
        /// Locks or unlocks the database
        /// </summary>
        /// <param name="lockFlag">true to lock, false to unlock</param>
        /// <returns>Returns if the lock was successful.</returns>
        /// <search>Sheet Set, Lock, Database</search>
        [IsVisibleInDynamoLibrary(false)]
        internal bool LockDatabase(bool lockFlag)
        {
            bool dbLock = false;
            // if the lockFlag is true, then attempt to lock the database, otherwise attempt to unlock it
            if ((lockFlag == true) && (_curDatabase.GetLockStatus() == AcSmLockStatus.AcSmLockStatus_UnLocked))
            {
                _curDatabase.LockDb(_curDatabase);
                if (_curDatabase.GetLockStatus() == AcSmLockStatus.AcSmLockStatus_Locked_Local)
                {
                    dbLock = true;
                }
            }
            else if ((lockFlag == false) && (_curDatabase.GetLockStatus() == AcSmLockStatus.AcSmLockStatus_Locked_Local))
            {
                _curDatabase.UnlockDb(_curDatabase);
                dbLock = true;
            }
            else
            {
                dbLock = false;
            }
            return dbLock;
        }

    }
}
