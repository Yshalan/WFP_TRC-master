<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Employee.ascx.vb" Inherits="Employee_WebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<table width="100%">
    <tr>
        <td>
            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employee Leaves" />
        </td>
    </tr>
    <tr>
        <td>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%"
                CssClass="Tab" AutoPostBack="True" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Employee info." ToolTip="Employee Information"
                    meta:resourcekey="TabPanel1Resource1">
                    <HeaderTemplate>
                        Employee info.
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNumber" runat="server" Text="Number"
                                        meta:resourcekey="lblNumberResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmployeeNumber" runat="server" meta:resourcekey="txtEmployeeNumberResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqEmployeeNumber" runat="server" ControlToValidate="txtEmployeeNumber"
                                        Display="None" ErrorMessage="Please enter employee number" ValidationGroup="EmployeeGroup"
                                        meta:resourcekey="reqEmployeeNumberResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderEmployeeNo" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmployeeNumber" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblStatus" runat="server" Text="Status"
                                        meta:resourcekey="lblStatusResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbStatus" runat="server" AppendDataBoundItems="True"
                                        MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbStatusResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" Value="0" meta:resourcekey="RadComboBoxItemResource1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqEmpStatus" runat="server" ControlToValidate="RadCmbStatus"
                                        Display="None" ErrorMessage="Please select status name" InitialValue="--Please Select--"
                                        ValidationGroup="EmployeeGroup" meta:resourcekey="reqEmpStatusResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEmpStatus" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmpStatus" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEnglishName" runat="server" Text="English Name"
                                        meta:resourcekey="lblEnglishNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEnglishName" runat="server" meta:resourcekey="txtEnglishNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqEnglishName" runat="server" ControlToValidate="txtEnglishName"
                                        Display="None" ErrorMessage="Please enter english name" ValidationGroup="EmployeeGroup"
                                        meta:resourcekey="reqEnglishNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEnglishName" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEnglishName" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblArabicName" runat="server" Text="Arabic Name"
                                        meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtArabicName" runat="server" meta:resourcekey="txtArabicNameResource1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblOrgCompany" runat="server" Text="Company"
                                        meta:resourcekey="lblOrgCompanyResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbOrgCompany" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" MarkFirstMatch="True" OnSelectedIndexChanged="RadCmbOrgCompany_SelectedIndexChanged"
                                        Skin="Vista" meta:resourcekey="RadCmbOrgCompanyResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqOrgCompany" runat="server" ControlToValidate="RadCmbOrgCompany"
                                        Display="None" ErrorMessage="Please select company" InitialValue="--Please Select--"
                                        ValidationGroup="EmployeeGroup" meta:resourcekey="reqOrgCompanyResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqOrgCompany" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqOrgCompany" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEntity" runat="server" Text="Entity"
                                        meta:resourcekey="lblEntityResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbEntity" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbEntityResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqEmpEntity" runat="server" ControlToValidate="RadCmbEntity"
                                        Display="None" ErrorMessage="Please select entity name" InitialValue="--Please Select--"
                                        ValidationGroup="EmployeeGroup" meta:resourcekey="reqEmpEntityResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEmpEntity" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmpEntity" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblGender" runat="server" Text="Gender"
                                        meta:resourcekey="lblGenderResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:RadioButton ID="RadioBtnMale" runat="server" Checked="True" GroupName="EmpGender"
                                        Text="Male" meta:resourcekey="RadioBtnMaleResource1" />
                                    <asp:RadioButton ID="RadioBtnFemale" runat="server" GroupName="EmpGender" Text="Female"
                                        meta:resourcekey="RadioBtnFemaleResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblDob" runat="server" Text="Birth Date"
                                        meta:resourcekey="lblDobResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dtpBirthDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" AutoPostBack="true"
                                        meta:resourcekey="dtpBirthDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblAge" runat="server" Text="Age" meta:resourcekey="lblAgeResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAge" runat="server" meta:resourcekey="txtAgeResource1" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                        Display="None" ErrorMessage="Please enter age" ValidationGroup="EmployeeGroup"
                                        meta:resourcekey="rfvAgeResource1" />
                                    <cc1:ValidatorCalloutExtender ID="vceAge" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvAge" WarningIconImageUrl="~/images/warning1.png" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmailAddress" runat="server" Text="E-mail Address"
                                        meta:resourcekey="lblEmailAddressResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailAddress" runat="server" meta:resourcekey="txtEmailAddressResource1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                        Display="None" ErrorMessage="invalid e-mail address format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="EmployeeGroup" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNationality" runat="server" Text="Nationality"
                                        meta:resourcekey="lblNationalityResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbNationality" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbNationalityResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblReligion" runat="server" Text="Religion"
                                        meta:resourcekey="lblReligionResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbReligion" runat="server" MarkFirstMatch="True" Skin="Vista"
                                        meta:resourcekey="RadCmbReligionResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblMaritalStatus" runat="server" Text="Marital Status"
                                        meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbMaritalStatus" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbMaritalStatusResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblWorkLocation" runat="server" Text="Work Location"
                                        meta:resourcekey="lblWorkLocationResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbWorkLocation" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbWorkLocationResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblGrade" runat="server" Text="Grade" meta:resourcekey="lblGradeResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbGrade" runat="server" MarkFirstMatch="True" Skin="Vista"
                                        meta:resourcekey="RadCmbGradeResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblDesignation" runat="server" Text="Designation"
                                        meta:resourcekey="lblDesignationResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbDesignation" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbDesignationResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblLogicalGroup" runat="server" Text="Logical Group"
                                        meta:resourcekey="lblLogicalGroupResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbLogicalGroup" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbLogicalGroupResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblAnnualLeaveBalance" runat="server" Text="Annual leave balance"
                                        meta:resourcekey="lblAnnualLeaveBalanceResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="RadtxtAnnualBalance" runat="server" AllowRounding="true"
                                        Culture="English (United States)" DecimalDigits="2" GroupSeparator="" KeepNotRoundedValue="false"
                                        LabelCssClass="" MaxValue="9999" MinValue="0" Skin="Vista" meta:resourcekey="RadtxtAnnualBalanceResource1">
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblJoinDate" runat="server" Text="Join Date"
                                        meta:resourcekey="lblJoinDateResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dtpJoinDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                        meta:resourcekey="dtpJoinDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                        meta:resourcekey="lblRemarksResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TA Policy" ToolTip="Time Attendance Policy"
                    meta:resourcekey="TabPanel2Resource1">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTaPolicy" runat="server" Text="TA Policy"
                                        meta:resourcekey="lblTaPolicyResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbPolicies" MarkFirstMatch="True" Skin="Vista" runat="server"
                                        Enabled="False" meta:resourcekey="RadCmbPoliciesResource1">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lStartDate" runat="server" Text="Start date"
                                        meta:resourcekey="lStartDateResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dtpStartDate" runat="server" AllowCustomText="false" ShowPopupOnFocus="True"
                                        MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista" Culture="English (United States)"
                                        meta:resourcekey="dtpStartDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTemporary" runat="server" Text="Temporary"
                                        meta:resourcekey="lblTemporaryResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" meta:resourcekey="chckTemporaryResource1" />
                                </td>
                            </tr>
                            <asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
                                <tr>
                                    <td>
                                        <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="End date"
                                            meta:resourcekey="lblEndDateResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                            MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                            meta:resourcekey="dtpEndDateResource1">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                                Width="">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnChangeTaPolicy" runat="server" Text="Change TA Policy" CssClass="button"
                                        Enabled="False" Width="150px" meta:resourcekey="btnChangeTaPolicyResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:GridView ID="grdVwTaPolicy" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CssClass="GridViewStyle" PageSize="5" Width="650px" meta:resourcekey="grdVwTaPolicyResource1">
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="TAPolicyName" HeaderText="Policy Name" SortExpression="TAPolicyName"
                                                meta:resourcekey="BoundFieldResource1" />
                                            <asp:BoundField DataField="StartDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date"
                                                SortExpression="StartDate" meta:resourcekey="BoundFieldResource2" />
                                            <asp:BoundField DataField="EndDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="End Date"
                                                SortExpression="EndDate" meta:resourcekey="BoundFieldResource3" />
                                            <asp:BoundField DataField="FK_TAPolicyId" Visible="False" meta:resourcekey="BoundFieldResource4" />
                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" meta:resourcekey="lnkDeleteResource1"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <RowStyle CssClass="RowStyle" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="OverTime Rules" ToolTip="OverTime Rules"
                    meta:resourcekey="TabPanel3Resource1">
                </cc1:TabPanel>
            </cc1:TabContainer>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmployeeGroup"
                meta:resourcekey="btnSaveResource1" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
        </td>
    </tr>
    <tr align="center">
        <td>
            <asp:GridView ID="grdVwEmployees" runat="server" AllowPaging="True" AllowSorting="True" PageSize="25"
                AutoGenerateColumns="False" CssClass="GridViewStyle" meta:resourcekey="grdVwEmployeesResource1">
                <RowStyle CssClass="RowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkRow" runat="server" meta:resourcekey="chkRowResource1" /></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="English Name" SortExpression="EmployeeName" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEmployeeName" runat="server" Text='<%# Bind("EmployeeName") %>'
                                OnClick="lnkEmployeeName_Click" meta:resourcekey="lnkEmployeeNameResource1"></asp:LinkButton></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmployeeArabicName" HeaderText="Arabic Name" SortExpression="EmployeeArabicName"
                        meta:resourcekey="BoundFieldResource5" />
                    <asp:BoundField DataField="EmployeeNo" HeaderText="Employee No." SortExpression="EmployeeNo"
                        meta:resourcekey="BoundFieldResource6" />
                    <asp:TemplateField Visible="False" meta:resourcekey="TemplateFieldResource4">
                        <ItemTemplate>
                            <asp:Label CssClass="Profiletitletxt" ID="lblEmployeeId" runat="server" Text='<%# Bind("EmployeeId") %>'
                                meta:resourcekey="lblEmployeeIdResource1"></asp:Label></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="HeaderStyle" />
                <PagerStyle CssClass="PagerStyle" />
            </asp:GridView>
        </td>
    </tr>
</table>
