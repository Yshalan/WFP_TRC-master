<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UC_DirectMangrApproval.ascx.vb"
    Inherits="OverTime_UserControls_UC_DirectMangrApproval" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Upanel1">
    <ProgressTemplate>
        <asp:Panel ID="pnlup" runat="server" meta:resourcekey="pnlupResource1">
            <asp:Image ID="imgload" runat="server" ImageAlign="AbsMiddle" CssClass="img" ImageUrl="~/Images/loading.gif"
                meta:resourcekey="imgloadResource1" />
        </asp:Panel>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="Upanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label2" runat="server" CssClass="HeaderText" Text="Waiting for Your Action-As Direct Manager "
                        ForeColor="Brown" meta:resourcekey="Label2Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMonth" runat="server" CssClass="Profiletitletxt" Text="Select Month"
                        meta:resourcekey="lblMonthResource1"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbMonth" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="cmbMonthResource1">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVMonth" ValidationGroup="VGOverTime" runat="server"
                        InitialValue="--Please Select--" ControlToValidate="cmbMonth" Display="None"
                        ErrorMessage="Please Select Month" meta:resourcekey="RFVMonthResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEMonth" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="RFVMonth" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </td>
                <td>
                    <asp:Label ID="lblYear" runat="server" CssClass="Profiletitletxt" Text="Select Year"
                        meta:resourcekey="lblYearResource1"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbYear" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="cmbYearResource1">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVYear" ValidationGroup="VGOverTime" runat="server"
                        InitialValue="--Please Select--" ControlToValidate="cmbYear" Display="None" ErrorMessage="Please Select Year"
                        meta:resourcekey="RFVYearResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="RFVYear" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" meta:resourcekey="btnSearchResource1" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="LbRowCount" CssClass="normaltxt" runat="server" meta:resourcekey="LbRowCountResource1"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <div class="TableRes">
                        <asp:GridView ID="gdvOverTimeSummary" runat="server" AllowPaging="True" AllowSorting="True"
                            Width="100%" AutoGenerateColumns="False" meta:resourcekey="gdvOverTimeSummaryResource1">
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeId" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EmployeeId") %>'
                                            meta:resourcekey="lblEmployeeIdResource1"></asp:Label>
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SearchMonth") %>'
                                            meta:resourcekey="lblMonthResource2"></asp:Label>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SearchYear") %>'
                                            meta:resourcekey="lblYearResource2"></asp:Label>
                                        <asp:Label ID="lblWorkedOTNormalNum" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.WorkedOT_Normal_Num") %>'
                                            meta:resourcekey="lblWorkedOTNormalNumResource1"></asp:Label>
                                        <asp:Label ID="lblWorkedOTRestNum" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.WorkedOT_Rest_Num") %>'
                                            meta:resourcekey="lblWorkedOTRestNumResource1"></asp:Label>
                                        <asp:Label ID="lblPlannedOTNormalNum" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PlannedOT_Normal_Num") %>'
                                            meta:resourcekey="lblPlannedOTNormalNumResource1"></asp:Label>
                                        <asp:Label ID="lblPlannedOTRestNum" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PlannedOT_Rest_Num") %>'
                                            meta:resourcekey="lblPlannedOTRestNumResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:TemplateField HeaderText="Employee Name" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Width="125px" Text='<%# Eval("EmployeeName") %>'
                                            meta:resourcekey="lblEmpNameResource1"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="125px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PlannedOT_Normal" SortExpression="PlannedOT_Normal" HeaderText="Planned OT(Normal)"
                                    meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="PlannedOT_Rest" SortExpression="PlannedOT_Rest" HeaderText="Planned OT(Rest)"
                                    meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="WorkedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Worked OT(Normal)"
                                    meta:resourcekey="BoundFieldResource4" />
                                <asp:BoundField DataField="WorkedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Worked OT(Rest)"
                                    meta:resourcekey="BoundFieldResource5" />
                                <asp:TemplateField HeaderText="Decision" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cmbDecision" runat="server" MarkFirstMatch="True" ZIndex="999999"
                                            Width="125px" meta:resourcekey="cmbDecisionResource1">
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved OT(Normal) in HH:mm" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtApprovedOTNormal_HH" runat="server" Width="20px" Text='<%# Eval("WorkedOT_Normal") %>'
                                            meta:resourcekey="txtApprovedOTNormal_HHResource1" />
                                        :
                                        <asp:TextBox ID="txtApprovedOTNormal_MM" runat="server" Width="20px" Text="00" meta:resourcekey="txtApprovedOTNormal_MMResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved OT(Rest) in HH:mm" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtApprovedOTRest_HH" runat="server" Width="15px" Text='<%# Eval("WorkedOT_Rest") %>'
                                            meta:resourcekey="txtApprovedOTRest_HHResource1" />
                                        :<asp:TextBox ID="txtApprovedOTRest_MM" runat="server" Width="15px" Text="00" meta:resourcekey="txtApprovedOTRest_MMResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="75px" Height="50px"
                                            meta:resourcekey="txtNotesResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" meta:resourcekey="TemplateFieldResource7">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSaveOT" runat="server" Text="Save" CssClass="button" OnClick="btnSaveOT_Click"
                                            meta:resourcekey="btnSaveOTResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Export Detailed Report" meta:resourcekey="TemplateFieldResource8">
                                    <ItemTemplate>
                                        <asp:Button ID="btnPrintPDF" runat="server" Text="PDF" CssClass="button" OnClick="btnPrintPDF_Click"
                                            meta:resourcekey="btnPrintPDFResource1" />
                                        <asp:Button ID="btnPrintExcel" runat="server" Text="EXCEL" CssClass="button" OnClick="btnPrintExcel_Click"
                                            meta:resourcekey="btnPrintExcelResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <RowStyle CssClass="RowStyle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label1" runat="server" CssClass="HeaderText" Text="Waiting for Your Action-As Internal Manager"
                        ForeColor="Brown" meta:resourcekey="Label1Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="LbRowCountOTMgr" CssClass="normaltxt" runat="server" meta:resourcekey="LbRowCountOTMgrResource1"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <div class="TableRes">
                        <asp:GridView ID="gdvManagerOTApps" runat="server" AllowPaging="True" AllowSorting="True"
                            Width="100%" AutoGenerateColumns="False" meta:resourcekey="gdvManagerOTAppsResource1">
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" meta:resourcekey="TemplateFieldResource9">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOT_MasterID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OT_MasterID") %>'
                                            meta:resourcekey="lblOT_MasterIDResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                    meta:resourcekey="BoundFieldResource6" />
                                <asp:TemplateField HeaderText="Employee Name" meta:resourcekey="TemplateFieldResource10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Width="125px" Text='<%# Eval("EmployeeName") %>'
                                            meta:resourcekey="lblEmpNameResource2"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="125px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Planned_OT_Normal" SortExpression="Planned_OT_Normal"
                                    HeaderText="PlannedOT(Normal)" meta:resourcekey="BoundFieldResource7" />
                                <asp:BoundField DataField="Planned_OT_Rest" SortExpression="Planned_OT_Rest" HeaderText="PlannedOT(Rest)"
                                    meta:resourcekey="BoundFieldResource8" />
                                <asp:BoundField DataField="WorkedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Worked OT(Normal)"
                                    meta:resourcekey="BoundFieldResource9" />
                                <asp:BoundField DataField="WorkedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Worked OT(Rest)"
                                    meta:resourcekey="BoundFieldResource10" />
                                <asp:TemplateField HeaderText="Current Status" meta:resourcekey="TemplateFieldResource11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCurrentStatus" runat="server" Width="125px" Text='<%# Eval("CurrentStatus_En") %>'
                                            meta:resourcekey="lblCurrentStatusResource1"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="125px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Decision" meta:resourcekey="TemplateFieldResource12">
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cmbDecision" runat="server" Width="130px" MarkFirstMatch="True"
                                            ZIndex="999999" AutoPostBack="True" OnSelectedIndexChanged="cmbDecision_SelectedIndexChanged"
                                            meta:resourcekey="cmbDecisionResource2">
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved OT(Normal) in HH:mm" meta:resourcekey="TemplateFieldResource13">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtApprovedOTNormal_HH" runat="server" Width="20px" Text='<%# Eval("ApprovedOT_Normal") %>'
                                            meta:resourcekey="txtApprovedOTNormal_HHResource2" />
                                        :
                                        <asp:TextBox ID="txtApprovedOTNormal_MM" runat="server" Width="20px" Text="00" meta:resourcekey="txtApprovedOTNormal_MMResource2" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved OT(Rest) in HH:mm" meta:resourcekey="TemplateFieldResource14">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtApprovedOTRest_HH" runat="server" Width="15px" Text='<%# Eval("ApprovedOT_Rest") %>'
                                            meta:resourcekey="txtApprovedOTRest_HHResource2" />
                                        :<asp:TextBox ID="txtApprovedOTRest_MM" runat="server" Width="15px" Text="00" meta:resourcekey="txtApprovedOTRest_MMResource2" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks/Notes" meta:resourcekey="TemplateFieldResource15">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="65px" Height="50px"
                                            meta:resourcekey="txtNotesResource2" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="65px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Justification Text" Visible="False" meta:resourcekey="TemplateFieldResource16">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJustificationText" runat="server" TextMode="MultiLine" Width="65px"
                                            Height="50px" meta:resourcekey="txtJustificationTextResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="65px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" meta:resourcekey="TemplateFieldResource17">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSaveOT" runat="server" Text="Save" CssClass="button" Width="50px"
                                            OnClick="btnSaveMgrOT_Click" meta:resourcekey="btnSaveOTResource2" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <RowStyle CssClass="RowStyle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lblHeading" runat="server" CssClass="HeaderText" Text="List of Over Time Applications that you Involved"
                        ForeColor="Brown" Visible="False" meta:resourcekey="lblHeadingResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <h3>
                        <asp:Label ID="LbRowCountOTMyApps" CssClass="normaltxt" runat="server" Visible="False"
                            meta:resourcekey="LbRowCountOTMyAppsResource1"></asp:Label>
                    </h3>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center" width="85%">
                    <asp:GridView ID="gdvMyApplications" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" meta:resourcekey="gdvMyApplicationsResource1">
                        <AlternatingRowStyle CssClass="AltRowStyle" Width="100%" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" meta:resourcekey="TemplateFieldResource18">
                                <ItemTemplate>
                                    <asp:Label ID="lblOT_MasterID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OT_MasterID") %>'
                                        meta:resourcekey="lblOT_MasterIDResource2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                meta:resourcekey="BoundFieldResource11" />
                            <asp:BoundField DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="BoundFieldResource12" />
                            <asp:BoundField DataField="Planned_OT_Normal" SortExpression="Planned_OT_Normal"
                                HeaderText="PlannedOT(Normal)" meta:resourcekey="BoundFieldResource13" />
                            <asp:BoundField DataField="Planned_OT_Rest" SortExpression="Planned_OT_Rest" HeaderText="PlannedOT(Rest)"
                                meta:resourcekey="BoundFieldResource14" />
                            <asp:BoundField DataField="WorkedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Worked OT(Normal)"
                                meta:resourcekey="BoundFieldResource15" />
                            <asp:BoundField DataField="WorkedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Worked OT(Rest)"
                                meta:resourcekey="BoundFieldResource16" />
                            <asp:BoundField DataField="CurrentStatus_En" SortExpression="CurrentStatus_En" HeaderText="Current Status"
                                meta:resourcekey="BoundFieldResource17" />
                            <asp:BoundField DataField="ApprovedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Approved OT(Normal)"
                                meta:resourcekey="BoundFieldResource18" />
                            <asp:BoundField DataField="ApprovedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Approved OT(Rest)"
                                meta:resourcekey="BoundFieldResource19" />
                            <asp:BoundField DataField="Note" SortExpression="Note" HeaderText="Remarks/Notes"
                                meta:resourcekey="BoundFieldResource20" />
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <RowStyle CssClass="RowStyle" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
