using System;
using System.Collections.Specialized;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System.IO;
using System.Linq;
using Microsoft.SharePoint.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.ServiceLocation;
using Microsoft.Practices.SharePoint.Common.Logging;
using System.Collections.Generic;

namespace VFS.PMS.ImportDataFromSAPFiles
{
    /// <summary>
    /// 
    /// </summary>
    public class SAPDataPortJob : SPJobDefinition
    {
        VFSPMSEntitiesDataContext PMSDataContext;
        public const string jobName = "VFS PMS SAP Data Import Timer job";
        string dataFilePath = "", dataFileSuffix = "", ADLoginUrl = "";
        string mySiteUrl = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="SAPDataPortJob"/> class.
        /// </summary>
        public SAPDataPortJob()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SAPDataPortJob"/> class.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="service">The service.</param>
        /// <param name="server">The server.</param>
        /// <param name="targetType">Type of the target.</param>
        public SAPDataPortJob(string jobName, SPService service, SPServer server, SPJobLockType targetType)
            : base(jobName, service, server, targetType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SAPDataPortJob"/> class.
        /// </summary>
        /// <param name="webApplication">The web application.</param>
        public SAPDataPortJob(SPWebApplication webApplication)
            : base(jobName, webApplication, null, SPJobLockType.Job)
        {
            Title = "VFS PMS SAP Data Import Timer job";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SAPDataPortJob"/> class.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="webApplication">The web application.</param>
        public SAPDataPortJob(string jobName, SPWebApplication webApplication)
            : base(jobName, webApplication, null, SPJobLockType.ContentDatabase)
        {
            this.Title = "VFS PMS SAP Data Import Timer job";
        }
        /// <summary>
        /// Executes the job definition.
        /// </summary>
        /// <param name="targetInstanceId">For target types of <see cref="T:Microsoft.SharePoint.Administration.SPContentDatabase" /> this is the database ID of the content database being processed by the running job. This value is Guid.Empty for all other target types.</param>
        /// <exception cref="System.Exception">Data path not configured. Please configure the Data files path</exception>
        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Properties["mySiteUrl"].ToString()))
                {
                    mySiteUrl = this.Properties["mySiteUrl"].ToString();
                }

                if (!string.IsNullOrEmpty(mySiteUrl))
                {
                    using (SPSite mySite = new SPSite(mySiteUrl))
                    {
                        using (SPWeb mySiteWeb = mySite.OpenWeb())
                        {
                            string weburl = mySiteWeb.Url;
                            LogHandler.LogVerbose("Populating PMS Master Data - StartTime " + DateTime.Now.ToString());
                            using (PMSDataContext = new VFSPMSEntitiesDataContext(weburl))
                            {
                                var itemToUpdate = (from r in PMSDataContext.PMSDataFileLocations where r.Title.ToString().Trim().ToUpper() == "SAP DATA" select r).FirstOrDefault();
                                if (itemToUpdate != null)
                                {
                                    dataFilePath = itemToUpdate.FileLocation;
                                    dataFileSuffix = String.Concat(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), ConstValues.DATAFILE_EXTENSION);
                                }
                                itemToUpdate = (from r in PMSDataContext.PMSDataFileLocations where r.Title.ToString().Trim().ToUpper() == "EMPLOYEE LOGIN AD MAPPING URL" select r).FirstOrDefault();
                                if (itemToUpdate != null)
                                    ADLoginUrl = itemToUpdate.FileLocation;
                            }
                            if (!string.IsNullOrEmpty(dataFilePath) && Directory.Exists(dataFilePath))
                            {
                                LogHandler.LogVerbose("Populating PMS Master Data - Regioins");
                                PopulateRegions(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Companies");
                                PopulateCompanies(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Area");
                                PopulateAreas(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Sub Areas");
                                PopulateSubAreas(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Employee Groups");
                                PopulateEmployeeGroups(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Employee Sub Groups");
                                PopulateEmployeeSubGroups(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Postions");
                                PopulatePositions(weburl);
                                LogHandler.LogVerbose("Populating PMS Master Data - Organization Units");
                                PopulateOUs(weburl);

                                if (CheckADDetailAvailability())
                                {
                                    LogHandler.LogVerbose("Populating PMS Master Data - Employees");
                                    PopulateEmployees(weburl);
                                }
                                else
                                    throw new Exception("AD Login URL not found. Please configure the ADLoginUrl Path");
                            }
                            else
                            {
                                throw new Exception("Data path not configured. Please configure the Data files path");
                            }
                        }
                        LogHandler.LogVerbose("Populating PMS Master Data - End Time " + DateTime.Now.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, ex.Message);
            }

        }

        /// <summary>
        /// Populates the areas.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateAreas(string webUrl)
        {
            string areasDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_AREAS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(areasDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            PersonnelArea = data[0],
                            PersonnelAreaText = data[1],
                            CompanyCode = data[2],
                            Region = data[3]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;

            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.PersonnelArea)) continue;
                    var itemToUpdate = (from r in PMSDataContext.Areas where r.PersonnelAreaCode == item.PersonnelArea select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Area Code: '" + item.PersonnelArea + "'");
                        AreasItem itemToInsert = new AreasItem()
                        {
                            PersonnelAreaCode = item.PersonnelArea,
                            Title = item.PersonnelAreaText,
                            PersonnelAreaText = item.PersonnelAreaText,
                            RegionName = (from r in PMSDataContext.Regions where r.RegionName == item.Region select r).FirstOrDefault(),
                            CompanyCode = (from r in PMSDataContext.Companies where r.CompanyCode.ToString() == item.CompanyCode select r).FirstOrDefault()
                        };
                        PMSDataContext.Areas.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Area Code: '" + item.PersonnelArea + "'");
                        itemToUpdate.PersonnelAreaCode = item.PersonnelArea;
                        itemToUpdate.Title = item.PersonnelAreaText;
                        itemToUpdate.PersonnelAreaText = item.PersonnelAreaText;
                        itemToUpdate.RegionName = (from r in PMSDataContext.Regions where r.RegionName == item.Region select r).FirstOrDefault();
                        itemToUpdate.CompanyCode = (from r in PMSDataContext.Companies where r.CompanyCode.ToString() == item.CompanyCode select r).FirstOrDefault();
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Area with Area Code: " + item.PersonnelArea);
                        //LogHandler.LogError(ex, "Error in Populate Areas");
                    }
                }
                LogHandler.LogVerbose("Total No of Areas processed: " + i.ToString());
            }
        }

        /// <summary>
        /// Populates the sub areas.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateSubAreas(string webUrl)
        {
            string subAreasDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_SUBAREAS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(subAreasDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            PersonnelArea = data[0],
                            PSA = data[1],
                            PSAText = data[2]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;

            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.PSA)) continue;
                    var itemToUpdate = (from r in PMSDataContext.SubAreas where r.PersonnelSubAreaCode == item.PSA select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Sub Area Code: '" + item.PSA + "'");
                        SubAreasItem itemToInsert = new SubAreasItem()
                        {
                            PersonnelAreaCode = (from r in PMSDataContext.Areas where r.PersonnelAreaCode == item.PersonnelArea select r).FirstOrDefault(),
                            Title = item.PSAText,
                            SubPersonnelAreaText = item.PSAText,
                            PersonnelSubAreaCode = item.PSA
                        };
                        PMSDataContext.SubAreas.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Sub Area Code: '" + item.PSA + "'");
                        itemToUpdate.PersonnelAreaCode = (from r in PMSDataContext.Areas where r.PersonnelAreaCode == item.PersonnelArea select r).FirstOrDefault();
                        itemToUpdate.Title = item.PSAText;
                        itemToUpdate.SubPersonnelAreaText = item.PSAText;
                        itemToUpdate.PersonnelSubAreaCode = item.PSA;
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Sub Area with Sub Area Code: " + item.PSA);
                        //LogHandler.LogError(ex, "Error in Populate Sub Areas");
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Sub Areas processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the positions.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulatePositions(string webUrl)
        {
            string positionsDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_POSITIONS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(positionsDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            PositionCode = data[0],
                            PositionShortText = data[1],
                            PostionText = data[2]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;

            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.PositionCode)) continue;
                    var itemToUpdate = (from r in PMSDataContext.Positions where r.PositionCode.ToString() == item.PositionCode select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Postion Code: '" + item.PositionCode + "'");
                        PositionsItem itemToInsert = new PositionsItem()
                        {
                            PositionCode = Convert.ToInt32(item.PositionCode),
                            PositionLongText = item.PostionText,
                            Title = item.PostionText,
                            PositionShortText = item.PositionShortText
                        };
                        PMSDataContext.Positions.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Postion Code: '" + item.PositionCode + "'");
                        itemToUpdate.PositionCode = Convert.ToInt32(item.PositionCode);
                        itemToUpdate.Title = item.PostionText;
                        itemToUpdate.PositionLongText = item.PostionText;
                        itemToUpdate.PositionShortText = item.PositionShortText;
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Position with Position Code: " + item.PositionCode);
                        //LogHandler.LogError(ex, "Error in Populate Positions");
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Positions processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the O us.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateOUs(string webUrl)
        {
            string ouDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_ORGANIZATIONUNITS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(ouDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            OUCode = data[0],
                            OUShortText = data[1],
                            OULongText = data[2]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;

            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.OUCode)) continue;
                    var itemToUpdate = (from r in PMSDataContext.OrganizationUnits where r.OrganizationUnitCode.ToString() == item.OUCode select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting OU Code: '" + item.OUCode + "'");
                        OrganizationUnitsItem itemToInsert = new OrganizationUnitsItem()
                        {
                            Title = item.OULongText,
                            OrganizationUnitCode = Convert.ToInt32(item.OUCode),
                            OrganizationUnitLongText = item.OULongText,
                            OrganizationUnitShortText = item.OUShortText
                        };
                        PMSDataContext.OrganizationUnits.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating OU Code: '" + item.OUCode + "'");
                        itemToUpdate.OrganizationUnitCode = Convert.ToInt32(item.OUCode);
                        itemToUpdate.Title = item.OULongText;
                        itemToUpdate.OrganizationUnitLongText = item.OULongText;
                        itemToUpdate.OrganizationUnitShortText = item.OUShortText;
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Organization Units with Organization Code: " + item.OUCode);
                        //LogHandler.LogError(ex, "Error in Populate Organization Units");
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Organization Units processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the companies.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateCompanies(string webUrl)
        {
            string companiesDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_COMPANYNAMES + dataFileSuffix);
            string[] allLines = File.ReadAllLines(companiesDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            CompanyCode = data[0],
                            CompanyName = data[1],
                            Region = data[2]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;
            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.CompanyCode)) continue;
                    var itemToUpdate = (from r in PMSDataContext.Companies where r.CompanyCode.ToString() == item.CompanyCode select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Company Code: '" + item.CompanyCode + "'");
                        CompaniesItem itemToInsert = new CompaniesItem()
                        {
                            CompanyName = item.CompanyName,
                            Title = item.CompanyName,
                            CompanyCode = Convert.ToInt32(item.CompanyCode),
                            Region = (from r in PMSDataContext.Regions where r.RegionName == item.Region select r).FirstOrDefault()
                        };
                        PMSDataContext.Companies.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Company Code: '" + item.CompanyCode + "'");
                        itemToUpdate.CompanyCode = Convert.ToInt32(item.CompanyCode);
                        itemToUpdate.Title = item.CompanyName;
                        itemToUpdate.CompanyName = item.CompanyName;
                        itemToUpdate.Region = (from r in PMSDataContext.Regions where r.RegionName == item.Region select r).FirstOrDefault();
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Companies with Company Code: " + item.CompanyCode);
                        //LogHandler.LogError(ex, "Error in Populate Companies");
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Companies processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the regions.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateRegions(string webUrl)
        {
            string regionsDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_REGIONS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(regionsDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            Region = data[0],
                            RegionalHREmpCode = data[1],
                            RegionalHREmpName = data[2]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;

            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.Region)) continue;
                    var itemToUpdate = (from r in PMSDataContext.Regions where r.RegionName == item.Region select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Region: '" + item.Region + "'");
                        RegionsItem itemToInsert = new RegionsItem()
                        {
                            Title = item.Region,
                            RegionName = item.Region
                        };
                        if (!string.IsNullOrEmpty(item.RegionalHREmpCode))
                        {
                            itemToInsert.RegionHREmployeeCode = Convert.ToInt32(item.RegionalHREmpCode);
                            itemToInsert.RegionHRName = item.RegionalHREmpName;
                        }
                        PMSDataContext.Regions.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Region: '" + item.Region + "'");
                        if (!string.IsNullOrEmpty(item.RegionalHREmpCode))
                        {
                            itemToUpdate.RegionHREmployeeCode = Convert.ToInt32(item.RegionalHREmpCode);
                            itemToUpdate.RegionHRName = item.RegionalHREmpName;
                        }
                        itemToUpdate.Title = item.Region;
                        itemToUpdate.RegionName = item.Region;
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the regions Units with region : " + item.Region);
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Regions processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the employee groups.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateEmployeeGroups(string webUrl)
        {
            string empGrpsDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_EMPLOYEEGROUPS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(empGrpsDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            EmpGroupCode = data[0],
                            EmpGroupText = data[1]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;

            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.EmpGroupCode)) continue;
                    var itemToUpdate = (from r in PMSDataContext.EmployeeGroups where r.EmployeeGroupCode == item.EmpGroupCode select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Employee Group Code: '" + item.EmpGroupCode + "'");
                        EmployeeGroupsItem itemToInsert = new EmployeeGroupsItem()
                        {
                            EmployeeGroupCode = item.EmpGroupCode,
                            Title = item.EmpGroupText,
                            EmployeeGroupText = item.EmpGroupText
                        };
                        PMSDataContext.EmployeeGroups.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Employee Group Code: '" + item.EmpGroupCode + "'");
                        itemToUpdate.EmployeeGroupCode = item.EmpGroupCode;
                        itemToUpdate.Title = item.EmpGroupText;
                        itemToUpdate.EmployeeGroupText = item.EmpGroupText;
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Employee Grroup with Emp group Code: " + item.EmpGroupCode);
                        //LogHandler.LogError(ex, "Error in Populate Employee Groups");
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Employee Groups processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the employee sub groups.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        void PopulateEmployeeSubGroups(string webUrl)
        {
            string empsubgrpDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_EMPLOYEESUBGROUPS + dataFileSuffix);
            string[] allLines = File.ReadAllLines(empsubgrpDataFile).Distinct().ToArray();
            var query = from line in allLines
                        let data = line.Split('|')
                        select new
                        {
                            EmpGroupCode = data[0],
                            EmpSubGroupCode = data[1],
                            EmpSubGroupText = data[2]
                        };
            // Now we got all the columns of the first line. Next add them to the  new list Item
            int i = 0;
            foreach (var item in query)
            {
                using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                {
                    if (i == 0) { i++; continue; } i++;
                    if (string.IsNullOrEmpty(item.EmpSubGroupCode)) continue;
                    var itemToUpdate = (from r in PMSDataContext.EmployeeSubGroups where r.EmployeeSubGroupCode == item.EmpSubGroupCode && r.EmployeeGroupCode.EmployeeGroupCode == item.EmpGroupCode select r).FirstOrDefault();
                    if (itemToUpdate == null)
                    {
                        LogHandler.LogVerbose("Inserting Empolyee Sub Group Code: '" + item.EmpSubGroupCode + "'");
                        EmployeeSubGroupsItem itemToInsert = new EmployeeSubGroupsItem()
                        {
                            Title = item.EmpSubGroupText,
                            EmployeeSubGroupCode = item.EmpSubGroupCode,
                            EmployeeSubGroupText = item.EmpSubGroupText,
                            EmployeeGroupCode = (from r in PMSDataContext.EmployeeGroups where r.EmployeeGroupCode == item.EmpGroupCode select r).FirstOrDefault()
                        };
                        PMSDataContext.EmployeeSubGroups.InsertOnSubmit(itemToInsert);
                    }
                    else
                    {
                        LogHandler.LogVerbose("Updating Empolyee Sub Group Code: '" + item.EmpSubGroupCode + "'");
                        itemToUpdate.Title = item.EmpSubGroupText;
                        itemToUpdate.EmployeeSubGroupText = item.EmpSubGroupText;
                        itemToUpdate.EmployeeSubGroupCode = item.EmpSubGroupCode;
                        itemToUpdate.EmployeeGroupCode = (from r in PMSDataContext.EmployeeGroups where r.EmployeeGroupCode == item.EmpGroupCode select r).FirstOrDefault();
                    }
                    try
                    {
                        PMSDataContext.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        LogHandler.LogWarning("Error in porting the Employee Sub Groups with Emp Sub Group Code : " + item.EmpSubGroupCode);
                        //LogHandler.LogError(ex, "Error in Populate Employee Sub Groups");
                    }
                }
            }
            LogHandler.LogVerbose("Total No of Employee Sub Groups processed: " + i.ToString());
        }
        /// <summary>
        /// Populates the employees.
        /// </summary>
        void PopulateEmployees(string webUrl)
        {
            DateTime dt = DateTime.MinValue;
            using (SPSite mySite = new SPSite(ADLoginUrl))
            {
                using (SPWeb mySiteWeb = mySite.OpenWeb())
                {
                    string empDataFile = Path.Combine(dataFilePath, ConstValues.DATAFILE_EMPLOYEEDATA + dataFileSuffix);
                    string[] allLines = File.ReadAllLines(empDataFile).Distinct().ToArray();
                    var query = from line in allLines
                                let data = line.Split('|')
                                select new
                                {
                                    FullName = data[0],
                                    EmployeeCode = data[1],
                                    OUCode = data[2],
                                    OU = data[3],
                                    Position = data[4],
                                    PositionText = data[5],
                                    JoiningDate = data[6],
                                    RMPersonnel = data[7],
                                    RMPersonnelName = data[8],
                                    DeptHead = data[9],
                                    DeptHeadName = data[10],
                                    HRPersonnel = data[11],
                                    HRPersonnelName = data[12],
                                    EmpGroup = data[13],
                                    EmpSubGroup = data[14],
                                    CompanyCode = data[15],
                                    CompanyName = data[16],
                                    Region = data[17],
                                    PersonnelArea = data[18],
                                    PersonnelAreaName = data[19],
                                    PersonnelSubArea = data[20],
                                    PersonnelSubAreaName = data[21],
                                    ConfirmationDate = data[22],
                                    ProbationExpiryDueDate = data[23],
                                    Status = data[24],
                                    OldEmployeeCode = data[25],
                                };
                    // Now we got all the columns of the first line. Next add them to the  new list Item
                    int i = 0;

                    foreach (var item in query)
                    {
                        try
                        {
                            using (PMSDataContext = new VFSPMSEntitiesDataContext(webUrl))
                            {
                                if (i == 0) { i++; continue; } i++;
                                if (string.IsNullOrEmpty(item.EmployeeCode)) continue;
                                var itemToUpdate = (from r in PMSDataContext.EmployeesMaster where r.EmployeeCode.ToString() == Convert.ToInt64(item.EmployeeCode).ToString() select r).ToList().FirstOrDefault();
                                if (itemToUpdate == null)
                                {
                                    LogHandler.LogVerbose("Inserting record of Employee Code: '" + item.EmployeeCode + "'");
                                    EmployeesMasterItem itemToInsert = new EmployeesMasterItem()
                                    {
                                        Title = item.FullName,
                                        EmployeeCode = Convert.ToInt32(item.EmployeeCode),
                                        EmployeeName = item.FullName,
                                        WindowsLogin = GetADLogin(item.EmployeeCode, mySiteWeb),
                                        EmployeeStatus = EmployeeStatus(item.Status),
                                    };
                                    if (!string.IsNullOrEmpty(item.HRPersonnel))
                                    {
                                        itemToInsert.HRBusinessPartnerEmployeeCode = Convert.ToInt32(item.HRPersonnel);
                                        itemToInsert.HRBusinessPartnerName = Convert.ToString(item.HRPersonnelName);
                                    }
                                    if (!string.IsNullOrEmpty(item.RMPersonnel))
                                    {
                                        itemToInsert.ReportingManagerEmployeeCode = Convert.ToInt32(item.RMPersonnel);
                                        itemToInsert.ReportingManagerName = Convert.ToString(item.RMPersonnelName);
                                    }
                                    if (!string.IsNullOrEmpty(item.DeptHead))
                                    {
                                        itemToInsert.DepartmentHeadEmployeeCode = Convert.ToInt32(item.DeptHead);
                                        itemToInsert.DepartmentHeadName = Convert.ToString(item.DeptHeadName);
                                    }
                                    itemToInsert.Status = (item.Status.Trim() == "3");
                                    var compcode = (from r in PMSDataContext.Companies where r.CompanyCode.ToString().Trim() == item.CompanyCode.Trim() select r).FirstOrDefault();
                                    if (!string.IsNullOrEmpty(item.CompanyCode))
                                    {
                                        if (compcode != null)
                                        {
                                            itemToInsert.CompanyCode = compcode;
                                            itemToInsert.CompanyName = compcode.CompanyName;
                                        }
                                    }
                                    var positioncode = (from r in PMSDataContext.Positions where r.PositionCode.ToString().Trim() == item.Position.Trim() select r).FirstOrDefault();
                                    if (positioncode != null)
                                    {
                                        itemToInsert.PositionCode = positioncode;
                                        itemToInsert.Position = positioncode.PositionLongText;
                                    }
                                    var region = (from r in PMSDataContext.Regions where r.RegionName.ToString().Trim() == item.Region.Trim() select r).FirstOrDefault();
                                    if (region != null)
                                        itemToInsert.HRRegion = region;

                                    var subarea = (from r in PMSDataContext.SubAreas where r.PersonnelSubAreaCode.ToString().Trim() == item.PersonnelSubArea.Trim() select r).ToList().FirstOrDefault();
                                    if (subarea != null)
                                    {
                                        itemToInsert.SubAreaCode = subarea;
                                        itemToInsert.SubArea = subarea.SubPersonnelAreaText;
                                    }
                                    var empgrp = (from r in PMSDataContext.EmployeeGroups where r.EmployeeGroupCode.ToString().Trim() == item.EmpGroup.Trim() select r).FirstOrDefault();
                                    if (empgrp != null)
                                    {
                                        itemToInsert.EmployeeGroup = empgrp;
                                        itemToInsert.EmployeeGroupName = empgrp.EmployeeGroupText;
                                    }
                                    var area = (from r in PMSDataContext.Areas where r.PersonnelAreaCode.ToString().Trim() == item.PersonnelArea.Trim() select r).FirstOrDefault();
                                    if (area != null)
                                    {
                                        itemToInsert.AreaCode = area;
                                        itemToInsert.Area = area.PersonnelAreaText;
                                    }
                                    var empsubgrp = (from r in PMSDataContext.EmployeeSubGroups where r.EmployeeGroupCode.EmployeeGroupCode.ToString().Trim() == item.EmpGroup.Trim() && r.EmployeeSubGroupCode.Trim() == item.EmpSubGroup.Trim() select r).FirstOrDefault();
                                    if (empsubgrp != null)
                                    {
                                        itemToInsert.EmployeeSubGroup = empsubgrp;
                                        itemToInsert.EmployeeSubGroupName = empsubgrp.EmployeeSubGroupText;
                                    }
                                    var oucode = (from r in PMSDataContext.OrganizationUnits where r.OrganizationUnitCode.ToString().Trim() == item.OUCode.Trim() select r).FirstOrDefault();
                                    if (oucode != null)
                                    {
                                        itemToInsert.OUCode = oucode;
                                        itemToInsert.OrganizationUnit = oucode.OrganizationUnitLongText;
                                    }

                                    if (!string.IsNullOrEmpty(item.ProbationExpiryDueDate))
                                    {
                                        if (DateTime.TryParseExact(item.ProbationExpiryDueDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
                                            itemToInsert.ConfirmationDueDate = dt;
                                        else
                                            itemToInsert.ConfirmationDueDate = null;
                                    }
                                    else
                                        itemToInsert.ConfirmationDueDate = null;
                                    if (!string.IsNullOrEmpty(item.ConfirmationDate))
                                    {
                                        if (DateTime.TryParseExact(item.ConfirmationDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
                                            itemToInsert.ConfirmationDate = dt;
                                        else
                                            itemToInsert.ConfirmationDate = null;
                                    }
                                    else
                                        itemToInsert.ConfirmationDate = null;
                                    if (!string.IsNullOrEmpty(item.JoiningDate))
                                    {
                                        if (DateTime.TryParseExact(item.JoiningDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
                                            itemToInsert.HireDate = dt;
                                        else
                                            itemToInsert.HireDate = null;
                                    }
                                    else
                                        itemToInsert.HireDate = null;
                                    if (!string.IsNullOrEmpty(item.OldEmployeeCode))
                                        itemToInsert.OldEmployeeCode = Convert.ToInt32(item.OldEmployeeCode);
                                    PMSDataContext.EmployeesMaster.InsertOnSubmit(itemToInsert);
                                }
                                else
                                {
                                    LogHandler.LogVerbose("Updating record of Employee Code: '" + item.EmployeeCode + "'");
                                    itemToUpdate.Status = (item.Status.Trim() == "3");
                                    itemToUpdate.EmployeeStatus = EmployeeStatus(item.Status);
                                    var compcode = (from r in PMSDataContext.Companies where r.CompanyCode.ToString().Trim() == item.CompanyCode.Trim() select r).FirstOrDefault();
                                    if (!string.IsNullOrEmpty(item.CompanyCode) && compcode != null)
                                    {
                                        itemToUpdate.CompanyCode = compcode;
                                        itemToUpdate.CompanyName = compcode.CompanyName;
                                    }
                                    itemToUpdate.EmployeeCode = Convert.ToInt32(item.EmployeeCode);
                                    if (!string.IsNullOrEmpty(item.DeptHead))
                                    {
                                        itemToUpdate.DepartmentHeadEmployeeCode = Convert.ToInt32(item.DeptHead);
                                        itemToUpdate.DepartmentHeadName = Convert.ToString(item.DeptHeadName);
                                    }
                                    itemToUpdate.EmployeeName = item.FullName;
                                    if (!string.IsNullOrEmpty(item.HRPersonnel))
                                    {
                                        itemToUpdate.HRBusinessPartnerEmployeeCode = Convert.ToInt32(item.HRPersonnel);
                                        itemToUpdate.HRBusinessPartnerName = Convert.ToString(item.HRPersonnelName);
                                    }
                                    if (!string.IsNullOrEmpty(item.OldEmployeeCode))
                                        itemToUpdate.OldEmployeeCode = Convert.ToInt32(item.OldEmployeeCode);
                                    if (!string.IsNullOrEmpty(item.RMPersonnel))
                                    {
                                        itemToUpdate.ReportingManagerEmployeeCode = Convert.ToInt32(item.RMPersonnel);
                                        itemToUpdate.ReportingManagerName = Convert.ToString(item.RMPersonnelName);
                                    }
                                    itemToUpdate.Title = item.FullName;
                                    if (!string.IsNullOrEmpty(item.ProbationExpiryDueDate))
                                    {
                                        if (DateTime.TryParseExact(item.ProbationExpiryDueDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
                                            itemToUpdate.ConfirmationDueDate = dt;
                                        else
                                            itemToUpdate.ConfirmationDueDate = null;
                                    }
                                    else
                                        itemToUpdate.ConfirmationDueDate = null;
                                    if (!string.IsNullOrEmpty(item.ConfirmationDate))
                                    {
                                        if (DateTime.TryParseExact(item.ConfirmationDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
                                            itemToUpdate.ConfirmationDate = dt;
                                        else
                                            itemToUpdate.ConfirmationDate = null;
                                    }
                                    else
                                        itemToUpdate.ConfirmationDate = null;
                                    if (!string.IsNullOrEmpty(item.JoiningDate))
                                    {
                                        if (DateTime.TryParseExact(item.JoiningDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dt))
                                            itemToUpdate.HireDate = dt;
                                        else
                                            itemToUpdate.HireDate = null;
                                    }
                                    else
                                        itemToUpdate.HireDate = null;
                                    var positioncode = (from r in PMSDataContext.Positions where r.PositionCode.ToString().Trim() == item.Position.Trim() select r).FirstOrDefault();
                                    if (positioncode != null)
                                    {
                                        itemToUpdate.PositionCode = positioncode;
                                        itemToUpdate.Position = positioncode.PositionLongText;
                                    }
                                    var region = (from r in PMSDataContext.Regions where r.RegionName.ToString().Trim() == item.Region.Trim() select r).FirstOrDefault();
                                    if (region != null)
                                        itemToUpdate.HRRegion = region;
                                    var subarea = (from r in PMSDataContext.SubAreas where r.PersonnelSubAreaCode.ToString().Trim() == item.PersonnelSubArea.Trim() select r).ToList().FirstOrDefault();
                                    if (subarea != null)
                                    {
                                        itemToUpdate.SubAreaCode = subarea;
                                        itemToUpdate.SubArea = subarea.SubPersonnelAreaText;
                                    }
                                    var empgrp = (from r in PMSDataContext.EmployeeGroups where r.EmployeeGroupCode.ToString().Trim() == item.EmpGroup.Trim() select r).FirstOrDefault();
                                    if (empgrp != null)
                                    {
                                        itemToUpdate.EmployeeGroup = empgrp;
                                        itemToUpdate.EmployeeGroupName = empgrp.EmployeeGroupText;
                                    }
                                    var area = (from r in PMSDataContext.Areas where r.PersonnelAreaCode.ToString().Trim() == item.PersonnelArea.Trim() select r).FirstOrDefault();
                                    if (area != null)
                                    {
                                        itemToUpdate.AreaCode = area;
                                        itemToUpdate.Area = area.PersonnelAreaText;
                                    }
                                    var empsubgrp = (from r in PMSDataContext.EmployeeSubGroups where r.EmployeeGroupCode.EmployeeGroupCode.ToString().Trim() == item.EmpGroup.Trim() && r.EmployeeSubGroupCode.Trim() == item.EmpSubGroup.Trim() select r).FirstOrDefault();
                                    if (empsubgrp != null)
                                    {
                                        itemToUpdate.EmployeeSubGroup = empsubgrp;
                                        itemToUpdate.EmployeeSubGroupName = empsubgrp.EmployeeSubGroupText;
                                    }
                                    var oucode = (from r in PMSDataContext.OrganizationUnits where r.OrganizationUnitCode.ToString().Trim() == item.OUCode.Trim() select r).FirstOrDefault();
                                    if (oucode != null)
                                    {
                                        itemToUpdate.OUCode = oucode;
                                        itemToUpdate.OrganizationUnit = oucode.OrganizationUnitLongText;
                                    }
                                    itemToUpdate.WindowsLogin = GetADLogin(item.EmployeeCode, mySiteWeb);
                                    
                                    
                                }
                                try
                                {
                                    
                                    PMSDataContext.SubmitChanges(ConflictMode.ContinueOnConflict,true);
                                }
                                catch (Exception)
                                {
                                    LogHandler.LogWarning("Error in porting the employee with Employee Code: " + item.EmployeeCode);
                                    //LogHandler.LogError(ex, "Error in porting the employee with Employee Code: " + item.EmployeeCode);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHandler.LogError(ex, "Error in porting the employee with Employee Code: " + item.EmployeeCode);
                        }
                    }
                    LogHandler.LogVerbose("Total No of Employees processed: " + i.ToString());
                }
            }
        }

        /// <summary>
        /// Gets the AD login.
        /// </summary>
        /// <param name="EmployeeCode">The employee code.</param>
        /// <returns></returns>
        string GetADLogin(string EmployeeCode, SPWeb web)
        {
            string ADLogin = string.Empty;
            try
            {
                EmployeeCode = "E" + Convert.ToInt32(EmployeeCode).ToString("000000000");
                SPList list = web.Lists["EmployeeLoginADMapping"];
                string query = @"<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>{0}</Value></Eq></Where>";
                query = string.Format(query, EmployeeCode);
                SPQuery spQuery = new SPQuery();
                spQuery.Query = query;
                SPListItemCollection items = list.GetItems(spQuery);
                string ADLoginFullName = string.Empty;
                //if (items != null && items.Count > 0)
                //    ADLoginFullName = Convert.ToString(items[0]["LoginName"]);
                //if (string.IsNullOrEmpty(ADLoginFullName)) return ADLogin;
                //ADLoginFullName = ADLoginFullName.Split('#')[1];
                //SPUser login = web.EnsureUser(ADLoginFullName);
                //ADLogin = login.LoginName;
                
                if (items != null && items.Count > 0)
                {
                    SPFieldUser UsersColumn = (SPFieldUser)items[0].Fields.GetField("LoginName");
                    SPUser login = ((SPFieldUserValue)(UsersColumn.GetFieldValue(items[0]["LoginName"].ToString()))).User;
                    ADLogin = login.LoginName;
                }
                if (!string.IsNullOrEmpty(ADLogin))
                    ADLogin = ADLogin.Split('\\')[1];
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in Populate Employees");
            }
            return ADLogin;
        }

        /// <summary>
        /// Checks the AD detail availability.
        /// </summary>
        /// <returns></returns>
        bool CheckADDetailAvailability()
        {

            if (string.IsNullOrEmpty(ADLoginUrl)) return false;
            try
            {


            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        string EmployeeStatus(string status)
        {
            switch (status)
            {
                case "0":
                    return "Withdrawn";
                case "1":
                    return "InActive";
                case "3":
                    return "Active";
                default:
                    return "";
            }

        }
    }

}
