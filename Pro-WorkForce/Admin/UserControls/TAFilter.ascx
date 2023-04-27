<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TAFilter.ascx.vb" Inherits="Admin_UserControls_TAFilter" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
    function disableValidator() {
        document.getElementById("<%=rfvEmployeeCompany.ClientID %>").disabled = true;
    } 

</script>
<asp:UpdatePanel ID="pnlHeirarchy" runat="server">
    <ContentTemplate>
            <div class="row" id="trCompanies" runat="server">
                <div class="col-md-2">
                    <asp:Label ID="lblCompanies" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblCompaniesResource1">Company</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlCompanies" AutoPostBack="True" runat="server" Filter="Contains"
                        MarkFirstMatch="true"  CausesValidation="False" meta:resourcekey="ddlCompaniesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvEmployeeCompany" runat="server" Display="None"
                        ErrorMessage="Please Select a Company" ControlToValidate="ddlCompanies" InitialValue="--Please Select--"
                        ValidationGroup="FilterEmployee" meta:resourcekey="rfvEmployeeCompanyResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="rfvEmployeeCompany_ValidatorCalloutExtender" runat="server"
                        Enabled="True" TargetControlID="rfvEmployeeCompany">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
        <div class="row">
            <div class="col-md-6">
        <asp:GridView ID="gvEmplotyeeFilter" runat="server" 
            AutoGenerateColumns="False" BorderStyle="Solid" ShowHeader="False" meta:resourcekey="gvEmplotyeeFilterResource1">
            <Columns>
                <asp:TemplateField HeaderText="Level Name" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:Label ID="lblLevelName" runat="server" CssClass="Profiletitletxt" Text='<%# Bind("LevelName") %>'
                            meta:resourcekey="lblLevelNameResource1"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderStyle="None" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OrgLevel" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="ddlHeirarchy" AutoPostBack="True" runat="server" MarkFirstMatch="true"
                            EnableViewState="False" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"
                             meta:resourcekey="ddlHeirarchyResource1">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                    <ItemStyle  BorderStyle="None" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblEmployee" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblEmployeeResource1">Employee</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlEmployees" AutoPostBack="True" runat="server" Filter="Contains"
                        MarkFirstMatch="true" OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged"
                       CausesValidation="False" meta:resourcekey="ddlEmployeesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                        ErrorMessage="Please Select a Company" ControlToValidate="ddlCompanies" InitialValue="--Please Select--"
                        ValidationGroup="FilterEmployee" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="rfvEmployeeCompany">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
