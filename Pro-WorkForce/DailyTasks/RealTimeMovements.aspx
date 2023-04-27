<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="RealTimeMovements.aspx.vb" Inherits="DailyTasks_RealTimeMovements"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../js/compiled/flipclock.css">

    <script type="text/javascript">
        var clock;
        $(document).ready(function () {
            clock = $('.clock').FlipClock({
                clockFace: 'TwentyFourHourClock'
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <div class="row">
        <div class="col-md-12 text-center">
            <div class="clock" style="margin: 2em;"></div>
            <div class="message"></div>
        </div>
    </div>
    <asp:UpdatePanel ID="UPAll" runat="server">
        <ContentTemplate>
            <div class="updateprogressAssign">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UPAll">
                    <ProgressTemplate>
                        <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">
                            <div>
                                <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" Style="margin-top: 20%;" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

            <asp:Timer ID="timerAll" runat="server" Interval="10000">
            </asp:Timer>
            <%--           <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblHead" runat="server" CssClass="Profiletitletxt" Text="Last Update:"
                        meta:resourcekey="lblHeadResource1"></asp:Label>
                    <asp:Label ID="lblLastUpdate" runat="server" CssClass="normaltxt" meta:resourcekey="lblLastUpdateResource1"></asp:Label>
                </div>

            </div>--%>

            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="gvEvents" Skin="Hay"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="gvEvents" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="15"
                        ShowFooter="True" meta:resourcekey="gvEventsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                            Owner="" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EmployeeName" />
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="اسم الموظف" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="EmployeeArabicName" />
                                <telerik:GridBoundColumn DataField="ReasonType" HeaderText="Reason Type" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="ReasonType" />
                                <telerik:GridBoundColumn DataField="ReasonTypeArb" HeaderText="نوع الحركة" meta:resourcekey="GridBoundColumnResource5"
                                    UniqueName="ReasonTypeArb" />
                                <telerik:GridBoundColumn DataField="M_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                    meta:resourcekey="GridBoundColumnResource6" UniqueName="M_DATE" />
                                <telerik:GridBoundColumn DataField="M_TIME" HeaderText="Time" DataFormatString="{0:HH:mm}"
                                    meta:resourcekey="GridBoundColumnResource7" UniqueName="M_TIME" />
                                <telerik:GridBoundColumn DataField="Location" HeaderText="Terminal Description" meta:resourcekey="GridBoundColumnResource8"
                                    UniqueName="Location" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False"></GroupingSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
