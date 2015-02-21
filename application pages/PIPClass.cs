// -----------------------------------------------------------------------
// <copyright file="PIPClass.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VFS.PMS.ApplicationPages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 
    [Serializable]
    public class PIPClass
    {
        public string ID { get; set; }
        public string appPerformanceCycle { get; set; }
        public string appEmployeeCode { get; set; }
        public string EmpName { get; set; }
        public string Phase { get; set; }
        public string appAppraisalStatus { get; set; }
    }
}
