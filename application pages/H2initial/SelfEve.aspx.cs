using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;
using System.Collections;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.H2initial
{
    public partial class SelfEve : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies;
        SPUser currentUser;
        DataTable dt;
        
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    dvGoalSettings.Style["display"] = "block";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "none";

                    //dvGoalSettings.Visible = true;
                    //dvCompetencies.Visible = false;
                    //saftymeasurementdevelopment.Visible = false;

                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "block";
                    saftymeasurementdevelopment.Style["display"] = "none";

                    //dvGoalSettings.Visible = false;
                    //dvCompetencies.Visible = true;
                    //saftymeasurementdevelopment.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "block";

                    //dvGoalSettings.Visible = false;
                    //dvCompetencies.Visible = false;
                    //saftymeasurementdevelopment.Visible = true;
                }
                if (!this.IsPostBack)
                {
                    //SPList lstAppraisalTasks;  //
                    //SPListItem taskItem;       // 
                    SPUser appraisee;

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);

                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];
                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
                            if (appraisalItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(appraisalItem["appFinalRating"]);
                            }

                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];
                            SPQuery phasesQueryH1 = new SPQuery();
                            phasesQueryH1.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollectionH1 = lstAppraisalPhases.GetItems(phasesQueryH1);  //
                            if (phasesCollectionH1.Count > 0)
                            {
                                SPListItem phaseItemH1 = phasesCollectionH1[0];

                                if (appraisalItem["appFinalScore"] != null)
                                {
                                    lblFS.Text = Convert.ToString(appraisalItem["appFinalScore"]);
                                }
                                else if (phaseItemH1["aphScore"] != null)
                                {
                                    lblFS.Text = phaseItemH1["aphScore"].ToString();
                                }

                                if (phaseItemH1["aphScore"] != null)
                                {
                                    lblH1scoreValue.Text = phaseItemH1["aphScore"].ToString();
                                    //GetRating(Convert.ToInt32(hfAppraisalID.Value));
                                }
                            }
                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);
                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                return;
                            }
                            if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), currentWeb))
                            {
                                string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                Response.Redirect(url, false);
                                //Response.End();
                            }

                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0];
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);
                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotalH2.Text = phaseItem["aphScore"].ToString();
                                if (!string.IsNullOrEmpty(lblFS.Text))
                                    lblfr.Text = CommonMaster.GetRating(Convert.ToDecimal(lblFS.Text));
                            }
                            else
                            {
                                lblScoretotalH2.Text = "0";
                            }



                            //if (appraisalItem["appFinalScore"] != null)
                            //{
                            //    lblFS.Text = Convert.ToString(appraisalItem["appFinalScore"]);
                            //}
                            //else
                            //{
                            //    lblFS.Text = Convert.ToString(phaseItemH1["aphScore"]);
                            //}
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName);

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
                        //DataTable dt = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        DataTable dt = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);

                        if (dt == null)
                            dt = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dt != null)//upendra added
                        {
                            dt.Columns.Add("SNo", typeof(string));

                            int i = 1;
                            int mandatoryGoalCount = 0;
                            decimal total = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                                dr["SNo"] = i;
                                i++;
                                if (dr["IsMandatory"].ToString() == "True")  //
                                {
                                    mandatoryGoalCount++;//
                                }//

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["agScore"])))
                                    total += Convert.ToDecimal(dr["agScore"]);
                            }
                            this.hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                        }
                        ViewState["Appraisals"] = dt;
                        rptGoalSettings.DataSource = dt;
                        rptGoalSettings.DataBind();
                        BindDateTimeControl("last");

                        if (!string.IsNullOrEmpty(this.hfldMandatoryGoalCount.Value) && Convert.ToInt32(this.hfldMandatoryGoalCount.Value) != 0)
                        {
                            MoreGoals(dt.Rows.Count);
                            SelectedDropDown(dt);
                        }


                        this.dtCompetencies = CommonMaster.GetCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);

                        if (this.dtCompetencies == null)
                            this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dtCompetencies != null)//upendra added
                        {
                            this.dtCompetencies.Columns.Add("SNo", typeof(string));
                            int k = 1;
                            foreach (DataRow dr in this.dtCompetencies.Rows)
                            {
                                dr["SNo"] = k;
                                k++;
                            }
                        }
                        rptCompetencies.DataSource = this.dtCompetencies;
                        rptCompetencies.DataBind();

                        EnableResultDropdown(this.dtCompetencies);

                        DataTable dtPDP1;
                        dtPDP1 = CommonMaster.GetAppraisalDevelopmentMesureDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (dtPDP1 == null)
                            dtPDP1 = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        if (dtPDP1 != null)
                        {
                            hfHasPDP.Value = "true";
                            dtPDP1.Columns.Add("SNo", typeof(string));
                            int j = 1;
                            foreach (DataRow dr in dtPDP1.Rows)
                            {
                                dr["SNo"] = j;
                                j++;
                            }

                            ViewState["PDP"] = dtPDP1;
                        }
                        else
                        {
                            dtPDP1 = developmentmesures(1);
                            ViewState["PDP"] = dtPDP1;
                        }
                        rptsaftymeasurementdevelopment.DataSource = ViewState["PDP"];
                        rptsaftymeasurementdevelopment.DataBind();
                        if (dtPDP1 != null)
                        {
                            BindDateTimeControlToPIPrpt(true);
                        }
                        if (dtPDP1.Rows.Count >= 1)
                        {
                            MorePDP(dtPDP1.Rows.Count);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }


        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int goalsCount = Convert.ToInt32(hfGoalsCount.Value) + 1;

                hfGoalsCount.Value = goalsCount.ToString();

                this.dummyTable = ViewState["Appraisals"] as DataTable;

                int i = 0;

                foreach (RepeaterItem item in rptGoalSettings.Items)
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
                        string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        dr["agDueDate"] = strDate;
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);

                        if (!string.IsNullOrEmpty((item.FindControl("txtEvaluation") as TextBox).Text))
                            dr["agEvaluation"] = Convert.ToDouble((item.FindControl("txtEvaluation") as TextBox).Text);

                        if (!string.IsNullOrEmpty((item.FindControl("lblScore1") as Label).Text))
                        {
                            dr["agScore"] = (item.FindControl("lblScore1") as Label).Text;
                        }
                        dr["agAppraiseeLatestComments"] = (item.FindControl("txtComments") as TextBox).Text;
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        i++;
                    }

                    else
                    {
                        //string url = SPContext.Current.Web.Url + "/_layouts/H2initial/SelfEve.aspx?AppId=" + hfAppraisalID.Value;
                        //Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + string.Format(CustomMessages.FillPreviousGoals, "H1") + "'); </script>");
                        Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script type='text/javascript'>alert('" + CustomMessages.FillPreviousGoals + "')</script>");
                        //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Fill first goals')</script>");
                        return;
                    }
                }

                DataRow dr1 = this.dummyTable.NewRow();

                dr1["SNo"] = this.dummyTable.Rows.Count + 1;

                this.dummyTable.Rows.Add(dr1);

                rptGoalSettings.DataSource = this.dummyTable;
                rptGoalSettings.DataBind();
                BindDateTimeControl("First");

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
                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        private void SelectedDropDown(DataTable dataTable)
        {
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    if (dataTable.Rows[item.ItemIndex]["agGoalCategory"].ToString() != string.Empty)
                    {
                        ddl.Items.FindByText(dataTable.Rows[item.ItemIndex]["agGoalCategory"].ToString()).Selected = true;
                    }
                }
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
                    count = rptGoalSettings.Items.Count - 1;
                }
                else
                {
                    count = rptGoalSettings.Items.Count;
                }
                DataTable dt = ViewState["Appraisals"] as DataTable;
                foreach (RepeaterItem item in rptGoalSettings.Items)
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
                throw ex;
            }
        }

        private void MoreGoals(int rowsCount)
        {
            // DataTable dtCategories = CommonMaster.GetCategories();
            DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    hfSaveFlag.Value = "true";

                    SaveToSavedDraft("Appraisal Goals Draft");
                    SavePDPDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Development Measures Draft");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies Draft");


                    //SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //SavePDP(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //UpdateGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    ////GetRating(Convert.ToInt32(hfAppraisalID.Value));
                    //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));

                    //DataTable dtPDP = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //dtPDP.Columns.Add("SNo", typeof(string));
                    //int j = 1;
                    //foreach (DataRow dr in dtPDP.Rows)
                    //{
                    //    dr["SNo"] = j;
                    //    j++;
                    //}

                    //ViewState["PDP"] = dtPDP;
                    //rptsaftymeasurementdevelopment.DataSource = ViewState["PDP"];
                    //rptsaftymeasurementdevelopment.DataBind();
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Saved successfully')</script>");
                    string url = SPContext.Current.Web.Url + "/_layouts/H2initial/SelfEve.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + string.Format(CustomMessages.Saved, "H1") + "'); </script>");
                });

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

                //LogHandler.LogError(ex, "Error in PMS Self Evaluation");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }

        private void SaveToSavedDraft(string listName)
        {

            hfflag.Value = "true";
            //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
            CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, listName);
            ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPListItem lstItem;
                    SPList lstGoalSettings = objWeb.Lists[listName];

                    int mandatoryItemCount = 0;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        //Label lblGoal = item.FindControl("lblGoalID") as Label;//Upendra changed the label Id and duplicate
                        Label lblDueDate = item.FindControl("lblDueDatei") as Label;//Upendra changed the label Id
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        TextBox txtComments = item.FindControl("txtComments") as TextBox;
                        TextBox txtEvaluation = item.FindControl("txtEvaluation") as TextBox;
                        Label lblScore = item.FindControl("lblScore1") as Label;
                        Label lblSno = item.FindControl("lblSno") as Label;
                        Label lblID = item.FindControl("lblGoalID") as Label;


                        lstItem = lstGoalSettings.AddItem();

                        lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);


                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        if (mandatoryItemCount < Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                        {
                            lstItem["IsMandatory"] = true;
                            mandatoryItemCount++;
                        }
                        else
                        {
                            lstItem["IsMandatory"] = false;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGoal.Text)))
                            lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
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
                        //lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy")).Date;
                        if (!string.IsNullOrEmpty(Convert.ToString(txtWeightage.Text)))
                            lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtEvaluation.Text)))
                            lstItem["agEvaluation"] = Convert.ToString(txtEvaluation.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(lblScore.Text)))
                            lstItem["agScore"] = Convert.ToDecimal(lblScore.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtComments.Text)))
                            lstItem["agAppraiseeLatestComments"] = Convert.ToString(txtComments.Text);
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Goal, " + txtGoal.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");
                    }




                    //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    //lstItem = lstCommentsHistory.AddItem();
                    //lstItem["chCommentFor"] = "Self Evaluation ";
                    //lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                    ////lstItem["chComment"] = txtComments.Text;
                    //lstItem["chCommentedBy"] = this.currentUser;
                    //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    //lstItem["chActor"] = lblEmpNameValue.Text;
                    //lstItem["chRole"] = "Appraisee";

                    //objWeb.AllowUnsafeUpdates = true;
                    //lstItem.Update();
                    //objWeb.AllowUnsafeUpdates = false;

                    SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];

                    lstItem = lstAppraisalPhases.GetItemById(Convert.ToInt32(hfAppraisalPhaseID.Value));
                    if (!string.IsNullOrEmpty(Convert.ToString(lblScoretotalH2.Text)))
                        lstItem["aphScore"] = Convert.ToDecimal(lblScoretotalH2.Text);

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
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
                        Label lblexpectedresult = item.FindControl("lblexectedresult") as Label;
                        Label lblrating = item.FindControl("lblrating") as Label;
                        Label LblComments = item.FindControl("LblCommentscomp") as Label; //Upendra label change
                        DropDownList ddlRating = item.FindControl("ddlRating") as DropDownList;
                        TextBox TxtAppraiseecmts = item.FindControl("TxtAppraiseecmts") as TextBox;
                        Label lblItemId = item.FindControl("lblItemId") as Label;
                        Label lblDescriptionValue1 = item.FindControl("lblDescriptionValue1") as Label;



                        lstItem = lstGoalSettings.AddItem();

                        lstItem["acmptAppraisalId"] = appraisalID;


                        lstItem["acmptAppraisalId"] = appraisalID;
                        lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;
                        lstItem["acmptDescription"] = Convert.ToString(lblDescriptionValue1.Text);
                        lstItem["acmptExpectedResult"] = lblexpectedresult.Text;
                        if (!string.IsNullOrEmpty(lblcompetencies.Text.ToString()))
                            lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                        if (ddlRating.SelectedIndex > 0)
                            lstItem["acmptRating"] = Convert.ToString(ddlRating.SelectedItem.Text);
                        if (!string.IsNullOrEmpty(LblComments.Text.ToString()))
                            lstItem["acmptAppraiseeLatestComments"] = TxtAppraiseecmts.Text;
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Competency, " + lblcompetencies.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), TxtAppraiseecmts.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");

                    }
                }
            }

        }

        private void SavePDPDraft(int appraisalID, int appraisalPhaseId, string listName)
        {

            CommonMaster.DeletePDPDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, listName);
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb oWeb = osite.OpenWeb())
                {
                    SPList oList = oWeb.Lists[listName];  //"Appraisal Development Measures"

                    SPListItem CurrentListItem;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in rptsaftymeasurementdevelopment.Items)
                    {
                        DateTimeControl shpdatecontrol = item.FindControl("shpdatecontrol") as DateTimeControl;
                        TextBox txtwhat = item.FindControl("txtwhat") as TextBox;
                        TextBox nextstep = item.FindControl("nextstep") as TextBox;
                        TextBox txtCommentspdp = item.FindControl("txtCommentspdp") as TextBox;
                        Label lblPDPID = item.FindControl("lblPDPID") as Label;

                        CurrentListItem = oList.AddItem();

                        // oListItem = oList.AddItem();
                        CurrentListItem["pdpAppraisalID"] = appraisalID;
                        CurrentListItem["pdpAppraisalPhaseID"] = appraisalPhaseId;
                        //oListItem["pdpWhen"] = shpdatecontrol.SelectedDate.ToString("dd-MMM-yyyy");
                        TextBox tctDate = shpdatecontrol.Controls[0] as TextBox;
                        if (string.IsNullOrEmpty(tctDate.Text))
                        {

                            string date = h1EndDate.ToString("dd-MMM-yyyy");
                            CurrentListItem["pdpWhen"] = date;
                        }
                        else
                        {
                            CurrentListItem["pdpWhen"] = shpdatecontrol.SelectedDate.ToString("dd-MMM-yyyy");
                        }
                        //CurrentListItem["pdpWhen"] = shpdatecontrol.SelectedDate.ToString("dd-MMM-yyyy");
                        if (!string.IsNullOrEmpty(txtwhat.Text))
                            CurrentListItem["pdpWhat"] = Convert.ToString(txtwhat.Text);
                        if (!string.IsNullOrEmpty(nextstep.Text))
                            CurrentListItem["pdpNextSteps"] = Convert.ToString(nextstep.Text);
                        if (!string.IsNullOrEmpty(txtCommentspdp.Text))
                            CurrentListItem["pdpH2AppraiseeComments"] = Convert.ToString(txtCommentspdp.Text);
                        CurrentListItem["Author"] = this.currentUser;
                        oWeb.AllowUnsafeUpdates = true;
                        CurrentListItem.Update();
                        oWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("PDP, " + txtwhat.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), txtCommentspdp.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");
                    }

                }
            }

        }

        private void SaveCompetencies(int appraisalID, int appraisalPhaseId)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = osite.OpenWeb())
                    {
                        oWeb.AllowUnsafeUpdates = true;

                        SPList oList = oWeb.Lists["Appraisal Competencies"];
                        SPListItem oListItem;

                        foreach (RepeaterItem item in rptCompetencies.Items)
                        {
                            Label lblcompetencies = item.FindControl("lblcompetencies") as Label;
                            Label lblexpectedresult = item.FindControl("lblexectedresult") as Label;
                            Label lblrating = item.FindControl("lblrating") as Label;
                            Label LblComments = item.FindControl("LblComments") as Label;
                            DropDownList ddlRating = item.FindControl("ddlRating") as DropDownList;
                            TextBox TxtAppraiseecmts = item.FindControl("TxtAppraiseecmts") as TextBox;
                            Label lblItemId = item.FindControl("lblItemId") as Label;

                            if (string.IsNullOrEmpty(lblItemId.Text))
                            {
                                oListItem = oList.AddItem();

                                oListItem["acmptAppraisalId"] = appraisalID;

                            }
                            else
                            {
                                oListItem = oList.GetItemById(Convert.ToInt32(lblItemId.Text));
                            }

                            oListItem["acmptAppraisalId"] = appraisalID;
                            oListItem["acmptAppraisalPhaseId"] = appraisalPhaseId;
                            if (!string.IsNullOrEmpty(lblcompetencies.Text.ToString()))
                                oListItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                            if (ddlRating.SelectedIndex > 0)
                                oListItem["acmptRating"] = Convert.ToString(ddlRating.SelectedItem.Text);
                            if (!string.IsNullOrEmpty(LblComments.Text.ToString()))
                                oListItem["acmptAppraiseeLatestComments"] = TxtAppraiseecmts.Text;

                            oWeb.AllowUnsafeUpdates = true;
                            oListItem.Update();
                            oWeb.AllowUnsafeUpdates = false;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(appLog.Value))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        //if (!Convert.ToBoolean(hfSaveFlag.Value))
                        //{
                        //    SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        //    SavePDP(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        //    UpdateGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        //    //GetRating(Convert.ToInt32(hfAppraisalID.Value));
                        //    ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
                        //}
                        ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
                        SaveToSavedDraft("Appraisal Goals");
                        SavePDPDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Development Measures");
                        SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies");

                        CommonMaster.DeleteCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Competencies Draft");
                        CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");
                        CommonMaster.DeletePDPDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Development Measures Draft");

                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;

                                SPList list = web.Lists["Appraisals"];
                                SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                SPList appraisalStatus = web.Lists["Appraisal Status"];
                                SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];

                                //SPListItem taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));
                                SPListItem lstStatusItem = appraisalStatus.GetItemById(16);

                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                CurrentListItem.Update();
                                web.AllowUnsafeUpdates = false;

                                //Hashtable ht = new Hashtable();
                                //ht["tskStatus"] = "Approved";       //Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                //ht["Status"] = "Approved";
                                //SPWorkflowTask.AlterTask(taskItem, ht, true);
                                //string url = SPContext.Current.Site.Url + "/_layouts/VFS_DashBoards/Dashboard.aspx";
                                //Context.Response.Write("<script type='text/javascript'>alert('Goals/Competencies submitted successfully.');window.open('" + url + "','_self'); </script>");
                            }
                            //Response.Redirect(SPContext.Current.Site.Url);
                        }
                    });
                    WorkflowTriggering();

                    Context.Response.Write("<script type='text/javascript'>alert('Submitted successfully.');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('APPRAISER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }
            catch (Exception ex)
            {


                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
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
                        //q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.LoginName + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + lblStatusValue.Text + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
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

        public void UpdateGoals(int appraisalID, int appraisalPhaseId)
        {

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb oWeb = osite.OpenWeb())
                {

                    SPList oGoalSettings = oWeb.Lists["Appraisal Goals"];
                    SPListItem lstItem;

                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblGoal = item.FindControl("lblGoal") as Label;
                        Label lblDueDate = item.FindControl("lblDueDate") as Label;
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        TextBox txtComments = item.FindControl("txtComments") as TextBox;
                        TextBox txtEvaluation = item.FindControl("txtEvaluation") as TextBox;
                        Label lblScore = item.FindControl("lblScore1") as Label;
                        Label lblSno = item.FindControl("lblSno") as Label;
                        Label lblID = item.FindControl("lblGoalID") as Label;

                        if (string.IsNullOrEmpty(lblID.Text))
                        {
                            lstItem = oGoalSettings.AddItem();

                            lstItem["agAppraisalId"] = appraisalID;
                            lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
                        }
                        else
                        {
                            lstItem = oGoalSettings.GetItemById(Convert.ToInt32(lblID.Text));
                        }

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGoal.Text)))
                            lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy")).Date;
                        if (!string.IsNullOrEmpty(Convert.ToString(txtWeightage.Text)))
                            lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtEvaluation.Text)))
                            lstItem["agEvaluation"] = Convert.ToString(txtEvaluation.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(lblScore.Text)))
                            lstItem["agScore"] = Convert.ToDecimal(lblScore.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtComments.Text)))
                            lstItem["agAppraiseeLatestComments"] = Convert.ToString(txtComments.Text);

                        oWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        oWeb.AllowUnsafeUpdates = false;

                    }



                    SPList lstCommentsHistory = oWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Self Evaluation ";
                    lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                    //lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    lstItem["chRole"] = "Appraisee";

                    oWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    oWeb.AllowUnsafeUpdates = false;

                    SPList lstAppraisalPhases = oWeb.Lists["Appraisal Phases"];
                    //SPQuery q = new SPQuery();
                    //q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                    //SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                    lstItem = lstAppraisalPhases.GetItemById(appraisalPhaseId);
                    if (!string.IsNullOrEmpty(Convert.ToString(lblScoretotalH2.Text)))
                        lstItem["aphScore"] = Convert.ToDecimal(lblScoretotalH2.Text);

                    oWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    oWeb.AllowUnsafeUpdates = false;



                }
                string url = SPContext.Current.Web.Url + "/_layouts/H2initial/SelfEve.aspx?AppId=" + hfAppraisalID.Value;
                Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Submitted, "H1") + "');window.open('" + url + "','_self'); </script>");

                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Gaols has been added successfully')</script>");
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            //Context.Response.Flush();
            //Context.Response.End();

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = sender as LinkButton;
                //if (!string.IsNullOrEmpty(lnk.CommandArgument))
                //{
                //    int itemID = Convert.ToInt32(lnk.CommandArgument);
                //    CommonMaster.DeleteGoalsItem(itemID);
                //}

                RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
                Label lblSno = rptItem.FindControl("lblSno") as Label;
                this.dummyTable = ViewState["Appraisals"] as DataTable;
                int sNo = Convert.ToInt32(lblSno.Text);
                int i = 0; decimal total = 0;

                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                    Label lblScore1 = item.FindControl("lblScore1") as Label;//lblScore
                    if (!string.IsNullOrEmpty(lblScore1.Text) && (i != Convert.ToInt32(lblSno.Text) - 1))
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
                    string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                    dr["agDueDate"] = strDate;

                    if (!string.IsNullOrEmpty(Convert.ToString((item.FindControl("txtWeightage") as TextBox).Text)))
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("txtEvaluation") as TextBox).Text))
                        dr["agEvaluation"] = Convert.ToDouble((item.FindControl("txtEvaluation") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("lblScore1") as Label).Text))
                    {
                        dr["agScore"] = (item.FindControl("lblScore1") as Label).Text;
                    }
                    dr["agAppraiseeLatestComments"] = (item.FindControl("txtComments") as TextBox).Text;
                    dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                    i++;
                    this.dummyTable.AcceptChanges();
                }
                this.dummyTable.Rows.RemoveAt(Convert.ToInt32(lblSno.Text) - 1);

                i = 0;
                foreach (DataRow row in this.dummyTable.Rows)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    dr["SNo"] = i + 1;
                    i++;
                }

                rptGoalSettings.DataSource = this.dummyTable;
                rptGoalSettings.DataBind();

                if (this.dummyTable.Rows.Count != rptItem.ItemIndex + 1)
                {
                    BindDateTimeControl("last");
                }
                else
                {
                    BindDateTimeControl("first");
                }

                //BindDateTimeControl("last");

                ViewState["Appraisals"] = null;
                ViewState["Appraisals"] = this.dummyTable;

                if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    MoreGoals(this.dummyTable.Rows.Count);
                    SelectedDropDown(this.dummyTable);
                }
                lblScoretotalH2.Text = Convert.ToString(total);
                lblfr.Text = CommonMaster.GetRating(total);
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }
        private DataTable developmentmesures(int count)
        {
            DataTable testTable = new DataTable();

            testTable.Columns.Add("SNo", typeof(string));
            testTable.Columns.Add("ID", typeof(string));
            testTable.Columns.Add("pdpWhen", typeof(string));
            testTable.Columns.Add("pdpWhat", typeof(string));
            testTable.Columns.Add("pdpNextSteps", typeof(string));
            testTable.Columns.Add("pdpH2AppraiseeComments", typeof(string));

            DataRow dr;

            for (int i = 0; i < count; i++)
            {
                dr = testTable.NewRow();
                dr["SNo"] = i + 1;
                testTable.Rows.Add(dr);

            }

            return testTable;
        }

        protected void btnAddPDP_Click(object sender, EventArgs e)
        {
            try
            {

                int pdpCount = Convert.ToInt32(hfsaftymeasurementdevelopment.Value) + 1;
                hfsaftymeasurementdevelopment.Value = pdpCount.ToString();

                this.dummyTable = ViewState["PDP"] as DataTable;

                int i = 0;
                foreach (RepeaterItem item in rptsaftymeasurementdevelopment.Items)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    if ((item.FindControl("shpdatecontrol") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtwhat") as TextBox).Text != string.Empty && (item.FindControl("nextstep") as TextBox).Text != string.Empty && (item.FindControl("txtCommentspdp") as TextBox).Text != string.Empty)
                    {
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        dr["pdpWhen"] = (item.FindControl("shpdatecontrol") as DateTimeControl).SelectedDate.ToString("dd-MMM-yyyy"); //(item.FindControl("shpdatecontrol") as Label).Text;
                        dr["pdpWhat"] = (item.FindControl("txtwhat") as TextBox).Text;
                        dr["pdpNextSteps"] = (item.FindControl("nextstep") as TextBox).Text;
                        dr["pdpH2AppraiseeComments"] = (item.FindControl("txtCommentspdp") as TextBox).Text;
                        i++;

                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + CustomMessages.FillPreviousDevelopmentMeasures + "')</script>");
                        return;
                    }
                }


                DataRow dr1 = this.dummyTable.NewRow();

                dr1["SNo"] = this.dummyTable.Rows.Count + 1;
                this.dummyTable.Rows.Add(dr1);
                ViewState["PDP"] = null;
                ViewState["PDP"] = this.dummyTable;

                rptsaftymeasurementdevelopment.DataSource = this.dummyTable;
                rptsaftymeasurementdevelopment.DataBind();
                dt = ViewState["PDP"] as DataTable;
                if (dt.Rows.Count >= 1)
                {
                    MorePDP(dt.Rows.Count);

                }
                BindDateTimeControlToPIPrpt(false);

            }
            catch (Exception ex)
            {


                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        private void BindDateTimeControlToPIPrpt(bool flag)
        {
            try
            {
                int i = 0, count = 0;
                if (!flag)
                {
                    count = rptsaftymeasurementdevelopment.Items.Count - 1;
                }
                else
                {
                    count = rptsaftymeasurementdevelopment.Items.Count;
                }
                DataTable dt = ViewState["PDP"] as DataTable;
                foreach (RepeaterItem item in rptsaftymeasurementdevelopment.Items)
                {
                    DateTimeControl dc = item.FindControl("shpdatecontrol") as DateTimeControl;
                    if (item.ItemIndex < count)
                    {
                        DataRow dr = dt.Rows[i];

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["pdpWhen"])))
                        {
                            dc.SelectedDate = Convert.ToDateTime(dr["pdpWhen"]);
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

            }
        }

        private void SavePDP(int appraisalID, int appraisalPhaseId)
        {

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb oWeb = osite.OpenWeb())
                {

                    SPList oList = oWeb.Lists["Appraisal Development Measures"];
                    SPListItem oListItem;
                    foreach (RepeaterItem item in rptsaftymeasurementdevelopment.Items)
                    {
                        DateTimeControl shpdatecontrol = item.FindControl("shpdatecontrol") as DateTimeControl;
                        TextBox txtwhat = item.FindControl("txtwhat") as TextBox;
                        TextBox nextstep = item.FindControl("nextstep") as TextBox;
                        TextBox txtCommentspdp = item.FindControl("txtCommentspdp") as TextBox;
                        Label lblPDPID = item.FindControl("lblPDPID") as Label;
                        if (string.IsNullOrEmpty(lblPDPID.Text))
                        {
                            oListItem = oList.AddItem();
                            //oListItem["agAppraisalId"] = appraisalID;
                        }
                        else
                        {
                            oListItem = oList.GetItemById(Convert.ToInt32(lblPDPID.Text));
                        }
                        // oListItem = oList.AddItem();
                        oListItem["pdpAppraisalID"] = appraisalID;
                        oListItem["pdpAppraisalPhaseID"] = appraisalPhaseId;
                        //oListItem["pdpWhen"] = shpdatecontrol.SelectedDate.ToString("dd-MMM-yyyy");
                        oListItem["pdpWhen"] = shpdatecontrol.SelectedDate.ToString("dd-MMM-yyyy");
                        if (!string.IsNullOrEmpty(txtwhat.Text))
                            oListItem["pdpWhat"] = Convert.ToString(txtwhat.Text);
                        if (!string.IsNullOrEmpty(nextstep.Text))
                            oListItem["pdpNextSteps"] = Convert.ToString(nextstep.Text);
                        if (!string.IsNullOrEmpty(txtCommentspdp.Text))
                            oListItem["pdpH2AppraiseeComments"] = Convert.ToString(txtCommentspdp.Text);
                        oWeb.AllowUnsafeUpdates = true;
                        oListItem.Update();
                        oWeb.AllowUnsafeUpdates = false;
                    }
                }
            }

        }

        protected void lnkPDPDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;

                RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
                Label lblSno = rptItem.FindControl("lblSno") as Label;
                dt = ViewState["PDP"] as DataTable;
                int sNo = Convert.ToInt32(lblSno.Text);
                int i = 0;
                if (sNo != rptGoalSettings.Items.Count)
                {
                    foreach (RepeaterItem item in rptsaftymeasurementdevelopment.Items)
                    {
                        DataRow dr = dt.Rows[i];
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        dr["pdpWhen"] = (item.FindControl("shpdatecontrol") as DateTimeControl).SelectedDate.ToString("dd-MMM-yyyy"); //(item.FindControl("shpdatecontrol") as Label).Text;
                        dr["pdpWhat"] = (item.FindControl("txtwhat") as TextBox).Text;
                        dr["pdpNextSteps"] = (item.FindControl("nextstep") as TextBox).Text;
                        dr["pdpH2AppraiseeComments"] = (item.FindControl("txtCommentspdp") as TextBox).Text;
                        i++;
                        dt.AcceptChanges();
                    }
                }
                dt.Rows.RemoveAt(Convert.ToInt32(lblSno.Text) - 1);
                i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dr = dt.Rows[i];
                    dr["SNo"] = i + 1;
                    i++;
                }

                rptsaftymeasurementdevelopment.DataSource = dt;
                rptsaftymeasurementdevelopment.DataBind();
                BindDateTimeControlToPIPrpt(true);

                ViewState["PDP"] = null;
                ViewState["PDP"] = dt;

                if (dt.Rows.Count >= 1)
                {
                    MorePDP(dt.Rows.Count);

                }
            }
            catch (Exception ex)
            {


                LogHandler.LogError(ex, "Error in PMS H2 Self Evaluation");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        private void MorePDP(int rowsCount)
        {
            foreach (RepeaterItem item in rptsaftymeasurementdevelopment.Items)
            {
                if (item.ItemIndex >= 1)
                {

                    LinkButton lnkPDPDelete = (LinkButton)item.FindControl("lnkPDPDelete");
                    lnkPDPDelete.Visible = true;

                }
            }
        }

        private void BindData(DataTable dt)
        {
            int i = 0;
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                DataRow dr = dt.Rows[i];
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
                string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                dr["agDueDate"] = strDate;
                dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);

                if (!string.IsNullOrEmpty((item.FindControl("TxtEvaluation") as TextBox).Text))
                    dr["agEvaluation"] = Convert.ToDouble((item.FindControl("TxtEvaluation") as TextBox).Text);

                if (!string.IsNullOrEmpty((item.FindControl("lblScore") as Label).Text))
                {
                    dr["agScore"] = (item.FindControl("lblScore") as Label).Text;
                }

                dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                i++;

            }

            rptGoalSettings.DataSource = dt;
            rptGoalSettings.DataBind();
            BindDateTimeControl("last");
            if (dt.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
            {
                MoreGoals(dt.Rows.Count);
                SelectedDropDown(dt);
            }
        }

        //protected void ViewH1_Click(object sender, EventArgs e)
        //{

        //    Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?AppId=" + Convert.ToString(Request.Params["AppId"])));
        //    //Response.Redirect((SPContext.Current.Site.Url + "/_layouts/VFS_ApplicationPages/AppraiseeSavedDraft.aspx?TaskID=" + taskId + "&AppId=" + appraisalID)); 
        //}

        private void EnableResultDropdown(DataTable dt)
        {
            try
            {
                foreach (RepeaterItem item in rptCompetencies.Items)
                {
                    DropDownList ddlrating = item.FindControl("ddlrating") as DropDownList;
                    ddlrating.Visible = true;
                    if (!string.IsNullOrEmpty(dt.Rows[item.ItemIndex]["acmptRating"].ToString()))
                        ddlrating.Items.FindByText(dt.Rows[item.ItemIndex]["acmptRating"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraisee Self Evaluation_H2_EnableResult");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }

        protected void txtWeightage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtaximumAch = sender as TextBox;

                RepeaterItem rptItem = txtaximumAch.NamingContainer as RepeaterItem;

                TextBox txtWeightage = rptItem.FindControl("txtWeightage") as TextBox;
                TextBox txtEvaluation = rptItem.FindControl("txtEvaluation") as TextBox;
                // Label lblScore = rptItem.FindControl("lblScore") as Label;
                Label lblScore1 = rptItem.FindControl("lblScore1") as Label;
                Label lblCategory = rptItem.FindControl("lblCategory") as Label;
                //Label lblAppraiseeComments1 = rptItem.FindControl("lblAppraiseeComments1") as Label;

                DropDownList ddlCategories = rptItem.FindControl("ddlCategories") as DropDownList;
                TextBox txtGoal = rptItem.FindControl("txtGoal") as TextBox;
                TextBox txtDescription = rptItem.FindControl("txtDescription") as TextBox;
                TextBox txtComments = rptItem.FindControl("txtComments") as TextBox;
                DateTimeControl SPDateLastDate = rptItem.FindControl("SPDateLastDate") as DateTimeControl;

                decimal weitage = 0, evaluation = 0;
                if (!string.IsNullOrEmpty(txtWeightage.Text) && !string.IsNullOrEmpty(txtEvaluation.Text))
                {
                    weitage = Convert.ToDecimal(txtWeightage.Text);
                    evaluation = Convert.ToDecimal(txtEvaluation.Text);
                    decimal weight = ((weitage * evaluation) / 100);
                    lblScore1.Text = weight.ToString();
                }
                else
                {
                    lblScore1.Text = 0.ToString();
                }

                decimal total = 0;
                DataTable dt = ViewState["Appraisals"] as DataTable;

                DataRow dr = dt.Rows[rptItem.ItemIndex];

                dr["agGoal"] = txtGoal.Text;
                dr["agDueDate"] = Convert.ToDateTime(SPDateLastDate.SelectedDate);

                if (rptItem.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                    dr["agGoalCategory"] = ddlCategories.SelectedItem.Text;
                else
                    dr["agGoalCategory"] = lblCategory.Text;

                if (!string.IsNullOrEmpty(lblScore1.Text))
                    dr["agScore"] = lblScore1.Text;
                if (!string.IsNullOrEmpty(txtWeightage.Text) && !string.IsNullOrEmpty(txtEvaluation.Text))
                {

                    if (!string.IsNullOrEmpty(txtEvaluation.Text))
                        dr["agEvaluation"] = Convert.ToDecimal(txtEvaluation.Text);

                    if (!string.IsNullOrEmpty(txtWeightage.Text))
                        dr["agWeightage"] = Convert.ToDecimal(txtWeightage.Text);
                }
                else
                {
                    lblScore1.Text = 0.ToString();
                }

                dr["agGoalDescription"] = txtDescription.Text;
                dr["agAppraiseeLatestComments"] = txtComments.Text;

                int i = 0;
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    Label lblScore = item.FindControl("lblScore1") as Label;
                    if (!string.IsNullOrEmpty(lblScore.Text))
                    {
                        total += Convert.ToDecimal(lblScore.Text);
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
                    string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                    dr2["agDueDate"] = strDate;
                    if (!string.IsNullOrEmpty((item.FindControl("txtWeightage") as TextBox).Text))
                        dr2["agWeightage"] = Convert.ToDouble((item.FindControl("txtWeightage") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("TxtEvaluation") as TextBox).Text))
                        dr2["agEvaluation"] = Convert.ToDouble((item.FindControl("TxtEvaluation") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("lblScore1") as Label).Text))
                    {
                        dr2["agScore"] = (item.FindControl("lblScore1") as Label).Text;
                    }

                    dr2["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                    dr2["agAppraiseeLatestComments"] = (item.FindControl("txtComments") as TextBox).Text;
                    i++;
                }

                Panel pnl = (Panel)rptItem.FindControl("testpnl");
                TextBox dummyFld = new TextBox();
                dummyFld.CssClass = "validation";
                dummyFld.Style.Add("display", "none");
                pnl.Controls.Add(dummyFld);


                txtEvaluation.Focus();
                lblScoretotalH2.Visible = true;
                lblScoretotalH2.Text = Convert.ToString(total);
                decimal finalValue;

                if (!string.IsNullOrEmpty(lblH1scoreValue.Text))
                    finalValue = (Convert.ToDecimal(total) + Convert.ToDecimal(lblH1scoreValue.Text)) / 2;

                else
                    finalValue = Convert.ToDecimal(total);

                lblScoretotalH2.Text = Convert.ToString(total);
                lblFS.Text = finalValue.ToString();
                lblfr.Text = CommonMaster.GetRating(finalValue);
                //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraisee Self Evaluation_H2_Text weaitage");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }


        private void ScoreFinal(int appraisalID)
        {
            try
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
            catch (Exception ex)
            {

                LogHandler.LogError(ex, "Error in PMS Appraisee Self Evaluation_H2_ScoreCalculation");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }

        protected void rptGoalSettingsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ritem = e.Item;
            if (ritem.ItemType == ListItemType.AlternatingItem || ritem.ItemType == ListItemType.Item)
            {
                DateTimeControl dc = ritem.FindControl("SPDateLastDate") as DateTimeControl;
                dc.MinDate = DateTime.Now.Date;
            }

        }
        protected void rptPDPDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ritem = e.Item;
            if (ritem.ItemType == ListItemType.AlternatingItem || ritem.ItemType == ListItemType.Item)
            {
                DateTimeControl dc = ritem.FindControl("shpdatecontrol") as DateTimeControl;
                dc.MinDate = DateTime.Now.Date;
            }
        }

    }
}
