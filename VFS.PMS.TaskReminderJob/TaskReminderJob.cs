using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System.Collections.Specialized;

namespace VFS.PMS.TaskReminderJob
{
    internal class TaskReminderJob : SPJobDefinition
    {

        string mySiteUrl = string.Empty;

        public TaskReminderJob()
            : base()
        {
        }

        public TaskReminderJob(string jobName, SPService service, SPServer server, SPJobLockType targetType)
            : base(jobName, service, server, targetType)
        {
        }

        public TaskReminderJob(string jobName, SPWebApplication webApplication)
            : base(jobName, webApplication, null, SPJobLockType.ContentDatabase)
        {
            this.Title = "VFS PMS Task Reminder Timer Job";
        }

        public override void Execute(Guid contentDbId)
        {
            //System.Diagnostics.Debug.Assert(false);  
            // get a reference to the current site collection's content database
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
                        SPList Listjob = mySiteWeb.Lists["VFSAppraisalTasks"];
                        SPListItemCollection oItems = Listjob.GetItems();
                        foreach (SPListItem listitem in oItems)
                        {
                            if (Convert.ToString(listitem["Status"]).ToUpper() == "NOT STARTED")
                            {

                                int appraisalID = Convert.ToInt32(listitem["tskAppraisalId"]);
                                SPListItem appraisalItem = GetCurrentAppraisal(mySiteWeb, appraisalID);
                                if (appraisalItem != null)
                                {
                                    SPList listReminders = mySiteWeb.Lists["Reminders"];
                                    //string qry = string.Format("<Where><And><Eq><FieldRef Name='rmdWorkflowState' /><Value Type='Text'>{0}</Value></Eq><Eq><FieldRef Name='rmdRemaindrRequirment' /><Value Type='Text'>Yes</Value></Eq></And></Where>", Convert.ToString(listitem["tskStatus"]));
                                    string qry = string.Format("<Where><And><Eq><FieldRef Name='rmdWorkflowState' /><Value Type='Text'>{0}</Value></Eq><Eq><FieldRef Name='rmdRemaindrRequirment' /><Value Type='Text'>Yes</Value></Eq></And></Where>", Convert.ToString(appraisalItem["appAppraisalStatus"]));

                                    SPQuery spQuery = new SPQuery();
                                    spQuery.Query = qry;
                                    SPListItemCollection reminderItems = listReminders.GetItems(spQuery);
                                    SPListItem reminderItem;
                                    DateTime stDate = DateTime.MinValue;
                                    if (reminderItems != null && reminderItems.Count > 0)
                                    {
                                        reminderItem = reminderItems[0];
                                        if (Convert.ToInt32(reminderItem["rmdDuration"]) > 0)
                                        {
                                            DateTime.TryParse(listitem["StartDate"].ToString(), out stDate);
                                            int result = DateTime.Compare(DateTime.Now.Date, stDate);
                                            //if (result < Convert.ToInt32(reminderItem["rmdDuration"]))
                                            //    continue;

                                            TimeSpan span = DateTime.Now.Date.Subtract(Convert.ToDateTime(reminderItem["Modified"]).Date);
                                            int span2 = Math.Abs(Convert.ToDateTime(reminderItem["Modified"]).Subtract(DateTime.Now.Date).Days); //Convert.ToInt32(DateTime.Now.Date.Day) - Convert.ToInt32(Convert.ToDateTime(reminderItem["Modified"]).Date.Day);//
                                            if (Convert.ToInt32(reminderItem["rmdDuration"]) == span.Days)
                                            {
                                            }
                                            else
                                                continue;
                                        }
                                        else
                                            continue;

                                        if (reminderItem["rmdRecurring"].ToString().ToUpper() == "NO" && Convert.ToInt32(reminderItem["NoOfRemindersSent"]) >= 1)
                                            continue;
                                    }
                                    else
                                        continue;

                                    SendMail(Convert.ToString(appraisalItem["appEmployeeCode"]), Convert.ToString(appraisalItem["appAppraisalStatus"]), mySiteWeb);

                                    if (reminderItem != null)
                                    {
                                        if (reminderItem["NoOfRemindersSent"] != null)
                                            reminderItem["NoOfRemindersSent"] = Convert.ToInt32(reminderItem["NoOfRemindersSent"]) + 1;
                                        else
                                            reminderItem["NoOfRemindersSent"] = 1;
                                        mySiteWeb.AllowUnsafeUpdates = true;
                                        reminderItem.Update();
                                        mySiteWeb.AllowUnsafeUpdates = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private SPListItem GetCurrentAppraisal(SPWeb mySiteWeb, int appraisalId)
        {
            SPListItem item;
            try
            {
                SPList appraisals = mySiteWeb.Lists["Appraisals"];
                item = appraisals.GetItemById(appraisalId);
                return item;
            }
            catch (Exception)
            {
                return null;
            }

        }

        StringDictionary EmailHeaders(SPListItem lstItem)
        {
            StringDictionary headers = new StringDictionary();

            return headers;
        }

        private void SendMail(string employeeCode, string status, SPWeb mySiteWeb)
        {
            try
            {
                string mailBody = string.Empty;
                string subject = string.Empty;
                string mail = string.Empty;
                StringDictionary headers;
                string DBurl = mySiteWeb.Url + "/_layouts/VFS_DashBoards/Dashboard.aspx"; ;
                headers = new StringDictionary();

                switch (status.Trim())
                {
                    case "H1-Awaiting Appraisee Goal Setting":
                        {
                            //mail = RemnderMailSettings.H1AwaitingAppraiseeGoalSetting;
                            mail = string.Format(RemnderMailSettings.H1AwaitingAppraiseeGoalSetting, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1-Awaiting Appraiser Goal Approval":
                        {

                            mail = string.Format(RemnderMailSettings.H1AwaitingAppraiserGoalApproval, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, GetCurrentPerformanceCycle(mySiteWeb), DBurl);

                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1-Goals Approved":
                        {
                            mail = string.Format(RemnderMailSettings.H1GoalsApproved,
                                GetCurrentPerformanceCycle(mySiteWeb),
                                GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name,
                                GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name,
                                GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name,
                                GetCurrentPerformanceCycle(mySiteWeb),
                                GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name,
                                DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1-Amend Goals":
                        {
                            mail = string.Format(RemnderMailSettings.H1AwaitingAppraiserGoalApproval, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1-Awaiting Self-evaluation":
                        {
                            mail = string.Format(RemnderMailSettings.H1AwaitingSelfevaluation, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1-Awaiting Appraiser Evaluation":
                        {
                            mail = string.Format(RemnderMailSettings.H1AwaitingAppraiserEvaluation, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1-Awaiting Reviewer Approval":
                        {
                            mail = string.Format(RemnderMailSettings.H1AwaitingReviewerApproval, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Reviewer", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, DBurl);
                            headers.Add("to", GetMailUserName("Reviewer", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email + "," + GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1 - Awaiting Appraisee Sign-off":
                        {
                            mail = string.Format(RemnderMailSettings.H1AwaitingAppraiseeSignoff, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Reviewer", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1 - Sign-off by Appraisee":
                        {
                            mail = string.Format(RemnderMailSettings.H1SignoffbyAppraisee, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("HR", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("HR", employeeCode, mySiteWeb).Email);
                            break;
                        }

                    case "H1 – Completed":
                        {
                            mail = string.Format(RemnderMailSettings.H1Completed, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1 - HR Review":
                        {
                            mail = string.Format(RemnderMailSettings.H1HRReview, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("HR", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Reviewer", employeeCode, mySiteWeb).Email + "," + GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H1 - Appraisee Appeal":
                        {
                            mail = string.Format(RemnderMailSettings.H1AppraiseeAppeal,
                                GetCurrentPerformanceCycle(mySiteWeb),
                                GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name,
                                GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name,
                                GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name,
                                GetCurrentPerformanceCycle(mySiteWeb),
                                DBurl);

                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Reviewer", employeeCode, mySiteWeb).Email + "," + GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - Awaiting Appraiser Goal Approval":
                        {
                            mail = string.Format(RemnderMailSettings.H2AwaitingAppraiserGoalApproval, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - Goals Approved":
                        {
                            mail = string.Format(RemnderMailSettings.H2GoalsApproved, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }

                    case "H2 - Awaiting Self-evaluation":
                        {
                            mail = string.Format(RemnderMailSettings.H2AwaitingSelfevaluation, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - Awaiting Appraiser Evaluation":
                        {
                            mail = string.Format(RemnderMailSettings.H2AwaitingAppraiserEvaluation, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - Awaiting Reviewer Approval":
                        {
                            mail = string.Format(RemnderMailSettings.H2AwaitingReviewerApproval, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Reviewer", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, employeeCode, DBurl);
                            headers.Add("to", GetMailUserName("Reviewer", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email + "," + GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - Awaiting Appraisee Sign-off":
                        {
                            mail = string.Format(RemnderMailSettings.H2AwaitingAppraiseeSignoff, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Reviewer", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }

                    case "H2 - Sign-off by Appraisee":
                        {
                            mail = string.Format(RemnderMailSettings.H2SignoffbyAppraisee, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("HR", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("HR", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - HR Review":
                        {
                            mail = string.Format(RemnderMailSettings.H2HRReview, GetCurrentPerformanceCycle(mySiteWeb), GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name, GetMailUserName("HR", employeeCode, mySiteWeb).Name, GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name, GetCurrentPerformanceCycle(mySiteWeb), DBurl);
                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Reviewer", employeeCode, mySiteWeb).Email + "," + GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email);
                            break;
                        }
                    case "H2 - Appraisee Appeal":
                        {
                            mail = string.Format(RemnderMailSettings.H2AppraiseeAppeal,
                                GetCurrentPerformanceCycle(mySiteWeb),
                                GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name,
                                GetMailUserName("Appraiser", employeeCode, mySiteWeb).Name,
                                GetMailUserName("Appraisee", employeeCode, mySiteWeb).Name,
                                GetCurrentPerformanceCycle(mySiteWeb),
                                DBurl);
                            headers.Add("to", GetMailUserName("Appraiser", employeeCode, mySiteWeb).Email);
                            headers.Add("cc", GetMailUserName("Appraisee", employeeCode, mySiteWeb).Email + "," + GetMailUserName("Reviewer", employeeCode, mySiteWeb).Email);
                            break;
                        }

                }

                subject = mail.Split('%')[0];
                mailBody = mail.Split('%')[1];
                headers.Add("subject", subject);
                headers.Add("content-type", "text/html");
                SPUtility.SendEmail(mySiteWeb, headers, mailBody);

            }
            catch (Exception ex)
            {

            }
        }

        private string GetCurrentPerformanceCycle(SPWeb mySiteWeb)
        {

            string currentPCycle = string.Empty;
            SPList appraisals = mySiteWeb.Lists["Performance Cycles"];
            SPListItemCollection appraisalCollection = appraisals.GetItems();
            if (appraisalCollection.Count > 0)
            {
                currentPCycle = Convert.ToString(appraisalCollection[appraisalCollection.Count - 1]["Performance_x0020_Cycle"]);
            }
            return currentPCycle;


        }

        private SPUser GetMailUserName(string userCode, string employeeCode, SPWeb mySiteWeb)
        {
            string appraiserCode = string.Empty;
            string assignedUser = string.Empty;
            SPUser spuser = null;

            switch (userCode)
            {
                case "Appraiser":
                    {
                        appraiserCode = GetCodesByAppraisee(employeeCode, "Appraiser", mySiteWeb);
                        assignedUser = GetAssignedToUser(appraiserCode, mySiteWeb); //workflowProperties.Item["appAppraiserCode"].ToString());
                        spuser = mySiteWeb.EnsureUser(assignedUser);
                        break;
                    }
                case "Reviewer":
                    {
                        appraiserCode = GetCodesByAppraisee(employeeCode, "Reviewer", mySiteWeb);
                        assignedUser = GetAssignedToUser(appraiserCode, mySiteWeb);  //workflowProperties.Item["appReviewerCode"].ToString());
                        spuser = mySiteWeb.EnsureUser(assignedUser);
                        break;
                    }
                case "HR":
                    {
                        appraiserCode = GetCodesByAppraisee(employeeCode, "HR", mySiteWeb);
                        assignedUser = GetAssignedToUser(appraiserCode, mySiteWeb); //workflowProperties.Item["appHRBusinessPartnerCode"].ToString());
                        spuser = mySiteWeb.EnsureUser(assignedUser);
                        break;
                    }
                case "Appraisee":
                    {
                        string strApprasee = GetAssignedToUser(employeeCode, mySiteWeb);
                        spuser = mySiteWeb.EnsureUser(strApprasee);
                        break;
                    }
            }
            return spuser;

        }

        private string GetCodesByAppraisee(string employeeCode, string flag, SPWeb currentWeb)
        {
            try
            {
                SPList lstEmployeeMaster = currentWeb.Lists["Employees Master"];

                SPQuery q = new SPQuery();
                q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";

                SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                SPListItem employeeItem = masterCollection[0];
                if (flag == "Appraiser")
                    return Convert.ToString(employeeItem["ReportingManagerEmployeeCode"]);
                else if (flag == "Reviewer")
                    return Convert.ToString(employeeItem["DepartmentHeadEmployeeCode"]);
                else if (flag == "HR")
                    return Convert.ToString(employeeItem["HRBusinessPartnerEmployeeCode"]);
                else
                    return string.Empty;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetAssignedToUser(string employeeCode, SPWeb currentWeb)
        {
            try
            {
                SPList lstEmployeeMaster = currentWeb.Lists["Employees Master"];

                SPQuery q = new SPQuery();
                q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";

                SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                SPListItem employeeItem = masterCollection[0];

                return Convert.ToString(employeeItem["WindowsLogin"]);

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
