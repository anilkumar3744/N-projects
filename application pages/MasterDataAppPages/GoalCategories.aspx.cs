using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages
{

    public partial class GoalCategories : LayoutsPageBase
    {
        //int listitemid;
        string strMessage = string.Empty;
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

            txtCategory.Visible = true;
            txtDescription.Visible = true;
            chkMandatory.Visible = true;
            btnSave.Visible = true;

            lblCategoryValue.Visible = false;
            lblMandatoryValue.Visible = false;
            lblDescriptionValue.Visible = false;

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    txtCategory.Text = Convert.ToString(lstItem["ctgrCategory"]);
                    chkMandatory.Checked = Convert.ToBoolean(lstItem["ctgrMandatory"]);
                    txtDescription.Text = Convert.ToString(lstItem["ctgrDescription"]);

                    btnSave.Text = "Update";
                }
            }
        }

        private void ViewDetails()
        {
            lbledit.Text = "View";

            btnEdit.Visible = true;
            btnDelete.Visible = true;

            txtCategory.Visible = false;
            txtDescription.Visible = false;
            chkMandatory.Visible = false;
            btnSave.Visible = false;

            lblCategoryValue.Visible = true;
            lblMandatoryValue.Visible = true;
            lblDescriptionValue.Visible = true;

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    lblCategoryValue.Text = Convert.ToString(lstItem["ctgrCategory"]);
                    lblMandatoryValue.Text = Convert.ToString(lstItem["ctgrMandatory"]);
                    lblDescriptionValue.Text = Convert.ToString(lstItem["ctgrDescription"]);
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

                LogHandler.LogError(ex, "Error in PMS Goal Ctegory Master Page");
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

                LogHandler.LogError(ex, "Error in PMS Goal Ctegory Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteItem(Convert.ToInt32(Request.Params["ID"]));
                strMessage = "Item Deleted Successfully";
                Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                Context.Response.Flush();
                Context.Response.End();
            }
            catch (Exception ex)
            {

                LogHandler.LogError(ex, "Error in PMS Goal Ctegory Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }

        }

        public void DeleteItem(int listitemid)
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
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
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

                        lstItem["ctgrCategory"] = txtCategory.Text.Trim();
                        lstItem["ctgrDescription"] = txtDescription.Text.Trim();
                        lstItem["ctgrMandatory"] = chkMandatory.Checked.ToString();

                        objWeb.AllowUnsafeUpdates = true;
                        lstItem.Update();
                        objWeb.AllowUnsafeUpdates = false;
                    }

                }
            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex, "Error in PMS Initial Goal Setting");
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");

                //Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
        }
    }
}
