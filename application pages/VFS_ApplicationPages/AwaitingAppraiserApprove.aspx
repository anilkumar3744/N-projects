<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AwaitingAppraiserApprove.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages.AwaitingAppraiserApprove" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
 <asp:Button Text="Appraiser Approve" ID="btnAppraiser" OnClick="btnAppraiser_Click" runat="server" />
    <asp:Button Text="Appraisee Submit" ID="btnAppraisee" OnClick="btnAppraisee_Click" runat="server" />
    <asp:Button Text="Self Evaluation" ID="btnSelfEvaluation" OnClick="btnSelfEvaluation_Click" runat="server" />
    <asp:Button Text="Appraiser Evaluation" ID="btnAppraiserEvaluation" OnClick="btnAppraiserEvaluation_Click" runat="server" />
    <asp:Button Text="Reviewer Approve" ID="btnReviewerApprove" OnClick="btnReviewerApprove_Click" runat="server" />
    <asp:Button Text="Appraisee Sign Off" ID="btnAppraiseeSignOff" OnClick="btnAppraiseeSignOff_Click" runat="server" />
    <asp:Button Text="Appraisee Appeal" ID="btnAppeal" OnClick="btnAppeal_Click" runat="server" />
    <asp:Button Text="HR Close" ID="btnHRClose" OnClick="btnHRClose_Click" runat="server" />
    <asp:Button Text="HR Request" ID="btnHRRequest" OnClick="btnHRRequest_Click" runat="server" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Awaiting Appraiser Approve
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Awaiting Appraiser Approve
</asp:Content>
