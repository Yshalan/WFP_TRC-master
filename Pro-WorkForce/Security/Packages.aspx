<%@ Page Title="Define User Groups" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="true"
    CodeFile="Packages.aspx.vb" Inherits="License" Culture="auto" UICulture="auto"
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
            <uc1:PageHeader ID="PageHeader1" runat="server"  HeaderText="Packages" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPackage" runat="server" CssClass="Profiletitletxt"
                        Text="Package Name" ></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPackage" runat="server" meta:resourcekey="txtCustomerNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valReqArName" runat="server" ControlToValidate="txtPackage"
                        ErrorMessage="Please enter Package Name" ValidationGroup="VGGroups"
                        Text="*" ></asp:RequiredFieldValidator>
                </div>
            </div>
            </br>
            
            <asp:Panel ID="pnlPermissions" runat="server" meta:resourcekey="pnlPermissionsResource1">
                <div class="row">
                    <div class="col-md-12" id="CtlTab" runat="server">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-21 text-center">
                        <asp:Button ID="btnSave" runat="server" ValidationGroup="VGGroups" Text="Save" CssClass="button" 
                            />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button"  />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button"  />
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-8">
                    <asp:ValidationSummary ID="valSunRole" runat="server" Style="width: 30%; text-align: left"
                        ValidationGroup="VGGroups" 
                        ShowMessageBox="True" ShowSummary="False" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="gvGroups" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" PageSize="15" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="gvGroupsResource1" AutoGenerateColumns="False" CellSpacing="0">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="PackageId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkGroup" runat="server" Text="&nbsp;"  />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="PackageName" AllowFiltering="False" UniqueName="PackageName"
                                    HeaderText="Package Name" SortExpression="PackageName" FilterControlAltText="Filter CustomerShortName column"
                                     />
                                <telerik:GridBoundColumn DataField="Forms" AllowFiltering="False" UniqueName="Forms"
                                    HeaderText="Forms" SortExpression="Forms" FilterControlAltText="Filter CustomerName column" 
                                    />                  
                                <telerik:GridBoundColumn DataField="PackageId" AllowFiltering="False"
                                    Visible="False" 
                                    UniqueName="PackageId" FilterControlAltText="Filter CustomerId column"  />
                            </Columns>
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
            <asp:HiddenField ID="hdnid" runat="server" Value="0" />
            <asp:HiddenField ID="hdnsortDir" runat="server" Value="ASC" />
            <asp:HiddenField ID="hdnsortExp" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
