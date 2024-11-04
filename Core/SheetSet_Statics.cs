using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if C3D2025
using ACSMCOMPONENTS25Lib;
#elif C3D2023 || C3D2024
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

namespace AMR.dynSSetMgr
{
    public partial class SheetSet
    {
        [IsVisibleInDynamoLibrary(false)]
        public static Guid GetClassID()
        {
            throw new NotImplementedException();
        }
        [IsVisibleInDynamoLibrary(false)]
        public static bool GetIsDirty(SheetSet sheetSet)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void Load(AcSmDSTFiler pFiler)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void Save(AcSmDSTFiler pFiler)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static string GetTypeName()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void InitNew(IAcSmPersist pOwner)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmPersist GetOwner()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetOwner(IAcSmPersist pOwner)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmObjectId GetObjectId()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void GetDirectlyOwnedObjects(out Array objects)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmCustomPropertyBag GetCustomPropertyBag()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmFileReference GetNewSheetLocation()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetNewSheetLocation(IAcSmFileReference pFileRef)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmAcDbLayoutReference GetDefDwtLayout()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetDefDwtLayout(AcSmAcDbLayoutReference pLayoutRef)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static bool GetPromptForDwt()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetPromptForDwt(bool askForDwt)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmEnumComponent GetSheetEnumerator()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static Sheet AddNewSheet(SheetSet sheetSet, string name, string desc = "")
        {
            Sheet newSheet;
            if (sheetSet != null)
            {
                newSheet = sheetSet.AddNewSheet(name, desc);
                return newSheet;
            }
            else { return null; }
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void InsertComponent(IAcSmComponent newSheet, IAcSmComponent beforeComp)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void InsertComponentAfter(IAcSmComponent newSheet, IAcSmComponent afterComp)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmSheet ImportSheet(AcSmAcDbLayoutReference pLayoutRef)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void RemoveSheet(AcSmSheet sheet)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmSubset CreateSubset(string name, string desc)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void RemoveSubset(IAcSmSubset subset)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void NotifyRegisteredEventHandlers(AcSmEvent eventcode, IAcSmPersist comp)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void UpdateInMemoryDwgHints()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmFileReference GetAltPageSetups()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetAltPageSetups(IAcSmFileReference pDwtRef)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmNamedAcDbObjectReference GetDefAltPageSetup()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetDefAltPageSetup(IAcSmNamedAcDbObjectReference pAltPageSetup)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static bool GetPromptForDwgName()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetPromptForDwgName(bool askForName)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmSheetSelSets GetSheetSelSets()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmResources GetResources()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static IAcSmCalloutBlocks GetCalloutBlocks()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmViewCategories GetViewCategories()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmAcDbBlockRecordReference GetDefLabelBlk()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void SetDefLabelBlk(AcSmAcDbBlockRecordReference pLabelBlkRef)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static AcSmPublishOptions GetPublishOptions()
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void Sync(AcadDatabase pXDb)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static int Register(IAcSmEvents eventHandler)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void Unregister(int cookie)
        {
            throw new NotImplementedException();
        }

        [IsVisibleInDynamoLibrary(false)]
        public static void UpdateSheetCustomProps()
        {
            throw new NotImplementedException();
        }
    }
}
