using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages
{
    public partial class Remainders : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Request.Params["ID"] != null)
                {
                    if (Request.Params["Source"] != null)
                    {
                        if (Request.Params["RootFolder"] != null)
                        {
                            // View
                            
                            txtWorkFlowvalue.Visible = false;
                            ddlRecurring.Visible = false;
                            ddlRemainderRequest.Visible = false;
                            txtduration.Visible = false;
                            btnSave.Visible = false;
                            btnUpdate.Visible = false;
                            ViewDetails();
                        }
                        else
                        {
                            // Edit
                            txtWorkFlowvalue.Visible = false;
                            btnSave.Visible = false;
                            UpdateDetails();
                        }
                    }
                    else
                    {
                        // edit
                        txtWorkFlowvalue.Visible = false;
                        btnSave.Visible = false;
                        UpdateDetails();
                    }
                }
                else
                {
                    // new

                } 
            }
        }
    

        private void UpdateDetails()
        {
            
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb oweb = osite.OpenWeb())
                {
                    SPList list = oweb.Lists["Reminders"];
                    SPListItem item = list.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    lblworkflowvalue.Text = Convert.ToString(item["rmdWorkflowState"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(item["rmdRemaindrRequirment"])))
                        ddlRemainderRequest.Items.FindByText(Convert.ToString(item["rmdRemaindrRequirment"])).Selected = true;
                    if (!string.IsNullOrEmpty(Convert.ToString(item["rmdRecurring"])))
                        ddlRecurring.Items.FindByText(Convert.ToString(item["rmdRecurring"])).Selected = true;
                    txtduration.Text = Convert.ToString(item["rmdDuration"]);

                }
            }
        }
        protected void ddlRemainderRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRemainderRequest.SelectedIndex == 0)
            {
                ddlRecurring.Items.Remove("Yes");
                txtduration.Enabled = false;
                if (ddlRecurring.SelectedIndex == 0)
                {
                   // lblRemainderRequestvalue.Visible = true;
                }
            }
            else 
            {
                txtduration.Enabled = true;
                //lblRemainderRequestvalue.Visible = false;
                if (ddlRecurring.Items.Count<2)
                {
                    ddlRecurring.Items.Add("Yes");
                    
                }
                
            }

        }
        private void ViewDetails()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    lblworkflowvalue.Text = Convert.ToString(lstItem["rmdWorkflowState"]);
                    lblRemainderRequestView.Text = Convert.ToString(lstItem["rmdRemaindrRequirment"]);
                    lblRecurringVIew.Text = Convert.ToString(lstItem["rmdRecurring"]);
                    lbldureationView.Text = Convert.ToString(lstItem["rmdDuration"]);

                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oweb = osite.OpenWeb())
                    {
                        SPList list = oweb.Lists["Reminders"];
                        SPListItem item = list.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                        item["rmdWorkflowState"] = Convert.ToString(lblworkflowvalue.Text);
                        item["rmdRemaindrRequirment"] = Convert.ToString(ddlRemainderRequest.SelectedItem.Text);
                        item["rmdDuration"] = Convert.ToInt32(txtduration.Text);
                        item["rmdRecurring"] = ddlRecurring.SelectedItem.Text;
                        oweb.AllowUnsafeUpdates = true;
                        item.Update();
                        oweb.AllowUnsafeUpdates = false;
                        popup();
                    }
                }
            }
            catch (Exception ex)
            {
                
                 LogHandler.LogError(ex, "Error in PMS Reminders Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            popup();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oweb = osite.OpenWeb())
                        {
                            SPList list = oweb.Lists["Reminders"];
                            SPListItem item = list.Items.Add();
                            item["rmdWorkflowState"] = Convert.ToString(txtWorkFlowvalue.Text);
                            item["rmdRemaindrRequirment"] = Convert.ToString(ddlRemainderRequest.SelectedItem.Text);
                            item["rmdDuration"] = Convert.ToInt32(txtduration.Text);
                            item["rmdRecurring"] = ddlRecurring.SelectedItem.Text;
                            oweb.AllowUnsafeUpdates = true;
                            item.Update();
                            oweb.AllowUnsafeUpdates = false;
                            popup();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                
                 LogHandler.LogError(ex, "Error in PMS Reminders Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void popup()
        {
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.Flush();
            Context.Response.End();
        }

    }
}
