using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.AppraiserEvaluationViewMode
{
    public partial class AppraiserEvaluationViewMode : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, PIPDetails, DummyTable;
        SPUser currentUser;
        string flag;
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
                    // pipdiv.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = true;
                    divdevelopmentmessures.Visible = false;
                    //pipdiv.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = false;
                    divdevelopmentmessures.Visible = true;
                    //pipdiv.Visible = false;
                }
                else
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = false;
                    divdevelopmentmessures.Visible = false;
                    //pipdiv.Visible = true;
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

                            //if (CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            //{
                            //    Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";windows.location.href=" + CommonMaster.DashBoardUrl + ";</script>");
                            //}

                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];//
                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);

                            SPListItem phaseItem = phasesCollection[0];
                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotal.Text = phaseItem["aphScore"].ToString();
                            }
                            if (appraisalItem["appFinalRating"] != null)
                            {
                                lblfr.Text = appraisalItem["appFinalRating"].ToString();
                            }
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //
                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

                            appraisee = currentWeb.EnsureUser(strAprraiseeName);


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, Web))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                return;
                            }
                            //////if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value)))
                            //////{
                            //////    string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                            //////    Response.Redirect(url);
                            //////}




                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);   //"Awaiting Appraiser Goal Approval";
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);
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

                        int i = 1;
                        int mandatoryGoalCount = 0; //
                        foreach (DataRow dr in this.dummyTable.Rows)
                        {
                            dr["SNo"] = i;
                            i++;
                            if (dr["IsMandatory"].ToString() == "True")  //
                            {
                                mandatoryGoalCount++;//
                            }//
                        }
                        hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();  //

                        ViewState["Appraisals"] = this.dummyTable;
                        rptGoalSettings.DataSource = ViewState["Appraisals"];
                        rptGoalSettings.DataBind();

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
                        //EnableResultDropdown(this.dtCompetencies);

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
                        // BindDateTimeControl2("last");



                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Appraiser EvaluationView");
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
                throw;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl,false);
            //Response.End();
                                
        }

    }
}
