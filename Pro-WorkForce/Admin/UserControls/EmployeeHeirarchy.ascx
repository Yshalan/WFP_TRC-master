<%@ Control Language="VB" AutoEventWireup="true" CodeFile="EmployeeHeirarchy.ascx.vb" 
Inherits="Admin_UserControls_EmployeeHeirarchy"  %>

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
 <table border="0" cellpadding="0" cellspacing="0" width="90%" style="margin-left:30px">
 <tr id="trCompanies" runat="server">
 <td style="width:100px"><asp:Label id="lblCompanies" runat="server" 
         CssClass="Profiletitletxt" meta:resourcekey="lblCompaniesResource1">Company</asp:Label></td>
 <td style="width:150"><telerik:RadComboBox ID="ddlCompanies" AutoPostBack="True" runat="server"
             OnSelectedIndexChanged="ddlCompanies_SelectedIndexChanged" Width="200px" 
         CausesValidation="False" meta:resourcekey="ddlCompaniesResource1"
             ></telerik:RadComboBox>
             <asp:RequiredFieldValidator ID="rfvEmployeeCompany" runat="server" Display="None"
                                ErrorMessage="Please Select a Company" ControlToValidate="ddlCompanies"
                                InitialValue="--Please Select--" 
         ValidationGroup="FilterEmployee" 
         meta:resourcekey="rfvEmployeeCompanyResource1" ></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="rfvEmployeeCompany_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="rfvEmployeeCompany">
                            </cc1:ValidatorCalloutExtender></td>
 </tr>
 </table>                
 <asp:GridView ID="gvEmplotyeeFilter" runat="server" Width="90%" style="margin-left:30px" 
         AutoGenerateColumns="False" BorderStyle="None" 
         ShowHeader="False" meta:resourcekey="gvEmplotyeeFilterResource1" >
         <Columns>
         
          <asp:TemplateField HeaderText="Level Name" 
                 meta:resourcekey="TemplateFieldResource1">
             <ItemTemplate>
             <asp:Label ID="lblLevelName" runat="server" CssClass="Profiletitletxt" 
                     Text='<%# Bind("LevelName") %>' meta:resourcekey="lblLevelNameResource1" ></asp:Label>
             </ItemTemplate>
              <ItemStyle BorderStyle="None" />
             </asp:TemplateField>
                            
             <asp:TemplateField HeaderText="OrgLevel" 
                 meta:resourcekey="TemplateFieldResource2">
             <ItemTemplate>
             <telerik:RadComboBox ID="ddlHeirarchy" AutoPostBack="True" runat="server" EnableViewState="False"
             OnSelectedIndexChanged="ddlTest_SelectedIndexChanged" Width="200px" 
                     meta:resourcekey="ddlHeirarchyResource1"></telerik:RadComboBox>
             </ItemTemplate>
                 <ItemStyle Width="150px" BorderStyle="None" />
             </asp:TemplateField>
         </Columns>
     </asp:GridView>
      <table border="0" cellpadding="0" cellspacing="0" width="90%" style="margin-left:30px">
 <tr>
 <td style="width:100px"><asp:Label id="lblEmployee" runat="server" 
         CssClass="Profiletitletxt" meta:resourcekey="lblEmployeeResource1">Employee</asp:Label></td>
 <td style="width:150"><telerik:RadComboBox ID="ddlEmployees" AutoPostBack="True" runat="server"
             OnSelectedIndexChanged="ddlEmployees_SelectedIndexChanged" 
         Width="200px" CausesValidation="False" meta:resourcekey="ddlEmployeesResource1"
             ></telerik:RadComboBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                ErrorMessage="Please Select a Company" ControlToValidate="ddlCompanies"
                                InitialValue="--Please Select--" 
         ValidationGroup="FilterEmployee" 
         meta:resourcekey="RequiredFieldValidator1Resource1" ></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1"
                                runat="server" Enabled="True" TargetControlID="rfvEmployeeCompany">
                            </cc1:ValidatorCalloutExtender></td>
 </tr>
 </table> 
     
 </ContentTemplate>
 </asp:UpdatePanel>
 




