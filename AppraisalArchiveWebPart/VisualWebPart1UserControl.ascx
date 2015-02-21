<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VisualWebPart1UserControl.ascx.cs"
    Inherits="VFS.PMS.ArchivalProject.AppraisalArchiveWebPart.VisualWebPart1UserControl" %>
<script type="text/javascript">
    function ValidateCycle() {
        var cycle = document.getElementById('<%=ddlCycles.ClientID %>').selectedIndex;
        var Selectedcycle = document.getElementById('<%=ddlCycles.ClientID %>').options[cycle].text;
        var currentCycle = document.getElementById('<%=hfldcycle.ClientID %>').value;
        var diff = parseInt(currentCycle) - parseInt(Selectedcycle);
        if (parseInt(diff) < 3) {
            alert('You cannot archive a performance cycle appraisals less than 3 years.');
            return false;
        }
        else return true;
        return true;
    }
</script>
<asp:HiddenField ID="hfldcycle" runat="server" Value="0" />
<div style="float: left">
    <asp:Label Text="Performance Cycle :" runat="server" ID="lblCycle" />
    <asp:DropDownList ID="ddlCycles" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Button Text="Archive" runat="server" ID="btnArchive" OnClick="btnArchive_Click" OnClientClick="return ValidateCycle();"
        Style="color: #ffffff; background-color: #00305d; font-family: Tahoma, Arial, Trebuchet MS, Verdana, Helvetica, sans-serif;
        font-size: 11px;" />
</div>
