<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ClassSchoolSchedule.aspx.vb" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master"
    Inherits="SchoolScheduling_ClassSchoolSchedule" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Class School Schedule" meta:resourcekey="PageHeader1Resource1" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblGrade" runat="server" CssClass="Profiletitletxt" Text="Class Grade"
                        meta:resourcekey="lblGradeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbGrade" runat="server" AppendDataBoundItems="True"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista"
                        meta:resourcekey="RadCmbGradeResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqGrade" runat="server" ControlToValidate="RadCmbGrade"
                        Display="None" ErrorMessage="Please select Grade" InitialValue="--Please Select--"
                        ValidationGroup="Grp1" meta:resourcekey="reqGradeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqGrade" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="reqGrade" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblClass" runat="server" Text="Class" meta:resourcekey="lblClassResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbClass" runat="server" AppendDataBoundItems="True"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbClassResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqClass" runat="server" ControlToValidate="RadCmbClass"
                        Display="None" ErrorMessage="Please select Class" InitialValue="--Please Select--"
                        ValidationGroup="Grp1" meta:resourcekey="reqClassResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqClass" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="reqClass" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdClassSchoolScedule" runat="server"
                        AllowMultiRowSelection="True" AllowPaging="True" AllowSorting="True"
                        GridLines="None" meta:resourcekey="dgrdCourseResource1" PageSize="15" Width="100%"
                        ShowFooter="True" ShowStatusBar="True">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ClassId"
                                    meta:resourcekey="GridBoundColumnResource1" SortExpression="ClassId"
                                    UniqueName="ClassId" Visible="False" />
                                <telerik:GridBoundColumn DataField="lesson"
                                    meta:resourcekey="GridBoundColumnResource1" SortExpression="lesson"
                                    UniqueName="lesson" Visible="False" />
                                <telerik:GridBoundColumn DataField="DayId"
                                    meta:resourcekey="GridBoundColumnResource1" SortExpression="DayId"
                                    UniqueName="DayId" Visible="False" />
                                <telerik:GridTemplateColumn HeaderText="Week Day"
                                    meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay" runat="server" meta:resourcekey="lblClassResource1" />
                                        <asp:HiddenField ID="hdnEnDay" runat="server"
                                            Value='<%# DataBinder.Eval(Container,"DataItem.DayName") %>' />
                                        <asp:HiddenField ID="hdnArDay" runat="server"
                                            Value='<%# DataBinder.Eval(Container,"DataItem.DayArabicName") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="lesson1" HeaderText="1"
                                    SortExpression="lesson1" UniqueName="lesson1"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="lesson2" HeaderText="2"
                                    SortExpression="lesson2" UniqueName="lesson2"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="lesson3" HeaderText="3"
                                    SortExpression="lesson3" UniqueName="lesson3"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="lesson4" HeaderText="4"
                                    SortExpression="lesson4" UniqueName="lesson4"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn DataField="lesson5" HeaderText="5"
                                    SortExpression="lesson5" UniqueName="lesson5"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn DataField="lesson6" HeaderText="6"
                                    SortExpression="lesson6" UniqueName="lesson6"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn DataField="lesson7" HeaderText="7"
                                    SortExpression="lesson7" UniqueName="lesson7"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource8" />
                                <telerik:GridBoundColumn DataField="lesson8" HeaderText="8"
                                    SortExpression="lesson8" UniqueName="lesson8"
                                    HeaderStyle-Wrap="False" meta:resourcekey="GridBoundColumnResource9" />

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
    </asp:UpdatePanel>
</asp:Content>
