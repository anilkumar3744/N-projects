using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.SharePoint.Workflow;
using System.Collections;

namespace VFS.PMS.ApplicationPages.Layouts.H2initial
{
    public partial class H2Initial : LayoutsPageBase
    {
        DataTable dummyTable;
        SPUser currentUser;
        // int workFlowItemId = 0;
        SPListItem appraiseeData;
       
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
                //message1=SPContext.Current.Web.Url++
                if (!this.IsPostBack)
                {

                    SPUser appraisee;
                    if (this.Request.Params["AppId"] != null)
                    {
                        SPListItem appraisalItem;
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb oWeb = osite.OpenWeb())
                            {

                                SPList lstAppraisala = oWeb.Lists["Appraisals"];
                                appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(Request.Params["AppId"]));
                                hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);

                                lblStatusValue.Text = "H2 - Awaiting Appraisee Goal Setting";

                                lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);

                                double AppraiseeCode = Convert.ToDouble(appraisalItem["appEmployeeCode"]);

                                string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(AppraiseeCode));
                                appraisee = oWeb.EnsureUser(strAprraiseeName);

                                if (appraisee.LoginName != this.currentUser.LoginName)
                                {
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('Appraisee did not submit goals.'); </script>");
                                }

                                if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, oWeb))
                                {
                                    Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                    return;
                                }
                                //if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), oWeb))
                                //{
                                //    string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                //    Response.Redirect(url, false);
                                //    //Response.End();
                                //}

                                if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), oWeb))
                                {
                                    //string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                    //Response.Redirect(url);
                                    Context.Response.Write("<script type='text/javascript'>window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                }

                                //double AppraiseeCode = Convert.ToDouble(appraisalItem["appEmployeeCode"]);
                                //if (!CommonMaster.CanUserViewAppraisal(AppraiseeCode, this.currentUser.LoginName, oWeb))
                                //{
                                //    
                                //    Context.Response.Write("<script type='text/javascript'>alert('Insufficient permissions');window.open('" + url + "','_self'); </script>");
                                //    return;
                                //}
                                //string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(AppraiseeCode));
                                //appraisee = oWeb.EnsureUser(strAprraiseeName);

                                SPList lstAppraisalPhases = oWeb.Lists["Appraisal Phases"];
                                SPQuery phasesQuery = new SPQuery();
                                phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";

                                SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                                if (phasesCollection.Count > 0)
                                {
                                    SPListItem phaseItem = phasesCollection[0];
                                    hfH1AppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);
                                }

                                SPQuery phasesQueryH2 = new SPQuery();
                                phasesQueryH2.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                                SPListItemCollection phasesCollectionH2 = lstAppraisalPhases.GetItems(phasesQueryH2);
                                if (phasesCollectionH2.Count > 0)
                                {
                                    SPListItem phaseItem = phasesCollectionH2[0];
                                    hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);
                                }


                            }
                            this.appraiseeData = CommonMaster.GetTheAppraiseeDetails(this.currentUser.LoginName);
                            //if (appraisee.LoginName != this.currentUser.LoginName)
                            //{
                            //    
                            //    Context.Response.Write("<script type='text/javascript'>alert('Appraisee didnot submit goals.');window.open('" + url + "','_self'); </script>");
                            //}
                            if (appraiseeData != null)
                            {
                                lblHeaderValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                                lblempcodevalue.Text = Convert.ToString(appraiseeData["EmployeeCode"]);
                                lblEmpNameValue.Text = Convert.ToString(appraiseeData["EmployeeName"]);
                                lblPositionValue.Text = Convert.ToString(appraiseeData["Position"]);
                                lblAppraiserValue.Text = Convert.ToString(appraiseeData["ReportingManagerName"]);
                                lblReviewerValue.Text = Convert.ToString(appraiseeData["DepartmentHeadName"]);
                                lblCountryHRValue.Text = Convert.ToString(appraiseeData["HRBusinessPartnerName"]);
                                hrReviewerCode.Value = Convert.ToString(this.appraiseeData["DepartmentHeadEmployeeCode"]);
                                hfAppraiserCode.Value = Convert.ToString(this.appraiseeData["ReportingManagerEmployeeCode"]);
                                hrCountryHrCode.Value = Convert.ToString(this.appraiseeData["HRBusinessPartnerEmployeeCode"]);
                                lblOrgUnitValue.Text = Convert.ToString(appraiseeData["OrganizationUnit"]);
                                if (!string.IsNullOrEmpty(Convert.ToString(appraiseeData["HireDate"])))
                                {
                                    DateTime hireDate = Convert.ToDateTime(appraiseeData["HireDate"]);
                                    lblHireDateValue.Text = hireDate.ToString("dd-MMM-yyyy");
                                }
                            }

                            string appraisercode;
                            appraisercode = Convert.ToString(appraiseeData["ReportingManagerEmployeeCode"]);

                            appLog.Value = CommonMaster.GetUserByCode(appraisercode);
                            ////DataTable dt = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                            if (string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                            {
                                if (!string.IsNullOrEmpty(hfH1AppraisalPhaseID.Value))
                                {
                                    this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfH1AppraisalPhaseID.Value));
                                    foreach (DataRow row in this.dummyTable.Rows)
                                    {
                                        //row["agGoal"] = DBNull.Value;
                                        //row["agGoalDescription"] = DBNull.Value;
                                        row["agDueDate"] = DBNull.Value;
                                        row["agWeightage"] = DBNull.Value;
                                    }
                                }
                                //else
                                //    this.dummyTable = CommonMaster.GetOptionalGoalCategories(); 
                            }
                            else
                            {
                                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                                {
                                    using (SPWeb oWeb = oSite.OpenWeb())
                                    {
                                        SPList history = oWeb.Lists["Comments History"];
                                        SPQuery q = new SPQuery();
                                        q.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraisee</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>";

                                        SPListItemCollection col = history.GetItems(q);
                                        if (col != null && col.Count > 0)
                                        {
                                            SPListItem historyItem = col[0];
                                            txtComments.Text = Convert.ToString(historyItem["chComment"]);
                                        }
                                    }

                                    this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                                    if (this.dummyTable == null)
                                        this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                                }
                            }

                            if (this.dummyTable != null)
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
                                hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
                                ViewState["Appraisals"] = this.dummyTable;
                                rptGoalSettings.DataSource = this.dummyTable;
                                rptGoalSettings.DataBind();
                                if (!string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                                {
                                    BindDateTimeControl("first");
                                }


                                if (Convert.ToInt32(hfldMandatoryGoalCount.Value) != 0)
                                {
                                    MoreGoals(this.dummyTable.Rows.Count);
                                    BindDateTimeControl("last");
                                    SelectedDropDown(this.dummyTable);
                                }
                                ////SelectedDropDown(dummyTable);
                            }
                            else
                            {
                                DataTable dt2 = this.GetCategories();
                                ViewState["Appraisals"] = dt2;
                                rptGoalSettings.DataSource = dt2;
                                rptGoalSettings.DataBind();
                                BindDateTimeControl("last");
                                MoreGoals(0);
                            }
                        }
                    }

                }
                if (ViewState["hfldMandatoryGoalCount"] != null)
                {
                    hfldMandatoryGoalCount.Value = ViewState["hfldMandatoryGoalCount"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Goal Setting");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    hfflag.Value = "true";
                    if (string.IsNullOrEmpty(Convert.ToString(hfAppraisalPhaseID.Value)))
                        SaveToSavedDraft();
                    else
                    {
                        UpadteGoalsToDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    }
                    //if (!string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                    //{
                    //    BindExistingGoals();
                    //}
                    ////Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('H2 Saved Successfully')</script>");

                    string url = SPContext.Current.Web.Url + "/_layouts/H2initial/H2Initial.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + CustomMessages.Saved + "'); </script>");

                });
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Goal Setting");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        private void UpadteGoalsToDraft(int appraisalID, int appraisalPhaseId)
        {


            CommonMaster.DeletePreviousGoal(appraisalPhaseId, this.currentUser.Name, "Appraisal Goals Draft");

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstGoalSettings = objWeb.Lists["Appraisal Goals Draft"];
                    SPListItem lstItem;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblGoal = item.FindControl("lblGoal") as Label;
                        Label lblDueDate = item.FindControl("lblDueDate") as Label;
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        Label lblSno = item.FindControl("lblSno") as Label;
                        Label lblID = item.FindControl("lblID") as Label;

                        lstItem = lstGoalSettings.AddItem();

                        lstItem["agAppraisalId"] = appraisalID;
                        lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                            lstItem["IsMandatory"] = false;
                        }
                        else
                        {
                            lstItem["IsMandatory"] = true;
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        //lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                        TextBox tctDate = dc.Controls[0] as TextBox;
                        if (string.IsNullOrEmpty(tctDate.Text))
                        {

                            string date = h1EndDate.ToString("dd-MMM-yyyy");
                            lstItem["agDueDate"] = date;
                        }
                        else
                        {
                            lstItem["agDueDate"] = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        }
                        lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                        lstItem["Author"] = this.currentUser;

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Goala amending";
                    lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                    lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    lstItem["chRole"] = "Appraisee";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                    //BindExistingGoals();

                }

            }

            //
            //////Context.Response.Write("<script type='text/javascript'>alert('Goals submitted successfully');window.open('" + url + "','_self'); </script>");window.open
            //Context.Response.Write("<script type='text/javascript'>alert('" + CustomMessages.Submitted + "');window.open('" + url + "','_self'); </script>");

        }

        private void SaveToSavedDraft()
        {
            hfflag.Value = "true";
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstAppraisals = objWeb.Lists["Appraisals"];

                    SPListItem appraisalItem = lstAppraisals.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                    SPListItem lstItem;
                    SPList lstGoals = objWeb.Lists["Appraisal Phases"];

                    lstItem = lstGoals.AddItem();

                    lstItem["aphAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                    lstItem["aphAppraisalPhase"] = "H2";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                    // SPListItem lstPhaseItem = CommonMaster.GetCurrentItemId("Appraisal Phases");
                    hfAppraisalPhaseID.Value = Convert.ToString(lstItem.ID);

                    SPList lstGoalSettings = objWeb.Lists["Appraisal Goals Draft"];

                    int mandatoryItemCount = 0;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H2");
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblGoal = item.FindControl("lblGoal") as Label;
                        Label lblDueDate = item.FindControl("lblDueDate") as Label;
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        Label lblSno = item.FindControl("lblSno") as Label;

                        lstItem = lstGoalSettings.AddItem();

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            if (ddlCategories.SelectedIndex > 0)
                                lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);

                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        if (mandatoryItemCount < Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["IsMandatory"] = true;
                            mandatoryItemCount++;
                        }
                        else
                        {
                            lstItem["IsMandatory"] = false;
                        }
                        lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        TextBox tctDate = dc.Controls[0] as TextBox;
                        if (string.IsNullOrEmpty(tctDate.Text))
                        {

                            string date = h1EndDate.ToString("dd-MMM-yyyy");
                            lstItem["agDueDate"] = date;
                        }
                        else
                        {
                            lstItem["agDueDate"] = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        }
                        //lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                        lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                        lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);

                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }

                    //CommonMaster.BindHistory("Goal setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-mm-yyyy"), lblEmpNameValue.Text, "Appraisee");

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Goal setting";
                    lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                    lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    lstItem["chRole"] = "Appraisee";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;

                }

            }

        }

        private void BindExistingGoals()
        {
            this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
            if (this.dummyTable.Rows.Count < 0)
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
            hfldMandatoryGoalCount.Value = mandatoryGoalCount.ToString();
            ViewState["Appraisals"] = null;
            ViewState["Appraisals"] = this.dummyTable;
            rptGoalSettings.DataSource = ViewState["Appraisals"];
            rptGoalSettings.DataBind();
            BindDateTimeControl("first");

            if (Convert.ToInt32(hfldMandatoryGoalCount.Value) != 0)
            {
                MoreGoals(this.dummyTable.Rows.Count);
                SelectedDropDown(this.dummyTable);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(appLog.Value))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        //if (!Convert.ToBoolean(hfflag.Value))
                        //{
                        SaveItem(true, 0);
                        //}
                        if (!string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                        {
                            //UpadteGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        }

                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;

                                SPList list = web.Lists["Appraisals"];
                                SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                SPList appraisalStatus = web.Lists["Appraisal Status"];

                                //SPList listTMT = web.Lists["TMT Actions"];
                                //SPQuery q = new SPQuery();
                                //q.Query = "<Where><And><Eq><FieldRef Name='tmtPerformanceCycle' /><Value Type='Text'>" + Convert.ToString(CurrentListItem["appPerformanceCycle"]) + "</Value></Eq><Eq><FieldRef Name='tmtIsH2SelfEvaluationStarted' /><Value Type='Text'>Started</Value></Eq></And></Where>";
                                //SPListItemCollection coll = listTMT.GetItems();
                                //if (coll.Count > 0)
                                //{
                                //    foreach (SPListItem item in coll)
                                //    {
                                //        if (Convert.ToString(item["tmtIsH2SelfEvaluationStarted"]) == "Started")
                                //            CurrentListItem["appAppraisalStatus"] = Convert.ToString(appraisalStatus.GetItemById(15)["Appraisal_x0020_Workflow_x0020_S"]);
                                //        else
                                //            CurrentListItem["appAppraisalStatus"] = Convert.ToString(appraisalStatus.GetItemById(14)["Appraisal_x0020_Workflow_x0020_S"]);
                                //    }

                                //}

                                //CurrentListItem.Update();

                                SPListItem lstStatusItem = appraisalStatus.GetItemById(13);
                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
                                appraisalItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                appraisalItem.Update();
                                web.AllowUnsafeUpdates = false;


                            }
                            CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");


                            Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('" + string.Format(CustomMessages.Submitted, "H2") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");




                        }
                    });
                    WorkflowTriggering();
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('APPRAISER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Goal Setting");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        private void WorkflowTriggering()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {

                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb web = osite.OpenWeb())
                    {
                        SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];
                        SPQuery q = new SPQuery();
                        //q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.LoginName + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + lblStatusValue.Text + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
                        q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H2 - Awaiting Appraisee Goal Setting</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
                        SPListItemCollection taskCollection = lstAppraisalTasks.GetItems(q);
                        if (taskCollection.Count > 0)
                        {
                            SPListItem taskItem = taskCollection[0];
                            Hashtable ht = new Hashtable();
                            ht["tskStatus"] = "Approved";
                            ht["Status"] = "Approved";
                            web.AllowUnsafeUpdates = true;
                            SPWorkflowTask.AlterTask(taskItem, ht, true);
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect(SPContext.Current.Site.Url);

                Response.Redirect(CommonMaster.DashBoardUrl, false);
                //Response.End();

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Goal Setting");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                int sNo = Convert.ToInt32(lnk.CommandArgument);
                DataTable dt = ViewState["Appraisals"] as DataTable;

                int i = 0;
                if (sNo != rptGoalSettings.Items.Count)
                {
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        DataRow dr = dt.Rows[i];
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//
                        {
                            // if ((item.FindControl("ddlCategories") as DropDownList).SelectedItem != null)
                            dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                        }
                        else
                        {
                            dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                        }

                        dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
                        dr["agDueDate"] = strDate;

                        if (!string.IsNullOrEmpty((item.FindControl("txtWeightage") as TextBox).Text))
                            dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        i++;
                        dt.AcceptChanges();
                    }
                }

                dt.Rows.RemoveAt(sNo - 1);
                i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow dr = dt.Rows[i];
                    dr["SNo"] = i + 1;
                    i++;
                }

                rptGoalSettings.DataSource = dt;
                rptGoalSettings.DataBind();
                BindDateTimeControl("first");

                ViewState["Appraisals"] = null;
                ViewState["Appraisals"] = dt;

                if (dt.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    MoreGoals(dt.Rows.Count);
                    SelectedDropDown(dt);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Goal Setting");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int goulsCount = Convert.ToInt32(hfGoalsCount.Value) + 1;

                hfGoalsCount.Value = goulsCount.ToString();

                this.dummyTable = ViewState["Appraisals"] as DataTable;

                int i = 0;


                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    if ((item.FindControl("txtGoal") as TextBox).Text != string.Empty && (item.FindControl("SPDateLastDate") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtWeightage") as TextBox).Text != string.Empty && (item.FindControl("txtDescription") as TextBox).Text != string.Empty)
                    {
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//
                        {
                            // if ((item.FindControl("ddlCategories") as DropDownList).SelectedItem != null)
                            dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                        }
                        else
                        {
                            dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                        }

                        dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        string strDate = dc.SelectedDate.ToString();
                        dr["agDueDate"] = strDate;
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        i++;
                    }
                    else
                    {

                        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + CustomMessages.FillPreviousGoals + "')</script>");
                        return;
                    }
                }

                DataRow dr1 = this.dummyTable.NewRow();
                dr1["SNo"] = this.dummyTable.Rows.Count + 1;
                this.dummyTable.Rows.Add(dr1);

                rptGoalSettings.DataSource = this.dummyTable;
                rptGoalSettings.DataBind();
                BindDateTimeControl("first");

                ViewState["Appraisals"] = null;
                ViewState["Appraisals"] = this.dummyTable;

                if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5
                {
                    MoreGoals(this.dummyTable.Rows.Count);
                    SelectedDropDown(this.dummyTable);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Goal Setting");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }

        }

        private void BindDateTimeControl(string last)
        {
            int i = 0, count = 0;
            if (last == "last")
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
                DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                if (item.ItemIndex < count)
                {

                    DataRow dr = dt.Rows[i];


                    if (!string.IsNullOrEmpty(Convert.ToString(dr["agDueDate"])))
                    {
                        dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                    }
                    else
                    {
                        dc.SelectedDate = CommonMaster.GetPhaseEndDate("H2");
                    }
                    i++;
                }
                else
                {
                    dc.SelectedDate = CommonMaster.GetPhaseEndDate("H2");
                }
                //    if (dr["agDueDate"] != DBNull.Value)
                //    {
                //        dc.SelectedDate = Convert.ToDateTime(dr["agDueDate"]);
                //    }
                //    i++;
                //}
            }
        }

        private DataTable GetCategories()
        {
            DataTable testTable = new DataTable();

            testTable = CommonMaster.GetCategories();

            testTable.Columns.Add("SNo", typeof(string));
            testTable.Columns.Add("agGoalCategory", typeof(string));
            testTable.Columns.Add("agGoal", typeof(string));
            testTable.Columns.Add("agDueDate", typeof(string));
            testTable.Columns.Add("agWeightage", typeof(int));
            testTable.Columns.Add("agGoalDescription", typeof(string));
            testTable.Columns.Add("IsMandatory", typeof(Boolean));
            int i = 1;
            foreach (DataRow dr in testTable.Rows)
            {
                dr["SNo"] = i;
                dr["agGoalCategory"] = dr["ctgrCategory"];
                dr["IsMandatory"] = dr["ctgrMandatory"];
                i++;
            }
            hfldMandatoryGoalCount.Value = testTable.Rows.Count.ToString();
            ViewState["hfldMandatoryGoalCount"] = hfldMandatoryGoalCount.Value;
            return testTable;
        }

        private void MoreGoals(int rowsCount)
        {
            ////DataTable dtCategories = CommonMasters.GetCategories();
            DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    LinkButton lnkDelete = (LinkButton)item.FindControl("lnkDelete");
                    Label lblCategory = item.FindControl("lblCategory") as Label;
                    ddl.Visible = true;
                    lnkDelete.Visible = true;
                    lblCategory.Visible = false;

                    ddl.DataSource = dtCategories;
                    ddl.DataValueField = "ID";
                    ddl.DataTextField = "ctgrCategory";
                    ddl.DataBind();

                    ddl.Items.Insert(0, "Choose Category");

                }
                else
                {
                    Panel pnl = (Panel)item.FindControl("testpnl");
                    TextBox dummyFld = new TextBox();
                    dummyFld.CssClass = "validation";
                    dummyFld.Style.Add("display", "none");
                    pnl.Controls.Add(dummyFld);
                    //item.FindControl("snolbl").Controls.Add(dummyFld);
                }
            }
        }

        private void SelectedDropDown(DataTable dt)
        {
            foreach (RepeaterItem item in rptGoalSettings.Items)
            {
                if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                    if (dt.Rows[item.ItemIndex]["agGoalCategory"].ToString() != string.Empty)
                    {
                        ddl.Items.FindByText(dt.Rows[item.ItemIndex]["agGoalCategory"].ToString()).Selected = true;
                    }
                }
            }
        }

        public void SaveItem(bool NewItem, int ItemID)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPListItem GoalItem;
                    if (string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                    {
                        SPList lstGoals = objWeb.Lists["Appraisal Phases"];

                        GoalItem = lstGoals.AddItem();
                        GoalItem["aphAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        //lstItem["aphAppraisal"] = DateTime.Now.Year.ToString();
                        GoalItem["aphAppraisalPhase"] = "H2";

                        objWeb.AllowUnsafeUpdates = true;
                        GoalItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        //SPListItem lstPhaseItem = CommonMaster.GetCurrentItemId("Appraisal Phases");
                        hfAppraisalPhaseID.Value = Convert.ToString(GoalItem.ID);
                    }
                    SPList lstGoalSettings = objWeb.Lists["Appraisal Goals"];

                    int mandatoryItemCount = 0;
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblGoal = item.FindControl("lblGoal") as Label;
                        Label lblDueDate = item.FindControl("lblDueDate") as Label;
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        Label lblSno = item.FindControl("lblSno") as Label;

                        GoalItem = lstGoalSettings.AddItem();

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//
                        {
                            if (ddlCategories.SelectedIndex > 0)
                                GoalItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                            //else
                            //{
                            //    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Select Category at row'" + Convert.ToString(item.ItemIndex + 1) + ");</script>");
                            //    return; //at row " + Convert.ToString(item.ItemIndex + 1) + "
                            //}

                        }
                        else
                        {
                            GoalItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        if (mandatoryItemCount < Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            GoalItem["IsMandatory"] = true;
                            mandatoryItemCount++;
                        }
                        else
                        {
                            GoalItem["IsMandatory"] = false;
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(txtGoal.Text)))
                            GoalItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        GoalItem["agDueDate"] = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                        if (!string.IsNullOrEmpty(Convert.ToString(txtWeightage.Text)))
                            GoalItem["agWeightage"] = Convert.ToInt32(txtWeightage.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            GoalItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                        GoalItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        GoalItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                        //hfAppraisalPhaseID.Value = Convert.ToString(GoalItem["ID"]);
                        objWeb.AllowUnsafeUpdates = true;
                        GoalItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }
                    CommonMaster.BindHistory("Goal setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    GoalItem = lstCommentsHistory.AddItem();
                    GoalItem["chCommentFor"] = "Goal setting";
                    GoalItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                    GoalItem["chComment"] = txtComments.Text;
                    GoalItem["chCommentedBy"] = this.currentUser;
                    GoalItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    GoalItem["chActor"] = lblEmpNameValue.Text;
                    GoalItem["chRole"] = "Appraisee";

                    objWeb.AllowUnsafeUpdates = true;
                    GoalItem.Update();
                    objWeb.AllowUnsafeUpdates = false;

                }
                ////if (ItemID == 0)
                ////    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Goals saved successfully');</script>");
                ////else
                ////    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Goals submitted successfully');</script>");
                if (ItemID == 0)


                    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Saved, "H2") + "');</script>");
                else
                    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Submitted, "H2") + "');</script>");

            }

        }

        public void UpadteGoals(int appraisalID, int appraisalPhaseId)
        {

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstGoalSettings = objWeb.Lists["Appraisal Goals"];
                    SPListItem lstItem;

                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        Label lblGoal = item.FindControl("lblGoal") as Label;
                        Label lblDueDate = item.FindControl("lblDueDate") as Label;
                        Label lblWeightage = item.FindControl("lblWeightage") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;

                        DropDownList ddlCategories = item.FindControl("ddlCategories") as DropDownList;
                        Label lblCategory = item.FindControl("lblCategory") as Label;
                        TextBox txtGoal = item.FindControl("txtGoal") as TextBox;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        TextBox txtWeightage = item.FindControl("txtWeightage") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        Label lblSno = item.FindControl("lblSno") as Label;
                        Label lblID = item.FindControl("lblID") as Label;

                        if (string.IsNullOrEmpty(lblID.Text))
                        {
                            lstItem = lstGoalSettings.AddItem();

                            lstItem["agAppraisalId"] = appraisalID;
                            lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
                        }
                        else
                        {
                            lstItem = lstGoalSettings.GetItemById(Convert.ToInt32(lblID.Text));
                        }

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                            lstItem["IsMandatory"] = false;
                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }

                        lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                        lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Goala amending";
                    lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                    lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    lstItem["chRole"] = "Appraisee";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;

                }

                //string url = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?AppId=" + hfAppraisalID.Value;
                //Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Update, "H2") + "');window.open('" + url + "','_self'); </script>");

                Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('" + CustomMessages.Update + "'); </script>");

                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Update, "H2") + "')</script>");
            }

        }

        protected void lnkbutton1_Click(object sender, EventArgs e)
        {

            Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/HRView.aspx?AppId=" + hfAppraisalID.Value.ToString(), false);
            //Response.End();
            //string url = (SPContext.Current.Site.Url + "/_layouts/VFS_ApplicationPages/HRView.aspx?AppId=" + hfAppraisalID.Value.ToString());// "http://www.dotnetcurry.com";

            //C lientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>");

        }

        protected void ViewH1_Click(object sender, EventArgs e)
        {

            Response.Redirect((SPContext.Current.Web.Url + "/_Layouts/VFSProjectH1/HRView.aspx?AppId=" + Convert.ToString(Request.Params["AppId"])), false);
            //Response.End();

        }

        protected void rptGoalSettingsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ritem = e.Item;
            if (ritem.ItemType == ListItemType.AlternatingItem || ritem.ItemType == ListItemType.Item)
            {
                DateTimeControl dc = ritem.FindControl("SPDateLastDate") as DateTimeControl;
                dc.MinDate = DateTime.Now.Date;
            }

        }

    }
}