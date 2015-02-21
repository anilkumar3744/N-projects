<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitialGoalSetting.aspx.cs"
    Inherits="VFS.PMS.ApplicationPages.Layouts.VFS_ApplicationPages.InitialGoalSetting"
    DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <%--<script type="text/javascript" src="/_layouts/Scripts/jquery-1.9.0.min.js"> </script>--%>
    <%--<script type="text/javascript" src="/_layouts/Scripts/jquery.SPServices-2013.01.min.js"> </script>--%>
    <SharePoint:ScriptLink Name="~sitecollection/SiteAssets/jquery.js" ID="ScriptLink1"
        runat="server" Defer="false">
    </SharePoint:ScriptLink>
    <script type="text/javascript">

                              
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
        .MLTextboxError
        {
            border-bottom: #5bb4fa 1px solid;
            border-left: #5bb4fa 1px solid;
            background-color: #ffffff;
            border-color: Red;
            font-family: Tahoma, Arial, Verdana, Helvetica, sans-serif;
            color: #333333;
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
    </style>--%>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        function RedirectWithMessage(str) {

        }
        $(document).ready(function () {

          //  $(".datecss").attr('readonly', 'readonly');
            $(".condtional_box").keypress(function (event) {
                var key = event.which || event.keyCode;
                if (!(48 <= key && key <= 57)) {
                    event.preventDefault();
                    return false;
                }
            });

        });
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

        function Validate() {
            var flag1 = true;
            //alert($(".validation").length);
            $('.validation').each(function (i, obj) {
                row = parseInt(i / 5) + 1;
                if (obj.value.trim() == '') {
                   
                    if ($(this).hasClass("reqtxtGoal")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Goal at row  ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqSPDateLastDate")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify due date for the Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("reqtxtDescription")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Description for the Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                    else if ($(this).hasClass("condtional_box")) {
                        //$(this).addClass("EmptyField");
                        alert('Please specify Weightage for the Goal at row: ' + row);
                        $(this).focus();
                        flag1 = false;
                        return false;
                    }
                }
                else if ($(this).hasClass("category") && $(this).prop("selectedIndex") == 0) {
                   // row = parseInt(i / 5) + 1;
                    alert('Please select Category for the Goal at row: ' + row);
                    $(this).focus();
                    flag1 = false;
                    return false;
                }
            });
            var totalSum = 0, row = 0;
            var maxVal = 100;
            $('.condtional_box').each(function (i, obj) {
                var sum = parseInt(obj.value);
                if (obj.value == '')
                    sum = 0;
                totalSum = parseInt(totalSum) + parseInt(sum);
            });

            if (flag1) {
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
        function OpenDialog(strPageURL) {
            var dialogOptions = SP.UI.$create_DialogOptions();
            dialogOptions.url = '<%=message%>'; // URL of the Page
            dialogOptions.width = 750; // Width of the Dialog
            dialogOptions.height = 500; // Height of the Dialog
            dialogOptions.dialogReturnValueCallback = Function.createDelegate(null, CloseCallback); // Function to capture dialog closed event
            SP.UI.ModalDialog.showModalDialog(dialogOptions); // Open the Dialog
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
    <asp:UpdatePanel runat="server" ID="upnlInitialGoalSetting">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="btnPrint" />
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
        <ContentTemplate>
            <div>
                <asp:HiddenField ID="hfAppraisalID" runat="server" />
                <asp:HiddenField ID="hfAppraisalPhaseID" runat="server" />
                <asp:HiddenField ID="hfflag" Value="false" runat="server" />
                <asp:HiddenField ID="hfAppraiserCode" runat="server" />
                <asp:HiddenField ID="hrReviewerCode" runat="server" />
                <asp:HiddenField ID="hrCountryHrCode" runat="server" />
                <asp:HiddenField ID="hfldMandatoryGoalCount" runat="server" />
                <asp:HiddenField ID="hfSavedFlag" Value="false" runat="server" />
                <asp:HiddenField ID="appLog"  runat="server" />
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class=""
                    style="padding: 0px;">
                    <tr>
                        <td align="center" style="font-size: large;" colspan="2">
                            <asp:Label Text="" ID="lblAppraiserView" runat="server" />
                        </td>
                    </tr>
                    <%--<tr>
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
                          <%--  <h3 style="margin-top: 3px; margin-left: 10px;">--%>
                                <asp:Label runat="server" Text="Performance Agreement for:  " ID="lblHeader" class="TableHeader_1"></asp:Label>&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblHeaderValue" class="TableHeader_1"></asp:Label>
                           <%-- </h3>--%>
                        </td>
                        <td align="right">
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
                                <asp:Label runat="server" ID="lblAppraisalPeriodValue"></asp:Label>
                            </td>
                            <td class="FieldTitle last_td">
                                <asp:Label runat="server" ID="ex"></asp:Label>
                            </td>
                            <td class="FieldCell FieldCell2">
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
            </div>
            <div runat="server" id="dvGoalSettings">
                <asp:HiddenField ID="hfGoalsCount" Value="5" runat="server" />
                <table cellpadding="7px" cellspacing="2px" width="100%" style="border: 1px solid;
                    border-collapse: collapse; padding-right: 5px" class="Grid"  >
                    <asp:Repeater ID="rptGoalSettings" runat="server">
                        <HeaderTemplate>
                            <tr class="GridHead">
                                <td align="center" class="td_lines " style="width: 5%;">
                                    <asp:Label Text="S.No" runat="server" ID="lblSno" />
                                </td>
                                <td align="center" class="td_lines" style="width: 25%;">
                                    <asp:Label Text="Category" runat="server" ID="lblCategory" />
                                </td>
                                <td align="center" class="td_lines" style="width: 42%;">
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
                                <%--<td align="center" class="td_lines" style="width: 5%;">
                            <asp:Label Text="" runat="server" ID="lblMandatory" Visible="false" />
                        </td>--%>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="Grid1" id="snolbl" runat="server">
                                    <asp:Label Text='<%#Eval("ID") %>' runat="server" Visible="false" ID="lblID" />
                                    <asp:Label Text='<%#Eval("SNo") %>' runat="server" ID="lblSno" />
                                    <asp:Panel ID="testpnl" runat="server" style="display:none"/>
                                </td>
                                <td class="td_lines Grid1">
                                    <asp:Label Text='<%#Eval("agGoalCategory")%>' runat="server" ID="lblCategory" />
                                    <asp:DropDownList ID="ddlCategories" runat="server" Visible="false" CssClass="category validation">
                                    </asp:DropDownList>
                                </td>
                                <td class="td_lines Grid1" align="left">
                                    <asp:Label runat="server" ID="lblGoal" Visible="false" Text='<%#Eval("agGoal") %>' />
                                    <asp:TextBox runat="server" ID="txtGoal" Width="99%" CssClass="Textbox reqtxtGoal validation"
                                        Text='<%#Eval("agGoal") %>' />
                                </td>
                                <td class="td_lines Grid1">
                                    <%-- <asp:Label runat="server" ID="lblDueDate" Visible="false" Text='<%# ((DateTime)Eval("agDueDate")).ToString("dd-MMM-yyyy")%>' />
                                    --%>
                                    <SharePoint:DateTimeControl LocaleId="2057" runat="server" ID="SPDateLastDate" CssClassTextBox="datecss reqSPDateLastDate validation"
                                        DateOnly="true" />
                                </td>
                                <td class="td_lines Grid1" style="" align="center">
                                    <asp:Label runat="server" ID="lblWeightage" Visible="false" Text='<%#Eval("agWeightage") %>' />
                                    <asp:TextBox runat="server" ID="txtWeightage" CssClass="Textbox condtional_box validation" onkeypress="return isNumberKeyW(event)"
                                        Width="40%" Text='<%#Eval("agWeightage") %>' MaxLength="4" onpaste="return false" />
                                    <%--  <asp:RegularExpressionValidator ID="rgNumericValidation" ValidationExpression="^[0-9]*$" ErrorMessage="Enter numerics only" ControlToValidate="txtWeightage"
                                runat="server" />--%>
                                </td>
                                <td class="td_lines Grid1">
                                    <%--<asp:LinkButton Text="Save" ID="lnkSave" Visible="false" runat="server" OnClick="lnkSave_Click"
                                CommandArgument='<%#Eval("SNo") %>' />
                            <asp:LinkButton Text="Cancel" ID="lnkCancel" Visible="false" runat="server" OnClick="lnkCancel_Click"
                                CommandArgument='<%#Eval("SNo") %>' />
                            <asp:LinkButton Text="Edit" ID="lnkEdit" Visible="false" runat="server" OnClick="lnkEdit_Click"
                                CommandArgument='<%#Eval("SNo") %>' />--%>
                                    <asp:LinkButton Text="Delete" ID="lnkDelete" Visible="false" runat="server" OnClick="LnkDelete_Click"
                                        OnClientClick="return confirmation();" CommandArgument='<%#Eval("SNo") %>' />
                                </td>
                                <%--<td>
                            <asp:Label Text='<%#Eval("isManadatory") %>' runat="server" ID="lblisManadatory"
                                Visible="false" />
                        </td>--%>
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
                            <asp:Label ID="Label1" Text="Comments" runat="server" CssClass="comments1" />
                            <asp:TextBox runat="server" ID="txtComments" TextMode="MultiLine" CssClass="MLTextbox  commentstextboxwidth" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="right">
                            <asp:Button Text="Print" ID="btnPrint" Visible="false"  CssClass="Button" runat="server" OnClientClick="printCert();return false;" />
                            <asp:Button Text="Save" ID="btnSave" runat="server" OnClick="BtnSave_Click" CssClass="Button" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <%--        OnClientClick="return Validate()" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                            <asp:Button Text="Submit" ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click"
                                CssClass="Button" OnClientClick="return Validate()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button Text="Cancel" ID="btnCancel" runat="server" OnClick="BtnCancel_Click"
                                CssClass="Button" />
                            <%--   <asp:Button Text="Amend Goals" ID="Button1" runat="server" 
                        CssClass="Button" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Goal Setting
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Goal Setting
</asp:Content>
