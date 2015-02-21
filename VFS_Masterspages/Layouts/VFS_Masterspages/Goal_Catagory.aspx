<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Goal_Catagory.aspx.cs"
    Inherits="VFS_Masterspages.Layouts.VFS_Masterspages.Goal_Catagory" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <%--<SharePoint:ScriptLink Name="~sitecollection/SiteAssets/jquery.js" ID="ScriptLink1"
        runat="server" Defer="false">
    </SharePoint:ScriptLink>--%>
    <%--<link href="/_layouts/CSS/VFSCss.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" href="../../SiteAssets/VFSCss.css" type="text/css"/>
   <%-- <style type="text/css">
        .fullWidth
        {
            width: 100%;
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
        .Button
        {
            border-bottom: #5f97c7 2px solid;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#e6f2fc, endColorstr=#5fa3dd);
            border-left: #5f97c7 2px solid;
            font-family: Tahoma, Arial, Trebuchet MS, Verdana, Helvetica, sans-serif;
            height: 20px;
            color: #02307e;
            font-size: 11px;
            border-top: #5f97c7 2px solid;
            cursor: hand;
            font-weight: bold;
            border-right: #5f97c7 2px solid;
        }
        .Dropdown
        {
            background-color: #ffffff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            color: #333333;
            font-size: 11px;
            font-weight: normal;
        }
        .DropdownColor
        {
            background-color: #ffffff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            color: #333333;
            font-size: 11px;
            font-weight: normal;
        }
        .gridRowHeight
        {
            height: 28px;
        }
        .LblTextbox
        {
            border-bottom: #5bb4fa 1px solid;
            border-left: #5bb4fa 1px solid;
            background-color: #ffffff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 14px;
            color: #005abc;
            font-size: 11px;
            border-top: #5bb4fa 1px solid;
            font-weight: normal;
            border-right: #5bb4fa 1px solid;
        }
        .Textbox
        {
            background-color: #ffffff;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 14px;
            color: #333333;
            font-size: 11px;
            font-weight: normal;
        }
        .ui-dialog
        {
            width: 710px !important;
        }
        
        .CompetencyDescription_Form1
        {
            width: 60%;
            margin-right: auto;
            margin-left: auto;
            padding: 4px;
            border: #005abc 1px solid;
        }
    </style>--%>
   <%-- <style type="text/css">
        .gvPagerCss span
        {
            background-color: #DEE1E7;
            font-size: 15px;
        }
        .gvPagerCss td
        {
            padding-left: 5px;
            padding-right: 5px;
        }
        .Lblbold
        {
            
            color: #00358f;
            font-size: 11px;
            font-weight: bold;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            }
            
    </style>--%>
    <script type="text/javascript" language="javascript">

        //User Defined Function to Open Dialog Framework
        //        function OpenDialog(strPageURL) {
        //            var dialogOptions = SP.UI.$create_DialogOptions();
        //            dialogOptions.url = strPageURL; // URL of the Page
        //            dialogOptions.width = 750; // Width of the Dialog
        //            dialogOptions.height = 500; // Height of the Dialog
        //            dialogOptions.dialogReturnValueCallback = Function.createDelegate(null, CloseCallback); // Function to capture dialog closed event
        //            SP.UI.ModalDialog.showModalDialog(dialogOptions); // Open the Dialog
        //            return false;
        //        }

        //        function CloseCallback(result, target) {
        //            window.location.href = window.location.href;
        //        }

        //    $(function () {

        //        $("#dialog-modal1").dialog({

        //            autoOpen: false,
        //            modal: true,
        //            
        //        });

        //        $(".popup").click(function () {


        //           $("#dialog-modal1").dialog("open");
        //        });
        //    });
        function confirmation() {
            if (confirm("Are you sure you want to delete?")) return true;
            else return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    
    <div>
        <asp:GridView ID="goalcatogeryGridview" AutoGenerateColumns="false" runat="server"
            CssClass="Grid fullWidth " Font-Bold="false" AlternatingRowStyle-CssClass="GridAlt gridRowHeight"
            PageSize="5" AllowPaging="true" OnPageIndexChanging="goalcatogeryGridview_PageIndexChanging"
            PagerStyle-CssClass="gvPagerCss" OnRowCommand="goalcatogeryGridview_RowCommand">
            <RowStyle CssClass="gridRowHeight" />
            <HeaderStyle CssClass="gridRowHeight GridHead" />
            <Columns>
                <asp:BoundField HeaderText="Category" DataField="ctgrCategory" ControlStyle-CssClass="Grid1" />
                <asp:BoundField HeaderText="Description" DataField="ctgrDescription" ControlStyle-CssClass="Grid1" />
                <asp:BoundField HeaderText="Mandatory" DataField="ctgrMandatory" ControlStyle-CssClass="Grid1" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("ID")%>'
                            CssClass="Grid1 popup" CommandName="CmdEdit"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("ID")%>'
                            CssClass="Grid1" OnClientClick="return confirmation();" CommandName="CmdDelete"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings Mode="Numeric" PageButtonCount="6" />
        </asp:GridView>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button Text="ADD" ID="Button1" runat="server" OnClick="btnADD_Click" CssClass="Button" /><%--OnClientClick="OpenListDialog();"--%>
                    <%-- <a href="#" onclick="return false;" class="Button popup">ADD</a>--%>
                </td>
            </tr>
        </table>
    </div>
    <div id="Goal_Catogery_Form" class="reminders_edit" runat="server" visible="false" style="width:100%">
       
            <table class="Grid CompetencyDescription_Form1 Performance_rating_second_table">
                <tr>
                    <td>
                        <asp:Label Text="Category" ID="lblCategory" runat="server" CssClass="Lblbold"
                            Style="vertical-align: top;" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCategory" CssClass="Textbox" Style="width: 314px;" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Group1"
                            runat="server" ErrorMessage="Please enter category" ControlToValidate="txtCategory"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Description" ID="lblDescription" runat="server" CssClass="Lblbold" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescription" CssClass="Textbox" TextMode="MultiLine"
                            Style="padding-bottom: 15%; width: 62%;" />
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Group1"
                            runat="server" ErrorMessage="Please enter description" ControlToValidate="txtDescription"
                            ForeColor="Red"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Mandatory" ID="lblMandatory" runat="server" CssClass="Lblbold" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkMandatory" runat="server" ForeColor="Blue" />
                    </td>
                </tr>
                <tr>
                <td></td>
                    <td align="center">
                        <asp:Button Text="Submit" ID="btnSubmit" OnClick="btnSubmit_OnClick" runat="server"
                            ValidationGroup="Group1" CssClass="Button" style="margin-left: 60px;" />
                   
                        <asp:Button Text="Cancel" ID="btnCancel" OnClick="btnCancel_OnClick" runat="server"
                            CssClass="Button"  style="margin-right: 9%;" />
                    </td>
                </tr>
            </table>
      
    </div>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Goal Category
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Goal Category
</asp:Content>
