using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages
{
    public partial class Competencies : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.Request.Params["ID"] != null)
                {
                    if (Request.Params["Source"] != null)
                    {
                        if (Request.Params["RootFolder"] != null)
                        {
                            // View
                            ViewDetails();

                        }
                        else
                        {
                            // Edit
                            UpdateDetails();

                        }
                    }
                    else
                    {
                        // edit
                        UpdateDetails();

                    }
                }
                //else
                //{
                //    // new

                //}
            }
        }

        private void UpdateDetails()
        {
            lbledit.Text = "Edit";
            btnEdit.Visible = false;
            btnDelete.Visible = false;

            txtCompetency.Visible = true;
            btnSave.Visible = true;
            lblCompetencyValue.Visible = false;

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    txtCompetency.Text = Convert.ToString(lstItem["cmptCompetency1"]);

                    btnSave.Text = "Update";
                }
            }
        }

        private void ViewDetails()
        {
            lbledit.Text = "View";
            btnEdit.Visible = true;
            btnDelete.Visible = true;

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));
                    txtCompetency.Visible = false;
                    btnSave.Visible = false;

                    lblCompetencyValue.Visible = true;

                    lblCompetencyValue.Text = Convert.ToString(lstItem["cmptCompetency1"]);
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    string strMessage = string.Empty;
                    if (Request.Params["ID"] != null)
                    {
                        SaveItem(false, Convert.ToInt32(Request.Params["ID"]));
                        strMessage = "Item Updated Successfully";
                    }
                    else
                    {
                        SaveItem(true, 0);
                        strMessage = "Item Saved Successfully";
                    }

                    Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                    Context.Response.Flush();
                    Context.Response.End();
                });
            }
            catch (Exception ex)
            {
                
                 LogHandler.LogError(ex, "Error in PMS Competency Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
         
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.Flush();
            Context.Response.End();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            try
            {
                UpdateDetails();

            }
            catch (Exception ex)
            {
                
              LogHandler.LogError(ex, "Error in PMS Competency Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strMessage = string.Empty;
                delete(Convert.ToInt32(Request.Params["ID"]));
                strMessage = "Item Deleted Successfully";
                Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                Context.Response.Flush();
                Context.Response.End();
            }
            catch (Exception ex)
            {
                
                LogHandler.LogError(ex, "Error in PMS Competency Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
         
            }
        }
        
        public void delete(int listitemid)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPList competencyDescriptions = objWeb.Lists[new Guid(Request.Params["List"])];
                        SPListItem descriptionsItem = competencyDescriptions.GetItemById(listitemid);
                        descriptionsItem["Status"] = false;
                        objWeb.AllowUnsafeUpdates = true;
                        descriptionsItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Competency Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }
        }
        
        public void SaveItem(bool NewItem, int ItemID)
        {
            try
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objWeb = osite.OpenWeb())
                    {
                        SPList lstPOCreation = objWeb.Lists[new Guid(Request.Params["List"])];
                        SPListItem lstItem;
                        if (NewItem)
                            lstItem = lstPOCreation.AddItem();
                        else
                        {
                            lstItem = lstPOCreation.GetItemById(ItemID);
                        }

                        lstItem["cmptCompetency1"] = txtCompetency.Text.Trim();

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }

                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }
    }
}
