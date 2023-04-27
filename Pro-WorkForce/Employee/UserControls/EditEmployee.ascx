<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditEmployee.ascx.vb"
    Inherits="EditEmployee_WebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="EmpLeave_List.ascx" TagName="EmpLeave_List" TagPrefix="uc2" %>
<%@ Register Src="EmpPermissions_List.ascx" TagName="EmpPermissions_List" TagPrefix="uc3" %>
<%@ Register Src="EmpActiveSchedule.ascx" TagName="Emp_ActiveSchedule" TagPrefix="uc4" %>
<script type="text/javascript">
    function viewPolicyDetails(tmp) {

        var PolicyId = $find('<%= RadCmbPolicies.ClientID %>')._value;
        if (PolicyId != -1)
            oWindow = radopen('../Admin/TAPolicyPopup.aspx?ID=' + PolicyId, "RadWindow1");
        return false;

    }

    function viewOverTimeRule(tmp) {

        var RuleId = $find('<%= RadCmbOvertime.ClientID %>')._value;
        if (RuleId != -1)
            oWindow = radopen('../Admin/OvertimeRulesPopUp.aspx?RuleId=' + RuleId, "RadWindow1");
        return false;

    }
    function hideValidatorCalloutTab() {
        try {
            if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();


            }
        }
        catch (err) {
        }
        return false;
    }

    function ConfirmDeleteEmployee() {
        var Lang = '<%= Lang %>'
        if (Lang == "en-US") {
            return confirm('Are you sure you want to delete?');
        } else {
            return confirm('هل انت متاكد من الحذف ؟');
        }
    }

    //    function ShowPopUp(Mode) {
    //        var lang = '<%= MsgLang %>';
    //        if (Mode == 1) {
    //            if (lang == 'en') {
    //                return confirm('Are you sure you want to Assign Manager to the employee?');
    //            }
    //            else {
    //                return confirm('هل انت متأكد تعيين مدير للموظف ؟');
    //            }
    //        }

    //    }

</script>
<%--<script type="text/javascript" language="javascript">
    function ValidatePage() {
       
        var tabContainer = $get('<%= TabEmployee.ClientID %>');
       
        var valCntl = $get('<%= reqEmployeeNumber.ClientID %>');
        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(0);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderEmployeeNo.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }


        var valCntl = $get('<%=reqEnglishName.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(0);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqEnglishName.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }
        var valCntl = $get('<%=reqOrgCompany.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(0);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqOrgCompany.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }
        var valCntl = $get('<%=reqEmpEntity.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(0);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqEmpEntity.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }
        var valCntl = $get('<%=RegularExpressionValidator1.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(0);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender1.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }
        var valCntl = $get('<%=reqEmpStatus.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(0);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqEmpStatus.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }
        var valCntl = $get('<%=RequiredFieldValidator3.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(1);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender5.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }

        var valCntl = $get('<%=RequiredFieldValidator1.ClientID%>');

        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(1);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender2.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }

        var valCntl = $get('<%=RequiredFieldValidator4.ClientID%>');
        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(2);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender6.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }
        var valCntl = $get('<%=RequiredFieldValidator4.ClientID%>');
        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(2);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender6.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }

        var valCntl = $get('<%=RequiredFieldValidator2.ClientID%>');
        if (valCntl != undefined && valCntl != null) {
            ValidatorValidate(valCntl);

            if (!valCntl.isvalid) {

                if (tabContainer != undefined && tabContainer != null) {
                    tabContainer = tabContainer.control;
                    tabContainer.set_activeTabIndex(2);
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender3.ClientID %>');
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                }
                return false;
            }
        }

    }

    function hideValidatorCalloutTab() {
        try {
           
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();
            }
        }
        catch (err) {
        }
        return false;
    }
</script>--%>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
    EnableShadow="True" InitialBehavior="None" Style="z-index: 8000;" Modal="true">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move,Resize"
            Behaviors="Close, Move,Resize" EnableShadow="True" Height="450px" IconUrl="~/images/HeaderWhiteChrome.jpg"
            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
            Width="600px" Skin="Vista">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <cc1:TabContainer ID="TabEmployee" runat="server" ActiveTabIndex="0" Width="100%"
            OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabEmployeeResource1">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Employee info" ToolTip="Employee Information"
                meta:resourcekey="TabPanel1Resource1">
                <ContentTemplate>
                    <asp:UpdatePanel ID="IpdatePanel2" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload1" />
                        </Triggers>
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmpImg" runat="server" Text="Employee Image"
                                        meta:resourcekey="lblEmpimgResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Image ID="EmpImg" runat="server" BorderStyle="Solid" Height="125px" 
                                        BorderWidth="1px" /> <%--Width="150px"--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div id="trAttachedFile" runat="server" class="col-md-4">
                                    <div class="input-group">
                                        <span class="input-group-btn">
                                            <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                            <asp:FileUpload ID="FileUpload1" runat="server" meta:resourcekey="fuAttachFileResource1" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" />
                                        </span>
                                        <span class="form-control"></span>
                                    </div>
                                </div>
                                <div class="col-md-4">

                                    <asp:Button ID="btnUpload1" runat="server" Text="Upload" CssClass="button" OnClick="UploadImg"
                                        meta:resourcekey="btnUploadResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmployeeType" runat="server" Text="Employee Type"
                                        meta:resourcekey="lblEmployeeTypeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="radEmployeeType" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="true" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="radEmployeeTypeResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" Value="0" meta:resourcekey="RadComboBoxItemResource3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rfvradEmployeeType" runat="server" ControlToValidate="radEmployeeType"
                                        Display="None" ErrorMessage="Please Select Employee Type" InitialValue="--Please Select--"
                                        ValidationGroup="gpNext" meta:resourcekey="rfvradEmployeeTypeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceradEmployeeType" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvradEmployeeType" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="trContractEndDate" runat="server" visible="false">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblContractEndDate" runat="server" Text="Expiry date"
                                        meta:resourcekey="lblContractEndDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="rdpContractEndDate" runat="server" AllowCustomText="false"
                                        Culture="English (United States)" MarkFirstMatch="true" PopupDirection="TopRight"
                                        ShowPopupOnFocus="True" Skin="Vista" AutoPostBack="true" meta:resourcekey="rdpContractEndDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvContractEndDate" runat="server" ControlToValidate="rdpContractEndDate"
                                        ValidationGroup="gpNext" Display="None" ErrorMessage="Please Enter Contract End Date"
                                        Enabled="false" meta:resourcekey="rfvContractEndDateResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceContractEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvContractEndDate" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="trExternalParty" runat="server" visible="false">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblExternalPartyName" runat="server" Text="External Party Name"
                                        meta:resourcekey="lblExternalPartyNameResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtExternalPartyName" runat="server" meta:resourcekey="txtExternalPartyNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtExternalPartyName" runat="server" ControlToValidate="txtExternalPartyName"
                                        ValidationGroup="gpNext" Display="None" ErrorMessage="Please Enter External Party Name"
                                        Enabled="false" meta:resourcekey="rfvtxtExternalPartyNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vcetxtExternalPartyName" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvtxtExternalPartyName" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNumber" runat="server" Text="Employee Number"
                                        meta:resourcekey="lblNumberResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEmployeeNumber" runat="server" Enabled="false" meta:resourcekey="txtEmployeeNumberResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqEmployeeNumber" runat="server" ControlToValidate="txtEmployeeNumber"
                                        ValidationGroup="grpsave" Display="None" ErrorMessage="Please enter employee number"
                                        meta:resourcekey="reqEmployeeNumberResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderEmployeeNo" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmployeeNumber" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblPayRollNumber" runat="server" Text="PayRoll Number"
                                        meta:resourcekey="lblPayRollNumberResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPayRollNumber" runat="server" meta:resourcekey="txtPayRollNumberResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmployeeCardNo" runat="server" Text="Employee Card Number"
                                        meta:resourcekey="lblEmployeeCardNoResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <%--  <telerik:RadNumericTextBox ID="txtEmployeeCardNo" runat="server" MinValue="1" Culture="English (United States)"
                                                        LabelCssClass="">
                                                        <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                                    </telerik:RadNumericTextBox>--%>
                                    <asp:TextBox ID="txtEmployeeCardNo" runat="server" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCardNo" runat="server" ControlToValidate="txtEmployeeCardNo"
                                        ValidationGroup="grpsave" Display="None" ErrorMessage="Please enter employee card number"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvCardNo" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                                 <div class="col-md-2">
                                    <div id="dvCardNoExists" runat="server" visible="false">
                                        <asp:Label ID="lblCardNoExists" runat="server" 
                                            Text="Card No. Exists!, Please Insert another Number"
                                            meta:resourcekey="lblCardNoExistsResource1" Font-Size="6pt" ForeColor="red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblENROLL_ID" runat="server" Text="Enroll ID"
                                        meta:resourcekey="lblENROLL_IDResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtENROLL_ID" runat="server" meta:resourcekey="txtENROLL_IDNumberResource1" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEnglishName" runat="server" Text="English Name"
                                        meta:resourcekey="lblEnglishNameResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEnglishName" runat="server" meta:resourcekey="txtEnglishNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqEnglishName" ValidationGroup="grpsave" runat="server"
                                        ControlToValidate="txtEnglishName" Display="None" ErrorMessage="Please enter english name"
                                        meta:resourcekey="reqEnglishNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEnglishName" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEnglishName" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblArabicName" runat="server" Text="Arabic Name"
                                        meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtArabicName" runat="server" meta:resourcekey="txtArabicNameResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" id="trGTReaders" runat="server">
                                <div class="col-md-2">
                                    <asp:Label ID="lblGTReader" runat="server" Text="GT Reader Values" meta:resourcekey="lblGTReaderResource1"
                                        CssClass="Profiletitletxt" />
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="radCmbGTReaders" runat="server" Skin="Vista"
                                        AutoPostBack="true" MarkFirstMatch="true">
                                        <%--  <Items>
                                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" Value="-1" meta:resourcekey="radCmbGTReadersItemResource1" />
                                                            <telerik:RadComboBoxItem runat="server" Text="Finger only" Value="noneIfBio" meta:resourcekey="radCmbGTReadersItemResource2" />
                                                            <telerik:RadComboBoxItem runat="server" Text="Card and Finger" Value="bio" meta:resourcekey="radCmbGTReadersItemResource3" />
                                                            <telerik:RadComboBoxItem runat="server" Text="Card only" Value="noneIfBadge" meta:resourcekey="radCmbGTReadersItemResource4" />
                                                            <telerik:RadComboBoxItem runat="server" Text="Card and PIN" Value="badge ,denied"
                                                                meta:resourcekey="radCmbGTReadersItemResource5" />
                                                        </Items>--%>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblPinNo" runat="server" Text="PIN No." meta:resourcekey="lblPinNo"
                                        CssClass="Profiletitletxt" Visible="false" />
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPinNo" runat="server" meta:resourcekey="txtPinNoResource1"
                                        Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblOrgCompany" runat="server" Text="Company"
                                        meta:resourcekey="lblOrgCompanyResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbOrgCompany" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" MarkFirstMatch="True" OnSelectedIndexChanged="RadCmbOrgCompany_SelectedIndexChanged"
                                        Skin="Vista" meta:resourcekey="RadCmbOrgCompanyResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqOrgCompany" ValidationGroup="grpsave" runat="server"
                                        ControlToValidate="RadCmbOrgCompany" Display="None" ErrorMessage="Please select company"
                                        InitialValue="--Please Select--" meta:resourcekey="reqOrgCompanyResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqOrgCompany" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqOrgCompany" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEntity" runat="server" Text="Entity"
                                        meta:resourcekey="lblEntityResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbEntity" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbEntityResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqEmpEntity" ValidationGroup="grpsave" runat="server"
                                        ControlToValidate="RadCmbEntity" Display="None" ErrorMessage="Please select entity name"
                                        InitialValue="--Please Select--" meta:resourcekey="reqEmpEntityResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEmpEntity" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmpEntity" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblWorkLocation" runat="server" Text="Work Location"
                                        meta:resourcekey="lblWorkLocationResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbWorkLocation" runat="server" MarkFirstMatch="True"
                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="RadCmbWorkLocationResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblGrade" runat="server" Text="Grade" meta:resourcekey="lblGradeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbGrade" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" AutoPostBack="True" meta:resourcekey="RadCmbGradeResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblDesignation" runat="server" Text="Designation"
                                        meta:resourcekey="lblDesignationResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbDesignation" runat="server" MarkFirstMatch="True"
                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="RadCmbDesignationResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblLogicalGroup" runat="server" Text="Logical Group"
                                        meta:resourcekey="lblLogicalGroupResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbLogicalGroup" runat="server" MarkFirstMatch="True"
                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="RadCmbLogicalGroupResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblGender" runat="server" Text="Gender"
                                        meta:resourcekey="lblGenderResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:RadioButton ID="RadioBtnMale" runat="server" Checked="True" GroupName="EmpGender"
                                        Text="Male" meta:resourcekey="RadioBtnMaleResource1" />
                                    <asp:RadioButton ID="RadioBtnFemale" runat="server" GroupName="EmpGender" Text="Female"
                                        meta:resourcekey="RadioBtnFemaleResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblDob" runat="server" Text="Birth Date"
                                        meta:resourcekey="lblDobResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpBirthDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                        AutoPostBack="true" meta:resourcekey="dtpBirthDateResource1" MinDate="1/1/1920 12:00:00 AM">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblAge" runat="server" Text="Age" meta:resourcekey="lblAgeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAge" runat="server" meta:resourcekey="txtAgeResource1" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                        Display="None" ErrorMessage="Please enter age" ValidationGroup="EmployeeGroup"
                                        meta:resourcekey="rfvAgeResource1" />
                                    <cc1:ValidatorCalloutExtender ID="vceAge" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvAge" WarningIconImageUrl="~/images/warning1.png" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmailAddress" runat="server" Text="E-mail Address"
                                        meta:resourcekey="lblEmailAddressResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" meta:resourcekey="txtEmailAddressResource1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="grpsave"
                                        runat="server" ControlToValidate="txtEmailAddress" Display="None" ErrorMessage="invalid e-mail address format"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblMobile" runat="server" Text="Mobile No."
                                        meta:resourcekey="lblMobileResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNationality" runat="server" Text="Nationality"
                                        meta:resourcekey="lblNationalityResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbNationality" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbNationalityResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="Label5" runat="server" Text="National Id No."
                                        meta:resourcekey="lblNationalIdResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtNationalId" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblReligion" runat="server" Text="Religion"
                                        meta:resourcekey="lblReligionResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbReligion" runat="server" MarkFirstMatch="True" Skin="Vista"
                                        meta:resourcekey="RadCmbReligionResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblMaritalStatus" runat="server" Text="Marital Status"
                                        meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbMaritalStatus" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbMaritalStatusResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblJoinDate" runat="server" Text="Join Date"
                                        meta:resourcekey="lblJoinDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpJoinDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                        AutoPostBack="true" meta:resourcekey="dtpJoinDateResource1" MinDate="1/1/1950 12:00:00 AM">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvJoinDate" runat="server" ControlToValidate="dtpJoinDate"
                                        ValidationGroup="gpNext" Display="None" ErrorMessage="Please Enter Join Date"
                                        meta:resourcekey="rfvJoinDateResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvJoinDate" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblStatus" runat="server" Text="Status"
                                        meta:resourcekey="lblStatusResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbStatus" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="true" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbStatusResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" Value="0" meta:resourcekey="RadComboBoxItemResource3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqEmpStatus" ValidationGroup="grpsave" runat="server"
                                        ControlToValidate="RadCmbStatus" Display="None" ErrorMessage="Please select status name"
                                        InitialValue="--Please Select--" meta:resourcekey="reqEmpStatusResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEmpStatus" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmpStatus" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="Termdate" runat="server" visible="false">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTerminationDate" runat="server" Text="Terminiation Date"
                                        meta:resourcekey="lblTerminationDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpTerminationDate" runat="server" AllowCustomText="false"
                                        Culture="English (United States)" MarkFirstMatch="true" PopupDirection="TopRight"
                                        ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpJoinDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                        meta:resourcekey="lblRemarksResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TA Policy" ToolTip="Time Attendance Policy"
                meta:resourcekey="TabPanel2Resource1">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblTaPolicy" runat="server" Text="TA Policy"
                                meta:resourcekey="lblTaPolicyResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbPolicies" MarkFirstMatch="True" Skin="Vista" runat="server"
                                meta:resourcekey="RadCmbPoliciesResource1">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="grpTA"
                                runat="server" ControlToValidate="RadCmbPolicies" Display="None" ErrorMessage="Please select TA policy"
                                InitialValue="--Please Select--" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator3" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                            <a href="#" onclick="viewPolicyDetails(1)">
                                <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lStartDate" runat="server" Text="Start date"
                                meta:resourcekey="lStartDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpStartDate" runat="server" AllowCustomText="false" ShowPopupOnFocus="True"
                                MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista" Culture="English (United States)"
                                meta:resourcekey="dtpStartDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                    Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="grpTA"
                                runat="server" ControlToValidate="dtpStartDate" Display="None" ErrorMessage="Please enter start date"
                                meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Temporary"
                                meta:resourcekey="lblTemporaryResource1" />
                        </div>
                    </div>
                    <asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="End date"
                                    meta:resourcekey="lblEndDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                                    meta:resourcekey="dtpEndDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:CompareValidator ID="Comparevalidator2" runat="server" ControlToValidate="dtpEndDate"
                                    ValidationGroup="grpTA" ControlToCompare="dtpStartDate" Display="None" Operator="GreaterThanEqual"
                                    ErrorMessage="End date should be greater than start date" meta:resourcekey="Comparevalidator2Resource1"></asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="Comparevalidator2" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnChangeTaPolicy" runat="server" Text="Change TA Policy" ValidationGroup="grpTA"
                                CssClass="button" meta:resourcekey="btnChangeTaPolicyResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid ID="grdVwTaPolicy" runat="server" AllowSorting="True" AllowPaging="True"
                                PageSize="5" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="true"
                                ShowFooter="True" meta:resourcekey="grdVwTaPolicyResource1">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView IsFilterItemExpanded="False" AutoGenerateColumns="False" DataKeyNames="FK_TAPolicyId,FK_EmployeeId,StartDate">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TAPolicyName" SortExpression="TAPolicyName" HeaderText="Policy Name"
                                            meta:resourcekey="GridBoundColumnResource1" />
                                        <telerik:GridBoundColumn DataField="StartDate" SortExpression="StartDate" HeaderText="Start Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource2" />
                                        <telerik:GridBoundColumn DataField="EndDate" SortExpression="EndDate" HeaderText="End Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource3" />
                                        <telerik:GridBoundColumn DataField="FK_TAPolicyId" AllowFiltering="false" Visible="false" />
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="false" Visible="false" />
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="OverTime Rules" ToolTip="OverTime Rules"
                meta:resourcekey="TabPanel3Resource1">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label1" runat="server" Text="Over Time Rule"
                                meta:resourcekey="Label1Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbOvertime" MarkFirstMatch="True" Skin="Vista" runat="server"
                                meta:resourcekey="RadCmbOvertimeResource1">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="grpOT"
                                runat="server" ControlToValidate="RadCmbOvertime" Display="None" ErrorMessage="Please select Overtime rule"
                                InitialValue="--Please Select--" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                            <a href="#" onclick="viewOverTimeRule(1)">
                                <asp:Literal ID="Literal2" runat="server" Text="View Details" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Text="Start date"
                                meta:resourcekey="Label2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpOTStartDate" runat="server" AllowCustomText="false"
                                ShowPopupOnFocus="True" MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista"
                                Culture="English (United States)" meta:resourcekey="dtpOTStartDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                    Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="grpOT"
                                runat="server" ControlToValidate="dtpOTStartDate" Display="None" ErrorMessage="Please enter start date"
                                meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckOvrTemporary" runat="server" AutoPostBack="True" Text="Temporary"
                                meta:resourcekey="Label3Resource1" />
                        </div>
                    </div>
                    <asp:Panel ID="PnlOTEnddate" runat="server" Visible="False" meta:resourcekey="PnlOTEnddateResource1">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server" Text="End date"
                                    meta:resourcekey="Label4Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpOTEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                                    meta:resourcekey="dtpOTEndDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:CompareValidator ID="Comparevalidator1" runat="server" ControlToValidate="dtpOTEndDate"
                                    ValidationGroup="grpOT" ControlToCompare="dtpOTStartDate" Display="None" Operator="GreaterThanEqual"
                                    ErrorMessage="To date should be greater than From date" meta:resourcekey="Comparevalidator1Resource1"></asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="Comparevalidator1" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnChangeOT" runat="server" Text="Change Overtime Rules" ValidationGroup="grpOT"
                                CssClass="button" meta:resourcekey="btnChangeOTResource1" />
                            </td>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid ID="grdVwOverTimeRule" runat="server" AllowSorting="True" AllowPaging="True"
                                PageSize="5" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                meta:resourcekey="grdVwOverTimeRuleResource1">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView IsFilterItemExpanded="False" AutoGenerateColumns="False" DataKeyNames="FK_RuleId,FK_EmployeeId,FromDate">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="RuleName" SortExpression="RuleName" HeaderText="Rule Name"
                                            meta:resourcekey="GridBoundColumnResource5" />
                                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate" HeaderText="From Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource6" />
                                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource7" />
                                        <telerik:GridBoundColumn DataField="FK_RuleId" AllowFiltering="false" Visible="false" />
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="false" Visible="false" />
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Employee Leaves" ToolTip="Employee Leaves"
                meta:resourcekey="TabPanel4Resource1">
                <ContentTemplate>
                    <uc2:EmpLeave_List ID="EmpLeave_List1" runat="server" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Employee Permissions" ToolTip="Employee Permissions"
                meta:resourcekey="TabPanel5Resource1">
                <ContentTemplate>
                    <uc3:EmpPermissions_List ID="EmpPermissions_List" runat="server" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel6" runat="server" HeaderText="Employee Schedule" meta:resourcekey="TabPanel6Resource1">
                <ContentTemplate>
                    <uc4:Emp_ActiveSchedule ID="EmpActiveSchedule" runat="server" />
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel7" runat="server" HeaderText="Employee Cards" meta:resourcekey="TabPanel7Resource1">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblExtraCardNo" runat="server" Text="Employee Card No." CssClass="Profiletitletxt"
                                meta:resourcekey="lblExtraCardNoResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <%-- <telerik:RadNumericTextBox ID="txtExtraCardNo" runat="server" MinValue="1" MaxValue="999999"
                                                Culture="English (United States)" LabelCssClass="">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>--%>
                            <asp:TextBox ID="txtExtraCardNo" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblFromDateExtra" runat="server" Text="From Date" CssClass="Profiletitletxt"
                                meta:resourcekey="lblFromDateExtraResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" ShowPopupOnFocus="True"
                                MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista" Culture="English (United States)"
                                meta:resourcekey="dtpOTStartDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                    Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="dtpFromDate"
                                Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="btnAdd"
                                meta:resourcekey="rfvFromDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceFromDate" runat="server" Enabled="True" TargetControlID="rfvFromDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblToDateExtra" runat="server" Text="To Date" CssClass="Profiletitletxt"
                                meta:resourcekey="lblToDateExtraResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" ShowPopupOnFocus="True"
                                MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista" Culture="English (United States)"
                                meta:resourcekey="dtpOTStartDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                    Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromDate" ControlToValidate="dtpToDate"
                                ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnAdd" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender ID="vceDate" TargetControlID="CVDate" runat="server"
                                Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" meta:resourcekey="lblIsActiveResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" ValidationGroup="btnAdd"
                                meta:resourcekey="btnAddResource1" />
                            <asp:Button ID="btnDeleteCard" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteCardResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid ID="dgrdEmpCards" runat="server" AllowSorting="True" AllowPaging="True"
                                PageSize="15" GridLines="None" ShowStatusBar="True"
                                AllowMultiRowSelection="True" ShowFooter="True">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView AllowFilteringByColumn="false" AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="CardId">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CardId" SortExpression="CardId" HeaderText="Employee Card No "
                                            meta:resourcekey="GridBoundColumnResource11" DataType="System.String" />
                                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate" HeaderText="From Date"
                                            DataFormatString="{0:d/MM/yyyy}" meta:resourcekey="GridBoundColumnResource12" />
                                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To Date"
                                            DataFormatString="{0:d/MM/yyyy}" meta:resourcekey="GridBoundColumnResource13" />
                                        <telerik:GridBoundColumn DataField="Active" SortExpression="Active" HeaderText="Active"
                                            meta:resourcekey="GridBoundColumnResource14" />
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="grpsave"
                    meta:resourcekey="btnSaveResource1" OnClick="btnSave_Click" /><%--onclientclick="return ShowPopUp('1')"--%>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:HiddenField runat="server" ID="hdnWindow" />
                <cc1:ModalPopupExtender ID="mpeSave" runat="server" PopupControlID="pnlPopup" TargetControlID="hdnWindow"
                    CancelControlID="btnNo" BackgroundCssClass="ModalBackground" DropShadow="true"
                    BehaviorID="6">
                </cc1:ModalPopupExtender>
                <div id="pnlPopup" class="commonPopup" style="display: none">

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblCheck" runat="server" Text="There is Already Active Card." CssClass="Profiletitletxt"
                                meta:resourcekey="lblCheckResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblConfirm" runat="server" Text="Are You Sure you want to set selected Card as Active?"
                                CssClass="Profiletitletxt" meta:resourcekey="lblConfirmResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button" meta:resourcekey="btnYesResource1" />
                            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="button" meta:resourcekey="btnNoResource1" />
                            <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" />--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
