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
using System.Web.UI;
using System.Web.Caching;
using System.Text;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_DashBoards
{
    public partial class Dashboard : LayoutsPageBase
    {
        static string userType;
        protected void Page_Load(object sender, EventArgs e)
        {
            LogHandler.LogVerbose("Start:" + DateTime.Now.ToString());
            try
            {
                bool isReviewBoard = false, isRegionalHR = false, isTMT = false, isHR = false, isReviewer = false, isAppraiser = false;
                //splong
                if (Locked())
                {
                    lblLocked.ForeColor = System.Drawing.Color.Red;
                    lblLocked.Text = "The current performance cycle has been locked, Please contact TMT.";
                }
                if (!IsPostBack)
                {
                    string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
                    string id = string.Empty;
                    string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                    LogHandler.LogVerbose("Page load:GetCurrentPerformanceCycle" + DateTime.Now.ToString());
                    SPListItem dtMaster = GetMasterDetails("WindowsLogin", currentUser);
                    LogHandler.LogVerbose("Page load:GetMasterDetails" + DateTime.Now.ToString());
                    if (dtMaster != null)
                    {
                        if (!string.IsNullOrEmpty(performanceCycle))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dtMaster["EmployeeCode"])))
                                id = dtMaster["EmployeeCode"].ToString();
                            if (Session["AppraiseeCode"] != null && id != Session["AppraiseeCode"].ToString())
                            {
                                Session.Abandon();
                                Session.Clear();
                            }

                            GetEmployeeRoles(id, SPContext.Current.Web);
                            hfEmpId.Value = id;
                            userType = "Appraisee";
                            LogHandler.LogVerbose("Page load:Review Board" + DateTime.Now.ToString());

                            isRegionalHR = (Page.Session["isRegionalHR"] != null && Page.Session["isRegionalHR"].ToString() == "True");
                            isTMT = (Page.Session["isTMT"] != null && Page.Session["isTMT"].ToString() == "True");
                            isReviewer = (Page.Session["isReviewer"] != null && Page.Session["isReviewer"].ToString() == "True");
                            isAppraiser = (Page.Session["isAppraiser"] != null && Page.Session["isAppraiser"].ToString() == "True");
                            isHR = (Page.Session["isHR"] != null && Page.Session["isHR"].ToString() == "True");
                            isReviewBoard = (Page.Session["isReviewBoard"] != null && Page.Session["isReviewBoard"].ToString() == "True");
                            List<SearchAppraisalClass> appraisalsData = GetData(performanceCycle, id, SPContext.Current.Web);

                            if (isReviewBoard)
                            {
                                userType = "ReviewBoard";
                                tpAppraiseePanel.Visible = true;
                                tpReviewerPanel.Visible = true;
                                tpRegionalHRPanel.Visible = true;
                                tpHrPanel.Visible = true;
                                tpReviewBoardPanel.Visible = true;
                                tpAppraiserPanel.Visible = true;
                                tpTMTPanel.Visible = true;
                                tpTMTPanel.Enabled = isTMT;//isUserInGroup("TMT", SPContext.Current.Web.CurrentUser);
                                tpHrPanel.Enabled = isHR;//isUSerHR(id);

                                List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);
                                gvPIPAppraisee.DataSource = empPIPData;
                                gvPIPAppraisee.DataBind();
                                gvAppraisee.DataSource = appraisalsData;
                                gvAppraisee.DataBind();
                                //DropDownBinds();
                            }
                            else
                            {
                                LogHandler.LogVerbose("Page load:RegionHREmployeeCode" + DateTime.Now.ToString());

                                if (isRegionalHR)
                                {
                                    userType = "RegionalHR";
                                    tpAppraiseePanel.Visible = true;
                                    tpReviewerPanel.Visible = true;
                                    tpRegionalHRPanel.Visible = true;
                                    tpHrPanel.Visible = true;
                                    tpReviewBoardPanel.Visible = false;
                                    tpAppraiserPanel.Visible = true;
                                    tpTMTPanel.Visible = true;
                                    tpTMTPanel.Enabled = isTMT;//isUserInGroup("TMT", SPContext.Current.Web.CurrentUser);

                                    List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);

                                    gvAppraisee.DataSource = appraisalsData;
                                    gvAppraisee.DataBind();
                                    gvPIPAppraisee.DataSource = empPIPData;
                                    gvPIPAppraisee.DataBind();
                                    //DropDownBinds();
                                }
                                else
                                {
                                    LogHandler.LogVerbose("Page load:TMT" + DateTime.Now.ToString());
                                    //isTMT = isUserInGroup("TMT", SPContext.Current.Web.CurrentUser);
                                    if (isTMT)
                                    {
                                        userType = "TMT";
                                        tpAppraiseePanel.Visible = true;
                                        tpReviewerPanel.Visible = true;
                                        tpRegionalHRPanel.Visible = false;
                                        tpHrPanel.Visible = true;
                                        tpReviewBoardPanel.Visible = false;
                                        tpAppraiserPanel.Visible = true;
                                        tpTMTPanel.Visible = true;

                                        List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);

                                        gvAppraisee.DataSource = appraisalsData; ////ViewState["gridData"];//// PreparingGridTable("Appraisee", currentUser, id); ////GetAppraisalsList("appEmployeeCode", id);
                                        gvAppraisee.DataBind();
                                        gvPIPAppraisee.DataSource = empPIPData;
                                        gvPIPAppraisee.DataBind();

                                        //DropDownBinds();

                                    }
                                    else
                                    {
                                        LogHandler.LogVerbose("Page load:HRBusinessPartnerEmployeeCode" + DateTime.Now.ToString());
                                        //spItem = GetMasterDetails("HRBusinessPartnerEmployeeCode", id);
                                        if (isHR)
                                        {
                                            userType = "HR";
                                            tpAppraiseePanel.Visible = true;
                                            tpReviewerPanel.Visible = true;
                                            tpRegionalHRPanel.Visible = false;
                                            tpHrPanel.Visible = true;
                                            tpReviewBoardPanel.Visible = false;
                                            tpAppraiserPanel.Visible = true;
                                            tpTMTPanel.Visible = false;

                                            List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);

                                            gvAppraisee.DataSource = appraisalsData;////ViewState["gridData"];
                                            gvAppraisee.DataBind();
                                            gvPIPAppraisee.DataSource = empPIPData;
                                            gvPIPAppraisee.DataBind();
                                            //DropDownBinds();
                                        }
                                        else
                                        {
                                            LogHandler.LogVerbose("Page load:DepartmentHeadEmployeeCode" + DateTime.Now.ToString());
                                            //spItem = GetMasterDetails("DepartmentHeadEmployeeCode", id);
                                            if (isReviewer)
                                            {
                                                userType = "Reviewer";
                                                tpAppraiseePanel.Visible = true;
                                                tpHrPanel.Visible = false;
                                                tpReviewerPanel.Visible = true;
                                                tpRegionalHRPanel.Visible = false;
                                                tpReviewBoardPanel.Visible = false;
                                                tpAppraiserPanel.Visible = true;
                                                tpTMTPanel.Visible = false;
                                                List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);
                                                gvAppraisee.DataSource = appraisalsData;////ViewState["gridReviewerData"];//// PreparingGridTable(userType, currentUser, id); ////GetAppraisalsList("appReviewerCode", id);
                                                gvAppraisee.DataBind();
                                                gvPIPAppraisee.DataSource = empPIPData;
                                                gvPIPAppraisee.DataBind();
                                                //DropDownBinds();
                                            }
                                            else
                                            {
                                                LogHandler.LogVerbose("Page load:ReportingManagerEmployeeCode " + DateTime.Now.ToString());
                                                //spItem = GetMasterDetails("ReportingManagerEmployeeCode", id);
                                                if (isAppraiser)
                                                {
                                                    userType = "Appraiser";
                                                    tpAppraiseePanel.Visible = true;
                                                    tpAppraiserPanel.Visible = true;
                                                    tpHrPanel.Visible = false;
                                                    tpRegionalHRPanel.Visible = false;
                                                    tpReviewBoardPanel.Visible = false;
                                                    tpReviewerPanel.Visible = false;
                                                    tpTMTPanel.Visible = false;

                                                    List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);

                                                    gvAppraisee.DataSource = appraisalsData;////ViewState["gridReviewerData"];//// PreparingGridTable(userType, currentUser, id); ////GetAppraisalsList("appReviewerCode", id);
                                                    gvAppraisee.DataBind();
                                                    gvPIPAppraisee.DataSource = empPIPData;
                                                    gvPIPAppraisee.DataBind();
                                                    //DropDownBinds();
                                                }
                                                else
                                                {
                                                    LogHandler.LogVerbose("Page load:Appraiseee " + DateTime.Now.ToString());
                                                    tpAppraiseePanel.Visible = true;
                                                    tpAppraiserPanel.Visible = false;
                                                    tpHrPanel.Visible = false;
                                                    tpRegionalHRPanel.Visible = false;
                                                    tpReviewBoardPanel.Visible = false;
                                                    tpReviewerPanel.Visible = false;
                                                    tpTMTPanel.Visible = false;
                                                    gvAppraisee.DataSource = appraisalsData;////dtAppraisee;
                                                    gvAppraisee.DataBind();
                                                    List<PIPClass> empPIPData = GetPIP("EmployeeCode", id);
                                                    gvPIPAppraisee.DataSource = empPIPData;
                                                    gvPIPAppraisee.DataBind();

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            tpHrPanel.Enabled = isHR;//isUSerHR(id);
                        }
                        else
                        {
                        }
                        if (tpReviewerPanel.Visible)
                            tpReviewerPanel.Enabled = isReviewer;
                        if (tpRegionalHRPanel.Visible)
                            tpRegionalHRPanel.Enabled = isRegionalHR;
                        if (tpHrPanel.Visible)
                            tpHrPanel.Enabled = isHR;
                        if (tpReviewBoardPanel.Visible)
                            tpReviewBoardPanel.Enabled = isReviewBoard;
                        if (tpAppraiserPanel.Visible)
                            tpAppraiserPanel.Enabled = isAppraiser;
                        if (tpTMTPanel.Visible)
                            tpTMTPanel.Enabled = isTMT;

                    }
                    else
                    {
                        tpAppraiseePanel.Visible = false;
                        tpReviewerPanel.Visible = false;
                        tpRegionalHRPanel.Visible = false;
                        tpHrPanel.Visible = false;
                        tpReviewBoardPanel.Visible = false;
                        tpAppraiserPanel.Visible = false;
                        tpTMTPanel.Visible = false;
                        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Insufficient permissions');</script>");
                        //Response.Redirect(SPContext.Current.Web.Url);//+ "/_layouts/VFS_DashBoards/Dashboard.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
            LogHandler.LogVerbose("End:" + DateTime.Now.ToString());
        }

        #region Button Clicks

        protected void lnkAppraisalStatus_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkAppraisalStatus = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkAppraisalStatus.NamingContainer;
                string taskId = Convert.ToString(lnkAppraisalStatus.CommandArgument);//// "0";
                taskId = tcDashBoard.ActiveTabIndex.ToString();

                int appraisalID = Convert.ToInt32((gvRow.FindControl("lblAppraisalId") as Label).Text);
                string deactivated = (gvRow.FindControl("lblDeactivated") as Label).Text;
                string status = lnkAppraisalStatus.Text.Trim();

                /*
                 * In below code SPContext.Current.Site.Url replaced by SPContext.Current.Web.Url on 24/06/20130012             */
                switch (status)
                {
                    case "H1-Awaiting Appraisee Goal Setting":
                        {
                            if (deactivated.ToLower() != "yes")
                            {
                                //if (tcDashBoard.ActiveTabIndex == 0)
                                //{
                                if (CheckSavedDraft(appraisalID))
                                {
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiseeSavedDraft.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                    //Response.End();
                                }
                                else
                                {
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                    //Response.End();
                                }
                                //}
                                //else
                                //{
                                //    string url = SPContext.Current.Web.Url + "/_layouts/VFS_DashBoards/Dashboard.aspx";
                                //    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('Appraisee did not submit the goals'); </script>");
                                //}
                            }
                            else
                            {
                                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('The appraisal deactivated');</script>");

                                //string url = SPContext.Current.Web.Url + "/_layouts/VFS_DashBoards/Dashboard.aspx";
                                //Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self'); </script>");
                                lblLocked.ForeColor = System.Drawing.Color.Red;
                                lblLocked.Text = "The appraisal deactivated.";

                            }
                            break;
                        }
                    case "H1-Awaiting Appraiser Goal Approval":
                        if (tcDashBoard.ActiveTabIndex == 0)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiserAppraiseeApprovedView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);//AppraiseeGoalsDraftView replaced with AppraiserAppraiseeApprovedView by Tataji
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AwaitingAppraiserApproveNew.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiserAppraiseeApprovedView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H1-Goals Approved":
                        Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiserAppraiseeApprovedView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                        //Response.End();

                        break;
                    case "H1-Awaiting Self-evaluation":
                        if (tcDashBoard.ActiveTabIndex == 0)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/SelfEva/self.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/SelfEva/SelfView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        //Response.Redirect((SPContext.Current.Web.Url+ "/_LAYOUTS/H2initial/SelfView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID));
                        break;
                    case "H1-Awaiting Appraiser Evaluation":////Venkaiah
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserEvaluation.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/AppraiserEvaluationViewMode/AppraiserEvaluationViewMode.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        break;
                    case "H1-Awaiting Reviewer Approval":////Jagadish
                        if (tcDashBoard.ActiveTabIndex == 2)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/ReviewerEvaluation.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false); //"/_Layouts/RH1/RevEval.aspx?TaskID=" + taskId));
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Krishna
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/ReviewerEvaluationView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);//_Layouts/RH1View/RevEvalH1View.aspx?TaskID=" + taskId));
                            //Response.End();
                        }
                        break;
                    case "H1 - Awaiting Appraisee Sign-off":////Jagadish
                        if (tcDashBoard.ActiveTabIndex == 0)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOff.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 3)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/SignOffBehalfof.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H1 - Sign-off by Appraisee":////Jagadish
                        if (tcDashBoard.ActiveTabIndex == 3)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRDecisions.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1) // we need to check IsPIP true available r not
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H1 – Completed":
                        if (tcDashBoard.ActiveTabIndex == 0 && deactivated.ToLower() != "yes")
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/H2initial.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        // Commented by Jeevan Response.Redirect((SPContext.Current.Web.Url+ "/_Layouts/VFSProjectH1/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID));
                        break;
                    case "H1 - Appraisee Appeal"://// Changed by the request of Mr Rao on 12/6/2013
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/H1AppraiserReEvaluation/H1AppraiserReEvaluation.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);//H1AppraiserReview.aspx
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H1 - HR Review":////Venkaiah
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/H1AppraiserReview/H1AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/AppraiserEvaluationViewMode/AppraiserEvaluationViewMode.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        ////Response.Redirect((SPContext.Current.Web.Url+ "/_Layouts/RH1/RevEval.aspx?TaskID=" + taskId + "&AppId=" + appraisalID));////Jagadish
                        break;
                    case "H2 - Awaiting Appraisee Goal Setting":////Jeevan  SaveAsDraft
                        if (deactivated.ToLower() != "yes")
                        {
                            if (CheckSavedDraft(appraisalID))
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/H2initial.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/H2initial.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);//links are interchanged
                                //Response.End();
                            }
                        }
                        else
                        {
                            lblLocked.ForeColor = System.Drawing.Color.Red;
                            lblLocked.Text = "The appraisal deactivated.";
                        }
                        break;
                    case "H2 - Awaiting Appraiser Goal Approval":////Jeevan
                        if (tcDashBoard.ActiveTabIndex == 0)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/AppraiserAppraiseeApprovedView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/H2AwaitingAppraiserApprove.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/AppraiserAppraiseeApprovedView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 - Goals Approved"://JeEvan
                        Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/AppraiserAppraiseeApprovedView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                        //Response.End();

                        break;
                    case "H2 - Awaiting Self-evaluation"://Jeevan
                        if (tcDashBoard.ActiveTabIndex == 0)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/SelfEve.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2initial/SelfView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 - Awaiting Appraiser Evaluation"://Venkaiah
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2AppraiserEvaluation/H2AppraiserEvaluation.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2AppraiserEvaluationViewmode/H2AppraiserEvaluationViewmode.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 - Awaiting Appraiser Evaluation1":
                        break;
                    case "H2 - Awaiting Reviewer Approval":////JAGADISH
                        if (tcDashBoard.ActiveTabIndex == 2)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/ReviewerEvaluation.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/ReviewerEvaluationView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 - Awaiting Appraisee Sign-off":///JAGADISH
                        if (tcDashBoard.ActiveTabIndex == 0)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOff.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 3)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffBehalfof.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1)//Added by Krishna
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 - Sign-off by Appraisee":////jAGADISH
                        if (tcDashBoard.ActiveTabIndex == 3)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRDecisions.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else if (tcDashBoard.ActiveTabIndex == 1) // we need to check IsPIP true available r not
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 – Completed"://JAGADISH
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;
                    case "H2 - Appraisee Appeal"://Venkaiah
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/H2AppraiserReEvaluation/H2AppraiserReEvaluation.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);//Rao told
                            //Response.End();
                        }
                        break;
                    case "H2 - Awaiting Appraisee Sign-off3":
                        break;
                    case "H2 - HR Review"://Venkaiah
                        if (tcDashBoard.ActiveTabIndex == 1)
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/H2AppraiserReview/H2AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        else
                        {
                            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/HRView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                            //Response.End();
                        }
                        break;

                }
                //// }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in lnkAppraisalStatus_Click event");
            }
            //finally
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            //}

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
                if (tcDashBoard.ActiveTabIndex > 0)
                {

                    switch (tcDashBoard.ActiveTabIndex)
                    {
                        case 1:
                            #region List Linq
                            // using (SPWeb currentWeb = SPContext.Current.Site.OpenWeb())
                            //{
                            //    VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(currentWeb.Url);
                            //    cs = (from empMaster in dc.EmployeeMasters.ToList()
                            //          where SqlMethods.Like(empMaster.EmployeeCode.ToString(),"E00%")
                            //          select new SearchAppraisalClass
                            //          {

                            //              ApprName = empMaster.EmployeeName,
                            //              appEmployeeCode = empMaster.EmployeeCode
                            //           }).ToList<SearchAppraisalClass>();
                            //} 
                            #endregion

                            #region SPListQuery

                            #endregion

                            List<SearchAppraisalClass> appraiserData = CommonMaster.GetData("ReportingManagerEmployeeCode", hfEmpId.Value);
                            if (ddlApprasierAppraisalCycle.SelectedIndex != -1)
                                appraiserData = appraiserData.Where(p => p.appPerformanceCycle.ToString() == ddlApprasierAppraisalCycle.SelectedItem.Text).ToList();

                            if (txtAppraiserEmployeeName.Text != null && txtAppraiserEmployeeName.Text != string.Empty)
                                //appraiserData = appraiserData.Where(p => p.EmpName.ToString().ToLower() == (txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();
                                appraiserData = appraiserData.Where(p => (p.EmpName == null ? "" : p.EmpName.ToString().ToLower()) == (txtAppraiserEmployeeName.Text.ToLower().Trim())).ToList();

                            if (txtAppraiserEmployeeCode.Text != null && txtAppraiserEmployeeCode.Text != string.Empty)
                                //appraiserData = appraiserData.Where(p => p.appEmployeeCode.ToString() == (txtAppraiserEmployeeCode.Text.Trim())).ToList();
                                appraiserData = appraiserData.Where(p => (p.appEmployeeCode == null ? "" : p.appEmployeeCode.ToString().ToLower()) == (txtAppraiserEmployeeCode.Text.ToLower().Trim())).ToList();

                            if (ddlAppraiserArea.SelectedIndex > 0)
                                appraiserData = appraiserData.Where(p => p.PersonnelAreaCode.ToString() == ddlAppraiserArea.SelectedItem.Value).ToList();

                            if (ddlAppraiserSubArea.SelectedIndex > 0)
                                appraiserData = appraiserData.Where(p => p.PersonnelSubAreaCode.ToString() == ddlAppraiserSubArea.SelectedItem.Value).ToList();

                            if (ddlAppraiserEmployeeGroup.SelectedIndex > 0)
                                appraiserData = appraiserData.Where(p => p.EmployeeGroupCode.ToString() == ddlAppraiserEmployeeGroup.SelectedItem.Value).ToList();

                            if (ddlAppraiserEmployeesSubGroup.SelectedIndex > 0)
                                appraiserData = appraiserData.Where(p => p.EmployeeSubGroupCode.ToString() == ddlAppraiserEmployeesSubGroup.SelectedItem.Value).ToList();

                            if (ddlAppraiserAppraisalStatus.SelectedIndex > 0)
                                appraiserData = appraiserData.Where(p => p.appAppraisalStatus.ToString() == ddlAppraiserAppraisalStatus.SelectedItem.Text).ToList();

                            if (ddlAppraiserCompany.SelectedIndex > 0)
                                appraiserData = appraiserData.Where(p => p.CompanyCode.Split('.')[0].ToString() == ddlAppraiserCompany.SelectedItem.Value).ToList();

                            gvAppraiserAppraisals.DataSource = appraiserData;
                            gvAppraiserAppraisals.DataBind();
                            // }
                            //}
                            //}


                            break;
                        case 2:
                            List<SearchAppraisalClass> reviewerData = CommonMaster.GetData("DepartmentHeadEmployeeCode", hfEmpId.Value);
                            if (ddlReviewer.SelectedIndex != -1)
                                reviewerData = reviewerData.Where(p => p.appPerformanceCycle.ToString() == ddlReviewer.SelectedItem.Text).ToList();

                            if (txtReviewerEmployeeName.Text != null && txtReviewerEmployeeName.Text != string.Empty)
                                //reviewerData = reviewerData.Where(p => p.EmpName.ToString().ToLower() == (txtReviewerEmployeeName.Text.ToLower().Trim())).ToList();
                                reviewerData = reviewerData.Where(p => (p.EmpName == null ? "" : p.EmpName.ToString().ToLower()) == (txtReviewerEmployeeName.Text.ToLower().Trim())).ToList();

                            if (txtReviewerEmployeeCode.Text != null && txtReviewerEmployeeCode.Text != string.Empty)
                                //reviewerData = reviewerData.Where(p => p.appEmployeeCode.ToString() == (txtReviewerEmployeeCode.Text.Trim())).ToList();
                                reviewerData = reviewerData.Where(p => (p.appEmployeeCode == null ? "" : p.appEmployeeCode.ToString().ToLower()) == (txtReviewerEmployeeCode.Text.ToLower().Trim())).ToList();

                            if (txtReviewerAppraiser.Text != null && txtReviewerAppraiser.Text != string.Empty)
                                //reviewerData = reviewerData.Where(p => p.ApprName.ToString().ToLower() == (txtReviewerAppraiser.Text.ToLower().Trim())).ToList();
                                reviewerData = reviewerData.Where(p => (p.ApprName == null ? "" : p.ApprName.ToString().ToLower()) == (txtReviewerAppraiser.Text.ToLower().Trim())).ToList();

                            //if (ddlReviewerOU.SelectedIndex > 0)
                            //    reviewerData = reviewerData.Where(p => p.OrganizationUnitCode.ToString() == ddlReviewerOU.SelectedItem.Value).ToList();

                            //if (ddlReviewerPosition.SelectedIndex > 0)
                            //    reviewerData = reviewerData.Where(p => p.PositionCode.ToString() == ddlReviewerPosition.SelectedItem.Value).ToList();

                            if (ddlReviewerArea.SelectedIndex > 0)
                                reviewerData = reviewerData.Where(p => p.PersonnelAreaCode.ToString() == ddlReviewerArea.SelectedItem.Value).ToList();

                            if (ddlRevieweSubArea.SelectedIndex > 0)
                                reviewerData = reviewerData.Where(p => p.PersonnelSubAreaCode.ToString() == ddlRevieweSubArea.SelectedItem.Value).ToList();

                            if (ddlRevieweEmployeegroup.SelectedIndex > 0)
                                reviewerData = reviewerData.Where(p => p.EmployeeGroupCode.ToString() == ddlRevieweEmployeegroup.SelectedItem.Value).ToList();

                            if (ddlRevieweEmployeeSubgroup.SelectedIndex > 0)
                                reviewerData = reviewerData.Where(p => p.EmployeeSubGroupCode.ToString() == ddlRevieweEmployeeSubgroup.SelectedItem.Value).ToList();

                            if (ddlRevieweAppraisalStatus.SelectedIndex > 0)
                                reviewerData = reviewerData.Where(p => p.appAppraisalStatus.ToString() == ddlRevieweAppraisalStatus.SelectedItem.Text).ToList();

                            if (ddlRevieweCompany.SelectedIndex > 0)
                                reviewerData = reviewerData.Where(p => p.CompanyCode.Split('.')[0].ToString() == ddlRevieweCompany.SelectedItem.Value).ToList();

                            gvReviewerAppraisals.DataSource = reviewerData;
                            gvReviewerAppraisals.DataBind();                           
                            break;
                        case 3:
                            List<SearchAppraisalClass> hrData = CommonMaster.GetData("HRBusinessPartnerEmployeeCode", hfEmpId.Value);
                            if (ddlHRBPAppraisalCycle.SelectedIndex != -1)
                                hrData = hrData.Where(p => p.appPerformanceCycle.ToString() == ddlHRBPAppraisalCycle.SelectedItem.Text).ToList();

                            if (txtHRBPEmployeeName.Text != null && txtHRBPEmployeeName.Text != string.Empty)
                                //hrData = hrData.Where(p => p.EmpName.ToString().ToLower() == (txtHRBPEmployeeName.Text.ToLower().Trim())).ToList();
                                hrData = hrData.Where(p => (p.EmpName == null ? "" : p.EmpName.ToString().ToLower()) == (txtHRBPEmployeeName.Text.ToLower().Trim())).ToList();

                            if (txtHRBPEmployeeCode.Text != null && txtHRBPEmployeeCode.Text != string.Empty)
                                //hrData = hrData.Where(p => p.appEmployeeCode.ToString() == (txtHRBPEmployeeCode.Text.Trim())).ToList();
                                  hrData = hrData.Where(p => (p.appEmployeeCode == null ? "" : p.appEmployeeCode.ToString().ToLower()) == (txtHRBPEmployeeCode.Text.ToLower().Trim())).ToList();

                            if (txtHRBPAppraiser.Text != null && txtHRBPAppraiser.Text != string.Empty)
                                //hrData = hrData.Where(p => p.ApprName.ToString().ToLower() == (txtHRBPAppraiser.Text.ToLower().Trim())).ToList();
                                hrData = hrData.Where(p => (p.ApprName == null ? "" : p.ApprName.ToString().ToLower()) == (txtHRBPAppraiser.Text.ToLower().Trim())).ToList();

                            if (txtHRBPReviewer.Text != null && txtHRBPReviewer.Text != string.Empty)
                                //hrData = hrData.Where(p => p.RevName.ToString().ToLower() == (txtHRBPReviewer.Text.ToLower().Trim())).ToList();
                                hrData = hrData.Where(p => (p.RevName == null ? "" : p.RevName.ToString().ToLower()) == (txtHRBPReviewer.Text.ToLower().Trim())).ToList();

                            //if (ddlHRBPOU.SelectedIndex > 0)
                            //    hrData = hrData.Where(p => p.OrganizationUnitCode.ToString() == ddlHRBPOU.SelectedItem.Value).ToList();

                            //if (ddlHRBPPosition.SelectedIndex > 0)
                            //    hrData = hrData.Where(p => p.PositionCode.ToString() == ddlHRBPPosition.SelectedItem.Value).ToList();

                            if (ddlHrPanelArea.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.PersonnelAreaCode.ToString() == ddlHrPanelArea.SelectedItem.Value).ToList();

                            if (ddlHRBPSubArea.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.PersonnelSubAreaCode.ToString() == ddlHRBPSubArea.SelectedItem.Value).ToList();

                            if (ddlHrPanelEmployeeGroup.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.EmployeeGroupCode.ToString() == ddlHrPanelEmployeeGroup.SelectedItem.Value).ToList();

                            if (ddlHRBPEmployeeSubgroup.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.EmployeeSubGroupCode.ToString() == ddlHRBPEmployeeSubgroup.SelectedItem.Value).ToList();

                            if (ddlHRBPAppraisalStatus.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.appAppraisalStatus.ToString() == ddlHRBPAppraisalStatus.SelectedItem.Text).ToList();

                            if (ddlHRBPCompany.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.CompanyCode.Split('.')[0].ToString() == ddlHRBPCompany.SelectedItem.Value).ToList();
                            if (ddlHRBPRegion.SelectedIndex > 0)
                                hrData = hrData.Where(p => p.Region.ToString() == ddlHRBPRegion.SelectedItem.Text).ToList();

                            gvHRBPAppraisals.DataSource = hrData;
                            gvHRBPAppraisals.DataBind();

                            break;
                        case 4:
                            List<SearchAppraisalClass> tmtData = GetData(false, ddlTMTAppraisalCycle.SelectedItem.Text);
                            if (ddlTMTAppraisalCycle.SelectedIndex != -1)
                                tmtData = tmtData.Where(p => p.appPerformanceCycle.ToString() == ddlTMTAppraisalCycle.SelectedItem.Text).ToList();

                            if (txtTMTAppraiseeName.Text != null && txtTMTAppraiseeName.Text != string.Empty)
                                //tmtData = tmtData.Where(p => p.EmpName.ToString().ToLower() == (txtTMTAppraiseeName.Text.ToLower().Trim())).ToList();
                                tmtData = tmtData.Where(p => (p.EmpName == null ? "" : p.EmpName.ToString().ToLower()) == (txtTMTAppraiseeName.Text.ToLower().Trim())).ToList();

                            if (txtTMTAppraiseeEmployeecode.Text != null && txtTMTAppraiseeEmployeecode.Text != string.Empty)
                                //tmtData = tmtData.Where(p => p.appEmployeeCode.ToString() == (txtTMTAppraiseeEmployeecode.Text.Trim())).ToList();
                                tmtData = tmtData.Where(p => (p.appEmployeeCode == null ? "" : p.appEmployeeCode.ToString().ToLower()) == (txtTMTAppraiseeEmployeecode.Text.ToLower().Trim())).ToList();

                            if (txtTMTAppraiser.Text != null && txtTMTAppraiser.Text != string.Empty)
                                //tmtData = tmtData.Where(p => p.ApprName.ToString().ToLower() == (txtTMTAppraiser.Text.ToLower().Trim())).ToList(); OLD
                                tmtData = tmtData.Where(p => (p.ApprName == null ? "" : p.ApprName.ToString().ToLower()) == (txtTMTAppraiser.Text.ToLower().Trim())).ToList();

                            if (txtTMTReviewer.Text != null && txtTMTReviewer.Text != string.Empty)
                                tmtData = tmtData.Where(p => (p.RevName == null ? "" : p.RevName.ToString().ToLower()) == (txtTMTReviewer.Text.ToLower().Trim())).ToList();
                            //tmtData = tmtData.Where(p => p.RevName.ToString().ToLower() == (txtTMTReviewer.Text.ToLower().Trim())).ToList(); OLD

                            //if (ddlTMTOU.SelectedIndex > 0)
                            //    tmtData = tmtData.Where(p => p.OrganizationUnitCode.Split('.')[0].ToString() == ddlTMTOU.SelectedItem.Value).ToList();

                            //if (ddlTMTPosition.SelectedIndex > 0)
                            //    tmtData = tmtData.Where(p => p.PositionCode.Split('.')[0].ToString() == ddlTMTPosition.SelectedItem.Value).ToList();

                            if (ddlTMTArea.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.PersonnelAreaCode.ToString() == ddlTMTArea.SelectedItem.Value).ToList();

                            if (ddlTMTSubArea.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.PersonnelSubAreaCode.ToString() == ddlTMTSubArea.SelectedItem.Value).ToList();

                            if (ddlTMTEmployeegroup.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.EmployeeGroupCode.ToString() == ddlTMTEmployeegroup.SelectedItem.Value).ToList();

                            if (ddlTMTEmployeeSubgroup.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.EmployeeSubGroupCode.ToString() == ddlTMTEmployeeSubgroup.SelectedItem.Value).ToList();

                            if (ddlTMTAppraisalStatus.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.appAppraisalStatus.ToString() == ddlTMTAppraisalStatus.SelectedItem.Text).ToList();

                            if (ddlTMTCompany.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.CompanyCode.Split('.')[0].ToString() == ddlTMTCompany.SelectedItem.Value).ToList();
                            if (ddlTMTRegion.SelectedIndex > 0)
                                tmtData = tmtData.Where(p => p.Region.ToString() == ddlTMTRegion.SelectedItem.Text).ToList();

                            gvTMTAppraisals.DataSource = tmtData;
                            gvTMTAppraisals.DataBind();

                            break;
                        case 5:
                            List<SearchAppraisalClass> regionalData = GetData(true, ddlRegionalHRAppraisalCycle.SelectedItem.Text);
                            if (ddlRegionalHRAppraisalCycle.SelectedIndex != -1)
                                regionalData = regionalData.Where(p => p.appPerformanceCycle.ToString() == ddlRegionalHRAppraisalCycle.SelectedItem.Text).ToList();

                            if (txtRegionalHRAppraiseeName.Text != null && txtRegionalHRAppraiseeName.Text != string.Empty)
                                //regionalData = regionalData.Where(p => p.EmpName.ToString().ToLower() == (txtRegionalHRAppraiseeName.Text.ToLower().Trim())).ToList();
                                  regionalData = regionalData.Where(p => (p.EmpName == null ? "" : p.EmpName.ToString().ToLower()) == (txtRegionalHRAppraiseeName.Text.ToLower().Trim())).ToList();
                           
                            if (txtRegionalHRAppraiseeEmployeecode.Text != null && txtRegionalHRAppraiseeEmployeecode.Text != string.Empty)
                                //regionalData = regionalData.Where(p => p.appEmployeeCode.ToString() == (txtRegionalHRAppraiseeEmployeecode.Text.Trim())).ToList();
                                regionalData = regionalData.Where(p => (p.appEmployeeCode == null ? "" : p.appEmployeeCode.ToString().ToLower()) == (txtRegionalHRAppraiseeEmployeecode.Text.ToLower().Trim())).ToList();
                           
                            if (txtRegionalHRAppraiser.Text != null && txtRegionalHRAppraiser.Text != string.Empty)
                                //regionalData = regionalData.Where(p => p.ApprName.ToString().ToLower() == (txtRegionalHRAppraiser.Text.ToLower().Trim())).ToList();
                                regionalData = regionalData.Where(p => (p.ApprName == null ? "" : p.ApprName.ToString().ToLower()) == (txtRegionalHRAppraiser.Text.ToLower().Trim())).ToList();
                           
                            if (txtRegionalHRReviewer.Text != null && txtRegionalHRReviewer.Text != string.Empty)
                                //regionalData = regionalData.Where(p => p.RevName.ToString().ToLower() == (txtRegionalHRReviewer.Text.ToLower().Trim())).ToList();
                                regionalData = regionalData.Where(p => (p.RevName == null ? "" : p.RevName.ToString().ToLower()) == (txtRegionalHRReviewer.Text.ToLower().Trim())).ToList();
                           
                            //if (ddlRegionalHROU.SelectedIndex > 0)
                            //    regionalData = regionalData.Where(p => p.OrganizationUnitCode.Split('.')[0].ToString() == ddlRegionalHROU.SelectedItem.Value).ToList();

                            //if (ddlRegionalHRPosition.SelectedIndex > 0)
                            //    regionalData = regionalData.Where(p => p.PositionCode.Split('.')[0].ToString() == ddlRegionalHRPosition.SelectedItem.Value).ToList();

                            if (ddlRegionalHRArea.SelectedIndex > 0)
                                regionalData = regionalData.Where(p => p.PersonnelAreaCode.ToString() == ddlRegionalHRArea.SelectedItem.Value).ToList();

                            if (ddlRegionalHRSubArea.SelectedIndex > 0)
                                regionalData = regionalData.Where(p => p.PersonnelSubAreaCode.ToString() == ddlRegionalHRSubArea.SelectedItem.Value).ToList();

                            if (ddlRegionalHREmployeegroup.SelectedIndex > 0)
                                regionalData = regionalData.Where(p => p.EmployeeGroupCode.ToString() == ddlRegionalHREmployeegroup.SelectedItem.Value).ToList();

                            if (ddlRegionalHREmployeeSubgroup.SelectedIndex > 0)
                                regionalData = regionalData.Where(p => p.EmployeeSubGroupCode.ToString() == ddlRegionalHREmployeeSubgroup.SelectedItem.Value).ToList();

                            if (ddlRegionalHRAppraisalStatus.SelectedIndex > 0)
                                regionalData = regionalData.Where(p => p.appAppraisalStatus.ToString() == ddlRegionalHRAppraisalStatus.SelectedItem.Text).ToList();

                            if (ddlRegionalHRCompany.SelectedIndex > 0)
                                regionalData = regionalData.Where(p => p.CompanyCode.Split('.')[0].ToString() == ddlRegionalHRCompany.SelectedItem.Value).ToList();

                            gvRegionalHR.DataSource = regionalData;
                            gvRegionalHR.DataBind();

                            break;
                        case 6:
                            List<SearchAppraisalClass> reviewBoardData = GetData(false, ddlReviewBoardAppraisalCycle.SelectedItem.Text);
                            if (ddlReviewBoardAppraisalCycle.SelectedIndex != -1)
                                reviewBoardData = reviewBoardData.Where(p => p.appPerformanceCycle.ToString() == ddlReviewBoardAppraisalCycle.SelectedItem.Text).ToList();

                            if (txtReviewBoardAppraiseeName.Text != null && txtReviewBoardAppraiseeName.Text != string.Empty)
                                //reviewBoardData = reviewBoardData.Where(p => p.EmpName.ToString().ToLower() == (txtReviewBoardAppraiseeName.Text.ToLower().Trim())).ToList();
                                reviewBoardData = reviewBoardData.Where(p => (p.EmpName == null ? "" : p.EmpName.ToString().ToLower()) == (txtReviewBoardAppraiseeName.Text.ToLower().Trim())).ToList();
                           
                            if (txtReviewBoardAppraiseeEmployeecode.Text != null && txtReviewBoardAppraiseeEmployeecode.Text != string.Empty)
                                //reviewBoardData = reviewBoardData.Where(p => p.appEmployeeCode.ToString() == (txtReviewBoardAppraiseeEmployeecode.Text.Trim())).ToList();
                                reviewBoardData = reviewBoardData.Where(p => (p.appEmployeeCode == null ? "" : p.appEmployeeCode.ToString().ToLower()) == (txtReviewBoardAppraiseeEmployeecode.Text.ToLower().Trim())).ToList();
                           
                            if (txtReviewBoardAppraiser.Text != null && txtReviewBoardAppraiser.Text != string.Empty)
                                //reviewBoardData = reviewBoardData.Where(p => p.ApprName.ToString().ToLower() == (txtReviewBoardAppraiser.Text.ToLower().Trim())).ToList();
                                reviewBoardData = reviewBoardData.Where(p => (p.ApprName == null ? "" : p.ApprName.ToString().ToLower()) == (txtReviewBoardAppraiser.Text.ToLower().Trim())).ToList();
                           
                            if (txtReviewBoardReviewer.Text != null && txtReviewBoardReviewer.Text != string.Empty)
                                //reviewBoardData = reviewBoardData.Where(p => p.RevName.ToString().ToLower() == (txtReviewBoardReviewer.Text.ToLower().Trim())).ToList();
                                reviewBoardData = reviewBoardData.Where(p => (p.RevName == null ? "" : p.RevName.ToString().ToLower()) == (txtReviewBoardReviewer.Text.ToLower().Trim())).ToList();
                           
                            if (ddlReviewBoardArea.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.PersonnelAreaCode.ToString() == ddlReviewBoardArea.SelectedItem.Value).ToList();

                            if (ddlReviewBoardSubArea.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.PersonnelSubAreaCode.ToString() == ddlReviewBoardSubArea.SelectedItem.Value).ToList();

                            if (ddlReviewBoardEmployeegroup.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.EmployeeGroupCode.ToString() == ddlReviewBoardEmployeegroup.SelectedItem.Value).ToList();

                            if (ddlReviewBoardEmployeeSubgroup.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.EmployeeSubGroupCode.ToString() == ddlReviewBoardEmployeeSubgroup.SelectedItem.Value).ToList();

                            if (ddlReviewBoardAppraisalStatus.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.appAppraisalStatus.ToString() == ddlReviewBoardAppraisalStatus.SelectedItem.Text).ToList();

                            if (ddlReviewBoardCompany.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.CompanyCode.Split('.')[0].ToString() == ddlReviewBoardCompany.SelectedItem.Value.ToString()).ToList();
                            if (ddlReviewBoardRegion.SelectedIndex > 0)
                                reviewBoardData = reviewBoardData.Where(p => p.Region.ToString() == ddlReviewBoardRegion.SelectedItem.Text).ToList();

                            gvReviewBord.DataSource = reviewBoardData;
                            gvReviewBord.DataBind();

                            break;

                    }
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in btnSearch_Click event");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                if (tcDashBoard.ActiveTabIndex > 0)
                {
                    string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
                    switch (tcDashBoard.ActiveTabIndex)
                    {
                        case 1:
                            ddlApprasierAppraisalCycle.ClearSelection();
                            ddlApprasierAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                            txtAppraiserEmployeeName.Text = string.Empty;
                            txtAppraiserEmployeeCode.Text = string.Empty;
                            ddlAppraiserArea.SelectedIndex = 0;

                            ddlAppraiserSubArea.Items.Clear();
                            ddlAppraiserSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlAppraiserSubArea.SelectedIndex = 0;

                            ddlAppraiserEmployeeGroup.SelectedIndex = 0;

                            ddlAppraiserEmployeesSubGroup.Items.Clear();
                            ddlAppraiserEmployeesSubGroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlAppraiserEmployeesSubGroup.SelectedIndex = 0;

                            ddlAppraiserAppraisalStatus.SelectedIndex = 0;
                            ddlAppraiserCompany.SelectedIndex = 0;
                            break;
                        case 2:
                            ddlReviewer.ClearSelection();
                            ddlReviewer.Items.FindByText(performanceCycle).Selected = true;
                            txtReviewerEmployeeName.Text = string.Empty;
                            txtReviewerEmployeeCode.Text = string.Empty;
                            txtReviewerAppraiser.Text = string.Empty;
                            ddlReviewerArea.SelectedIndex = 0;

                            ddlRevieweSubArea.Items.Clear();
                            ddlRevieweSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRevieweSubArea.SelectedIndex = 0;

                            ddlRevieweEmployeegroup.Items.Clear();

                            ddlRevieweEmployeeSubgroup.SelectedIndex = 0;
                            ddlRevieweEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRevieweEmployeeSubgroup.SelectedIndex = 0;

                            ddlRevieweAppraisalStatus.SelectedIndex = 0;
                            ddlRevieweCompany.SelectedIndex = 0;
                            break;
                        case 3:
                            ddlHRBPAppraisalCycle.ClearSelection();
                            ddlHRBPAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                            txtHRBPEmployeeName.Text = string.Empty;
                            txtReviewerEmployeeCode.Text = string.Empty;
                            txtHRBPAppraiser.Text = string.Empty;
                            txtHRBPReviewer.Text = string.Empty;
                            ddlHrPanelArea.SelectedIndex = 0;

                            ddlHRBPSubArea.Items.Clear();
                            ddlHRBPSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlHRBPSubArea.SelectedIndex = 0;

                            ddlHrPanelEmployeeGroup.SelectedIndex = 0;

                            ddlHRBPEmployeeSubgroup.Items.Clear();
                            ddlHRBPEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlHRBPEmployeeSubgroup.SelectedIndex = 0;

                            ddlHRBPAppraisalStatus.SelectedIndex = 0;
                            ddlHRBPCompany.SelectedIndex = 0;
                            ddlHRBPRegion.SelectedIndex = 0;
                            break;
                        case 4:
                            ddlTMTAppraisalCycle.ClearSelection();
                            ddlTMTAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                            txtTMTAppraiseeName.Text = string.Empty;
                            txtTMTAppraiseeEmployeecode.Text = string.Empty;
                            txtTMTAppraiser.Text = string.Empty;
                            txtTMTReviewer.Text = string.Empty;
                            ddlTMTArea.SelectedIndex = 0;

                            ddlTMTSubArea.Items.Clear();
                            ddlTMTSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlTMTSubArea.SelectedIndex = 0;

                            ddlTMTEmployeegroup.SelectedIndex = 0;

                            ddlTMTEmployeeSubgroup.Items.Clear();
                            ddlTMTEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlTMTEmployeeSubgroup.SelectedIndex = 0;

                            ddlTMTAppraisalStatus.SelectedIndex = 0;
                            ddlTMTCompany.SelectedIndex = 0;
                            ddlTMTRegion.SelectedIndex = 0;

                            break;
                        case 5:
                            ddlRegionalHRAppraisalCycle.ClearSelection();
                            ddlRegionalHRAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                            txtRegionalHRAppraiseeName.Text = string.Empty;
                            txtRegionalHRAppraiseeEmployeecode.Text = string.Empty;
                            txtRegionalHRAppraiser.Text = string.Empty;
                            txtRegionalHRReviewer.Text = string.Empty;
                            ddlRegionalHRArea.SelectedIndex = 0;

                            ddlRegionalHRSubArea.Items.Clear();
                            ddlRegionalHRSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRegionalHRSubArea.SelectedIndex = 0;

                            ddlRegionalHREmployeegroup.SelectedIndex = 0;

                            ddlRegionalHREmployeeSubgroup.Items.Clear();
                            ddlRegionalHREmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRegionalHREmployeeSubgroup.SelectedIndex = 0;

                            ddlRegionalHRAppraisalStatus.SelectedIndex = 0;
                            ddlRegionalHRCompany.SelectedIndex = 0;
                            break;
                        case 6:
                            ddlReviewBoardAppraisalCycle.ClearSelection();
                            ddlReviewBoardAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                            txtReviewBoardAppraiseeName.Text = string.Empty;
                            txtReviewBoardAppraiseeEmployeecode.Text = string.Empty;
                            txtReviewBoardAppraiser.Text = string.Empty;
                            txtReviewBoardReviewer.Text = string.Empty;
                            ddlReviewBoardArea.SelectedIndex = 0;

                            ddlReviewBoardSubArea.Items.Clear();
                            ddlReviewBoardSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlReviewBoardSubArea.SelectedIndex = 0;

                            ddlReviewBoardEmployeegroup.SelectedIndex = 0;

                            ddlReviewBoardEmployeeSubgroup.Items.Clear();
                            ddlReviewBoardEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlReviewBoardEmployeeSubgroup.SelectedIndex = 0;

                            ddlReviewBoardAppraisalStatus.SelectedIndex = 0;
                            ddlReviewBoardCompany.SelectedIndex = 0;
                            ddlReviewBoardRegion.SelectedIndex = 0;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in btnReset_Click event");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }

        }

        protected void tcDashBoard_ActiveTabChanged(object sender, EventArgs e)
        {
            try
            {
                LogHandler.LogVerbose("tcDashBoard_ActiveTabChanged(object sender, EventArgs e)");
                PostBackTrigger trigger;
                int tabIndex = tcDashBoard.ActiveTabIndex;
                string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
                DropDownBinds();
                switch (tabIndex)
                {
                    case 0:
                        tcDashBoard.ActiveTabIndex = 0;//// ActiveTab = tpAppraiseePanel;

                        break;
                    case 1:
                        //tcDashBoard.ActiveTabIndex = 1;////.ActiveTab = tpAppraiserPanel;
                        if (btnReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnReset";
                            updatePnl.Triggers.Add(trigger);
                        } if (btnSearch.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnSearch";
                            updatePnl.Triggers.Add(trigger);
                        }
                        //List<SearchAppraisalClass> apprData = GetData("ReportingManagerEmployeeCode", hfEmpId.Value, performanceCycle);
                        List<SearchAppraisalClass> apprData = GetData(performanceCycle);
                        apprData = apprData.Where(p => p.ApprCode == hfEmpId.Value).ToList();
                        List<PIPClass> apprPIPData = GetPIP("ReportingManagerEmployeeCode", hfEmpId.Value);
                        gvAppraiserAppraisals.DataSource = apprData;//ViewState["Appraiser"];
                        gvAppraiserAppraisals.DataBind();
                        gvAppraiserPIP.DataSource = apprPIPData;//ViewState["AppraiserPIP"];
                        gvAppraiserPIP.DataBind();
                        //}

                        break;
                    case 2:
                        if (btnReviewerReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnReviewerReset";
                            updatePnl.Triggers.Add(trigger);
                        } if (btnReviewerSearch.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnReviewerSearch";
                            updatePnl.Triggers.Add(trigger);
                        }
                        List<SearchAppraisalClass> revData = GetData(performanceCycle);
                        revData = revData.Where(p => p.RevCode == hfEmpId.Value).ToList();
                        gvReviewerAppraisals.DataSource = revData; //ViewState["Reviewer"];
                        gvReviewerAppraisals.DataBind();

                        break;
                    case 3:
                        //tcDashBoard.ActiveTabIndex = 3;////.ActiveTab = tpHrPanel;
                        if (btnHRBPReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnHRBPReset";
                            updatePnl.Triggers.Add(trigger);
                        }
                        if (btnHRBPSearch.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnHRBPSearch";
                            updatePnl.Triggers.Add(trigger);
                        }
                        List<SearchAppraisalClass> hrData = GetData(performanceCycle);
                        hrData = hrData.Where(p => p.HrCode == hfEmpId.Value).ToList();
                        gvHRBPAppraisals.DataSource = hrData;// ViewState["HrData"];
                        gvHRBPAppraisals.DataBind();

                        break;
                    case 4:
                        //tcDashBoard.ActiveTabIndex = 4;////.ActiveTab = tpTMTPanel;
                        if (btnTMTReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnTMTReset";
                            updatePnl.Triggers.Add(trigger);
                        } if (btnTMTReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnTMTSearch";
                            updatePnl.Triggers.Add(trigger);
                        }
                        List<SearchAppraisalClass> tmtData = GetData(false, performanceCycle);
                        gvTMTAppraisals.DataSource = tmtData;// ViewState["TMTData"];
                        gvTMTAppraisals.DataBind();
                        break;
                    case 5:
                        //tcDashBoard.ActiveTabIndex = 5;////.ActiveTab = tpRegionalHRPanel;
                        if (btnRegionalHRSearch.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnRegionalHRSearch";
                            updatePnl.Triggers.Add(trigger);
                        }
                        if (btnRegionalHRReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnRegionalHRReset";
                            updatePnl.Triggers.Add(trigger);
                        }
                        List<SearchAppraisalClass> rHRData = GetData(true, performanceCycle);
                        gvRegionalHR.DataSource = rHRData;// ViewState["RegionalHR"];
                        gvRegionalHR.DataBind();
                        break;
                    case 6:
                        //tcDashBoard.ActiveTabIndex = 6;////.ActiveTab = tpReviewBoardPanel;
                        if (btnReviewBoardReset.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnReviewBoardReset";
                            updatePnl.Triggers.Add(trigger);
                        } if (btnReviewBoardSearch.Visible)
                        {
                            trigger = new PostBackTrigger();
                            trigger.ControlID = "btnReviewBoardSearch";
                            updatePnl.Triggers.Add(trigger);
                        }
                        List<SearchAppraisalClass> reviewBoardData = GetData(false, performanceCycle);
                        gvReviewBord.DataSource = reviewBoardData;// ViewState["TMTData"];
                        gvReviewBord.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in Active Tab changed event");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
        }

        protected void btnDeactivation_Click(object sender, EventArgs e)
        {
            //string url = SPContext.Current.Web.Url + "_layouts/HRDeactivation/HRDeactivation.aspx";//?AppId= + hfAppraisalID.Value;
            //Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Saved, "H1") + "');window.open('" + url + "','_self'); </script>");
            Response.Redirect((SPContext.Current.Web.Url + "/_layouts/HRDeactivation/HRDeactivation.aspx"), false);
            //Response.End();

        }

        protected void lnkPIPPhase_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkPIPPhase = (LinkButton)sender;
                GridViewRow gvRow = (GridViewRow)lnkPIPPhase.NamingContainer;
                string status = Convert.ToString(lnkPIPPhase.CommandArgument);
                string taskId = tcDashBoard.ActiveTabIndex.ToString();
                int appraisalID = Convert.ToInt32((gvRow.FindControl("lblPIPAppraisalId") as Label).Text);
                string phase = lnkPIPPhase.Text;
                if (tcDashBoard.ActiveTabIndex == 1 || tcDashBoard.ActiveTabIndex == 0)
                {
                    switch (status)
                    {
                        case "H1 - Sign-off by Appraisee":
                            if (tcDashBoard.ActiveTabIndex == 0)
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else if (tcDashBoard.ActiveTabIndex == 1)
                            {
                                Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Rao garu
                                //Response.End();
                            }
                            break;
                        case "H1 - HR Review":
                            if (tcDashBoard.ActiveTabIndex == 0)
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else if (tcDashBoard.ActiveTabIndex == 1)
                            {
                                Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H1AppraiserReview/H1AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);// Mr Rao
                                //Response.End();
                            }
                            break;
                        case "H1-Awaiting Reviewer Approval":
                            if (tcDashBoard.ActiveTabIndex == 0)
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else if (tcDashBoard.ActiveTabIndex == 1)
                            {
                                Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Mr Rao
                                //Response.End();
                            }
                            break;
                        case "H1 - Awaiting Appraisee Sign-off":
                            if (tcDashBoard.ActiveTabIndex == 0)
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else if (tcDashBoard.ActiveTabIndex == 1)
                            {
                                Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Krishna
                                //Response.End();
                            }
                            //Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H1AppraiserReview/H1AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID);//Mr Rao
                            break;
                        case "H1 - Appraisee Appeal":
                            if (tcDashBoard.ActiveTabIndex == 0)
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else if (tcDashBoard.ActiveTabIndex == 1)
                            {
                                Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H1AppraiserReview/H1AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Mr Rao
                                //Response.End();
                            }
                            break;
                        case "H1 – Completed":
                            if (tcDashBoard.ActiveTabIndex == 0)
                            {
                                Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                //Response.End();
                            }
                            else if (tcDashBoard.ActiveTabIndex == 1)
                            {
                                Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Mr Rao
                                //Response.End();
                            }
                            break;

                        // Developed by tataji for H2 PIP on 060720131155


                        case "H2 - Awaiting Appraisee Goal Setting":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                {
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                    //Response.End();
                                }
                                else
                                {
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                    //Response.End();
                                }
                            }
                            break;
                        case "H2 - Awaiting Appraiser Goal Approval":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                //Response.End();

                            }
                            break;
                        case "H2 - Goals Approved":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                //Response.End();

                            }
                            break;
                        case "H2 - Awaiting Self-evaluation":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                //Response.End();

                            }
                            break;
                        case "H2 - Awaiting Appraiser Evaluation":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                //Response.End();

                            }
                            break;
                        case "H2 - Awaiting Reviewer Approval":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                //Response.End();

                            }
                            else
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                                //Response.End();

                            }
                            break;
                        case "H2 - Awaiting Appraisee Sign-off":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            else
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);//Tataji
                                //Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserReview/H2AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID);
                            }
                            //Response.End();

                            break;
                        case "H2 - Sign-off by Appraisee":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            else
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            //Response.End();

                            break;
                        case "H2 – Completed":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            else
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserEvaluation/H2AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            //Response.End();

                            break;
                        case "H2 - Appraisee Appeal":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            else
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserReview/H2AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            //Response.End();

                            break;
                        case "H2 - HR Review":
                            if (phase == "H1")
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/VFSApplicationPages/AppraiserPIPEdit.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            else
                            {
                                if (tcDashBoard.ActiveTabIndex == 0)
                                    Response.Redirect((SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/SignOffView.aspx?TaskID=" + taskId + "&AppId=" + appraisalID), false);
                                else
                                    Response.Redirect(SPContext.Current.Web.Url + "/_Layouts/H2AppraiserReview/H2AppraiserReview.aspx?TaskID=" + taskId + "&AppId=" + appraisalID, false);
                            }
                            //Response.End();
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in lnkPIPPhase_Click event");
            }
            //finally
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            //} 
        }

        #endregion

        #region ddl Bindings
        protected void ddlAppraiseeAppraisalCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddlEmployeeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtEmpSubGroup = null;
                int val = tcDashBoard.ActiveTabIndex;
                switch (val)
                {
                    case 1:
                        if (ddlAppraiserEmployeeGroup.SelectedIndex > 0)
                        {
                            ddlAppraiserEmployeesSubGroup.Items.Clear();
                            dtEmpSubGroup = DropDownWithCodition("Employee Sub Groups", "EmployeeGroupCode", ddlAppraiserEmployeeGroup.SelectedValue.ToString());
                            ddlAppraiserEmployeesSubGroup.DataTextField = "EmployeeSubGroupCode";//EmployeeSubGroupText replaced with EmployeeSubGroupCode, Tataji told that Sekhar sir said
                            ddlAppraiserEmployeesSubGroup.DataValueField = "EmployeeSubGroupCode";
                            ddlAppraiserEmployeesSubGroup.DataSource = dtEmpSubGroup;
                            ddlAppraiserEmployeesSubGroup.DataBind();
                            ddlAppraiserEmployeesSubGroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlAppraiserEmployeesSubGroup.SelectedIndex = 0;
                        }
                        break;
                    case 2:
                        if (ddlRevieweEmployeegroup.SelectedIndex > 0)
                        {

                            ddlRevieweEmployeeSubgroup.Items.Clear();
                            dtEmpSubGroup = DropDownWithCodition("Employee Sub Groups", "EmployeeGroupCode", ddlRevieweEmployeegroup.SelectedValue.ToString());
                            ddlRevieweEmployeeSubgroup.DataTextField = "EmployeeSubGroupCode";//EmployeeSubGroupText replaced with EmployeeSubGroupCode, Tataji told that Sekhar sir said
                            ddlRevieweEmployeeSubgroup.DataValueField = "EmployeeSubGroupCode";
                            ddlRevieweEmployeeSubgroup.DataSource = dtEmpSubGroup;
                            ddlRevieweEmployeeSubgroup.DataBind();
                            ddlRevieweEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRevieweEmployeeSubgroup.SelectedIndex = 0;
                        }
                        break;
                    case 3:
                        if (ddlHrPanelEmployeeGroup.SelectedIndex > 0)
                        {
                            ddlHRBPEmployeeSubgroup.Items.Clear();
                            dtEmpSubGroup = DropDownWithCodition("Employee Sub Groups", "EmployeeGroupCode", ddlHrPanelEmployeeGroup.SelectedValue.ToString());
                            ddlHRBPEmployeeSubgroup.DataTextField = "EmployeeSubGroupCode";//EmployeeSubGroupText Tataji told that Sekhar sir said
                            ddlHRBPEmployeeSubgroup.DataValueField = "EmployeeSubGroupCode";
                            ddlHRBPEmployeeSubgroup.DataSource = dtEmpSubGroup;
                            ddlHRBPEmployeeSubgroup.DataBind();
                            ddlHRBPEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlHRBPEmployeeSubgroup.SelectedIndex = 0;
                        }
                        break;
                    case 4:
                        if (ddlTMTEmployeegroup.SelectedIndex > 0)
                        {
                            ddlTMTEmployeeSubgroup.Items.Clear();
                            dtEmpSubGroup = DropDownWithCodition("Employee Sub Groups", "EmployeeGroupCode", ddlTMTEmployeegroup.SelectedValue.ToString());
                            ddlTMTEmployeeSubgroup.DataTextField = "EmployeeSubGroupCode";//EmployeeSubGroupText replaced with EmployeeSubGroupCode, Tataji told that Sekhar sir said
                            ddlTMTEmployeeSubgroup.DataValueField = "EmployeeSubGroupCode";
                            ddlTMTEmployeeSubgroup.DataSource = dtEmpSubGroup;
                            ddlTMTEmployeeSubgroup.DataBind();
                            ddlTMTEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlTMTEmployeeSubgroup.SelectedIndex = 0;
                        }
                        break;
                    case 5:
                        if (ddlRegionalHREmployeegroup.SelectedIndex > 0)
                        {
                            ddlRegionalHREmployeeSubgroup.Items.Clear();
                            dtEmpSubGroup = DropDownWithCodition("Employee Sub Groups", "EmployeeGroupCode", ddlRegionalHREmployeegroup.SelectedValue.ToString());
                            ddlRegionalHREmployeeSubgroup.DataTextField = "EmployeeSubGroupCode";//EmployeeSubGroupText replaced with EmployeeSubGroupCode, Tataji told that Sekhar sir said
                            ddlRegionalHREmployeeSubgroup.DataValueField = "EmployeeSubGroupCode";
                            ddlRegionalHREmployeeSubgroup.DataSource = dtEmpSubGroup;
                            ddlRegionalHREmployeeSubgroup.DataBind();
                            ddlRegionalHREmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRegionalHREmployeeSubgroup.SelectedIndex = 0;
                        }
                        break;
                    case 6:
                        if (ddlReviewBoardEmployeegroup.SelectedIndex > 0)
                        {
                            ddlReviewBoardEmployeeSubgroup.Items.Clear();
                            dtEmpSubGroup = DropDownWithCodition("Employee Sub Groups", "EmployeeGroupCode", ddlReviewBoardEmployeegroup.SelectedValue.ToString());
                            ddlReviewBoardEmployeeSubgroup.DataTextField = "EmployeeSubGroupCode";//EmployeeSubGroupText replaced with EmployeeSubGroupCode, Tataji told that Sekhar sir said
                            ddlReviewBoardEmployeeSubgroup.DataValueField = "EmployeeSubGroupCode";
                            ddlReviewBoardEmployeeSubgroup.DataSource = dtEmpSubGroup;
                            ddlReviewBoardEmployeeSubgroup.DataBind();
                            ddlReviewBoardEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                            ddlReviewBoardEmployeeSubgroup.SelectedIndex = 0;
                        }
                        break;

                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in ddlEmployeeGroup_SelectedIndexChanged event");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtSubArea = null; ////DropDownWithCodition("Sub Areas", "AreaCode", ddlAppraiserArea.SelectedValue.ToString());
                int val = tcDashBoard.ActiveTabIndex;//Convert.ToInt32(tcDashBoard.TabIndex);
                switch (val)
                {
                    case 1:
                        if (ddlAppraiserArea.SelectedIndex > 0)
                        {
                            ddlAppraiserSubArea.Items.Clear();
                            dtSubArea = DropDownWithCodition("Sub-Areas", "PersonnelAreaCode", ddlAppraiserArea.SelectedValue.ToString());
                            ddlAppraiserSubArea.DataTextField = "SubPersonnelAreaText";
                            ddlAppraiserSubArea.DataValueField = "PersonnelSubAreaCode";
                            ddlAppraiserSubArea.DataSource = dtSubArea;
                            ddlAppraiserSubArea.DataBind();
                            ddlAppraiserSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlAppraiserSubArea.SelectedIndex = 0;
                        }
                        break;
                    case 2:
                        if (ddlReviewerArea.SelectedIndex > 0)
                        {
                            ddlRevieweSubArea.Items.Clear();
                            dtSubArea = DropDownWithCodition("Sub-Areas", "PersonnelAreaCode", ddlReviewerArea.SelectedValue.ToString());
                            ddlRevieweSubArea.DataTextField = "SubPersonnelAreaText";
                            ddlRevieweSubArea.DataValueField = "PersonnelSubAreaCode";
                            ddlRevieweSubArea.DataSource = dtSubArea;
                            ddlRevieweSubArea.DataBind();
                            ddlRevieweSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRevieweSubArea.SelectedIndex = 0;
                        }
                        break;
                    case 3:
                        if (ddlHrPanelArea.SelectedIndex > 0)
                        {
                            ddlHRBPSubArea.Items.Clear();
                            dtSubArea = DropDownWithCodition("Sub-Areas", "PersonnelAreaCode", ddlHrPanelArea.SelectedValue.ToString());
                            ddlHRBPSubArea.DataTextField = "SubPersonnelAreaText";
                            ddlHRBPSubArea.DataValueField = "PersonnelSubAreaCode";
                            ddlHRBPSubArea.DataSource = dtSubArea;
                            ddlHRBPSubArea.DataBind();
                            ddlHRBPSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlHRBPSubArea.SelectedIndex = 0;
                        }
                        break;
                    case 4:
                        if (ddlTMTArea.SelectedIndex > 0)
                        {
                            ddlTMTSubArea.Items.Clear();
                            dtSubArea = DropDownWithCodition("Sub-Areas", "PersonnelAreaCode", ddlTMTArea.SelectedValue.ToString());
                            ddlTMTSubArea.DataTextField = "SubPersonnelAreaText";
                            ddlTMTSubArea.DataValueField = "PersonnelSubAreaCode";
                            ddlTMTSubArea.DataSource = dtSubArea;
                            ddlTMTSubArea.DataBind();
                            ddlTMTSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlTMTSubArea.SelectedIndex = 0;
                        }
                        break;
                    case 5:
                        if (ddlRegionalHRArea.SelectedIndex > 0)
                        {
                            ddlRegionalHRSubArea.Items.Clear();
                            dtSubArea = DropDownWithCodition("Sub-Areas", "PersonnelAreaCode", ddlRegionalHRArea.SelectedValue.ToString());
                            ddlRegionalHRSubArea.DataTextField = "SubPersonnelAreaText";
                            ddlRegionalHRSubArea.DataValueField = "PersonnelSubAreaCode";
                            ddlRegionalHRSubArea.DataSource = dtSubArea;
                            ddlRegionalHRSubArea.DataBind();
                            ddlRegionalHRSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlRegionalHRSubArea.SelectedIndex = 0;
                        }
                        break;
                    case 6:
                        if (ddlReviewBoardArea.SelectedIndex > 0)
                        {
                            ddlReviewBoardSubArea.Items.Clear();
                            dtSubArea = DropDownWithCodition("Sub-Areas", "PersonnelAreaCode", ddlReviewBoardArea.SelectedValue.ToString());
                            ddlReviewBoardSubArea.DataTextField = "SubPersonnelAreaText";
                            ddlReviewBoardSubArea.DataValueField = "PersonnelSubAreaCode";
                            ddlReviewBoardSubArea.DataSource = dtSubArea;
                            ddlReviewBoardSubArea.DataBind();
                            ddlReviewBoardSubArea.Items.Insert(0, new ListItem("Select", "0"));
                            ddlReviewBoardSubArea.SelectedIndex = 0;
                        }
                        break;
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in ddlArea_SelectedIndexChanged event");
            }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Key11", "closeDialog();", true);
            }
        }
        #endregion

        #region Grid Events

        protected void GvHRBPRequestsTracker_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvHRBPRequestsTracker.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["gridHRData"];
            dt.Select("SendForReview=Yes");
            gvHRBPRequestsTracker.DataSource = dt;//// this.ViewState["gridHRData"];
            gvHRBPRequestsTracker.DataBind();
        }

        protected void GvHRBPAppraisals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvHRBPAppraisals.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["gridHRData"];
            gvHRBPAppraisals.DataSource = dt; //this.ViewState["gridHRData"];
            gvHRBPAppraisals.DataBind();
        }

        protected void GvReviewerAppraisals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvReviewerAppraisals.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["gridReviewerData"];
            gvHRBPAppraisals.DataSource = dt;//// this.ViewState["gridReviewerData"];
            gvHRBPAppraisals.DataBind();
        }

        protected void GvAppraiserAppraisals_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAppraiserAppraisals.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["gridReviewerData"];
            gvAppraiserAppraisals.DataSource = dt;//// this.ViewState["gridAppraiserData"];
            gvAppraiserAppraisals.DataBind();
        }

        protected void GvAppraisee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvAppraisee.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)ViewState["gridReviewerData"];
            gvAppraisee.DataSource = dt;//// this.ViewState["gridData"];
            gvAppraisee.DataBind();
        }

        #endregion

        #region Methods

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

        public static SPListItem CheckRegionalHRUser(string list, string colName, string windId)
        {
            SPListItemCollection masterItemsColl = null;
            SPListItem employeeItem = null;
            try
            {
                LogHandler.LogVerbose("CheckRegionalHRUser");
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists[list];
                        SPQuery q = new SPQuery();
                        q.ViewFields = @"<FieldRef Name='LoginName'/><FieldRef Name='EmployeeCode'/>";
                        q.ViewFieldsOnly = true;
                        q.RowLimit = 1;
                        q.Query = "<Where><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + windId + "</Value></Eq></Where>";
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

        private DataTable GetAppraisalsList(string colName, string id)
        {
            DataTable dt;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists["Appraisals"];
                    SPQuery spQuery = new SPQuery();
                    spQuery.Query = "<Where><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + id + "</Value></Eq></Where>";
                    SPListItemCollection items = appraisals.GetItems(spQuery);
                    dt = items.GetDataTable();

                }
                return dt;
            }
        }

        private DataTable GetAppraisalsList()
        {
            DataTable dt;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists["Appraisals"];
                    SPListItemCollection items = appraisals.GetItems();////spQuery
                    dt = items.GetDataTable();

                }
                return dt;
            }
        }

        #region CommentedGetTaskList
        //private DataTable GetTaskListDetails(string currentUserName)
        //{
        //    DataTable dt;
        //    using (SPWeb currentWeb = SPContext.Current.Site.OpenWeb())
        //    {
        //        SPList appraisals = currentWeb.Lists["VFSAppraisalTasks"];
        //        SPQuery spQuery = new SPQuery();
        //        spQuery.Query = "<Where><Eq><FieldRef Name='AssignedTo' /><Value Type='Text'>" + currentUserName + "</Value></Eq></Where>";
        //        ////query.ViewFields = "<FieldRef name='appPerformanceCycle'/><FieldRef name='appAppraisalStatus'/>";
        //        SPListItemCollection items = appraisals.GetItems(spQuery);
        //        dt = items.GetDataTable();
        //        ////gvAppraisee.DataSource = dt;
        //        ////gvAppraisee.DataBind();
        //    }
        //    return dt;
        //} 
        #endregion

        #region commented Previous code
        //private DataTable PreparingGridTable(string userType, string userName, string userid)
        //{
        //    //// DataTable dt = new DataTable();
        //    //// DataTable dtspItem = GetMasterDetails("WindowID", userName);

        //    DataTable dtAppraisal = new DataTable();
        //    DataTable dtTaskList = GetTaskListDetails(userName);
        //    ////string id = dtspItem.Rows[0]["EmployeeCode"].ToString();
        //    switch (userType)
        //    {
        //        case "Appraisee":
        //            ////id= spItem[2].ToString();
        //            dtAppraisal = GetAppraisalsList("appEmployeeCode", userid);
        //            break;
        //        case "Appraiser":
        //            dtAppraisal = GetAppraisalsList("appH1GoalSettingStartDate", userid);
        //            break;
        //        case "Reviewer":
        //            dtAppraisal = GetAppraisalsList("appReviewerCode", userid);
        //            break;
        //        case "HR":
        //            dtAppraisal = GetAppraisalsList("appHRBusinessPartnerCode", userid);
        //            break;
        //        default:
        //            dtAppraisal = GetAppraisalsList();
        //            break;
        //    }
        //    if (dtAppraisal != null)
        //    {
        //        dtAppraisal.Columns.Add("TaskID", typeof(int));
        //        dtAppraisal.Columns.Add("EmpName", typeof(string));
        //        dtAppraisal.Columns.Add("ApprName", typeof(string));
        //        dtAppraisal.Columns.Add("RevName", typeof(string));
        //        dtAppraisal.Columns.Add("HrName", typeof(string));
        //        foreach (DataRow drAppr in dtAppraisal.Rows)
        //        {
        //            SPListItem spItemEmp = GetMasterDetails("EmployeeCode", drAppr["appEmployeeCode"].ToString());
        //            SPListItem spItemAppr = GetMasterDetails("EmployeeCode", drAppr["appH1GoalSettingStartDate"].ToString());
        //            SPListItem spItemRev = GetMasterDetails("EmployeeCode", drAppr["appReviewerCode"].ToString());
        //            SPListItem spItemHr = GetMasterDetails("EmployeeCode", drAppr["appHRBusinessPartnerCode"].ToString());
        //            drAppr["EmpName"] = spItemEmp["WindowID"].ToString();
        //            drAppr["ApprName"] = spItemAppr["WindowID"].ToString();
        //            drAppr["RevName"] = spItemRev["WindowID"].ToString();
        //            drAppr["HrName"] = spItemHr["WindowID"].ToString();

        //            #region TaskIdBindingCode
        //            ////if (dtTaskList != null)
        //            ////{
        //            ////    foreach (DataRow drTask in dtTaskList.Rows)
        //            ////    {
        //            ////        if (userType == "Appraisee")
        //            ////        {
        //            ////            string apprId = drAppr["ID"].ToString();
        //            ////            string tskId = drTask["tskAppraisalId"].ToString();
        //            ////            string apprCode = drAppr["appEmployeeCode"].ToString();
        //            ////            string tskCode = drTask["tskAppraiseeCode"].ToString();
        //            ////            if (drAppr["ID"].ToString() == drTask["tskAppraisalId"].ToString() && drAppr["appEmployeeCode"].ToString() == drTask["tskAppraiseeCode"].ToString())
        //            ////            {
        //            ////                drAppr["TaskID"] = drTask["ID"];

        //            ////            }
        //            ////        }
        //            ////        else if (userType == "Appraiser")
        //            ////        {
        //            ////            if (drAppr["ID"].ToString() == drTask["tskAppraisalId"].ToString() && drAppr["appH1GoalSettingStartDate"].ToString() == drTask["tskAppraiserCode"].ToString())
        //            ////            {
        //            ////                drAppr["TaskID"] = drTask["ID"];
        //            ////            }
        //            ////        }
        //            ////        else if (userType == "Reviewer")
        //            ////        {
        //            ////            if (drAppr["ID"].ToString() == drTask["tskAppraisalId"].ToString() && drAppr["appReviewerCode"].ToString() == drTask["tskReviewerCode"].ToString())
        //            ////            {
        //            ////                drAppr["TaskID"] = drTask["ID"];
        //            ////            }
        //            ////        }
        //            ////        else if (userType == "HR")
        //            ////        {
        //            ////            if (drAppr["ID"].ToString() == drTask["tskAppraisalId"].ToString() && drAppr["appHRBusinessPartnerCode"].ToString() == drTask["tskHRCode"].ToString())
        //            ////            {
        //            ////                drAppr["TaskID"] = drTask["ID"];
        //            ////            }
        //            ////        }
        //            ////        ////switch(userType)
        //            ////        ////{
        //            ////        ////    case "Appraisee":
        //            ////        ////        if(drAppr["ID"]==drTask["tskAppraisalId"]&& drAppr["appEmployeeCode"]==drTask["tskAppraiseeCode"])
        //            ////        ////    break;
        //            ////        ////    case "Appraiser":
        //            ////        ////        if(drAppr["ID"]==drTask["tskAppraisalId"]&& drAppr["appH1GoalSettingStartDate"]==drTask["tskAppraiserCode"])
        //            ////        ////    break;
        //            ////        ////    case "Reviewer":
        //            ////        ////        if(drAppr["ID"]==drTask["tskAppraisalId"]&& drAppr["appReviewerCode"]==drTask["tskReviewerCode"])
        //            ////        ////    break;
        //            ////        ////    case "HR":
        //            ////        ////        if(drAppr["ID"]==drTask["tskAppraisalId"]&& drAppr["appHRBusinessPartnerCode"]==drTask["tskHRCode"])
        //            ////        ////    break;

        //            ////        ////}

        //            ////    }
        //            ////} 
        //            #endregion

        //        }
        //    }
        //    return dtAppraisal;
        //} 
        #endregion

        private bool CheckSavedDraft(int appraisalID)
        {
            bool flag = false;
            try
            {
                LogHandler.LogVerbose("CheckSavedDraft");
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList appraisalGoals = currentWeb.Lists["Appraisal Goals Draft"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='agAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq></Where>";

                        SPListItemCollection coll = appraisalGoals.GetItems(q);
                        if (coll.Count > 0)
                        {
                            flag = true;
                            return flag;
                        }
                        appraisalGoals = currentWeb.Lists["Appraisal Goals"];
                        q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='agAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq></Where>";

                        coll = appraisalGoals.GetItems(q);
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

        private DataTable DropDownValues(string list)
        {
            LogHandler.LogVerbose("DropDownValues");
            DataTable dt = null;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists[list];
                    SPListItemCollection items = appraisals.GetItems();////(spQuery);
                    dt = items.GetDataTable();

                }
                return dt;

            }
        }

        private static object _lock = new object();
        private DataTable GetDropDownItems(string list)
        {
            LogHandler.LogVerbose("GetDropDownItems");
            DataTable dtDropDownItems = null;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {

                    SPListItemCollection objListItems;
                    lock (_lock)
                    {
                        SPList appraisals = currentWeb.Lists[list];
                        objListItems = appraisals.GetItems();
                        dtDropDownItems = objListItems.GetDataTable();
                        //Cache.Add(list, dtDropDownItems.Clone(), null, DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null); // provide rest of the parameter of this method
                    }
                }
            }
            return dtDropDownItems;
        }



        protected void DropDownBinds()
        {
            LogHandler.LogVerbose("DropDownBinds");
            //DataTable dtOU;
            DataTable dtArea;
            DataTable dtEmpGroup;
            DataTable dtAppraisalStatus;
            DataTable dtCompanies;
            //DataTable dtPositions;
            DataTable dtRegions;
            DataTable dtPerformanceCycles;

            //if (Cache["Organization Units"] == null)
            //    dtOU = GetDropDownItems("Organization Units");
            //else
            //    dtOU = Cache["Organization Units"] as DataTable;

            if (Cache["Areas"] == null)
            {
                dtArea = GetDropDownItems("Areas");
                Cache["Areas"] = dtArea;
            }
            else
                dtArea = Cache["Areas"] as DataTable;

            if (Cache["Employee Groups"] == null)
            {
                dtEmpGroup = GetDropDownItems("Employee Groups");
                Cache["Employee Groups"] = dtEmpGroup;
            }
            else
                dtEmpGroup = Cache["Employee Groups"] as DataTable;

            if (Cache["Appraisal Status"] == null)
            {
                dtAppraisalStatus = GetDropDownItems("Appraisal Status");
                Cache["Appraisal Status"] = dtAppraisalStatus;
            }
            else
                dtAppraisalStatus = Cache["Appraisal Status"] as DataTable;

            if (Cache["Companies"] == null)
            {
                dtCompanies = GetDropDownItems("Companies");
                Cache["Companies"] = dtCompanies;
            }
            else
                dtCompanies = Cache["Companies"] as DataTable;
            if (Cache["Regions"] == null)
            {
                dtRegions = GetDropDownItems("Regions");
                Cache["Regions"] = dtRegions;
            }
            else
                dtRegions = Cache["Regions"] as DataTable;

            //if (Cache["Positions"] == null)
            //    dtPositions = GetDropDownItems("Positions");
            //else
            //    dtPositions = Cache["Positions"] as DataTable;

            dtPerformanceCycles = CommonMaster.GetPerformanceCycles();
            string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();

            dtArea.DefaultView.Sort = "PersonnelAreaText Asc";
            dtCompanies.DefaultView.Sort = "CompanyName Asc";
            dtEmpGroup.DefaultView.Sort = "EmployeeGroupText Asc";

            int val = tcDashBoard.ActiveTabIndex; //Convert.ToInt32(tcDashBoard.TabIndex);
            switch (val)
            {
                case 1:
                    {
                        //ddlAppraiserOU.Items.Clear();
                        //ddlAppraiserOU.DataTextField = "OrganizationUnitLongText";
                        //ddlAppraiserOU.DataValueField = "OrganizationUnitCode";
                        //ddlAppraiserOU.DataSource = dtOU;
                        //ddlAppraiserOU.DataBind();
                        //ddlAppraiserOU.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlAppraiserOU.SelectedIndex = 0;

                        ddlAppraiserArea.Items.Clear();
                        ddlAppraiserArea.DataTextField = "PersonnelAreaText";
                        ddlAppraiserArea.DataValueField = "PersonnelAreaCode";
                        dtArea.DefaultView.Sort = "PersonnelAreaText Asc";
                        ddlAppraiserArea.DataSource = dtArea;
                        ddlAppraiserArea.DataBind();
                        ddlAppraiserArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlAppraiserArea.SelectedIndex = 0;

                        ddlAppraiserSubArea.Items.Clear();
                        ddlAppraiserSubArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlAppraiserSubArea.SelectedIndex = 0;

                        ddlAppraiserEmployeeGroup.Items.Clear();
                        ddlAppraiserEmployeeGroup.DataTextField = "EmployeeGroupText";
                        ddlAppraiserEmployeeGroup.DataValueField = "EmployeeGroupCode";
                        dtEmpGroup.DefaultView.Sort = "EmployeeGroupText Asc";
                        ddlAppraiserEmployeeGroup.DataSource = dtEmpGroup;
                        ddlAppraiserEmployeeGroup.DataBind();
                        ddlAppraiserEmployeeGroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlAppraiserEmployeeGroup.SelectedIndex = 0;

                        ddlAppraiserEmployeesSubGroup.Items.Clear();
                        ddlAppraiserEmployeesSubGroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlAppraiserEmployeesSubGroup.SelectedIndex = 0;

                        ddlAppraiserAppraisalStatus.Items.Clear();
                        ddlAppraiserAppraisalStatus.DataTextField = "Appraisal_x0020_Workflow_x0020_S";
                        ddlAppraiserAppraisalStatus.DataValueField = "ID";
                        ddlAppraiserAppraisalStatus.DataSource = dtAppraisalStatus;
                        ddlAppraiserAppraisalStatus.DataBind();
                        ddlAppraiserAppraisalStatus.Items.Insert(0, new ListItem("Select", "0"));
                        ddlAppraiserAppraisalStatus.SelectedIndex = 0;

                        ddlAppraiserCompany.Items.Clear();
                        ddlAppraiserCompany.DataTextField = "CompanyName";
                        ddlAppraiserCompany.DataValueField = "CompanyCode";
                        dtCompanies.DefaultView.Sort = "CompanyName Asc";
                        ddlAppraiserCompany.DataSource = dtCompanies;
                        ddlAppraiserCompany.DataBind();
                        ddlAppraiserCompany.Items.Insert(0, new ListItem("Select", "0"));
                        ddlAppraiserCompany.SelectedIndex = 0;
                        ddlApprasierAppraisalCycle.Items.Clear();
                        ddlApprasierAppraisalCycle.DataTextField = "Performance_x0020_Cycle";
                        ddlApprasierAppraisalCycle.DataValueField = "Performance_x0020_Cycle";
                        ddlApprasierAppraisalCycle.DataSource = dtPerformanceCycles;
                        ddlApprasierAppraisalCycle.DataBind();
                        ddlApprasierAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                        //ddlApprasierAppraisalCycle.SelectedIndex = 0;
                    }
                    break;
                case 2:
                    {
                        ddlReviewerArea.Items.Clear();
                        ddlReviewerArea.DataTextField = "PersonnelAreaText";
                        ddlReviewerArea.DataValueField = "PersonnelAreaCode";
                        ddlReviewerArea.DataSource = dtArea;
                        ddlReviewerArea.DataBind();
                        ddlReviewerArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewerArea.SelectedIndex = 0;

                        ddlRevieweSubArea.Items.Clear();
                        ddlRevieweSubArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRevieweSubArea.SelectedIndex = 0;

                        ddlRevieweEmployeegroup.Items.Clear();
                        ddlRevieweEmployeegroup.DataTextField = "EmployeeGroupText";
                        ddlRevieweEmployeegroup.DataValueField = "EmployeeGroupCode";
                        ddlRevieweEmployeegroup.DataSource = dtEmpGroup;
                        ddlRevieweEmployeegroup.DataBind();
                        ddlRevieweEmployeegroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRevieweEmployeegroup.SelectedIndex = 0;

                        ddlRevieweEmployeeSubgroup.Items.Clear();
                        ddlRevieweEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRevieweEmployeeSubgroup.SelectedIndex = 0;

                        ddlRevieweAppraisalStatus.Items.Clear();
                        ddlRevieweAppraisalStatus.DataTextField = "Appraisal_x0020_Workflow_x0020_S";
                        ddlRevieweAppraisalStatus.DataValueField = "ID";
                        ddlRevieweAppraisalStatus.DataSource = dtAppraisalStatus;
                        ddlRevieweAppraisalStatus.DataBind();
                        ddlRevieweAppraisalStatus.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRevieweAppraisalStatus.SelectedIndex = 0;

                        ddlRevieweCompany.Items.Clear();
                        ddlRevieweCompany.DataTextField = "CompanyName";
                        ddlRevieweCompany.DataValueField = "CompanyCode";
                        ddlRevieweCompany.DataSource = dtCompanies;
                        ddlRevieweCompany.DataBind();
                        ddlRevieweCompany.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRevieweCompany.SelectedIndex = 0;

                        ddlReviewer.Items.Clear();
                        ddlReviewer.DataTextField = "Performance_x0020_Cycle";
                        ddlReviewer.DataValueField = "Performance_x0020_Cycle";
                        ddlReviewer.DataSource = dtPerformanceCycles;
                        ddlReviewer.DataBind();
                        ddlReviewer.Items.FindByText(performanceCycle).Selected = true;
                        //ddlReviewer.SelectedIndex = 0;
                    }
                    break;
                case 3:
                    {

                        ddlHrPanelArea.Items.Clear();
                        ddlHrPanelArea.DataTextField = "PersonnelAreaText";
                        ddlHrPanelArea.DataValueField = "PersonnelAreaCode";
                        ddlHrPanelArea.DataSource = dtArea;
                        ddlHrPanelArea.DataBind();
                        ddlHrPanelArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHrPanelArea.SelectedIndex = 0;

                        ddlHRBPSubArea.Items.Clear();
                        ddlHRBPSubArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHRBPSubArea.SelectedIndex = 0;

                        ddlHrPanelEmployeeGroup.Items.Clear();
                        ddlHrPanelEmployeeGroup.DataTextField = "EmployeeGroupText";
                        ddlHrPanelEmployeeGroup.DataValueField = "EmployeeGroupCode";
                        ddlHrPanelEmployeeGroup.DataSource = dtEmpGroup;
                        ddlHrPanelEmployeeGroup.DataBind();
                        ddlHrPanelEmployeeGroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHrPanelEmployeeGroup.SelectedIndex = 0;

                        ddlHRBPEmployeeSubgroup.Items.Clear();
                        ddlHRBPEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHRBPEmployeeSubgroup.SelectedIndex = 0;

                        ddlHRBPAppraisalStatus.Items.Clear();
                        ddlHRBPAppraisalStatus.DataTextField = "Appraisal_x0020_Workflow_x0020_S";
                        ddlHRBPAppraisalStatus.DataValueField = "ID";
                        ddlHRBPAppraisalStatus.DataSource = dtAppraisalStatus;
                        ddlHRBPAppraisalStatus.DataBind();
                        ddlHRBPAppraisalStatus.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHRBPAppraisalStatus.SelectedIndex = 0;

                        ddlHRBPCompany.Items.Clear();
                        ddlHRBPCompany.DataTextField = "CompanyName";
                        ddlHRBPCompany.DataValueField = "CompanyCode";
                        ddlHRBPCompany.DataSource = dtCompanies;
                        ddlHRBPCompany.DataBind();
                        ddlHRBPCompany.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHRBPCompany.SelectedIndex = 0;

                        //ddlHRBPPosition.Items.Clear();
                        //ddlHRBPPosition.DataTextField = "PositionLongText";
                        //ddlHRBPPosition.DataValueField = "PositionCode";
                        //ddlHRBPPosition.DataSource = dtPositions;
                        //ddlHRBPPosition.DataBind();
                        //ddlHRBPPosition.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlHRBPPosition.SelectedIndex = 0;

                        ddlHRBPAppraisalCycle.Items.Clear();
                        ddlHRBPAppraisalCycle.DataTextField = "Performance_x0020_Cycle";
                        ddlHRBPAppraisalCycle.DataValueField = "Performance_x0020_Cycle";
                        ddlHRBPAppraisalCycle.DataSource = dtPerformanceCycles;
                        ddlHRBPAppraisalCycle.DataBind();
                        ddlHRBPAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                        //ddlHRBPAppraisalCycle.SelectedIndex = 0;

                        ddlHRBPRegion.Items.Clear();
                        ddlHRBPRegion.DataTextField = "RegionName";
                        ddlHRBPRegion.DataValueField = "ID";
                        ddlHRBPRegion.DataSource = dtRegions;
                        ddlHRBPRegion.DataBind();
                        ddlHRBPRegion.Items.Insert(0, new ListItem("Select", "0"));
                        ddlHRBPRegion.SelectedIndex = 0;
                    }
                    break;
                case 4:
                    {
                        //ddlTMTOU.Items.Clear();
                        //ddlTMTOU.DataTextField = "OrganizationUnitLongText";
                        //ddlTMTOU.DataValueField = "OrganizationUnitCode";
                        //ddlTMTOU.DataSource = dtOU;
                        //ddlTMTOU.DataBind();
                        //ddlTMTOU.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlTMTOU.SelectedIndex = 0;

                        ddlTMTArea.Items.Clear();
                        ddlTMTArea.DataTextField = "PersonnelAreaText";
                        ddlTMTArea.DataValueField = "PersonnelAreaCode";
                        ddlTMTArea.DataSource = dtArea;
                        ddlTMTArea.DataBind();
                        ddlTMTArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTArea.SelectedIndex = 0;

                        ddlTMTSubArea.Items.Clear();
                        ddlTMTSubArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTSubArea.SelectedIndex = 0;

                        ddlTMTEmployeegroup.Items.Clear();
                        ddlTMTEmployeegroup.DataTextField = "EmployeeGroupText";
                        ddlTMTEmployeegroup.DataValueField = "EmployeeGroupCode";
                        ddlTMTEmployeegroup.DataSource = dtEmpGroup;
                        ddlTMTEmployeegroup.DataBind();
                        ddlTMTEmployeegroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTEmployeegroup.SelectedIndex = 0;

                        ddlTMTEmployeeSubgroup.Items.Clear();
                        ddlTMTEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTEmployeeSubgroup.SelectedIndex = 0;

                        ddlTMTAppraisalStatus.Items.Clear();
                        ddlTMTAppraisalStatus.DataTextField = "Appraisal_x0020_Workflow_x0020_S";
                        ddlTMTAppraisalStatus.DataValueField = "ID";
                        ddlTMTAppraisalStatus.DataSource = dtAppraisalStatus;
                        ddlTMTAppraisalStatus.DataBind();
                        ddlTMTAppraisalStatus.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTAppraisalStatus.SelectedIndex = 0;

                        ddlTMTCompany.Items.Clear();
                        ddlTMTCompany.DataTextField = "CompanyName";
                        ddlTMTCompany.DataValueField = "CompanyCode";
                        ddlTMTCompany.DataSource = dtCompanies;
                        ddlTMTCompany.DataBind();
                        ddlTMTCompany.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTCompany.SelectedIndex = 0;

                        //ddlTMTPosition.Items.Clear();
                        //ddlTMTPosition.DataTextField = "PositionLongText";
                        //ddlTMTPosition.DataValueField = "PositionCode";
                        //ddlTMTPosition.DataSource = dtPositions;
                        //ddlTMTPosition.DataBind();
                        //ddlTMTPosition.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlTMTPosition.SelectedIndex = 0;

                        ddlTMTAppraisalCycle.Items.Clear();
                        ddlTMTAppraisalCycle.DataTextField = "Performance_x0020_Cycle";
                        ddlTMTAppraisalCycle.DataValueField = "Performance_x0020_Cycle";
                        ddlTMTAppraisalCycle.DataSource = dtPerformanceCycles;
                        ddlTMTAppraisalCycle.DataBind();
                        ddlTMTAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                        //ddlTMTAppraisalCycle.SelectedIndex = 0;

                        ddlTMTRegion.Items.Clear();
                        ddlTMTRegion.DataTextField = "RegionName";
                        ddlTMTRegion.DataValueField = "ID";
                        ddlTMTRegion.DataSource = dtRegions;
                        ddlTMTRegion.DataBind();
                        ddlTMTRegion.Items.Insert(0, new ListItem("Select", "0"));
                        ddlTMTRegion.SelectedIndex = 0;
                    }
                    break;
                case 5:
                    {
                        //ddlRegionalHROU.Items.Clear();
                        //ddlRegionalHROU.DataTextField = "OrganizationUnitLongText";
                        //ddlRegionalHROU.DataValueField = "OrganizationUnitCode";
                        //ddlRegionalHROU.DataSource = dtOU;
                        //ddlRegionalHROU.DataBind();
                        //ddlRegionalHROU.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlRegionalHROU.SelectedIndex = 0;

                        ddlRegionalHRArea.Items.Clear();
                        ddlRegionalHRArea.DataTextField = "PersonnelAreaText";
                        ddlRegionalHRArea.DataValueField = "PersonnelAreaCode";
                        ddlRegionalHRArea.DataSource = dtArea;
                        ddlRegionalHRArea.DataBind();
                        ddlRegionalHRArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRegionalHRArea.SelectedIndex = 0;

                        ddlRegionalHRSubArea.Items.Clear();
                        ddlRegionalHRSubArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRegionalHRSubArea.SelectedIndex = 0;

                        ddlRegionalHREmployeegroup.Items.Clear();
                        ddlRegionalHREmployeegroup.DataTextField = "EmployeeGroupText";
                        ddlRegionalHREmployeegroup.DataValueField = "EmployeeGroupCode";
                        ddlRegionalHREmployeegroup.DataSource = dtEmpGroup;
                        ddlRegionalHREmployeegroup.DataBind();
                        ddlRegionalHREmployeegroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRegionalHREmployeegroup.SelectedIndex = 0;

                        ddlRegionalHREmployeeSubgroup.Items.Clear();
                        ddlRegionalHREmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRegionalHREmployeeSubgroup.SelectedIndex = 0;

                        ddlRegionalHRAppraisalStatus.Items.Clear();
                        ddlRegionalHRAppraisalStatus.DataTextField = "Appraisal_x0020_Workflow_x0020_S";
                        ddlRegionalHRAppraisalStatus.DataValueField = "ID";
                        ddlRegionalHRAppraisalStatus.DataSource = dtAppraisalStatus;
                        ddlRegionalHRAppraisalStatus.DataBind();
                        ddlRegionalHRAppraisalStatus.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRegionalHRAppraisalStatus.SelectedIndex = 0;

                        ddlRegionalHRCompany.Items.Clear();
                        ddlRegionalHRCompany.DataTextField = "CompanyName";
                        ddlRegionalHRCompany.DataValueField = "CompanyCode";
                        ddlRegionalHRCompany.DataSource = dtCompanies;
                        ddlRegionalHRCompany.DataBind();
                        ddlRegionalHRCompany.Items.Insert(0, new ListItem("Select", "0"));
                        ddlRegionalHRCompany.SelectedIndex = 0;

                        //ddlRegionalHRPosition.Items.Clear();
                        //ddlRegionalHRPosition.DataTextField = "PositionLongText";
                        //ddlRegionalHRPosition.DataValueField = "PositionCode";
                        //ddlRegionalHRPosition.DataSource = dtPositions;
                        //ddlRegionalHRPosition.DataBind();
                        //ddlRegionalHRPosition.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlRegionalHRPosition.SelectedIndex = 0;

                        ddlRegionalHRAppraisalCycle.Items.Clear();
                        ddlRegionalHRAppraisalCycle.DataTextField = "Performance_x0020_Cycle";
                        ddlRegionalHRAppraisalCycle.DataValueField = "Performance_x0020_Cycle";
                        ddlRegionalHRAppraisalCycle.DataSource = dtPerformanceCycles;
                        ddlRegionalHRAppraisalCycle.DataBind();
                        ddlRegionalHRAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                        //ddlRegionalHRAppraisalCycle.SelectedIndex = 0;
                    }
                    break;
                case 6:
                    {
                        //ddlReviewBoardOU.Items.Clear();
                        //ddlReviewBoardOU.DataTextField = "OrganizationUnitLongText";
                        //ddlReviewBoardOU.DataValueField = "OrganizationUnitCode";
                        //ddlReviewBoardOU.DataSource = dtOU;
                        //ddlReviewBoardOU.DataBind();
                        //ddlReviewBoardOU.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlReviewBoardOU.SelectedIndex = 0;

                        ddlReviewBoardArea.Items.Clear();
                        ddlReviewBoardArea.DataTextField = "PersonnelAreaText";
                        ddlReviewBoardArea.DataValueField = "PersonnelAreaCode";
                        ddlReviewBoardArea.DataSource = dtArea;
                        ddlReviewBoardArea.DataBind();
                        ddlReviewBoardArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardArea.SelectedIndex = 0;

                        ddlReviewBoardSubArea.Items.Clear();
                        ddlReviewBoardSubArea.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardSubArea.SelectedIndex = 0;

                        ddlReviewBoardEmployeegroup.Items.Clear();
                        ddlReviewBoardEmployeegroup.DataTextField = "EmployeeGroupText";
                        ddlReviewBoardEmployeegroup.DataValueField = "EmployeeGroupCode";
                        ddlReviewBoardEmployeegroup.DataSource = dtEmpGroup;
                        ddlReviewBoardEmployeegroup.DataBind();
                        ddlReviewBoardEmployeegroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardEmployeegroup.SelectedIndex = 0;

                        ddlReviewBoardEmployeeSubgroup.Items.Clear();
                        ddlReviewBoardEmployeeSubgroup.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardEmployeeSubgroup.SelectedIndex = 0;

                        ddlReviewBoardAppraisalStatus.Items.Clear();
                        ddlReviewBoardAppraisalStatus.DataTextField = "Appraisal_x0020_Workflow_x0020_S";
                        ddlReviewBoardAppraisalStatus.DataValueField = "ID";
                        ddlReviewBoardAppraisalStatus.DataSource = dtAppraisalStatus;
                        ddlReviewBoardAppraisalStatus.DataBind();
                        ddlReviewBoardAppraisalStatus.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardAppraisalStatus.SelectedIndex = 0;

                        ddlReviewBoardCompany.Items.Clear();
                        ddlReviewBoardCompany.DataTextField = "CompanyName";
                        ddlReviewBoardCompany.DataValueField = "CompanyCode";
                        ddlReviewBoardCompany.DataSource = dtCompanies;
                        ddlReviewBoardCompany.DataBind();
                        ddlReviewBoardCompany.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardCompany.SelectedIndex = 0;

                        //ddlReviewBoardPosition.Items.Clear();
                        //ddlReviewBoardPosition.DataTextField = "PositionLongText";
                        //ddlReviewBoardPosition.DataValueField = "PositionCode";
                        //ddlReviewBoardPosition.DataSource = dtPositions;
                        //ddlReviewBoardPosition.DataBind();
                        //ddlReviewBoardPosition.Items.Insert(0, new ListItem("Select", "0"));
                        //ddlReviewBoardPosition.SelectedIndex = 0;

                        ddlReviewBoardAppraisalCycle.Items.Clear();
                        ddlReviewBoardAppraisalCycle.DataTextField = "Performance_x0020_Cycle";
                        ddlReviewBoardAppraisalCycle.DataValueField = "Performance_x0020_Cycle";
                        ddlReviewBoardAppraisalCycle.DataSource = dtPerformanceCycles;
                        ddlReviewBoardAppraisalCycle.DataBind();
                        ddlReviewBoardAppraisalCycle.Items.FindByText(performanceCycle).Selected = true;
                        //ddlReviewBoardAppraisalCycle.SelectedIndex = 0;

                        ddlReviewBoardRegion.Items.Clear();
                        ddlReviewBoardRegion.DataTextField = "RegionName";
                        ddlReviewBoardRegion.DataValueField = "ID";
                        ddlReviewBoardRegion.DataSource = dtRegions;
                        ddlReviewBoardRegion.DataBind();
                        ddlReviewBoardRegion.Items.Insert(0, new ListItem("Select", "0"));
                        ddlReviewBoardRegion.SelectedIndex = 0;
                    }
                    break;

            }
        }

        private DataTable DropDownWithCodition(string list, string colName, string val)
        {
            LogHandler.LogVerbose("DropDownWithCodition");
            DataTable dt = null;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists[list];
                    SPQuery spQuery = new SPQuery();
                    if (val != "0")
                        spQuery.Query = "<Where><Eq><FieldRef Name='" + colName + "' /><Value Type='Text'>" + val + "</Value></Eq></Where>";
                    SPListItemCollection items = appraisals.GetItems(spQuery);////(spQuery);
                    dt = items.GetDataTable();

                }
                return dt;
            }
        }

        private Boolean isUserInGroup(string sGroupName, SPUser sUserLoginName)
        {
            Boolean bUserIsInGroup = false;
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    try
                    {
                        currentWeb.AllowUnsafeUpdates = true;
                        SPSecurity.RunWithElevatedPrivileges(delegate()
                        {

                            SPGroup oGroup = currentWeb.Groups[sGroupName];
                            bUserIsInGroup = currentWeb.IsCurrentUserMemberOfGroup(oGroup.ID);
                            //SPGroupCollection userGroups = sUserLoginName.Groups;
                            //foreach (SPGroup group in userGroups)
                            //{
                            //    if (group.Name.Contains(oGroup.Name))
                            //    {
                            //        bUserIsInGroup = true;
                            //        break;
                            //    }
                            //}

                        });
                        currentWeb.AllowUnsafeUpdates = false;
                    }
                    catch (SPException)
                    {
                        bUserIsInGroup = false;
                        currentWeb.AllowUnsafeUpdates = false;
                    }

                }

            }
            return bUserIsInGroup;
        }

        public bool InGroup(SPUser user, SPGroup group)
        {
            return user.Groups.Cast<SPGroup>()
              .Any(g => g.ID == group.ID);
        }
        private List<SearchAppraisalClass> GetData(string performanceCycle, string empId, SPWeb currentWeb)
        {
            if (ViewState["EmpAppData"] != null) return ViewState["EmpAppData"] as List<SearchAppraisalClass>;
            LogHandler.LogVerbose("GetData(string colName, string empId)");
            //string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
            List<SearchAppraisalClass> hrData = new List<SearchAppraisalClass>();

            //SPList appraisalList = currentWeb.Lists["Appraisals"];
            //SPQuery ospAppraisalQuery = new SPQuery();
            //DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
            DataTable dtAppraisal = getAppraisalDT(currentWeb);

            DataTable dtEmpMasters = getEmpMaster(currentWeb);

            hrData = (from appraisal in dtAppraisal.AsEnumerable()
                      join empMasters in dtEmpMasters.AsEnumerable() on appraisal.Field<string>("appEmployeeCode").ToString() equals empMasters.Field<double>("EmployeeCode").ToString() ////into appMasters
                      where empMasters.Field<double>("EmployeeCode") == Convert.ToDouble(empId)
                      && appraisal.Field<double>("appPerformanceCycle").ToString() == performanceCycle
                      select new SearchAppraisalClass
                      {
                          appPerformanceCycle = Convert.ToString(appraisal.Field<double>("appPerformanceCycle")), //Convert.ToDouble(appraisal.PerformanceCycle),
                          appEmployeeCode = empMasters.Field<double>("EmployeeCode").ToString(),//EmployeeCode,
                          appAppraisalStatus = appraisal.Field<string>("appAppraisalStatus"),////&& SqlMethods.Like(empMasters.Field<string>("EmployeeCode").ToString(), cid) 
                          EmpName = empMasters.Field<string>("EmployeeName"),
                          HrCode = empMasters.Field<double>("HRBusinessPartnerEmployeeCode").ToString(),
                          RevCode = empMasters.Field<double>("DepartmentHeadEmployeeCode").ToString(),
                          ApprCode = empMasters.Field<double>("ReportingManagerEmployeeCode").ToString(),
                          ApprName = empMasters.Field<string>("ReportingManagerName"),
                          RevName = empMasters.Field<string>("DepartmentHeadName"),
                          HrName = empMasters.Field<string>("HRBusinessPartnerName"),
                          ID = appraisal.Field<int>("ID").ToString(),
                          IsReview = appraisal.Field<string>("appIsReview") == "True" ? "Not Closed" : appraisal.Field<string>("appIsReview"),
                          Deactivated = string.IsNullOrEmpty(appraisal.Field<string>("appDeactivated")) ? "No" : appraisal.Field<string>("appDeactivated"),
                          Region = empMasters.Field<string>("HRRegion")
                      }).OrderByDescending(p => p.appPerformanceCycle).ToList();

            ViewState["EmpAppData"] = hrData.ToList();
            return hrData;
        }
        private List<SearchAppraisalClass> GetData(string performanceCycle)
        {
            if (ViewState["EmpAppData"] != null) return ViewState["EmpAppData"] as List<SearchAppraisalClass>;
            LogHandler.LogVerbose("GetData(string colName, string empId)");
            //string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
            List<SearchAppraisalClass> hrData = new List<SearchAppraisalClass>();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    //SPList appraisalList = currentWeb.Lists["Appraisals"];
                    //SPQuery ospAppraisalQuery = new SPQuery();
                    //DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
                    DataTable dtAppraisal = getAppraisalDT(currentWeb);

                    DataTable dtEmpMasters = getEmpMaster(currentWeb);
                    hrData = (from appraisal in dtAppraisal.AsEnumerable()
                              join empMasters in dtEmpMasters.AsEnumerable() on appraisal.Field<string>("appEmployeeCode").ToString() equals empMasters.Field<double>("EmployeeCode").ToString() ////into appMasters
                              where appraisal.Field<double>("appPerformanceCycle").ToString() == performanceCycle
                              select new SearchAppraisalClass
                              {
                                  appPerformanceCycle = Convert.ToString(appraisal.Field<double>("appPerformanceCycle")), //Convert.ToDouble(appraisal.PerformanceCycle),
                                  appEmployeeCode = empMasters.Field<double>("EmployeeCode").ToString(),//EmployeeCode,
                                  appAppraisalStatus = appraisal.Field<string>("appAppraisalStatus"),////&& SqlMethods.Like(empMasters.Field<string>("EmployeeCode").ToString(), cid) 
                                  EmpName = empMasters.Field<string>("EmployeeName"),
                                  HrCode = empMasters.Field<double>("HRBusinessPartnerEmployeeCode").ToString(),
                                  RevCode = empMasters.Field<double>("DepartmentHeadEmployeeCode").ToString(),
                                  ApprCode = empMasters.Field<double>("ReportingManagerEmployeeCode").ToString(),
                                  ApprName = empMasters.Field<string>("ReportingManagerName"),
                                  RevName = empMasters.Field<string>("DepartmentHeadName"),
                                  HrName = empMasters.Field<string>("HRBusinessPartnerName"),
                                  ID = appraisal.Field<int>("ID").ToString(),
                                  IsReview = appraisal.Field<string>("appIsReview") == "True" ? "Not Closed" : appraisal.Field<string>("appIsReview"),
                                  Deactivated = string.IsNullOrEmpty(appraisal.Field<string>("appDeactivated")) ? "No" : appraisal.Field<string>("appDeactivated"),
                                  Region = empMasters.Field<string>("HRRegion")

                              }).OrderByDescending(p => p.appPerformanceCycle).ToList();
                }
                ViewState["EmpAppData"] = hrData.ToList();
                return hrData;
            }
        }
        private List<SearchAppraisalClass> GetData(bool regionalHR, string performanceCycle)
        {
            LogHandler.LogVerbose("GetData(bool regionalHR, string performanceCycle)");
            if (regionalHR)
            {
                if (ViewState["HRDataQuery"] != null)
                    return (List<SearchAppraisalClass>)ViewState["HRDataQuery"];
            }
            else
            {
                if (ViewState["DataQuery"] != null)
                    return (List<SearchAppraisalClass>)ViewState["DataQuery"];
            }
            //string performanceCycle = CommonMaster.GetCurrentPerformanceCycle();
            List<SearchAppraisalClass> hrData = new List<SearchAppraisalClass>();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    string hrRegion = string.Empty;
                    if (regionalHR)
                    {
                        using (VFSPMSEntitiesDataContext dc = new VFSPMSEntitiesDataContext(currentWeb.Url))
                        {
                            var region = (from r in dc.Regions where r.RegionHREmployeeCode.ToString().Trim() == hfEmpId.Value select r).FirstOrDefault();
                            if (region != null)
                                hrRegion = region.RegionName;
                            else
                                tpRegionalHRPanel.Enabled = false;
                        }
                    }

                    //SPList appraisalList = currentWeb.Lists["Appraisals"];
                    //SPQuery ospAppraisalQuery = new SPQuery();
                    //DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
                    DataTable dtAppraisal = getAppraisalDT(currentWeb);

                    DataTable dtEmpMasters = getEmpMaster(currentWeb);

                    if (!string.IsNullOrEmpty(hrRegion))
                    {
                        DataView dv = dtEmpMasters.AsDataView();
                        dv.RowFilter = "HRRegion='" + hrRegion + "'";
                        dtEmpMasters = dv.ToTable();
                    }
                    hrData = (from appraisal in dtAppraisal.AsEnumerable()
                              join empMasters in dtEmpMasters.AsEnumerable() on appraisal.Field<string>("appEmployeeCode").ToString() equals empMasters.Field<double>("EmployeeCode").ToString() ////into appMasters
                              //where appraisal.Field<double>("appPerformanceCycle").ToString() == performanceCycle
                              select new SearchAppraisalClass
                              {
                                  appPerformanceCycle = Convert.ToString(appraisal.Field<double>("appPerformanceCycle")), //Convert.ToDouble(appraisal.PerformanceCycle),
                                  appEmployeeCode = empMasters.Field<double>("EmployeeCode").ToString(),//EmployeeCode,
                                  appAppraisalStatus = appraisal.Field<string>("appAppraisalStatus"),////&& SqlMethods.Like(empMasters.Field<string>("EmployeeCode").ToString(), cid) 
                                  EmpName = empMasters.Field<string>("EmployeeName"),
                                  HrCode = empMasters.Field<double>("HRBusinessPartnerEmployeeCode").ToString(),
                                  RevCode = empMasters.Field<double>("DepartmentHeadEmployeeCode").ToString(),
                                  ApprCode = empMasters.Field<double>("ReportingManagerEmployeeCode").ToString(),
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
                                  Deactivated = appraisal.Field<string>("appDeactivated"),
                                  Region = empMasters.Field<string>("HRRegion")

                              }).OrderByDescending(p => p.appPerformanceCycle).ToList();
                }
                foreach (SearchAppraisalClass li in hrData)
                {
                    if (li.IsReview == "True")
                        li.IsReview = "Not Closed";
                    if (string.IsNullOrEmpty(li.Deactivated))
                        li.Deactivated = "No";
                }
            }
            if (regionalHR)
            {
                ViewState["HRDataQuery"] = hrData.ToList();
            }
            else
            {
                ViewState["DataQuery"] = hrData.ToList();
            }
            return hrData;
        }

        private List<PIPClass> GetPIP(string colName, string empId)
        {
            if (colName.Equals("ReportingManagerEmployeeCode"))
            {
                if (ViewState["AppraiserPIPData"] != null) return ViewState["AppraiserPIPData"] as List<PIPClass>;
            }
            else
            {
                if (ViewState["PIPData"] != null) return ViewState["PIPData"] as List<PIPClass>;
            }
            LogHandler.LogVerbose("GetPIP(string colName, string empId)");
            List<PIPClass> pIPData = new List<PIPClass>();
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList pipList = currentWeb.Lists["PIP"];
                        SPQuery pipQuery = new SPQuery();
                        pipQuery.ViewFields = "";
                        pipQuery.ViewFieldsOnly = true;
                        DataTable dtPIP = pipList.GetItems(pipQuery).GetDataTable();

                        if (dtPIP == null || dtPIP.Rows.Count == 0) return null;
                        //SPList appraisalList = currentWeb.Lists["Appraisals"];
                        //SPQuery ospAppraisalQuery = new SPQuery();
                        //DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();

                        DataTable dtAppraisal = getAppraisalDT(currentWeb);

                        //SPList empMastersList = currentWeb.Lists["Employees Master"];
                        //SPQuery ospEmpMastersQuery = new SPQuery();
                        //DataTable dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();

                        DataTable dtEmpMasters = getEmpMaster(currentWeb);

                        SPList appPhaseStatlList = currentWeb.Lists["Appraisal Phases"];
                        SPQuery ospAppPhaseStatQuery = new SPQuery();
                        ospAppPhaseStatQuery.ViewFields = BuildViewFieldsXml("ID", "aphIsPIP", "aphAppraisalId", "aphAppraisalPhase");
                        ospAppPhaseStatQuery.ViewFieldsOnly = true;
                        DataTable dtAppPhaseStat = appPhaseStatlList.GetItems(ospAppPhaseStatQuery).GetDataTable();

                        pIPData = (from appraisal in dtAppraisal.AsEnumerable()
                                   join empMasters in dtEmpMasters.AsEnumerable() on appraisal.Field<string>("appEmployeeCode").ToString() equals empMasters.Field<double>("EmployeeCode").ToString()
                                   join appPhase in dtAppPhaseStat.AsEnumerable() on appraisal.Field<int>("ID").ToString() equals appPhase.Field<double>("aphAppraisalId").ToString()
                                   where appPhase.Field<string>("aphIsPIP") == "True" && empMasters.Field<double>(colName) == Convert.ToDouble(empId)
                                   select new PIPClass
                                   {
                                       ID = (appraisal.Field<int>("ID") == null ? "0" : appraisal.Field<int>("ID").ToString()),
                                       appPerformanceCycle = appraisal.Field<double>("appPerformanceCycle") == null ? "0" : appraisal.Field<double>("appPerformanceCycle").ToString(),
                                       appEmployeeCode = (empMasters.Field<double>("EmployeeCode") == null ? "0" : empMasters.Field<double>("EmployeeCode").ToString()),
                                       EmpName = empMasters.Field<string>("EmployeeName"),
                                       Phase = (appPhase.Field<string>("aphAppraisalPhase") == null ? "0" : appPhase.Field<string>("aphAppraisalPhase")),
                                       appAppraisalStatus = (appraisal.Field<string>("appAppraisalStatus") == null ? "0" : appraisal.Field<string>("appAppraisalStatus"))
                                   }).OrderByDescending(p => p.appPerformanceCycle).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                pIPData = null;
            }
            if (pIPData != null)
            {
                if (colName.Equals("ReportingManagerEmployeeCode"))
                    ViewState["AppraiserPIPData"] = pIPData.ToList();
                else
                    ViewState["PIPData"] = pIPData.ToList();
            }
            return pIPData;
        }

        private bool Locked()
        {
            bool lockYear = false;
            using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(SPContext.Current.Web.Url))
            {
                var locked = (from r in PMSDataContext.PerformanceCycles select r.Locked).LastOrDefault();
                if (Convert.ToBoolean(locked))
                    lockYear = true;
            }
            return lockYear;
        }

        #endregion

        //void GetEmployeeRoles(string EmployeeCode, SPWeb web)
        //{
        //    if (Session["AppraiseeCode"] != null) return;
        //    Page.Session["AppraiseeCode"] = EmployeeCode;
        //    using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
        //    {
        //        var empRoles = (from p in PMSDataContext.EmployeesMaster
        //                        where
        //                            p.HRBusinessPartnerEmployeeCode.ToString() == EmployeeCode
        //                            || p.ReportingManagerEmployeeCode.ToString() == EmployeeCode
        //                            || p.DepartmentHeadEmployeeCode.ToString() == EmployeeCode
        //                        select p);


        //        foreach (EmployeesMasterItem item in empRoles)
        //        {
        //            if (item.DepartmentHeadEmployeeCode.ToString() == EmployeeCode)
        //            {
        //                Page.Session["isReviewer"] = "True";
        //                break;
        //            }
        //        }
        //        foreach (EmployeesMasterItem item in empRoles)
        //        {
        //            if (item.HRBusinessPartnerEmployeeCode.ToString() == EmployeeCode)
        //            {
        //                Page.Session["isHR"] = "True";
        //                break;
        //            }
        //        }
        //        foreach (EmployeesMasterItem item in empRoles)
        //        {
        //            if (item.ReportingManagerEmployeeCode.ToString() == EmployeeCode)
        //            {
        //                Page.Session["isAppraiser"] = "True";
        //                break;
        //            }
        //        }

        //        SPSecurity.RunWithElevatedPrivileges(delegate()
        //        {
        //            SPGroup reviewGroup = web.Groups["Review Board"];
        //            SPGroup TMTGroup = web.Groups["TMT"];
        //            if (web.IsCurrentUserMemberOfGroup(reviewGroup.ID))
        //                Page.Session["isReviewBoard"] = "True";
        //            if (web.IsCurrentUserMemberOfGroup(TMTGroup.ID))
        //                Page.Session["isTMT"] = "True";
        //        });

        //        var RegionalHR = (from r in PMSDataContext.Regions.AsEnumerable() where r.RegionHREmployeeCode.ToString().Trim() == EmployeeCode.Trim() select r).FirstOrDefault();
        //        if (RegionalHR != null)
        //            Page.Session["isRegionalHR"] = "True";
        //    }
        //}
        void GetEmployeeRoles(string EmployeeCode, SPWeb web)
        {
            if (Session["AppraiseeCode"] != null) return;
            Page.Session["AppraiseeCode"] = EmployeeCode;
            using (VFSPMSEntitiesDataContext PMSDataContext = new VFSPMSEntitiesDataContext(web.Url))
            {
                var empRoles = (from p in PMSDataContext.EmployeesMaster
                                where
                                    p.HRBusinessPartnerEmployeeCode.ToString() == EmployeeCode
                                    || p.ReportingManagerEmployeeCode.ToString() == EmployeeCode
                                    || p.DepartmentHeadEmployeeCode.ToString() == EmployeeCode
                                select p);

                if (empRoles.Where(p => p.DepartmentHeadEmployeeCode.ToString() == EmployeeCode).AsEnumerable().Count() > 0)
                    Page.Session["isReviewer"] = "True";

                if (empRoles.Where(p => p.HRBusinessPartnerEmployeeCode.ToString() == EmployeeCode).AsEnumerable().Count() > 0)
                    Page.Session["isHR"] = "True";

                if (empRoles.Where(p => p.ReportingManagerEmployeeCode.ToString() == EmployeeCode).AsEnumerable().Count() > 0)
                    Page.Session["isAppraiser"] = "True";
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPGroup reviewGroup = web.Groups["Review Board"];
                    SPGroup TMTGroup = web.Groups["TMT"];
                    if (web.IsCurrentUserMemberOfGroup(reviewGroup.ID))
                        Page.Session["isReviewBoard"] = "True";
                    if (web.IsCurrentUserMemberOfGroup(TMTGroup.ID))
                        Page.Session["isTMT"] = "True";
                });

                var RegionalHR = (from r in PMSDataContext.Regions.AsEnumerable() where r.RegionHREmployeeCode.ToString().Trim() == EmployeeCode.Trim() select r).FirstOrDefault();
                if (RegionalHR != null)
                    Page.Session["isRegionalHR"] = "True";
            }
        }
        DataTable getAppraisalDT(SPWeb currentWeb)
        {
            SPList appraisalList = currentWeb.Lists["Appraisals"];
            SPQuery ospAppraisalQuery = new SPQuery();
            ospAppraisalQuery.ViewFields = BuildViewFieldsXml("appEmployeeCode", "appPerformanceCycle", "appAppraisalStatus", "appIsReview", "appDeactivated", "ReportingManagerName", "ID");
            ospAppraisalQuery.ViewFieldsOnly = true;
            DataTable dtAppraisal = appraisalList.GetItems(ospAppraisalQuery).GetDataTable();
            return dtAppraisal;
        }
        DataTable getEmpMaster(SPWeb currentWeb)
        {
            DataTable dtEmpMasters = new DataTable();
            if (Session["empMST"] == null)
            {
                SPList empMastersList = currentWeb.Lists["Employees Master"];
                SPQuery ospEmpMastersQuery = new SPQuery();
                ospEmpMastersQuery.ViewFields = BuildViewFieldsXml("EmployeeCode", "EmployeeName",
                    "HRBusinessPartnerEmployeeCode", "DepartmentHeadEmployeeCode", "ReportingManagerEmployeeCode",
                    "ReportingManagerName", "DepartmentHeadName", "HRBusinessPartnerName", "HRRegion",
                    "OUCode", "PositionCode", "SubAreaCode", "AreaCode", "CompanyCode", "EmployeeSubGroup", "EmployeeGroup"
                    );
                ospEmpMastersQuery.ViewFieldsOnly = true;
                dtEmpMasters = empMastersList.GetItems(ospEmpMastersQuery).GetDataTable();
                Session["empMST"] = dtEmpMasters;
            }
            else
                dtEmpMasters = Session["empMST"] as DataTable;

            return dtEmpMasters;
        }

        public static string BuildViewFieldsXml(params string[] fieldNames)
        {
            const string TEMPLATE = @"<FieldRef Name='{0:S}'/>";
            StringBuilder sb = new StringBuilder();
            foreach (string fieldName in fieldNames)
            {
                sb.AppendFormat(TEMPLATE, fieldName);
            }
            return sb.ToString();
        }

    }
}
