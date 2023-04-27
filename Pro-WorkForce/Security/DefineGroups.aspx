<%@ Page Title="Define User Groups" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="true"
    CodeFile="DefineGroups.aspx.vb" Inherits="DefineGroup" Culture="auto" UICulture="auto"
    meta:resourcekey="PageResource2" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function confirmDelete(gvName) {
            var TargetBaseControl = null;
            try {
                //get target base control.
                TargetBaseControl = document.getElementById(gvName);

            }
            catch (err) {
                TargetBaseControl = null;
            }

            if (TargetBaseControl == null) {
                ShowMessage("لا يوجد بيانات")
                return false;
            }

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            var TargetChildControl = "chkGroup";
            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].type == 'checkbox' && Inputs[n].checked && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    return confirm("هل أنت متأكد من الحذف؟");
                }

            }
            ShowMessage("الرجاء الاختيار من القائمة");
            return false;
        }



    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblGroupArName" runat="server" Text="Group Arabic Name" CssClass="Profiletitletxt"
                        meta:resourcekey="lblGroupArNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtGroupArName" runat="server" Width="209px" meta:resourcekey="txtGroupArNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqArName" runat="server" ControlToValidate="txtGroupArName"
                        ErrorMessage="Please enter Group Arabic Name" ValidationGroup="VGGroups"
                        meta:resourcekey="valReqArNameResource1" Text="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblGroupEnName" runat="server" CssClass="Profiletitletxt" Text="Group English Name"
                        meta:resourcekey="lblGroupEnNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtGroupEnName" runat="server" Width="209px" meta:resourcekey="txtGroupEnNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqEnName" runat="server" ControlToValidate="txtGroupEnName"
                        ErrorMessage="Please enter Group English Name" ValidationGroup="VGGroups"
                        meta:resourcekey="valReqEnNameResource1" Text="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDefinePermmission" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblDefinePermmissionResource1"
                        Text="Define Permission"></asp:Label>
                </div>
                <div class="col-md-10">
                    <asp:CheckBoxList ID="chkListPermessions" runat="server" CssClass="BoxList"
                        meta:resourcekey="chkListPermessionsResource1" RepeatDirection="Vertical">
                        <asp:ListItem Value="Save" meta:resourcekey="chkListPermessionsSave"
                            Text="Allow Save (Add/Edit)"></asp:ListItem>
                        <asp:ListItem Value="Delete" meta:resourcekey="chkListPermessionsDelete"
                            Text="Allow Delete"></asp:ListItem>
                        <asp:ListItem Value="Approve" meta:resourcekey="chkListPermessionsApprove"
                            Text="Allow View"></asp:ListItem>
                        <asp:ListItem Value="Print" meta:resourcekey="chkListPermessionsPrint"
                            Text="Allow Print"></asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="Svpanel">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDefaultPage" runat="server" Text="Default Page" meta:resourcekey="lblDefaultPageResource1"></asp:Label>
                    </div>
                    <div class="col-md-10">
                        <asp:RadioButtonList ID="rblDefaultPage" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Text="Home Page" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Self Service (Summary Page)" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Self Service (Reports)" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                        </asp:RadioButtonList>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="btnCreate" runat="server" CssClass="button" Text="Create" ValidationGroup="VGGroups"
                        meta:resourcekey="btnCreate" Visible="False" />
                </div>
            </div>
            <div class="table-responsive">
                <asp:Panel ID="pnlPermissions" runat="server"
                    meta:resourcekey="pnlPermissionsResource1">
                    <div class="row">
                        <div id="CtlTab" runat="server" class="col-md-12"></div>
                    </div>


                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" runat="server" ValidationGroup="VGGroups" Text="Save" CssClass="button"
                                meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <asp:ValidationSummary ID="valSunRole" runat="server" Style="width: 30%; text-align: left"
                    ValidationGroup="VGGroups" Width="500px" meta:resourcekey="valSunRoleResource1"
                    ShowMessageBox="True" ShowSummary="False" />
            </div>

            <div class="table-responsive">
                <telerik:RadGrid ID="gvGroups" runat="server" AllowSorting="True" AllowPaging="True"
                    GridLines="None" ShowStatusBar="True" PageSize="25" AllowMultiRowSelection="True" GroupingSettings-CaseSensitive="false"
                    ShowFooter="True" meta:resourcekey="gvGroupsResource1" AutoGenerateColumns="false">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="GroupID">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>

                            <telerik:GridTemplateColumn AllowFiltering="False"
                                meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkGroup" runat="server" CssClass="Checkbox" Text="&nbsp;" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="GroupID" AllowFiltering="False" UniqueName="GroupID" Display="false"
                                HeaderText="" SortExpression="GroupID"
                                meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="Desc_En" AllowFiltering="False" UniqueName="Desc_En"
                                HeaderText="Group English Name" SortExpression="Desc_En"
                                meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="Desc_Ar" AllowFiltering="False" UniqueName="Desc_Ar"
                                HeaderText="Group Arabic Name" SortExpression="Desc_Ar"
                                meta:resourcekey="GridBoundColumnResource2" />


                        </Columns>

                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>

            <asp:HiddenField ID="hdnid" runat="server" Value="0" />
            <asp:HiddenField ID="hdnsortDir" runat="server" Value="ASC" />
            <asp:HiddenField ID="hdnsortExp" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

