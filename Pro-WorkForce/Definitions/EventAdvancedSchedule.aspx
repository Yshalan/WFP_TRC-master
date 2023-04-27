<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EventAdvancedSchedule.aspx.vb" Inherits="Definitions_EventAdvancedSchedule_"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/TAFilter.ascx" TagName="EmployeeFilter" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ScheduleGrid.css" rel="stylesheet" type="text/css" />
    <%-- <script src="../js/browser.js" type="text/javascript"></script>
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/jquery.dd.js" type="text/javascript"></script>--%>
    <%-- <script src="../js/jsonpath.js" type="text/javascript"></script>
    <asp:Literal runat="server" ID="JSON" meta:resourcekey="JSONResource1" />
    <script src="../js/ScheduleGrid.js" type="text/javascript"></script>
    <script src="../js/brwsniff.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var isMozilla = (navigator.userAgent.indexOf("compatible") == -1);

        //        var hdnEmployeeNos = document.getElementById("<%= hdnEmployeeNos.ClientID %>").value;
        //        var hdnGroupIDs = document.getElementById("<%= hdnGroupIDs.ClientID %>").value;
        //        var hdnShifts = document.getElementById("<%= hdnShifts.ClientID %>").value;
        //        var hdnSelectedDays = document.getElementById("<%= hdnSelectedDays.ClientID %>").value;

        var hdnShiftACount = document.getElementById("<%= hdnShiftACount.ClientID %>").value;
        var hdnShiftBCount = document.getElementById("<%= hdnShiftBCount.ClientID %>").value;
        var hdnShiftCCount = document.getElementById("<%= hdnShiftCCount.ClientID %>").value;

        var leftButtonDown = false;
        var rightButtonDown = false;

        window.onunload = function () {
            leftButtonDown = false;
            rightButtonDown = false;
        }

        function ClearScheduleGrid() {
            var cntrl_tblSchedule = document.getElementById('tblSchedule');

            ClearTable(cntrl_tblSchedule); //.innerHTML = '';
            var days = (document.getElementById("<%= hdnDays.ClientID %>").value).split(',');
            var months = (document.getElementById("<%= hdnMonths.ClientID %>").value).split(',');
            var Lang = '<%= strLang %>';

            var tr_h = document.createElement("tr");

            if (Lang == "en") {

                var td_h4 = document.createElement("td");
                setInnerText(td_h4, 'Group. Name.');
                td_h4.setAttribute('class', 'td_h_name');
                tr_h.appendChild(td_h4);

                var td_h1 = document.createElement("td");
                setInnerText(td_h1, 'Emp. No.');
                td_h1.setAttribute('class', 'td_h_name');
                tr_h.appendChild(td_h1);

                var td_h2 = document.createElement("td");
                setInnerText(td_h2, 'Emp. Name');
                td_h2.setAttribute('class', 'td_h_name');
                td_h2.style.width = "260px"
                tr_h.appendChild(td_h2);
            }
            else {

                var td_h4 = document.createElement("td");
                setInnerText(td_h4, 'إسم مجموعة العمل');
                td_h4.setAttribute('class', 'td_h_name');
                tr_h.appendChild(td_h4);

                var td_h1 = document.createElement("td");
                setInnerText(td_h1, 'رقم الموظف');
                td_h1.setAttribute('class', 'td_h_name');
                tr_h.appendChild(td_h1);

                var td_h2 = document.createElement("td");
                setInnerText(td_h2, 'إسم الموظف');
                td_h2.setAttribute('class', 'td_h_name');
                td_h2.style.width = "260px"
                tr_h.appendChild(td_h2);
            }

            for (var j = 1; j <= days.length - 1; j++) {
                var td_h3 = document.createElement("td");
                setInnerText(td_h3, days[j].toString() + "/" + months[j].toString());
                td_h3.setAttribute('class', 'tblSchedule_td');
                tr_h.appendChild(td_h3);
            }

            var td_d = document.createElement("td");
            tr_h.appendChild(td_d);

            cntrl_tblSchedule.appendChild(tr_h);
        }

        function ClearTable(tbl) {
            var rowCount = tbl.childNodes.length
            for (var i = 0; i < rowCount; i++) {
                var row = tbl.childNodes.item(0);
                var cellCount = row.childNodes.length;

                for (var k = 0; k < cellCount; k++) {

                    row.removeChild(row.childNodes.item(0));
                }

                tbl.removeChild(tbl.childNodes.item(0));
            }
        }

        function setInnerText(cntrl, text) {
            if (isMozilla)
                cntrl.innerHTML = text;
            else
                cntrl.innerText = text;
        }

        function scheduleGrid_AddEmp(param) {
            //alert("Welcome");
            var cntrl_tblSchedule = document.getElementById('tblSchedule');
            var days = (document.getElementById("<%= hdnDays.ClientID %>").value).split(',');
            var months = (document.getElementById("<%= hdnMonths.ClientID %>").value).split(',');

            var selectedEmployee = (document.getElementById("<%= hdnSelectedEmployees.ClientID %>").value).split(',');
            var selectedShifts = (document.getElementById("<%= hdnSelectedShift.ClientID %>").value).split(',');
            var savedDays = (document.getElementById("<%= hdnSavedDays.ClientID %>").value).split(',');
            var selectedEmployeeIDs = (document.getElementById("<%= hdnSelectedEmployeesIDs.ClientID %>").value).split(',');

            //Employee Leave
            var leaveDays = (document.getElementById("<%= hdnLeaveDays.ClientID %>").value).split(',');
            var leaveMonths = (document.getElementById("<%= hdnLeaveMonths.ClientID %>").value).split(',');
            var leaveEmployee = (document.getElementById("<%= hdnLeaveEmployee.ClientID %>").value).split(',');
            var isLeavEmployee = false;

            var isSelected = false;
            var strShift = '';

            var data = param.split(',');

            var tr_b = document.createElement("tr");

            var td_b1 = document.createElement("td");
            setInnerText(td_b1, data[3].toString());
            td_b1.setAttribute('class', 'tblSchedule_tdText');
            tr_b.appendChild(td_b1);

            var td_b2 = document.createElement("td");
            setInnerText(td_b2, data[1].toString());
            td_b2.setAttribute('class', 'tblSchedule_tdText');
            tr_b.appendChild(td_b2);

            var td_b3 = document.createElement("td");
            setInnerText(td_b3, data[2].toString());
            td_b3.setAttribute('class', 'tblSchedule_tdText');
            tr_b.appendChild(td_b3);

            for (var j = 1; j <= days.length - 1; j++) {
                strShift = '';
                isLeavEmployee = false;
                var td_b = document.createElement("td");
                td_b.style.backgroundColor = '#FFFFFF';

                for (var l = 0; l <= leaveEmployee.length; l++) {
                    if (leaveEmployee[l] == data[1] && leaveDays[l] == days[j] && leaveMonths[l] == months[j]) {
                        isLeavEmployee = true;
                        break;
                    }
                }

                if (isLeavEmployee == false) {
                    for (var s = 0; s <= selectedEmployee.length; s++) {
                        if (selectedEmployee[s] == data[1] && savedDays[s] == days[j]) {
                            document.getElementById("<%= hdnEmployeeNos.ClientID %>").value += "," + selectedEmployee[s];
                            document.getElementById("<%= hdnShifts.ClientID %>").value += "," + selectedShifts[s].trim();
                            document.getElementById("<%= hdnSelectedDays.ClientID %>").value += "," + savedDays[s];
                            isSelected = true;
                            strShift = selectedShifts[s].trim();
                            break;
                        }
                    }
                }

                //FadiH:: Start:: Nested Table
                var tbl = document.createElement("tbody");

                var tr_tbl = document.createElement("tr");

                var td_tbl1 = document.createElement("td");
                if (isLeavEmployee == true) {
                    setInnerText(td_tbl1, 'L');
                }
                else {
                    setInnerText(td_tbl1, 'A');
                }

                td_tbl1.setAttribute('class', 'td_h_no');
                td_tbl1.style.backgroundColor = '#FFFFFF';
                if (isLeavEmployee == false) {
                    td_tbl1.setAttribute('onmousedown', 'ChangeShiftA(' + 'this,event,' + data[0] + ',' + data[1] + ',' + days[j] + ');');
                    td_tbl1.setAttribute('onclick', 'DayMouseUp()');
                    td_tbl1.setAttribute('onmouseup', 'DayMouseUp()');
                    td_tbl1.setAttribute('oncontextmenu', 'rightButtonDown= false; return false;')
                }
                if (isLeavEmployee == true) {
                    td_tbl1.style.backgroundColor = '#C0C0C0';
                    td_tbl1.disabled = true;
                }
                else if (strShift == 'A') {
                    td_tbl1.style.backgroundColor = '#FF0000';
                }

                tr_tbl.appendChild(td_tbl1);

                var td_tbl2 = document.createElement("td");
                if (isLeavEmployee == true) {
                    setInnerText(td_tbl2, 'L');
                }
                else {
                    setInnerText(td_tbl2, 'B');
                }

                td_tbl2.setAttribute('class', 'td_h_no');
                td_tbl2.style.backgroundColor = '#FFFFFF';
                if (isLeavEmployee == false) {
                    td_tbl2.setAttribute('onmousedown', 'ChangeShiftB(' + 'this,event,' + data[0] + ',' + data[1] + ',' + days[j] + ');');
                    td_tbl2.setAttribute('onclick', 'DayMouseUp()');
                    td_tbl2.setAttribute('onmouseup', 'DayMouseUp()');
                    td_tbl2.setAttribute('oncontextmenu', 'rightButtonDown= false; return false;')
                }
                if (isLeavEmployee == true) {
                    td_tbl2.style.backgroundColor = '#C0C0C0';
                    td_tbl2.disabled = true;
                }
                else if (strShift == 'B') {
                    td_tbl2.style.backgroundColor = '#00FF00';
                }

                tr_tbl.appendChild(td_tbl2);

                var td_tbl3 = document.createElement("td");
                if (isLeavEmployee == true) {
                    setInnerText(td_tbl3, 'L');
                }
                else {
                    setInnerText(td_tbl3, 'C');
                }

                td_tbl3.setAttribute('class', 'td_h_no');
                td_tbl3.style.backgroundColor = '#FFFFFF';
                if (isLeavEmployee == false) {
                    td_tbl3.setAttribute('onmousedown', 'ChangeShiftC(' + 'this,event,' + data[0] + ',' + data[1] + ',' + days[j] + ');');
                    td_tbl3.setAttribute('onclick', 'DayMouseUp()');
                    td_tbl3.setAttribute('onmouseup', 'DayMouseUp()');
                    td_tbl3.setAttribute('oncontextmenu', 'rightButtonDown= false; return false;')
                }
                if (isLeavEmployee == true) {
                    td_tbl3.style.backgroundColor = '#C0C0C0';
                    td_tbl3.disabled = true;
                }
                else if (strShift == 'C') {
                    td_tbl3.style.backgroundColor = '#00FFFF';
                }

                tr_tbl.appendChild(td_tbl3);

                tbl.appendChild(tr_tbl);

                td_b.appendChild(tbl)
                //FadiH:: End:: Nested Table

                //                td_b.setAttribute('onmousedown', 'ChangeShift(' + 'this,event);');
                //                td_b.setAttribute('onclick', 'DayMouseUp()');
                //                td_b.setAttribute('onmouseup', 'DayMouseUp()');
                //                td_b.setAttribute('oncontextmenu', 'rightButtonDown= false; return false;')//'RemoveSchedule(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this); return false;');
                // You can use the bellow line to enable shift change while mouse move with left mouse button click
                /* BEGIN */

                /* END */
                tr_b.appendChild(td_b);
            }
            //            var imgDelete = document.createElement("img");
            //            imgDelete.src = '../Icons/delete.png';
            //            imgDelete.setAttribute('class', 'imgDelete');
            //            var td_d = document.createElement("td");
            //            td_d.setAttribute('onclick', 'td_delete_click(' + CurrentEmp[0].EmpId + ',this.parentNode)');
            //            td_d.style.cursor = 'pointer';
            //            td_d.appendChild(imgDelete);
            //            tr_b.appendChild(td_d);

            cntrl_tblSchedule.appendChild(tr_b);

            var tr_b2 = document.createElement("tr");
            cntrl_tblSchedule.appendChild(tr_b2);


        }

        function DayMouseUp() {
            leftButtonDown = true;
        }

        function ChangeShiftA(td, e, GroupId, EmpId, days) {

            //var hidden = $get("<%=hdnEmployeeNos.ClientID%>").value;

            if (e.which != undefined) {
                if (e.which === 1) {
                    rightButtonDown = false;
                    leftButtonDown = true;
                }
                else if (e.which === 3) {
                    rightButtonDown = true;
                    leftButtonDown = false;
                }
            }
            else {
                if (e.button === 1) {
                    rightButtonDown = false;
                    leftButtonDown = true;
                }
                else if (e.button === 2) {
                    rightButtonDown = true;
                    leftButtonDown = false;
                }
            }

            // this will fire on shift table cell shift changed (mouse-click), Line: 469 in this file   ..  td_b.setAttribute('onclick', 'ChangeShift('  ..
            if (leftButtonDown) {
                td.style.backgroundColor = '#FF0000';

                //hdnGroupIDs += "," + GroupId;
                document.getElementById("<%= hdnGroupIDs.ClientID %>").value += "," + GroupId;
                document.getElementById("<%= hdnEmployeeNos.ClientID %>").value += "," + EmpId;
                //hidden += "," + EmpId;
                //hdnShifts += ",A";
                document.getElementById("<%= hdnShifts.ClientID %>").value += ",A";
                //hdnSelectedDays += "," + days;
                document.getElementById("<%= hdnSelectedDays.ClientID %>").value += "," + days;

                if (hdnShiftACount == undefined) {
                    hdnShiftACount = "0";
                }

                hdnShiftACount = (parseInt(hdnShiftACount) + 1).toString();
                //alert("ShiftA Count: " + hdnShiftACount);
            }
            else if (rightButtonDown) {
                return RemoveSchedule(td);
            }
        }

        function ChangeShiftB(td, e, GroupId, EmpId, days) {
            if (e.which != undefined) {
                if (e.which === 1) {
                    rightButtonDown = false;
                    leftButtonDown = true;
                }
                else if (e.which === 3) {
                    rightButtonDown = true;
                    leftButtonDown = false;
                }
            }
            else {
                if (e.button === 1) {
                    rightButtonDown = false;
                    leftButtonDown = true;
                }
                else if (e.button === 2) {
                    rightButtonDown = true;
                    leftButtonDown = false;
                }
            }

            // this will fire on shift table cell shift changed (mouse-click), Line: 469 in this file   ..  td_b.setAttribute('onclick', 'ChangeShift('  ..
            if (leftButtonDown) {
                td.style.backgroundColor = '#00FF00';

                //hdnGroupIDs += "," + GroupId;
                document.getElementById("<%= hdnGroupIDs.ClientID %>").value += "," + GroupId;
                document.getElementById("<%= hdnEmployeeNos.ClientID %>").value += "," + EmpId;
                //hidden += "," + EmpId;
                //hdnShifts += ",B";
                document.getElementById("<%= hdnShifts.ClientID %>").value += ",B";
                //hdnSelectedDays += "," + days;
                document.getElementById("<%= hdnSelectedDays.ClientID %>").value += "," + days;
                if (hdnShiftBCount == undefined) {
                    hdnShiftBCount = "0";
                }

                hdnShiftBCount = (parseInt(hdnShiftBCount) + 1).toString();

                //alert("ShiftB Count: " + hdnShiftBCount);
            }
            else if (rightButtonDown) {
                return RemoveSchedule(td);
            }
        }

        function ChangeShiftC(td, e, GroupId, EmpId, days) {
            if (e.which != undefined) {
                if (e.which === 1) {
                    rightButtonDown = false;
                    leftButtonDown = true;
                }
                else if (e.which === 3) {
                    rightButtonDown = true;
                    leftButtonDown = false;
                }
            }
            else {
                if (e.button === 1) {
                    rightButtonDown = false;
                    leftButtonDown = true;
                }
                else if (e.button === 2) {
                    rightButtonDown = true;
                    leftButtonDown = false;
                }
            }

            // this will fire on shift table cell shift changed (mouse-click), Line: 469 in this file   ..  td_b.setAttribute('onclick', 'ChangeShift('  ..
            if (leftButtonDown) {
                td.style.backgroundColor = '#00FFFF';

                //hdnGroupIDs += "," + GroupId;
                document.getElementById("<%= hdnGroupIDs.ClientID %>").value += "," + GroupId;
                document.getElementById("<%= hdnEmployeeNos.ClientID %>").value += "," + EmpId;
                //hidden += "," + EmpId;
                //hdnShifts += ",C";
                document.getElementById("<%= hdnShifts.ClientID %>").value += ",C";
                //hdnSelectedDays += "," + days;
                document.getElementById("<%= hdnSelectedDays.ClientID %>").value += "," + days;
                if (hdnShiftCCount == undefined) {
                    hdnShiftCCount = "0";
                }

                hdnShiftCCount = (parseInt(hdnShiftCCount) + 1).toString();

                //alert("ShiftC Count: " + hdnShiftCCount);
            }
            else if (rightButtonDown) {
                return RemoveSchedule(td);
            }
        }

        function RemoveSchedule(td) {
            if (rightButtonDown) {
                td.style.backgroundColor = '#FFFFFF';
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 600px">
        <tr>
            <td colspan="5">
                <center>
                    <uc1:PageHeader ID="PageHeader1" runat="server" />
                </center>
                <br />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblEventName" runat="server" Text="Event Name:" CssClass="Profiletitletxt"
        meta:resourcekey="lblEventNameResource1" />
    <br />
    <asp:Label ID="lblEventDate" runat="server" Text="Event Date:" CssClass="Profiletitletxt"
        meta:resourcekey="lblEventDateResource1" />
    <br />
    <div style="overflow: auto;">
        <table id="tblSchedule" border="1">
        </table>
        <asp:HiddenField ID="hdnDays" runat="server" />
        <asp:HiddenField ID="hdnMonths" runat="server" />
        <%--Store Data to save in DB--%>
        <asp:HiddenField ID="hdnEmployeeNos" runat="server" Value="Fadi" />
        <asp:HiddenField ID="hdnGroupIDs" runat="server" />
        <asp:HiddenField ID="hdnShifts" runat="server" />
        <asp:HiddenField ID="hdnSelectedDays" runat="server" />
        <asp:HiddenField ID="hdnShiftACount" runat="server" />
        <asp:HiddenField ID="hdnShiftBCount" runat="server" />
        <asp:HiddenField ID="hdnShiftCCount" runat="server" />
        <%--Store Data to fill from DB--%>
        <asp:HiddenField ID="hdnSelectedShift" runat="server" />
        <asp:HiddenField ID="hdnSavedDays" runat="server" />
        <asp:HiddenField ID="hdnSelectedEmployees" runat="server" />
        <asp:HiddenField ID="hdnSelectedEmployeesIDs" runat="server" />
        <asp:HiddenField ID="hdnSelectedGroups" runat="server" />
        <%--Store Data Employee Leave--%>
        <asp:HiddenField ID="hdnLeaveDays" runat="server" />
        <asp:HiddenField ID="hdnLeaveMonths" runat="server" />
        <asp:HiddenField ID="hdnLeaveEmployee" runat="server" />
    </div>
    <br />
    <center>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr align="center">
                <td colspan="4" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" meta:resourcekey="btnSaveResource1" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
