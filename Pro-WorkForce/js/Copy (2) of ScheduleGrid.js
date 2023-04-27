if (!Array.prototype.indexOf) {
    Array.prototype.indexOf = function (elt /*, from*/) {
        var len = this.length >>> 0;

        var from = Number(arguments[1]) || 0;
        from = (from < 0)
         ? Math.ceil(from)
         : Math.floor(from);
        if (from < 0)
            from += len;

        for (; from < len; from++) {
            if (from in this &&
          this[from] === elt)
                return from;
        }
        return -1;
    };
}

// find JSON classes at the end of this file

var leftButtonDown = false;
var rightButtonDown = false;
var SelectedShift = null; // the current selected shift (JSON object)
var SelectedEmployeeList = null;
var xhr = null; // XMLHttpRequest
var infoJSON = null; // eval(chr)
var employeesJSON = null; // list of employees
var scheduleJSON = null; // list of emp_Shifts
var year = 0;
var month = 0;
var Saved = true; // if you have unsaved data this var = false, Don't use directly, use functions save() and unsave()
var __NONE = 0; 	// possible status of each scheduleJSON object
var __ADD = 1; 		// possible status of each scheduleJSON object
var __UPDATE = 2; // possible status of each scheduleJSON object
var __DELETE = 3; // possible status of each scheduleJSON object
var ddlWorkSchedule_selectedIndex = 0;
var empAutocompleteSrc = null; // list of employees to appear in auto complete list
var SelectedSchedule_Global = '';

//detect browser
var isMozilla = (navigator.userAgent.indexOf("compatible") == -1);

window.onload = function () {

}

function save() {
    // call this function to set form saved = true;
    Saved = true;
    removeEvent(window, 'beforeunload', ConfirmClose);

    return true;
}

function unsave() {
    // call this function to set form saved = false;
    Saved = false;
    window.addEvent(window, 'beforeunload', ConfirmClose);
    return false;
}

function ConfirmClose() {
    var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
    if (UICulture.value == "ar-JO") {
        return "جدول الموظفين لم يحفظ";
    } else {
        return "Employees Schedule not saved";
    }
}

function addEvent(node, evt, fnc) {
    if (node.addEventListener)
        node.addEventListener(evt, fnc, false);
    else if (node.attachEvent)
        node.attachEvent('on' + evt, fnc);
    else
        return false;
    return true;
}

function removeEvent(node, evt, fnc) {
    if (node.removeEventListener)
        node.removeEventListener(evt, fnc, false);
    else if (node.detachEvent)
        node.detachEvent('on' + evt, fnc);
    else
        return false;
    return true;
}

function setInnerText(cntrl, text) {
    if (isMozilla)
        cntrl.innerHTML = text;
    else
        cntrl.innerText = text;
}

function ClearForm() {
    var conf = false;
    if (!Saved) {
        var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
        if (UICulture.value == "ar-JO") {
            conf = confirm('لم تقم بحفظ التغيرات التي قمت بها, هل انت متأكد من الإستمرار؟');
        } else {
            conf = confirm('You have unsaved data that will be lost, do you want to continue?');
        }
    }
    else
        conf = true;

    if (!conf) {
        cntrl_ddlWorkSchedule.selectedIndex = ddlWorkSchedule_selectedIndex;
        return;
    }
    save();
    location.href = "../dailytasks/Emp_Shifts.aspx";
    //    ClearTable(cntrl_tblSchedule);
    //    for (var i = 0; i < Shifts.length; i++)
    //        Shifts.listed = "false";
    //    setInnerText(cntrl_lblYearMessage, '');
    //    setInnerText(cntrl_lblMonthMessage, '');
    //    cntrl_ddlYear.selectedIndex = 0;
    //    cntrl_ddlMonth.selectedIndex = 0;
    //    cntrl_ddlWorkSchedule.selectedIndex = 0;
    //    cntrl_divColor.innerHTML = '';
    //    SelectedShift = null;
    //    selectedCol.style.backgroundColor = '#FFFFFF';
    //    setInnerText(selectedTxt, '');
    //    $('#divColor').hide();
    //    SelectedEmployeeList = null;
    //    ddlWorkSchedule_selectedIndex = 0;
    //    empAutocompleteSrc = null;
    //    cntrl_txtEmpNo.value = '';

}


function SelectShift(shiftId, shiftCode, sender) {
    // this will be fired when the client choose a shift, note line 156 in this file  ..  divShift.setAttribute('onclick', 'SelectShift(\'' ..
    SelectedShift = jsonPath(Shifts, "$[?(@.id == " + shiftId + ")]")[0];
    selectedCol.style.backgroundColor = SelectedShift.bc;
    setInnerText(selectedTxt, ' ' + shiftCode);
    $('#divColor').hide();
    cntrl_divColor.style.display = "none";
}


function ddlWorkSchedule_changed(selectedSchedule) {
    debugger;
    // Emp_Shifts.aspx, Line: 59, <asp:DropDownList ID="ddlWorkSchedule" ...
    var conf = false;
    if (!Saved) {
        var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
        if (UICulture.value == "ar-JO") {
            conf = confirm('لم تقم بحفظ التغيرات التي قمت بها, هل انت متأكد من الإستمرار؟');
        } else {
            conf = confirm('You have unsaved data that will be lost, do you want to continue?');
        }
    }
    else
        conf = true;

    if (!conf) {
        cntrl_ddlWorkSchedule.selectedIndex = ddlWorkSchedule_selectedIndex;
        return;
    }

    var bValid = validateDDLs();

    if (!bValid) {
        return;
    }
    ddlWorkSchedule_selectedIndex = cntrl_ddlWorkSchedule.selectedIndex;

    ClearScheduleGrid();
    SelectedShift = null;

    if (selectedSchedule == "0") return;
    SelectedSchedule_Global = selectedSchedule;

    // add shifts of selected schedule to divColor, Emp_Shifts.aspx, Line: 74 , <div id="divColor"></div>
    var len = Shifts.length;
    if (browserVersion().agent == 'msie' && browserVersion().version == '8.0')
        len -= 1;
    for (var i = 0; i < len; i++) {
        var obj = eval(Shifts[i]);
        if (obj.schedule == selectedSchedule.toString()) {
            var divShift = document.createElement("div");
            divShift.setAttribute('id', 'divShift' + obj.id);
            divShift.setAttribute('onclick', 'SelectShift(\'' + obj.id + '\',\'' + obj.txt + '\',' + '\'divShift' + obj.id + '\')');

            divShift.setAttribute('class', 'divShiftClass');
            divShift.setAttribute('style', 'border-color: #000000;');
            var ShiftColor = document.createElement("div");
            ShiftColor.setAttribute('style', '');
            ShiftColor.setAttribute('class', 'col');
            ShiftColor.setAttribute('style', 'background-color:' + obj.bc);

            var ShiftCode = document.createElement("label");
            setInnerText(ShiftCode, '  ' + obj.txt);

            divShift.appendChild(ShiftColor);
            divShift.appendChild(ShiftCode);
            divShift.appendChild(document.createElement("br"));
            cntrl_divColor.appendChild(divShift);
        }
    }
    GetData();
}


function GetData() {
    //	send XMLHttpRequest to get list of employees and their shifts according to selected schedule
    var Url = "../Handlers/GetEmployeeSchedule.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global + "&r=" + Math.random().toString();
    xhr = new XMLHttpRequest();
    xhr.onreadystatechange = ResponseReceived;
    xhr.open("GET", Url, true);
    xhr.send(null);
}

function ResponseReceived() {
    if (xhr.readyState == 4 && xhr.status == 200) {
        infoJSON = eval(xhr.responseText);
        ProcessData();
    }
    if (xhr.readyState == 4 && xhr.status >= 400) {
        var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
        if (UICulture.value == "ar-JO") {
            ShowMessage('خطأ');
        } else {
            ShowMessage('Error');
        }
    }
}
function ProcessData() {
    debugger;
    // convert received response to JSON data
    employeesJSON = infoJSON[0].employees;
    if (employeesJSON == undefined) {
        employeesJSON = infoJSON[0][0].employees;
    }

    FillAutoCompleteList();
   
    scheduleGrid_AddAllEmp(hdnEmployeeIDz.value);
    scheduleJSON = infoJSON[0].schedule;
    if (scheduleJSON == undefined) {
        scheduleJSON = infoJSON[0][0].schedule;
    }
}

function PostData() {
    if (scheduleJSON) {
        var dataToPost = jsonPath(scheduleJSON, "$[?(@.status != \"0\")]");
        if (dataToPost) {
            cntrl_imgLoading.style.display = '';
            cntrl_btnSave.disabled = 'disabled';
            cntrl_btnClear.disabled = 'disabled';

            var Url = "../Handlers/GetEmployeeSchedule.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global;

            xhr = new XMLHttpRequest();
            xhr.onreadystatechange = DataPosted;
            xhr.open("POST", Url, true);
            xhr.send(JSON.stringify(dataToPost));
        }
    }
}

function DataPosted() {
    var AffectedRows = 0;
    if (xhr.readyState == 4) {
        if (xhr.status == 200) {
            var data = eval(xhr.responseText);
            if (data) {
                infoJSON = data;
                AffectedRows = infoJSON[1][0].AffectedRows;  //infoJSON[0].AffectedRows;
                var lst = jsonPath(employeesJSON, "$[?(@.listed == \"true\")]");

                ProcessData();

                // This code to consider listed items
                if (!!lst) {
                    var i = 0;
                    for (i; i < lst.length; i++) {
                        var CurrentEmp = jsonPath(employeesJSON, "$[?(@.EmpId == " + lst[i].EmpId + ")]");
                        if (!!CurrentEmp)
                            CurrentEmp[0].listed = "true";
                    }
                }
            }
            if (AffectedRows > 0) {
                var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
                if (UICulture.value == "ar-JO") {
                    ShowMessage('تم الحفظ بنجاح'); //ShowMessage('AffectedRows: ' + AffectedRows);
                } else {
                    ShowMessage('Saved Successfully'); //ShowMessage('AffectedRows: ' + AffectedRows);
                }
            }
            else {
                var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
                if (UICulture.value == "ar-JO") {
                    ShowMessage('تمت العملية بنجاح, ولكن بدون اي تغيير على البيانات');
                } else {
                    ShowMessage('Process Completed, but no data changed');
                }
            }

            save();
        }
        if (xhr.status >= 400) {
            var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
            if (UICulture.value == "ar-JO") {
                ShowMessage('خطأ');
            } else {
                ShowMessage('Error');
            }
        }
        cntrl_imgLoading.style.display = 'none';
        cntrl_btnSave.removeAttribute('disabled', 'disabled');
        cntrl_btnClear.removeAttribute('disabled', 'disabled');
    }
}

function ShowMessage(message) {
    alert(message);
}

function FillAutoCompleteList() {
    var lst = jsonPath(employeesJSON, "$[?(@.listed == \"false\")]");
    if (!!lst)
        empAutocompleteSrc = jQuery.map(lst, function (e) { return { label: e.EmpName, value: e.EmpNo }; }); //empAutocompleteSrc = lst.map(function(e) { return { label: e.EmpName, value: e.EmpId }; });
    else
        empAutocompleteSrc = null;

    $("#ctl00_ContentPlaceHolder1_txtEmpNo").autocomplete({
        source: empAutocompleteSrc,
        select: function (event, ui) {
            var id = GetEmpId(ui.item.value);
            scheduleGrid_AddEmp(id);
            FillAutoCompleteList();
            cntrl_txtEmpNo.value = '';
            return false;
        }
    });
}

function CreateScheduleNode(year, month, day, EmpId) {
    return eval('[{ "EmpId": "' + EmpId
					 + '", "ShiftId": "' + SelectedShift.id
					 + '", "year": "' + year
					 + '", "month": "' + month
					 + '", "day": "' + day
					 + '", "color": "' + SelectedShift.bc
					 + '", "status": "' + "1"
					 + '",},]');

}
function RemoveSchedule(year, month, day, EmpId, td) {
    if (rightButtonDown) {
        // this will fire on right shift table cell right-click, Line: 470 in this file  ..  td_b.setAttribute('oncontextmenu', 'RemoveSchedule('  ..
        var _currentSchedule = jsonPath(scheduleJSON, "$[?(@.year == " + year + " && @.month == " + month + " && @.day == " + day + " && @.EmpId == " + EmpId + ")]");
        if (!!_currentSchedule) {
            currentSchedule = _currentSchedule[0];
            if (!!currentSchedule) {
                if (currentSchedule.status == __ADD)
                    scheduleJSON.splice(scheduleJSON.indexOf(currentSchedule), 1); // splice means remove
                else
                    currentSchedule.status = __DELETE;

                currentSchedule.ShiftId = 0;

                td.style.backgroundColor = '#FFFFFF';
            }
        }
        return false;
    }
}

function ChangeShift(year, month, day, EmpId, td, e) {
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
        if (!!SelectedShift) {
            var currentSchedule;
            if (!!scheduleJSON) {
                var _currentSchedule = jsonPath(scheduleJSON, "$[?(@.year == " + year
															+ " && @.month == " + month
															+ " && @.day == " + day
															+ " && @.EmpId == " + EmpId + ")]");
                if (!!_currentSchedule) {
                    currentSchedule = _currentSchedule[0];
                    currentSchedule.ShiftId = SelectedShift.id;
                    if (currentSchedule.status != __ADD)
                        currentSchedule.status = __UPDATE;
                    td.style.backgroundColor = SelectedShift.bc;
                    unsave();
                }
                else {
                    currentSchedule = CreateScheduleNode(year, month, day, EmpId)[0];
                    currentSchedule.status = __ADD;
                    scheduleJSON = scheduleJSON.concat(currentSchedule)
                    td.style.backgroundColor = SelectedShift.bc;
                    unsave();
                }
            }
            else {
                currentSchedule = CreateScheduleNode(year, month, day, EmpId);
                currentSchedule.status = __ADD;
                scheduleJSON = currentSchedule;
                td.style.backgroundColor = SelectedShift.bc;
                unsave();
            }
        }
    }
    else if (rightButtonDown) {
        return RemoveSchedule(year, month, day, EmpId, td);
    }
}

function YearMonthChanged() {
    var valid = validateDDLs();
    
    if (!valid) {
        return;
    }

    var conf = false;
    if (!Saved) {
        var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
        if (UICulture.value == "ar-JO") {
            conf = confirm('لم تقم بحفظ التغيرات التي قمت بها, هل انت متأكد من الإستمرار؟');
        } else {
            conf = confirm('You have unsaved data that will be deleted, do you want to continue?');
        }
    }

    else
        conf = true;
    
    if (conf) {
        cntrl_ddlWorkSchedule.selectedIndex = 0;
        ClearScheduleGrid();        

        cntrl_divColor.innerHTML = '';
        SelectedShift = null;
        selectedCol.style.backgroundColor = '#FFFFFF';
        setInnerText(selectedTxt, '');
        $('#divColor').hide();

        GetData();        
    }
}

function ClearScheduleGrid() {

    ClearTable(cntrl_tblSchedule); //.innerHTML = '';
    cntrl_divColor.innerHTML = '';

    SelectedEmployeeList = null;
    SelectedShift = null;

    year = cntrl_ddlYear.options[cntrl_ddlYear.selectedIndex].value;
    month = cntrl_ddlMonth.options[cntrl_ddlMonth.selectedIndex].value;

    var tr_h = document.createElement("tr");
    var td_h1 = document.createElement("td");
    setInnerText(td_h1, 'Emp. No.');
    td_h1.setAttribute('class', 'td_h_no');
    tr_h.appendChild(td_h1);

    var td_h2 = document.createElement("td");
    setInnerText(td_h2, 'Emp. Name');
    td_h2.setAttribute('class', 'td_h_name');
    tr_h.appendChild(td_h2);

    for (var j = 1; j <= new Date(year, month, 0).getDate(); j++) {
        var td_h3 = document.createElement("td");
        setInnerText(td_h3, j.toString());
        td_h3.setAttribute('class', 'tblSchedule_td');
        tr_h.appendChild(td_h3);
    }

    var td_d = document.createElement("td");
    tr_h.appendChild(td_d);

    cntrl_tblSchedule.appendChild(tr_h);

    save();
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

function td_delete_click(EmpId, tr) {
    // Delete entire row (employee) from ScheduleGrid, any unsaved data for this employee will be lost,
    // if the employee added again the original data will appear.
    var conf = false;
    if (!Saved) {
        var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
        if (UICulture.value == "ar-JO") {
            conf = confirm('لم تقم بحفظ التغيرات التي قمت بها, هل انت متأكد من الإستمرار؟');
        } else {
            conf = confirm('You have unsaved data that will be deleted, do you want to continue?');
        }
    }
    else
        conf = true;

    if (conf) {
        scheduleGrid_RemoveEmp(EmpId, tr);
        FillAutoCompleteList();
    }
}

function scheduleGrid_RemoveEmp(EmpId, tr) {

    var CurrentEmp = jsonPath(employeesJSON, "$[?(@.EmpId == " + EmpId + ")]");
    if (!!CurrentEmp) {

        var len = tr.childNodes.length
        for (var i = 0; i < len; i++) {
            tr.removeChild(tr.childNodes.item(0));
        }

        cntrl_tblSchedule.removeChild(tr);
        tr = null; // keep this to free allocated memory.
        CurrentEmp[0].listed = "false";

        var originalScheduleJSON = eval(xhr.responseText)[0].schedule;

        var EmpScheduleList = jsonPath(scheduleJSON, "$[?(@.EmpId == " + EmpId + ")]");
        for (var i = 0; i < EmpScheduleList.length; i++) {
            scheduleJSON.splice($.inArray(EmpScheduleList[i], scheduleJSON), 1); //scheduleJSON.splice(scheduleJSON.indexOf(EmpScheduleList[i]), 1); // splice means remove. indexOf is not compatible with ie8
        }

        EmpScheduleList = jsonPath(originalScheduleJSON, "$[?(@.EmpId == " + EmpId + ")]");

        for (var i = 0; i < EmpScheduleList.length; i++) {
            scheduleJSON.push(EmpScheduleList[i]);
        }
    }
}
function scheduleGrid_AddEmp(EmpId) {
    debugger;
    if (ddlWorkSchedule_selectedIndex == 0) return;

    var CurrentEmp = jsonPath(employeesJSON, "$[?(@.EmpId == " + EmpId + ")]");
    if (!!CurrentEmp) {
        if (CurrentEmp[0].listed == "true") return;
        var tr_b = document.createElement("tr");

        var td_b1 = document.createElement("td");
        setInnerText(td_b1, CurrentEmp[0].EmpNo);
        tr_b.appendChild(td_b1);

        var td_b2 = document.createElement("td");
        setInnerText(td_b2, CurrentEmp[0].EmpName);
        tr_b.appendChild(td_b2);
        for (var j = 1; j <= new Date(year, month, 0).getDate(); j++) {
            var td_b = document.createElement("td");
            td_b.style.backgroundColor = '#FFFFFF';
            td_b.setAttribute('onmousedown', 'ChangeShift(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this,event);');
            td_b.setAttribute('onclick', 'DayMouseUp()');
            td_b.setAttribute('onmouseup', 'DayMouseUp()');
            td_b.setAttribute('oncontextmenu', 'rightButtonDown= false; return false;')//'RemoveSchedule(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this); return false;');
            // You can use the bellow line to enable shift change while mouse move with left mouse button click
            /* BEGIN */
            td_b.setAttribute('onmouseover', 'td_b_mouseover(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this,event);');
            /* END */
            if (!!scheduleJSON) {
                var existing = jsonPath(scheduleJSON, "$[?(@.year == " + year + " && @.month == " + month + " && @.day == " + j + " && @.EmpId == " + CurrentEmp[0].EmpId + ")]");
                if (!!existing) td_b.style.backgroundColor = existing[0].color;
            }
            tr_b.appendChild(td_b);
        }
        var imgDelete = document.createElement("img");
        imgDelete.src = '../Icons/delete.png';
        imgDelete.setAttribute('class', 'imgDelete');
        var td_d = document.createElement("td");
        td_d.setAttribute('onclick', 'td_delete_click(' + CurrentEmp[0].EmpId + ',this.parentNode)');
        td_d.style.cursor = 'pointer';
        td_d.appendChild(imgDelete);
        tr_b.appendChild(td_d);

        cntrl_tblSchedule.appendChild(tr_b);

        CurrentEmp[0].listed = "true";
    }
}

function scheduleGrid_AddAllEmp(EmployeeIDz) {
    if (EmployeeIDz && EmployeeIDz != '') {
        var EmpIDz = EmployeeIDz;
        var arrEmpIDz = new Array();
        arrEmpIDz = EmpIDz.split(',');
    }
    if (validateDDLs() && employeesJSON) {
        var lst = jsonPath(employeesJSON, "$[?(@.listed == \"false\")]");
        if (lst) {
            for (var i = 0; i < lst.length; i++) {
                if (lst[i])
                    if (arrEmpIDz && arrEmpIDz.length > 0) {
                        for (var item = 0; item < arrEmpIDz.length; item++) {
                            if (arrEmpIDz[item] != '' && arrEmpIDz[item] == lst[i].EmpId)
                                scheduleGrid_AddEmp(lst[i].EmpId);
                        }
                    }
            }
            FillAutoCompleteList();
        }
    }
}

function btnAddEmp_Click() {
    if (Page_ClientValidate('AddEmployeeGroup') && Page_ClientValidate('WorkSchedule')) {
        var id = GetEmpId(cntrl_txtEmpNo.value);
        if (id != null) {
            scheduleGrid_AddEmp(id);
            FillAutoCompleteList();
            cntrl_txtEmpNo.value = '';
        }
        else {
            var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
            if (UICulture.value == "ar-JO") {
                ShowMessage("لم يتم العثور على الموظف رقم.: " + cntrl_txtEmpNo.value + ", الرجاء التاكد ان الموظف المختار لديه جدول عمل متقدم");
            }
            else {
                ShowMessage("Cannot find employee with no.: " + cntrl_txtEmpNo.value + ", Make sure selected employee has advanced schedule");
            }
        }
    }
}

function txtEmpNo_keypress(e) {
    if (e.keyCode == 13) {
        btnAddEmp_Click();
        return false;
    }
}

function GetEmpId(EmpNo) {
    if (employeesJSON != null) {
        var selected = jsonPath(employeesJSON, "$[?(@.EmpNo == \"" + EmpNo + "\")]")[0];
        if (!!selected)
            return selected.EmpId;
        else
            return null;
    }
}


//	You can use the bellow JQuery to enable shift change while mouse move with left mouse button click
/*	BEGIN */
$(document).mousedown(function (e) {
    //if (e.which === 1) leftButtonDown = true;
});
$(document).mouseup(function (e) {
    //if (e.which === 1) leftButtonDown = false;
});

function tweakMouseMoveEvent(e) {
    if ($.browser.msie && !(document.documentMode >= 9) && !event.button) {
        leftButtonDown = false;
    }
    //if (e.which === 1 && !leftButtonDown) e.which = 0;
}

function td_b_mouseover(year, month, day, EmpId, td, event) {
    //tweakMouseMoveEvent(e);
    if (leftButtonDown) {
        rightButtonDown = false;
        ChangeShift(year, month, day, EmpId, td, event);
    }

    if (rightButtonDown) {
        leftButtonDown = false;
        return RemoveSchedule(year, month, day, EmpId, td);

    }
}

function DayMouseUp() {
    leftButtonDown = false;
}

function validateDDLs() {
    var IsValidate = true;
    // clear ddls labels error message
    setInnerText(cntrl_lblYearMessage, '');
    setInnerText(cntrl_lblMonthMessage, '');
    //setInnerText(cntrl_lblWorkSchedule, '');

    if (cntrl_ddlYear.selectedIndex == -1) {
        IsValidate = false;
        cntrl_ddlYear.focus();
        //setInnerText(cntrl_lblYearMessage, 'Select year');
    }

    if (cntrl_ddlMonth.selectedIndex == -1) {
        IsValidate = false;
        cntrl_ddlMonth.focus();
        //setInnerText(cntrl_lblMonthMessage, 'Select month');
    }

    if (cntrl_ddlWorkSchedule.selectedIndex == -1) {
        IsValidate = false;
        cntrl_ddlWorkSchedule.focus();
        //setInnerText(cntrl_lblWorkSchedule, 'Select workschedule');
    }

    return IsValidate;
}

window.onunload = function () {
    leftButtonDown = false;
    rightButtonDown = false;
}
/*	END */


/*
JSON Classes:
1. employeesJSON
- this JSON list is created in the generic handler ~/Handlers/GetEmployeeSchedule.ashx
- this JSON list is assigned to the memory throug code of this js file(function ProcessData()) each time ddlWorkSchedule_changed() fired.
- It contains all employees under the selected schedule in the selected month.
- It conains for each employee: EmployeeId, EmployeeNo, EmployeeName, listed(default=false, will be used to show if employee is added to ScheduleGrid)
- ex: { "EmpId": "1", "EmpNo": "5525", "EmpName": "ahmad ahmad", "listed": "false" }
		
2. scheduleJSON
- this JSON list is created in the generic handler ~/Handlers/GetEmployeeSchedule.ashx
- this JSON list is assigned to the memory throug code of this js file(function ProcessData()) each time ddlWorkSchedule_changed() fired.
- It contains for each Emp_Shift: EmpId, ShiftId, year, month, day, color, status(default=0,{0:NONE,1:ADD,2:UPDATE,3:DELETE})
- ex: { "EmpId": "1", "ShiftId": "4", "year": "2012", "month": "12", "day": "31", "color": "#ABC19D", "status": "0" }
- it will be posted to the server on save_click: function PostData().
		
3. Shifts:
- this JSON list is assigned to <head> using the code behind method(Emp_Shifts.aspx.vb: ddlShift_Bind()),
- it contains all shifts under all schedules.
- its data will be changed only after postback.
- it contains for each shift: ShiftId, ShiftCode, SchedulrId, ShiftBackgroundColor.
- ex: { "id": "1", "txt": "A", "schedule": "5", "bc": "#ABC19D" }
*/
