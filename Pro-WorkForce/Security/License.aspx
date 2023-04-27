<%@ Page Title="Define User Groups" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="true"
    CodeFile="License.aspx.vb" Inherits="License" Culture="auto" UICulture="auto"
    meta:resourcekey="PageResource2" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function confirmDelete(gvName) {
            var TargetBaseControl = null;
            try {
                //get target base control.
                TargetBaseControl = document.getElementById(gvName);

            }
            catch (err) {
                TargetBaseControl = null;
            }

            if (TargetBaseControl == null) {
                ShowMessage("لا يوجد بيانات")
                return false;
            }

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            var TargetChildControl = "chkGroup";
            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].type == 'checkbox' && Inputs[n].checked && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    return confirm("هل أنت متأكد من الحذف؟");
                }

            }
            ShowMessage("الرجاء الاختيار من القائمة");
            return false;
        }



    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server"  HeaderText="License Details"/>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerShortName" runat="server" Text="Customer Short Name" CssClass="Profiletitletxt" meta:resourcekey="lblCustomerShortNameResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerShortName" runat="server" meta:resourcekey="txtCustomerShortNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustomerShortName"
                        ErrorMessage="Please Enter Customer Short Name" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" CssClass="Profiletitletxt" meta:resourcekey="lblCustomerNameResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerName" runat="server" meta:resourcekey="txtCustomerNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqArName" runat="server" ControlToValidate="txtCustomerName"
                        ErrorMessage="Please enter Customer Name" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="valReqArNameResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerArabicName" runat="server" Text="Customer Arabic Name" CssClass="Profiletitletxt" meta:resourcekey="lblCustomerArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerArabicName" runat="server" meta:resourcekey="txtCustomerArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCustomerArabicName"
                        ErrorMessage="Please enter Customer Arabic Name" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number" CssClass="Profiletitletxt" meta:resourcekey="lblPhoneNumberResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadNumericTextBox ID="txtPhoneNumber" runat="server" meta:resourcekey="txtPhoneNumberResource1" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px" >
                        <NegativeStyle Resize="None" />
                        <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhoneNumber"
                        ErrorMessage="Please enter Phone Number" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerCountry" runat="server" Text="Customer Country" CssClass="Profiletitletxt" meta:resourcekey="lblCustomerCountryResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerCountry" runat="server" meta:resourcekey="txtCustomerCountryResource1"
                       ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtCustomerCountry"
                        ErrorMessage="Please enter Country" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator14Resource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerCity" runat="server" Text="Customer City" CssClass="Profiletitletxt"
                        meta:resourcekey="lblGroupArNameResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerCity" runat="server" meta:resourcekey="txtCustomerCityResource1"
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCustomerCity"
                        ErrorMessage="Please enter City" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerAddress" runat="server" Text="Customer Address" CssClass="Profiletitletxt" meta:resourcekey="lblCustomerAddressResource1"
                        ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerAddress" runat="server" meta:resourcekey="txtCustomerAddressResource1"
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCustomerAddress"
                        ErrorMessage="Please enter Customer Address" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerGPS" runat="server" Text="Customer GPS Coordinates" CssClass="Profiletitletxt" meta:resourcekey="lblCustomerGPSResource1"
                       ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCustomerGPS" runat="server" meta:resourcekey="txtCustomerGPSResource1"
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCustomerGPS"
                        ErrorMessage="Please enter Customer GPS Coordinates" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
               
                <div class="col-md-3">
                    <asp:Label ID="lblProjectManager" runat="server" Text="Project Manager" CssClass="Profiletitletxt" meta:resourcekey="lblProjectManagerResource1"
                        ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtProjectManager" runat="server" meta:resourcekey="txtProjectManagerResource1"
                       ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtProjectManager"
                        ErrorMessage="Please enter Project Manager Name" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-md-3">
                    <asp:Label ID="lblImpEngg" runat="server" Text="Implementation Engineer" CssClass="Profiletitletxt" meta:resourcekey="lblImpEnggResource1"
                       ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtImpEngg" runat="server" meta:resourcekey="txtImpEnggResource1"
                       ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtImpEngg"
                        ErrorMessage="Please enter Implementation Engineer Name" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="row">
               
                <div class="col-md-3">
                    <asp:Label ID="lblSuppEngg" runat="server" Text="Support Engineer" CssClass="Profiletitletxt" meta:resourcekey="lblSuppEnggResource1"
                        ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtSuppEngg" runat="server" meta:resourcekey="txtSuppEnggResource1"
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSuppEngg"
                        ErrorMessage="Please enter Support Engineer Name" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-md-3">
                    <asp:Label ID="lblIntegEngg" runat="server" Text="Integration Engineer" CssClass="Profiletitletxt" meta:resourcekey="lblIntegEnggResource1"
                       ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIntegEngg" runat="server" meta:resourcekey="txtIntegEnggResource1"
                        ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtIntegEngg"
                        ErrorMessage="Please enter Integration Engineer Name" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="row">
               
                <div class="col-md-3">
                    <asp:Label ID="lblMACAddress" runat="server" CssClass="Profiletitletxt" Text="Server MAC Address/ Key" meta:resourcekey="lblMACAddressResource1"
                       ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtMACAddress" runat="server" meta:resourcekey="txtMACAddressResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqEnName" runat="server" ControlToValidate="txtMACAddress"
                        ErrorMessage="Please enter Mac Address" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="valReqEnNameResource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblPackage" runat="server" CssClass="Profiletitletxt"
                        Text="Package" meta:resourcekey="lblPackageResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="ddlpackage" runat="server" MarkFirstMatch="True"
                        OnSelectedIndexChanged="ddlpackage_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlpackageResource1">
                        
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvddlPackage" runat="server" ControlToValidate="ddlpackage" 
                         ValidationGroup="VGGroups" InitialValue="--Please Select--" ErrorMessage="Please Select a Package" Text="*" meta:resourcekey="rfvddlPackageResource1"></asp:RequiredFieldValidator>
                   
                </div>
                 <div class="col-md-3">
                    <asp:Label ID="lblNoOfUsers" runat="server" Text="Number Of Users" CssClass="Profiletitletxt" meta:resourcekey="lblNoOfUsersResource1"
                        ></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadNumericTextBox ID="txtNoOfUsers" runat="server" Culture="en-US" meta:resourcekey="txtNoOfUsersResource1" DbValueFactor="1" LabelCssClass="" LabelWidth="64px">
                        <NegativeStyle Resize="None" />
                  <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNoOfUsers"
                        ErrorMessage="Please enter No of Users" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblStartDate" runat="server" CssClass="Profiletitletxt"
                        Text="Start date" meta:resourcekey="lblStartDateResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadDatePicker ID="dpStartDate" runat="server" Culture="en-US" meta:resourcekey="dpStartDateResource1">
                        <Calendar EnableWeekends="True" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                        </Calendar>
                        <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="64px" Width="" >
                            <EmptyMessageStyle Resize="None" />
                            <ReadOnlyStyle Resize="None" />
                            <FocusedStyle Resize="None" />
                            <DisabledStyle Resize="None" />
                            <InvalidStyle Resize="None" />
                            <HoveredStyle Resize="None" />
                            <EnabledStyle Resize="None" />
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="dpStartDate"
                        ErrorMessage="Please Select a Start Date" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator15Resource1" ></asp:RequiredFieldValidator>
                </div>
                  <div class="col-md-3">
                    <asp:Label ID="lblNoOfReaders" runat="server" Text="Number Of Readers" CssClass="Profiletitletxt" meta:resourcekey="lblNoOfReadersResource1"
                        ></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadNumericTextBox ID="txtNoOfReaders" runat="server"  meta:resourcekey="txtNoOfReadersResource1" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px">
                        <NegativeStyle Resize="None" />
                       <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNoOfReaders"
                        ErrorMessage="Please enter No of Readers" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator12Resource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt"
                        Text="Support End Date" meta:resourcekey="lblEndDateResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadDatePicker ID="dpEndDate" runat="server" Culture="en-US" meta:resourcekey="dpEndDateResource1">
                        <Calendar EnableWeekends="True" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                        </Calendar>
                        <DateInput DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy" LabelWidth="64px" Width="" >
                            <EmptyMessageStyle Resize="None" />
                            <ReadOnlyStyle Resize="None" />
                            <FocusedStyle Resize="None" />
                            <DisabledStyle Resize="None" />
                            <InvalidStyle Resize="None" />
                            <HoveredStyle Resize="None" />
                            <EnabledStyle Resize="None" />
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="dpEndDate"
                        ErrorMessage="Please Select an End Date" ValidationGroup="VGGroups"
                         Text="*" meta:resourcekey="RequiredFieldValidator16Resource1" ></asp:RequiredFieldValidator>
                </div>
                 <div class="col-md-3">
                    <asp:Label ID="lblNoOfEmployees" runat="server" Text="Number Of Employees" CssClass="Profiletitletxt" meta:resourcekey="lblNoOfEmployeesResource1"
                        ></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadNumericTextBox ID="txtNoOfEmployees" runat="server"  meta:resourcekey="txtNoOfEmployeesResource1" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px">
                        <NegativeStyle Resize="None" />
                     <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNoOfEmployees"
                        ErrorMessage="Please enter No of Employees" ValidationGroup="VGGroups"
                        Text="*" meta:resourcekey="RequiredFieldValidator13Resource1"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnCreate" runat="server" CssClass="button" Text="Create" ValidationGroup="VGGroups"
                        Visible="False" meta:resourcekey="btnCreateResource1" />
                </div>
            </div>
            <asp:Panel ID="pnlPermissions" runat="server" meta:resourcekey="pnlPermissionsResource1">
                <div class="row">
                    <div class="col-md-12" id="CtlTab" runat="server">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-21 text-center">
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="VGGroups" Text="Save" CssClass="button" meta:resourcekey="btnSaveResource1"
                            />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-8">
                    <asp:ValidationSummary ID="valSunRole" runat="server" Style="width: 30%; text-align: left"
                        ValidationGroup="VGGroups" meta:resourcekey="valSunRoleResource1"
                        ShowMessageBox="True" ShowSummary="False" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="gvGroups" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" PageSize="15" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="gvGroupsResource1" AutoGenerateColumns="False" CellSpacing="0">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="CustomerId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkGroup" runat="server" Text="&nbsp;" meta:resourcekey="chkGroupResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CustomerShortName" AllowFiltering="False" UniqueName="CustomerShortName"
                                    HeaderText="Customer Short Name" SortExpression="CustomerShortName" FilterControlAltText="Filter CustomerShortName column" meta:resourcekey="GridBoundColumnResource1"
                                     />
                                <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="False" UniqueName="CustomerName"
                                    HeaderText="Customer Name" SortExpression="CustomerName" FilterControlAltText="Filter CustomerName column" meta:resourcekey="GridBoundColumnResource2"
                                    />
                                 <telerik:GridBoundColumn DataField="CustomerArabicName" AllowFiltering="False" UniqueName="CustomerArabicName"
                                    HeaderText="Customer Arabic Name" SortExpression="CustomerArabicName" FilterControlAltText="Filter CustomerArabicName column" meta:resourcekey="GridBoundColumnResource3"
                                    visible="False" />
                                  <telerik:GridBoundColumn DataField="StartDate" AllowFiltering="False" UniqueName="StartDate"
                                    HeaderText="Support Start Date" SortExpression="StartDate" FilterControlAltText="Filter StartDate column"  meta:resourcekey="GridBoundColumnResource12"
                                    />
                                <telerik:GridDateTimeColumn  DataField="SupportEndDate" AllowFiltering="False" UniqueName="SupportEndDate"
                                    HeaderText="Support End Date" SortExpression="SupportEndDate" FilterControlAltText="Filter SupportEndDate column" meta:resourcekey="GridBoundColumnResource13"
                                    />
                                <telerik:GridBoundColumn DataField="PhoneNumber" AllowFiltering="False" UniqueName="PhoneNumber"
                                    HeaderText="Phone Number" SortExpression="PhoneNumber" FilterControlAltText="Filter PhoneNumber column" meta:resourcekey="GridBoundColumnResource4"
                                    Visible="False"  />
                                 <telerik:GridBoundColumn DataField="CustomerCity" AllowFiltering="False" UniqueName="CustomerCity"
                                    HeaderText="Customer City" SortExpression="CustomerCity" FilterControlAltText="Filter CustomerCity column" meta:resourcekey="GridBoundColumnResource5"
                                    Visible="False" />
                                <telerik:GridBoundColumn DataField="CustomerAddress" AllowFiltering="False" UniqueName="CustomerAddress"
                                    HeaderText="Customer Address" SortExpression="CustomerAddress" FilterControlAltText="Filter CustomerAddress column" meta:resourcekey="GridBoundColumnResource6"
                                    />
                                 <telerik:GridBoundColumn DataField="CustomerGPSCoordinates" AllowFiltering="False" UniqueName="CustomerGPSCoordinates"
                                    HeaderText="Customer GPS Coordinates" SortExpression="CustomerGPSCoordinates" FilterControlAltText="Filter CustomerGPSCoordinates column" meta:resourcekey="GridBoundColumnResource7"
                                     Visible="False" />
                                <telerik:GridBoundColumn DataField="NoOfUsers" AllowFiltering="False" UniqueName="NoOfUsers"
                                    HeaderText="No: Of Users" SortExpression="NoOfUsers" FilterControlAltText="Filter NoOfUsers column" meta:resourcekey="GridBoundColumnResource8"
                                    />
                                 <telerik:GridBoundColumn DataField="NoOfReaders" AllowFiltering="False" UniqueName="NoOfReaders"
                                    HeaderText="No: Of Readers" SortExpression="NoOfReaders" FilterControlAltText="Filter NoOfReaders column" meta:resourcekey="GridBoundColumnResource9"
                                    />
                                <telerik:GridBoundColumn DataField="NoOfEmployees" AllowFiltering="False" UniqueName="NoOfEmployees"
                                    HeaderText="No: Of Employees" SortExpression="NoOfEmployees" FilterControlAltText="Filter NoOfEmployees column" meta:resourcekey="GridBoundColumnResource10"
                                    />
                                <telerik:GridBoundColumn DataField="Package" AllowFiltering="False" UniqueName="Package"
                                    HeaderText="Package" SortExpression="Package" FilterControlAltText="Filter Package column" meta:resourcekey="GridBoundColumnResource11"
                                    />
                              
                                <telerik:GridBoundColumn DataField="CustomerId" AllowFiltering="False"
                                    Visible="False" 
                                    UniqueName="CustomerId" FilterControlAltText="Filter CustomerId column" meta:resourcekey="GridBoundColumnResource14" />
                            </Columns>
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
            <asp:HiddenField ID="hdnid" runat="server" Value="0" />
            <asp:HiddenField ID="hdnsortDir" runat="server" Value="ASC" />
            <asp:HiddenField ID="hdnsortExp" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
