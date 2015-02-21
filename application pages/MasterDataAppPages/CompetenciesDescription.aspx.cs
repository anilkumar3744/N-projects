using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;

namespace VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages
{
    public partial class CompetenciesDescription : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                BindEmployeeGroups();
                BindCompetencies();
                BindEmployeeSubgroups(string.Empty);

                if (this.Request.Params["ID"] != null)// IS ID HAVING
                {
                    if (Request.Params["Source"] != null)// IS ID MATCH WITH VIEW   
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
                else
                {
                    // new

                }
            }
        }

        //private void BindEmployeeSubgroups(string? empGroup)
        //{
        //    using (SPWeb objWeb = SPContext.Current.Site.OpenWeb())
        //    {
        //        SPList lstEmpSubGroup = objWeb.Lists["Employee Sub Group"];
        //        SPListItemCollection lstItemColl = lstEmpSubGroup.GetItems();

        //        DataTable dtCompetencies = lstItemColl.GetDataTable();

        //        ddlEmpSubGroup.DataSource = dtCompetencies;
        //        ddlEmpSubGroup.DataValueField = "ID";
        //        ddlEmpSubGroup.DataTextField = "Emp_x0020_Group_x0020_Code";
        //        ddlEmpSubGroup.DataBind();
        //        ddlEmpSubGroup.Items.Insert(0, "Choose Emp Sub Group");

        //        btnSave.Text = "Save";
        //    }
        //}

        private void BindCompetencies()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCompetency = objWeb.Lists["Competencies"];
                    SPListItemCollection lstItemColl = lstCompetency.GetItems();

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

                    ddlEmpSubGroup.DataSource = dtCompetencies;
                    ddlEmpSubGroup.DataValueField = "EmployeeSubGroupCode";
                    ddlEmpSubGroup.DataTextField = "EmployeeSubGroupCode";

                    ddlEmpSubGroup.DataBind();
                    ddlEmpSubGroup.Items.Insert(0, "Choose Emp Sub Group");

                    //btnSave.Text = "Save";
                }
            }
        }

        private void UpdateDetails()
        {

            //BindEmployeeSubgroups();
            lbledit.Text = "Edit";

            btnEdit.Visible = false;
            btnDelete.Visible = false;

            txtDescription.Visible = true;
            ddlCompetency.Visible = true;
            ddlEmpSubGroup.Visible = true;
            btnSave.Visible = true;
            ddlEmployeeGroup.Visible = true;
            lblEmpGroup.Visible = false;

            lblDescriptionValue.Visible = false;
            lblCompetencyValue.Visible = false;
            lblEmpSubGroupValue.Visible = false;
            lblEmpGroup.Visible = false;

            BindEmployeeGroups();
            BindCompetencies();
            BindEmployeeSubgroups(string.Empty);

            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    txtDescription.Text = Convert.ToString(lstItem["cmptDescription"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(lstItem["cmptCompetency"])))
                        ddlCompetency.Items.FindByText(Convert.ToString(lstItem["cmptCompetency"])).Selected = true;
                    if (!string.IsNullOrEmpty(Convert.ToString(lstItem["cmptEmpGroup"])))
                    {
                        ddlEmployeeGroup.Items.FindByText(Convert.ToString(lstItem["cmptEmpGroup"])).Selected = true;
                        ddlEmpSubGroup.Items.FindByText(Convert.ToString(lstItem["cmptEmpSubGroup"])).Selected = true;

                    }

                    btnSave.Text = "Update";
                }
            }
        }

        private void ViewDetails()
        {
            lbledit.Text = "View";

            btnEdit.Visible = true;
            btnDelete.Visible = true;

            txtDescription.Visible = false;
            ddlCompetency.Visible = false;
            ddlEmpSubGroup.Visible = false;
            btnSave.Visible = false;
            ddlEmployeeGroup.Visible = false;

            lblEmpGroup.Visible = true;
            lblDescriptionValue.Visible = true;
            lblCompetencyValue.Visible = true;
            lblEmpSubGroupValue.Visible = true;


            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstCategory = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItem lstItem = lstCategory.GetItemById(Convert.ToInt32(Request.Params["ID"]));

                    lblDescriptionValue.Text = Convert.ToString(lstItem["cmptDescription"]);
                    lblCompetencyValue.Text = Convert.ToString(lstItem["cmptCompetency"]);
                    lblEmpSubGroupValue.Text = Convert.ToString(lstItem["cmptEmpSubGroup"]);
                    lblEmpGroup.Text = Convert.ToString(lstItem["cmptEmpGroup"]);

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

                LogHandler.LogError(ex, "Error in PMS CompetencyDescription Master Page");
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
                LogHandler.LogError(ex, "Error in PMS CompetencyDescription Master Page");
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
                
                        LogHandler.LogError(ex, "Error in PMS CompetencyDescription Master Page");
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
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('" + ex.Message + " .')</script>");
            }
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
                        lstItem["cmptCompetency"] = ddlCompetency.SelectedItem.Text;
                        lstItem["cmptEmpSubGroup"] = ddlEmpSubGroup.SelectedItem.Text;
                        lstItem["cmptDescription"] = txtDescription.Text.Trim();
                        lstItem["cmptEmpGroup"] = ddlEmployeeGroup.SelectedItem.Text;
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
