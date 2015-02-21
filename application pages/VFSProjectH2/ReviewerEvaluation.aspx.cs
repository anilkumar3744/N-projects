using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Collections;
using Microsoft.SharePoint.Workflow;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFSProjectH2
{
    public partial class ReviewerEvaluation : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, dtPip;
        SPUser currentUser;
        string pipbtn;
        decimal total1;
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    DvREvaluation.Style["display"] = "block";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "none";
                    pipdiv.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    DvREvaluation.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "block";
                    saftymeasurementdevelopment.Style["display"] = "none";
                    pipdiv.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    DvREvaluation.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "block";
                    pipdiv.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 3)
                {
                    DvREvaluation.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "none";
                    pipdiv.Style["display"] = "block";
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
                            if (taskItem["appFinalScore"] != null)
                            {
                                lblFS.Text = Convert.ToString(taskItem["appFinalScore"]);

                            }

                            lblStatusValue.Text = Convert.ToString(taskItem["appAppraisalStatus"]); //
                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(taskItem["appPerformanceCycle"]);
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
                                Response.Redirect(url);
                                Response.End();
                            }



                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery(); //

                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0]; //
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //

                            SPQuery phasesQueryH1 = new SPQuery();
                            phasesQueryH1.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollectionH1 = lstAppraisalPhases.GetItems(phasesQueryH1);  //
                            if (phasesCollectionH1.Count > 0)
                            {
                                SPListItem phaseItemH1 = phasesCollectionH1[0];
                                if (phaseItemH1["aphScore"] != null)
                                    lblH1scoreValue.Text = phaseItemH1["aphScore"].ToString();
                            }


                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotalH2.Text = phaseItem["aphScore"].ToString();
                            }

                            if (Convert.ToString(phaseItem["aphIsAppealed"]) == "Appealed")
                            {
                                lblReValue.Text = "(Appeal)";
                            }
                            else if (Convert.ToString(phaseItem["IsReview"]) == "True")
                            {
                                lblReValue.Text = "(H2-HR Review)";
                            }


                            //if (Convert.ToString(phaseItem["IsReview"]) == "True" && phaseItem["aphIsAppealed"] != null)
                            //{
                            //    lblReValue.Text = "(H2-HR Review)";
                            //}


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
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(taskItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName); //Convert.ToString(appraisalItem["Author"]).Split('#')[1]);
                            //lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";

                            SPList history = currentWeb.Lists["Comments History"];

                            //SPQuery q2 = new SPQuery();
                            //q2.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraiser</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where>"; //
                            //SPListItemCollection col2 = history.GetItems(q2);
                            //if (col2.Count > 0)
                            //{
                            //    SPListItem historyItem2 = col2[0];
                            //    txtComments.Text = Convert.ToString(historyItem2["chComment"]);
                            //}
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

                        //this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value)); //
                        this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.dummyTable == null)
                            this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        this.dummyTable.Columns.Add("SNo", typeof(string));

                        int i = 1;
                        int mandatoryGoalCount = 0;
                        decimal total1 = 0;
                        foreach (DataRow dr in this.dummyTable.Rows)
                        {
                            dr["SNo"] = i;
                            i++;
                            if (dr["IsMandatory"].ToString() == "True")  //
                            {
                                mandatoryGoalCount++;
                            }
                            if (!string.IsNullOrEmpty(Convert.ToString(dr["agScore"])))
                                total1 += Convert.ToDecimal(dr["agScore"]);
                            lblScoretotalH2.Text = total1.ToString();
                        }


                        if (!string.IsNullOrEmpty(lblH1scoreValue.Text))
                        {
                            lblFS.Text = Convert.ToString((Convert.ToDecimal(lblH1scoreValue.Text) + total1) / 2);
                        }
                        else
                            lblFS.Text = total1.ToString();
                        this.hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                        ViewState["Appraisals"] = this.dummyTable;
                        RptRevaluation.DataSource = ViewState["Appraisals"];
                        RptRevaluation.DataBind();
                        BindDateTimeControl("last");


                        if (Convert.ToInt32(this.hfldMandatoryGoalCount.Value) != 0)
                        {
                            MoreGoals(this.dummyTable.Rows.Count);
                            SelectedDropDown(this.dummyTable);
                        }


                        //this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        this.dtCompetencies = CommonMaster.GetCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.dtCompetencies == null)
                            this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        this.dtCompetencies.Columns.Add("SNo", typeof(string));

                        int j = 1;
                        foreach (DataRow dr in this.dtCompetencies.Rows)
                        {
                            dr["SNo"] = j;
                            j++;
                        }


                        rptCompetencies.DataSource = this.dtCompetencies;
                        rptCompetencies.DataBind();
                        EnableResultDropdown(this.dtCompetencies);

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
                    }
                }
                //decimal finalValue = (Convert.ToDecimal(total1) + Convert.ToDecimal(lblH1scoreValue.Text)) / 2;
                //lblfr.Text = CommonMaster.GetRating(finalValue);
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation PageLoad");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }


        }
        private void BindDateTimeControl(string last)
        {
            try
            {
                int i = 0;
                int count = 0;
                if (last != "last")
                {
                    count = RptRevaluation.Items.Count - 1;
                }
                else
                {
                    count = RptRevaluation.Items.Count;
                }
                DataTable dt = ViewState["Appraisals"] as DataTable;
                foreach (RepeaterItem item in RptRevaluation.Items)
                {
                    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                    if (item.ItemIndex < count)
                    {
                        DataRow dr = dt.Rows[i];

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["agDueDate"])))
                        {
                            dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                        }
                        else
                        {
                            dc.SelectedDate = CommonMaster.GetPhaseEndDate("H2");
                        }
                        i++;
                    }
                    else
                    {
                        dc.SelectedDate = CommonMaster.GetPhaseEndDate("H2");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int goulsCount = Convert.ToInt32(hfGoalsCount.Value) + 1;

                hfGoalsCount.Value = goulsCount.ToString();

                this.dummyTable = ViewState["Appraisals"] as DataTable;

                int i = 0;

                foreach (RepeaterItem item in RptRevaluation.Items)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    if ((item.FindControl("txtGoal") as TextBox).Text != string.Empty && (item.FindControl("SPDateLastDate") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtWeightage") as TextBox).Text != string.Empty && (item.FindControl("txtDescription") as TextBox).Text != string.Empty)
                    {
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                        }
                        else
                        {
                            dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                        }

                        dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                        dr["agDueDate"] = strDate;
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        dr["agEvaluation"] = (item.FindControl("txtEvaluation") as TextBox).Text;
                        dr["agScore"] = (item.FindControl("lblScore") as Label).Text;
                        dr["agReviewerLatestComments"] = (item.FindControl("txtComments") as TextBox).Text;
                        i++;
                    }

                    else
                    {
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Please fill all the previous goal details')</script>");
                        //Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script type='text/javascript'>alert('Please fill all the previous goal details')</script>");
                        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + CustomMessages.FillPreviousGoals + "')</script>");
                        return;
                    }
                }

                DataRow dr1 = this.dummyTable.NewRow();

                dr1["SNo"] = this.dummyTable.Rows.Count + 1;

                this.dummyTable.Rows.Add(dr1);

                RptRevaluation.DataSource = this.dummyTable;
                RptRevaluation.DataBind();
                BindDateTimeControl("first");

                ViewState["Appraisals"] = null;
                ViewState["Appraisals"] = this.dummyTable;

                if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    MoreGoals(this.dummyTable.Rows.Count);
                    SelectedDropDown(this.dummyTable);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void MoreGoals(int rowsCount)
        {
            DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
            foreach (RepeaterItem item in RptRevaluation.Items)
            {
                if (item.ItemIndex >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    LinkButton lnkDelete = (LinkButton)item.FindControl("lnkDelete");
                    Label lblCategory = (Label)item.FindControl("lblCategory");

                    ddl.Visible = true;
                    lnkDelete.Visible = true;
                    lblCategory.Visible = false;

                    ddl.DataSource = dtCategories;
                    ddl.DataValueField = "ID";
                    ddl.DataTextField = "ctgrCategory";
                    ddl.DataBind();

                    ddl.Items.Insert(0, "Choose Category");

                }
                else
                {
                    Panel pnl = (Panel)item.FindControl("testpnl");
                    TextBox dummyFld = new TextBox();
                    dummyFld.CssClass = "validation";
                    dummyFld.Style.Add("display", "none");
                    pnl.Controls.Add(dummyFld);
                }
            }
        }

        private void SelectedDropDown(DataTable dt)
        {
            foreach (RepeaterItem item in RptRevaluation.Items)
            {
                if (item.ItemIndex >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    if (dt.Rows[item.ItemIndex]["agGoalCategory"].ToString() != string.Empty)
                    {
                        ddl.Items.FindByText(dt.Rows[item.ItemIndex]["agGoalCategory"].ToString()).Selected = true;
                    }
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SaveToSavedDraft("Appraisal Goals Draft");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies Draft");
                    //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
                    string url = SPContext.Current.Web.Url + "/_layouts/VFSProjectH2/ReviewerEvaluation.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Saved, "H2") + "');window.open('" + url + "','_self'); </script>");
                });
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation Save");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    //int row = txtboxvalidation();
                    //if (row == 0)
                    //{
                    CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");
                    CommonMaster.DeleteCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Competencies Draft");
                    SaveToSavedDraft("Appraisal Goals");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies");
                    //UpadteGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //SavePDP(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));

                    UpdateAppeledFalse(Convert.ToInt32(hfAppraisalPhaseID.Value));

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb web = osite.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;

                            SPList list = web.Lists["Appraisals"];
                            SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));


                            SPList appraisalStatus = web.Lists["Appraisal Status"];
                            // SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];

                            // SPListItem taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));
                            SPListItem lstStatusItem = appraisalStatus.GetItemById(18);

                            SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                            CurrentListItem.Update();

                            web.AllowUnsafeUpdates = false;
                        }

                        Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Approved, "H2") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                    }
                    //}
                    //else
                    //    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Comments cannot be blank at row " + row + "')</script>");
                });
                WorkflowTriggering();
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        private void UpdateAppeledFalse(int phaseId)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        objWeb.AllowUnsafeUpdates = true;
                        SPList phases = objWeb.Lists["Appraisal Phases"];
                        SPListItem item = phases.GetItemById(phaseId);
                        if (Convert.ToString(item["aphIsAppealed"]) == "Appealed")
                        {
                            item["aphIsAppealed"] = "Appealed1";
                            item.Update();
                        }
                        objWeb.AllowUnsafeUpdates = false;

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl);
            Response.End();
        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            //if (!string.IsNullOrEmpty(lnk.CommandArgument))
            //{
            //    int itemID = Convert.ToInt32(lnk.CommandArgument);
            //    CommonMaster.DeleteGoalsItem(itemID);
            //}

            RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
            Label lblSno1 = rptItem.FindControl("lblSno") as Label;
            //this.dummyTable = ViewState["Appraisals"] as DataTable;
            DataTable dt = ViewState["Appraisals"] as DataTable;
            int sNo = Convert.ToInt32(lblSno1.Text);
            int i = 0; decimal total = 0;
            //if (sNo != RptRevaluation.Items.Count)
            //{
            foreach (RepeaterItem item in RptRevaluation.Items)
            {
                DataRow dr = dt.Rows[i];
                dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                Label lblScore1 = item.FindControl("lblScore") as Label;//lblScore
                Label lblSno = item.FindControl("lblSno") as Label;
                if (!string.IsNullOrEmpty(lblScore1.Text) && (i != sNo - 1))
                {
                    total += Convert.ToDecimal(lblScore1.Text);
                }
                if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                }
                else
                {
                    dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                }
                dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                dr["agDueDate"] = strDate;
                if (!string.IsNullOrEmpty((item.FindControl("txtWeightage") as TextBox).Text))
                    dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                if (!string.IsNullOrEmpty((item.FindControl("txtEvaluation") as TextBox).Text))
                    dr["agEvaluation"] = (item.FindControl("txtEvaluation") as TextBox).Text;
                if (!string.IsNullOrEmpty((item.FindControl("lblScore") as Label).Text))
                    dr["agScore"] = (item.FindControl("lblScore") as Label).Text;
                dr["agReviewerLatestComments"] = (item.FindControl("txtComments") as TextBox).Text;
                i++;
                dt.AcceptChanges();
            }
            //}

            dt.Rows.RemoveAt(sNo - 1);
            i = 0;
            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = dt.Rows[i];
                dr["SNo"] = i + 1;
                i++;
            }
            RptRevaluation.DataSource = dt;
            RptRevaluation.DataBind();
            //BindDateTimeControl("last");
            if (dt.Rows.Count != rptItem.ItemIndex + 1)
            {
                BindDateTimeControl("last");
            }
            else
            {
                BindDateTimeControl("first");
            }

            ViewState["Appraisals"] = null;
            ViewState["Appraisals"] = dt;

            if (dt.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
            {
                MoreGoals(dt.Rows.Count);
                SelectedDropDown(dt);
            }
            decimal finalValue;
            if (!string.IsNullOrEmpty(lblH1scoreValue.Text))
            {
                finalValue = (Convert.ToDecimal(total) + Convert.ToDecimal(lblH1scoreValue.Text)) / 2;
            }
            else
            {
                finalValue = Convert.ToDecimal(total);
            }
            lblScoretotalH2.Text = Convert.ToString(total);
            //lblH1scoreValue.Text = Convert.ToString(total);
            lblFS.Text = finalValue.ToString();
            lblfr.Text = CommonMaster.GetRating(finalValue);
        }

        protected void txtWeightage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtaximumAch = sender as TextBox;

                RepeaterItem rptItem = txtaximumAch.NamingContainer as RepeaterItem;

                TextBox txtWeightage = rptItem.FindControl("txtWeightage") as TextBox;
                TextBox txtEvaluation = rptItem.FindControl("txtEvaluation") as TextBox;
                Label lblScore = rptItem.FindControl("lblScore") as Label;
                Label lblCategory = rptItem.FindControl("lblCategory") as Label;

                DropDownList ddlCategories = rptItem.FindControl("ddlCategories") as DropDownList;
                TextBox txtGoal = rptItem.FindControl("txtGoal") as TextBox;
                TextBox txtDescription = rptItem.FindControl("txtDescription") as TextBox;
                TextBox txtComments = rptItem.FindControl("txtComments") as TextBox;
                DateTimeControl SPDateLastDate = rptItem.FindControl("SPDateLastDate") as DateTimeControl;

                decimal weitage = 0, evaluation = 0;
                if (!string.IsNullOrEmpty(txtWeightage.Text))
                    weitage = Convert.ToDecimal(txtWeightage.Text);

                if (!string.IsNullOrEmpty(txtEvaluation.Text))
                    evaluation = Convert.ToDecimal(txtEvaluation.Text);
                decimal weight = ((weitage * evaluation) / 100);
                lblScore.Text = weight.ToString();

                decimal total = 0;
                DataTable dt = ViewState["Appraisals"] as DataTable;

                DataRow dr = dt.Rows[rptItem.ItemIndex];

                dr["agGoal"] = txtGoal.Text;
                dr["agDueDate"] = Convert.ToDateTime(SPDateLastDate.SelectedDate);

                if (rptItem.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                    dr["agGoalCategory"] = ddlCategories.SelectedItem.Text;
                else
                    dr["agGoalCategory"] = lblCategory.Text;

                if (!string.IsNullOrEmpty(lblScore.Text))
                    dr["agScore"] = lblScore.Text;

                if (!string.IsNullOrEmpty(txtEvaluation.Text))
                    dr["agEvaluation"] = Convert.ToDouble(txtEvaluation.Text);

                if (!string.IsNullOrEmpty(txtWeightage.Text))
                    dr["agWeightage"] = txtWeightage.Text;

                dr["agGoalDescription"] = txtDescription.Text;
                dr["agReviewerLatestComments"] = txtComments.Text;

                int i = 0;
                foreach (RepeaterItem item in RptRevaluation.Items)
                {
                    Label lblScore1 = item.FindControl("lblScore") as Label;
                    if (!string.IsNullOrEmpty(lblScore1.Text))
                    {
                        total += Convert.ToDecimal(lblScore1.Text);
                    }

                    DataRow dr2 = dt.Rows[i];
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                    if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                    {
                        dr2["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                    }
                    else
                    {
                        dr2["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                    }

                    dr2["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                    string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                    dr2["agDueDate"] = strDate;
                    dr2["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("txtEvaluation") as TextBox).Text))
                        dr2["agEvaluation"] = Convert.ToDouble((item.FindControl("txtEvaluation") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("lblScore") as Label).Text))
                    {
                        dr2["agScore"] = (item.FindControl("lblScore") as Label).Text;
                    }

                    dr2["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                    dr2["agReviewerLatestComments"] = (item.FindControl("txtComments") as TextBox).Text;
                    i++;


                }

                Panel pnl = (Panel)rptItem.FindControl("testpnl");
                TextBox dummyFld = new TextBox();
                dummyFld.CssClass = "validation";
                dummyFld.Style.Add("display", "none");
                pnl.Controls.Add(dummyFld);


                txtEvaluation.Focus();
                lblScoretotalH2.Visible = true;
                total1 = total;
                lblScoretotalH2.Text = Convert.ToString(total);
                decimal finalValue;
                if (!string.IsNullOrEmpty(lblH1scoreValue.Text))
                {
                    finalValue = (Convert.ToDecimal(total) + Convert.ToDecimal(lblH1scoreValue.Text)) / 2;
                }
                else
                    finalValue = Convert.ToDecimal(total);

                lblFS.Text = finalValue.ToString();
                lblfr.Text = CommonMaster.GetRating(finalValue);
                ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }

        }


        private void EnableResultDropdown(DataTable dt)
        {
            try
            {
                foreach (RepeaterItem item in rptCompetencies.Items)
                {
                    DropDownList ddl = item.FindControl("ddlExpectedResult") as DropDownList;
                    Label lblexectedresult = item.FindControl("lblexectedresult") as Label;
                    lblexectedresult.Visible = false;
                    ddl.Visible = true;
                    ddl.Items.FindByText(dt.Rows[item.ItemIndex]["acmptExpectedResult"].ToString()).Selected = true;

                    DropDownList ddlrating = item.FindControl("ddlRating") as DropDownList;
                    Label lblrating = item.FindControl("lblrating") as Label;
                    lblrating.Visible = false;
                    ddlrating.Visible = true;
                    ddlrating.Items.FindByText(dt.Rows[item.ItemIndex]["acmptRating"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }
        private void WorkflowTriggering()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];
                        SPQuery q = new SPQuery();

                        q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + lblStatusValue.Text + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
                        SPListItemCollection taskCollection = lstAppraisalTasks.GetItems(q);
                        if (taskCollection.Count > 0)
                        {
                            SPListItem taskItem = taskCollection[0];
                            Hashtable ht = new Hashtable();
                            ht["tskStatus"] = "Approved";
                            ht["Status"] = "Approved";
                            web.AllowUnsafeUpdates = true;
                            SPWorkflowTask.AlterTask(taskItem, ht, true);
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }
        private void ScoreFinal(int appraisalID)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstAppraisalPhases = objWeb.Lists["Appraisals"];
                    SPListItem lstItem;
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='ID' /><Value Type='Number'>" + appraisalID + "</Value></Eq></Where>";
                    SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                    lstItem = coll[0];

                    lstItem["appFinalScore"] = Convert.ToString(lblFS.Text);
                    lstItem["appFinalRating"] = Convert.ToString(lblfr.Text);

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                }
            }
        }
        protected void rptGoalSettingsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ritem = e.Item;
            if (ritem.ItemType == ListItemType.AlternatingItem || ritem.ItemType == ListItemType.Item)
            {
                DateTimeControl dc = ritem.FindControl("SPDateLastDate") as DateTimeControl;
                dc.MinDate = DateTime.Now;
            }

        }
        private void SaveToSavedDraft(string listName)
        {

            hfflag.Value = "true";
            CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, listName);

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstGoalSettings = objWeb.Lists[listName];
                    SPListItem lstItem;

                    int mandatoryItemCount = 0;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in RptRevaluation.Items)
                    {
                        Label lblGoal = item.FindControl("lblGoal") as Label;
                        Label lblDueDate = item.FindControl("lblDueDate") as Label;
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
                        Label lblAppraise = item.FindControl("lblAppraise") as Label;
                        Label lblAppraiserComments = item.FindControl("lblAppraiserComments") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        TextBox txtEvaluation = item.FindControl("txtEvaluation") as TextBox;
                        TextBox txtComments = item.FindControl("txtComments") as TextBox;
                        Label lblScore = item.FindControl("lblScore") as Label;
                        Label lblSno = item.FindControl("lblSno") as Label;
                        Label lblID = item.FindControl("lblId") as Label;

                        lstItem = lstGoalSettings.AddItem();

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            if (ddlCategories.SelectedIndex > 0)
                                lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);

                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        if (mandatoryItemCount < Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["IsMandatory"] = true;
                            mandatoryItemCount++;
                        }
                        else
                        {
                            lstItem["IsMandatory"] = false;
                        }

                        lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        //lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                        TextBox tctDate = dc.Controls[0] as TextBox;
                        if (string.IsNullOrEmpty(tctDate.Text))
                        {
                            string date = h1EndDate.ToString("dd-MMM-yyyy");
                            lstItem["agDueDate"] = date;
                        }
                        else
                        {
                            lstItem["agDueDate"] = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        }
                        lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
                        lstItem["agEvaluation"] = Convert.ToString(txtEvaluation.Text);
                        lstItem["agScore"] = Convert.ToString(lblScore.Text);
                        lstItem["agAppraiseeLatestComments"] = Convert.ToString(lblAppraise.Text);
                        lstItem["agAppraiserLatestComments"] = Convert.ToString(lblAppraiserComments.Text);
                        lstItem["agReviewerLatestComments"] = Convert.ToString(txtComments.Text);


                        lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                        lstItem["Author"] = this.currentUser;

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToString(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                        SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                        lstItem = coll[0];

                        lstItem["aphScore"] = Convert.ToString(lblScoretotalH2.Text);

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        CommonMaster.BindHistory("Goal, " + txtGoal.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Reviewer");
                        //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                        //lstItem = lstCommentsHistory.AddItem();
                        //lstItem["chCommentFor"] = "Reviewer Approval";
                        //lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                        //if (!string.IsNullOrEmpty(Convert.ToString(txtComments)))
                        //    lstItem["chComment"] = "Goal, "+txtComments.Text;
                        //lstItem["chCommentedBy"] = this.currentUser;
                        //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                        //lstItem["chActor"] = lblEmpNameValue.Text;
                        //lstItem["chRole"] = "Reviewer";

                        //objWeb.AllowUnsafeUpdates = true;
                        //lstItem.Update();
                        //objWeb.AllowUnsafeUpdates = false;

                    }


                }

            }

        }
        private void SaveCompetenciesDraft(int appraisalID, int appraisalPhaseId, string listName)
        {

            CommonMaster.DeleteCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, listName);
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstGoalSettings = objWeb.Lists[listName];
                    SPListItem lstItem;

                    foreach (RepeaterItem item in rptCompetencies.Items)
                    {
                        Label lblcompetencies = item.FindControl("lblcompetencies") as Label;
                        Label lblexpectedresult = item.FindControl("lblexpectedresult") as Label;
                        Label lblrating = item.FindControl("lblrating") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
                        Label LblComments = item.FindControl("LblComments") as Label;
                        DropDownList ddlExpectedResult = item.FindControl("ddlExpectedResult") as DropDownList;
                        DropDownList ddlRating = item.FindControl("ddlRating") as DropDownList;
                        TextBox txtComments = item.FindControl("txtComments") as TextBox;
                        Label lblItemId = item.FindControl("lblItemId") as Label;
                        Label lblAppraise = item.FindControl("lblAppraise") as Label;
                        Label lblAppraiser = item.FindControl("lblAppraiser") as Label;
                        lstItem = lstGoalSettings.AddItem();
                        lstItem["acmptAppraisalId"] = appraisalID;
                        lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;           //Convert.ToInt32(lstItemAppraisal["ID"]);
                        lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                        lstItem["acmptExpectedResult"] = Convert.ToString(ddlExpectedResult.SelectedItem.Text);
                        lstItem["acmptRating"] = Convert.ToString(ddlRating.SelectedItem.Text);
                        lstItem["acmptDescription"] = lblDescriptionValue.Text;
                        lstItem["acmptAppraiseeLatestComments"] = Convert.ToString(lblAppraise.Text);
                        lstItem["acmptAppraiserLatestComments"] = Convert.ToString(lblAppraiser.Text);
                        //if (!string.IsNullOrEmpty(txtComments.Text))
                        lstItem["acmptReviewerLatestComments"] = Convert.ToString(txtComments.Text);
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Competency, " + lblcompetencies.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Reviewer");
                    }

                }
            }

        }
        private void BingExistingGoals()
        {
            this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
            if (this.dummyTable.Rows.Count < 0)
                this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

            this.dummyTable.Columns.Add("SNo", typeof(string));

            int i = 1;
            int mandatoryGoalCount = 0;
            foreach (DataRow dr in this.dummyTable.Rows)
            {
                dr["SNo"] = i;
                i++;
                if (dr["IsMandatory"].ToString() == "True")
                {
                    mandatoryGoalCount++;
                }
            }
            hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
            ViewState["dummyTable"] = null;
            ViewState["dummyTable"] = this.dummyTable;
            RptRevaluation.DataSource = ViewState["dummyTable"];
            RptRevaluation.DataBind();
            BindDateTimeControl("last");

            if (Convert.ToInt32(hfldMandatoryGoalCount.Value) != 0)
            {
                MoreGoals(this.dummyTable.Rows.Count);
                SelectedDropDown(this.dummyTable);
            }
        }
        protected void ddlrating_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlRating = (DropDownList)sender;
            RepeaterItem rItem = (RepeaterItem)ddlRating.NamingContainer;
            TextBox txtComments = rItem.FindControl("txtComments") as TextBox;
            txtComments.Text = string.Empty;
            //foreach (RepeaterItem item in rptCompetencies.Items)
            //{
            //    TextBox txtComments = item.FindControl("txtComments") as TextBox;

            //    txtComments.Text = "";
            //}

        }

        private int txtboxvalidation()
        {
            int txtVal = 0;
            int count = 0;
            foreach (RepeaterItem item in rptCompetencies.Items)
            {
                count++;
                TextBox txtComments = item.FindControl("txtComments") as TextBox;
                if (txtComments.Text == string.Empty || txtComments.Text == null)
                {
                    txtVal = count;
                    break;
                }
            }
            return txtVal;
        }
    }
}
