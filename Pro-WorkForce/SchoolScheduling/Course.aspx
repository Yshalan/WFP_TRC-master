
<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Course.aspx.vb" Inherits="SchoolScheduling_Course" Title="Define Course" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<telerik:RadScriptBlock ID="scriptblock" runat="server">
        <script type="text/javascript">
            function colorChanged(sender) {
                sender.get_element().style.backgroundColor = "#" + sender.get_selectedColor();
                sender.get_element().style.color = "#" + sender.get_selectedColor();
                sender.get_element().value = "#" + sender.get_selectedColor();
            }
           </script>
    </telerik:RadScriptBlock>

    <script type="text/javascript" language="javascript">
        
        function ValidatePage() {
            var tabContainer = $get('<%=TabContainer1.ClientID%>');
            var valCntl = $get('<%=reqCourseCode.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqCourseCode.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            valCntl = $get('<%=reqCourseName.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqCourseName.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            valCntl = $get('<%=reqCourseNameAr.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderCourseNameAr.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
           
        }

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

                            <uc1:PageHeader ID="PageHeader1" runat="server"  HeaderText="Define Course" meta:resourcekey="DefineCourseResource1" />
                        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                            meta:resourcekey="TabContainer1Resource1">
                            <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Course" meta:resourcekey="Tab1Resource1">
                                <ContentTemplate>
                                    
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label CssClass="Profiletitletxt" ID="lblCourseCode" runat="server" Text="Course Code"
                                                    meta:resourcekey="lblCourseCodeResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtCourseCode" runat="server" meta:resourcekey="TxtCourseCodeResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="reqCourseCode" runat="server" ControlToValidate="TxtCourseCode"
                                                    Display="None" ErrorMessage="Please Enter a Course Code" ValidationGroup="Grp1"
                                                    meta:resourcekey="reqCourseCodeResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderreqCourseCode" runat="server" Enabled="True"
                                                    TargetControlID="reqCourseCode" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label CssClass="Profiletitletxt" ID="lblCourseEnName" runat="server" Text="English Name"
                                                    meta:resourcekey="lblCourseEnNameResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="TxtCourseName" runat="server" meta:resourcekey="TxtCourseNameResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="reqCourseName" runat="server" ControlToValidate="TxtCourseName"
                                                    Display="None" ErrorMessage="Please Enter a Course Name" ValidationGroup="Grp1"
                                                    meta:resourcekey="reqCourseNameResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderreqCourseName" runat="server" CssClass="AISCustomCalloutStyle"
                                                    Enabled="True" TargetControlID="reqCourseName" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label CssClass="Profiletitletxt" ID="lblCourseNameAr" runat="server" Text="Arabic Name"
                                                    meta:resourcekey="lblCourseNameArResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtCourseNameAr" runat="server" meta:resourcekey="txtCourseNameArResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="reqCourseNameAr" runat="server" ControlToValidate="txtCourseNameAr"
                                                    Display="None" ErrorMessage="Please Enter Course arabic name" ValidationGroup="Grp1"
                                                    meta:resourcekey="reqCourseNameArResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderCourseNameAr" runat="server" CssClass="AISCustomCalloutStyle"
                                                    Enabled="True" TargetControlID="reqCourseNameAr" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                      
                                      <div class="row">
                                      <div class="col-md-2">
                                            <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblColor" Text="Color" 
                                                meta:resourcekey="lblColorResource1" />
                                                </div>
                                                <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtTKColor" disabled="disabled" Style="background-color: #FFFFFF;
                                                color: #FFFFFF"  Text="#FFFFFF" meta:resourcekey="txtTKColorResource1"></asp:TextBox>
                                            <cc1:ColorPickerExtender runat="server" ID="ColorPickerExtender1" TargetControlID="txtTKColor"
                                                OnClientColorSelectionChanged="colorChanged" PopupButtonID="btnShowColorExtender"
                                                Enabled="True" />
                                                    </div>
                                                    <div class="col-md-1">
                                            <asp:ImageButton runat="server" ID="btnShowColorExtender" ImageUrl="~/images/icon_color_picker.gif"
                                                Height="20px"  Style="vertical-align: middle" meta:resourcekey="btnShowColorExtenderResource1" />
                                            <asp:RequiredFieldValidator ID="rfvColorCode" runat="server" ControlToValidate="txtTKColor"
                                                InitialValue="#FFFFFF" Display="None" ErrorMessage="Please Choose Color" ValidationGroup="shift"
                                                meta:resourcekey="rfvColorCodeResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="rfvColorCode_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvColorCode">
                                            </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            
                        </cc1:TabContainer>

                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" OnClientClick="javascript:return ValidatePage();"
                            ValidationGroup="Grp1" meta:resourcekey="ibtnSaveResource1" />
                        <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="ibtnDeleteResource1" />
                        <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" meta:resourcekey="ibtnRestResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="table-responsive">
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdCourse"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid ID="dgrdCourse" runat="server" AllowSorting="True" AllowPaging="True"
                            PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="dgrdCourseResource1">
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="CourseId,CourseCode">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CourseCode" SortExpression="CourseCode" HeaderText="Course Code"
                                        meta:resourcekey="GridBoundColumnResource8" UniqueName="CourseCode" />
                                    <telerik:GridBoundColumn DataField="CourseName" SortExpression="CourseName" HeaderText="Course English Name"
                                        meta:resourcekey="GridBoundColumnResource9" UniqueName="CourseName" />
                                    <telerik:GridBoundColumn DataField="CourseNameAr" SortExpression="CourseNameAr"
                                        HeaderText="Course Arabic Name" meta:resourcekey="GridBoundColumnResource10" UniqueName="CourseNameAr" />
                                    <telerik:GridBoundColumn DataField="CourseId" SortExpression="CourseId" HeaderText="CourseId"
                                        Visible="False" meta:resourcekey="GridBoundColumnResource11" UniqueName="CourseId" />
                                        <telerik:GridBoundColumn DataField="Color" SortExpression="Color" HeaderText="Color"
                                         meta:resourcekey="GridBoundColumnResource12" UniqueName="Color" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server" 
                                                meta:resourcekey="RadToolBarButtonResource1" Owner="" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
