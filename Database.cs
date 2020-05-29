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
        public static void Clear(AMRDatabase database)
        {
            if (database != null)
            {
                database.Clear();
            }
        }
        // db.FindObject(hand);
        [IsVisibleInDynamoLibrary(true)]
        public static IAcSmPersist FindObject(AMRDatabase database, string hand)
        {
            if (database != null)
            {
                return database.FindObject(hand);
            }
            else
            { return null; }
        }
        // db.GetCustomPropertyBag();
        [IsVisibleInDynamoLibrary(true)]
        public static void GetCustomPropertyBag(AMRDatabase database)
        {
            if (database != null)
            {
                database.GetCustomPropertyBag();
            }
        }
        // db.GetDatabase();
        [IsVisibleInDynamoLibrary(true)]
        public static AcSmDatabase GetDatabase(AMRDatabase database)
        {
            if (database != null)
            {
                return database.Database;
            }
            else { return null; }
        }
        // db.GetDbVersion();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetDatabaseVersion(AMRDatabase database)
        {
            if (database != null)
            {
                return database.DatabaseVersion;
            }
            else { return null; }
        }
        // db.GetDesc();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetDescription(AMRDatabase database)
        {
            if (database != null)
            {
                return database.Description;
            }
            else { return null; }
        }
        // db.GetDirectlyOwnedObjects(out objects);
        [IsVisibleInDynamoLibrary(true)]
        public static Array GetDirectlyOwnedObjects(AMRDatabase database)
        {
            if (database != null)
            {
                return database.GetDirectlyOwnedObjects();
            }
            else { return null; }
        }
        // db.GetEnumerator();
        [IsVisibleInDynamoLibrary(true)]
        public static IAcSmEnumPersist GetEnumerator(AMRDatabase database)
        {
            if (database != null)
            {
                return database.GetEnumerator;
            }
            else { return null; }
        }
        // db.GetFileName();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetFileName(AMRDatabase database)
        {
            if (database != null)
            {
                return database.FileName;
            }
            else { return null; }
        }
        // db.GetIsDirty();
        [IsVisibleInDynamoLibrary(true)]
        public static bool GetIsDirty(AMRDatabase database)
        {
            if (database != null)
            {
                return database.IsDirty;
            }
            else { return true; }
        }
        // db.GetIsTemporary();
        [IsVisibleInDynamoLibrary(true)]
        public static bool GetIsTemporary(AMRDatabase database)
        {
            if (database != null)
            {
                return database.IsTemporary;
            }
            else { return true; }
        }
        // db.GetLockOwnerInfo(pstrUserName, pstrMachineName);
        // db.GetLockStatus();
        // db.GetName();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetName(AMRDatabase database)
        {
            if (database != null)
            {
                return database.Name;
            }
            else { return null; }
        }
        // db.GetNewObjectId(hand, out cookie);
        // db.GetObjectId();
        // db.GetOwner();
        // db.GetSheetSet();
        [IsVisibleInDynamoLibrary(true)]
        public static AMRSheetSet GetSheetSet(AMRDatabase database)
        {
            return database.SheetSet;
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
        public static AMRDatabase Save(AMRDatabase database, AcSmDSTFiler pFiler)
        {
            if ((database != null) && (pFiler != null))
            {
                database.Save(pFiler);
                return database;
            }
            else { return null; }
        }
        // db.SetDesc(desc);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetDescription(AMRDatabase database, string newDecription)
        {
            if (database != null)
            {
                database.Description = newDecription;
                return database;
            }
            else { return null; }
        }
        // db.SetFileName(newVal);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetFileName(AMRDatabase database, string filename)
        {
            if (database != null)
            {
                database.FileName = filename;
                return database;
            }
            else { return null; }
        }
        // db.SetIsTemporary();
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetIsTemporary(AMRDatabase database, bool isTemporary)
        {
            if (database != null)
            {
                database.IsTemporary = isTemporary;
                return database;
            }
            else { return null; }
        }
        // db.SetName(name);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetName(AMRDatabase database, string name = "")
        {
            if (database != null)
            {
                database.Name = name;
                return database;
            }
            else { return null; }
        }
        // db.SetOwner(pOwner);
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase SetOwner(AMRDatabase database, IAcSmPersist owner)
        {
            if (database != null)
            {
                database.Owner = owner;
                return database;
            }
            else { return null; }
        }
        // db.UnlockDb(pObject, bCommit);
        // db.Unregister(cookie);
        // db.UpdateInMemoryDwgHints();

    }
}
