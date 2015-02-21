using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts
{
    public partial class PrintAppraisal : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, dtPip;
        SPUser currentUser;
        double score;
        string pipbtn;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
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
                            taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["AppId"]));
                            if (taskItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(taskItem["appFinalRating"]);
                            }
                            lblStatusValue.Text = Convert.ToString(taskItem["appAppraisalStatus"]);
                            
                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];
                            SPListItemCollection phasesCollection;
                            SPListItem phaseItem;

                            SPQuery phasesQueryH2 = new SPQuery();
                            phasesQueryH2.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                            phasesCollection = lstAppraisalPhases.GetItems(phasesQueryH2);
                            if (phasesCollection.Count > 0)
                            {
                                phaseItem = phasesCollection[0];
                                lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(taskItem["appPerformanceCycle"]);
                            }
                            else
                            {
                                SPQuery phasesQuery = new SPQuery();
                                phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";
                                lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(taskItem["appPerformanceCycle"]);
                                phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            }
                            if (phasesCollection.Count == 0) return;
                            phaseItem = phasesCollection[0];

                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);

                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]);

                            }
                            if (phaseItem["aphSignoffonbehalfcomments"] != null)
                                lblSignOffComments.Text = Convert.ToString(phaseItem["aphSignoffonbehalfcomments"]);
                            if (phaseItem["aphHRReviewLatestComments"] != null)
                                lblCommentsFinal.Text = Convert.ToString(phaseItem["aphHRReviewLatestComments"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["aphIsPIP"])))
                            {
                                pipbtn = Convert.ToString(phaseItem["aphIsPIP"]);
                                if (pipbtn == "True")
                                {
                                    pipdiv.Visible = true;
                                }
                                else
                                {
                                    pipdiv.Visible = false;
                                }
                            }
                            else
                            {
                                pipdiv.Visible = false;
                            }
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

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

                        this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (this.dummyTable != null && this.dummyTable.Rows.Count > 0)
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
                        else
                        {
                            DvREvaluation.Visible = false;
                        }
                        this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (this.dtCompetencies != null && this.dtCompetencies.Rows.Count > 0)
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
                        else
                        {
                            dvCompetencies.Visible = false;
                        }
                        this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (this.DtDevelopmentmesure != null && this.DtDevelopmentmesure.Rows.Count > 0)
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
                        else
                        {
                            saftymeasurementdevelopment.Visible = false;
                        }
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS HR View");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl);
            Response.End();
        }
    }
}
