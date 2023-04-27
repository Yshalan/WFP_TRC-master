<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SummaryPage.ascx.vb"
    Inherits="SelfServices_UserControls_SummaryPage" %>


<div class="row">
    <div class="col-md-12">
        <asp:Panel ID="pnlDurationTotals" runat="server" GroupingText="Duration Totals"
            meta:resourcekey="pnlDurationTotalsResource1">
            <div class="col-md-10">
                <asp:Label ID="lblDelay" Visible="false" runat="server" Text="Delay : " CssClass="Profiletitletxt"
                    meta:resourcekey="lblDelayResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblDelayValue" Visible="false" runat="server" meta:resourcekey="lblDelayValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblEarlyOut" Visible="false" runat="server" Text="Early Out : " CssClass="Profiletitletxt"
                    meta:resourcekey="lblEarlyOutResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblEarlyOutValue" Visible="false" runat="server" meta:resourcekey="lblEarlyOutValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblDelayAndEarlyOut" Visible="false" runat="server" Text="Total Dealy and Early Out : " CssClass="Profiletitletxt"
                    meta:resourcekey="lblDelayAndEarlyOutResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblDelayAndEarlyOutValue" Visible="false" runat="server" meta:resourcekey="lblDelayAndEarlyOutValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblLostTime" Visible="false" runat="server" Text="Lost Time :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblLostTimeResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblLostTimeValue" Visible="false" runat="server" meta:resourcekey="lblLostTimeValueResource1"></asp:Label>
            </div>

            <div class="col-md-10">
                <asp:Label ID="lblAbsent" Visible="false" runat="server" Text="Total Absent :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblAbsentResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblAbsentValue" Visible="false" runat="server" meta:resourcekey="lblLostTimeValueResource1"></asp:Label>
            </div>

            <div class="col-md-10">
                <asp:Label ID="lblRemainingPermissionBalance" Visible="false" runat="server" Text="Personal Permission Balance :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblRemainingPermissionBalanceResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblRemainingPermissionBalanceValue" Visible="false" runat="server" meta:resourcekey="lblLostTimeValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblRemainingTimesPermission" Visible="false" runat="server" Text="Remaining Of Personal Permission  :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblRemainingTimesPermissionResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblRemainingTimesPermissionValue" Visible="false" runat="server" meta:resourcekey="lblRemainingTimesPermissionValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblMissingIn" Visible="false" runat="server" Text="Total Missing In :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblMissingInResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblMissingInValue" Visible="false" runat="server" meta:resourcekey="lblLostTimeValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblMissingOut" Visible="false" runat="server" Text="Total Missing Out :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblMissingOutResource1"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblMissingOutValue" Visible="false" runat="server" meta:resourcekey="lblLostTimeValueResource1"></asp:Label>
            </div>
            <div class="col-md-10">
                <asp:Label ID="lblNotCompletionHalfDay" Visible="false" runat="server"
                    Text="Times of Not completing 50% of the work schedule :" CssClass="Profiletitletxt"
                    meta:resourcekey="lblNotCompletionHalfDayResource1" Width="200px"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblNotCompletionHalfDayValue" Visible="false" runat="server" meta:resourcekey="lblLostTimeValueResource1"></asp:Label>
            </div>

            <div class="col-md-10">
                <asp:Label ID="lblRemainingYearlyLeaveBalance" Visible="false" runat="server"
                    Text="Remaining Over Time Balance" CssClass="Profiletitletxt"
                    meta:resourcekey="lblRemainingYearlyLeaveBalanceResource1" Width="200px"></asp:Label> <%--Remaining OvertTime Balance Converted From Leave--%>
            </div>
            <div class="col-md-2">
                <asp:Label ID="lblRemainingYearlyLeaveBalanceValue" Visible="false" runat="server" meta:resourcekey="lblRemainingYearlyLeaveBalanceValueResource1"></asp:Label>
            </div>


        </asp:Panel>
    </div>
</div>
