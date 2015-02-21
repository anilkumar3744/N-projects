<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TMTCycles.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.VFS_TMTActions.TMTCycles"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <%--<link href="/_layouts/Styles/VFSCss.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../SiteAssets/VFSCss.css" type="text/css"/>
    <script type="text/javascript" language="javascript">
        function disableButton(btnId) {
            var btn = document.getElementById(btnId);
            btn.enabled = false;
        }
    </script>
   <%-- <style type="text/css">
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
        .Grid
        {
            background-color: #edf7ff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 20px;
            color: #005abc;
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
        }
        
        #ctl00_PlaceHolderMain_gvCycles
        {
            width: 100%;
        }
        #ctl00_PlaceHolderMain_gvCycles_ctl02_lblCycle
        {
            width: 50%;
        }
        
        .CompetencyDescription_Form1
        {
            width: 55%;
            margin-right: auto;
            margin-left: auto;
            padding: 4px;
            border: #005abc 1px solid;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="dvHeader">
        <div style="width: 235px;margin: 0px auto;">
            <br />
            <asp:LinkButton runat="server" ID="lnkStart" OnClick="lnkStart_Click" Font-Bold="true">Manage Performance Cycle</asp:LinkButton><br />
            <br />
            <asp:LinkButton runat="server" ID="lnkManage" OnClick="lnkManage_Click" Font-Bold="true">Manage Performance Cycle Stages</asp:LinkButton><br />
            <br />
            <asp:LinkButton runat="server" ID="lnkLock" OnClick="lnkLock_Click" Font-Bold="true">Lock and Unlock Performance Cycle</asp:LinkButton><br />
            <br />
        </div>
    </div>
    <div id="dvStartPerformanceCycle" visible="false" runat="server">
       
            <div style="margin-top: 10px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
                <table cellpadding="7px" cellspacing="2px" style="border: 1px solid black; border-collapse: collapse;"
                    class="Grid CompetencyDescription_Form1">
                    <tr class="GridHead">
                        <td align="center" width="50%">
                            <h3>
                                <asp:Label runat="server" Text="Performance Cycle" ID="lblPerformanceCycle1" CssClass="Grid1" Font-Bold="true"></asp:Label>
                            </h3>
                        </td>
                        <td align="center" width="50%">
                            <h3>
                                <asp:Label runat="server" ID="lblStatus" Text="Status" CssClass="Grid1" Font-Bold="true"></asp:Label></h3>
                                <%--<th>--%>
                        </td>
                    </tr>
                    <tr id="trCurrentCycle" runat="server">
                        <td class="td1 Grid1" align="center">
                            <asp:Label runat="server" ID="lblPerformanceCycleV"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnCycleStart" Text="Start" runat="server" OnClick="BtnCycleStart_Click"
                                OnClientClick="javascript:disableButton(this.id);" CssClass="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="gvCycles" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true"
                                ShowHeader="False" CssClass="">
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCycle" Text='<%#Eval("tmtPerformanceCycle") %>' Width="50px" CssClass="Grid1"
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumberScale" Text='<%#Convert.ToString(Eval("IsClosed"))=="1"?"Closed":"Started"%>'
                                                CssClass="Grid1" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        
    </div>
    <div id="dvLock" runat="server" visible="false" style="margin-top: 10px; margin-left: 20px;
        margin-right: 20px; margin-bottom: 20px;">
        <table cellpadding="7px" cellspacing="2px" style="border: 1px solid black; border-collapse: collapse;"
            class="Grid CompetencyDescription_Form1">
            <tr class="GridHead">
                <td class="td1 Grid1">
                    <asp:Label runat="server" Text="Performance Cycle" ID="lblCuurentCycleH"></asp:Label>
                </td>
                <td width="300px">
                    <asp:Label runat="server" Text="Status" ID="lblLockedStatus"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td1 Grid1">
                    <asp:Label runat="server" ID="lblCuurentCycle"></asp:Label>
                </td>
                <td width="300px">
                    <asp:Button ID="btnLock" Text="Lock" runat="server" OnClick="BtnLock_Click" CssClass="Button" />
                </td>
            </tr>
            <td colspan="2">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" align="left">
                            <asp:Label runat="server" Text="History" ID="lblLockHistoryHeader" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1" colspan="2">
                            <asp:Label runat="server" ID="lblLockHistoryValue" ></asp:Label><br />
                        </td>
                    </tr>
                </table>
            </td>
    </div>
    <div id="dvManagePerformaceCycle" runat="server" visible="false">
       
            <div style="margin-top: 10px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
                <table cellpadding="7px" cellspacing="2px" style="border: 1px solid black; border-collapse: collapse;"
                    class="Grid CompetencyDescription_Form1">
                    <tr class="GridHead">
                        <th colspan="2">
                            <h3>
                                <asp:Label runat="server" Text="Performance Cycle" ID="lblPerformanceCycle" CssClass="Grid1"></asp:Label>
                                <asp:Label runat="server" ID="lblPerformanceCycleValue" CssClass="Grid1"></asp:Label>
                            </h3>
                        </th>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Goal Setting" ID="lblH1GoalSetting"></asp:Label>
                        </td>
                        <td width="300px">
                            <asp:Button ID="btnGoalSetting" Text="Start" runat="server" OnClientClick="javascript:disableButton(this.id);"
                                OnClick="BtnGoalSetting_Click" CssClass="Button" />
                            <asp:Label ID="lblGoalSettingH1S" runat="server" CssClass="Grid1" Font-Bold="true"
                                Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H1 Self Evaluation" ID="lblH1SelfEvaluation"></asp:Label>
                        </td>
                        <td width="300px">
                            <asp:Button ID="btnH1SelfEvaluation" Text="Start" runat="server" CssClass="Button"
                                OnClick="btn_h1selfevaluation" Visible="false" OnClientClick="javascript:disableButton(this.id);" />
                            <asp:Label ID="lblH1SelfEvaluationH1S" runat="server" Text="Not Started" Font-Bold="true"
                                CssClass="Grid1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="H2 Self Evaluation" ID="Label1"></asp:Label>
                        </td>
                        <td width="300px">
                            <asp:Button ID="btnH2SelfEvaluation" Text="Start" runat="server" CssClass="Button"
                                OnClick="btnH2SelfEvaluation_Click" Visible="false" OnClientClick="javascript:disableButton(this.id);" />
                            <asp:Label ID="lblH1SelfEvaluationH2S" runat="server" CssClass="Grid1" Text="Not Started"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="Close Performance Cycle" ID="lblPcycleClose"></asp:Label>
                        </td>
                        <td width="300px">
                            <asp:Button ID="btnClose" Text="Close" runat="server" CssClass="Button" OnClick="BtnClose_Click"
                                Visible="false" />
                            <asp:Label ID="lblPcycleCloseValue" runat="server" CssClass="Grid1" Text="Not Closed"
                                Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="2" align="left">
                                        <asp:Label runat="server" Text="History" ID="lblHistory" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td1 Grid1" colspan="2">
                                        <asp:Label runat="server" ID="lblHistoryH1Start"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblHistoryH1SelStart"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblHistoryH2SelStart"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblHistoryClosed"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        
    </div>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Manage Performance Cycle
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Manage Performance Cycle
</asp:Content>
