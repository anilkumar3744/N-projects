using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS_Masterspages.Layouts.VFS_Masterspages
{
    public partial class Goal_Catagory : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        void BindGrid()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb currentWeb = osite.OpenWeb())
                {
                    SPList list = currentWeb.Lists["Goal Categories"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='ctgrStatus' /><Value Type='Boolean'>1</Value></Eq></Where>";
                    SPListItemCollection itemcollection = list.GetItems(q);
                    DataTable dt = new DataTable();
                    dt = itemcollection.GetDataTable();
                    goalcatogeryGridview.DataSource = dt;
                    goalcatogeryGridview.DataBind();
                }
            }
        }

        protected void goalcatogeryGridview_RowCommand(object sender, CommandEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        SPList list = currentWeb.Lists["Goal Categories"];
                       
                        if (e.CommandName == "CmdEdit")
                        {
                            SPListItem listItem = list.Items.GetItemById(id);
                            Goal_Catogery_Form.Visible = true;
                            txtCategory.Text = listItem["ctgrCategory"].ToString();
                            txtDescription.Text = listItem["ctgrDescription"].ToString();
                            chkMandatory.Checked = (listItem["ctgrMandatory"].ToString() == "True");
                            btnSubmit.Text = "Update";
                            ViewState["Id"] = id.ToString();
                        }
                        else if (e.CommandName == "CmdDelete")
                        {
                            SPListItem listItem = list.Items.GetItemById(id);
                            listItem["Status"] = false;
                            currentWeb.AllowUnsafeUpdates = true;
                            listItem.Update();
                            currentWeb.AllowUnsafeUpdates = false;
                            string strMessage = "Deleted Successfully";
                            string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Goal_Catagory.aspx";
                            Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");

                        }
                    }
                }
            });
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {

                SPSecurity.RunWithElevatedPrivileges(delegate()
               {
                   using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                   {
                       using (SPWeb oweb = osite.OpenWeb())
                       {
                           SPList lstCategories = oweb.Lists["Goal Categories"];
                           SPQuery query = new SPQuery();
                           query.Query = "<Where><Eq><FieldRef Name='ctgrCategory' /><Value Type='Text'>" + txtCategory.Text.Trim() + "</Value></Eq></Where>";
                           SPListItemCollection Itemcollection = lstCategories.GetItems(query);
                           //bool flag = false;
                           SPListItem lstItem;
                           if (btnSubmit.Text == "Submit")
                           {
                               if (Itemcollection.Count == 0)
                               {
                                   lstItem = lstCategories.AddItem();

                                   lstItem["ctgrCategory"] = txtCategory.Text.Trim();
                                   lstItem["ctgrDescription"] = txtDescription.Text.Trim();
                                   lstItem["ctgrMandatory"] = chkMandatory.Checked.ToString();
                                   lstItem["Status"] = true;
                                   oweb.AllowUnsafeUpdates = true;
                                   lstItem.Update();
                                   BindGrid();
                                   oweb.AllowUnsafeUpdates = false;
                                   txtCategory.Text = string.Empty;
                                   txtDescription.Text = string.Empty;
                                   string strMessage = "Goal category " + txtCategory.Text.Trim()+ " saved successfully";
                                   string url1 = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Goal_Catagory.aspx";
                                   Context.Response.Write("<script type='text/javascript'>window.open('" + url1 + "','_self');alert('" + strMessage + "'); </script>");
                               }
                               else
                               {
                                   string error = txtCategory.Text + " category  allready exists";
                                   string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Goal_Catagory.aspx";
                                   Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + error + "'); </script>");
                               }
                               //Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                               //Context.Response.Flush();
                               //Context.Response.End();

                           }
                           else
                           {
                               int Id = Convert.ToInt32(ViewState["Id"]);
                               lstItem = lstCategories.Items.GetItemById(Id);
                               if (lstItem["ctgrCategory"].ToString().Equals(txtCategory.Text.Trim()))
                               {
                                   lstItem["ctgrCategory"] = txtCategory.Text.Trim();
                                   lstItem["ctgrDescription"] = txtDescription.Text.Trim();
                                   lstItem["ctgrMandatory"] = chkMandatory.Checked.ToString();
                                   oweb.AllowUnsafeUpdates = true;
                                   lstItem.Update();
                                   BindGrid();
                                   oweb.AllowUnsafeUpdates = false;

                                   string strMessage = "Goal category " + txtCategory.Text.Trim()+ " updated successfully";
                                   string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Goal_Catagory.aspx";
                                   Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");
                               }
                               else
                               {
                                   if (Itemcollection.Count == 0)
                                   {
                                       lstItem["ctgrCategory"] = txtCategory.Text.Trim();
                                       lstItem["ctgrDescription"] = txtDescription.Text.Trim();
                                       lstItem["ctgrMandatory"] = chkMandatory.Checked.ToString();
                                       oweb.AllowUnsafeUpdates = true;
                                       lstItem.Update();
                                       BindGrid();
                                       oweb.AllowUnsafeUpdates = false;

                                       string strMessage = "Goal category " + txtCategory.Text.Trim() + " updated successfully"; 
                                       string url1 = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Goal_Catagory.aspx";
                                       Context.Response.Write("<script type='text/javascript'>window.open('" + url1 + "','_self');alert('" + strMessage + "'); </script>");
                                   }
                                   else
                                   {
                                       string error = txtCategory.Text + " category  allready exists";
                                       string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Goal_Catagory.aspx";
                                       Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + error + "'); </script>");
                                   }
                               }
                               //Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                               //Context.Response.Flush();
                               //Context.Response.End();

                           }
                           // goalcatogeryGridview.Rows.Add(txtCategory.Text,);

                       }
                   }
               });
            }
            catch (Exception ex)
            {

                //LogHandler.LogError(ex, "Error in PMS Goal Ctegory Master Page");
                //Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }

        }

        protected void btnADD_Click(object sender, EventArgs e)
        {
            Goal_Catogery_Form.Visible = true;
            btnSubmit.Text = "Submit";
            txtCategory.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Goal_Catogery_Form.Visible = false;
        }

        protected void goalcatogeryGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            goalcatogeryGridview.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
