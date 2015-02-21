<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H2AppraiseeGoalsDraftView.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.H2initial.H2AppraiseeGoalsDraftView" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
<SharePoint:ScriptLink Name="~sitecollection/SiteAssets/jquery.js" ID="ScriptLink1"
        runat="server" Defer="false">
    </SharePoint:ScriptLink>
    <script type="text/javascript">


        $(document).ready(function () {
            $("body").delegate(".condtional_box", "keyup", function (e) {

                var str = $(this).val();

                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    if (e.which < 96 || e.which > 105) {
                        $(this).val(str.substring(0, str.length - 1));
                        return false;
                    }
                }
            });

        });

    </script>
    <script type="text/javascript">
        function OpenDialog(strPageURL) {

            var dialogOptions = SP.UI.$create_DialogOptions();
            dialogOptions.url = '<%=message%>'; // URL of the Page
            dialogOptions.width = 750; // Width of the Dialog
            dialogOptions.height = 500; // Height of the Dialog
            SP.UI.ModalDialog.showModalDialog(dialogOptions); // Open the Dialog
            //dialogOptions.dialogReturnValueCallback = Function.createDelegate(null, CloseCallback); // Function to capture dialog closed event
            
            return false;
        }
        function CloseCallback(result, target) {
            window.location.href = window.location.href;
        }
        function printCert() {
            var url = "../Print/PrintAppraisal.aspx?AppId=" + '<%=Request.Params["AppId"]%>' + "&IsDlg=1";
            wopen(url, 'popup', 640, 480);
        }
        function wopen(url, name, w, h) {
            // Fudge factors for window decoration space.
            // In my tests these work well on all platforms & browsers.
            w += 32;
            h += 96;
            var win = window.open(url,
			  name,
			  'width=' + w + ', height=' + h + ', ' +
			  'location=no, menubar=no, ' +
			  'status=no, toolbar=no, scrollbars=1, resizable=yes');
            win.resizeTo(w, h);
            win.focus();
        }
    </script>
      
<%--<link href="/_layouts/STYLES/VFSCss.css" rel="stylesheet" type="text/css" />--%>
<link rel="stylesheet" href="../../SiteAssets/VFSCss.css" type="text/css"/>
   <%-- <style type="text/css">
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
        
        .last_td
        {
            border-right: transparent;
        }
        
        .td_lines
        {
            border: solid 1px;
        }
        .td_bottom
        {
            border-bottom-color: transparent;
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
            width: 838px;
        }
        .addbtn
        {
            width: 3px;
            height: 1px;
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
    </style>--%>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
<div>
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" /> 
      <asp:UpdatePanel runat="server" ID="upnlInitialGoalSetting">
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnSubmit" />--%>
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnPrint" />
           <%-- <asp:PostBackTrigger ControlID="btnAdd" />--%>
        </Triggers>
        <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class=""
            style="padding: 0px;">
            <tr>
                <td align="center" style="font-size: large;" colspan="2">
                    <asp:Label Text="" ID="lblAppraiserView" runat="server" />
                </td>
            </tr>
           <%-- <tr>
                <td align="right" colspan="2">
                    <div style="float: right">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/_layouts/images/vfs-global-logo.png"
                            Width="50%" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>--%>
          <tr class="TableHeader">
                <td>
                    <%--<h3 style="margin-top: 3px; margin-left: 10px;">--%>
                        <asp:Label runat="server" Text="Performance Agreement for:  " ID="lblHeader" class="TableHeader_1"></asp:Label>&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblHeaderValue" class="TableHeader_1"></asp:Label> 
                   <%-- </h3>--%>
                </td>
                <td align="right">
                   <%-- <h3 style="margin-top: 3px;">--%>
                        <asp:Label runat="server" Text="Workflow State: " ID="lblStatus" class="TableHeader_1"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblStatusValue" class="TableHeader_1"></asp:Label>
                      <%--  </h3>--%>
                </td>
            </tr>
        </table>
        <div>
            <table cellpadding="7px" cellspacing="0px" width="100%" style="">
                <tr>
                    <th colspan="4">
                    </th>
                </tr>
                <tr>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Employee Code" ID="lblEmpCode"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell1">
                        <asp:Label runat="server" ID="lblempcodevalue"></asp:Label>
                    </td>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Employee Name" ID="lblEmpName"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell3">
                        <asp:Label runat="server" ID="lblEmpNameValue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Organizational Unit" ID="lblOrgUnit"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell1">
                        <asp:Label runat="server" ID="lblOrgUnitValue"></asp:Label>
                    </td>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Position" ID="lblPosition"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell3">
                        <asp:Label runat="server" ID="lblPositionValue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Hire Date" ID="lblHireDate"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell1">
                        <asp:Label runat="server" ID="lblHireDateValue"></asp:Label>
                    </td>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Appraiser" ID="lblAppraiser"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell3">
                        <asp:Label runat="server" ID="lblAppraiserValue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="Reviewer" ID="lblReviewer"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell1">
                        <asp:Label runat="server" ID="lblReviewerValue"></asp:Label>
                    </td>
                    <td class="FieldTitle FieldTitle1">
                        <asp:Label runat="server" Text="HR Business Partner" ID="lblCountryHR"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell3">
                        <asp:Label runat="server" ID="lblCountryHRValue"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="FieldTitle last_td">
                        <asp:Label runat="server" Text="Appraisal Period" ID="lblAppraisalPeriod"></asp:Label>
                    </td>
                    <td class="FieldCell last_td">
                        <asp:Label runat="server" ID="lblAppraisalPeriodValue"></asp:Label>
                    </td>
                    <td class="FieldTitle last_td">
                        <asp:Label runat="server" ID="ex"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell2">
                        <asp:LinkButton ID="lnkViewH1Details" runat="server" OnClientClick="javascript:return OpenDialog();"  Text="View H1 Details" Font-Underline="true" ></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
    </div>
    <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
        border-collapse: collapse; padding-right: 5px" class="Grid">
        <asp:Repeater ID="rptAppraiserView" runat="server">
            <HeaderTemplate>
                <tr class="GridHead">
                    <td align="center" class="td_lines " style="width: 5%;">
                        <asp:Label Text="S.No" runat="server" ID="lblSno" />
                    </td>
                    <td align="center" class="td_lines" style="width: 25%;">
                        <asp:Label Text="Category" runat="server" ID="lblCategory" />
                    </td>
                    <td align="center" class="td_lines" style="width: 47%;">
                        <asp:Label Text="Goal" runat="server" ID="lblGoal" />
                    </td>
                    <td align="center" class="td_lines" style="width: 10%;">
                        <asp:Label Text="Due Date" runat="server" ID="lblDueDate" />
                    </td>
                    <td align="center" class="td_lines" style="width: 3%;">
                        <asp:Label Text="Weightage (%)" runat="server" ID="lblWeightage" />
                    </td>
                   
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="center" class="td_lines Grid1 td_bottom td_bottom">
                        <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                    </td>
                    <td class="td_lines Grid1">
                        <asp:Label Text='<%#Eval("agGoalCategory") %>' runat="server" ID="lblCategory" />                        
                    </td>
                    <td class="td_lines Grid1" align="left" style="">
                        <asp:Label runat="server" ID="lblGoal" Visible="true" Text='<%#Eval("agGoal") %>'  />                        
                    </td>
                    <td class="td_lines Grid1" align="center">
                        <asp:Label runat="server" ID="lblDueDate" Text='<%# ((DateTime)Eval("agDueDate")).ToString("dd-MMM-yyyy")%>' />                        
                    </td>
                    <td class="td_lines Grid1" style="" align="center">
                        <asp:Label runat="server" ID="lblWeightage" Text='<%#Eval("agWeightage") %>' />
                    </td>                   
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="4" class="td_lines Grid1" align="left">
                        <asp:Label Text="Description" runat="server" ID="lblDescriptions" />
                        <asp:Label runat="server" ID="lblDescriptionValues" Text='<%#Eval("agGoalDescription") %>' />                      
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
       <%-- <tr>
            <td class="td_lines td_bottom Grid1" align="center" style="width: 5%;">
                <asp:Label Text="  6" ID="lblsno" runat="server" />
            </td>
            <td class="td_lines  Grid1" style="width: 25%;">
                 <asp:DropDownList ID="ddlCategories" runat="server" Width="70%" Visible="true" CssClass="MLTextbox">
                    <asp:ListItem>Special Project </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_lines Grid1" style="width: 47%;">
                  <asp:TextBox ID="TextBox5" Width="99%" Text="FTE should be more than 0.8" runat="server"
                    CssClass="MLTextbox" />
            </td>
            <td class="td_lines Grid1 " align="center" style="width: 10%;">
                  <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="DateTimeControl1" CssClassTextBox="datecss"
                    DateOnly="true" SelectedDate="" />
            </td>
            <td class="td_lines Grid1" align="center" style="width: 3%;">
                <asp:Label runat="server" ID="lblWeightage" Text="10" />
             </td>
            <td class="td_lines Grid1" align="center" style="width: 10%;">
                <asp:LinkButton Text="Edit" ID="lnkedit" Visible="false" runat="server" />
                <asp:LinkButton Text="Delete" ID="lnkdelete" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="5" class="td_lines Grid1" align="left">
                <asp:Label Text="Description" runat="server" ID="Label3" />
                  <asp:TextBox ID="txtdescip6" TextMode="MultiLine" Text="I should be able to maintain 0.8 for all employees reporting to me"
                    runat="server" Width="99%" CssClass="MLTextbox" />
            </td>
        </tr>
        <tr>
            <td class="td_lines Grid1  td_bottom" align="center" style="width: 5%;">
                <asp:Label Text="7" ID="lblsno7" runat="server" />
            </td>
            <td class="td_lines Grid1" style="width: 25%;">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="70%" Visible="true" CssClass="MLTextbox">
                    <asp:ListItem>Financial </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td_lines MLTextbox" style="width: 47%;">
                            CssClass="MLTextbox" />
            </td>
            <td class="td_lines MLTextbox" align="center" style="width: 10%;">
                  <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" CssClassTextBox="datecss"
                    DateOnly="true" SelectedDate="" />
            </td>
            <td class="td_lines Grid1" align="center" style="width: 3%;">
                <asp:Label Text="10" ID="lblweightage7" runat="server" />
            </td>
            <td class="td_lines Grid1" align="center" style="width: 10%;">
                <asp:LinkButton Text="Delete" ID="lnkSave" Visible="true" runat="server" />
                <asp:LinkButton Text="Cancel" ID="lnkCancel" Visible="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td_lines Grid1">
            </td>
            <td colspan="5" class="td_lines Grid1" align="left">
                <asp:Label Text="Description" runat="server" ID="lblDescriptions" />
                    <asp:TextBox ID="txtdescription7" TextMode="MultiLine" Text="I should be able to maintain 0.8 for all employees reporting to me"
                    runat="server" Width="99%" CssClass="MLTextbox" />
            </td>
        </tr>
        <tr>
            <td colspan="6" align="right" class="addbtn td_lines Grid1 ">
                <asp:Button Text="Add" ID="btnAdd" CssClass="Button" Width="100" runat="server" />
            </td>
        </tr>--%>
        <tr>
            <td colspan="4 " align="right" class="td_lines td_bottom">
                <asp:Label runat="server" ID="txtweight" Text="Weight"></asp:Label>
            </td>
            <td align="center" class="td_lines">
                <asp:Label Text="100%" ID="lblTotalWeightage" runat="server" />
            </td>
            <%--<td class="td_lines td_up td_bottom" colspan="1">
            </td>--%>            
        </tr>
        <tr>
            <td class="td_lines Grid1 td_bottom" colspan="5">
                <asp:Label Text="Appraisee Comments:" ID="lblAppraiseeComments" runat="server" />
                <br />
               <asp:Label runat="server" ID="lblAppraiseeComments1" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right" class="addbtn td_lines">
            <asp:Button Text="Print" ID="btnPrint" CssClass="Button"  runat="server" OnClientClick="printCert();return false;" />
                <asp:Button Text="Cancel" ID="btnCancel" CssClass="Button" OnClick="btnCancel_Click" Width="100" runat="server" />
            </td>
        </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Goals View
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Goals View
</asp:Content>
