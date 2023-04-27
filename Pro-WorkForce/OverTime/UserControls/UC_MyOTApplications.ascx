<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UC_MyOTApplications.ascx.vb"
    Inherits="OverTime_UserControls_UC_MyOTApplications" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="Upanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label ID="lblHeading" runat="server" CssClass="HeaderText" Text="List of Over Time Applications that you Involved"
                        ForeColor="Brown" meta:resourcekey="lblHeadingResource1"></asp:Label>
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
                        <asp:Label ID="LbRowCountOTMyApps" CssClass="normaltxt" runat="server" 
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
                <td align="center" width="85%">
                    <asp:GridView ID="gdvMyApplications" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="25" AutoGenerateColumns="False" 
                        meta:resourcekey="gdvMyApplicationsResource1">
                        <AlternatingRowStyle CssClass="AltRowStyle" Width="100%" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False" 
                                meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:Label ID="lblOT_MasterID" runat="server" 
                                        Text='<%# DataBinder.Eval(Container,"DataItem.OT_MasterID") %>' 
                                        meta:resourcekey="lblOT_MasterIDResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeNo" SortExpression="EmployeeNo" 
                                HeaderText="Employee No" meta:resourcekey="BoundFieldResource1" />
                            <asp:BoundField DataField="EmployeeName" SortExpression="EmployeeName" 
                                HeaderText="Employee Name" meta:resourcekey="BoundFieldResource2" />
                            <asp:BoundField DataField="WorkedOT_Normal" SortExpression="WorkedOT_Normal" 
                                HeaderText="Worked OT(Normal)" meta:resourcekey="BoundFieldResource3" />
                            <asp:BoundField DataField="WorkedOT_Rest" SortExpression="WorkedOT_Rest" 
                                HeaderText="Worked OT(Rest)" meta:resourcekey="BoundFieldResource4" />
                            <asp:BoundField DataField="CurrentStatus_En" SortExpression="CurrentStatus_En" 
                                HeaderText="Current Status" meta:resourcekey="BoundFieldResource5" />
                            <asp:BoundField DataField="ApprovedOT_Normal" SortExpression="WorkedOT_Normal" 
                                HeaderText="Approved OT(Normal)" meta:resourcekey="BoundFieldResource6" />
                            <asp:BoundField DataField="ApprovedOT_Rest" SortExpression="WorkedOT_Rest" 
                                HeaderText="Approved OT(Rest)" meta:resourcekey="BoundFieldResource7" />
                            <asp:BoundField DataField="Note" SortExpression="Note" 
                                HeaderText="Remarks/Notes" meta:resourcekey="BoundFieldResource8" />
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
