
using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.VFSApplicationPages
{
    public partial class AppraiserEvaluation : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure;
        SPUser currentUser;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    dvGoalSettings.Style["display"] = "block";
                    dvCompetencies.Style["display"] = "none";
                    divdevelopmentmessures.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "block";
                    divdevelopmentmessures.Style["display"] = "none";
                }
                else
                {
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    divdevelopmentmessures.Style["display"] = "block";
                }
                if (!this.IsPostBack)
                {
                    SPUser appraisee;//appraisee
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {

                            SPListItem appraisalItem;

                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);//modified at 14-06-2013

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //
                            if (appraisalItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(appraisalItem["appFinalRating"]);
                            }
                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            SPListItem phaseItem = phasesCollection[0];
                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotal.Text = phaseItem["aphScore"].ToString();
                            }

                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName);

                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


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



                        }

                        SPListItem appraiseeData = CommonMaster.GetTheAppraiseeDetails(appraisee.LoginName);

                        if (appraiseeData != null)
                        {
                            lblHeaderValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                            lblemployeevalue.Text = Convert.ToString(appraiseeData["EmployeeCode"]);
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

                        string reviewercode;
                        reviewercode = Convert.ToString(appraiseeData["DepartmentHeadEmployeeCode"]);

                        appLog.Value = CommonMaster.GetUserByCode(reviewercode);

                        this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.dummyTable == null)
                            this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dummyTable != null)//upendra
                        {
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
                            this.hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                        }
                        ViewState["Appraisals"] = this.dummyTable;
                        rptGoalSettings.DataSource = this.dummyTable;//// ViewState["Appraisals"];
                        rptGoalSettings.DataBind();
                        BindDateTimeControl("last");

                        if (Convert.ToInt32(this.hfldMandatoryGoalCount.Value) != 0)
                        {
                            MoreGoals(this.dummyTable.Rows.Count);
                            SelectedDropDown(this.dummyTable);
                        }

                        // this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
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
                        //if (appraiser.Name != this.currentUser.Name)
                        //{

                        //}
                        rptCompetencies.DataSource = this.dtCompetencies;
                        rptCompetencies.DataBind();
                        EnableResultDropdown(this.dtCompetencies);


                        this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesureDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.DtDevelopmentmesure == null)
                            this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        // this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        if (this.DtDevelopmentmesure != null)
                        {
                            // hfHasPDP.Value = "true";
                            this.DtDevelopmentmesure.Columns.Add("SNo", typeof(string));
                            int k = 1;
                            foreach (DataRow dr in this.DtDevelopmentmesure.Rows)
                            {
                                dr["SNo"] = k;
                                k++;
                            }

                            ViewState["PDP"] = this.DtDevelopmentmesure;
                        }


                        RptDevelopmentMesure.DataSource = ViewState["PDP"];
                        RptDevelopmentMesure.DataBind();
                        BindDateTimeControl2("last");
                        if (this.DtDevelopmentmesure.Rows.Count >= 1)
                        {
                            MorePDP(this.DtDevelopmentmesure.Rows.Count);
                        }

                    }
                }
                if (!string.IsNullOrEmpty(lblScoretotal.Text))
                {
                    decimal finalValue = Convert.ToDecimal(lblScoretotal.Text);
                    lblfr.Text = CommonMaster.GetRating(finalValue);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser Evaluation PageLoad");
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
                    count = rptGoalSettings.Items.Count - 1;
                }
                else
                {
                    count = rptGoalSettings.Items.Count;
                }
                DataTable dt = ViewState["Appraisals"] as DataTable;
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    DateTimeControl dc = item.FindControl("SPDateLastDategoal") as DateTimeControl;
                    if (item.ItemIndex < count)
                    {
                        DataRow dr = dt.Rows[i];

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["agDueDate"])))
                        {
                            dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                        }
                        else
                        {
                            dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                        }
                        i++;
                    }
                    else
                    {
                        dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void BindDateTimeControl2(string last)
        {
            try
            {
                int i = 0;
                int count = 0;
                if (last != "last")
                {
                    count = RptDevelopmentMesure.Items.Count - 1;
                }
                else
                {
                    count = RptDevelopmentMesure.Items.Count;
                }
                DataTable dt = ViewState["PDP"] as DataTable;
                foreach (RepeaterItem item in RptDevelopmentMesure.Items)
                {
                    DateTimeControl dc = item.FindControl("SPDateLastDate1") as DateTimeControl;
                    if (item.ItemIndex < count)
                    {
                        DataRow dr = dt.Rows[i];

                        if (!string.IsNullOrEmpty(Convert.ToString(dr["pdpWhen"])))
                        {
                            dc.SelectedDate = Convert.ToDateTime(dr["pdpWhen"]);
                        }
                        else
                        {
                            dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                        }
                        i++;
                    }
                    else
                    {
                        dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                    }
                }
            }
            catch (Exception)
            {
                //throw;
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

                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    if ((item.FindControl("txtGoal") as TextBox).Text != string.Empty && (item.FindControl("SPDateLastDategoal") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtWeightage") as TextBox).Text != string.Empty && (item.FindControl("txtDescription") as TextBox).Text != string.Empty)
                    {
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                        {
                            dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                        }
                        else
                        {
                            dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                        }

                        dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                        DateTimeControl dc = item.FindControl("SPDateLastDategoal") as DateTimeControl;
                        string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        dr["agDueDate"] = strDate;
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                        dr["agEvaluation"] = (item.FindControl("TxtEvaluation") as TextBox).Text;
                        dr["agScore"] = (item.FindControl("LblScore1") as Label).Text;
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        dr["agAppraiserLatestComments"] = (item.FindControl("TxtagAprComments") as TextBox).Text;
                        // dr["agScore"] = (item.FindControl("lblScoretotal") as Label).Text;
                        i++;
                    }

                    else
                    {

                        //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script type='text/javascript'>alert('Please fill all the previous goal details')</script>");
                        //Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script type='text/javascript'>alert('Please fill all the previous goal details')</script>");
                        Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + CustomMessages.FillPreviousGoals + "')</script>");
                        return;
                    }
                }

                DataRow dr1 = this.dummyTable.NewRow();

                dr1["SNo"] = this.dummyTable.Rows.Count + 1;

                this.dummyTable.Rows.Add(dr1);

                rptGoalSettings.DataSource = this.dummyTable;
                rptGoalSettings.DataBind();
                BindDateTimeControl("first");

                ViewState["Appraisals"] = null;
                ViewState["Appraisals"] = this.dummyTable;

                //if (this.dummyTable.Rows.Count >= 5)
                if (this.dummyTable.Rows.Count >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                {
                    MoreGoals(this.dummyTable.Rows.Count);
                    SelectedDropDown(this.dummyTable);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser Evaluation Add");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        private void MoreGoals(int rowsCount)
        {
            DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                // if (item.ItemIndex >= 5)
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
            int j = 0;
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {

                //if (item.ItemIndex >= 5)
                DataRow dr = dt.Rows[j];
                if (item.ItemIndex >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    if (dt.Rows[item.ItemIndex]["agGoalCategory"].ToString() != string.Empty)
                    {
                        ddl.Items.FindByText(dt.Rows[item.ItemIndex]["agGoalCategory"].ToString()).Selected = true;
                    }
                    dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                }
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
                    if (!string.IsNullOrEmpty(dt.Rows[item.ItemIndex]["acmptRating"].ToString()))
                        ddlrating.Items.FindByText(dt.Rows[item.ItemIndex]["acmptRating"].ToString()).Selected = true;
                }
            }
            catch (Exception)
            {

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    hfflag.Value = "true";
                    //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));

                    SaveToSavedDraft("Appraisal Goals Draft");
                    SavePDPDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Development Measures Draft");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies Draft");


                    string url = SPContext.Current.Web.Url + "/_layouts/VFSApplicationPages/AppraiserEvaluation.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Saved, "H2") + "');window.open('" + url + "','_self'); </script>");
                });
            //SaveGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
            //SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
            //SavePDP(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));

            //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Saved successfully')</script>");

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

                    SPListItem lstItem;
                    SPList lstGoalSettings = objWeb.Lists[listName];
                    int mandatoryItemCount = 0;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H1");
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDategoal") as DateTimeControl;//upendra change label
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox TxtEvaluation = item.FindControl("TxtEvaluation") as TextBox;
                        Label Lblscore1 = item.FindControl("Lblscore1") as Label;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        Label lblAppraiseeComments = item.FindControl("lblAppraiseeComments1") as Label;//upendra changes the label name
                        TextBox TxtagAprComments = item.FindControl("TxtagAprComments") as TextBox;
                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        //TextBox TxtScore = item.FindControl("TxtScore") as TextBox; //commented by upendra not found in the reapeter

                        //Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        //Label LblEvaluation = item.FindControl("LblEvaluation") as Label;
                        //Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
                        //Label LblCmts = item.FindControl("LblCmts") as Label;
                        //Label lblSno = item.FindControl("lblSno") as Label;
                        //Label lblID = item.FindControl("lblID") as Label;

                        lstItem = lstGoalSettings.AddItem();

                        lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);

                        if (item.ItemIndex >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text); //Upendra did not bind the ddlCategories
                        }
                        else
                        {
                            lstItem["agGoalCategory"] = lblCategory.Text;
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

                        lstItem["agGoal"] = txtGoal.Text;
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
                        lstItem["agWeightage"] = txtWeightage.Text;
                        lstItem["agEvaluation"] = TxtEvaluation.Text;
                        lstItem["agScore"] = Lblscore1.Text;
                        lstItem["agAppraiseeLatestComments"] = lblAppraiseeComments.Text;
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription)))
                            lstItem["agGoalDescription"] = txtDescription.Text;
                        if (!string.IsNullOrEmpty(Convert.ToString(TxtagAprComments)))
                            lstItem["agAppraiserLatestComments"] = TxtagAprComments.Text;
                        lstItem["Author"] = this.currentUser;

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];

                        lstItem = lstAppraisalPhases.GetItemById(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        lstItem["aphScore"] = Convert.ToDecimal(lblScoretotal.Text);

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                        //lstItem = lstCommentsHistory.AddItem();
                        //lstItem["chCommentFor"] = "Goals approved";
                        //lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                        //if (!string.IsNullOrEmpty(Convert.ToString(TxtagAprComments)))
                        //    lstItem["chComment"] = CommonMaster.BindHistory("Goal, " + txtGoal.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Reviewer");.Text;
                        //lstItem["chCommentedBy"] = this.currentUser;
                        //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                        //lstItem["chActor"] = lblEmpNameValue.Text;
                        //lstItem["chRole"] = "Appraiser";

                        //objWeb.AllowUnsafeUpdates = true;
                        //lstItem.Update();
                        //objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Goal, " + txtGoal.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), TxtagAprComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");
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
                        Label lblDescriptionValue1 = item.FindControl("lblDescriptionValue1") as Label;
                        Label lblAppraise = item.FindControl("lblAppraise") as Label;

                        DropDownList ddlExpectedResult = item.FindControl("ddlExpectedResult") as DropDownList;
                        DropDownList ddlRating = item.FindControl("ddlRating") as DropDownList;
                        TextBox TxtAppraisercmts = item.FindControl("TxtAppraisercmts") as TextBox;

                        lstItem = lstGoalSettings.AddItem();

                        lstItem["acmptAppraisalId"] = appraisalID;
                        lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;

                        lstItem["acmptDescription"] = lblDescriptionValue1.Text;
                        lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                        lstItem["acmptExpectedResult"] = Convert.ToString(ddlExpectedResult.SelectedItem.Text);
                        lstItem["acmptRating"] = Convert.ToString(ddlRating.SelectedItem.Text);
                        lstItem["acmptAppraiseeLatestComments"] = lblAppraise.Text;
                        if (!string.IsNullOrEmpty(Convert.ToString(TxtAppraisercmts)))
                            lstItem["acmptAppraiserLatestComments"] = Convert.ToString(TxtAppraisercmts.Text);
                        lstItem["Author"] = this.currentUser;

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Competency, " + lblcompetencies.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), TxtAppraisercmts.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");
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
                    SPList oList = oWeb.Lists[listName];

                    SPListItem CurrentListItem;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H1");
                    foreach (RepeaterItem item in RptDevelopmentMesure.Items)
                    {
                        DateTimeControl dc = item.FindControl("SPDateLastDate1") as DateTimeControl;
                        TextBox txtwhat = item.FindControl("txtwhat") as TextBox;
                        TextBox txtNextSteps = item.FindControl("txtNextSteps") as TextBox;
                        TextBox TxtDevComments = item.FindControl("TxtDevComments") as TextBox;
                        Label lblAppraise = item.FindControl("lblAppraise") as Label;

                        CurrentListItem = oList.AddItem();

                        CurrentListItem["pdpAppraisalID"] = appraisalID;
                        CurrentListItem["pdpAppraisalPhaseID"] = appraisalPhaseId;

                        CurrentListItem["pdpWhat"] = Convert.ToString(txtwhat.Text);
                        //CurrentListItem["pdpWhen"] = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                        TextBox tctDate = dc.Controls[0] as TextBox;
                        if (string.IsNullOrEmpty(tctDate.Text))
                        {
                            string date = h1EndDate.ToString("dd-MMM-yyyy");
                            CurrentListItem["pdpWhen"] = date;
                        }
                        else
                        {
                            CurrentListItem["pdpWhen"] = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        }
                        CurrentListItem["pdpNextSteps"] = Convert.ToString(txtNextSteps.Text);
                        CurrentListItem["pdpH1AppraiseeComments"] = lblAppraise.Text;
                        CurrentListItem["Author"] = this.currentUser;
                        if (!string.IsNullOrEmpty(Convert.ToString(TxtDevComments)))
                            CurrentListItem["pdpH1AppraiserComments"] = Convert.ToString(TxtDevComments.Text);

                        oWeb.AllowUnsafeUpdates = true;
                        CurrentListItem.Update();
                        oWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("PDP, " + txtwhat.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), TxtDevComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");
                    }
                }
            }

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(appLog.Value))
                {
                //int row = txtboxvalidation();
                //if (row == 0)
                //{
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
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

                            SPListItem lstStatusItem = appraisalStatus.GetItemById(6);

                            SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                            CurrentListItem.Update();

                            web.AllowUnsafeUpdates = false;
                        }
                        //WorkflowTriggering();

                        Context.Response.Write("<script type='text/javascript'>alert('Submitted successfully');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                    }
                });
                WorkflowTriggering();
                //}
                //else
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Comments cannot be blank at row " + row + "')</script>");
                 }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('REVIEWER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }

            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser Evaluation Approve");
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

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
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
                if (i >= Convert.ToInt32(this.hfldMandatoryGoalCount.Value))
                {
                    dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                }
                else
                {
                    dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                }

                dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                DateTimeControl dc = item.FindControl("SPDateLastDategoal") as DateTimeControl;
                string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                dr["agDueDate"] = strDate;
                if (!string.IsNullOrEmpty((item.FindControl("txtWeightage") as TextBox).Text))
                    dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                if (!string.IsNullOrEmpty((item.FindControl("TxtEvaluation") as TextBox).Text))
                    dr["agEvaluation"] = (item.FindControl("TxtEvaluation") as TextBox).Text;
                if (!string.IsNullOrEmpty((item.FindControl("LblScore1") as Label).Text))
                    dr["agScore"] = (item.FindControl("LblScore1") as Label).Text;
                dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                dr["agAppraiserLatestComments"] = (item.FindControl("TxtagAprComments") as TextBox).Text;
                // dr["agScore"] = (item.FindControl("lblScoretotal") as Label).Text;
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

            ViewState["Appraisals"] = null;
            ViewState["Appraisals"] = this.dummyTable;

            // if (this.dummyTable.Rows.Count >= 5)
            if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
            {
                MoreGoals(this.dummyTable.Rows.Count);
                SelectedDropDown(this.dummyTable);
            }
            lblScoretotal.Text = Convert.ToString(total);
            lblfr.Text = CommonMaster.GetRating(total);
        }

        public void SaveGoals(int appraisalID, int appraisalPhaseId)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        objWeb.AllowUnsafeUpdates = true;

                        SPList lstGoalSettings = objWeb.Lists["Appraisal Goals"];
                        SPListItem lstItem;

                        foreach (RepeaterItem item in rptGoalSettings.Items)
                        {
                            Label lblGoal = item.FindControl("lblGoal") as Label;
                            Label lblDueDate = item.FindControl("lblDueDate") as Label;
                            Label lblWeightage = item.FindControl("lblWeightage") as Label;
                            Label LblEvaluation = item.FindControl("LblEvaluation") as Label;
                            Label Lblscore1 = item.FindControl("Lblscore1") as Label;
                            Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
                            //Label lblAppraiseeComments = item.FindControl("lblAppraiseeComments") as Label;
                            //Label lblAppraiseeComments1 = item.FindControl("lblAppraiseeComments1") as Label;
                            Label LblCmts = item.FindControl("LblCmts") as Label;
                            TextBox TxtagAprComments = item.FindControl("TxtagAprComments") as TextBox;

                            DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                            Label lblCategory = item.FindControl("lblCategory") as Label;
                            TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                            DateTimeControl dc = item.FindControl("SPDateLastDategoal") as DateTimeControl;
                            TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                            TextBox TxtEvaluation = item.FindControl("TxtEvaluation") as TextBox;
                            TextBox TxtScore = item.FindControl("TxtScore") as TextBox;
                            TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                            Label lblSno = item.FindControl("lblSno") as Label;
                            Label lblID = item.FindControl("lblID") as Label;

                            //int itemId = 0;
                            if (string.IsNullOrEmpty(lblID.Text))
                            {
                                lstItem = lstGoalSettings.AddItem();

                                lstItem["agAppraisalId"] = appraisalID;
                                lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
                            }
                            else
                            {
                                lstItem = lstGoalSettings.GetItemById(Convert.ToInt32(lblID.Text));
                            }

                            //if (item.ItemIndex >= 5)
                            if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                            {
                                //lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);//Upendra ddlCategories not binded
                            }
                            else
                            {
                                lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                            }

                            lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                            lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                            lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                            lstItem["agEvaluation"] = Convert.ToString(TxtEvaluation.Text);
                            lstItem["agScore"] = Convert.ToString(Lblscore1.Text);
                            if (!string.IsNullOrEmpty(Convert.ToString(txtDescription)))
                                lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
                            if (!string.IsNullOrEmpty(Convert.ToString(TxtagAprComments)))
                                lstItem["agAppraiserLatestComments"] = Convert.ToString(TxtagAprComments.Text);
                            //lstItem["agAppraiseeLatestComments"] = Convert.ToString(lblAppraiseeComments1.Text);

                            //workFlowItemId = Convert.ToInt32(lstItemGoals["ID"]);

                            //lstItem["agAppraisalId"] = Convert.ToInt32(lstItemGoals["ID"]);
                            //lstItem["agAppraisalPhaseId"] = Convert.ToInt32(lstItemAppraisal["ID"]);
                            objWeb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            objWeb.AllowUnsafeUpdates = false;

                            SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where></Query>";
                            SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                            lstItem = coll[0];

                            lstItem["aphScore"] = Convert.ToDecimal(lblScoretotal.Text);

                            objWeb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            objWeb.AllowUnsafeUpdates = false;


                            CommonMaster.BindHistory("Goal approved", Convert.ToInt32(hfAppraisalPhaseID.Value), Convert.ToString(TxtagAprComments.Text), this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");

                            //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                            //lstItem = lstCommentsHistory.AddItem();
                            //lstItem["chCommentFor"] = "Goals approved";
                            //lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                            //if (!string.IsNullOrEmpty(Convert.ToString(TxtagAprComments)))
                            //    lstItem["chComment"] = Convert.ToString(TxtagAprComments.Text);
                            //lstItem["chCommentedBy"] = this.currentUser;
                            //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                            //lstItem["chActor"] = lblEmpNameValue.Text;
                            //lstItem["chRole"] = "Appraiser";

                            //objWeb.AllowUnsafeUpdates = true;
                            //lstItem.Update();
                            //objWeb.AllowUnsafeUpdates = false;

                        }



                    }
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Submited Successfully')</script>");
                    //Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
                    //Context.Response.Flush();
                    //Context.Response.End();

                }
            }
            catch (Exception ex)
            {
                // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }

        private void SaveCompetencies(int appraisalID, int appraisalPhaseId)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        objWeb.AllowUnsafeUpdates = true;

                        SPList Appcompetency = objWeb.Lists["Appraisal Competencies"];
                        SPListItem lstItem;

                        foreach (RepeaterItem item in rptCompetencies.Items)
                        {
                            Label lblcompetencies = item.FindControl("lblcompetencies") as Label;
                            Label lblexpectedresult = item.FindControl("lblexpectedresult") as Label;
                            Label Lblrating = item.FindControl("Lblrating") as Label;
                            Label LblComments = item.FindControl("LblComments") as Label;

                            DropDownList ddlExpectedResult = item.FindControl("ddlExpectedResult") as DropDownList;
                            DropDownList ddlRating = item.FindControl("ddlRating") as DropDownList;
                            TextBox TxtAppraisercmts = item.FindControl("TxtAppraisercmts") as TextBox;
                            Label lblItemId = item.FindControl("lblItemId") as Label;

                            if (string.IsNullOrEmpty(lblItemId.Text))
                            {
                                lstItem = Appcompetency.AddItem();

                                lstItem["acmptAppraisalId"] = appraisalID;
                                lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;//Convert.ToInt32(lstItemAppraisal["ID"]);
                            }
                            else
                            {
                                lstItem = Appcompetency.GetItemById(Convert.ToInt32(lblItemId.Text));
                            }

                            lstItem["acmptAppraisalId"] = appraisalID;
                            lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;           //Convert.ToInt32(lstItemAppraisal["ID"]);
                            lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                            lstItem["acmptExpectedResult"] = Convert.ToString(ddlExpectedResult.SelectedItem.Text);
                            lstItem["acmptRating"] = Convert.ToString(ddlRating.SelectedItem.Text);
                            //lstItem["acmptAppraiseeLatestComments"] = Convert.ToString(txtDescription.Text);
                            if (!string.IsNullOrEmpty(Convert.ToString(TxtAppraisercmts)))
                                lstItem["acmptAppraiserLatestComments"] = Convert.ToString(TxtAppraisercmts.Text);
                            //lstItem["acmptReviewerLatestComments"] = Convert.ToString(txtWeightage.Text);
                            //lstItem["acmptAppraisalPhase"] = Convert.ToString(txtDescription.Text);  

                            objWeb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            objWeb.AllowUnsafeUpdates = false;

                        }
                    }
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Submited Successfully')</script>");
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void SavePDP(int appraisalID, int appraisalPhaseId)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        objWeb.AllowUnsafeUpdates = true;

                        SPList lstPDP = objWeb.Lists["Appraisal Development Measures"];
                        SPListItem listItem;

                        foreach (RepeaterItem item in RptDevelopmentMesure.Items)
                        {
                            Label lblSno = item.FindControl("lblSno") as Label;
                            Label LblDeId = item.FindControl("LblDeId") as Label;

                            Label lblwhen = item.FindControl("lblwhen") as Label;
                            Label lblwhat = item.FindControl("lblwhat") as Label;
                            Label lblNextSteps = item.FindControl("lblNextSteps") as Label;
                            Label LblDemcomments = item.FindControl("LblDemcomments") as Label;

                            DateTimeControl dc = item.FindControl("SPDateLastDate1") as DateTimeControl;
                            TextBox txtwhat = item.FindControl("txtwhat") as TextBox;
                            TextBox txtNextSteps = item.FindControl("txtNextSteps") as TextBox;
                            TextBox TxtDevComments = item.FindControl("TxtDevComments") as TextBox;

                            if (string.IsNullOrEmpty(LblDeId.Text))
                            {
                                listItem = lstPDP.AddItem();

                                listItem["pdpAppraisalID"] = appraisalID;
                                listItem["pdpAppraisalPhaseID"] = appraisalPhaseId;
                            }
                            else
                            {
                                listItem = lstPDP.GetItemById(Convert.ToInt32(LblDeId.Text));
                            }

                            listItem["pdpWhat"] = Convert.ToString(txtwhat.Text);
                            listItem["pdpWhen"] = dc.SelectedDate.ToString("dd-MMM-yyyy");
                            listItem["pdpNextSteps"] = Convert.ToString(txtNextSteps.Text);
                            if (!string.IsNullOrEmpty(Convert.ToString(TxtDevComments)))
                                listItem["pdpH1AppraiserComments"] = Convert.ToString(TxtDevComments.Text);
                            objWeb.AllowUnsafeUpdates = true;
                            listItem.Update();
                            objWeb.AllowUnsafeUpdates = false;

                        }


                    }

                    //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Submited Successfully')</script>");


                }
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }

        }


        protected void PDPAdd_Click(object sender, EventArgs e)
        {
            // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Inprogress')</script>");
            int PDPCount = Convert.ToInt32(HfDevelopmentMesure.Value) + 1;

            HfDevelopmentMesure.Value = PDPCount.ToString();

            this.DtDevelopmentmesure = ViewState["PDP"] as DataTable;
            int i = 0;
            foreach (RepeaterItem item in RptDevelopmentMesure.Items)
            {
                DataRow dr = this.DtDevelopmentmesure.Rows[i];
                if ((item.FindControl("SPDateLastDate1") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtwhat") as TextBox).Text != string.Empty && (item.FindControl("txtNextSteps") as TextBox).Text != string.Empty)
                {
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                    dr["pdpWhat"] = (item.FindControl("txtwhat") as TextBox).Text;
                    DateTimeControl dc = item.FindControl("SPDateLastDate1") as DateTimeControl;
                    string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                    dr["pdpWhen"] = strDate;
                    dr["pdpH1AppraiserComments"] = (item.FindControl("TxtDevComments") as TextBox).Text;
                    dr["pdpNextSteps"] = (item.FindControl("txtNextSteps") as TextBox).Text;
                    i++;
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + CustomMessages.FillPreviousDevelopmentMeasures + "')</script>");
                    return;
                }
            }

            DataRow dr1 = this.DtDevelopmentmesure.NewRow();

            dr1["SNo"] = this.DtDevelopmentmesure.Rows.Count + 1;

            this.DtDevelopmentmesure.Rows.Add(dr1);

            RptDevelopmentMesure.DataSource = this.DtDevelopmentmesure;
            RptDevelopmentMesure.DataBind();
            BindDateTimeControl2("first");

            ViewState["PDP"] = null;
            ViewState["PDP"] = this.DtDevelopmentmesure;

            if (this.DtDevelopmentmesure.Rows.Count >= 2)
            {
                MorePDP(this.DtDevelopmentmesure.Rows.Count);
                //SelectedDropDown(this.DtDevelopmentmesure);
            }
        }


        private void MorePDP(int rowsCount)
        {
            //DataTable DtDevelopmentmesure = CommonMasters.GetAppraisalDevelopmentMesure(itemID);
            foreach (RepeaterItem item in RptDevelopmentMesure.Items)
            {
                if (item.ItemIndex >= 1)
                {
                    LinkButton lnkDelete = (LinkButton)item.FindControl("lnkDelete");

                    lnkDelete.Visible = true;
                }
            }
        }


        protected void LnkDelete_Click1(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            //if (!string.IsNullOrEmpty(lnk.CommandArgument))
            //{
            //    int itemID = Convert.ToInt32(lnk.CommandArgument);
            //    //CommonMasters.DeleteGoalsItem(itemID);
            //    CommonMaster.DeletePDP(itemID);
            //}
            RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
            Label lblSno = rptItem.FindControl("lblSno") as Label;
            this.DtDevelopmentmesure = ViewState["PDP"] as DataTable;

            int sNo = Convert.ToInt32(lblSno.Text);
            int i = 0;
            if (sNo != rptGoalSettings.Items.Count)
            {
                foreach (RepeaterItem item in RptDevelopmentMesure.Items)
                {
                    DataRow dr = this.DtDevelopmentmesure.Rows[i];
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                    dr["pdpWhat"] = (item.FindControl("txtwhat") as TextBox).Text;
                    DateTimeControl dc = item.FindControl("SPDateLastDate1") as DateTimeControl;
                    string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                    dr["pdpWhen"] = strDate;
                    dr["pdpH1AppraiserComments"] = (item.FindControl("TxtDevComments") as TextBox).Text;
                    dr["pdpNextSteps"] = (item.FindControl("txtNextSteps") as TextBox).Text;
                    i++;
                    this.DtDevelopmentmesure.AcceptChanges();
                }
            }

            this.DtDevelopmentmesure.Rows.RemoveAt(Convert.ToInt32(lblSno.Text) - 1);
            i = 0;
            foreach (DataRow row in this.DtDevelopmentmesure.Rows)
            {
                DataRow dr = this.DtDevelopmentmesure.Rows[i];
                dr["SNo"] = i + 1;
                i++;
            }

            RptDevelopmentMesure.DataSource = this.DtDevelopmentmesure;
            RptDevelopmentMesure.DataBind();
            if (this.DtDevelopmentmesure.Rows.Count != rptItem.ItemIndex + 1)
            {
                BindDateTimeControl2("last");
            }
            else
            {
                BindDateTimeControl2("first");
            }
            // BindDateTimeControl2("last");

            ViewState["PDP"] = null;
            ViewState["PDP"] = this.DtDevelopmentmesure;

            if (this.DtDevelopmentmesure.Rows.Count >= 1)
            {
                MorePDP(this.DtDevelopmentmesure.Rows.Count);
                // SelectedDropDown(this.dummyTable);
            }
        }

        protected void txtWeightage_TextChanged(object sender, EventArgs e)
        {
            TextBox txtaximumAch = sender as TextBox;

            RepeaterItem rptItem = txtaximumAch.NamingContainer as RepeaterItem;

            TextBox txtWeightage = rptItem.FindControl("txtWeightage") as TextBox;
            TextBox txtEvaluation = rptItem.FindControl("txtEvaluation") as TextBox;
            // Label lblScore = rptItem.FindControl("lblScore") as Label;
            Label lblScore1 = rptItem.FindControl("lblScore1") as Label;
            Label lblCategory = rptItem.FindControl("lblCategory") as Label;
            Label lblAppraiseeComments1 = rptItem.FindControl("lblAppraiseeComments1") as Label;

            DropDownList ddlCategories = rptItem.FindControl("ddlCategories") as DropDownList;
            TextBox txtGoal = rptItem.FindControl("txtGoal") as TextBox;
            TextBox txtDescription = rptItem.FindControl("txtDescription") as TextBox;
            TextBox txtComments1 = rptItem.FindControl("txtComments") as TextBox;
            DateTimeControl SPDateLastDate = rptItem.FindControl("SPDateLastDategoal") as DateTimeControl;

            decimal weitage = 0, evaluation = 0;
            if (!string.IsNullOrEmpty(txtWeightage.Text) && !string.IsNullOrEmpty(txtEvaluation.Text))
            {
                weitage = Convert.ToDecimal(txtWeightage.Text);
                // if (!string.IsNullOrEmpty(txtEvaluation.Text))
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

            if (!string.IsNullOrEmpty(txtEvaluation.Text))
                dr["agEvaluation"] = Convert.ToDouble(txtEvaluation.Text);

            if (!string.IsNullOrEmpty(txtWeightage.Text))
                dr["agWeightage"] = Convert.ToDouble(txtWeightage.Text);

            dr["agGoalDescription"] = txtDescription.Text;
            dr["agAppraiseeLatestComments"] = lblAppraiseeComments1.Text;

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
                DateTimeControl dc = item.FindControl("SPDateLastDategoal") as DateTimeControl;
                string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                dr2["agDueDate"] = strDate;
                if (!string.IsNullOrEmpty(txtWeightage.Text) && !string.IsNullOrEmpty(txtEvaluation.Text))
                {
                    dr2["agWeightage"] = Convert.ToDecimal((item.FindControl("txtWeightage") as TextBox).Text);

                    if (!string.IsNullOrEmpty((item.FindControl("TxtEvaluation") as TextBox).Text))
                        dr2["agEvaluation"] = Convert.ToDecimal((item.FindControl("TxtEvaluation") as TextBox).Text);
                }
                else
                {
                    lblScore1.Text = 0.ToString();
                }

                if (!string.IsNullOrEmpty((item.FindControl("lblScore1") as Label).Text))
                {
                    dr2["agScore"] = (item.FindControl("lblScore1") as Label).Text;
                }

                dr2["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                dr2["agAppraiseeLatestComments"] = (item.FindControl("lblAppraiseeComments1") as Label).Text;
                i++;

            }


            Panel pnl = (Panel)rptItem.FindControl("testpnl");
            TextBox dummyFld = new TextBox();
            dummyFld.CssClass = "validation";
            dummyFld.Style.Add("display", "none");
            pnl.Controls.Add(dummyFld);

            txtEvaluation.Focus();
            lblScoretotal.Visible = true;
            lblScoretotal.Text = Convert.ToString(total);
            lblfr.Text = CommonMaster.GetRating(total);

            //decimal finalValue = Convert.ToDecimal(lblScoretotal.Text);
            //GetRating(finalValue);
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
                DateTimeControl dc = ritem.FindControl("SPDateLastDategoal") as DateTimeControl;
                dc.MinDate = DateTime.Now;
            }

        }

        protected void RptDevelopmentMesure_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ritem = e.Item;
            if (ritem.ItemType == ListItemType.AlternatingItem || ritem.ItemType == ListItemType.Item)
            {
                DateTimeControl dc = ritem.FindControl("SPDateLastDate1") as DateTimeControl;
                dc.MinDate = DateTime.Now;
            }

        }

        private int txtboxvalidation()
        {
            int txtVal = 0;
            int count = 0;
            foreach (RepeaterItem item in RptDevelopmentMesure.Items)
            {
                count++;
                TextBox TxtDevComments = item.FindControl("TxtDevComments") as TextBox;
                if (TxtDevComments.Text == string.Empty || TxtDevComments.Text == null)
                {
                    txtVal = count;
                    break;
                }
            }
            return txtVal;
        }
    }
}
