using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;
using Microsoft.SharePoint.Workflow;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_TMTActions
{
    public partial class WorkflowDeactivation : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
        }

        protected void BtnDeactivate_Click(object sender, EventArgs e)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList appraisalTasks = objWeb.Lists["Employees Master"];
                    SPListItemCollection itemColl = appraisalTasks.GetItems();

                    objWeb.AllowUnsafeUpdates = true;

                    foreach (SPListItem item in itemColl)
                    {
                        item["Status"] = true;
                        item.Update();
                    }
                    objWeb.AllowUnsafeUpdates = false;
                    //SPQuery q = new SPQuery();
                    ////q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H2 - Goals Approved</Value></Eq></And></Where>";

                    //q.Query = "<Where><And><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Request.Params["AppId"] + "</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Choice'>Not started</Value></Eq></And></Where>";
                    //SPListItemCollection taskItemCollection = appraisalTasks.GetItems(q);
                    //SPListItem item = taskItemCollection[0];

                    //SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];

                    //Hashtable ht = new Hashtable();

                    //ht["tskStatus"] = "Deactivation";
                    //ht["Status"] = "Deactivation";

                    //SPWorkflowTask.AlterTask(item, ht, true);

                    //SPList appraisals = objWeb.Lists["Appraisals"];
                    //SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
                    //appraisalItem["appAppraisalStatus"] = "H1 Deactivated";
                    //objWeb.AllowUnsafeUpdates = true;
                    //appraisalItem.Update();
                    //objWeb.AllowUnsafeUpdates = false;

                }
            }
        }

        protected void BtnH2Start_Click(object sender, EventArgs e)
        {
            //using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            //{
            //    using (SPWeb objWeb = osite.OpenWeb())
            //    {
            //        SPList appraisalTasks = objWeb.Lists["VFSAppraisalTasks"];
            //        SPQuery q = new SPQuery();
            //        //q.Query = "<Where><And><IsNull><FieldRef Name='AssignedTo' /></IsNull><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>H2 - Goals Approved</Value></Eq></And></Where>";

            //        q.Query = "<Where><And><Eq><FieldRef Name='tskAppraisalId' /><Value Type='Number'>" + Request.Params["AppId"] + "</Value></Eq><Eq><FieldRef Name='tskStatus' /><Value Type='Text'>Awiting H2 Activation</Value></Eq></And></Where>";
            //        SPListItemCollection taskItemCollection = appraisalTasks.GetItems(q);
            //        SPListItem item = taskItemCollection[0];

            //        SPList lstAppraisalStatus = objWeb.Lists["Appraisal Status"];

            //        Hashtable ht = new Hashtable();

            //        ht["tskStatus"] = "H2 Started";
            //        ht["Status"] = "H2 Started";

            //        SPWorkflowTask.AlterTask(item, ht, true);

            //        SPList appraisals = objWeb.Lists["Appraisals"];
            //        SPListItem appraisalItem = appraisals.GetItemById(Convert.ToInt32(item["tskAppraisalId"]));
            //        appraisalItem["appAppraisalStatus"] = "H1 – Completed";
            //        objWeb.AllowUnsafeUpdates = true;
            //        appraisalItem.Update();
            //        objWeb.AllowUnsafeUpdates = false;

            //    }
            //}
            // }

            using (SPLongOperation longOperation = new SPLongOperation(this.Page))
            {
                //Custom Messages on the Spinning Wheel Screen
                longOperation.LeadingHTML = "Provisioning Sites";
                longOperation.TrailingHTML = "Please wait while the sites are being provisioned.";

                //Start the long operation
                longOperation.Begin();

                for (int i = 0; i < 100000000; i++)
                {
                    
                }

                //End the long operation
                string redirectURL = SPContext.Current.Web.Url + "/SitePages/SiteProvisioning.aspx";
                longOperation.End(redirectURL);
            }
        }

    }
}
