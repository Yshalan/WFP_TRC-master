
<%@ Page Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ClassGrade.aspx.vb" Title="Class Grade" Inherits="SchoolScheduling_ClassGrade" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function ValidatePage() {
            var tabContainer = $get('<%=TabContainer1.ClientID%>');
            var valCntl = $get('<%=reqClassGradeEngName.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqClassGradeEngName.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            var valCntl = $get('<%=reqClassGradeArName.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderClassGradeArName.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

           
            var valCntl = $get('<%=reqOrder1.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqOrder.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                     
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

                            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Define Class Grade" meta:resourcekey="PageHeader1Resource1"/>
                        <cc1:tabcontainer id="TabContainer1" runat="server" activetabindex="0" onclientactivetabchanged="hideValidatorCalloutTab"
                            meta:resourcekey="TabContainer1Resource1">
                            <cc1:TabPanel ID="TabClassGrade" runat="server" HeaderText="Class Grade" meta:resourcekey="Tab1Resource1">
                                <ContentTemplate>
                                    
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                                    meta:resourcekey="Label1Resource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtClassGradeEnglish" runat="server" meta:resourcekey="txtClassGradeEnglishResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="reqClassGradeEngName" runat="server" ControlToValidate="txtClassGradeEnglish"
                                                    Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrClassGrade"
                                                    meta:resourcekey="reqClassGradeEngNameResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                                        ID="ExtenderreqClassGradeEngName" runat="server" Enabled="True" TargetControlID="reqClassGradeEngName"
                                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                                    meta:resourcekey="Label2Resource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtClassGradeArabic" runat="server" meta:resourcekey="txtClassGradeArabicResource1"></asp:TextBox>                                          
                                                <asp:RequiredFieldValidator ID="reqClassGradeArName" runat="server" ControlToValidate="txtClassGradeArabic"
                                                    Display="None" ErrorMessage="Please Enter Arablic Name" ValidationGroup="GrClassGrade"
                                                    meta:resourcekey="reqClassGradeArNameResource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                                        ID="ExtenderClassGradeArName" runat="server" Enabled="True" TargetControlID="reqClassGradeArName"
                                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                     <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblOrder" runat="server" CssClass="Profiletitletxt" Text="Order"
                                                    meta:resourcekey="lblOrderResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtOrder" runat="server" meta:resourcekey="txtOrderResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="reqOrder1" runat="server" ControlToValidate="txtOrder"
                                                    Display="None" ErrorMessage="Please Enter Order" ValidationGroup="GrClassGrade"
                                                    meta:resourcekey="reqOrderResource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender
                                                        ID="ExtenderreqOrder" runat="server" Enabled="True" TargetControlID="reqOrder1"
                                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                                    </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>                                      
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabGradeCourses" runat="server" HeaderText="Grade Courses" TabIndex="1" Visible="true" meta:resourcekey="Tab2Resource1" >
                                <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCourse" runat="server" CssClass="Profiletitletxt" Text="Course"
                                                    meta:resourcekey="lblCourseResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                               <telerik:RadComboBox ID="RadCmbCourse" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" >
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="reqCourse" runat="server" ControlToValidate="RadCmbCourse"
                                                Display="None" ErrorMessage="Please select Course" InitialValue="--Please Select--"
                                                ValidationGroup="groupAddCourse" meta:resourcekey="reqCourseResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqCourse" runat="server" CssClass="AISCustomCalloutStyle"
                                                Enabled="True" TargetControlID="reqCourse" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                            </div>
                                            
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblWeeklyNo" runat="server" CssClass="Profiletitletxt" Text="Weekly Course Number"
                                                    meta:resourcekey="lblWeeklyNoResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtWeeklyNo" runat="server" meta:resourcekey="txtWeeklyNoResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="ReqWeeklyNo" runat="server" ControlToValidate="txtWeeklyNo"
                                                    Display="None" ErrorMessage="Please enter Weekly Cources Number" ValidationGroup="groupAddCourse"
                                                    meta:resourcekey="ReqWeeklyNoResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderReqWeeklyNo" runat="server" Enabled="True"
                                                    TargetControlID="ReqWeeklyNo">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 text-center">
                                                <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="button" ValidationGroup="groupAddCourse"
                                                    meta:resourcekey="btnAddResource1" />
                                                <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove" meta:resourcekey="btnRemoveResource1" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="table-responsive">
                                                <telerik:RadGrid ID="dgrdClassGradeCourses" runat="server" AllowSorting="true" AllowPaging="True" 
                                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                                    PageSize="15" meta:resourcekey="dgrdClassGradeCoursesResource1">
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="WeeklyCourcesNumber,FK_ClassGradeId,FK_CourseId,CourseName,ClassGradeName,ClassGradeId">
                                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                        <Columns>
                                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                                UniqueName="TemplateColumn">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;"/>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="CourseCode" SortExpression="CourseCode" HeaderText="Course Code"
                                                                UniqueName="CourseCode" meta:resourcekey="CourseCodeResource1" />
                                                            <telerik:GridBoundColumn DataField="CourseName" SortExpression="CourseName" HeaderText="Course Name"
                                                                UniqueName="CourseName" meta:resourcekey="GridBoundColumnResource1" />
                                                            <telerik:GridBoundColumn DataField="CourseNameAr" SortExpression="CourseNameAr"
                                                                HeaderText="Course Arabic Name" UniqueName="CourseNameAr" meta:resourcekey="GridBoundColumnResource2" />
                                                             <telerik:GridBoundColumn DataField="WeeklyCourcesNumber" SortExpression="WeeklyCourcesNumber"
                                                                HeaderText="Weekly Cources Number" UniqueName="WeeklyCourcesNumber" meta:resourcekey="GridBoundColumnResource3" />
                                                    <telerik:GridBoundColumn DataField="FK_ClassGradeId" SortExpression="FK_ClassGradeId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource20" UniqueName="FK_ClassGradeId" />
                                                     <telerik:GridBoundColumn DataField="FK_CourseId" SortExpression="FK_CourseId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource21" UniqueName="FK_CourseId" />
                                                        </Columns>
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                    </MasterTableView>
                                                    <SelectedItemStyle ForeColor="Maroon" />
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                           
                        </cc1:tabcontainer>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="GrClassGrade"
                                        OnClientClick="javascript:return ValidatePage();" meta:resourcekey="ibtnSaveResource1" />
                                    <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                                        meta:resourcekey="ibtnDeleteResource1" />
                                    <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                                        meta:resourcekey="ibtnRestResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="table-responsive">
                                    <div>
                                        <telerik:radfilter runat="server" id="RadFilter1" filtercontainerid="dgrdClassGrade"
                                            skin="Hay" showapplybutton="False" meta:resourcekey="RadFilter1Resource1" />
                                    </div>
                                    <telerik:radgrid id="dgrdClassGrade" runat="server" allowsorting="True" allowpaging="True"
                                         pagesize="15"  gridlines="None" showstatusbar="True"
                                        allowmultirowselection="True" showfooter="True" onitemcommand="dgrdClassGrade_ItemCommand"
                                        meta:resourcekey="dgrdClassGradeResource1">
                                        <SelectedItemStyle ForeColor="Maroon" />
                                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource5"
                                                    UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource3" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="ClassGradeName" SortExpression="ClassGradeName" HeaderText="Class Grade English Name"
                                                    meta:resourcekey="GridBoundColumnResource16" UniqueName="ClassGradeName" />
                                                <telerik:GridBoundColumn DataField="ClassGradeNameAr" SortExpression="ClassGradeNameAr"
                                                    HeaderText="Class Grade Arabic Name" meta:resourcekey="GridBoundColumnResource17"
                                                    UniqueName="ClassGradeNameAr" />
                                                    <telerik:GridBoundColumn DataField="ClassGradeOrder" SortExpression="ClassGradeOrder"
                                                    HeaderText="Class Grade Order" meta:resourcekey="GridBoundColumnResource19"
                                                    UniqueName="ClassGradeOrder" />
                                                <telerik:GridBoundColumn DataField="ClassGradeId" SortExpression="ClassGradeId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource18" UniqueName="ClassGradeId" />
                                            </Columns>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <CommandItemTemplate>
                                                <telerik:RadToolBar runat="server" ID="RadToolBar1" 
                                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                                    <Items>
                                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="<%# GetFilterIcon() %>"
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
                                    </telerik:radgrid>
                                </div>
                            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
