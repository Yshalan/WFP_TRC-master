<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ManualEntryMassUpdate_WithReasons.aspx.vb" Inherits="DailyTasks_ManualEntryMassUpdate_WithReasons"
    Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 80px;
        }

        .customValidator {
            width: 100%;
            background-color: #ffdfb1;
            display: block;
            color: #222;
            padding: 10px;
            margin: 5px 0;
            border: 1px solid #222;
        }
    </style>
    <script type="text/javascript" language="javascript">


        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

    </script>
    <asp:UpdatePanel ID="pnlEmployee" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt"
                        Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" RenderMode="Native" AutoPostBack="True"
                        Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True" RenderMode="Native">
                        </Calendar>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                            Width="" AutoPostBack="True" LabelWidth="64px">
                            <EmptyMessageStyle Resize="None" />
                            <ReadOnlyStyle Resize="None" />
                            <FocusedStyle Resize="None" />
                            <DisabledStyle Resize="None" />
                            <InvalidStyle Resize="None" />
                            <HoveredStyle Resize="None" />
                            <EnabledStyle Resize="None" />
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today"
                        ValidationGroup="ReasonValidation" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt"
                        Text="Company" meta:resourcekey="lblCompanyResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
                        MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="EmpPermissionGroup" meta:resourcekey="RadCmbBxCompaniesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                        Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company"
                        ValidationGroup="ReasonValidation" meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                        OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Students" meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                        <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" CssClass="checkboxlist"
                            DataTextField="EmployeeName" DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False"
                        Text="View Org Level Employees " meta:resourcekey="hlViewEmployeeResource1"></asp:HyperLink>
                </div>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-1">
                            <i class="fa fa-user" aria-hidden="true" style="font-size: 32px;"></i>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblTotalEmp" runat="server" Text="Total Student(s):" meta:resourcekey="lblTotalEmpResource1"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblTotalEmpVal" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <i class="fa fa-user-plus" aria-hidden="true" style="font-size: 32px;"></i>

                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblAttendEmp" runat="server" Text="Attend Student(s):" meta:resourcekey="lblAttendEmpResource1"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblAttendEmpVal" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <i class="fa fa-user-times" aria-hidden="true" style="font-size: 32px;"></i>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblAbsentEmp" runat="server" Text="Absent Student(s):" meta:resourcekey="lblAbsentEmpResource1"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblAbsentEmpVal" runat="server"></asp:Label>
                        </div>
                    </div>


                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CustomValidator ID="cvEmpListValidation" ErrorMessage="please select at least one employee"
                        ValidationGroup="ReasonValidation" ForeColor="Black" runat="server" CssClass="customValidator" meta:resourcekey="cvEmpListValidationResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-10">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("PageNo") %>' meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>|
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource1">Reason</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                        DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlReasonResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                        InitialValue="--Please Select--" ErrorMessage="Please Select Reason\Type" ControlToValidate="ddlReason"
                        ValidationGroup="ReasonValidation" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator4">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt" meta:resourcekey="lblRemarksResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine"
                        MaxLength="100" meta:resourcekey="txtremarksResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="None" ControlToValidate="txtremarks" ID="RegularExpressionValidator2"
                        ValidationExpression="^[\s\S]{0,100}$" runat="server" ErrorMessage="Maximum 100 characters allowed."
                        ValidationGroup="ReasonValidation" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="RegularExpressionValidator2">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server"
                        ValidationGroup="ReasonValidation" OnClientClick="Page_ClientValidate();" meta:resourcekey="btnSaveResource1"></asp:Button>
                    <asp:Button ID="ibtnDelete" CssClass="button" Text="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"
                        CausesValidation="False" Visible="False" meta:resourcekey="ibtnDeleteResource1"></asp:Button>
                    <asp:Button ID="ibtnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="False" meta:resourcekey="ibtnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgEmpAtt" Skin="Hay"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                        <ContextMenu FeatureGroupID="rfContextMenu">
                        </ContextMenu>
                    </telerik:RadFilter>
                    <telerik:RadGrid runat="server" ID="dgEmpAtt" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" AllowSorting="True" GridLines="None"
                        Width="100%" CellSpacing="0" meta:resourcekey="dgEmpAttResource1">
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="moveid" SortExpression="Emp_no" HeaderText="Emp_no"
                                    Visible="False" UniqueName="moveid" FilterControlAltText="Filter moveid column" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="name" SortExpression="name" HeaderText="Name"
                                    UniqueName="name" FilterControlAltText="Filter name column" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                    UniqueName="ReasonName" FilterControlAltText="Filter ReasonName column" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="MoveDate" SortExpression="M_DATE" HeaderText="Date"
                                    DataFormatString="{0:dd/MM/yyyy}" UniqueName="MoveDate" FilterControlAltText="Filter MoveDate column" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="MoveTime" SortExpression="M_Time" HeaderText="Time"
                                    DataFormatString="{0:HH:mm}" UniqueName="MoveTime" FilterControlAltText="Filter MoveTime column" meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                    UniqueName="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource6" />
                            </Columns>
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server"
                                            Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlEmployee">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/loading.gif" meta:resourcekey="imgLoadingResource1" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

