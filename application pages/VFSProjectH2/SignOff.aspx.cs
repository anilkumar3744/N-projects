using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Collections;
using Microsoft.SharePoint.Workflow;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.VFSProjectH2
{
    public partial class SignOff : LayoutsPageBase
    {
        DataTable dummyTable, dtCompetencies, DtDevelopmentmesure, dtPip;
        SPUser currentUser;
        string pipbtn;
       
        protected string message = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.currentUser = SPContext.Current.Web.CurrentUser;
                message = SPContext.Current.Web.Url + "/_layouts/VFSProjectH1/HRView.aspx?AppId=" + Request.Params["AppId"] + "&IsDlg=1&from=popup";
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
                else if (TabContainer1.ActiveTabIndex == 3)
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


                            lblStatusValue.Text = Convert.ToString(taskItem["appAppraisalStatus"]); //
                            lblAppraisalPeriodValue.Text = "H2, " + Convert.ToString(taskItem["appPerformanceCycle"]);
                            SPListItem appraisalItem;
                            hfAppraisalID.Value = Convert.ToString(Request.Params["AppId"]);
                            SPList lstAppraisala = currentWeb.Lists["Appraisals"];

                            appraisalItem = lstAppraisala.GetItemById(Convert.ToInt32(hfAppraisalID.Value)); //


                            if (!CommonMaster.CanUserViewAppraisal(Convert.ToDouble(appraisalItem["appEmployeeCode"]), currentUser.LoginName, currentWeb))
                            {
                                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage("You are not authorized to view this Appraisal") + ";window.location.href='" + CommonMaster.DashBoardUrl + "';</script>");
                            }
                            if (!CommonMaster.CheckCurrentUserIsActor(this.currentUser, lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value), currentWeb))
                            {
                                string url = CommonMaster.NavigateToViewPage(lblStatusValue.Text, Convert.ToInt32(hfAppraisalID.Value));
                                Response.Redirect(url);
                                Response.End();
                            }



                            if (taskItem["appFinalScore"] != null)
                            {
                                lblFS.Text = Convert.ToString(taskItem["appFinalScore"]);
                            }
                            if (taskItem["appFinalRating"] != null)
                            {
                                lblfr.Text = Convert.ToString(taskItem["appFinalRating"]);
                            }

                            SPList lstAppraisalPhases = currentWeb.Lists["Appraisal Phases"]; //

                            SPQuery phasesQuery = new SPQuery(); //
                            //phasesQuery.Query = "<Where><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq></Where>";
                            phasesQuery.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollection = lstAppraisalPhases.GetItems(phasesQuery);  //
                            SPListItem phaseItem = phasesCollection[0]; //
                            hfAppraisalPhaseID.Value = Convert.ToString(phaseItem["ID"]);  //

                            SPQuery phasesQueryH1 = new SPQuery();
                            phasesQueryH1.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H1</Value></Eq></And></Where>";
                            SPListItemCollection phasesCollectionH1 = lstAppraisalPhases.GetItems(phasesQueryH1);
                            if (phasesCollectionH1.Count > 0)
                            {
                                SPListItem phaseItemH1 = phasesCollectionH1[0];
                                if (phaseItemH1["aphScore"] != null)
                                    lblscore.Text = Convert.ToString(phaseItemH1["aphScore"]);
                            }

                            if (phaseItem["aphScore"] != null)
                                lblScoretotal.Text = Convert.ToString(phaseItem["aphScore"]); //



                            if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["aphIsPIP"])))
                            {
                                pipbtn = Convert.ToString(phaseItem["aphIsPIP"]);
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
                            if (!string.IsNullOrEmpty(Convert.ToString(phaseItem["aphIsAppealed"])))
                            {
                                string s = Convert.ToString(phaseItem["aphIsAppealed"]);
                                if (s.StartsWith("Appeal"))
                                    btnAppeal.Visible = false;
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

                        string HRCode;
                        string appraiserCode;
                        HRCode = Convert.ToString(appraiseeData["HRBusinessPartnerEmployeeCode"]);
                        appraiserCode = Convert.ToString(appraiseeData["ReportingManagerEmployeeCode"]);
                        HRLog.Value = CommonMaster.GetUserByCode(HRCode);
                        appLog.Value = CommonMaster.GetUserByCode(appraiserCode);

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

                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Sign-Off");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }

        }


        protected void BtnSignoff_Click(object sender, EventArgs e)
        {
            coc.Visible = true;
            btnSignOff.Visible = false;
            btnAppeal.Visible = false;
            btnCancel.Visible = false;
        }
        protected void BtnAppeal_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(appLog.Value))
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SaveComments(Convert.ToInt32(hfAppraisalID.Value), Convert.ToInt32(hfAppraisalPhaseID.Value));
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;

                                SPList list = web.Lists["Appraisals"];
                                SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));


                                SPList appraisalStatus = web.Lists["Appraisal Status"];

                                SPListItem lstStatusItem = appraisalStatus.GetItemById(21);

                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                CurrentListItem.Update();


                                web.AllowUnsafeUpdates = false;
                            }
                            //WorkflowTriggering("Appeal");

                            Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.Appeal, "H2") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                        }
                    });
                    WorkflowTriggering("Appeal");
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('APPRAISER role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }

            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Sign-Off");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(HRLog.Value))
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        SPList lstAppraisalTasks1;  //
                        SPListItem taskItem1;
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb objWeb = osite.OpenWeb())
                            {
                                SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                                SPListItem lstItem;
                                SPQuery q = new SPQuery();
                                q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + Convert.ToInt32(hfAppraisalID.Value) + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                                SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                                lstItem = coll[0];

                                lstItem["aphSignoffonbehalfcomments"] = Convert.ToString(txtCommentsFinal.Text);
                                lstAppraisalTasks1 = objWeb.Lists["Appraisals"];   //
                                taskItem1 = lstAppraisalTasks1.GetItemById(Convert.ToInt32(Request.Params["AppId"]));  //
                                if (taskItem1["appFinalRating"] != null)
                                {
                                    lblfr.Text = Convert.ToString(taskItem1["appFinalRating"]);

                                    if (Convert.ToDouble(lblfr.Text) <= 2)
                                    {
                                        lstItem["aphIsPIP"] = "True";
                                    }
                                    else
                                        lstItem["aphIsPIP"] = "False";
                                    objWeb.AllowUnsafeUpdates = true;
                                    lstItem.Update();
                                    objWeb.AllowUnsafeUpdates = false;
                                }
                            }
                        }
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                web.AllowUnsafeUpdates = true;

                                SPList list = web.Lists["Appraisals"];
                                SPListItem CurrentListItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                SPList appraisalStatus = web.Lists["Appraisal Status"];

                                SPListItem lstStatusItem = appraisalStatus.GetItemById(19);

                                SPListItem appraisalItem = list.GetItemById(Convert.ToInt32(hfAppraisalID.Value));

                                CurrentListItem["appAppraisalStatus"] = Convert.ToString(lstStatusItem["Appraisal_x0020_Workflow_x0020_S"]);
                                CurrentListItem.Update();
                                web.AllowUnsafeUpdates = false;

                            }
                            //WorkflowTriggering("Sign Off");
                            Context.Response.Write("<script type='text/javascript'>alert('" + string.Format(CustomMessages.SignOff, "H2") + "');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                        }
                    });
                    WorkflowTriggering("Sign Off");
                }
                else
                {
                    Context.Response.Write("<script type='text/javascript'>alert('HR role user is not available for current Appraisee!');window.open('" + CommonMaster.DashBoardUrl + "','_self'); </script>");
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Sign-Off");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl);
            Response.End();
        }
        protected void BtnCancelFinal_Click(object sender, EventArgs e)
        {
            Response.Redirect(CommonMaster.DashBoardUrl);
            Response.End();
        }
        private void WorkflowTriggering(string status)
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
                            ht["tskStatus"] = status;
                            ht["Status"] = status;
                            web.AllowUnsafeUpdates = true;
                            SPWorkflowTask.AlterTask(taskItem, ht, true);
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }
            });
        }
        public void SaveComments(int appraisalID, int appraisalPhaseId)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstAppraisalPhases = objWeb.Lists["Appraisal Phases"];
                    SPListItem lstItem;
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><And><Eq><FieldRef Name='aphAppraisalId' /><Value Type='Number'>" + appraisalID + "</Value></Eq><Eq><FieldRef Name='aphAppraisalPhase' /><Value Type='Text'>H2</Value></Eq></And></Where>";
                    SPListItemCollection coll = lstAppraisalPhases.GetItems(q);

                    lstItem = coll[0];

                    lstItem["aphSignoffonbehalfcomments"] = Convert.ToString(txtCommentsFinal.Text);
                    lstItem["aphIsAppealed"] = "Appealed";
                    objWeb.AllowUnsafeUpdates = true;
                    lstItem.Update();
                    objWeb.AllowUnsafeUpdates = false;
                }
            }
        }
    }
}