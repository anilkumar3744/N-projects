using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFSProjectH1
{
    public partial class SignOffView : LayoutsPageBase
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

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    DvREvaluation.Visible = true;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = true;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = true;
                    pipdiv.Visible = false;
                }
                else
                {
                    DvREvaluation.Visible = false;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = false;
                    pipdiv.Visible = true;
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
                            lblStatusValue.Text = Convert.ToString(taskItem["appAppraisalStatus"]); //
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(taskItem["appPerformanceCycle"]);
                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //



                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, Web))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                            }
                            //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value)))
                            //{
                            //    string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                            //    Response.Redirect(url);
                            //}




                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery(); //
                            phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0]; //
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //

                            if (phaseItem["aphSignoffonbehalfcomments"] != null)
                                lblCommentsFinal.Text = Convert.ToString(phaseItem["aphSignoffonbehalfcomments"]);

                            if (phaseItem["aphScore"] != null)
                            {
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]);  //
                                score = Convert.ToDouble(lblScoretotal.Text);
                            }

                            if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["aphIsPIP"])))
                            {
                                pipbtn = Convert.ToString(phaseItem["aphIsPIP"]);
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

                        this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value)); //
                        if (dummyTable != null && dummyTable.Rows.Count > 0)
                        {
                            this.dummyTable.Columns.Add("SNo", typeof(string));

                            int i = 1;
                            int mandatoryGoalCount = 0; //
                            foreach (DataRow dr in this.dummyTable.Rows)
                            {
                                dr["SNo"] = i;
                                i++;
                                if (dr["IsMandatory"].ToString() == "True")  //
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

                        //if (pipbtn == "True")
                        //{
                        //    this.dtPip = CommonMaster.GetPIPDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        //    if (dtPip != null && dtPip.Rows.Count > 0)
                        //    {
                        //        this.dtPip.Columns.Add("SNo", typeof(string));
                        //        int z = 1;
                        //        foreach (DataRow dr in this.dtPip.Rows)
                        //        {
                        //            dr["SNo"] = z;
                        //            z++;
                        //        }
                        //        rptpip.DataSource = this.dtPip;
                        //        rptpip.DataBind();
                        //    }
                        //}


                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Sign-Off View");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }
    }
}
