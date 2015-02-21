using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFSProjectH1
{
    public partial class HRDecisions : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, dtPip, dtCommentHistory;
        SPUser currentUser;
        string pipbtn;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    DvREvaluation.Visible = true;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = false;
                    dvCHistory.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = true;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = false;
                    dvCHistory.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = true;
                    pipdiv.Visible = false;
                    dvCHistory.Visible = false;
                }

                else if (TabContainer1.ActiveTabIndex == 3)
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = false;
                    dvCHistory.Visible = true;
                }
                else if (TabContainer1.ActiveTabIndex == 4)
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = true;
                    dvCHistory.Visible = false;
                }
                if (!this.IsPostBack)
                {
                    SPList lstAppraisalTasks;  //
                    SPListItem taskItem;       // 
                    SPUser appraisee;

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            lstAppraisalTasks = currentWeb.Lists["Appraisals"];   //
                            taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["AppId"]));  //
                            if (taskItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(taskItem["appFinalRating"]);
                            }
                            lblStatusValue.Text = Convert.ToString(taskItem["appAppraisalStatus"]); //
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(taskItem["appPerformanceCycle"]);


                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                            }
                            if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), currentWeb))
                            {
                                string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                Response.Redirect(url, false);
                                //Response.End();
                            }




                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery(); //
                            phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0]; //
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //
                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]);  //

                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["aphIsPIP"])))
                            {
                                pipbtn = Convert.ToString(phaseItem["aphIsPIP"]);
                                if (pipbtn == "True")
                                {
                                    this.dtPip = CommonMaster.GetPIPDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                                    if (dtPip != null && dtPip.Rows.Count > 0)
                                    {
                                        this.dtPip.Columns.Add("SNo", typeof(string));
                                        int z = 1;
                                        foreach (DataRow dr in this.dtPip.Rows)
                                        {
                                            dr["SNo"] = z;
                                            z++;
                                        }
                                        rptpip.DataSource = this.dtPip;
                                        rptpip.DataBind();
                                    }
                                    else
                                    {
                                        btnComplete.Visible = false;
                                        tbpnlpip.Visible = false;
                                        pipdiv.Visible = false;
                                    }

                                }
                                else
                                {
                                    tbpnlpip.Visible = false;
                                    pipdiv.Visible = false;
                                }
                            }
                            else
                            {
                                tbpnlpip.Visible = false;
                                pipdiv.Visible = false;
                            }
                            if (phaseItem["aphSignoffonbehalfcomments"] != null)
                                lblSignOffComments.Text = Convert.ToString(phaseItem["aphSignoffonbehalfcomments"]);
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName); //Convert.ToString(appraisalItem["Author"]).Split('#')[1]);

                        }

                        SPListItem appraiseeData = CommonMaster.GetTheAppraiseeDetails(appraisee.LoginName);

                        if (appraiseeData != null)
                        {
                            lblHeaderValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                            lblempcodevalue.Text = Convert.ToString(appraiseeData["EmployeeCode"]);
                            lblEmpNameValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                            lblPositionValue.Text = Convert.ToString(appraiseeData["Position"]);
                            lblAppraiserValue.Text = Convert.ToString(appraiseeData["ReportingManagerName"]);
                            lblReviewerValue.Text = Convert.ToString(appraiseeData["DepartmentHeadName"]);
                            lblCountryHRValue.Text = Convert.ToString(appraiseeData["HRBusinessPartnerName"]);
                            lblOrgUnitValue.Text = Convert.ToString(appraiseeData["OrganizationUnit"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(appraiseeData["HireDate"])))
                            {
                                DateTime hireDate = Convert.ToDateTime(appraiseeData["HireDate"]);
                                lblHireDateValue.Text = hireDate.ToString("dd-MMM-yyyy");
                            }
                        }

                        string appraisercode;
                        appraisercode = Convert.ToString(appraiseeData["ReportingManagerEmployeeCode"]);

                        appLog.Value = CommonMaster.GetUserByCode(appraisercode);

                        this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dummyTable != null && dummyTable.Rows.Count > 0)
                        {
                            this.dummyTable.Columns.Add("SNo", typeof(string));

                            int i = 1;
                            int mandatoryGoalCount = 0;
                            foreach (DataRow dr in this.dummyTable.Rows)
                            {
                                dr["SNo"] = i;
                                i++;
                                if (dr["IsMandatory"].ToString() == "True")  //
                                {
                                    mandatoryGoalCount++;
                                }
                            }

                            this.hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                            ViewState["Appraisals"] = this.dummyTable;
                            RptRevaluation.DataSource = ViewState["Appraisals"];
                            RptRevaluation.DataBind();
                        }
                        this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dtCompetencies != null && dtCompetencies.Rows.Count > 0)
                        {
                            this.dtCompetencies.Columns.Add("SNo", typeof(string));

                            int j = 1;
                            foreach (DataRow dr in this.dtCompetencies.Rows)
                            {
                                dr["SNo"] = j;
                                j++;
                            }

                            rptCompetencies.DataSource = this.dtCompetencies;
                            rptCompetencies.DataBind();
                        }

                        this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (DtDevelopmentmesure != null && DtDevelopmentmesure.Rows.Count > 0)
                        {
                            this.DtDevelopmentmesure.Columns.Add("SNo", typeof(string));
                            int k = 1;
                            foreach (DataRow dr in this.DtDevelopmentmesure.Rows)
                            {
                                dr["SNo"] = k;
                                k++;
                            }
                            rptsaftymeasurementdevelopment.DataSource = this.DtDevelopmentmesure;
                            rptsaftymeasurementdevelopment.DataBind();
                        }

                        this.dtCommentHistory = CommonMaster.GetCommonHistory(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        this.dtCommentHistory.Columns.Add("SNo", typeof(string));
                        ViewState["dtCommentsHistory"] = dtCommentHistory;
                        int l = 1;
                        foreach (DataRow dr in this.dtCommentHistory.Rows)
                        {
                            dr["SNo"] = l;
                            l++;
                        }
                        gvCommentsHistory.DataSource = this.dtCommentHistory;
                        gvCommentsHistory.DataBind();
                        //if (pipbtn == "True")
                        //{
                        //    this.dtPip = CommonMaster.GetPIPDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        //    if (dtPip != null && dtPip.Rows.Count > 0)
                        //    {
                        //        this.dtPip.Columns.Add("SNo", typeof(string));
                        //        int z = 1;
                        //        foreach (DataRow dr in this.dtPip.Rows)
                        //        {
                        //            dr["SNo"] = z;
                        //            z++;
                        //        }
                        //        rptpip.DataSource = this.dtPip;
                        //        rptpip.DataBind();
                        //    }
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS HR Decisions PageLoad");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }

        }


        protected void BtnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SaveComments(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb web = osite.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;

                            SPList list = web.Lists["Appraisals"];
                            SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            SPList appraisalStatus = web.Lists["Appraisal Status"];

                            SPListItem lstStatusItem = appraisalStatus.GetItemById(9);

                            SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                            CurrentListItem.Update();

                            web.AllowUnsafeUpdates = false;

                            SPList lstAppraisalPhases = web.Lists["Appraisal Phases"];
                            SPListItem lstItem;
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + hfAppraisalID.Value + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                            lstItem = coll[0];
                            if (Convert.ToString(lstItem["IsReview"]) == "True")
                            {
                                lstItem["IsReview"] = "Closed";
                                web.AllowUnsafeUpdates = true;
                                lstItem.Update();
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                        //WorkflowTriggering("Close");

                        //Context.Response.Write("<script type='text/javascript'>alert('Completed successfully');window.open('" + url + "','_self'); </script>");
                        Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Completed, "H1") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                    }
                });
                WorkflowTriggering("Close");
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS HR Decisions Complete");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }
        protected void BtnReview_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(appLog.Value))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SaveComments(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;

                                SPList list = web.Lists["Appraisals"];
                                SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                SPList appraisalStatus = web.Lists["Appraisal Status"];

                                SPListItem lstStatusItem = appraisalStatus.GetItemById(11);

                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                CurrentListItem["appIsReview"] = "True";
                                CurrentListItem.Update();

                                web.AllowUnsafeUpdates = false;

                                SPList lstAppraisalPhases = web.Lists["Appraisal Phases"];
                                SPListItem lstItem;
                                SPQuery q = new SPQuery();
                                q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + hfAppraisalID.Value + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                                SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                                lstItem = coll[0];

                                lstItem["IsReview"] = "True";
                                web.AllowUnsafeUpdates = true;
                                lstItem.Update();
                                web.AllowUnsafeUpdates = false;
                            }
                            //WorkflowTriggering("Request");

                            //Context.Response.Write("<script type='text/javascript'>alert('Review Confirmed');window.open('" + url + "','_self'); </script>");
                            Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Review, "H1") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                        }
                    });
                    WorkflowTriggering("Request");
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('APPRAISER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS HR Decisions Review");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }

        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }

        public void updateComments(int appraisalID, int appraisalPhaseId)
        {

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    CommonMaster.BindHistory("Comments by Hr", Convert.ToInt32(hfAppraisalPhaseID.Value), txtCommentsFinal.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "HR");

                    //SPList lstCommentsHistory = objWeb.Lists["Comments History"];
                    //SPListItem lstItem;
                    //lstItem = lstCommentsHistory.AddItem();
                    //lstItem["chCommentFor"] = "Comments by Hr";
                    //lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                    //lstItem["chComment"] = txtCommentsFinal.Text;
                    //lstItem["chCommentedBy"] = this.currentUser;
                    //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    //lstItem["chActor"] = lblEmpNameValue.Text;
                    //lstItem["chRole"] = "HR";

                    //objWeb.AllowUnsafeUpdates = true;
                    //lstItem.Update();
                    //objWeb.AllowUnsafeUpdates = false;
                }
            }

        }

        private void WorkflowTriggering(string status)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {

                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];
                        SPQuery q = new SPQuery();
                        //q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.LoginName + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + lblStatusValue.Text + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
                        //q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H1 - HR Review</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
                        q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + lblStatusValue.Text + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
                        SPListItemCollection taskCollection = lstAppraisalTasks.GetItems(q);
                        if (taskCollection.Count > 0)
                        {
                            SPListItem taskItem = taskCollection[0];
                            Hashtable ht = new Hashtable();
                            ht["tskStatus"] = status;
                            ht["Status"] = status;
                            web.AllowUnsafeUpdates = true;
                            SPWorkflowTask.AlterTask(taskItem, ht, true);
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }
        public void SaveComments(int appraisalID, int appraisalPhaseId)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                    SPListItem lstItem;
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where></Query>";
                    SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                    lstItem = coll[0];

                    lstItem["aphHRReviewLatestComments"] = Convert.ToString(txtCommentsFinal.Text);

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                }
            }
        }
        protected void gvCommentsHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCommentsHistory.PageIndex = e.NewPageIndex;

                gvCommentsHistory.DataSource = ViewState["dtCommentsHistory"];
                gvCommentsHistory.DataBind();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}