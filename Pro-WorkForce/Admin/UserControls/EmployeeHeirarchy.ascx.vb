Imports TA.Admin
Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports System.Data
Imports TA.Lookup
Imports TA.Employees
Imports SmartV.Version

Partial Class Admin_UserControls_EmployeeHeirarchy
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objOrgLevel As New OrgLevel
    Private objOrgCompany As New OrgCompany
    Private objOrgEntity As New OrgEntity
    Dim arrRowLevel1Entities As DataRow()
    Private objEmployee As Employee
    Private objVersion As SmartV.Version.version

#End Region

#Region "Properties"
#Region "PROPERTY :: CompanyId"
    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
    End Property
#End Region
#Region "PROPERTY :: DropDownSource"
    Public Property DropDownSource() As ArrayList
        Get
            Return ViewState("DropDownSource")
        End Get
        Set(ByVal value As ArrayList)
            ViewState("DropDownSource") = value
        End Set
    End Property
#End Region
#Region "PROPERTY :: DropDownSelectedValues"
    Public Property DropDownSelectedValues() As ArrayList
        Get
            Return ViewState("DropDownSelectedValues")
        End Get
        Set(ByVal value As ArrayList)
            ViewState("DropDownSelectedValues") = value
        End Set
    End Property
#End Region
#Region "PROPERTY :: TableSource"
    Public Property TableSource() As DataTable
        Get
            Return ViewState("TableSource")
        End Get
        Set(ByVal value As DataTable)
            ViewState("TableSource") = value
        End Set
    End Property
#End Region
#Region "PROPERTY :: EmployeeeId"
    Public Property EmployeeeId() As Integer
        Get
            Return ViewState("EmployeeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeeId") = value
        End Set
    End Property



    Public Property dtAllEntities() As DataTable
        Get
            Return ViewState("dtAllEntities")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtAllEntities") = value
        End Set
    End Property
    Public Property dtLevelentity() As DataTable
        Get
            Return ViewState("dtLevelentity")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtLevelentity") = value
        End Set
    End Property

    Public Property dtAllLevels() As DataTable
        Get
            Return ViewState("dtAllLevels")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtAllLevels") = value
        End Set
    End Property

#End Region

#End Region

#Region "Handlers"
#Region "Page_Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            DropDownSource = New ArrayList
            DropDownSelectedValues = New ArrayList

            ' Adding frist row to grid view to show companies
            CreateCompaniesRow("Companies")
            FillCompanies(ddlCompanies)
            If (objVersion.HasMultiCompany() = False) Then
                ddlCompanies.SelectedValue = objVersion.GetCompanyId()
                ddlCompanies_SelectedIndexChanged(ddlCompanies, Nothing)
                trCompanies.Visible = False
            End If
        End If
    End Sub
#End Region

    Protected Sub ddlCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanies.SelectedIndexChanged
        CompanyId = ddlCompanies.SelectedValue
        objOrgLevel.FK_CompanyId = ddlCompanies.SelectedValue
        dtAllLevels = objOrgLevel.GetAllByComapany
        dtAllLevels.Columns.Add("Order")

        For Index As Integer = 0 To dtAllLevels.Rows.Count - 1
            dtAllLevels.Rows(Index)("Order") = Index + 1
        Next

        objOrgEntity.FK_CompanyId = CompanyId
        dtAllEntities = objOrgEntity.GetAllEntityByCompany
        dtLevelentity = dtAllEntities.Clone

        FillEmployee()
        If (dtAllEntities IsNot Nothing And dtAllEntities.Rows.Count > 0) Then

            'DeleteNextRows(DropSownSourceItemsCount, TableSourceItemsCount, SelectedValuesItemsCount, -1)

            arrRowLevel1Entities = dtAllEntities.Select("FK_LevelId = 1")
            If (arrRowLevel1Entities IsNot Nothing) Then
                For Each drEntity As DataRow In arrRowLevel1Entities
                    dtLevelentity.ImportRow(drEntity)
                Next

                Dim LevelsNumber As Integer
                LevelsNumber = CInt(dtAllEntities.Rows(dtAllEntities.Rows.Count - 1)("FK_LevelId"))
                ' Adding next row data source to DropDownSource ArrayList
                DropDownSource.Add(dtLevelentity)


                ' Adding this row selected value to DropDownSelectedValues ArrayList
                DropDownSelectedValues.Add(ddlCompanies.SelectedValue)

                'For Index As Integer = 0 To DropDownSource.Count - 1
                '    Dim drNewRow As DataRow
                '    drNewRow = dtAllLevels.Select("Order = " & 0 + 1)(0)
                '    TableSource.Rows.Add(drNewRow("levelName"))
                'Next

                For Index As Integer = 0 To dtAllLevels.Rows.Count - 1
                    Dim drNewRow As DataRow
                    drNewRow = dtAllLevels.Select("Order = " & Index + 1)(0)
                    TableSource.Rows.Add(drNewRow("levelName"))
                    DropDownSource.Add(Nothing)
                Next



                RetrieveGridData()
            End If
        Else
            ddlEmployees.Items.Clear()
            ddlEmployees.DataSource = Nothing
            ddlEmployees.DataBind()
            DropDownSource = New ArrayList
            If (DropDownSelectedValues.Count > 0) Then
                DropDownSelectedValues.RemoveRange(1, DropDownSelectedValues.Count - 1)
                DropDownSelectedValues(0) = ddlCompanies.SelectedValue
            Else
                DropDownSelectedValues.Add(ddlCompanies.SelectedValue)
            End If

            TableSource = New DataTable
            CreateCompaniesRow("Companies")
            RetrieveGridData()
        End If


    End Sub
#Region "ddlTest_SelectedIndexChanged"
    Protected Sub ddlTest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ddlCurrentSelectedDropDown As RadComboBox = DirectCast(sender, RadComboBox)

        Dim DropSownSourceItemsCount As Integer
        Dim TableSourceItemsCount As Integer
        Dim SelectedValuesItemsCount As Integer
        DropSownSourceItemsCount = DropDownSource.Count
        TableSourceItemsCount = TableSource.Rows.Count
        SelectedValuesItemsCount = DropDownSelectedValues.Count

        For Each row As GridViewRow In gvEmplotyeeFilter.Rows
            Dim ctrl As RadComboBox
            ctrl = DirectCast(row.FindControl("ddlHeirarchy"), RadComboBox)


            If (ctrl IsNot Nothing AndAlso ddlCurrentSelectedDropDown.ClientID = ctrl.ClientID) Then
                ddlCurrentSelectedDropDown.Items.Clear()

                'DeleteNextRows(DropSownSourceItemsCount, TableSourceItemsCount, SelectedValuesItemsCount, row.RowIndex)

                Dim currentIndex As Integer
                currentIndex = row.RowIndex + 2
                For Each drEntity As DataRow In dtAllEntities.Select("FK_ParentId = " & ddlCurrentSelectedDropDown.SelectedValue & "AND FK_LevelId = " & currentIndex)
                    dtLevelentity.ImportRow(drEntity)
                Next
                If (dtAllEntities.Select("FK_ParentId = " & ddlCurrentSelectedDropDown.SelectedValue & "AND FK_LevelId = " & currentIndex).Length = 0) Then
                    If (ddlCurrentSelectedDropDown.SelectedValue <> -1) Then
                        DropDownSelectedValues.Add(ddlCurrentSelectedDropDown.SelectedValue)
                    End If

                    RetrieveGridData()
                    Return
                End If
                DropDownSource.Add(dtLevelentity)
                DropDownSelectedValues = New ArrayList()
                DropDownSelectedValues.Add(ddlCompanies.SelectedValue)
                For Each rowCurrent As GridViewRow In gvEmplotyeeFilter.Rows
                    ' save current selected dropdown values
                    Dim currentDropDown As RadComboBox
                    currentDropDown = rowCurrent.FindControl("ddlHeirarchy")
                    If (currentDropDown.Items.Count > 0 AndAlso currentDropDown.SelectedValue <> -1) Then
                        DropDownSelectedValues.Add(currentDropDown.SelectedValue)
                    End If
                Next

                For Index As Integer = 0 To DropDownSource.Count - 1
                    If (Index > row.RowIndex + 1) Then
                        DropDownSource.RemoveAt(Index + 1)
                    End If
                Next
                Dim drNewRow As DataRow
                If (dtAllLevels.Select("Order = " & currentIndex).Length > 0) Then
                    drNewRow = dtAllLevels.Select("Order = " & currentIndex)(0)
                    TableSource.Rows.Add(drNewRow("levelName"))
                Else
                    Return
                End If

                RetrieveGridData()
            End If
        Next

    End Sub
#End Region

#Region "Handler :: ddlEmployees_SelectedIndexChanged"
    Protected Sub ddlEmployees_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEmployees.SelectedIndexChanged
        If (ddlEmployees.SelectedValue <> -1) Then
            EmployeeeId = ddlEmployees.SelectedValue
        Else
            EmployeeeId = 0
        End If
        RetrieveGridData()
    End Sub
#End Region


#End Region

#Region "Methods"

#Region "FillCompanies"
    Private Sub FillCompanies(ByVal ddlHeirarchy As RadComboBox)
        Dim dt As New DataTable
        Dim objOrgcompany As New OrgCompany
        dt = objOrgcompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(ddlHeirarchy, dt, CtlCommon.Lang.EN)
    End Sub
#End Region

#Region "CreateCompaniesRow"
    Private Sub CreateCompaniesRow(ByVal strLevelName As String)
        Dim dtFilterSource As New DataTable
        Dim LevelName As New DataColumn("LevelName", System.Type.GetType("System.String"))
        dtFilterSource.Columns.Add(LevelName)
        TableSource = dtFilterSource.Clone
        TableSource = dtFilterSource
    End Sub
#End Region

#Region "RetrieveGridData"
    Public Sub RetrieveGridData()
        gvEmplotyeeFilter.DataSource = TableSource
        gvEmplotyeeFilter.DataBind()

        FillCompanies(ddlCompanies)
        If (DropDownSelectedValues.Count > 0) Then
            ddlCompanies.SelectedValue = DropDownSelectedValues(0)
        Else
            ddlCompanies.SelectedValue = -1
        End If

        ' retrieve selected dropdown value
        For Each row As GridViewRow In gvEmplotyeeFilter.Rows
            Dim DropDownList As New RadComboBox
            DropDownList = row.FindControl("ddlHeirarchy")
            Dim dtSource As New DataTable
            dtSource = DropDownSource(row.RowIndex)
            If (dtSource IsNot Nothing) Then
                Dim dtRow As New Integer
                dtRow = 0
                For dtRow = 0 To dtSource.Rows.Count - 1
                    If (dtSource.Rows.Count > dtRow) Then
                        If (dtSource.Rows(dtRow)(0) = -1) Then
                            dtSource.Rows.Remove(dtSource(dtRow))
                        End If
                    End If
                Next
            End If

            CtlCommon.FillTelerikDropDownList(DropDownList, DropDownSource(row.RowIndex))
            If (row.RowIndex <> gvEmplotyeeFilter.Rows.Count) Then ' Filling selected dropdown values
                Dim ddlHeirarchy As New RadComboBox
                ddlHeirarchy = row.FindControl("ddlHeirarchy")
                If (row.RowIndex < DropDownSelectedValues.Count - 1) Then
                    ddlHeirarchy.SelectedValue = CType(DropDownSelectedValues(row.RowIndex + 1), Integer)
                End If
            End If
        Next
    End Sub
#End Region

#Region "DropNextRows"
    Private Sub DeleteNextRows(ByVal DropSownSourceItemsCount As Integer, ByVal TableSourceItemsCount As Integer, ByVal SelectedValuesItemsCount As Integer _
                               , ByVal RowIndex As Integer)
        If (RowIndex = -1) Then
            ' Reset and clear gridView
            If (RowIndex < DropDownSource.Count) Then
                DropDownSource = New ArrayList()
                DropDownSelectedValues = New ArrayList()
                For LevelId As Integer = RowIndex + 1 To TableSourceItemsCount - 1
                    TableSource.Rows.RemoveAt(TableSource.Rows.Count - 1)
                Next
            End If
        ElseIf (RowIndex >= 0) Then
            ' delete unneeded dropdowns and their selected values
            If (RowIndex < DropDownSource.Count - 1) Then
                For Item As Integer = RowIndex + 2 To DropSownSourceItemsCount ' SelectedValuesItemsCount = TableSourceItemsCount
                    DropDownSource.RemoveAt(DropDownSource.Count - 1)
                Next
                For LevelId As Integer = RowIndex + 1 To TableSourceItemsCount - 1
                    TableSource.Rows.RemoveAt(TableSource.Rows.Count - 1)
                Next
                For Index As Integer = RowIndex + 2 To SelectedValuesItemsCount
                    DropDownSelectedValues.RemoveAt(DropDownSelectedValues.Count - 1)
                Next
            ElseIf (RowIndex + 1 = DropDownSource.Count And DropDownSelectedValues.Count > RowIndex + 1) Then
                For Index As Integer = RowIndex + 1 To DropDownSelectedValues.Count - 1
                    DropDownSelectedValues.RemoveAt(DropDownSelectedValues.Count - 1)
                Next
            End If
        End If

    End Sub
#End Region

#Region "GetEmployeez"
    Public Function GetEmployeez(ByRef CompanyId As Integer, ByRef CompanyName As String, ByRef EntitiesIdz As Integer(), ByRef EntitiesNames As String()) As Integer()
        ' Get Company ID, Name, EntitiesIdz, EntitiesNames
        If (gvEmplotyeeFilter.Rows.Count > 0) Then
            Dim LastDDL = DirectCast(gvEmplotyeeFilter.Rows(gvEmplotyeeFilter.Rows.Count - 1).FindControl("ddlHeirarchy"), RadComboBox)

            If (LastDDL.SelectedValue <> -1) Then
                EntitiesIdz = New Integer(gvEmplotyeeFilter.Rows.Count - 1) {}
                EntitiesNames = New String(gvEmplotyeeFilter.Rows.Count - 1) {}
            Else
                EntitiesIdz = New Integer(gvEmplotyeeFilter.Rows.Count - 2) {}
                EntitiesNames = New String(gvEmplotyeeFilter.Rows.Count - 2) {}
            End If

            For Each gvRow As GridViewRow In gvEmplotyeeFilter.Rows
                Dim ddlLevel = DirectCast(gvRow.FindControl("ddlHeirarchy"), RadComboBox)
                If (ddlLevel.SelectedValue <> -1) Then
                    EntitiesIdz(gvRow.RowIndex) = ddlLevel.SelectedValue
                    EntitiesNames(gvRow.RowIndex) = ddlLevel.Text
                ElseIf (gvEmplotyeeFilter.Rows.Count = 1) Then
                    EntitiesIdz = New Integer() {}
                    EntitiesNames = New String() {}
                End If
            Next
        Else
            EntitiesIdz = New Integer() {}
            EntitiesNames = New String() {}
        End If


        ' Company ID and Name
        CompanyId = DropDownSelectedValues(0) 'same as ddlCompanies.SelectedValue
        CompanyName = ddlCompanies.Text

        objEmployee = New Employee()
        objEmployee.FK_CompanyId = DropDownSelectedValues(0)
        If (DropDownSelectedValues.Count - 1 > 0) Then
            objEmployee.FK_EntityId = DropDownSelectedValues(DropDownSelectedValues.Count - 1)
        Else
            objEmployee.FK_EntityId = -1
        End If

        Dim dtEmployees As DataTable = New DataTable
        dtEmployees = objEmployee.GetEmployeeIDz()

        If (dtEmployees IsNot Nothing And dtEmployees.Rows.Count > 0) Then

            Dim EmployeeIdz As Integer() = New Integer(dtEmployees.Rows.Count - 1) {}

            For i As Integer = 0 To dtEmployees.Rows.Count - 1
                EmployeeIdz(i) = dtEmployees.Rows(i)("EmployeeId")
            Next
            Return EmployeeIdz
        End If
        Return New Integer() {}
    End Function
#End Region

    Private Sub FillEmployee()
        If ddlCompanies.SelectedValue <> -1 And ddlCompanies.SelectedValue <> -1 Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = ddlCompanies.SelectedValue
            objEmployee.FK_EntityId = -1
            Dim dtEmployees As DataTable = objEmployee.GetEmpByCompany
            CtlCommon.FillTelerikDropDownList(ddlEmployees, dtEmployees)
        End If
    End Sub

#End Region

End Class
