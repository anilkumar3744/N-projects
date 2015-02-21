using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.H2AppraiserEvaluation
{
    public partial class H2AppraiserPIPEdit : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, dtPip;
        SPUser currentUser;
        double score;
        string pipbtn;
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
                hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    //DvREvaluation.Visible = true;
                    //dvCompetencies.Visible = false;
                    //saftymeasurementdevelopment.Visible = false;
                    //pipdiv.Visible = false;
                    DvREvaluation.Style["display"] = "block";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "none";
                    divPIP.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    DvREvaluation.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "block";
                    saftymeasurementdevelopment.Style["display"] = "none";
                    divPIP.Style["display"] = "none";
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    DvREvaluation.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "block";
                    divPIP.Style["display"] = "none";
                }
                else
                {
                    DvREvaluation.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "none";
                    saftymeasurementdevelopment.Style["display"] = "none";
                    divPIP.Style["display"] = "block";
                }

                if (!this.IsPostBack)
                {
                    SPList lstAppraisalTasks;
                    SPListItem taskItem;
                    SPUser appraisee;
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            lstAppraisalTasks = currentWeb.Lists["Appraisals"];
                            taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["AppId"]));  //

                            if (taskItem["appFinalScore"] != null)
                            {
                                lblFS.Text = Convert.ToString(taskItem["appFinalScore"]);
                            }
                            if (taskItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(taskItem["appFinalRating"]);
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
                            //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), currentWeb))
                            //{
                            //    lnkSave.Visible = false;
                            //}

                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery(); //
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0]; //
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);
                            ViewState["PhaseId"] = Convert.ToString(phaseItem["ID"]);//
                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]);
                            }

                            SPQuery phasesQueryH1 = new SPQuery();
                            phasesQueryH1.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";

                            SPListItemCollection phasesCollectionH1 = lstAppraisalPhases.GetItems(phasesQueryH1);  //
                            if (phasesCollectionH1.Count > 0)
                            {
                                SPListItem phaseItemH1 = phasesCollectionH1[0];
                                if (phaseItemH1["aphScore"] != null)
                                    lblscore.Text = Convert.ToString(phaseItemH1["aphScore"]);
                            }

                            if (phaseItem["aphScore"] != null)
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]); //                          

                            if (phaseItem["aphSignoffonbehalfcomments"] != null)
                                lblSignOffComments.Text = Convert.ToString(phaseItem["aphSignoffonbehalfcomments"]);
                            ////if (phaseItem["aphHRReviewLatestComments"] != null)
                            ////    lblCommentsFinal.Text = Convert.ToString(phaseItem["aphHRReviewLatestComments"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["aphIsPIP"])))
                            {
                                pipbtn = Convert.ToString(phaseItem["aphIsPIP"]);
                                if (pipbtn == "True")
                                {
                                    tbpnlpip.Visible = true;
                                    divPIP.Visible = true;
                                }
                                else
                                {
                                    tbpnlpip.Visible = false;
                                    divPIP.Visible = false;
                                }
                            }

                            else
                            {
                                tbpnlpip.Visible = false;
                                divPIP.Visible = false;
                            }
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            appraisee = currentWeb.EnsureUser(strAprraiseeName);
                            //if (appraisee.Name == this.currentUser.Name)
                            //{
                            //    lnkSave.Visible = false;
                            //}
                            //string id = string.Empty;
                            //string currentUser1 = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                            //SPListItem dtMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser1);
                            //if (dtMaster != null)
                            //{
                            //    if (!string.IsNullOrEmpty(Convert.ToString(dtMaster["EmployeeCode"])))
                            //        id = dtMaster["EmployeeCode"].ToString();

                            //    SPListItem spItem;
                            //    spItem = CommonMaster.GetMasterDetails("ReportingManagerEmployeeCode", id);
                            //    if (spItem != null)
                            //    {
                            //        lnkSave.Visible = true;
                            //    }
                            //    else
                            //        lnkSave.Visible = false;
                            //}
                            string id = string.Empty;
                            string currentUser1 = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                            SPListItem dtMaster = CommonMaster.GetMasterDetails("WindowsLogin", appraisee.LoginName.Split('\\')[1]);
                            if (dtMaster != null)
                            {
                                if (!string.IsNullOrEmpty(Convert.ToString(dtMaster["ReportingManagerEmployeeCode"])))
                                    id = dtMaster["ReportingManagerEmployeeCode"].ToString();

                                SPListItem spItem;
                                spItem = CommonMaster.GetMasterDetails("EmployeeCode", id);
                                SPUser appraiser = currentWeb.EnsureUser(Convert.ToString(spItem["WindowsLogin"]));
                                if (appraiser.LoginName == currentWeb.CurrentUser.LoginName)
                                {
                                    lnkSave.Visible = true;
                                    btnPIPAdd.Visible = true;
                                }
                                else
                                {
                                    lnkSave.Visible = false;
                                    btnPIPAdd.Visible = false;
                                }
                            }
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
                        this.hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                        ViewState["Appraisals"] = this.dummyTable;
                        RptRevaluation.DataSource = ViewState["Appraisals"];
                        RptRevaluation.DataBind();


                        this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dtCompetencies != null)
                        {
                            this.dtCompetencies.Columns.Add("SNo", typeof(string));

                            int j = 1;
                            foreach (DataRow dr in this.dtCompetencies.Rows)
                            {
                                dr["SNo"] = j;
                                j++;
                            }
                        }
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
                        rptsaftymeasurementdevelopment.DataSource = this.DtDevelopmentmesure;
                        rptsaftymeasurementdevelopment.DataBind();
                        string tabValue = Convert.ToString(Request.Params["TaskID"]);
                        if (pipbtn == "True")
                        {
                            this.dtPip = CommonMaster.GetPIPDetails(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                            if (dtPip != null && dtPip.Rows.Count > 0)
                            {
                                this.dtPip.Columns.Add("SNo", typeof(string));
                                int z = 1;
                                foreach (DataRow dr in this.dtPip.Rows)
                                {
                                    dr["SNo"] = z;
                                    z++;
                                }
                                rptPIP.DataSource = this.dtPip;
                                rptPIP.DataBind();

                                foreach (RepeaterItem item in rptPIP.Items)
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
                                    LinkButton lnkDelete = item.FindControl("lnkPIPDelete") as LinkButton;

                                    if (tabValue == "1")
                                    {
                                        txtPerformanceIssu.Visible = true;
                                        txtExpectedAchivements.Visible = true;
                                        txtTimeFrame.Visible = true;
                                        if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text))
                                        {
                                            lnkDelete.Visible = false;
                                            btnPIPAdd.Visible = false;
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
                                            lnkDelete.Visible = false;
                                            btnPIPAdd.Visible = false;
                                            lblActualResultmidterm.Visible = true;
                                            txtActualResultfinelterm.Visible = true;
                                            txtActualResultmidterm.Visible = false;
                                            txtAppraisersAssessmentmidterm.Visible = false;
                                            txtAppraisersAssessmentfinalterm.Visible = true;
                                            lblAppraisersAssessmentmidterm.Visible = true;
                                        }
                                        if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text) && !string.IsNullOrEmpty(txtActualResultfinelterm.Text) && !string.IsNullOrEmpty(txtAppraisersAssessmentfinalterm.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text))
                                        {
                                            lnkDelete.Visible = false;
                                            btnPIPAdd.Visible = false;
                                            txtActualResultfinelterm.Visible = false;
                                            txtAppraisersAssessmentfinalterm.Visible = false;
                                            btnPIPAdd.Visible = false;
                                            lnkSave.Visible = false;
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
                                        lnkSave.Visible = false;
                                    }

                                }
                            }
                            else
                            {
                                if (tabValue == "1")
                                    dtPip = dumytable();
                                else
                                {
                                    divPIP.Style["display"] = "none";
                                    tbpnlpip.Visible = false;
                                }
                                //{
                                //    btnPIPAdd.Visible = false;
                                //    lnkSave.Visible = false;
                                //}
                                rptPIP.DataSource = dtPip;
                                rptPIP.DataBind();
                            }

                            ViewState["PIP"] = dtPip;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS HR View");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(SPContext.Current.Web.Url,false);
            //Response.End();

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
            ViewState["PIP"] = tbl;
            return tbl;
        }

        protected void btnPIPAdd_Click(object sender, EventArgs e)
        {
            int goulsCount = Convert.ToInt32(piphfAppraiselid.Value) + 1;
            piphfAppraiselid.Value = goulsCount.ToString();
            this.dummyTable = ViewState["PIP"] as DataTable;
            int i = 0;
            //flag = Convert.ToString(ViewState["flag"]);
            foreach (RepeaterItem item in rptPIP.Items)
            {
                DataRow dr = this.dummyTable.Rows[i];
                if ((item.FindControl("txtPerformanceIssu") as TextBox).Text != string.Empty && (item.FindControl("txtExpectedAchivements") as TextBox).Text != string.Empty && (item.FindControl("txtTimeFrame") as TextBox).Text != string.Empty)// && (item.FindControl("txtActualResultmidterm") as TextBox).Text != string.Empty && (item.FindControl("txtAppraisersAssessmentmidterm") as TextBox).Text != string.Empty && (item.FindControl("txtActualResultfinelterm") as TextBox).Text != string.Empty && (item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox).Text != string.Empty)// && (item.FindControl("textaction") as LinkButton).Text != string.Empty)
                {
                    foreach (RepeaterItem items in rptPIP.Items)
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
            rptPIP.DataSource = this.dummyTable;
            rptPIP.DataBind();

            ViewState["PIP"] = null;
            ViewState["PIP"] = this.dummyTable;
            if (this.dummyTable.Rows.Count >= 1)
            {
                Morepip(this.dummyTable.Rows.Count);
            }
        }

        private void Morepip(int rowsCount)
        {
            foreach (RepeaterItem item in rptPIP.Items)
            {
                if (item.ItemIndex >= 1)
                {
                    LinkButton lnkDelete = (LinkButton)item.FindControl("lnkPIPDelete");
                    lnkDelete.Visible = true;
                }
            }
        }

        protected void lnkPIPDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            int sNo = Convert.ToInt32(lnk.CommandArgument);
            DataTable dt = ViewState["PIP"] as DataTable;
            dt.Rows.Clear();
            foreach (RepeaterItem item in rptPIP.Items)
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
            rptPIP.DataSource = dt;
            rptPIP.DataBind();
            ViewState["PIP"] = null;
            ViewState["PIP"] = dt;
            //foreach (RepeaterItem item in rptPIP.Items)
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
                    foreach (RepeaterItem item in rptPIP.Items)
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
                        lstItem["pipAppraisalPhaseID"] = Convert.ToInt32(ViewState["PhaseId"].ToString());
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }

                }

            }
            Context.Response.Write("<script language=\"javascript\">alert('" + CustomMessages.PIPSubmit + "'); window.location.href='" + CommonMaster.DashBoardUrl + "'</script>");
            //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('PIP saved Successfully')</script>");

        }

        protected void btnSave_Click(object sender, EventArgs e)
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
            ///Response.Redirect(SPContext.Current.Web.Url);
        }

        #endregion
    }
}
