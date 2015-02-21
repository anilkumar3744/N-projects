using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
namespace VFS.PMS.ApplicationPages.Layouts.H2initial
{
    public partial class H2AppraiseeGoalsDraftView : LayoutsPageBase
    {
        SPUser currentUser;
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";

                if (!this.IsPostBack)
                {
                    SPUser appraisee;
                    SPListItem appraisalItem;

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


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


                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);
                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];

                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            SPListItem phaseItem = phasesCollection[0];
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);

                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

                            appraisee = currentWeb.EnsureUser(strAprraiseeName); //Convert.ToString(appraisalItem["Author"]).Split('#')[1]);

                            SPList history = currentWeb.Lists["Comments History"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraisee</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";
                            SPListItemCollection col = history.GetItems(q);
                            if (col != null && col.Count > 0)
                            {
                                SPListItem historyItem = col[0];
                                lblAppraiseeComments1.Text = Convert.ToString(historyItem["chComment"]);
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

                        DataTable dt = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (dt != null && dt.Rows.Count > 0)
                            dt.Columns.Add("SNo", typeof(string));

                        int i = 1;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["SNo"] = i;
                            i++;
                        }

                        rptAppraiserView.DataSource = dt;
                        rptAppraiserView.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Appraisee Goals Draft View");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(SPContext.Current.Site.Url);

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }
    }
}
