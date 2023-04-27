<%@ Page Language="VB" StylesheetTheme="Default" Title="Send Notification" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"  AutoEventWireup="false" CodeFile="SendNotification.aspx.vb" Inherits="Admin_SendNotification"  meta:resourcekey="PageResource3"  UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter" TagPrefix="uc2" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <uc1:PageHeader ID="PageHeader1" HeaderText="Leave Types" runat="server" />
            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter" ValidationGroup="grpCardPrinting" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblDeductType" runat="server" Text="Type of Deducting"
                        meta:resourcekey="lblDeductTypeResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:radcombobox ID="ddlDeductType" runat="server" AppendDataBoundItems="True"
                        MarkFirstMatch="True"  Skin="Vista" meta:resourcekey="ddlDeductTypeResource1" xmlns:telerik="telerik.web.ui" />
                  <%--  <asp:RequiredFieldValidator ID="rfvDeductType" runat="server" ControlToValidate="ddlDeductType"
                        Display="None" ErrorMessage="Please Select Type of Deducting" InitialValue="--Please Select--" ValidationGroup="grpCardPrinting"
                        meta:resourcekey="rfvDeductTypeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceDeductType" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvDeductType" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>--%>
                </div>
            </div>
    
            <div class="row">
                     <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblDob" runat="server" Text="Leave Date"
                                        meta:resourcekey="lblleavedateResource1"></asp:Label>
                                </div>
                      <div class="col-md-4">
                      <telerik:RadDatePicker ID="dtpleavedate" runat="server" DateInput-MinDate="01/01/1920 12:00:00 AM"
                                        Culture="English (United States)" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpleavedateResource1"
                                      >
                                        <Calendar runat="server" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput runat="server"  DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                      </telerik:RadDatePicker>
                      </div>
             </div>
            <div class ="row">
                    <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label1" runat="server" Text="Number of Deducted Days"
                                        meta:resourcekey="lbldeducteddaysResource1"></asp:Label>
                    </div>
                 <div class="col-md-4">
                <asp:TextBox ID="txtDeductedDaysNo" runat="server" meta:resourcekey="TxtDeductedDaysNoResource1"
                    Width="350px"></asp:TextBox>
                </div>
           </div>
            <div class ="row">
                  <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label3" runat="server" Text="Number of Hours of Departure"
                                        meta:resourcekey="lblNoofhoursResource1"></asp:Label>
                  </div>
                 <div class="col-md-4">
                <asp:TextBox ID="TextBox2" runat="server" meta:resourcekey="TxtHoursResource1"
                    Width="350px"></asp:TextBox>
                </div>
         </div>
            <div class ="row">
                    <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Text="Deduction Reason"
                                        meta:resourcekey="lbldeductreasonResource1"></asp:Label>
                    </div>
                 <div class="col-md-4">
                <asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TxtDeductionReasonResource1"
                    Width="350px"></asp:TextBox>
                </div>
           </div>
            <div class ="row">
                    <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server" Text="Email Address"
                                        meta:resourcekey="lblEmailAddressResource1"></asp:Label>
                    </div>
                 <div class="col-md-4">
                <asp:TextBox ID="txtEmail" runat="server" meta:resourcekey="txtEmailResource1" ToolTip ="Please Enter Comma separated email addresses"
                    Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ErrorMessage="Please Enter Comma separated email addresses" ValidationGroup="gpNext"
                                        meta:resourcekey="reqEmailResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ExtenderEmail" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmail" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                </div>
           </div>
     

     <div class ="row">
      <div class="col-md-4">
                
                <asp:Button ID="btnSaveEmployee" runat="server" CssClass="button" Text="Save"
                    CausesValidation="False" meta:resourcekey="btnSaveEmployeeResource1"   />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDeductType"
                    Display="None" ErrorMessage="Please Enter Deduct Type" ValidationGroup="grpCardPrinting"
                    meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                    TargetControlID="RequiredFieldValidator2">
                </cc1:ValidatorCalloutExtender>
            </div>
      </div>
</asp:Content>

