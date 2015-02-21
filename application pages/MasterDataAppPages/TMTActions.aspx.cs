using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages
{
    public partial class TMTActions : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void BtnGoalSetting_Click(object sender, EventArgs e)
        {
            btnGoalSetting.Visible = false;
            btnH1SelfEvaluation.Visible = true;
            lblH1SelfEvaluationH1S.Visible = false;
            SPListItem masteritem = GetEmployeeMaster();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList appraisals = currentWeb.Lists["Appraisals"];


                    //int i = 0;
                    //foreach (SPListItem item in masterCollection)
                    //{

                    SPListItem appraisalItem = appraisals.AddItem();
                    appraisalItem["appPerformanceCycle"] = DateTime.Now.Year.ToString();
                    appraisalItem["appEmployeeCode"] = masteritem["EmployeeCode"].ToString();
                    appraisalItem["appAppraisalStatus"] = "H1 - Awiting Appraisee Goal Settinng";

                    appraisalItem["appH1GoalSettingStartDate"] = Convert.ToDateTime(DateTime.Now);

                    appraisalItem["appAppraiserCode"] = Convert.ToString(masteritem["DepartmentHead_x003a_EmployeeCod"]);
                    appraisalItem["appReviewerCode"] = Convert.ToString(masteritem["ImmediateSupervisor_x003a_Employ"]);
                    appraisalItem["appHRBusinessPartnerCode"] = Convert.ToString(masteritem["HREmployeeCode_x003a_EmployeeCod"]);
                    Web.AllowUnsafeUpdates = true;
                    appraisalItem.Update();


                    //}

                    //SPListItemCollection appraisalCollection = appraisals.GetItems();
                    //foreach (SPListItem item in appraisalCollection)
                    //{

                    const string TDS_GUID = "eaa1c0d6-b879-4a27-a0f6-5be96b3969e8";
                    SPWorkflowManager WFmanager = SPContext.Current.Site.WorkflowManager;
                    SPWorkflowAssociation WFAssociations = appraisals.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
                    SPWorkflowCollection workflowColl = WFmanager.GetItemWorkflows(appraisalItem);

                    if (workflowColl.Count == 0)
                    {
                        SPWorkflow workflow = WFmanager.StartWorkflow(appraisalItem, WFAssociations, "", true);
                    }
                    //}
                    Web.AllowUnsafeUpdates = false;
                    SPList tmtActions = currentWeb.Lists[new Guid(Request.Params["List"])];

                    SPListItem listItem = tmtActions.AddItem();

                    listItem["tmtPerformanceCycle"] = "";
                    listItem["tmtIsH1GoalSettingStarted"] = "Started";
                    Web.AllowUnsafeUpdates = true;
                    listItem.Update();
                    Web.AllowUnsafeUpdates = false;

                }


            }
        }
        private SPListItem GetEmployeeMaster()
        {
            SPListItem masterItem = null;
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList employeMaster = currentWeb.Lists["Employee Masters"];

                        return masterItem = employeMaster.GetItemById(2639);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
