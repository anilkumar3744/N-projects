<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TMTActions.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages.TMTActions"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="/_layouts/Styles/VFSCss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .HeaderLabel
        {
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 20px;
            color: #00358f;
            font-size: 15px;
            font-weight: normal;
            text-decoration: none;
        }
        .Grid1
        {
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 20px;
            color: #005abc;
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
        }
        .tittle_td
        {
            color: #00358F;
            font-size: 13px;
            font-weight: bold;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
        }
        .MLTextbox
        {
            border-bottom: #5bb4fa 1px solid;
            border-left: #5bb4fa 1px solid;
            background-color: #ffffff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            color: #333333;
            font-size: 11px;
            border-top: #5bb4fa 1px solid;
            font-weight: normal;
            border-right: #5bb4fa 1px solid;
        }
        .td_lines
        {
            border: solid 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
  <%--  <div>
        <fieldset>
            <%--<asp:Label ID="lbledit" Text="New" runat="server" Font-Bold="true" CssClass="HeaderLabel"></asp:Label>
         
            <div style="margin-top: 10px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
                <table cellpadding="7px" cellspacing="2px" style="border: 1px solid black; border-collapse: collapse;"
                    class="TableSubHeader">
                    <tr>
                        <th colspan="2">
                            <h3>
                                <asp:Label runat="server" Text="Performance Cycle" ID="lblPerformanceCycle" CssClass="Grid1"></asp:Label>
                                <asp:Label runat="server" Text="2013" ID="lblPerformanceCycleValue" CssClass="Grid1"></asp:Label>
                            </h3>
                        </th>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Goal Setting" ID="lblH1GoalSetting"></asp:Label>
                        </td>
                        <td width="300px">
                            <%--<asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>
                            <asp:Button ID="btnGoalSetting" Text="Start" runat="server" CssClass="Button" />
                            <asp:Label ID="lblGoalSettingH1S" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                            <asp:Label ID="lblGoalSettingH1NS" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Self Evaluation" ID="lblH1SelfEvaluation"></asp:Label>
                        </td>
                        <td width="300px">
                            <%--<asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>
                            <asp:Button ID="btnH1SelfEvaluation" Text="Start" runat="server" CssClass="Button"
                                Visible="false" />
                            <asp:Label ID="lblH1SelfEvaluationH1S" runat="server" Text="Not Started" Font-Bold="true"
                                CssClass="Grid1"></asp:Label>
                            <asp:Label ID="lblH1SelfEvaluationH1NS" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Self Evaluation" ID="Label1"></asp:Label>
                        </td>
                        <td width="300px">
                            <%--<asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>
                            <asp:Button ID="btnH2SelfEvaluation" Text="Start" runat="server" CssClass="Button"
                                Visible="false" />
                            <asp:Label ID="lblH1SelfEvaluationH2S" runat="server" CssClass="Grid1" Text="Not Started"
                                Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblH1SelfEvaluationH2NS" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <%-- <tr>
                                    <td> 
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Label runat="server" Text="History" ID="Label5"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1 Grid1" colspan="2">
                                        <asp:Label runat="server" Text="1. H1 Self-evaluation was started by Chen Tiffany @ 02-March-2013 09:30"
                                            ID="Label2"></asp:Label><br />
                                        <asp:Label runat="server" Text="2. H1 Goal setting was started by Chen Tiffany @ 01-Jan-2013 14:30"
                                            ID="Label3"></asp:Label>
                                    </td>
                                </tr>
                                <%-- </td>
                                </tr> 
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>--%>

     <div>
        <fieldset>
            <%--<asp:Label ID="lbledit" Text="New" runat="server" Font-Bold="true" CssClass="HeaderLabel"></asp:Label>--%>
            <%--<hr />--%>
            <div style="margin-top: 10px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
                <table cellpadding="7px" cellspacing="2px" style="border: 1px solid black; border-collapse: collapse;"
                    class="TableSubHeader">
                    <tr>
                        <th colspan="2">
                            <h3>
                                <asp:Label runat="server" Text="Performance Cycle" ID="lblPerformanceCycle" CssClass="Grid1"></asp:Label>
                                <asp:Label runat="server" Text="2013" ID="lblPerformanceCycleValue" CssClass="Grid1"></asp:Label>
                            </h3>
                        </th>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Goal Setting" ID="lblH1GoalSetting"></asp:Label>
                        </td>
                        <td width="300px">
                            <%--<asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>--%>
                            <asp:Button ID="btnGoalSetting" Text="Start" runat="server" OnClick="BtnGoalSetting_Click" CssClass="Button" />
                            <asp:Label ID="lblGoalSettingH1S" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                            <asp:Label ID="lblGoalSettingH1NS" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Self Evaluation" ID="lblH1SelfEvaluation"></asp:Label>
                        </td>
                        <td width="300px">
                            <%--<asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>--%>
                            <asp:Button ID="btnH1SelfEvaluation" Text="Start" runat="server" CssClass="Button"
                                Visible="false" />
                            <asp:Label ID="lblH1SelfEvaluationH1S" runat="server" Text="Not Started" Font-Bold="true"
                                CssClass="Grid1"></asp:Label>
                            <asp:Label ID="lblH1SelfEvaluationH1NS" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H2 Self Evaluation" ID="Label1"></asp:Label>
                        </td>
                        <td width="300px">
                            <%--<asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>--%>
                            <asp:Button ID="btnH2SelfEvaluation" Text="Start" runat="server" CssClass="Button"
                                Visible="false" />
                            <asp:Label ID="lblH1SelfEvaluationH2S" runat="server" CssClass="Grid1" Text="Not Started"
                                Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblH1SelfEvaluationH2NS" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0">                              
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Label runat="server" Text="History" ID="Label5"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1 Grid1" colspan="2">
                                        <asp:Label runat="server" ID="Label2"></asp:Label><br />
                                        <asp:Label runat="server" ID="Label3"></asp:Label>
                                    </td>
                                </tr>
                                <%-- Text="2. H1 Goal setting was started by Chen Tiffany @ 01-Jan-2013 14:30"--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    TMT Actions
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    TMT Actions
</asp:Content>
