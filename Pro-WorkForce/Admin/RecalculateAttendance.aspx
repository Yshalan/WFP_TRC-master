<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RecalculateAttendance.aspx.vb" 
  StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master" Inherits="Admin_RecalculateAttendance" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 </asp:Content>
 
 
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
 <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 80px;
        }
    </style>
    
 <asp:UpdatePanel ID="pnlEmployee" runat="server">
                    <ContentTemplate>
                    <uc1:PageFilter ID="UserCtrlOrgHeirarchy" runat="server" HeaderText="View Employee Heirarchy" />
                    
                    <table class="style1">
                    <tr>
            <td width="154px">
            <asp:label id="lblFromDate" CssClass="Profiletitletxt" runat="server" >From Date</asp:label></td><td  >
				<telerik:RadDatePicker ID="dpFromDate" runat="server" Width="120px" 
                     Culture="English (United States)" DateInput-AutoPostBack="True" DateInput-CausesValidation="True" DateInput-Enabled="False">
                     <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                         >
                     </Calendar>
                     <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                     <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" 
                         LabelCssClass="" Width="" >
                     </DateInput>
                 </telerik:RadDatePicker>
				
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="dpFromDate" Display="None" 
                        ErrorMessage="Please Select Date,The Max Date Allowed is Today" 
                        meta:resourcekey="RequiredFieldValidator10Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator10_ValidatorCalloutExtender" 
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                    </cc1:ValidatorCalloutExtender>
				</td>
            </tr>
            
             <tr >
            	<td>
                <asp:label id="lblToDate" runat="server" CssClass="Profiletitletxt">To Date</asp:label></td>
                <td>
				<telerik:RadDatePicker ID="dpToDate" runat="server" Width="120px" DateInput-CausesValidation="True" DateInput-Enabled="False"
                        Culture="English (United States)" meta:resourcekey="dpToDateResource1" DateInput-AutoPostBack="True">
                     <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                        >
                     </Calendar>
                     <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                     <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" 
                         LabelCssClass="" Width="" >
                     </DateInput>
                 </telerik:RadDatePicker>
		
			    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="dpToDate" Display="None" 
                        ErrorMessage="Please Select Date,The Max Date Allowed is Today" 
                     meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" 
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
			        <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="dpFromDate" ControlToValidate="dpToDate" 
                        Display="None" ErrorMessage="To Date must be greater than or Equal From Date!" 
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="Save"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" 
                        Enabled="True" TargetControlID="CompareValidator1">
                    </cc1:ValidatorCalloutExtender>
             </td>
           
        </tr>
        <tr>
        <td></td>
        <td>
         <asp:Button id="btnStart" Text="Recalculate" CssClass="button" 
                runat="server" CausesValidation="true" ValidationGroup="ValidateComp" ></asp:Button>
        </td>
        </tr>
        
                    </table>
      
        
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </asp:Content>
