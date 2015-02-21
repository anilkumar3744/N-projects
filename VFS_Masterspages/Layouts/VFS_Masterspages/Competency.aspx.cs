using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS_Masterspages.Layouts.VFS_Masterspages
{
    public partial class Competency : LayoutsPageBase
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
                    SPList list = currentWeb.Lists["Competencies"];
                    SPQuery q = new SPQuery();
                    q.Query = "<Where><Eq><FieldRef Name='cmptStatus' /><Value Type='Boolean'>1</Value></Eq></Where>";
                    SPListItemCollection itemcollection = list.GetItems(q);
                    DataTable dt = new DataTable();
                    dt = itemcollection.GetDataTable();
                    ComptencyGridview.DataSource = dt;
                    ComptencyGridview.DataBind();
                }
            }
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
                            SPList lstCompetency = oweb.Lists["Competencies"];
                            SPQuery query = new SPQuery();
                            query.Query = "<Where><Eq><FieldRef Name='cmptCompetency1' /><Value Type='Text'>" + txtCompetency.Text.Trim() + "</Value></Eq></Where>"; ;
                            SPListItemCollection Itemcollection = lstCompetency.GetItems(query);

                            SPListItem lstItem;
                            if (btn_Submit.Text == "Submit")
                            {
                                if (Itemcollection.Count == 0)
                                {
                                    lstItem = lstCompetency.AddItem();

                                    lstItem["cmptCompetency1"] = txtCompetency.Text.Trim();

                                    lstItem["Status"] = true;
                                    oweb.AllowUnsafeUpdates = true;
                                    lstItem.Update();
                                    BindGrid();
                                    oweb.AllowUnsafeUpdates = false;
                                    txtCompetency.Text = string.Empty;
                                    string strMessage = "Competency " +txtCompetency.Text.Trim()+" saved successfully";
                                    string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency.aspx";
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");
                                }
                                else
                                {
                                    string error = txtCompetency.Text + "  competency allready exists";
                                    string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency.aspx";
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + error + "'); </script>");
                                }
                                // Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');</script>");
                                //Context.Response.Flush();
                                //Context.Response.End();

                            }
                            else
                            {
                                int Id = Convert.ToInt32(ViewState["Id"]);
                                lstItem = lstCompetency.Items.GetItemById(Id);
                                if (!lstItem["cmptCompetency1"].ToString().Equals(txtCompetency.Text.Trim()))
                                {
                                    string BeforeValue = lstItem["cmptCompetency1"].ToString();
                                    lstItem["cmptCompetency1"] = txtCompetency.Text.Trim();
                                    oweb.AllowUnsafeUpdates = true;
                                    lstItem.Update();
                                    BindGrid();
                                    UpdatecomptDescript(BeforeValue, lstItem["cmptCompetency1"].ToString());
                                    oweb.AllowUnsafeUpdates = false;
                                    txtCompetency.Text = string.Empty;
                                    string strMessage = "Competency " +lstItem["cmptCompetency1"].ToString() +" updated successfully";
                                    string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency.aspx";
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");

                                    // Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');</script>");
                                    //Context.Response.Flush();
                                    //Context.Response.End();
                                }
                                else
                                {
                                    if (Itemcollection.Count == 0)
                                    {
                                        string BeforeValue = lstItem["cmptCompetency1"].ToString();
                                        lstItem["cmptCompetency1"] = txtCompetency.Text.Trim();
                                        oweb.AllowUnsafeUpdates = true;
                                        lstItem.Update();
                                        BindGrid();
                                        UpdatecomptDescript(BeforeValue, lstItem["cmptCompetency1"].ToString());
                                        oweb.AllowUnsafeUpdates = false;
                                        txtCompetency.Text = string.Empty;
                                        string strMessage = "Competency " + lstItem["cmptCompetency1"].ToString() + " updated successfully";
                                        string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency.aspx";
                                        Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");
                                    }
                                    else
                                    {
                                        string error = txtCompetency.Text + "  competency allready exists";
                                        string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency.aspx";
                                        Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + error + "'); </script>");
                                    }
                                }

                            }
                            // goalcatogeryGridview.Rows.Add(txtCategory.Text,);

                        }
                    }
                });
            }
            catch (Exception ex)
            {


            }

        }
        protected void ComptencyGridview_RowCommand(object sender, CommandEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {

                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                   
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        SPList list = currentWeb.Lists["Competencies"];
                        
                        if (e.CommandName == "CmdEdit")
                        {
                            SPListItem listItem = list.Items.GetItemById(id);
                            Competency_Form.Visible = true;
                            txtCompetency.Text = listItem["cmptCompetency1"].ToString();

                            btn_Submit.Text = "Update";
                            ViewState["Id"] = id.ToString();
                        }
                        else if (e.CommandName == "CmdDelete")
                        {
                            SPListItem listItem = list.Items.GetItemById(id);
                            listItem["Status"] = false;
                            currentWeb.AllowUnsafeUpdates = true;
                            listItem.Update();
                            BindGrid();
                            currentWeb.AllowUnsafeUpdates = false;

                            string strMessage = "Deleted Successfully";
                            string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency.aspx";
                            Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");

                        }
                    }
                }
            });
        }
        protected void btnADD_Click(object sender, EventArgs e)
        {
            Competency_Form.Visible = true;
            btn_Submit.Text = "Submit";
            txtCompetency.Text = string.Empty;
            //txtDescription.Text = string.Empty;
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Competency_Form.Visible = false;
        }

        public void UpdatecomptDescript(string Beforevalue, string AfterValue)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
           {

               using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
               {
                   using (SPWeb currentWeb = osite.OpenWeb())
                   {
                       SPList list = currentWeb.Lists["Competency Descriptions"];
                       SPQuery q = new SPQuery();
                       q.Query = "<Where><Eq><FieldRef Name='cmptCompetency' /><Value Type='Text'>" + Beforevalue.Trim() + "</Value></Eq></Where>";
                       SPListItemCollection itemcollection = list.GetItems(q);
                       if (!Beforevalue.Equals(AfterValue))
                       {
                           foreach (SPListItem item in itemcollection)
                           {

                               item["cmptCompetency"] = AfterValue.Trim();
                               currentWeb.AllowUnsafeUpdates = true;
                               item.Update();
                               currentWeb.AllowUnsafeUpdates = false;
                           }
                       }
                   }
               }
           });
        }

        protected void ComptencyGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ComptencyGridview.PageIndex = e.NewPageIndex;
            BindGrid();
        }

    }
}
