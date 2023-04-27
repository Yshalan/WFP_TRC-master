<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="NotificationExceptions_MultiSelection.aspx.vb" Inherits="Employee_NotificationExceptions_MultiSelection"
    Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Employee/UserControls/NotificationException_Employee.ascx" TagName="NotificationException_Employee"
    TagPrefix="uc3" %>
<%@ Register Src="~/Employee/UserControls/NotificationException_LogicalGroup.ascx" TagName="NotificationException_LogicalGroup"
    TagPrefix="uc4" %>
<%@ Register Src="~/Employee/UserControls/NotificationException_WorkLocation.ascx" TagName="NotificationException_WorkLocation"
    TagPrefix="uc5" %>
<%@ Register Src="~/Employee/UserControls/NotificationException_Entity.ascx" TagName="NotificationException_Entity"
    TagPrefix="uc6" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">

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


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <cc1:TabContainer ID="tabNotification_Exception" runat="server" ActiveTabIndex="0"
        OnClientActiveTabChanged="hideValidatorCalloutTab" CssClass="" meta:resourcekey="tabNotification_ExceptionResource1">

        <cc1:TabPanel ID="tabEmpException" runat="server" HeaderText="Employee" meta:resourcekey="tabEmpExceptionResource1">
            <ContentTemplate>
                <uc3:NotificationException_Employee ID="NotificationException_Employee" runat="server" />
            </ContentTemplate>




        </cc1:TabPanel>
        <cc1:TabPanel ID="tabLGException" runat="server" HeaderText="Logical Group" meta:resourcekey="tabLGExceptionResource1">
            <ContentTemplate>
                <uc4:NotificationException_LogicalGroup ID="NotificationException_LogicalGroup" runat="server" />
            </ContentTemplate>




        </cc1:TabPanel>
        <cc1:TabPanel ID="tabWLException" runat="server" HeaderText="Work Location" meta:resourcekey="tabWLExceptionResource1">
            <ContentTemplate>
                <uc5:NotificationException_WorkLocation ID="NotificationException_WorkLocation" runat="server" />
            </ContentTemplate>




        </cc1:TabPanel>
        <cc1:TabPanel ID="tabEntityException" runat="server" HeaderText="Entity" meta:resourcekey="tabEntityExceptionResource1">
            <ContentTemplate>
                <uc6:NotificationException_Entity ID="NotificationException_Entity" runat="server" />
            </ContentTemplate>




        </cc1:TabPanel>
    </cc1:TabContainer>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnDelete" runat="server" Text="Delete"
                CssClass="button" meta:resourcekey="btnDeleteResource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadFilter runat="server" ID="radfilter1" FilterContainerID="dgrdNotificationExceptions"
                Skin="Hay" ShowApplyButton="False" meta:resourcekey="radfilter1Resource1">
                <ContextMenu FeatureGroupID="rfContextMenu">
                </ContextMenu>
            </telerik:RadFilter>
            <telerik:RadGrid runat="server" ID="dgrdNotificationExceptions" AllowSorting="True" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                PageSize="15" ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdNotificationExceptionsResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="NotificationExceptionId,FK_EmployeeId,FromDate,ToDate,GroupId,WorkLocationId,EntityId">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" Text="&nbsp;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                            UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                            UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                            SortExpression="EmployeeArabicName" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource3">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GroupName" HeaderText="Logical Group Name"
                            SortExpression="GroupName" UniqueName="GroupName" meta:resourcekey="GridBoundColumnResource4">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GroupArabicName" HeaderText="Logical Group Arabic Name"
                            SortExpression="GroupArabicName" UniqueName="GroupArabicName" meta:resourcekey="GridBoundColumnResource5">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="WorkLocationName" HeaderText="WorkLocation Name"
                            SortExpression="WorkLocationName" UniqueName="WorkLocationName" meta:resourcekey="GridBoundColumnResource6">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WorkLocationArabicName" HeaderText="WorkLocation Arabic Name"
                            SortExpression="WorkLocationArabicName" UniqueName="WorkLocationArabicName" meta:resourcekey="GridBoundColumnResource7">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name"
                            SortExpression="EntityName" UniqueName="EntityName" meta:resourcekey="GridBoundColumnResource8">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Entity Arabic Name"
                            SortExpression="EntityArabicName" UniqueName="EntityArabicName" meta:resourcekey="GridBoundColumnResource9">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NotificationExceptionId" DataType="System.Int32" Visible="False"
                            HeaderText="NotificationExceptionId" UniqueName="NotificationExceptionId" meta:resourcekey="GridBoundColumnResource10">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FK_EmployeeId" DataType="System.Int32" Visible="False"
                            HeaderText="FK_EmployeeId" UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource11">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GroupId" DataType="System.Int32" Visible="False"
                            HeaderText="GroupId" UniqueName="GroupId" meta:resourcekey="GridBoundColumnResource12">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WorkLocationId" DataType="System.Int32" Visible="False"
                            HeaderText="WorkLocationId" UniqueName="WorkLocationId" meta:resourcekey="GridBoundColumnResource13">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EntityId" DataType="System.Int32" Visible="False"
                            HeaderText="EntityId" UniqueName="EntityId" meta:resourcekey="GridBoundColumnResource14">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                            UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource15">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                            UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource16" />
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
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>

