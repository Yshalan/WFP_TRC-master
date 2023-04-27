<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Designation.aspx.vb" Inherits="Admin_Emp_Designation" Title="Define Designations" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
        function openRadWin() {

            var cmbvalue = $find('<%=CmbOvertimeRule.ClientID%>')._value;
            if (cmbvalue > 0) {
                oWindow = radopen("OvertimeRulesPopUp.aspx?RuleId=" + cmbvalue, "RadWindow1");
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
        //function CheckIsAnual(chk)
        //{
        //var x= $find("<%= dgrdLeavesTypes.ClientID %>").get_masterTableView().get_element().tBodies[0].rows.length;
        //var gridView = document.getElementById('<%= dgrdLeavesTypes.ClientID %>'); 
        //var RowIndex =chk.parentNode.parentNode.parentNode.rowIndex;
        // var cell = gridView.rows[RowIndex].cells[7];  
        // var AnualChk = cell.childNodes[0].childNodes[0];    
        //    if(chk.checked==true)
        //    {
        //    if (AnualChk.checked==true)
        //{
        //        var i=0;
        //        for(i=1;i<gridView.rows.length ;i++)
        //        {
        //        if(i!=RowIndex)
        //        {
        //        if(gridView.rows[i].cells[7].childNodes[0].childNodes[0].checked==true )
        //        {
        //        gridView.rows[i].cells[0].childNodes[0].childNodes[0].checked=false ;
        //        }
        //        }
        //        }
        //}

        //}

        // 
        //}

        function CheckIsAnual(chk) {
            var gridView = $find("<%= dgrdLeavesTypes.ClientID %>").get_masterTableView().get_element().tBodies[0];
            if (chk.checked == true) {
                var Row_Index = chk.parentNode.parentNode.parentNode.rowIndex;
                if (chk.parentNode.parentNode.parentNode.childNodes[7].getElementsByTagName("input")(0).checked == true) {
                    for (i = 0; i < gridView.rows.length; i++) {
                        if (i + 1 != Row_Index) {
                            if (gridView.rows[i].cells[7].getElementsByTagName("input")(0).checked == true) {
                                gridView.rows[i].cells[0].getElementsByTagName("input")(0).checked = false;
                            }
                        }
                    }
                }
            }


        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeader1" runat="server" />


            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
                OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Designation "
                    meta:resourcekey="Tab1Resource1">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt"
                                    Text="Designation Code" meta:resourcekey="Label1Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtDesignationCode" runat="server"
                                    meta:resourcekey="TxtDesignationCodeResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqDesignationCode" runat="server" ControlToValidate="TxtDesignationCode"
                                    Display="None" ErrorMessage="Please Enter a Designation Code"
                                    ValidationGroup="Grp1" meta:resourcekey="reqDesignationCodeResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreDesignationCode" runat="server" Enabled="True"
                                    TargetControlID="reqDesignationCode" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDesignationName" runat="server" CssClass="Profiletitletxt"
                                    Text="English Name" meta:resourcekey="lblDesignationNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtDesignationName" runat="server"
                                    meta:resourcekey="TxtDesignationNameResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqDesignationName" runat="server" ControlToValidate="TxtDesignationName"
                                    Display="None" ErrorMessage="Please Enter a Grade Name"
                                    ValidationGroup="Grp1" meta:resourcekey="reqDesignationNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreqDesignationName" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="reqDesignationName" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt"
                                    Text="ArabicName" meta:resourcekey="lblArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtArabicName" runat="server"
                                    meta:resourcekey="txtArabicNameResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqDesigArName" runat="server" ControlToValidate="txtArabicName"
                                    Display="None" ErrorMessage="Please enter arabic grade name"
                                    ValidationGroup="Grp1" meta:resourcekey="reqDesigArNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderDesigArName" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="reqDesigArName" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%--<tr id="trAnnualLeaveBalance" runat="server">
                                                        <td runat="server">
                                                            <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Annual Leave Balance" meta:resourcekey="Label3Resource1"></asp:Label>
                                                        </td>
                                                        <td runat="server">
                                                            <telerik:RadNumericTextBox ID="TxtAnnualLeaveBalance" MinValue="0" MaxValue="365"
                                                                Skin="Vista" runat="server" Culture="English (United States)" LabelCssClass=""
                                                                Width="200px">
                                                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                            </telerik:RadNumericTextBox>
                                                        </td>
                                                        <td runat="server">
                                                            &nbsp;
                                                        </td>
                                                    </tr>--%>
                        <div class="row" id="trOverTimeRule" runat="server">
                            <div class="col-md-2">
                                <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Overtime Rule Name" meta:resourcekey="Label4Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="CmbOvertimeRule" runat="server" MarkFirstMatch="True" Skin="Vista">
                                </telerik:RadComboBox>
                                <a href="#" onclick="openRadWin();">
                                    <asp:Literal ID="Literal1"
                                        runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
                                    EnableShadow="True" InitialBehavior="None">
                                    <Windows>
                                        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
                                            Behaviors="Close, Move" EnableShadow="True" Height="450px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                                            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                                            Skin="Windows7" Width="700px">
                                        </telerik:RadWindow>
                                    </Windows>
                                </telerik:RadWindowManager>

                                <%-- <asp:RequiredFieldValidator ID="reqOvertimeRule" runat="server" ControlToValidate="CmbOvertimeRule" ValidationGroup="Grp1"
                                    Display="None" ErrorMessage="Please Select a Overtime Rule Name" InitialValue="--Please Select--"></asp:RequiredFieldValidator>

                                <cc1:ValidatorCalloutExtender ID="ExtenderreqOvertimeRule" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="reqOvertimeRule" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="ChkIsTAException" runat="server"
                                    Text="TA Exception" meta:resourcekey="lblTaExceptionResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab2" HeaderText="Leave Types" runat="server"
                    meta:resourcekey="Tab2Resource1" Visible="false">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label44" runat="server" CssClass="Profiletitletxt"
                                    Text="Leave Type" meta:resourcekey="Label44Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLeaveTypes" runat="server"
                                    meta:resourcekey="ddlLeaveTypesResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLeaveTypes"
                                    Display="None" ErrorMessage="Please Select Leave Type" InitialValue="--Please Select--"
                                    ValidationGroup="groupAdd"
                                    meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator1" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button"
                                    ValidationGroup="groupAdd" meta:resourcekey="btnAddResource1" />
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove"
                                    meta:resourcekey="btnRemoveResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadGrid ID="dgrdLeavesTypes" runat="server" PageSize="15"
                                    AllowPaging="True" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                                    AllowMultiRowSelection="True" meta:resourcekey="dgrdLeavesTypesResource1">
                                    <SelectedItemStyle ForeColor="Maroon" />
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="LeaveID">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False"
                                                meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="LeaveName" SortExpression="LeaveName" HeaderText="Leave Name"
                                                UniqueName="LeaveName" meta:resourcekey="GridBoundColumnResource1" />
                                            <telerik:GridBoundColumn DataField="LeaveArabicName" SortExpression="LeaveArabicName"
                                                HeaderText="Arabic Name" UniqueName="LeaveArabicName"
                                                meta:resourcekey="GridBoundColumnResource2" />
                                            <telerik:GridBoundColumn DataField="Balance" SortExpression="Balance" HeaderText="Balance"
                                                UniqueName="Balance" meta:resourcekey="GridBoundColumnResource3" />
                                            <telerik:GridBoundColumn DataField="MonthlyBalancing" SortExpression="MonthlyBalancing"
                                                HeaderText="Monthly Balancing" UniqueName="MonthlyBalancing"
                                                meta:resourcekey="GridBoundColumnResource4" />
                                            <telerik:GridBoundColumn DataField="MinDuration" SortExpression="MinDuration" HeaderText="Min Duration"
                                                UniqueName="MinDuration" meta:resourcekey="GridBoundColumnResource5" />
                                            <telerik:GridBoundColumn DataField="MaxDuration" SortExpression="MaxDuration" HeaderText="Max Duration"
                                                UniqueName="MaxDuration" meta:resourcekey="GridBoundColumnResource6" />
                                            <telerik:GridCheckBoxColumn DataField="IsAnnual" SortExpression="IsAnnual" HeaderText="Is Annual"
                                                UniqueName="IsAnnual" meta:resourcekey="GridCheckBoxColumnResource1" ItemStyle-CssClass="nocheckboxstyle" />
                                            <telerik:GridBoundColumn DataField="LeaveID" SortExpression="LeaveID" HeaderText="LeaveID"
                                                Visible="False" UniqueName="LeaveID"
                                                meta:resourcekey="GridBoundColumnResource7" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
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
                    <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button"
                        ValidationGroup="Grp1" meta:resourcekey="ibtnSaveResource1" />
                    <%-- OnClientClick="javascript:return ValidatePage();" --%>
                    <asp:Button ID="ibtnDelete" runat="server" OnClientClick="return ValidateDelete();" Text="Delete" CssClass="button"
                        meta:resourcekey="ibtnDeleteResource1" />
                    <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button"
                        meta:resourcekey="ibtnRestResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmp_Designation"
                        Skin="Hay" ShowApplyButton="False"
                        meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmp_Designation" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        OnItemCommand="dgrdEmp_Designation_ItemCommand" ShowFooter="True" PageSize="15"
                        meta:resourcekey="dgrdEmp_DesignationResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="DesignationId,DesignationCode"
                            AutoGenerateColumns="False" CommandItemDisplay="Top">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    meta:resourcekey="GridTemplateColumnResource2" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DesignationCode" SortExpression="DesignationCode"
                                    HeaderText=" Code" meta:resourcekey="GridBoundColumnResource8"
                                    UniqueName="DesignationCode" />
                                <telerik:GridBoundColumn DataField="DesignationName" SortExpression="DesignationName"
                                    HeaderText="Designation English Name"
                                    meta:resourcekey="GridBoundColumnResource9" UniqueName="DesignationName" />
                                <telerik:GridBoundColumn DataField="DesignationArabicName" SortExpression="DesignationArabicName"
                                    HeaderText="Designation Arabic Name"
                                    meta:resourcekey="GridBoundColumnResource10"
                                    UniqueName="DesignationArabicName" />
                                <telerik:GridBoundColumn DataField="AnnualLeaveBalance" SortExpression="AnnualLeaveBalance"
                                    AllowFiltering="False" HeaderText="Annual Leave Balance"
                                    meta:resourcekey="GridBoundColumnResource11" UniqueName="AnnualLeaveBalance" />
                                <telerik:GridBoundColumn DataField="DesignationId" SortExpression="DesignationId"
                                    AllowFiltering="False" Visible="False" HeaderText="DesignationId"
                                    meta:resourcekey="GridBoundColumnResource12" UniqueName="DesignationId" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" Skin="Hay" ID="RadToolBar1"
                                    OnButtonClick="RadToolBar1_ButtonClick" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server"
                                            meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdEmp_Designation.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }

    </script>
</asp:Content>
