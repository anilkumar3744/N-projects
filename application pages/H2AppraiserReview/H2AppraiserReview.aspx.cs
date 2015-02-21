using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Microsoft.SharePoint.Workflow;
using System.Web.UI;

namespace VFS.PMS.ApplicationPages.Layouts.H2AppraiserReview
{
    public partial class H2AppraiserReview : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure;
        SPUser currentUser;
        protected string message = string.Empty;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                divButtons.Style.Add("display", "block");
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    //dvGoalSettings.Visible = true;
                    //dvCompetencies.Visible = false;
                    //divdevelopmentmessures.Visible = false;
                    dvGoalSettings.Style["display"] = "block";
                    dvCompetencies.Style["display"] = "none";
                    divdevelopmentmessures.Style["display"] = "none";
                    pipdiv.Style["display"] = "none";

                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    //dvGoalSettings.Visible = false;
                    //dvCompetencies.Visible = true;
                    //divdevelopmentmessures.Visible = false;
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "block";
                    divdevelopmentmessures.Style["display"] = "none";
                    pipdiv.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    //dvGoalSettings.Visible = false;
                    //dvCompetencies.Visible = false;
                    //divdevelopmentmessures.Visible = true;
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    divdevelopmentmessures.Style["display"] = "block";
                    pipdiv.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 3)
                {
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    divdevelopmentmessures.Style["display"] = "none";
                    pipdiv.Style["display"] = "block";
                    //Button10.Visible = false;
                    divButtons.Style.Add("display", "none");
                    PostBackTrigger trigger = new PostBackTrigger();
                    trigger.ControlID = "lnkSave";
                    Updatepanel.Triggers.Add(trigger);
                }
                if (!this.IsPostBack)
                {

                    SPList lstAppraisalTasks;  //
                    SPListItem taskItem; //
                    // int itemID;
                    // int itemID = Convert.ToInt32(Request.Params["ID"]);
                    SPUser appraisee;//appraisee
                    //SPListItem appraisalItem;
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            //lstAppraisalTasks = currentWeb.Lists["VFSAppraisalTasks"];   //
                            //taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));  //
                            //hfAppraisalID.Value = Convert.ToString(taskItem["tskAppraisalId"]); //
                            //lblStatusValue.Text = Convert.ToString(taskItem["tskStatus"]); //
                            SPListItem appraisalItem;

                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);//modified at 14-06-2013

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
                            if (appraisalItem["appFinalRating"] != null)
                            {
                                Lblfr.Text = appraisalItem["appFinalRating"].ToString();
                            }
                            if (appraisalItem["appFinalScore"] != null)
                            {
                                LblFS.Text = appraisalItem["appFinalScore"].ToString();
                            }
                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                            }
                            //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), currentWeb))
                            //{
                            //    lnksave.Visible = false;
                            //}


                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];
                            SPQuery phasesQuery = new SPQuery();

                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            if (phasesCollection.Count > 0)
                            {
                                SPListItem phaseItem = phasesCollection[0];
                                if (phaseItem["aphScore"] != null)
                                {
                                    LblH1score1.Text = phaseItem["aphScore"].ToString();
                                }
                            }
                            SPQuery phasesQueryH2 = new SPQuery();
                            phasesQueryH2.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollectionH2 = lstAppraisalPhases.GetItems(phasesQueryH2);
                            SPListItem phaseItemH2 = phasesCollectionH2[0];
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItemH2["ID"]);
                            if (phaseItemH2["aphScore"] != null)
                            {
                                lblH2Score.Text = phaseItemH2["aphScore"].ToString();
                            }
                            if (phaseItemH2["aphHRReviewLatestComments"] != null)
                            {
                                lblHRReviewFinalComments.Text = phaseItemH2["aphHRReviewLatestComments"].ToString();

                            }

                            string tabValue = Convert.ToString(Request.Params["TaskID"]);

                            #region Developed By Krishna 240620130539

                            DataTable dtPIPDetails = new DataTable();
                            SPListItem spIMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser.LoginName.Split('\\')[1]);
                            string id = spIMaster["EmployeeCode"].ToString();
                            SPListItem spItem = CommonMaster.GetMasterDetails("ReportingManagerEmployeeCode", id);
                            SPListItem spIAppraisal = CommonMaster.GetAppraisalCheck("ID", hfAppraisalID.Value);
                            if (spItem != null)
                            {

                                //Decimal Score = Convert.ToDecimal(lblScoretotal.Text);
                                if (phaseItemH2["aphIsPIP"] != null && phaseItemH2["aphIsPIP"].ToString() != string.Empty)
                                {
                                    if (phaseItemH2["aphIsPIP"].ToString() == "True")
                                    {
                                        tbpnlpip.Visible = true;
                                        pipdiv.Visible = true;
                                        Button10.Visible = false;
                                        //btnAdd.Visible = false;
                                        //btnSubmit.Visible = false;
                                        dtPIPDetails = CommonMaster.GetPIPDetails(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                                        if (dtPIPDetails != null)
                                        {
                                            int i = 0;
                                            dtPIPDetails.Columns.Add("SNo", typeof(string));
                                            foreach (DataRow row in dtPIPDetails.Rows)
                                            {
                                                DataRow dr = dtPIPDetails.Rows[i];
                                                dr["SNo"] = i + 1;
                                                i++;
                                            }
                                            rptpip.DataSource = dtPIPDetails;
                                            rptpip.DataBind();
                                            foreach (RepeaterItem item in rptpip.Items)
                                            {
                                                TextBox txtActualResultmidterm = item.FindControl("txtActualResultmidterm") as TextBox;
                                                TextBox txtAppraisersAssessmentmidterm = item.FindControl("txtAppraisersAssessmentmidterm") as TextBox;
                                                TextBox txtActualResultfinelterm = item.FindControl("txtActualResultfinelterm") as TextBox;
                                                TextBox txtAppraisersAssessmentfinalterm = item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox;
                                                TextBox txtPerformanceIssu = item.FindControl("txtPerformanceIssu") as TextBox;
                                                TextBox txtExpectedAchivements = item.FindControl("txtExpectedAchivements") as TextBox;
                                                TextBox txtTimeFrame = item.FindControl("txtTimeFrame") as TextBox;
                                                Label lblPerformanceIssu = item.FindControl("lblPerformanceIssu") as Label;
                                                Label lblExpectedAchivements = item.FindControl("lblExpectedAchivements") as Label;
                                                Label lblTimeFrame = item.FindControl("lblTimeFrame") as Label;
                                                Label lblActualResultmidterm = item.FindControl("lblActualResultmidterm") as Label;
                                                Label lblAppraisersAssessmentmidterm = item.FindControl("lblAppraisersAssessmentmidterm") as Label;
                                                Label lblActualResultfinelterm = item.FindControl("lblActualResultfinelterm") as Label;
                                                Label lblAppraisersAssessmentfinalterm = item.FindControl("lblAppraisersAssessmentfinalterm") as Label;
                                                LinkButton lnkDelete = item.FindControl("lnkDelete") as LinkButton;

                                                if (tabValue == "1")
                                                {
                                                    txtPerformanceIssu.Visible = true;
                                                    txtExpectedAchivements.Visible = true;
                                                    txtTimeFrame.Visible = true;
                                                    if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text))
                                                    {
                                                        btnPIPAdd.Visible = false;
                                                        lnkDelete.Visible = false;
                                                        txtPerformanceIssu.Visible = false;
                                                        txtExpectedAchivements.Visible = false;
                                                        txtTimeFrame.Visible = false;
                                                        lblPerformanceIssu.Visible = true;
                                                        lblExpectedAchivements.Visible = true;
                                                        lblTimeFrame.Visible = true;
                                                        txtActualResultmidterm.Visible = true;
                                                        txtAppraisersAssessmentmidterm.Visible = true;
                                                    }
                                                    if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text) && !string.IsNullOrEmpty(txtActualResultmidterm.Text) && !string.IsNullOrEmpty(txtAppraisersAssessmentmidterm.Text))
                                                    {
                                                        btnPIPAdd.Visible = false;
                                                        lnkDelete.Visible = false;
                                                        lblActualResultmidterm.Visible = true;
                                                        txtActualResultfinelterm.Visible = true;
                                                        txtActualResultmidterm.Visible = false;
                                                        txtAppraisersAssessmentmidterm.Visible = false;
                                                        txtAppraisersAssessmentfinalterm.Visible = true;
                                                        lblAppraisersAssessmentmidterm.Visible = true;
                                                    }
                                                    if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text) && !string.IsNullOrEmpty(txtActualResultfinelterm.Text) && !string.IsNullOrEmpty(txtAppraisersAssessmentfinalterm.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text))
                                                    {
                                                        txtActualResultfinelterm.Visible = false;
                                                        txtAppraisersAssessmentfinalterm.Visible = false;
                                                        btnPIPAdd.Visible = false;
                                                        lnkDelete.Visible = false;
                                                        lnksave.Visible = false;
                                                        lblPerformanceIssu.Visible = true;
                                                        lblExpectedAchivements.Visible = true;
                                                        lblTimeFrame.Visible = true;
                                                        lblActualResultmidterm.Visible = true;
                                                        lblAppraisersAssessmentmidterm.Visible = true;
                                                        lblActualResultfinelterm.Visible = true;
                                                        lblAppraisersAssessmentfinalterm.Visible = true;
                                                    }
                                                }
                                                else
                                                {
                                                    txtActualResultmidterm.Visible = false;
                                                    txtAppraisersAssessmentmidterm.Visible = false;
                                                    txtActualResultfinelterm.Visible = false;
                                                    txtAppraisersAssessmentfinalterm.Visible = false;
                                                    txtPerformanceIssu.Visible = false;
                                                    txtExpectedAchivements.Visible = false;
                                                    txtTimeFrame.Visible = false;
                                                    lblPerformanceIssu.Visible = true;
                                                    lblExpectedAchivements.Visible = true;
                                                    lblTimeFrame.Visible = true;
                                                    lblActualResultmidterm.Visible = true;
                                                    lblAppraisersAssessmentmidterm.Visible = true;
                                                    lblAppraisersAssessmentfinalterm.Visible = true;
                                                    lblActualResultfinelterm.Visible = true;
                                                    lnkDelete.Visible = false;
                                                    btnPIPAdd.Visible = false;
                                                    lnksave.Visible = false;
                                                }


                                            }

                                        }
                                        else
                                        {
                                            if (tabValue == "1")
                                                dtPIPDetails = dumytable();
                                            else
                                            {
                                                pipdiv.Style["display"] = "none";
                                                tbpnlpip.Visible = false;
                                            }
                                            //{
                                            //    btnPIPAdd.Visible = false;
                                            //    lnksave.Visible = false;
                                            //}
                                            rptpip.DataSource = dumytable();
                                            rptpip.DataBind();
                                        }
                                        ViewState["dummyTable"] = dtPIPDetails;
                                    }
                                }

                            }


                            #endregion

                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName);

                            //if (appraisee.Name == this.currentUser.Name)
                            //{
                            //    lnksave.Visible = false;
                            //}


                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, Web))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                return;
                            }
                            if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, "H2 - Awaiting Appraiser Evaluation", Convert.ToInt32(hfAppraisalID.Value), Web))
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
                        // this.dummyTable = CommonMasters.GetGoalsDetails(itemID);
                        this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.dummyTable == null)
                            this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dummyTable != null)//upendra
                        {
                            this.dummyTable.Columns.Add("SNo", typeof(string));

                            int i = 1;
                            int mandatoryGoalCount = 0; //
                            decimal total = 0;
                            foreach (DataRow dr in this.dummyTable.Rows)
                            {
                                dr["SNo"] = i;
                                i++;
                                if (dr["IsMandatory"].ToString() == "True")  //
                                {
                                    mandatoryGoalCount++;//
                                }//

                                if (!string.IsNullOrEmpty(Convert.ToString(dr["agScore"])))
                                    total += Convert.ToDecimal(dr["agScore"]);
                                lblH2Score.Text = total.ToString();
                            }

                            if (!string.IsNullOrEmpty(LblH1score1.Text))
                            {
                                LblFS.Text = Convert.ToString((Convert.ToDecimal(LblH1score1.Text) + total) / 2);
                            }
                            else
                                LblFS.Text = total.ToString();

                            //decimal fv = Convert.ToDecimal(LblFS.Text);
                            //Lblfr.Text = CommonMaster.GetRating(fv);
                            hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();  //
                        }
                        ViewState["Appraisals"] = this.dummyTable;
                        rptGoalSettings.DataSource = ViewState["Appraisals"];
                        rptGoalSettings.DataBind();
                        BindDateTimeControl("last");

                        // if (this.dummyTable.Rows.Count >= 5)
                        if (Convert.ToInt32(hfldMandatoryGoalCount.Value) != 0)
                        {
                            MoreGoals(this.dummyTable.Rows.Count);
                            SelectedDropDown(this.dummyTable);
                        }


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
                        if (this.DtDevelopmentmesure != null)
                        {
                            this.DtDevelopmentmesure.Columns.Add("SNo", typeof(string));
                            int k = 1;
                            foreach (DataRow dr in this.DtDevelopmentmesure.Rows)
                            {
                                dr["SNo"] = k;
                                k++;
                            }
                            ViewState["PDP"] = this.DtDevelopmentmesure;
                        }
                        RptDevelopmentMesure.DataSource = this.DtDevelopmentmesure;
                        RptDevelopmentMesure.DataBind();
                        BindDateTimeControl2("last");
                        if (DtDevelopmentmesure != null)
                        {
                            if (this.DtDevelopmentmesure.Rows.Count >= 1)
                            {
                                MorePDP(this.DtDevelopmentmesure.Rows.Count);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
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
                LogHandler.LogError(ex, "Error in PMS Appraiser Review H2");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
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
                    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
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
            catch (Exception)
            {
                throw;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            int goulsCount = Convert.ToInt32(hfGoalsCount.Value) + 1;

            hfGoalsCount.Value = goulsCount.ToString();

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
                    string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                    dr["agDueDate"] = strDate;
                    dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                    dr["agEvaluation"] = (item.FindControl("TxtEvaluation") as TextBox).Text;
                    dr["agScore"] = (item.FindControl("LblScore1") as Label).Text;
                    dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                    dr["agAppraiserLatestComments"] = (item.FindControl("TxtAprComments") as TextBox).Text;
                    i++;
                }

                else
                {
                    // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Fill first 5 goals')</script>");
                    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + CustomMessages.FillPreviousGoals + "')</script>");
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
            if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
            {
                MoreGoals(this.dummyTable.Rows.Count);
                SelectedDropDown(this.dummyTable);
            }
        }

        //protected void BtnAdd_Click(object sender, EventArgs e)
        //{
        //    int goulsCount = Convert.ToInt32(hfGoalsCount.Value) + 1;

        //    hfGoalsCount.Value = goulsCount.ToString();

        //    this.dummyTable = ViewState["Appraisals"] as DataTable;

        //    int i = 0;

        //    foreach (RepeaterItem item in rptGoalSettings.Items)
        //    {
        //        DataRow dr = this.dummyTable.Rows[i];
        //        if ((item.FindControl("txtGoal") as TextBox).Text != string.Empty && (item.FindControl("SPDateLastDate") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtWeightage") as TextBox).Text != string.Empty && (item.FindControl("txtDescription") as TextBox).Text != string.Empty)
        //        {
        //            dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

        //            if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
        //            {
        //                dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
        //            }
        //            else
        //            {
        //                dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
        //            }

        //            dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
        //            DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
        //            string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
        //            dr["agDueDate"] = strDate;
        //            dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
        //            dr["agEvaluation"] = (item.FindControl("TxtEvaluation") as TextBox).Text;
        //            dr["agScore"] = (item.FindControl("LblScore1") as Label).Text;
        //            dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
        //            dr["agAppraiserLatestComments"] = (item.FindControl("TxtAprComments") as TextBox).Text;
        //            i++;
        //        }

        //        else
        //        {
        //            // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Fill first 5 goals')</script>");
        //            // return;
        //        }
        //    }

        //    DataRow dr1 = this.dummyTable.NewRow();

        //    dr1["SNo"] = this.dummyTable.Rows.Count + 1;

        //    this.dummyTable.Rows.Add(dr1);

        //    rptGoalSettings.DataSource = this.dummyTable;
        //    rptGoalSettings.DataBind();
        //    BindDateTimeControl("first");

        //    ViewState["Appraisals"] = null;
        //    ViewState["Appraisals"] = this.dummyTable;

        //    //if (this.dummyTable.Rows.Count >= 5)
        //    if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
        //    {
        //        MoreGoals(this.dummyTable.Rows.Count);
        //        SelectedDropDown(this.dummyTable);
        //    }
        //}

        private void MoreGoals(int rowsCount)
        {
            //DataTable dtCategories = CommonMaster.GetCategories();
            DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                // if (item.ItemIndex >= 5)
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
                    //TextBox dummyFld = new TextBox();
                    //dummyFld.CssClass = "validation";
                    //dummyFld.Style.Add("display", "none");
                    //item.Controls.Add(dummyFld);
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
                DataRow dr = dt.Rows[j];
                //if (item.ItemIndex >= 5)
                if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
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
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    hfflag.Value = "true";
                    SaveToSavedDraft("Appraisal Goals Draft");
                    SavePDPDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Development Measures Draft");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies Draft");
                    //SaveGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));             
                    //SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));             
                    //SavePDP(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
                    //Lblfr.Text = CommonMaster.GetRating(Convert.ToInt32(hfAppraisalID.Value));

                    //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('H2 Saved Successfully')</script>");
                    string url = SPContext.Current.Web.Url + "/_layouts/H2AppraiserReview/H2AppraiserReview.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + string.Format(CustomMessages.Saved, "H2") + "'); </script>");



                });

            }
            catch (Exception ex)
            {

                LogHandler.LogError(ex, "Error in PMS H2 Appraiser Review");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(appLog.Value))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SaveToSavedDraft("Appraisal Goals");
                        SavePDPDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Development Measures");
                        SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies");

                        CommonMaster.DeleteCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Competencies Draft");
                        CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");
                        CommonMaster.DeletePDPDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Development Measures Draft");

                        //Lblfr.Text = CommonMaster.GetRating(Convert.ToDecimal(hfAppraisalID.Value));
                        ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));

                        //}
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;

                                SPList list = web.Lists["Appraisals"];
                                SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));


                                SPList appraisalStatus = web.Lists["Appraisal Status"];
                                //SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];

                                //SPListItem taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));
                                SPListItem lstStatusItem = appraisalStatus.GetItemById(17);

                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                CurrentListItem.Update();

                                //Hashtable ht = new Hashtable();
                                //ht["tskStatus"] = "Approved";       //Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                //ht["Status"] = "Approved";

                                //SPWorkflowTask.AlterTask(taskItem, ht, true);
                                web.AllowUnsafeUpdates = false;
                            }

                        }
                    });
                    WorkflowTriggering();

                    Context.Response.Write("<script type='text/javascript'>alert('Submitted successfully');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                    Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Submitted, "H2") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('REVIEWER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Appraiser Review");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }
        //private void WorkflowTriggering()
        //{
        //    try
        //    {
        //        using (SPWeb web = SPContext.Current.Site.OpenWeb())
        //        {

        //            SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];

        //            SPListItem taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));
        //            taskItem[SPBuiltInFieldId.WorkflowVersion] = 1;
        //            taskItem.SystemUpdate();

        //            Hashtable ht = new Hashtable();
        //            ht["tskStatus"] = "Approved"; //Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
        //            ht["Status"] = "Approved";

        //            SPWorkflowTask.AlterTask(taskItem, ht, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
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
                        q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H2 - Awaiting Appraiser Evaluation</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
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
            //if (sNo != rptGoalSettings.Items.Count)
            //{
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                DataRow dr = this.dummyTable.Rows[i];
                dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                Label LblScore1 = item.FindControl("LblScore1") as Label;//lblScore
                if (!string.IsNullOrEmpty(LblScore1.Text) && (i != Convert.ToInt32(lblSno.Text) - 1))
                {
                    total += Convert.ToDecimal(LblScore1.Text);
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
                if (!string.IsNullOrEmpty((item.FindControl("TxtEvaluation") as TextBox).Text))
                    dr["agEvaluation"] = (item.FindControl("TxtEvaluation") as TextBox).Text;
                if (!string.IsNullOrEmpty((item.FindControl("LblScore1") as Label).Text))
                    dr["agScore"] = (item.FindControl("LblScore1") as Label).Text;
                dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                dr["agAppraiserLatestComments"] = (item.FindControl("TxtAprComments") as TextBox).Text;
                i++;

            }
            //}

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
            decimal finalValue;
            if (!string.IsNullOrEmpty(LblH1score1.Text))
                finalValue = (Convert.ToDecimal(total) + Convert.ToDecimal(LblH1score1.Text)) / 2;
            else
                finalValue = Convert.ToDecimal(total);
            LblFS.Text = finalValue.ToString();
            lblH2Score.Text = Convert.ToString(total);
            Lblfr.Text = CommonMaster.GetRating(finalValue);
        }

        //public void SaveGoals(int appraisalID, int appraisalPhaseId)
        //{
        //    try
        //    {
        //        using (SPWeb objWeb = SPContext.Current.Site.OpenWeb())
        //        {
        //            objWeb.AllowUnsafeUpdates = true;

        //            SPList lstGoalSettings = objWeb.Lists["Appraisal Goals"];
        //            SPListItem lstItem;

        //            foreach (RepeaterItem item in rptGoalSettings.Items)
        //            {
        //                Label lblGoal = item.FindControl("lblGoal") as Label;
        //                Label lblDueDate = item.FindControl("lblDueDate") as Label;
        //                Label lblWeightage = item.FindControl("lblWeightage") as Label;
        //                Label LblEvaluation = item.FindControl("LblEvaluation") as Label;
        //                Label Lblscore1 = item.FindControl("Lblscore1") as Label;
        //                Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
        //                //Label lblAppraiseeComments = item.FindControl("lblAppraiseeComments") as Label;
        //                //Label lblAppraiseeComments1 = item.FindControl("lblAppraiseeComments1") as Label;
        //                Label LblCmts = item.FindControl("LblCmts") as Label;
        //                TextBox TxtAprComments = item.FindControl("TxtAprComments") as TextBox;

        //                DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
        //                Label lblCategory = item.FindControl("lblCategory") as Label;
        //                TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
        //                DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
        //                TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
        //                TextBox TxtEvaluation = item.FindControl("TxtEvaluation") as TextBox;
        //                TextBox TxtScore = item.FindControl("TxtScore") as TextBox;
        //                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
        //                Label lblSno = item.FindControl("lblSno") as Label;
        //                Label lblID = item.FindControl("lblID") as Label;

        //                //int itemId = 0;
        //                if (string.IsNullOrEmpty(lblID.Text))
        //                {
        //                    lstItem = lstGoalSettings.AddItem();

        //                    lstItem["agAppraisalId"] = appraisalID;
        //                    lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
        //                }
        //                else
        //                {
        //                    lstItem = lstGoalSettings.GetItemById(Convert.ToInt32(lblID.Text));
        //                }

        //                //if (item.ItemIndex >= 5)
        //                if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
        //                {
        //                    lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
        //                }
        //                else
        //                {
        //                    lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
        //                }

        //                lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
        //                lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.Date.ToString("dd-MMM-yyyy"));
        //                lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
        //                lstItem["agEvaluation"] = Convert.ToString(txtWeightage.Text);
        //                lstItem["agScore"] = Convert.ToString(Lblscore1.Text);
        //                if (!string.IsNullOrEmpty(Convert.ToString(txtDescription)))
        //                    lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
        //                if (!string.IsNullOrEmpty(Convert.ToString(TxtAprComments)))
        //                    lstItem["agAppraiserLatestComments"] = Convert.ToString(TxtAprComments.Text);
        //                //lstItem["agAppraiseeLatestComments"] = Convert.ToString(lblAppraiseeComments1.Text);

        //                //workFlowItemId = Convert.ToInt32(lstItemGoals["ID"]);

        //                //lstItem["agAppraisalId"] = Convert.ToInt32(lstItemGoals["ID"]);
        //                //lstItem["agAppraisalPhaseId"] = Convert.ToInt32(lstItemAppraisal["ID"]);
        //                objWeb.AllowUnsafeUpdates = true;
        //                lstItem.Update();
        //                objWeb.AllowUnsafeUpdates = false;

        //                //SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
        //                //SPQuery q = new SPQuery();
        //                //q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where></Query>";
        //                //SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

        //                //lstItem = coll[0];

        //                //lstItem["aphScore"] = Convert.ToInt32(lblH2Score.Text);

        //                //objWeb.AllowUnsafeUpdates = true;
        //                //lstItem.Update();
        //                //objWeb.AllowUnsafeUpdates = false;

        //                SPList lstCommentsHistory = objWeb.Lists["Comments History"];

        //                lstItem = lstCommentsHistory.AddItem();
        //                lstItem["chCommentFor"] = "Goals approved";
        //                lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
        //                if (!string.IsNullOrEmpty(Convert.ToString(TxtAprComments)))
        //                    lstItem["chComment"] = Convert.ToString(TxtAprComments.Text);
        //                lstItem["chCommentedBy"] = this.currentUser;
        //                lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
        //                lstItem["chActor"] = lblEmpNameValue.Text;
        //                lstItem["chRole"] = "Appraiser";

        //                objWeb.AllowUnsafeUpdates = true;
        //                lstItem.Update();
        //                objWeb.AllowUnsafeUpdates = false;

        //            }



        //        }

        //        // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Submited Successfully')</script>");
        //        //Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
        //        //Context.Response.Flush();
        //        //Context.Response.End();

        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
        //    }
        //}

        //private void SaveCompetencies(int appraisalID, int appraisalPhaseId)
        //{
        //    try
        //    {
        //        using (SPWeb objWeb = SPContext.Current.Site.OpenWeb())
        //        {
        //            objWeb.AllowUnsafeUpdates = true;

        //            SPList Appcompetency = objWeb.Lists["Appraisal Competencies"];
        //            SPListItem lstItem;

        //            foreach (RepeaterItem item in rptCompetencies.Items)
        //            {
        //                Label lblcompetencies = item.FindControl("lblcompetencies") as Label;
        //                Label lblexpectedresult = item.FindControl("lblexpectedresult") as Label;
        //                Label Lblrating = item.FindControl("Lblrating") as Label;
        //                Label LblComments = item.FindControl("LblComments") as Label;

        //                DropDownList ddlExpectedResult = item.FindControl("ddlExpectedResult") as DropDownList;
        //                DropDownList ddlRating = item.FindControl("ddlRating") as DropDownList;
        //                TextBox TxtAppraisercmts = item.FindControl("TxtAppraisercmts") as TextBox;
        //                Label lblItemId = item.FindControl("lblItemId") as Label;

        //                if (string.IsNullOrEmpty(lblItemId.Text))
        //                {
        //                    lstItem = Appcompetency.AddItem();

        //                    lstItem["acmptAppraisalId"] = appraisalID;
        //                    lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;//Convert.ToInt32(lstItemAppraisal["ID"]);
        //                }
        //                else
        //                {
        //                    lstItem = Appcompetency.GetItemById(Convert.ToInt32(lblItemId.Text));
        //                }

        //                lstItem["acmptAppraisalId"] = appraisalID;
        //                lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;           //Convert.ToInt32(lstItemAppraisal["ID"]);
        //                lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
        //                lstItem["acmptExpectedResult"] = Convert.ToString(ddlExpectedResult.SelectedItem.Text);
        //                lstItem["acmptRating"] = Convert.ToString(ddlRating.SelectedItem.Text);
        //                //lstItem["acmptDescription"] = Convert.ToString(txtDescription.Text);
        //                if (!string.IsNullOrEmpty(Convert.ToString(TxtAppraisercmts)))
        //                    lstItem["acmptAppraiserLatestComments"] = Convert.ToString(TxtAppraisercmts.Text);
        //                //lstItem["acmptReviewerLatestComments"] = Convert.ToString(txtWeightage.Text);
        //                //lstItem["acmptAppraisalPhase"] = Convert.ToString(txtDescription.Text);  

        //                objWeb.AllowUnsafeUpdates = true;
        //                lstItem.Update();
        //                objWeb.AllowUnsafeUpdates = false;

        //            }
        //        }
        //        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Submitted Successfully')</script>");
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //public void SavePDP(int appraisalID, int appraisalPhaseId)
        //{
        //    try
        //    {
        //        using (SPWeb objWeb = SPContext.Current.Site.OpenWeb())
        //        {
        //            objWeb.AllowUnsafeUpdates = true;

        //            SPList lstPDP = objWeb.Lists["Appraisal Development Measures"];
        //            SPListItem listItem;

        //            foreach (RepeaterItem item in RptDevelopmentMesure.Items)
        //            {
        //                Label lblSno = item.FindControl("lblSno") as Label;
        //                Label LblDeId = item.FindControl("LblDeId") as Label;

        //                Label lblwhen = item.FindControl("lblwhen") as Label;
        //                Label lblwhat = item.FindControl("lblwhat") as Label;
        //                Label lblNextSteps = item.FindControl("lblNextSteps") as Label;
        //                Label LblDemcomments = item.FindControl("LblDemcomments") as Label;

        //                DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
        //                TextBox txtwhat = item.FindControl("txtwhat") as TextBox;
        //                TextBox txtNextSteps = item.FindControl("txtNextSteps") as TextBox;
        //                TextBox TxtDevComments = item.FindControl("TxtDevComments") as TextBox;

        //                if (string.IsNullOrEmpty(LblDeId.Text))
        //                {
        //                    listItem = lstPDP.AddItem();

        //                    listItem["pdpAppraisalID"] = appraisalID;
        //                    listItem["pdpAppraisalPhaseID"] = appraisalPhaseId;
        //                }
        //                else
        //                {
        //                    listItem = lstPDP.GetItemById(Convert.ToInt32(LblDeId.Text));
        //                }

        //                listItem["pdpWhat"] = Convert.ToString(txtwhat.Text);
        //                listItem["pdpWhen"] = Convert.ToDateTime(dc.SelectedDate.Date.ToString("dd-MMM-yyyy"));
        //                listItem["pdpNextSteps"] = Convert.ToString(txtNextSteps.Text);
        //                if (!string.IsNullOrEmpty(Convert.ToString(TxtDevComments)))
        //                    listItem["pdpH2AppraiserComments"] = Convert.ToString(TxtDevComments.Text);
        //                objWeb.AllowUnsafeUpdates = true;
        //                listItem.Update();
        //                objWeb.AllowUnsafeUpdates = false;

        //            }


        //        }

        //        // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Submited Successfully')</script>");


        //    }
        //    catch (Exception ex)
        //    {
        //        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
        //    }

        //}


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
                if ((item.FindControl("SPDateLastDate") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtwhat") as TextBox).Text != string.Empty && (item.FindControl("txtNextSteps") as TextBox).Text != string.Empty)
                {
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                    dr["pdpWhat"] = (item.FindControl("txtwhat") as TextBox).Text;
                    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                    string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                    dr["pdpWhen"] = strDate;
                    dr["pdpNextSteps"] = (item.FindControl("txtNextSteps") as TextBox).Text;
                    dr["pdpH2AppraiserComments"] = (item.FindControl("TxtDevComments") as TextBox).Text;
                    i++;
                }

                else
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Please fill what & next step')</script>");
                    //return;
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
            if (sNo != RptDevelopmentMesure.Items.Count)
            {
                foreach (RepeaterItem item in RptDevelopmentMesure.Items)
                {
                    DataRow dr = this.DtDevelopmentmesure.Rows[i];
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                    dr["pdpWhat"] = (item.FindControl("txtwhat") as TextBox).Text;
                    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                    string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                    dr["pdpWhen"] = strDate;
                    dr["pdpNextSteps"] = (item.FindControl("txtNextSteps") as TextBox).Text;
                    dr["pdpH2AppraiserComments"] = (item.FindControl("TxtDevComments") as TextBox).Text;
                    i++;
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

            if (this.DtDevelopmentmesure.Rows.Count >= 2)
            {
                MorePDP(this.DtDevelopmentmesure.Rows.Count);
                // SelectedDropDown(this.dummyTable);
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
                Label lblAppraiseeComments1 = rptItem.FindControl("lblAppraiseeComments1") as Label;

                DropDownList ddlCategories = rptItem.FindControl("ddlCategories") as DropDownList;
                TextBox txtGoal = rptItem.FindControl("txtGoal") as TextBox;
                TextBox txtDescription = rptItem.FindControl("txtDescription") as TextBox;
                TextBox TxtAprComments = rptItem.FindControl("TxtAprComments") as TextBox;
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
                    dr["agEvaluation"] = Convert.ToDecimal(txtEvaluation.Text);

                    if (!string.IsNullOrEmpty(txtWeightage.Text))
                        dr["agWeightage"] = Convert.ToDecimal(txtWeightage.Text);
                }
                else
                {
                    lblScore1.Text = 0.ToString();
                }

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
                    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                    string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
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
                    dr2["agAppraiseeLatestComments"] = (item.FindControl("lblAppraiseeComments1") as Label).Text;
                    i++;
                }

                //TextBox dummyFld = new TextBox();
                //dummyFld.CssClass = "validation";
                //dummyFld.Style.Add("display", "none");
                //rptItem.Controls.Add(dummyFld);

                //Panel pnl = (Panel)rptItem.FindControl("testpnl");
                //TextBox dummyFld = new TextBox();
                //dummyFld.CssClass = "validation";
                //dummyFld.Style.Add("display", "none");
                //                                                                                                                                                                                                    ][
                //pnl.Controls.Add(dummyFld);

                Panel pnl = (Panel)rptItem.FindControl("testpnl");
                TextBox dummyFld = new TextBox();
                dummyFld.CssClass = "validation";
                dummyFld.Style.Add("display", "none");
                pnl.Controls.Add(dummyFld);

                txtEvaluation.Focus();
                lblH2Score.Visible = true;
                lblH2Score.Text = Convert.ToString(total);
                decimal finalValue;
                if (!string.IsNullOrEmpty(LblH1score1.Text))
                    finalValue = (Convert.ToDecimal(total) + Convert.ToDecimal(LblH1score1.Text)) / 2;
                else
                    finalValue = Convert.ToDecimal(total);
                lblH2Score.Text = Convert.ToString(total);//
                LblFS.Text = finalValue.ToString();
                Lblfr.Text = CommonMaster.GetRating(finalValue);

                //ScoreFinal(Convert.ToInt32(hfAppraisalID.Value));
                //GetRating(finalValue);

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser Review");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }

        }

        protected void LnkDetails_Click(object sender, EventArgs e)
        {
            string message = SPContext.Current.Web.Url + "_layouts/VFSProjectH1/HRView.aspx?AppId=" + hfAppraisalID.Value + "&IsDlg=1";
            string radalertscript = "<script language='javascript'>function f(){ OpenDialog('" + message + "'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
            //Response.Redirect(SPContext.Current.Site.Url + "_Layouts/H1AppraiserReview/H1AppraiserReview.aspx");
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
                    lstItem["appFinalScore"] = Convert.ToString(LblFS.Text);
                    lstItem["appFinalRating"] = Convert.ToString(Lblfr.Text);
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

                    SPListItem lstItem;
                    SPList lstGoalSettings = objWeb.Lists[listName];
                    int mandatoryItemCount = 0;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;//upendra change label
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox TxtEvaluation = item.FindControl("TxtEvaluation") as TextBox;
                        Label Lblscore1 = item.FindControl("Lblscore1") as Label;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        Label lblAppraiseeComments = item.FindControl("lblAppraiseeComments1") as Label;//upendra changes the label name
                        //Label LblAprCmts = item.FindControl("LblAprCmts") as Label;
                        TextBox TxtAprComments = item.FindControl("TxtAprComments") as TextBox;
                        Label LblReviewercomments = item.FindControl("LblReviewercomments") as Label;

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
                            lstItem["agDueDate"] = h1EndDate;
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
                        if (!string.IsNullOrEmpty(Convert.ToString(TxtAprComments)))
                            lstItem["agAppraiserLatestComments"] = TxtAprComments.Text;
                        if (!string.IsNullOrEmpty(Convert.ToString(LblReviewercomments)))
                            lstItem["agReviewerLatestComments"] = LblReviewercomments.Text;
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];

                        lstItem = lstAppraisalPhases.GetItemById(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        lstItem["aphScore"] = Convert.ToDecimal(lblH2Score.Text);

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                        //lstItem = lstCommentsHistory.AddItem();
                        //lstItem["chCommentFor"] = "Goals approved";
                        //lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                        //if (!string.IsNullOrEmpty(Convert.ToString(TxtAprComments)))
                        //    lstItem["chComment"] = TxtAprComments.Text;
                        //lstItem["chCommentedBy"] = this.currentUser;
                        //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                        //lstItem["chActor"] = lblEmpNameValue.Text;
                        //lstItem["chRole"] = "Appraiser";

                        //objWeb.AllowUnsafeUpdates = true;
                        //lstItem.Update();
                        //objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Goal, " + txtGoal.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), Convert.ToString(TxtAprComments.Text), this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");
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
                        Label lblDescriptionValue1 = item.FindControl("lblDescriptionValue") as Label;
                        Label lblAppraise = item.FindControl("lblAppraise") as Label;
                        Label LblReviewer = item.FindControl("LblReviewer") as Label;
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
                        lstItem["acmptReviewerLatestComments"] = Convert.ToString(LblReviewer.Text);
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
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in RptDevelopmentMesure.Items)
                    {
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtwhat = item.FindControl("txtwhat") as TextBox;
                        TextBox txtNextSteps = item.FindControl("txtNextSteps") as TextBox;
                        TextBox TxtDevComments = item.FindControl("TxtDevComments") as TextBox;
                        Label lblAppraise = item.FindControl("lblAppraise") as Label;

                        CurrentListItem = oList.AddItem();

                        CurrentListItem["pdpAppraisalID"] = appraisalID;
                        CurrentListItem["pdpAppraisalPhaseID"] = appraisalPhaseId;

                        CurrentListItem["pdpWhat"] = Convert.ToString(txtwhat.Text);
                        //CurrentListItem["pdpWhen"] = Convert.ToDateTime(dc.SelectedDate.Date.ToString("dd-MMM-yyyy"));
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
                        CurrentListItem["pdpH2AppraiseeComments"] = lblAppraise.Text;

                        if (!string.IsNullOrEmpty(Convert.ToString(TxtDevComments)))
                            CurrentListItem["pdpH2AppraiserComments"] = Convert.ToString(TxtDevComments.Text);
                        CurrentListItem["Author"] = this.currentUser;

                        oWeb.AllowUnsafeUpdates = true;
                        CurrentListItem.Update();
                        oWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("PDP, " + txtwhat.Text, Convert.ToInt32(hfAppraisalPhaseID.Value), TxtDevComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");
                    }
                }
            }

        }

        #region Krishna 26062013123

        private DataTable dumytable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("SNo", typeof(string));
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("pipAppraisalPhaseID", typeof(string));
            tbl.Columns.Add("pipPerformanceIssue", typeof(string));
            tbl.Columns.Add("pipExpectedachivement", typeof(string));
            tbl.Columns.Add("pipTimeFrame", typeof(string));
            tbl.Columns.Add("pipMidTermActualResult", typeof(string));
            tbl.Columns.Add("pipMidTermAppraisersAssessment", typeof(string));
            tbl.Columns.Add("pipFinalAcutualResult", typeof(string));
            tbl.Columns.Add("pipFinalAppraisersAssesment", typeof(string));
            DataRow dr = tbl.NewRow();
            dr["SNo"] = 1;
            dr["ID"] = 0;
            tbl.Rows.Add(dr);
            ViewState["dummyTable"] = tbl;
            return tbl;
        }

        protected void btnPIPAdd_Click(object sender, EventArgs e)
        {
            int goulsCount = Convert.ToInt32(piphfAppraiselid.Value) + 1;
            piphfAppraiselid.Value = goulsCount.ToString();
            this.dummyTable = ViewState["dummyTable"] as DataTable;
            int i = 0;
            //flag = Convert.ToString(ViewState["flag"]);
            foreach (RepeaterItem item in rptpip.Items)
            {
                DataRow dr = this.dummyTable.Rows[i];
                if ((item.FindControl("txtPerformanceIssu") as TextBox).Text != string.Empty && (item.FindControl("txtExpectedAchivements") as TextBox).Text != string.Empty && (item.FindControl("txtTimeFrame") as TextBox).Text != string.Empty)// && (item.FindControl("txtActualResultmidterm") as TextBox).Text != string.Empty && (item.FindControl("txtAppraisersAssessmentmidterm") as TextBox).Text != string.Empty && (item.FindControl("txtActualResultfinelterm") as TextBox).Text != string.Empty && (item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox).Text != string.Empty)// && (item.FindControl("textaction") as LinkButton).Text != string.Empty)
                {
                    foreach (RepeaterItem items in rptpip.Items)
                    {
                        TextBox txtActualResultmidterm = items.FindControl("txtActualResultmidterm") as TextBox;
                        TextBox txtAppraisersAssessmentmidterm = items.FindControl("txtAppraisersAssessmentmidterm") as TextBox;
                        TextBox txtActualResultfinelterm = items.FindControl("txtActualResultfinelterm") as TextBox;
                        TextBox txtAppraisersAssessmentfinalterm = items.FindControl("txtAppraisersAssessmentfinalterm") as TextBox;
                        TextBox txtPerformanceIssu = items.FindControl("txtPerformanceIssu") as TextBox;
                        TextBox txtExpectedAchivements = items.FindControl("txtExpectedAchivements") as TextBox;
                        TextBox txtTimeFrame = items.FindControl("txtTimeFrame") as TextBox;
                    }
                    dr["SNo"] = (item.FindControl("lblSno") as Label).Text;
                    dr["pipPerformanceIssue"] = (item.FindControl("txtPerformanceIssu") as TextBox).Text;
                    dr["pipExpectedachivement"] = (item.FindControl("txtExpectedAchivements") as TextBox).Text;
                    dr["pipTimeFrame"] = (item.FindControl("txtTimeFrame") as TextBox).Text;
                    dr["pipMidTermActualResult"] = (item.FindControl("txtActualResultmidterm") as TextBox).Text;
                    dr["pipMidTermAppraisersAssessment"] = (item.FindControl("txtAppraisersAssessmentmidterm") as TextBox).Text;
                    dr["pipFinalAcutualResult"] = (item.FindControl("txtActualResultfinelterm") as TextBox).Text;
                    dr["pipFinalAppraisersAssesment"] = (item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox).Text;
                    dr["pipAppraisalPhaseID"] = Convert.ToInt32(piphfAppraiselid.Value);
                    i++;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Fill Atleast 1 PIP')</script>");
                    return;
                }
            }
            DataRow dr1 = this.dummyTable.NewRow();
            dr1["SNo"] = this.dummyTable.Rows.Count + 1;
            dr1["ID"] = 0;
            this.dummyTable.Rows.Add(dr1);
            rptpip.DataSource = this.dummyTable;
            rptpip.DataBind();

            ViewState["dummyTable"] = null;
            ViewState["dummyTable"] = this.dummyTable;
            if (this.dummyTable.Rows.Count >= 1)
            {
                Morepip(this.dummyTable.Rows.Count);
            }
        }

        private void Morepip(int rowsCount)
        {
            foreach (RepeaterItem item in rptpip.Items)
            {
                if (item.ItemIndex >= 1)
                {
                    LinkButton lnkDelete = (LinkButton)item.FindControl("lnkPIPDelete");
                    //lnkDelete.Visible = true;
                }
            }
        }

        protected void lnkPIPDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int sNo = Convert.ToInt32(lnk.CommandArgument);
            DataTable dt = ViewState["dummyTable"] as DataTable;
            dt.Rows.Clear();
            foreach (RepeaterItem item in rptpip.Items)
            {
                Label lblsno = item.FindControl("lblSno") as Label;
                Label lblId = item.FindControl("lblID") as Label;
                TextBox txtPerformanceIssu = item.FindControl("txtPerformanceIssu") as TextBox;
                TextBox txtExpectedAchivements = item.FindControl("txtExpectedAchivements") as TextBox;
                TextBox txtTimeFrame = item.FindControl("txtTimeFrame") as TextBox;
                DataRow dr = dt.NewRow();
                dr["SNo"] = lblsno.Text;
                dr["ID"] = lblId.Text;
                dr["pipPerformanceIssue"] = txtPerformanceIssu.Text;
                dr["pipExpectedachivement"] = txtExpectedAchivements.Text;
                dr["pipTimeFrame"] = txtTimeFrame.Text;
                dt.Rows.Add(dr);
            }
            dt.Rows.RemoveAt(sNo - 1);
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = dt.Rows[i];
                dr["SNo"] = i + 1;
                i++;
            }
            rptpip.DataSource = dt;
            rptpip.DataBind();
            ViewState["dummyTable"] = null;
            ViewState["dummyTable"] = dt;
            //foreach (RepeaterItem item in rptpip.Items)
            //{
            //    TextBox txtPerformanceIssu = item.FindControl("txtPerformanceIssu") as TextBox;
            //    TextBox txtExpectedAchivements = item.FindControl("txtExpectedAchivements") as TextBox;
            //    TextBox txtTimeFrame = item.FindControl("txtTimeFrame") as TextBox;
            //    txtPerformanceIssu.Visible = true;
            //    txtExpectedAchivements.Visible = true;
            //    txtTimeFrame.Visible = true;
            //}
            if (dt.Rows.Count >= 1)
            {
                Morepip(dt.Rows.Count);
            }
        }

        protected void btnPIPCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Web.Url, false);
            //Response.End();
        }

        public void SaveItem(bool NewItem, int ItemID)
        {

            using (SPSite objSite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = objSite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;
                    SPList lstPOCreation = objWeb.Lists["PIP"];
                    SPListItem lstItem;
                    if (NewItem)
                        lstItem = lstPOCreation.AddItem();
                    else
                    {
                        lstItem = lstPOCreation.GetItemById(ItemID);
                    }
                    ////SPListItem lstItemAppraisal = this.GetAppraisalId();
                    SPList lstpip = objWeb.Lists["PIP"];
                    foreach (RepeaterItem item in rptpip.Items)
                    {
                        TextBox txtPerformanceIssu = item.FindControl("txtPerformanceIssu") as TextBox;
                        TextBox txtExpectedAchivements = item.FindControl("txtExpectedAchivements") as TextBox;
                        TextBox txtTimeFrame = item.FindControl("txtTimeFrame") as TextBox;
                        TextBox txtActualResultmidterm = item.FindControl("txtActualResultmidterm") as TextBox;
                        TextBox txtAppraisersAssessmentmidterm = item.FindControl("txtAppraisersAssessmentmidterm") as TextBox;
                        TextBox txtActualResultfinelterm = item.FindControl("txtActualResultfinelterm") as TextBox;
                        TextBox txtAppraisersAssessmentfinalterm = item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox;
                        Label lblID = item.FindControl("lblID") as Label;
                        if (!string.IsNullOrEmpty(lblID.Text) && lblID.Text != "0")
                        {
                            lstItem = lstpip.GetItemById(Convert.ToInt32(lblID.Text));
                        }
                        else
                        {
                            lstItem = lstpip.AddItem();
                        }
                        lstItem["pipAppraisalID"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["pipPerformanceIssue"] = Convert.ToString(txtPerformanceIssu.Text);
                        lstItem["pipExpectedachivement"] = Convert.ToString(txtExpectedAchivements.Text);
                        lstItem["pipTimeFrame"] = Convert.ToString(txtTimeFrame.Text);
                        lstItem["pipMidTermActualResult"] = Convert.ToString(txtActualResultmidterm.Text);
                        lstItem["pipMidTermAppraisersAssessment"] = Convert.ToString(txtAppraisersAssessmentmidterm.Text);
                        lstItem["pipFinalAcutualResult"] = Convert.ToString(txtActualResultfinelterm.Text);
                        lstItem["pipFinalAppraisersAssesment"] = Convert.ToString(txtAppraisersAssessmentfinalterm.Text);
                        lstItem["pipAppraisalPhaseID"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }

                }

            }
            string sss = "'PIP saved Successfully'";
            Context.Response.Write("<script language=\"javascript\">alert('PIP saved Successfully'); window.location.href='" + CommonMaster.DashBoardUrl + "'</script>");
            //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('PIP saved Successfully')</script>");

        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (Request.Params["ID"] != null)
                    {
                        SaveItem(false, Convert.ToInt32(Request.Params["ID"]));
                    }
                    else
                    {
                        SaveItem(true, 0);
                    }
                });
            ////Response.Redirect(SPContext.Current.Web.Url);
        }

        #endregion
    }
}
