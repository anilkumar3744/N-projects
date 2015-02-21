using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace VFS.PMS.ApplicationPages.Layouts.H2AppraiserEvaluationViewmode
{
    public partial class H2AppraiserEvaluationViewmode : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure;
        SPUser currentUser;
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
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
                else
                {
                    //dvGoalSettings.Visible = false;
                    //dvCompetencies.Visible = false;
                    //divdevelopmentmessures.Visible = false;
                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    divdevelopmentmessures.Style["display"] = "none";
                    pipdiv.Style["display"] = "block";

                    PostBackTrigger trigger = new PostBackTrigger();
                    trigger.ControlID = "lnksave";
                    updatepanel.Triggers.Add(trigger);
                }
                if (!this.IsPostBack)
                {
                    //int score1 = Convert.ToInt32(LblH1score1.Text);
                    //int score2 = Convert.ToInt32(lblH2Score.Text);
                    //LblFS.Text = ((score1 + score2) / 2).ToString();

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

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //
                            if (appraisalItem["appFinalScore"] != null)
                            {
                                LblFS.Text = Convert.ToString(appraisalItem["appFinalScore"]);
                            }
                            if (appraisalItem["appFinalRating"] != null)
                            {
                                Lblfr.Text = Convert.ToString(appraisalItem["appFinalRating"]);
                            }
                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery();//
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            if (phasesCollection.Count > 0)
                            {
                                SPListItem phaseItem = phasesCollection[0]; //
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

                            string tabValue = Convert.ToString(Request.Params["TaskID"]);

                            #region Developed By Krishna 240620130539
                            DataTable dtPIPDetails = new DataTable();
                            SPListItem spIMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser.LoginName.Split('\\')[1]);
                            string id = spIMaster["EmployeeCode"].ToString();
                            SPListItem spItem = CommonMaster.GetMasterDetails("ReportingManagerEmployeeCode", id);
                            SPListItem spIAppraisal = CommonMaster.GetAppraisalCheck("ID", hfAppraisalID.Value);
                            if (spItem != null)
                            {

                                if (phaseItemH2["aphIsPIP"] != null && phaseItemH2["aphIsPIP"].ToString() != string.Empty)
                                {
                                    if (phaseItemH2["aphIsPIP"].ToString() == "True")
                                    {
                                        tbpnlpip.Visible = true;
                                        Button10.Visible = false;
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
                                                        btnPIPAdd.Visible = true;
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
                                                        btnPIPAdd.Visible = true;
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
                                            rptpip.DataSource = dtPIPDetails;
                                            rptpip.DataBind();
                                        }

                                    }
                                }

                            }


                            #endregion

                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName);

                            // SPList lstAppraisala = currentWeb.Lists[new Guid(Request.Params["List"])];9

                            //appraisalItem = lstAppraisala.GetItemById(itemID);9

                            // appraisee = currentWeb.EnsureUser(Convert.ToString(appraisalItem["Author"]).Split('#')[1]);
                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);

                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, Web))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                return;
                            }
                            //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value)))
                            //{
                            //    string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                            //    Response.Redirect(url);
                            //}

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

                        // this.dummyTable = CommonMasters.GetGoalsDetails(itemID);
                        this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        this.dummyTable.Columns.Add("SNo", typeof(string));

                        int ii = 1;
                        int mandatoryGoalCount = 0; //
                        foreach (DataRow dr in this.dummyTable.Rows)
                        {
                            dr["SNo"] = ii;
                            ii++;
                            if (dr["IsMandatory"].ToString() == "True")  //
                            {
                                mandatoryGoalCount++;//
                            }//
                        }
                        hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();  //

                        ViewState["Appraisals"] = this.dummyTable;
                        rptGoalSettings.DataSource = ViewState["Appraisals"];
                        rptGoalSettings.DataBind();
                        BindDateTimeControl("last");

                        // if (this.dummyTable.Rows.Count >= 5)


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


                        this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        this.DtDevelopmentmesure.Columns.Add("SNo", typeof(string));
                        int k = 1;
                        foreach (DataRow dr in this.DtDevelopmentmesure.Rows)
                        {
                            dr["SNo"] = k;
                            k++;
                        }
                        ViewState["PDP"] = this.DtDevelopmentmesure;
                        RptDevelopmentMesure.DataSource = this.DtDevelopmentmesure;
                        RptDevelopmentMesure.DataBind();


                    }
                }
                //using (SPWeb oWeb = SPContext.Current.Site.OpenWeb())
                //{

                //    SPList lstAppraisalPhases = oWeb.Lists["Appraisal Phases"];
                //    SPQuery q = new SPQuery();
                //    q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(Request.Params["ID"]) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where></Query>";
                //    SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                //    SPListItem lstItem = coll[0];

                //    lblH2Score.Text = Convert.ToString(lstItem["aphScore"]);
                //}

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser Evaluation_SaveasPDP");
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
                    if (item.ItemIndex < count)
                    {
                        DataRow dr = dt.Rows[i];
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl,false); 
            //Response.End();

            //Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            //Context.Response.Flush();
            //Context.Response.End();

        }

        protected void LnkDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?AppId=" + Convert.ToString(Request.Params["AppId"])),false);
            //Response.End();

            //string message = SPContext.Current.Site.Url + "_layouts/VFSProjectH1/HRView.aspx?AppId=" + hfAppraisalID.Value + "&IsDlg=1";
            //string radalertscript = "<script language='javascript'>function f(){ OpenDialog('" + message + "'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
            // Response.Redirect(SPContext.Current.Site.Url + "_layouts/VFSProjectH1/HRView.aspx");
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
            Response.Redirect(SPContext.Current.Web.Url,false); 
            //Response.End();

        }

        public void SaveItem(bool NewItem, int ItemID)
        {
            //try
            //{
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
                // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('PIP saved Successfully')</script>");
            //}
            //catch (Exception ex)
            //{
            //    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            //}
        }

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (Request.Params["ID"] != null)
            {
                SaveItem(false, Convert.ToInt32(Request.Params["ID"]));
            }
            else
            {
                SaveItem(true, 0);
            }
            ////Response.Redirect(SPContext.Current.Web.Url);
        }

        #endregion
    }
}
