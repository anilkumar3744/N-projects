using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using Microsoft.SharePoint.Linq;
using System.Linq;
using VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated;
namespace VFS.PMS.EventReceiver.EmployeeDataReciever
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EmployeeDataReciever : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            try
            {
                if (Convert.ToString(properties.ListTitle) == "Employees Master")
                {
                    base.ItemUpdated(properties);
                    if (CheckAppraisal(properties))
                    {
                        LogHandler.LogVerbose("ItemUpdating=Employees Master -- SPItemEventReceiver Started");
                        if (!string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ReportingManagerEmployeeCode"])))
                        {
                            LogHandler.LogVerbose("ItemUpdating=ReportingManagerEmployeeCode");
                            if (Convert.ToString(properties.ListItem["ReportingManagerEmployeeCode"]) != Convert.ToString(properties.AfterProperties["ReportingManagerEmployeeCode"]))
                            {
                                LogHandler.LogVerbose("Reporting Manager changed for this Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                //Change the Appraiser value of the workflow tasks of Appraisals & PIPs having appraiser as old value of ‘Reporting Manager Emp Code’.

                                //string reportingmanagerLogin = GetUserLogin(Convert.ToString(properties.ListItem["ReportingManagerEmployeeCode"]), properties);
                                //SPUser oldReportingManager = properties.Web.EnsureUser(reportingmanagerLogin);

                                string newReportingmanagerLogin = GetUserLogin(Convert.ToString(properties.AfterProperties["ReportingManagerEmployeeCode"]), properties);
                                SPUser newReportingManager = properties.Web.EnsureUser(newReportingmanagerLogin);

                                UpdateAssignedToUser(null, newReportingManager, properties, "Appraiser");
                            }
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["DepartmentHeadEmployeeCode"])))
                        {
                            LogHandler.LogVerbose("ItemUpdating=DepartmentHeadEmployeeCode");
                            if (Convert.ToString(properties.ListItem["DepartmentHeadEmployeeCode"]) != Convert.ToString(properties.AfterProperties["DepartmentHeadEmployeeCode"]))
                            {
                                //Change the Appraiser value of the workflow tasks of Appraisals having reviewer as old value of ‘Reporting Manager Emp Code’.
                                LogHandler.LogVerbose("Department Head changed for this Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));

                                //string reviewerLogin = GetUserLogin(Convert.ToString(properties.ListItem["DepartmentHeadEmployeeCode"]), properties);
                                //SPUser oldReviewer = properties.Web.EnsureUser(reviewerLogin);

                                string newreviewerLogin = GetUserLogin(Convert.ToString(properties.AfterProperties["DepartmentHeadEmployeeCode"]), properties);
                                SPUser newreviewer = properties.Web.EnsureUser(newreviewerLogin);

                                UpdateAssignedToUser(null, newreviewer, properties, "Reviewer");
                            }
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["HRBusinessPartnerEmployeeCode"])))
                        {
                            LogHandler.LogVerbose("ItemUpdating=HRBusinessPartnerEmployeeCode");
                            if (Convert.ToString(properties.ListItem["HRBusinessPartnerEmployeeCode"]) != Convert.ToString(properties.AfterProperties["HRBusinessPartnerEmployeeCode"]))
                            {
                                //Change the Appraiser value of the workflow tasks of Appraisals having HR Business Partner as old value of ‘HR Business Partner Emp Code’’.
                                LogHandler.LogVerbose("HR Business Partner Employee changed for this Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));

                                //string stroldHR = GetUserLogin(Convert.ToString(properties.ListItem["HRBusinessPartnerEmployeeCode"]), properties);
                                //SPUser oldHR = properties.Web.EnsureUser(stroldHR);

                                string newHRLogin = GetUserLogin(Convert.ToString(properties.AfterProperties["HRBusinessPartnerEmployeeCode"]), properties);
                                SPUser newHR = properties.Web.EnsureUser(newHRLogin);

                                UpdateAssignedToUser(null, newHR, properties, "HR");
                            }
                        }
                        LogHandler.LogVerbose("ItemUpdating=End");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.ItemUpdating");
            }
        }

        //void createAppraisals(SPItemEventProperties properties)
        //{
        //    try
        //    {
        //        using (SPSite site = new SPSite(properties.SiteId))
        //        {
        //            using (SPWeb web = site.OpenWeb(properties.RelativeWebUrl))
        //            {

        //                if (Convert.ToString(properties.ListTitle) == "Employees Master")
        //                {
        //                    SPSecurity.RunWithElevatedPrivileges(delegate()
        //                    {
        //                        string currentCyccle = GetCurrentPerformanceCycle(properties);
        //                        if (!string.IsNullOrEmpty(currentCyccle) && !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["WindowsLogin"])))
        //                        {
        //                            web.AllowUnsafeUpdates = true;
        //                            SPList listTMPActions = properties.Web.Lists["Performance Cycle Activity"];
        //                            SPQuery phasesQuery = new SPQuery();
        //                            phasesQuery.Query = "<Where><Eq><FieldRef Name='PerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></Where>";
        //                            SPListItemCollection phasesCollection = listTMPActions.GetItems(phasesQuery);
        //                            if (phasesCollection.Count > 0)
        //                            {
        //                                SPListItem phaseItem = phasesCollection[0];
        //                                if (string.IsNullOrEmpty(Convert.ToString(phaseItem["H1GoalSettingStartDate"])))
        //                                {
        //                                    //do nothing
        //                                }
        //                                else if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H1GoalSettingStartDate"])) && string.IsNullOrEmpty(Convert.ToString(phaseItem["H1SelfEvaluationStartDate"])))
        //                                {
        //                                    H1Eligibility(properties);
        //                                }
        //                                else if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H1SelfEvaluationStartDate"])) && string.IsNullOrEmpty(Convert.ToString(phaseItem["H2SelfEvaluationStartDate"])))
        //                                {
        //                                    if (!H1Eligibility(properties))
        //                                        H2Eligibility(properties);
        //                                }
        //                                else if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H2SelfEvaluationStartDate"])) && string.IsNullOrEmpty(Convert.ToString(phaseItem["CycleCloseDate"])))
        //                                {
        //                                    H2Eligibility(properties);
        //                                }
        //                            }
        //                        }
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogError(ex, "EmployeeDataReciever.createAppraisals");
        //    }
        //}

        //bool H1Eligibility(SPItemEventProperties properties)
        //{
        //    try
        //    {
        //        bool flag = false;
        //        if (GetEmployeeGroup(Convert.ToString(properties.AfterProperties["EmployeeGroup"]), properties) == "A" && Convert.ToString(properties.AfterProperties["EmployeeStatus"]).ToUpper() == "ACTIVE")
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ConfirmationDate"])) && GetEmployeeSubGroup(Convert.ToString(properties.AfterProperties["EmployeeSubGroup"]), properties).StartsWith("P"))
        //            {
        //                if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H1"))
        //                {
        //                    //create appraisal with status ‘H1 Awaiting appraisee goal setting’
        //                    flag = true;
        //                    CreateAppraisalForEligibleEmployee(properties, 1);
        //                }
        //            }
        //            else if (GetEmployeeSubGroup(Convert.ToString(properties.AfterProperties["EmployeeSubGroup"]), properties).StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ConfirmationDueDate"])))
        //            {
        //                if (Convert.ToDateTime(Convert.ToString(properties.AfterProperties["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H1")) //need to modify h1 enddate
        //                {
        //                    //create an appraisal record with status ‘H1 - Awaiting Appraisee Goal Setting’
        //                    flag = true;
        //                    CreateAppraisalForEligibleEmployee(properties, 1);
        //                }

        //            }

        //            else if (!GetEmployeeSubGroup(Convert.ToString(properties.AfterProperties["EmployeeSubGroup"]), properties).StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["HireDate"])))
        //            {
        //                if (Convert.ToDateTime(Convert.ToString(properties.AfterProperties["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H1")) //need to modify h1 enddate
        //                {
        //                    //create an appraisal record with status ‘H1 - Awaiting Appraisee Goal Setting’
        //                    flag = true;
        //                    CreateAppraisalForEligibleEmployee(properties, 1);
        //                }
        //            }
        //        }
        //        return flag;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogError(ex, "EmployeeDataReciever.H1Eligibility");
        //        return false;
        //    }
        //}

        //void H2Eligibility(SPItemEventProperties properties)
        //{
        //    try
        //    {
        //        if (GetEmployeeGroup(Convert.ToString(properties.AfterProperties["EmployeeGroup"]), properties) == "A" && Convert.ToString(properties.AfterProperties["EmployeeStatus"]).ToUpper() == "ACTIVE")
        //        {
        //            if (!string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ConfirmationDate"])) && GetEmployeeSubGroup(Convert.ToString(properties.AfterProperties["EmployeeSubGroup"]), properties).StartsWith("P")) // || !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ConfirmationDate"])))
        //            {
        //                if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H2"))
        //                {
        //                    //create appraisal with status ‘H2 Awaiting appraisee goal setting’
        //                    CreateAppraisalForEligibleEmployee(properties, 12);
        //                }
        //            }
        //            else if (GetEmployeeSubGroup(Convert.ToString(properties.AfterProperties["EmployeeSubGroup"]), properties).StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ConfirmationDueDate"])))
        //            {
        //                if (Convert.ToDateTime(Convert.ToString(properties.AfterProperties["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) >= GetPhaseEndDate(properties, "H1"))
        //                {
        //                    //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
        //                    CreateAppraisalForEligibleEmployee(properties, 12);
        //                }
        //            }
        //            else if (!GetEmployeeSubGroup(Convert.ToString(properties.AfterProperties["EmployeeSubGroup"]), properties).StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["HireDate"])))
        //            {
        //                if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2") || Convert.ToDateTime(Convert.ToString(properties.AfterProperties["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) >= GetPhaseEndDate(properties, "H1"))
        //                {
        //                    //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
        //                    CreateAppraisalForEligibleEmployee(properties, 12);
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogError(ex, "EmployeeDataReciever.H2Eligibility");
        //    }
        //}

        //private void CreateAppraisalForEligibleEmployee(SPItemEventProperties properties, int statusId)
        //{
        //    try
        //    {
        //        string currentCyccle = GetCurrentPerformanceCycle(properties);
        //        if (!string.IsNullOrEmpty(currentCyccle))
        //        {
        //            SPList appraisals = properties.Web.Lists["Appraisals"];
        //            SPList lstAppraisalStatus = properties.Web.Lists["Appraisal Status"];

        //            SPListItem appraisalItem = appraisals.AddItem();
        //            appraisalItem["appPerformanceCycle"] = Convert.ToInt32(currentCyccle);
        //            appraisalItem["appEmployeeCode"] = Convert.ToString(properties.ListItem["EmployeeCode"]);
        //            appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(statusId)["Appraisal_x0020_Workflow_x0020_S"]);
        //            appraisalItem["appH1GoalSettingStartDate"] = DateTime.Now.Date;
        //            appraisalItem.Update();

        //            using (var site = new SPSite(properties.WebUrl))
        //            {
        //                using (var web = site.OpenWeb())
        //                {
        //                    const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";
        //                    SPWorkflowManager WFmanager = site.WorkflowManager;
        //                    SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
        //                    SPWorkflowCollection workflowColl = WFmanager.GetItemWorkflows(appraisalItem);

        //                    if (workflowColl.Count == 0)
        //                    {
        //                        SPWorkflow workflow = WFmanager.StartWorkflow(appraisalItem, WFAssociations, "Subbu", true);
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogError(ex, "EmployeeDataReciever.CreateAppraisalForEligibleEmployee");
        //    }
        //}

        private bool CheckAppraisal(SPItemEventProperties properties)
        {
            bool flag = false;
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(properties);
                if (!string.IsNullOrEmpty(currentCyccle))
                {
                    SPList appraisals = properties.Web.Lists["Appraisals"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='appEmployeeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><Eq><FieldRef Name='appPerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></And></Where>";
                    SPListItemCollection appraisalCollection = appraisals.GetItems(q);
                    if (appraisalCollection.Count > 0)
                    {
                        flag = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.CheckAppraisal");
            }
            return flag;
        }

        private bool CheckAppraisalStatus(SPItemEventProperties properties, string phase)
        {
            bool flag = false;
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(properties);
                if (!string.IsNullOrEmpty(currentCyccle))
                {
                    SPList appraisals = properties.Web.Lists["Appraisals"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='appEmployeeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><Eq><FieldRef Name='appPerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></And></Where>";

                    SPListItemCollection appraisalCollection = appraisals.GetItems(q);
                    if (appraisalCollection.Count > 0)
                    {
                        SPListItem item = appraisalCollection[0];
                        if (Convert.ToString(item["appAppraisalStatus"]).Contains(phase))
                            flag = true;
                    }
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.CheckAppraisalStatus");

            }
            return flag;
        }

        private void DeactivateWorkflow(SPItemEventProperties properties)
        {
            try
            {
                int appraisalID = 0;
                SPList appraisalTasks = properties.Web.Lists["VFSAppraisalTasks"];
                SPQuery q = new SPQuery();
                //q.Query = "<Where><And><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq></And></Where>";
                q.Query = "<Where><And><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Neq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Neq></And></And></Where>";
                SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);
                if (appraisalCollection.Count > 0)
                {
                    SPListItem item = appraisalCollection[appraisalCollection.Count - 1];
                    appraisalID = Convert.ToInt32(item["tskAppraisalId"]);
                    Hashtable ht = new Hashtable();
                    ht["tskStatus"] = "Deactivation";
                    ht["Status"] = "Deactivation";
                    ht["tskActionTakenBy"] = properties.Web.CurrentUser;
                    SPWorkflowTask.AlterTask(item, ht, true);
                }
                if (appraisalID != 0)
                {
                    SPList appraisals = properties.Web.Lists["Appraisals"];
                    SPListItem appItem = appraisals.GetItemById(appraisalID);
                    properties.Web.AllowUnsafeUpdates = true;
                    appItem["appDeactivated"] = "Yes";
                    appItem.Update();
                    properties.Web.AllowUnsafeUpdates = false;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.DeactivateWorkflow");
            }
        }

        private void CancelWorkflow(SPItemEventProperties properties)
        {
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(properties);
                if (!string.IsNullOrEmpty(currentCyccle))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (var site = new SPSite(properties.WebUrl))
                        {
                            using (var web = site.OpenWeb())
                            {
                                SPList appraisals = web.Lists["Appraisals"];
                                SPQuery q = new SPQuery();
                                q.Query = "<Where><And><Eq><FieldRef Name='appEmployeeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><Eq><FieldRef Name='appPerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></And></Where>";
                                SPListItemCollection appCollection = appraisals.GetItems(q);
                                if (appCollection.Count > 0)
                                {

                                    SPListItem item = appCollection[0];

                                    SPWorkflowManager WFmanager = site.WorkflowManager;
                                    SPWorkflowCollection workflowColl = WFmanager.GetItemActiveWorkflows(item);

                                    if (workflowColl.Count > 0)
                                    {
                                        web.AllowUnsafeUpdates = true;
                                        SPWorkflow workflow = workflowColl[0];
                                        SPWorkflowManager.CancelWorkflow(workflow);

                                        item["appDeactivated"] = "Yes";
                                        item.Update();
                                        web.AllowUnsafeUpdates = false;
                                    }
                                }
                            }
                        }
                    });
                }
            }

            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.CancelWorkflow");
            }
        }

        private void UpdateAssignedToUser(SPUser oldUser, SPUser newUser, SPItemEventProperties properties, string changedUser)
        {
            try
            {

                //string user = string.Empty;
                SPList appraisalTasks = properties.Web.Lists["VFSAppraisalTasks"];

                SPQuery q = new SPQuery();
                //q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + oldUser.Name + "</Value></Eq><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + properties.ListItem["EmployeeCode"] + "</Value></Eq></And></And></Where>";
                q.Query = "<Where><And><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq></And></Where>";

                DisableAppraisalTaskEvents obj = new DisableAppraisalTaskEvents();
                SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);

                int count = appraisalCollection.Count;
                //for (int i = 0; i < count; i++)
                //{
                if (appraisalCollection != null && appraisalCollection.Count > 0)
                {
                    SPListItem item = appraisalCollection[appraisalCollection.Count - 1];

                    bool flag = false;
                    if (changedUser == "Appraiser")
                    {
                        if (Convert.ToString(item["tskStatus"]).Contains(changedUser))
                        {
                            LogHandler.LogVerbose(changedUser + " is changed from " + Convert.ToString(properties.ListItem["ReportingManagerEmployeeCode"]) + " to " + Convert.ToString(properties.AfterProperties["ReportingManagerEmployeeCode"]) + " for the Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                            flag = true;
                        }
                    }
                    else if (changedUser == "Reviewer")
                    {
                        if (Convert.ToString(item["tskStatus"]).Contains(changedUser))
                        {
                            LogHandler.LogVerbose(changedUser + " is changed from " + Convert.ToString(properties.ListItem["DepartmentHeadEmployeeCode"]) + " to " + Convert.ToString(properties.AfterProperties["DepartmentHeadEmployeeCode"]) + " for the Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                            flag = true;
                        }
                    }
                    else if (changedUser == "HR")
                    {
                        if (Convert.ToString(item["tskStatus"]).Contains("Sign-off by Appraisee"))
                        {
                            LogHandler.LogVerbose(changedUser + " is changed from " + Convert.ToString(properties.ListItem["HRBusinessPartnerEmployeeCode"]) + " to " + Convert.ToString(properties.AfterProperties["HRBusinessPartnerEmployeeCode"]) + " for the Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        //LogHandler.LogVerbose(changedUser + " is changed from " + oldUser.Name + " to " + newUser.Name + " for the Employee Code " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                        obj.DisableEventFiring();
                        item["AssignedTo"] = newUser;
                        item.Update();
                        obj.EnableEventFiring();
                    }
                }
            }
            //}
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.UpdateAssignedToUser");
            }
        }

        private string GetUserLogin(string employeeCode, SPItemEventProperties properties)
        {
            string wondowsId = string.Empty;
            try
            {
                SPList lstEmployeeMaster = properties.Web.Lists["Employees Master"];

                SPQuery q = new SPQuery();
                //q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";
                q.Query = "<Where><And><Eq><FieldRef Name='EmployeeCode' /><Value Type='Number'>" + employeeCode + "</Value></Eq><Eq><FieldRef Name='EmployeeStatus' /><Value Type='Text'>Active</Value></Eq></And></Where>";
                SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                if (masterCollection != null && masterCollection.Count > 0)
                {
                    SPListItem employeeItem = masterCollection[masterCollection.Count - 1];

                    wondowsId = Convert.ToString(employeeItem["WindowsLogin"]);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.GetUserLogin");
                return null;
            }
            return wondowsId;
        }

        private DateTime GetPhaseEndDate(SPItemEventProperties properties, string phase)
        {
            DateTime date = DateTime.Now;
            try
            {
                string pCycle = GetCurrentPerformanceCycle(properties);
                if (!string.IsNullOrEmpty(pCycle))
                {
                    using (VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(properties.Web.Url))
                    {
                        var performancePhases = (from p in dc.PerformanceCyclePhases.AsEnumerable()
                                                 where p.PerformanceCycle.PerformanceCycle.ToString() == pCycle && p.Phase.Phase.ToString() == phase
                                                 select p).FirstOrDefault();
                        if (performancePhases != null)
                            date = Convert.ToDateTime(performancePhases.EndDate);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.GetPhaseEndDate");
            }
            return date;
        }

        private string GetEmployeeGroup(string empGroupId, SPItemEventProperties properties)
        {
            string empGroup = string.Empty;
            //if (!string.IsNullOrEmpty(empGroupId))
            //{
            try
            {
                //SPFieldLookupValue employeeGroup1 = new SPFieldLookupValue(properties.AfterProperties["EmployeeGroup"] as string);
                //empGroup = employeeGroup1.LookupValue;
                SPList employeeGroup = properties.Web.Lists["Employee Groups"];
                SPListItem empItem = employeeGroup.GetItemById(Convert.ToInt32(empGroupId));
                empGroup = Convert.ToString(empItem["EmployeeGroupCode"]);

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.GetEmployeeGroup");
            }
            //}
            return empGroup;
        }

        private string GetEmployeeSubGroup(string empSubGrroup, SPItemEventProperties properties)
        {
            string empSubGroup = string.Empty;
            //if (!string.IsNullOrEmpty(empSubGrroup))
            //{
            try
            {
                //SPFieldLookupValue employeeSubGroup = new SPFieldLookupValue(properties.AfterProperties["EmployeeSubGroup"] as string);
                //empSubGroup = employeeSubGroup.LookupValue;

                SPList employeeGroup = properties.Web.Lists["Employee Sub Groups"];
                SPListItem empItem = employeeGroup.GetItemById(Convert.ToInt32(empSubGrroup));

                empSubGroup = Convert.ToString(empItem["EmployeeSubGroupCode"]);
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.GetEmployeeSubGroup");
            }
            //}
            return empSubGroup;
        }

        private string GetCurrentPerformanceCycle(SPItemEventProperties properties)
        {
            string currentPCycle = string.Empty;
            try
            {
                SPList appraisals = properties.Web.Lists["Performance Cycles"];
                SPListItemCollection appraisalCollection = appraisals.GetItems();
                if (appraisalCollection.Count > 0)
                {
                    currentPCycle = Convert.ToString(appraisalCollection[appraisalCollection.Count - 1]["Performance_x0020_Cycle"]);
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.GetEmployeeSubGroup");
            }
            return currentPCycle;
        }

    }
}
