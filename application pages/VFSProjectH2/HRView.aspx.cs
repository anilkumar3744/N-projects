using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFSProjectH2
{
    public partial class HRView : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, dtPip, dtCommentHistory;
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
                //if (Request.QueryString["from"] != null && Request.QueryString["from"] == "popup")
                //{
                //    btnCancel.Visible = false;
                //    lblStatusValue.Text = "H2-Completed";
                //}
                //SPListItem spEmpid = CommonMaster.GetMasterDetails("WindowsLogin", SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1]);
                //string hrid = spEmpid["EmployeeCode"].ToString();
                //SPListItem hrItem = CommonMaster.GetMasterDetails("HRBusinessPartnerName", hrid);
                //if (hrItem == null)
                //    tbpnlCHistory.Visible = false;
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
                            //hfAppraisalID.Value = Convert.ToString(taskItem["tskAppraisalId"]); //

                            lblStatusValue.Text = Convert.ToString(taskItem["appAppraisalStatus"]); //
                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(taskItem["appPerformanceCycle"]);
                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), this.currentUser.LoginName, Web))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                            }
                            //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value)))
                            //{
                            //    string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                            //    Response.Redirect(url);
                            //}



                            if (taskItem["appFinalScore"] != null)
                            {
                                lblFS.Text = Convert.ToString(taskItem["appFinalScore"]);
                            }
                            if (taskItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(taskItem["appFinalRating"]);
                            }
                            else
                            {
                                lblfr.Text = "0";
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
                                    lblscore.Text = Convert.ToString(phaseItemH1["aphScore"]);
                            }
                            if (phaseItem["aphScore"] != null)
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]); //

                            

                            if (phaseItem["aphHRReviewLatestComments"] != null)
                                lblFinalComments.Text = Convert.ToString(phaseItem["aphHRReviewLatestComments"]);

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
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                            //
                            appraisee = currentWeb.EnsureUser(strAprraiseeName); //Convert.ToString(appraisalItem["Author"]).Split('#')[1]);
                            //lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";


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
                        if (dummyTable != null && dummyTable.Rows.Count > 0)
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

                        string id = string.Empty;
                        string currentUser = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                        SPListItem dtMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser);
                        if (dtMaster != null)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dtMaster["EmployeeCode"])))
                                id = dtMaster["EmployeeCode"].ToString();

                            SPListItem spItem;
                            spItem = CommonMaster.GetMasterDetails("HRBusinessPartnerEmployeeCode", id);
                            if (spItem != null)
                            {
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
                            }
                            else
                            {
                                tbpnlCHistory.Visible = false;
                                dvCHistory.Visible = false;
                            }
                        }
                        if (Request.QueryString["from"] != null && Request.QueryString["from"] == "popup")
                        {
                            btnCancel.Visible = false;
                            lblStatusValue.Text = "H2-Completed";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS HR View PageLoad");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl);
            Response.End();
        }
        protected void gvCommentsHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCommentsHistory.PageIndex = e.NewPageIndex;

                gvCommentsHistory.DataSource = ViewState["dtCommentsHistory"];
                gvCommentsHistory.DataBind();

            }
            catch (Exception)
            {
            }
        }

    }
}
