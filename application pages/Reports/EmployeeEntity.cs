// -----------------------------------------------------------------------
// <copyright file="EmployeeEntity.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VFS.PMS.ApplicationPages.Layouts.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EmployeeEntity
    {
        public string RegionName { get; set; }
        public string RegionCode { get; set; }

        public string CountryName { get; set; }
        public string CountryCode { get; set; }

        public string OrganizationUnitCode { get; set; }
        public string OrganizationUnitText { get; set; }
        
        public string EmployeeCode { get; set; }
        public string EmpName { get; set; }
        public string PositionText { get; set; }
        public string WorkLevel { get; set; }
        public string EmployeeSubGroup { get; set; }
        //public string Region { get; set; }
        //public string Country { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string HRBusinessPatner { get; set; }
        public string AppraiserName { get; set; }
        public string AppraiserCode { get; set; }
        public string ReviewerName { get; set; }
        public string JoiningDate { get; set; }
        public string ConfirmationDueDate { get; set; }
        public string ConfirmationDate { get; set; }
        public string EmployeeStatus { get; set; }
        public string EligibilityH1 { get; set; }
        public string EligibilityH2 { get; set; }
        public string OldEmployeeCode { get; set; }

        public string AppraisalCurrentState { get; set; }
    }
}
