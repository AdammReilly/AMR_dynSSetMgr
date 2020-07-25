using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [IsVisibleInDynamoLibrary(true)]
    public partial class Database
    {

        // db.Clear();
        [IsVisibleInDynamoLibrary(true)]
        public static void Clear(Database database)
        {
            if (database != null)
            {
                database.Clear();
            }
        }
        // db.FindObject(hand);
        [IsVisibleInDynamoLibrary(true)]
        public static IAcSmPersist FindObject(Database database, string hand)
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
        public static void GetCustomPropertyBag(Database database)
        {
            if (database != null)
            {
                database.GetCustomPropertyBag();
            }
        }
        // db.GetDatabase();
        [IsVisibleInDynamoLibrary(true)]
        public static AcSmDatabase GetDatabase(Database database)
        {
            if (database != null)
            {
                return database.GetInternalDatabase();
            }
            else { return null; }
        }
        // db.GetDbVersion();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetDatabaseVersion(Database database)
        {
            if (database != null)
            {
                return database.DatabaseVersion;
            }
            else { return null; }
        }
        // db.GetDesc();
        [IsVisibleInDynamoLibrary(false)]
        public static string GetDescription(Database database)
        {
            if (database != null)
            {
                return database.Description;
            }
            else { return null; }
        }
        // db.GetDirectlyOwnedObjects(out objects);
        [IsVisibleInDynamoLibrary(true)]
        public static Array GetDirectlyOwnedObjects(Database database)
        {
            if (database != null)
            {
                return database.GetDirectlyOwnedObjects();
            }
            else { return null; }
        }
        // db.GetEnumerator();
        [IsVisibleInDynamoLibrary(true)]
        public static IAcSmEnumPersist GetEnumerator(Database database)
        {
            if (database != null)
            {
                return database.Enumerator;
            }
            else { return null; }
        }
        // db.GetFileName();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetFileName(Database database)
        {
            if (database != null)
            {
                return database.FileName;
            }
            else { return null; }
        }
        // db.GetIsDirty();
        [IsVisibleInDynamoLibrary(true)]
        public static bool GetIsDirty(Database database)
        {
            if (database != null)
            {
                return database.IsDirty;
            }
            else { return true; }
        }
        // db.GetIsTemporary();
        [IsVisibleInDynamoLibrary(true)]
        public static bool GetIsTemporary(Database database)
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
        [IsVisibleInDynamoLibrary(false)]
        public static string GetName(Database database)
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
        public static SheetSet GetSheetSet(Database database)
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
        public static Database Save(Database database, AcSmDSTFiler pFiler)
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
        public static Database SetDescription(Database database, string newDecription)
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
        public static Database SetFileName(Database database, string filename)
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
        public static Database SetIsTemporary(Database database, bool isTemporary)
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
        public static Database SetName(Database database, string name = "")
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
        public static Database SetOwner(Database database, IAcSmPersist owner)
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
