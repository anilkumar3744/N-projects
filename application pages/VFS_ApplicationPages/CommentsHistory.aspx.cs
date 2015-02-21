using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using System.Web.UI.WebControls;

namespace VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages
{
    public partial class CommentsHistory : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = GetHistory();
                ViewState["dt"] = dt;
                gvCommentsHistory.DataSource = dt;
                gvCommentsHistory.DataBind(); 
            }
        }

        private DataTable GetHistory()
        {
            DataTable testTable = new DataTable();

            testTable.Columns.Add("SNo", typeof(string));
            testTable.Columns.Add("Reference", typeof(string));
            testTable.Columns.Add("Comments", typeof(string));
            testTable.Columns.Add("By", typeof(string));

            DataRow dr00 = testTable.NewRow();

            dr00["SNo"] = 1;
            dr00["Reference"] = "Goal, FTE should be more than 0.8";
            dr00["Comments"] = "Reviewer comments";
            dr00["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr00);

            DataRow dr01 = testTable.NewRow();

            dr01["SNo"] = 2;
            dr01["Reference"] = "Goal, Number of Customer Complaints should not exceed 2";
            dr01["Comments"] = "Reviewer comments";
            dr01["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr01);

            DataRow dr02 = testTable.NewRow();

            dr02["SNo"] = 3;
            dr02["Reference"] = "Goal, 100% satisfaction of Colleagues ";
            dr02["Comments"] = "Reviewer comments";
            dr02["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr02);

            DataRow dr03 = testTable.NewRow();

            dr03["SNo"] = 4;
            dr03["Reference"] = "Goal, Number of Data Entry Errors should be Zero";
            dr03["Comments"] = "Reviewer comments";
            dr03["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr03);

            DataRow dr04 = testTable.NewRow();

            dr04["SNo"] = 5;
            dr04["Reference"] = "Goal, 100% Attendance of Training Programs";
            dr04["Comments"] = "Reviewer comments";
            dr04["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr04);

            //Comments for Competencies
            
            //DataRow dr1 = testTable.NewRow();

            //dr1["SNo"] = 2;
            //dr1["Reference"] = "Goal, FTE should be more than 0.8";
            //dr1["Comments"] = "Appraiser comments";
            //dr1["By"] = "By Jhon (Appraiser) @ 05-Apr-2013 13:00:00";

            //testTable.Rows.Add(dr1);

            //DataRow dr2 = testTable.NewRow();

            //dr2["SNo"] = 3;
            //dr2["Reference"] = "Goal, FTE should be more than 0.8";
            //dr2["Comments"] = "Appraisee comments";
            //dr2["By"] = "By Chen Tiffeny (Appraisee) @ 05-Feb-2013 13:00:00";

            //testTable.Rows.Add(dr2);

            DataRow dr11 = testTable.NewRow();

            dr11["SNo"] = 6;
            dr11["Reference"] = "Competency, Entrepreneurship";
            dr11["Comments"] = "Reviewer comments";
            dr11["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr11);

            DataRow dr12 = testTable.NewRow();

            dr12["SNo"] = 7;
            dr12["Reference"] = "Competency, Quality & Service Orientation";
            dr12["Comments"] = "Reviewer comments";
            dr12["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr12);


            DataRow dr13 = testTable.NewRow();

            dr13["SNo"] = 8;
            dr13["Reference"] = "Competency, Teamwork & Collaboration";
            dr13["Comments"] = "Reviewer comments";
            dr13["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr13);

            DataRow dr14 = testTable.NewRow();

            dr14["SNo"] = 9;
            dr14["Reference"] = "Competency, Diversity Sensitivity";
            dr14["Comments"] = "Reviewer comments";
            dr14["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr14);

            DataRow dr15 = testTable.NewRow();

            dr15["SNo"] = 10;
            dr15["Reference"] = "Competency, Personal & Professional Development";
            dr15["Comments"] = "Reviewer comments";
            dr15["By"] = "By Chris Martin (Reviewer) @ 05-May-2013 13:00:00";

            testTable.Rows.Add(dr15);

            //DataRow dr4 = testTable.NewRow();

            //dr4["SNo"] = 5;
            //dr4["Reference"] = "Competency, Entrepreneurship";
            //dr4["Comments"] = "Appraiser comments";
            //dr4["By"] = "By Jhon (Appraiser) @ 05-Apr-2013 13:00:00";

            //testTable.Rows.Add(dr4);

            //DataRow dr5 = testTable.NewRow();

            //dr5["SNo"] = 6;
            //dr5["Reference"] = "Competency, Entrepreneurship";
            //dr5["Comments"] = "Appraisee comments";
            //dr5["By"] = "By Chen Tiffeny (Appraisee) @ 05-Feb-2013 13:00:00";

            //testTable.Rows.Add(dr5);

            //hjdfgkfdhkg dfjkghkdjfh gkjdfhgjkhdfkjgh

            DataRow dr20 = testTable.NewRow();

            dr20["SNo"] = 11;
            dr20["Reference"] = "Goal, FTE should be more than 0.8";
            dr20["Comments"] = "Appraiser comments";
            dr20["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr20);

            DataRow dr21 = testTable.NewRow();

            dr21["SNo"] = 12;
            dr21["Reference"] = "Goal, Number of Customer Complaints should not exceed 2";
            dr21["Comments"] = "Appraiser comments";
            dr21["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr21);

            DataRow dr22 = testTable.NewRow();

            dr22["SNo"] = 13;
            dr22["Reference"] = "Goal, 100% satisfaction of Colleagues ";
            dr22["Comments"] = "Appraiser comments";
            dr22["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr22);

            DataRow dr23 = testTable.NewRow();

            dr23["SNo"] = 14;
            dr23["Reference"] = "Goal, Number of Data Entry Errors should be Zero";
            dr23["Comments"] = "Appraiser comments";
            dr23["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr23);

            DataRow dr24 = testTable.NewRow();

            dr24["SNo"] = 15;
            dr24["Reference"] = "Goal, 100% Attendance of Training Programs";
            dr24["Comments"] = "Appraiser comments";
            dr24["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr24);

            DataRow dr30 = testTable.NewRow();

            dr30["SNo"] = 16;
            dr30["Reference"] = "Competency, Entrepreneurship";
            dr30["Comments"] = "Appraiser comments";
            dr30["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr30);

            DataRow dr31 = testTable.NewRow();

            dr31["SNo"] = 17;
            dr31["Reference"] = "Competency, Quality & Service Orientation";
            dr31["Comments"] = "Appraiser comments";
            dr31["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr31);


            DataRow dr32 = testTable.NewRow();

            dr32["SNo"] = 18;
            dr32["Reference"] = "Competency, Teamwork & Collaboration";
            dr32["Comments"] = "Appraiser comments";
            dr32["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr32);

            DataRow dr34 = testTable.NewRow();

            dr34["SNo"] = 19;
            dr34["Reference"] = "Competency, Diversity Sensitivity";
            dr34["Comments"] = "Appraiser comments";
            dr34["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr34);

            DataRow dr35 = testTable.NewRow();

            dr35["SNo"] = 20;
            dr35["Reference"] = "Competency, Personal & Professional Development";
            dr35["Comments"] = "Appraiser comments";
            dr35["By"] = "By Jhon (Appraiser) @ 01-May-2013 13:00:00";

            testTable.Rows.Add(dr35);
            

            return testTable;
        }

        protected void gvCommentsHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCommentsHistory.PageIndex = e.NewPageIndex;

                gvCommentsHistory.DataSource = ViewState["dt"];
                gvCommentsHistory.DataBind();
               
            }
            catch (Exception)
            {
            }
        }
    }
}
