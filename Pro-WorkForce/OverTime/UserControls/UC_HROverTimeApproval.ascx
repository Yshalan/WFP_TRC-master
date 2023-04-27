<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UC_HROverTimeApproval.ascx.vb"
    Inherits="OverTime_UserControls_UC_HROverTimeApproval" %>
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
                    <asp:Label ID="Label1" runat="server" CssClass="HeaderText" Text="Waiting for Your Action-As HR Employee"
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
                    <h3>
                        <asp:Label ID="LbRowCountOTMgr" CssClass="normaltxt" runat="server" meta:resourcekey="LbRowCountResource1"></asp:Label>
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
                        <asp:GridView ID="gdvHREmployeeOTApps" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" meta:resourcekey="gdvHREmployeeOTAppsResource1">
                            <AlternatingRowStyle CssClass="AltRowStyle" Width="100%" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" meta:resourcekey="TemplateFieldResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOT_MasterID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OT_MasterID") %>'
                                            meta:resourcekey="lblOT_MasterIDResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                    meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                    meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="Planned_OT_Normal" SortExpression="Planned_OT_Normal"
                                    HeaderText="PlannedOT(Normal)" meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="Planned_OT_Rest" SortExpression="Planned_OT_Rest" HeaderText="PlannedOT(Rest)"
                                    meta:resourcekey="BoundFieldResource4" />
                                <asp:BoundField DataField="WorkedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Worked OT(Normal)"
                                    meta:resourcekey="BoundFieldResource5" />
                                <asp:BoundField DataField="WorkedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Worked OT(Rest)"
                                    meta:resourcekey="BoundFieldResource6" />
                                <asp:BoundField DataField="CurrentStatus_En" SortExpression="CurrentStatus_En" HeaderText="Current Status"
                                    meta:resourcekey="BoundFieldResource7" />
                                <asp:TemplateField HeaderText="Decision" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="cmbDecision" runat="server" SkinID="RadcomboChkbox" MarkFirstMatch="True"
                                            ZIndex="999999" AutoPostBack="True" OnSelectedIndexChanged="cmbDecision_SelectedIndexChanged"
                                            meta:resourcekey="cmbDecisionResource1">
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved OT(Normal) in HH:mm" meta:resourcekey="TemplateFieldResource3">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtApprovedOTNormal_HH" runat="server" Width="20px" Text='<%# Eval("ApprovedOT_Normal") %>'
                                            meta:resourcekey="txtApprovedOTNormal_HHResource1" />
                                        :
                                        <asp:TextBox ID="txtApprovedOTNormal_MM" runat="server" Width="20px" Text="00" meta:resourcekey="txtApprovedOTNormal_MMResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved OT(Rest) in HH:mm" meta:resourcekey="TemplateFieldResource4">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtApprovedOTRest_HH" runat="server" Width="15px" Text='<%# Eval("ApprovedOT_Rest") %>'
                                            meta:resourcekey="txtApprovedOTRest_HHResource1" />
                                        :<asp:TextBox ID="txtApprovedOTRest_MM" runat="server" Width="15px" Text="00" meta:resourcekey="txtApprovedOTRest_MMResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Width="75px" Height="50px"
                                            meta:resourcekey="txtNotesResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Justification Text" Visible="False" meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtJustificationText" runat="server" TextMode="MultiLine" Width="75px"
                                            Height="50px" meta:resourcekey="txtJustificationTextResource1" />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="False" Width="75px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" meta:resourcekey="TemplateFieldResource7">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSaveOT" runat="server" Text="Save" CssClass="button" OnClick="btnSaveHROT_Click"
                                            meta:resourcekey="btnSaveOTResource1" />
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
                        PageSize="25" AutoGenerateColumns="False" meta:resourcekey="gdvMyApplicationsResource1">
                        <AlternatingRowStyle CssClass="AltRowStyle" Width="100%" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" meta:resourcekey="TemplateFieldResource8">
                                <ItemTemplate>
                                    <asp:Label ID="lblOT_MasterID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OT_MasterID") %>'
                                        meta:resourcekey="lblOT_MasterIDResource2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                meta:resourcekey="BoundFieldResource8" />
                            <asp:BoundField DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="BoundFieldResource9" />
                            <asp:BoundField DataField="Planned_OT_Normal" SortExpression="Planned_OT_Normal"
                                HeaderText="PlannedOT(Normal)" meta:resourcekey="BoundFieldResource10" />
                            <asp:BoundField DataField="Planned_OT_Rest" SortExpression="Planned_OT_Rest" HeaderText="PlannedOT(Rest)"
                                meta:resourcekey="BoundFieldResource11" />
                            <asp:BoundField DataField="WorkedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Worked OT(Normal)"
                                meta:resourcekey="BoundFieldResource12" />
                            <asp:BoundField DataField="WorkedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Worked OT(Rest)"
                                meta:resourcekey="BoundFieldResource13" />
                            <asp:BoundField DataField="CurrentStatus_En" SortExpression="CurrentStatus_En" HeaderText="Current Status"
                                meta:resourcekey="BoundFieldResource14" />
                            <asp:BoundField DataField="ApprovedOT_Normal" SortExpression="WorkedOT_Normal" HeaderText="Approved OT(Normal)"
                                meta:resourcekey="BoundFieldResource15" />
                            <asp:BoundField DataField="ApprovedOT_Rest" SortExpression="WorkedOT_Rest" HeaderText="Approved OT(Rest)"
                                meta:resourcekey="BoundFieldResource16" />
                            <asp:BoundField DataField="Note" SortExpression="Note" HeaderText="Remarks/Notes"
                                meta:resourcekey="BoundFieldResource17" />
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
