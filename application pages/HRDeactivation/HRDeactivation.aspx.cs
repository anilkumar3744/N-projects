using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlTypes;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using System.Collections;
using Microsoft.SharePoint.Workflow;
using System.Web.UI;

namespace VFS.PMS.ApplicationPages.Layouts.HRDeactivation
{
    public partial class HRDeactivation : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PostBackTrigger trigger = new PostBackTrigger();
            trigger.ControlID = "btnDeactivation";
            upHRDeactivation.Triggers.Add(trigger);
            if (!IsPostBack)
            {
                string id = string.Empty;
                string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                SPListItem spMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);
                id = spMaster["EmployeeCode"].ToString();
                hfEmpId.Value = id;
                if (spMaster != null)
                {
                    SPListItem spItem = CommonMaster.GetMasterDetails("HRBusinessPartnerEmployeeCode", id);
                    if (spItem != null)
                    {
                        //List<SearchAppraisalClass> hrData = CommonMaster.GetData("HRBusinessPartnerEmployeeCode", id);
                        //gvDeactivation.DataSource = hrData;
                        //gvDeactivation.DataBind();
                    }
                }
            }
        }

        //protected void btnDeactivation_Click(object sender, EventArgs e)
        //{
        //    if (txtComments.Text != string.Empty)
        //    {
        //        if (gvDeactivation.Rows.Count == 1)
        //        {
        //            SPUser user = SPContext.Current.Web.CurrentUser;
        //            SPSecurity.RunWithElevatedPrivileges(delegate()
        //            {

        //                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
        //                {
        //                    using (SPWeb objWeb = osite.OpenWeb())
        //                    {
        //                        foreach (GridViewRow row in gvDeactivation.Rows)
        //                        {
        //                            Label appraisalId = row.FindControl("lblAppraisalId") as Label;
        //                            Label lblActor = row.FindControl("lblHRName") as Label;
        //                            SPList appraisals = objWeb.Lists["Appraisals"];
        //                            SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(appraisalId.Text));
        //                            const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";

        //                            SPWorkflowManager WFmanager = SPContext.Current.Site.WorkflowManager;
        //                            SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
        //                            SPWorkflowCollection workflowColl = WFmanager.GetItemActiveWorkflows(appraisalItem);

        //                            if (workflowColl.Count > 0)
        //                            {
        //                                objWeb.AllowUnsafeUpdates = true;
        //                                // SPWorkflow workflow = workflowColl[0];
        //                                // SPWorkflowManager.CancelWorkflow(workflow);
        //                                DeactivateWorkflow(appraisalId.Text);

        //                                bool h1SelfStarted = CheckH1Startred(objWeb, Convert.ToString(appraisalItem["appPerformanceCycle"]));
        //                                if (!h1SelfStarted)
        //                                {
        //                                    appraisalItem["appDeactivated"] = "Yes";
        //                                    appraisalItem["Title"] = "Yes";
        //                                    appraisalItem.Update();
        //                                }
        //                                else
        //                                {
        //                                    if (!Convert.ToString(appraisalItem["appAppraisalStatus"]).Contains("H2"))
        //                                    {
        //                                        SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];
        //                                        appraisalItem["appDeactivated"] = string.Empty;
        //                                        appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(12)["Appraisal_x0020_Workflow_x0020_S"]);
        //                                        appraisalItem["Title"] = "Yes";
        //                                        appraisalItem.Update();


        //                                        SPList appraisalTasks = objWeb.Lists["VFSAppraisalTasks"];
        //                                        SPQuery q = new SPQuery();
        //                                        q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Eq><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt64(appraisalId.Text) + "</Value></Eq></And></And></And></Where>";
        //                                        SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);
        //                                        if (appraisalCollection.Count > 0)
        //                                        {
        //                                            SPListItem item = appraisalCollection[0];
        //                                            Hashtable ht = new Hashtable();
        //                                            objWeb.AllowUnsafeUpdates = true;
        //                                            ht["tskStatus"] = "Approved";
        //                                            ht["Status"] = "Approved";
        //                                            ht["tskActionTakenBy"] = user;
        //                                            SPWorkflowTask.AlterTask(item, ht, true);
        //                                            objWeb.AllowUnsafeUpdates = false;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        appraisalItem["appDeactivated"] = "Yes";
        //                                        appraisalItem["Title"] = "Yes";
        //                                        appraisalItem.Update();
        //                                    }
        //                                }

        //                                objWeb.AllowUnsafeUpdates = false;
        //                                CommonMaster.BindHistory("HRDeactivation", Convert.ToInt32(appraisalId.Text), txtComments.Text, user, DateTime.Now.ToString("dd-MMM-yyyy"), lblActor.Text.Trim(), "HR");
        //                            }
        //                        }
        //                    }
        //                }
        //            });
        //            string url = SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx";
        //            Context.Response.Write("<script type='text/javascript'>alert('Deactivated successfully.');window.open('" + url + "','_self'); </script>");
        //        }
        //        else
        //        {
        //            string url = SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx";
        //            Context.Response.Write("<script type='text/javascript'>alert('Select single employee.');window.open('" + url + "','_self'); </script>");
        //            //Context.Response.Write("<script language=\"javascript\">alert('PIP saved Successfully')</script>");
        //            //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Search single employee.');</script>");
        //        }
        //    }
        //    else
        //    {
        //        //string url = SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx";
        //        //Context.Response.Write("<script type='text/javascript'>alert('Please specify reason for deactivation.')</script>");
        //        //Context.Response.Write("<script language=\"javascript\">alert('PIP saved Successfully')</script>");;window.open('" + url + "','_self'); 
        //        //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Please enter comments.');</script>");
        //    }
        //}
        protected void btnDeactivation_Click(object sender, EventArgs e)
        {
            if (txtComments.Text != string.Empty)
            {
                if (gvDeactivation.Rows.Count == 1)
                {
                    SPUser user = SPContext.Current.Web.CurrentUser;
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb objWeb = osite.OpenWeb())
                            {
                                foreach (GridViewRow row in gvDeactivation.Rows)
                                {
                                    Label appraisalId = row.FindControl("lblAppraisalId") as Label;
                                    Label lblActor = row.FindControl("lblHRName") as Label;
                                    SPList appraisals = objWeb.Lists["Appraisals"];
                                    SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(appraisalId.Text));
                                    const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";

                                    SPWorkflowManager WFmanager = SPContext.Current.Site.WorkflowManager;
                                    SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
                                    SPWorkflowCollection workflowColl = WFmanager.GetItemActiveWorkflows(appraisalItem);
                                    int phaseID = 0;
                                    if (workflowColl.Count > 0)
                                    {
                                        objWeb.AllowUnsafeUpdates = true;
                                        // SPWorkflow workflow = workflowColl[0];
                                        // SPWorkflowManager.CancelWorkflow(workflow);

                                        DeactivateWorkflow(appraisalId.Text);

                                        bool h1SelfStarted = CheckH1Startred(objWeb, Convert.ToString(appraisalItem["appPerformanceCycle"]));
                                        if (!h1SelfStarted)
                                        {
                                            appraisalItem["appDeactivated"] = "Yes";
                                            appraisalItem.Update();
                                        }
                                        else
                                        {
                                            if (!Convert.ToString(appraisalItem["appAppraisalStatus"]).Contains("H2"))
                                            {
                                                SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];
                                                appraisalItem["appDeactivated"] = string.Empty;
                                                appraisalItem["appAppraisalStatus"] = Convert.ToString(lstAppraisalStatus.GetItemById(12)["Appraisal_x0020_Workflow_x0020_S"]);
                                                appraisalItem.Update();

                                                #region H1ScoreEmpty

                                                SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                                                SPQuery phasesQueryH1 = new SPQuery();
                                                phasesQueryH1.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt64(appraisalId.Text) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                                                SPListItemCollection phasesCollectionH1 = lstAppraisalPhases.GetItems(phasesQueryH1);

                                                if (phasesCollectionH1.Count > 0)
                                                {
                                                    SPListItem phaseItemH1 = phasesCollectionH1[0];
                                                    phaseItemH1["aphScore"] = "";
                                                    phaseItemH1.Update();
                                                    phaseID = phaseItemH1.ID;
                                                }
                                                #endregion


                                                SPList appraisalTasks = objWeb.Lists["VFSAppraisalTasks"];
                                                SPQuery q = new SPQuery();
                                                q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Eq><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt64(appraisalId.Text) + "</Value></Eq></And></And></And></Where>";
                                                SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);
                                                if (appraisalCollection.Count > 0)
                                                {
                                                    SPListItem item = appraisalCollection[0];
                                                    Hashtable ht = new Hashtable();
                                                    objWeb.AllowUnsafeUpdates = true;


                                                    ht["tskStatus"] = "Approved";
                                                    ht["Status"] = "Approved";
                                                    ht["tskActionTakenBy"] = user;
                                                    SPWorkflowTask.AlterTask(item, ht, true);
                                                    objWeb.AllowUnsafeUpdates = false;
                                                }
                                            }
                                            else
                                            {
                                                SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                                                SPQuery phasesQueryH2 = new SPQuery();
                                                phasesQueryH2.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt64(appraisalId.Text) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                                                SPListItemCollection phasesCollectionH2 = lstAppraisalPhases.GetItems(phasesQueryH2);

                                                if (phasesCollectionH2.Count > 0)
                                                {
                                                    SPListItem phaseItemH2 = phasesCollectionH2[0];
                                                    phaseID = phaseItemH2.ID;
                                                }

                                                appraisalItem["appDeactivated"] = "Yes";
                                                appraisalItem.Update();
                                            }
                                        }

                                        objWeb.AllowUnsafeUpdates = false;
                                        CommonMaster.BindHistory("HRDeactivation", phaseID, txtComments.Text, user, DateTime.Now.ToString("dd-MMM-yyyy"), lblActor.Text.Trim(), "HR");
                                    }
                                }
                            }
                        }
                    });
                    string url = SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx";
                    Context.Response.Write("<script type='text/javascript'>alert('Deactivated successfully.');window.open('" + url + "','_self'); </script>");
                }
                else
                {
                    string url = SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx";
                    Context.Response.Write("<script type='text/javascript'>alert('Select single employee.');window.open('" + url + "','_self'); </script>");
                    //Context.Response.Write("<script language=\"javascript\">alert('PIP saved Successfully')</script>");
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Search single employee.');</script>");
                }
            }
            else
            {
                //string url = SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx";
                //Context.Response.Write("<script type='text/javascript'>alert('Please specify reason for deactivation.')</script>");
                //Context.Response.Write("<script language=\"javascript\">alert('PIP saved Successfully')</script>");;window.open('" + url + "','_self'); 
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Please enter comments.');</script>");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string currentCycle = CommonMaster.GetCurrentPerformanceCycle();
            List<SearchAppraisalClass> hrData = CommonMaster.GetData("HRBusinessPartnerEmployeeCode", hfEmpId.Value);
            hrData = hrData.Where(p => p.appPerformanceCycle.ToString().ToLower() == currentCycle).ToList();
            hrData = hrData.Where(p => p.appEmployeeCode.ToString().ToLower() == (txtEmployeeCode.Text.ToLower().Trim())).ToList();
            foreach (SearchAppraisalClass li in hrData)
            {
                if (li.Deactivated != null)
                    hrData = hrData.Where(p => p.Deactivated.ToString().ToLower() != "yes").ToList();
                hrData = hrData.Where(p => !p.appAppraisalStatus.ToString().ToLower().Contains(("H2 – Completed").ToLower())).ToList();

            }
            gvDeactivation.DataSource = hrData;
            gvDeactivation.DataBind();
            if (hrData.Count > 0)
                dvDeactivate.Visible = true;
            else
                dvDeactivate.Visible = false;

        }

        protected void lnkAppraisalStatus_Click(object sender, EventArgs e)
        {
        }

        private void DeactivateWorkflow(SPListItem properties, SPWeb objweb)
        {
            try
            {
                int appraisalID = 0;
                SPList appraisalTasks = objweb.Lists["VFSAppraisalTasks"];
                SPQuery q = new SPQuery();
                q.Query = "<Where><And><Eq><FieldRef Name='tskAppraiseeCode' /><Value Type='Text'>" + Convert.ToString(properties["EmployeeCode"]) + "</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq></And></Where>";
                SPListItemCollection appraisalCollection = appraisalTasks.GetItems(q);
                if (appraisalCollection.Count > 0)
                {
                    SPListItem item = appraisalCollection[0];
                    appraisalID = Convert.ToInt32(item["tskAppraisalId"]);
                    Hashtable ht = new Hashtable();
                    ht["tskStatus"] = "Deactivation";
                    ht["Status"] = "Deactivation";
                    ht["tskActionTakenBy"] = SPContext.Current.Web.CurrentUser;
                    SPWorkflowTask.AlterTask(item, ht, true);
                }
                if (appraisalID != 0)
                {
                    SPList appraisals = objweb.Lists["Appraisals"];
                    SPListItem appItem = appraisals.GetItemById(appraisalID);
                    objweb.AllowUnsafeUpdates = true;
                    appItem["appDeactivated"] = "Yes";
                    appItem.Update();
                    objweb.AllowUnsafeUpdates = false;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.DeactivateWorkflow");
            }
        }

        private bool CheckH1Startred(SPWeb objWeb, string currentCyccle)
        {
            bool flag = false;
            SPList listTMPActions = objWeb.Lists["Performance Cycle Activity"];
            SPQuery phasesQuery = new SPQuery();
            phasesQuery.Query = "<Where><Eq><FieldRef Name='PerformanceCycle' /><Value Type='Number'>" + Convert.ToInt32(currentCyccle) + "</Value></Eq></Where>";
            SPListItemCollection phasesCollection = listTMPActions.GetItems(phasesQuery);
            if (phasesCollection.Count > 0)
            {
                SPListItem phaseItem = phasesCollection[0];
                if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["H1SelfEvaluationStartDate"])))
                {
                    flag = true;
                }
            }
            return flag;

        }

        private void DeactivateWorkflow(string appraisalId1)
        {
            try
            {

                SPUser user = SPContext.Current.Web.CurrentUser;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite osite1 = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb objWeb1 = osite1.OpenWeb())
                        {

                            //Label appraisalId = row.FindControl("lblAppraisalId") as Label;
                            //SPList appraisals = objWeb1.Lists["Appraisals"];
                            //SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(appraisalId.Text));
                            //objWeb1.AllowUnsafeUpdates = false;
                            //Hashtable ht = new Hashtable();
                            //ht["tskStatus"] = "Deactivation";
                            //ht["Status"] = "Deactivation";
                            //ht["tskActionTakenBy"] = user;
                            //SPWorkflowTask.AlterTask(appraisalItem, ht, true);
                            //objWeb1.AllowUnsafeUpdates = true;

                            SPList appraisalsTasks = objWeb1.Lists["VFSAppraisalTasks"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(appraisalId1) + "</Value></Eq></And></Where>";
                            SPListItemCollection taskCollections = appraisalsTasks.GetItems(q);
                            if (taskCollections.Count > 0)
                            {
                                objWeb1.AllowUnsafeUpdates = true;
                                SPListItem taskItem = taskCollections[0];
                                Hashtable ht = new Hashtable();
                                ht["tskStatus"] = "Deactivation";
                                ht["Status"] = "Deactivation";
                                ht["tskActionTakenBy"] = user;
                                SPWorkflowTask.AlterTask(taskItem, ht, true);
                                objWeb1.AllowUnsafeUpdates = false;
                            }



                        }
                    }
                });

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "EmployeeDataReciever.DeactivateWorkflow");
            }
        }
    }
}
