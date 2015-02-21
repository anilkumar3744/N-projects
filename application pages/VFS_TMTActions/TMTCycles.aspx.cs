using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VFS.PMS.ApplicationPages.Helpers;
using System.Text;
using Microsoft.SharePoint.Linq;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_TMTActions
{
    public partial class TMTCycles : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool ckeckTMTUser = false;
            if (!IsPostBack)
            {
                //commented code while TMT Group not found in site
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPGroup TMTGroup = objWeb.Groups["TMT"];
                        ckeckTMTUser = objWeb.IsCurrentUserMemberOfGroup(TMTGroup.ID);
                    }
                }
                SPListItem dtMaster = GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
                if (ckeckTMTUser && dtMaster != null)
                {
                    GetPerformanceCycles();
                }
                else
                {
                    string url = SPContext.Current.Web.Url + "/_layouts/VFS_DashBoards/Dashboard.aspx";
                    Context.Response.Write("<script type='text/javascript'>alert('You do not have permissions to view this page');window.open('" + url + "','_self'); </script>");
                }
            }
        }

        protected void lnkStart_Click(object sender, EventArgs e)
        {
            try
            {
                dvStartPerformanceCycle.Visible = true;
                dvManagePerformaceCycle.Visible = false;
                dvLock.Visible = false;

                GetClosed();
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS TMTCycles PageLoad");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }

        }

        protected void lnkManage_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateDetails();
                //if (IsPerformanceCycleStarted())
                //{
                //    if (IsPerformanceCyclePhasesDefined())
                //    {
                dvManagePerformaceCycle.Visible = true;
                dvLock.Visible = false;
                dvStartPerformanceCycle.Visible = false;

                //}
                //else
                //{
                //    string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                //    Context.Response.Write("<script type='text/javascript'>alert('Please define the performance cycle phases duration in the list - Performance Cycle Phases');window.open('" + url + "','_self'); </script>");
                //}
                //}
                //else
                //{
                //    string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                //    Context.Response.Write("<script type='text/javascript'>alert('Performance cycle is not Started');window.open('" + url + "','_self'); </script>");

                //}
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS TMTCycles lnkManage");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void lnkLock_Click(object sender, EventArgs e)
        {
            try
            {
                dvLock.Visible = true;
                dvStartPerformanceCycle.Visible = false;
                dvManagePerformaceCycle.Visible = false;

                SPList lstCategory;
                SPListItemCollection coll;
                SPListItem item;
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        lstCategory = objWeb.Lists["Performance Cycles"];

                        coll = lstCategory.GetItems();
                        DataTable dt = coll.GetDataTable();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[dt.Rows.Count - 1];
                            //item = coll[0];
                            lblCuurentCycle.Text = Convert.ToString(dr["Performance_x0020_Cycle"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["Description"])))
                                lblLockHistoryValue.Text = Convert.ToString(dr["Description"]);
                            lblCuurentCycle.Visible = true;
                            if (Convert.ToString(dr["Locked"]) == "0")
                            {
                                btnLock.Text = "Lock";
                            }
                            else
                            {
                                btnLock.Text = "Unlock";
                            }
                        }
                        else
                        {
                            btnLock.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS TMTCycles LinkLock");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }


        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPList lstCategory = objWeb.Lists["TMT Actions"];

                        int performanceID = GetCurrentPerformanceYearId("TMT Actions");
                        SPListItem lstItem = lstCategory.GetItemById(performanceID);

                        lstItem["IsClosed"] = true;
                        lstItem["ClosedBy"] = SPContext.Current.Web.CurrentUser;
                        lstItem["CycleCloseDate"] = DateTime.UtcNow;

                        lstItem["tmtIsH1GoalSettingStarted"] = "Closed";
                        lstItem["tmtIsH1SelfEvaluationStarted"] = "Closed";
                        lstItem["tmtIsH2SelfEvaluationStarted"] = "Closed";

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                        SPListItem spMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);

                        SPList performanceCycleActions = objWeb.Lists["Performance Cycle Activity"];
                        int performanceActID = GetCurrentPerformanceYearId("Performance Cycle Activity");
                        SPListItem pcItem = performanceCycleActions.GetItemById(performanceActID);

                        pcItem["CycleCloseDate"] = DateTime.Today.Date;
                        if (spMaster["EmployeeCode"] != null)
                            pcItem["CycleClosedBy"] = Convert.ToDecimal(spMaster["EmployeeCode"]);

                        //pcItem[""] = "Closed";
                        //pcItem[""] = "Closed";
                        //pcItem[""] = "Closed";

                        objWeb.AllowUnsafeUpdates = true;
                        pcItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        dvStartPerformanceCycle.Visible = false;
                        dvManagePerformaceCycle.Visible = false;
                        dvLock.Visible = false;

                        ExportEmployeeDataToSAPAsCSV(objWeb, pcItem["Performance Cycle"].ToString());
                    }

                    string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                    Context.Response.Write("<script type='text/javascript'>alert('The Performance Cycle " + lblCuurentCycle.Text + "  is Closed');window.open('" + url + "','_self'); </script>");

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS TMTCycles Close Click");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");


            }

        }

        protected void BtnLock_Click(object sender, EventArgs e)
        {
            try
            {
                SPList lstCategory;
                SPListItemCollection coll;
                SPListItem item;
                string strMsg = string.Empty;
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        lstCategory = objWeb.Lists["Performance Cycles"];
                        coll = lstCategory.GetItems();
                        DataTable dt = coll.GetDataTable();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[dt.Rows.Count - 1];
                            item = lstCategory.GetItemById(Convert.ToInt32(dr["ID"]));
                            if (btnLock.Text == "Unlock")
                            {
                                item["Locked"] = false;
                                item["Description"] = "Unlocked by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                                strMsg = "Unlocked";
                            }
                            else
                            {
                                item["Locked"] = true;
                                item["Description"] = "Locked by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                                strMsg = "Locked";
                            }
                            objWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            objWeb.AllowUnsafeUpdates = false;

                        }

                    }

                    dvStartPerformanceCycle.Visible = false;
                    dvManagePerformaceCycle.Visible = false;
                    dvLock.Visible = false;

                    string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                    Context.Response.Write("<script type='text/javascript'>alert('The Performance cycle " + lblCuurentCycle.Text + " is " + strMsg + "');window.open('" + url + "','_self'); </script>");

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS TMTCycles Lock Click");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void BtnCycleStart_Click(object sender, EventArgs e)
        {
            try
            {
                SPList lstCategory;
                SPListItem item;
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        objWeb.AllowUnsafeUpdates = true;

                        lstCategory = objWeb.Lists["Performance Cycles"];
                        SPListItem item1 = lstCategory.AddItem();
                        item1["Performance_x0020_Cycle"] = Convert.ToInt32(lblPerformanceCycleV.Text);

                        item1.Update();

                        lstCategory = objWeb.Lists["TMT Actions"];
                        item = lstCategory.AddItem();
                        item["tmtPerformanceCycle"] = Convert.ToInt32(lblPerformanceCycleV.Text);

                        item.Update();

                        string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                        SPListItem spMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);

                        SPList performanceCycleActions = objWeb.Lists["Performance Cycle Activity"];

                        item = performanceCycleActions.AddItem();
                        item["PerformanceCycle"] = Convert.ToInt32(lblPerformanceCycleV.Text);
                        item.Update();

                        SPList performanceCyclePhases = objWeb.Lists["Performance Cycle Phases"];

                        item = performanceCyclePhases.AddItem();
                        SPFieldLookupValue spv;
                        spv = new SPFieldLookupValue(item1.ID, lblPerformanceCycleV.Text);
                        item["Performance_x0020_Cycle"] = spv;

                        int intSelectedId = GetPhaseID(objWeb, "H1");
                        SPFieldLookupValue spv1;
                        spv1 = new SPFieldLookupValue(intSelectedId, "H1");

                        item["Phase"] = spv1;
                        item["Start_x0020_Date"] = Convert.ToDateTime(lblPerformanceCycleV.Text + "/01/01");
                        item["End_x0020_Date"] = Convert.ToDateTime(lblPerformanceCycleV.Text + "/06/30");
                        item.Update();

                        item = performanceCyclePhases.AddItem();
                        item["Performance_x0020_Cycle"] = spv;

                        int H2Id = GetPhaseID(objWeb, "H2");
                        SPFieldLookupValue spvH2;
                        spvH2 = new SPFieldLookupValue(H2Id, "H2");

                        item["Phase"] = spvH2;
                        item["Start_x0020_Date"] = Convert.ToDateTime(lblPerformanceCycleV.Text + "/07/01");
                        item["End_x0020_Date"] = Convert.ToDateTime(lblPerformanceCycleV.Text + "/12/31");
                        item.Update();

                        Web.AllowUnsafeUpdates = false;
                    }
                    GetPerformanceCycles();

                    dvStartPerformanceCycle.Visible = false;
                    dvManagePerformaceCycle.Visible = false;
                    dvLock.Visible = false;

                    string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                    Context.Response.Write("<script type='text/javascript'>alert('The Performance cycle " + lblPerformanceCycleV.Text + " Started');window.open('" + url + "','_self'); </script>");

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS TMTCycles Cycle Start");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        //protected void ViewDetails()
        //{
        //    lblGoalSettingH1S.Text = "Started";
        //    btnGoalSetting.Visible = false;
        //    lblGoalSettingH1S.Visible = true;

        //    btnH1SelfEvaluation.Visible = false;
        //    lblH1SelfEvaluationH1S.Visible = true;
        //    lblH1SelfEvaluationH1S.Text = "Stared";
        //    string H1SelfEval = string.Empty;
        //    string H2SelfEval = string.Empty;
        //    using (SPWeb objWeb = SPContext.Current.Site.OpenWeb())
        //    {
        //        SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
        //        SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));
        //        H1SelfEval = Convert.ToString(lstItem["tmtIsH1SelfEvaluationStarted"]);
        //        H2SelfEval = Convert.ToString(lstItem["tmtIsH2SelfEvaluationStarted"]);

        //        lblHistoryH1Start.Text = Convert.ToString(lstItem["tmtH1History"]);
        //        lblHistoryH1SelStart.Text = Convert.ToString(lstItem["tmtH1SelfHistory"]);
        //        lblHistoryH2SelStart.Text = Convert.ToString(lstItem["tmtH2SelHistory"]);
        //    }

        //    if (!string.IsNullOrEmpty(H2SelfEval))
        //    { // All are labels
        //        lblGoalSettingH1S.Visible = true;
        //        lblH1SelfEvaluationH1S.Visible = true;
        //        lblH1SelfEvaluationH2S.Visible = true;
        //        lblGoalSettingH1S.Text = "Started";
        //        lblH1SelfEvaluationH1S.Text = "Started";
        //        lblH1SelfEvaluationH2S.Text = "Started";
        //    }
        //    else if (string.IsNullOrEmpty(H1SelfEval))
        //    {
        //        btnH1SelfEvaluation.Visible = false;
        //        lblH1SelfEvaluationH1S.Visible = true;
        //        lblH1SelfEvaluationH1S.Text = "NotStarted";
        //    }
        //    else
        //    {

        //        btnH2SelfEvaluation.Visible = false;
        //        btnH1SelfEvaluation.Visible = false;
        //        lblH1SelfEvaluationH2S.Visible = true;
        //        //lblH1SelfEvaluationH2S.Text = "Started";
        //    }
        //}

        protected void btn_h1selfevaluation(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPLongOperation longOperation = new SPLongOperation(this.Page))
                {
                    try
                    {
                        longOperation.LeadingHTML = "H1 Self evaluation.";
                        longOperation.TrailingHTML = "Please wait while the appraisals are being created.";
                        longOperation.Begin();
                        //bool flag = CheckH1GoalSettingCompleted(Convert.ToInt32(lblPerformanceCycleValue.Text), "H1-Goals Approved");
                        //if (!flag)
                        //{
                        SPListItemCollection masterCollection = GetAllEmployeeMasters();
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb objWeb = osite.OpenWeb())
                            {                                
                                objWeb.AllowUnsafeUpdates = true;
                                SPList lstCategory = objWeb.Lists["TMT Actions"];

                                int performanceID = GetCurrentPerformanceYearId("TMT Actions");
                                SPListItem lstItem = lstCategory.GetItemById(performanceID);

                                lstItem["tmtIsH1SelfEvaluationStarted"] = "Started";
                                lstItem["tmtH1SelfHistory"] = "H1 Self-evaluation was started by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");

                                //lstItem["tmtPerformanceCycle"] = Convert.ToString(DateTime.Now.Year);
                                objWeb.AllowUnsafeUpdates = true;
                                lstItem.Update();
                                objWeb.AllowUnsafeUpdates = false;

                                string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                                SPListItem spMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);

                                SPList performanceCycleActions = objWeb.Lists["Performance Cycle Activity"];
                                int performanceActID = GetCurrentPerformanceYearId("Performance Cycle Activity");
                                SPListItem pcItem = performanceCycleActions.GetItemById(performanceActID);

                                pcItem["H1SelfEvaluationStartDate"] = DateTime.Today.Date;
                                if (spMaster["EmployeeCode"] != null)
                                    pcItem["H1SelfEvaluationStartedBy"] = Convert.ToDecimal(spMaster["EmployeeCode"]);

                                objWeb.AllowUnsafeUpdates = true;
                                pcItem.Update();
                                objWeb.AllowUnsafeUpdates = false;

                                string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                                Context.Response.Write("<script type='text/javascript'>alert('H1 - Self-evaluation started successfully');window.open('" + url + "','_self'); </script>");
                                objWeb.AllowUnsafeUpdates = false;

                                foreach (SPListItem item in masterCollection)
                                {
                                    if (Convert.ToString(item["EmployeeGroup"]).Split('#')[1] == "A")
                                    {
                                        if (!CheckAppraisal(objWeb, Convert.ToString(item["EmployeeCode"])))
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDate"])) && Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                                            {
                                                if (Convert.ToDateTime(Convert.ToString(item["ConfirmationDate"])) <= GetPhaseEndDate(objWeb, "H2")) 
                                                {
                                                    CreateAppraisalsForH2EligibleEmployees(objWeb, item);
                                                }
                                            }
                                            else if (Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDueDate"]))) //!string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDueDate"])))
                                            {
                                                if (Convert.ToDateTime(Convert.ToString(item["ConfirmationDueDate"])) <= GetPhaseEndDate(objWeb, "H2"))
                                                {
                                                    CreateAppraisalsForH2EligibleEmployees(objWeb, item);
                                                }
                                            }
                                            else if (!Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(item["HireDate"])))
                                            {
                                                if (Convert.ToDateTime(Convert.ToString(item["HireDate"])).AddMonths(3) <= GetPhaseEndDate(objWeb, "H2"))
                                                {
                                                    CreateAppraisalsForH2EligibleEmployees(objWeb, item);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        WorkflowTriggering();
                        dvStartPerformanceCycle.Visible = false;
                        dvManagePerformaceCycle.Visible = false;
                        dvLock.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        LogHandler.LogError(ex, "Error in PMS TMTCycles btn_h1selfevaluation");
                        Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
                    } string redirectURL = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                    longOperation.End(redirectURL);
                }
            });
            //popup();
        }

        private bool CheckAppraisal(SPWeb web, string employeeCode)
        {
            bool flag = false;
            try
            {
                string currentCyccle = lblPerformanceCycleValue.Text.Trim();
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

        private void CreateAppraisalsForH2EligibleEmployees(SPWeb currentWeb, SPListItem item)
        {
            try
            {
                SPList appraisals = currentWeb.Lists["Appraisals"];
                SPList lstAppraisalStatus = currentWeb.Lists["Appraisal Status"];

                SPListItem appraisalItem = appraisals.AddItem();
                appraisalItem["appPerformanceCycle"] = lblPerformanceCycleValue.Text;
                appraisalItem["appEmployeeCode"] = item["EmployeeCode"].ToString();
                appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(12)["Appraisal_x0020_Workflow_x0020_S"]);
                appraisalItem["appH1GoalSettingStartDate"] = DateTime.Now.Date;
                currentWeb.AllowUnsafeUpdates = true;
                appraisalItem.Update();
                currentWeb.AllowUnsafeUpdates = false;

                currentWeb.AllowUnsafeUpdates = true;
                const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";
                SPWorkflowManager WFmanager = SPContext.Current.Site.WorkflowManager;
                SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
                SPWorkflowCollection workflowColl = WFmanager.GetItemWorkflows(appraisalItem);

                if (workflowColl.Count == 0)
                {
                    SPWorkflow workflow = WFmanager.StartWorkflow(appraisalItem, WFAssociations, "", true);
                }
                currentWeb.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnH2SelfEvaluation_Click(object sender, EventArgs e)
        {
            btnH2SelfEvaluation.Visible = false;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPLongOperation longOperation = new SPLongOperation(this.Page))
                {
                    try
                    {
                        longOperation.LeadingHTML = "H2 Self evaluation.";
                        longOperation.TrailingHTML = "Please wait while the appraisals are being created.";
                        longOperation.Begin();
                        //bool flag = CheckH1GoalSettingCompleted(Convert.ToInt32(lblPerformanceCycleValue.Text), "H2 - Goals Approved");
                        //if (!flag)
                        //{
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb objWeb = osite.OpenWeb())
                            {
                                objWeb.AllowUnsafeUpdates = true;
                                SPList lstCategory = objWeb.Lists["TMT Actions"];

                                int performanceID = GetCurrentPerformanceYearId("TMT Actions");
                                SPListItem lstItem = lstCategory.GetItemById(performanceID);

                                lstItem["tmtIsH2SelfEvaluationStarted"] = "Started";
                                lstItem["tmtH2SelHistory"] = "H2 Self-evaluation was started by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");

                                objWeb.AllowUnsafeUpdates = true;
                                lstItem.Update();
                                objWeb.AllowUnsafeUpdates = false;

                                string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                                SPListItem spMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);

                                SPList performanceCycleActions = objWeb.Lists["Performance Cycle Activity"];
                                int performanceActID = GetCurrentPerformanceYearId("Performance Cycle Activity");
                                SPListItem pcItem = performanceCycleActions.GetItemById(performanceActID);

                                pcItem["H2SelfEvaluationStartDate"] = DateTime.Today.Date;
                                if (spMaster["EmployeeCode"] != null)
                                    pcItem["H2SelfEvaluationStartedBy"] = Convert.ToDecimal(spMaster["EmployeeCode"]);

                                objWeb.AllowUnsafeUpdates = true;
                                pcItem.Update();
                                objWeb.AllowUnsafeUpdates = false;

                                //SPList appraisalTasks = objWeb.Lists["VFSAppraisalTasks"];
                                //SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];

                                //SPQuery q = new SPQuery();
                                //q.Query = "<Where><Eq><FieldRef Name='Status'/><Value Type='Text'>Approved</Value></Eq></Where>"; //" + Convert.ToString(lstAppraisalStatus.GetItemById(3)["Appraisal_x0020_Workflow_x0020_S"]) + "
                                //SPListItemCollection taskItemCollection = appraisalTasks.GetItems(q);

                                //Hashtable ht = new Hashtable();

                                //foreach (SPListItem item in taskItemCollection)
                                //{
                                //    ht["glsTaskStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(4)["Appraisal_x0020_Workflow_x0020_S"]);
                                //    ht["Status"] = "Not started";                   
                                //    SPWorkflowTask.AlterTask(item, ht, true);
                                //}

                                WorkflowTriggeringForH2SelfEvaluation();

                                //popup();


                                dvStartPerformanceCycle.Visible = false;
                                dvManagePerformaceCycle.Visible = false;
                                dvLock.Visible = false;
                                //Performance Cycle " + lblPerformanceCycleValue.Text + "
                                string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                                Context.Response.Write("<script type='text/javascript'>alert('H2 - Self-evaluation started successfully');window.open('" + url + "','_self'); </script>");
                                objWeb.AllowUnsafeUpdates = false;
                            }

                        }
                        //}
                        //else
                        //{
                        //    string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx"; // The Performance cycle " + lblPerformanceCycleValue.Text + "
                        //    Context.Response.Write("<script type='text/javascript'>alert('Some of the Appraisals are pending Performance Cycle " + lblPerformanceCycleValue.Text + "'); </script>");
                        //}

                    }
                    catch (Exception ex)
                    {
                        LogHandler.LogError(ex, "Error in PMS TMTCycles btnH2SelfEvaluation_Click");
                        Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
                    }
                    string redirectURL = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                    longOperation.End(redirectURL);
                }
            });
        }

        protected void BtnGoalSetting_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPLongOperation longOperation = new SPLongOperation(this.Page))
                {
                    try
                    {

                        longOperation.LeadingHTML = "Creating appraisals.";
                        longOperation.TrailingHTML = "Please wait while the appraisals being created.";
                        longOperation.Begin();
                        btnGoalSetting.Visible = false;
                        // SPListItem masteritem = GetEmployeeMaster();             
                        SPListItemCollection masterCollection = GetAllEmployeeMasters();
                        DataTable dtMasters = masterCollection.GetDataTable();
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb currentWeb = osite.OpenWeb())
                            {
                                currentWeb.AllowUnsafeUpdates = true;
                                SPList appraisals = currentWeb.Lists["Appraisals"];
                                SPList lstAppraisalStatus = currentWeb.Lists["Appraisal Status"];

                                foreach (SPListItem item in masterCollection)
                                {
                                    if (Convert.ToString(item["EmployeeGroup"]).Split('#')[1] == "A")
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDate"])) && Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                                        {
                                            if (Convert.ToDateTime(Convert.ToString(item["ConfirmationDate"])) <= GetPhaseEndDate(currentWeb, "H1")) //need to modify h1 enddate
                                            {
                                                InsertAppraisalsForCreateAppraisals(currentWeb, item);
                                            }
                                        }
                                        else if (Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDueDate"]))) //!string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDueDate"])))
                                        {
                                            //if (!string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDueDate"])))
                                            //{
                                            if (Convert.ToDateTime(Convert.ToString(item["ConfirmationDueDate"])) <= GetPhaseEndDate(currentWeb, "H1")) //need to modify h1 enddate
                                            {
                                                InsertAppraisalsForCreateAppraisals(currentWeb, item);
                                            }
                                            //}
                                        }
                                        else if (!Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(item["HireDate"])))
                                        {
                                            if (Convert.ToDateTime(Convert.ToString(item["HireDate"])).AddMonths(3) <= GetPhaseEndDate(currentWeb, "H1")) //need to modify h1 enddate
                                            {
                                                InsertAppraisalsForCreateAppraisals(currentWeb, item);
                                            }
                                        }
                                        //else if (string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDueDate"])) && string.IsNullOrEmpty(Convert.ToString(item["ConfirmationDate"])))
                                        //{
                                        //if (Convert.ToString(item["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                                        //{
                                        //    if (Convert.ToDateTime(Convert.ToString(item["HireDate"])).AddMonths(3) <= GetPhaseEndDate(currentWeb, "H1")) //need to modify h1 enddate
                                        //    {
                                        //        InsertAppraisalsForCreateAppraisals(currentWeb, item);
                                        //    }
                                        //}
                                        //}
                                    }
                                }

                                SPListItemCollection appraisalCollection = appraisals.GetItems();
                                foreach (SPListItem item in appraisalCollection)
                                {
                                    Web.AllowUnsafeUpdates = true;
                                    const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";
                                    SPWorkflowManager WFmanager = SPContext.Current.Site.WorkflowManager;
                                    SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
                                    SPWorkflowCollection workflowColl = WFmanager.GetItemWorkflows(item);

                                    if (workflowColl.Count == 0)
                                    {
                                        SPWorkflow workflow = WFmanager.StartWorkflow(item, WFAssociations, "", true);
                                    }
                                    Web.AllowUnsafeUpdates = false;
                                }

                                SPList lstCategory = currentWeb.Lists["TMT Actions"];

                                int performanceID = GetCurrentPerformanceYearId("TMT Actions");
                                SPListItem lstItem = lstCategory.GetItemById(performanceID);

                                lstItem["tmtIsH1GoalSettingStarted"] = "Started";
                                lstItem["tmtPerformanceCycle"] = lblPerformanceCycleValue.Text;
                                lstItem["tmtH1History"] = "H1 Goal setting was started by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                                Web.AllowUnsafeUpdates = true;
                                lstItem.Update();
                                Web.AllowUnsafeUpdates = false;

                                string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                                SPListItem spMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);

                                SPList performanceCycleActions = currentWeb.Lists["Performance Cycle Activity"];
                                int performanceActID = GetCurrentPerformanceYearId("Performance Cycle Activity");
                                SPListItem pcItem = performanceCycleActions.GetItemById(performanceActID);

                                pcItem["H1GoalSettingStartDate"] = DateTime.Today.Date;
                                if (spMaster != null)
                                    pcItem["H1GoalSettingStartedBy"] = Convert.ToDecimal(spMaster["EmployeeCode"]);

                                currentWeb.AllowUnsafeUpdates = true;
                                pcItem.Update();
                                currentWeb.AllowUnsafeUpdates = false;

                                dvStartPerformanceCycle.Visible = false;
                                dvManagePerformaceCycle.Visible = false;
                                dvLock.Visible = false;
                                currentWeb.AllowUnsafeUpdates = false;
                            }

                            string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                            Context.Response.Write("<script type='text/javascript'>alert('H1 - Goal setting started successfully');window.open('" + url + "','_self'); </script>");
                            // Performance Cycle " + lblPerformanceCycleValue.Text + "
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.StackTrace trce = new System.Diagnostics.StackTrace(ex, true);

                        LogHandler.LogError(ex, "Error in PMS TMTCycles BtnGoalSetting_Click");
                        Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
                    }
                    string redirectURL = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                    longOperation.End(redirectURL);
                }

            });

        }

        private DateTime GetPhaseEndDate(SPWeb currentWeb, string phase)
        {
            DateTime date = DateTime.Now;
            using (VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(currentWeb.Url))
            {
                var performancePhases = (from p in dc.PerformanceCyclePhases.AsEnumerable()
                                         where p.PerformanceCycle.PerformanceCycle.ToString().Trim() == lblPerformanceCycleValue.Text && p.Phase.Phase.ToString().Trim() == phase
                                         select p).FirstOrDefault();
                if (performancePhases != null)
                    date = Convert.ToDateTime(performancePhases.EndDate);
            }

            return date;
        }

        private void InsertAppraisalsForCreateAppraisals(SPWeb currentWeb, SPListItem item)
        {
            SPList appraisals = currentWeb.Lists["Appraisals"];
            SPList lstAppraisalStatus = currentWeb.Lists["Appraisal Status"];

            SPListItem appraisalItem = appraisals.AddItem();
            appraisalItem["appPerformanceCycle"] = lblPerformanceCycleValue.Text;
            appraisalItem["appEmployeeCode"] = item["EmployeeCode"].ToString();
            appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(1)["Appraisal_x0020_Workflow_x0020_S"]);
            appraisalItem["appH1GoalSettingStartDate"] = DateTime.Now.Date;
            Web.AllowUnsafeUpdates = true;
            appraisalItem.Update();
            Web.AllowUnsafeUpdates = false;
        }

        protected void popup()
        {
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.Flush();
            Context.Response.End();

        }

        private SPListItemCollection GetAllEmployeeMasters()
        {
            SPListItemCollection masterItemsColl = null;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists["Employees Master"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><And><IsNotNull><FieldRef Name='WindowsLogin' /></IsNotNull><And><Eq><FieldRef Name='EmployeeStatus' /><Value Type='Text'>Active</Value></Eq><Eq><FieldRef Name='EmployeeGroup' /><Value Type='Lookup'>A</Value></Eq></And></And></Where>";
                        masterItemsColl = employeMaster.GetItems(q);
                    }
                    return masterItemsColl;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private SPListItem GetEmployeeMaster()
        {
            SPListItem masterItem = null;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists["Employees Master"];
                        return masterItem = employeMaster.GetItemById(2639);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private bool CheckH1GoalSettingCompleted(int cycle, string status)
        {
            try
            {
                bool flag = false;
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPList appraisals = objWeb.Lists["Appraisals"];
                        SPQuery q = new SPQuery();
                        //q.Query = "<Where><Eq><FieldRef Name='appPerformanceCycle' /><Value Type='Number'>" + cycle + "</Value></Eq></Where>";
                        q.Query = "<Where><And><Eq><FieldRef Name='appPerformanceCycle' /><Value Type='Number'>" + cycle + "</Value></Eq><IsNull><FieldRef Name='appDeactivated' /></IsNull></And></Where>";
                        SPListItemCollection coll = appraisals.GetItems(q);
                        foreach (SPListItem item in coll)
                        {
                            if (Convert.ToString(item["appAppraisalStatus"]) != status)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WorkflowTriggering()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPList appraisalTasks = objWeb.Lists["VFSAppraisalTasks"];
                        SPQuery q = new SPQuery();

                        q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H1-Goals Approved</Value></Eq></And></Where>";
                        SPListItemCollection taskItemCollection = appraisalTasks.GetItems(q);

                        SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];
                        int i = 0;

                        Hashtable ht = new Hashtable();
                        foreach (SPListItem item in taskItemCollection)
                        {
                            i++;
                            //item[SPBuiltInFieldId.WorkflowVersion] = i;
                            //item.SystemUpdate();
                            ht["tskStatus"] = "H1-Awaiting Self-evaluation";
                            ht["Status"] = "H1-Awaiting Self-evaluation";
                            objWeb.AllowUnsafeUpdates = true;
                            SPWorkflowTask.AlterTask(item, ht, true);

                            SPList appraisals = objWeb.Lists["Appraisals"];
                            SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
                            appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(4)["Appraisal_x0020_Workflow_x0020_S"]);
                            appraisalItem["appH1AppraisalEvaluationStartDat"] = DateTime.Now.Date;

                            appraisalItem.Update();
                            objWeb.AllowUnsafeUpdates = false;
                        }

                        SPQuery q2 = new SPQuery();
                        q2.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Eq></And></Where>";
                        SPListItemCollection deactivatedTasks = appraisalTasks.GetItems(q2);
                        foreach (SPListItem item in deactivatedTasks)
                        {
                            SPListItem masterItem = GetUserByCode(objWeb, Convert.ToString(item["tskAppraiseeCode"]));
                            if (H2Eligibility(masterItem, objWeb))
                            {
                                ht["tskStatus"] = "Approved";
                                ht["Status"] = "Approved";
                                objWeb.AllowUnsafeUpdates = true;
                                SPWorkflowTask.AlterTask(item, ht, true);

                                SPList appraisals = objWeb.Lists["Appraisals"];
                                SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
                                appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(12)["Appraisal_x0020_Workflow_x0020_S"]);
                                //appraisalItem["appH1AppraisalEvaluationStartDat"] = DateTime.Now.Date;
                                appraisalItem["appDeactivated"] = string.Empty;
                                //objWeb.AllowUnsafeUpdates = true;
                                appraisalItem.Update();
                                objWeb.AllowUnsafeUpdates = false;
                            }
                        }



                    }

                }
            });
        }

        private bool H2Eligibility(SPListItem masterItem, SPWeb currentWeb)
        {
            bool flag = false;
            try
            {
                if (Convert.ToString(masterItem["EmployeeGroup"]).Split('#')[1] == "A" && Convert.ToString(masterItem["EmployeeStatus"]).ToUpper() == "ACTIVE")
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(masterItem["ConfirmationDate"])) && Convert.ToString(masterItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P"))
                    {
                        //create appraisal with status ‘H2 Awaiting appraisee goal setting’
                        //CreateAppraisalForEligibleEmployee(properties, 12);
                        flag = true;
                    }
                    //else if (!string.IsNullOrEmpty(properties.ListItem["ConfirmationDueDate"].ToString()))
                    //{
                    else if (Convert.ToString(masterItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(masterItem["ConfirmationDueDate"])))
                    {
                        if (Convert.ToDateTime(Convert.ToString(masterItem["ConfirmationDueDate"])) <= GetPhaseEndDate(currentWeb, "H2"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            //CreateAppraisalForEligibleEmployee(properties, 12);
                            flag = true;
                        }
                    }
                    else if (!Convert.ToString(masterItem["EmployeeSubGroup"]).Split('#')[1].StartsWith("P") && !string.IsNullOrEmpty(Convert.ToString(masterItem["HireDate"])))
                    {
                        if (Convert.ToDateTime(masterItem["HireDate"].ToString()).AddMonths(3) <= GetPhaseEndDate(currentWeb, "H2"))
                        {
                            //create an appraisal record with status ‘H2 - Awaiting Appraisee Goal Setting’
                            //CreateAppraisalForEligibleEmployee(properties, 12);
                            flag = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //LogHandler.LogError(ex, "EmployeeDataReciever.H2Eligibility");
            }
            return flag;
        }

        public SPListItem GetUserByCode(SPWeb currentWeb, string employeeCode)
        {
            try
            {
                SPList lstEmployeeMaster = currentWeb.Lists["Employees Master"];

                SPQuery q = new SPQuery();
                q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";

                SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                SPListItem employeeItem = masterCollection[0];

                return employeeItem;

            }
            catch (Exception)
            {
                return null;
            }
        }

        private void WorkflowTriggeringForH2SelfEvaluation()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPList appraisalTasks = objWeb.Lists["VFSAppraisalTasks"];
                        SPQuery q = new SPQuery();
                        //q.Query = "<Where><Eq><FieldRef Name='Status'/><Value Type='Text'>Not started</Value></Eq></Where>"; //" + Convert.ToString(lstAppraisalStatus.GetItemById(3)["Appraisal_x0020_Workflow_x0020_S"]) + "
                        //q.Query = "<Where><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H1-Goals Approved</Value></Eq></And></Where>"; //" + Convert.ToString(lstAppraisalStatus.GetItemById(3)["Appraisal_x0020_Workflow_x0020_S"]) + "

                        q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H2 - Goals Approved</Value></Eq></And></Where>";
                        SPListItemCollection taskItemCollection = appraisalTasks.GetItems(q);

                        SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];
                        int i = 0;

                        Hashtable ht = new Hashtable();
                        foreach (SPListItem item in taskItemCollection)
                        {
                            //i++;
                            //item[SPBuiltInFieldId.WorkflowVersion] = i;
                            //item.SystemUpdate();
                            ht["tskStatus"] = "H2 - Awaiting Self-evaluation";   //Convert.ToString(lstAppraisalStatus.GetItemById(15)["Appraisal_x0020_Workflow_x0020_S"]);
                            ht["Status"] = "H2 - Awaiting Self-evaluation";
                            objWeb.AllowUnsafeUpdates = true;
                            SPWorkflowTask.AlterTask(item, ht, true);

                            SPList appraisals = objWeb.Lists["Appraisals"];
                            SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
                            appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(15)["Appraisal_x0020_Workflow_x0020_S"]);
                            appraisalItem["appH2AppraisalEvaluationStartDat"] = DateTime.Now.Date;

                            appraisalItem.Update();
                            objWeb.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }

        private void GetPerformanceCycles()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    //SPList lstCategory = objWeb.Lists["Performance Cycles"];
                    //SPListItemCollection coll;
                    //SPQuery q = new SPQuery();
                    //q.Query = "<OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";
                    //coll = lstCategory.GetItems(q);
                    //DataTable dt = coll.GetDataTable();
                    //gvCycles.DataSource = dt;
                    //gvCycles.DataBind();
                    SPList lstCategory = objWeb.Lists["TMT Actions"];
                    SPListItemCollection coll;
                    SPQuery q = new SPQuery();
                    q.Query = "<OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";
                    coll = lstCategory.GetItems(q);
                    DataTable dt = coll.GetDataTable();
                    gvCycles.DataSource = dt;
                    gvCycles.DataBind();
                }
            }
        }

        private int GetCurrentPerformanceYearId(string listName)
        {
            int Id = 0;
            DataTable dt = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[listName];
                    SPListItemCollection coll = lstCategory.GetItems();
                    if (coll.Count > 0)
                        dt = coll.GetDataTable();
                }
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[dt.Rows.Count - 1];
                    Id = Convert.ToInt32(dr["ID"]);
                }
                return Id;
            }
        }

        private void GetClosed()
        {
            SPList lstCategory;
            SPListItemCollection coll;

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    lstCategory = objWeb.Lists["TMT Actions"];
                    coll = lstCategory.GetItems();
                    DataTable dt = coll.GetDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[dt.Rows.Count - 1];
                        if (Convert.ToString(dr["IsClosed"]) == "1")
                        {
                            lblPerformanceCycleV.Text = Convert.ToString(Convert.ToInt32(dr["tmtPerformanceCycle"]) + 1);
                            lblCuurentCycle.Visible = true;
                            btnCycleStart.Visible = true;
                        }
                        else
                        {
                            lblPerformanceCycleV.Visible = false;
                            btnCycleStart.Visible = false;
                        }
                    }
                    else
                    {
                        lblPerformanceCycleV.Text = Convert.ToString(DateTime.UtcNow.Year);
                        lblCuurentCycle.Visible = true;
                        btnCycleStart.Visible = true;
                    }
                }

            }
        }

        private bool IsPerformanceCyclePhasesDefined()
        {
            bool flag = false;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {

                    using (VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(currentWeb.Url))
                    {
                        var performancePhases = (from p in dc.PerformanceCyclePhases.AsEnumerable()
                                                 where p.PerformanceCycle.PerformanceCycle.ToString().Trim() == lblPerformanceCycleValue.Text
                                                 select p).FirstOrDefault();
                        if (performancePhases != null)
                            flag = true;
                    }
                }
            }

            return flag;
        }

        private bool IsPerformanceCycleStarted()
        {
            bool flag = false;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    using (VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(currentWeb.Url))
                    {
                        var performanceCycle = (from p in dc.PerformanceCycles.AsEnumerable()
                                                where p.PerformanceCycle.ToString().Trim() == lblPerformanceCycleValue.Text
                                                select p).FirstOrDefault();
                        if (performanceCycle != null)
                            flag = true;
                    }
                }
            }

            return flag;
        }

        private int GetPhaseID(SPWeb objWeb, string phase)
        {
            int ID = 0;
            SPList spList = objWeb.Lists["Phases"];
            SPQuery q = new SPQuery();
            q.Query = "<Where><Eq><FieldRef Name='Phase' /><Value Type='Text'>" + phase + "</Value></Eq></Where>";
            SPListItemCollection coll = spList.GetItems(q);
            SPListItem item = coll[0];
            ID = item.ID;
            return ID;
        }

        protected void UpdateDetails()
        {
            //btnGoalSetting.Visible = false;
            //lblGoalSettingH1S.Visible = true;
            //lblGoalSettingH1S.Text = "Started";
            string H1initial = string.Empty;
            string H1SelfEval = string.Empty;
            string H2SelfEval = string.Empty;
            bool flag = false;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists["TMT Actions"];

                    int performanceID = GetCurrentPerformanceYearId("TMT Actions");
                    if (performanceID != 0)
                    {
                        SPListItem lstItem = lstCategory.GetItemById(performanceID);

                        H1initial = Convert.ToString(lstItem["tmtIsH1GoalSettingStarted"]);
                        H1SelfEval = Convert.ToString(lstItem["tmtIsH1SelfEvaluationStarted"]);
                        H2SelfEval = Convert.ToString(lstItem["tmtIsH2SelfEvaluationStarted"]);
                        lblHistoryH1Start.Text = Convert.ToString(lstItem["tmtH1History"]);
                        lblHistoryH1SelStart.Text = Convert.ToString(lstItem["tmtH1SelfHistory"]);
                        lblHistoryH2SelStart.Text = Convert.ToString(lstItem["tmtH2SelHistory"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(lstItem["ClosedBy"])))
                            lblHistoryClosed.Text = "Performance Cycle" + Convert.ToString(lstItem["tmtPerformanceCycle"]) + " was closed by " + Convert.ToString(lstItem["ClosedBy"]).Split('#')[1] + "@ " + Convert.ToDateTime(lstItem["CycleCloseDate"]).ToString("dd-MMM-yyyy mm ss");
                        lblPerformanceCycleValue.Text = Convert.ToString(lstItem["tmtPerformanceCycle"]);

                        if (Convert.ToBoolean(lstItem["IsClosed"]))
                        {
                            lblPcycleCloseValue.Visible = true;
                            lblPcycleCloseValue.Text = "Closed";
                            lblGoalSettingH1S.Text = "Closed";
                            lblH1SelfEvaluationH1S.Text = "Closed";
                            lblH1SelfEvaluationH2S.Text = "Closed";
                            btnClose.Visible = false;
                            flag = true;
                        }
                        else
                        {
                            lblPcycleCloseValue.Visible = false;
                            lblPcycleCloseValue.Visible = true;
                            //btnClose.Visible = true;
                        }
                    }
                    else
                    {
                        string url = SPContext.Current.Web.Url + "/_layouts/VFS_TMTActions/TMTCycles.aspx";
                        Context.Response.Write("<script type='text/javascript'>alert('Performance cycle not yet started');window.open('" + url + "','_self'); </script>");
                    }
                }
                if (string.IsNullOrEmpty(H1initial))
                {
                    btnH1SelfEvaluation.Visible = false;
                    btnH2SelfEvaluation.Visible = false;
                    //btnClose.Visible = true;
                }
                else if (!string.IsNullOrEmpty(H1initial) && string.IsNullOrEmpty(H1SelfEval))
                {
                    btnH1SelfEvaluation.Visible = true;
                    lblGoalSettingH1S.Text = "Started";
                    lblGoalSettingH1S.Visible = true;
                    btnGoalSetting.Visible = false;
                    lblH1SelfEvaluationH1S.Visible = false;
                    //btnClose.Visible = true;
                    btnGoalSetting.Visible = false;
                }
                else if (!string.IsNullOrEmpty(H1initial) && !string.IsNullOrEmpty(H1SelfEval) && string.IsNullOrEmpty(H2SelfEval))
                {
                    lblGoalSettingH1S.Visible = true;
                    lblH1SelfEvaluationH1S.Visible = true;
                    lblH1SelfEvaluationH2S.Visible = false;
                    lblGoalSettingH1S.Text = "Started";
                    lblH1SelfEvaluationH1S.Text = "Started";
                    //lblH1SelfEvaluationH2S.Text = "Started";
                    btnH2SelfEvaluation.Visible = true;
                    //btnClose.Visible = true;
                    btnGoalSetting.Visible = false;
                }
                else if (!string.IsNullOrEmpty(H1initial) && !string.IsNullOrEmpty(H1SelfEval) && !string.IsNullOrEmpty(H2SelfEval) && !flag)
                {
                    btnClose.Visible = true;
                    btnGoalSetting.Visible = false;
                    lblGoalSettingH1S.Visible = true;
                    lblH1SelfEvaluationH1S.Visible = true;
                    lblH1SelfEvaluationH2S.Visible = true;
                    lblGoalSettingH1S.Text = "Started";
                    lblH1SelfEvaluationH1S.Text = "Started";
                    lblH1SelfEvaluationH2S.Text = "Started";
                    //btnClose.Visible = true;
                    lblPcycleCloseValue.Visible = false;
                }
                else
                { // All are labels
                    lblGoalSettingH1S.Visible = true;
                    lblH1SelfEvaluationH1S.Visible = true;
                    lblH1SelfEvaluationH2S.Visible = true;
                    lblGoalSettingH1S.Text = "Closed";
                    lblH1SelfEvaluationH1S.Text = "Closed";
                    lblH1SelfEvaluationH2S.Text = "Closed";
                    //btnClose.Visible = true;
                    btnH2SelfEvaluation.Visible = false;
                    btnGoalSetting.Visible = false;
                }

                //else
                //{
                //    btnH2SelfEvaluation.Visible = true;
                //    btnH1SelfEvaluation.Visible = false;
                //    lblH1SelfEvaluationH2S.Visible = false;
                //    lblH1SelfEvaluationH1S.Visible = true;
                //    lblH1SelfEvaluationH1S.Text = "Started";
                //}  
            }
        }

        void ExportEmployeeDataToSAPAsCSV(SPWeb oWeb, string performanceCycle)
        {
            try
            {
                using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(oWeb.Url))
                {
                    var appraisalRecords = (from app in PMSDataContext.Appraisals.AsEnumerable()
                                            join appPhases in PMSDataContext.AppraisalPhases.AsEnumerable()
                                                on app.Id.ToString() equals appPhases.Appraisal.ToString()
                                            join percycles in PMSDataContext.PerformanceCycleActivity.AsEnumerable()
                                                on app.PerformanceCycle.ToString() equals percycles.PerformanceCycle.ToString()
                                            where app.PerformanceCycle.ToString() == performanceCycle.ToString() && appPhases.AppraisalPhase == "H1"
                                            select new CSVClassToExport
                                            {
                                                EmployeeCode = app.EmployeeCode.ToString(),
                                                H1Score = string.Format("{0:0.##}", appPhases.Score),
                                                H2Score = (from app1 in PMSDataContext.Appraisals.AsEnumerable()
                                                           join appPhases1 in PMSDataContext.AppraisalPhases.AsEnumerable()
                                                               on app1.Id.ToString() equals appPhases1.Appraisal.ToString()
                                                           join percycles1 in PMSDataContext.PerformanceCycleActivity.AsEnumerable()
                                                               on app1.PerformanceCycle.ToString() equals percycles1.PerformanceCycle.ToString()
                                                           where app1.PerformanceCycle.ToString() == performanceCycle.ToString() && appPhases1.AppraisalPhase == "H2"
                                                           select string.Format("{0:0.##}", appPhases1.Score)).FirstOrDefault(),
                                                FinalScore = string.Format("{0:0.##}", app.FinalScore),
                                                FinalRating = app.FinalRating.ToString(),
                                                PerformanceCycleStartDate = Convert.ToDateTime(percycles.H1GoalSettingStartDate).ToString("yyyyMMdd"),
                                                PerformanceCycleEndDate = Convert.ToDateTime(percycles.CycleCloseDate).ToString("yyyyMMdd")
                                            }).ToList<CSVClassToExport>();

                    // TODO add items to list :)
                    var csv = GetCsv(appraisalRecords);
                    var itemToUpdate = (from r in PMSDataContext.PMSDataFileLocations where r.Title.ToString().Trim().ToUpper() == "PMS DATA TO SAP" select r).FirstOrDefault();
                    System.IO.File.WriteAllText(itemToUpdate.FileLocation + "\\" + "PerformanceCycle" + performanceCycle.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt", csv);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in Exporting Appraisals as CSV to SAP Data files Location");
            }
        }

        /// <summary>
        /// Generate a CSV as a string from a list
        /// of objects that have the CsvColumnNameAttribute
        /// applied
        /// </summary>
        public string GetCsv<T>(List<T> csvDataObjects)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            var sb = new StringBuilder();
            sb.AppendLine(GetCsvHeaderSorted(propertyInfos));
            csvDataObjects.ForEach(d => sb.AppendLine(GetCsvDataRowSorted(d, propertyInfos)));
            return sb.ToString();
        }

        private string GetCsvDataRowSorted<T>(T csvDataObject, PropertyInfo[] propertyInfos)
        {
            IEnumerable<string> valuesSorted = propertyInfos
                .Select(x => new
                {
                    Value = x.GetValue(csvDataObject, null),
                    Attribute = (CsvColumn)Attribute.GetCustomAttribute(x, typeof(CsvColumn), false)
                })
                .Where(x => x.Attribute != null && x.Attribute.Export)
                .OrderBy(x => x.Attribute.Order)
                .Select(x => GetPropertyValueAsString(x.Value));
            return String.Join("|", valuesSorted.ToArray());
        }

        private string GetCsvHeaderSorted(PropertyInfo[] propertyInfos)
        {
            IEnumerable<string> headersSorted = propertyInfos
                .Select(x => (CsvColumn)Attribute.GetCustomAttribute(x, typeof(CsvColumn), false))
                .Where(x => x != null && x.Export)
                .OrderBy(x => x.Order)
                .Select(x => x.Name);
            return String.Join("|", headersSorted.ToArray());
        }

        private string GetPropertyValueAsString(object propertyValue)
        {
            string propertyValueString;

            if (propertyValue == null)
                propertyValueString = "";
            else if (propertyValue is DateTime)
                propertyValueString = ((DateTime)propertyValue).ToString("dd MMM yyyy");
            else if (propertyValue is int)
                propertyValueString = propertyValue.ToString();
            else if (propertyValue is float)
                propertyValueString = ((float)propertyValue).ToString("#.####"); // format as you need it
            else if (propertyValue is double)
                propertyValueString = ((double)propertyValue).ToString("#.####"); // format as you need it
            else
                propertyValueString = propertyValue.ToString();
            return propertyValueString;
        }

        public static SPListItem GetMasterDetails(string colName, string currentUserName)
        {

            LogHandler.LogVerbose("GetMasterDetails(string colName, string currentUserName)");
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
                        q.ViewFields = @"<FieldRef Name='LoginName'/><FieldRef Name='EmployeeCode'/>";
                        q.ViewFieldsOnly = true;
                        q.RowLimit = 1;
                        q.Query = "<Where><And><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + currentUserName + "</Value></Eq><Eq><FieldRef Name='EmployeeStatus' /><Value Type='Text'>Active</Value></Eq></And></Where>";//<Query><Where><And><Eq><FieldRef Name="WindowsLogin" /><Value Type="Text">test1</Value></Eq><Eq><FieldRef Name="EmployeeStatus" /><Value Type="Text">Active</Value></Eq></And></Where></Query>
                        masterItemsColl = employeMaster.GetItems(q);
                        if (masterItemsColl.Count > 0)
                            dt = masterItemsColl.GetDataTable();
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
    }
}
