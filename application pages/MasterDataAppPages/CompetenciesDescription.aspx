<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompetenciesDescription.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages.CompetenciesDescription"
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
            <asp:Label ID="lbledit" Text="New" runat="server" Font-Bold="true" CssClass="HeaderLabel"></asp:Label>
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
                            <asp:Label runat="server" Text="Empolyee Group" ID="Label1"></asp:Label>
                        </td>
                        <td width="390px">
                            <asp:DropDownList runat="server" ID="ddlEmployeeGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployeeGroup_SelectedIndexChanged"
                                CssClass="MyDropdown" Width="392px">
                            </asp:DropDownList>
                            <asp:Label ID="lblEmpGroup" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Group1" InitialValue="Choose Employee Group"  runat="server" ErrorMessage="Please Select any Employee group" ControlToValidate="ddlEmployeeGroup" ForeColor="Red" ></asp:RequiredFieldValidator>
                        
                        </td>
                    </tr>
                    <tr>
                        <td width="250px" class="td1 Grid1">
                            <asp:Label runat="server" Text="Employee Sub Group" ID="lblEmpSubGroup"></asp:Label>
                        </td>
                        <td width="390px">
                            <asp:DropDownList runat="server" ID="ddlEmpSubGroup" CssClass="MyDropdown" Width="392px">
                            </asp:DropDownList>
                            <asp:Label ID="lblEmpSubGroupValue" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Group1" InitialValue="Choose Emp Sub Group"  runat="server" ErrorMessage="Please Select any subgroup" ControlToValidate="ddlEmpSubGroup" ForeColor="Red" ></asp:RequiredFieldValidator>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="td1 Grid1">
                            <asp:Label runat="server" Text="Competency" ID="lblCompetency"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlCompetency" Width="392px" CssClass="MyDropdown">
                            </asp:DropDownList>
                            <asp:Label ID="lblCompetencyValue" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Group1" InitialValue="Choose Competency"  runat="server" ErrorMessage="Please Select any Competency" ControlToValidate="ddlCompetency" ForeColor="Red" ></asp:RequiredFieldValidator>
                        
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top" class="td1 Grid1">
                            <asp:Label runat="server" Text="Description" ID="lblDescription"></asp:Label>
                        </td>
                        <td>
                        
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Height="" runat="server" Rows="4"
                                CssClass="MLTextbox" Width="388px"></asp:TextBox>
                            <asp:Label ID="lblDescriptionValue" runat="server" CssClass="Grid1" Visible="false"></asp:Label>
                           <asp:RequiredFieldValidator ID="RFVCategory" ValidationGroup="Group1" runat="server" ErrorMessage="Please Give a Decscription" ControlToValidate="txtDescription" ForeColor="Red" ></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="right">
                            <asp:Button Text="Edit" runat="server" OnClick="btnEdit_Click" ID="btnEdit" Visible="false"
                                CssClass="Button" />&nbsp;&nbsp;
                            <asp:Button Text="Delete" runat="server" OnClick="btnDelete_Click" ID="btnDelete"
                                Visible="false" CssClass="Button" />&nbsp;&nbsp;
                            <asp:Button Text="Save" runat="server" ValidationGroup="Group1"  OnClick="btnSave_Click" ID="btnSave" CssClass="Button" />&nbsp;&nbsp;
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
    Competency Description
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Competency Description
</asp:Content>
