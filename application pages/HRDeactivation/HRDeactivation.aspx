<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRDeactivation.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.HRDeactivation.HRDeactivation" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link rel="stylesheet" href="../../SiteAssets/VFSCss.css" type="text/css"/>
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
    .td_lines
    {
        border: solid 1px;
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
</style>--%>
<script type="text/javascript">
    function confirmation() {
        if (document.getElementById('<%=txtComments.ClientID %>').value != "") {
            if (confirm("Are you sure you want to Deactivate?")) return true;
            else return false;
        }
        else {
            alert("Please specify reason for deactivation");
            return false;
        }
        return true;
    }
</script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:UpdatePanel ID="upHRDeactivation" runat="server">
     <Triggers>
            <asp:PostBackTrigger ControlID="btnDeactivation" />
            </Triggers>
        <ContentTemplate>
            <div>
                <div>
                    <asp:HiddenField ID="hfEmpId" runat="server" />
                </div>
                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                    padding-right: 5px" class="Grid fullWidth">
                    <tr class="GridHead">
                        <td align="left" class=" td_lines" colspan="2">
                            <asp:Label ID="lblAppraiseeSerch" Text="Search" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Grid1 td_lines">
                            <asp:Label Text="Appraisee Code:" ID="lblAppraiseeAppraisalCycle" runat="server" />
                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="MLTextbox" />
                        </td>
                        <td class="Grid1 td_lines">
                            <asp:Button Text="Search" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                                CssClass="Button" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <br />
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:GridView ID="gvDeactivation" runat="server" EmptyDataText="No records found." AutoGenerateColumns="false" CssClass="Grid fullWidth">
                                <HeaderStyle CssClass="GridHead" />
                                <RowStyle CssClass="gridRowHeight" />
                                <AlternatingRowStyle CssClass="GridAlt gridRowHeight" />
                                <%--OnPageIndexChanging="GvHRBPAppraisals_PageIndexChanging" AllowPaging="true" PageSize="10"--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Performance Cycle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPerformanceCycle" CssClass="Grid1" runat="server" Text='<%#Eval("appPerformanceCycle") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraisee Employee Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmployeeCode" CssClass="Grid1" runat="server" Text='<%#Eval("appEmployeeCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraisee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" CssClass="Grid1" runat="server" Text='<%#Eval("EmpName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Appraiser Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblApprName" CssClass="Grid1" runat="server" Text='<%#Eval("ApprName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reviewer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRevName" CssClass="Grid1" runat="server" Text='<%#Eval("RevName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HR Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHRName" CssClass="Grid1" runat="server" Text='<%#Eval("HrName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAppraisalId" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
            <div runat="server" id="dvDeactivate" visible="false">
                <table cellpadding="7px" cellspacing="0px" style="padding: 0px; border-collapse: collapse;
                    padding-right: 5px" class="Grid fullWidth">
                    <tr>
                        <td align="left" class="td_lines" style="width:7%;border-right: none;">
                            <asp:Label ID="lblComments" Text="Comments:" runat="server" /></td>
                            <td class="Grid1 td_lines" style="border-left:none;">
                                <asp:TextBox ID="txtComments" TextMode="Multiline" style="width:50%;float:left;" runat="server" CssClass="MLTextbox" />
                            </td>
                        
                    </tr>
                    <tr>
                        <td class="Grid1 td_lines" colspan="2" align="right">
                            <asp:Button Text="Deactivate" ID="btnDeactivation" runat="server" OnClick="btnDeactivation_Click"
                                CssClass="Button" OnClientClick="return confirmation();"/>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    HR Deactivation Page
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    HR Deactivation Page
</asp:Content>
