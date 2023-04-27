<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Assign_Multi_Schedule.aspx.vb" Inherits="DailyTasks_Assign_Multi_Schedule"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../DailyTasks/UserControls/AssignSchedule_Employee.ascx" TagName="Assign_Emp"
    TagPrefix="uc1" %>
<%@ Register Src="../DailyTasks/UserControls/AssignSchedule_LogicalGroup.ascx" TagName="Assign_Logic"
    TagPrefix="uc2" %>
<%@ Register Src="../DailyTasks/UserControls/AssignSchedule_WorkLoc.ascx" TagName="Assign_WorkLoc"
    TagPrefix="uc3" %>
<%@ Register Src="../DailyTasks/UserControls/AssignSchedule_Entity.ascx" TagName="Assign_Entity"
    TagPrefix="uc4" %>
<%@ Register Src="../DailyTasks/UserControls/AssignSchedule_Company.ascx" TagName="Assign_Company"
    TagPrefix="uc5" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc6" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                <uc6:PageHeader ID="Assign_Company" runat="server" />
            <div id="img_en" runat="server" visible="true">
                <image src="../images/Eng-assign-schedule.png"></image>
            </div>
            <div id="img_ar" runat="server" visible="true">
                <image src="../images/Ar-assign-schedule.png"></image>
            </div>
            </br>
            <cc1:TabContainer ID="TabContainer1" runat="server" AutoPostBack="True" ActiveTabIndex="0"
                OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="Assign_by_Emp" runat="server" HeaderText="Employees" meta:resourcekey="TabPanel1Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc1:Assign_Emp ID="objAssign_Emp" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Logical Group" meta:resourcekey="TabPanel2Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:Assign_Logic ID="objAssign_Logic" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Work Location" meta:resourcekey="TabPanel3Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc3:Assign_WorkLoc ID="objAssign_WorkLoc" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Entity" meta:resourcekey="TabPanel4Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc4:Assign_Entity ID="objAssign_Entity" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Company" meta:resourcekey="TabPanel5Resource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc5:Assign_Company ID="objAssign_Company" runat="server" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
