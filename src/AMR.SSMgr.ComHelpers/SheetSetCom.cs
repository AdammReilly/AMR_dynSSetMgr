using Autodesk.AutoCAD.Interop;

namespace AMR.SSMgr.ComHelpers
{
    /// <summary>
    /// Sheet Set Manager COM calls whose signatures use interop enums.
    /// </summary>
    /// <remarks>
    /// Embedding the interop copies every enum a called method uses into the calling assembly as a
    /// public type, and Dynamo lists every public enum in a node library. Keeping these calls here,
    /// behind an int-based API, keeps PropertyFlags and AcSmLockStatus out of AMR.SSMgr.dll and so
    /// out of the Dynamo library. Constants are inlined into callers, so they don't embed the enums.
    /// COM objects are passed as object and cast here; the cast is a QueryInterface by IID.
    /// </remarks>
    public static class SheetSetCom
    {
        /// <summary>Flag for a custom property stored on the sheet set.</summary>
        public const int SheetSetPropertyFlag = (int)PropertyFlags.CUSTOM_SHEETSET_PROP;

        /// <summary>Flag for a custom property stored on each sheet.</summary>
        public const int SheetPropertyFlag = (int)PropertyFlags.CUSTOM_SHEET_PROP;

        /// <summary>The database is not locked.</summary>
        public const int LockStatusUnlocked = (int)AcSmLockStatus.AcSmLockStatus_UnLocked;

        /// <summary>The database is locked by this session.</summary>
        public const int LockStatusLockedLocal = (int)AcSmLockStatus.AcSmLockStatus_Locked_Local;

        /// <summary>Gets the flags of an AcSmCustomPropertyValue.</summary>
        public static int GetPropertyFlags(object customPropertyValue)
        {
            return (int)((IAcSmCustomPropertyValue)customPropertyValue).GetFlags();
        }

        /// <summary>Sets the flags of an AcSmCustomPropertyValue.</summary>
        public static void SetPropertyFlags(object customPropertyValue, int flags)
        {
            ((IAcSmCustomPropertyValue)customPropertyValue).SetFlags((PropertyFlags)flags);
        }

        /// <summary>Gets the lock status of an AcSmDatabase.</summary>
        public static int GetLockStatus(object database)
        {
            return (int)((IAcSmDatabase)database).GetLockStatus();
        }
    }
}
