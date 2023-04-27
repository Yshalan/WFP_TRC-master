<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Tree-Organization.aspx.vb" Inherits="Admin_Tree_Organization" Title="Organization Structure"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/OrgCompany.ascx" TagName="OrgCompany" TagPrefix="uc2" %>
<%@ Register Src="UserControls/OrgEntity.ascx" TagName="OrgEntity" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .RadTreeStyle
        {
            min-width: 200px;
            max-width: 250px;
            max-height: 550px;
            border: solid 1px #D2D2D2;
            background-color: #FAFAFA;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <div>
        <div class="row">
            <div class="col-md-3">
                <div style="overflow: scroll" class="RadTreeStyle">
                    <telerik:RadTreeView ID="RadTreeView1" runat="server" Skin="Windows7">
                    </telerik:RadTreeView>
                </div>
            </div>
            <div class="col-md-6">

                <uc2:OrgCompany ID="OrgCompany" runat="server" Visible="False" Location="TreeOrgn"
                    style="border: solid 1px red;" />
                <div style="border: solid 1px #D2D2D2; padding: 0px; margin: 0; clear: both; background-color: #FAFAFA;">
                    <uc3:OrgEntity ID="OrgEntity" runat="server" Visible="False" Location="TreeOrgn" style="margin-left: 1px" />
                </div>
            </div>
        </div>

    </div>
</asp:Content>
