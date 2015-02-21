using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS_Masterspages.Layouts.VFS_Masterspages
{
    public partial class Competency_Description : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                BindGrid();
                BindCompetencies();
                BindEmployeeGroups();
                BindEmployeeSubgroups(string.Empty);
            }

        }
        void BindGrid()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb currentWeb = osite.OpenWeb())
                    {
                        SPList list = currentWeb.Lists["Competency Descriptions"];
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='cmptdStatus' /><Value Type='Boolean'>1</Value></Eq></Where>";
                        SPListItemCollection itemcollection = list.GetItems(q);
                        DataTable dt = new DataTable();
                        dt = itemcollection.GetDataTable();
                        goalcatogeryGridview.DataSource = dt;
                        goalcatogeryGridview.DataBind();
                    }
                }
            });
        }
        private void BindCompetencies()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCompetency = objWeb.Lists["Competencies"];
                    SPQuery query = new SPQuery();
                    query.Query = "<Where><Eq><FieldRef Name='cmptStatus' /><Value Type='Boolean'>1</Value></Eq></Where>";
                    SPListItemCollection lstItemColl = lstCompetency.GetItems(query);

                    DataTable dtCompetencies = lstItemColl.GetDataTable();

                    ddlCompetency.DataSource = dtCompetencies;
                    ddlCompetency.DataValueField = "ID";
                    ddlCompetency.DataTextField = "cmptCompetency1";
                    ddlCompetency.DataBind();
                    ddlCompetency.Items.Insert(0, "Choose Competency");
                }
            }
        }

        private void BindEmployeeGroups()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstEmpGroups = objWeb.Lists["Employee Groups"];
                    SPListItemCollection lstItemColl = lstEmpGroups.GetItems();

                    DataTable dtCompetencies = lstItemColl.GetDataTable();

                    ddlEmployeeGroup.DataSource = dtCompetencies;
                    ddlEmployeeGroup.DataValueField = "EmployeeGroupCode";
                    ddlEmployeeGroup.DataTextField = "EmployeeGroupText";
                    ddlEmployeeGroup.DataBind();
                    ddlEmployeeGroup.Items.Insert(0, "Choose Employee Group");
                }
            }
        }

        private void BindEmployeeSubgroups(string EmpGroup)
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstEmpSubGroup = objWeb.Lists["Employee Sub Groups"];
                    SPListItemCollection lstItemColl;

                    if (EmpGroup != string.Empty)
                    {
                        SPQuery q = new SPQuery();
                        q.Query = "<Where><Eq><FieldRef Name='EmployeeGroupCode'  /><Value Type='Lookup'>" + EmpGroup + "</Value></Eq></Where>";
                        lstItemColl = lstEmpSubGroup.GetItems(q);
                    }
                    else
                    {
                        lstItemColl = lstEmpSubGroup.GetItems();
                    }

                    DataTable dtCompetencies = lstItemColl.GetDataTable();

                    ddlEmployeeSubGroup.DataSource = dtCompetencies;
                    ddlEmployeeSubGroup.DataValueField = "EmployeeSubGroupCode";
                    ddlEmployeeSubGroup.DataTextField = "EmployeeSubGroupCode";

                    ddlEmployeeSubGroup.DataBind();
                    ddlEmployeeSubGroup.Items.Insert(0, "Choose Emp Sub Group");

                    //btnSave.Text = "Save";
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
                            SPList lstcompetencydescription = oweb.Lists["Competency Descriptions"];
                            SPQuery query = new SPQuery();
                            query.Query = "<Where><And><Eq><FieldRef Name='cmptCompetency' /><Value Type='Text'>" + ddlCompetency.SelectedItem.Text.Trim() + "</Value></Eq><Eq><FieldRef Name='cmptEmpSubGroup' /><Value Type='Text'>" + ddlEmployeeSubGroup.SelectedItem.Text.Trim() + "</Value></Eq></And></Where>";
                            SPListItemCollection Itemcollection = lstcompetencydescription.GetItems(query);
                            //bool flag = false;

                            SPListItem lstItem;
                            if (btnSubmit.Text == "Submit")
                            {
                                if (Itemcollection.Count == 0)
                                {
                                    lstItem = lstcompetencydescription.AddItem();

                                    lstItem["cmptEmpGroup"] = ddlEmployeeGroup.SelectedItem.Text.Trim();
                                    lstItem["cmptEmpSubGroup"] = ddlEmployeeSubGroup.SelectedItem.Text.Trim();
                                    lstItem["cmptCompetency"] = ddlCompetency.SelectedItem.Text.ToString();
                                    lstItem["cmptDescription"] = txtDescription.Text.ToString();
                                    lstItem["Status"] = true;
                                    oweb.AllowUnsafeUpdates = true;
                                    lstItem.Update();
                                    BindGrid();
                                    oweb.AllowUnsafeUpdates = false;

                                    string strMessage = "Competency description saved successfully";
                                    string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency%20Description.aspx";
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");
                                }
                                else
                                {
                                    string error = ddlCompetency.SelectedItem.Text.Trim()+" for the sub group "+ddlEmployeeSubGroup.SelectedItem.Text.Trim()+ " allready exists";
                                    string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency%20Description.aspx";
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + error + "'); </script>");
                                }
                                

                            }
                            else
                            {

                                int Id = Convert.ToInt32(ViewState["Id"]);
                                lstItem = lstcompetencydescription.Items.GetItemById(Id);
                                if (lstItem["cmptEmpSubGroup"].ToString().Equals(ddlEmployeeSubGroup.SelectedItem.Text.ToString().Trim()))
                                {
                                    lstItem["cmptEmpGroup"] = ddlEmployeeGroup.SelectedItem.Text.Trim();
                                    lstItem["cmptEmpSubGroup"] = ddlEmployeeSubGroup.SelectedItem.Text.Trim();
                                    lstItem["cmptCompetency"] = ddlCompetency.SelectedItem.Text.ToString();
                                    lstItem["cmptDescription"] = txtDescription.Text.ToString();
                                    oweb.AllowUnsafeUpdates = true;
                                    lstItem.Update();
                                    BindGrid();
                                    oweb.AllowUnsafeUpdates = false;

                                    string strMessage = "Competency description updated successfully";
                                    string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency%20Description.aspx";
                                    Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");
                                }
                                else
                                {
                                    if (Itemcollection.Count == 0)
                                    {
                                        lstItem["cmptEmpGroup"] = ddlEmployeeGroup.SelectedItem.Text.Trim();
                                        lstItem["cmptEmpSubGroup"] = ddlEmployeeSubGroup.SelectedItem.Text.Trim();
                                        lstItem["cmptCompetency"] = ddlCompetency.SelectedItem.Text.ToString();
                                        lstItem["cmptDescription"] = txtDescription.Text.ToString();
                                        oweb.AllowUnsafeUpdates = true;
                                        lstItem.Update();
                                        BindGrid();
                                        oweb.AllowUnsafeUpdates = false;

                                        string strMessage = "Competency description updated successfully";
                                        string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency%20Description.aspx";
                                        Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");
                                    }
                                    else
                                    {
                                        string error = ddlCompetency.SelectedItem.Text.Trim() + " for the sub group " + ddlEmployeeSubGroup.SelectedItem.Text.Trim() + "  allready exists";
                                        string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency%20Description.aspx";
                                        Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + error + "'); </script>");
                                    }
                                }
                                // Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                                //Context.Response.Flush();
                                //Context.Response.End();
                            }
                        }
                    }

                });

            }
            catch (Exception ex)
            {


            }

        }

        protected void goalcatogeryGridview_OnRowCommand(object sender, CommandEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oweb = osite.OpenWeb())
                    {
                        int id = Convert.ToInt32(e.CommandArgument.ToString());
                        SPList list = oweb.Lists["Competency Descriptions"];
                        
                        if (e.CommandName == "CmdEdit")
                        {
                            SPListItem lstItem = list.Items.GetItemById(id);
                            CompetencyDescription_Form.Visible = true;
                            ddlEmployeeGroup.ClearSelection();
                            ddlEmployeeGroup.Items.FindByText(lstItem["cmptEmpGroup"].ToString()).Selected = true;
                            ddlEmployeeSubGroup.ClearSelection();
                            ddlEmployeeSubGroup.Items.FindByText(lstItem["cmptEmpSubGroup"].ToString()).Selected = true;
                            ddlCompetency.ClearSelection();
                            ddlCompetency.Items.FindByText(lstItem["cmptCompetency"].ToString()).Selected = true;
                            txtDescription.Text = lstItem["cmptDescription"].ToString();

                            btnSubmit.Text = "Update";
                            ViewState["Id"] = id.ToString();
                        }
                        else if (e.CommandName == "CmdDelete")
                        {
                            SPListItem lstItem = list.Items.GetItemById(id);
                            lstItem["Status"] = false;
                            oweb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            oweb.AllowUnsafeUpdates = false;

                            string strMessage = "Deleted Successfully";
                            string url = SPContext.Current.Web.Url + "/_Layouts/VFS_Masterspages/Competency%20Description.aspx";
                            Context.Response.Write("<script type='text/javascript'>window.open('" + url + "','_self');alert('" + strMessage + "'); </script>");

                        }
                    }
                }
            });
        }

        protected void ddlEmployeeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployeeGroup.SelectedIndex > 0)
            {
                BindEmployeeSubgroups(ddlEmployeeGroup.SelectedValue.ToString());
            }
            else
            {
                BindEmployeeSubgroups(ddlEmployeeGroup.SelectedValue.ToString());
            }
        }

        protected void btnADD_Click(object sender, EventArgs e)
        {
            CompetencyDescription_Form.Visible = true;
            ddlCompetency.ClearSelection();
            ddlEmployeeGroup.ClearSelection();
            ddlEmployeeSubGroup.ClearSelection();
            txtDescription.Text = string.Empty;
            btnSubmit.Text = "Submit";
            #region MyRegion
            //try
            //{
            //    string listGUID = string.Empty;
            //    using (SPWeb web = SPContext.Current.Site.OpenWeb())
            //    {
            //        SPSecurity.RunWithElevatedPrivileges(delegate()
            //        {
            //            SPList cabResponses = web.Lists["Competency Descriptions"];
            //            listGUID = cabResponses.ID.ToString();
            //        });
            //    }
            //    //Page.Response.Redirect(SPContext.Current.Web.Url + "/_layouts/15/BIALApplicationPages/NewCABRequest.aspx?List=b941a25f-b99c-480d-958c-0098046032c1&ID=" + itemId);

            //    string message = SPContext.Current.Site.Url + "/_layouts/VFS_Masterspages/Competency.aspx?List=" + listGUID + "&IsDlg=1";
            //    string radalertscript = "<script language='javascript'>function f(){ OpenDialog('" + message + "'); Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";

            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", radalertscript);
            //}
            //catch (Exception)
            //{

            //} 
            #endregion
        }
        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            CompetencyDescription_Form.Visible = false;
        }
        protected void goalcatogeryGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            goalcatogeryGridview.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}
