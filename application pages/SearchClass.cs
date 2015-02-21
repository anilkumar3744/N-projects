// -----------------------------------------------------------------------
// <copyright file="SearchClass.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VFS.PMS.ApplicationPages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.SharePoint;
    using Microsoft.SharePoint.Linq;
    using Microsoft.SharePoint.WebControls;
    using System.Data;
    using System.Web.UI.WebControls;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 
    [Serializable]
    public class SearchAppraisalClass
    {
        public string appPerformanceCycle { get; set; }
        public string appEmployeeCode { get; set; }
        public string EmpName { get; set; }
        public string ApprName { get; set; }
        public string ApprCode { get; set; }
        public string RevName { get; set; }
        public string RevCode { get; set; }
        public string HrName { get; set; }
        public string HrCode { get; set; }
        public string OrganizationUnitCode { get; set; }
        public string PositionCode { get; set; }
        public string PersonnelSubAreaCode { get; set; }
        public string PersonnelAreaCode { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeSubGroupCode { get; set; }
        public string EmployeeGroupCode { get; set; }
        public string ID { get; set; }
        public string appAppraisalStatus { get; set; }
        public string TaskID { get; set; }
        public string IsReview { get; set; }
        public string Deactivated { get; set; }
        public string Region { get; set; }
        public string acmptCompetency { get; set; }
        public string acmptDescription { get; set; }
        #region Commented
        ////public string appPerformanceCycle { get; set; }
        ////public string appPerformanceCycle { get; set; }
        ////private DataTable GetTaskListDetails(string currentUserName)
        ////{
        ////    DataTable dt = null;

        ////    using (SPWeb currentWeb = SPContext.Current.Site.OpenWeb())
        ////    {
        ////        VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(currentWeb.Url);
        ////        AppraisalCompetenciesItem aci=new AppraisalCompetenciesItem();

        ////          //var AppraiserRecords=(from varAppraisals in dc.Appraisals 
        ////          //                            join varEmps in dc.EmployeeMasters on varAppraisals.Id equals varEmps.EmployeeCode
        ////          //                            join varAreas in dc.Areas on varEmps.AreaID equals varAreas.AreaCode
        ////          //                            join varSubArea in dc.SubAreas on varAreas.AreaCode equals varSubArea.

        ////        //var ss =(from r in dc.AppraisalPhases where r
        ////        //SPList appraisals = currentWeb.Lists["VFSAppraisalTasks"];
        ////        //SPQuery spQuery = new SPQuery();
        ////        //spQuery.Query = "<Where><Eq><FieldRef Name='AssignedTo' /><Value Type='Text'>" + currentUserName + "</Value></Eq></Where>";
        ////        //////query.ViewFields = "<FieldRef name='appPerformanceCycle'/><FieldRef name='appAppraisalStatus'/>";
        ////        //SPListItemCollection items = appraisals.GetItems(spQuery);
        ////        //DataTable dt = items.GetDataTable();
        ////        return dt;
        ////    }
        ////} 
        #endregion
    }
}
