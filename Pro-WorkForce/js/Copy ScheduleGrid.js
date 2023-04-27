// find JSON classes at the end of this file

var SelectedShift = null; // the current selected shift (JSON object)
var SelectedEmployeeList = null;
var xhr = null; // XMLHttpRequest
var infoJSON = null; // eval(chr)
var employeesJSON = null; // list of employees
var scheduleJSON = null;	// list of emp_Shifts
var year = 0;
var month = 0;
var Saved = true;	// if you have unsaved data this var = false, Don't use directly, use functions save() and unsave()
var __NONE = 0;		// possible status of each scheduleJSON object
var __ADD = 1; 		// possible status of each scheduleJSON object
var __UPDATE = 2;	// possible status of each scheduleJSON object
var __DELETE = 3;	// possible status of each scheduleJSON object
var ddlWorkSchedule_selectedIndex = 0;
var empAutocompleteSrc = null; // list of employees to appear in auto complete list
var SelectedSchedule_Global = '';

//detect browser
var isMozilla = (navigator.userAgent.indexOf("compatible") == -1);

function save()
{
	// call this function to set form saved = true;
	Saved = true;
	removeEvent(window, 'beforeunload', ConfirmClose);
	
	return true;
}

function unsave()
{
	// call this function to set form saved = false;
	Saved = false;
	window.addEvent(window, 'beforeunload', ConfirmClose);
	return false;
}

function ConfirmClose()
{
	return "Employees Schedule not saved";
}

function addEvent(node, evt, fnc)
{
	if (node.addEventListener)
		node.addEventListener(evt, fnc, false);
	else if (node.attachEvent)
		node.attachEvent('on' + evt, fnc);
	else
		return false;
	return true;
}

function removeEvent(node, evt, fnc)
{
	if (node.removeEventListener)
		node.removeEventListener(evt, fnc, false);
	else if (node.detachEvent)
		node.detachEvent('on' + evt, fnc);
	else
		return false;
	return true;
}


function ClearForm()
{
	var conf = false;
	if (!Saved)
		conf = confirm('You have unsaved data that will be deleted, do you want to continue?');
	else
		conf = true;

	if (!conf) {
		cntrl_ddlWorkSchedule.selectedIndex = ddlWorkSchedule_selectedIndex;
		return;
	}

	cntrl_tblSchedule.innerHTML = '';
	for (var i = 0; i < Shifts.length; i++)
		Shifts.listed = "false";
	cntrl_lblYearMessage.innerHTML = '';
	cntrl_lblMonthMessage.innerHTML = '';
	cntrl_ddlYear.selectedIndex = 0;
	cntrl_ddlMonth.selectedIndex = 0;
	cntrl_ddlWorkSchedule.selectedIndex = 0;
	cntrl_divColor.innerHTML = '';
	SelectedShift = null;
	selectedCol.style.backgroundColor = '#FFFFFF';
	selectedTxt.innerHTML = '';
	$('#divColor').hide();
	SelectedEmployeeList = null;
	ddlWorkSchedule_selectedIndex = 0;
	empAutocompleteSrc = null;
	save();
}


function SelectShift(shiftId, shiftCode, sender)
{
	// this will be fired when the client choose a shift, note line 156 in this file  ..  divShift.setAttribute('onclick', 'SelectShift(\'' ..
	SelectedShift = jsonPath(Shifts, "$[?(@.id == " + shiftId + ")]")[0];
	selectedCol.style.backgroundColor = SelectedShift.bc;
	selectedTxt.innerHTML = ' ' + shiftCode;
	$('#divColor').hide();
}


function ddlWorkSchedule_changed(selectedSchedule)
{
	// Emp_Shifts.aspx, Line: 59, <asp:DropDownList ID="ddlWorkSchedule" ...
	cntrl_lblYearMessage.innerHTML = '';
	cntrl_lblMonthMessage.innerHTML = '';

	var conf = false;
	if (!Saved)
		conf = confirm('You have unsaved data that will be deleted, do you want to continue?');
	else
		conf = true;

	if (!conf) {
		cntrl_ddlWorkSchedule.selectedIndex = ddlWorkSchedule_selectedIndex;
		return;
	}

	if (year == 0) {
		cntrl_ddlWorkSchedule.selectedIndex = ddlWorkSchedule_selectedIndex;
		cntrl_ddlYear.focus();
		cntrl_lblYearMessage.innerHTML = 'Select year';
		return;
	}

	if (month == 0) {
		cntrl_ddlWorkSchedule.selectedIndex = ddlWorkSchedule_selectedIndex;
		cntrl_ddlMonth.focus();
		cntrl_lblMonthMessage.innerHTML = 'Select month';
		return;
	}

	ddlWorkSchedule_selectedIndex = cntrl_ddlWorkSchedule.selectedIndex;

	ClearScheduleGrid();
	SelectedShift = null;

	if (selectedSchedule == "0") return;
	SelectedSchedule_Global = selectedSchedule;

	// add shifts of selected schedule to divColor, Emp_Shifts.aspx, Line: 74 , <div id="divColor"></div>
	for (var i = 0; i < Shifts.length; i++) {
		var obj = eval(Shifts[i]);
		if (obj.schedule == selectedSchedule.toString()) {
			var divShift = document.createElement("div");
			divShift.setAttribute('id', 'divShift' + obj.id);
			divShift.setAttribute('onclick', 'SelectShift(\'' + obj.id + '\',\'' + obj.txt + '\',' + '\'divShift' + obj.id + '\')');

			divShift.setAttribute('class', 'divShiftClass');
			divShift.setAttribute('style', 'border-color: #000000;');
			var ShiftColor = document.createElement("div");
			ShiftColor.style = '';
			ShiftColor.setAttribute('class', 'col');
			ShiftColor.setAttribute('style', 'background-color:' + obj.bc);

			var ShiftCode = document.createElement("label");
			ShiftCode.innerHTML = '  ' + obj.txt; //ShiftCode.innerText = '  ' + obj.txt;

			divShift.appendChild(ShiftColor);
			divShift.appendChild(ShiftCode);
			divShift.appendChild(document.createElement("br"));
			cntrl_divColor.appendChild(divShift);
		}
	}
	GetData();
}


function GetData()
{
	//	send XMLHttpRequest to get list of employees and their shifts according to selected schedule
	var Url = "../Handlers/GetEmployeeSchedule.ashx?year=" + year + "&month=" + month + "&schedule=" + SelectedSchedule_Global + "&r=" + Math.random().toString();
	xhr = new XMLHttpRequest();
	xhr.onreadystatechange = ResponseReceived;
	xhr.open("GET", Url, true);
	xhr.send(null);
}

function ResponseReceived()
{
	if (xhr.readyState == 4 && xhr.status == 200) {
		infoJSON = eval(xhr.responseText);
		ProcessData();
	}
	if (xhr.readyState == 4 && xhr.status >= 400) {
		ShowMessage('Error');
	}
}
function ProcessData() {
	// convert received response to JSON data
	employeesJSON = infoJSON[0].employees;

	FillAutoCompleteList();

	scheduleJSON = infoJSON[0].schedule;
}

function PostData()
{
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

function DataPosted()
{
	var AffectedRows = 0;
	if (xhr.readyState == 4) {
		if (xhr.status == 200) {
			var data = eval(xhr.responseText);
			if (data) {
				AffectedRows = data[1][0].AffectedRows;
				infoJSON = data[0];
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
			if(AffectedRows > 0)
				ShowMessage('Saved Successfully'); //ShowMessage('AffectedRows: ' + AffectedRows);
			else
				ShowMessage('Process Completed, but no data changed');
			save();
		}
		if (xhr.status >= 400) {
			ShowMessage('Error');
		}
		cntrl_imgLoading.style.display = 'none';
		cntrl_btnSave.removeAttribute('disabled', 'disabled');
		cntrl_btnClear.removeAttribute('disabled', 'disabled');
	}
}

function ShowMessage(message)
{
	alert(message);
}

function FillAutoCompleteList()
{
	var lst = jsonPath(employeesJSON, "$[?(@.listed == \"false\")]");
	if (!!lst)
		empAutocompleteSrc = lst.map(function(e) { return { label: e.EmpName, value: e.EmpId }; });
	else
		empAutocompleteSrc = null;

	$("#ctl00_ContentPlaceHolder1_txtEmpNo").autocomplete({
		source: empAutocompleteSrc,
		select: function(event, ui)
		{
			scheduleGrid_AddEmp(ui.item.value);
			FillAutoCompleteList();
		}
	});
}

function CreateScheduleNode(year, month, day, EmpId)
{
	return eval('[{ "EmpId": "' + EmpId
					 + '", "ShiftId": "' + SelectedShift.id
					 + '", "year": "' + year
					 + '", "month": "' + month
					 + '", "day": "' + day
					 + '", "color": "' + SelectedShift.bc
					 + '", "status": "' + "1"
					 + '",},]');

}
function RemoveSchedule(year, month, day, EmpId, td)
{
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
}

function ChangeShift(year, month, day, EmpId, td)
{
	// this will fire on shift table cell shift changed (mouse-click), Line: 469 in this file   ..  td_b.setAttribute('onclick', 'ChangeShift('  ..
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

function YearMonthChanged()
{
	var conf = false;
	if (!Saved)
		conf = confirm('You have unsaved data that will be deleted, do you want to continue?');
	else
		conf = true;

	if (conf) {
		cntrl_ddlWorkSchedule.selectedIndex = 0;
		ClearScheduleGrid();
	}
}

function ClearScheduleGrid()
{
	
	cntrl_tblSchedule.innerHTML = '';
	cntrl_divColor.innerHTML = '';

	SelectedEmployeeList = null;
	SelectedShift = null;

	year = cntrl_ddlYear.options[cntrl_ddlYear.selectedIndex].value;
	month = cntrl_ddlMonth.options[cntrl_ddlMonth.selectedIndex].value;
	
	var tr_h = document.createElement("tr");
	var td_h1 = document.createElement("td");
	td_h1.innerHTML = 'Emp. No.';//td_h1.innerText = 'Emp. No.';
	td_h1.setAttribute('class', 'td_h_no');
	tr_h.appendChild(td_h1);

	var td_h2 = document.createElement("td");
	td_h2.innerHTML = 'Emp. Name'; //td_h2.innerText = 'Emp. Name';
	td_h1.setAttribute('class', 'td_h_name');
	tr_h.appendChild(td_h2);

	for (var j = 1; j <= new Date(year, month, 0).getDate(); j++) {
		var td_h3 = document.createElement("td");
		td_h3.innerHTML = j.toString(); //td_h3.innerText = j.toString();
		td_h3.setAttribute('class', 'tblSchedule_td');
		tr_h.appendChild(td_h3);
	}

	var td_d = document.createElement("td");
	tr_h.appendChild(td_d);

	cntrl_tblSchedule.appendChild(tr_h);

	save();
}

function td_delete_click(EmpId, tr)
{
	// Delete entire row (employee) from ScheduleGrid, any unsaved data for this employee will be lost,
	// if the employee added again the original data will appear.
	var conf = false;
	if (!Saved)
		conf = confirm('You have unsaved data that will be deleted, do you want to continue?');
	else
		conf = true;

	if (conf) {
		scheduleGrid_RemoveEmp(EmpId, tr);
		FillAutoCompleteList();
	}
}

function scheduleGrid_RemoveEmp(EmpId, tr)
{

	var CurrentEmp = jsonPath(employeesJSON, "$[?(@.EmpId == " + EmpId + ")]");
	if (!!CurrentEmp) {

		var len = tr.childNodes.length
		for (var i = 0; i < len; i++) {
			tr.removeChild(tr.childNodes.item(0));
		}

		cntrl_tblSchedule.removeChild(tr);
		tr = null; // keep this to free allocated memory.
		CurrentEmp[0].listed = "false";

		var originalScheduleJSON = eval(xhr.responseText)[0][0].schedule;

		var EmpScheduleList = jsonPath(scheduleJSON, "$[?(@.EmpId == " + EmpId + ")]");
		for (var i = 0; i < EmpScheduleList.length; i++) {
			scheduleJSON.splice(scheduleJSON.indexOf(EmpScheduleList[i]), 1); // splice means remove
		}

		EmpScheduleList = jsonPath(originalScheduleJSON, "$[?(@.EmpId == " + EmpId + ")]");

		for (var i = 0; i < EmpScheduleList.length; i++) {
			scheduleJSON.push(EmpScheduleList[i]);
		}
	}
}
function scheduleGrid_AddEmp(EmpId)
{
	if (ddlWorkSchedule_selectedIndex == 0) return;

	var CurrentEmp = jsonPath(employeesJSON, "$[?(@.EmpId == " + EmpId + ")]");
	if (!!CurrentEmp) {
		if (CurrentEmp[0].listed == "true") return;
		var tr_b = document.createElement("tr");

		var td_b1 = document.createElement("td");
		td_b1.innerHTML = CurrentEmp[0].EmpNo; //td_b1.innerText = CurrentEmp[0].EmpNo;
		tr_b.appendChild(td_b1);

		var td_b2 = document.createElement("td");
		td_b2.innerHTML = CurrentEmp[0].EmpName; //td_b2.innerText = CurrentEmp[0].EmpName;
		tr_b.appendChild(td_b2);
		for (var j = 1; j <= new Date(year, month, 0).getDate(); j++) {
			var td_b = document.createElement("td");
			td_b.style.backgroundColor = '#FFFFFF';
			td_b.setAttribute('onclick', 'ChangeShift(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this);');
			td_b.setAttribute('oncontextmenu', 'RemoveSchedule(' + year + ',' + month + ',' + j + ',' + CurrentEmp[0].EmpId + ',this); return false;');
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

function scheduleGrid_AddAllEmp()
{
	if (employeesJSON) {
		var lst = jsonPath(employeesJSON, "$[?(@.listed == \"false\")]");
		if (lst) {
			for (var i = 0; i < lst.length; i++) {
				if (lst[i])
					scheduleGrid_AddEmp(lst[i].EmpId);
			}
			FillAutoCompleteList();
		}
	}
}

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
