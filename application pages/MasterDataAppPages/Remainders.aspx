<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Remainders.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages.Remainders"
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
        
        .MyDropdown
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
    </style>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div>
        <input type="hidden" id="sum_input" />
        <fieldset>
            <asp:Label ID="lbledit" Text="Reminders" runat="server" Font-Bold="true" CssClass="HeaderLabel"></asp:Label>
            <hr />
            <div style="margin-top: 20px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
                <table width="550px" cellpadding="7px" cellspacing="2px" style="border: 1px solid black;
                    border-collapse: collapse;" class="TableSubHeader">
                    <tr>
                        <th colspan="2">
                        </th>
                    </tr>
                    <tr>
                        <td width="250px" class="td1 Grid1">
                            <asp:Label runat="server" Text="WorkflowState" ID="lblworkflow"></asp:Label>
                        </td>
                        <td width="390px">
                            <asp:Label ID="lblworkflowvalue" runat="server" CssClass="Grid1"></asp:Label>
                            <asp:TextBox runat="server" ID="txtWorkFlowvalue" />
                        </td>
                    </tr>
                    <tr>
                        <td width="250px" class="td1 Grid1">
                            <asp:Label runat="server" Text="Reminder Required" ID="lblRemainderRequest"></asp:Label>
                        </td>
                        <td width="390px">
                            <asp:DropDownList runat="server" ID="ddlRemainderRequest" CssClass="MyDropdown"  AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlRemainderRequest_SelectedIndexChanged">
                                <asp:ListItem Text="No" />
                                <asp:ListItem Text="Yes" />
                            </asp:DropDownList>
                            <asp:Label Text="" ID="lblRemainderRequestView" runat="server" />
                            </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="Recurring" ID="lblRecurring"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlRecurring" CssClass="MyDropdown">
                                <asp:ListItem Text="NO" />
                                <asp:ListItem Text="Yes" />
                            </asp:DropDownList>
                           <%-- <asp:CompareValidator ErrorMessage="Please Select Reminder Request as YES " 
                            ControlToCompare="ddlRemainderRequest" ControlToValidate="ddlRecurring" Operator="Equal" 
                                runat="server" />--%>
                            <asp:Label Text="Please Select Reminder Reqest as Yes" ID="lblRemainderRequestvalue" Visible="false" runat="server" />
                           <asp:Label Text="" ID="lblRecurringVIew" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="Duration" ID="lbldureation"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtduration" Text="0" Width="40px" />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" Type="Integer" MinimumValue="0"
                                MaximumValue="180" ControlToValidate="txtduration" ErrorMessage="Only positive 1 to 180 days allowed" />
                        <asp:Label Text="" ID="lbldureationView" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button Text="Update" runat="server" OnClick="btnUpdate_Click" ID="btnUpdate"
                                CssClass="Button" />&nbsp;&nbsp;
                            <asp:Button Text="Save" runat="server" OnClick="btnSave_Click" ID="btnSave" CssClass="Button" />&nbsp;&nbsp;
                            <asp:Button Text="Cancel" runat="server" OnClick="btnCancel_Click" ID="btnCancel"
                                CssClass="Button" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Reminder
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    My Application Page
</asp:Content>
