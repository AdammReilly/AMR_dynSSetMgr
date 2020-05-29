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
    [IsVisibleInDynamoLibrary(false)]
    public class AMRDatabase
    {
        AcSmDatabase _curDatabase;

        [IsVisibleInDynamoLibrary(false)]
        public AcSmDatabase BaseObject
        { get => _curDatabase; }

        [IsVisibleInDynamoLibrary(false)]
        public AMRDatabase(AcSmDatabase dbFromSSMgr)
        {
            _curDatabase = dbFromSSMgr;
        }

        // db.Clear();
        [IsVisibleInDynamoLibrary(false)]
        public void Clear()
        {
            _curDatabase.Clear();
        }
        // db.FindObject(hand);
        [IsVisibleInDynamoLibrary(false)]
        public IAcSmPersist FindObject(string hand)
        {
            return _curDatabase.FindObject(hand);
        }
        // db.GetCustomPropertyBag();
        [IsVisibleInDynamoLibrary(false)]
        AcSmCustomPropertyBag CustomPropertyBag
        {
            get => _curDatabase.GetCustomPropertyBag();
        }
        // db.GetDatabase();
        [IsVisibleInDynamoLibrary(false)]
        public AcSmDatabase Database
        {
            get => _curDatabase.GetDatabase();
        }
        // db.GetDbVersion();
        [IsVisibleInDynamoLibrary(false)]
        public string DatabaseVersion
        {
            get => _curDatabase.GetDbVersion();
        }
        // db.GetDesc();
        // db.SetDesc(desc);
        [IsVisibleInDynamoLibrary(false)]
        public string Description
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
        public Array GetDirectlyOwnedObjects()
        {
            Array objects;
            _curDatabase.GetDirectlyOwnedObjects(out objects);
            return objects;
        }
        // db.GetEnumerator();
        [IsVisibleInDynamoLibrary(false)]
        public IAcSmEnumPersist GetEnumerator
        {
            get => _curDatabase.GetEnumerator();
        }
        // db.GetFileName();
        // db.SetFileName(newVal);
        [IsVisibleInDynamoLibrary(false)]
        public string FileName
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
        public bool IsDirty
        {
            get => _curDatabase.GetIsDirty();
        }
        // db.GetIsTemporary();
        // db.SetIsTemporary();
        [IsVisibleInDynamoLibrary(false)]
        public bool IsTemporary
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
        public AcSmCustomPropertyBag GetCustomPropertyBag()
        {
            return _curDatabase.GetCustomPropertyBag();
        }

        // db.GetLockOwnerInfo(pstrUserName, pstrMachineName);
        [IsVisibleInDynamoLibrary(false)]
        public void GetLockOwnerInfo()
        {
            // this would be a prime candidate for returning multi values
            // need to read up on that!
            string pstrUserName;
            string pstrMachineName;
            _curDatabase.GetLockOwnerInfo(out pstrUserName, out pstrMachineName);
        }
        // db.GetLockStatus();
        [IsVisibleInDynamoLibrary(false)]
        public AcSmLockStatus LockStatus
        {
            get => _curDatabase.GetLockStatus();
        }
        // db.GetName();
        // db.SetName(name);
        [IsVisibleInDynamoLibrary(false)]
        public string Name
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
        public int GetNewObjectId(string hand)
        {
            int idCookie;
            _curDatabase.GetNewObjectId(hand, out idCookie);
            return idCookie;
        }
        // db.GetObjectId();
        [IsVisibleInDynamoLibrary(false)]
        public IAcSmObjectId ObjectId
        {
            get => _curDatabase.GetObjectId();
        }
        // db.GetOwner();
        // db.SetOwner(pOwner);
        [IsVisibleInDynamoLibrary(false)]
        public IAcSmPersist Owner
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
        public AMRSheetSet SheetSet
        {
            get => new AMRSheetSet(_curDatabase.GetSheetSet());
        }
        // db.GetTemplateDstFileName();
        [IsVisibleInDynamoLibrary(false)]
        public string TemplateDstFileName
        {
            get => _curDatabase.GetTemplateDstFileName();
        }
        // db.GetTypeName();
        [IsVisibleInDynamoLibrary(false)]
        public string TypeName
        {
            get => _curDatabase.GetTypeName();
        }
        // db.InitNew(pOwner);
        [IsVisibleInDynamoLibrary(false)]
        public void InitNew(IAcSmPersist pOwner)
        {
            _curDatabase.InitNew(pOwner);
        }
        // db.Load(pFiler);
        [IsVisibleInDynamoLibrary(false)]
        public void Load(AcSmDSTFiler pFiler)
        {
            _curDatabase.Load(pFiler);
        }
        // db.LoadFromFile(templateDstFileName);
        [IsVisibleInDynamoLibrary(false)]
        public void LoadFromFile(string templateDstFileName = "0")
        {
            _curDatabase.LoadFromFile(templateDstFileName);
        }
        // db.LockDb(pObject);
        [IsVisibleInDynamoLibrary(false)]
        public void LockDatabase(object pObject)
        {
            _curDatabase.LockDb(pObject);
        }
        // db.NotifyRegisteredEventHandlers(eventcode, comp);
        [IsVisibleInDynamoLibrary(false)]
        public void NotifyRegisteredEventHandlers(AcSmEvent eventCode, IAcSmPersist comp)
        {
            _curDatabase.NotifyRegisteredEventHandlers(eventCode, comp);
        }
        // db.Register(eventHandler);
        [IsVisibleInDynamoLibrary(false)]
        public int RegisterEventHandler(IAcSmEvents eventHandler)
        {
            return _curDatabase.Register(eventHandler);
        }
        // db.RegisterOwner(idcookie, pObject, pOwner);
        [IsVisibleInDynamoLibrary(false)]
        public void RegisterOwner(int idCookie, IAcSmPersist pObject, IAcSmPersist pOwner)
        {
            _curDatabase.RegisterOwner(idCookie, pObject, pOwner);
        }
        // db.Save(pFiler);
        [IsVisibleInDynamoLibrary(false)]
        public void Save(AcSmDSTFiler pFiler)
        {
            _curDatabase.Save(pFiler);
        }
        // db.UnlockDb(pObject, bCommit);
        [IsVisibleInDynamoLibrary(false)]
        public void UnlockDatabase(object pObject, bool commit = true)
        {
            _curDatabase.UnlockDb(pObject, commit);
        }
        // db.Unregister(cookie);
        [IsVisibleInDynamoLibrary(false)]
        public void UnregisterEventHandler(int cookie)
        {
            _curDatabase.Unregister(cookie);
        }

        // db.UpdateInMemoryDwgHints();
        [IsVisibleInDynamoLibrary(false)]
        public void UpdateInMemoryDwgHints()
        {
            _curDatabase.UpdateInMemoryDwgHints();
        }

        /// <summary>
        /// Locks or unlocks the database
        /// </summary>
        /// <param name="lockFlag">true to lock, false to unlock</param>
        /// <returns>Returns if the lock was successful.</returns>
        [IsVisibleInDynamoLibrary(false)]
        public bool LockDatabase(bool lockFlag)
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
