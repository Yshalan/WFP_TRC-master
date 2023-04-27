<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ScheduleGroup.ascx.vb"
    Inherits="DailyTasks_UserControls_ScheduleGroup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Upanel1">
    <ProgressTemplate>
        <asp:Panel ID="pnlup" runat="server">
            <asp:Image ID="imgload" runat="server" ImageAlign="AbsMiddle" CssClass="img" ImageUrl="~/Images/loading.gif" />
        </asp:Panel>
    </ProgressTemplate>
</asp:UpdateProgress>--%>
<asp:UpdatePanel ID="Upanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td colspan="2">
                   
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="120px">
                    <asp:Label ID="lblGroupCode" runat="server" CssClass="Profiletitletxt" Text="Group Code"></asp:Label>
                </td>
                <td width="480px">
                    <asp:TextBox ID="txtGroupCode" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVGroupCode" runat="server" ControlToValidate="txtGroupCode"
                        Display="None" ErrorMessage="Please Enter Group Code" ValidationGroup="VGGroup"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEGroupCode" runat="server" Enabled="True" TargetControlID="RFVGroupCode">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td width="120px">
                    <asp:Label ID="lblNameEn" runat="server" CssClass="Profiletitletxt" Text="Group English Name"></asp:Label>
                </td>
                <td width="480px">
                    <asp:TextBox ID="txtGroupNameEn" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVGroupNameEn" runat="server" ControlToValidate="txtGroupNameEn"
                        Display="None" ErrorMessage="Please Enter Group English Name " ValidationGroup="VGGroup"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEGroupNameEn" runat="server" Enabled="True" TargetControlID="RFVGroupNameEn">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td width="120px">
                    <asp:Label ID="lblNameAr" runat="server" CssClass="Profiletitletxt" Text="Group Arabic Name"></asp:Label>
                </td>
                <td width="480px">
                    <asp:TextBox ID="txtGroupNameAr" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFVGroupNameAr" runat="server" ControlToValidate="txtGroupNameAr"
                        Display="None" ErrorMessage="Please Enter Group Arabic  Name " ValidationGroup="VGGroup"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEGroupNameAr" runat="server" Enabled="True" TargetControlID="RFVGroupNameAr">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td width="135px">
                    <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"></asp:Label>
                </td>
                <td colspan="2">
                    <telerik:RadComboBox ID="RadCmbBxCompany" AutoPostBack="true" AllowCustomText="false"
                        Width="450px" MarkFirstMatch="true" CausesValidation="false" Skin="Vista" runat="server">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ValidationGroup="VGGroup"
                        InitialValue="--Please Select--" ControlToValidate="RadCmbBxCompany" Display="None"
                        ErrorMessage="Please Select Company"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCompany" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvCompany" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td width="135px">
                    <asp:Label ID="lblEntity" runat="server" Text="Entity" CssClass="Profiletitletxt"></asp:Label>
                </td>
                <td colspan="2">
                    <telerik:RadComboBox ID="RadCmbBxEntity" AllowCustomText="false" MarkFirstMatch="true"
                        Width="450px" Skin="Vista" runat="server" CausesValidation="false">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
                        InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Entity"
                        ValidationGroup="VGGroup"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblActive" runat="server" CssClass="Profiletitletxt" Text="Active"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkActive" runat="server" CausesValidation="false" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="VGGroup" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdGroupDetails"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdGroupDetails" runat="server" AllowPaging="True" Skin="Hay"
                        AllowSorting="true" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdGroupDetails_ItemCommand"
                        ShowFooter="True">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="GroupId,GroupCode">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                        <asp:HiddenField ID="hdnEntityArabicName" runat="server" Value='<%# Eval("EntityArabicName") %>' />
                                        <asp:HiddenField ID="hdnGroupNameAr" runat="server" Value='<%# Eval("GroupNameAr") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="GroupId" HeaderText="GroupId"
                                    SortExpression="GroupId" Visible="False" UniqueName="GroupId" />
                                <telerik:GridBoundColumn DataField="GroupCode" HeaderText="Group Code" SortExpression="GroupCode"
                                    Resizable="False" UniqueName="GroupCode" />
                                <telerik:GridBoundColumn DataField="GroupNameEn" HeaderText="Group Name" SortExpression="GroupNameEn"
                                    UniqueName="GroupNameEn" />
                                <telerik:GridBoundColumn DataField="IsActive" HeaderText="Active" SortExpression="IsActive"
                                    Resizable="False" UniqueName="IsActive" />
                                <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" SortExpression="EntityName"
                                    Resizable="False" UniqueName="EntityName" />
                                <telerik:GridBoundColumn DataField="CREATED_BY" HeaderText="Created By" SortExpression="CREATED_BY"
                                    Resizable="False" UniqueName="CREATED_BY" />
                                <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" SortExpression="CREATED_DATE"
                                    Resizable="False" UniqueName="CREATED_DATE" DataFormatString="{0:dd/M/yyyy}" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
