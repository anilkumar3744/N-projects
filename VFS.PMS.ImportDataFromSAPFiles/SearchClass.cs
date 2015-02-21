using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VFS.PMS.ImportDataFromSAPFiles
{
    public class SearchClass
    {
        public double appPerformanceCycle { get; set; }
        public string appEmployeeCode { get; set; }
        public string EmpName { get; set; }
        public string ApprName { get; set; }
        public string RevName { get; set; }
        public int ID { get; set; }
        public string appAppraisalStatus { get; set; }
        public string TaskID { get; set; }
    }
}
