using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if C3D2023
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
using Dynamo.Core;

namespace AMR.dynSSetMgr
{
    /// <summary>
    /// Represents a subset.
    /// </summary>
    [IsVisibleInDynamoLibrary(true)]
    public class EmptyClass
    {
        AcSmSheetSelSet _curSelSet;

        #region constructors
        // constuctors need to be static methods in order to show up in the Create category
        internal EmptyClass(AcSmSheetSelSet baseObject)
        {
            _curSelSet = baseObject;
        }

        #endregion


        #region properties
        // properties can only be read-only and will show up in the Query category
        // settable properties will be ignored in Dynamo

        #endregion



        #region publicMethods
        // These will show up in the Actions category



        #endregion


    }
}
