<%@ Page Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="AssignSchedule.aspx.vb" Inherits="Admin_AssignSchedule" Title="Assign Schedule" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        </style>

    <script type="text/javascript">
    function showPopup(path, name, height, width) {
        var options = 'width=' + width + ',height=' + height;
        var newwindow;
        newwindow = window.open(path, name, options);
        if (window.focus) {
            newwindow.focus();
        }
    }
    function open_window(url, target, w, h) { //opens new window 
        var parms = "width=" + w + ",height=" + h + ",menubar=no,location=no,resizable,scrollbars";
        var win = window.open(url, target, parms);
        if (win) {
            win.focus();
        }
    }

    function CheckBoxListSelect(state) {
        var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<center>
    <uc1:PageHeader ID="PageHeader1" runat="server"/>
    </center> <br />
    
    <asp:UpdatePanel ID="pnlAssignEmployees" runat="server"><ContentTemplate>
    <table class="style1">
       <tr><td colspan="3">
        <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged" OneventEntitySelected="FillEmployee" />
        </td></tr>
        <tr>
            <td width="195px">
                <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" 
                    Text="Schedule Type" meta:resourcekey="Label7Resource1"></asp:Label>
            </td>
            <td>
                <telerik:RadComboBox ID="RadCmbBxScheduletype" MarkFirstMatch="True"
                    Skin="Vista" runat="server" AutoPostBack="True" 
                    meta:resourcekey="RadCmbBxScheduletypeResource1">
                    <Items>
                        <telerik:RadComboBoxItem Text="--Please Select--" Value="-1" runat="server" 
                            meta:resourcekey="RadComboBoxItemResource1" />
                        <telerik:RadComboBoxItem Text="Normal" Value="1" runat="server" 
                            meta:resourcekey="RadComboBoxItemResource2" />
                        <telerik:RadComboBoxItem Text="Flexible" Value="2" runat="server" 
                            meta:resourcekey="RadComboBoxItemResource3" />
                        <telerik:RadComboBoxItem Text="Advanced" Value="3" runat="server" 
                            meta:resourcekey="RadComboBoxItemResource4" />
                    </Items>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvScheduletype" ValidationGroup="grpSave" runat="server"
                    ControlToValidate="RadCmbBxScheduletype" InitialValue="--Please Select--" Display="None"
                    ErrorMessage="Please Select Schedule Type" 
                    meta:resourcekey="rfvScheduletypeResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvScheduletype" 
                    WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" 
                    Text="Schedules" meta:resourcekey="Label3Resource1"></asp:Label>
            </td>
            <td>
                <telerik:RadComboBox ID="RadCmbBxSchedules" MarkFirstMatch="True"
                    Skin="Vista" runat="server" meta:resourcekey="RadCmbBxSchedulesResource1">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvSchedule" ValidationGroup="grpSave" InitialValue="--Please Select--"
                    runat="server" ControlToValidate="RadCmbBxSchedules" Display="None" 
                    ErrorMessage="Please Select Schedule" meta:resourcekey="rfvScheduleResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vceSchedule" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvSchedule" WarningIconImageUrl="~/images/warning1.png" 
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" 
                    Text="From Date" meta:resourcekey="Label4Resource1"></asp:Label>
            </td>
            <td>
                <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" 
                    Culture="English (United States)" meta:resourcekey="dtpFromdateResource1">
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                        Width="">
                    </DateInput>
                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" 
                    Text="Is Temporary" meta:resourcekey="Label6Resource1"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" 
                    meta:resourcekey="chckTemporaryResource1" />
            </td>
            <td>
            </td>
        </tr>
        <asp:Panel ID="pnlEndDate" runat="server" Visible="False" 
            meta:resourcekey="pnlEndDateResource1">
            <tr>
                <td>
                    <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" 
                        Text="End date" meta:resourcekey="lblEndDateResource1"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" 
                        Culture="English (United States)" meta:resourcekey="dtpEndDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" 
                    Text="List Of Employees" meta:resourcekey="Label5Resource1"></asp:Label>
            </td>
            <td>
            
            <table>
            <tr><td><div style="width: 200px; height: 200px; overflow: auto; border-style: solid; border-width: 1px;
                    border-color: #ccc">
                    <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                        DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                    </asp:CheckBoxList>
                    
                </div></td>
                <td style="vertical-align:top"><table>
                <tr><td><a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size:8pt"><asp:Literal ID="Literal1" 
                                                    runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a></td></tr>
                <tr><td><a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size:8pt"><asp:Literal ID="Literal2" 
                                                    runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a></td></tr>
                </table></td></tr>
            </table>

            </td>
            <td>
                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" 
                    meta:resourcekey="hlViewEmployeeResource1">View Org Level Employees </asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3">
         <center>
                <asp:Button ID="btnSave" runat="server" Text="Assign Schedule" Width="110px" ValidationGroup="grpSave"
                    CssClass="button" meta:resourcekey="btnSaveResource1" /></center>
            </td>
        </tr>
    </table></ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
