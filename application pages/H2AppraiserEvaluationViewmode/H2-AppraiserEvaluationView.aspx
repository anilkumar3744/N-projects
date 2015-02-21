<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H2-AppraiserEvaluationView.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.H2AppraiserEvaluationViewmode.H2_AppraiserEvaluationView"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">

        //User Defined Function to Open Dialog Framework
        function OpenDialog(strPageURL) {
            var dialogOptions = SP.UI.$create_DialogOptions();
            dialogOptions.url = strPageURL; // URL of the Page
            dialogOptions.width = 750; // Width of the Dialog
            dialogOptions.height = 500; // Height of the Dialog
            //dialogOptions.dialogReturnValueCallback = Function.createDelegate(null, CloseCallback); // Function to capture dialog closed event
            SP.UI.ModalDialog.showModalDialog(dialogOptions); // Open the Dialog
            return false;
        }

        function CloseCallback(result, target) {
            window.location.href = window.location.href;
        }
    </script>
   <%-- <link href="/_layouts/STYLES/VFSCss.css" rel="stylesheet" type="text/css" />--%>
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
            text-align: center;
        }
        .rating1
        {
            vertical-align: top;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $(".datecss").attr('readonly', 'readonly');
            $(".condtional_box").keypress(function (event) {
                var key = event.which || event.keyCode;
                if (!(48 <= key && key <= 57)) {
                    event.preventDefault();
                    return false;
                }
            });

        });

        function Validate() {
            var flag1 = true, flag2 = true, flag3 = true, flag4 = true, flag5 = true, flag6 = true, flag7 = true, flag8 = true, flag9 = true;
            var totalSum = 0, row = 0;
            $('.condtional_box').each(function (i, obj) {

                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });
            $('.validation').each(function (i, obj) {
                if (obj.value == '') {
                    row = parseInt(i / 6) + 1;
                    if ($(this).hasClass("reqtxtGoal")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqSPDateLastDate")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Due date at row: ' + row);
                        $(this).focus();
                        flag2 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqtxtDescription")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Description at row : ' + row);
                        $(this).focus();
                        flag3 = false;
                        return false;
                    }
                    else if ($(this).hasClass("condtional_box")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Weightage at row : ' + row);
                        $(this).focus();
                        flag4 = false;
                        return false;
                    }
                    else if ($(this).hasClass("Evaluation")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Evaluation at row : ' + row);
                        $(this).focus();
                        flag5 = false;
                        return false;
                    }
                    else if ($(this).hasClass("Score")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Score at row : ' + row);
                        $(this).focus();
                        flag6 = false;
                        return false;
                    }
                    else if ($(this).hasClass("When")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify When at row : ' + row);
                        $(this).focus();
                        flag7 = false;
                        return false;
                    }
                    else if ($(this).hasClass("What")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify What at row : ' + row);
                        $(this).focus();
                        flag8 = false;
                        return false;
                    }
                    else if ($(this).hasClass("NextSteps")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Next Steps at row : ' + row);
                        $(this).focus();
                        flag9 = false;
                        return false;
                    }
                }
                else {
                    ////  $(".abc").keyup(function () {
                    //// if ($(this).val())
                    //// if( $(this).hasClass("EmptyField"))
                    //// $(this).removeClass("EmptyField");
                    ////                    });
                    //                    row = parseInt(i / 4) + 1;
                    //                    if ($(this).hasClass("condtional_box")) {
                    //                        if ($.isNumeric(this.val)) {
                    //                            alert('Please eneter numeric values for Weightage at row : ' + row);
                    //                            //$(this).addClass("EmptyField");
                    //                            $(this).focus();
                    //                            flag3 = false;
                    //                            return false;
                    //                        }
                    //                    }

                }
            });
            var maxVal = 100;
            if (flag1 && flag2 && flag3) {

                if (parseInt(maxVal) == parseInt(totalSum)) {
                    return true;
                }
                else {
                    alert('All weights must not exceed 100%');
                    return false;

                }
            }
            else {
                return false;
            }
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
    <asp:HiddenField ID="hfflag" Value="false" runat="server" />
    <asp:HiddenField ID="HiddenField1" Value="false" runat="server" />
    <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
    <asp:HiddenField ID="HiddenField2" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" />
    <asp:UpdatePanel ID="updatepanel" runat="server">
    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnAdd" />
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
                  <%--  <tr>
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
                           <%-- <h3 style="margin-top: 3px; margin-left: 10px;">--%>
                                <asp:Label runat="server" Text="Performance Agreement for:  " ID="lblHeader" class="TableHeader_1"></asp:Label>&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblHeaderValue" class="TableHeader_1"></asp:Label>
                           <%-- </h3>--%>
                        </td>
                        <td align="right">
                           <%-- <h3 style="margin-top: 3px;">--%>
                                <asp:Label runat="server" Text="Workflow State:" ID="lblStatus" class="TableHeader_1"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblStatusValue" class="TableHeader_1"></asp:Label>
                               <%-- </h3>--%>
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
                            <asp:Label runat="server" ID="lblemployeevalue"></asp:Label>
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
                            <asp:LinkButton ID="LinkView" Text="H1 Details" OnClick="LnkDetails_Click" runat="server"></asp:LinkButton>
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
                    <asp:TabPanel ID="TabPanelDev" runat="server">
                        <HeaderTemplate>
                            DevelopmentMeasures
                            <br />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Tbpanel" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <asp:TabPanel ID="tbpnlpip" runat="server">
                        <HeaderTemplate>
                            PIP
                        </HeaderTemplate>
                        <ContentTemplate>
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
                                    <asp:Label Text="Evaluation" runat="server" ID="LblEvaluation" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Score" runat="server" ID="Lblscore" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Action" runat="server" ID="lblAction" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Grid1 serial_numbers">
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblID" />
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("agGoalCategory") %>' runat="server" ID="lblCategory" />
                                    <asp:DropDownList ID="ddlCategories" runat="server" Visible="false">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines Grid1" align="left">
                                    <asp:Label Text='<%#Eval("agGoal") %>' runat="server" ID="LblGoal" />
                                    <asp:TextBox runat="server" ID="txtGoal" Width="99%" Visible="false" CssClass="Textbox reqtxtGoal validation"
                                        Text='<%#Eval("agGoal") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label runat="server" ID="lblDueDate" Text='<%#Eval("agDueDate") %>' />
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" Visible="false" ID="SPDateLastDate" CssClassTextBox="datecss reqSPDateLastDate validation"
                                        DateOnly="true" />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="lblWeightage" Text='<%#Eval("agWeightage") %>' />
                                    <asp:TextBox runat="server" ID="txtWeightage" Visible="false" CssClass="Textbox condtional_box validation"
                                        Width="40%" Text='<%#Eval("agWeightage") %>' />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="Label2" Text='<%#Eval("agEvaluation") %>' />
                                    <asp:TextBox runat="server" ID="TxtEvaluation" CssClass="Textbox Evaluation validation"
                                        Width="40%" AutoPostBack="true" Visible="false" Text='<%#Eval("agEvaluation") %>' />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="LblScore1" Text='<%#Eval("agScore") %>' />
                                    <asp:TextBox runat="server" ID="TxtScore" Visible="false" CssClass="Textbox Score validation"
                                        Width="40%" Text='<%#Eval("agScore") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <%-- <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click"
                                CommandArgument='<%#Eval("ID") %>' />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up td_bottom" style="vertical-align: top;">
                                </td>
                                <td colspan="7" class="td_lines Grid1 " align="left">
                                    <asp:Label Text="Description:  " runat="server" ID="lblDescription" CssClass="comments1" />
                                    <asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("agGoalDescription") %>' />
                                    <asp:TextBox runat="server" Visible="false" ID="txtDescription" Text='<%#Eval("agGoalDescription") %>'
                                        TextMode="MultiLine" Columns="71" Width="99%" CssClass="MLTextbox reqtxtDescription" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="" style="vertical-align: top;">
                                </td>
                                <td colspan="7" class="td_lines Grid1 " align="left">
                                    <asp:Label Text="Appraisee:" ID="lblAppraiseeComments" CssClass="comments1" runat="server" />
                                    <asp:Label runat="server" ID="lblAppraiseeComments1" Text='<%#Eval("agAppraiseeLatestComments") %>'
                                        CssClass="comments1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up" style="vertical-align: top;">
                                </td>
                                <td colspan="7" class="td_lines Grid1 " align="left">
                                    <asp:Label ID="LblCmts" Text="Appraiser Comments" runat="server" CssClass="comments1" />
                                    <asp:Label runat="server" ID="LblApprcmts" Text='<%#Eval("agAppraiserLatestComments") %>' />
                                    <asp:TextBox runat="server" Visible="false" ID="TxtAprComments" TextMode="MultiLine"
                                        Width="99%" CssClass="MLTextbox  commentstextboxwidth" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- <tr>
                <td colspan="8" align="right" class="addbtn td_lines">
                    <asp:Button Text="Add" ID="btnAdd" OnClick="BtnAdd_Click" CssClass="Button" runat="server" />
                </td>
            </tr>--%>
                    <tr>
                        <td colspan="4" class="td_lines td_bottom Grid1" align="right">
                            <asp:Label Text="Weight" ID="lblW" runat="server" />
                        </td>
                        <td align="center" class="td_lines">
                            <asp:Label Text="100%" ID="lblTotalWeightage" runat="server" />
                        </td>
                        <td class="td_lines td_bottom Grid1" align="right">
                            <asp:Label Text="H2Score" ID="lblS" runat="server" />
                        </td>
                        <td align="center" class="td_lines" colspan="2">
                            <asp:Label ID="lblH2Score" runat="server" />
                        </td>
                        <%--<td class="td_lines td_bottom">
                </td>--%>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <%--<td  align="center" class="td_lines Grid1"></td>--%>
                        <td align="right" colspan="6" class="td_lines Grid1">
                            <asp:Label ID="LblH1score" runat="server"></asp:Label>
                        </td>
                        <td align="center" class="td_lines" colspan="2">
                            <asp:Label ID="LblH1score1" Text='<%#Eval("agScore") %>' runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="6" class="td_lines Grid1">
                            <asp:Label ID="LblFA" Text="Final Score" runat="server"></asp:Label>
                        </td>
                        <td align="center" class="td_lines" colspan="2">
                            <asp:Label ID="LblFS" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="6" class="td_lines Grid1">
                            <asp:Label ID="LblfinalRating" Text="Final Rating" runat="server"></asp:Label>
                        </td>
                        <td align="center" class="td_lines" colspan="2">
                            <asp:Label ID="Lblfr" runat="server" />
                        </td>
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
                                    <asp:Label Text="Rating" runat="server" ID="Lblrating" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Grid1 serial_numbers">
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblItemId" />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("acmptCompetency") %>' Font-Bold="true" runat="server" ID="lblcompetencies" />
                                    <br />
                                    <asp:Label Text="Description:" runat="server" ID="lblDescription" />&nbsp;&nbsp;
                                    <asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("acmptDescription") .ToString().Replace(Environment.NewLine,"<br />")%>' />
                                </td>
                                <td class="td_lines rating1" align="center">
                                    <asp:Label runat="server" ID="lblexectedresult" Text='<%#Eval("acmptExpectedResult") %>' />
                                    <asp:DropDownList runat="server" Visible="false" ID="ddlExpectedResult">
                                        <asp:ListItem Text="Select" />
                                        <asp:ListItem Text="Good" />
                                        <asp:ListItem Text="Fair" />
                                        <asp:ListItem Text="Poor" />
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines rating1" align="center">
                                    <asp:Label runat="server" ID="lblrating" Text='<%#Eval("acmptRating") %>' />
                                    <asp:DropDownList runat="server" Visible="false" ID="ddlRating">
                                        <asp:ListItem Text="Select" />
                                        <asp:ListItem Text="Good" />
                                        <asp:ListItem Text="Fair" />
                                        <asp:ListItem Text="Poor" />
                                    </asp:DropDownList>
                                </td>
                                <tr>
                                    <td align="center" class="td_lines td_up td_bottom" style="vertical-align: top;">
                                    </td>
                                    <td class="td_lines Grid1" colspan="4">
                                        <asp:Label Text="Appraisee:       " ID="lblAppraiseeComments" runat="server" />&nbsp;&nbsp;
                                        <asp:Label ID="lblAppraise" Text='<%#Eval("acmptAppraiseeLatestComments") %>' runat="server"></asp:Label>
                                        <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                                    </td>
                                </tr>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines" style="vertical-align: top;">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Appraiser Comments" ID="LblComments" runat="server" />
                                    <asp:Label ID="LblAppraisercmts" Text='<%#Eval("acmptAppraiserLatestComments") %>'
                                        runat="server"></asp:Label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" Visible="false" ID="TxtAppraisercmts"
                                        Text='<%#Eval("acmptAppraiserLatestComments") %>' CssClass="MLTextbox" Width="99%" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%-- <tr>
                <td class="td_lines Grid1" colspan="6" align="right">
                    <asp:Button Text="Save" ID="btncmptSave" runat="server" OnClick="BtnCompetency_Click" CssClass="Button" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Approve" ID="btncmptApprove" runat="server" OnClick="BtnApprove_Click"
                        CssClass="Button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Cancel" ID="btncmptCancel" runat="server" OnClick="BtnCancel_Click"
                        CssClass="Button" />
                </td>
            </tr>--%>
                </table>
            </div>
            <div id="divdevelopmentmessures" runat="server">
                <asp:HiddenField ID="HfDevelopmentMesure" Value="5" runat="server" />
                <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
                    border-collapse: collapse;" class="Grid">
                    <asp:Repeater ID="RptDevelopmentMesure" runat="server">
                        <HeaderTemplate>
                            <tr class="GridHead">
                                <td align="center" class="td_lines" style="width: 5%;">
                                    <%-- <%# Container.DataItemIndex + 1 %>--%>
                                    <asp:Label Text="S.No" runat="server" ID="lblSno" />
                                </td>
                                <td align="center" class="td_lines when" style="width: 15%;">
                                    <asp:Label Text="When" runat="server" ID="lblwhen" />
                                </td>
                                <td align="center" class="td_lines what" style="width: 30%;">
                                    <asp:Label Text="What" runat="server" ID="lblwhat" />
                                </td>
                                <td align="center" class="td_lines nextstep" style="width: 40%; height: 30%">
                                    <asp:Label Text="Next Steps" runat="server" ID="lblNextSteps" />
                                </td>
                                <td align="center" class="td_lines actiontd" style="width: 10%;">
                                    <asp:Label Text="Actions" runat="server" ID="lblActions" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="td_lines td_up td_bottom Grid1">
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="LblDeId" />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label ID="Label1" Text='<%#Eval("pdpWhen") %>' runat="server" />
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" Visible="false" ID="SPDateLastDate" CssClassTextBox="datecss validation When"
                                        DateOnly="true" />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("pdpWhat") %>' runat="server" ID="lblwhat" />
                                    <asp:TextBox ID="txtwhat" runat="server" Visible="false" Width="99%" Rows="2" Text='<%#Eval("pdpWhat") %>'
                                        TextMode="MultiLine" CssClass="MLTextbox validation What"></asp:TextBox>
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:TextBox ID="txtNextSteps" runat="server" Visible="false" Width="99%" Rows="2"
                                        Text='<%#Eval("pdpNextSteps") %>' TextMode="MultiLine" CssClass="MLTextbox validation NextSteps"></asp:TextBox>
                                    <asp:Label Text='<%#Eval("pdpNextSteps") %>' runat="server" ID="lblNextSteps" />
                                </td>
                                <td class="td_lines Grid1">
                                    <%-- <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click1"
                                CommandArgument='<%#Eval("ID") %>' />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up td_bottom" style="vertical-align: top;">
                                </td>
                                <td class="td_lines Grid1" colspan="4">
                                    <asp:Label Text="Appraisee: " ID="lblAppraiseeComments" runat="server" />
                                    <asp:Label ID="lblAppraiseDev" Text='<%#Eval("pdpH2AppraiseeComments") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up" style="vertical-align: top;">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Comments " ID="LblDemcomments" runat="server" />
                                    <asp:Label ID="LbldevAppraiser" Text='<%#Eval("pdpH2AppraiserComments") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div runat="server" id="pipdiv">
                <asp:HiddenField ID="piphfAppraiselid" Value="2" runat="server" />
                <asp:HiddenField ID="pipAppraisalPhaseID" Value="2" runat="server" />
                <asp:HiddenField ID="piphidenfield2" Value="2" runat="server" />
                <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
                    border-collapse: collapse;" class="Grid">
                    <asp:Repeater ID="rptpip" runat="server">
                        <HeaderTemplate>
                            <tr class="GridHead">
                                <td align="center" class="td_lines " style="">
                                    <asp:Label Text="SNo" runat="server" ID="lblSno" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Performance Issue" runat="server" ID="lblcompetencies" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Expected Achivements" runat="server" ID="lblexpectedresult" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Time Frame" runat="server" ID="lblrating" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Actual Result (Mid Term)" runat="server" ID="Label10" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Appraisers Assessment(mid term)" runat="server" ID="Label11" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Actual Result (final)" runat="server" ID="Label12" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Appraisers Assessment (final)" runat="server" ID="Label14" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Action" runat="server" ID="Label15" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="td_lines Grid1 serial_numbers " style="">
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" ID="lblID" Visible="false" />
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipPerformanceIssue") %>' CssClass="Grid1" ID="lblPerformanceIssu"
                                        runat="server" Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipPerformanceIssue") %>' ID="txtPerformanceIssu"
                                        runat="server" Visible="false" Width="99%" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipExpectedachivement") %>' CssClass="Grid1" ID="lblExpectedAchivements"
                                        runat="server" Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipExpectedachivement") %>' ID="txtExpectedAchivements"
                                        runat="server" Visible="false" Width="99%" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipTimeFrame") %>' ID="lblTimeFrame" CssClass="Grid1" runat="server"
                                        Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipTimeFrame") %>' ID="txtTimeFrame"
                                        runat="server" Visible="false" Width="99%" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipMidTermActualResult") %>' ID="lblActualResultmidterm"
                                        runat="server" Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipMidTermActualResult") %>' ID="txtActualResultmidterm"
                                        runat="server" Width="99%" Visible="false" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipMidTermAppraisersAssessment") %>' ID="lblAppraisersAssessmentmidterm"
                                        runat="server" Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipMidTermAppraisersAssessment") %>'
                                        ID="txtAppraisersAssessmentmidterm" runat="server" Visible="false" Width="99%" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipFinalAcutualResult") %>' ID="lblActualResultfinelterm"
                                        runat="server" Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipFinalAcutualResult") %>' ID="txtActualResultfinelterm"
                                        runat="server" Visible="false" Width="99%" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipFinalAppraisersAssesment") %>' ID="lblAppraisersAssessmentfinalterm"
                                        runat="server" Visible="false" />
                                    <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipFinalAppraisersAssesment") %>'
                                        ID="txtAppraisersAssessmentfinalterm" runat="server" Visible="false" Width="99%" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" OnClick="lnkDelete_Click"
                                        CommandArgument='<%#Eval("SNo") %>' runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td align="right" class="td_lines " colspan="9">
                            <asp:Button Text="Add" ID="btnadd" OnClick="btnAdd_Click" runat="server" CssClass="Button" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="td_lines " colspan="9">
                            <asp:Button Text="Submit" ID="lnksave" OnClick="lnkSave_Click" runat="server" CssClass="Button"
                                CommandArgument='<%#Eval("SNo") %>' />
                            <%--<asp:Button Text="Submit" ID="lnkapprove" OnClick="lnkapprove_Click" runat="server"
                        CssClass="Button" />--%>
                            <asp:Button Text="Cancel" ID="Button10" OnClick="btn_Cancel" runat="server" CssClass="Button" />
                        </td>
                    </tr>
                </table>
            </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="8" align="right" class="td_lines td_bottom">
                 <asp:Button Text="Print" ID="btnPrint" CssClass="Button"  runat="server" OnClientClick="printCert();return false;" />
                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" OnClick="BtnCancel_Click"
                        CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Application Page
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    My Application Page
</asp:Content>
