using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace VFS.PMS.ApplicationPages.Layouts.AppraiserEvaluationViewMode
{
    public partial class H1AppraiserEvaluationView : LayoutsPageBase
    {
        DataTable dummyTable;
        SPUser currentUser;
        string flag;
        DataTable DummyTable, dtCompetencies, DtDevelopmentmesure;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    dvGoalSettings.Visible = true;
                    dvCompetencies.Visible = false;
                    divdevelopmentmessures.Visible = false;
                    pipdiv.Visible = false;
                    btnCancel.Visible = true;
                    //tbpnlpip.Visible = false;

                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = true;
                    divdevelopmentmessures.Visible = false;
                    pipdiv.Visible = false;
                    btnCancel.Visible = true;
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = false;
                    divdevelopmentmessures.Visible = true;
                    pipdiv.Visible = false;
                    btnCancel.Visible = true;
                }
                else
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = false;
                    divdevelopmentmessures.Visible = false;
                    pipdiv.Visible = true;
                    btnCancel.Visible = false;
                    PostBackTrigger trigger = new PostBackTrigger();
                    trigger.ControlID = "lnkSave";
                    upnlInitialGoalSetting.Triggers.Add(trigger);
                }

                if (!this.IsPostBack)
                {
                    goalscompetenciespdp();

                    SPListItem appraisalItem;
                    SPUser appraisee;

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            this.currentUser = SPContext.Current.Web.CurrentUser;

                            //lstAppraisalTasks = currentWeb.Lists["VFSAppraisalTasks"];
                            //taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));
                            //piphfAppraiselid.Value = Convert.ToString(taskItem["tskAppraisalId"]);

                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];
                            piphfAppraiselid.Value = Convert.ToString(Request.Params["AppId"]);
                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(piphfAppraiselid.Value));



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






                            pipAppraisalPhaseID.Value = Convert.ToString(Request.Params["TaskID"]);



                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];
                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            //phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(piphfAppraiselid.Value) + "</Value></Eq><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + Convert.ToInt32(pipAppraisalPhaseID.Value) + "</Value></Eq></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            SPListItem phaseItem = phasesCollection[0];
                            pipAppraisalPhaseID.Value = phaseItem["ID"].ToString();
                            //pipAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);
                            //string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];

                            string tabValue = Convert.ToString(Request.Params["TaskID"]);

                            #region Commented By Krishna 240620130539

                            DataTable dtPIPDetails = new DataTable();
                            SPListItem spIMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser.LoginName.Split('\\')[1]);
                            string id = spIMaster["EmployeeCode"].ToString();
                            SPListItem spItem = CommonMaster.GetMasterDetails("ReportingManagerEmployeeCode", id);
                            SPListItem spIAppraisal = CommonMaster.GetAppraisalCheck("ID", hfAppraisalID.Value);
                            lblScoretotal.Text = Convert.ToString(spIAppraisal["appFinalScore"]);//.ToString( );
                            if (lblScoretotal.Text == null || lblScoretotal.Text == string.Empty)
                                lblScoretotal.Text = "0";////(phaseItem["aphScore"]);
                            if (spItem != null)
                            {
                                tbpnlpip.Visible = true;
                                // phaseItem["aphScore"] = Convert.ToString(lblScoretotal.Text);
                                Decimal Score = Convert.ToDecimal(lblScoretotal.Text);
                                //int rating = CommonMaster.GetRating(Score);
                                //if (rating < 2 && spIAppraisal["appAppraisalStatus"].ToString() == "H1 - Sign-off by Appraisee")
                                //{
                                //    SaveItem(true, 0);
                                //}
                                if (phaseItem["aphIsPIP"]!=null&&phaseItem["aphIsPIP"].ToString() != string.Empty)
                                {
                                    if (phaseItem["aphIsPIP"].ToString() == "True")
                                    {
                                        //SaveItem(true, 0);
                                        dtPIPDetails = GetPIPDetails(Convert.ToInt32(piphfAppraiselid.Value), Convert.ToInt32(pipAppraisalPhaseID.Value));
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
                                                        btnPIPAdd.Visible = true;
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
                                                pipdiv.Visible = false;
                                                tbpnlpip.Visible = false;
                                            }
                                            //{
                                            //    if (dtPIPDetails != null)
                                            //    {
                                            //        btnPIPAdd.Visible = false;
                                            //        lnksave.Visible = false;
                                            //    }
                                            //    else
                                            //        pipdiv.Visible = false;
                                            //}
                                            rptpip.DataSource = dtPIPDetails;
                                            rptpip.DataBind();
                                        }

                                    } 
                                }
                                #region COMMENTED
                                //else
                                //{
                                //    dtPIPDetails = GetPIPDetails(Convert.ToInt32(piphfAppraiselid.Value), Convert.ToInt32(pipAppraisalPhaseID.Value));
                                //    //tbpnlpip.Visible = false;
                                //    //pipdiv.Visible = false;
                                //    int i = 0;
                                //    dtPIPDetails.Columns.Add("SNo", typeof(string));
                                //    foreach (DataRow row in dtPIPDetails.Rows)
                                //    {
                                //        DataRow dr = dtPIPDetails.Rows[i];
                                //        dr["SNo"] = i + 1;
                                //        i++;
                                //    }
                                //    rptpip.DataSource = dtPIPDetails;
                                //    rptpip.DataBind();
                                //} 
                                #endregion
                            }
                            #region COMMENTED
                            //else
                            //{
                            //    dtPIPDetails = GetPIPDetails(Convert.ToInt32(piphfAppraiselid.Value), Convert.ToInt32(pipAppraisalPhaseID.Value));
                            //    //tbpnlpip.Visible = false;
                            //    //pipdiv.Visible = false;
                            //    if (dtPIPDetails!=null)
                            //    {
                            //        tbpnlpip.Visible = true;
                            //        int i = 0;
                            //        dtPIPDetails.Columns.Add("SNo", typeof(string));
                            //        foreach (DataRow row in dtPIPDetails.Rows)
                            //        {
                            //            DataRow dr = dtPIPDetails.Rows[i];
                            //            dr["SNo"] = i + 1;
                            //            i++;
                            //        }
                            //        rptpip.DataSource = dtPIPDetails;
                            //        rptpip.DataBind(); 
                            //    }
                            //} 
                            #endregion

                            #endregion




                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));// GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

                            appraisee = currentWeb.EnsureUser(strAprraiseeName); //lblAppraiserValue
                            // Appraiser = currentWeb.EnsureUser(strAprraiseeName);
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


                            #region PIP Binding Commented by Krishna on 250620131213
                            //DataTable dtPIP = GetPIPDetails(Convert.ToInt32(pipAppraisalPhaseID.Value));

                            //if (dtPIP != null)
                            //{
                            //    int i = 0;
                            //    dtPIP.Columns.Add("SNo", typeof(string));
                            //    //dtPIP.Columns.Add("ID",typeof(string));
                            //    foreach (DataRow row in dtPIP.Rows)
                            //    {
                            //        DataRow dr = dtPIP.Rows[i];
                            //        dr["SNo"] = i + 1;
                            //        i++;
                            //    }
                            //    // ViewState["dummyTable"] = dtPIP;
                            //    rptpip.DataSource = dtPIP;
                            //    rptpip.DataBind();
                            //}
                            //else
                            //{
                            //    DataTable dt = dumytable();
                            //    dt.Columns.Add("ID", typeof(int));
                            //    rptpip.DataSource = dt;
                            //    rptpip.DataBind();
                            //} 
                            #endregion

                            #region Commented by Krishna on 010720131247
                            //foreach (RepeaterItem item in rptpip.Items)
                            //{
                            //    TextBox txtActualResultmidterm = item.FindControl("txtActualResultmidterm") as TextBox;
                            //    TextBox txtAppraisersAssessmentmidterm = item.FindControl("txtAppraisersAssessmentmidterm") as TextBox;
                            //    TextBox txtActualResultfinelterm = item.FindControl("txtActualResultfinelterm") as TextBox;
                            //    TextBox txtAppraisersAssessmentfinalterm = item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox;
                            //    TextBox txtPerformanceIssu = item.FindControl("txtPerformanceIssu") as TextBox;
                            //    TextBox txtExpectedAchivements = item.FindControl("txtExpectedAchivements") as TextBox;
                            //    TextBox txtTimeFrame = item.FindControl("txtTimeFrame") as TextBox;

                            //    Label lblPerformanceIssu = item.FindControl("lblPerformanceIssu") as Label;
                            //    Label lblExpectedAchivements = item.FindControl("lblExpectedAchivements") as Label;
                            //    Label lblTimeFrame = item.FindControl("lblTimeFrame") as Label;
                            //    Label lblActualResultmidterm = item.FindControl("lblActualResultmidterm") as Label;
                            //    Label lblAppraisersAssessmentmidterm = item.FindControl("lblAppraisersAssessmentmidterm") as Label;
                            //    Label lblActualResultfinelterm = item.FindControl("lblActualResultfinelterm") as Label;
                            //    Label lblAppraisersAssessmentfinalterm = item.FindControl("lblAppraisersAssessmentfinalterm") as Label;

                            //    if (lblAppraiserValue.Text != this.currentUser.Name)
                            //    {
                            //        // flag = "appraisee";
                            //        btnPIPAdd.Visible = false;
                            //        lnksave.Visible = true;//
                            //        btnCancel.Visible = false;
                            //        //lnkapprove.Visible = false;
                            //        lblPerformanceIssu.Visible = true;
                            //        lblExpectedAchivements.Visible = true;
                            //        lblTimeFrame.Visible = true;
                            //        lblActualResultmidterm.Visible = true;
                            //        lblAppraisersAssessmentmidterm.Visible = true;
                            //        lblActualResultfinelterm.Visible = true;
                            //        lblAppraisersAssessmentfinalterm.Visible = true;
                            //    }
                            //    else if (lblAppraiserValue.Text == this.currentUser.Name)
                            //    {
                            //        //flag = "Appraiser";
                            //        txtPerformanceIssu.Visible = true;
                            //        txtExpectedAchivements.Visible = true;
                            //        txtTimeFrame.Visible = true;
                            //        if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text))
                            //        {
                            //            btnPIPAdd.Visible = true;
                            //            txtPerformanceIssu.Visible = false;
                            //            txtExpectedAchivements.Visible = false;
                            //            txtTimeFrame.Visible = false;

                            //            lblPerformanceIssu.Visible = true;
                            //            lblExpectedAchivements.Visible = true;
                            //            lblTimeFrame.Visible = true;

                            //            txtActualResultmidterm.Visible = true;
                            //            txtAppraisersAssessmentmidterm.Visible = true;

                            //        }
                            //        if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text) && !string.IsNullOrEmpty(txtActualResultmidterm.Text) && !string.IsNullOrEmpty(txtAppraisersAssessmentmidterm.Text))
                            //        {
                            //            btnPIPAdd.Visible = true;//jst for
                            //            lblActualResultmidterm.Visible = true;
                            //            txtActualResultfinelterm.Visible = true;

                            //            txtActualResultmidterm.Visible = false;
                            //            txtAppraisersAssessmentmidterm.Visible = false;

                            //            txtAppraisersAssessmentfinalterm.Visible = true;
                            //            lblAppraisersAssessmentmidterm.Visible = true;
                            //        }
                            //        if (!string.IsNullOrEmpty(txtPerformanceIssu.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text) && !string.IsNullOrEmpty(txtActualResultfinelterm.Text) && !string.IsNullOrEmpty(txtAppraisersAssessmentfinalterm.Text) && !string.IsNullOrEmpty(txtExpectedAchivements.Text) && !string.IsNullOrEmpty(txtTimeFrame.Text))
                            //        {

                            //            txtActualResultfinelterm.Visible = false;
                            //            txtAppraisersAssessmentfinalterm.Visible = false;
                            //            btnPIPAdd.Visible = true;//jst for
                            //            //lnkapprove.Visible = false;
                            //            lnksave.Visible = false;
                            //            lblPerformanceIssu.Visible = true;
                            //            lblExpectedAchivements.Visible = true;
                            //            lblTimeFrame.Visible = true;
                            //            lblActualResultmidterm.Visible = true;
                            //            lblAppraisersAssessmentmidterm.Visible = true;
                            //            lblActualResultfinelterm.Visible = true;
                            //            lblAppraisersAssessmentfinalterm.Visible = true;

                            //        }
                            //    }

                            //    ViewState["flag"] = flag;
                            //} 
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }

        private DataTable GetPIPDetails(int appraisalPhaseID)
        {
            DataTable dt = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList pipDetails = web.Lists["PIP"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='pipAppraisalPhaseID' /><Value Type='Number'>" + Convert.ToInt32(pipAppraisalPhaseID.Value) + "</Value></Eq></Where>";

                    SPListItemCollection pipCollection = pipDetails.GetItems(q);
                    dt = pipCollection.GetDataTable();
                }
                return dt;
            }
        }

        private string GetUserByCode(string employeeCode)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList lstEmployeeMaster = currentWeb.Lists["Employees Master"];

                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='EmployeeCode' /><Value Type='Text'>" + employeeCode + "</Value></Eq></Where>";

                        SPListItemCollection masterCollection = lstEmployeeMaster.GetItems(q);
                        SPListItem employeeItem = masterCollection[0];

                        return Convert.ToString(employeeItem["EmployeeName"]);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

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
            dr["ID"] = "0";
            tbl.Rows.Add(dr);
            //DataRow dr1 = tbl.NewRow();
            //dr1["SNo"] = 2;
            //tbl.Rows.Add(dr1);
            ViewState["dummyTable"] = tbl;
            return tbl;

        }

        protected void btnPIPAdd_Click(object sender, EventArgs e)
        {
            int goulsCount = Convert.ToInt32(piphfAppraiselid.Value) + 1;
            piphfAppraiselid.Value = goulsCount.ToString();
            this.dummyTable = ViewState["dummyTable"] as DataTable;
            int i = 0;
            // flag = Convert.ToString(ViewState["flag"]);
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
                    Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Fill Atleast 1 PIP')</script>");
                    return;
                }

            }
            DataRow dr1 = this.dummyTable.NewRow();
            dr1["SNo"] = this.dummyTable.Rows.Count + 1;
            dr1["ID"] = "0";
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

                    LinkButton lnkDelete = (LinkButton)item.FindControl("lnkDelete");
                    lnkDelete.Visible = true;
                }
            }
        }

        internal static void DeleteGoalsItem(int itemID)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList appraisalGoals = web.Lists["PIP"];
                    SPListItem goalItem = appraisalGoals.GetItemById(itemID);

                    web.AllowUnsafeUpdates = true;
                    goalItem.Delete();
                    web.AllowUnsafeUpdates = false;

                }
            }
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
        }

        protected void btnPIPCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl,false);
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
                        //if (NewItem)
                        //    lstItem = lstPOCreation.AddItem();
                        //else
                        //{
                        //    lstItem = lstPOCreation.GetItemById(ItemID);
                        //}
                        #region MyRegion
                        //lstItem["ApprMasterEmployeeCode"] = Convert.ToString(lblempcodevalue.Text);
                        //lstItem["ApprMasterSatus"] = Convert.ToString("Goal Setting completed");
                        ////lstItem["ealAppraiser"] = Convert.ToString(lbl);
                        ////lstItem["ealReviewer"] = Convert.ToString("");
                        ////lstItem["ealTmt"] = Convert.ToString("");
                        ////lstItem["ealWorkflowState"] = Convert.ToString("");
                        ////lstItem["ealStatus"] = Convert.ToString("");
                        ////lstItem["ealStage"] = Convert.ToString("");

                        //objWeb.AllowUnsafeUpdates = true;
                        //lstItem.Update();
                        //objWeb.AllowUnsafeUpdates = false; 
                        #endregion
                        SPListItem lstItemAppraisal = this.GetAppraisalId();
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
                            lstItem["pipAppraisalID"] = Convert.ToInt32(piphfAppraiselid.Value);
                            lstItem["pipPerformanceIssue"] = Convert.ToString(txtPerformanceIssu.Text);
                            lstItem["pipExpectedachivement"] = Convert.ToString(txtExpectedAchivements.Text);
                            lstItem["pipTimeFrame"] = Convert.ToString(txtTimeFrame.Text);
                            lstItem["pipMidTermActualResult"] = Convert.ToString(txtActualResultmidterm.Text);
                            lstItem["pipMidTermAppraisersAssessment"] = Convert.ToString(txtAppraisersAssessmentmidterm.Text);
                            lstItem["pipFinalAcutualResult"] = Convert.ToString(txtActualResultfinelterm.Text);
                            lstItem["pipFinalAppraisersAssesment"] = Convert.ToString(txtAppraisersAssessmentfinalterm.Text);
                            lstItem["pipAppraisalPhaseID"] = Convert.ToInt32(pipAppraisalPhaseID.Value);
                            objWeb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            objWeb.AllowUnsafeUpdates = false;
                        }

                    }

                }
                string sss = "'PIP saved Successfully'";
                Context.Response.Write("<script language=\"javascript\">alert('PIP saved Successfully'); window.location.href='" + CommonMaster.DashBoardUrl + "'</script>");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('PIP saved Successfully')</script>");
            //}
            //catch (Exception ex)
            //{
            //    Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            //}
        }

        private SPListItem GetAppraisalId()
        {
            using (SPSite site = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists["PIP"];

                    SPListItemCollection collListItems = list.Items;
                    if (list.ItemCount > 0)
                    {
                        SPListItem listItem = list.Items[list.ItemCount - 1];
                        return listItem;
                    }
                    else
                        return null;
                }
            }
        }

        protected void lnkapprove_Click(object sender, EventArgs e)
        {
            SaveItem(true, 0);
            Response.Redirect(SPContext.Current.Web.Url,false);
            //Response.End();

        }

        public static DataTable GetGoalsDetails(int itemID)
        {
            DataTable dtCategories = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["PIP"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='pipAppraiselID' /><Value Type='Text'>" + itemID + "</Value></Eq></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    dtCategories = categoryColl.GetDataTable();
                }
                dtCategories.Columns.Add("SNo");
                int i = 1;
                foreach (DataRow dr in dtCategories.Rows)
                {
                    dr["SNo"] = i;
                    i++;
                }
                return dtCategories;
            }
        }

        public static DataTable GetPIPDetails(int appID, int phaseId)
        {
            DataTable dtPIP = new DataTable();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    SPList categoryList = web.Lists["PIP"];
                    SPQuery q = new SPQuery();//pipAppraisalPhaseID
                    q.Query = "<Where><And><Eq><FieldRef Name='pipAppraisalID' /><Value Type='Number'>" + appID + "</Value></Eq><Eq><FieldRef Name='pipAppraisalPhaseID' /><Value Type='Number'>" + phaseId + "</Value></Eq></And></Where>";
                    SPListItemCollection categoryColl = categoryList.GetItems(q);
                    dtPIP = categoryColl.GetDataTable();
                }
                //dtPIP.Columns.Add("SNo");
                //int i = 1;
                //foreach (DataRow dr in dtPIP.Rows)
                //{
                //    dr["SNo"] = i;
                //    i++;
                //}
                return dtPIP;
            }
        }

        //public void approvemethd(bool NewItem, int ItemID)
        //{
        //    try
        //    {

        //        using (SPSite objSite = new SPSite(SPContext.Current.Site.Url))
        //        {
        //            using (SPWeb objWeb = objSite.OpenWeb())
        //            {
        //                objWeb.AllowUnsafeUpdates = true;

        //                SPList lstPip = objWeb.Lists["PIP"];
        //                SPListItem lstitm;


        //                #region MyRegion
        //                //SPListItem lstItem;

        //                //if (NewItem)
        //                //    lstItem = lstPip.AddItem();
        //                //else
        //                //{
        //                //    lstItem = lstPOCreation.GetItemById(ItemID);
        //                //    SPListItem lstItemAppraisal = this.GetAppraisalId();
        //                //}


        //                //lstItem["ApprMasterEmployeeCode"] = Convert.ToString(lblempcodevalue.Text);
        //                //lstItem["ApprMasterSatus"] = Convert.ToString("Goal Setting completed");
        //                ////lstItem["ealAppraiser"] = Convert.ToString(lbl);
        //                ////lstItem["ealReviewer"] = Convert.ToString("");
        //                ////lstItem["ealTmt"] = Convert.ToString("");
        //                ////lstItem["ealWorkflowState"] = Convert.ToString("");
        //                ////lstItem["ealStatus"] = Convert.ToString("");
        //                ////lstItem["ealStage"] = Convert.ToString("");

        //                //objWeb.AllowUnsafeUpdates = true;
        //                //lstItem.Update();
        //                //objWeb.AllowUnsafeUpdates = false; 
        //                #endregion



        //               // SPList lstGoalSettings = objWeb.Lists["PIP"];

        //                foreach (RepeaterItem item in rptpip.Items)
        //                {
        //                    Label lblPerformanceIssu = item.FindControl("txtPerformanceIssu") as Label;
        //                    Label lblExpectedAchivements = item.FindControl("txtExpectedAchivements") as Label;
        //                    Label lblTimeFrame = item.FindControl("txtTimeFrame") as Label;
        //                    Label lblActualResultmidterm = item.FindControl("txtActualResultmidterm") as Label;
        //                    Label lblAppraisersAssessmentmidterm = item.FindControl("txtAppraisersAssessmentmidterm") as Label;
        //                    Label lblActualResultfinelterm = item.FindControl("txtActualResultfinelterm") as Label;
        //                    Label lblAppraisersAssessmentfinalterm = item.FindControl("txtAppraisersAssessmentfinalterm") as Label;
        //                    lblPerformanceIssu.Visible = true;
        //                    lblExpectedAchivements.Visible = true;
        //                    lblTimeFrame.Visible = true;
        //                    lblActualResultmidterm.Visible = true;
        //                    lblAppraisersAssessmentmidterm.Visible = true;
        //                    lblActualResultfinelterm.Visible = true;
        //                    lblAppraisersAssessmentfinalterm.Visible = true;
        //                    #region MyRegion
        //                    //Label lblDueDate = item.FindControl("lblDueDate") as Label;
        //                    //Label lblWeightage = item.FindControl("lblWeightage") as Label;
        //                    //Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

        //                    //DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
        //                    //Label lblCategory = item.FindControl("lblCategory") as Label;
        //                    //TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
        //                    //DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
        //                    //TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
        //                    //TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
        //                    //Label lblSno = item.FindControl("lblSno") as Label; 
        //                    #endregion


        //                    #region MyRegion
        //                    //if (NewItem)
        //                    //    lstItem = lstGoalSettings.AddItem();
        //                    //else
        //                    //{
        //                    //    lstItem = lstGoalSettings.GetItemById(ItemID);
        //                    //}

        //                    //lstItem["pipPerformanceIssue_x0020_"] = Convert.ToString(lblPerformanceIssu.Text);
        //                    //lstItem["pipExpectedAchievement"] = Convert.ToString(lblExpectedAchivements.Text);
        //                    //lstItem["pipTimeframe"] = Convert.ToString(lblTimeFrame.Text);
        //                    //lstItem["pipActualResults_x002d_Midterm"] = Convert.ToString(lblActualResultmidterm.Text);
        //                    //lstItem["pipAppraiserAssessment_x002d_Mid"] = Convert.ToString(lblAppraisersAssessmentmidterm.Text);
        //                    //lstItem["pipActualResults_x002d_Final"] = Convert.ToString(lblActualResultfinelterm.Text);

        //                    //lstItem["pipAppraiserAssessment_x002d_Fin"] = Convert.ToString(lblAppraisersAssessmentfinalterm.Text);

        //                    //objWeb.AllowUnsafeUpdates = true;
        //                    //lstItem.Update();
        //                    //objWeb.AllowUnsafeUpdates = false; 
        //                    #endregion

        //                    TextBox txtPerformanceIssu = item.FindControl("txtPerformanceIssu") as TextBox;
        //                    TextBox txtExpectedAchivements = item.FindControl("txtExpectedAchivements") as TextBox;
        //                    TextBox txtTimeFrame = item.FindControl("txtTimeFrame") as TextBox;
        //                    TextBox txtActualResultmidterm = item.FindControl("txtActualResultmidterm") as TextBox;
        //                    TextBox txtAppraisersAssessmentmidterm = item.FindControl("txtAppraisersAssessmentmidterm") as TextBox;
        //                    TextBox txtActualResultfinelterm = item.FindControl("txtActualResultfinelterm") as TextBox;
        //                    TextBox txtAppraisersAssessmentfinalterm = item.FindControl("txtAppraisersAssessmentfinalterm") as TextBox;
        //                    txtPerformanceIssu.Visible = false;
        //                    txtExpectedAchivements.Visible = false;
        //                    txtTimeFrame.Visible = false;
        //                    txtAppraisersAssessmentmidterm.Visible = false;
        //                    txtActualResultfinelterm.Visible = false;
        //                    txtAppraisersAssessmentfinalterm.Visible = false;
        //                    txtActualResultmidterm.Visible = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}


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
                    if (item.ItemIndex <= count)
                    {
                        DataRow dr = dt.Rows[i];
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser Evaluation_SaveasPDP");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl,false);
            //Response.End();
                                

        }

        protected void goalscompetenciespdp()
        {
            if (!this.IsPostBack)
            {
                //SPList lstAppraisalTasks; 
                // SPListItem taskItem; 
                SPUser appraisee;//appraisee
                //SPListItem appraisalItem;
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        //lstAppraisalTasks = currentWeb.Lists["VFSAppraisalTasks"];   //
                        //taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));  
                        //hfAppraisalID.Value = Convert.ToString(taskItem["tskAppraisalId"]); //

                        //lblStatusValue.Text = Convert.ToString(taskItem["tskStatus"]); //
                        SPListItem appraisalItem;
                        hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                        SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                        appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //

                        SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                        SPQuery phasesQuery = new SPQuery(); //
                        phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";

                        SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                        SPListItem phaseItem = phasesCollection[0]; //
                        lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]);

                        hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //
                        string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

                        appraisee = currentWeb.EnsureUser(strAprraiseeName);

                        appraisee = currentWeb.EnsureUser(Convert.ToString(appraisalItem["Author"]).Split('#')[1]);
                        lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";

                    }

                    SPListItem appraiseeData = CommonMaster.GetTheAppraiseeDetails(appraisee.LoginName);

                    if (appraiseeData != null)
                    {
                        ////lblHeaderValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                        ////lblemployeevalue.Text = Convert.ToString(appraiseeData["EmployeeCode"]);
                        ////lblEmpNameValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                        ////lblPositionValue.Text = Convert.ToString(appraiseeData["PositionID_x003a_PositionText"]);
                        ////lblAppraiserValue.Text = Convert.ToString(appraiseeData["ImmediateSupervisor_x003a_Employ0"]);
                        ////lblReviewerValue.Text = Convert.ToString(appraiseeData["DepartmentHead_x003a_EmployeeNam"]);
                        ////lblCountryHRValue.Text = Convert.ToString(appraiseeData["HREmployeeCode_x003a_EmployeeNam"]);
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
                    this.DummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                    this.DummyTable.Columns.Add("SNo", typeof(string));

                    int i = 1;
                    int mandatoryGoalCount = 0; //
                    foreach (DataRow dr in this.DummyTable.Rows)
                    {
                        dr["SNo"] = i;
                        i++;
                        if (dr["IsMandatory"].ToString() == "True")  //
                        {
                            mandatoryGoalCount++;//
                        }//
                    }
                    hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();  //

                    ViewState["Appraisals"] = this.DummyTable;
                    rptGoalSettings.DataSource = ViewState["Appraisals"];
                    rptGoalSettings.DataBind();

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
                    //EnableResultDropdown(this.dtCompetencies);
                    //this.DtDevelopmentmesure = new DataTable();
                    this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));
                    if (DtDevelopmentmesure != null)
                    {
                        this.DtDevelopmentmesure.Columns.Add("SNo", typeof(string));
                        int k = 1;
                        foreach (DataRow dr in this.DtDevelopmentmesure.Rows)
                        {
                            dr["SNo"] = k;
                            k++;
                        }
                    } ViewState["PDP"] = this.DtDevelopmentmesure;
                    RptDevelopmentMesure.DataSource = this.DtDevelopmentmesure;
                    RptDevelopmentMesure.DataBind();
                    // BindDateTimeControl2("last");

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

    }
}
