<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelfView.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.H2initial.SelfView"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
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
         .MLTextboxError
        {
            border-bottom: #5bb4fa 1px solid;
            border-left: #5bb4fa 1px solid;
            background-color: #ffffff;
            border-color:Red;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            color: #333333;;
            font-size: 11px;
            border-top: #5bb4fa 1px solid;
            font-weight: normal;
            border-right: #5bb4fa 1px solid;
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
            width: 100%;
        }
        .comments1
        {
            display: block;
        }
        .commentstextboxwidth
        {
            width: 99%;
        }
        .addbtn
        {
            width: 1.5px;
            height: 0.27;
        }
          .serial_numbers
        {
            vertical-align:top;
            text-align:left;
            }
        .rating1
          {   
           vertical-align:top;   
              }  
    </style>--%>
    <script type="text/javascript">
        function OpenDialog(strPageURL) {
            var dialogOptions = SP.UI.$create_DialogOptions();
            dialogOptions.url = '<%=message%>'; // URL of the Page
            dialogOptions.width = 750; // Width of the Dialog
            dialogOptions.height = 500; // Height of the Dialog
            //dialogOptions.dialogReturnValueCallback = Function.createDelegate(null, CloseCallback); // Function to capture dialog closed event
            SP.UI.ModalDialog.showModalDialog(dialogOptions); // Open the Dialog
            return false;
        }

        $(document).ready(function () {
            ////alert("hi");
            $(".datecss").attr('readonly', 'readonly');
            ////            $(".condtional_box").keypress(function (event) {
            ////                var key = event.which || event.keyCode;
            ////                if (!(48 <= key && key <= 57)) {
            ////                    event.preventDefault();
            ////                    return false;
            ////                }
            ////            });

        });
        function Validate() {
            var flag1 = true, flag2 = true, flag3 = true;
            var totalSum = 0, row = 0;
            $('.condtional_box').each(function (i, obj) {

                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });

            $('.validation').each(function (i, obj) {
                if (obj.value == '') {
                    row = parseInt(i / 5) + 1;
                    if ($(this).hasClass("reqtxtGoal")) {
                        alert('Please specify Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqSPDateLastDate")) {
                        alert('Please specify Due Date at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqtxtDescription")) {
                        alert('Please specify Description at row : ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("condtional_box")) {
                        alert('Please specify Weightage at row : ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("Evalution")) {
                        alert('Please specify Evalution at row : ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    $("#ctl00_PlaceHolderMain_dvGoalSettings").show();
                    $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                    $("#ctl00_PlaceHolderMain_saftymeasurementdevelopment").hide();
                    $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnluser ").addClass("ajax__tab_active");
                    $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails").removeClass("ajax__tab_active");
                    $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnljobdetails").removeClass("ajax__tab_active");
                }
            });

            if (flag1) {
                $('.competeniciesValidations').each(function (i, obj) {
                    if ($(this).prop("selectedIndex") == 0) {
                        row = parseInt(i / 1) + 1;
                        alert('Please select Rating at row: ' + row);
                        $("#ctl00_PlaceHolderMain_dvGoalSettings").hide();
                        $("#ctl00_PlaceHolderMain_dvCompetencies").show();
                        $("#ctl00_PlaceHolderMain_saftymeasurementdevelopment").hide();
                        $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnluser ").removeClass("ajax__tab_active");
                        $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails").addClass("ajax__tab_active");
                        $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnljobdetails").removeClass("ajax__tab_active");
                        $(this).focus();
                        flag2 = false;
                        return false;
                    }
                });
            }

            if (flag1 && flag2) {
                $('.measuresValidations').each(function (i, obj) {
                    if (obj.value == '') {
                        row = parseInt(i / 4) + 1;
                        if ($(this).hasClass("datecss")) {
                            alert('Please specify When at row: ' + row);
                            $(this).focus();
                            flag3 = false;
                            return false;
                        }
                        else if ($(this).hasClass("what")) {
                            alert('Please specify what at row: ' + row);
                            $(this).focus();
                            flag3 = false;
                            return false;
                        }
                        else if ($(this).hasClass("nextstep")) {
                            alert('Please specify nextstep at row : ' + row);
                            $(this).focus();
                            flag3 = false;
                            return false;
                        }
                        else if ($(this).hasClass("Comments")) {
                            alert('Please specify Comments at row : ' + row);
                            $(this).focus();
                            flag3 = false;
                            return false;
                        }
                        $("#ctl00_PlaceHolderMain_dvGoalSettings").hide();
                        $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                        $("#ctl00_PlaceHolderMain_saftymeasurementdevelopment").show();
                        $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnluser ").removeClass("ajax__tab_active");
                        $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails").removeClass("ajax__tab_active");
                        $("#__tab_ctl00_PlaceHolderMain_TabContainer1_tbpnljobdetails").addClass("ajax__tab_active");
                    }
                });
            }

            if (flag1 && flag2 && flag3)
                return true;
            else
                return false;
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
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
    <asp:HiddenField ID="hfflag" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" />
    <asp:HiddenField ID="hfSaveFlag" Value="false" runat="server" />
    <asp:UpdatePanel runat="server" ID="upnlInitialGoalSetting">
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnSubmit" />--%>
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnPrint" />
          <%--  <asp:PostBackTrigger ControlID="btnAdd" />--%>
        </Triggers>
        <ContentTemplate>
    <div>
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
                        <asp:Image ID="imgvfsLog" runat="server" ImageUrl="~/_layouts/images/vfs-global-logo.png"
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
                   <%-- <h3 style="margin-top: 3px; margin-left: 10px;">--%>
                        <asp:Label runat="server" Text="Performance Agreement for:  " ID="lblHeader" class="TableHeader_1"></asp:Label>&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblHeaderValue" class="TableHeader_1"></asp:Label>
                    <%--</h3>--%>
                </td>
                <td align="right">
                   <%-- <h3 style="margin-top: 3px;">--%>
                        <asp:Label runat="server" Text="Workflow State: " ID="lblStatus" class="TableHeader_1"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblStatusValue" class="TableHeader_1"></asp:Label><%--</h3>--%>
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
                        <%-- <asp:Label runat="server" ID="lblAppraisalPeriodValue"></asp:Label>--%>
                        <asp:Label runat="server" ID="lblAppraisalPeriodValue"></asp:Label>
                    </td>
                    <td class="FieldTitle last_td">
                        <asp:Label runat="server" ID="ex"></asp:Label>
                    </td>
                    <td class="FieldCell FieldCell2">
                        <asp:LinkButton ID="lnkViewH1Details" runat="server" Text="View H1 Details" OnClientClick="javascript:return OpenDialog();" Font-Underline="true"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <br />
    <div style="width: 100%; font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
        height: 25px; color: #00358f; font-size: 13px; font-weight: bold;">
        <asp:TabContainer ID="TabContainer1" runat="server" AutoPostBack="true">
            <asp:TabPanel ID="tbpnluser" runat="server">
                <HeaderTemplate>
                    Goals
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="UserReg" runat="server">
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbpnlusrdetails" runat="server">
                <HeaderTemplate>
                    Competencies
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="Panel1" runat="server">
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tbpnljobdetails" runat="server">
                <HeaderTemplate>
                    Development Measures
                </HeaderTemplate>
                <ContentTemplate>
                    <asp:Panel ID="Panel2" runat="server">
                    </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </div>
    <div runat="server" id="dvGoalSettings">
        <asp:HiddenField ID="hfGoalsCount" Value="5" runat="server" />
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse; padding-right: 5px" class="Grid">
            <asp:Repeater ID="rptGoalSettings" runat="server">
                <HeaderTemplate>
                    <tr class="GridHead">
                        <td align="center" class="td_lines " style="width: 5%;">
                            <asp:Label Text="S.No" runat="server" ID="lblSno" />
                        </td>
                        <td align="center" class="td_lines" style="width: 25%;">
                            <asp:Label Text="Category" runat="server" ID="lblCategory" />
                        </td>
                        <td align="center" class="td_lines" style="width: 41%;">
                            <asp:Label Text="Goal" runat="server" ID="lblGoal" />
                        </td>
                        <td align="center" class="td_lines" style="width: 10%;">
                            <asp:Label Text="Due Date" runat="server" ID="lblDueDate" />
                        </td>
                        <td align="center" class="td_lines" style="width: 3%;">
                            <asp:Label Text="Weightage(%)" runat="server" ID="lblWeightage" />
                        </td>
                        <td align="center" class="td_lines" style="width: 3%;">
                            <asp:Label Text="Evaluation(%)" runat="server" ID="lblEvaluation" />
                        </td>
                        <td align="center" class="td_lines" style="width: 3%;">
                            <asp:Label Text="Score" runat="server" ID="lblScore" />
                        </td>
                        <%-- <td align="center" class="td_lines" style="width: 10%;">
                            <asp:Label Text="Action" Visible="false" runat="server" ID="lblAction" />
                        </td>--%>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="Grid1 ">
                            <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                            <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="lblId" />
                        </td>
                        <td class="td_lines Grid1">
                            <asp:Label Text='<%#Eval("agGoalCategory") %>' runat="server" ID="lblCategory" />
                            <asp:DropDownList ID="ddlCategories" runat="server" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td class="td_lines Grid1" align="left">
                            <asp:Label runat="server" ID="lblGoal" Visible="true" Text='<%#Eval("agGoal") %>' />
                            <asp:TextBox runat="server" ID="txtGoal" Width="99%" Visible="false" CssClass="Textbox reqtxtGoal"
                                Text='<%#Eval("agGoal") %>' />
                        </td>
                        <td class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblDueDate" Visible="true" Text='<%# ((DateTime)Eval("agDueDate")).ToString("dd-MMM-yyyy")%>' />
                            <%--<SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" Visible="false" CssClassTextBox="datecss reqSPDateLastDate"
                                DateOnly="true" />--%>
                        </td>
                        <td class="td_lines Grid1" style="" align="center">
                            <asp:Label runat="server" ID="lblWeightage" Visible="true" Text='<%#Eval("agWeightage") %>' />
                            <asp:TextBox runat="server" ID="txtWeightage" Visible="false" CssClass="Textbox condtional_box"
                                Width="40%" Text='<%#Eval("agWeightage") %>' />
                        </td>
                        <td class="td_lines Grid1" style="" align="center">
                            <asp:Label runat="server" ID="lblEvaluation" Visible="true" Text='<%#Eval("agEvaluation") %>' />
                            <asp:TextBox runat="server" ID="txtEvaluation" Visible="false" CssClass="Textbox condtional_box"
                                Width="40%" Text='<%#Eval("agEvaluation") %>' />
                        </td>
                        <td class="td_lines Grid1" style="" align="center">
                            <asp:Label runat="server" ID="lblScore" Visible="true" Text='<%#Eval("agScore") %>' />
                        </td>
                        <%-- <td class="td_lines Grid1" style="" align="center">
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="center" class="td_lines td_up" style="vertical-align: top;">
                        </td>
                        <td colspan="6" class="td_lines Grid1 " align="left">
                            <asp:Label Text="Description:  " runat="server" ID="lblDescription" CssClass="comments1" />
                            <asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("agGoalDescription") %>' />
                            <%-- <asp:TextBox runat="server" ID="txtDescription" Text='<%#Eval("agGoalDescription") %>'
                                TextMode="MultiLine" Width="99%" CssClass="MLTextbox reqtxtDescription" Visible="false" />--%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="4" class="td_lines" align="right">
                    <asp:Label Text="Weight" ID="lblweight" runat="server"></asp:Label>
                </td>
                <td align="center" class="td_lines">
                    <asp:Label Text="100%" ID="lbl100" runat="server" />
                </td>
                <td class="td_lines" align="right">
                    <asp:Label Text="Score" ID="lblScore" runat="server"></asp:Label>
                </td>
                <td align="center" class="td_lines">
                    <asp:Label ID="lblScoretotal" Visible="true" runat="server" />
                </td>
                <%--<td colspan="3" class="td_lines">
                </td>--%>
            </tr>
            <tr>
                <%-- <td colspan="8">
                    <asp:Label ID="Label1" Text="Comments" runat="server" CssClass="comments1" />
                    <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Visible="false" CssClass="MLTextbox  commentstextboxwidth" />
                     <asp:Label Text='<%#Eval("agAppraiseeLatestComments")%>' ID="lblReviewerComments" runat="server"></asp:Label>
                </td>--%>
            </tr>
        </table>
    </div>
    <div runat="server" id="dvCompetencies">
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse;" class="Grid">
            <asp:Repeater ID="rptCompetencies" runat="server">
                <HeaderTemplate>
                    <tr class="GridHead">
                        <td align="center" class="td_lines">
                            <asp:Label Text="S.No" runat="server" ID="lblSno" />
                        </td>
                        <td align="center" class="td_lines competebcytdwidth">
                            <asp:Label Text="Competency" runat="server" ID="lblcompetencies" />
                        </td>
                        <td align="center" class="td_lines">
                            <asp:Label Text="Expected Results" runat="server" ID="lblexpectedresult" />
                        </td>
                        <td align="center" class="td_lines">
                            <asp:Label Text="Rating" runat="server" ID="lblRating" />
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="Grid1 serial_numbers">
                            <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                            <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="lblItemId" />
                        </td>
                        <td align="left" class="td_lines Grid1">
                            <asp:Label Text='<%#Eval("acmptCompetency") %>' Font-Bold="true" runat="server" ID="lblcompetencies" />
                            <br />
                            <asp:Label Text="Description:     " runat="server" ID="lblDescription" />&nbsp;&nbsp;
                            <asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("acmptDescription") .ToString().Replace(Environment.NewLine,"<br />")%>' />
                        </td>
                        <td class="Grid1 td_lines serial_numbers" align="center">
                            <asp:Label runat="server" ID="lblexectedresult" Text='<%#Eval("acmptExpectedResult") %>'
                                Visible="true" />
                        </td>
                        <td class="Grid1 td_lines rating1" align="center">
                            <asp:Label runat="server" ID="lblrating" Visible="false" />
                            <asp:Label runat="server" ID="lblratingValue" Text='<%#Eval("acmptRating")%>' Visible="true" />
                            <%-- <asp:DropDownList runat="server" ID="ddlrating">
                                <asp:ListItem Text="Select" />
                                <asp:ListItem Text="Good" />
                                <asp:ListItem Text="Fair" />
                                <asp:ListItem Text="Poor" />
                            </asp:DropDownList>--%>
                        </td>
                        <tr>
                            <td class="td_lines td_up" align="center">
                            </td>
                            <td class="td_lines Grid1" colspan="7">
                                <%--<asp:Label Text="Comments" ID="LblComments" runat="server" />--%>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="TxtAppraisercmts" CssClass="MLTextbox"
                                    Width="99%" Visible="false" />
                                <asp:Label Text='<%#Eval("acmptAppraiseeLatestComments")%>' ID="lblReviewerComm"
                                    runat="server" />
                            </td>
                        </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div runat="server" id="saftymeasurementdevelopment">
        <asp:HiddenField ID="hfsaftymeasurementdevelopment" Value="1" runat="server" />
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse;" class="Grid">
            <asp:Repeater ID="rptsaftymeasurementdevelopment" runat="server">
                <HeaderTemplate>
                    <tr class="GridHead">
                        <td align="center" class="td_lines" style="width: 5%">
                            <%-- <%# Container.DataItemIndex + 1 %>--%>
                            <asp:Label Text="S.No" runat="server" ID="lblSno" />
                        </td>
                        <td align="center" class="td_lines" style="width: 10%">
                            <asp:Label Text="When" runat="server" ID="lblwhen1" />
                        </td>
                        <td align="center" class="td_lines" style="width: 40%">
                            <asp:Label Text="What" runat="server" ID="lblwhat1" />
                        </td>
                        <td align="center" class="td_lines" style="width: 35%">
                            <asp:Label Text="Next Step" runat="server" ID="lblnextstep1" />
                        </td>
                        <%--<td align="center" class="td_lines" style="width: 10%">
                            <asp:Label Text="Action" Visible="false" runat="server" ID="lblaction" />
                        </td>--%>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="td_lines Grid1 td_bottom verticalalign">
                            <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                            <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="LblDeld" />
                            <%--  <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblID" />--%>
                        </td>
                        <td align="center" class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblWhen" Text='<%# Eval("pdpWhen") %>' />
                            <%--<SharePoint:DateTimeControl LocaleId="2057" ID="shpdatecontrol" runat="server" DateOnly="true" CssClassTextBox="datecss"
                                TimeOnly="false" />--%>
                        </td>
                        <td align="left" class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblWhat" Text='<%#Eval("pdpWhat") %>' />
                            <%--<asp:TextBox runat="server" ID="txtwhat" TextMode="MultiLine" Width="99%" Text='<%#Eval("pdpWhat") %>' />--%>
                        </td>
                        <td align="left" class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblNextSteps" Text='<%#Eval("pdpNextSteps") %>' />
                            <%-- <asp:TextBox runat="server" ID="nextstep" TextMode="MultiLine" Width="99%" Text='<%#Eval("pdpNextSteps") %>' />--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="td_lines td_up">
                        </td>
                        <td class="td_lines Grid1" colspan="4">
                            <asp:Label Text=" Comments" ID="lblComments" runat="server" CssClass="comments1" />
                            <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="99%" Visible="false"
                                Text='<%#Eval("pdpH1AppraiserComments") %>' />
                            <asp:Label Text='<%#Eval("pdpH1AppraiserComments") %>' ID="lblCommentsValue" runat="server"
                                CssClass="comments1"></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div>
        <table cellpadding="7px" cellspacing="2px" width="100%" class="buttondiv">
            <tr>
                <td align="right" class="td_lines Grid1 td_up" colspan="4">
                 <asp:Button Text="Print" ID="btnPrint" CssClass="Button"  runat="server" OnClientClick="printCert();return false;" />
                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                        CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Self Evaluation View
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Self Evaluation View
</asp:Content>
