using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.H2initial
{
    public partial class H2AwaitingAppraiserApprove : LayoutsPageBase
    {

        DataTable dummyTable, dtCompetencies;
        SPUser currentUser;
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    dvGoalSettings.Style["display"] = "block";
                    dvCompetencies.Style["display"] = "none";
                }
                else
                {

                    dvGoalSettings.Style["display"] = "none";
                    dvCompetencies.Style["display"] = "block";
                }
                if (!this.IsPostBack)
                {
                    SPUser appraisee;
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));
                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);
                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                                return;
                            }
                            if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), currentWeb))
                            {
                                string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                Response.Redirect(url, false);
                                //Response.End();
                            }




                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"];

                            SPQuery phasesQuery = new SPQuery();
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";

                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);
                            SPListItem phaseItem = phasesCollection[0];
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);

                            string strAprraiseeName = CommonMaster.GetUserByCode(Convert.ToString(appraisalItem["appEmployeeCode"]));

                            appraisee = currentWeb.EnsureUser(strAprraiseeName);
                            hfAppraisee.Value = appraisee.Name;

                            SPList history = currentWeb.Lists["Comments History"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraisee</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>"; //
                            SPListItemCollection col = history.GetItems(q);
                            if (col != null && col.Count > 0)
                            {
                                SPListItem historyItem = col[0];
                                lblAppraiseeComments1.Text = Convert.ToString(historyItem["chComment"]);

                            }

                            SPQuery q2 = new SPQuery();
                            q2.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraiser</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where><OrderBy><FieldRef Name='ID' Ascending='False' /></OrderBy>"; //
                            SPListItemCollection col2 = history.GetItems(q2);

                            if (col2 != null && col2.Count > 0)
                            {
                                SPListItem historyItem2 = col2[0];
                                txtComments.Text = Convert.ToString(historyItem2["chComment"]);
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
                            hfEmpSubGroup.Value = Convert.ToString(appraiseeData["EmployeeSubGroup"]);
                            lblOrgUnitValue.Text = Convert.ToString(appraiseeData["OrganizationUnit"]);
                            if (!string.IsNullOrEmpty(Convert.ToString(appraiseeData["HireDate"])))
                            {
                                DateTime hireDate = Convert.ToDateTime(appraiseeData["HireDate"]);
                                lblHireDateValue.Text = hireDate.ToString("dd-MMM-yyyy");
                            }

                        }

                        //this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));

                        this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.dummyTable == null)
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

                        ViewState["Appraisals"] = this.dummyTable;
                        rptGoalSettings.DataSource = ViewState["Appraisals"];
                        rptGoalSettings.DataBind();
                        BindDateTimeControl("last");

                        if (Convert.ToInt32(hfldMandatoryGoalCount.Value) != 0)
                        {
                            MoreGoals(this.dummyTable.Rows.Count);
                            SelectedDropDown(this.dummyTable);
                        }

                        //this.dtCompetencies = CommonMaster.GetMasterCompetencies(Convert.ToString(hfEmpSubGroup.Value));

                        this.dtCompetencies = CommonMaster.GetCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
                        if (this.dtCompetencies == null)
                            this.dtCompetencies = CommonMaster.GetAppraisalCompetencies(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        if (this.dtCompetencies == null)
                        {
                            hfMasterCompetencies.Value = "true";
                            this.dtCompetencies = CommonMaster.GetMasterCompetencies(Convert.ToString(hfEmpSubGroup.Value));

                            this.dtCompetencies.Columns.Add("acmptCompetency", typeof(string));
                            this.dtCompetencies.Columns.Add("acmptDescription", typeof(string));
                            this.dtCompetencies.Columns.Add("acmptExpectedResult", typeof(string));
                            //this.dtCompetencies.Columns.Add("SNo", typeof(string));
                            foreach (DataRow dr in this.dtCompetencies.Rows)
                            {

                                dr["acmptCompetency"] = dr["cmptCompetency"];
                                dr["acmptDescription"] = dr["cmptDescription"];

                            }
                        }
                        this.dtCompetencies.Columns.Add("SNo", typeof(string));
                        int k = 1;
                        foreach (DataRow dr in this.dtCompetencies.Rows)
                        {
                            dr["SNo"] = k;
                            k++;
                        }
                        rptCompetencies.DataSource = this.dtCompetencies;
                        rptCompetencies.DataBind();
                        EnableResultDropdown(this.dtCompetencies);
                        //this.dtCompetencies.Columns.Add("SNo", typeof(string));
                        //SPQuery CompeteniesQuery = new SPQuery();
                        //CompeteniesQuery.Query = "<Where><Eq><FieldRef Name='acmptAppraisalPhaseId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></Where>"; //
                        //SPListItemCollection CompeteniesList = SPContext.Current.Web.Lists["Appraisal Competencies"].GetItems(CompeteniesQuery);

                        //int j = 1;
                        //if (CompeteniesList != null && CompeteniesList.Count > 0)
                        //{
                        //    this.dtCompetencies = CompeteniesList.GetDataTable();
                        //    this.dtCompetencies.Columns.Add("SNo", typeof(string));
                        //    foreach (DataRow dr in this.dtCompetencies.Rows)
                        //    {
                        //        dr["SNo"] = j;
                        //        j++;
                        //    }

                        //    rptCompetencies.DataSource = this.dtCompetencies;
                        //    rptCompetencies.DataBind();
                        //    EnableResultDropdown(this.dtCompetencies);
                        //}
                        //else
                        //{
                        //    hfMasterCompetencies.Value = "true";
                        //    this.dtCompetencies = CommonMaster.GetMasterCompetencies(Convert.ToString(hfEmpSubGroup.Value));
                        //    this.dtCompetencies.Columns.Add("SNo", typeof(string));
                        //    this.dtCompetencies.Columns.Add("acmptCompetency", typeof(string));
                        //    this.dtCompetencies.Columns.Add("acmptDescription", typeof(string));
                        //    this.dtCompetencies.Columns.Add("acmptExpectedResult", typeof(string));

                        //    foreach (DataRow dr in this.dtCompetencies.Rows)
                        //    {
                        //        dr["SNo"] = j;
                        //        dr["acmptCompetency"] = dr["cmptCompetency"];
                        //        dr["acmptDescription"] = dr["cmptDescription"];
                        //        j++;
                        //    }

                        //    rptCompetencies.DataSource = this.dtCompetencies;
                        //    rptCompetencies.DataBind();

                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Awaiting Appraiser Approve");
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


                }
            }
            catch (Exception ex)
            {
                throw ex;
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

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value)) //5)
                        {
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
                    }

                    else
                    {
                        //string url = SPContext.Current.Web.Url + "/_layouts/H2initial/H2AmmendGoals.aspx?AppId=" + hfAppraisalID.Value;
                        //Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + CustomMessages.FillPreviousGoals + "'); </script>");

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

                if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value))// 5)
                {
                    MoreGoals(this.dummyTable.Rows.Count);
                    SelectedDropDown(this.dummyTable);
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Awaiting Appraiser Approve");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        private void MoreGoals(int rowsCount)
        {
            try
            {
                DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5)
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
                throw ex;
            }
        }

        private void SelectedDropDown(DataTable dt)
        {
            try
            {
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value)) //5)
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
                throw ex;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    hfflag.Value = "true";
                    //SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //UpadteGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    SaveToSavedDraft("Appraisal Goals Draft");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies Draft");
                    string url = SPContext.Current.Web.Url + "/_layouts/H2initial/H2AwaitingAppraiserApprove.aspx?AppId=" + hfAppraisalID.Value;
                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + string.Format(CustomMessages.Saved, "H2") + "'); </script>");
                });
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Awaiting Apprasier Approve");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");


            }
        }

        private void SaveToSavedDraft(string listName)
        {

            hfflag.Value = "true";
            CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, listName);

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPListItem lstItem;
                    SPList lstGoalSettings = objWeb.Lists[listName];

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
                        Label lblID = item.FindControl("lblID") as Label;

                        lstItem = lstGoalSettings.AddItem();
                        lstItem["agAppraisalId"] = Convert.ToInt32(hfAppraisalID.Value);
                        lstItem["agAppraisalPhaseId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
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

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGoal.Text)))
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
                        if (!string.IsNullOrEmpty(Convert.ToString(txtWeightage.Text)))
                            lstItem["agWeightage"] = Convert.ToInt32(txtWeightage.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }

                    // CommonMaster.BindHistory("Goal setting", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraiser");

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Goal setting";
                    lstItem["chReferenceId"] = Convert.ToInt32(hfAppraisalPhaseID.Value);
                    lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    // lstItem["chRole"] = "Appraiser";
                    if (hfAppraisee.Value == this.currentUser.LoginName)
                        lstItem["chRole"] = "Appraisee";
                    else
                        lstItem["chRole"] = "Appraiser";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;

                }

            }

        }

        private void SaveCompetenciesDraft(int appraisalID, int appraisalPhaseId, string listName)
        {

            CommonMaster.DeleteCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, listName);
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstGoalSettings = objWeb.Lists[listName];
                    SPListItem lstItem;

                    foreach (RepeaterItem item in rptCompetencies.Items)
                    {
                        Label lblcompetencies = item.FindControl("lblcompetencies") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
                        DropDownList ddlExpectedResult = item.FindControl("ddlExpectedResult") as DropDownList;
                        Label lblCompetencyId = item.FindControl("lblCompetencyId") as Label;

                        lstItem = lstGoalSettings.AddItem();
                        lstItem["acmptAppraisalId"] = appraisalID;
                        lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;           //Convert.ToInt32(lstItemAppraisal["ID"]);
                        lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                        if (ddlExpectedResult.SelectedIndex > 0)
                            lstItem["acmptExpectedResult"] = Convert.ToString(ddlExpectedResult.SelectedItem.Text);
                        lstItem["acmptDescription"] = Convert.ToString(lblDescriptionValue.Text);
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                        //CommonMaster.BindHistory("Competency", Convert.ToInt32(hfAppraisalPhaseID.Value), txtComments.Text, this.currentUser, DateTime.Now.ToString("dd-MMM-yyyy"), lblEmpNameValue.Text, "Appraisee");
                    }
                }
            }

        }

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    SaveToSavedDraft("Appraisal Goals");
                    SaveCompetenciesDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value), "Appraisal Competencies");

                    //if (!Convert.ToBoolean(hfflag.Value))
                    //{
                    //    SaveCompetencies(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //    UpadteGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                    //}
                    CommonMaster.DeleteCompetenciesDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Competencies Draft");
                    CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");


                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb web = osite.OpenWeb())
                        {
                            web.AllowUnsafeUpdates = true;

                            SPList list = web.Lists["Appraisals"];
                            SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            SPList appraisalStatus = web.Lists["Appraisal Status"];

                            SPListItem lstStatusItem = appraisalStatus.GetItemById(14);
                            SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            SPList listTMT = web.Lists["TMT Actions"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><Eq><FieldRef Name='tmtPerformanceCycle' /><Value Type='Text'>" + Convert.ToString(CurrentListItem["appPerformanceCycle"]) + "</Value></Eq></Where>";
                            SPListItemCollection coll = listTMT.GetItems(q);
                            if (coll.Count > 0)
                            {
                                foreach (SPListItem item in coll)
                                {
                                    if (Convert.ToString(item["tmtIsH2SelfEvaluationStarted"]) == "Started" || Convert.ToString(item["tmtIsH2SelfEvaluationStarted"]) == "Closed")
                                        CurrentListItem["appAppraisalStatus"] = Convert.ToString(appraisalStatus.GetItemById(15)["Appraisal_x0020_Workflow_x0020_S"]);
                                    else
                                        CurrentListItem["appAppraisalStatus"] = Convert.ToString(appraisalStatus.GetItemById(14)["Appraisal_x0020_Workflow_x0020_S"]);
                                }

                            }

                            //CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                            CurrentListItem.Update();
                            web.AllowUnsafeUpdates = false;


                        }
                    }
                });
                WorkflowTriggering();

                Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('" + string.Format(CustomMessages.Approved, "H2") + "'); </script>");

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Awaiting Appraiser Approve");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        private void WorkflowTriggering()
        {
            try
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
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect(CommonMaster.DashBoardUrl, false);
            //Response.End();
        }

        //protected void LnkDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LinkButton lnk = sender as LinkButton;
        //        //if (!string.IsNullOrEmpty(lnk.CommandArgument))
        //        //{
        //        //    int itemID = Convert.ToInt32(lnk.CommandArgument);
        //        //    CommonMaster.DeleteGoalsItem(itemID);
        //        //}

        //        RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
        //        Label lblSno = rptItem.FindControl("lblSno") as Label;
        //        this.dummyTable = ViewState["Appraisals"] as DataTable;

        //        int sNo = Convert.ToInt32(lblSno.Text);
        //        int i = 0;
        //        foreach (RepeaterItem item in rptGoalSettings.Items)
        //        {
        //            DataRow dr = this.dummyTable.Rows[i];
        //            dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

        //            if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value)) //5)
        //            {
        //                dr["agGoalCategory"] = (item.FindControl("ddlCategories") as DropDownList).SelectedItem.Text;
        //            }
        //            else
        //            {
        //                dr["agGoalCategory"] = (item.FindControl("lblCategory") as Label).Text;
        //            }

        //            dr["agGoal"] = (item.FindControl("txtGoal") as TextBox).Text;
        //            DateTimeControl dc = item.FindControl("SPDateLastDate") as DateTimeControl;
        //            string strDate = dc.SelectedDate.ToString("dd-MMM-yyyy");
        //            dr["agDueDate"] = strDate;
        //            dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
        //            dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
        //            i++;
        //        }

        //        this.dummyTable.Rows.RemoveAt(Convert.ToInt32(lblSno.Text) - 1);
        //        i = 0;
        //        foreach (DataRow row in this.dummyTable.Rows)
        //        {
        //            DataRow dr = this.dummyTable.Rows[i];
        //            dr["SNo"] = i + 1;
        //            i++;
        //        }
        //        rptGoalSettings.DataSource = this.dummyTable;
        //        rptGoalSettings.DataBind();

        //        if (this.dummyTable.Rows.Count != rptItem.ItemIndex + 1)
        //        {
        //            BindDateTimeControl("last");
        //        }
        //        else
        //        {
        //            BindDateTimeControl("first");
        //        }

        //        ViewState["Appraisals"] = null;
        //        ViewState["Appraisals"] = this.dummyTable;

        //        if (this.dummyTable.Rows.Count >= Convert.ToInt32(hfldMandatoryGoalCount.Value)) //5)
        //        {
        //            MoreGoals(this.dummyTable.Rows.Count);
        //            SelectedDropDown(this.dummyTable);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHandler.LogError(ex, "Error in PMS H2 Awaiting Appraiser Approve");
        //        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", CommonMaster.serializeMessage(ex.Message));
        //    }
        //}
        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
                Label lblSno = rptItem.FindControl("lblSno") as Label;
                int sNo = Convert.ToInt32(lblSno.Text); //Convert.ToInt32(lnk.CommandArgument);
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
                //BindDateTimeControl("first");
                if (dt.Rows.Count != rptItem.ItemIndex + 1)
                {
                    BindDateTimeControl("last");
                }
                else
                {
                    BindDateTimeControl("first");
                }

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

                        //int itemId = 0;
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

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))// 5)
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(txtGoal.Text)))
                            lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                        if (!string.IsNullOrEmpty(Convert.ToString(txtWeightage.Text)))
                            lstItem["agWeightage"] = Convert.ToInt32(txtWeightage.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Goals approved";
                    lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                    lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    lstItem["chRole"] = "Appraiser";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;

                }

                Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('" + CustomMessages.Update + "'); </script>");

                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Item Saved Successfully')</script>");

            }

        }

        private void SaveCompetencies(int appraisalID, int appraisalPhaseId)
        {

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    objWeb.AllowUnsafeUpdates = true;

                    SPList lstGoalSettings = objWeb.Lists["Appraisal Competencies"];
                    SPListItem lstItem;

                    foreach (RepeaterItem item in rptCompetencies.Items)
                    {
                        Label lblcompetencies = item.FindControl("lblcompetencies") as Label;
                        Label lblDescriptionValue = item.FindControl("lblDescriptionValue") as Label;
                        DropDownList ddlExpectedResult = item.FindControl("ddlExpectedResult") as DropDownList;
                        Label lblCompetencyId = item.FindControl("lblCompetencyId") as Label;
                        if (!Convert.ToBoolean(hfMasterCompetencies.Value))
                        {
                            if (string.IsNullOrEmpty(lblCompetencyId.Text))
                            {
                                lstItem = lstGoalSettings.AddItem();
                            }
                            else
                            {
                                lstItem = lstGoalSettings.GetItemById(Convert.ToInt32(lblCompetencyId.Text));
                            }
                        }
                        else
                        {
                            lstItem = lstGoalSettings.AddItem();
                        }

                        lstItem["acmptAppraisalId"] = appraisalID;
                        lstItem["acmptAppraisalPhaseId"] = appraisalPhaseId;           //Convert.ToInt32(lstItemAppraisal["ID"]);
                        lstItem["acmptCompetency"] = Convert.ToString(lblcompetencies.Text);
                        if (ddlExpectedResult.SelectedIndex > 0)
                            lstItem["acmptExpectedResult"] = Convert.ToString(ddlExpectedResult.SelectedItem.Text);
                        lstItem["acmptDescription"] = Convert.ToString(lblDescriptionValue.Text);

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }
                }
            }

        }

        private void EnableResultDropdown(DataTable dt)
        {
            try
            {

                foreach (RepeaterItem item in rptCompetencies.Items)
                {
                    DropDownList ddl = item.FindControl("ddlExpectedResult") as DropDownList;
                    Label lblexectedresult = item.FindControl("lblexectedresult") as Label;
                    lblexectedresult.Visible = false;
                    ddl.Visible = true;
                    if (ddl.Items.FindByText(dt.Rows[item.ItemIndex]["acmptExpectedResult"].ToString()) != null)
                        ddl.Items.FindByText(dt.Rows[item.ItemIndex]["acmptExpectedResult"].ToString()).Selected = true;
                }
            }
            catch (Exception ex)
            {

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
