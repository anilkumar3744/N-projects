using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_TMTActions
{
    public partial class AppraisalTaskEditPage : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["ID"] != null)
            {
                SPListItem taskItem = null;
                SPList appraisalTasks;
                SPList appraisalStatus;

                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        appraisalTasks = currentWeb.Lists[new Guid(Request.Params["List"])];
                        appraisalStatus = currentWeb.Lists["Appraisal Status"];

                        taskItem = appraisalTasks.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    }

                    if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(1)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString() + "AppraisalId=" + Convert.ToString(taskItem["tskAppraisalId"]), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(2)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(3)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(4)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(5)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(6)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(7)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(8)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(9)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(10)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                    else if (Convert.ToString(taskItem["tskStatus"]) == Convert.ToString(appraisalStatus.GetItemById(11)["Appraisal_x0020_Workflow_x0020_S"]))
                    {
                        Response.Redirect(SPContext.Current.Web.Url + "/_layouts/VFS_ApplicationPages/InitialGoalSetting.aspx?TaskID=" + Request.Params["ID"].ToString(), false);
                        //Response.End();
                    }
                }
            }
           
        }
    }
}
