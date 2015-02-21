<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H2AppraiserEvaluation.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.H2AppraiserEvaluation.H2AppraiserEvaluation"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">

 <SharePoint:ScriptLink Name="~sitecollection/SiteAssets/jquery.js" 
                           ID="ScriptLink1"
                           runat="server"
                           Defer="false"
                           ></SharePoint:ScriptLink>
<script type="text/javascript">

//    $(document).ready(function () {
//        $("body").delegate(".condtional_box", "keyup", function (e) {

//            var str = $(this).val();

//            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
//                if (e.which < 96 || e.which > 105) {
//                    $(this).val(str.substring(0, str.length - 1));
//                    return false;
//                }
//            }
//        });

        //        $("body").delegate(".Evaluation", "keyup", function (e) {

        //            var str = $(this).val();

        //            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //                if (e.which < 96 || e.which > 105) {
        //                    $(this).val(str.substring(0, str.length - 1));
        //                    return false;
        //                }
        //            }
        //        });

    //});
    function isNumberKeyW(evt) {
        var index;
        var key = evt.which || event.keyCode;
        if (!(48 <= key && key <= 57)) {
            return false;
        }

        return true;
    }
    function isNumberKey(evt) {
        var index;
        var key = evt.which || event.keyCode;
        if (!(48 <= key && key <= 57)) {
            if (!(key == 45 || key == 46)) {
                return false;
            }
        }
    }
</script>
    <script type="text/javascript">

        //User Defined Function to Open Dialog Framework
        function OpenDialog(strPageURL) {
            var dialogOptions = SP.UI.$create_DialogOptions();
            dialogOptions.url = '<%=message%>'; // URL of the Page
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
  <%--  <link href="/_layouts/STYLES/VFSCss.css" rel="stylesheet" type="text/css" />--%>
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
            width: 219px;
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
            width: 150px;
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
            height: 20px;
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
        .tittle_td
        {
            color: #00358F;
            font-size: 13px;
            font-weight: bold;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
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
</asp:content>
<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <script type="text/javascript" src="/_layouts/Scripts/jquery-1.9.0.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$(".datecss").attr('readonly', 'readonly');
            $(".condtional_box").keypress(function (event) {
                var key = event.which || event.keyCode;
                if (!(48 <= key && key <= 57)) {
                    event.preventDefault();
                    return false;
                }
            });

        });
       

        function Validate() {
            var flag1 = 0, flag2 = 0, flag3 = 0;
            var totalSum = 0, row = 0;


            $('.condtional_box').each(function (i, obj) {

                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });

           // alert($('.validation').length);
            $('.validation').each(function (i, obj) {
                row = parseInt(i / 7) + 1;
                //alert(obj.value);
                if (obj.value.trim() == '') {
                    // row = parseInt(i / 6) + 1;
                   

                    if ($(this).hasClass("reqtxtGoal")) {
                        alert('Please specify Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }
                    else if ($(this).hasClass("reqSPDateLastDate")) {
                        alert('Please specify due date for the Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }
                    else if ($(this).hasClass("reqtxtDescription")) {
                        alert('Please specify Description for the Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }
                    else if ($(this).hasClass("condtional_box")) {
                        alert('Please specify Weightage for the Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }
                    else if ($(this).hasClass("Evaluation")) {
                        alert('Please specify Evaluation for the Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }

                    else if ($(this).hasClass("reqAppraiseeLatestComments")) {
                        alert('Please specify Comments for the Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }

                    else if ($(this).hasClass("Score")) {
                        alert('Please specify Score for the Goal at row: ' + row);
                        $(this).focus();
                        flag1++;
                        return false;
                    }

                }

                else if ($(this).hasClass("category") && $(this).prop("selectedIndex") == 0) {
                    // row = parseInt(i / 5) + 1;
                    alert('Please select Category for the Goal at row: ' + row);
                    $(this).focus();
                    flag1++;
                    return false;
                }

            });

            if (flag1 == 0) {

                $('.ExpectedResult').each(function (i, obj) {

                    row = parseInt(i / 3) + 1;
                    if ($(this).prop("selectedIndex") == 0) {
                        if ($(this).hasClass("Result")) {
                            alert('Please select Expected result for the Competency at row: ' + row);
                            $(this).focus();
                            flag2++;
                            return false;
                        }
                        else if ($(this).hasClass("Rating")) {
                            alert('Please select Rating for the Competency at row: ' + row);
                            $(this).focus();
                            flag2++;
                            return false;
                        }
                    }


                    else if ($(this).hasClass("AppraiseeLatestComments") && obj.value.trim() == '') {

                        alert('Please specify Comments for the Competency at row: ' + row);
                        $(this).focus();
                        flag2++;
                        return false;

                    }


                });
            }
            else {


                $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                $("#ctl00_PlaceHolderMain_divdevelopmentmessures").hide();
                $("#ctl00_PlaceHolderMain_dvGoalSettings").show();
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_TabPanelDev_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").addClass("ajax__tab_active");
                return false;

            }
            if (flag2 == 0) {
                $('.Devmesure').each(function (i, obj) {
                    row = parseInt(i / 4) + 1;
                    if (obj.value.trim() == '') {

                        if ($(this).hasClass("When")) {
                            alert('Please specify When for the Development Measures  at row: ' + row);
                            $(this).focus();
                            flag3++;
                            return false;
                        }
                        else if ($(this).hasClass("What")) {
                            alert('Please specify What for the Development Measures  at row: ' + row);
                            $(this).focus();
                            flag3++;
                            return false;
                        }
                        else if ($(this).hasClass("NextSteps")) {
                            alert('Please specify Next Steps for the Development Measures  at row: ' + row);
                            $(this).focus();
                            flag3++;
                            return false;
                        }
                        else if ($(this).hasClass("AppraiseeLatestComments")) {

                            alert('Please specify Comments for the Development Measures  at row: ' + row);
                            $(this).focus();
                            flag3++;
                            return false;

                        }

                    }

                });
            }

            else {

                $("#ctl00_PlaceHolderMain_dvGoalSettings").hide();

                $("#ctl00_PlaceHolderMain_divdevelopmentmessures").hide();
                $("#ctl00_PlaceHolderMain_dvCompetencies").show();
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_TabPanelDev_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").addClass("ajax__tab_active");
                return false;


            }

            var maxVal = 100;
            if (flag3 == 0) {

                if (parseInt(maxVal) == parseInt(totalSum)) {
                    return true;
                }
                else {
                    alert('The sum of all weights should be 100%');



                    $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                    $("#ctl00_PlaceHolderMain_divdevelopmentmessures").hide();
                    $("#ctl00_PlaceHolderMain_dvGoalSettings").show();
                    $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").removeClass("ajax__tab_active");
                    $("#ctl00_PlaceHolderMain_TabContainer1_TabPanelDev_tab").removeClass("ajax__tab_active");
                    $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").addClass("ajax__tab_active");
                    return false;

                }
            }
            else {

                $("#ctl00_PlaceHolderMain_dvGoalSettings").hide();


                $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                $("#ctl00_PlaceHolderMain_divdevelopmentmessures").show();
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_TabPanelDev_tab").addClass("ajax__tab_active");
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
        function confirmation() {
            if (confirm("Are you sure you want to delete?")) return true;
            else return false;
        }
    </script>
    <asp:HiddenField ID="hfflag" Value="false" runat="server" />
    <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
    <asp:HiddenField ID="HiddenField1" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" />
    <asp:HiddenField ID="appLog" runat="server" />
    <asp:UpdatePanel runat="server" ID="upadate">
    <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnApprove" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnAdd" />
             <asp:PostBackTrigger ControlID="BtnPDPAdd" />

        </Triggers>
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="TableSubHeader"
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
                           <%-- <h3 style="margin-top: 3px; margin-left: 10px;">--%>
                                <asp:Label runat="server" Text="Performance Agreement for:  " ID="lblHeader" class="TableHeader_1"></asp:Label>&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblHeaderValue" class="TableHeader_1"></asp:Label>
                           <%-- </h3>--%>
                        </td>
                        <td align="right">
                            <%--<h3 style="margin-top: 3px;">--%>
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
                            <asp:Label runat="server" Text="H1 Details" ID="ex"></asp:Label>
                        </td>
                        <td class="FieldCell FieldCell2">
                            <asp:LinkButton Text="View H1 Details" ID="lkView" runat="server" OnClientClick="javascript:return OpenDialog();" />
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
                            Development Measures
                            <br />
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Tbpanel" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                    <%--<asp:TabPanel ID="tbpnlpip" runat="server">
                <HeaderTemplate>
                    PIP
                    <br />
                </HeaderTemplate>
                <ContentTemplate>
                <asp:Panel ID="TbpanelPIP" runat="server">
                            </asp:Panel>
                </ContentTemplate>
            </asp:TabPanel>--%>
                </asp:TabContainer>
            </div>
            <div runat="server" id="dvGoalSettings">
                <asp:HiddenField ID="hfGoalsCount" Value="5" runat="server" />
                <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
                    border-collapse: collapse; padding-right: 5px" class="Grid">
                    <asp:Repeater ID="rptGoalSettings" runat="server" >
                        <HeaderTemplate>
                            <tr class="GridHead">
                                <td align="center" class="td_lines " style="width: 5%;">
                                    <asp:Label Text="S.No" runat="server" ID="lblSnoh" />
                                </td>
                                <td align="center" class="td_lines" style="width: 25%;">
                                    <asp:Label Text="Category" runat="server" ID="lblCategoryh" />
                                </td>
                                <td align="center" class="td_lines" style="width: 47%;">
                                    <asp:Label Text="Goal" runat="server" ID="lblGoal" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Due Date" runat="server" ID="lblDueDateh" />
                                </td>
                                <td align="center" class="td_lines" style="width: 3%;">
                                    <asp:Label Text="Weightage(%)" runat="server" ID="lblWeightageh" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Evaluation" runat="server" ID="LblEvaluation" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Score" runat="server" ID="Lblscoreh" />
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
                                    <asp:Panel ID="testpnl" runat="server" style="display:none"/>
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("agGoalCategory") %>' runat="server" ID="lblCategory" />
                                    <asp:DropDownList ID="ddlCategories" runat="server" Visible="false" CssClass="validation category">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines Grid1" align="left">
                                    <asp:TextBox runat="server" ID="txtGoal" Width="99%" CssClass="Textbox reqtxtGoal validation"
                                        Text='<%#Eval("agGoal") %>' />
                                </td>
                                <td class="td_lines Grid1">                                   
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" CssClassTextBox="datecss reqSPDateLastDate validation"
                                        DateOnly="true" />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">                                    
                                    <asp:TextBox runat="server" ID="txtWeightage" AutoPostBack="true" OnTextChanged="txtWeightage_TextChanged"
                                        CssClass="Textbox condtional_box validation" Width="40%" Text='<%#Eval("agWeightage") %>' MaxLength="6" onkeypress="return isNumberKeyW(event)" />
                                    <%--<asp:CompareValidator ID="CompareValidator2" runat="server" Operator="DataTypeCheck"
                                        Type="Double" ControlToValidate="txtWeightage" ErrorMessage="Value must be a whole number"></asp:CompareValidator>--%>
                                </td>
                                <td class="td_lines Grid1" style="" align="center">                                    
                                    <asp:TextBox runat="server" ID="TxtEvaluation" CssClass="Textbox Evaluation validation"
                                        Width="40%" AutoPostBack="true" OnTextChanged="txtWeightage_TextChanged" Text='<%#Eval("agEvaluation") %>' onkeypress="return isNumberKey(event)" MaxLength="6" />
                                    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck"
                                        Type="Double" ControlToValidate="TxtEvaluation" ErrorMessage="Value must be a whole number"></asp:CompareValidator>--%>
                                </td>
                                <td class="td_lines Grid1" style="" align="center">                                    
                                    <asp:Label runat="server" ID="LblScore1" Text='<%#Eval("agScore") %>' />
                                    <asp:TextBox runat="server" ID="TxtScore" Visible="false" CssClass="Textbox " Width="40%"
                                        Text='<%#Eval("agScore") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click" OnClientClick="return confirmation();"
                                        CommandArgument='<%#Eval("ID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up td_bottom" style="vertical-align: top;">
                                </td>
                                <td colspan="7" class="td_lines Grid1 " align="left">
                                    <asp:Label Text="Description:  " runat="server" ID="lblDescription" CssClass="comments1" />
                                    <asp:Label runat="server" ID="lblDescriptionValue" />
                                    <asp:TextBox runat="server" ID="txtDescription" Text='<%#Eval("agGoalDescription") %>'
                                        TextMode="MultiLine" Columns="71" Width="99%" CssClass="MLTextbox reqtxtDescription validation" />
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
                                    <asp:Label ID="LblCmts" Text="Comments" runat="server" CssClass="comments1" />
                                    <asp:TextBox runat="server" ID="TxtAprComments" Text='<%#Eval("agAppraiserLatestComments") %>'
                                        TextMode="MultiLine" Width="99%" CssClass="MLTextbox  commentstextboxwidth validation reqAppraiseeLatestComments" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="8" align="right" class="addbtn td_lines">
                            <asp:Button Text="Add" ID="btnAdd" OnClick="BtnAdd_Click" CssClass="Button" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td_lines td_bottom Grid1" align="right">
                            <asp:Label Text="Weight" ID="lblW" runat="server" />
                        </td>
                        <td align="center" class="td_lines">
                            <asp:Label Text="100%" ID="lblTotalWeightage" runat="server" />
                        </td>
                        <td class="td_lines td_bottom Grid1" align="right">
                            <asp:Label Text="H2Score" ID="Lblscore" runat="server" />
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
                            <asp:Label ID="LblH1score" Text="H1Score" runat="server"></asp:Label>
                        </td>
                        <td align="center" class="td_lines" colspan="2">
                            <asp:Label ID="LblH1score1" runat="server" />
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
                                    <asp:Label Text="S.No" runat="server" ID="lblSnoCH" />
                                </td>
                                <td align="center" class="td_lines competebcytdwidth">
                                    <asp:Label Text="Competency" runat="server" ID="lblcompetencieshCH" />
                                </td>
                                <td align="center" class="td_lines">
                                    <asp:Label Text="Expected Results" runat="server" ID="lblexpectedresultCH" />
                                </td>
                                <td align="center" class="td_lines">
                                    <asp:Label Text="Rating" runat="server" ID="LblratinghCH" />
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
                                    <asp:Label runat="server" ID="lblDescriptionValue1" Text='<%#Eval("acmptDescription") .ToString().Replace(Environment.NewLine,"<br />")%>' />
                                </td>
                                <td class="td_lines rating1" align="center">
                                    <asp:Label runat="server" ID="lblexectedresult" Visible="false" />
                                    <asp:DropDownList runat="server" ID="ddlExpectedResult" CssClass="ExpectedResult Result ">
                                        <asp:ListItem Text="Select" />
                                        <asp:ListItem Text="Good" />
                                        <asp:ListItem Text="Fair" />
                                        <asp:ListItem Text="Poor" />
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines rating1" align="center">
                                    <asp:Label runat="server" ID="lblrating" Visible="false" />
                                    <asp:DropDownList runat="server" ID="ddlRating" CssClass="ExpectedResult Rating">
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
                                        <asp:Label Text="Appraisee:" ID="lblAppraiseeComments" runat="server" />&nbsp;&nbsp;
                                        <asp:Label ID="lblAppraise" Text='<%#Eval("acmptAppraiseeLatestComments") %>' runat="server"></asp:Label>
                                        <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                                    </td>
                                </tr>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines" style="vertical-align: top;">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Comments" ID="LblComments" runat="server" />
                                    <asp:TextBox runat="server" TextMode="MultiLine" Text='<%#Eval("acmptAppraiserLatestComments") %>'
                                        ID="TxtAppraisercmts" CssClass="MLTextbox AppraiseeLatestComments ExpectedResult" Width="99%" />
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
                                    <asp:Label Text="S.No" runat="server" ID="lblSnoDH" />
                                </td>
                                <td align="center" class="td_lines when" style="width: 15%;">
                                    <asp:Label Text="When" runat="server" ID="lblwhenDH" />
                                </td>
                                <td align="center" class="td_lines what" style="width: 30%;">
                                    <asp:Label Text="What" runat="server" ID="lblwhatDH" />
                                </td>
                                <td align="center" class="td_lines nextstep" style="width: 40%; height: 30%">
                                    <asp:Label Text="Next Steps" runat="server" ID="lblNextStepsDH" />
                                </td>
                                <td align="center" class="td_lines actiontd" style="width: 10%;">
                                    <asp:Label Text="Actions" runat="server" ID="lblActionsDH" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="td_lines td_up td_bottom Grid1">
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSnoVal" />
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="LblDeId" />
                                </td>
                                <td align="left" class="td_lines Grid1">                                    
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate1"  CssClassTextBox="datecss Devmesure When"  
                                        DateOnly="true" />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <%--<asp:Label Text="Performance Indicators" runat="server" ID="lblwhat" />--%>
                                    <asp:TextBox ID="txtwhat" runat="server" Width="99%" Rows="2" Text='<%#Eval("pdpWhat") %>'
                                        TextMode="MultiLine" CssClass="MLTextbox Devmesure What"></asp:TextBox>
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:TextBox ID="txtNextSteps" runat="server" Width="99%" Rows="2" Text='<%#Eval("pdpNextSteps") %>'
                                        TextMode="MultiLine" CssClass="MLTextbox Devmesure NextSteps"></asp:TextBox>
                                    <%-- <asp:Label Text=""
                                runat="server" ID="lblNextSteps" />--%>
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click1" OnClientClick="return confirmation()"
                                        CommandArgument='<%#Eval("ID") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up td_bottom" style="vertical-align: top;">
                                </td>
                                <td class="td_lines Grid1" colspan="4">
                                    <asp:Label Text="Appraisee: " ID="lblAppraiseeComments" runat="server" />
                                    <asp:Label ID="lblAppraise" Text='<%#Eval("pdpH2AppraiseeComments") %>' runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="td_lines td_up" style="vertical-align: top;">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Comments " ID="LblDemcomments" runat="server" />
                                    <asp:TextBox runat="server" ID="TxtDevComments" Text='<%#Eval("pdpH2AppraiserComments") %>'
                                        TextMode="MultiLine" Width="99%" CssClass="MLTextbox Devmesure AppraiseeLatestComments" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="7" align="right" class="addbtn ">
                            <asp:Button Text="Add" ID="BtnPDPAdd" OnClick="PDPAdd_Click" CssClass="Button" Width="100"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <%--<div runat="server" id="pipdiv" visible="false">
        <asp:HiddenField ID="piphfAppraiselid" Value="2" runat="server" />
        <asp:HiddenField ID="pipAppraisalPhaseID" Value="2" runat="server" />
        <asp:HiddenField ID="piphidenfield2" Value="2" runat="server" />
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse;" class="TableSubHeader">
            <asp:Repeater ID="rptpip" runat="server">
                <HeaderTemplate>
                    <tr>
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
                            <asp:Label Text="Actual Result (Final)" runat="server" ID="Label12" />
                        </td>
                        <td align="center" class="td_lines" style="">
                            <asp:Label Text="Appraisers Assessment (Final)" runat="server" ID="Label14" />
                        </td>
                        <td align="center" class="td_lines" style="">
                            <asp:Label Text="Action" runat="server" ID="Label15" />
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="td_lines Grid1 serial_numbers " style="">
                            <asp:Label Text='<%#Eval("ID") %>' runat="server" ID="lblID"  Visible="false"/>
                            <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                        </td>
                        <td align="center" class="td_lines Grid1" style="">
                            <asp:Label Text='<%#Eval("pipPerformanceIssue") %>' CssClass="Grid1" ID="lblPerformanceIssu"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipPerformanceIssue") %>' ID="txtPerformanceIssu"
                                runat="server" Width="99%" />
                        </td>
                        <td align="center" class="td_lines Grid1" style="">
                            <asp:Label Text='<%#Eval("pipExpectedachivement") %>' CssClass="Grid1" ID="lblExpectedAchivements"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipExpectedachivement") %>' ID="txtExpectedAchivements"
                                runat="server" Width="99%" />
                        </td>
                        <td align="center" class="td_lines Grid1" style="">
                            <asp:Label Text='<%#Eval("pipTimeFrame") %>' ID="lblTimeFrame" CssClass="Grid1" runat="server"
                                Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipTimeFrame") %>' ID="txtTimeFrame"
                                runat="server" Width="99%" />
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
                            <asp:LinkButton Text="Delete" ID="lnkDelete" OnClick="lnkPIPDelete_Click"
                                CommandArgument='<%#Eval("SNo") %>' runat="server" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td align="right" class="td_lines " colspan="9">
                    <asp:Button Text="Add" ID="btnPIPAdd" OnClick="btnPIPAdd_Click" runat="server" CssClass="Button" />
                </td>
            </tr>
            <tr>
                <td align="right" class="td_lines td_up" colspan="9">
                    <asp:Button Text="Save" ID="lnksave" OnClick="lnkSave_Click" runat="server" CssClass="Button"
                        CommandArgument='<%#Eval("SNo") %>' />
                    <asp:Button Text="Submit" ID="lnkapprove" OnClick="lnkapprove_Click" runat="server"
                        CssClass="Button" />
                    <asp:Button Text="Cancel" ID="btnPIPCancel" OnClick="btnPIPCancel_Click" runat="server" CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>--%>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="8" align="right" class="td_lines td_bottom class="Grid1"" >
                 <asp:Button Text="Print" ID="btnPrint" CssClass="Button"  runat="server" OnClientClick="printCert();return false;"/>
                    <asp:Button Text="Save" ID="btnSave" runat="server" OnClick="BtnSave_Click" 
                        CssClass="Button" />&nbsp;&nbsp;&nbsp;&nbsp;<%--OnClientClick="return Validate()"--%>
                    <asp:Button Text="Submit" ID="btnApprove" runat="server" OnClientClick="return Validate()"
                        OnClick="BtnSubmit_Click" CssClass="Button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" OnClick="BtnCancel_Click"
                        CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
<asp:content id="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
    H2Appraiser Evaluation Page
</asp:content>
<asp:content id="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea"
    runat="server">
    H2Appraiser Evaluation Page
</asp:content>
