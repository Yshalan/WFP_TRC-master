<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UC_PlannedOverTime.ascx.vb"
    Inherits="OverTime_UserControls_UC_PlannedOverTime" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc1" %>
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
                    <asp:Label ID="Label2" runat="server" CssClass="HeaderText" Text="Define Employee's Planned Over Time"
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
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" />
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
                    <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Planned OT(Normal) HH:mm"
                        meta:resourcekey="Label1Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPlannedOTNormal_HH" runat="server" Width="20px" Text="00" meta:resourcekey="txtPlannedOTNormal_HHResource1"></asp:TextBox>
                    :
                    <asp:TextBox ID="txtPlannedOTNormal_MM" runat="server" Width="20px" Text="00" meta:resourcekey="txtPlannedOTNormal_MMResource1"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Planned OT(Rest) HH:mm"
                        meta:resourcekey="Label3Resource1"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPlannedOTRest_HH" runat="server" Width="20px" Text="00" meta:resourcekey="txtPlannedOTRest_HHResource1"></asp:TextBox>
                    :
                    <asp:TextBox ID="txtPlannedOTRest_MM" runat="server" Width="20px" Text="00" meta:resourcekey="txtPlannedOTRest_MMResource1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="button" meta:resourcekey="btnAddResource1" />
                    &nbsp;
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
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
                    <asp:GridView ID="gdvMyApplications" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" meta:resourcekey="gdvMyApplicationsResource1">
                        <AlternatingRowStyle CssClass="AltRowStyle" Width="100%" />
                        <Columns>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" CssClass="Checkbox" Text=" " meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:Label ID="lblOT_MasterID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OT_MasterID") %>'
                                        meta:resourcekey="lblOT_MasterIDResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="OverTimeMonth" SortExpression="OverTimeMonth" HeaderText="OverTimeMonth"
                                meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="OverTimeYear" SortExpression="OverTimeYear" HeaderText="OverTimeYear"
                                meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="Planned_OT_Normal" SortExpression="Planned_OT_Normal"
                                HeaderText="PlannedOT(Normal)" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField DataField="Planned_OT_Rest" SortExpression="Planned_OT_Rest" HeaderText="PlannedOT(Rest)"
                                meta:resourcekey="BoundFieldResource6" />
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
