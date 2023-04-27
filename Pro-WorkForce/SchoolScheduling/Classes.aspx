
<%@ Page Language="VB"  Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Classes.aspx.vb" Inherits="SchoolScheduling_Classes" Title="Define Class" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">

        function ValidatePage() {
            var tabContainer = $get('<%=TabContainer1.ClientID%>');
           

            valCntl = $get('<%=reqClassName.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqClassName.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            valCntl = $get('<%=reqClassNameAr.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderClassNameAr.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            valCntl = $get('<%=reqGrade.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqGrade.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            valCntl = $get('<%=ExtenderreqCourse.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqCourse.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }

            valCntl = $get('<%=ExtenderreqTeacher.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderreqTeacher.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            valCntl = $get('<%=ExtenderReqWeeklyNo.ClientID%>');
            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ExtenderReqWeeklyNo.ClientID %>');
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

                            <uc1:PageHeader ID="PageHeader1" runat="server"  HeaderText="Define Class" meta:resourcekey="DefineClassResource1" />
                        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                            meta:resourcekey="TabContainer1Resource1">
                            <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Class" meta:resourcekey="Tab1Resource1">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblGrade" runat="server" CssClass="Profiletitletxt" Text="Class Grade"
                                                meta:resourcekey="lblGradeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadComboBox ID="RadCmbGrade" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="True" MarkFirstMatch="True" Skin="Vista"
                                                meta:resourcekey="RadCmbGradeResource1">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="reqGrade" runat="server" ControlToValidate="RadCmbGrade"
                                                Display="None" ErrorMessage="Please select Grade" InitialValue="--Please Select--"
                                                ValidationGroup="Grp1" meta:resourcekey="reqGradeResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqGrade" runat="server" CssClass="AISCustomCalloutStyle"
                                                Enabled="True" TargetControlID="reqGrade" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label CssClass="Profiletitletxt" ID="lblClassEnName" runat="server" Text="English Name"
                                                meta:resourcekey="lblClassEnNameResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="TxtClassName" runat="server" meta:resourcekey="TxtClassNameResource1"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="reqClassName" runat="server" ControlToValidate="TxtClassName"
                                                Display="None" ErrorMessage="Please Enter a Class Name" ValidationGroup="Grp1"
                                                meta:resourcekey="reqClassNameResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqClassName" runat="server" CssClass="AISCustomCalloutStyle"
                                                Enabled="True" TargetControlID="reqClassName" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label CssClass="Profiletitletxt" ID="lblClassNameAr" runat="server" Text="Arabic Name"
                                                meta:resourcekey="lblClassNameArResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtClassNameAr" runat="server" meta:resourcekey="txtClassNameArResource1"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="reqClassNameAr" runat="server" ControlToValidate="txtClassNameAr"
                                                Display="None" ErrorMessage="Please Enter Class arabic name" ValidationGroup="Grp1"
                                                meta:resourcekey="reqClassNameArResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderClassNameAr" runat="server" CssClass="AISCustomCalloutStyle"
                                                Enabled="True" TargetControlID="reqClassNameAr" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                              <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Courses&Teachers" meta:resourcekey="CoursesTeachersResource1">
                                <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblCourse" runat="server" CssClass="Profiletitletxt" Text="Course"
                                                    meta:resourcekey="lblCourseResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <telerik:RadComboBox ID="RadCmbCourse" runat="server" AppendDataBoundItems="True"
                                                    AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbStatusResource1">
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="reqCourse" runat="server" ControlToValidate="RadCmbCourse"
                                                    Display="None" ErrorMessage="Please select Course" InitialValue="--Please Select--"
                                                    ValidationGroup="Grp2" meta:resourcekey="reqCourseResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderreqCourse" runat="server" CssClass="AISCustomCalloutStyle"
                                                    Enabled="True" TargetControlID="reqCourse" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblTeacher" runat="server" CssClass="Profiletitletxt" Text="Teacher"
                                                    meta:resourcekey="lblTeacherResource1"></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <telerik:RadComboBox ID="RadCmbTeacher" runat="server" AppendDataBoundItems="True"
                                                    AutoPostBack="True"  MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbStatusResource1">
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="reqTeacher" runat="server" ControlToValidate="RadCmbTeacher"
                                                    Display="None" ErrorMessage="Please select Teacher" InitialValue="--Please Select--"
                                                    ValidationGroup="Grp2" meta:resourcekey="reqCourseResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderreqTeacher" runat="server" CssClass="AISCustomCalloutStyle"
                                                    Enabled="True" TargetControlID="reqTeacher" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">
                                                <asp:Label ID="lblWeeklyNo" runat="server" CssClass="Profiletitletxt" Text="Weekly Course Number"
                                                    meta:resourcekey="lblWeeklyNoResource1" ></asp:Label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtWeeklyNo" runat="server" Enabled="false" meta:resourcekey="txtWeeklyNoResource1"></asp:TextBox>
                                            
                                                <asp:RequiredFieldValidator ID="ReqWeeklyNo" runat="server" ControlToValidate="txtWeeklyNo"
                                                    Display="None" ErrorMessage="Please enter Weekly Cources Number" ValidationGroup="Grp2"
                                                    meta:resourcekey="ReqWeeklyNoResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ExtenderReqWeeklyNo" runat="server" Enabled="True"
                                                    TargetControlID="ReqWeeklyNo">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                                            <div class="row">
                                            <div class="col-md-8 text-center">
                                                <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="button" ValidationGroup="Grp2"
                                                    meta:resourcekey="btnAddResource1" />
                                                <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove" meta:resourcekey="btnRemoveResource1" />
                                              <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="ibtnRestResource1" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="table-responsive">
                                                <telerik:RadGrid ID="dgrdTeacherClasses" runat="server" AllowSorting="true" AllowPaging="True"
                                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                                    PageSize="15" meta:resourcekey="dgrdClassGradeCoursesResource1">
                                                   
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="FK_ClassId,CourseName,CourseNameAr,weeklyCount,FK_CourseId,FK_EmployeeId,TeacherNo,TeacherName">
                                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                        <Columns>
                                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                                UniqueName="TemplateColumn">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                                                </ItemTemplate>
                                                               
                                                                <FooterStyle CssClass="Profiletitletxt"/>
                                                            </telerik:GridTemplateColumn>
                                                             <telerik:GridBoundColumn DataField="TeacherNo" SortExpression="TeacherNo" HeaderText="Teacher No"
                                                                UniqueName="TeacherNo" meta:resourcekey="TeacherNoResource1" />
                                                            <telerik:GridBoundColumn DataField="TeacherName" SortExpression="TeacherName" HeaderText="Teacher Name"
                                                                UniqueName="TeacherName" meta:resourcekey="gbcTeacherNameResource1" />
                                                            <telerik:GridBoundColumn DataField="TeacherArabicName" SortExpression="TeacherArabicName"
                                                                HeaderText="Teacher Arabic Name" UniqueName="TeacherArabicName" meta:resourcekey="gbcTeacherArabicNamerResource" />
                                                                 <telerik:GridBoundColumn DataField="CourseName" SortExpression="CourseName"
                                                                HeaderText="Course Name" UniqueName="CourseName" meta:resourcekey="gbcCourseNameResource" />
                                                                 <telerik:GridBoundColumn DataField="CourseNameAr" SortExpression="CourseNameAr"
                                                                HeaderText="Course Arabic Name" UniqueName="CourseNameAr" meta:resourcekey="gbcCourseNameArResource" />
                                                             <telerik:GridBoundColumn DataField="weeklyCount" SortExpression="weeklyCount"
                                                                HeaderText="Weekly Cources Number" UniqueName="weeklyCount" meta:resourcekey="GridBoundColumnResource3" />
                                                    <telerik:GridBoundColumn DataField="FK_ClassId" SortExpression="FK_ClassId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource20" UniqueName="FK_ClassId" />
                                                     <telerik:GridBoundColumn DataField="FK_CourseId" SortExpression="FK_CourseId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource21" UniqueName="FK_CourseId" />
                                                        
                                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" SortExpression="FK_EmployeeId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource22" UniqueName="FK_EmployeeId" />
                                                        </Columns>
                                                        
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                    </MasterTableView>
                                                    <SelectedItemStyle ForeColor="Maroon" />
                                                </telerik:RadGrid>
                                            </div>
                                        </div>
                                 </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
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
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdClass"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid ID="dgrdClass" runat="server" AllowSorting="True" AllowPaging="True"
                            PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="dgrdClassResource1">
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ClassName,FK_ClassGradeId,ClassId">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ClassName" SortExpression="ClassName" HeaderText="Class English Name"
                                        meta:resourcekey="GridBoundColumnResource9" UniqueName="ClassName" />
                                    <telerik:GridBoundColumn DataField="ClassNameAr" SortExpression="ClassNameAr" HeaderText="Class Arabic Name"
                                        meta:resourcekey="GridBoundColumnResource10" UniqueName="ClassNameAr" />
                                    <telerik:GridBoundColumn DataField="ClassGradeName" SortExpression="ClassGradeName"
                                        HeaderText="Class Grade Name" meta:resourcekey="GridBoundColumnResource13" UniqueName="ClassGradeName" />
                                         <telerik:GridBoundColumn DataField="TotalweeklyCourses" SortExpression="TotalweeklyCourses"
                                        HeaderText="Total weekly Courses" meta:resourcekey="TotalweeklyClassesColumnResource13" UniqueName="TotalweeklyCourses" />
                                    <telerik:GridBoundColumn DataField="ClassId" SortExpression="ClassId" HeaderText="ClassId"
                                        Visible="False" meta:resourcekey="GridBoundColumnResource11" UniqueName="ClassId" />
                                    <telerik:GridBoundColumn DataField="FK_ClassGradeId" SortExpression="FK_ClassGradeId"
                                        HeaderText="FK_ClassGradeId" Visible="False" meta:resourcekey="FK_ClassGradeIdResource11"
                                        UniqueName="FK_ClassGradeId" />
                                        
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" 
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
