<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Left.ascx.vb" Inherits="UserControls_left" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<telerik:RadPanelBar runat="server" ID="RadPanelBar1" Height="100%" 
    PersistStateInCookie="True" Width="210px" Skin="Windows7"  CssClass="Normaltxt">  
    <Items>
    
    <telerik:RadPanelItem Text="User Functionalites">
    <Items>
    <telerik:RadPanelItem Text="Account Profile"></telerik:RadPanelItem>
    <telerik:RadPanelItem Text="Authorized Persons"></telerik:RadPanelItem>
     <telerik:RadPanelItem Text="Register New Product"></telerik:RadPanelItem>
    </Items>
    </telerik:RadPanelItem>
    </Items>         
        </telerik:RadPanelBar>
