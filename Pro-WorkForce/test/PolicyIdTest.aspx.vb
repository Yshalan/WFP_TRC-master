Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Lookup
Imports TA.DailyTasks
Imports System.Data
Imports Telerik.Web.UI

Partial Class test_PolicyIdTest
    Inherits System.Web.UI.Page
#Region "Class Variables"
    ' Instances will be used to fill combo boxes
    Private objEmpStatus As Emp_Status
    Private objOrgEntity As OrgEntity
    Private objEmpNationality As Emp_Nationality
    Private objEmpReligion As Emp_Religion
    Private objEmpMaritalStatus As EmpMaritalStatus
    Private objEmpWorkLocation As Emp_WorkLocation
    Private objEmpGrade As Emp_Grade
    Private objEmpDesignation As Emp_Designation
    Private objEmpLogicalGroup As Emp_logicalGroup
    Private objProjectCommon As New ProjectCommon
    Private objEmployee As Employee
    Private objOrgCompany As OrgCompany
    Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum
    ' Shared variables of main Gridview
    Shared SortDir As String
    Shared SortExep As String
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    ' shared variables of Ta policy grid
    Shared dtTaPolicyCurrent As DataTable
    Private objTAPolicy As TAPolicy
    Private objEmployeeTAPolicy As New Employee_TAPolicy


    Private ds As DataSet

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.IsPostBack() <> True Then

            FillLists()
        End If


    End Sub




    Private Sub FillLists()

        ds = New DataSet()



        objProjectCommon = New ProjectCommon()

        ' Fill the combo boxes without using the ctlCommon 
        ' standard function
        Dim dt As New DataTable("OrgCompany")
        objOrgCompany = New OrgCompany()
        dt = objOrgCompany.GetAll()
        objProjectCommon.FillMultiLevelRadComboBox(RadCmbOrgCompany, dt, _
                                                   "CompanyId", "CompanyName", _
                                                   "CompanyArabicName", "FK_ParentId")

        ds.Tables.Add(dt)

        dt = New DataTable("Emp_WorkLocation")
        objEmpWorkLocation = New Emp_WorkLocation()
        dt = objEmpWorkLocation.GetAll()
        ProjectCommon.FillRadComboBox(RadCmbWorkLocation, dt, _
                                      "WorkLocationName", "WorkLocationArabicName", _
                                      "WorkLocationId")
        ds.Tables.Add(dt)


        dt = New DataTable("Emp_logicalGroup")
        objEmpLogicalGroup = New Emp_logicalGroup()
        dt = objEmpLogicalGroup.GetAll()

        ProjectCommon.FillRadComboBox(RadCmbLogicalGroup, dt, _
                                      "GroupName", "GroupArabicName", "GroupId")



        Cache("MyCach") = ds


    End Sub

    Protected Sub RadCmbOrgCompany_SelectedIndexChanged(ByVal o As Object, ByVal e As RadComboBoxSelectedIndexChangedEventArgs)


        If RadCmbOrgCompany.SelectedValue <> -1 Then
            objOrgEntity = New OrgEntity()
            objProjectCommon = New ProjectCommon()

            ' CompanyId is the foreign key .And so expected a datatable 
            '  to be binded to the combo box
            objOrgEntity.FK_CompanyId = RadCmbOrgCompany.SelectedValue
            Dim dt As DataTable = objOrgEntity.GetAll()

            If dt.Rows.Count > 0 Then

                objProjectCommon.FillMultiLevelRadComboBox(RadCmbEntity, dt, "EntityId", _
                                                           "EntityName", "EntityArabicName", "FK_ParentId")



                ds = Cache("MyCach")
                ds.Tables.Add(dt)
                Cache("MyCach") = ds

            Else
                RadCmbEntity.Items.Clear()
                RadCmbEntity.Text = String.Empty
                RadCmbEntity.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))


            End If


        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetPolicyId(RadCmbLogicalGroup.SelectedValue, _
                    RadCmbWorkLocation.SelectedValue, _
                    RadCmbOrgCompany.SelectedValue, _
                    RadCmbEntity.SelectedValue)
    End Sub

    Private Function GetPolicyId(ByVal LogicalGroupId As Integer, _
                               ByVal WorkLocationId As Integer, _
                               ByVal CompanyId As Integer, _
                               ByVal EntityId As Integer) As Integer
        Dim intTaPolicyId As Integer = -1
        Dim ValueIsSet As Boolean = False
        If LogicalGroupId > 0 Then
            objEmpLogicalGroup = New Emp_logicalGroup()
            objEmpLogicalGroup.GroupId = RadCmbLogicalGroup.SelectedValue
            objEmpLogicalGroup.GetByPK()
            If objEmpLogicalGroup.FK_TAPolicyId <> 0 Then
                intTaPolicyId = objEmpLogicalGroup.FK_TAPolicyId
                ValueIsSet = True
            End If
        End If
        If Not ValueIsSet Then
            If WorkLocationId > 0 Then
                objEmpWorkLocation = New Emp_WorkLocation()
                objEmpWorkLocation.WorkLocationId = RadCmbWorkLocation.SelectedValue
                objEmpWorkLocation.GetByPK()
                If objEmpWorkLocation.FK_TAPolicyId <> 0 Then
                    intTaPolicyId = objEmpWorkLocation.FK_TAPolicyId
                    ValueIsSet = True
                End If
            End If
        End If

        If Not ValueIsSet Then
            If CompanyId > 0 Then
                objOrgCompany = New OrgCompany()
                objOrgCompany.CompanyId = RadCmbOrgCompany.SelectedValue
                objOrgCompany.GetByPK()
                If objOrgCompany.FK_DefaultPolicyId <> 0 Then
                    intTaPolicyId = objOrgCompany.FK_DefaultPolicyId
                End If
            End If
        End If

        If Not ValueIsSet Then
            If EntityId > 0 Then
                objOrgEntity = New OrgEntity()



                objOrgEntity.EntityId = RadCmbEntity.SelectedValue
                objOrgEntity.GetByPK()
                If objOrgEntity.FK_DefaultPolicyId <> 0 Then
                    intTaPolicyId = objOrgEntity.FK_DefaultPolicyId
                End If

            End If
        End If
        Return intTaPolicyId
    End Function

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        ds = New DataSet()

        Dim obj As OrgCompany
        obj = New OrgCompany()

        Dim dt As DataTable = obj.GetAll()


        ds.Tables.Add(dt)


        Dim key As Integer = RadCmbOrgCompany.SelectedValue

        Dim dr As DataRow = getDataRow(dt, key)

        MsgBox(dr("FK_DefaultPolicyId").ToString())

    End Sub

    Private Function getDataRow(ByVal dt As DataTable, ByVal key As String) As DataRow

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If dr(0) = key Then
                    Return (dr)
                End If
            Next
        Else
            Dim r As DataRow = Nothing
            Return r
        End If
    End Function
End Class
