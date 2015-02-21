using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_TMTActions
{
    public partial class TMTActions : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //btnGoalSetting.Visible = false;
            //lblGoalSettingH1S.Visible = true;
            //lblGoalSettingH1S.Text = "Started";
            //lblH1SelfEvaluationH1S.Visible = false;
            //btnH1SelfEvaluation.Visible = true;
            if (!IsPostBack)
            {
                if (this.Request.Params["ID"] != null)
                {
                    if (Request.Params["Source"] != null)
                    {
                        if (Request.Params["RootFolder"] != null)
                        {
                            // View
                            ViewDetails();
                        }
                        else
                        {
                            // Edit
                            UpdateDetails();
                        }
                    }
                    else
                    {
                        // edit

                        UpdateDetails();
                    }
                }
                else
                {
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb objWeb = osite.OpenWeb())
                        {
                            SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                            SPListItemCollection itemCollection = lstCategory.GetItems();
                            lblHistory.Visible = false;
                            if (itemCollection.Count > 0)
                            {
                                SPListItem item = itemCollection[itemCollection.Count - 1];
                                lblPerformanceCycleValue.Text = Convert.ToString(Convert.ToInt32(item["tmtPerformanceCycle"]) + 1);
                            }
                            else
                            {
                                lblPerformanceCycleValue.Text = DateTime.Now.Year.ToString();
                            }
                        }
                    }
                }
            }
        }

        protected void UpdateDetails()
        {
            //btnGoalSetting.Visible = false;
            //lblGoalSettingH1S.Visible = true;
            //lblGoalSettingH1S.Text = "Started";
            string H1initial = string.Empty;
            string H1SelfEval = string.Empty;
            string H2SelfEval = string.Empty;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    H1initial = Convert.ToString(lstItem["tmtIsH1GoalSettingStarted"]);
                    H1SelfEval = Convert.ToString(lstItem["tmtIsH1SelfEvaluationStarted"]);
                    H2SelfEval = Convert.ToString(lstItem["tmtIsH2SelfEvaluationStarted"]);
                    lblHistoryH1Start.Text = Convert.ToString(lstItem["tmtH1History"]);
                    lblHistoryH1SelStart.Text = Convert.ToString(lstItem["tmtH1SelfHistory"]);
                    lblHistoryH2SelStart.Text = Convert.ToString(lstItem["tmtH2SelHistory"]);
                    lblPerformanceCycleValue.Text = Convert.ToString(lstItem["tmtPerformanceCycle"]);
                }
                if (string.IsNullOrEmpty(H1initial))
                {
                    btnH1SelfEvaluation.Visible = false;
                    btnH2SelfEvaluation.Visible = false;
                }
                else if (!string.IsNullOrEmpty(H1initial) && string.IsNullOrEmpty(H1SelfEval))
                {
                    btnH1SelfEvaluation.Visible = true;
                    lblGoalSettingH1S.Text = "Started";
                    lblGoalSettingH1S.Visible = true;
                    btnGoalSetting.Visible = false;
                    lblH1SelfEvaluationH1S.Visible = false;
                }
                else
                { // All are labels
                    lblGoalSettingH1S.Visible = true;
                    lblH1SelfEvaluationH1S.Visible = true;
                    lblH1SelfEvaluationH2S.Visible = false;
                    lblGoalSettingH1S.Text = "Started";
                    lblH1SelfEvaluationH1S.Text = "Started";
                    //lblH1SelfEvaluationH2S.Text = "Started";
                    btnH2SelfEvaluation.Visible = true;
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

        protected void ViewDetails()
        {
            lblGoalSettingH1S.Text = "Started";
            btnGoalSetting.Visible = false;
            lblGoalSettingH1S.Visible = true;

            btnH1SelfEvaluation.Visible = false;
            lblH1SelfEvaluationH1S.Visible = true;
            lblH1SelfEvaluationH1S.Text = "Stared";
            string H1SelfEval = string.Empty;
            string H2SelfEval = string.Empty;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    H1SelfEval = Convert.ToString(lstItem["tmtIsH1SelfEvaluationStarted"]);
                    H2SelfEval = Convert.ToString(lstItem["tmtIsH2SelfEvaluationStarted"]);

                    lblHistoryH1Start.Text = Convert.ToString(lstItem["tmtH1History"]);
                    lblHistoryH1SelStart.Text = Convert.ToString(lstItem["tmtH1SelfHistory"]);
                    lblHistoryH2SelStart.Text = Convert.ToString(lstItem["tmtH2SelHistory"]);
                }

                if (!string.IsNullOrEmpty(H2SelfEval))
                { // All are labels
                    lblGoalSettingH1S.Visible = true;
                    lblH1SelfEvaluationH1S.Visible = true;
                    lblH1SelfEvaluationH2S.Visible = true;
                    lblGoalSettingH1S.Text = "Started";
                    lblH1SelfEvaluationH1S.Text = "Started";
                    lblH1SelfEvaluationH2S.Text = "Started";
                }
                else if (string.IsNullOrEmpty(H1SelfEval))
                {
                    btnH1SelfEvaluation.Visible = false;
                    lblH1SelfEvaluationH1S.Visible = true;
                    lblH1SelfEvaluationH1S.Text = "NotStarted";
                }
                else
                {

                    btnH2SelfEvaluation.Visible = false;
                    btnH1SelfEvaluation.Visible = false;
                    lblH1SelfEvaluationH2S.Visible = true;
                    //lblH1SelfEvaluationH2S.Text = "Started";
                }
            }
        }

        protected void btn_h1selfevaluation(object sender, EventArgs e)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    lstItem["tmtIsH1SelfEvaluationStarted"] = "Started";
                    lstItem["tmtH1SelfHistory"] = "H1 Self-eavaluation was started by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");

                    //lstItem["tmtPerformanceCycle"] = Convert.ToString(DateTime.Now.Year);
                    Web.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    Web.AllowUnsafeUpdates = false;
                }

                WorkflowTriggering();
                popup();
            }
        }

        private void WorkflowTriggering()
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

                        SPWorkflowTask.AlterTask(item, ht, true);

                        SPList appraisals = objWeb.Lists["Appraisals"];
                        SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
                        appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(4)["Appraisal_x0020_Workflow_x0020_S"]);
                        appraisalItem["appH1AppraisalEvaluationStartDat"] = DateTime.Now.Date;
                        objWeb.AllowUnsafeUpdates = true;
                        appraisalItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }
                }

            }
        }

        protected void btnH2SelfEvaluation_Click(object sender, EventArgs e)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    lstItem["tmtIsH2SelfEvaluationStarted"] = "Started";
                    lstItem["tmtH2SelHistory"] = "H2 Self-eavaluation was started by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");

                    Web.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    Web.AllowUnsafeUpdates = false;

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

                    popup();


                }
            }
        }

        private void WorkflowTriggeringForH2SelfEvaluation()
        {
            try
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

                            SPWorkflowTask.AlterTask(item, ht, true);

                            SPList appraisals = objWeb.Lists["Appraisals"];
                            SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
                            appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(15)["Appraisal_x0020_Workflow_x0020_S"]);
                            appraisalItem["appH2AppraisalEvaluationStartDat"] = DateTime.Now.Date;
                            objWeb.AllowUnsafeUpdates = true;
                            appraisalItem.Update();
                            objWeb.AllowUnsafeUpdates = false;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        protected void BtnGoalSetting_Click(object sender, EventArgs e)
        {
            // SPListItem masteritem = GetEmployeeMaster();
            SPListItemCollection masterCollection = GetAllEmployeeMasters();

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists["Appraisals"];
                    SPList lstAppraisalStatus = currentWeb.Lists["Appraisal Status"];

                    //SPList lstMasterList = currentWeb.Lists["Employee Masters"];
                    //SPListItem masterItem;
                    //int[] arr = { 2651, 2657, 2658, 2659, 2660 };

                    //for (int i = 0; i < arr.Length - 1; i++)
                    //{
                    //    masterItem = lstMasterList.GetItemById(arr[i]);
                    //    SPListItem appraisalItem = appraisals.AddItem();
                    //    appraisalItem["appPerformanceCycle"] = lblPerformanceCycleValue.Text;
                    //    appraisalItem["appEmployeeCode"] = masterItem["EmployeeCode"].ToString();
                    //    appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(1)["Appraisal_x0020_Workflow_x0020_S"]);
                    //    appraisalItem["appPerformanceCycle"] = lblPerformanceCycleValue.Text;

                    //    //appraisalItem["appAppraiserCode"] = Convert.ToString(masterItem["ImmediateSupervisor_x003a_Employ"]);
                    //    //appraisalItem["appReviewerCode"] = Convert.ToString(masterItem["DepartmentHead_x003a_EmployeeCod"]);
                    //    //appraisalItem["appHRBusinessPartnerCode"] = Convert.ToString(masterItem["HREmployeeCode_x003a_EmployeeCod"]);
                    //    Web.AllowUnsafeUpdates = true;
                    //    appraisalItem.Update();
                    //}

                    foreach (SPListItem item in masterCollection)
                    {
                        SPListItem appraisalItem = appraisals.AddItem();
                        appraisalItem["appPerformanceCycle"] = lblPerformanceCycleValue.Text;
                        appraisalItem["appEmployeeCode"] = item["EmployeeCode"].ToString();
                        appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(1)["Appraisal_x0020_Workflow_x0020_S"]);
                        appraisalItem["appH1GoalSettingStartDate"] = DateTime.Now.Date;
                        Web.AllowUnsafeUpdates = true;
                        appraisalItem.Update();
                    }

                    SPListItemCollection appraisalCollection = appraisals.GetItems();
                    foreach (SPListItem item in appraisalCollection)
                    {

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

                    SPList tmtActions = currentWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem listItem;
                    if (Request.Params["ID"] != null)
                        listItem = tmtActions.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    else
                        listItem = tmtActions.AddItem();
                    listItem["tmtIsH1GoalSettingStarted"] = "Started";
                    listItem["tmtPerformanceCycle"] = lblPerformanceCycleValue.Text;
                    listItem["tmtH1History"] = "H1 Goal setting was started by " + SPContext.Current.Web.CurrentUser.Name + " @ " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm");
                    Web.AllowUnsafeUpdates = true;
                    listItem.Update();
                    Web.AllowUnsafeUpdates = false;

                    popup();

                }

            }
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
                        //q.Query = "<Where><And><Eq><FieldRef Name='Status' /><Value Type='Boolean'>Yes</Value></Eq><Eq><FieldRef Name='EmployeeGroup' /><Value Type='Lookup'>A</Value></Eq></And></Where>";
                        q.Query = "<Where><And><Eq><FieldRef Name='EmployeeGroup' /><Value Type='Lookup'>A</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Boolean'>1</Value></Eq></And></Where>";
                        masterItemsColl = employeMaster.GetItems(q);
                        //masterItemsColl = employeMaster.GetItems();
                        //DataTable dt = masterItemsColl.GetDataTable();
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

    }
}
