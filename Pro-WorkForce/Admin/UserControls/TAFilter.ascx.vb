Imports TA.Employees
Imports System.Data
Imports TA.Lookup
Imports TA.Admin
Imports Telerik.Web.UI
Imports SmartV.UTILITIES

Partial Class Admin_UserControls_TAFilter
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objOrgLevel As New OrgLevel
    Private objOrgCompany As New OrgCompany
    Private objOrgEntity As New OrgEntity
    Dim arrRowLevel1Entities As DataRow()
    Private objEmployee As Employee
    Private objVersion As SmartV.Version.version
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

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

#Region "Page_Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        ElseIf SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

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

#Region "CreateCompaniesRow"

    Private Sub CreateCompaniesRow(ByVal strLevelName As String)
        TableSource = New DataTable
        Dim LevelName As New DataColumn("LevelName", System.Type.GetType("System.String"))
        TableSource.Columns.Add(LevelName)
    End Sub

#End Region

#Region "FillCompanies"

    Private Sub FillCompanies(ByVal ddlHeirarchy As RadComboBox)
        Dim dt As New DataTable
        Dim objOrgcompany As New OrgCompany
        dt = objOrgcompany.GetAllforddl
        CtlCommon.FillTelerikDropDownList(ddlHeirarchy, dt, Lang)
    End Sub

#End Region

#Region "ddlCompanies_SelectedIndexChanged"

    Protected Sub ddlCompanies_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanies.SelectedIndexChanged
        Dim dt As New DataTable
        DropDownSource = New ArrayList
        DropDownSelectedValues = New ArrayList
        DropDownSelectedValues.Add(ddlCompanies.SelectedValue)
        CreateCompaniesRow("Companies")
        gvEmplotyeeFilter.DataSource = dt
        gvEmplotyeeFilter.DataBind()



        CompanyId = ddlCompanies.SelectedValue
        objOrgLevel.FK_CompanyId = ddlCompanies.SelectedValue
        dtAllLevels = objOrgLevel.GetAllByComapany

        objOrgEntity.FK_CompanyId = CompanyId
        dtAllEntities = objOrgEntity.GetAllEntityByCompany
        dtLevelentity = dtAllEntities.Clone
        'DeleteNextRows(ddlCompanies.Items.Count, dtAllEntities.Rows.Count, dtAllLevels.Rows.Count, -1)

        FillEmployee(-1)
        If (dtAllEntities IsNot Nothing And dtAllEntities.Rows.Count > 0) Then

            'DeleteNextRows(ddlCompanies.Items.Count, dtAllEntities.Rows.Count, 4, -1)

            arrRowLevel1Entities = dtAllEntities.Select("FK_LevelId = 1")
            If (arrRowLevel1Entities IsNot Nothing) Then
                For Each drEntity As DataRow In arrRowLevel1Entities
                    dtLevelentity.ImportRow(drEntity)
                Next

                Dim LevelsNumber As Integer
                LevelsNumber = CInt(dtAllEntities.Rows(dtAllEntities.Rows.Count - 1)("FK_LevelId"))
                ' Adding next row data source and selected values to DropDownSource ArrayList
                DropDownSource.Add(dtLevelentity)
                DropDownSelectedValues.Add(ddlCompanies.SelectedValue)
                For Index As Integer = 0 To dtAllLevels.Rows.Count - 1
                    Dim drNewRow As DataRow
                    drNewRow = dtAllLevels.Select("LevelId = " & Index + 1)(0)
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

#End Region

#Region "FillEmployee"

    Private Sub FillEmployee(ByVal FK_EntityId As Integer)
        If ddlCompanies.SelectedValue <> -1 And ddlCompanies.SelectedValue <> -1 Then
            Dim objEmployee As New Employee
            objEmployee.FK_CompanyId = ddlCompanies.SelectedValue
            objEmployee.FK_EntityId = FK_EntityId
            Dim dtEmployees As DataTable = objEmployee.GetEmpByCompany
            CtlCommon.FillTelerikDropDownList(ddlEmployees, dtEmployees, Lang)
        End If
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

            CtlCommon.FillTelerikDropDownList(DropDownList, DropDownSource(row.RowIndex), Lang)
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

#Region "ddlLevel_SelectedIndexChanged"

    Protected Sub ddlLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ddlCurrentSelected As RadComboBox = DirectCast(sender, RadComboBox)
        Dim gvRow As GridViewRow = DirectCast(ddlCurrentSelected.Parent.Parent, GridViewRow)

        Dim DropSownSourceItemsCount As Integer = DropDownSource.Count
        Dim TableSourceItemsCount As Integer = TableSource.Rows.Count
        Dim SelectedValuesItemsCount As Integer = DropDownSelectedValues.Count

        ddlCurrentSelected.Items.Clear()
        DeleteNextRows(DropSownSourceItemsCount, TableSourceItemsCount, SelectedValuesItemsCount, gvRow.RowIndex)

        Dim LevelIndex As Integer = gvRow.RowIndex + 2 'Always level index equals to rowIndex + 2
        Dim drEntitiesArray As DataRow() = dtAllEntities.Select("FK_ParentId = " & ddlCurrentSelected.SelectedValue & "AND FK_LevelId = " & LevelIndex)
        dtLevelentity.Rows.Clear()

        FillEmployee(ddlCurrentSelected.SelectedValue)

        For Each drEntity As DataRow In drEntitiesArray
            dtLevelentity.ImportRow(drEntity)
        Next

        If (ddlCurrentSelected.SelectedValue <> -1) Then
            DropDownSelectedValues.Add(ddlCurrentSelected.SelectedValue)
        End If

        If (drEntitiesArray.Length = 0) Then
            RetrieveGridData()
            Return
        End If

        DropDownSource(LevelIndex - 1) = dtLevelentity
        DropDownSelectedValues = New ArrayList()
        DropDownSelectedValues.Add(ddlCompanies.SelectedValue)

        For Each rowCurrent As GridViewRow In gvEmplotyeeFilter.Rows
            ' save current selected dropdown values
            Dim currentDropDown As RadComboBox = rowCurrent.FindControl("ddlHeirarchy")
            If (currentDropDown.SelectedValue <> String.Empty AndAlso currentDropDown.SelectedValue <> -1) Then
                DropDownSelectedValues.Add(currentDropDown.SelectedValue)
            End If
        Next
        RetrieveGridData()
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

#Region "GetEmployeez"

    Public Function GetEmployeez(ByRef CompanyId As Integer, ByRef CompanyName As String, ByRef EntitiesIdz As Integer(), ByRef EntitiesNames As String()) As Integer()
        ' Get Company ID, Name, EntitiesIdz, EntitiesNames
        If (gvEmplotyeeFilter.Rows.Count > 0) Then

            If (DropDownSelectedValues.Count > 0) Then
                EntitiesIdz = New Integer(DropDownSelectedValues.Count) {}
                EntitiesNames = New String(DropDownSelectedValues.Count) {}
            Else
                EntitiesIdz = New Integer() {}
                EntitiesNames = New String() {}
            End If

            For Index As Integer = 0 To DropDownSelectedValues.Count - 1
                EntitiesIdz(Index) = DropDownSelectedValues(Index)
                EntitiesNames(Index) = DropDownSelectedValues(Index)
            Next

        Else
            EntitiesIdz = New Integer() {}
            EntitiesNames = New String() {}
        End If


        ' Company ID and Name
        CompanyId = DropDownSelectedValues(0) 'same as ddlCompanies.SelectedValue
        CompanyName = ddlCompanies.Text

        objEmployee = New Employee()
        objEmployee.FK_CompanyId = CompanyId
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
                    DropDownSource(Item - 1) = Nothing
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

End Class
