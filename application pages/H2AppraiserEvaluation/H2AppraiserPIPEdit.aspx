<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="H2AppraiserPIPEdit.aspx.cs" Inherits="VFS.PMS.ApplicationPages.Layouts.H2AppraiserEvaluation.H2AppraiserPIPEdit" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:content id="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
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
</asp:content>
<asp:content id="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <script type="text/javascript" src="/_layouts/Scripts/jquery-1.9.0.min.js"> </script>
    <script type="text/javascript">

        function Validate() {
            var flag1 = true, flag2 = true, flag3 = true;
            var totalSum = 0;
            $('.condtional_box').each(function (i, obj) {

                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });

            $('.reqtxtDescription').each(function (i, obj) {

                if (obj.value.trim() == '') {
                    //                    $(".reqtxtDescription").removeClass("MLTextbox");
                    //                    $(".reqtxtDescription").addClass("MLTextboxError");                 
                    alert('Description is mandatory');
                    flag1 = false;
                    return false;
                }

            });

            $('.reqtxtGoal').each(function (i, obj) {
                if (obj.value.trim() == '') {
                    alert('Goal is mandatory');
                    flag2 = false;
                    return false;
                }
            });


            $('.reqSPDateLastDate').each(function (i, obj) {
                if (obj.value.trim() == '') {
                    alert('Due date is mandatory');
                    flag3 = false;
                    return false;
                }
            });


            var maxVal = 100;
            if (flag1 && flag2 && flag3) {

                if (parseInt(maxVal) == parseInt(totalSum)) {
                    return true;
                }
                else {
                    alert('Total Weightage must be 100%');
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

        function ValidatePIP() {
            var flag = true;
            $('.PIPMandatory').each(function (i, obj) {
                row = parseInt(i / 3) + 1;
                if (obj.value.trim() == '') {
                    if ($(this).hasClass("performance")) {
                        alert('Please enter Performance Issue at row ' + row);
                        $(this).focus();
                        flag = false;
                        return false;
                    }
                    else if ($(this).hasClass("achivement")) {
                        alert('Please enter Expected Achievement at row ' + row);
                        $(this).focus();
                        flag = false;
                        return false;
                    }
                    else if ($(this).hasClass("TimeFrame")) {
                        alert('Please enter Time Frame at row ' + row);
                        $(this).focus();
                        flag = false;
                        return false;
                    }
                }
            });

            if (flag)
                return true;
            else
                return false;
        }

    </script>
    <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
    <asp:HiddenField ID="hfflag" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalID" Value="false" runat="server" />
    <asp:HiddenField ID="hfAppraisalPhaseID" Value="false" runat="server" />
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class=""
            style="padding: 0px;">
            <tr>
                <td align="center" style="font-size: large;" colspan="2">
                    <asp:Label Text=" PIP " ID="lblselfevaluation" runat="server" />
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
                <td align="right" style="padding-right: 4px;">
                  <%--  <h3 style="margin-top: 3px;">--%>
                        <asp:Label runat="server" Text="Workflow State:" ID="lblStatus" class="TableHeader_1"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblStatusValue" Text="H1 – Completed" class="TableHeader_1"></asp:Label>
                  <%--  </h3>--%>
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
                    <asp:Label runat="server"  ID="lblempcodevalue"></asp:Label>
                </td>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="Employee Name" ID="lblEmpName"></asp:Label>
                </td>
                <td class="FieldCell FieldCell3">
                    <asp:Label runat="server" ID="lblEmpNameValue" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="Organizational Unit" ID="lblOrgUnit"></asp:Label>
                </td>
                <td class="FieldCell FieldCell1">
                    <asp:Label runat="server" ID="lblOrgUnitValue" ></asp:Label>
                </td>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="Position" ID="lblPosition"></asp:Label>
                </td>
                <td class="FieldCell FieldCell3">
                    <asp:Label runat="server" ID="lblPositionValue" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="Hire Date" ID="lblHireDate"></asp:Label>
                </td>
                <td class="FieldCell FieldCell1">
                    <asp:Label runat="server" ID="lblHireDateValue" ></asp:Label>
                </td>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="Appraiser" ID="lblAppraiser"></asp:Label>
                </td>
                <td class="FieldCell FieldCell3">
                    <asp:Label runat="server" ID="lblAppraiserValue" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="Reviewer" ID="lblReviewer"></asp:Label>
                </td>
                <td class="FieldCell FieldCell1">
                    <asp:Label runat="server" ID="lblReviewerValue" ></asp:Label>
                </td>
                <td class="FieldTitle FieldTitle1">
                    <asp:Label runat="server" Text="HR Business Partner" ID="lblCountryHR"></asp:Label>
                </td>
                <td class="FieldCell FieldCell3">
                    <asp:Label runat="server" ID="lblCountryHRValue" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FieldTitle last_td">
                    <asp:Label runat="server" Text="Appraisal Period" ID="lblAppraisalPeriod"></asp:Label>
                </td>
                <td class="FieldCell last_td">
                    <asp:Label runat="server" ID="lblAppraisalPeriodValue" ></asp:Label>
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
            <asp:Repeater ID="RptRevaluation" runat="server">
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
                            <asp:Label Text="Score (%)" runat="server" ID="lblScore" />
                        </td>
                        <td align="center" class="td_lines" style="width: 10%;">
                            <asp:Label Text="Action" runat="server" ID="lblAction" />
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="td_lines td_bottom Grid1 serial_numbers" valign="top">
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
                            <asp:TextBox runat="server" ID="txtGoal" Visible="false" CssClass="Textbox" Text='<%#Eval("agGoal") %>'
                                Width="99%" />
                        </td>
                        <td class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblDueDate" Text='<%# ((DateTime)Eval("agDueDate")).ToString("dd-MMM-yyyy")%>' />
                            <%--<SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" CssClassTextBox="datecss"
                                DateOnly="true" />--%>
                        </td>
                        <td class="td_lines Grid1" style="" align="center">
                            <asp:Label runat="server" ID="lblWeightage" Text='<%#Eval("agWeightage") %>' Visible="true" />
                            <asp:TextBox runat="server" ID="txtWeightage" Visible="false" class="condtional_box"
                                CssClass="Textbox" Text='<%#Eval("agWeightage") %>' Width="50%" />
                            <td class="td_lines Grid1" style="" align="center">
                                <asp:Label runat="server" ID="lblEvaluation" Visible="true" Text='<%#Eval("agEvaluation") %>' />
                                <asp:TextBox runat="server" ID="txtEvaluation" class="condtional_box" CssClass="Textbox"
                                    Text="25" Width="50%" Visible="false" />
                            </td>
                            <td class="td_lines Grid1" style="" align="center">
                                <asp:Label runat="server" ID="lblScore" Visible="true" Text='<%#Eval("agScore") %>' />
                            </td>
                            <td class="td_lines">
                                <%-- <asp:LinkButton Text="Save" ID="lnkSave" Visible="false" runat="server" CommandArgument='<%#Eval("SNo") %>' />
                                <asp:LinkButton Text="Cancel" ID="lnkCancel" Visible="false" runat="server" CommandArgument='<%#Eval("SNo") %>' />
                                <asp:LinkButton Text="Edit" ID="lnkEdit" runat="server" Visible="false" CommandArgument='<%#Eval("SNo") %>' />--%>
                                <%--<asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click"
                                    CommandArgument='<%#Eval("ID") %>' />--%>
                            </td>
                    </tr>
                    <tr>
                        <td class="td_lines td_bottom td_up">
                        </td>
                        <td class="td_lines Grid1" colspan="7">
                            <asp:Label Text="Description:" runat="server" ID="lblDescription1" />
                            <asp:Label runat="server" ID="lblDescription" Text='<%#Eval("agGoalDescription") %>' />
                            <asp:TextBox runat="server" ID="txtDescription" Text='<%#Eval("agGoalDescription") %>'
                                TextMode="MultiLine" Columns="71" Width="99%" CssClass="MLTextbox" Visible="false" />
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
                            <asp:Label Text="Reviewer:" ID="lblComments" runat="server" />
                            <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="99%" CssClass="MLTextbox"
                                Visible="false" />
                            <asp:Label Text='<%#Eval("agReviewerLatestComments")%>' ID="lblReviewerComments"
                                runat="server"></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
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
                    <asp:Label  ID="lblScoretotal" runat="server" />
                </td>                
            </tr>
            <tr>
            </tr>
            <tr>                
                <td align="right" colspan="6" class="td_lines Grid1">
                    <asp:Label ID="LblH1score" Text="H1Score" runat="server"></asp:Label>
                </td>
                <td align="center" class="td_lines" colspan="2">
                    <asp:Label  ID="lblscore" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="6" class="td_lines Grid1">
                    <asp:Label ID="LblFA" Text="Final Score" runat="server"></asp:Label>
                </td>
                <td align="center" class="td_lines" colspan="2">
                    <asp:Label  ID="lblFS" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" colspan="6" class="td_lines Grid1">
                    <asp:Label ID="LblfinalRating" Text="Final Rating" runat="server"></asp:Label>
                </td>
                <td align="center" class="td_lines" colspan="2">
                    <asp:Label Text="" ID="lblfr" runat="server" />
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
                            <asp:Label runat="server" ID="lblDescriptionValue" Text='<%#Eval("acmptDescription") %>' />
                        </td>
                        <td class="td_lines Grid1 rating1" align="center">
                            <asp:Label runat="server" ID="lblexectedresult" Text='<%#Eval("acmptExpectedResult")%>'
                                Visible="true" />
                            <%--<asp:DropDownList runat="server" ID="ddlExpectedResult">
                                <asp:ListItem Text="Select" />
                                <asp:ListItem Text="Good" />
                                <asp:ListItem Text="Fair" />
                                <asp:ListItem Text="Poor" />
                            </asp:DropDownList>--%>
                        </td>
                        <td class="td_lines Grid1 rating1" align="center">
                            <asp:Label runat="server" ID="lblrating" Text='<%#Eval("acmptRating")%>' Visible="true" />
                            <%--<asp:DropDownList runat="server" ID="ddlrating">
                                <asp:ListItem Text="Select" />
                                <asp:ListItem Text="Good" />
                                <asp:ListItem Text="Fair" />
                                <asp:ListItem Text="Poor" />
                            </asp:DropDownList>--%>
                        </td>
                        <tr>
                            <td>
                            </td>
                            <td class="td_lines Grid1" colspan="4">
                                <asp:Label Text="Appraisee:       " ID="lblAppraiseeComments" runat="server" />&nbsp;&nbsp;
                                <asp:Label ID="lblAppraise" Text='<%#Eval("acmptAppraiseeLatestComments")%>' runat="server"></asp:Label>
                                <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="td_lines Grid1" colspan="4">
                                <asp:Label Text="Appraiser:       " ID="Label1" runat="server" />&nbsp;&nbsp;
                                <asp:Label ID="lblAppraiser" Text='<%#Eval("acmptAppraiserLatestComments")%>' runat="server"></asp:Label>
                                <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_lines td_up">
                            </td>
                            <td class="td_lines Grid1" colspan="7">
                                <asp:Label Text="Reviewer:" ID="LblComments" runat="server" />
                                <%--<asp:TextBox runat="server" TextMode="MultiLine" ID="TxtAppraisercmts" CssClass="MLTextbox"
                                    Width="99%" Visible="false" />--%>
                                <asp:Label ID="lblReviewerComm" Text='<%#Eval("acmptReviewerLatestComments")%>' runat="server" />
                            </td>
                        </tr>
                </ItemTemplate>
            </asp:Repeater>
            <%--<tr>
                <td class="td_lines Grid1" colspan="6" align="right">
                    <asp:Button Text="Save" ID="btncmptSave" runat="server" OnClick="BtnSave_Click" CssClass="Button" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Approve" ID="btncmptApprove" runat="server" OnClick="BtnApprove_Click"
                        CssClass="Button" />&nbsp;&nbsp;&nbsp;
                    <asp:Button Text="Cancel" ID="btncmptCancel" runat="server" OnClick="BtnCancel_Click"
                        CssClass="Button" />
                </td>
            </tr>--%>
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
                        <%--<td align="center" class="td_lines" style="width: 10%">
                            <asp:Label Text="Action" runat="server" ID="lblaction" />
                        </td>--%>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="td_lines Grid1 td_bottom verticalalign">
                            <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                            <asp:Label Text='<%#Eval("ID") %>' Visible="false" runat="server" ID="LblDeId" />
                            <%--  <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblID" />--%>
                        </td>
                        <td align="center" class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblWhen" Visible="true" Text='<%#Eval("pdpWhen")%>' />
                            <%--<SharePoint:DateTimeControl LocaleId="2057" ID="shpdatecontrol" runat="server" DateOnly="true" CssClassTextBox="datecss"   TimeOnly="false" />--%>
                        </td>
                        <td align="left" class="td_lines Grid1">
                            <asp:Label runat="server" ID="lblWhat" Visible="true" Text='<%#Eval("pdpWhat") %>' />
                            <%--<asp:TextBox runat="server" ID="txtwhat" TextMode="MultiLine" Width="99%" Text='<%#Eval("pdpWhat") %>' />--%>
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
                            <asp:Label ID="lblAppraise" Text='<%#Eval("pdpH2AppraiseeComments") %>' runat="server"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="td_lines Grid1" colspan="3">
                            <asp:Label Text="Appraiser:       " ID="Label1" runat="server" />&nbsp;&nbsp;
                            <asp:Label ID="lblAppraiser" Text='<%#Eval("pdpH2AppraiserComments") %>' runat="server"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="txtAppraiseeComments" TextMode="MultiLine" Width="619px" />--%>
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="td_lines td_up">
                        </td>
                        <td class="td_lines Grid1" colspan="5">
                            <asp:Label Text=" Comments" ID="lblComments" runat="server" CssClass="comments1"  />
                            <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" Width="99%"  Text='<%#Eval("pdpH1AppraiserComments") %>'/>
                        </td>
                    </tr>--%>
                </ItemTemplate>
            </asp:Repeater>
            <%--   <tr>
                <td align="right" class="td_lines " colspan="5">
                    <asp:Button Text="Save" ID="btnSavePDP" runat="server" OnClick="BtnSave_Click"
                        CssClass="Button" />
                    <asp:Button Text="Approve" ID="btnSubmitPDP" runat="server"
                        CssClass="Button" />
                    <asp:Button Text="Cancel" ID="btnCancelPDP" runat="server"
                        CssClass="Button" />
                </td>
            </tr>--%>
        </table>
    </div>
    <div runat="server" id="divPIP" visible="false">
        <asp:HiddenField ID="piphfAppraiselid" Value="2" runat="server" />
        <asp:HiddenField ID="pipAppraisalPhaseID" Value="2" runat="server" />
        <asp:HiddenField ID="piphidenfield2" Value="2" runat="server" />
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse;" class="Grid">
            <asp:Repeater ID="rptPIP" runat="server">
                <HeaderTemplate>
                    <tr class="GridHead">
                        <td align="center" class="td_lines " style="">
                            <asp:Label Text="S.No" runat="server" ID="lblSno" />
                        </td>
                        <td align="center" class="td_lines" style="">
                            <asp:Label Text="Performance Issue" runat="server" ID="lblPerformanceIssue" />
                        </td>
                        <td align="center" class="td_lines" style="">
                            <asp:Label Text="Expected Achievement" runat="server" ID="lblExpectedAchivements" />
                        </td>
                        <td align="center" class="td_lines" style="">
                            <asp:Label Text="Time Frame" runat="server" ID="lblTimeFrame" />
                        </td>
                        <td align="center" class="td_lines" runat="server" id="tdResultMT" style="">
                            <asp:Label Text="Actual Result (Mid Term)" runat="server" ID="lblActualResult"/>
                        </td>
                        <td align="center" class="td_lines" runat="server" id="tdAssessmentMT" style="">
                            <asp:Label Text="Appraiser Assessment(Mid Term)" runat="server" ID="lblAppraisersAssessment"/>
                        </td>
                        <td align="center" class="td_lines" runat="server" id="tdResultFT" style="">
                            <asp:Label Text="Actual Result (Final)" runat="server" ID="lblFActualResult"/>
                        </td>
                        <td align="center" class="td_lines" runat="server" id="tdAssessmentFT" style="">
                            <asp:Label Text="Appraiser Assessment (Final)" runat="server" ID="lblFAppraisersAssessment"/>
                        </td>
                        <td align="center" class="td_lines" style="">
                            <asp:Label Text="Action" runat="server" ID="lblAction" />
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td align="center" class="td_lines Grid1 serial_numbers " style="">
                            <asp:Label Text='<%#Eval("ID") %>' runat="server"  ID="lblID"  Visible="false"/>
                            <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                        </td>
                        <td align="center" class="td_lines Grid1" style="">
                            <asp:Label Text='<%#Eval("pipPerformanceIssue") %>' CssClass="Grid1" ID="lblPerformanceIssu"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipPerformanceIssue") %>' ID="txtPerformanceIssu" CssClass="PIPMandatory performance" Rows="4"
                                runat="server" Width="99%" />
                        </td>
                        <td align="center" class="td_lines Grid1" style="">
                            <asp:Label Text='<%#Eval("pipExpectedachivement") %>' CssClass="Grid1" ID="lblExpectedAchivements"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipExpectedachivement") %>' ID="txtExpectedAchivements" CssClass="PIPMandatory achivement" Rows="4"
                                runat="server" Width="99%" />
                        </td>
                        <td align="center" class="td_lines Grid1" style="">
                            <asp:Label Text='<%#Eval("pipTimeFrame") %>' ID="lblTimeFrame" CssClass="Grid1" runat="server"
                                Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipTimeFrame") %>' ID="txtTimeFrame" CssClass="PIPMandatory TimeFrame" Rows="4"
                                runat="server" Width="99%" />
                        </td>
                        <td align="center" class="td_lines Grid1" runat="server" id="tdActualResultMT" style="">
                            <asp:Label Text='<%#Eval("pipMidTermActualResult") %>' ID="lblActualResultmidterm"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipMidTermActualResult") %>' ID="txtActualResultmidterm" Rows="4"
                                runat="server" Width="99%" Visible="false"/>
                        </td>
                        <td align="center" class="td_lines Grid1" runat="server" id="tdActualAssessmentMT"  style="">
                            <asp:Label Text='<%#Eval("pipMidTermAppraisersAssessment") %>' ID="lblAppraisersAssessmentmidterm"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipMidTermAppraisersAssessment") %>' Rows="4"
                                ID="txtAppraisersAssessmentmidterm" runat="server" Width="99%" Visible="false"/>
                        </td>
                        <td align="center" class="td_lines Grid1" runat="server" id="tdAcutualResultFT" style="">
                            <asp:Label Text='<%#Eval("pipFinalAcutualResult") %>' ID="lblActualResultfinelterm"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipFinalAcutualResult") %>' ID="txtActualResultfinelterm" Rows="4"
                                runat="server" Width="99%" Visible="false"/>
                        </td>
                        <td align="center" class="td_lines Grid1" runat="server" id="tdAcutualAssesmentFT" style="">
                            <asp:Label Text='<%#Eval("pipFinalAppraisersAssesment") %>' ID="lblAppraisersAssessmentfinalterm"
                                runat="server" Visible="false" />
                            <asp:TextBox TextMode="MultiLine" Text='<%#Eval("pipFinalAppraisersAssesment") %>' Rows="4"
                                ID="txtAppraisersAssessmentfinalterm" runat="server" Width="99%" Visible="false"/>
                        </td>
                        <td align="center" class="td_lines Grid1" runat="server" id="tdPIPDelete" style="">
                            <asp:LinkButton Text="Delete" ID="lnkPIPDelete" OnClick="lnkPIPDelete_Click"
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
                <td align="right" class="td_lines " colspan="9">
                    <asp:Button Text="Submit" ID="lnkSave" OnClick="btnSave_Click" runat="server" CssClass="Button" OnClientClick="return ValidatePIP();"/>
                    <%--<asp:Button Text="Submit" ID="lnkapprove" OnClick="lnkapprove_Click" runat="server"
                        CssClass="Button" />--%>
                   <%-- <asp:Button Text="Cancel" ID="btnPIPCancel" OnClick="btnPIPCancel_Click" runat="server" CssClass="Button" />--%>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
            border-collapse: collapse; padding-right: 5px; border-top: none;">
            <tr>
            <td class="Grid1">
            <asp:Label Text="Appraisee Sign-Off Comments:" ID="lblSignOffComm" runat="server" />
            <asp:Label  ID="lblSignOffComments" runat="server" />
            </td>
            </tr>             
            <tr>
                <td align="right" class="td_lines td_up Grid1">
                 <asp:Button Text="Print" ID="btnPrint" CssClass="Button"  runat="server" OnClientClick="printCert();return false;" />
                    <%--<asp:Button Text="Complete" ID="btnComplete" runat="server" CssClass="Button" />
                    <asp:Button Text="Review" ID="btnReview" runat="server" CssClass="Button" />--%>
                    <asp:Button Text="Cancel" ID="btnCancel" runat="server" CssClass="Button" OnClick="BtnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
H2 Appraiser PIP Entry
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
H2 Appraiser PIP Entry
</asp:Content>
