// -----------------------------------------------------------------------
// <copyright file="ReportDetails.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VFS.PMS.ApplicationPages.Layouts.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.SharePoint;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ReportDetails
    {
        public List<EmployeeEntity> GetRegions()
        {
            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    return (from regions in PMSDataContext.Regions.AsEnumerable()
                            select new EmployeeEntity
                            {
                                RegionName = regions.RegionName,
                                RegionCode = regions.Id.ToString(),
                            }).ToList();

                }
            }
        }

        public List<EmployeeEntity> GetCompanies(string regionIds)
        {
            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    return (from companies in PMSDataContext.Companies.AsEnumerable()
                            select new EmployeeEntity
                            {
                                CountryName = companies.CompanyName,
                                CountryCode = companies.CompanyCode.ToString(),
                            }).ToList();

                }
            }
        }

        public List<EmployeeEntity> GetOrganizations(string regionIds)
        {
            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    return (from organizationUnits in PMSDataContext.OrganizationUnits.AsEnumerable()
                            select new EmployeeEntity
                            {
                                OrganizationUnitText = organizationUnits.OrganizationUnitShortText,
                                OrganizationUnitCode = organizationUnits.OrganizationUnitCode.ToString(),
                            }).ToList();

                }
            }
        }

        public List<EmployeeEntity> EligibleEmployees()
        {
            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    return (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                            //where app.PerformanceCycle.ToString() == performanceCycle.ToString() && appPhases.AppraisalPhase == "H1"
                            select new EmployeeEntity
                            {
                                EmployeeCode = employeesMaster.EmployeeCode.ToString(),
                                EmpName = employeesMaster.EmployeeName.ToString(),
                                PositionText = employeesMaster.Position,
                                WorkLevel = "WorkLevel",
                                EmployeeSubGroup = employeesMaster.EmployeeSubGroup.ToString(),//need to take name
                                RegionName = employeesMaster.HRRegion.ToString(),
                                CountryName = employeesMaster.CompanyName,
                                Area = employeesMaster.Area.ToString(),
                                SubArea = employeesMaster.SubArea.ToString(),
                                HRBusinessPatner = employeesMaster.HRBusinessPartnerName,
                                AppraiserName = "AppraiserName",
                                ReviewerName = "ReviewerName",
                                JoiningDate = "JoiningDate",
                                ConfirmationDate = employeesMaster.ConfirmationDate.ToString(),
                                ConfirmationDueDate = employeesMaster.ConfirmationDueDate.ToString(),
                                EmployeeStatus = employeesMaster.Status == null ? "In-active" : (employeesMaster.Status == true ? "Active" : "In-active"),
                                EligibilityH1 = "EligibilityH1",
                                EligibilityH2 = "EligibilityH2",
                                OldEmployeeCode = "OldEmployeeCode",
                            }).ToList();
                }
            }
        }

        public List<EmployeeEntity> GetStatusreport()
        {
            using (SPWeb web = SPContext.Current.Site.OpenWeb())
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    return (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                            //where app.PerformanceCycle.ToString() == performanceCycle.ToString() && appPhases.AppraisalPhase == "H1"
                            select new EmployeeEntity
                            {
                                EmployeeCode = employeesMaster.EmployeeCode.ToString(),
                                EmpName = employeesMaster.EmployeeName.ToString(),
                                PositionText = employeesMaster.Position,
                                WorkLevel = "WorkLevel",
                                EmployeeSubGroup = employeesMaster.EmployeeSubGroup.ToString(),//need to take name
                                RegionName = employeesMaster.HRRegion.ToString(),
                                CountryName = employeesMaster.CompanyName,
                                Area = employeesMaster.Area.ToString(),
                                SubArea = employeesMaster.SubArea.ToString(),
                                HRBusinessPatner = employeesMaster.HRBusinessPartnerName,
                                AppraiserName = "AppraiserName",
                                ReviewerName = "ReviewerName",
                                JoiningDate = "JoiningDate",
                                ConfirmationDate = employeesMaster.ConfirmationDate.ToString(),
                                ConfirmationDueDate = employeesMaster.ConfirmationDueDate.ToString(),
                                EmployeeStatus = employeesMaster.Status == null ? "In-active" : (employeesMaster.Status == true ? "Active" : "In-active"),
                                AppraisalCurrentState = "AppraisalCurrentState",
                                OldEmployeeCode = "OldEmployeeCode",
                            }).ToList();
                }
            }
        }

    }
}
