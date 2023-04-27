<%@ Page Language="VB" AutoEventWireup="false" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    CodeFile="AssignManager.aspx.vb" Inherits="Admin_AssignManager" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc3" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:UpdatePanel ID="pnlAssignEmployees" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlManagerInfo" runat="server" GroupingText="Manager Information"
                meta:resourcekey="pnlManagerInfoResource1">
                <uc3:EmployeeFilter ID="EmployeeFilter1" runat="server" ValidationGroup="grpSave"
                    OneventEmployeeSelect="CompanyChanged" />
            </asp:Panel>
                <div class="row">
                    <div class="col-md-12">
                        <%--<uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                            OneventEntitySelected="FillEmployee" />--%>
                        <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                            OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged"
                            OneventCompanySelect="OtherCompanyChanged" ShowOtherCompany="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="From Date"
                            meta:resourcekey="Label4Resource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                            meta:resourcekey="dtpFromdateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Is Temporary"
                            meta:resourcekey="Label6Resource1" />
                    </div>
                </div>
                <asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" Text="End date"
                                meta:resourcekey="lblEndDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                                meta:resourcekey="dtpEndDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:CompareValidator ID="cvDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                                ErrorMessage="End Date should be greater than or equal to From Date" Display="None"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="cvDateResource1">
                            </asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="cvDate" ID="vceDate" runat="server"
                                Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                </asp:Panel>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc;
                        margin-top:5px; border-radius:5px">
                        <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                            DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1">View Org Level Employees </asp:HyperLink>
                </div>
            </div>


              <div class="row">
               <div  class="col-md-2"></div>
                     <div  class="col-md-10">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="row">
                <div  class="col-md-12"><br /></div> 
                </div> 


                <div class="row">
                    <div class="col-md-12 text-center">
                            <asp:Button ID="btnAssign" runat="server" Text="Assign Manager" ValidationGroup="grpSave"
                                CssClass="button" meta:resourcekey="btnAssignResource1" /></center>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
