<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoalCategories.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages.GoalCategories" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
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
    </style>
</asp:content>
<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <div>
        <fieldset>
            <asp:Label ID="lbledit" Text="New" runat="server" Font-Bold="true" CssClass="HeaderLabel"></asp:Label>
            <hr />
            <div style="margin-top: 10px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
                <table cellpadding="7px" cellspacing="2px" style="border: 1px solid black; border-collapse: collapse;"
                    class="TableSubHeader">
                    <tr>
                        <th colspan="2">
                        </th>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="Category" ID="lblCategory"></asp:Label>
                        </td>
                        <td width="390px">
                            <asp:TextBox ID="txtCategory" runat="server" Width="388px" CssClass="Textbox"></asp:TextBox>
                           
                            <asp:Label ID="lblCategoryValue" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVCategory" ValidationGroup="Group1" runat="server" ErrorMessage="Please enter Category" ControlToValidate="txtCategory" ForeColor="Red" ></asp:RequiredFieldValidator>

                        </td>

                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="Mandatory" ID="lblMandatory"></asp:Label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkMandatory" runat="server" ForeColor="Blue" />
                            <asp:Label ID="lblMandatoryValue" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1" style="vertical-align: top">
                            <asp:Label runat="server" Text="Description" ID="lblDescription"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Rows="4" CssClass="MLTextbox"
                                Width="388px"></asp:TextBox>
                            <asp:Label ID="lblDescriptionValue" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                             <asp:RequiredFieldValidator ID="RFVDesc" runat="server" ValidationGroup="Group1" ErrorMessage="Please enter description" ControlToValidate="txtDescription" ForeColor="Red" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button Text="Edit" runat="server" OnClick="btnEdit_Click" ID="btnEdit" Visible="false"
                                CssClass="Button" />&nbsp;&nbsp;
                            <asp:Button Text="Delete" runat="server" OnClick="btnDelete_Click" ID="btnDelete"
                                Visible="false" CssClass="Button" />&nbsp;&nbsp;
                            <asp:Button Text="Save" runat="server" ValidationGroup="Group1" OnClick="btnSave_Click" CssClass="Button"
                                ID="btnSave" />&nbsp;&nbsp;
                            <asp:Button Text="Cancel" runat="server"  OnClick="btnCancel_Click" CssClass="Button"
                                ID="btnCancel" />
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</asp:content>
<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
    Goal Categories
</asp:content>
<asp:content id="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Goal Categories
</asp:content>
