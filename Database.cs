using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ACSMCOMPONENTS24Lib;
using Autodesk.DesignScript.Runtime;
using AXDBLib;

namespace AMR.dynSSetMgr
{
    [IsVisibleInDynamoLibrary(true)]
    public static class Database
    {

        // db.Clear();
        [IsVisibleInDynamoLibrary(true)]
        public static void Clear(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                amrDatabase.Clear();
            }
        }
        // db.FindObject(hand);
        [IsVisibleInDynamoLibrary(true)]
        public static IAcSmPersist FindObject(AMRDatabase amrDatabase, string hand)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.FindObject(hand);
            }
            else
            { return null; }
        }
        // db.GetCustomPropertyBag();
        [IsVisibleInDynamoLibrary(true)]
        public static void GetCustomPropertyBag(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                amrDatabase.GetCustomPropertyBag();
            }
        }
        // db.GetDatabase();
        [IsVisibleInDynamoLibrary(true)]
        public static AcSmDatabase GetDatabase(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.Database;
            }
            else { return null; }
        }
        // db.GetDbVersion();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetDatabaseVersion(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.DatabaseVersion;
            }
            else { return null; }
        }
        // db.GetDesc();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetDescription(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.Description;
            }
            else { return null; }
        }
        // db.GetDirectlyOwnedObjects(out objects);
        [IsVisibleInDynamoLibrary(true)]
        public static Array GetDirectlyOwnedObjects(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.GetDirectlyOwnedObjects();
            }
            else { return null; }
        }
        // db.GetEnumerator();
        [IsVisibleInDynamoLibrary(true)]
        public static IAcSmEnumPersist GetEnumerator(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.GetEnumerator;
            }
            else { return null; }
        }
        // db.GetFileName();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetFileName(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.FileName;
            }
            else { return null; }
        }
        // db.GetIsDirty();
        [IsVisibleInDynamoLibrary(true)]
        public static bool GetIsDirty(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.IsDirty;
            }
            else { return true; }
        }
        // db.GetIsTemporary();
        [IsVisibleInDynamoLibrary(true)]
        public static bool GetIsTemporary(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.IsTemporary;
            }
            else { return true; }
        }
        // db.GetLockOwnerInfo(pstrUserName, pstrMachineName);
        // db.GetLockStatus();
        // db.GetName();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetName(AMRDatabase amrDatabase)
        {
            if (amrDatabase != null)
            {
                return amrDatabase.Name;
            }
            else { return null; }
        }
        // db.GetNewObjectId(hand, out cookie);
        // db.GetObjectId();
        // db.GetOwner();
        // db.GetSheetSet();
        [IsVisibleInDynamoLibrary(true)]
        public static AMRSheetSet GetSheetSet(AMRDatabase amrDatabase)
        {
            return amrDatabase.SheetSet;
        }
        // db.GetTemplateDstFileName();
        // db.GetTypeName();
        // db.InitNew(pOwner);
        // db.Load(pFiler);
        // db.LoadFromFile(templateDstFileName);
        // db.LockDb(pObject);
        // db.NotifyRegisteredEventHandlers(eventcode, comp);
        // db.Register(eventHandler);
        // db.RegisterOwner(idcookie, pObject, pOwner);
        // db.Save(pFiler);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase Save(AMRDatabase amrDatabase, AcSmDSTFiler pFiler)
        {
            if ((amrDatabase != null) && (pFiler != null))
            {
                amrDatabase.Save(pFiler);
                return amrDatabase;
            }
            else { return null; }
        }
        // db.SetDesc(desc);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetDescription(AMRDatabase amrDatabase, string newDecription)
        {
            if (amrDatabase != null)
            {
                amrDatabase.Description = newDecription;
                return amrDatabase;
            }
            else { return null; }
        }
        // db.SetFileName(newVal);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetFileName(AMRDatabase amrDatabase, string filename)
        {
            if (amrDatabase != null)
            {
                amrDatabase.FileName = filename;
                return amrDatabase;
            }
            else { return null; }
        }
        // db.SetIsTemporary();
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetIsTemporary(AMRDatabase amrDatabase, bool isTemporary)
        {
            if (amrDatabase != null)
            {
                amrDatabase.IsTemporary = isTemporary;
                return amrDatabase;
            }
            else { return null; }
        }
        // db.SetName(name);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetName(AMRDatabase amrDatabase, string name = "")
        {
            if (amrDatabase != null)
            {
                amrDatabase.Name = name;
                return amrDatabase;
            }
            else { return null; }
        }
        // db.SetOwner(pOwner);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetOwner(AMRDatabase amrDatabase, IAcSmPersist owner)
        {
            if (amrDatabase != null)
            {
                amrDatabase.Owner = owner;
                return amrDatabase;
            }
            else { return null; }
        }
        // db.UnlockDb(pObject, bCommit);
        // db.Unregister(cookie);
        // db.UpdateInMemoryDwgHints();

    }
}
