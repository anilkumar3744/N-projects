<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="True" CodeBehind="Dashboard.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.VFS_DashBoards.Dashboard" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <%-- <link href="/_layouts/STYLES/VFSCss.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../SiteAssets/VFSCss.css" type="text/css" />
    <SharePoint:ScriptLink Name="sp.js" LoadAfterUI="true" OnDemand="false" Localizable="false"
        runat="server" ID="ScriptLink123" />
    <script type="text/javascript">

        $(document).ready(function () {

            //ExecuteOrDelayUntilScriptLoaded(showWaitScreen, "sp.js"); 
            $(document).on("click", ".customTabs", function () { showWaitScreen(); });


            $(".ajax__tab_tab").each(function () {

                $(this).find(':first-child').addClass('width1');
                // $(this).find(':first-child').css('width', '100% !important');
            });

            //$(".ajax__tab_tab").next("span").css("width", "100% !important");


        });

   
    </script>
    <script type="text/javascript">

        function showWaitScreen() {

            //var nid = SP.UI.Notify.addNotification("<img src='/_layouts/images/loadingcirclests16.gif' style='vertical-align: top;'/> Operation in progress...', true");
            //SP.UI.ModalDialog.showWaitScreenWithNoClose('Processing', 'Your request is being processed. Please wait while this process completes.');
            window.parent.eval("window.waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose('Please Wait', 'Please wait as this may take a few seconds...', 76, 330);");
            //closeDialog();
        }

        function closeDialog() {

            if (window.parent.waitDialog != null) {
                setTimeout(function () {
                    window.parent.waitDialog.close();

                }, 0)
                //window.parent.waitDialog.get_html().getElementsByTagName('TD')[1].innerHTML = 'hi';

            }

        }
   
    </script>
    <%-- <script type="text/javascript" language="javascript">
       function StartProgressBar() {
           var myExtender = $find('ProgressBarModalPopupExtender');
           myExtender.show();
           return true;
       }
</script>--%>
    <%--<script language="javascript" type="text/javascript">
    function showWaitScreen() {
        //alert("hi");
        window.parent.eval("window.waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose('Please Wait', 'Please wait as this may take a few seconds...', 76, 330);");
    }
 </script>--%>
    <%--<style type="text/css">
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
        .tittle_td
        {
            color: #00358F;
            font-size: 13px;
            font-weight: bold;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
        }
        .final_anual_score
        {
            background-color: #00358f;
            color: White;
        }
        .verticalalign
        {
            vertical-align: -42px;
        }
        .td_lines
        {
            border: solid 1px;
        }
        .td_bottom
        {
            border-bottom-color: transparent;
        }
        .th_color
        {
            background-color: #93cefc;
        }
        
        .td_up
        {
            border-top-color: transparent;
        }
        .comments
        {
            float: left;
            padding-top: 12px;
        }
        .datecss
        {
            width: 60px;
            border-bottom: #5bb4fa 1px solid;
            border-left: #5bb4fa 1px solid;
            background-color: #ffffff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 14px;
            color: #333333;
            font-size: 11px;
            border-top: #5bb4fa 1px solid;
            font-weight: normal;
            border-right: #5bb4fa 1px solid;
        }
        .tablewidth
        {
            width: 100%;
        }
        .addbtn
        {
            width: 3px;
            height: 1px;
        }
        .TableHeader
        {
            padding-left: 10px;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            background: url(../images/pixel-th.gif) #ffffff repeat-x;
            height: 20px;
            color: #ffffff;
            font-size: 13px;
            font-weight: bold;
        }
        .TableSubHeader
        {
            padding-left: 10px;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            background: url(../images/pixel-subth.gif) #ffffff repeat-x;
            height: 25px;
            color: #00358f;
            font-size: 13px;
            font-weight: bold;
        }
        .Tableborder
        {
            background-color: #b2ddff;
        }
        .FieldTitle
        {
            text-align: left;
            padding-left: 10px;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            background: url(../Images/pixel-field.gif) #ffffff repeat-x;
            height: 23px;
            color: #00358f;
            font-size: 11px;
            font-weight: bold;
            text-decoration: none;
            border: #93cefc 1px solid;
        }
        .FieldCell
        {
            text-align: left;
            background-color: #ffffff;
            padding-left: 10px;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 23px;
            color: #00358f;
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
            border: #93cefc 1px solid;
        }
        .FieldTitle1
        {
            border-right-color: transparent;
            border-bottom-color: transparent;
        }
        
        .FieldTitle2
        {
            border-bottom-color: #93cefc 1px solid !important;
            border-bottom-color: #93cefc 1px solid !important;
            border-right-color: #93cefc 1px solid !important;
        }
        
        .FieldCell1
        {
            border-bottom-color: transparent;
            border-right-color: transparent;
        }
        .FieldCell2
        {
            border-bottom-color: #93cefc 1px solid !important;
            border-bottom-color: #93cefc 1px solid !important;
            border-right-color: #93cefc 1px solid !important;
        }
        .FieldCell3
        {
            border-bottom-color: transparent;
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
        
        .last_td
        {
            border-right: transparent;
        }
        
        .ajax__tab_xp .ajax__tab_active .ajax__tab_tab
        {
            background: url(../images/pixel-th.gif) #ffffff repeat-x;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="updatePnl" runat="server">
        <%-- <Triggers>
            <asp:PostBackTrigger ControlID="lnkAppraisalStatus" />
            <asp:PostBackTrigger ControlID="lnkPIPPhase" />
            <asp:PostBackTrigger ControlID="lnkAppraisalStatus1" />
            <asp:PostBackTrigger ControlID="lnkAppraiserPIPPhase" />
            <asp:PostBackTrigger ControlID="lnkAppraisalStatus2" />
            <asp:PostBackTrigger ControlID="lnkAppraisalStatus3" />
            <asp:PostBackTrigger ControlID="lnkTMTAppraisalStatus" />
            <asp:PostBackTrigger ControlID="lnkRHRAppraisalStatus" />
            <asp:PostBackTrigger ControlID="lnkRBAppraisalStatus" />
        </Triggers>--%>
        <ContentTemplate>
            <div style="width: 100%; font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
                height: 100px; color: #00358f; font-size: 13px; font-weight: bold;">
                <asp:HiddenField ID="hfEmpId" runat="server" />
                <asp:Label ID="lblLocked" runat="server" />
                <asp:TabContainer ID="tcDashBoard" runat="server" EnableViewState="true" ViewStateMode="Enabled"
                    AutoPostBack="true" OnActiveTabChanged="tcDashBoard_ActiveTabChanged">
                    <asp:TabPanel ID="tpAppraiseePanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="Appraisee" ID="lblAppraiseeTab" Width="99%" runat="server" CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <%-- <div>
                        <table cellpadding="7px" cellspacing="0px" width="100%" style="padding: 0px; border-collapse: collapse;
                            padding-right: 5px" class="TableSubHeader">
                            <tr>
                                <td align="left" class=" td_lines" colspan="4">
                                    <asp:Label ID="lblAppraiseeSerch" Text="Search" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="Grid1 td_lines">
                                    <asp:Label Text="Appraisal Cycle:" ID="lblAppraiseeAppraisalCycle" runat="server" />
                                    <asp:DropDownList runat="server" Width="122px" ID="ddlAppraiseeAppraisalCycle" AutoPostBack="true"
                                        CssClass="MLTextbox" OnSelectedIndexChanged="ddlAppraiseeAppraisalCycle_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="2013" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                            <div class="fullWidth">
                                <div style="width: 57%; padding-right: 13.5%;">
                                    <br />
                                    <asp:Label Text="Appraisals" ID="lblAppraiseeAppraisals" runat="server" />
                                    <br />
                                    <table class="fullWidth">
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="gvAppraisee" runat="server" CssClass="Grid fullWidth" AutoGenerateColumns="false">
                                                    <%-- AllowPaging="true" PageSize="10" OnPageIndexChanging="GvAppraisee_PageIndexChanging"--%>
                                                    <HeaderStyle CssClass="GridHead" />
                                                    <RowStyle CssClass="gridRowHeight" />
                                                    <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAppraisalId" CssClass="Grid1" runat="server" Text='<%#Eval("ID")%>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Performance Cycle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAppraiseePerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deactivated">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="appPerformanceCycle" HeaderText="Performance Cycle" />--%>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                                    CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="">
                                    <br />
                                    <asp:Label Text="PIP" ID="lblAppraiseePIP" runat="server" />
                                    <br />
                                    <table class="fullWidth">
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="gvPIPAppraisee" runat="server" CssClass="fullWidth" AutoGenerateColumns="false">
                                                    <HeaderStyle CssClass="GridHead" />
                                                    <RowStyle CssClass="gridRowHeight" />
                                                    <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPIPAppraisalId" CssClass="Grid1" runat="server" Text='<%#Eval("ID")%>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Performance Cycle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPIPPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phase(H1/H2)">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblPIPPhase" CssClass="Grid1" runat="server" Text='<%#Eval("Phase")%>'></asp:Label>--%>
                                                                <asp:LinkButton ID="lnkPIPPhase" CssClass="Grid1" runat="server" Text='<%#Eval("Phase")%>'
                                                                    CommandArgument='<%#Eval("appAppraisalStatus")%>' OnClick="lnkPIPPhase_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="appPerformanceCycle" HeaderText="Performance Cycle" />--%>
                                                        <%--<asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                            CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridHead" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpAppraiserPanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="Appraiser" ID="lblAppraiserTab" Width="99%" runat="server" CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                                    padding-right: 5px" class="Grid fullWidth">
                                    <tr class="GridHead">
                                        <td align="left" class=" td_lines" colspan="4">
                                            <asp:Label ID="lblAppraiserSearch" Text="Search" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Cycle" ID="lblAppraiserAppraisalCycle" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlApprasierAppraisalCycle" CssClass="MLTextbox Select_inputbox_Width">
                                                <%-- <asp:ListItem Text="Select"></asp:ListItem>
                                        <asp:ListItem Text="2013"></asp:ListItem>
                                        <asp:ListItem Text="2014"></asp:ListItem>
                                        <asp:ListItem Text="2015"></asp:ListItem>
                                        <asp:ListItem Text="2016"></asp:ListItem>
                                        <asp:ListItem Text="2017"></asp:ListItem>
                                        <asp:ListItem Text="2018"></asp:ListItem>
                                        <asp:ListItem Text="2019"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Employee code " ID="lblAppraiserEmployeeCode" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtAppraiserEmployeeCode" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserEmployeeCode"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Name" ID="lblAppraiserEmployeeName" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtAppraiserEmployeeName" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserEmployeeName"
                                                CssClass="image" runat="server" />--%>
                                            <%-- <td class="Grid1 td_lines">
                                        <asp:Label Text="OU" ID="lblAppraiserOU" runat="server" Visible="false" />
                                    </td>
                                    <td class="td_lines Grid1">
                                        <asp:TextBox ID="txtAppraiserOU" runat="server" CssClass="MLTextbox" Visible="false" />
                                        <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="Image6" CssClass="image"
                                            runat="server" Visible="false" />
                                    </td>--%>
                                            <td class="Grid1 td_lines">
                                                <%--<asp:Label Text="OU " ID="lblAppraiserOU" runat="server" />--%>
                                                <asp:Label Text="Area " ID="lblAppraiserArea" runat="server" />
                                            </td>
                                            <td class="td_lines Grid1">
                                                <asp:DropDownList runat="server" ID="ddlAppraiserArea" AutoPostBack="true" CssClass="MLTextbox Select_inputbox_Width"
                                                    onchange="showWaitScreen();" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <%--   <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserArea"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                                <%--<asp:DropDownList runat="server" ID="ddlAppraiserOU" CssClass="MLTextbox Select_inputbox_Width">
                                                </asp:DropDownList>
                                                <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserOU" CssClass="image"
                                                    Visible="false" runat="server" />--%>
                                            </td>
                                    </tr>
                                    <%--<tr  class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="position" ID="lblAppraiserPosition" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserPosition" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserPosition"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Area " ID="lblAppraiserArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserArea" AutoPostBack="true" CssClass="MLTextbox Select_inputbox_Width"
                                                OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserArea"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Sub Area" ID="lblAppraiserSubArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserSubArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--    <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserSubArea"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee group " ID="lblAppraiserEmployeeGroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserEmployeeGroup" CssClass="MLTextbox Select_inputbox_Width"
                                                onchange="showWaitScreen();" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--   <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserEmployeeGroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee Sub-group" ID="lblAppraiserEmployeesSubGroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserEmployeesSubGroup" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--    <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserEmployeesSubGroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines" colspan="">
                                            <asp:Label Text="Appraisal Status" ID="lblAppraiserAppraisalStatus" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserAppraisalStatus" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserAppraisalStatus"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Company" ID="lblAppraiserCompany" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlAppraiserCompany" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgAppraiserCompany"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                        </td>
                                        <td class="td_lines Grid1">
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines" colspan="4" align="right">
                                            <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                                OnClientClick="showWaitScreen();" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button Text="Reset" ID="btnReset" runat="server" OnClick="btnReset_Click" CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="fullWidth">
                                <div style="padding-right: 11%;">
                                    <br />
                                    <asp:Label Text="Appraisals" ID="lblAppraiserAppraisals" runat="server" />
                                    <br />
                                    <table class="fullWidth">
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="gvAppraiserAppraisals" CssClass="Grid fullWidth" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <%--PageSize="5"  AllowPaging="true"  OnPageIndexChanging="GvAppraiserAppraisals_PageIndexChanging"--%>
                                                    <HeaderStyle CssClass="GridHead" />
                                                    <RowStyle CssClass="gridRowHeight" />
                                                    <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                                    <Columns>
                                                        <%-- <asp:BoundField DataField="TaskID" HeaderText="Task" />--%>
                                                        <asp:TemplateField HeaderText="Performance Cycle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="appPerformanceCycle" ControlStyle-Font-Size="11px" ControlStyle-Font-Bold="false"
                                                    HeaderText="Performance Cycle" />--%>
                                                        <asp:TemplateField HeaderText="Employee Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApprEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" runat="server" CssClass="Grid1" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="EmpName" ControlStyle-Font-Size="11px" ControlStyle-Font-Bold="false"
                                                    HeaderText="Employee Name" />--%>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deactivated">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAppraisalStatus1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                                    CommandArgument='<%#Eval("TaskID")%>' CssClass="Grid1" OnClick="lnkAppraisalStatus_Click"></asp:LinkButton><%--CommandArgument='<%#Eval("TaskID")%>' --%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div>
                                    <br />
                                    <asp:Label Text="PIP" ID="lblAppraiserPIP" runat="server" />
                                    <br />
                                    <table class="fullWidth">
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="gvAppraiserPIP" runat="server" CssClass="fullWidth" AutoGenerateColumns="false">
                                                    <HeaderStyle CssClass="GridHead" />
                                                    <RowStyle CssClass="gridRowHeight" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPIPAppraisalId" CssClass="Grid1" runat="server" Text='<%#Eval("ID")%>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Performance Cycle">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPIPPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPIPEmpCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPIPEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Phase(H1/H2)">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAppraiserPIPPhase" CssClass="Grid1" runat="server" Text='<%#Eval("Phase")%>'
                                                                    CommandArgument='<%#Eval("appAppraisalStatus")%>' OnClick="lnkPIPPhase_Click"></asp:LinkButton>
                                                                <%--<asp:Label ID="lblPIPPhase" CssClass="Grid1" runat="server" Text='<%#Eval("Phase")%>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="appPerformanceCycle" HeaderText="Performance Cycle" />--%>
                                                        <%--<asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                            CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpReviewerPanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="Reviewer" ID="lblReviewer" Width="99%" runat="server" CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                                    padding-right: 5px" class="Grid fullWidth">
                                    <tr class="GridHead">
                                        <td align="left" class=" td_lines" colspan="4">
                                            <asp:Label ID="lblReviewerSearch" Text="Search" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Cycle" ID="lblReviewerAppraisalCycle" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewer" CssClass="MLTextbox Select_inputbox_Width">
                                                <%--<asp:ListItem Text="2013" />--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Employee code " ID="lblReviewerEmployeeCode" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewerEmployeeCode" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewerEmployeeCode"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Name" ID="lblReviewerEmployeeName" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewerEmployeeName" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewerEmployeeName"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraiser" ID="lblReviewerAppraiser" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewerAppraiser" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewerAppraiser"
                                                CssClass="image" runat="server" />--%>
                                    </tr>
                                    <%--<tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="OU " ID="lblReviewerOU" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewerOU" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewerOU" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="position" ID="lblReviewerPosition" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewerPosition" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewerPosition"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Area " ID="lblReviewerArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewerArea" AutoPostBack="true" CssClass="MLTextbox Select_inputbox_Width"
                                                onchange="showWaitScreen();" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewerArea" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Sub Area" ID="lblRevieweSubArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRevieweSubArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRevieweSubArea"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee group " ID="lblRevieweEmployeegroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRevieweEmployeegroup" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged" CssClass="MLTextbox Select_inputbox_Width"
                                                onchange="showWaitScreen();">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRevieweEmployeegroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee Sub-group" ID="lblRevieweEmployeeSubgroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRevieweEmployeeSubgroup" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRevieweEmployeeSubgroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines" colspan="">
                                            <asp:Label Text="Appraisal Status" ID="lblRevieweAppraisalStatus" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRevieweAppraisalStatus" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRevieweAppraisalStatus"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Company" ID="lblRevieweCompany" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRevieweCompany" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="Image50" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines" colspan="4" align="right">
                                            <asp:Button Text="Search" ID="btnReviewerSearch" runat="server" OnClick="btnSearch_Click"
                                                OnClientClick="showWaitScreen();" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button Text="Reset" ID="btnReviewerReset" runat="server" OnClick="btnReset_Click"
                                                CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <br />
                                <asp:Label Text="Appraisals" ID="lblReviewerAppraisals" runat="server" />
                                <br />
                                <table class="fullWidth">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="gvReviewerAppraisals" runat="server" AutoGenerateColumns="false"
                                                BorderColor="#005abc" CssClass="Grid fullWidth">
                                                <%-- AllowPaging="true" OnPageIndexChanging="GvReviewerAppraisals_PageIndexChanging"--%>
                                                <HeaderStyle CssClass="GridHead" />
                                                <RowStyle CssClass="gridRowHeight" />
                                                <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Performance Cycle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRevPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRevEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRevEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraiser">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRevApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="appPerformanceCycle" HeaderText="Performance Cycle" />
                                            <asp:BoundField DataField="appEmployeeCode" HeaderText="Employee Code" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="ApprName" HeaderText="Appraiser" />--%>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deactivated">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAppraisalStatus2" runat="server" CssClass="Grid1" Text='<%#Eval("appAppraisalStatus")%>'
                                                                CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpHrPanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="HRBusinessPartner" ID="lblHRBussinessParter" Width="99%" runat="server"
                                CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                                    padding-right: 5px" class="Grid fullWidth">
                                    <tr class="GridHead">
                                        <td align="left" class=" td_lines" colspan="4">
                                            <asp:Label ID="lblHRBPSearch" Text="Search" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Cycle" ID="blHRBPAppraisalCycle" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHRBPAppraisalCycle" CssClass="MLTextbox Select_inputbox_Width">
                                                <%-- <asp:ListItem Text="2013" />--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Employee code " ID="lblHRBPEmployeeCode" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtHRBPEmployeeCode" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="Image101" CssClass="image"
                                                runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Name" ID="lblHRBPEmployeeName" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtHRBPEmployeeName" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="ImaHRBPEmployee" CssClass="image"
                                                runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraiser" ID="lblHRBPAppraiser" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtHRBPAppraiser" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="ImgBPAppraiser" CssClass="image"
                                                runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Reviewer" ID="lblHRBPReviewer" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtHRBPReviewer" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="ImgHRBPReviewer" CssClass="image"
                                                runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <%--<asp:Label Text="OU " ID="Label17" runat="server" />--%>
                                            <asp:Label Text="Region" ID="lblHRBPRegion" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                           <asp:DropDownList runat="server" ID="ddlHRBPRegion" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHrPanelArea" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                            <%-- <asp:DropDownList runat="server" ID="ddlHRBPOU" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHRBPOU" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Position" ID="lblHRBPPosition" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHRBPPosition" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="Image12" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Area " ID="lblHrPanelArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHrPanelArea" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" onchange="showWaitScreen();"
                                                CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHrPanelArea" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                    <td class="Grid1 td_lines">
                                    <asp:Label Text="Area " ID="lblHrPanelArea" runat="server" />
                                    </td>
                                    <td class="td_lines Grid1">
                                    <asp:DropDownList runat="server" ID="ddlHrPanelArea" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                                                CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                    </td>
                                    <td class="Grid1 td_lines">
                                    <asp:Label Text="Sub Area" ID="lblHRBPSubArea" runat="server" />
                                    </td>
                                    <td class="td_lines Grid1">
                                    <asp:DropDownList runat="server" ID="ddlHRBPSubArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            </td>
                                     
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee group " ID="lblHrPanelEmployeeGroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHrPanelEmployeeGroup" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged" CssClass="MLTextbox Select_inputbox_Width"
                                                onchange="showWaitScreen();">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHRBPSubArea" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee Sub-group" ID="lblHRBPEmployeeSubgroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHRBPEmployeeSubgroup" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHrPanelEmployeeGroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                         <asp:Label Text="Appraisal Status" ID="lblHRBPAppraisalStatus" runat="server" />   
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHRBPAppraisalStatus" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHRBPEmployeeSubgroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines" colspan="">
                                            <asp:Label Text="Company" ID="lblHRBPCompany" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlHRBPCompany" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHRBPAppraisalStatus"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                     <%--  <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            
                                        </td>
                                        <td class="td_lines Grid1">
                                            
                                             <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgHRBPCompany" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1" colspan="2" align="center">
                                          <asp:LinkButton ID="lnkHRBPStatusDeactivation" Text="Workflow Status Deactivation"
                                        Font-Underline="true" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines" colspan="4" align="right">
                                            <asp:Button Text="Search" ID="btnHRBPSearch" OnClick="btnSearch_Click" runat="server"
                                                OnClientClick="showWaitScreen();" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button Text="Reset" ID="btnHRBPReset" OnClick="btnReset_Click" runat="server"
                                                CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines" colspan="4" align="left">
                                            <asp:Button Text="Deactivation" ID="btnDeactivation" OnClick="btnDeactivation_Click"
                                                runat="server" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <br />
                                <asp:Label Text="Appraisals" ID="lblHRBPAppraisals" runat="server" />
                                <br />
                                <table class="fullWidth">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="gvHRBPAppraisals" runat="server" CssClass="Grid fullWidth" AutoGenerateColumns="false">
                                                <HeaderStyle CssClass="GridHead" />
                                                <RowStyle CssClass="gridRowHeight" />
                                                <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                                <%--OnPageIndexChanging="GvHRBPAppraisals_PageIndexChanging" AllowPaging="true" PageSize="10"--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Performance Cycle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraisee Employee Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraisee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraiser Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reviewer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPRevName" CssClass="Grid1" runat="server" Text='<%#Eval("RevName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="appPerformanceCycle" HeaderText="Performance Cycle" />
                                            <asp:BoundField DataField="appEmployeeCode" HeaderText="Employee Code" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />--%>
                                                    <%--<asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />--%>
                                                    <%--<asp:BoundField DataField="ApprName" HeaderText="Appraiser" />
                                            <asp:BoundField DataField="RevName" HeaderText="Reviewer" />--%>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deactivated">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAppraisalStatus3" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                                CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <br />
                                <asp:Label Text="Appraisals Review Requests Tracker" ID="lblHRBPRequestsTracker"
                                    runat="server" />
                                <br />
                                <table class="fullWidth">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="gvHRBPRequestsTracker" runat="server" CssClass="Grid fullWidth"
                                                AutoGenerateColumns="false">
                                                <HeaderStyle CssClass="GridHead" />
                                                <RowStyle CssClass="gridRowHeight" />
                                                <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                                <%--PageSize="10" AllowPaging="true" OnPageIndexChanging="GvHRBPRequestsTracker_PageIndexChanging"--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Performance Cycle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPReqPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraisee Employee Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPReqEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraisee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPReqEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Appraiser">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPReqApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reviewer">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPReqRevName" CssClass="Grid1" runat="server" Text='<%#Eval("RevName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:BoundField DataField="TaskID" HeaderText="Task" Visible="false" />--%>
                                                    <%-- <asp:BoundField DataField="appPerformanceCycle" HeaderText="Performance Cycle" />
                                            <asp:BoundField DataField="appEmployeeCode" HeaderText="Employee Code" />--%>
                                                    <%--<asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />--%>
                                                    <%--<asp:BoundField DataField="appAppraiser" HeaderText="Appraiser" />--%>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phase">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhases" CssClass="Grid1" runat="server" Text='<%#Eval("acmptCompetency")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deactivated">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                                CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Review Closed">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHRBPIsReview" runat="server" CssClass="Grid1" Text='<%#Eval("IsReview")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="IsReview" HeaderText="Review Closed" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpTMTPanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="TMT" ID="lblTMT" Width="99%" runat="server" CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                                    padding-right: 5px" class="Grid fullWidth">
                                    <tr class="GridHead">
                                        <td align="left" class=" td_lines" colspan="4">
                                            <asp:Label ID="lblTMTSearch" Text="Search" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Cycle" ID="lblTMTAppraisalCycle" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTAppraisalCycle" CssClass="MLTextbox Select_inputbox_Width">
                                                <%-- <asp:ListItem Text="2013" />--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Employee code" ID="lblTMTAppraiseeEmployeecode" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtTMTAppraiseeEmployeecode" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTAppraiseeEmployeecode"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Name" ID="lblTMTAppraiseeName" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtTMTAppraiseeName" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTAppraiseeName"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraiser" ID="lblTMTAppraiser" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtTMTAppraiser" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTAppraiser" CssClass="image"
                                                runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Reviewer" ID="lblTMTReviewer" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtTMTReviewer" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTReviewer" CssClass="image"
                                                runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <%--<asp:Label Text="OU " ID="lblTMTOU" runat="server" />--%>
                                            <asp:Label Text="Region" ID="lblTMTRegion" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTRegion" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTArea" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                            <%--<asp:DropDownList runat="server" ID="ddlTMTOU" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTOU" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                    <td class="Grid1 td_lines">
                                    <asp:Label Text="Area " ID="lblTMTArea" runat="server" />
                                    </td>
                                    <td class="td_lines Grid1">
                                    <asp:DropDownList runat="server" ID="ddlTMTArea" AutoPostBack="true" CssClass="MLTextbox Select_inputbox_Width"
                                                OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" onchange="showWaitScreen();">
                                            </asp:DropDownList>
                                    </td>
                                    <td class="Grid1 td_lines">
                                    <asp:Label Text="Sub Area" ID="lblTMTSubArea" runat="server" />
                                    </td>
                                    <td class="td_lines Grid1">
                                    <asp:DropDownList runat="server" ID="ddlTMTSubArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                    </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Position" ID="lblTMTPosition" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTPosition" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTPosition" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Area " ID="lblTMTArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTArea" AutoPostBack="true" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTArea" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee group" ID="lblTMTEmployeegroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                           <asp:DropDownList runat="server" ID="ddlTMTEmployeegroup" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged"
                                                onchange="showWaitScreen();" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList> 
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTSubArea" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                          <asp:Label Text="Employee Sub-group" ID="lblTMTEmployeeSubgroup" runat="server" />  
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTEmployeeSubgroup" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTEmployeegroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                           <asp:Label Text="Appraisal Status" ID="lblTMTAppraisalStatus" runat="server" /> 
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTAppraisalStatus" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgTMTEmployeeSubgroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines" colspan="">
                                            <asp:Label Text="Company" ID="lblTMTCompany" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlTMTCompany" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="Image41" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <%-- <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            
                                        </td>
                                        <td class="td_lines Grid1">
                                            
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="Image42" CssClass="image"
                                                Visible="false" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1" colspan="2" align="center">
                                            <asp:LinkButton ID="lblTMTWorkflowStatusDeactivation" Text="Workflow Status Deactivation"
                                        Font-Underline="true" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines" colspan="4" align="right">
                                            <asp:Button Text="Search" ID="btnTMTSearch" OnClick="btnSearch_Click" runat="server"
                                                OnClientClick="showWaitScreen();" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button Text="Reset" ID="btnTMTReset" OnClick="btnReset_Click" runat="server"
                                                CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <br />
                                <asp:Label Text="Appraisals" ID="lblTMTAppraisals" runat="server" />
                                <br />
                                <asp:GridView ID="gvTMTAppraisals" runat="server" CssClass="Grid fullWidth" AutoGenerateColumns="false">
                                    <HeaderStyle CssClass="GridHead" />
                                    <RowStyle CssClass="gridRowHeight" />
                                    <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                    <%-- AllowPaging="true" PageSize="10" OnPageIndexChanging="GvTMTAppraisals_PageIndexChanging"--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Performance Cycle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTMTPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraisee Employee Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTMTEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraisee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTMTEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraiser">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTMTApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reviewer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTMTRevName" CssClass="Grid1" runat="server" Text='<%#Eval("RevName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deactivated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTMTAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                    OnClick="lnkAppraisalStatus_Click"></asp:LinkButton><%--CommandArgument='<%#Eval("TaskID")%>' --%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpRegionalHRPanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="RegionalHR" ID="lblRegionalHR" Width="99%" runat="server" CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                                    padding-right: 5px" class="Grid fullWidth">
                                    <tr class="GridHead">
                                        <td align="left" class=" td_lines" colspan="4">
                                            <asp:Label ID="lblRegionalHRSearch" Text="Search" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Cycle" ID="lblRegionalHRAppraisalCycle" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRAppraisalCycle" CssClass="MLTextbox Select_inputbox_Width">
                                                <%--<asp:ListItem Text="2013" />--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Employee code " ID="lblRegionalHRAppraiseeEmployeecode"
                                                runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtRegionalHRAppraiseeEmployeecode" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRAppraiseeEmployeecode"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Name" ID="lblRegionalHRAppraiseeName" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtRegionalHRAppraiseeName" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRAppraiseeName"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraiser" ID="lblRegionalHRAppraiser" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtRegionalHRAppraiser" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRAppraiser"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Reviewer" ID="lblRegionalHRReviewer" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtRegionalHRReviewer" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRReviewer"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <%--<asp:Label Text="OU " ID="lblRegionalHROU" runat="server" />--%>
                                            <asp:Label Text="Area" ID="lblRegionalHRArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRArea" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                                                CssClass="MLTextbox Select_inputbox_Width" onchange="showWaitScreen();">
                                            </asp:DropDownList>
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRArea"
                                                CssClass="image" Visible="false" runat="server"/>--%>
                                            <%--<asp:DropDownList runat="server" ID="ddlRegionalHROU" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHROU" CssClass="image"
                                                Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Position" ID="lblRegionalHRPosition" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRPosition" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRPosition"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Area" ID="lblRegionalHRArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRArea"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Sub Area" ID="lblRegionalHRSubArea" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRSubArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRSubArea"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee group" ID="lblRegionalHREmployeegroup" Width="122" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHREmployeegroup" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged" onchange="showWaitScreen();"
                                                CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHREmployeegroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Employee Sub-group" ID="lblRegionalHREmployeeSubgroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHREmployeeSubgroup" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHREmployeeSubgroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines" colspan="">
                                            <asp:Label Text="Appraisal Status" ID="lblRegionalHRAppraisalStatus" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRAppraisalStatus" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRAppraisalStatus"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Company" ID="lblRegionalHRCompany" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlRegionalHRCompany" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgRegionalHRCompany"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="td_lines Grid1" colspan="2" align="center">
                                            <%--<asp:LinkButton ID="lnkRegionalHRDeactivation" Text="Workflow Status Deactivation"
                                        Font-Underline="true" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines" colspan="4" align="right">
                                            <asp:Button Text="Search" ID="btnRegionalHRSearch" OnClick="btnSearch_Click" runat="server"
                                                OnClientClick="showWaitScreen();" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button Text="Reset" ID="btnRegionalHRReset" OnClick="btnReset_Click" runat="server"
                                                CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <br />
                                <asp:Label Text="Appraisals" ID="Label134" runat="server" />
                                <br />
                                <asp:GridView ID="gvRegionalHR" runat="server" CssClass="Grid fullWidth" AutoGenerateColumns="false">
                                    <HeaderStyle CssClass="GridHead" />
                                    <RowStyle CssClass="gridRowHeight" />
                                    <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                    <%-- AllowPaging="true" PageSize="10" OnPageIndexChanging="GvRegionalHR_PageIndexChanging"--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Performance Cycle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRHRPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraisee Employee Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRHREmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraisee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRHREmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraiser">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRHRApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reviewer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRHRRevName" CssClass="Grid1" runat="server" Text='<%#Eval("RevName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deactivated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRHRAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                    CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tpReviewBoardPanel" runat="server">
                        <HeaderTemplate>
                            <asp:Label Text="ReviewBoard" ID="lblReviewBord" Width="99%" runat="server" CssClass="customTabs" />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <div>
                                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                                    padding-right: 5px" class="Grid fullWidth">
                                    <tr class="GridHead">
                                        <td align="left" class=" td_lines" colspan="4">
                                            <asp:Label ID="lblReviewBoardSearch" Text="Search" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Cycle" ID="lblReviewBoardAppraisalCycle" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewBoardAppraisalCycle" CssClass="MLTextbox Select_inputbox_Width">
                                                <%--<asp:ListItem Text="2013" />--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Employee code " ID="lblReviewBoardAppraiseeEmployeecode"
                                                runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewBoardAppraiseeEmployeecode" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardAppraiseeEmployeecode"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisee Name" ID="lblReviewBoardAppraiseeName" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewBoardAppraiseeName" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardAppraiseeName"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraiser" ID="lblReviewBoardAppraiser" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewBoardAppraiser" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardAppraiser"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Reviewer" ID="lblReviewBoardReviewer" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:TextBox ID="txtReviewBoardReviewer" runat="server" CssClass="MLTextbox Inputbox_Width" />
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardReviewer"
                                                CssClass="image" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <%--<asp:Label Text="OU " ID="lblReviewBoardOU" runat="server" />--%>
                                            <asp:Label Text="Region" ID="lblReviewBoardRegion" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewBoardRegion" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardArea"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                            <%-- <asp:DropDownList runat="server" ID="ddlReviewBoardOU" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardOU"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                    <td class="Grid1 td_lines">
                                    <asp:Label Text="Area" ID="lblReviewBoardArea" runat="server" />
                                    </td>
                                    <td class="td_lines Grid1">
                                    <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged"
                                                ID="ddlReviewBoardArea" CssClass="MLTextbox Select_inputbox_Width" onchange="showWaitScreen();">
                                            </asp:DropDownList>
                                    </td>
                                    <td class="Grid1 td_lines">
                                     <asp:Label Text="Sub Area" ID="lblReviewBoardSubArea" runat="server" />
                                    </td>
                                    <td class="td_lines Grid1">
                                   <asp:DropDownList runat="server" ID="ddlReviewBoardSubArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                    </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Position" ID="lblReviewBoardPosition" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewBoardPosition" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardPosition"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Area" ID="lblReviewBoardArea" AutoPostBack="true" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewBoardArea" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardArea"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="Grid1 td_lines">
                                         <asp:Label Text="Employee group" ID="lblReviewBoardEmployeegroup" Width="122" runat="server" />
                                           
                                        </td>
                                        <td class="td_lines Grid1">
                                         <asp:DropDownList runat="server" ID="ddlReviewBoardEmployeegroup" OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged"
                                                onchange="showWaitScreen();" AutoPostBack="true" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            
                                            <%-- <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardSubArea"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines">
                                           <asp:Label Text="Employee Sub-group" ID="lblReviewBoardEmployeeSubgroup" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewBoardEmployeeSubgroup" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--  <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardEmployeegroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr class="GridAlt">
                                        <td class="Grid1 td_lines">
                                            <asp:Label Text="Appraisal Status" ID="lblReviewBoardAppraisalStatus" runat="server" /> 
                                        </td>
                                        <td class="td_lines Grid1">
                                            <asp:DropDownList runat="server" ID="ddlReviewBoardAppraisalStatus" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--   <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardEmployeeSubgroup"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                        <td class="Grid1 td_lines" colspan="">
                                           <asp:Label Text="Company" ID="lblReviewBoardCompany" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1">
                                             <asp:DropDownList runat="server" ID="ddlReviewBoardCompany" CssClass="MLTextbox Select_inputbox_Width">
                                            </asp:DropDownList>
                                            <%--<asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardAppraisalStatus"
                                                CssClass="image" Visible="false" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td class="Grid1 td_lines">
                                            
                                        </td>
                                        <td class="td_lines Grid1">
                                           
                                             <asp:Image ImageUrl="~/_layouts/images/SearchQueston1.gif" ID="imgReviewBoardCompany"
                                                CssClass="image" Visible="false" runat="server" />
                                        </td>
                                        <td class="td_lines Grid1" colspan="2" align="center">
                                            <asp:LinkButton ID="lnkReviewBoardDeactivation" Text="Workflow Status Deactivation"
                                        Font-Underline="true" runat="server" />
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="Grid1 td_lines" colspan="4" align="right">
                                            <asp:Button Text="Search" ID="btnReviewBoardSearch" OnClick="btnSearch_Click" runat="server"
                                                OnClientClick="showWaitScreen();" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                                            <asp:Button Text="Reset" ID="btnReviewBoardReset" OnClick="btnReset_Click" runat="server"
                                                CssClass="Button" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <br />
                                <asp:Label Text="Appraisals" ID="Label149" runat="server" />
                                <br />
                                <asp:GridView ID="gvReviewBord" CssClass="Grid fullWidth" runat="server" AutoGenerateColumns="false">
                                    <HeaderStyle CssClass="GridHead" />
                                    <RowStyle CssClass="gridRowHeight" />
                                    <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                    <%-- AllowPaging="true" PageSize="10" OnPageIndexChanging="GvRegionalHR_PageIndexChanging"--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Performance Cycle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRBPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraisee Employee Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRBEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraisee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRBEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appraiser">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRBApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reviewer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRBRevName" CssClass="Grid1" runat="server" Text='<%#Eval("RevName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Deactivated">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeactivated" CssClass="Grid1" runat="server" Text='<%#Eval("Deactivated")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRBAppraisalStatus" CssClass="Grid1" runat="server" Text='<%#Eval("appAppraisalStatus")%>'
                                                    CommandArgument='<%#Eval("TaskID")%>' OnClick="lnkAppraisalStatus_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Dash Board Page
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Dash Board Page
</asp:Content>
