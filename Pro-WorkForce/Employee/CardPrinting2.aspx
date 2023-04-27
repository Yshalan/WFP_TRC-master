<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="CardPrinting2.aspx.vb" Inherits="Employee_CardPrinting" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 100%;
        }
    </style>
    <script type="text/javascript">
        //function showPopup(path, name, height, width) {
        //    var options = 'width=' + width + ',height=' + height;
        //    var newwindow;
        //    newwindow = window.open(path, name, options);
        //    if (window.focus) {
        //        newwindow.focus();
        //    }
        //}
        //function open_window(url, target, w, h) { //opens new window 
        //    var parms = "width=" + w + ",height=" + h + ",menubar=no,location=no,resizable,scrollbars,toolbar=no";
        //    var win = window.open(url, target, parms);
        //    if (win) {
        //        win.focus();
        //    }
        //}

        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

        function CheckBoxListSelect2(state) {
            var chkBoxList = document.getElementById("<%= cblEmpListInQueue.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

        function ConfirmDelete() {
            var checkList = document.getElementById("<%= cblEmpListInQueue.ClientID %>");
            var checkBoxList = checkList.getElementsByTagName("input");
            var selectedcount = 0;
            var lang = '<%= MsgLang %>';

            for (var i = 0; i < checkBoxList.length; i++) {
                if (checkBoxList[i].checked) {
                    selectedcount += 1;
                }
            }
            if (selectedcount > 0) {
                if (lang == 'en') {
                    if (confirm("Are you sure you want to delete selected employees?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    if (confirm("هل انت متأكد من حذف الموظفين الذين تم اختيارهم؟")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
            else {
                if (lang == 'en') {
                    ShowMessage("Please select employee to delete");
                }
                else {
                    ShowMessage("الرجاء اختيار موظف من القائمة للحذف");
                }
            }

        }

        function ValidatePrint() {

            window.open("CardPreview.aspx", "popup", "width=800,height=650,left=175,top=10,resizable=yes");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="userCtrlCardPrintHeader" runat="server" HeaderText="Card Printing" />
    <div class="row" id="trcompany" runat="server">
        <div class="col-md-2">
            <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblCompanyResource1"
                Text="Company"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
                MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="EmpPermissionGroup"
                meta:resourcekey="RadCmbBxCompaniesResource1">
            </telerik:RadComboBox>

            <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                Display="None" ErrorMessage="Please Select Company" meta:resourcekey="rfvCompaniesResource1"
                ValidationGroup="VGCards"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDesignation" runat="server" CssClass="Profiletitletxt" 
                Text="Designation"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlDesignation" runat="server" AutoPostBack="True" CausesValidation="False"
                MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="EmpPermissionGroup"
                meta:resourcekey="RadCmbBxCompaniesResource1">
            </telerik:RadComboBox>

            <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                Display="None" ErrorMessage="Please Select Designation" meta:resourcekey="rfvCompaniesResource1"
                ValidationGroup="VGCards"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                Enabled="True" TargetControlID="rfvDesignation" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblCardDesign" runat="server" CssClass="Profiletitletxt" Text="Card Type"
                ></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlCardDesign" AllowCustomText="false" AutoPostBack="true" MarkFirstMatch="true"
                Skin="Vista" runat="server" ValidationGroup="VGCards">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Card Design"
                ControlToValidate="ddlCardDesign" ValidationGroup="btnPrint" InitialValue="--Please Select--"
                Display="None" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
   <%-- <div class="row">
        <div class="col-md-12">
            <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
        </div>
    </div>--%>

    <div class="row">

        <div class="col-md-4">
            <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                meta:resourcekey="Label5Resource1"></asp:Label>
        </div>
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="Employees in the Queue"
                meta:resourcekey="Label6Resource1"></asp:Label>
        </div>
        <%--<td>
                &nbsp;
            </td>--%>
    </div>

    <%--<td>
                &nbsp;
            </td>--%>
    <div class="row">

        <div class="col-md-2">
            <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
        </div>
        <div class="col-md-2">
            <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
        </div>
        <div class="col-md-2"></div>

        <div class="col-md-2">
            <a href="javascript:void(0)" onclick="CheckBoxListSelect2(true)" style="font-size: 8pt">
                <asp:Literal ID="Literal3" runat="server" Text="Select All" meta:resourcekey="Literal3Resource1"></asp:Literal></a>
        </div>
        <div class="col-md-2">
            <a href="javascript:void(0)" onclick="CheckBoxListSelect2(false)" style="font-size: 8pt">
                <asp:Literal ID="Literal4" runat="server" Text="Unselect All" meta:resourcekey="Literal4Resource1"></asp:Literal></a>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-4">
        </div>
        <div class="col-md-2">
        </div>
        <div class="col-md-2">
            <asp:LinkButton ID="lnkViewCard" runat="server" Text="View" Style="font-size: 8pt"
                meta:resourcekey="lnkViewCardResource1"></asp:LinkButton>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                    DataValueField="CardRequestId" meta:resourcekey="cblEmpListResource1">
                </asp:CheckBoxList>
            </div>
        </div>
        <div class="col-md-4">
            <center>
            <asp:Button ID="btnAddtoQueue" runat="server" Text="Add to print queue"
                ValidationGroup="VGCards" CssClass="button" meta:resourcekey="btnAddtoQueueResource1" />&nbsp;
                                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete"
                                    CssClass="button" OnClientClick="javascript:return ConfirmDelete();" meta:resourcekey="btnDeleteResource1" />--%>
            </center>
        </div>
        
        <div class="col-md-3">
            <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                <asp:CheckBoxList ID="cblEmpListInQueue" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                    DataValueField="EmployeeId" meta:resourcekey="cblEmpListInQueueResource1">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <div class="col-md-2">
        <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1">View Org Level Employees </asp:HyperLink>
    </div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-10">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <br />
        </div>
    </div>

    <div class="row" id="trControls" runat="server">
        
    </div>
    <%--</table>
            </td>
        </tr>--%>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblPrinter" runat="server" Text="Printer" meta:resourcekey="lblPrinterResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="rcmbxPrinter" AllowCustomText="false" MarkFirstMatch="true"
                Skin="Vista" runat="server">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="rfvPrinter" runat="server" ErrorMessage="Please Select Printer"
                ControlToValidate="rcmbxPrinter" ValidationGroup="btnPrint" InitialValue="--Please Select--"
                Display="None" meta:resourcekey="rfvPrinterResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcePrinter" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvPrinter" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row" style="text-align: center">
        <div class="col-md-8 text-center">
            <asp:Button ID="btnPrint" runat="server" Text="Print Cards" ValidationGroup="btnPrint"
                meta:resourcekey="btnPrintResource1" />
            <%-- <input id="cmdPrint1" type="button" value="Print Cards" runat="server" class="button"
                onclick="javascript: return ValidatePrint();" />--%>
        </div>
    </div>
</asp:Content>
