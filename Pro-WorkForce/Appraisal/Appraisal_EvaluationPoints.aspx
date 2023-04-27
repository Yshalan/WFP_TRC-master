<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"  AutoEventWireup="false" CodeFile="Appraisal_EvaluationPoints.aspx.vb" Inherits="Appraisal_Appraisal_EvaluationPoints" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

       <asp:UpdatePanel ID="pnlEmpNationality" runat="server">
        <ContentTemplate>

    <uc1:PageHeader ID="PageHeader1" HeaderText="Appraisal Evaluation Points" runat="server" />
      <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblEvaluationPoint" runat="server" CssClass="Profiletitletxt" Text="Evaluation Point "
                      ></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEvaluationPoint" CssClass="AIStextBoxCss" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEvaluationPoint"
                        Display="None" ErrorMessage="Please enter Evaluation Point" ValidationGroup="GroupPoints"
                       ></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPointsName" runat="server" CssClass="Profiletitletxt" Text="Point Name"
                      ></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPointsName" CssClass="AIStextBoxCss" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtxtPointsName" runat="server" ControlToValidate="txtPointsName"
                        Display="None" ErrorMessage="Please enter Evaluation Point Name" ValidationGroup="GroupPoints"
                       ></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqtxtPointsName" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqtxtPointsName" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPointsNameAr" runat="server" CssClass="Profiletitletxt" Text="Point Arabic Name"
                       ></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPointsNameAr" runat="server" Width="200px"></asp:TextBox>
                  
                </div>
            </div>

     <div class="row">
                <div class="col-md-8 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GroupPoints"
                        />
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete"  />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" />
                </div>
            </div>
                 <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwEsubNationality"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEvaluationPoints" runat="server" AllowPaging="True" AllowSorting="true"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" AutoGenerateColumns="False" PageSize="15"
                        OnItemCommand="dgrdEvaluationPoints_ItemCommand" ShowFooter="True" >
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="EvaluationPoint,PointName">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="EvaluationPoint" HeaderText="EvaluationPoint"
                                    SortExpression="EvaluationPoint" 
                                    UniqueName="EvaluationPoint" />                              
                                <telerik:GridBoundColumn DataField="PointName" HeaderText="Point Name"
                                    SortExpression="PointName"    UniqueName="PointName" />
                                <telerik:GridBoundColumn DataField="PointNameArabic" HeaderText="Point Name Arabic "
                                    SortExpression="PointNameArabic" Resizable="False"
                                    UniqueName="PointNameArabic" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>

               </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>