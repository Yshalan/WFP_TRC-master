<%@ Page Title="" Language="VB"  Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="UserPrivileges.aspx.vb" Inherits="Employee_UserPrevileges" meta:resourcekey="PageResource1"
    UICulture="auto" Culture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc5" %>
<%@ Register Src="UserControls/UserPrivilege_Companies.ascx" TagName="CompanyPrev"
    TagPrefix="uc1" %><%@ Register Src="UserControls/UserPrivilege_Entities.ascx" TagName="EntityPrev"
    TagPrefix="uc2" %><%@ Register Src="UserControls/UserPrivilege_LogicalGroup.ascx" TagName="LogicalPrev"
    TagPrefix="uc3" %><%@ Register Src="UserControls/UserPrivilege_WorkLocations.ascx" TagName="WorkLocPrev"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();
                }
            }
            catch (err) {
            }
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
        <uc5:PageHeader ID="PageHeader1" runat="server" />
        <div class="row">
            <div class="col-md-12">
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                    OnClientActiveTabChanged="hideValidatorCalloutTab" CssClass="" 
                    meta:resourcekey="TabContainer1Resource2">
                    <cc1:TabPanel ID="Company" runat="server" HeaderText="Company" meta:resourcekey="CompanyResource1">
                        <ContentTemplate>
                            <uc1:CompanyPrev ID="objCompanyPrev" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Entity" runat="server" HeaderText="Entity" meta:resourcekey="EntityResource1">
                        <ContentTemplate>
                            <uc2:EntityPrev ID="objEntityPrev" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="LogicalGroup" runat="server" Visible="true" HeaderText="Logical Group"
                        meta:resourcekey="LogicalGroupResource1">
                        <ContentTemplate>
                            <uc3:LogicalPrev ID="objLogicalGroupPrev" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="WorkLocation" runat="server" Visible="true" HeaderText="Work Location"
                        meta:resourcekey="WorkLocationResource1">
                        <ContentTemplate>
                            <uc4:WorkLocPrev ID="WorkLocationPrev" runat="server" />
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </div>
</asp:Content>
