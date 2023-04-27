<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ApproveInvalidAttempts.aspx.vb" Inherits="DailyTasks_ApproveInvalidAttempts"
    Theme="SvTheme" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Image ID="img_EmpImage" runat="server"  Width="100%" meta:resourcekey="img_EmpImageResource1"></asp:Image>
                     <asp:Label ID="lblEmployeeImage" runat="server" Text="Employee Image" meta:resourcekey="lblEmployeeImageResource1"></asp:Label>
                </div>
                 <div class="col-md-8">
                      <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee No." meta:resourcekey="lblEmployeeNoResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblEmployeeNoVal" runat="server" SkinID="normaltext" meta:resourcekey="lblEmployeeNoValResource1"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name" meta:resourcekey="lblEmployeeNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblEmployeeNameVal" runat="server" SkinID="normaltext" meta:resourcekey="lblEmployeeNameValResource1"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Label ID="lblDate" runat="server" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblDateVal" runat="server" SkinID="normaltext" meta:resourcekey="lblDateValResource1"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Label ID="lblTime" runat="server" Text="Time" meta:resourcekey="lblTimeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblTimeVal" runat="server" SkinID="normaltext" meta:resourcekey="lblTimeValResource1"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Label ID="lblType" runat="server" Text="Type" meta:resourcekey="lblTypeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblTypeVal" runat="server" SkinID="normaltext" meta:resourcekey="lblTypeValResource1"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblDecision" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rblDecisionResource1">
                        <asp:ListItem Text="Approve" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Reject" Value="0" meta:resourcekey="ListItemResource2"></asp:ListItem>
                    </asp:RadioButtonList>

                    <asp:RequiredFieldValidator ID="rfvDecision" runat="server" ControlToValidate="rblDecision"
                        ValidationGroup="btnSave" ErrorMessage="Please Select Transaction Decision"
                        Display="None" meta:resourcekey="rfvDecisionResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceDecision" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvDecision" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="btnSave" meta:resourcekey="btnSaveResource1" />
                </div>
            </div>
                </div>
                <div class="col-md-2">
                    <asp:Image ID="img_ReaderImage" runat="server"  Width="100%" meta:resourcekey="img_ReaderImageResource1"></asp:Image>
                     <asp:Label ID="lblReaderImage" runat="server" Text="Reader Image" meta:resourcekey="lblReaderImageResource1"></asp:Label>
                </div>
            </div>
           
            <div class="row">
                <div class="table-responsive">
                    <div>
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdInvalidAttempts"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" >
                            <ContextMenu FeatureGroupID="rfContextMenu">
                            </ContextMenu>
                        </telerik:RadFilter>
                    </div>
                    <telerik:RadGrid ID="dgrdInvalidAttempts" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True"
                        AllowMultiRowSelection="True" ShowFooter="True" OnItemCommand="dgrdTAPolicy_ItemCommand" CellSpacing="0" meta:resourcekey="dgrdInvalidAttemptsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="TransactionId,EmployeeId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" Visible="false"/>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No."
                                    UniqueName="EmployeeNo" FilterControlAltText="Filter EmployeeNo column" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                    UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                    UniqueName="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="M_Date" SortExpression="M_Date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                    UniqueName="M_Date" FilterControlAltText="Filter M_Date column" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="M_Time" SortExpression="M_Time" HeaderText="Time" DataFormatString="{0:HH:mm}"
                                    UniqueName="M_Time" FilterControlAltText="Filter M_Time column" meta:resourcekey="GridBoundColumnResource5" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

