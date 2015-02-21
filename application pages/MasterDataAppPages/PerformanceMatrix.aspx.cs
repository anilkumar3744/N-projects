using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages
{
    public partial class PerformanceMatrix : LayoutsPageBase
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
                            ViewDetails();
                            lbledit.Text = "View";
                            btnSave.Visible = false;
                            btnAdd.Visible = false;
                        }
                        else
                        {
                            // Edit
                            UpdateDetails();
                            lbledit.Text = "Edit";
                        }
                    }
                    else
                    {
                        // edit
                        UpdateDetails();
                        lbledit.Text = "Edit";
                    }
                }
                else
                {
                    //DataTable dt = GetDataTable();
                    //gvPerformanceMatrix.DataSource = dt;
                    //gvPerformanceMatrix.DataBind();

                    UpdateDetails();
                    lbledit.Text = "Edit";
                }
            }
        }

        private void UpdateDetails()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstPerformanceMatrix = objWeb.Lists[new Guid(Request.Params["List"])];//[new Guid(Request.Params["List"])];
                    SPListItemCollection lstItemColl = lstPerformanceMatrix.GetItems();// lstPerformanceMatrix.GetItems();
                    if (lstItemColl.Count > 0)
                    {
                        DataTable dt = lstItemColl.GetDataTable();

                        ViewState["Perfomance"] = dt;
                        gvPerformanceMatrix.DataSource = dt;
                        gvPerformanceMatrix.DataBind();
                    }
                    else
                    {
                        DataTable dt = GetDataTable();
                        ViewState["Perfomance"] = dt;
                        gvPerformanceMatrix.DataSource = dt;
                        gvPerformanceMatrix.DataBind();
                    }
                }
            }
        }

        private void ViewDetails()
        {
            using (SPSite osite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objWeb = osite.OpenWeb())
                {
                    SPList lstPerformanceMatrix = objWeb.Lists[new Guid(Request.Params["List"])];
                    SPListItemCollection lstItemColl = lstPerformanceMatrix.GetItems();

                    DataTable dt = lstItemColl.GetDataTable();
                    gvPerformanceMatrixView.DataSource = dt;
                    gvPerformanceMatrixView.DataBind();
                }
            }
        }

        protected void txtMaximum_Maximum(object sender, EventArgs e)
        {
            TextBox txtaximumAch = sender as TextBox;

            GridViewRow gr = txtaximumAch.NamingContainer as GridViewRow;
            int initial = 0;

            if ((gvPerformanceMatrix.Rows[gr.RowIndex].FindControl("lblInitialAchievement") as Label).Text != string.Empty)
            {
                initial = Convert.ToInt32((gvPerformanceMatrix.Rows[gr.RowIndex].FindControl("lblInitialAchievement") as Label).Text);
            }

            if (initial < Convert.ToInt32((gvPerformanceMatrix.Rows[gr.RowIndex].FindControl("txtMaximumAchievement") as TextBox).Text))
            {
                DataTable dt = ViewState["Perfomance"] as DataTable;

                DataRow dr = dt.Rows[gr.RowIndex];
                if (initial == 0)
                    dr["pmInitialAchievement"] = DBNull.Value;//Convert.ToInt32((gvPerformanceMatrix.Rows[gr.RowIndex].FindControl("lblInitialAchievement") as Label).Text);
                else
                    dr["pmInitialAchievement"] = initial;
                dr["pmMaximumAchievement"] = Convert.ToInt32((gvPerformanceMatrix.Rows[gr.RowIndex].FindControl("txtMaximumAchievement") as TextBox).Text);

                if (dt.Rows.Count - 1 > gr.RowIndex)
                {
                    DataRow dr2 = dt.Rows[gr.RowIndex + 1];

                    TextBox txtMax = gvPerformanceMatrix.Rows[gr.RowIndex].FindControl("txtMaximumAchievement") as TextBox;
                    dr2["pmInitialAchievement"] = Convert.ToInt32(txtMax.Text) + 1;

                    //if (Convert.ToInt32(dr2["pmInitialAchievement"])>Convert.ToInt32(dr2["pmMaximumAchievement"]))
                    //{
                    dr2["pmMaximumAchievement"] = DBNull.Value;
                    //}


                    ViewState["Perfomance"] = null;
                    ViewState["Perfomance"] = dt;
                }

                gvPerformanceMatrix.DataSource = dt;
                gvPerformanceMatrix.DataBind();
                txtaximumAch.Focus();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(SPAlert), "alert", "<script language=\"javascript\">alert('Incorrect data entered')</script>");
                txtaximumAch.Text = string.Empty;
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    string strMessage = string.Empty;
                    //if (Request.Params["ID"] != null)
                    //{
                    //    SaveItem(false, Convert.ToInt32(Request.Params["ID"]));
                    //    strMessage = "Performance Matrix Updated Successfully";
                    //}
                    //else
                    //{
                    SaveItem(true, 0);
                    strMessage = "Performance Matrix Saved Successfully";
                    //}

                    Context.Response.Write("<script type='text/javascript'>alert('" + strMessage + "');window.frameElement.commitPopup();</script>");
                    Context.Response.Flush();
                    Context.Response.End();
                });
            }
            catch (Exception ex)
            {

                LogHandler.LogError(ex, "Error in PMS Performance Matrix Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>");
            Context.Response.Flush();
            Context.Response.End();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int goulsCount = gvPerformanceMatrixView.Rows.Count;

                DataTable dt = ViewState["Perfomance"] as DataTable;

                DataRow dr1 = dt.NewRow();

                dr1["pmScaleNumber"] = dt.Rows.Count + 1;
                dr1["pmInitialAchievement"] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["pmMaximumAchievement"]) + 1;

                dt.Rows.Add(dr1);

                gvPerformanceMatrix.DataSource = dt;
                gvPerformanceMatrix.DataBind();

                ViewState["Perfomance"] = null;
                ViewState["Perfomance"] = dt;
            }
            catch (Exception ex)
            {


                LogHandler.LogError(ex, "Error in PMS Performance Matrix Master Page");
                Context.Response.Write("<script type='text/javascript'> " + CommonMaster.serializeMessage(ex.Message) + ";</script>");
            }

        }

        private DataTable GetDataTable()
        {
            DataTable testTable = new DataTable();
            testTable.Columns.Add("ID", typeof(int));
            testTable.Columns.Add("pmScaleNumber", typeof(int));
            testTable.Columns.Add("pmInitialAchievement", typeof(int));
            testTable.Columns.Add("pmMaximumAchievement", typeof(int));

            for (int i = 0; i < 2; i++)
            {
                DataRow dr = testTable.NewRow();
                dr["pmScaleNumber"] = i + 1;
                if (i == 0)
                {
                    dr["pmInitialAchievement"] = DBNull.Value;
                }
                testTable.Rows.Add(dr);
            }

            ViewState["Perfomance"] = testTable;
            return testTable;
        }

        protected void gvPerformanceMatrix_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;

                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Number Scale";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell1.RowSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell1);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "%Achivement";
                HeaderCell2.ColumnSpan = 2;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell2);

                gvPerformanceMatrix.Controls[0].Controls.AddAt(0, HeaderGridRow);


            }
        }

        protected void gvPerformanceMatrixView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;

                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Number Scale";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                //HeaderCell1.RowSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell1);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "%Achivement";
                HeaderCell2.ColumnSpan = 2;
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell2);

                gvPerformanceMatrixView.Controls[0].Controls.AddAt(0, HeaderGridRow);
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
                        SPList lstPerformanceMatrix = objWeb.Lists[new Guid(Request.Params["List"])];
                        SPListItem lstItem;

                        //if (NewItem)
                        //{
                        foreach (GridViewRow gvRow in gvPerformanceMatrix.Rows)
                        {

                            if (string.IsNullOrEmpty((gvRow.FindControl("lblID") as Label).Text))
                            {
                                lstItem = lstPerformanceMatrix.AddItem();
                            }
                            else
                            {
                                lstItem = lstPerformanceMatrix.GetItemById(Convert.ToInt32((gvRow.FindControl("lblID") as Label).Text));
                            }
                            int max = 0;
                            if ((gvRow.FindControl("txtMaximumAchievement") as TextBox).Text != string.Empty)
                            {
                                max = Convert.ToInt32((gvRow.FindControl("txtMaximumAchievement") as TextBox).Text);
                            }

                            lstItem["pmScaleNumber"] = Convert.ToInt32((gvRow.FindControl("lblNumberScale") as Label).Text);

                            if (gvRow.DataItemIndex > 0) //FindControl("lblInitialAchievement") as Label).Text != string.Empty)
                                lstItem["pmInitialAchievement"] = Convert.ToInt32((gvRow.FindControl("lblInitialAchievement") as Label).Text);

                            else
                                lstItem["pmInitialAchievement"] = DBNull.Value;  //Convert.ToInt32((gvRow.FindControl("lblInitialAchievement") as Label).Text);

                            if (max > 0)
                                lstItem["pmMaximumAchievement"] = max;//Convert.ToInt32((gvRow.FindControl("txtMaximumAchievement") as TextBox).Text);

                            else
                                lstItem["pmMaximumAchievement"] = DBNull.Value;

                            lstItem["pmAchievement"] = (gvRow.FindControl("lblInitialAchievement") as Label).Text + " - " + (gvRow.FindControl("txtMaximumAchievement") as TextBox).Text;

                            objWeb.AllowUnsafeUpdates = true;
                            lstItem.Update();
                            objWeb.AllowUnsafeUpdates = false;
                        }
                        //}
                        //else
                        //{
                        //    int i = 0;
                        //    foreach (GridViewRow gvRow in gvPerformanceMatrix.Rows)
                        //    {
                        //        int max = 0;
                        //        if ((gvRow.FindControl("txtMaximumAchievement") as TextBox).Text != string.Empty)
                        //        {
                        //            max = Convert.ToInt32((gvRow.FindControl("txtMaximumAchievement") as TextBox).Text);
                        //        }

                        //        lstItem = lstPerformanceMatrix.Items[i];
                        //        lstItem["pmScaleNumber"] = Convert.ToInt32((gvRow.FindControl("lblNumberScale") as Label).Text);
                        //        lstItem["pmInitialAchievement"] = Convert.ToInt32((gvRow.FindControl("lblInitialAchievement") as Label).Text);
                        //        if (max > 0)
                        //            lstItem["pmMaximumAchievement"] = max;//Convert.ToInt32((gvRow.FindControl("txtMaximumAchievement") as TextBox).Text);

                        //        else
                        //            lstItem["pmMaximumAchievement"] = DBNull.Value;
                        //        lstItem["pmAchievement"] = (gvRow.FindControl("lblInitialAchievement") as Label).Text + " - " + (gvRow.FindControl("txtMaximumAchievement") as TextBox).Text;

                        //        objWeb.AllowUnsafeUpdates = true;
                        //        lstItem.Update();
                        //        objWeb.AllowUnsafeUpdates = false;
                        //        i++;
                        //    }
                        //}
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
