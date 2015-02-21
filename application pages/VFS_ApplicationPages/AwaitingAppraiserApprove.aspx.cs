using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages
{
    public partial class AwaitingAppraiserApprove : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAppraiser_Click(object sender, EventArgs e)
        {

            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Appraiser Approved";
            ht["Status"] = "Appraiser Approved";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        private void CommitPOPup()
        {
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.End();
            Context.Response.Flush();
        }

        protected void btnAppraisee_Click(object sender, EventArgs e)
        {

            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Awaiting Appraiser Approves";
            ht["Status"] = "Goals changed.";

            SPWorkflowTask.AlterTask(taskItem, ht, true);
            CommitPOPup();
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    web.AllowUnsafeUpdates = true;

                    SPList list = web.Lists["Goal Settings"];

                    SPListItem CurrentListItem = list.GetItemById(1);

                    const string TDS_GUID = "2e1337fd-7aec-40f0-b008-b1c2af939c97";
                    SPWorkflowManager WFmanager = SPContext.Current.Site.WorkflowManager;
                    SPWorkflowAssociation WFAssociations = list.WorkflowAssociations.GetAssociationByBaseID(new Guid(TDS_GUID));
                    SPWorkflowCollection workflowColl = WFmanager.GetItemWorkflows(CurrentListItem);

                    web.AllowUnsafeUpdates = false;
                }

            }
        }

        protected void btnSelfEvaluation_Click(object sender, EventArgs e)
        {
            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Self evaluation complted";
            ht["Status"] = "Self evaluation complted";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        protected void btnAppraiserEvaluation_Click(object sender, EventArgs e)
        {

            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Appraiser Evaluation Approved";
            ht["Status"] = "Approved";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        protected void btnReviewerApprove_Click(object sender, EventArgs e)
        {
            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Reviewer Approved";
            ht["Status"] = "Approved";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        protected void btnAppraiseeSignOff_Click(object sender, EventArgs e)
        {

            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Sign Off";
            ht["Status"] = "Appraisee Sign Off complted";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        protected void btnAppeal_Click(object sender, EventArgs e)
        {
            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Appeal";
            ht["Status"] = "Appraisee Appeal";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        protected void btnHRClose_Click(object sender, EventArgs e)
        {
            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Close";
            ht["Status"] = "Close";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        protected void btnHRRequest_Click(object sender, EventArgs e)
        {

            SPListItem taskItem;
            taskItem = GetTaskListItem();
            Hashtable ht = new Hashtable();

            ht["glsTaskStatus"] = "Request";
            ht["Status"] = "Request";

            SPWorkflowTask.AlterTask(taskItem, ht, true);

            CommitPOPup();
        }

        private SPListItem GetTaskListItem()
        {
            SPList taskList;
            SPListItem taskItem;

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb web = osite.OpenWeb())
                {
                    taskList = web.Lists[new Guid(Request.Params["List"].ToString())];

                    taskItem = taskList.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                }
                return taskItem;

            }
        }
    }
}
