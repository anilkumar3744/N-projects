using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.SharePoint;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;

namespace VFS.PMS.ApplicationPages
{
    public static class CommonMaster
    {
        /// <summary>
        /// Serializes the message.
        /// </summary>
        /// <param name="exMessage">The ex message.</param>
        /// <returns></returns>
        public static string serializeMessage(string exMessage)
        {
            var message = new JavaScriptSerializer().Serialize(exMessage);
            var script = string.Format("alert({0});", message);
            return script;
        }
        /// <summary>
        /// Serializes the message.
        /// </summary>
        /// <param name="exMessage">The ex message.</param>
        /// <returns></returns>
        public static string serializestring(string exMessage)
        {
            var message = new JavaScriptSerializer().Serialize(exMessage);
            return message;
        }

        public static DataTable GetCategories()
        {
            DataTable dtCategories = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Goal Categories"];
                    SPQuery catogoriesQuery = new SPQuery();
                    catogoriesQuery.Query = "<Where><And><Eq><FieldRef Name='ctgrMandatory'/><Value Type='Choice'>True</Value></Eq><Eq><FieldRef Name='ctgrStatus' /><Value Type='Boolean'>1</Value></Eq></And></Where>";//"<Where><Eq><FieldRef Name='ctgrMandatory' /><Value Type='Text'>" + true + "</Value></Eq></Where>";

                    SPListItemCollection categoryColl = categoryList.GetItems(catogoriesQuery);
                    ////SPListItemCollection categoryColl = categoryList.GetItems();
                    dtCategories = categoryColl.GetDataTable();
                }
                return dtCategories;
            }
        }

        public static DataTable GetOptionalGoalCategories()
        {
            DataTable dtCategories = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Goal Categories"];
                    SPQuery catogoriesQuery = new SPQuery();
                    catogoriesQuery.Query = "<Where><Eq><FieldRef Name='ctgrStatus' /><Value Type='Boolean'>1</Value></Eq></Where>";
                    ////catogoriesQuery.Query = "<Where><Eq><FieldRef Name='ctgrMandatory' /><Value Type='Text'>" + false + "</Value></Eq></Where>";
                    ////SPListItemCollection categoryColl = categoryList.GetItems(catogoriesQuery);
                    SPListItemCollection categoryColl = categoryList.GetItems(catogoriesQuery);
                    dtCategories = categoryColl.GetDataTable();
                }
                return dtCategories;
            }
        }

        public static SPListItem GetTheAppraiseeDetails(string currentUserName)
        {
            SPListItemCollection masterItemsColl = null;
            SPListItem employeeItem = null;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists["Employees Master"];
                        SPQuery q = new SPQuery();
                        ////q.Query = "<Where><Eq><FieldRef Name='WindowsLogin' /><Value Type='Text'>" + currentUserName + "</Value></Eq></Where>";
                        q.Query = "<Where><Eq><FieldRef Name='WindowsLogin' /><Value Type='Text'>" + currentUserName.Split('\\')[1] + "</Value></Eq></Where>";
                        //q.Query = "<Where><And><Eq><FieldRef Name='WindowsLogin' /><Value Type='Text'>" + currentUserName.Split('\\')[1] + "</Value></Eq><Eq><FieldRef Name='EmployeeStatus' /><Value Type='Text'>Active</Value></Eq></And></Where>";
                        masterItemsColl = employeMaster.GetItems(q);
                        employeeItem = masterItemsColl[0];
                        return employeeItem;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetUserByCode(string employeeCode)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList lstEmployeeMaster = currentWeb.Lists["Employees Master"];

                        SPQuery q = new SPQuery();
                        //q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";
                        q.Query = "<Where><And><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq><Eq><FieldRef Name='EmployeeStatus' /><Value Type='Text'>Active</Value></Eq></And></Where>";
                        SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                        SPListItem employeeItem = masterCollection[0];

                        return Convert.ToString(employeeItem["WindowsLogin"]);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static SPListItem GetCurrentItemId(string listName)
        {
            using (SPSite site = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[listName];

                    SPListItemCollection collListItems = list.Items;
                    if (list.ItemCount > 0)
                    {
                        SPListItem listItem = list.Items[list.ItemCount - 1];
                        return listItem;
                    }
                    else
                        return null;
                }
            }
        }

        public static DataTable GetGoalsDetails(int itemID)
        {
            DataTable dtCategories = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Appraisal Goals"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='agAppraisalPhaseID' /><Value Type='Number'>" + itemID + "</Value></Eq></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    dtCategories = categoryColl.GetDataTable();
                }
                return dtCategories;
            }
        }

        public static DataTable GetDraftGoalsDetails(int itemID, string currentUser)
        {
            DataTable dtCategories = new DataTable();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        SPList categoryList = web.Lists["Appraisal Goals Draft"];
                        SPQuery q = new SPQuery();
                        //q.Query = "<Where><Eq><FieldRef Name='agAppraisalPhaseID' /><Value Type='Number'>" + itemID + "</Value></Eq></Where>";<Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And>
                        q.Query = "<Where><And><Eq><FieldRef Name='agAppraisalPhaseID' /><Value Type='Number'>" + itemID + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                        SPListItemCollection categoryColl = categoryList.GetItems(q);
                        dtCategories = categoryColl.GetDataTable();
                    }
                }
            });
            return dtCategories;
        }

        internal static void DeleteGoalsItem(int itemID)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList appraisalGoals = web.Lists["Appraisal Goals"];
                    SPListItem goalItem = appraisalGoals.GetItemById(itemID);

                    web.AllowUnsafeUpdates = true;
                    goalItem.Delete();
                    web.AllowUnsafeUpdates = false;

                }
            }
        }

        internal static DataTable GetMasterCompetencies(string empSubGroup)
        {
            DataTable dtCompetencies = new DataTable();
            DataTable dtFiltered;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList lstCompetencies = web.Lists["Competency Descriptions"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='cmptdStatus' /><Value Type='Boolean'>1</Value></Eq></Where>"; //"<Where><Eq><FieldRef Name='cmptEmpSubGroup' /><Value Type='Text'>" + empSubGroup.Split('#')[1] + "</Value></Eq></Where>";
                    SPListItemCollection categoryColl = lstCompetencies.GetItems(q);
                    dtCompetencies = categoryColl.GetDataTable();
                    DataView dview = new DataView();
                    dview = dtCompetencies.DefaultView;
                    dview.RowFilter = "cmptEmpSubGroup='" + empSubGroup.Split('#')[1] + "'";
                    if (dview.Count > 0)
                    {
                        dtFiltered = dview.ToTable();
                    }
                    else
                    {
                        dtFiltered = null;
                    }

                }

                return dtFiltered;
                //using (SPWeb currentWeb = SPContext.Current.Site.OpenWeb())
                //            {
                //                SPList competencyDesList = currentWeb.Lists["Competency Descriptions"];
                //                SPQuery ospcompetencyDesQuery = new SPQuery();
                //                DataTable dtcompetencyDes = competencyDesList.GetItems(ospcompetencyDesQuery).GetDataTable();
                //                SPList empMastersList = currentWeb.Lists["Employees Master"];
                //                SPQuery ospEmpMastersQuery = new SPQuery();
                //                DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
                //                SPList subGroup = currentWeb.Lists["Employee Sub Groups"];
                //                SPQuery ospsubGroupQuery = new SPQuery();
                //                DataTable dtsubGroup = subGroup.GetItems(ospsubGroupQuery).GetDataTable();
                //                List<SearchAppraisalClass> joinData = (from competencyDes in dtcompetencyDes.AsEnumerable()
                //                                                        join empsSubGroup in dtsubGroup.AsEnumerable() on competencyDes.Field<string>("cmptEmpSubGroup") equals empsSubGroup.Field<string>("EmployeeSubGroupCode") ////into appMasters
                //                                                        join empMasters in dtEmpMasters.AsEnumerable() on empsSubGroup.Field<string>("EmployeeSubGroupCode") equals empMasters.Field<string>("EmployeeSubGroup")
                //                                                        where empMasters.Field<double>("EmployeeCode")==Convert.ToDouble(empSubGroup)
                //                                                        //where appraisal.Field<double>("appPerformanceCycle").ToString().StartsWith(year) && empMasters.Field<string>("EmployeeName").StartsWith(txtAppraiserEmployeeName.Text.Trim())
                //                                                       //&& empMasters.Field<string>("EmployeeCode").StartsWith(txtAppraiserEmployeeCode.Text.Trim())// && empMasters.Field<string>("GroupID").ToString().StartsWith(empGroup)
                //                                                       select new SearchAppraisalClass
                //                                                      {
                //                                                          //appPerformanceCycle = appraisal.Field<double>("appPerformanceCycle"), //Convert.ToDouble(appraisal.PerformanceCycle),
                //                                                          //appEmployeeCode = empMasters.Field<double>("EmployeeCode"),//EmployeeCode,
                //                                                          //appAppraisalStatus = appraisal.Field<string>("appAppraisalStatus"),////&& SqlMethods.Like(empMasters.Field<string>("EmployeeCode").ToString(), cid) 
                //                                                          //EmpName = empMasters.Field<string>("EmployeeName"),
                //                                                          ////ApprName=appprMaster.Field<string>("EmployeeName"),
                //                                                          ////RevName = appRMaster.Field<string>("EmployeeName"),
                //                                                          ////HrName = appHRMaster.Field<string>("EmployeeName"),
                //                                                          //ID = appraisal.Field<int>("ID")
                //                                                          acmptCompetency = competencyDes.Field<string>("cmptCompetency"),
                //                                                          acmptDescription=competencyDes.Field<string>("cmptDescription")

                //                                                      }).ToList<SearchAppraisalClass>();

                //                return ConvertToDataTable(joinData);
                //}

            }
        }

        static DataTable ConvertListToDataTable(List<SearchAppraisalClass> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 2;

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            if (data != null)
            {
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static DateTime GetPhaseEndDate(string phase)
        {

            DateTime date = DateTime.Now;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    using (VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(web.Url))
                    {

                        var performancePhases = (from p in dc.PerformanceCyclePhases.AsEnumerable()
                                                 where p.PerformanceCycle.PerformanceCycle.ToString().Trim() == GetCurrentPerformanceCycle() && p.Phase.Phase.ToString().Trim() == phase
                                                 select p).FirstOrDefault();
                        if (performancePhases != null)
                            date = Convert.ToDateTime(performancePhases.EndDate);
                    }
                }
            }

            return date;
        }


        internal static DataTable GetAppraisalCompetencies(int itemID)
        {
            DataTable dtCompetencies = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Appraisal Competencies"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='acmptAppraisalPhaseId' /><Value Type='Number'>" + itemID + "</Value></Eq></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    dtCompetencies = categoryColl.GetDataTable();
                }
                return dtCompetencies;
            }
        }

        internal static DataTable GetAppraisalDevelopmentMesure(int itemID)
        {
            DataTable DtDevelopmentmesure = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Appraisal Development Measures"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='pdpAppraisalPhaseID' /><Value Type='Number'>" + itemID + "</Value></Eq></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    DtDevelopmentmesure = categoryColl.GetDataTable();
                }
                return DtDevelopmentmesure;
            }
        }

        internal static DataTable GetPIPDetails(int itemID)
        {
            DataTable dtPip = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList pipDetails = web.Lists["PIP"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='pipAppraisalPhaseID' /><Value Type='Number'>" + itemID + "</Value></Eq></Where>";

                    SPListItemCollection pipCollection = pipDetails.GetItems(q);
                    dtPip = pipCollection.GetDataTable();
                }
                return dtPip;
            }
        }

        internal static void DeletePDP(int itemID)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList AppraisalDevelopmentMeasures = web.Lists["Appraisal Development Measures"];
                    SPListItem PDPItem = AppraisalDevelopmentMeasures.GetItemById(itemID);

                    web.AllowUnsafeUpdates = true;
                    PDPItem.Delete();
                    web.AllowUnsafeUpdates = false;

                }
            }
        }

        #region Implemted by Krishna

        public static SPListItem GetMasterDetails(string colName, string currentUserName)
        {
            SPListItemCollection masterItemsColl = null;
            SPListItem employeeItem = null;
            DataTable dt;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists["Employees Master"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + currentUserName + "</Value></Eq></Where>";
                        masterItemsColl = employeMaster.GetItems(q);
                        if (masterItemsColl.Count > 0)
                            //dt = masterItemsColl.GetDataTable();
                            employeeItem = masterItemsColl[0];
                        return employeeItem;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static SPListItem GetAppraisalCheck(string colName, string currentUserName)
        {
            SPListItemCollection masterItemsColl = null;
            SPListItem employeeItem = null;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists["Appraisals"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + currentUserName + "</Value></Eq></Where>";
                        masterItemsColl = employeMaster.GetItems(q);

                        DataTable dt = masterItemsColl.GetDataTable();
                        employeeItem = masterItemsColl[0];
                        return employeeItem;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool CheckSavedDraft(int appraisalID)
        {
            bool flag = false;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList appraisalGoals = currentWeb.Lists["Appraisal Goals"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='agAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq></Where>";

                        SPListItemCollection coll = appraisalGoals.GetItems(q);
                        if (coll.Count > 0)
                        {
                            flag = true;
                        }
                    }
                    return flag;
                }
            }
            catch (Exception)
            {
                return flag;
            }
        }

        public static List<SearchAppraisalClass> GetData(string colName, string empId)
        {
            List<SearchAppraisalClass> hrData = new List<SearchAppraisalClass>();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisalList = currentWeb.Lists["Appraisals"];
                    SPQuery ospAppraisalQuery = new SPQuery();
                    DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
                    SPList empMastersList = currentWeb.Lists["Employees Master"];
                    SPQuery ospEmpMastersQuery = new SPQuery();
                    DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
                    hrData = (from appraisal in dtAppraisal.AsEnumerable()
                              join empMasters in dtEmpMasters.AsEnumerable() on appraisal.Field<string>("appEmployeeCode").ToString() equals empMasters.Field<double>("EmployeeCode").ToString() ////into appMasters
                              where empMasters.Field<double>(colName) == Convert.ToDouble(empId)// && appraisal.Field<double>("appPerformanceCycle").ToString()==performanceCycle
                              select new SearchAppraisalClass
                              {
                                  appPerformanceCycle = Convert.ToString(appraisal.Field<double>("appPerformanceCycle")), //Convert.ToDouble(appraisal.PerformanceCycle),
                                  appEmployeeCode = empMasters.Field<double>("EmployeeCode").ToString(),//EmployeeCode,
                                  appAppraisalStatus = appraisal.Field<string>("appAppraisalStatus"),////&& SqlMethods.Like(empMasters.Field<string>("EmployeeCode").ToString(), cid) 
                                  EmpName = empMasters.Field<string>("EmployeeName"),
                                  HrCode = empMasters.Field<double>("HRBusinessPartnerEmployeeCode").ToString(),
                                  RevCode = empMasters.Field<double>("ReportingManagerEmployeeCode").ToString(),
                                  ApprCode = empMasters.Field<double>("DepartmentHeadEmployeeCode").ToString(),
                                  ApprName = empMasters.Field<string>("ReportingManagerName"),
                                  RevName = empMasters.Field<string>("DepartmentHeadName"),
                                  HrName = empMasters.Field<string>("HRBusinessPartnerName"),
                                  OrganizationUnitCode = empMasters.Field<string>("OUCode"),
                                  PositionCode = empMasters.Field<string>("PositionCode"),
                                  PersonnelSubAreaCode = empMasters.Field<string>("SubAreaCode"),
                                  PersonnelAreaCode = empMasters.Field<string>("AreaCode"),
                                  CompanyCode = empMasters.Field<string>("CompanyCode"),
                                  EmployeeSubGroupCode = empMasters.Field<string>("EmployeeSubGroup"),
                                  EmployeeGroupCode = empMasters.Field<string>("EmployeeGroup"),
                                  ID = appraisal.Field<int>("ID").ToString(),
                                  IsReview = appraisal.Field<string>("appIsReview"),
                                  Deactivated = string.IsNullOrEmpty(appraisal.Field<string>("appDeactivated")) ? "No" : appraisal.Field<string>("appDeactivated"),
                                  Region=empMasters.Field<string>("HRRegion")


                              }).ToList();
                }
                foreach (SearchAppraisalClass li in hrData)
                {
                    if (li.IsReview == "True")
                        li.IsReview = "Yes";
                    else
                        li.IsReview = "No";
                }
                return hrData;
            }
        }

        public static DataTable GetPerformanceCycles()
        {
            DataTable dtPerformanceCylce;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList performanceCylce = currentWeb.Lists["Performance Cycles"];
                    SPListItemCollection items = performanceCylce.GetItems();
                    dtPerformanceCylce = items.GetDataTable();
                }
                return dtPerformanceCylce;
            }
        }

        public static string GetCurrentPerformanceCycle()
        {
            string currentPCycle = string.Empty;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists["Performance Cycles"];
                    SPListItemCollection appraisalCollection = appraisals.GetItems();
                    if (appraisalCollection.Count > 0)
                    {
                        currentPCycle = Convert.ToString(appraisalCollection[appraisalCollection.Count - 1]["Performance_x0020_Cycle"]);
                    }
                }
            }
            return currentPCycle;
        }

        #endregion

        internal static void DeleteAppraisalGoalsDraft(int appraisalPhaseId, string currentUser, string listName)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList AppraisalDevelopmentMeasures = web.Lists[listName];
                    SPQuery q = new SPQuery();
                    if (listName.Contains("Draft"))
                        q.Query = "<Where><And><Eq><FieldRef Name='agAppraisalPhaseID' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                    else
                        q.Query = "<Where><Eq><FieldRef Name='agAppraisalPhaseID' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq></Where>";

                    SPListItemCollection appraisalGoalsColl = AppraisalDevelopmentMeasures.GetItems(q);
                    int count = appraisalGoalsColl.Count;
                    for (int i = 0; i < count; i++)
                    {
                        web.AllowUnsafeUpdates = true;
                        appraisalGoalsColl.Delete(0);
                        web.AllowUnsafeUpdates = false;
                    }
                }
            }
        }

        internal static void DeletePreviousGoal(int appraisalPhaseId, string currentUser, string listName)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList AppraisalDevelopmentMeasures = web.Lists[listName];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='agAppraisalPhaseID' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                    SPListItemCollection appraisalGoalsColl = AppraisalDevelopmentMeasures.GetItems(q);
                    int count = appraisalGoalsColl.Count;
                    for (int i = 0; i < count; i++)
                    {
                        web.AllowUnsafeUpdates = true;
                        appraisalGoalsColl.Delete(0);
                        web.AllowUnsafeUpdates = false;
                    }
                }
            }
        }

        internal static DataTable GetCompetenciesDraft(int appraisalPhaseId, string currentUser)
        {
            DataTable dtCategories = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Appraisal Competencies Draft"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='acmptAppraisalPhaseId' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    dtCategories = categoryColl.GetDataTable();
                }
                return dtCategories;
            }
        }

        internal static void DeleteCompetenciesDraft(int appraisalPhaseId, string currentUser, string listName)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList AppraisalDevelopmentMeasures = web.Lists[listName];
                    SPQuery q = new SPQuery();
                    if (listName.Contains("Draft"))
                        q.Query = "<Where><And><Eq><FieldRef Name='acmptAppraisalPhaseId' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                    else
                        q.Query = "<Where><Eq><FieldRef Name='acmptAppraisalPhaseId' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq></Where>";

                    SPListItemCollection appraisalGoalsColl = AppraisalDevelopmentMeasures.GetItems(q);
                    int count = appraisalGoalsColl.Count;
                    for (int i = 0; i < count; i++)
                    {
                        web.AllowUnsafeUpdates = true;
                        appraisalGoalsColl.Delete(0);
                        web.AllowUnsafeUpdates = false;
                    }
                }
            }
        }

        internal static DataTable GetAppraisalDevelopmentMesureDraft(int appraisalPhaseId, string currentUser)
        {
            DataTable dtCategories = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["Appraisal Development Measures Draft"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='pdpAppraisalPhaseID' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    dtCategories = categoryColl.GetDataTable();
                }
                return dtCategories;
            }
        }

        internal static void DeletePDPDraft(int appraisalPhaseId, string currentUser, string listName)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList AppraisalDevelopmentMeasures = web.Lists[listName];
                    SPQuery q = new SPQuery();
                    if (listName.Contains("Draft"))
                        q.Query = "<Where><And><Eq><FieldRef Name='pdpAppraisalPhaseID' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq><Eq><FieldRef Name='Author' /><Value Type='User'>" + currentUser + "</Value></Eq></And></Where>";
                    else
                        q.Query = "<Where><Eq><FieldRef Name='pdpAppraisalPhaseID' /><Value Type='Number'>" + appraisalPhaseId + "</Value></Eq></Where>";

                    SPListItemCollection appraisalGoalsColl = AppraisalDevelopmentMeasures.GetItems(q);
                    int count = appraisalGoalsColl.Count;
                    for (int i = 0; i < count; i++)
                    {
                        web.AllowUnsafeUpdates = true;
                        appraisalGoalsColl.Delete(0);
                        web.AllowUnsafeUpdates = false;
                    }
                }
            }
        }

        internal static DataTable GetPIPDetails(int appraisalId, int appraisalPhaseID)
        {
            DataTable dt = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList pipDetails = web.Lists["PIP"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='pipAppraisalID' /><Value Type='Number'>" + appraisalId + "</Value></Eq><Eq><FieldRef Name='pipAppraisalPhaseID' /><Value Type=Number'>" + appraisalPhaseID + "</Value></Eq></And></Where>";
                    SPListItemCollection pipCollection = pipDetails.GetItems(q);
                    dt = pipCollection.GetDataTable();
                }
                return dt;
            }
        }

        public static void BindHistory(string chCommentFor, int chReferenceId, string chComment, SPUser chCommentedBy, string chCommentedDate, string chActor, string chRole)
        {
            SPListItem lstItem;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];
                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = chCommentFor;
                    lstItem["chReferenceId"] = chReferenceId;
                    lstItem["chCommentedBy"] = chCommentedBy;
                    lstItem["chComment"] = chComment;
                    lstItem["chCommentedDate"] = chCommentedDate;
                    lstItem["chActor"] = chActor;
                    lstItem["chRole"] = chRole;
                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                }
            }
        }

        /// <summary>
        /// Determines whether this instance [can user view appraisal] the specified employee code.
        /// </summary>
        /// <param name="employeeCode">The employee code.</param>
        /// <param name="windowLogin">The window login.</param>
        /// <param name="web">The web.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can user view appraisal] the specified employee code; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanUserViewAppraisal(double employeeCode, string windowLogin, SPWeb web)
        {
            try
            {
                //string windLogin1 = windowLogin;
                if (windowLogin.Split('\\').Length == 2)
                    windowLogin = windowLogin.Split('\\')[1];

                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    var empTocheck = (from r in PMSDataContext.EmployeesMaster where r.WindowsLogin == windowLogin && r.EmployeeStatus.ToString().ToUpper() == "ACTIVE" select r).FirstOrDefault();
                    if (empTocheck == null) return false;
                    double empCodeToCheck = Convert.ToDouble(empTocheck.EmployeeCode);
                    var checking = (from r in PMSDataContext.EmployeesMaster.AsEnumerable()
                                    where (r.ReportingManagerEmployeeCode.ToString() == empCodeToCheck.ToString() ||
                                        r.DepartmentHeadEmployeeCode.ToString() == empCodeToCheck.ToString() ||
                                        r.HRBusinessPartnerEmployeeCode.ToString() == empCodeToCheck.ToString() || r.EmployeeCode == empCodeToCheck) && r.EmployeeCode == employeeCode 
                                    select r).FirstOrDefault();

                    if (checking != null) return true;


                    //Regional hr
                    //var RegionalHR=(from r in PMSDataContext.Regions.AsEnumerable() where r.RegionHREmployeeCode.ToString().Trim() == employeeCode.ToString().Trim() select r).FirstOrDefault();
                    //if (RegionalHR !=null) return true;
                    SPListItem item = GetTheAppraiseeDetails(windowLogin);
                    if (item != null) return true;

                    bool flag = false;
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPGroup reviewGroup = web.Groups["Review Board"];
                    SPGroup TMTGroup = web.Groups["TMT"];
                    flag = web.IsCurrentUserMemberOfGroup(reviewGroup.ID) || web.IsCurrentUserMemberOfGroup(TMTGroup.ID);
                });
                    return flag;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Exception in chekcing permissions");
                return false;
            }

        }

        /// <summary>
        /// Gets the common history.
        /// </summary>
        /// <param name="appraisalPhaseID">The appraisal phase ID.</param>
        /// <returns></returns>
        public static DataTable GetCommonHistory(int appraisalPhaseID)
        {
            DataTable dtCommentsHistory = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList commentsList = web.Lists["Comments History"];
                    SPQuery commentsQuery = new SPQuery();
                    commentsQuery.Query = "<Where><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + appraisalPhaseID + "</Value></Eq></Where><OrderBy><FieldRef Name='ID' Ascending=False' /></OrderBy>";
                    SPListItemCollection categoryColl = commentsList.GetItems(commentsQuery);
                    dtCommentsHistory = categoryColl.GetDataTable();
                }
                return dtCommentsHistory;
            }
        }

        /// <summary>
        /// Gets the rating.
        /// </summary>
        /// <param name="finalValue">The final value.</param>
        /// <returns></returns>
        public static string GetRating(decimal finalValue)
        {
            finalValue = Math.Round(finalValue,0,MidpointRounding.AwayFromZero);
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    decimal minValue = 0, maxValue = decimal.MaxValue;
                    SPList matrix = objWeb.Lists["Performance Ratings"];
                    SPListItemCollection matrixColl = matrix.GetItems();
                    for (int i = 0; i < matrixColl.Count; i++)
                    {
                        SPListItem item = matrixColl[i];
                        if (i == 0)
                            minValue = Convert.ToDecimal(item["pmInitialAchievement"]);
                        if (i == matrixColl.Count - 1)
                            maxValue = Convert.ToDecimal(item["pmMaximumAchievement"]);
                        if (Convert.ToDecimal(item["pmInitialAchievement"]) <= finalValue && Convert.ToDecimal(item["pmMaximumAchievement"]) >= finalValue)
                        {
                            return Convert.ToString(item["pmScaleNumber"]);
                        }
                        else
                        {
                            if (finalValue > maxValue || finalValue < minValue)
                            {
                                return Convert.ToString(item["pmScaleNumber"]);
                            }
                        }
                    }
                }
                return "";
            }
        }
        /// <summary>
        /// Gets the dash board URL.
        /// </summary>
        /// <value>
        /// The dash board URL.
        /// </value>
        public static string DashBoardUrl
        {
            get
            {
                return SPContext.Current.Web.Url + "/_layouts/VFS_DashBoards/Dashboard.aspx";
            }
        }

        public static bool CheckCurrentUserIsActor(SPUser currentUser, string wfStatus, int appraisalID, SPWeb web)
        {
            bool flag = false;
            try
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
                {
                    var locked = (from r in PMSDataContext.PerformanceCycles select r.Locked).LastOrDefault();
                    if (Convert.ToBoolean(locked)) return false;
                }
                SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];
                SPQuery q = new SPQuery();
                q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + wfStatus + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq></And></And></Where>";
                SPListItemCollection taskCollection = lstAppraisalTasks.GetItems(q);
                if (taskCollection.Count > 0)
                    flag = true;
                return flag;
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Exception in chekcing permissions");
                return false;
            }

        }


        internal static string NavigateToViewPage(string status, int appraisalID)
        {
            try
            {
                string returnUrl = string.Empty;
                switch (status)
                {
                    case "H1-Awaiting Appraisee Goal Setting":
                        {
                            returnUrl = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/Das.aspx?AppId=" + appraisalID;//AppraiseeSavedDraft
                            break;
                        }
                    case "H1-Awaiting Appraiser Goal Approval":

                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiseeGoalsDraftView.aspx?AppId=" + appraisalID;
                        break;
                    case "H1-Goals Approved":
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiserAppraiseeApprovedView.aspx?AppId=" + appraisalID;
                        break;
                    case "H1-Awaiting Self-evaluation":
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/SelfEva/SelfView.aspx?AppId=" + appraisalID;
                        //Response.Redirect((SPContext.Current.Web.Url+ "/_LAYOUTS/H2initial/SelfView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID));
                        break;
                    case "H1-Awaiting Appraiser Evaluation":////Venkaiah                       
                        returnUrl = SPContext.Current.Web.Url + "/_Layouts/AppraiserEvaluationViewMode/AppraiserEvaluationViewMode.aspx?AppId=" + appraisalID;
                        break;
                    case "H1-Awaiting Reviewer Approval":////Jagadish

                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/ReviewerEvaluationView.aspx?AppId=" + appraisalID;//_Layouts/RH1View/RevEvalH1View.aspx?TaskID=" + taskId));
                        break;
                    case "H1 - Awaiting Appraisee Sign-off":////Jagadish

                        returnUrl = SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?AppId=" + appraisalID;
                        break;
                    case "H1 - Sign-off by Appraisee":////Jagadish                       
                        returnUrl = SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?AppId=" + appraisalID;
                        break;
                    case "H1 – Completed":

                        returnUrl = SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?AppId=" + appraisalID;
                        // Commented by Jeevan Response.Redirect((SPContext.Current.Web.Url+ "/_Layouts/VFSProjectH1/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID));
                        break;
                    case "H1 - Appraisee Appeal"://// Changed by the request of Mr Rao on 12/6/2013

                        returnUrl = SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?AppId=" + appraisalID;
                        break;
                    case "H1 - HR Review":////Venkaiah

                        returnUrl = SPContext.Current.Web.Url + "/_Layouts/AppraiserEvaluationViewMode/AppraiserEvaluationViewMode.aspx?AppId=" + appraisalID;
                        ////Response.Redirect((SPContext.Current.Web.Url+ "/_Layouts/RH1/RevEval.aspx?TaskID=" + taskId + "&AppId=" + appraisalID));////Jagadish
                        break;
                    case "H2 - Awaiting Appraisee Goal Setting":////Jeevan

                        returnUrl = SPContext.Current.Web.Url + "/_layouts/H2initial/H2AppraiseeGoalsDraftView.aspx?AppId=" + appraisalID;//links are interchanged
                        break;
                    case "H2 - Awaiting Appraiser Goal Approval":////Jeevan
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/H2initial/H2AppraiseeGoalsDraftView.aspx?AppId=" + appraisalID;
                        //returnUrl = SPContext.Current.Web.Url + "/_layouts/H2initial/H2AwaitingAppraiserApprove.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Goals Approved"://JeEvan
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/H2initial/AppraiserAppraiseeApprovedView.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Awaiting Self-evaluation"://Jeevan                        
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/H2initial/SelfView.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Awaiting Appraiser Evaluation"://Venkaiah

                        returnUrl = SPContext.Current.Web.Url + "/_layouts/H2AppraiserEvaluationViewmode/H2AppraiserEvaluationViewmode.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Awaiting Appraiser Evaluation1":
                        break;
                    case "H2 - Awaiting Reviewer Approval":////JAGADISH
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/ReviewerEvaluationView.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Awaiting Appraisee Sign-off":///JAGADISH
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Sign-off by Appraisee":////jAGADISH
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 – Completed"://JAGADISH
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?AppId=" + appraisalID;
                        break;
                    case "H2 - Appraisee Appeal"://Venkaiah
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?AppId=" + appraisalID;//Rao told
                        break;
                    case "H2 - Awaiting Appraisee Sign-off3":
                        break;
                    case "H2 - HR Review"://Venkaiah
                        returnUrl = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?AppId=" + appraisalID;
                        break;
                }
                return returnUrl;
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Exception in chekcing permissions");
                return string.Empty;
            }

        }

        public static void SendMail(SPUser appraisee, SPUser appraiser, string status, SPWeb currentWeb)
        {
            try
            {
                string toUsers = string.Empty;
                string mailBody = string.Empty;
                string subject = string.Empty;
                string mail = string.Empty;
                StringDictionary headers;
                string DBurl = currentWeb.Url + "/_LayOuts/VFS_DashBoards/Dashboard.aspx";
                headers = new StringDictionary();
                switch (status)
                {
                    case "H1GoalsAmend":
                        {
                            mail = string.Format(CustomMessages.H1GoalsAmend,
                                GetCurrentPerformanceCycle(),
                                appraisee.Name,
                                appraiser.Name,
                                appraisee.Name, 
                                GetCurrentPerformanceCycle(), 
                                DBurl);
                            headers.Add("to", appraisee.Email);
                            break;
                        }
                    case "H2GoalsAmend":
                        {
                            mail = string.Format(CustomMessages.H1GoalsAmend, 
                                GetCurrentPerformanceCycle(), 
                                appraisee.Name,
                                appraiser.Name,
                                appraisee.Name, 
                                GetCurrentPerformanceCycle(), 
                                DBurl);
                            headers.Add("to", appraisee.Email);
                            break;
                        }
                }
                subject = mail.Split('%')[0];
                mailBody = mail.Split('%')[1];
                headers.Add("subject", subject);
                headers.Add("content-type", "text/html");
                SPUtility.SendEmail(currentWeb, headers, mailBody);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
