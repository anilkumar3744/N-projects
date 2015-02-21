using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;
using Microsoft.SharePoint.Client.Services;
using System.ServiceModel.Activation;
using System.Data;
namespace VFS.PMS.ReportingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [AspNetCompatibilityRequirementsAttribute(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ReportsHelperService : IReportsHelperService
    {
        string HRRegion = string.Empty, DeptHead = string.Empty, HR = string.Empty, Appraiser = string.Empty;
        bool isHR = false, isReviewer = false, isAppraiser = false, isTMT = false, isReviewBoard = false, isRegionalHR = false;
        //string url = "http://shptraining:12345";
        string url = SPContext.Current.Web.Url;
        SPListItem spItem = null;
        public List<RegionEntity> GetAllRegions()
        {
            List<RegionEntity> newList = new List<RegionEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            newList = (from regions in PMSDataContext.Regions.AsEnumerable()
                                       orderby regions.RegionName
                                       select new RegionEntity
                                       {
                                           RegionName = regions.RegionName,
                                           RegionCode = regions.Id.ToString(),
                                       }).ToList();

                        }
                    }
                }
            });
            return newList;
        }

        public RegionResponse GetRegions(RegionRequest regRequest)
        {
            RegionResponse RegResponse = new RegionResponse();
            List<RegionEntity> newList = new List<RegionEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            newList = (from regions in PMSDataContext.Regions.AsEnumerable()
                                       orderby regions.RegionName
                                       select new RegionEntity
                                       {
                                           RegionName = regions.RegionName,
                                           RegionCode = regions.Id.ToString(),
                                       }).ToList();

                        }
                    }
                }
                RegResponse.DataItems = newList;

            });
            return RegResponse;
        }

        public List<OrganizationUnitEntity> GetOrganizationUnits()
        {
            List<OrganizationUnitEntity> OUList = new List<OrganizationUnitEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            var OUListG = (from organizationUnits in PMSDataContext.OrganizationUnits.AsEnumerable()
                                           orderby organizationUnits.OrganizationUnitLongText
                                           select organizationUnits.OrganizationUnitLongText).Distinct().ToList();
                            //OUList = OUListG.ToList<OrganizationUnitEntity>();
                            foreach (var ou in OUListG)
                            {
                                OrganizationUnitEntity o = new OrganizationUnitEntity();
                                o.OrganizationUnitLongName = ou;
                                OUList.Add(o);
                            }
                        }
                    }
                }
            });
            return OUList;
        }

        public CompanyResponse GetCompaniesByRegionId(CompanyRequest request)
        {
            List<CompanyEntity> companyList = new List<CompanyEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            companyList = (from companies in PMSDataContext.Companies.AsEnumerable()
                                           orderby companies.CompanyName
                                           where request.RegionIds.Contains(companies.Region != null ? companies.Region.RegionName : "")
                                           select new CompanyEntity
                                           {
                                               CompanyName = companies.CompanyName,
                                               CompanyCode = companies.CompanyCode.ToString(),
                                           }).ToList();
                        }
                    }
                }
            });
            CompanyResponse objCompanyResponse = new CompanyResponse();
            objCompanyResponse.DataItems = companyList;
            return objCompanyResponse;
        }

        public DataTable FilterDatatable(DataTable dt, string filter, string filterIn)
        {
            DataTable tblReturnTable = dt.Clone();
            string strExpression = string.Empty;

            if (filter.Trim() != string.Empty)
                strExpression = filterIn + "in (" + filter + ")";

            DataRow[] dtrMatchResult = dt.Select(strExpression);
            foreach (DataRow dtrCurrentRow in dtrMatchResult)
                tblReturnTable.ImportRow(dtrCurrentRow);

            return tblReturnTable;
        }


        //public EligibleEmployeesResponse EligibleEmployees(EligibleEmployeesRequest request)
        //{
        //    List<ParametersEntity> ParametersEntityList = new List<ParametersEntity>();
        //    //SPContext.cu
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate()
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    string H1EndDate, H2EndDate, H1Eligible;

        //                    //request.
        //                    string[] strRegionIds = request.RegionIds.Split('|');
        //                    string[] strCompanyIds = request.CompanyIds.Split('|');
        //                    string[] strOrganizationUnitIds = request.OrganizationUnitIds.Split('|');
        //                    string empEligibilityH1 = string.Empty;
        //                    string empEligibilityH2 = string.Empty;
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    SPList performanceCyclePhasesList = web.Lists["Performance Cycle Phases"];
        //                    SPQuery performanceCyclePhasesQuery = new SPQuery();
        //                    DataTable dtPerformancePhases = performanceCyclePhasesList.GetItems(performanceCyclePhasesQuery).GetDataTable();
        //                    SPList performanceCyclesList = web.Lists["Performance Cycles"];
        //                    SPQuery performanceCyclesQuery = new SPQuery();
        //                    DataTable dtperformanceCycles = performanceCyclesList.GetItems(performanceCyclesQuery).GetDataTable();
        //                    ParametersEntityList = (from employeesMaster in dtEmpMasters.AsEnumerable()
        //                                            where strRegionIds.Contains(employeesMaster.Field<string>("HRRegion"))
        //                                                && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName"))
        //                                                && strOrganizationUnitIds.Contains(employeesMaster.Field<string>("OrganizationUnit"))
        //                                                && employeesMaster.Field<string>("EmployeeGroup").StartsWith("A")
        //                                                && employeesMaster.Field<string>("EmployeeStatus") == "Active"
        //                                            orderby employeesMaster.Field<double>("EmployeeCode")
        //                                            select new ParametersEntity
        //                                            {
        //                                                EmployeeCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode")),
        //                                                EmpName = Convert.ToString(employeesMaster.Field<string>("EmployeeName")),
        //                                                PositionText = Convert.ToString(employeesMaster.Field<string>("Position")),
        //                                                EmployeeGroupName = Convert.ToString(employeesMaster.Field<string>("EmployeeGroup")),
        //                                                EmployeeSubGroup = Convert.ToString(employeesMaster.Field<string>("EmployeeSubGroup")),
        //                                                RegionName = Convert.ToString(employeesMaster.Field<string>("HRRegion")),
        //                                                CountryName = Convert.ToString(employeesMaster.Field<string>("CompanyName")),
        //                                                Area = Convert.ToString(employeesMaster.Field<string>("Area")),
        //                                                SubArea = Convert.ToString(employeesMaster.Field<string>("SubArea")),
        //                                                HRBusinessPatner = Convert.ToString(employeesMaster.Field<string>("HRBusinessPartnerName")),
        //                                                AppraiserName = Convert.ToString(employeesMaster.Field<string>("DepartmentHeadName")),
        //                                                ReviewerName = Convert.ToString(employeesMaster.Field<string>("ReportingManagerName")),
        //                                                HRBusinessPatnerCode = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                                AppraiserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
        //                                                ReviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
        //                                                JoiningDate = employeesMaster.Field<DateTime?>("HireDate") == null ? "" : employeesMaster.Field<DateTime>("HireDate").ToString("dd-MMM-yyyy"),
        //                                                ConfirmationDate = employeesMaster.Field<DateTime?>("ConfirmationDate") == null ? "" : employeesMaster.Field<DateTime>("ConfirmationDate").ToString("dd-MMM-yyyy"),
        //                                                ConfirmationDueDate = employeesMaster.Field<DateTime?>("ConfirmationDueDate") == null ? "" : employeesMaster.Field<DateTime>("ConfirmationDueDate").ToString("dd-MMM-yyyy"),
        //                                                EmployeeStatus = employeesMaster.Field<string>("EmployeeStatus") == null ? "Active" : (employeesMaster.Field<string>("EmployeeStatus").ToString()),
        //                                                Dummy = empEligibilityH1 = Convert.ToDateTime(employeesMaster.Field<DateTime?>("HireDate")).Date.AddMonths(3).ToString(),
        //                                                H1PhaseEndDate = H1EndDate = (from performanceCyclePhases in PMSDataContext.PerformanceCyclePhases.AsEnumerable()
        //                                                                              where performanceCyclePhases.PerformanceCycle.PerformanceCycle.ToString() == (from performanceCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select performanceCycles.PerformanceCycle.ToString()).ToList().Max()
        //                                                                                 && performanceCyclePhases.Phase.Phase.ToString() == "H1"
        //                                                                              select Convert.ToDateTime(performanceCyclePhases.EndDate).Date).FirstOrDefault().ToString(),
        //                                                EligibilityH1 = H1Eligible = employeesMaster.Field<DateTime?>("ConfirmationDate") != null ? "Yes" : employeesMaster.Field<string>("EmployeeSubGroup").ToString().StartsWith("P") ? employeesMaster.Field<DateTime?>("ConfirmationDueDate") == null ? "No" : Convert.ToDateTime(employeesMaster.Field<DateTime?>("ConfirmationDueDate")) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No" : Convert.ToDateTime(empEligibilityH1) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No",

        //                                                Dummy1 = empEligibilityH2 = Convert.ToDateTime(employeesMaster.Field<DateTime?>("HireDate")).Date.AddMonths(3).ToString(),
        //                                                H2PhaseEndDate = H2EndDate = (from performanceCyclePhases in PMSDataContext.PerformanceCyclePhases.AsEnumerable()
        //                                                                              where performanceCyclePhases.PerformanceCycle.PerformanceCycle.ToString() == (from performanceCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select performanceCycles.PerformanceCycle.ToString()).ToList().Max()
        //                                                                                 && performanceCyclePhases.Phase.Phase.ToString() == "H2"
        //                                                                              select Convert.ToDateTime(performanceCyclePhases.EndDate).Date).FirstOrDefault().ToString(),
        //                                                EligibilityH2 = H1Eligible == "Yes" ? "Yes" : employeesMaster.Field<DateTime?>("ConfirmationDate") != null ? "Yes" : employeesMaster.Field<string>("EmployeeSubGroup").ToString().StartsWith("P") ? employeesMaster.Field<DateTime?>("ConfirmationDueDate") == null ? "No" : (Convert.ToDateTime(employeesMaster.Field<DateTime?>("ConfirmationDueDate")) <= Convert.ToDateTime(H2EndDate) && Convert.ToDateTime(employeesMaster.Field<DateTime?>("ConfirmationDueDate")) >= Convert.ToDateTime(H1EndDate)) ? "Yes" : "No" : (Convert.ToDateTime(empEligibilityH2) <= Convert.ToDateTime(H2EndDate) && Convert.ToDateTime(empEligibilityH2) >= Convert.ToDateTime(H1EndDate)) ? "Yes" : "No",
        //                                                OldEmployeeCode = Convert.ToString(employeesMaster.Field<double?>("OldEmployeeCode")),
        //                                            }).ToList();
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    EligibleEmployeesResponse objEligibleEmployeesResponse = new EligibleEmployeesResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        ParametersEntityList = ParametersEntityList.Where(p => p.RegionName.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        ParametersEntityList = ParametersEntityList.Where(p => p.HRBusinessPatnerCode.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }

        //    objEligibleEmployeesResponse.DataItems = ParametersEntityList;
        //    return objEligibleEmployeesResponse;
        //}

        public EligibleEmployeesResponse EligibleEmployees(EligibleEmployeesRequest request)
        {
            List<ParametersEntity> ParametersEntityList = new List<ParametersEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);

                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            string H1EndDate, H2EndDate, H1Eligible;
                            string[] strRegionIds = request.RegionIds.Split('|');
                            string[] strCompanyIds = request.CompanyIds.Split('|');
                            string[] strOrganizationUnitIds = request.OrganizationUnitIds.Split('|');
                            string empEligibilityH1 = string.Empty;
                            string empEligibilityH2 = string.Empty;
                            double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                            H1EndDate = (from performanceCyclePhases in PMSDataContext.PerformanceCyclePhases.AsEnumerable()
                                         where performanceCyclePhases.PerformanceCycle.PerformanceCycle.ToString() == Convert.ToString(cycle)
                                             && performanceCyclePhases.Phase.Phase.ToString() == "H1"
                                         select Convert.ToDateTime(performanceCyclePhases.EndDate).Date).FirstOrDefault().ToString();
                            H2EndDate = (from performanceCyclePhases in PMSDataContext.PerformanceCyclePhases.AsEnumerable()
                                         where performanceCyclePhases.PerformanceCycle.PerformanceCycle.ToString() == Convert.ToString(cycle)
                                             && performanceCyclePhases.Phase.Phase.ToString() == "H2"
                                         select Convert.ToDateTime(performanceCyclePhases.EndDate).Date).FirstOrDefault().ToString();
                            ParametersEntityList = (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                                    where strRegionIds.Contains(employeesMaster.HRRegion == null ? "" : employeesMaster.HRRegion.RegionName.ToString())
                                                        && strCompanyIds.Contains(employeesMaster.CompanyName == null ? "" : employeesMaster.CompanyName.ToString())
                                                        && strOrganizationUnitIds.Contains(employeesMaster.OrganizationUnit)
                                                        && employeesMaster.EmployeeGroup.EmployeeGroupCode.StartsWith("A")
                                                        && employeesMaster.EmployeeStatus.ToString() == "Active"
                                                    && ((isTMT || isReviewBoard) ? employeesMaster.EmployeeStatus.ToString() == "Active" :
                                       (isRegionalHR ? Convert.ToString(employeesMaster.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                                        //(employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                        (employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                                    orderby employeesMaster.EmployeeCode
                                                    select new ParametersEntity
                                                    {
                                                        EmployeeCode = Convert.ToString(employeesMaster.EmployeeCode),
                                                        EmpName = Convert.ToString(employeesMaster.EmployeeName),
                                                        PositionText = Convert.ToString(employeesMaster.Position),
                                                        EmployeeGroupName = Convert.ToString(employeesMaster.EmployeeGroup.EmployeeGroupCode),
                                                        EmployeeSubGroup = Convert.ToString(employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode),
                                                        RegionName = Convert.ToString(employeesMaster.HRRegion.RegionName),
                                                        CountryName = Convert.ToString(employeesMaster.CompanyName),
                                                        Area = Convert.ToString(employeesMaster.Area),
                                                        SubArea = Convert.ToString(employeesMaster.SubArea),
                                                        HRBusinessPatner = Convert.ToString(employeesMaster.HRBusinessPartnerName),
                                                        AppraiserName = Convert.ToString(employeesMaster.ReportingManagerName),
                                                        ReviewerName = Convert.ToString(employeesMaster.DepartmentHeadName),
                                                        HRBusinessPatnerCode = Convert.ToString(employeesMaster.HRBusinessPartnerEmployeeCode),
                                                        AppraiserCode = Convert.ToString(employeesMaster.ReportingManagerEmployeeCode),
                                                        ReviewerCode = Convert.ToString(employeesMaster.DepartmentHeadEmployeeCode),
                                                        JoiningDate = employeesMaster.HireDate == null ? "" : Convert.ToDateTime(employeesMaster.HireDate).ToString("dd-MMM-yyyy"),
                                                        ConfirmationDate = employeesMaster.ConfirmationDate == null ? "" : Convert.ToDateTime(employeesMaster.ConfirmationDate).ToString("dd-MMM-yyyy"),
                                                        ConfirmationDueDate = employeesMaster.ConfirmationDueDate == null ? "" : Convert.ToDateTime(employeesMaster.ConfirmationDueDate).ToString("dd-MMM-yyyy"),
                                                        EmployeeStatus = employeesMaster.EmployeeStatus == null ? "Active" : employeesMaster.EmployeeStatus.ToString(),
                                                        Dummy = empEligibilityH1 = Convert.ToDateTime(employeesMaster.HireDate).Date.AddMonths(3).ToString(),
                                                        H1PhaseEndDate = H1EndDate,
                                                        ////EligibilityH1 = H1Eligible = employeesMaster.ConfirmationDate != null ? "Yes" : employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode.ToString().StartsWith("P") ? employeesMaster.ConfirmationDueDate == null ? "No" : Convert.ToDateTime(employeesMaster.ConfirmationDueDate) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No" : Convert.ToDateTime(empEligibilityH1) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No",////commented by jhansi on 9/12/2013
                                                        EligibilityH1 = H1Eligible = employeesMaster.ConfirmationDate != null ? employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode.ToString().StartsWith("P") ? Convert.ToDateTime(employeesMaster.ConfirmationDate) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No" : Convert.ToDateTime(empEligibilityH1) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No"
                                                        : employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode.ToString().StartsWith("P") ? employeesMaster.ConfirmationDueDate == null ? "No" : Convert.ToDateTime(employeesMaster.ConfirmationDueDate) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No" : Convert.ToDateTime(empEligibilityH1) <= Convert.ToDateTime(H1EndDate) ? "Yes" : "No",
                                                        Dummy1 = empEligibilityH2 = Convert.ToDateTime(employeesMaster.HireDate).Date.AddMonths(3).ToString(),
                                                        H2PhaseEndDate = H2EndDate,
                                                        ////EligibilityH2 = H1Eligible == "Yes" ? "Yes" : employeesMaster.ConfirmationDate != null ? "Yes" : employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode.ToString().StartsWith("P") ? employeesMaster.ConfirmationDueDate == null ? "No" : (Convert.ToDateTime(employeesMaster.ConfirmationDueDate) <= Convert.ToDateTime(H2EndDate) && Convert.ToDateTime(employeesMaster.ConfirmationDueDate) >= Convert.ToDateTime(H1EndDate)) ? "Yes" : "No" : (Convert.ToDateTime(empEligibilityH2) <= Convert.ToDateTime(H2EndDate) && Convert.ToDateTime(empEligibilityH2) >= Convert.ToDateTime(H1EndDate)) ? "Yes" : "No",////commented by jhansi on 9/12/2013
                                                        EligibilityH2 = H1Eligible == "Yes" ? "Yes"
                                                        : employeesMaster.ConfirmationDate != null ? employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode.ToString().StartsWith("P") ? Convert.ToDateTime(employeesMaster.ConfirmationDate) <= Convert.ToDateTime(H2EndDate) ? "Yes" : "No" : Convert.ToDateTime(empEligibilityH2) <= Convert.ToDateTime(H2EndDate) ? "Yes" : "No"
                                                        : employeesMaster.EmployeeSubGroup.EmployeeSubGroupCode.ToString().StartsWith("P") ? employeesMaster.ConfirmationDueDate == null ? "No" : (Convert.ToDateTime(employeesMaster.ConfirmationDueDate) <= Convert.ToDateTime(H2EndDate) && Convert.ToDateTime(employeesMaster.ConfirmationDueDate) >= Convert.ToDateTime(H1EndDate)) ? "Yes" : "No" : (Convert.ToDateTime(empEligibilityH2) <= Convert.ToDateTime(H2EndDate) && Convert.ToDateTime(empEligibilityH2) >= Convert.ToDateTime(H1EndDate)) ? "Yes" : "No",
                                                        OldEmployeeCode = Convert.ToString(employeesMaster.OldEmployeeCode),
                                                    }).ToList();
                        }

                    }
                }
            });
            EligibleEmployeesResponse objEligibleEmployeesResponse = new EligibleEmployeesResponse();
            objEligibleEmployeesResponse.DataItems = ParametersEntityList;
            return objEligibleEmployeesResponse;
        }

        //public StatusResponse GetStatusReport(StatusRequest request)
        //{
        //    List<ParametersEntity> ParametersEntityList = new List<ParametersEntity>();
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    string[] strRegionIds = request.Region.Split('|');
        //                    string[] strCompanyIds = request.Country.Split('|');
        //                    string[] strOrganizationUnitIds = request.OrganizationUnit.Split('|');
        //                    double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
        //                    SPList appraisalList = web.Lists["Appraisals"];
        //                    SPQuery ospAppraisalQuery = new SPQuery();
        //                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    ParametersEntityList = (from empMasters in dtEmpMasters.AsEnumerable().AsEnumerable()
        //                                            join appraisal in dtAppraisal.AsEnumerable() on empMasters.Field<double>("EmployeeCode").ToString() equals appraisal.Field<string>("appEmployeeCode").ToString()
        //                                            where strRegionIds.Contains(empMasters.Field<string>("HRRegion"))
        //                                            && strOrganizationUnitIds.Contains(empMasters.Field<string>("OrganizationUnit"))
        //                                            && strCompanyIds.Contains(empMasters.Field<string>("CompanyName"))
        //                                            && appraisal.Field<double>("appPerformanceCycle").ToString() == cycle.ToString()
        //                                            orderby empMasters.Field<double>("EmployeeCode")
        //                                            select new ParametersEntity
        //                                            {
        //                                                EmployeeCode = Convert.ToString(empMasters.Field<double>("EmployeeCode")),
        //                                                EmpName = Convert.ToString(empMasters.Field<string>("EmployeeName")),
        //                                                PositionText = Convert.ToString(empMasters.Field<string>("Position")),
        //                                                EmployeeGroupName = Convert.ToString(empMasters.Field<string>("EmployeeGroup")),
        //                                                EmployeeSubGroup = Convert.ToString(empMasters.Field<string>("EmployeeSubGroup")),
        //                                                CountryName = Convert.ToString(empMasters.Field<string>("CompanyName")),
        //                                                RegionName = Convert.ToString(empMasters.Field<string>("HRRegion")),
        //                                                Area = Convert.ToString(empMasters.Field<string>("Area")),
        //                                                SubArea = Convert.ToString(empMasters.Field<string>("SubArea")),
        //                                                HRBusinessPatner = Convert.ToString(empMasters.Field<string>("HRBusinessPartnerName")),
        //                                                AppraiserName = Convert.ToString(empMasters.Field<string>("DepartmentHeadName")),
        //                                                ReviewerName = Convert.ToString(empMasters.Field<string>("ReportingManagerName")),
        //                                                HRBusinessPatnerCode = Convert.ToString(empMasters.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                                AppraiserCode = Convert.ToString(empMasters.Field<double>("ReportingManagerEmployeeCode")),
        //                                                ReviewerCode = Convert.ToString(empMasters.Field<double>("DepartmentHeadEmployeeCode")),
        //                                                JoiningDate = empMasters.Field<DateTime?>("HireDate") == null ? "" : empMasters.Field<DateTime>("HireDate").ToString("dd-MMM-yyyy"),
        //                                                ConfirmationDate = empMasters.Field<DateTime?>("ConfirmationDate") == null ? "" : empMasters.Field<DateTime>("ConfirmationDate").ToString("dd-MMM-yyyy"),
        //                                                ConfirmationDueDate = empMasters.Field<DateTime?>("ConfirmationDueDate") == null ? "" : empMasters.Field<DateTime>("ConfirmationDueDate").ToString("dd-MMM-yyyy"),
        //                                                EmployeeStatus = empMasters.Field<string>("EmployeeStatus") == null ? "In-active" : (empMasters.Field<string>("EmployeeStatus").ToString()),
        //                                                AppraisalCurrentState = appraisal.Field<string>("appAppraisalStatus").ToString(),
        //                                                OldEmployeeCode = Convert.ToString(empMasters.Field<double?>("OldEmployeeCode")),
        //                                            }).ToList();
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    StatusResponse Response = new StatusResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        ParametersEntityList = ParametersEntityList.Where(p => p.RegionName.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        ParametersEntityList = ParametersEntityList.Where(p => p.HRBusinessPatnerCode.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }

        //    Response.DataItems = ParametersEntityList;
        //    return Response;
        //}

        public StatusResponse GetStatusReport(StatusRequest request)
        {
            List<ParametersEntity> ParametersEntityList = new List<ParametersEntity>();
            double? cycle = 0;
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            string[] strRegionIds = request.Region.Split('|');
                            string[] strCompanyIds = request.Country.Split('|');
                            string[] strOrganizationUnitIds = request.OrganizationUnit.Split('|');
                            cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                            ParametersEntityList = (from empMasters in PMSDataContext.EmployeesMaster.AsEnumerable().AsEnumerable()
                                                    join appraisal in PMSDataContext.Appraisals.AsEnumerable() on empMasters.EmployeeCode.ToString() equals appraisal.EmployeeCode.ToString()
                                                    where strRegionIds.Contains(empMasters.HRRegion == null ? "" : empMasters.HRRegion.RegionName.ToString())
                                                    && strOrganizationUnitIds.Contains(empMasters.OrganizationUnit)
                                                    && strCompanyIds.Contains(empMasters.CompanyName == null ? "" : empMasters.CompanyName.ToString())
                                                    && Convert.ToInt32(appraisal.PerformanceCycle.ToString()) == Convert.ToInt32(cycle.ToString())
                                                    && ((isTMT || isReviewBoard) ? Convert.ToInt32(appraisal.PerformanceCycle.ToString()) == Convert.ToInt32(cycle.ToString()) :
                                       (isRegionalHR ? Convert.ToString(empMasters.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                        (empMasters.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || empMasters.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || empMasters.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                                    orderby empMasters.EmployeeCode
                                                    select new ParametersEntity
                                                    {
                                                        EmployeeCode = Convert.ToString(empMasters.EmployeeCode),
                                                        EmpName = Convert.ToString(empMasters.EmployeeName),
                                                        PositionText = Convert.ToString(empMasters.Position),
                                                        EmployeeGroupName = Convert.ToString(empMasters.EmployeeGroup.EmployeeGroupCode),
                                                        EmployeeSubGroup = Convert.ToString(empMasters.EmployeeSubGroup.EmployeeSubGroupCode),
                                                        CountryName = Convert.ToString(empMasters.CompanyName),
                                                        RegionName = Convert.ToString(empMasters.HRRegion.RegionName),
                                                        Area = Convert.ToString(empMasters.Area),
                                                        SubArea = Convert.ToString(empMasters.SubArea),
                                                        HRBusinessPatner = Convert.ToString(empMasters.HRBusinessPartnerName),
                                                        AppraiserName = Convert.ToString(empMasters.ReportingManagerName),
                                                        ReviewerName = Convert.ToString(empMasters.DepartmentHeadName),
                                                        HRBusinessPatnerCode = Convert.ToString(empMasters.HRBusinessPartnerEmployeeCode),
                                                        AppraiserCode = Convert.ToString(empMasters.ReportingManagerEmployeeCode),
                                                        ReviewerCode = Convert.ToString(empMasters.DepartmentHeadEmployeeCode),
                                                        JoiningDate = empMasters.HireDate == null ? "" : Convert.ToDateTime(empMasters.HireDate).ToString("dd-MMM-yyyy"),
                                                        ConfirmationDate = empMasters.ConfirmationDate == null ? "" : Convert.ToDateTime(empMasters.ConfirmationDate).ToString("dd-MMM-yyyy"),
                                                        ConfirmationDueDate = empMasters.ConfirmationDueDate == null ? "" : Convert.ToDateTime(empMasters.ConfirmationDueDate).ToString("dd-MMM-yyyy"),
                                                        EmployeeStatus = empMasters.EmployeeStatus == null ? "Active" : empMasters.EmployeeStatus.ToString(),
                                                        AppraisalCurrentState = appraisal.AppraisalStatus.ToString(),
                                                        OldEmployeeCode = Convert.ToString(empMasters.OldEmployeeCode),
                                                    }).Distinct().ToList();
                        }
                    }
                }
            });
            StatusResponse Response = new StatusResponse();
            Response.DataItems = ParametersEntityList;
            return Response;
        }

        //public EmployeeWiseResponse GetEmployeeWiseReport(EmployeeWiseRequest employeeRequest)
        //{
        //    List<ParametersEntity> ParametersEntityList = new List<ParametersEntity>();

        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {

        //                    SPList appraisalList = web.Lists["Appraisals"];
        //                    SPQuery ospAppraisalQuery = new SPQuery();
        //                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    SPList appPhasesList = web.Lists["Appraisal Phases"];
        //                    SPQuery appPhasesQuery = new SPQuery();
        //                    DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
        //                    string[] strRegionIds = employeeRequest.Region.Split('|');
        //                    string[] strOrganizationUnitIds = employeeRequest.Organization.Split('|');
        //                    string[] strCountryIds = employeeRequest.Country.Split('|');
        //                    ParametersEntityList = (from empMasters in dtEmpMasters.AsEnumerable().AsEnumerable()
        //                                            join appraisal in dtAppraisal.AsEnumerable() on empMasters.Field<double>("EmployeeCode").ToString() equals appraisal.Field<string>("appEmployeeCode").ToString()
        //                                            where strRegionIds.Contains(empMasters.Field<string>("HRRegion"))
        //                                            && strOrganizationUnitIds.Contains(empMasters.Field<string>("OrganizationUnit"))
        //                                            && strCountryIds.Contains(empMasters.Field<string>("CompanyName"))
        //                                            && appraisal.Field<double>("appPerformanceCycle").ToString() == employeeRequest.PerformanceCycle.ToString()
        //                                            orderby empMasters.Field<double>("EmployeeCode")
        //                                            select new ParametersEntity
        //                                            {
        //                                                EmployeeCode = Convert.ToString(empMasters.Field<double>("EmployeeCode")),
        //                                                EmpName = Convert.ToString(empMasters.Field<string>("EmployeeName")),
        //                                                PositionText = Convert.ToString(empMasters.Field<string>("Position")),
        //                                                WorkLevel = Convert.ToString(empMasters.Field<string>("EmployeeGroup")),
        //                                                EmployeeSubGroup = Convert.ToString(empMasters.Field<string>("EmployeeSubGroup")),
        //                                                RegionName = Convert.ToString(empMasters.Field<string>("HRRegion")),
        //                                                CountryName = Convert.ToString(empMasters.Field<string>("CompanyName")),
        //                                                Area = Convert.ToString(empMasters.Field<string>("Area")),
        //                                                SubArea = Convert.ToString(empMasters.Field<string>("SubArea")),
        //                                                HRBusinessPatner = Convert.ToString(empMasters.Field<string>("HRBusinessPartnerName")),
        //                                                HRBusinessPatnerCode = Convert.ToString(empMasters.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                                AppraiserName = Convert.ToString(empMasters.Field<string>("ReportingManagerName")),
        //                                                AppraiserCode = Convert.ToString(empMasters.Field<double>("ReportingManagerEmployeeCode")),
        //                                                ReviewerName = Convert.ToString(empMasters.Field<string>("DepartmentHeadName")),
        //                                                ReviewerCode = Convert.ToString(empMasters.Field<double>("DepartmentHeadEmployeeCode")),
        //                                                JoiningDate = empMasters.Field<DateTime?>("HireDate") == null ? "" : empMasters.Field<DateTime>("HireDate").ToString("dd-MMM-yyyy"),
        //                                                ConfirmationDate = empMasters.Field<DateTime?>("ConfirmationDate") == null ? "" : empMasters.Field<DateTime>("ConfirmationDate").ToString("dd-MMM-yyyy"),
        //                                                ConfirmationDueDate = empMasters.Field<DateTime?>("ConfirmationDueDate") == null ? "" : empMasters.Field<DateTime>("ConfirmationDueDate").ToString("dd-MMM-yyyy"),
        //                                                EmployeeStatus = empMasters.Field<string>("EmployeeStatus") == null ? "In-active" : (empMasters.Field<string>("EmployeeStatus").ToString()),
        //                                                AppraisalCurrentState = appraisal.Field<string>("appAppraisalStatus").ToString(),
        //                                                OldEmployeeCode = Convert.ToString(empMasters.Field<double?>("OldEmployeeCode")),
        //                                                FinalScore = (from appraisals in dtAppraisal.AsEnumerable()
        //                                                              where appraisals.Field<string>("appEmployeeCode").ToString() == empMasters.Field<double>("EmployeeCode").ToString()
        //                                                              && appraisals.Field<double>("appPerformanceCycle").ToString() == employeeRequest.PerformanceCycle.ToString()
        //                                                              select appraisals.Field<double?>("appFinalScore").ToString()).FirstOrDefault(),
        //                                                FinalRating = (from appraisals in dtAppraisal.AsEnumerable()
        //                                                               where appraisals.Field<string>("appEmployeeCode").ToString() == empMasters.Field<double>("EmployeeCode").ToString()
        //                                                               && appraisals.Field<double>("appPerformanceCycle").ToString() == employeeRequest.PerformanceCycle.ToString()
        //                                                               select appraisals.Field<double?>("appFinalRating").ToString()).FirstOrDefault(),
        //                                                H1Score = (from appPhases in dtAppPhases.AsEnumerable()
        //                                                           join app in dtAppraisal.AsEnumerable() on appPhases.Field<double>("aphAppraisalId").ToString() equals app.Field<int>("ID").ToString()
        //                                                           where appPhases.Field<string>("aphAppraisalPhase") == "H1" && app.Field<int>("ID").ToString() == appraisal.Field<int>("ID").ToString()
        //                                                           select appPhases.Field<double?>("aphScore").ToString()).FirstOrDefault(),
        //                                                H2Score = (from appPhases in dtAppPhases.AsEnumerable()
        //                                                           join app in dtAppraisal.AsEnumerable() on appPhases.Field<double>("aphAppraisalId").ToString() equals app.Field<int>("ID").ToString()
        //                                                           where appPhases.Field<string>("aphAppraisalPhase") == "H2" && app.Field<int>("ID").ToString() == appraisal.Field<int>("ID").ToString()
        //                                                           select appPhases.Field<double?>("aphScore").ToString()).FirstOrDefault(),
        //                                            }).ToList();
        //                }

        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    EmployeeWiseResponse EmployeesResponse = new EmployeeWiseResponse();

        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        ParametersEntityList = ParametersEntityList.Where(p => p.RegionName.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        ParametersEntityList = ParametersEntityList.Where(p => p.HRBusinessPatnerCode.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }
        //    EmployeesResponse.DataItems = ParametersEntityList;
        //    return EmployeesResponse;
        //}

        public EmployeeWiseResponse GetEmployeeWiseReport(EmployeeWiseRequest employeeRequest)
        {
            List<ParametersEntity> ParametersEntityList = new List<ParametersEntity>();

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                            string[] strRegionIds = employeeRequest.Region.Split('|');
                            string[] strOrganizationUnitIds = employeeRequest.Organization.Split('|');
                            string[] strCountryIds = employeeRequest.Country.Split('|');
                            ParametersEntityList = (from empMasters in PMSDataContext.EmployeesMaster.AsEnumerable()
                                                    join appraisal in PMSDataContext.Appraisals.AsEnumerable() on Convert.ToString((double)empMasters.EmployeeCode) equals appraisal.EmployeeCode
                                                    where strRegionIds.Contains(empMasters.HRRegion == null ? "" : empMasters.HRRegion.RegionName.ToString())
                                                    && strOrganizationUnitIds.Contains(empMasters.OrganizationUnit)
                                                    && strCountryIds.Contains(empMasters.CompanyName == null ? "" : empMasters.CompanyName.ToString())
                                                    && Convert.ToInt32(appraisal.PerformanceCycle.ToString()) == Convert.ToInt32(cycle.ToString())
                                                    && ((isTMT || isReviewBoard) ? appraisal.PerformanceCycle.ToString() == employeeRequest.PerformanceCycle.ToString() :
                                       (isRegionalHR ? Convert.ToString(empMasters.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                        (empMasters.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || empMasters.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || empMasters.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                                    orderby empMasters.EmployeeCode
                                                    select new ParametersEntity
                                                    {
                                                        EmployeeCode = Convert.ToString(empMasters.EmployeeCode),
                                                        EmpName = Convert.ToString(empMasters.EmployeeName),
                                                        PositionText = Convert.ToString(empMasters.Position),
                                                        WorkLevel = Convert.ToString(empMasters.EmployeeGroup.EmployeeGroupCode),
                                                        EmployeeSubGroup = Convert.ToString(empMasters.EmployeeSubGroup.EmployeeSubGroupCode),
                                                        RegionName = Convert.ToString(empMasters.HRRegion.RegionName),
                                                        CountryName = Convert.ToString(empMasters.CompanyName),
                                                        Area = Convert.ToString(empMasters.Area),
                                                        SubArea = Convert.ToString(empMasters.SubArea),
                                                        HRBusinessPatner = Convert.ToString(empMasters.HRBusinessPartnerName),
                                                        HRBusinessPatnerCode = Convert.ToString(empMasters.HRBusinessPartnerEmployeeCode),
                                                        AppraiserName = Convert.ToString(empMasters.ReportingManagerName),
                                                        AppraiserCode = Convert.ToString(empMasters.ReportingManagerEmployeeCode),
                                                        ReviewerName = Convert.ToString(empMasters.DepartmentHeadName),
                                                        ReviewerCode = Convert.ToString(empMasters.DepartmentHeadEmployeeCode),
                                                        JoiningDate = empMasters.HireDate == null ? "" : Convert.ToDateTime(empMasters.HireDate).ToString("dd-MMM-yyyy"),
                                                        ConfirmationDate = empMasters.ConfirmationDate == null ? "" : Convert.ToDateTime(empMasters.ConfirmationDate).ToString("dd-MMM-yyyy"),
                                                        ConfirmationDueDate = empMasters.ConfirmationDueDate == null ? "" : Convert.ToDateTime(empMasters.ConfirmationDueDate).ToString("dd-MMM-yyyy"),
                                                        EmployeeStatus = empMasters.EmployeeStatus == null ? "Active" : empMasters.EmployeeStatus.ToString(),
                                                        AppraisalCurrentState = appraisal.AppraisalStatus.ToString(),
                                                        OldEmployeeCode = Convert.ToString(empMasters.OldEmployeeCode),
                                                        FinalScore = Convert.ToString(appraisal.FinalScore),
                                                        FinalRating = Convert.ToString(appraisal.FinalRating),
                                                        H1Score = (from appPhases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                                                   where appPhases.AppraisalPhase == "H1" && appPhases.Appraisal.ToString() == appraisal.Id.ToString()
                                                                   select appPhases.Score.ToString()).FirstOrDefault(),
                                                        H2Score = (from appPhases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                                                   where appPhases.AppraisalPhase == "H2" && appPhases.Appraisal.ToString() == appraisal.Id.ToString()
                                                                   select appPhases.Score.ToString()).FirstOrDefault(),
                                                    }).ToList();
                        }
                    }
                }
            });
            EmployeeWiseResponse EmployeesResponse = new EmployeeWiseResponse();
            EmployeesResponse.DataItems = ParametersEntityList;
            return EmployeesResponse;
        }

        //public CompletionStatusResponse GetCompletionStatusReport(CompletionStatusRequest completionRequest)
        //{
        //    List<CompletionStatusEntity> CompletionStatus = new List<CompletionStatusEntity>();
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                SPList appraisalStatus = web.Lists["Appraisal Status"];
        //                string a, b;
        //                string x, y;
        //                string strRegion, strCountry, strNoOfAppraises, strOU, strArea, strApprisalCompleted;
        //                string[] strRegionIds = completionRequest.Region.Split('|');
        //                string[] strCompanyIds = completionRequest.Country.Split('|');
        //                SPList appraisalList = web.Lists["Appraisals"];
        //                SPQuery ospAppraisalQuery = new SPQuery();
        //                DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                SPList empMastersList = web.Lists["Employees Master"];
        //                SPQuery ospEmpMastersQuery = new SPQuery();
        //                DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                SPList appPhasesList = web.Lists["Appraisal Phases"];
        //                SPQuery appPhasesQuery = new SPQuery();
        //                DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
        //                    CompletionStatus = (from employeesMaster in dtEmpMasters.AsEnumerable().AsEnumerable()
        //                                        join appraisals in dtAppraisal.AsEnumerable()
        //                                            on employeesMaster.Field<double>("EmployeeCode").ToString() equals appraisals.Field<string>("appEmployeeCode").ToString()
        //                                        join appphases in dtAppPhases.AsEnumerable()
        //                                            on appraisals.Field<int>("ID").ToString() equals appphases.Field<double>("aphAppraisalId").ToString()
        //                                        where strRegionIds.Contains(employeesMaster.Field<string>("HRRegion") == null ? "" : employeesMaster.Field<string>("HRRegion"))
        //                                            //&& employeesMaster.Field<string>("CompanyName").ToString() == completionRequest.Country.ToString()
        //                                        && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName")!=null?employeesMaster.Field<string>("CompanyName"):"")
        //                                        && appphases.Field<string>("aphAppraisalPhase").ToString() == completionRequest.Phase.ToString()
        //                                        && appraisals.Field<double>("appPerformanceCycle").ToString() == cycle.ToString()
        //                                        select new CompletionStatusEntity
        //                                        {
        //                                            EmployeeCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode") == null ? 0 : employeesMaster.Field<double>("EmployeeCode")),
        //                                            Region = Convert.ToString(employeesMaster.Field<string>("HRRegion") == null ? "" : employeesMaster.Field<string>("HRRegion")),
        //                                            Country = Convert.ToString(employeesMaster.Field<string>("CompanyName") == null ? "" : employeesMaster.Field<string>("CompanyName")),
        //                                            Area = Convert.ToString(employeesMaster.Field<string>("Area")),
        //                                            OU = Convert.ToString(employeesMaster.Field<string>("OrganizationUnit")),
        //                                            HRBusinessPartnerCode = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode") == null ? 0 : employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                            AppraiserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode") == null ? 0 : employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
        //                                            ReviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode") == null ? 0 : employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
        //                                            NoOfAppraisees = a = (from app in dtAppraisal.AsEnumerable()
        //                                                                  join appPhases in dtAppPhases.AsEnumerable() on app.Field<int>("ID").ToString() equals appPhases.Field<double>("aphAppraisalId").ToString()
        //                                                                  join emp in dtEmpMasters.AsEnumerable() on app.Field<string>("appEmployeeCode").ToString() equals emp.Field<double>("EmployeeCode").ToString()
        //                                                                  where appPhases.Field<string>("aphAppraisalPhase").ToString() == completionRequest.Phase.ToString()
        //                                                                  && emp.Field<string>("HRRegion").ToString() == employeesMaster.Field<string>("HRRegion").ToString()
        //                                                                  && emp.Field<string>("CompanyName").ToString() == employeesMaster.Field<string>("CompanyName").ToString()
        //                                                                  && app.Field<double>("appPerformanceCycle").ToString() == cycle.ToString()
        //                                                                  select new { x = app.Field<int>("ID") }).Count().ToString(),
        //                                            NoOfAppraisalsCompleted = b = completionRequest.Phase.ToString() == "H2" ? (from app in dtAppraisal.AsEnumerable()
        //                                                                                                                        join appPhases in dtAppPhases.AsEnumerable() on app.Field<int>("ID").ToString() equals appPhases.Field<double>("aphAppraisalId").ToString()
        //                                                                                                                        join emp in dtEmpMasters.AsEnumerable() on app.Field<string>("appEmployeeCode").ToString() equals emp.Field<double>("EmployeeCode").ToString()
        //                                                                                                                        where appPhases.Field<string>("aphAppraisalPhase").ToString() == completionRequest.Phase.ToString()
        //                                                                                                                        && (app.Field<string>("appAppraisalStatus").ToString() == this.GetAppraisalStatus(20))
        //                                                                                                                        && emp.Field<string>("HRRegion").ToString() == employeesMaster.Field<string>("HRRegion").ToString()
        //                                                                                                                        && emp.Field<string>("CompanyName").ToString() == employeesMaster.Field<string>("CompanyName").ToString()
        //                                                                                                                        select new { x = app.Field<int>("ID") }).Count().ToString() : (from app in dtAppraisal.AsEnumerable()
        //                                                                                                                                                                                       join appPhases in dtAppPhases.AsEnumerable() on app.Field<int>("ID").ToString() equals appPhases.Field<double>("aphAppraisalId").ToString()
        //                                                                                                                                                                                       join emp in dtEmpMasters.AsEnumerable() on app.Field<string>("appEmployeeCode").ToString() equals emp.Field<double>("EmployeeCode").ToString()
        //                                                                                                                                                                                       where appPhases.Field<string>("aphAppraisalPhase").ToString() == "H2"
        //                                                                                                                                                                                       && appPhases.Field<double>("aphAppraisalId").ToString() == app.Field<int>("ID").ToString()
        //                                                                                                                                                                                       && emp.Field<string>("CompanyName").ToString() == employeesMaster.Field<string>("CompanyName").ToString()
        //                                                                                                                                                                                       && app.Field<double>("appPerformanceCycle").ToString() == cycle.ToString()
        //                                                                                                                                                                                       select new { x = app.Field<int>("ID") }).Count().ToString(),
        //                                            NoOfAppraisalsPending = (Convert.ToInt32(a) - Convert.ToInt32(b)).ToString(),
        //                                            PercentageOfCompletion = (Math.Floor((Convert.ToDecimal(b) / Convert.ToDecimal(a)) * 100)).ToString(),

        //                                        }).ToList();
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    CompletionStatusResponse completionResponse = new CompletionStatusResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        CompletionStatus = CompletionStatus.Where(p => p.Region.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        CompletionStatus = CompletionStatus.Where(p => p.HRBusinessPartner.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }
        //    completionResponse.DataItems = CompletionStatus;
        //    return completionResponse;
        //}



        //public RatingTrendResponse GetRatingTrendReport(RatingTrendRequest ratingRequest)
        //{
        //    List<RatingTrendEntity> RatingTrendList = new List<RatingTrendEntity>();
        //    List<RatingTrendEntity> FinalList = new List<RatingTrendEntity>();
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    string rating, ratingCount, region, regionRatingCount, Organization, company, regionRating;
        //                    int apprasisalsCount;
        //                    string[] strRegionIds = ratingRequest.Region.Split('|');
        //                    string[] strOrganizationUnitIds = ratingRequest.OrganizationUnit.Split('|');
        //                    string[] strCompanyIds = ratingRequest.Country.Split('|');
        //                    SPList appraisalList = web.Lists["Appraisals"];
        //                    SPQuery ospAppraisalQuery = new SPQuery();
        //                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    SPList appPhasesList = web.Lists["Appraisal Phases"];
        //                    SPQuery appPhasesQuery = new SPQuery();
        //                    DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
        //                    SPList performanceRatingsList = web.Lists["Performance Ratings"];
        //                    SPQuery performanceRatingsQuery = new SPQuery();
        //                    DataTable dtperformanceRatings = performanceRatingsList.GetItems(performanceRatingsQuery).GetDataTable();

        //                    apprasisalsCount = (from app in dtAppraisal.AsEnumerable()
        //                                        join Phases in dtAppPhases.AsEnumerable()
        //                                         on app.Field<int>("ID").ToString() equals Phases.Field<double>("aphAppraisalId").ToString()
        //                                        where app.Field<double>("appPerformanceCycle").ToString() == ratingRequest.PerformanceCycle.ToString() && Phases.Field<string>("aphAppraisalPhase").ToString() == ratingRequest.Phase.ToString()
        //                                        select app.Field<int>("ID")).Count();

        //                    RatingTrendList = (from appraisals in dtAppraisal.AsEnumerable()
        //                                       join employeesMaster in dtEmpMasters.AsEnumerable()
        //                                            on appraisals.Field<string>("appEmployeeCode").ToString() equals employeesMaster.Field<double>("EmployeeCode").ToString()
        //                                       join appphases in dtAppPhases.AsEnumerable()
        //                                            on appraisals.Field<int>("ID").ToString() equals appphases.Field<double>("aphAppraisalId").ToString()
        //                                       join pRating in dtperformanceRatings.AsEnumerable()
        //                                            on appraisals.Field<double?>("appFinalRating").ToString() equals pRating.Field<double>("pmScaleNumber").ToString() into x
        //                                       from y in x.DefaultIfEmpty()
        //                                       where appraisals.Field<double>("appPerformanceCycle").ToString() == ratingRequest.PerformanceCycle.ToString()
        //                                       && strRegionIds.Contains(employeesMaster.Field<string>("HRRegion"))
        //                                       && strOrganizationUnitIds.Contains(employeesMaster.Field<string>("OrganizationUnit"))
        //                                       && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName"))
        //                                       && appphases.Field<string>("aphAppraisalPhase").ToString() == ratingRequest.Phase.ToString()
        //                                       select new RatingTrendEntity
        //                                       {
        //                                           Rating = regionRating = y == null ? string.Empty : y.Field<double>("pmScaleNumber").ToString(),
        //                                           OU = employeesMaster.Field<string>("OrganizationUnit").ToString(),
        //                                           CountryName = employeesMaster.Field<string>("CompanyName"),
        //                                           RegionName = employeesMaster.Field<string>("HRRegion"),
        //                                           EmpCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode")),
        //                                           HRBusinessPartner = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                           AppraiserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
        //                                           ReviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
        //                                       }).ToList();
        //                    foreach (RatingTrendEntity rte in RatingTrendList)
        //                    {
        //                        rte.Count = (from appraisals1 in dtAppraisal.AsEnumerable()
        //                                     join employeesMaster1 in dtEmpMasters.AsEnumerable()
        //                                         on appraisals1.Field<string>("appEmployeeCode").ToString() equals employeesMaster1.Field<double>("EmployeeCode").ToString()
        //                                     join appphases1 in dtAppPhases.AsEnumerable()
        //                                         on appraisals1.Field<int>("ID").ToString() equals appphases1.Field<double>("aphAppraisalId").ToString()
        //                                     where appraisals1.Field<double>("appPerformanceCycle").ToString() == ratingRequest.PerformanceCycle.ToString()
        //                                         && appphases1.Field<string>("aphAppraisalPhase").ToString() == ratingRequest.Phase.ToString()
        //                                         && employeesMaster1.Field<string>("HRRegion") == rte.RegionName
        //                                         && employeesMaster1.Field<string>("CompanyName") == rte.CountryName
        //                                         && appraisals1.Field<double?>("appFinalRating").ToString() == rte.Rating.ToString()
        //                                     select new { a = appraisals1.Field<int>("ID") }).Count().ToString();
        //                        rte.Percentage = (Convert.ToInt32(rte.Count) == 0 ? 0 : Math.Floor((Convert.ToDecimal(rte.Count) / apprasisalsCount) * 100)).ToString();
        //                    }
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    RatingTrendResponse ratingTrendResponse = new RatingTrendResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        RatingTrendList = RatingTrendList.Where(p => p.RegionName.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        RatingTrendList = RatingTrendList.Where(p => p.HRBusinessPartner.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmpCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }

        //    ratingTrendResponse.DataItems = RatingTrendList;
        //    return ratingTrendResponse;
        //}

        //public PDPResponse GetPDPReport(PDPRequest pdpRequest)
        //{
        //    List<PDPEntity> PDPList = new List<PDPEntity>();
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    string[] strRegionIds = pdpRequest.Region.Split('|');
        //                    string[] strOrganizationUnitIds = pdpRequest.OrganizationUnit.Split('|');
        //                    string[] strCompanyIds = pdpRequest.Country.Split('|');
        //                    SPList appraisalList = web.Lists["Appraisals"];
        //                    SPQuery ospAppraisalQuery = new SPQuery();
        //                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    SPList appPhasesList = web.Lists["Appraisal Phases"];
        //                    SPQuery appPhasesQuery = new SPQuery();
        //                    DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
        //                    SPList dmList = web.Lists["Appraisal Development Measures"];
        //                    SPQuery dmQuery = new SPQuery();
        //                    DataTable dtDmQuery = dmList.GetItems(dmQuery).GetDataTable();
        //                    PDPList = (from employeesMaster in dtEmpMasters.AsEnumerable()
        //                               join appraisals in dtAppraisal.AsEnumerable()
        //                                    on employeesMaster.Field<double>("EmployeeCode").ToString() equals appraisals.Field<string>("appEmployeeCode").ToString()
        //                               join appphases in dtAppPhases.AsEnumerable()
        //                                   on appraisals.Field<int>("ID").ToString() equals appphases.Field<double>("aphAppraisalId").ToString()
        //                               join developmentMeasures in dtDmQuery.AsEnumerable()
        //                               on appraisals.Field<int>("ID").ToString() equals developmentMeasures.Field<double>("pdpAppraisalID").ToString()
        //                               where appraisals.Field<double>("appPerformanceCycle").ToString() == pdpRequest.PerformanceCycle.ToString()
        //                               && strRegionIds.Contains(employeesMaster.Field<string>("HRRegion"))
        //                               && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName"))
        //                               && strOrganizationUnitIds.Contains(employeesMaster.Field<string>("OrganizationUnit"))
        //                               orderby employeesMaster.Field<double>("EmployeeCode")
        //                               select new PDPEntity
        //                               {
        //                                   EmployeeCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode")),
        //                                   EmployeeName = Convert.ToString(employeesMaster.Field<string>("EmployeeName")),
        //                                   Position = Convert.ToString(employeesMaster.Field<string>("Position")),
        //                                   WorkLevel = Convert.ToString(employeesMaster.Field<string>("EmployeeGroup")),
        //                                   Country = Convert.ToString(employeesMaster.Field<string>("CompanyName")),
        //                                   Region = Convert.ToString(employeesMaster.Field<string>("HRRegion")),
        //                                   Area = Convert.ToString(employeesMaster.Field<string>("Area")),
        //                                   SubArea = Convert.ToString(employeesMaster.Field<string>("SubArea")),
        //                                   OrganizationUnit = Convert.ToString(employeesMaster.Field<string>("OrganizationUnit")),
        //                                   HRBusinessPartner = Convert.ToString(employeesMaster.Field<string>("HRBusinessPartnerName")),
        //                                   Appraiser = Convert.ToString(employeesMaster.Field<string>("DepartmentHeadName")),
        //                                   Reviewer = Convert.ToString(employeesMaster.Field<string>("ReportingManagerName")),
        //                                   HRBusinessPartnerCode = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                   AppraiserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
        //                                   ReviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
        //                                   DMWhen = Convert.ToString(developmentMeasures.Field<string>("pdpWhen")),
        //                                   DMWhat = Convert.ToString(developmentMeasures.Field<string>("pdpWhat")),
        //                                   DMNextSteps = Convert.ToString(developmentMeasures.Field<string>("pdpNextSteps")),
        //                                   H1AppraiseeComments = Convert.ToString(developmentMeasures.Field<string>("pdpH1AppraiseeComments")),
        //                                   H1AppraiserComments = Convert.ToString(developmentMeasures.Field<string>("pdpH1AppraiserComments")),
        //                                   H2AppraiseeComments = Convert.ToString(developmentMeasures.Field<string>("pdpH2AppraiseeComments")),
        //                                   H2AppraiserComments = Convert.ToString(developmentMeasures.Field<string>("pdpH2AppraiserComments")),
        //                                   OldEmployeeCode = Convert.ToString(employeesMaster.Field<double?>("OldEmployeeCode")),
        //                               }).ToList();
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    PDPResponse pdpdResponse = new PDPResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        PDPList = PDPList.Where(p => p.Region.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        PDPList = PDPList.Where(p => p.HRBusinessPartnerCode.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }
        //    pdpdResponse.DataItems = PDPList;
        //    return pdpdResponse;
        //}

        public PDPResponse GetPDPReport(PDPRequest pdpRequest)
        {
            List<PDPEntity> PDPList = new List<PDPEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            string[] strRegionIds = pdpRequest.Region.Split('|');
                            string[] strOrganizationUnitIds = pdpRequest.OrganizationUnit.Split('|');
                            string[] strCompanyIds = pdpRequest.Country.Split('|');
                            PDPList = (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                       join appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                            on employeesMaster.EmployeeCode.ToString() equals appraisals.EmployeeCode.ToString()
                                       join appphases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                           on appraisals.Id.ToString() equals appphases.Appraisal.ToString()
                                       join developmentMeasures in PMSDataContext.AppraisalDevelopmentMeasures.AsEnumerable()
                                       on appphases.Id.ToString() equals developmentMeasures.AppraisalPhaseID.ToString()
                                       where appraisals.PerformanceCycle.ToString() == pdpRequest.PerformanceCycle.ToString()
                                       && strRegionIds.Contains(employeesMaster.HRRegion == null ? "" : employeesMaster.HRRegion.RegionName.ToString())
                                       && strCompanyIds.Contains(employeesMaster.CompanyName == null ? "" : employeesMaster.CompanyName.ToString())
                                       && strOrganizationUnitIds.Contains(employeesMaster.OrganizationUnit)
                                       && (appraisals.Deactivated != null ? (appraisals.Deactivated.Value.ToString() != "Yes") : 1 == 1)
                                       && ((isTMT || isReviewBoard) ? appraisals.PerformanceCycle.ToString() == pdpRequest.PerformanceCycle.ToString() :
                                       (isRegionalHR ? Convert.ToString(employeesMaster.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                           // (employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                       (employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                       orderby employeesMaster.EmployeeCode
                                       select new PDPEntity
                                       {
                                           EmployeeCode = Convert.ToString(employeesMaster.EmployeeCode),
                                           EmployeeName = Convert.ToString(employeesMaster.EmployeeName),
                                           Position = Convert.ToString(employeesMaster.Position),
                                           WorkLevel = Convert.ToString(employeesMaster.EmployeeGroup.EmployeeGroupCode),
                                           Country = Convert.ToString(employeesMaster.CompanyName),
                                           Region = Convert.ToString(employeesMaster.HRRegion.RegionName),
                                           Area = Convert.ToString(employeesMaster.Area),
                                           SubArea = Convert.ToString(employeesMaster.SubArea),
                                           OrganizationUnit = Convert.ToString(employeesMaster.OrganizationUnit),
                                           HRBusinessPartner = Convert.ToString(employeesMaster.HRBusinessPartnerName),
                                           Appraiser = Convert.ToString(employeesMaster.ReportingManagerName),
                                           Reviewer = Convert.ToString(employeesMaster.DepartmentHeadName),
                                           HRBusinessPartnerCode = Convert.ToString(employeesMaster.HRBusinessPartnerEmployeeCode),
                                           AppraiserCode = Convert.ToString(employeesMaster.ReportingManagerEmployeeCode),
                                           ReviewerCode = Convert.ToString(employeesMaster.DepartmentHeadEmployeeCode),
                                           DMWhen = Convert.ToString(developmentMeasures.When),
                                           DMWhat = Convert.ToString(developmentMeasures.What),
                                           DMNextSteps = Convert.ToString(developmentMeasures.NextSteps),
                                           H1AppraiseeComments = Convert.ToString(developmentMeasures.H1AppraiseeComments),
                                           H1AppraiserComments = Convert.ToString(developmentMeasures.H1AppraiserComments),
                                           H2AppraiseeComments = Convert.ToString(developmentMeasures.H2AppraiseeComments),
                                           H2AppraiserComments = Convert.ToString(developmentMeasures.H2AppraiserComments),
                                           OldEmployeeCode = Convert.ToString(employeesMaster.OldEmployeeCode),
                                       }).Distinct().ToList();
                        }
                    }
                }
            });
            PDPResponse pdpdResponse = new PDPResponse();
            pdpdResponse.DataItems = PDPList;
            return pdpdResponse;
        }

        //public HRReviewResponse GetHRReviewReport(HRReviewRequest reviewRequest)
        //{
        //    List<PDPEntity> PDPList = new List<PDPEntity>();
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
        //                    string[] strRegionIds = reviewRequest.Region.Split('|');
        //                    string[] strCompanyIds = reviewRequest.Country.Split('|');
        //                    SPList appraisalList = web.Lists["Appraisals"];
        //                    SPQuery ospAppraisalQuery = new SPQuery();
        //                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    SPList appPhasesList = web.Lists["Appraisal Phases"];
        //                    SPQuery appPhasesQuery = new SPQuery();
        //                    DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
        //                    PDPList = (from employeesMaster in dtEmpMasters.AsEnumerable()
        //                               join appraisals in dtAppraisal.AsEnumerable()
        //                                    on employeesMaster.Field<double>("EmployeeCode").ToString() equals appraisals.Field<string>("appEmployeeCode").ToString()
        //                               join appphases in dtAppPhases.AsEnumerable()
        //                                   on appraisals.Field<int>("ID").ToString() equals appphases.Field<double>("aphAppraisalId").ToString()
        //                               where strRegionIds.Contains(employeesMaster.Field<string>("HRRegion"))
        //                               && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName"))
        //                               && appphases.Field<string>("aphAppraisalPhase").ToString() == reviewRequest.Phase.ToString()
        //                               && appraisals.Field<double>("appPerformanceCycle").ToString() == cycle.ToString()
        //                               orderby employeesMaster.Field<double>("EmployeeCode")
        //                               select new PDPEntity
        //                               {
        //                                   EmployeeCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode")),
        //                                   EmployeeName = Convert.ToString(employeesMaster.Field<string>("EmployeeName")),
        //                                   Position = Convert.ToString(employeesMaster.Field<string>("Position")),
        //                                   WorkLevel = Convert.ToString(employeesMaster.Field<string>("EmployeeGroup")),
        //                                   Country = Convert.ToString(employeesMaster.Field<string>("CompanyName")),
        //                                   Region = Convert.ToString(employeesMaster.Field<string>("HRRegion")),
        //                                   Area = Convert.ToString(employeesMaster.Field<string>("Area")),
        //                                   SubArea = Convert.ToString(employeesMaster.Field<string>("SubArea")),
        //                                   OrganizationUnit = Convert.ToString(employeesMaster.Field<string>("OrganizationUnit")),
        //                                   HRBusinessPartner = Convert.ToString(employeesMaster.Field<string>("HRBusinessPartnerName")),
        //                                   Appraiser = Convert.ToString(employeesMaster.Field<string>("DepartmentHeadName")),
        //                                   Reviewer = Convert.ToString(employeesMaster.Field<string>("ReportingManagerName")),
        //                                   HRBusinessPartnerCode = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                   AppraiserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
        //                                   ReviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
        //                                   HRComments = Convert.ToString(appphases.Field<string>("aphHRReviewLatestComments")),
        //                                   OldEmployeeCode = Convert.ToString(employeesMaster.Field<double?>("OldEmployeeCode")),
        //                               }).ToList();
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    HRReviewResponse hrReviewResponse = new HRReviewResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        PDPList = PDPList.Where(p => p.Region.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        PDPList = PDPList.Where(p => p.HRBusinessPartnerCode.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }

        //    hrReviewResponse.DataItems = PDPList;
        //    return hrReviewResponse;
        //}

        public HRReviewResponse GetHRReviewReport(HRReviewRequest reviewRequest)
        {
            List<PDPEntity> PDPList = new List<PDPEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                            string[] strRegionIds = reviewRequest.Region.Split('|');
                            string[] strCompanyIds = reviewRequest.Country.Split('|');
                            PDPList = (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                       join appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                            on employeesMaster.EmployeeCode.ToString() equals appraisals.EmployeeCode.ToString()
                                       join appphases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                           on appraisals.Id.ToString() equals appphases.Appraisal.ToString()
                                       where strRegionIds.Contains(employeesMaster.HRRegion.RegionName == null ? "" : employeesMaster.HRRegion.RegionName.ToString())
                                       && strCompanyIds.Contains(employeesMaster.CompanyName == null ? "" : employeesMaster.CompanyName.ToString())
                                       && appphases.AppraisalPhase.ToString() == reviewRequest.Phase.ToString()
                                       && appraisals.PerformanceCycle.ToString() == cycle.ToString()
                                       && (appphases.IsReview.Value.ToString() != "False")
                                           //&& (appphases.IsReview.Value.ToString()=="Closed")
                                       && ((isTMT || isReviewBoard) ? appraisals.PerformanceCycle.ToString() == cycle.ToString() :
                                       (isRegionalHR ? Convert.ToString(employeesMaster.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                           //(employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                        (employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                       orderby employeesMaster.EmployeeCode
                                       select new PDPEntity
                                       {
                                           EmployeeCode = Convert.ToString(employeesMaster.EmployeeCode),
                                           EmployeeName = Convert.ToString(employeesMaster.EmployeeName),
                                           Position = Convert.ToString(employeesMaster.Position),
                                           WorkLevel = Convert.ToString(employeesMaster.EmployeeGroup.EmployeeGroupCode),
                                           Country = Convert.ToString(employeesMaster.CompanyName),
                                           Region = Convert.ToString(employeesMaster.HRRegion.RegionName),
                                           Area = Convert.ToString(employeesMaster.Area),
                                           SubArea = Convert.ToString(employeesMaster.SubArea),
                                           OrganizationUnit = Convert.ToString(employeesMaster.OrganizationUnit),
                                           HRBusinessPartner = Convert.ToString(employeesMaster.HRBusinessPartnerName),
                                           Appraiser = Convert.ToString(employeesMaster.ReportingManagerName),
                                           Reviewer = Convert.ToString(employeesMaster.DepartmentHeadName),
                                           HRBusinessPartnerCode = Convert.ToString(employeesMaster.HRBusinessPartnerEmployeeCode),
                                           AppraiserCode = Convert.ToString(employeesMaster.ReportingManagerEmployeeCode),
                                           ReviewerCode = Convert.ToString(employeesMaster.DepartmentHeadEmployeeCode),
                                           HRComments = Convert.ToString(appphases.HRReviewLatestComments),
                                           OldEmployeeCode = Convert.ToString(employeesMaster.OldEmployeeCode),
                                           DMWhen = Convert.ToString(appphases.IsReview.Value),
                                           ReviewStatus = Convert.ToString(appphases.IsReview.Value),
                                       }).ToList();
                        }

                    }
                }
            });
            HRReviewResponse hrReviewResponse = new HRReviewResponse();
            hrReviewResponse.DataItems = PDPList;
            return hrReviewResponse;
        }

        //public PIPResponse GetPIPReport(PIPRequest PIPRequest)
        //{
        //    List<PIPEntity> PDPList = new List<PIPEntity>();
        //    spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
        //    SPSecurity.RunWithElevatedPrivileges(delegate
        //    {
        //        using (SPSite osite = new SPSite(url))
        //        {
        //            using (SPWeb web = osite.OpenWeb())
        //            {
        //                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //                {
        //                    string[] strRegionIds = PIPRequest.Region.Split('|');
        //                    string[] strCompanyIds = PIPRequest.Country.Split('|');
        //                    SPList appraisalList = web.Lists["Appraisals"];
        //                    SPQuery ospAppraisalQuery = new SPQuery();
        //                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
        //                    SPList empMastersList = web.Lists["Employees Master"];
        //                    SPQuery ospEmpMastersQuery = new SPQuery();
        //                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
        //                    SPList appPhasesList = web.Lists["Appraisal Phases"];
        //                    SPQuery appPhasesQuery = new SPQuery();
        //                    DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
        //                    SPList pipList = web.Lists["PIP"];
        //                    SPQuery pipQuery = new SPQuery();
        //                    DataTable dtPIP = pipList.GetItems(pipQuery).GetDataTable();
        //                    PDPList = (from employeesMaster in dtEmpMasters.AsEnumerable()
        //                               join appraisals in dtAppraisal.AsEnumerable()
        //                                    on employeesMaster.Field<double>("EmployeeCode").ToString() equals appraisals.Field<string>("appEmployeeCode").ToString()
        //                               join appphases in dtAppPhases.AsEnumerable()
        //                                   on appraisals.Field<int>("ID").ToString() equals appphases.Field<double>("aphAppraisalId").ToString()
        //                               join pip in dtPIP.AsEnumerable()
        //                                on appraisals.Field<int>("ID").ToString() equals pip.Field<double>("pipAppraisalID").ToString()
        //                               where appraisals.Field<double>("appPerformanceCycle").ToString() == PIPRequest.PerformanceCycle.ToString() &&
        //                                strRegionIds.Contains(employeesMaster.Field<string>("HRRegion"))
        //                               && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName"))
        //                               orderby employeesMaster.Field<double>("EmployeeCode")
        //                               select new PIPEntity
        //                               {
        //                                   EmployeeCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode")),
        //                                   EmployeeName = Convert.ToString(employeesMaster.Field<string>("EmployeeName")),
        //                                   Position = Convert.ToString(employeesMaster.Field<string>("Position")),
        //                                   WorkLevel = Convert.ToString(employeesMaster.Field<string>("EmployeeGroup")),
        //                                   Country = Convert.ToString(employeesMaster.Field<string>("CompanyName")),
        //                                   Region = Convert.ToString(employeesMaster.Field<string>("HRRegion")),
        //                                   Area = Convert.ToString(employeesMaster.Field<string>("Area")),
        //                                   SubArea = Convert.ToString(employeesMaster.Field<string>("SubArea")),
        //                                   OrganizationUnit = Convert.ToString(employeesMaster.Field<string>("OrganizationUnit")),
        //                                   HRBusinessPartner = Convert.ToString(employeesMaster.Field<string>("HRBusinessPartnerName")),
        //                                   Appraiser = Convert.ToString(employeesMaster.Field<string>("DepartmentHeadName")),
        //                                   Reviewer = Convert.ToString(employeesMaster.Field<string>("ReportingManagerName")),
        //                                   HRBusinessPartnerCode = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
        //                                   AppraiserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
        //                                   ReviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
        //                                   Phase = Convert.ToString(appphases.Field<string>("aphAppraisalPhase")),
        //                                   Rating = Convert.ToString(appraisals.Field<double?>("appFinalRating")),
        //                                   PerformanceIssue = Convert.ToString(pip.Field<string>("pipPerformanceIssue")),
        //                                   MidTermEvalComments = Convert.ToString(pip.Field<string>("pipMidTermAppraisersAssessment")),
        //                                   MidFinalEvalComments = Convert.ToString(pip.Field<string>("pipFinalAppraisersAssesment")),
        //                                   OldEmployeeCode = Convert.ToString(employeesMaster.Field<double?>("OldEmployeeCode")),
        //                               }).ToList();
        //                }
        //                GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
        //            }
        //        }
        //    });
        //    PIPResponse PIPResponse = new PIPResponse();
        //    if (isReviewBoard || isTMT)
        //    { }
        //    else if (isRegionalHR)
        //    {
        //        PDPList = PDPList.Where(p => p.Region.ToString() == HRRegion.ToString()).ToList();//reg.ToString().ToLower().Contains(txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
        //    }
        //    else if (isHR || isReviewer || isAppraiser)
        //    {
        //        PDPList = PDPList.Where(p => p.HRBusinessPartnerCode.ToString() == spItem["EmployeeCode"].ToString() || p.ReviewerCode.ToString() == spItem["EmployeeCode"].ToString() || p.AppraiserCode.ToString() == spItem["EmployeeCode"].ToString() || p.EmployeeCode.ToString() == spItem["EmployeeCode"].ToString()).ToList();
        //    }
        //    PIPResponse.DataItems = PDPList;
        //    return PIPResponse;
        //}

        public PIPResponse GetPIPReport(PIPRequest PIPRequest)
        {
            List<PIPEntity> PDPList = new List<PIPEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            string[] strRegionIds = PIPRequest.Region.Split('|');
                            string[] strCompanyIds = PIPRequest.Country.Split('|');
                            string[] OUs = PIPRequest.OrganizationUnit.Split('|');
                            PDPList = (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                       join appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                            on employeesMaster.EmployeeCode.ToString() equals appraisals.EmployeeCode.ToString()
                                       join appphases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                           on appraisals.Id.ToString() equals appphases.Appraisal.ToString()
                                       join pip in PMSDataContext.PIP.AsEnumerable()
                                       on appphases.Id.ToString() equals pip.AppraisalPhaseID.ToString()
                                       where appraisals.PerformanceCycle.ToString() == PIPRequest.PerformanceCycle.ToString() &&
                                        (strRegionIds.Contains(employeesMaster.HRRegion.RegionName == null ? "" : employeesMaster.HRRegion.RegionName.ToString()))
                                       && (strCompanyIds.Contains(employeesMaster.CompanyName == null ? "" : employeesMaster.CompanyName.ToString()))
                                       && (OUs.Contains(employeesMaster.OrganizationUnit == null ? "" : employeesMaster.OrganizationUnit.ToString()))
                                       && (appraisals.Deactivated != null ? (appraisals.Deactivated.Value.ToString() != "Yes") : 1 == 1)
                                       && ((isTMT || isReviewBoard) ? appraisals.PerformanceCycle.ToString() == PIPRequest.PerformanceCycle.ToString() :
                                       (isRegionalHR ? Convert.ToString(employeesMaster.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                           //(employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                        (employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                       orderby employeesMaster.EmployeeCode
                                       select new PIPEntity
                                       {
                                           EmployeeCode = Convert.ToString(employeesMaster.EmployeeCode),
                                           EmployeeName = Convert.ToString(employeesMaster.EmployeeName),
                                           Position = Convert.ToString(employeesMaster.Position),
                                           WorkLevel = Convert.ToString(employeesMaster.EmployeeGroupName),
                                           Country = Convert.ToString(employeesMaster.CompanyName),
                                           Region = Convert.ToString(employeesMaster.HRRegion.RegionName),
                                           Area = Convert.ToString(employeesMaster.Area),
                                           SubArea = Convert.ToString(employeesMaster.SubArea),
                                           OrganizationUnit = Convert.ToString(employeesMaster.OrganizationUnit),
                                           HRBusinessPartner = Convert.ToString(employeesMaster.HRBusinessPartnerName),
                                           Appraiser = Convert.ToString(employeesMaster.ReportingManagerName),
                                           Reviewer = Convert.ToString(employeesMaster.DepartmentHeadName),
                                           HRBusinessPartnerCode = Convert.ToString(employeesMaster.HRBusinessPartnerEmployeeCode),
                                           AppraiserCode = Convert.ToString(employeesMaster.ReportingManagerEmployeeCode),
                                           ReviewerCode = Convert.ToString(employeesMaster.DepartmentHeadEmployeeCode),
                                           Phase = Convert.ToString(appphases.AppraisalPhase),
                                           Rating = Convert.ToString(appraisals.FinalRating),
                                           PerformanceIssue = Convert.ToString(pip.PerformanceIssue),
                                           MidTermEvalComments = Convert.ToString(pip.MidTermAppraisersAssessment),
                                           MidFinalEvalComments = Convert.ToString(pip.FinalAppraisersAssesment),
                                           OldEmployeeCode = Convert.ToString(employeesMaster.OldEmployeeCode),
                                       }).Distinct().ToList();
                        }
                    }
                }
            });
            PIPResponse PIPResponse = new PIPResponse();
            PIPResponse.DataItems = PDPList;
            return PIPResponse;
        }

        public List<PerformanceRatingEntity> GetPerformanceRatings()
        {
            List<PerformanceRatingEntity> ratingList = new List<PerformanceRatingEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            ratingList = (from ratings in PMSDataContext.PerformanceRatings.AsEnumerable()
                                          select new PerformanceRatingEntity
                                          {
                                              numberScale = Convert.ToInt32(ratings.Rating),
                                              PercentageAchievement = string.Concat(ratings.LowerValue.ToString(), "-", ratings.UpperValue),
                                          }).ToList();
                        }
                    }
                }
            });
            return ratingList;
        }

        public List<PerformnceCycleEntity> GetAllPerformanceCycles()
        {
            List<PerformnceCycleEntity> performanceCycles = new List<PerformnceCycleEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            performanceCycles = (from cycles in PMSDataContext.PerformanceCycles
                                                 select new PerformnceCycleEntity
                                                 {
                                                     PerformanceCycle = cycles.PerformanceCycle.ToString(),
                                                 }).ToList();

                        }
                    }
                }
            });
            return performanceCycles;
        }

        public string GetAppraisalStatus(int statusId)
        {
            string status = string.Empty;
            using (SPSite osite = new SPSite(url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList appraisalStatus = web.Lists["Appraisal Status"];
                    SPListItem lstStatusItem = appraisalStatus.GetItemById(statusId);
                    status = lstStatusItem["Appraisal_x0020_Workflow_x0020_S"].ToString();
                }
            }
            return status;
        }

        #region New Added Code On 31072013

        public SPListItem GetMasterDetails(string colName, string currentUserName, SPWeb currentWeb)
        {

            //LogHandler.LogVerbose("GetMasterDetails(string colName, string currentUserName)");
            SPListItemCollection masterItemsColl = null;
            SPListItem employeeItem = null;
            DataTable dt;
            try
            {
                SPList employeMaster = currentWeb.Lists["Employees Master"];
                SPQuery q = new SPQuery();
                q.ViewFields = @"<FieldRef Name='LoginName'/><FieldRef Name='EmployeeCode'/><FieldRef Name='HRRegion'/><FieldRef Name='ReportingManagerEmployeeCode'/><FieldRef Name='HRBusinessPartnerEmployeeCode'/><FieldRef Name='DepartmentHeadEmployeeCode'/>";
                q.ViewFieldsOnly = true;
                q.RowLimit = 1;
                q.Query = "<Where><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + currentUserName + "</Value></Eq></Where>";
                masterItemsColl = employeMaster.GetItems(q);
                dt = masterItemsColl.GetDataTable();
                employeeItem = masterItemsColl[0];
                if (employeeItem["HRRegion"] != null && employeeItem["HRRegion"].ToString() != "")
                    HRRegion = employeeItem["HRRegion"].ToString().Split('#')[1];
                return employeeItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Not an valid user");
            }
        }

        void GetEmployeeRoles(string EmployeeCode, SPWeb web)
        {
            //if (Session["AppraiseeCode"] != null) return;
            //Page.Session["AppraiseeCode"] = EmployeeCode;
            using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
            {
                var empRoles = (from p in PMSDataContext.EmployeesMaster
                                where
                                    p.HRBusinessPartnerEmployeeCode.ToString() == EmployeeCode
                                    || p.ReportingManagerEmployeeCode.ToString() == EmployeeCode
                                    || p.DepartmentHeadEmployeeCode.ToString() == EmployeeCode
                                select p);


                foreach (EmployeesMasterItem item in empRoles)
                {
                    if (item.DepartmentHeadEmployeeCode.ToString() == EmployeeCode)
                    {
                        //Page.Session["isReviewer"] = "True";
                        isReviewer = true;
                        break;
                    }
                }
                foreach (EmployeesMasterItem item in empRoles)
                {
                    if (item.HRBusinessPartnerEmployeeCode.ToString() == EmployeeCode)
                    {
                        //Page.Session["isHR"] = "True";
                        isHR = true;
                        break;
                    }
                }
                foreach (EmployeesMasterItem item in empRoles)
                {
                    if (item.ReportingManagerEmployeeCode.ToString() == EmployeeCode)
                    {
                        //Page.Session["isAppraiser"] = "True";
                        isAppraiser = true;
                        break;
                    }
                }
                SPGroup group = web.SiteGroups["Review Board"];
                isReviewBoard = InGroup(SPContext.Current.Web.CurrentUser, group);
                group = web.SiteGroups["TMT"];
                isTMT = InGroup(SPContext.Current.Web.CurrentUser, group);

                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //{
                //    SPGroup reviewGroup = web.Groups["Review Board"];
                //    SPGroup TMTGroup = web.Groups["TMT"];
                //    if (web.IsCurrentUserMemberOfGroup(reviewGroup.ID))
                //        isReviewBoard = true;
                //        //Page.Session["isReviewBoard"] = "True";
                //    if (web.IsCurrentUserMemberOfGroup(TMTGroup.ID))
                //        isTMT = true;
                //        //Page.Session["isTMT"] = "True";
                //});

                var RegionalHR = (from r in PMSDataContext.Regions.AsEnumerable() where r.RegionHREmployeeCode.ToString().Trim() == EmployeeCode.Trim() select r).FirstOrDefault();
                if (RegionalHR != null)
                    isRegionalHR = true;
                //Page.Session["isRegionalHR"] = "True";
            }
        }

        public bool InGroup(SPUser user, SPGroup group)
        {
            return user.Groups.Cast<SPGroup>()
              .Any(g => g.ID == group.ID);
        }

        public RatingTrendResponse GetRatingTrendReport(RatingTrendRequest ratingRequest)
        {
            List<RatingTrendEntity> RatingTrendList = new List<RatingTrendEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                            GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                            if (!isTMT) if (!isTMT) throw new Exception("Not an Authorized user");
                            string[] strRegionIds = ratingRequest.Region.Split('|');
                            ////string[] strOrganizationUnitIds = ratingRequest.OrganizationUnit.Split('|');
                            string[] strCompanyIds = ratingRequest.Country.Split('|');
                            RatingTrendList = (from appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                               join employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                               on appraisals.EmployeeCode.ToString() equals employeesMaster.EmployeeCode.ToString()
                                               join apphases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                               on appraisals.Id.ToString() equals apphases.Appraisal.ToString()
                                               where appraisals.PerformanceCycle.ToString() == ratingRequest.PerformanceCycle.ToString()
                                                                  && strRegionIds.Contains(employeesMaster.HRRegion.RegionName == null ? "" : employeesMaster.HRRegion.RegionName.ToString())
                                                   ////&& strOrganizationUnitIds.Contains(employeesMaster.OrganizationUnit)
                                                                  && strCompanyIds.Contains(employeesMaster.CompanyName != null ? employeesMaster.CompanyName : "")
                                                                  && apphases.AppraisalPhase.ToString() == ratingRequest.Phase.ToString()
                                                                  && (appraisals.Deactivated == null ? 1 == 1 : appraisals.Deactivated.Value.ToString() != "Yes")
                                               //group employeesMaster by new
                                               //{
                                               //    HRRegion = employeesMaster.HRRegion.RegionName,
                                               //    CountryName = employeesMaster.CompanyName,
                                               //    OU = employeesMaster.OrganizationUnit,
                                               //    Rating = appraisals.FinalRating,
                                               //} into g
                                               //select new RatingTrendEntity
                                               //{
                                               //    OU = g.Key.OU,
                                               //    RegionName = g.Key.HRRegion,
                                               //    CountryName = g.Key.CountryName,
                                               //    Rating = Convert.ToString(g.Key.Rating),

                                               //}).ToList();
                                               select new RatingTrendEntity
                                               {
                                                   OU = employeesMaster.OrganizationUnit,
                                                   RegionName = employeesMaster.HRRegion.RegionName,
                                                   CountryName = employeesMaster.CompanyName,
                                                   Rating = Convert.ToString(appraisals.FinalRating),
                                                   EmpCode = employeesMaster.ToString(),
                                               }).Distinct().ToList();
                            foreach (RatingTrendEntity rte in RatingTrendList)
                            {
                                rte.TotalRecords = Convert.ToInt32(RatingTrendList.Count);
                            }
                        }
                    }
                }
            });
            RatingTrendResponse ratingTrendResponse = new RatingTrendResponse();
            ratingTrendResponse.DataItems = RatingTrendList;
            return ratingTrendResponse;
        }

        public RatingTrendResponse GetAllRatingTrendReport(AllRatingTrendRequest ratingRequest)
        {
            List<RatingTrendEntity> RatingTrendList = new List<RatingTrendEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                            GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                            if (!isTMT) throw new Exception("Not an Authorized user");
                            RatingTrendList = (from appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                               join employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                               on appraisals.EmployeeCode.ToString() equals employeesMaster.EmployeeCode.ToString()
                                               join apphases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                               on appraisals.Id.ToString() equals apphases.Appraisal.ToString()
                                               where appraisals.PerformanceCycle.ToString() == ratingRequest.PerformanceCycle.ToString()
                                                                 && apphases.AppraisalPhase.ToString() == ratingRequest.Phase.ToString()
                                                                 && (appraisals.Deactivated == null ? 1 == 1 : appraisals.Deactivated.Value.ToString() != "Yes")
                                               //group employeesMaster by new
                                               //{
                                               //    HRRegion = employeesMaster.HRRegion.RegionName,
                                               //    CountryName = employeesMaster.CompanyName,
                                               //    OU = employeesMaster.OrganizationUnit,
                                               //    Rating = appraisals.FinalRating,
                                               //} into g
                                               //select new RatingTrendEntity
                                               //{
                                               //    OU = g.Key.OU,
                                               //    RegionName = g.Key.HRRegion,
                                               //    CountryName = g.Key.CountryName,
                                               //    Count = Convert.ToString(g.Count()),
                                               //    Rating = Convert.ToString(g.Key.Rating),
                                               //}).ToList();
                                               select new RatingTrendEntity
                                               {
                                                   EmpCode = employeesMaster.ToString(),
                                                   OU = employeesMaster.OrganizationUnit,
                                                   RegionName = employeesMaster.HRRegion.RegionName,
                                                   CountryName = employeesMaster.CompanyName,
                                                   Rating = Convert.ToString(appraisals.FinalRating),
                                               }).Distinct().ToList();

                            foreach (RatingTrendEntity rte in RatingTrendList)
                            {
                                rte.TotalRecords = Convert.ToInt32(RatingTrendList.Count);
                            }
                            //rte.TotalRecords = Convert.ToInt32(rte.Count);
                        }
                    }
                }
            });
            RatingTrendResponse ratingTrendResponse = new RatingTrendResponse();
            ratingTrendResponse.DataItems = RatingTrendList;
            return ratingTrendResponse;
        }

        #endregion

        public CompletionStatusResponse GetCompletionStatusReportNew(CompletionStatusRequest completionRequest)
        {
            List<CompletionStatusEntity> CompletionStatus = new List<CompletionStatusEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);

                        string a, b;
                        string[] strRegionIds = completionRequest.Region.Split('|');
                        string[] strCompanyIds = completionRequest.Country.Split('|');
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                            CompletionStatus = (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                                join appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                                    on employeesMaster.EmployeeCode.ToString() equals appraisals.EmployeeCode.ToString()
                                                join appphases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                                    on appraisals.Id.ToString() equals appphases.Appraisal.ToString()
                                                where strRegionIds.Contains(employeesMaster.HRRegion.RegionName == null ? "" : employeesMaster.HRRegion.RegionName)
                                                && strCompanyIds.Contains(employeesMaster.CompanyName != null ? employeesMaster.CompanyName : "")
                                                && appphases.AppraisalPhase.ToString() == completionRequest.Phase.ToString()
                                                && Convert.ToInt32(appraisals.PerformanceCycle.ToString()) == Convert.ToInt32(cycle.ToString())
                                                    //&& isRegionalHR ? Convert.ToString(employeesMaster.HRRegion) == Convert.ToString(HRRegion) :
                                                    //((isHR || isReviewer || isAppraiser) ?
                                                    //(employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString()) : (appraisals.PerformanceCycle.ToString() == cycle.ToString()))
                                                && ((isTMT || isReviewBoard) ? Convert.ToInt32(appraisals.PerformanceCycle.ToString()) == Convert.ToInt32(cycle.ToString()) :
                                                        (isRegionalHR ? Convert.ToString(employeesMaster.HRRegion.RegionName) == Convert.ToString(HRRegion) :
                                                    //(employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() ||
                                                    //    employeesMaster.DepartmentHeadEmployeeCode.ToString() == spItem["EmployeeCode"].ToString() ||
                                                    //    employeesMaster.ReportingManagerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                                        (employeesMaster.HRBusinessPartnerEmployeeCode.ToString() == spItem["EmployeeCode"].ToString())))
                                                select new CompletionStatusEntity
                                                {
                                                    //EmployeeCode = Convert.ToString(employeesMaster.EmployeeCode),
                                                    Region = Convert.ToString(employeesMaster.HRRegion.RegionName),
                                                    Country = Convert.ToString(employeesMaster.CompanyName),
                                                    Area = Convert.ToString(employeesMaster.Area),
                                                    OU = Convert.ToString(employeesMaster.OrganizationUnit),
                                                    //HRBusinessPartnerCode = Convert.ToString(employeesMaster.HRBusinessPartnerEmployeeCode),
                                                    //AppraiserCode = Convert.ToString(employeesMaster.ReportingManagerEmployeeCode),
                                                    //ReviewerCode = Convert.ToString(employeesMaster.DepartmentHeadEmployeeCode),
                                                    NoOfAppraisees = a = (from app in PMSDataContext.Appraisals.AsEnumerable()
                                                                          join appPhases in PMSDataContext.AppraisalPhases.AsEnumerable() on app.Id.ToString() equals appPhases.Appraisal.ToString()
                                                                          join emp in PMSDataContext.EmployeesMaster.AsEnumerable() on app.EmployeeCode.ToString() equals emp.EmployeeCode.ToString()
                                                                          where //app.Field<int>("ID").ToString() == appraisals.Field<int>("ID").ToString()
                                                                              //employeesMaster.EmployeeCode.ToString() == emp.EmployeeCode.ToString() 
                                                                              //&& appPhases.AppraisalPhase.ToString() == completionRequest.Phase.ToString()
                                                                              //&& app.PerformanceCycle.ToString() == cycle.ToString()
                                                                              //select new { x = app.Id }).Count().ToString(),
                                                                              //employeesMaster.EmployeeCode == emp.EmployeeCode &&
                                                                          appPhases.AppraisalPhase == completionRequest.Phase.ToString()
                                                                          && strRegionIds.Contains(emp.HRRegion.RegionName)
                                                                          && strCompanyIds.Contains(emp.CompanyName)
                                                                          && app.PerformanceCycle.ToString() == cycle.ToString()
                                                                          select new { x = app.Id }).Count().ToString(),
                                                    NoOfAppraisalsCompleted = b =
                                                        completionRequest.Phase.ToString() == "H2" ? (from app in PMSDataContext.Appraisals.AsEnumerable()
                                                                                                      join appPhases in PMSDataContext.AppraisalPhases.AsEnumerable() on app.Id.ToString() equals appPhases.Appraisal.ToString()
                                                                                                      join emp in PMSDataContext.EmployeesMaster.AsEnumerable() on app.EmployeeCode.ToString() equals emp.EmployeeCode.ToString()
                                                                                                      where //app.Field<int>("ID").ToString() == appraisals.Field<int>("ID").ToString()
                                                                                                      employeesMaster.EmployeeCode.ToString() == emp.EmployeeCode.ToString() &&
                                                                                                      appPhases.AppraisalPhase.ToString() == completionRequest.Phase.ToString()
                                                                                                      && (app.AppraisalStatus.ToString() == this.GetAppraisalStatus(20))
                                                                                                      && strRegionIds.Contains(emp.HRRegion.RegionName)
                                                                                                      && strCompanyIds.Contains(emp.CompanyName)
                                                                                                      select new { x = app.Id }).Count().ToString() : (from app in PMSDataContext.Appraisals.AsEnumerable()
                                                                                                                                                       join appPhases in PMSDataContext.AppraisalPhases.AsEnumerable() on app.Id.ToString() equals appPhases.Appraisal.ToString()
                                                                                                                                                       join emp in PMSDataContext.EmployeesMaster.AsEnumerable() on app.EmployeeCode.ToString() equals emp.EmployeeCode.ToString()
                                                                                                                                                       where //app.Field<int>("ID").ToString() == appraisals.Field<int>("ID").ToString()
                                                                                                                                                       employeesMaster.EmployeeCode.ToString() == emp.EmployeeCode.ToString() &&
                                                                                                                                                           //appPhases.AppraisalPhase.ToString() == "H2"
                                                                                                                                                       ((appPhases.AppraisalPhase.ToString() == "H2" && !string.IsNullOrEmpty(app.H1GoalSettingStartDate.ToString())) ||
                                                                                                                                                                                               (app.AppraisalStatus.ToString() == "H1 – Completed"))
                                                                                                                                                       && appPhases.Appraisal.ToString() == app.Id.ToString()
                                                                                                                                                       && strRegionIds.Contains(emp.HRRegion.RegionName)
                                                                                                                                                        && strCompanyIds.Contains(emp.CompanyName)
                                                                                                                                                       && app.PerformanceCycle.ToString() == cycle.ToString()
                                                                                                                                                       select new { x = app.Id }).Count().ToString(),
                                                    NoOfAppraisalsPending = (Convert.ToInt32(a) - Convert.ToInt32(b)).ToString(),
                                                    PercentageOfCompletion = (a == "0" ? "0" : (Math.Floor((Convert.ToDecimal(b) / Convert.ToDecimal(a)) * 100)).ToString()),

                                                }).ToList();
                        }

                    }
                }
            });
            CompletionStatusResponse completionResponse = new CompletionStatusResponse();
            completionResponse.DataItems = CompletionStatus;
            return completionResponse;
        }

        public CompletionStatusResponse GetCompletionStatusReport(CompletionStatusRequest completionRequest)
        {
            List<CompletionStatusEntity> CompletionStatus = new List<CompletionStatusEntity>();

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        spItem = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1], web);
                        GetEmployeeRoles(spItem["EmployeeCode"].ToString(), web);
                        SPList appraisalStatus = web.Lists["Appraisal Status"];
                        string a, b;
                        string x, y;
                        //string strCountry, strNoOfAppraises, strOU, strArea, strApprisalCompleted;
                        //strRegion = HRRegion.ToString();
                        string[] strRegionIds = completionRequest.Region.Split('|');
                        string[] strCompanyIds = completionRequest.Country.Split('|');
                        SPList appraisalList = web.Lists["Appraisals"];
                        SPQuery ospAppraisalQuery = new SPQuery();
                        DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
                        SPList empMastersList = web.Lists["Employees Master"];
                        SPQuery ospEmpMastersQuery = new SPQuery();
                        DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
                        SPList appPhasesList = web.Lists["Appraisal Phases"];
                        SPQuery appPhasesQuery = new SPQuery();
                        DataTable dtAppPhases = appPhasesList.GetItems(appPhasesQuery).GetDataTable();
                        ////isRegionalHR = false; // for testing purpose need to be commented later.
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            double? cycle = (from perCycles in PMSDataContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                            CompletionStatus = (from employeesMaster in dtEmpMasters.AsEnumerable().AsEnumerable()
                                                join appraisals in dtAppraisal.AsEnumerable()
                                                    on employeesMaster.Field<double?>("EmployeeCode").ToString() equals appraisals.Field<string>("appEmployeeCode").ToString()
                                                //join appphases in dtAppPhases.AsEnumerable()
                                                //    on appraisals.Field<int>("ID").ToString() equals appphases.Field<double?>("aphAppraisalId").ToString()
                                                where strRegionIds.Contains(employeesMaster.Field<string>("HRRegion") == null ? "" : employeesMaster.Field<string>("HRRegion"))
                                                && strCompanyIds.Contains(employeesMaster.Field<string>("CompanyName") != null ? employeesMaster.Field<string>("CompanyName") : "")
                                                    //&& appphases.Field<string>("aphAppraisalPhase").ToString() == completionRequest.Phase.ToString()
                                                && appraisals.Field<double?>("appPerformanceCycle").ToString() == cycle.ToString()
                                                    //&& isRegionalHR ? Convert.ToString(employeesMaster.Field<string>("HRRegion")) == Convert.ToString(HRRegion) :
                                                    //((isHR || isReviewer || isAppraiser) ?
                                                    //(employeesMaster.Field<double?>("HRBusinessPartnerEmployeeCode").ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.Field<double?>("DepartmentHeadEmployeeCode").ToString() == spItem["EmployeeCode"].ToString() || employeesMaster.Field<double?>("ReportingManagerEmployeeCode").ToString() == spItem["EmployeeCode"].ToString()) : (appraisals.Field<double?>("appPerformanceCycle").ToString() == cycle.ToString()))
                                                && ((isTMT || isReviewBoard) ? Convert.ToInt32(appraisals.Field<double?>("appPerformanceCycle").ToString()) == Convert.ToInt32(cycle.ToString()) :
                                                        (isRegionalHR ? Convert.ToString(employeesMaster.Field<string>("HRRegion")) == Convert.ToString(HRRegion) :
                                                    //(employeesMaster.Field<double?>("HRBusinessPartnerEmployeeCode").ToString() == spItem["EmployeeCode"].ToString() ||
                                                    //    employeesMaster.Field<double?>("DepartmentHeadEmployeeCode").ToString() == spItem["EmployeeCode"].ToString() ||
                                                    //    employeesMaster.Field<double?>("ReportingManagerEmployeeCode").ToString() == spItem["EmployeeCode"].ToString())))
                                                        (employeesMaster.Field<double?>("HRBusinessPartnerEmployeeCode").ToString() == spItem["EmployeeCode"].ToString())))
                                                select new CompletionStatusEntity
                                                {
                                                    EmployeeCode = Convert.ToString(employeesMaster.Field<double?>("EmployeeCode").ToString() == null ? 0 : employeesMaster.Field<double?>("EmployeeCode")),
                                                    Region = Convert.ToString(employeesMaster.Field<string>("HRRegion") == null ? "" : employeesMaster.Field<string>("HRRegion")),
                                                    Country = Convert.ToString(employeesMaster.Field<string>("CompanyName") == null ? "" : employeesMaster.Field<string>("CompanyName")),
                                                    Area = Convert.ToString(employeesMaster.Field<string>("Area")),
                                                    OU = Convert.ToString(employeesMaster.Field<string>("OrganizationUnit")),
                                                    //HRBusinessPartnerCode = Convert.ToString(employeesMaster.Field<double?>("HRBusinessPartnerEmployeeCode")), 
                                                    //AppraiserCode = Convert.ToString(employeesMaster.Field<double?>("ReportingManagerEmployeeCode")),
                                                    //ReviewerCode = Convert.ToString(employeesMaster.Field<double?>("DepartmentHeadEmployeeCode")),
                                                    NoOfAppraisees = a = (from app in dtAppraisal.AsEnumerable()
                                                                          //join appPhases in dtAppPhases.AsEnumerable() on app.Field<int>("ID").ToString() equals appPhases.Field<double?>("aphAppraisalId").ToString()
                                                                          join emp in dtEmpMasters.AsEnumerable() on app.Field<string>("appEmployeeCode").ToString() equals emp.Field<double?>("EmployeeCode").ToString()
                                                                          where app.Field<int>("ID").ToString() == appraisals.Field<int>("ID").ToString() &&
                                                                          employeesMaster.Field<double?>("EmployeeCode").ToString() == emp.Field<double?>("EmployeeCode").ToString() &&
                                                                              //appPhases.Field<string>("aphAppraisalPhase").ToString() == completionRequest.Phase.ToString()
                                                                              //&& emp.Field<string>("HRRegion").ToString() == employeesMaster.Field<string>("HRRegion").ToString()
                                                                              //&& emp.Field<string>("CompanyName").ToString() == employeesMaster.Field<string>("CompanyName").ToString()
                                                                          app.Field<double?>("appPerformanceCycle").ToString() == cycle.ToString()
                                                                          && app.Field<string>("appDeactivated") != "Yes"
                                                                          select new { x = app.Field<int>("ID") }).Distinct().Count().ToString(),
                                                    NoOfAppraisalsCompleted = b = completionRequest.Phase.ToString() == "H2" ? (from app in dtAppraisal.AsEnumerable()
                                                                                                                                join appPhases in dtAppPhases.AsEnumerable() on app.Field<int>("ID").ToString() equals appPhases.Field<double?>("aphAppraisalId").ToString()
                                                                                                                                join emp in dtEmpMasters.AsEnumerable() on app.Field<string>("appEmployeeCode").ToString() equals emp.Field<double?>("EmployeeCode").ToString()
                                                                                                                                where
                                                                                                                                employeesMaster.Field<double?>("EmployeeCode").ToString() == emp.Field<double?>("EmployeeCode").ToString() &&
                                                                                                                                appPhases.Field<string>("aphAppraisalPhase").ToString() == completionRequest.Phase.ToString()
                                                                                                                                && (app.Field<string>("appAppraisalStatus").ToString() == "H2 – Completed")//this.GetAppraisalStatus(20))
                                                                                                                                && emp.Field<string>("HRRegion").ToString() == employeesMaster.Field<string>("HRRegion").ToString()
                                                                                                                                && emp.Field<string>("CompanyName").ToString() == employeesMaster.Field<string>("CompanyName").ToString() 
                                                                                                                                && app.Field<double?>("appPerformanceCycle").ToString() == cycle.ToString()// added by jhansi on 9/14/2013(showing  wrong count of noofappraisees for H2 if there are no users for the latest cycle)
                                                                                                                                select new { x = app.Field<int>("ID") }).Distinct().Count().ToString() : (from app in dtAppraisal.AsEnumerable()
                                                                                                                                                                                                          join appPhases in dtAppPhases.AsEnumerable() on app.Field<int>("ID").ToString() equals appPhases.Field<double?>("aphAppraisalId").ToString()
                                                                                                                                                                                                          join emp in dtEmpMasters.AsEnumerable() on app.Field<string>("appEmployeeCode").ToString() equals emp.Field<double?>("EmployeeCode").ToString()
                                                                                                                                                                                                          where
                                                                                                                                                                                                          employeesMaster.Field<double?>("EmployeeCode").ToString() == emp.Field<double?>("EmployeeCode").ToString() &&
                                                                                                                                                                                                          ((appPhases.Field<string>("aphAppraisalPhase").ToString() == "H2" && !string.IsNullOrEmpty(app.Field<DateTime?>("appH1AppraisalEvaluationStartDat") == null ? "" : app.Field<DateTime>("appH1AppraisalEvaluationStartDat").ToString())) ||
                                                                                                                                                                                                          (app.Field<string>("appAppraisalStatus").ToString() == "H1 – Completed"))
                                                                                                                                                                                                          && Convert.ToString(appPhases.Field<double?>("aphAppraisalId")) ==Convert.ToString(app.Field<int>("ID"))
                                                                                                                                                                                                          && Convert.ToString(emp.Field<string>("CompanyName"))==Convert.ToString(employeesMaster.Field<string>("CompanyName"))
                                                                                                                                                                                                          && Convert.ToString(app.Field<double?>("appPerformanceCycle")) == cycle.ToString()
                                                                                                                                                                                                          select new { x = app.Field<int>("ID") }).Distinct().Count().ToString(),
                                                    NoOfAppraisalsPending = (Convert.ToInt32(a) - Convert.ToInt32(b)).ToString(),
                                                    PercentageOfCompletion = (a == "0" ? "0" : (Math.Floor((Convert.ToDecimal(b) / Convert.ToDecimal(a)) * 100)).ToString()),

                                                }).Distinct().ToList();
                        }

                    }
                }
            });
            CompletionStatusResponse completionResponse = new CompletionStatusResponse();
            completionResponse.DataItems = CompletionStatus;
            return completionResponse;
        }

        #region AppraisalData

        public AppraisalDataResponse GetAppraisalData(AppraisalAllDataRequest Request)
        {
            List<AppraisalEntity> appEntity = new List<AppraisalEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite site = new SPSite(url))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            //SPList appraisalList = web.Lists["Appraisals"];
                            //SPQuery ospAppraisalQuery = new SPQuery();
                            //DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
                            //SPList empMastersList = web.Lists["Employees Master"];
                            //SPQuery ospEmpMastersQuery = new SPQuery();
                            //DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
                            //appEntity = (from employeesMaster in dtEmpMasters.AsEnumerable()
                            //             join appraisals in dtAppraisal.AsEnumerable()
                            //                  on employeesMaster.Field<double>("EmployeeCode").ToString() equals appraisals.Field<string>("appEmployeeCode").ToString()
                            //             where appraisals.Field<double>("appPerformanceCycle").ToString() == Request.performanceCycle.ToString()
                            //             && appraisals.Field<string>("appEmployeeCode").ToString() == Request.employeeCode.ToString()
                            //             select new AppraisalEntity
                            //             {
                            //                 appriaseName = Convert.ToString(employeesMaster.Field<string>("EmployeeName")),
                            //                 appriaserName = Convert.ToString(employeesMaster.Field<string>("ReportingManagerName")),
                            //                 reviewerName = Convert.ToString(employeesMaster.Field<string>("DepartmentHeadName")),
                            //                 hrBusinessPartnerName = Convert.ToString(employeesMaster.Field<string>("HRBusinessPartnerName")),
                            //                 region = Convert.ToString(employeesMaster.Field<string>("HRRegion")),
                            //                 company = Convert.ToString(employeesMaster.Field<string>("CompanyName")),
                            //                 organization = Convert.ToString(employeesMaster.Field<string>("OrganizationUnit")),
                            //                 dueDate = employeesMaster.Field<DateTime?>("ConfirmationDate") == null ? "" : Convert.ToDateTime(employeesMaster.Field<DateTime?>("ConfirmationDate")).ToString("dd-MMM-yyyy"),
                            //                 confirmationDuedate = employeesMaster.Field<DateTime?>("confirmationDuedate") == null ? "" : Convert.ToDateTime(employeesMaster.Field<DateTime?>("confirmationDuedate")).ToString("dd-MMM-yyyy"),
                            //                 appriaseCode = Convert.ToString(employeesMaster.Field<double>("EmployeeCode")),
                            //                 appriaserCode = Convert.ToString(employeesMaster.Field<double>("ReportingManagerEmployeeCode")),
                            //                 reviewerCode = Convert.ToString(employeesMaster.Field<double>("DepartmentHeadEmployeeCode")),
                            //                 hrBusinessPartnerCode = Convert.ToString(employeesMaster.Field<double>("HRBusinessPartnerEmployeeCode")),
                            //                 position = Convert.ToString(employeesMaster.Field<string>("Position")),
                            //                 hireDate = employeesMaster.Field<DateTime?>("HireDate") == null ? "" : Convert.ToDateTime(employeesMaster.Field<DateTime?>("HireDate")).ToString("dd-MMM-yyyy"),
                            //             }).ToList();

                            appEntity = (from employeesMaster in PMSDataContext.EmployeesMaster.AsEnumerable()
                                         join appraisals in PMSDataContext.Appraisals.AsEnumerable()
                                              on employeesMaster.EmployeeCode.ToString() equals appraisals.EmployeeCode.ToString()
                                         where appraisals.PerformanceCycle.ToString() == Request.performanceCycle.ToString()
                                         && appraisals.EmployeeCode.ToString() == Request.employeeCode.ToString()
                                         select new AppraisalEntity
                                         {
                                             appriaseName = Convert.ToString(employeesMaster.EmployeeName),
                                             appriaserName = Convert.ToString(employeesMaster.ReportingManagerName),
                                             reviewerName = Convert.ToString(employeesMaster.DepartmentHeadName),
                                             hrBusinessPartnerName = Convert.ToString(employeesMaster.HRBusinessPartnerName),
                                             region = Convert.ToString(employeesMaster.HRRegion.RegionName),
                                             company = Convert.ToString(employeesMaster.CompanyName),
                                             organization = Convert.ToString(employeesMaster.OrganizationUnit),
                                             dueDate = employeesMaster.ConfirmationDate == null ? "" : Convert.ToDateTime(employeesMaster.ConfirmationDate).ToString("dd-MMM-yyyy"),
                                             confirmationDuedate = employeesMaster.ConfirmationDueDate == null ? "" : Convert.ToDateTime(employeesMaster.ConfirmationDueDate).ToString("dd-MMM-yyyy"),
                                             appriaseCode = Convert.ToString(employeesMaster.EmployeeCode),
                                             appriaserCode = Convert.ToString(employeesMaster.ReportingManagerEmployeeCode),
                                             reviewerCode = Convert.ToString(employeesMaster.DepartmentHeadEmployeeCode),
                                             hrBusinessPartnerCode = Convert.ToString(employeesMaster.HRBusinessPartnerEmployeeCode),
                                             position = Convert.ToString(employeesMaster.Position),
                                             hireDate = employeesMaster.HireDate == null ? "" : Convert.ToDateTime(employeesMaster.HireDate).ToString("dd-MMM-yyyy"),
                                         }).ToList();
                        }
                    }
                }
            });
            AppraisalDataResponse appResponse = new AppraisalDataResponse();
            appResponse.DataItems = appEntity;
            return appResponse;

        }

        public GoalsResponse GetAppraiseeH1Goals(AppraisalAllDataRequest goalRequest)
        {
            List<GoalsEntity> H1GoalsList = new List<GoalsEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(goalRequest.employeeCode, goalRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[1]))
                            {
                                H1GoalsList = (from appGoals in PMSDataContext.AppraisalGoals
                                               where appGoals.AppraisalId == Convert.ToInt32(apprisalData[0])
                                               && appGoals.AppraisalPhase == Convert.ToInt32(apprisalData[1])
                                               ////orderby appGoals.Goal, appGoals.GoalCategory
                                               select new GoalsEntity
                                               {
                                                   appraiseeComments = Convert.ToString(appGoals.AppraiseeLatestComments),
                                                   appraiserComments = Convert.ToString(appGoals.AppraiserLatestComments),
                                                   dueDate = appGoals.DueDate.ToString(),
                                                   evalution = appGoals.Evaluation.ToString(),
                                                   goal = appGoals.Goal,
                                                   goalCategory = appGoals.GoalCategory,
                                                   goalDescription = appGoals.GoalDescription,
                                                   reviewerComments = appGoals.ReviewerLatestComments,
                                                   score =appGoals.Score==null ?"0" : appGoals.Score.ToString(),
                                                   weightage = Convert.ToString(appGoals.Weightage),
                                               }).ToList();

                            }

                        }
                    }
                }
            });
            GoalsResponse H1GoalsResponse = new GoalsResponse();
            H1GoalsResponse.DataItems = H1GoalsList;
            return H1GoalsResponse;
        }

        public GoalsResponse GetAppraiseeH2Goals(AppraisalAllDataRequest goalRequest)
        {
            List<GoalsEntity> H2GoalsList = new List<GoalsEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(goalRequest.employeeCode, goalRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[2]))
                            {
                                H2GoalsList = (from appGoals in PMSDataContext.AppraisalGoals
                                               where appGoals.AppraisalId == Convert.ToInt32(apprisalData[0])
                                               && appGoals.AppraisalPhase == Convert.ToInt32(apprisalData[2])
                                               ////orderby appGoals.Goal, appGoals.GoalCategory
                                               select new GoalsEntity
                                               {
                                                   appraiseeComments = Convert.ToString(appGoals.AppraiseeLatestComments),
                                                   appraiserComments = Convert.ToString(appGoals.AppraiserLatestComments),
                                                   dueDate = appGoals.DueDate.ToString(),
                                                   evalution = appGoals.Evaluation.ToString(),
                                                   goal = appGoals.Goal,
                                                   goalCategory = appGoals.GoalCategory,
                                                   goalDescription = appGoals.GoalDescription,
                                                   reviewerComments = appGoals.ReviewerLatestComments,
                                                   score = appGoals.Score==null ?"0" : appGoals.Score.ToString(),
                                                   weightage = Convert.ToString(appGoals.Weightage),
                                               }).ToList();

                            }

                        }
                    }
                }
            });
            GoalsResponse H1GoalsResponse = new GoalsResponse();
            H1GoalsResponse.DataItems = H2GoalsList;
            return H1GoalsResponse;
        }

        public CompetenciesResponse GetAppraiseeH1Competencies(AppraisalAllDataRequest competencyRequest)
        {
            List<CompetenciesEntity> H1CompetenciesList = new List<CompetenciesEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(competencyRequest.employeeCode, competencyRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[1]))
                            {
                                H1CompetenciesList = (from appCompetency in PMSDataContext.AppraisalCompetencies.AsEnumerable()
                                                      where Convert.ToString(appCompetency.AppraisalId) == Convert.ToString(apprisalData[0])
                                                      && Convert.ToString(appCompetency.AppraisalPhaseId) == Convert.ToString(apprisalData[1])
                                                      orderby appCompetency.Competency
                                                      select new CompetenciesEntity
                                                      {
                                                          appraiseeComments = Convert.ToString(appCompetency.AppraiseeLatestComments),
                                                          appraiserComments = Convert.ToString(appCompetency.AppraiserLatestComments),
                                                          competency = appCompetency.Competency,
                                                          description = appCompetency.Description,
                                                          expectedResult = appCompetency.ExpectedResult,
                                                          rating = appCompetency.Rating,
                                                          reviewerComments = appCompetency.ReviewerLatestComments,
                                                      }).ToList();
                            }
                        }
                    }
                }
            });
            CompetenciesResponse response = new CompetenciesResponse();
            response.DataItems = H1CompetenciesList;
            return response;
        }

        public CompetenciesResponse GetAppraiseeH2Competencies(AppraisalAllDataRequest competencyRequest)
        {
            List<CompetenciesEntity> H2CompetenciesList = new List<CompetenciesEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(competencyRequest.employeeCode, competencyRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[2]))
                            {
                                H2CompetenciesList = (from appCompetency in PMSDataContext.AppraisalCompetencies.AsEnumerable()
                                                      where Convert.ToString(appCompetency.AppraisalId) == Convert.ToString(apprisalData[0])
                                                      && Convert.ToString(appCompetency.AppraisalPhaseId) == Convert.ToString(apprisalData[2])
                                                      orderby appCompetency.Competency
                                                      select new CompetenciesEntity
                                                      {
                                                          appraiseeComments = Convert.ToString(appCompetency.AppraiseeLatestComments),
                                                          appraiserComments = Convert.ToString(appCompetency.AppraiserLatestComments),
                                                          competency = appCompetency.Competency,
                                                          description = appCompetency.Description,
                                                          expectedResult = appCompetency.ExpectedResult,
                                                          rating = appCompetency.Rating,
                                                          reviewerComments = appCompetency.ReviewerLatestComments,
                                                      }).ToList();
                            }
                        }
                    }
                }
            });
            CompetenciesResponse response = new CompetenciesResponse();
            response.DataItems = H2CompetenciesList;
            return response;
        }

        public DevelopmentMeasuresResponse GetAppraiseeH1DevelopmentMeasures(AppraisalAllDataRequest developmentMeasuresRequest)
        {
            List<DevelopmentMeasuresEntity> H1DevMeasuresList = new List<DevelopmentMeasuresEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(developmentMeasuresRequest.employeeCode, developmentMeasuresRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[1]))
                            {
                                H1DevMeasuresList = (from appDevMeasures in PMSDataContext.AppraisalDevelopmentMeasures
                                                     where appDevMeasures.AppraisalID == Convert.ToInt32(apprisalData[0])
                                                     && appDevMeasures.AppraisalPhaseID.ToString() == apprisalData[1]
                                                     select new DevelopmentMeasuresEntity
                                                     {
                                                         H1AppraiseeComments = Convert.ToString(appDevMeasures.H1AppraiseeComments),
                                                         H1AppraiserComments = Convert.ToString(appDevMeasures.H1AppraiserComments),
                                                         H2AppraiseeComments = Convert.ToString(appDevMeasures.H2AppraiseeComments),
                                                         H2AppraiserComments = Convert.ToString(appDevMeasures.H2AppraiserComments),
                                                         nextSteps = Convert.ToString(appDevMeasures.NextSteps),
                                                         when = Convert.ToString(appDevMeasures.When),
                                                         what = Convert.ToString(appDevMeasures.What),
                                                     }).ToList();
                            }
                        }
                    }
                }
            });
            DevelopmentMeasuresResponse response = new DevelopmentMeasuresResponse();
            response.DataItems = H1DevMeasuresList;
            return response;
        }

        public DevelopmentMeasuresResponse GetAppraiseeH2DevelopmentMeasures(AppraisalAllDataRequest developmentMeasuresRequest)
        {
            List<DevelopmentMeasuresEntity> H2DevMeasuresList = new List<DevelopmentMeasuresEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(developmentMeasuresRequest.employeeCode, developmentMeasuresRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[2]))
                            {
                                H2DevMeasuresList = (from appDevMeasures in PMSDataContext.AppraisalDevelopmentMeasures
                                                     where appDevMeasures.AppraisalID == Convert.ToInt32(apprisalData[0])
                                                     && appDevMeasures.AppraisalPhaseID.ToString() == apprisalData[2]
                                                     select new DevelopmentMeasuresEntity
                                                     {
                                                         H1AppraiseeComments = Convert.ToString(appDevMeasures.H1AppraiseeComments),
                                                         H1AppraiserComments = Convert.ToString(appDevMeasures.H1AppraiserComments),
                                                         H2AppraiseeComments = Convert.ToString(appDevMeasures.H2AppraiseeComments),
                                                         H2AppraiserComments = Convert.ToString(appDevMeasures.H2AppraiserComments),
                                                         nextSteps = Convert.ToString(appDevMeasures.NextSteps),
                                                         when = Convert.ToString(appDevMeasures.When),
                                                         what = Convert.ToString(appDevMeasures.What),
                                                     }).ToList();
                            }
                        }
                    }
                }
            });
            DevelopmentMeasuresResponse response = new DevelopmentMeasuresResponse();
            response.DataItems = H2DevMeasuresList;
            return response;
        }

        public AppraisalPIPResponse GetAppraiseeH1PIPData(AppraisalAllDataRequest pipRequest)
        {
            List<AppraisalPIPEntity> H1PIPList = new List<AppraisalPIPEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(pipRequest.employeeCode, pipRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[1]))
                            {
                                H1PIPList = (from appPIP in PMSDataContext.PIP
                                             where appPIP.AppraisalID == Convert.ToInt32(apprisalData[0])
                                                     && appPIP.AppraisalPhaseID.ToString() == apprisalData[1]
                                             select new AppraisalPIPEntity
                                             {
                                                 performanceIssue = appPIP.PerformanceIssue,
                                                 expectedAchievement = appPIP.ExpectedAchivement,
                                                 timeFrame = appPIP.TimeFrame,
                                                 midTermActualResult = appPIP.MidTermActualResult,
                                                 midTermAppraisersAssessment = appPIP.MidTermAppraisersAssessment,
                                                 finalActualResult = appPIP.FinalAcutualResult,
                                                 finalTermAppraisersAssessment = appPIP.FinalAppraisersAssesment
                                             }).ToList();
                            }
                        }
                    }
                }
            });
            AppraisalPIPResponse response = new AppraisalPIPResponse();
            response.DataItems = H1PIPList;
            return response;
        }

        public AppraisalPIPResponse GetAppraiseeH2PIPData(AppraisalAllDataRequest pipRequest)
        {
            List<AppraisalPIPEntity> H2PIPList = new List<AppraisalPIPEntity>();
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite osite = new SPSite(url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        string[] apprisalData = getAppraisalIdAndPhaseIds(pipRequest.employeeCode, pipRequest.performanceCycle, web);
                        using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                        {
                            if (!string.IsNullOrEmpty(apprisalData[0]) && !string.IsNullOrEmpty(apprisalData[2]))
                            {
                                H2PIPList = (from appPIP in PMSDataContext.PIP
                                             where appPIP.AppraisalID == Convert.ToInt32(apprisalData[0])
                                                     && appPIP.AppraisalPhaseID.ToString() == apprisalData[2]
                                             orderby appPIP.PerformanceIssue
                                             select new AppraisalPIPEntity
                                             {
                                                 performanceIssue = appPIP.PerformanceIssue,
                                                 expectedAchievement = appPIP.ExpectedAchivement,
                                                 timeFrame = appPIP.TimeFrame,
                                                 midTermActualResult = appPIP.MidTermActualResult,
                                                 midTermAppraisersAssessment = appPIP.MidTermAppraisersAssessment,
                                                 finalActualResult = appPIP.FinalAcutualResult,
                                                 finalTermAppraisersAssessment = appPIP.FinalAppraisersAssesment
                                             }).ToList();
                            }
                        }
                    }
                }
            });
            AppraisalPIPResponse response = new AppraisalPIPResponse();
            response.DataItems = H2PIPList;
            return response;
        }

        public string[] getAppraisalIdAndPhaseIds(string employeeCode, string performancecycle, SPWeb web)
        {
            string[] appraisalData = new string[3];
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (VFSPMSEntitiesDataContext pmsContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    var appId = from appraisal in pmsContext.Appraisals
                                where appraisal.EmployeeCode.ToString() == employeeCode.ToString() && appraisal.PerformanceCycle.ToString() == performancecycle.ToString()
                                select appraisal.Id;
                    if (appId != null)
                    {
                        appraisalData[0] = Convert.ToString(appId.FirstOrDefault());
                        var H1PhaseId = from appPhases in pmsContext.AppraisalPhases
                                        where appPhases.Appraisal.ToString() == appraisalData[0] && appPhases.AppraisalPhase.ToString() == "H1"
                                        select appPhases.Id;
                        var H2PhaseId = from appPhases in pmsContext.AppraisalPhases
                                        where appPhases.Appraisal.ToString() == appraisalData[0] && appPhases.AppraisalPhase.ToString() == "H2"
                                        select appPhases.Id;
                        appraisalData[1] = Convert.ToString(H1PhaseId.FirstOrDefault());
                        appraisalData[2] = Convert.ToString(H2PhaseId.FirstOrDefault());
                    }
                }
            });
            return appraisalData;
        }
        #endregion
    }
}
