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
    public static class Sheet
    {
        // sheet.Clear();
        [IsVisibleInDynamoLibrary(false)]
        public static void Clear()
        {

        }
        // sheet.GetCustomPropertyBag();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetCustomPropertyBag()
        {

        }
        // sheet.GetDatabase();
        [IsVisibleInDynamoLibrary(true)]
        public static AMRDatabase GetDatabase(AMRSheet sheet)
        {
            if (sheet != null)
            { return sheet.GetDatabase(); }
            else { return null; }
        }
        // sheet.GetDesc();
        [IsVisibleInDynamoLibrary(true)]
        public static string GetDesc(AMRSheet sheet)
        {
            if (sheet != null)
            { return sheet.Description; }
            else { return ""; }
        }
        // sheet.GetDirectlyOwnedObjects(out objects);
        [IsVisibleInDynamoLibrary(false)]
        public static void GetDirectlyOwnedObjects()
        {

        }
        // sheet.GetDoNotPlot();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetDoNotPlot()
        {

        }
        // sheet.GetIsDirty();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetIsDirty()
        {

        }
        // sheet.GetLayout();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetLayout()
        {

        }
        // sheet.GetName();
        [IsVisibleInDynamoLibrary(false)]
        public static string GetName(AMRSheet sheet)
        {
            if (sheet != null)
            { return sheet.Name; }
            else { return ""; }
        }
        // sheet.GetNumber();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetNumber()
        {

        }
        // sheet.GetObjectId();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetObjectId()
        {

        }
        // sheet.GetOwner();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetOwner()
        {

        }
        // sheet.GetSheetViews();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetSheetViews()
        {

        }
        // sheet.GetTitle();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetTitle()
        {

        }
        // sheet.GetTypeName();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetTypeName()
        {

        }
        // sheet.InitNew(pOwner);
        [IsVisibleInDynamoLibrary(false)]
        public static void InitNew()
        {

        }
        // sheet.Load(pFiler);
        [IsVisibleInDynamoLibrary(false)]
        public static void Load()
        {

        }
        // sheet.Save(pFiler);
        [IsVisibleInDynamoLibrary(false)]
        public static void Save()
        {

        }
        // sheet.SetDesc(desc);
        [IsVisibleInDynamoLibrary(false)]
        public static AMRSheet SetDesc(AMRSheet sheet, string description)
        {
            if (sheet != null)
            { sheet.Description = description; }
            return sheet;
        }
        // sheet.SetDoNotPlot(doNotPlot);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetDoNotPlot()
        {

        }
        // sheet.SetLayout(pLayoutRef);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetLayout()
        {

        }
        // sheet.SetName(name);
        [IsVisibleInDynamoLibrary(false)]
        public static AMRSheet SetName(AMRSheet sheet, string name)
        {
            if (sheet != null)
            { sheet.Name = name; }
            return sheet;
        }
        // sheet.SetNumber(num);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetNumber()
        {

        }
        // sheet.SetOwner(pOwner);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetOwner()
        {

        }
        // sheet.SetTitle(title);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetTitle()
        {

        }
        // sheet2.GetCategory();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetCategory()
        {

        }
        // sheet2.GetIssuePurpose();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetIssuePurpose()
        {

        }
        // sheet2.GetRevisionDate();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetRevisionDate()
        {

        }
        // sheet2.GetRevisionNumber();
        [IsVisibleInDynamoLibrary(false)]
        public static void GetRevisionNumber()
        {

        }
        // sheet2.SetCategory(newVal);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetCategory()
        {

        }
        // sheet2.SetIssuePurpose(newVal);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetIssuePurpose()
        {

        }
        // sheet2.SetRevisionDate(newVal);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetRevisionDate()
        {

        }
        // sheet2.SetRevisionNumber(newVal);
        [IsVisibleInDynamoLibrary(false)]
        public static void SetRevisionNumber()
        {

        }

    }
}
