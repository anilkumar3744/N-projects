using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Linq;
using System.Data;
using VFS.PMS.ArchivalProject;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using VFS.PMS.ArchivalProject.VFSReportService;

namespace VFS.PMS.ArchivalProject.AppraisalArchiveWebPart
{
    public partial class VisualWebPart1UserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                SPList cyclesList = web.Lists["Performance Cycle Activity"];
                                SPQuery query = new SPQuery();
                                query.Query = "<Where><IsNotNull><FieldRef Name=CycleClosedBy/></IsNotNull></Where>";
                                DataTable dtPerformanceCycles = cyclesList.GetItems(query).GetDataTable();
                                this.ddlCycles.DataSource = dtPerformanceCycles;
                                this.ddlCycles.DataTextField = "PerformanceCycle";
                                this.ddlCycles.DataValueField = "PerformanceCycle";
                                this.ddlCycles.DataBind();
                                using (VFSPMSEntitiesDataContext pmsContext = new VFSPMSEntitiesDataContext(web.Url))
                                {
                                    double? cycle = (from perCycles in pmsContext.PerformanceCycles.AsEnumerable() select perCycles.PerformanceCycle).ToList().Max();
                                    hfldcycle.Value = Convert.ToString(cycle);
                                }
                                //hfldcycle.Value = dtPerformanceCycles.Rows[dtPerformanceCycles.Rows.Count - 1]["PerformanceCycle"].ToString();
                            }

                        }
                    });

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnArchive_Click(object sender, EventArgs e)
        {
            DataTable dtAppraisals = new DataTable();
            string redirectUrl = SPContext.Current.Web.Url;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPLongOperation longOperation = new SPLongOperation(this.Page))
                {
                    try
                    {
                        longOperation.LeadingHTML = "Archiving appraisals.";
                        longOperation.TrailingHTML = "Please wait while the appraisals being archived.";
                        longOperation.Begin();

                        using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                        {
                            using (SPWeb web = osite.OpenWeb())
                            {
                                SPList AppraisalsList = web.Lists["Appraisals"];
                                SPQuery AppraisalQuery = new SPQuery();
                                AppraisalQuery.Query = "<Where><Eq><FieldRef Name=appPerformanceCycle /><Value Type=Number>" + this.ddlCycles.SelectedValue + "</Value></Eq></Where><OrderBy><FieldRef Name=ID Ascending='True' /></OrderBy>";

                                #region uncomment this line if you wnat to archive single record
                                ////AppraisalQuery.Query = "<Where><And><Eq><FieldRef Name=appPerformanceCycle /><Value Type=Number>" + this.ddlCycles.SelectedValue + "</Value></Eq><Eq><FieldRef Name=appEmployeeCode /><Value Type=Text>83</Value></Eq></And></Where>";
                                #endregion

                                dtAppraisals = AppraisalsList.GetItems(AppraisalQuery).GetDataTable();
                                if (dtAppraisals != null && dtAppraisals.Rows.Count > 0)
                                {
                                    web.AllowUnsafeUpdates = true;
                                    SPList AppraisalArchivalDocLib = web.Lists["Appraisal Archivals"];
                                    String url = AppraisalArchivalDocLib.RootFolder.ServerRelativeUrl.ToString();
                                    string foldername = Convert.ToString(this.ddlCycles.SelectedValue.ToString());
                                    SPFolder SubFolder = web.GetFolder(url + "/" + foldername);
                                    if (!SubFolder.Exists)
                                    {
                                        SPFolderCollection folders = web.GetFolder(url).SubFolders;
                                        folders.Add(foldername);
                                    }
                                    for (int i = 0; i < dtAppraisals.Rows.Count; i++)
                                    {
                                        try
                                        {
                                            byte[] AppData = getAppraisalReport(this.ddlCycles.SelectedValue, dtAppraisals.Rows[i]["appEmployeeCode"].ToString());
                                            SPFile file = SubFolder.Files.Add(SubFolder.Url + "/" + "Appraisal Detais" + dtAppraisals.Rows[i]["appEmployeeCode"].ToString(), AppData, true);
                                            SPListItem item = AppraisalArchivalDocLib.Items[file.UniqueId];
                                            item["EmployeeCode"] = Convert.ToString(dtAppraisals.Rows[i]["appEmployeeCode"]);
                                            if (!string.IsNullOrEmpty(dtAppraisals.Rows[i]["appFinalRating"].ToString()))
                                                item["Rating"] = Convert.ToDouble(dtAppraisals.Rows[i]["appFinalRating"]);
                                            if (!string.IsNullOrEmpty(dtAppraisals.Rows[i]["appFinalScore"].ToString()))
                                                item["Score"] = Convert.ToDouble(dtAppraisals.Rows[i]["appFinalScore"]);
                                            item.Update();
                                            this.deleteAppraisalDetails(web.Url, Convert.ToInt32(dtAppraisals.Rows[i]["ID"]));
                                            web.AllowUnsafeUpdates = false;
                                        }
                                        catch (Exception)
                                        {

                                        }

                                    }

                                    Context.Response.Write("<script type='text/javascript'>alert('Archival completed successfully');window.open('" + redirectUrl + "','_self'); </script>");
                                }
                                else
                                {
                                    redirectUrl = redirectUrl + "/SitePages/Archival.aspx";
                                    Context.Response.Write("<script type='text/javascript'>alert('There are no appraisals to archive for the selected cycle.');window.open('" + redirectUrl + "','_self'); </script>");
                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        longOperation.End(redirectUrl);
                    }
                    longOperation.End(redirectUrl);
                }

            });

        }

        private Byte[] getAppraisalReport(string performanceCycle, string employeeCode)
        {

            byte[] result = null;
            try
            {
                ReportExecutionService rs = new ReportExecutionService();
                //System.Net.CredentialCache.DefaultNetworkCredentials.UserName = @"dc\Administrator";
                //System.Net.CredentialCache.DefaultNetworkCredentials.Password = @"Hello@123#";
                rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                //System.Net.NetworkCredential nc = new System.Net.NetworkCredential("Administrator", "Hello@123#", "DC");
                //rs.Credentials = nc;
                //rs.Credentials.GetCredential(
                ////rs.Url = "http://sqlsrv01/ReportServer/ReportExecution2005.asmx";
                string reportPath = "/VFSPMSReports/H1andH2";
                string format = "PDF";
                string historyID = null;
                string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";
                ParameterValue[] parameters = new ParameterValue[2];
                parameters[0] = new ParameterValue();
                parameters[0].Name = "Cycle";
                parameters[0].Value = performanceCycle;

                parameters[1] = new ParameterValue();
                parameters[1].Name = "EmployeeCode";
                parameters[1].Value = employeeCode;
                string encoding;
                string mimeType;
                string extension;
                Warning[] warnings = null;
                string[] streamIDs = null;
                ExecutionInfo execInfo = new ExecutionInfo();
                ExecutionHeader execHeader = new ExecutionHeader();
                rs.ExecutionHeaderValue = execHeader;
                execInfo = rs.LoadReport(reportPath, historyID);
                rs.SetExecutionParameters(parameters, "en-us");
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                execInfo = rs.GetExecutionInfo();
                return result;
            }
            catch (Exception ex)
            {
                ////WriteintoLogFile("getStudentReport", ex);
                throw ex;
            }

        }

        protected void deleteAppraisalDetails(string siteUrl, int appraisalId)
        {
            try
            {
                using (VFSPMSEntitiesDataContext data = new VFSPMSEntitiesDataContext(siteUrl))
                {
                    var appraisal = (from app in data.Appraisals.AsEnumerable() where Convert.ToInt32(app.Id) == appraisalId select app);
                    var goals = (from goal in data.AppraisalGoals.AsEnumerable()
                                 where goal.AppraisalId == Convert.ToInt32(appraisalId)
                                 select goal);
                    var GoalsDraft = (from goal in data.AppraisalGoalsDraft.AsEnumerable()
                                      where goal.AppraisalId == Convert.ToInt32(appraisalId)
                                      select goal);
                    var competencies = (from comp in data.AppraisalCompetencies.AsEnumerable()
                                        where Convert.ToString(comp.AppraisalId) == Convert.ToString(appraisalId)
                                        select comp);
                    var CompetenciesDraft = (from comp in data.AppraisalCompetenciesDraft.AsEnumerable()
                                             where Convert.ToString(comp.AppraisalId) == Convert.ToString(appraisalId)
                                             select comp);
                    var developmentMeasures = (from DMeasures in data.AppraisalDevelopmentMeasures.AsEnumerable()
                                               where Convert.ToString(DMeasures.AppraisalID) == Convert.ToString(appraisalId)
                                               select DMeasures);
                    var DevelopmentMeasuresDraft = (from DMeasures in data.AppraisalDevelopmentMeasuresDraft.AsEnumerable()
                                                    where Convert.ToString(DMeasures.AppraisalID) == Convert.ToString(appraisalId)
                                                    select DMeasures);
                    var PIP = (from appPIP in data.PIP
                               where Convert.ToString(appPIP.AppraisalID) == Convert.ToString(appraisalId)
                               select appPIP);
                    var PIPDraft = (from appPIP in data.PIPDraft
                                    where Convert.ToString(appPIP.AppraisalID) == Convert.ToString(appraisalId)
                                    select appPIP);
                    var AppPhases = (from appPhases in data.AppraisalPhases
                                     where Convert.ToString(appPhases.Appraisal) == Convert.ToString(appraisalId)
                                     select appPhases);
                    if (PIPDraft != null)
                        data.PIPDraft.DeleteAllOnSubmit(PIPDraft);
                    if (PIP != null)
                        data.PIP.DeleteAllOnSubmit(PIP);
                    if (DevelopmentMeasuresDraft != null)
                        data.AppraisalDevelopmentMeasuresDraft.DeleteAllOnSubmit(DevelopmentMeasuresDraft);
                    if (developmentMeasures != null)
                        data.AppraisalDevelopmentMeasures.DeleteAllOnSubmit(developmentMeasures);
                    if (CompetenciesDraft != null)
                        data.AppraisalCompetenciesDraft.DeleteAllOnSubmit(CompetenciesDraft);
                    if (competencies != null)
                        data.AppraisalCompetencies.DeleteAllOnSubmit(competencies);
                    if (GoalsDraft != null)
                        data.AppraisalGoalsDraft.DeleteAllOnSubmit(GoalsDraft);
                    if (goals != null)
                        data.AppraisalGoals.DeleteAllOnSubmit(goals);

                    string[] appraisalData = new string[2];
                    var H1PhaseId = from appPhases in data.AppraisalPhases
                                    where appPhases.Appraisal.ToString() == appraisalId.ToString() && appPhases.AppraisalPhase.ToString() == "H1"
                                    select appPhases.Id;
                    var H2PhaseId = from appPhases in data.AppraisalPhases
                                    where appPhases.Appraisal.ToString() == appraisalId.ToString() && appPhases.AppraisalPhase.ToString() == "H2"
                                    select appPhases.Id;
                    appraisalData[0] = Convert.ToString(H1PhaseId.FirstOrDefault());
                    appraisalData[1] = Convert.ToString(H2PhaseId.FirstOrDefault());
                    if (!string.IsNullOrEmpty(appraisalData[0]) || !string.IsNullOrEmpty(appraisalData[1]))
                    {
                        var comments = from comm in data.CommentsHistory.AsEnumerable()
                                       where Convert.ToString(comm.ReferenceId) == appraisalData[0] || Convert.ToString(comm.ReferenceId) == appraisalData[1]
                                       select comm;
                        if (comments != null)
                        {
                            data.CommentsHistory.DeleteAllOnSubmit(comments);
                        }
                    }
                    if (AppPhases != null)
                        data.AppraisalPhases.DeleteAllOnSubmit(AppPhases);

                    if (appraisal != null)
                        data.Appraisals.DeleteAllOnSubmit(appraisal);

                    data.SubmitChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
