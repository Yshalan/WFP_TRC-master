<%@ Page Language="VB" StylesheetTheme="SvTheme" AutoEventWireup="false" MasterPageFile="~/Default/NewMaster.master"
    CodeFile="ChangeEmployeeEntity.aspx.vb" Inherits="Employee_ChangeEmployeeEntity"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

    </script>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" meta:resourcekey="PageHeader1Resource1"/>
             <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label1Resource1"
                        Text="Company"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxCompanies1" runat="server" AutoPostBack="True" CausesValidation="False"
                        MarkFirstMatch="True" Skin="Vista" Style="width: 350px" 
                        meta:resourcekey="RadCmbBxCompaniesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvCompanies1" runat="server" ControlToValidate="RadCmbBxCompanies1"
                        Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company"
                        meta:resourcekey="rfvCompanies1Resource1" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvCompanies1" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc4:multiemployeefilter id="MultiEmployeeFilterUC" runat="server" onevententityselected="EntityChanged"
                        oneventworkgroupselect="WorkGroupChanged" oneventworklocationsselected="WorkLocationsChanged"/>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                        <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" CssClass="checkboxlist"
                            DataTextField="EmployeeName" DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                        Text="View Org Level Employees "></asp:HyperLink>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CustomValidator ID="cvEmpListValidation" ErrorMessage="please select at least one employee"
                        ValidationGroup="btnSave" ForeColor="Black" runat="server" CssClass="customValidator"
                        meta:resourcekey="cvEmpListValidationResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-10">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Company" meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxCompanies" AutoPostBack="True" runat="server"
                         Filter="Contains" MarkFirstMatch="True" Skin="Vista"
                         Style="width: 350px" meta:resourcekey="RadCmbBxCompaniesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                        Display="None" ErrorMessage="Please Select Company" ValidationGroup="btnSave"
                        meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblLevels" runat="server" Text="Entity" CssClass="Profiletitletxt"
                        meta:resourcekey="lblLevelsResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxEntity" Filter="Contains"
                        MarkFirstMatch="True" Skin="Vista" runat="server" AutoPostBack="True"
                        Style="width: 350px" meta:resourcekey="RadCmbBxEntityResource1">
                    </telerik:RadComboBox>
                    <asp:HiddenField ID="hdnIsEntityClick" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
                        InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Entity"
                        meta:resourcekey="rfvEntityResource1" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Update" class="button" ValidationGroup="btnSave"
                        meta:resourcekey="btnaddResource1"  />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




