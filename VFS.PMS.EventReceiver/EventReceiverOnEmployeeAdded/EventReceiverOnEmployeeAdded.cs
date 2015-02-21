using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using Microsoft.SharePoint.Linq;
using System.Linq;
using VFS.PMS.EventReceiver.EmployeeDataReciever;
using VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated;

namespace VFS.PMS.EventReceiver.EventReceiverOnEmployeeAdded
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiverOnEmployeeAdded : SPItemEventReceiver
    {
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            if (Convert.ToString(properties.ListTitle) == "Employees Master")
            {
                base.ItemAdded(properties);
                if (!CheckAppraisal(properties.Web, Convert.ToString(properties.ListItem["EmployeeCode"])))
                {
                    createAppraisals(properties);
                }
            }
        }

        void createAppraisals(SPItemEventProperties properties)
        {
            try
            {
                using (SPSite site = new SPSite(properties.SiteId))
                {
                    using (SPWeb web = site.OpenWeb(properties.RelativeWebUrl))
                    {
                        if (Convert.ToString(properties.ListTitle) == "Employees Master")
                        {
                            SPSecurity.RunWithElevatedPrivileges(delegate()
                            {
                                string currentCyccle = GetCurrentPerformanceCycle(properties);
                                if (!string.IsNullOrEmpty(currentCyccle) && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["WindowsLogin"])))
                                {
                                    web.AllowUnsafeUpdates = true;
                                    SPList listTMPActions = properties.Web.Lists["Performance Cycle Activity"];
                                    SPQuery phasesQuery = new SPQuery();
                                    phasesQuery.Query = "<Where><Eq><FieldRef Name='PerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></Where>";
                                    SPListItemCollection phasesCollection = listTMPActions.GetItems(phasesQuery);
                                    if (phasesCollection.Count > 0)
                                    {
                                        SPListItem phaseItem = phasesCollection[0];
                                        if (string.IsNullOrEmpty(Convert.ToString(phaseItem["H1GoalSettingStartDate"])))
                                        {
                                            //do nothing
                                        }
                                        else if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H1GoalSettingStartDate"])) && string.IsNullOrEmpty(Convert.ToString(phaseItem["H1SelfEvaluationStartDate"])))
                                        {
                                            H1Eligibility(properties);
                                        }
                                        else if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H1SelfEvaluationStartDate"])) && string.IsNullOrEmpty(Convert.ToString(phaseItem["H2SelfEvaluationStartDate"])))
                                        {
                                            if (!H1Eligibility(properties))
                                                H2Eligibility(properties);
                                        }
                                        else if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H2SelfEvaluationStartDate"])) && string.IsNullOrEmpty(Convert.ToString(phaseItem["CycleCloseDate"])))
                                        {
                                            H2Eligibility(properties);
                                        }
                                    }
                                }
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.createAppraisals");
            }
        }

        bool H1Eligibility(SPItemEventProperties properties)
        {
            try
            {
                bool flag = false;
                if (Convert.ToString(properties.ListItem["EmployeeGroup"]).Split('#')[1] == "A" && Convert.ToString(properties.ListItem["EmployeeStatus"]).ToUpper() == "ACTIVE")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDate"])) && Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H1"))
                        {
                            //create appraisal with status ‘H1 Awaiting appraisee goal setting’
                            flag = true;
                            CreateAppraisalForEligibleEmployee(properties, 1);
                        }
                    }

                    else if (Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H1")) //need to modify h1 enddate
                        {
                            //create an appraisal record with status ‘H1 - Awaiting Appraisee Goal Setting’
                            flag = true;
                            CreateAppraisalForEligibleEmployee(properties, 1);
                        }

                    }

                    else if (!Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["HireDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H1")) //need to modify h1 enddate
                        {
                            //create an appraisal record with status ‘H1 - Awaiting Appraisee Goal Setting’
                            flag = true;
                            CreateAppraisalForEligibleEmployee(properties, 1);
                        }
                    }
                }
                return flag;
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.H1Eligibility");
                return false;
            }
        }

        void H2Eligibility(SPItemEventProperties properties)
        {
            try
            {
                if (Convert.ToString(properties.ListItem["EmployeeGroup"]).Split('#')[1] == "A" && Convert.ToString(properties.ListItem["EmployeeStatus"]).ToUpper() == "ACTIVE")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDate"])) && Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P")) // || !string.IsNullOrEmpty(Convert.ToString(properties.AfterProperties["ConfirmationDate"])))
                    {
                        //create appraisal with status ‘H2 Awaiting appraisee goal setting’
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H2"))
                        {
                            CreateAppraisalForEligibleEmployee(properties, 12);
                        }
                    }

                    else if (Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) >= GetPhaseEndDate(properties, "H1"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            CreateAppraisalForEligibleEmployee(properties, 12);
                        }
                    }
                    else if (!Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["HireDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2") || Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) >= GetPhaseEndDate(properties, "H1"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            CreateAppraisalForEligibleEmployee(properties, 12);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.H2Eligibility");
            }
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

        private void CreateAppraisalForEligibleEmployee(SPItemEventProperties properties, int statusId)
        {
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(properties);
                if (!string.IsNullOrEmpty(currentCyccle))
                {
                    SPList appraisals = properties.Web.Lists["Appraisals"];
                    SPList lstAppraisalStatus = properties.Web.Lists["Appraisal Status"];

                    SPListItem appraisalItem = appraisals.AddItem();
                    appraisalItem["appPerformanceCycle"] = Convert.ToInt32(currentCyccle);
                    appraisalItem["appEmployeeCode"] = Convert.ToString(properties.ListItem["EmployeeCode"]);
                    appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(statusId)["Appraisal_x0020_Workflow_x0020_S"]);
                    appraisalItem["appH1GoalSettingStartDate"] = DateTime.Now.Date;
                    appraisalItem.Update();

                    using (var site = new SPSite(properties.WebUrl))
                    {
                        using (var web = site.OpenWeb())
                        {
                            const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";
                            SPWorkflowManager WFmanager = site.WorkflowManager;
                            SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
                            SPWorkflowCollection workflowColl = WFmanager.GetItemWorkflows(appraisalItem);

                            if (workflowColl.Count == 0)
                            {
                                SPWorkflow workflow = WFmanager.StartWorkflow(appraisalItem, WFAssociations, "", true);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.CreateAppraisalForEligibleEmployee");
            }
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
                LogHandler.LogError(ex, "EmployeeDataReciever.GetCurrentPerformanceCycle");
            }
            return currentPCycle;
        }

        private bool CheckAppraisal(SPWeb web, string employeeCode)
        {
            bool flag = false;
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(web);
                if (!string.IsNullOrEmpty(currentCyccle))
                {
                    SPList appraisals = web.Lists["Appraisals"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='appEmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq><Eq><FieldRef Name='appPerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></And></Where>";
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

        private string GetCurrentPerformanceCycle(SPWeb web)
        {
            string currentPCycle = string.Empty;
            try
            {
                SPList performanceCycles = web.Lists["Performance Cycles"];
                SPListItemCollection appraisalCollection = performanceCycles.GetItems();
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
