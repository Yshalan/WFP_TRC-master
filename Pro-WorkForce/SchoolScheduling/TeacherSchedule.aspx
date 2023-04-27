<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TeacherSchedule.aspx.vb"
    Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" Inherits="SchoolScheduling_TeacherSchedule"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Teacher Schedule" meta:resourcekey="PageHeader1Resource1" />    

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <uc1:EmployeeFilter ID="EmployeeFilter" runat="server" ShowRadioSearch="true" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-4">
                                                    <asp:RadioButtonList ID="rblEmpStatus" runat="server" CssClass="Profiletitletxt"
                                                        RepeatDirection="Horizontal" meta:resourcekey="rblEmpStatusResource1">
                                                        <asp:ListItem Value="1" Text="Active" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="InActive" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="row" id="trControls" runat="server" align="center">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-2" id="Td2" runat="server">
                                                    <asp:Button ID="btnFilter" runat="server" Text="Get By Filter" class="button" ValidationGroup="ValidateComp"
                                                        meta:resourcekey="btnFilterResource1" />
                                                </div>
                                            </div>                                  

              
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid ID="dgrdTeacherSchedule" runat="server" AllowMultiRowSelection="True"
                                AllowPaging="True" AllowSorting="True" GridLines="None" meta:resourcekey="dgrdTeacherScheduleResource1"
                                PageSize="15" ShowFooter="True" ShowStatusBar="True" >
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="ClassId" meta:resourcekey="GridBoundColumnResource1"
                                            SortExpression="ClassId" UniqueName="ClassId" Visible="False" />
                                        <telerik:GridTemplateColumn HeaderText="Week Day" meta:resourcekey="GridTemplateColumnResource1"
                                           UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDay" runat="server" meta:resourcekey="lblClassResource1" />
                                                <asp:HiddenField ID="hdnEnDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.DayName") %>' />
                                                <asp:HiddenField ID="hdnArDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.DayArabicName") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="lesson1" HeaderText="1" SortExpression="lesson1"
                                           HeaderStyle-Wrap="False" UniqueName="lesson1" meta:resourcekey="GridBoundColumnResource2" />
                                        <telerik:GridBoundColumn DataField="lesson2" HeaderText="2" SortExpression="lesson2"
                                           HeaderStyle-Wrap="False" UniqueName="lesson2" meta:resourcekey="GridBoundColumnResource3" />
                                        <telerik:GridBoundColumn DataField="lesson3" HeaderText="3" SortExpression="lesson3"
                                           HeaderStyle-Wrap="False" UniqueName="lesson3" meta:resourcekey="GridBoundColumnResource4" />
                                        <telerik:GridBoundColumn DataField="lesson4" HeaderText="4" SortExpression="lesson4"
                                           HeaderStyle-Wrap="False" UniqueName="lesson4" meta:resourcekey="GridBoundColumnResource5" />
                                        <telerik:GridBoundColumn DataField="lesson5" HeaderText="5" SortExpression="lesson5"
                                           HeaderStyle-Wrap="False" UniqueName="lesson5" meta:resourcekey="GridBoundColumnResource6" />
                                        <telerik:GridBoundColumn DataField="lesson6" HeaderText="6" SortExpression="lesson6"
                                           HeaderStyle-Wrap="False" UniqueName="lesson6" meta:resourcekey="GridBoundColumnResource7" />
                                        <telerik:GridBoundColumn DataField="lesson7" HeaderText="7" SortExpression="lesson7"
                                           HeaderStyle-Wrap="False" UniqueName="lesson7" meta:resourcekey="GridBoundColumnResource8" />
                                        <telerik:GridBoundColumn DataField="lesson8" HeaderText="8" SortExpression="lesson8"
                                           HeaderStyle-Wrap="False" UniqueName="lesson8" meta:resourcekey="GridBoundColumnResource9" />
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
                </caption>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
