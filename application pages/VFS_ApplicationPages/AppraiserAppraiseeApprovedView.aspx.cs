using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages
{
    public partial class AppraiserAppraiseeApprovedView : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies;
        SPUser currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;

                if (TabContainer1.ActiveTabIndex == 0)
                {
                    dvGoalSettings.Visible = true;
                    dvCompetencies.Visible = false;
                }
                else
                {
                    dvGoalSettings.Visible = false;
                    dvCompetencies.Visible = true;
                }
                if (!this.IsPostBack)
                {
                    //SPList lstAppraisalTasks;
                    //SPListItem taskItem;
                    SPUser appraisee;
                    SPListItem appraisalItem;

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];
                            //lstAppraisalTasks = currentWeb.Lists["VFSAppraisalTasks"];
                            //taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));

                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
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


                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];
                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            SPListItem phaseItem = phasesCollection[0];
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);

                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

                            appraisee = currentWeb.EnsureUser(strAprraiseeName);


                            //by jagadeesh
                            //if (appraisee.Name == this.currentUser.Name)
                            //{
                            //    btnAmmend.Visible = true;
                            //}
                            //else
                            //    btnAmmend.Visible = false;


                            //string id = string.Empty;
                            //string currentUser1 = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];
                            //SPListItem dtMaster = CommonMaster.GetMasterDetails("WindowsLogin", currentUser1);
                            //if (dtMaster != null)
                            //{
                            //    if (!string.IsNullOrEmpty(Convert.ToString(dtMaster["EmployeeCode"])))
                            //        id = dtMaster["EmployeeCode"].ToString();

                            //    SPListItem spItem;
                            //    spItem = CommonMaster.GetMasterDetails("ReportingManagerEmployeeCode", id);
                            //    if (spItem != null || appraisee.Name == this.currentUser.Name)
                            //    {
                            //        btnAmmend.Visible = true;
                            //    }
                            //    else
                            //        btnAmmend.Visible = false;
                            //}


                            //by jagadeesh
                            //modofied by tataji for amend button
                           

                            SPList history = currentWeb.Lists["Comments History"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraisee</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False'/></OrderBy>";
                            SPListItemCollection col = history.GetItems(q);
                            SPListItem historyItem = col[0];
                            lblAppraiseeComments1.Text = Convert.ToString(historyItem["chComment"]);

                            SPQuery q2 = new SPQuery();
                            q2.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraiser</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";
                            SPListItemCollection col2 = history.GetItems(q2);
                            if (col2.Count > 0)
                            {
                                SPListItem historyItem2 = col2[0];
                                lblAppraiserCommentsValue.Text = Convert.ToString(historyItem2["chComment"]);
                            }
                        }

                        SPListItem appraiseeData = CommonMaster.GetTheAppraiseeDetails(appraisee.LoginName);

                        if (appraiseeData != null)
                        {
                            lblHeaderValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                            lblemployeevalueeee.Text = Convert.ToString(appraiseeData["EmployeeCode"]);
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

                        string id = string.Empty;
                        string currentUser1 = SPContext.Current.Web.CurrentUser.LoginName.Split('\\')[1];

                        SPListItem dtMaster = CommonMaster.GetMasterDetails("EmployeeCode", lblemployeevalueeee.Text);
                        if (dtMaster != null)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dtMaster["ReportingManagerEmployeeCode"])))
                                id = dtMaster["ReportingManagerEmployeeCode"].ToString();

                            SPListItem spItem;
                            spItem = CommonMaster.GetMasterDetails("WindowsLogin", currentUser1);
                            if ((id == Convert.ToString(spItem["EmployeeCode"]) || appraisee.Name == this.currentUser.Name) && lblStatusValue.Text != "H1-Awaiting Appraiser Goal Approval")
                            {
                                btnAmmend.Visible = true;
                            }
                            else
                                btnAmmend.Visible = false;
                        }


                        this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        this.dummyTable.Columns.Add("SNo", typeof(string));

                        int i = 1;
                        foreach (DataRow dr in this.dummyTable.Rows)
                        {
                            dr["SNo"] = i;
                            i++;
                        }

                        rptGoalSettings.DataSource = this.dummyTable;
                        rptGoalSettings.DataBind();


                        this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (this.dtCompetencies != null)
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
                            tbpnlusrdetails.Visible = false;
                            btnAmmend.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H1 Appraiser Appraisee Approved View");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");

            }
        }

        protected void BtnAmmend_Click(object sender, EventArgs e)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    //SPList list = web.Lists[new Guid(Request.Params["List"])];
                    Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AmmendGoals.aspx?AppId=" + Convert.ToInt32(Request.Params["AppId"]), false);
                    //Response.End();
                }
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(SPContext.Current.Site.Url);

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }
    }
}
