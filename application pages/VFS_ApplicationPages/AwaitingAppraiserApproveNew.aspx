<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AwaitingAppraiserApproveNew.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages.AwaitingAppraiserApproveNew"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink Name="~sitecollection/SiteAssets/jquery.js" ID="ScriptLink1"
        runat="server" Defer="false">
    </SharePoint:ScriptLink>
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
        .comments
        {
            float: left;
            padding-top: 12px;
        }
        .comments1
        {
            display: block;
        }
        .when
        {
            width: 74px;
        }
        
        .td_lines
        {
            border: solid 1px;
        }
        .competebcytdwidth
        {
            width: 882px;
        }
        .td_bottom
        {
            border-bottom-color: transparent;
        }
        .td_up
        {
            border-top-color: transparent;
        }
        .addbtn
        {
            width: 83px;
            height: 29px;
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
            width: 1024px;
        }
        .actiontd
        {
            width: 59px;
        }
        .nextstep
        {
            width: 350px;
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
        .what
        {
            width: 350px;
        }
        .addbtn
        {
            width: 50px;
            height: 20px;
        }
        
        .TableHeader
        {
            padding-left: 10px;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            background: url(../images/pixel-th.gif) #ffffff repeat-x;
            height: 24px;
            color: #ffffff;
            font-size: 13px;
            font-weight: bold;
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
        .Tableborder
        {
            background-color: #b2ddff;
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
        
        .tabs
        {
            width: 100%;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            height: 25px;
            color: #00358f;
            font-size: 13px;
            font-weight: bold;
        }
        .serial_numbers
        {
            vertical-align: top;
            text-align: left;
        }
        .rating1
        {
            vertical-align: top;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <script type="text/javascript" src="/_layouts/Scripts/jquery-1.9.0.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
           // $(".datecss").attr('readonly', 'readonly');
            $(".condtional_box").keypress(function (event) {
                var key = event.which || event.keyCode;
                if (!(48 <= key && key <= 57)) {
                    event.preventDefault();
                    return false;
                }
            });

        });
        function Validate() {
           
            var flag1 = true, flag2 = true;
            var totalSum = 0, row = 0;
            $('.condtional_box').each(function (i, obj) {

                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });

            //var validationlength = $('.validation').length;
            //alert(validationlength);
            $('.validation').each(function (i, obj) {
                row = parseInt(i / 5) + 1;
                if (obj.value.trim() == '') {

                    if ($(this).hasClass("reqtxtGoal")) {
                        alert('Please specify Goal at row: ' + row);

                        flag1 = false;
                        $(this).focus();
                        return false;
                    }
                    else if ($(this).hasClass("reqSPDateLastDate")) {
                        alert('Please specify due date for the Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqtxtDescription")) {
                        alert('Please specify Description for the Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("condtional_box")) {
                        alert('Please specify Weightage for the Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    
                    $("#ctl00_PlaceHolderMain_dvGoalSettings").show();
                    $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                    $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").addClass("ajax__tab_active");
                    $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").removeClass("ajax__tab_active");
                }
                else if ($(this).hasClass("golaCategory") && $(this).prop("selectedIndex") == 0) {

                    $("#ctl00_PlaceHolderMain_dvGoalSettings").show();
                    $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                    $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").addClass("ajax__tab_active");
                    $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").removeClass("ajax__tab_active");
                    alert('Please select Category for the Goal at row: ' + row);
                    $(this).focus();
                    flag1 = false;
                    return false;
                }
            });
            if (flag1) {
                $('.ExpectedResult').each(function (i, obj) {
                    if ($(this).prop("selectedIndex") == 0) {
                        row = parseInt(i / 1) + 1;
                        $("#ctl00_PlaceHolderMain_dvGoalSettings").hide();
                        $("#ctl00_PlaceHolderMain_dvCompetencies").show();
                        $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").removeClass("ajax__tab_active");
                        $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").addClass("ajax__tab_active");
                        alert('Please select Expected Result for the Competency at row: ' + row);
                        $(this).focus();
                        flag2 = false;
                        return false;
                    }
                });
            }
            var maxVal = 100;
            if (flag1 && flag2) {

                if (parseInt(maxVal) == parseInt(totalSum)) {
                    return true;
                }
                else {
                    alert('The sum of all weights should be 100%');
                    return false;

                }
            }
            else {
                return false;
            }
        }
        function confirmation() {
            if (confirm("Are you sure you want to delete?")) return true;
            else return false;
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
    <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
    <asp:HiddenField ID="hfflag" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" />
    <asp:HiddenField ID="hfMasterCompetencies" Value="false" runat="server" />
    <asp:HiddenField ID="hfEmpSubGroup" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnAdd"/>
        </Triggers>
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class=""
                    style="padding: 0px;">
                    <tr>
                        <td align="center" style="font-size: large;" colspan="2">
                            <%-- <asp:Label Text=" Appraiser" ID="lblselfevaluation" runat="server" />--%>
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
                                <%--</h3>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table cellpadding="7px" cellspacing="0px" width="100%" class="" style="">
                    <tr>
                        <th colspan="4">
                        </th>
                    </tr>
                    <tr>
                        <td class="FieldTitle FieldTitle1">
                            <asp:Label runat="server" Text="Employee Code" ID="lblEmpCodeValue"></asp:Label>
                        </td>
                        <td class="FieldCell FieldCell1">
                            <asp:Label runat="server" ID="lblemployeevalueeee"></asp:Label>
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
                            <asp:Label runat="server" Text="" ID="ex"></asp:Label>
                        </td>
                        <td class="FieldCell FieldCell2">
                            <asp:Label runat="server" ID="ex1" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="tabs">
                <asp:TabContainer ID="TabContainer1" runat="server" AutoPostBack="true">
                    <asp:TabPanel ID="tbpnluser" runat="server">
                        <HeaderTemplate>
                            Goals
                            <br />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="UserReg" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tbpnlusrdetails" runat="server">
                        <HeaderTemplate>
                            Competencies
                            <br />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
            <div runat="server" id="dvGoalSettings" class="tab1">
                <asp:HiddenField ID="hfGoalsCount" Value="5" runat="server" />
                <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
                    border-collapse: collapse; padding-right: 5px" class="Grid">
                    <asp:Repeater ID="rptGoalSettings" runat="server" >
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
                                    <asp:Label Text="Weightage(%)" runat="server" ID="lblWeightage" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Action" runat="server" ID="lblAction" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Grid1 td_lines td_bottom">
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblID" />
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                   <asp:Panel ID="testpnl" runat="server" style="display:none"/>
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("agGoalCategory") %>' runat="server" ID="lblCategory" />
                                    <asp:DropDownList ID="ddlCategories" runat="server" Visible="false"  CssClass="golaCategory validation">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines Grid1" align="left">
                                    <asp:TextBox runat="server" ID="txtGoal" Width="99%" CssClass="Textbox reqtxtGoal validation"
                                        Text='<%#Eval("agGoal") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label runat="server" ID="lblDueDate" Visible="false" Text='<%#Eval("agDueDate") %>' />
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" CssClassTextBox="datecss reqSPDateLastDate validation"
                                        DateOnly="true" />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="lblWeightage" Visible="false" Text='<%#Eval("agWeightage") %>' />
                                    <asp:TextBox runat="server" ID="txtWeightage" CssClass="Textbox condtional_box validation"
                                        Width="40%" Text='<%#Eval("agWeightage") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click"
                                        OnClientClick="return confirmation();" CommandArgument='<%#Eval("ID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up" style="vertical-align: top;">
                                </td>
                                <td colspan="5" class="td_lines Grid1 " align="left">
                                    <asp:Label Text="Description:  " runat="server" ID="lblDescription" CssClass="comments1" />
                                    <asp:Label runat="server" ID="lblDescriptionValue" />
                                    <asp:TextBox runat="server" ID="txtDescription" Text='<%#Eval("agGoalDescription") %>'
                                        TextMode="MultiLine" Columns="71" Width="99%" CssClass="MLTextbox reqtxtDescription validation" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="6" align="right" class="addbtn td_lines">
                            <asp:Button Text="Add" ID="btnAdd" OnClick="BtnAdd_Click" CssClass="Button" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td_lines td_bottom">
                        </td>
                        <td align="center" class="td_lines">
                            <asp:Label Text="100%" ID="lblTotalWeightage" runat="server" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label Text="Appraisee Comments:" ID="lblAppraiseeComments" CssClass="comments1"
                                runat="server" />
                            <br />
                            <asp:Label runat="server" ID="lblAppraiseeComments1" CssClass="Grid1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label ID="Label1" Text="Comments:" runat="server" CssClass="comments1" />
                            <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="99%" CssClass="MLTextbox  commentstextboxwidth" />
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" id="dvCompetencies" class="tab2">
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
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Grid1 td_lines serial_numbers">
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblCompetencyId" />
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("acmptCompetency") %>' Font-Bold="true" runat="server" ID="lblcompetencies" />
                                    <%-- <asp:Label Text='<%#Eval("cmptCompetency") %>' Font-Bold="true" runat="server" ID="lblcompetencies" />--%>
                                    <br />
                                    <asp:Label Text="Description:     " runat="server" ID="lblDescription" />&nbsp;&nbsp;
                                    <asp:Label runat="server" ID="lblDescriptionValue1" Text='<%#Eval("acmptDescription") .ToString().Replace(Environment.NewLine,"<br />")%>' />
                                    <%--<asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("cmptDescription").ToString().Replace(Environment.NewLine,"<br />")%>' />--%><%--Wrap="true" ReadOnly="true" TextMode="MultiLine" Rows="10"--%>
                                </td>
                                <td class="td_lines serial_numbers" align="center" style="vertical-align: top">
                                    <asp:Label runat="server" ID="lblexectedresult" Visible="false" />
                                    <asp:DropDownList runat="server" ID="ddlExpectedResult" CssClass="ExpectedResult">
                                        <asp:ListItem Text="Select" />
                                        <asp:ListItem Text="Good" />
                                        <asp:ListItem Text="Fair" />
                                        <asp:ListItem Text="Poor" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div>
                <table cellpadding="7px" cellspacing="2px" width="100%" class="buttondiv" ><%--style="border: 1px solid;
                    border-collapse: collapse; border-color: #000000; padding-right: 5px"
                    <tr>--%>
                        <td colspan="6" align="right">
                            <asp:Button Text="Print"   ID="btnPrint" CssClass="Button" runat="server" OnClientClick="printCert();return false;" />
                            <asp:Button Text="Save"    ID="btnSave" runat="server" OnClick="BtnSave_Click" CssClass="Button" />&nbsp;&nbsp;&nbsp;&nbsp;
                             
                            <asp:Button Text="Approve" ID="btnApprove" runat="server" OnClick="BtnApprove_Click" CssClass="Button" OnClientClick="return Validate()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button Text="Cancel"  ID="btnCancel" runat="server" OnClick="BtnCancel_Click"
                                CssClass="Button" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Awaiting Appraiser Approve
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Awaiting Appraiser Approve
</asp:Content>
