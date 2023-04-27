<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ScheduleInfo.ascx.vb" Inherits="SelfServices_UserControls_ScheduleInfo" %>
<div class="selfservicesummaryarea">
    <div>
        <div runat="server" id="dvEmpCardNo" visible="false" class="inline borderright">
            <i class="fa fa-id-card-o"></i>
            <asp:Label ID="lblEmpCardNo" runat="server" Text="Employee Card No. : " CssClass="Profiletitletxt inline"
                meta:resourcekey="lblEmpCardNoResource1" SkinID="normaltext"></asp:Label>
            <asp:Label ID="lblEmpCardNoValue" runat="server" meta:resourcekey="lblEmpCardNoValueResource1" SkinID="inline"></asp:Label>

        </div>

        <div runat="server" id="divInTime" visible="false" class="inline borderright">
            <i class="fa fa-sign-in"></i>
            <asp:Label ID="lblInTime" runat="server" Text="IN Time : " CssClass="Profiletitletxt inline"
                meta:resourcekey="lblInTimeResource1" SkinID="normaltext"></asp:Label>
            <asp:Label ID="lblInTimeValue" runat="server" meta:resourcekey="lblInTimeValueResource1" SkinID="inline"></asp:Label>

        </div>
        <div runat="server" id="divScheduleType" visible="false" class="inline borderright">
            <i class="fa fa-calendar"></i>
            <asp:Label ID="lblScheduleType" runat="server"
                Text="Schedule Type : " CssClass="Profiletitletxt"
                meta:resourcekey="lblScheduleTypeResource1" SkinID="normaltext"></asp:Label>

            <asp:Label ID="lblScheduleTypeValue" runat="server"
                meta:resourcekey="lblScheduleTypeValueResource1" SkinID="inline"></asp:Label>

        </div>
        <div runat="server" id="divSchedule" visible="false" class="inline borderright">
            <i class="fa fa-calendar-o"></i>
            <asp:Label ID="lblSchedule" runat="server" Text="Schedule : " CssClass="inline Profiletitletxt"
                meta:resourcekey="lblScheduleResource1" SkinID="normaltext"></asp:Label>

            <asp:Label ID="lblScheduleValue" runat="server"
                meta:resourcekey="lblScheduleValueResource1" SkinID="inline"></asp:Label>
            <asp:Label ID="lblTimeVal" Visible="False" SkinID="inline" runat="server"
                meta:resourcekey="lblTimeValResource1"></asp:Label>

        </div>
        <div runat="server" id="divExpectOut" visible="false" class="inline borderright">
            <i class="fa fa-sign-out"></i>
            <asp:Label ID="lblExpectOut" runat="server"
                Text="Expected Out : " CssClass="Profiletitletxt"
                meta:resourcekey="lblExpectOutResource1" SkinID="normaltext"></asp:Label>

            <asp:Label ID="lblExpectOutValue" runat="server"
                meta:resourcekey="lblExpectOutValueResource1" SkinID="inline"></asp:Label>

        </div>
        <div runat="server" id="divStatus" visible="false" class="inline">
            <i class="fa fa-clock-o"></i>
            <asp:Label ID="lblStatus" runat="server" Text="Status : " SkinID="normaltext" CssClass="Profiletitletxt"
                meta:resourcekey="lblStatusResource1"></asp:Label>

            <asp:Label ID="lblStatusVal" runat="server" meta:resourcekey="lblStatusValResource1" SkinID="inline"></asp:Label>

        </div>
    </div>
</div>

