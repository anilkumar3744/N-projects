using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace VFS.PMS.ApplicationPages.Layouts.SelfEva
{
    public partial class SelfView : LayoutsPageBase
    {
        SPUser currentUser;
        DataTable dtCompetencies, DtDevelopmentmesure;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    dvGoalSettings.Visible = true;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = true;
                    saftymeasurementdevelopment.Visible = false;
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = false;
                    saftymeasurementdevelopment.Visible = true;
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
                            //lstAppraisalTasks = currentWeb.Lists["VFSAppraisalTasks"];   //
                            //taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));  //
                            //hfAppraisalID.Value = Convert.ToString(taskItem["tskAppraisalId"]); //

                            //lblStatusValue.Text = Convert.ToString(taskItem["tskStatus"]); //

                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //

                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //
                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);
                            lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                return;
                            }
                            //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value)))
                            //{
                            //    string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                            //    Response.Redirect(url);
                            //}


                            SPQuery phasesQuery = new SPQuery(); //
                            phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0]; //
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //
                            lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]);
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

                        // lblStatusValue.Text = "Awaiting Self Evaluation";


                        DataTable dt = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        dt.Columns.Add("SNo", typeof(string));

                        int i = 1;
                        int mandatoryGoalCount = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["SNo"] = i;
                            i++;
                            if (dr["IsMandatory"].ToString() == "True")  //
                            {
                                mandatoryGoalCount++;//
                            }//
                        }
                        hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                        ViewState["Appraisals"] = dt;
                        rptGoalSettings.DataSource = dt;
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


                        this.DtDevelopmentmesure = CommonMaster.GetAppraisalDevelopmentMesure(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (this.DtDevelopmentmesure != null && this.DtDevelopmentmesure.Rows.Count > 0)
                        {
                            tbpnljobdetails.Visible = true;
                            this.DtDevelopmentmesure.Columns.Add("SNo", typeof(string));
                            int k = 1;
                            foreach (DataRow dr in this.DtDevelopmentmesure.Rows)
                            {
                                dr["SNo"] = k;
                                k++;
                            }
                        }
                        else
                        {
                            tbpnljobdetails.Visible = false;
                        }
                        rptsaftymeasurementdevelopment.DataSource = this.DtDevelopmentmesure;
                        rptsaftymeasurementdevelopment.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                
                LogHandler.LogError(ex, "Error in PMS H1 Self Evaluation");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }
        private void BindDateTimeControl(string last)
        {
            try
            {
                // int i = 0;
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
                    //if (item.ItemIndex < count)
                    //{
                    //    DataRow dr = dt.Rows[i];
                    //    DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                    //    dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                    //    i++;
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MoreGoals(int rowsCount)
        {
            //DataTable dtCategories = CommonMaster.GetCategories();
            DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                if (item.ItemIndex >= 5)
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
            }
        }

        private void SelectedDropDown(DataTable dataTable)
        {
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                if (item.ItemIndex >= 5)
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    if (dataTable.Rows[item.ItemIndex]["agGoalCategory"].ToString() != string.Empty)
                    {
                        ddl.Items.FindByText(dataTable.Rows[item.ItemIndex]["agGoalCategory"].ToString()).Selected = true;
                    }
                }
            }
        }
    }
}
