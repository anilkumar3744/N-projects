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

namespace VFS.PMS.EventReceiver.EventReceiverOnEmployeeUpdated
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiverOnEmployeeUpdated : SPItemEventReceiver
    {
        /// <summary>
        /// An item was updated.
        /// </summary>
        public override void ItemUpdated(SPItemEventProperties properties)
        {
            try
            {
                base.ItemUpdated(properties);

                if (Convert.ToString(properties.ListTitle) == "Employees Master")
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                           {
                               LogHandler.LogVerbose("ItemUpdated=Employees Master");
                               //bool isAppraisalDeactivated = CheckAppraisalIsDeactivated(properties.Web, Convert.ToString(properties.ListItem["EmployeeCode"]));
                               if (!CheckAppraisal(properties.Web, Convert.ToString(properties.ListItem["EmployeeCode"])))
                               {
                                   //Appraisal doesn’t exist                            
                                   createAppraisals(properties);
                                   return;
                               }

                               else if (!CheckAppraisalIsDeactivated(properties.Web, Convert.ToString(properties.ListItem["EmployeeCode"])))
                               {
                                   if (Convert.ToString(properties.ListItem["EmployeeGroup"]).Split('#')[1] != "A")
                                   {
                                       LogHandler.LogVerbose("Deactivated in CASE:1 -- Employee group changed " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                       DeactivateWorkflow(properties);
                                   }
                                   else if (Convert.ToString(properties.ListItem["EmployeeStatus"]).ToUpper() != "ACTIVE")
                                   {
                                       LogHandler.LogVerbose("Deactivated in CASE:9 EmployeeStatus Changed " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                       DeactivateWorkflow(properties);
                                   }
                                   else
                                   {
                                       if (Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                                       {
                                           //if appraisal exists, de-activate the appraisal
                                           if (string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDate"])) && string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                                           {
                                               LogHandler.LogVerbose("Deactivated in CASE:2 ConfirmationDate, ConfirmationDueDate are blank and Employee Sub Group Starts with 'P' " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                               DeactivateWorkflow(properties);
                                           }
                                           else if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDate"])))
                                           {
                                               if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) > GetPhaseEndDate(properties, "H1") && CheckAppraisalStatus(properties, "H1"))
                                               {
                                                   //if appraisal exists, de-activate the appraisal
                                                   LogHandler.LogVerbose("Deactivated in CASE:3 " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                                   DeactivateWorkflow(properties);
                                               }
                                               else if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) > GetPhaseEndDate(properties, "H2") && CheckAppraisalStatus(properties, "H2"))
                                               {
                                                   //if appraisal exists, de-activate the appraisal
                                                   LogHandler.LogVerbose("Deactivated in CASE:4 " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                                   DeactivateWorkflow(properties);
                                               }
                                           }
                                           else if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                                           {
                                               if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) > GetPhaseEndDate(properties, "H1") && CheckAppraisalStatus(properties, "H1"))
                                               {
                                                   //if appraisal exists, de-activate the appraisal
                                                   LogHandler.LogVerbose("Deactivated in CASE:5 " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                                   DeactivateWorkflow(properties);
                                               }
                                               else if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) > GetPhaseEndDate(properties, "H2") && CheckAppraisalStatus(properties, "H2"))
                                               {
                                                   //if appraisal exists, de-activate the appraisal
                                                   LogHandler.LogVerbose("Deactivated in CASE:6 " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                                   DeactivateWorkflow(properties);
                                               }
                                           }
                                       }
                                       else if (!Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                                       {
                                           if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["HireDate"])))
                                           {
                                               LogHandler.LogVerbose("ItemUpdating=HireDate");
                                               if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) > GetPhaseEndDate(properties, "H1") && CheckAppraisalStatus(properties, "H1")) //&& and (H1 appraisal exists
                                               {
                                                   //deactivate it.
                                                   LogHandler.LogVerbose("Deactivated in CASE:7 " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                                   DeactivateWorkflow(properties);
                                               }
                                               else if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) > GetPhaseEndDate(properties, "H2") && CheckAppraisalStatus(properties, "H2")) //&& and (H2 appraisal exists
                                               {
                                                   //deactivate it.
                                                   LogHandler.LogVerbose("Deactivated in CASE:8 " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                                                   DeactivateWorkflow(properties);
                                               }
                                           }
                                       }
                                   }
                               }
                               if (CheckAppraisalIsDeactivated(properties.Web, Convert.ToString(properties.ListItem["EmployeeCode"])))
                               {
                                   //H2 activation
                                   if (ActivateH1DeactivatedAppraisals(properties))
                                   {
                                       int appraisalID = 0;
                                       SPList appraisalTasks = properties.Web.Lists["VFSAppraisalTasks"];
                                       SPQuery q = new SPQuery();
                                       q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Eq><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq></And></And></Where>";
                                       SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);
                                       if (appraisalCollection.Count > 0)
                                       {
                                           properties.Web.AllowUnsafeUpdates = true;
                                           SPListItem item = appraisalCollection[appraisalCollection.Count-1];
                                           appraisalID = Convert.ToInt32(item["tskAppraisalId"]);
                                           Hashtable ht = new Hashtable();
                                           ht["tskStatus"] = "Approved";
                                           ht["Status"] = "Approved";
                                           ht["tskActionTakenBy"] = properties.Web.CurrentUser;
                                           SPWorkflowTask.AlterTask(item, ht, true);
                                           properties.Web.AllowUnsafeUpdates = false;
                                       }
                                       else
                                       {
                                           SPQuery q2 = new SPQuery();
                                           q2.Query = "<Where><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H2 - Awaiting Appraisee Goal Setting</Value></Eq><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq></And></Where>";
                                           SPListItemCollection appraisalCollection2 = appraisalTasks.GetItems(q2);
                                           if (appraisalCollection2.Count > 0)
                                           {
                                               SPListItem item = appraisalCollection2[0];
                                               appraisalID = Convert.ToInt32(item["tskAppraisalId"]);
                                           }
                                       }
                                       if (appraisalID != 0)
                                       {
                                           SPList appraisals = properties.Web.Lists["Appraisals"];
                                           SPListItem appItem = appraisals.GetItemById(appraisalID);
                                           properties.Web.AllowUnsafeUpdates = true;
                                           appItem["appAppraisalStatus"] = "H2 - Awaiting Appraisee Goal Setting";
                                           appItem["appDeactivated"] = string.Empty;
                                           appItem.Update();

                                           #region H1ScoreEmpty
                                           SPList lstAppraisalPhases = properties.Web.Lists["Appraisal Phases"];
                                           SPQuery phasesQueryH1 = new SPQuery();
                                           phasesQueryH1.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                                           SPListItemCollection phasesCollectionH1 = lstAppraisalPhases.GetItems(phasesQueryH1);

                                           if (phasesCollectionH1.Count > 0)
                                           {
                                               SPListItem phaseItemH1 = phasesCollectionH1[0];
                                               phaseItemH1["aphScore"] = "";
                                               phaseItemH1.Update();
                                           }
                                           #endregion

                                           properties.Web.AllowUnsafeUpdates = false;
                                       }
                                   }
                               }
                           });
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.ItemUpdated.createAppraisals");
            }
        }

        void createAppraisals(SPItemEventProperties properties)
        {
            try
            {
                LogHandler.LogVerbose("ItemUpdated=Employees Master -- createAppraisals()");
                using (SPSite site = new SPSite(properties.SiteId))
                {
                    using (SPWeb web = site.OpenWeb(properties.RelativeWebUrl))
                    {

                        if (Convert.ToString(properties.ListTitle) == "Employees Master")
                        {

                            string currentCyccle = GetCurrentPerformanceCycle(properties.Web);
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
                        //create appraisal with status ‘H1 Awaiting appraisee goal setting’
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H1"))
                        {
                            flag = true;
                            CreateAppraisalForEligibleEmployee(properties, 1);
                            LogHandler.LogVerbose("ItemUpdated=Employees Master -- H1 Appraisal has created for this EmployeeCode " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                        }
                    }
                    else if (Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H1")) //need to modify h1 enddate
                        {
                            //create an appraisal record with status ‘H1 - Awaiting Appraisee Goal Setting’
                            flag = true;
                            CreateAppraisalForEligibleEmployee(properties, 1);
                            LogHandler.LogVerbose("ItemUpdated=Employees Master -- H1 Appraisal has created for this EmployeeCode " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                        }

                    }
                    else if (!Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["HireDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H1")) //need to modify h1 enddate
                        {
                            //create an appraisal record with status ‘H1 - Awaiting Appraisee Goal Setting’
                            flag = true;
                            CreateAppraisalForEligibleEmployee(properties, 1);
                            LogHandler.LogVerbose("ItemUpdated=Employees Master -- H1 Appraisal has created for this EmployeeCode " + Convert.ToString(properties.ListItem["EmployeeCode"]));
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
                    if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDate"])) && Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                    {
                        //create appraisal with status ‘H2 Awaiting appraisee goal setting’
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H2"))
                        {
                            CreateAppraisalForEligibleEmployee(properties, 12);
                            LogHandler.LogVerbose("ItemUpdated=Employees Master -- H2 Appraisal has created for this EmployeeCode " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                        }
                    }
                    else if (Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) >= GetPhaseEndDate(properties, "H1"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            CreateAppraisalForEligibleEmployee(properties, 12);
                            LogHandler.LogVerbose("ItemUpdated=Employees Master -- H2 Appraisal has created for this EmployeeCode " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                        }
                    }
                    else if (!Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["HireDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2")) // || Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) >= GetPhaseEndDate(properties, "H1"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            CreateAppraisalForEligibleEmployee(properties, 12);
                            LogHandler.LogVerbose("ItemUpdated=Employees Master -- H2 Appraisal has created for this EmployeeCode " + Convert.ToString(properties.ListItem["EmployeeCode"]));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.H2Eligibility");
            }
        }

        private void CreateAppraisalForEligibleEmployee(SPItemEventProperties properties, int statusId)
        {
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(properties.Web);
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
                                SPWorkflow workflow = WFmanager.StartWorkflow(appraisalItem, WFAssociations, "Subbu", true);

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

        private bool CheckAppraisalStatus(SPItemEventProperties properties, string phase)
        {
            bool flag = false;
            try
            {
                string currentCyccle = GetCurrentPerformanceCycle(properties.Web);
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

        private DateTime GetPhaseEndDate(SPItemEventProperties properties, string phase)
        {
            DateTime date = DateTime.Now;
            try
            {
                string pCycle = GetCurrentPerformanceCycle(properties.Web);
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

        private bool CheckAppraisalIsDeactivated(SPWeb web, string employeeCode)
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
                        SPListItem item = appraisalCollection[0];
                        if (!string.IsNullOrEmpty(Convert.ToString(item["appDeactivated"])))
                        {
                            if (Convert.ToString(item["appDeactivated"]).ToUpper() == "YES")
                                flag = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.CheckAppraisal");
            }
            return flag;
        }

        private bool ActivateH1DeactivatedAppraisals(SPItemEventProperties properties)
        {
            bool flag = false;
            try
            {
                using (SPSite site = new SPSite(properties.SiteId))
                {
                    using (SPWeb web = site.OpenWeb(properties.RelativeWebUrl))
                    {
                        string currentCyccle = GetCurrentPerformanceCycle(properties.Web);
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

                                if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H1SelfEvaluationStartDate"])))
                                {
                                    if (CheckH2Activation(properties))
                                        flag = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.createAppraisals");
            }
            return flag;
        }

        private bool CheckH2Activation(SPItemEventProperties properties)
        {
            bool flag = false;
            try
            {
                if (Convert.ToString(properties.ListItem["EmployeeGroup"]).Split('#')[1] == "A" && Convert.ToString(properties.ListItem["EmployeeStatus"]).ToUpper() == "ACTIVE")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDate"])) && Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                    {
                        //create appraisal with status ‘H2 Awaiting appraisee goal setting’
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDate"])) <= GetPhaseEndDate(properties, "H2"))
                        {
                            flag = true;
                        }
                    }
                    else if (Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["ConfirmationDueDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["ConfirmationDueDate"])) >= GetPhaseEndDate(properties, "H1"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            flag = true;
                        }
                    }
                    else if (!Convert.ToString(properties.ListItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(properties.ListItem["HireDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2") || Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) <= GetPhaseEndDate(properties, "H2")) //&& Convert.ToDateTime(Convert.ToString(properties.ListItem["HireDate"])).AddMonths(3) >= GetPhaseEndDate(properties, "H1"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            flag = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.H2Eligibility");
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
                q.Query = "<Where><And><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties.ListItem["EmployeeCode"]) + "</Value></Eq><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Neq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Neq></And></And></Where>";
                SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);
                if (appraisalCollection.Count > 0)
                {
                    properties.Web.AllowUnsafeUpdates = true;
                    SPListItem item = appraisalCollection[appraisalCollection.Count - 1];
                    appraisalID = Convert.ToInt32(item["tskAppraisalId"]);
                    Hashtable ht = new Hashtable();
                    ht["tskStatus"] = "Deactivation";
                    ht["Status"] = "Deactivation";
                    ht["tskActionTakenBy"] = properties.Web.CurrentUser;
                    SPWorkflowTask.AlterTask(item, ht, true);
                    properties.Web.AllowUnsafeUpdates = false;
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

    }
}
