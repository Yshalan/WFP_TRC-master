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
var employeesJSON = null; // list of Groups
var scheduleJSON = null; // list of Group_Shifts
var year = 0;
var month = 0;
var Saved = true; // if you have unsaved data this var = false, Don't use directly, use functions save() and unsave()
var __NONE = 0; 	// possible status of each scheduleJSON object
var __ADD = 1; 		// possible status of each scheduleJSON object
var __UPDATE = 2; // possible status of each scheduleJSON object
var __DELETE = 3; // possible status of each scheduleJSON object
var ddlWorkSchedule_selectedIndex = 0;
var empAutocompleteSrc = null; // list of employees to appear in auto complete list
var GroupAutocompleteSrc = null; // list of Groups to appear in auto complete list
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
    if (UICulture.value == "ar-JO") 
    {
        return "Group Schedule not saved";
    }
     else 
    {
        return "Group Schedule not saved";
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
    location.href = "../DailyTasks/Emp_Shifts.aspx";
    //location.href = "../Requests/EmpShiftsManagers.aspx";
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
    if (selectedSchedule == "0") return;
    SelectedSchedule_Global = selectedSchedule;
    ClearScheduleGrid();
    SelectedShift = null;

  
  
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


    //alert(Shifts.length);
    GetData();

    ////Add Shift Details into table
    
    var len = ShiftDetails.length;
    if (browserVersion().agent == 'msie' && browserVersion().version == '8.0')
        len -= 1;
    var divScheduleShiflegend = document.createElement("div");
    for (var i = 0; i < len; i++) {
        var obj = eval(ShiftDetails[i]);
        var divScheduleDetails = document.createElement("div");
        divScheduleDetails.setAttribute('class', 'divShiftClass');
       // divScheduleDetails.setAttribute('style', 'border-color: #C0C0C0;');
       // divScheduleDetails.style.border = "solid #C0C0C0";
        if (selectedSchedule == obj.Scheduleid) {

            var ShiftColor = document.createElement("div");

            ShiftColor.setAttribute('style', '');
            ShiftColor.setAttribute('class', 'col');
            ShiftColor.setAttribute('style', 'background-color:' + obj.bc);

            var ShiftCode = document.createElement("label");
            setInnerText(ShiftCode, '  ' + obj.ScheduleCode);
            var ShiftTime = document.createElement("label");
            setInnerText(ShiftTime, '  ' + obj.ScheduleTime);
            //divScheduleDetails.style.border = "solid #C0C0C0";
            divScheduleDetails.appendChild(ShiftColor);
            divScheduleDetails.innerHTML += ' ';
            divScheduleDetails.appendChild(ShiftCode);
            divScheduleDetails.innerHTML += ' ';
            divScheduleDetails.innerHTML += ' ';
            divScheduleDetails.appendChild(ShiftTime);
            divScheduleDetails.appendChild(document.createElement("br"));
            divScheduleShiflegend.appendChild(divScheduleDetails);
        }

    }
    while (cntrl_divScheduleDetails.firstChild) {
        cntrl_divScheduleDetails.removeChild(cntrl_divScheduleDetails.firstChild);
    }
    divScheduleShiflegend.style.border = "1px solid #C0C0C0";
    cntrl_divScheduleDetails.appendChild(divScheduleShiflegend);
    

}

function ClearMonthTable() {
    if (Page_ClientValidate('WorkSchedule')) {

        var lst = jsonPath(employeesJSON, "$[?(@.listed == \"true\")]");
        if (lst) {
            for (var i = 0; i < lst.length; i++) {
                if (lst[i]) {
                    lst[i].listed = "false";
                }
            }
        }
        var GrpTable = document.getElementById("tblSchedule");
        var GrpTableLength = GrpTable.rows.length;
        if (browserVersion().agent == 'msie' && browserVersion().version == '8.0') {
            var tblScheduleRows = document.getElementById("tblSchedule");
            var tableRows = tblScheduleRows.getElementsByTagName('tr');
            var rowCount = tableRows.length;

            for (var x = rowCount - 1; x > 0; x--) {
                tblScheduleRows.removeChild(tableRows[x]);

            }
        }

        else {
            for (var nItem = 1; nItem < GrpTableLength; nItem++) {
                GrpTable.deleteRow(1);
            }
        }
    }
}

//function btnAddEmp_Click() 
function ShowGrid() 
{
    if (Page_ClientValidate('WorkSchedule')) {        
 
           // scheduleGrid_AddEmp(id);
           // FillAutoCompleteList();     
       
       
    }

}





function GetData() {
    //	send XMLHttpRequest to get list of employees and their shifts according to selected schedule

    var Url = "../Handlers/GetEmployeeSchedule.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global + "&r=" + Math.random().toString();
   
   // var Url = "../Handlers/GetEmployeeSchedule2.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global + "&r=" + Math.random().toString();
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
    // convert received response to JSON data


 
//      employeesJSON = infoJSON[0].Groups;
//   
//    if (employeesJSON == undefined) {
//       
//        employeesJSON = infoJSON[0][0].Groups;
//    }
    employeesJSON = infoJSON[0].employees;
    if (employeesJSON == undefined) {
        employeesJSON = infoJSON[0][0].employees;
    }
    //FillAutoCompleteList();

    scheduleGrid_AddAllEmp(hdnEmployeez.value);

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

            //var Url = "../Handlers/GetEmployeeSchedule2.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global;
            var Url = "../Handlers/GetEmployeeSchedule.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global + "&r=" + Math.random().toString();
          
        

            xhr = new XMLHttpRequest();
            xhr.onreadystatechange = DataPosted;
            xhr.open("POST", Url, true);
            xhr.send(JSON.stringify(dataToPost));
        }
    }
}

function DataPosted() {
    
    var AffectedRows = 0;
    var ValidationHrs = "";
    if (xhr.readyState == 4) {
        if (xhr.status == 200) {
            var data = eval(xhr.responseText);
            if (data) {
                infoJSON = data;
                AffectedRows = infoJSON[1][0].AffectedRows;  //infoJSON[0].AffectedRows;

                ValidationHrs = infoJSON[2][0].ValidationHrs;
                
                var lst = jsonPath(employeesJSON, "$[?(@.listed == \"true\")]");
                if (AffectedRows != -99) {
                    ClearScheduleGrid();
                    ProcessData();
                }
            
              
                // This code to consider listed items
                if (!!lst) {
                    var i = 0;
                   
                    for (i; i < lst.length; i++) {
                        var CurrentEmp = jsonPath(employeesJSON, "$[?(@.EmpId == " + lst[i].EmpId + ")]");

                      
                        if (!!CurrentEmp) {
                          
                         
                            CurrentEmp[0].listed = "true";
                        }
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
            else if (AffectedRows = -99) {
                var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
                if (UICulture.value == "ar-JO") {
                    ShowMessage(ValidationHrs); //ShowMessage('AffectedRows: ' + AffectedRows);
                } else {
                    ShowMessage(ValidationHrs); //ShowMessage('AffectedRows: ' + AffectedRows);
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

            ClearScheduleGrid();
            GetData();

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
    ShowMessage(message);
}

function FillAutoCompleteList() {

    var lst = jsonPath(employeesJSON, "$[?(@.listed == \"false\")]");
    
    if (!!lst)
        GroupAutocompleteSrc = jQuery.map(lst, function (e) { return { label: e.EmpName, value: e.EmpID }; }); //empAutocompleteSrc = lst.map(function(e) { return { label: e.EmpName, value: e.EmpId }; });
    else
        GroupAutocompleteSrc = null;

    
}

function CreateScheduleNode(year, month, day, EmpID) {
    return eval('[{ "EmpID": "' + EmpID
					 + '", "ShiftId": "' + SelectedShift.id
					 + '", "year": "' + year
					 + '", "month": "' + month
					 + '", "day": "' + day
					 + '", "color": "' + SelectedShift.bc
					 + '", "status": "' + "1"
					 + '",},]');

}
function RemoveSchedule(year, month, day, EmpID, td) {
    if (rightButtonDown) {
        
        // this will fire on right shift table cell right-click, Line: 470 in this file  ..  td_b.setAttribute('oncontextmenu', 'RemoveSchedule('  ..
        var _currentSchedule = jsonPath(scheduleJSON, "$[?(@.year == " + year + " && @.month == " + month + " && @.day == " + day + " && @.EmpId == " + EmpID + ")]");
     
        if (!!_currentSchedule) {
            currentSchedule = _currentSchedule[0];
            if (!!currentSchedule) {
                if (currentSchedule.status == __ADD)
                    scheduleJSON.splice(scheduleJSON.indexOf(currentSchedule), 1); // splice means remove
                else
                    currentSchedule.status = __DELETE;

                currentSchedule.ShiftId = 0;

             
            }
        }
        td.style.backgroundColor = '#FFFFFF';
        return false;
    }
}

function ChangeShift(year, month, day, EmpID, td, e) {
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
                var _currentSchedule = jsonPath(scheduleJSON, "$[?(@.year == " + year + " && @.month == " + month + " && @.day == " + day + " && @.EmpId == " + EmpID + ")]");
     
//                var _currentSchedule = jsonPath(scheduleJSON, "$[?(@.year == " + year
//															+ " && @.month == " + month
//															+ " && @.day == " + day
                //															+ " && @.EmpId == " + EmpID + ")]");
               
                if (!!_currentSchedule) {
                    currentSchedule = _currentSchedule[0];
                    currentSchedule.ShiftId = SelectedShift.id;
                    if (currentSchedule.status != __ADD)
                        currentSchedule.status = __UPDATE;
                    td.style.backgroundColor = SelectedShift.bc;
                    unsave();
                }
                else {
                    currentSchedule = CreateScheduleNode(year, month, day, EmpID)[0];
                    currentSchedule.status = __ADD;
                    scheduleJSON = scheduleJSON.concat(currentSchedule)
                    td.style.backgroundColor = SelectedShift.bc;
                    unsave();
                }
            }
            else {
                currentSchedule = CreateScheduleNode(year, month, day, EmpID);
                currentSchedule.status = __ADD;
                scheduleJSON = currentSchedule;
                td.style.backgroundColor = SelectedShift.bc;
                unsave();
            }
        }
    }
    else if (rightButtonDown) {
        return RemoveSchedule(year, month, day, EmpID, td);
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

    var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
    if (UICulture.value == "ar-JO") {
        setInnerText(td_h1, 'رقم الموظف');

    }
    else {
        setInnerText(td_h1, 'Employee No.');

    }

    td_h1.setAttribute('class', 'td_h_no');
    tr_h.appendChild(td_h1);

    var td_h2 = document.createElement("td");

    
    if (UICulture.value == "ar-JO") {
        setInnerText(td_h2, 'اسم الموظف');
      
    }
    else 
    {
       setInnerText(td_h2, 'Employee Name');
  
    }


    td_h2.setAttribute('class', 'td_h_name');
    tr_h.appendChild(td_h2);
  
    for (var j = 1; j <= new Date(year, month, 0).getDate(); j++) {
        var td_h3 = document.createElement("td");
        setInnerText(td_h3, j.toString() + '\n' + getdayName(year, month - 1, j).toString());
        td_h3.setAttribute('class', 'tblSchedule_td');
        if ((new Date(year, month - 1, j).getDay() == 6) || new Date(year, month - 1, j).getDay() == 5) {

            td_h3.style.backgroundColor = '#ccc';
        }
        tr_h.appendChild(td_h3);


        //Ismail working here 

        if (new Date(year, month-1, j).getDay() == 6) {

            var td_hsum = document.createElement("td");
                var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
                if (UICulture.value == "ar-JO") {
                    setInnerText(td_hsum, 'المجموع');
                }
                else {
                    setInnerText(td_hsum, 'Total');
                }
           
            td_hsum.setAttribute('class', 'tblSchedule_td');
            tr_h.appendChild(td_hsum);
        }
      

    }



    // ismail4  working here 
    
    var len2 = Shifts.length;

    for (var i = 0; i < len2; i++) {
        var obj2 = eval(Shifts[i]);
      
        if (obj2.schedule == SelectedSchedule_Global) {
            var td_schshifth = document.createElement("td");
            // setInnerText(td_schshifth, obj2.id);
            td_schshifth.setAttribute('style', 'background-color:' + obj2.bc);
            tr_h.appendChild(td_schshifth);
        }
    }

    var td_d = document.createElement("td");

    tr_h.appendChild(td_d);
    td_d.setAttribute('class', 'tblSchedule_td');
    var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
    if (UICulture.value == "ar-JO") {
        setInnerText(td_d, 'عطل');
    }
    else {
        setInnerText(td_d, 'W.E.');
    }
   
    td_d.setAttribute('style', 'background-color:#ccc;');
    cntrl_tblSchedule.appendChild(tr_h);

    save();
}


function getdayName(year, month, day) {

    var d = new Date(year, month, day,0,0,0,0)
    var weekday = new Array(7);
    var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
    if (UICulture.value == "ar-JO") {
        weekday[0] = "أ.ح";
        weekday[1] = "أ.ث";
        weekday[2] = "ث.ل";
        weekday[3] = "أ.ر";
        weekday[4] = "خ.م";
        weekday[5] = "ج.م";
        weekday[6] = "س.ب";
    }
    else {
        weekday[0] = "Su";
        weekday[1] = "Mo";
        weekday[2] = "Tu";
        weekday[3] = "We";
        weekday[4] = "Th";
        weekday[5] = "Fr";
        weekday[6] = "Sa";
    }
   

    var n = weekday[d.getDay()];
    return n;

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

function td_delete_click(EmpID, tr) {
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
        scheduleGrid_RemoveEmp(EmpID, tr);
        FillAutoCompleteList();
    }
}

function scheduleGrid_RemoveEmp(EmpId, tr) {
    ShowMessage(EmpId);
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

        var GrpScheduleList = jsonPath(scheduleJSON, "$[?(@.EmpId == " + EmpId + ")]");
        for (var i = 0; i < GrpScheduleList.length; i++) {
            scheduleJSON.splice($.inArray(GrpScheduleList[i], scheduleJSON), 1); //scheduleJSON.splice(scheduleJSON.indexOf(EmpScheduleList[i]), 1); // splice means remove. indexOf is not compatible with ie8
        }

        GrpScheduleList = jsonPath(originalScheduleJSON, "$[?(@.EmpId == " + EmpId + ")]");

        for (var i = 0; i < GrpScheduleList.length; i++) {
            scheduleJSON.push(GrpScheduleList[i]);
        }
    }
}


function scheduleGrid_AddAllEmp(EmployeeIDz) {
    var EmpID = 0;
    
    if (EmployeeIDz && EmployeeIDz != '') 
    {
        var EmpIDz = EmployeeIDz;
        var arrEmpIDz = new Array();
        arrEmpIDz = EmpIDz.split(',');

    }
   
    if (validateDDLs() && employeesJSON) {
      
        var lst = jsonPath(employeesJSON, "$[?(@.listed == \"false\")]");

     
            if (arrEmpIDz && arrEmpIDz.length > 0) {

                for (var item = 0; item < arrEmpIDz.length; item++) {

                    EmpID = arrEmpIDz[item];

                    //if (arrEmpIDz[item] != '' && arrEmpIDz[item] == lst[i].GroupID)
                    if (EmpID > 0) {

                        scheduleGrid_AddEMP(EmpID);

                    }


                }
            }
        
        }


            }


            function scheduleGrid_AddEMP(EmpId) 
 {

   
        if (ddlWorkSchedule_selectedIndex == 0) return

        scheduleJSON = infoJSON[0].schedule;

        if (scheduleJSON == undefined) {

            scheduleJSON = infoJSON[0][0].schedule;
        }




        var CurrentGroup = jsonPath(employeesJSON, "$[?(@.EmpId == " + EmpId + ")]");
        if (!!CurrentGroup) {
            
          
            if (CurrentGroup[0].listed == "true") return;
            var tr_b = document.createElement("tr");

            var td_b1 = document.createElement("td");
            setInnerText(td_b1, CurrentGroup[0].EmpNo);
            tr_b.appendChild(td_b1);

            var td_b2 = document.createElement("td");
            setInnerText(td_b2, CurrentGroup[0].EmpName);
            tr_b.appendChild(td_b2);
            var workhours = 0;     
            var WeekendCount=0;    
            for (var j = 1; j <= new Date(year, month, 0).getDate(); j++) {
               
                var td_b = document.createElement("td");
                td_b.style.backgroundColor = '#FFFFFF';
                td_b.setAttribute('onmousedown', 'ChangeShift(' + year + ',' + month + ',' + j + ',' + CurrentGroup[0].EmpId + ',this,event);');
                td_b.setAttribute('onclick', 'DayMouseUp()');
                td_b.setAttribute('onmouseup', 'DayMouseUp()');
                td_b.setAttribute('oncontextmenu', 'rightButtonDown= false; return false;')
                
                //'RemoveSchedule(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this); return false;');
                // You can use the bellow line to enable shift change while mouse move with left mouse button click
                /* BEGIN */
                td_b.setAttribute('onmouseover', 'td_b_mouseover(' + year + ',' + month + ',' + j + ',' + CurrentGroup[0].EmpId + ',this,event);');
                /* END */

             
               
                if (!!scheduleJSON) {

                    var existing = jsonPath(scheduleJSON, "$[?(@.year == " + year + " && @.month == " + month + " && @.day == " + j + " && @.EmpId == " + CurrentGroup[0].EmpId + ")]");

                    if (!!existing) {

                        td_b.style.backgroundColor = existing[0].color;
                        // number of hours per shift need change

                       

                               var len4 = Shifts.length;

                               for (var i = 0; i < len4; i++) {
                                   var obj1 = eval(Shifts[i]);
                                   if (obj1.id == existing[0].ShiftId) {                              
                                       workhours = parseInt( workhours) + parseInt (obj1.Duration);           
                                   }
                               }

                        


                        if ((new Date(year, month - 1, j).getDay() == 5) || (new Date(year, month - 1, j).getDay() == 6)) {

                            WeekendCount = WeekendCount + 1;

                        }



                    }
                    else {

                        if ((new Date(year, month - 1, j).getDay() == 5) || (new Date(year, month - 1, j).getDay() == 6)) {

                            td_b.style.backgroundColor = '#ccc';

                        }
                    
                    }
                }
                tr_b.appendChild(td_b);


                //Ismail2 working here 

                if (new Date(year, month - 1, j).getDay() == 6) {

                    var td_hsum = document.createElement("td");
                    var UICulture = document.getElementById("ctl00_ContentPlaceHolder1_hdnUICulture");
                    if (UICulture.value == "ar-JO") {
                        setInnerText(td_hsum, workhours + ' س');
                    }
                    else {
                        setInnerText(td_hsum, workhours + ' H');
                    }
                    
                    td_hsum.setAttribute('class', 'tblSchedule_td');
                    tr_b.appendChild(td_hsum);
                    workhours = 0;
                }



            }


            // ismail3 to add shifts

            var len = Shifts.length;
            
            for (var i = 0; i < len; i++) {

                var obj1 = eval(Shifts[i]);

            
                // add total of each shift 

                var empshiftId = obj1.id;


             

           

                    var Empshiftcount = jsonPath(scheduleJSON, "$[?(@.year == " + year + " && @.ShiftId == " + empshiftId + " && @.month == " + month + " && @.EmpId == " + CurrentGroup[0].EmpId + ")]");
                    var len2 = 0;
                    if (SelectedSchedule_Global == obj1.schedule) {
                        for (var j = 0; j <= 31; j++) {
                            if (!!Empshiftcount) {

                                if (!!Empshiftcount[j]) {


                                    len2 = len2 + 1;
                                }
                            }
                        }

                    var td_schshift = document.createElement("td");
                    setInnerText(td_schshift, len2);
                    td_schshift.setAttribute('class', 'tblSchedule_td');
                    tr_b.appendChild(td_schshift);
                }
            }

            var td_weekend = document.createElement("td");
            setInnerText(td_weekend, WeekendCount);
            td_weekend.setAttribute('class', 'tblSchedule_td');
            td_weekend.style.backgroundColor = '#ccc';
            tr_b.appendChild(td_weekend);


            var imgDelete = document.createElement("img");
            imgDelete.src = '../Icons/delete.png';
            imgDelete.setAttribute('class', 'imgDelete');
            var td_d = document.createElement("td");
            td_d.setAttribute('onclick', 'td_delete_click(' + CurrentGroup[0].EmpId + ',this.parentNode)');
            td_d.style.cursor = 'pointer';
            td_d.appendChild(imgDelete);
            tr_b.appendChild(td_d);

            cntrl_tblSchedule.appendChild(tr_b);


           



            CurrentGroup[0].listed = "true";
           
        }


}

//function test(id) {
//    alert("here");
//    alert(id);

//}


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
- this JSON list is created in the generic handler ~/Handlers/GetEmployeeSchedule2.ashx
- this JSON list is assigned to the memory throug code of this js file(function ProcessData()) each time ddlWorkSchedule_changed() fired.
- It contains all employees under the selected schedule in the selected month.
- It conains for each employee: EmployeeId, EmployeeNo, EmployeeName, listed(default=false, will be used to show if employee is added to ScheduleGrid)
- ex: { "EmpId": "1", "EmpNo": "5525", "EmpName": "ahmad ahmad", "listed": "false" }
		
2. scheduleJSON
- this JSON list is created in the generic handler ~/Handlers/GetEmployeeSchedule2.ashx
- this JSON list is assigned to the memory throug code of this js file(function ProcessData()) each time ddlWorkSchedule_changed() fired.
- It contains for each Emp_Shift: EmpId, ShiftId, year, month, day, color, status(default=0,{0:NONE,1:ADD,2:UPDATE,3:DELETE})
- ex: { "GroupId": "1", "ShiftId": "4", "year": "2012", "month": "12", "day": "31", "color": "#ABC19D", "status": "0" }
- it will be posted to the server on save_click: function PostData().
		
3. Shifts:
- this JSON list is assigned to <head> using the code behind method(ScheduleGroup_Shifts.aspx.vb: ddlShift_Bind()),
- it contains all shifts under all schedules.
- its data will be changed only after postback.
- it contains for each shift: ShiftId, ShiftCode, SchedulrId, ShiftBackgroundColor.
- ex: { "id": "1", "txt": "A", "schedule": "5", "bc": "#ABC19D" }
*/
