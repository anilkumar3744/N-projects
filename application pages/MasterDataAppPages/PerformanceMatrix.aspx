<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerformanceMatrix.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.MasterDataAppPages.PerformanceMatrix"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <link href="/_layouts/Styles/VFSCss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .Grid1
        {
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 20px;
            color: #005abc;
            font-size: 11px;
            font-weight: normal;
            text-decoration: none;
        }
        .HeaderLabel
        {
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 20px;
            color: #00358f;
            font-size: 15px;
            font-weight: normal;
            text-decoration: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <%-- <fieldset>--%>
    <div class="TableSubHeader">
        <asp:Label ID="lbledit" Text="New" runat="server" Font-Bold="true" CssClass="HeaderLabel"></asp:Label>
        <div style="margin-top: 10px; margin-left: 20px; margin-right: 20px; margin-bottom: 20px;">
            <center>
                <div>
                    <h3>
                        <asp:Label Text="Performance Rating Matrix" ID="lblHeader" runat="server" /></h3>
                </div>
                <asp:UpdatePanel ID="upnlAll" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table cellpadding="7px" cellspacing="2px" width="400px">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="gvPerformanceMatrix" ShowHeader="False" runat="server" AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true" OnRowCreated="gvPerformanceMatrix_RowCreated">
                                        <%-- HeaderStyle-ForeColor="White" CssClass="Gridview" HeaderStyle-BackColor="#61A6F8"--%>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumberScale" Text='<%#Eval("pmScaleNumber") %>' Width="50px" CssClass="Grid1"
                                                        runat="server" />
                                                        <asp:Label ID="lblID" Text='<%#Eval("ID") %>' Width="50px" runat="server" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInitialAchievement" Text='<%#Eval("pmInitialAchievement") %>'
                                                        runat="server" Visible="false" />
                                                    <asp:Label ID="lblInitialAchievement" Text='<%#Eval("pmInitialAchievement") %>' CssClass="Grid1"
                                                        Width="50px" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMaximumAchievement" Text='<%#Eval("pmMaximumAchievement") %>'
                                                        CssClass="Textbox" Width="50px" OnTextChanged="txtMaximum_Maximum" AutoPostBack="true"
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" /> --%>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="gvPerformanceMatrixView" runat="server" AutoGenerateColumns="false"
                                        HeaderStyle-Font-Bold="true" OnRowCreated="gvPerformanceMatrixView_RowCreated">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumberScale" Text='<%#Eval("pmScaleNumber") %>' CssClass="Grid1"
                                                        Width="50px" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInitialAchievement" Text='<%#Eval("pmInitialAchievement") %>' CssClass="Grid1"
                                                        Width="50px" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInitialAchievement" Text='<%#Eval("pmMaximumAchievement") %>' CssClass="Grid1"
                                                        Width="50px" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <table cellpadding="7px" cellspacing="2px" width="400px">
                    <tr>
                        <td style="padding-left: 165px">
                            <asp:Button Text="Add" runat="server" OnClick="btnAdd_Click" CssClass="Button" ID="btnAdd" />
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 90px">
                            <asp:Button Text="Save" runat="server" OnClick="btnSave_Click" CssClass="Button"
                                ID="btnSave" />&nbsp;&nbsp;
                            <asp:Button Text="Cancel" runat="server" OnClick="btnCancel_Click" CssClass="Button"
                                ID="btnCancel" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </div>
    <%--  </fieldset>--%>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Performance Matrix
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Performance Matrix
</asp:Content>
