
using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.SharePoint.Workflow;
using System.Collections;
using System.Resources;
namespace VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages
{

    public partial class InitialGoalSetting : LayoutsPageBase
    {
        DataTable dummyTable;
        SPUser currentUser;
        SPListItem appraiseeData;
        //string appraiserLogIn;
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //btnPrint.Visible = false;
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1";
                if (!this.IsPostBack)
                {
                    SPUser appraisee;
                    if (this.Request.Params["AppId"] != null)
                    {
                        SPList lstAppraisals;
                        SPListItem appraisalItem;
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                lstAppraisals = web.Lists["Appraisals"];
                                appraisalItem = lstAppraisals.GetItemById(Convert.ToInt32(Request.Params["AppId"]));
                                hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);

                                string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));
                                appraisee = web.EnsureUser(strAprraiseeName);

                                lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);
                                lblAppraisalPeriodValue.Text = "H1, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


                                if (appraisee.LoginName != this.currentUser.LoginName)
                                {
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('Appraisee did not submit goals'); </script>");
                                }

                                #region MyRegion
                                //user restricction//
                                if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, Web))
                                {
                                    Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                }
                                if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), web))
                                {
                                    //string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                    //Response.Redirect(url);
                                    //Context.Response.Write("<script type='text/javascript'>window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                    Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                }
                                #endregion
                            }

                            this.appraiseeData = CommonMaster.GetTheAppraiseeDetails(appraisee.LoginName);
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

                            DataTable dt = this.GetCategories();
                            rptGoalSettings.DataSource = dt;
                            rptGoalSettings.DataBind();
                            BindDateTimeControl("last");
                            MoreGoals(1);
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
                LogHandler.LogError(ex, "Error in PMS H1 Intial Goal Setting page load");
                //Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
                //Context.Response.Write("<script type='text/javascript'> alert('" + CommonMaster.serializeMessage(ex.Message) + "');</script>");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int goulsCount = Convert.ToInt32(hfGoalsCount.Value) + 1;

                hfGoalsCount.Value = goulsCount.ToString();

                this.dummyTable = ViewState["dummyTable"] as DataTable;

                int i = 0;

                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    if ((item.FindControl("txtGoal") as TextBox).Text != string.Empty && (item.FindControl("SPDateLastDate") as DateTimeControl).SelectedDate.ToString() != string.Empty && (item.FindControl("txtWeightage") as TextBox).Text != string.Empty && (item.FindControl("txtDescription") as TextBox).Text != string.Empty)
                    {
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//
                        {
                            dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                        }
                        else
                        {
                            dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                            //dr["ctgrCategory"] = (item.FindControl("lblCategory") as TextBox).Text;
                        }

                        dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;

                        // dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                        string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                        dr["agDueDate"] = strDate;
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        i++;
                    }

                    else
                    {
                        #region MyRegion
                        //string url = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?AppId=" + hfAppraisalID.Value;
                        //Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.FillPreviousGoals, "H2") + "');window.open('" + url + "','_self'); </script>");
                        //string url = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?AppId=" + hfAppraisalID.Value; 
                        //Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + string.Format(CustomMessages.FillPreviousGoals, "H2") + "'); </script>");


                        // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.FillPreviousGoals, "H1") + "')</script>");
                        //Context.Response.Write("<script type='text/javascript'>alert('Fill all the details of goals at row " + Convert.ToInt32(item.ItemIndex + 1) + "');</script>");

                        #endregion
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

                ViewState["dummyTable"] = null;
                ViewState["dummyTable"] = this.dummyTable;

                if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5
                {
                    MoreGoals(this.dummyTable.Rows.Count);
                    SelectedDropDown(this.dummyTable);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H1 Initial Goal Setting Add");
                // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
                // Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
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
                   #region MyRegion
                   //if (!string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                   //{
                   //    BingExistingGoals();
                   //}

                   //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert(" + string.Format(CustomMessages.Saved, "H1") + "')</script>");
                   //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Saved, "H1") + "')</script>");

                   #endregion
                   string url = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiseeSavedDraft.aspx?AppId=" + hfAppraisalID.Value;
                   Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self'); alert('" + string.Format(CustomMessages.Saved, "H1") + "');</script>");
               });
            }

            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H1 Initial Goal Setting Save");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
                //Context.Response.Write("<script type='text/javascript'> alert('" + CommonMaster.serializeMessage(ex.Message) + "');</script>");
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
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        objWeb.AllowUnsafeUpdates = true;

                        SPList lstGoalSettings = objWeb.Lists["Appraisal Goals Draft"];
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
                            lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
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
                        lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                        lstItem["chComment"] = txtComments.Text;
                        lstItem["chCommentedBy"] = this.currentUser;
                        lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                        lstItem["chActor"] = lblEmpNameValue.Text;
                        lstItem["chRole"] = "Appraisee";

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                        CommonMaster.BindHistory("Goal setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");

                    });
                }
                #region MyRegion
                //string url = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/AppraiseeSavedDraft.aspx?AppId=" + hfAppraisalID.Value;
                //Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Update, "H2") + "');window.open('" + url + "','_self'); </script>");


                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Update, "H1") + "')</script>");

                #endregion
            }

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

                    SPQuery phaseQuery = new SPQuery();
                    phaseQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                    SPListItemCollection phasesCollection = lstGoals.GetItems(phaseQuery);
                    if (phasesCollection != null && phasesCollection.Count > 0)
                    {
                        lstItem = phasesCollection[0];
                    }
                    else
                    {
                        lstItem = lstGoals.AddItem();
                        lstItem["aphAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["aphAppraisalPhase"] = "H1";
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }

                    //SPListItem lstPhaseItem = CommonMaster.GetCurrentItemId("Appraisal Phases");
                    hfAppraisalPhaseID.Value = Convert.ToString(lstItem.ID);

                    SPList lstGoalSettings = objWeb.Lists["Appraisal Goals Draft"];

                    int mandatoryItemCount = 0;
                    DateTime h1EndDate = CommonMaster.GetPhaseEndDate("H1");
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
                        //by jagadeesh
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
                        //by jagadeesh
                        lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                        lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);

                        lstItem["Author"] = this.currentUser;

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }

                    CommonMaster.BindHistory("Goal setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");
                    #region MyRegion
                    //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    //lstItem = lstCommentsHistory.AddItem();
                    //lstItem["chCommentFor"] = "Goal setting";
                    //lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                    //lstItem["chComment"] = txtComments.Text;
                    //lstItem["chCommentedBy"] = this.currentUser;
                    //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    //lstItem["chActor"] = lblEmpNameValue.Text;
                    //lstItem["chRole"] = "Appraisee";

                    //objWeb.AllowUnsafeUpdates = true;
                    //lstItem.Update();
                    //objWeb.AllowUnsafeUpdates = false; 
                    #endregion
                }

            }


        }

        private void BingExistingGoals()
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
            ViewState["dummyTable"] = null;
            ViewState["dummyTable"] = this.dummyTable;
            rptGoalSettings.DataSource = ViewState["dummyTable"];
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
                if (appLog.Value != string.Empty)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SaveItem(true, 0);
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
                                SPList appraisalStatus = web.Lists["Appraisal Status"];
                                SPListItem lstStatusItem = appraisalStatus.GetItemById(2);
                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
                                appraisalItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);

                                appraisalItem.Update();
                                web.AllowUnsafeUpdates = false;

                            }
                        }

                        CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");
                    });
                    ////Context.Response.Write("<script type='text/javascript'>alert('Goals submitted successfully');window.open('" + url + "','_self'); </script>");window.open
                    Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('" + string.Format(CustomMessages.Submitted, "H1") + "'); </script>");
                    WorkflowTriggering();
                }
                else
                {
                    //Page.ClientScript.RegisterStartupScript(typeof(SPAlert), "alert", "<script type='text/javascript'>alert('" + CustomMessages.CheckAppraiser + "')</script>");
                    Context.Response.Write("<script type='text/javascript'>alert('APPRAISER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H1 Intial Goal Setting Submit");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
                // Context.Response.Write("<script type='text/javascript'> alert('" + CommonMaster.serializeMessage(ex.Message) + "');</script>");
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
                        q.Query = "<Where><And><Eq><FieldRef Name='AssignedTo' /><Value Type='User'>" + this.currentUser.Name + "</Value></Eq><And><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>" + lblStatusValue.Text + "</Value></Eq><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></And></And></Where>";
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
            //Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            //Context.Response.Flush();
            try
            {
                //Context.Response.End();

                Response.Redirect(CommonMaster.DashBoardUrl, false);
                //Response.End();

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H1 Intial Goal Setting Cancel");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
                // Context.Response.Write("<script type='text/javascript'> alert('" + CommonMaster.serializeMessage(ex.Message) + "');</script>");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton lnk = sender as LinkButton;
                RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem; //----------
                int sNo = Convert.ToInt32(lnk.CommandArgument);
                DataTable dt = ViewState["dummyTable"] as DataTable;
                int i = 0;
                if (sNo != rptGoalSettings.Items.Count)
                {
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        DataRow dr = dt.Rows[i];
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//
                            dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
                        else
                            dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
                        dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
                        DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
                        string strDate = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
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
                //BindDateTimeControl("first");
                if (dt.Rows.Count != rptItem.ItemIndex + 1)
                {
                    BindDateTimeControl("last");
                }
                else
                {
                    BindDateTimeControl("first");
                }

                ViewState["dummyTable"] = null;
                ViewState["dummyTable"] = dt;

                if (dt.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                {
                    MoreGoals(dt.Rows.Count);
                    SelectedDropDown(dt);
                }

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H1 Initial Goal Setting delete");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
            }
        }

        private void BindDateTimeControl(string last)
        {
            try
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
                DataTable dt = ViewState["dummyTable"] as DataTable;
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
                            dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                        }
                        i++;
                    }
                    else
                    {
                        dc.SelectedDate = CommonMaster.GetPhaseEndDate("H1");
                    }

                }
            }
            catch (Exception ex)
            {
                //    LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }

        private void SelectedDropDown(DataTable dt)
        {
            try
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
            catch (Exception ex)
            {
                //    LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");

            }
        }

        private void MoreGoals(int rowsCount)
        {
            try
            {
                ////DataTable dtCategories = CommonMasters.GetCategories();
                DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value) || rptGoalSettings.Items.Count == 1)//5
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

                    }
                }
            }
            catch (Exception ex)
            {
                //LogHandler.LogError(ex, "Error in PMS Reviewer Evaluation");
                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");

            }
        }

        private DataTable GetCategories()
        {


            DataTable testTable = CommonMaster.GetCategories();
            if (testTable == null)
            {
                testTable = new DataTable();
            }
            #region MyRegion

            //testTable.Columns.Add("SNo", typeof(string));
            //testTable.Columns.Add("Goal", typeof(string));
            //testTable.Columns.Add("DueDate", typeof(string));
            //testTable.Columns.Add("Weightage", typeof(int));
            //testTable.Columns.Add("Description", typeof(string));
            //testTable.Columns.Add("isManadatory", typeof(Boolean));

            #endregion
            testTable.Columns.Add("SNo", typeof(string));
            testTable.Columns.Add("agGoalCategory", typeof(string));
            testTable.Columns.Add("agGoal", typeof(string));
            testTable.Columns.Add("agDueDate", typeof(string));
            testTable.Columns.Add("agWeightage", typeof(int));
            testTable.Columns.Add("agGoalDescription", typeof(string));
            testTable.Columns.Add("IsMandatory", typeof(Boolean));

            int i = 1;
            if (testTable.Rows.Count > 0)
            {
                foreach (DataRow dr in testTable.Rows)
                {
                    dr["SNo"] = i;
                    dr["agGoalCategory"] = dr["ctgrCategory"];
                    i++;
                }
            }
            else
            {
                testTable.Columns.Add("ID", typeof(string));
                DataRow dr = testTable.NewRow();
                dr["ID"] = i;
                dr["SNo"] = i;
                testTable.Rows.Add(dr);
            }
            hfldMandatoryGoalCount.Value = testTable.Rows.Count.ToString();
            ViewState["hfldMandatoryGoalCount"] = hfldMandatoryGoalCount.Value;
            ViewState["dummyTable"] = testTable;
            return testTable;
        }

        public void SaveItem(bool NewItem, int ItemID)
        {

            hfSavedFlag.Value = "true";
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        objWeb.AllowUnsafeUpdates = true;

                        SPList lstAppraisals = objWeb.Lists["Appraisals"];
                        SPListItem appraisalItem = lstAppraisals.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                        SPListItem lstItem;

                        if (string.IsNullOrEmpty(hfAppraisalPhaseID.Value))
                        {

                            SPList lstGoals = objWeb.Lists["Appraisal Phases"];

                            SPQuery phaseQuery = new SPQuery();
                            phaseQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstGoals.GetItems(phaseQuery);
                            if (phasesCollection != null && phasesCollection.Count > 0)
                            {
                                lstItem = phasesCollection[0];
                            }
                            else
                            {
                                lstItem = lstGoals.AddItem();
                                lstItem["aphAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                                lstItem["aphAppraisalPhase"] = "H1";
                                objWeb.AllowUnsafeUpdates = true;
                                lstItem.Update();
                                objWeb.AllowUnsafeUpdates = false;
                            }

                            //SPListItem lstPhaseItem = CommonMaster.GetCurrentItemId("Appraisal Phases");
                            hfAppraisalPhaseID.Value = Convert.ToString(lstItem.ID);
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

                            lstItem = lstGoalSettings.AddItem();

                            if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                            {
                                if (ddlCategories.SelectedIndex > 0)
                                    lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                                else
                                {
                                    Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Select Category at row'" + Convert.ToString(item.ItemIndex + 1) + ");</script>");
                                    return;
                                }

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
                            lstItem["agDueDate"] = dc.SelectedDate.Date.ToString("dd-MMM-yyyy");
                            lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                            lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                            lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                            lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);

                            lstItem["Author"] = this.currentUser;

                            objWeb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            objWeb.AllowUnsafeUpdates = false;

                        }

                        CommonMaster.BindHistory("Goal setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");
                    });
                }
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

                        //lstItem["Author"] = this.currentUser;

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                        CommonMaster.BindHistory("Goal Setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");

                        #region MyRegion
                        //SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                        //lstItem = lstCommentsHistory.AddItem();
                        //lstItem["chCommentFor"] = "Goal ammending";
                        //lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                        //lstItem["chComment"] = txtComments.Text;
                        //lstItem["chCommentedBy"] = this.currentUser;
                        //lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                        //lstItem["chActor"] = lblEmpNameValue.Text;
                        //lstItem["chRole"] = "Appraisee";

                        //objWeb.AllowUnsafeUpdates = true;
                        //lstItem.Update();
                        //objWeb.AllowUnsafeUpdates = false; 
                        #endregion

                    }
                    string url = SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Update, "H1") + "');window.open('" + url + "','_self'); </script>");
                }
                // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Update, "H1") + "')</script>");
            }

        }

        protected void rptGoalSettingsItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem ritem = e.Item;
            if (ritem.ItemType == ListItemType.AlternatingItem || ritem.ItemType == ListItemType.Item)
            {
                DateTimeControl dc = ritem.FindControl("SPDateLastDate") as DateTimeControl;
                dc.MinDate = DateTime.Now;
            }

        }

    }
}
