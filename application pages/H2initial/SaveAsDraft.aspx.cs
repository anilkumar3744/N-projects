using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.H2initial
{
    public partial class SaveAsDraft : LayoutsPageBase
    {
        DataTable dummyTable;
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
                    SPList lstAppraisalTasks;
                    SPListItem taskItem;
                    SPUser appraisee;
                    SPListItem appraisalItem;

                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb currentWeb = osite.OpenWeb())
                        {
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            //lstAppraisalTasks = currentWeb.Lists["VFSAppraisalTasks"];
                            //taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["TaskID"]));

                            //lblStatusValue.Text = Convert.ToString(taskItem["tskStatus"]);   //"Awaiting Appraiser Goal Approval";
                            //hfAppraisalID.Value = Convert.ToString(taskItem["tskAppraisalId"]);
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(appraisalItem["appPerformanceCycle"]);
                            lblStatusValue.Text = Convert.ToString(appraisalItem["appAppraisalStatus"]);

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

                            SPList history = currentWeb.Lists["Comments History"];
                            SPQuery q = new SPQuery();
                            q.Query = "<Where><And><Eq><FieldRef Name='chRole' /><Value Type='Text'>Appraisee</Value></Eq><Eq><FieldRef Name='chReferenceId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalPhaseID.Value) + "</Value></Eq></And></Where>";

                            SPListItemCollection col = history.GetItems(q);
                            if (col != null && col.Count > 0)
                            {
                                SPListItem historyItem = col[0];
                                txtComments.Text = Convert.ToString(historyItem["chComment"]);
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

                        string appraisercode;
                        appraisercode = Convert.ToString(appraiseeData["ReportingManagerEmployeeCode"]);

                        appLog.Value = CommonMaster.GetUserByCode(appraisercode);
                        //this.dummyTable = CommonMaster.GetGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value));
                        this.dummyTable = CommonMaster.GetDraftGoalsDetails(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name);
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

                        if (Convert.ToInt32(hfldMandatoryGoalCount.Value) != 0)   ////this.dummyTable.Rows.Count >= 5
                        {
                            MoreGoals(this.dummyTable.Rows.Count);
                            SelectedDropDown(this.dummyTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Save As Draft");
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
                    if (item.ItemIndex <= count)
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
            catch (Exception)
            {

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

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5
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
                LogHandler.LogError(ex, "Error in PMS H2 Save As Draft");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        private void MoreGoals(int rowsCount)
        {
            try
            {
                //// DataTable dtCategories = CommonMasters.GetCategories();
                DataTable dtCategories = CommonMaster.GetOptionalGoalCategories();
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))////item.ItemIndex >= 5
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
            catch (Exception)
            {

            }
        }

        private void SelectedDropDown(DataTable dt)
        {
            try
            {
                foreach (RepeaterItem item in rptGoalSettings.Items)
                {
                    if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5
                    {
                        DropDownList ddl = (DropDownList)item.FindControl("ddlCategories");
                        if (dt.Rows[item.ItemIndex]["agGoalCategory"].ToString() != string.Empty)
                        {
                            ddl.Items.FindByText(dt.Rows[item.ItemIndex]["agGoalCategory"].ToString()).Selected = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    //hfflag.Value = "true";
                    //UpadteGoals(Convert.ToInt32(Request.Params["ID"]), 0);
                    hfflag.Value = "true";
                    UpadteGoalsToDraft(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));

                    BingExistingGoals();
                });
            }
            catch (Exception ex)
            {

                LogHandler.LogError(ex, "Error in PMS H1 Save as Draft");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");


            }
        }

        private void UpadteGoalsToDraft(int appraisalID, int appraisalPhaseId)
        {

            CommonMaster.DeleteAppraisalGoalsDraft(Convert.ToInt32(hfAppraisalPhaseID.Value), this.currentUser.Name, "Appraisal Goals Draft");
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

                        //if (string.IsNullOrEmpty(lblID.Text))
                        //{
                        lstItem = lstGoalSettings.AddItem();

                        lstItem["agAppraisalId"] = appraisalID;
                        lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
                        //}
                        //else
                        //{
                        //    lstItem = lstGoalSettings.GetItemById(Convert.ToInt32(lblID.Text));
                        //}

                        if (item.ItemIndex >= Convert.ToInt32(hfldMandatoryGoalCount.Value))
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(ddlCategories.SelectedItem.Text);
                            lstItem["IsMandatory"] = false;
                        }
                        else
                        {
                            lstItem["agGoalCategory"] = Convert.ToString(lblCategory.Text);
                            lstItem["IsMandatory"] = true;
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

                }

                Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('" + CustomMessages.Update + "'); </script>");

                // Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + string.Format(CustomMessages.Update, "H1") + "')</script>");
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
            BindDateTimeControl("last");

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
                        UpadteGoals(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        //}

                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                SPList list = web.Lists["Appraisals"];
                                SPList appraisalStatus = web.Lists["Appraisal Status"];

                                SPList lstAppraisalTasks = web.Lists["VFSAppraisalTasks"];
                                SPListItem taskItem = lstAppraisalTasks.GetItemById(Convert.ToInt32(Request.Params["AppId"]));

                                SPListItem lstStatusItem = appraisalStatus.GetItemById(2);

                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));  //Request.Params["ID"]));

                                appraisalItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                web.AllowUnsafeUpdates = true;
                                appraisalItem.Update();

                                Hashtable ht = new Hashtable();
                                ht["tskStatus"] = "Approved";
                                ht["Status"] = "Approved";
                                SPWorkflowTask.AlterTask(taskItem, ht, true);
                                web.AllowUnsafeUpdates = false;

                                Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self');alert('Goals submitted successfully.'); </script>");
                            }

                        }
                    });
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('APPRAISER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS H2 Save As Draft");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {

            Context.Response.Write("<script type='text/javascript'>window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
            //Response.Redirect(SPContext.Current.Site.Url);

        }

        protected void LnkDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = sender as LinkButton;
                //if (!string.IsNullOrEmpty(lnk.CommandArgument))
                //{
                //    int itemID = Convert.ToInt32(lnk.CommandArgument);
                //    CommonMaster.DeleteGoalsItem(itemID);
                //}
                RepeaterItem rptItem = lnk.NamingContainer as RepeaterItem;
                Label lblSno = rptItem.FindControl("lblSno") as Label;
                this.dummyTable = ViewState["Appraisals"] as DataTable;

                int sNo = Convert.ToInt32(lblSno.Text);
                int i = 0;
                if (sNo != rptGoalSettings.Items.Count)
                {
                    foreach (RepeaterItem item in rptGoalSettings.Items)
                    {
                        DataRow dr = this.dummyTable.Rows[i];
                        dr["SNo"] = (item.FindControl("lblSno") as Label).Text;

                        if (i >= Convert.ToInt32(hfldMandatoryGoalCount.Value))//5
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
                        dr["agWeightage"] = Convert.ToInt32((item.FindControl("txtWeightage") as TextBox).Text);
                        dr["agGoalDescription"] = (item.FindControl("txtDescription") as TextBox).Text;
                        i++;
                        dummyTable.AcceptChanges();
                    }
                }

                this.dummyTable.Rows.RemoveAt(Convert.ToInt32(lblSno.Text) - 1);
                i = 0;
                foreach (DataRow row in this.dummyTable.Rows)
                {
                    DataRow dr = this.dummyTable.Rows[i];
                    dr["SNo"] = i + 1;
                    i++;
                }
                rptGoalSettings.DataSource = this.dummyTable;
                rptGoalSettings.DataBind();

                if (this.dummyTable.Rows.Count != rptItem.ItemIndex + 1)
                {
                    BindDateTimeControl("last");
                }
                else
                {
                    BindDateTimeControl("first");
                }

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

                        //if (string.IsNullOrEmpty(lblID.Text))
                        //{
                        lstItem = lstGoalSettings.AddItem();

                        lstItem["agAppraisalId"] = appraisalID;
                        lstItem["agAppraisalPhaseId"] = appraisalPhaseId;
                        //}
                        //else
                        //{
                        //    lstItem = lstGoalSettings.GetItemById(Convert.ToInt32(lblID.Text));
                        //}

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

                        if (!string.IsNullOrEmpty(Convert.ToString(txtGoal.Text)))
                            lstItem["agGoal"] = Convert.ToString(txtGoal.Text);
                        lstItem["agDueDate"] = Convert.ToDateTime(dc.SelectedDate.ToString("dd-MMM-yyyy"));
                        if (!string.IsNullOrEmpty(Convert.ToString(txtWeightage.Text)))
                            lstItem["agWeightage"] = Convert.ToString(txtWeightage.Text);
                        if (!string.IsNullOrEmpty(Convert.ToString(txtDescription.Text)))
                            lstItem["agGoalDescription"] = Convert.ToString(txtDescription.Text);
                        lstItem["Author"] = this.currentUser;
                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;

                    }

                    SPList lstCommentsHistory = objWeb.Lists["Comments History"];

                    lstItem = lstCommentsHistory.AddItem();
                    lstItem["chCommentFor"] = "Goals Setting";
                    lstItem["chReferenceId"] = Convert.ToInt32(appraisalID);
                    if (!string.IsNullOrEmpty(Convert.ToString(txtComments.Text)))
                        lstItem["chComment"] = txtComments.Text;
                    lstItem["chCommentedBy"] = this.currentUser;
                    lstItem["chCommentedDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    lstItem["chActor"] = lblEmpNameValue.Text;
                    lstItem["chRole"] = "Appraisee";

                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                    if (appraisalPhaseId == 0)

                        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Goals saved successfully');</script>");
                    else

                        Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Goals submitted successfully');</script>");
                }

                string url = SPContext.Current.Web.Url + "/_layouts/H2initial/H2AmmendGoals.aspx?AppId=" + hfAppraisalID.Value;
                Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + string.Format(CustomMessages.Saved, "H1") + "'); </script>");


                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Gaols has been added successfully')</script>");
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
