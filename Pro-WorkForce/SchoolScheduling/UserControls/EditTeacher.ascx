<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditTeacher.ascx.vb"
    Inherits="EditTeacher_WebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
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

    function ConfirmDeleteEmployee() {
        var Lang = '<%= Lang %>'
        if (Lang == "en-US") {
            return confirm('Are you sure you want to delete?');
        } else {
            return confirm('هل انت متاكد من الحذف ؟');
        }
    }

    function CheckBoxListSelect(state) {
        var chkBoxList = document.getElementById("<%= cblCourseList.ClientID %>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }
    function CheckBoxListSelect1(state) {
        var chkBoxList = document.getElementById("<%= cblGradeList.ClientID %>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }
</script>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
    EnableShadow="True" InitialBehavior="None" Style="z-index: 8000;" Modal="true">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
            Behaviors="Close, Move" EnableShadow="True" Height="600px" IconUrl="~/images/HeaderWhiteChrome.jpg"
            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
            Width="100%" Skin="Vista">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label CssClass="Profiletitletxt" ID="lblTeacherNumber" runat="server" Text="Teacher Number"
                                                meta:resourcekey="lblTeacherNumberResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtTeacherNumber" runat="server" Enabled="false" meta:resourcekey="txtTeacherNumberResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label CssClass="Profiletitletxt" ID="lblTeacherName" runat="server" Text="Teacher Name"
                                                meta:resourcekey="lblTeacherNameResource1"></asp:Label>
                                        </div>
                                        <div class="col-m-4">
                                            <asp:TextBox ID="txtTeacherName" runat="server"  Enabled="false" meta:resourcekey="txtTeacherNameResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label CssClass="Profiletitletxt" ID="lblTeacherArabicName" runat="server" Text="Teacher Arabic Name"
                                                meta:resourcekey="lblTeacherArabicNameResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtTeacherArabicName" runat="server"  Enabled="false"
                                                meta:resourcekey="txtTeacherArabicNameResource1"></asp:TextBox>
                                        </div>
                                    </div>
                    <cc1:TabContainer ID="TabEmployee" runat="server" ActiveTabIndex="0" Width="100%"
                        OnClientActiveTabChanged="hideValidatorCalloutTab">
                        <cc1:TabPanel ID="TabTeacherCourse" runat="server" HeaderText="Teacher Course" ToolTip="Teacher Course"
                            meta:resourcekey="TabPanel1Resource1">
                            <ContentTemplate>
                               
                                    
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Courses"
                                                meta:resourcekey="Label5Resource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                                        <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px;
                                                            border-color: #ccc">
                                                            <asp:CheckBoxList ID="cblCourseList" runat="server" Style="height: 26px" DataTextField="CourseName"
                                                                DataValueField="CourseId" meta:resourcekey="cblEmpListResource1">
                                                            </asp:CheckBoxList>
                                                        </div>
                                            </div>

                                                                <div class="col-md-2">
                                                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                                                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                                                
                                                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                                                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                                                </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="grpsave"
                                                meta:resourcekey="btnSaveResource1" />
                                        </div>
                                    </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                   
                        <cc1:TabPanel ID="TabTeacherGrade" runat="server" HeaderText="Teacher Grade" meta:resourcekey="TabPanel2Resource1">
                            <ContentTemplate>
                               
                                   <%-- <tr>
                                        <td>
                                            <asp:Label CssClass="Profiletitletxt" ID="lblTeacherNumber2" runat="server" Text="Teacher Number"
                                                meta:resourcekey="lblTeacherNumberResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTeacherNumber2" runat="server" Enabled="false" meta:resourcekey="txtTeacherNumberResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label CssClass="Profiletitletxt" ID="lblTeacherName2" runat="server" Text="Teacher Name"
                                                meta:resourcekey="lblTeacherNameResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTeacherName2" runat="server" Width="250px" Enabled="false" meta:resourcekey="txtTeacherNameResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label CssClass="Profiletitletxt" ID="lblTeacherArabicName2" runat="server" Text="Teacher Arabic Name"
                                                meta:resourcekey="lblTeacherArabicNameResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTeacherArabicName2" runat="server" Width="250px" Enabled="false"
                                                meta:resourcekey="txtTeacherArabicNameResource1"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="List Of Grade"
                                                meta:resourcekey="ListOfGradeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                                        <div style=" height: 200px; overflow: auto; border-style: solid; border-width: 1px;
                                                            border-color: #ccc">
                                                            <asp:CheckBoxList ID="cblGradeList" runat="server" Style="height: 26px" DataTextField="GradeName"
                                                                DataValueField="GradeId" meta:resourcekey="cblEmpListResource1">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                                <div class="col-md-2">
                                                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect1(true)" style="font-size: 8pt">
                                                                        <asp:Literal ID="Literal3" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                                                
                                                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect1(false)" style="font-size: 8pt">
                                                                        <asp:Literal ID="Literal4" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                                                </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="grpsave"
                                                meta:resourcekey="btnSaveResource1" /><%--onclientclick="return ShowPopUp('1')"--%>
                                        </div>
                            </ContentTemplate>
                        </cc1:TabPanel>

                        <cc1:TabPanel ID="TabClassesCourses" runat="server" HeaderText="Classes & Courses" meta:resourcekey="TabPanel3Resource1">
                            <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label CssClass="Profiletitletxt" ID="lblClass" runat="server" Text="Class" meta:resourcekey="lblClassResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadComboBox ID="RadCmbClass" runat="server" AppendDataBoundItems="True"
                                                AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbClassResource1">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="reqClass" runat="server" ControlToValidate="RadCmbClass"
                                                Display="None" ErrorMessage="Please select Class" InitialValue="--Please Select--"
                                                ValidationGroup="Grp1" meta:resourcekey="reqClassResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqClass" runat="server" CssClass="AISCustomCalloutStyle"
                                                Enabled="True" TargetControlID="reqClass" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                 
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
                                                ValidationGroup="Grp1" meta:resourcekey="reqCourseResource1"></asp:RequiredFieldValidator>
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
                                                Display="None" ErrorMessage="Please enter Weekly Cources Number" ValidationGroup="Grp1"
                                                meta:resourcekey="ReqWeeklyNoResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderReqWeeklyNo" runat="server" Enabled="True"
                                                TargetControlID="ReqWeeklyNo">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="button" ValidationGroup="grpsave"
                                                meta:resourcekey="btnSaveResource1" />
                                                 <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove" meta:resourcekey="btnRemoveResource1" />
                                             <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" meta:resourcekey="btnClearResource1" />
                                        </div>
                                    </div>
                                     <div class="row">
                    <div class="table-responsive">
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdCourse"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid ID="dgrdClassCourse" runat="server" AllowSorting="True" AllowPaging="True"
                            PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="dgrdClassCourseResource1">
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="FK_ClassId,FK_CourseId,FK_EmployeeId,weeklyCount,ClassName,CourseName">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="ClassName" SortExpression="ClassName" HeaderText="Class Name"
                                                                UniqueName="ClassName" meta:resourcekey="ClassNameResource1" />
                                                            <telerik:GridBoundColumn DataField="ClassNameAr" SortExpression="ClassNameAr"
                                                                HeaderText="Class Arabic Name" UniqueName="ClassNameAr" meta:resourcekey="ClassNameArResource" />
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
                                                    <telerik:GridBoundColumn DataField="FK_ClassGradeId" SortExpression="FK_ClassGradeId" Visible="False"
                                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource23" UniqueName="FK_ClassGradeId" />
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
                        </cc1:TabPanel>

                    </cc1:TabContainer>
               <%-- </td>
            </tr>
        </table>--%>

            <div class="row">
                <div class="col-md-8">
                    <asp:HiddenField runat="server" ID="hdnWindow" />
                    <cc1:ModalPopupExtender ID="mpeSave" runat="server" PopupControlID="pnlPopup" TargetControlID="hdnWindow"
                        CancelControlID="btnNo" BackgroundCssClass="ModalBackground" DropShadow="true"
                        BehaviorID="6">
                    </cc1:ModalPopupExtender>
                    <div id="pnlPopup" class="commonPopup" style="display: none">
                       
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblCheck" runat="server" Text="There is Already Active Card." CssClass="Profiletitletxt"
                                        meta:resourcekey="lblCheckResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblConfirm" runat="server" Text="Are You Sure you want to set selected Card as Active?"
                                        CssClass="Profiletitletxt" meta:resourcekey="lblConfirmResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button" meta:resourcekey="btnYesResource1" />
                                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="button" meta:resourcekey="btnNoResource1" />
                                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" />--%>
                                </div>
                            </div>
                    </div>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
