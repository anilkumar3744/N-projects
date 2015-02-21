<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewerEvaluation.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.VFSProjectH1.ReviewerEvaluation" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink Name="~sitecollection/SiteAssets/jquery.js" ID="ScriptLink1"
        runat="server" Defer="false">
    </SharePoint:ScriptLink>
    <%--<link href="/_layouts/STYLES/VFSCss.css" rel="stylesheet" type="text/css" />--%>
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
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript" src="/_layouts/Scripts/jquery-1.9.0.min.js"> </script>
    <script type="text/javascript">

        $(document).ready(function () {
            //$(".datecss").attr('readonly', 'readonly');
        });

        function confirmation() {
            if (confirm("Are you sure you want to delete?")) return true;
            else return false;
        }

        function Validate() {
            var flag1 = 0, flag2 = 0, flag3 = 0;
            var totalSum = 0, row = 0;
            $('.condtional_box').each(function (i, obj) {

                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });
            //alert($('.validation').length);
            $('.validation').each(function (i, obj) {
               
                    row = parseInt(i / 7) + 1;
                    if (obj.value.trim() == '') {

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
                    else if ($(this).hasClass("reqReviewerLatestComments")) {
                        alert('Please specify Comments for the Goal at row: ' + row);
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
                $('.competeniciesValidations').each(function (i, obj) {
                      row = parseInt(i / 3) + 1;
                    if ($(this).prop("selectedIndex") == 0) {
                        
                        if ($(this).hasClass("Rating")) {
                            alert('Please select Rating for the Competency at row: ' + row);
                            $(this).focus();
                            flag2++;
                            return false;
                        }
                        else if ($(this).hasClass("Result")) {
                            alert('Please select Expected Result for the Competency at row: ' + row);
                            $(this).focus();
                            flag2++;
                            return false;
                        }
                    }

                    else if ($(this).hasClass("competeniciescomments") && obj.value.trim() == '') {

                       alert('Please specify Comments for the Competency at row: ' + row);
                        $(this).focus();
                        flag2++;
                        return false;
                      }

                });

               

            }
            else {
                $("#ctl00_PlaceHolderMain_dvCompetencies").hide();
                $("#ctl00_PlaceHolderMain_saftymeasurementdevelopment").hide();
                $("#ctl00_PlaceHolderMain_DvREvaluation").show();
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnljobdetails_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").addClass("ajax__tab_active");
                return false;

            }

            if (flag2 != 0) {
                $("#ctl00_PlaceHolderMain_DvREvaluation").hide();
                $("#ctl00_PlaceHolderMain_saftymeasurementdevelopment").hide();
                $("#ctl00_PlaceHolderMain_dvCompetencies").show();
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnluser_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnljobdetails_tab").removeClass("ajax__tab_active");
                $("#ctl00_PlaceHolderMain_TabContainer1_tbpnlusrdetails_tab").addClass("ajax__tab_active");
                return false;


            }
            var maxVal = 100;
            if (flag3 == 0) {

                if (parseInt(maxVal) != parseInt(totalSum)) {

                    alert('The sum of all weights should be 100%');
                    return false;
                }
            }
        }
        
    </script>
    <script type="text/javascript">
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

//        $(document).ready(function () {
//            $("body").delegate(".condtional_box", "keyup", function (e) {

//                var str = $(this).val();

//                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
//                    if (e.which < 96 || e.which > 105) {
//                        $(this).val(str.substring(0, str.length - 1));
//                        return false;
//                    }
//                }
//            });

        //        });
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
            return true;
        }
    </script>
    <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
    <asp:HiddenField ID="hfflag" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" />
    <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class=""
                    style="padding: 0px;">
                    <tr>
                        <td align="center" style="font-size: large;" colspan="2">
                            <asp:Label Text=" " ID="lblselfevaluation" runat="server" />
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
                            <%--</h3>--%>
                        </td>
                        <td align="right" style="padding-right: 4px;">
                           <%-- <h3 style="margin-top: 3px;">--%>
                                <asp:Label runat="server" Text="Workflow State:" ID="lblStatus" class="TableHeader_1"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblStatusValue" class="TableHeader_1"></asp:Label>
                                 <asp:Label runat="server" ID="lblReValue" class="TableHeader_1"></asp:Label>
                            <%--</h3>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
            </div>
            <div>
                <table cellpadding="7px" cellspacing="0px" width="100%" class="" style="">
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
                            <asp:Label runat="server" Text="" ID="ex"></asp:Label>
                        </td>
                        <td class="FieldCell FieldCell2">
                            <asp:Label runat="server" ID="ex1" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
            <div class="tabs">
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
                    <asp:TabPanel ID="tbpnlpip" runat="server">
                        <HeaderTemplate>
                            PIP
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Panel3" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:TabPanel>
                </asp:TabContainer>
            </div>
            <div runat="server" id="DvREvaluation">
                <asp:HiddenField ID="hfGoalsCount" Value="5" runat="server" />
                <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
                    border-collapse: collapse; padding-right: 5px" class="Grid">
                    <asp:Repeater ID="RptRevaluation" runat="server" >
                        <HeaderTemplate>
                            <tr class="GridHead">
                                <td align="center" class="td_lines" style="width: 5%;">
                                    <asp:Label Text="S.No" runat="server" ID="lblSno" />
                                </td>
                                <td align="center" class="td_lines" style="width: 20%;">
                                    <asp:Label Text="Category" runat="server" ID="lblCategory" />
                                </td>
                                <td align="center" class="td_lines" style="width: 35%;">
                                    <asp:Label Text="Goal" runat="server" ID="lblGoal" />
                                </td>
                                <td align="center" class="td_lines" style="width: 14%;">
                                    <asp:Label Text="Due Date" runat="server" ID="lblDueDate" />
                                </td>
                                <td align="center" class="td_lines" style="width: 3%;">
                                    <asp:Label Text="Weightage (%)" runat="server" ID="lblWeightage" />
                                </td>
                                <td align="center" class="td_lines" style="width: 3%;">
                                    <asp:Label Text="Evaluation (%)" runat="server" ID="lblEvaluation" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Score (%)" runat="server" ID="lblScore1" />
                                </td>
                                <td align="center" class="td_lines" style="width: 10%;">
                                    <asp:Label Text="Action" runat="server" ID="lblAction" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="td_lines td_bottom Grid1 serial_numbers" valign="top">
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno1" />
                                    <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="lblId" />
                                    <asp:Panel ID="testpnl" runat="server" style="display:none"/>
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("agGoalCategory") %>' runat="server" ID="lblCategory" />
                                    <asp:DropDownList ID="ddlCategories" runat="server" Visible="false" CssClass="category validation">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines Grid1" align="left">
                                    <asp:Label runat="server" ID="lblGoal" Visible="false" Text='<%#Eval("agGoal") %>' />
                                    <asp:TextBox runat="server" ID="txtGoal" Visible="true" CssClass="Textbox reqtxtGoal validation"
                                        Text='<%#Eval("agGoal") %>' Width="99%" />
                                </td>
                                <td class="td_lines Grid1">
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" CssClassTextBox="datecss reqSPDateLastDate validation"
                                        DateOnly="true" />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="lblWeightage" Text='<%#Eval("agWeightage") %>' Visible="false" />
                                    <asp:TextBox runat="server" ID="txtWeightage" Visible="true" AutoPostBack="true"
                                        OnTextChanged="txtWeightage_TextChanged" CssClass="Textbox condtional_box validation"
                                        Text='<%#Eval("agWeightage") %>' Width="50%" MaxLength="6" onkeypress="return isNumberKeyW(event)" />
                                </td>
                                <td class="td_lines" style="" align="center">
                                    <asp:Label runat="server" ID="lblEvaluation" Visible="false" Text="" />
                                    <asp:TextBox runat="server" ID="txtEvaluation" CssClass="Textbox Evaluation validation"
                                        AutoPostBack="true" OnTextChanged="txtWeightage_TextChanged" Text='<%#Eval("agEvaluation") %>'
                                        Width="50%" onkeypress="return isNumberKey(event)" MaxLength="6" />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="lblScore1" Visible="true" Text='<%#Eval("agScore") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click"
                                        OnClientClick="return confirmation()" CommandArgument='<%#Eval("SNo") %>'  />
                                </td>
                            </tr>
                            <tr>
                                <td class="td_lines td_bottom td_up">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Description:" runat="server" ID="lblDescription" />
                                    <%--<asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("agGoalDescription") %>' />--%>
                                    <asp:TextBox runat="server" ID="txtDescription" Text='<%#Eval("agGoalDescription") %>'
                                        TextMode="MultiLine" Columns="71" Width="99%" CssClass="MLTextbox reqtxtDescription validation "
                                        Visible="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Appraisee:" ID="lblAppraiseeComments" runat="server" />
                                    <asp:Label ID="lblAppraise" Text='<%#Eval("agAppraiseeLatestComments")%>' runat="server"></asp:Label>
                                    <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="td_lines td_up td_bottom">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Appraiser:" ID="lblAppraiser" runat="server" />
                                    <asp:Label ID="lblAppraiserComments" Text='<%#Eval("agAppraiserLatestComments")%>'
                                        runat="server"></asp:Label>
                                    <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                                </td>
                            </tr>
                            <%--<tr>
                    <td class="td_lines td_up td_bottom"></td>
                        <td class="td_lines Grid1"  colspan="7">
                            <asp:Label Text="Reviewer:" ID="Label16" runat="server" />
                        
                            <asp:Label ID="Label17" Text="Reviewer Comments" runat="server"></asp:Label>
                            <asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />
                        </td>
                    </tr>
                    <tr>
                    <td class="td_lines td_up td_bottom"></td>
                        <td class="td_lines Grid1"  colspan="7">
                            <asp:Label Text="Appraiser:" ID="Label14" runat="server" />
                        
                            <asp:Label ID="Label15" Text="Appraiser Appeal Comments" runat="server"></asp:Label>
                            <asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />
                        </td>
                    </tr>--%>
                            <tr>
                                <td class="td_lines td_up">
                                </td>
                                <td class="td_lines Grid1" colspan="7">
                                    <asp:Label Text="Comments" ID="Label3" runat="server" />
                                    <asp:TextBox runat="server" ID="txtComments" Text='<%#Eval("agReviewerLatestComments")%>'
                                        TextMode="MultiLine" Width="99%" CssClass="MLTextbox " /><%--validation reqReviewerLatestComments--%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="8" align="right" class="addbtn">
                            <asp:Button Text="Add" ID="btnAdd" CssClass="Button" runat="server" OnClick="BtnAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="td_lines td_bottom" align="right">
                            <asp:Label Text="Weight" ID="lblW" runat="server" />
                        </td>
                        <td align="center" class="td_lines Grid1">
                            <asp:Label Text="100%" ID="lblTotalWeightage" runat="server" />
                        </td>
                        <td class="td_lines td_bottom " align="right">
                            <asp:Label Text="Score" ID="lblS" runat="server" />
                        </td>
                        <td align="center" class="td_lines Grid1">
                            <asp:Label ID="lblScoretotal" runat="server" />
                            <asp:Label ID="lblfr" runat="server" Visible="false" />
                        </td>
                        <td class="td_lines td_bottom">
                        </td>
                    </tr>
                   <%-- <tr>
                        <td align="right" colspan="6" class="td_lines">
                            <asp:Label ID="lblR" Text="Rating" runat="server"></asp:Label>
                        </td>
                        <td align="center" class="td_lines Grid1">
                            <asp:Label ID="lblfr" runat="server" />
                        </td>
                        <td class="td_lines">
                        </td>
                    </tr>--%>
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
                                    <asp:Label Text="Rating" runat="server" ID="Label4" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Grid1 serial_numbers" align="center">
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                    <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="lblItemId" />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("acmptCompetency") %>' Font-Bold="true" runat="server" ID="lblcompetencies" />
                                    <br />
                                    <asp:Label Text="Description:     " runat="server" ID="lblDescription" />&nbsp;&nbsp;
                                    <asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("acmptDescription") .ToString().Replace(Environment.NewLine,"<br />")%>' />
                                </td>
                                <td class="td_lines serial_numbers" align="center">
                                    <asp:Label runat="server" ID="lblexectedresult" Visible="false" />
                                    <asp:DropDownList runat="server" ID="ddlExpectedResult" CssClass="competeniciesValidations Result">
                                        <asp:ListItem Text="Select" />
                                        <asp:ListItem Text="Good" />
                                        <asp:ListItem Text="Fair" />
                                        <asp:ListItem Text="Poor" />
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines serial_numbers" align="center">
                                    <asp:Label runat="server" ID="lblrating" Visible="false" />
                                    <asp:DropDownList runat="server" ID="ddlrating" CssClass="competeniciesValidations Rating"
                                        AutoPostBack="true" > <%--OnSelectedIndexChanged="ddlrating_SelectedIndexChanged"--%>
                                        <asp:ListItem Text="Select" />
                                        <asp:ListItem Text="Good" />
                                        <asp:ListItem Text="Fair" />
                                        <asp:ListItem Text="Poor" />
                                    </asp:DropDownList>
                                </td>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="td_lines Grid1" colspan="4">
                                        <asp:Label Text="Appraisee:       " ID="lblAppraiseeComments" runat="server" />&nbsp;&nbsp;
                                        <asp:Label ID="lblAppraise" Text='<%#Eval("acmptAppraiseeLatestComments")%>' runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="td_lines Grid1" colspan="4">
                                        <asp:Label Text="Appraiser:       " ID="Label1" runat="server" />&nbsp;&nbsp;
                                        <asp:Label ID="lblAppraiser" Text='<%#Eval("acmptAppraiserLatestComments")%>' runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_lines td_up">
                                    </td>
                                    <td class="td_lines Grid1" colspan="7">
                                        <asp:Label Text="Comments" ID="LblComments" runat="server" />
                                        <asp:TextBox runat="server" TextMode="MultiLine" ID="txtComments" Text='<%#Eval("acmptReviewerLatestComments")%>'
                                            CssClass="MLTextbox " Width="99%" /> <%--competeniciesValidations competeniciescomments--%>
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
                                    <asp:Label Text="When" runat="server" ID="lblwhen" />
                                </td>
                                <td align="center" class="td_lines" style="width: 45%">
                                    <asp:Label Text="What" runat="server" ID="lblwhat" />
                                </td>
                                <td align="center" class="td_lines" style="width: 40%">
                                    <asp:Label Text="Next Step" runat="server" ID="lblnextstep" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="td_lines Grid1 td_bottom verticalalign">
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                    <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="LblDeId" />
                                </td>
                                <td align="center" class="td_lines Grid1">
                                    <asp:Label runat="server" ID="lblWhen" Visible="true" Text='<%# Convert.ToDateTime(Eval("pdpWhen")).ToString("dd-MMM-yyyy")%>' />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label runat="server" ID="lblWhat" Visible="true" Text='<%#Eval("pdpWhat") %>' />
                                </td>
                                <td align="left" class="td_lines Grid1">
                                    <asp:Label runat="server" ID="lblNextSteps" Visible="true" Text='<%#Eval("pdpNextSteps") %>' />
                                    <%--<asp:TextBox runat="server" ID="nextstep" TextMode="MultiLine" Width="99%" Text='<%#Eval("pdpNextSteps") %>'/>--%>
                                </td>
                                <%--<td align="left" class="td_lines Grid1">
                             <asp:LinkButton Text="Edit" ID="lnkedit" runat="server" />|
                            <asp:LinkButton Text="Delete" ID="lnkPDPDelete" runat="server" Visible="false" OnClick="lnkPDPDelete_Click" CommandArgument='<%#Eval("sNo") %>' />
                        </td>--%>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="td_lines Grid1" colspan="3">
                                    <asp:Label Text="Appraisee:       " ID="lblAppraiseeComments" runat="server" />&nbsp;&nbsp;
                                    <asp:Label ID="lblAppraise" Text='<%#Eval("pdpH1AppraiseeComments") %>' runat="server"></asp:Label>
                                    <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td class="td_lines Grid1" colspan="3">
                                    <asp:Label Text="Appraiser:       " ID="Label1" runat="server" />&nbsp;&nbsp;
                                    <asp:Label ID="lblAppraiser" Text='<%#Eval("pdpH1AppraiserComments") %>' runat="server"></asp:Label>
                                    <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
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
                                    <asp:Label Text="Expected Achievement" runat="server" ID="lblexpectedresult" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Time Frame" runat="server" ID="lblrating" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Actual Result  (Mid Term)" runat="server" ID="Label10" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Appraiser Assessment (Mid Term)" runat="server" ID="Label11" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Actual Result (Final)" runat="server" ID="Label12" />
                                </td>
                                <td align="center" class="td_lines" style="">
                                    <asp:Label Text="Appraiser Assessment  (Final)" runat="server" ID="Label14" />
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td align="center" class="td_lines Grid1 serial_numbers " style="">
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" ID="lblIDP" Visible="false" />
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipPerformanceIssue") %>' CssClass="Grid1" ID="lblPerformanceIssu"
                                        runat="server" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipExpectedachivement") %>' CssClass="Grid1" ID="lblExpectedAchivements"
                                        runat="server" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipTimeFrame") %>' ID="lblTimeFrame" CssClass="Grid1" runat="server" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipMidTermActualResult") %>' ID="lblActualResultmidterm"
                                        runat="server" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipMidTermAppraisersAssessment") %>' ID="lblAppraisersAssessmentmidterm"
                                        runat="server" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipFinalAcutualResult") %>' ID="lblActualResultfinelterm"
                                        runat="server" />
                                </td>
                                <td align="center" class="td_lines Grid1" style="">
                                    <asp:Label Text='<%#Eval("pipFinalAppraisersAssesment") %>' ID="lblAppraisersAssessmentfinalterm"
                                        runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse; padding-right: 5px">
            <tr>
                <td class="td_lines Grid1" colspan="6" align="right">
                    <asp:Button Text="Print" ID="btnPrint" runat="server" OnClientClick="printCert();return false;"
                        CssClass="Button" />
                    <asp:Button Text="Save" ID="Button3" runat="server" OnClick="BtnSave_Click" CssClass="Button" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Approve" ID="Button4" runat="server" OnClick="BtnApprove_Click"
                        CssClass="Button" OnClientClick="return Validate()" />&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Cancel" ID="Button5" runat="server" OnClick="BtnCancel_Click" CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Reviewer Evaluation
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Reviewer Evaluation
</asp:Content>
