<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkflowDeactivation.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.VFS_TMTActions.WorkflowDeactivation" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
<script type="text/javascript">
    function myfunction() {
        //var nid = SP.UI.Notify.addNotification("<img src='/_layouts/images/loadingcirclests16.gif' style='vertical-align: top;'/> Operation in progress...', true");
        var waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose('Something awesome...', 'Please wait while something awesome is happening...', 76, 330);

       //var waitDialog = SP.UI.ModalDialog.showWaitScreenWithNoClose = function (title, message, height, width) {
        
    }
   
</script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:Button ID="btnDeactivate" Text="Deactivte" OnClick="BtnDeactivate_Click" OnClientClick ="myfunction();" runat="server" />
     <asp:Button ID="btnH2Start" Text="H2Start" OnClick="BtnH2Start_Click" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
