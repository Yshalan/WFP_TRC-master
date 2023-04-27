<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchoolSchedule.aspx.vb" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" 
Inherits="SchoolScheduling_SchoolSchedule"  Title="School Schedule" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

          <uc1:PageHeader ID="PageHeader1" runat="server"  HeaderText="School Schedule" meta:resourcekey="SchoolScheduleResource1" />
                <div class="row">
                    <div class="table-responsive">
                    
                        <telerik:RadGrid ID="dgrdSchoolScedule" runat="server" AllowSorting="True" AllowPaging="True"
                            PageSize="15" Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="dgrdCourseResource1"  Height="550px" >
                            <SelectedItemStyle ForeColor="Maroon"  />

                            <MasterTableView AllowMultiColumnSorting="True"  AutoGenerateColumns="False" Width="100%">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                
                                <Columns>
                                   <telerik:GridBoundColumn DataField="ClassId" SortExpression="ClassId" Visible="False"
                            meta:resourcekey="GridBoundColumnResource1" UniqueName="ClassId" >
                                       <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Class" 
                                       HeaderStyle-Wrap="False" ItemStyle-Wrap="false" meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:Label ID="lblClass" runat="server" meta:resourcekey="lblClassResource1" />
                                <asp:HiddenField ID="hdnEnClass" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.ClassName") %>' />
                                <asp:HiddenField ID="hdnArClass" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.ClassNameAr") %>' />
                            </ItemTemplate>
                            <HeaderStyle Width="75px" Wrap="False" />
                        </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="1-lesson1" SortExpression="1-lesson1" 
                                        HeaderText="Sun-lesson1" UniqueName="1-lesson1" 
                                        meta:resourcekey="GridBoundColumnResource2" >
                                       
                                        <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                       
                                    <telerik:GridBoundColumn DataField="1-lesson2" SortExpression="1-lesson2" 
                                        HeaderText="Sun-lesson2" UniqueName="1-lesson2" 
                                        meta:resourcekey="GridBoundColumnResource3" >
                                        <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="1-lesson3" SortExpression="1-lesson3"
                                        HeaderText="Sun-lesson3" UniqueName="1-lesson3" 
                                        meta:resourcekey="GridBoundColumnResource4" >
                                        <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="1-lesson4" SortExpression="1-lesson4"
                                        HeaderText="Sun-lesson4" UniqueName="1-lesson4" 
                                        meta:resourcekey="GridBoundColumnResource5" >
                                        <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="1-lesson5" SortExpression="1- lesson5"
                                        HeaderText="Sun-lesson5" UniqueName="1-lesson5" 
                                        meta:resourcekey="GridBoundColumnResource6" >
                                            <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="1-lesson6" SortExpression="1-lesson6"
                                        HeaderText="Sun-lesson6" UniqueName="1-lesson6" 
                                        meta:resourcekey="GridBoundColumnResource7" >
                                            <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="1-lesson7" SortExpression="1- lesson7"
                                        HeaderText="Sun-lesson7" UniqueName="1-lesson7" 
                                        meta:resourcekey="GridBoundColumnResource8" >
                                            <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="1-lesson8" SortExpression="1-lesson8"
                                        HeaderText="Sun-lesson8" UniqueName="1-lesson8" 
                                        meta:resourcekey="GridBoundColumnResource9" >
                                       
                                            <HeaderStyle Wrap="False" />
                                    </telerik:GridBoundColumn>
                                       
                                        <telerik:GridBoundColumn DataField="2-lesson1" 
                                        SortExpression="2-lesson1" HeaderText="Mon - lesson 1" 
                                        UniqueName="2-lesson1" meta:resourcekey="GridBoundColumnResource10" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="2-lesson2" SortExpression="2-lesson2" 
                                        HeaderText="Mon - lesson 2" UniqueName="2-lesson2" 
                                        meta:resourcekey="GridBoundColumnResource11" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="2-lesson3" SortExpression="2-lesson3" 
                                        HeaderText="Mon - lesson 3" UniqueName="2-lesson3" 
                                        meta:resourcekey="GridBoundColumnResource12" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="2-lesson4" SortExpression="2-lesson4" 
                                        HeaderText="Mon - lesson 4" UniqueName="2-lesson4" 
                                        meta:resourcekey="GridBoundColumnResource13" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="2-lesson5" 
                                        SortExpression="2-lesson5" HeaderText="Mon - lesson 5" 
                                        UniqueName="2-lesson5" meta:resourcekey="GridBoundColumnResource14" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="2-lesson6" 
                                        SortExpression="2-lesson6"  HeaderText="Mon - lesson 6" 
                                        UniqueName="2-lesson6" meta:resourcekey="GridBoundColumnResource15" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="2-lesson7" 
                                        SortExpression="2-lesson7" HeaderText="Mon - lesson 7" 
                                        UniqueName="2-lesson7" meta:resourcekey="GridBoundColumnResource16" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="2-lesson8" 
                                        SortExpression="2-lesson8" HeaderText="Mon - lesson 8" 
                                        UniqueName="2-lesson8" meta:resourcekey="GridBoundColumnResource17" >

                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="3-lesson1" 
                                        SortExpression="3-lesson1" HeaderText="Tue - lesson 1" 
                                        UniqueName="3-lesson1" meta:resourcekey="GridBoundColumnResource18" >
                                             <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="3-lesson2" SortExpression="3-lesson2" 
                                        HeaderText="Tue - lesson 2" UniqueName="3-lesson2" 
                                        meta:resourcekey="GridBoundColumnResource19" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="3-lesson3" SortExpression="3-lesson3" 
                                        HeaderText="Tue - lesson 3" UniqueName="3-lesson3" 
                                        meta:resourcekey="GridBoundColumnResource20" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="3-lesson4" SortExpression="3-lesson4" 
                                        HeaderText="Tue - lesson 4" UniqueName="3-lesson4" 
                                        meta:resourcekey="GridBoundColumnResource21" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="3-lesson5" 
                                        SortExpression="3-lesson5" HeaderText="Tue - lesson 5" 
                                        UniqueName="3-lesson5" meta:resourcekey="GridBoundColumnResource22" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="3-lesson6" 
                                        SortExpression="3-lesson6"  HeaderText="Tue - lesson 6" 
                                        UniqueName="3-lesson6" meta:resourcekey="GridBoundColumnResource23" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="3-lesson7" 
                                        SortExpression="3-lesson7" HeaderText="Tue - lesson 7" 
                                        UniqueName="3-lesson7" meta:resourcekey="GridBoundColumnResource24" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="3-lesson8" 
                                        SortExpression="3-lesson8" HeaderText="Tue - lesson 8" 
                                        UniqueName="3-lesson8" meta:resourcekey="GridBoundColumnResource25" >

                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="4-lesson1" 
                                        SortExpression="4-lesson1" HeaderText="Wed - lesson 1" 
                                        UniqueName="4-lesson1" meta:resourcekey="GridBoundColumnResource26" >
                                             <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="4-lesson2" SortExpression="4-lesson2" 
                                        HeaderText="Wed - lesson 2" UniqueName="4-lesson2" 
                                        meta:resourcekey="GridBoundColumnResource27" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="4-lesson3" SortExpression="4-lesson3" 
                                        HeaderText="Wed - lesson 3" UniqueName="4-lesson3" 
                                        meta:resourcekey="GridBoundColumnResource28" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="4-lesson4" SortExpression="4-lesson4" 
                                        HeaderText="Wed - lesson 4" UniqueName="4-lesson4" 
                                        meta:resourcekey="GridBoundColumnResource29" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="4-lesson5" 
                                        SortExpression="4-lesson5" HeaderText="Wed - lesson 5" 
                                        UniqueName="4-lesson5" meta:resourcekey="GridBoundColumnResource30" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="4-lesson6" 
                                        SortExpression="4-lesson 6"  HeaderText="Wed - lesson 6" 
                                        UniqueName="4-lesson6" meta:resourcekey="GridBoundColumnResource31" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="4-lesson7" 
                                        SortExpression="4-lesson7" HeaderText="Wed - lesson 7" 
                                        UniqueName="4-lesson7" meta:resourcekey="GridBoundColumnResource32" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="4-lesson8" 
                                        SortExpression="4-lesson8" HeaderText="Wed - lesson 8" 
                                        UniqueName="4-lesson8" meta:resourcekey="GridBoundColumnResource33" >

                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>

                                         <telerik:GridBoundColumn DataField="5-lesson1" 
                                        SortExpression="5-lesson1" HeaderText="Thu - lesson 1" 
                                        UniqueName="5-lesson1" meta:resourcekey="GridBoundColumnResource34" >
                                             <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="5-lesson2" SortExpression="5-lesson2" 
                                        HeaderText="Thu - lesson 2" UniqueName="5-lesson2" 
                                        meta:resourcekey="GridBoundColumnResource35" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="5-lesson3" SortExpression="5-lesson3" 
                                        HeaderText="Thu - lesson 3" UniqueName="5-lesson3" 
                                        meta:resourcekey="GridBoundColumnResource36" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="5-lesson4" SortExpression="5-lesson4" 
                                        HeaderText="Thu - lesson 4" UniqueName="5-lesson4" 
                                        meta:resourcekey="GridBoundColumnResource37" >
                                        <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="5-lesson5" 
                                        SortExpression="5-lesson5" HeaderText="Thu - lesson 5" 
                                        UniqueName="5-lesson5" meta:resourcekey="GridBoundColumnResource38" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="5-lesson6" 
                                        SortExpression="5-lesson6"  HeaderText="Thu - lesson 6" 
                                        UniqueName="5-lesson6" meta:resourcekey="GridBoundColumnResource39" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="5-lesson7" 
                                        SortExpression="5-lesson7" HeaderText="Thu - lesson 7" 
                                        UniqueName="5-lesson7" meta:resourcekey="GridBoundColumnResource40" >
                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="5-lesson8" 
                                        SortExpression="5-lesson8" HeaderText="Thu - lesson 8" 
                                        UniqueName="5-lesson8" meta:resourcekey="GridBoundColumnResource41" >

                                            <HeaderStyle Width="25px" Wrap="False" />
                                    </telerik:GridBoundColumn>

                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                             
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