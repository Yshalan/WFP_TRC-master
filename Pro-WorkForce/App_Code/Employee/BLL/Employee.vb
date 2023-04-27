Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Employee

#Region "Class Variables"


        Private _EmployeeId As Long
        Private _EmployeeNo As String
        Private _EmployeeCardNo As String
        Private _FK_Status As Integer
        Private _EmployeeName As String
        Private _EmployeeArabicName As String
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private _EntityHierarchy As String
        Private _Gender As String
        Private _DOB As DateTime
        Private _Email As String
        Private _FK_Nationality As Integer
        Private _FK_Religion As Integer
        Private _FK_MaritalStatus As Integer
        Private _FK_WorkLocation As Integer
        Private _FK_Grade As Integer
        Private _FK_Designation As Integer
        Private _FK_LogicalGroup As Integer
        Private _AnnualLeaveBalance As Double
        Private _JoinDate As DateTime
        Private _IsTerminated As Boolean
        Private _TerminateDate As DateTime
        Private _Remarks As String
        Private _EmpImagePath As String
        Private _NationalId As String
        Private _MobileNo As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALEmployee As DALEmployee
        Private _DashDate As DateTime
        Private _PrintStatus As Boolean
        Private _FilterType As String = "C"
        Private _Type As Integer
        Private _Value As String
        Private _Oper As String
        Private _GT_VerfiedBY As String
        Private _Status As Integer
        Private _PIN As String
        Private _PayRollNumber As String
        Private _FK_EmployeeTypeId As Integer
        Private _ContractEndDate As DateTime
        Private _ExternalPartyName As String
        Private _InitialIndex As Integer
        Private _EnRoll_ID As String
        Private _FK_ManagerID As Integer
        Private _PageNo As Integer
        Private _PageSize As Integer
        Private _Emp_RecordCount As Integer
        Private _UserId As Integer
        Private _Fk_CardType As Integer

#End Region

#Region "Public Properties"
        
        Public Property EmployeeId() As Long
            Set(ByVal value As Long)
                _EmployeeId = value
            End Set
            Get
                Return (_EmployeeId)
            End Get
        End Property
        Public Property Fk_CardType() As Integer
            Set(ByVal value As Integer)
                _Fk_CardType = value
            End Set
            Get
                Return (_Fk_CardType)
            End Get
        End Property
        Public Property EmployeeNo() As String
            Set(ByVal value As String)
                _EmployeeNo = value
            End Set
            Get
                Return (_EmployeeNo)
            End Get
        End Property
        Public Property EmployeeCardNo() As String
            Set(ByVal value As String)
                _EmployeeCardNo = value
            End Set
            Get
                Return (_EmployeeCardNo)
            End Get
        End Property
        Public Property FK_Status() As Integer
            Set(ByVal value As Integer)
                _FK_Status = value
            End Set
            Get
                Return (_FK_Status)
            End Get
        End Property
        Public Property EmployeeName() As String
            Set(ByVal value As String)
                _EmployeeName = value
            End Set
            Get
                Return (_EmployeeName)
            End Get
        End Property
        Public Property EmployeeArabicName() As String
            Set(ByVal value As String)
                _EmployeeArabicName = value
            End Set
            Get
                Return (_EmployeeArabicName)
            End Get
        End Property
        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property
        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property
        Public Property EntityHierarchy() As String
            Set(ByVal value As String)
                _EntityHierarchy = value
            End Set
            Get
                Return (_EntityHierarchy)
            End Get
        End Property
        Public Property Gender() As String
            Set(ByVal value As String)
                _Gender = value
            End Set
            Get
                Return (_Gender)
            End Get
        End Property
        Public Property DOB() As DateTime
            Set(ByVal value As DateTime)
                _DOB = value
            End Set
            Get
                Return (_DOB)
            End Get
        End Property
        Public Property Email() As String
            Set(ByVal value As String)
                _Email = value
            End Set
            Get
                Return (_Email)
            End Get
        End Property
        Public Property FK_Nationality() As Integer
            Set(ByVal value As Integer)
                _FK_Nationality = value
            End Set
            Get
                Return (_FK_Nationality)
            End Get
        End Property
        Public Property FK_Religion() As Integer
            Set(ByVal value As Integer)
                _FK_Religion = value
            End Set
            Get
                Return (_FK_Religion)
            End Get
        End Property
        Public Property FK_MaritalStatus() As Integer
            Set(ByVal value As Integer)
                _FK_MaritalStatus = value
            End Set
            Get
                Return (_FK_MaritalStatus)
            End Get
        End Property
        Public Property FK_WorkLocation() As Integer
            Set(ByVal value As Integer)
                _FK_WorkLocation = value
            End Set
            Get
                Return (_FK_WorkLocation)
            End Get
        End Property
        Public Property FK_Grade() As Integer
            Set(ByVal value As Integer)
                _FK_Grade = value
            End Set
            Get
                Return (_FK_Grade)
            End Get
        End Property
        Public Property FK_Designation() As Integer
            Set(ByVal value As Integer)
                _FK_Designation = value
            End Set
            Get
                Return (_FK_Designation)
            End Get
        End Property
        Public Property FK_LogicalGroup() As Integer
            Set(ByVal value As Integer)
                _FK_LogicalGroup = value
            End Set
            Get
                Return (_FK_LogicalGroup)
            End Get
        End Property
        Public Property AnnualLeaveBalance() As Double
            Set(ByVal value As Double)
                _AnnualLeaveBalance = value
            End Set
            Get
                Return (_AnnualLeaveBalance)
            End Get
        End Property
        Public Property JoinDate() As DateTime
            Set(ByVal value As DateTime)
                _JoinDate = value
            End Set
            Get
                Return (_JoinDate)
            End Get
        End Property
        Public Property IsTerminated() As Boolean
            Set(ByVal value As Boolean)
                _IsTerminated = value
            End Set
            Get
                Return (_IsTerminated)
            End Get
        End Property
        Public Property TerminateDate() As DateTime
            Set(ByVal value As DateTime)
                _TerminateDate = value
            End Set
            Get
                Return (_TerminateDate)
            End Get
        End Property
        Public Property EmpImagePath() As String
            Set(ByVal value As String)
                _EmpImagePath = value
            End Set
            Get
                Return (_EmpImagePath)
            End Get
        End Property
        Public Property NationalId() As String
            Set(ByVal value As String)
                _NationalId = value
            End Set
            Get
                Return (_NationalId)
            End Get
        End Property
        Public Property MobileNo() As String
            Set(ByVal value As String)
                _MobileNo = value
            End Set
            Get
                Return (_MobileNo)
            End Get
        End Property
        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property
        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property
        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property
        Public Property DashDate() As DateTime
            Set(ByVal value As DateTime)
                _DashDate = value
            End Set
            Get
                Return (_DashDate)
            End Get
        End Property
        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property
        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property
        Public Property PrintStatus() As Boolean
            Set(ByVal value As Boolean)
                _PrintStatus = value
            End Set
            Get
                Return (_PrintStatus)
            End Get
        End Property
        Public Property FilterType() As String
            Set(ByVal value As String)
                _FilterType = value
            End Set
            Get
                Return (_FilterType)
            End Get
        End Property
        Public Property GT_VERIFYBY As String
            Get
                Return _GT_VerfiedBY
            End Get
            Set(ByVal value As String)
                _GT_VerfiedBY = value
            End Set
        End Property
        Public Property Status() As Integer
            Set(ByVal value As Integer)
                _Status = value
            End Set
            Get
                Return (_Status)
            End Get
        End Property
        Public Property Pin() As String
            Set(ByVal value As String)
                _PIN = value
            End Set
            Get
                Return (_PIN)
            End Get
        End Property
        Public Property PayRollNumber() As String
            Set(ByVal value As String)
                _PayRollNumber = value
            End Set
            Get
                Return (_PayRollNumber)
            End Get
        End Property
        Public Property FK_EmployeeTypeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeTypeId = value
            End Set
            Get
                Return (_FK_EmployeeTypeId)
            End Get
        End Property
        Public Property ContractEndDate() As DateTime
            Set(ByVal value As DateTime)
                _ContractEndDate = value
            End Set
            Get
                Return (_ContractEndDate)
            End Get
        End Property
        Public Property ExternalPartyName() As String
            Set(ByVal value As String)
                _ExternalPartyName = value
            End Set
            Get
                Return (_ExternalPartyName)
            End Get
        End Property

        Public Property InitialIndex() As Long
            Set(ByVal value As Long)
                _InitialIndex = value
            End Set
            Get
                Return (_InitialIndex)
            End Get
        End Property

        Public Property EnRoll_ID() As String
            Set(ByVal value As String)
                _EnRoll_ID = value
            End Set
            Get
                Return (_EnRoll_ID)
            End Get
        End Property
        Public Property FK_ManagerID() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerID = value
            End Set
            Get
                Return (_FK_ManagerID)
            End Get
        End Property
        Public Property PageNo() As Integer
            Set(ByVal value As Integer)
                _PageNo = value
            End Set
            Get
                Return (_PageNo)
            End Get
        End Property
        Public Property PageSize() As Integer
            Set(ByVal value As Integer)
                _PageSize = value
            End Set
            Get
                Return (_PageSize)
            End Get
        End Property
        Public Property Emp_RecordCount() As Integer
            Set(ByVal value As Integer)
                _Emp_RecordCount = value
            End Set
            Get
                Return (_Emp_RecordCount)
            End Get
        End Property
        Public Property UserId() As Integer
            Set(ByVal value As Integer)
                _UserId = value
            End Set
            Get
                Return (_UserId)
            End Get
        End Property
#Region "SearchMethod"
        Public Property Type() As Integer
            Set(ByVal value As Integer)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property
        Public Property Value() As String
            Set(ByVal value As String)
                _Value = value
            End Set
            Get
                Return (_Value)
            End Get
        End Property
        Public Property Oper() As String
            Set(ByVal value As String)
                _Oper = value
            End Set
            Get
                Return (_Oper)
            End Get
        End Property
#End Region
#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmployee = New DALEmployee()

        End Sub

#End Region

#Region "Methods"

        Public Function Get_EmployeeRecordCount() As Employee

            Dim dr As DataRow
            dr = objDALEmployee.Get_EmployeeRecordCount(_EmployeeId, _FK_EntityId, _FK_CompanyId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("Emp_RecordCount")) Then
                    _Emp_RecordCount = dr("Emp_RecordCount")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function ChangeEntity() As Integer
            Return objDALEmployee.ChangeEntity(_EmployeeId, _FK_EntityId, _LAST_UPDATE_BY)
        End Function

        Public Function Add() As Integer






            Dim rslt As Integer = objDALEmployee.Add(_EmployeeId, _EmployeeNo, _EmployeeCardNo, _FK_Status, _
                                      _EmployeeName, _EmployeeArabicName, _
                                      _FK_CompanyId, _FK_EntityId, _EntityHierarchy, _
                                      _Gender, _DOB, _Email, _FK_Nationality, _
                                      _FK_Religion, _
                                      _FK_MaritalStatus, _
                                      _FK_WorkLocation, _
                                      _FK_Grade, _
                                      _FK_Designation, _
                                      _FK_LogicalGroup, _
                                      _AnnualLeaveBalance, _
                                      _JoinDate, _
                                      _IsTerminated, _
                                       _TerminateDate, _
                                      _Remarks, _EmpImagePath,
                                      _NationalId, _MobileNo,
                                      _CREATED_BY, _
                                      _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE, _PayRollNumber, _FK_EmployeeTypeId, _ContractEndDate, _ExternalPartyName, _GT_VerfiedBY, _PIN)
            App_EventsLog.Insert_ToEventLog("Add", _EmployeeId, "Employee", "Employee")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmployee.Update(_EmployeeId, _EmployeeNo, _EmployeeCardNo, _FK_Status, _
                                                  _EmployeeName, _EmployeeArabicName, _
                                                  _FK_CompanyId, _FK_EntityId, _EntityHierarchy, _
                                                  _Gender, _DOB, _Email, _FK_Nationality, _
                                                  _FK_Religion, _
                                                  _FK_MaritalStatus, _
                                                   _FK_WorkLocation, _
                                                  _FK_Grade, _
                                                  _FK_Designation, _
                                                  _FK_LogicalGroup, _
                                                  _AnnualLeaveBalance, _
                                                   _JoinDate, _
                                                  _IsTerminated, _
                                                   _TerminateDate, _
                                                  _Remarks, _EmpImagePath,
                                                  _NationalId, _MobileNo,
                                                  _LAST_UPDATE_BY, _GT_VerfiedBY, _PIN, _PayRollNumber, _FK_EmployeeTypeId, _ContractEndDate, _ExternalPartyName)
            App_EventsLog.Insert_ToEventLog("Edit", _EmployeeId, "Employee", "Employee")
            Return rslt

        End Function

        Public Function Update_EmpNo() As Integer

            Dim rslt As Integer = objDALEmployee.Update_EmpNo(_EmployeeId, _EmployeeNo)
            App_EventsLog.Insert_ToEventLog("Edit", _EmployeeId, "Employee", "Employee")
            Return rslt

        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmployee.Delete(_EmployeeId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmployeeId, "Employee", "Employee")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmployee.GetAll()

        End Function

        Public Function GetActive_EmployeeCount() As DataTable

            Return objDALEmployee.GetActive_EmployeeCount(_FK_CompanyId)

        End Function

        Public Function Get_Inner_Summary() As DataTable

            Return objDALEmployee.Get_Inner_Summary(_UserId)

        End Function

        Public Function Get_EmployeeIdByEmail() As DataTable

            Return objDALEmployee.Get_EmployeeIdByEmail(_Email)

        End Function

        Public Function GetCountEmployeesAndUsers() As DataTable

            Return objDALEmployee.GetCountEmployeesAndUsers()

        End Function

        Public Function GettEmployee_EntityCount() As DataTable

            Return objDALEmployee.GettEmployee_EntityCount(_FK_CompanyId)

        End Function

        Public Function GetEmpByCompany() As DataTable

            Return objDALEmployee.GetEmpByCompany(_FK_CompanyId, _FK_EntityId, _FK_LogicalGroup, _FK_WorkLocation, _FilterType, _PageNo, _PageSize)

        End Function

        Public Function GetEmpByStatus() As DataTable

            Return objDALEmployee.GetEmpByStatus(_FK_CompanyId, _FK_EntityId, _FK_LogicalGroup, _FK_WorkLocation, _FilterType, _Status, _PageNo, _PageSize)

        End Function

        Public Function GetManagersByCompany() As DataTable

            Return objDALEmployee.GetManagersByCompany(_FK_CompanyId, _FK_EntityId, _FilterType)

        End Function

        Public Function GetEmpByCompany_ByPrintQueue() As DataTable

            Return objDALEmployee.GetEmpByCompany_ByPrintQueue(_FK_CompanyId, _FK_EntityId, _PrintStatus, _PageNo, _PageSize)

        End Function
        Public Function GetEmpByCompanyDesig_ByPrintQueue() As DataTable

            Return objDALEmployee.GetEmpByCompanyDesig_ByPrintQueue(_FK_CompanyId, _FK_Designation, _PrintStatus, _Fk_CardType)

        End Function

        Public Function GetAllEmpSchedule() As DataTable

            Return objDALEmployee.GetAllEmpSchedule(_EmployeeNo)

        End Function

        Public Function GetAllforDDL() As DataTable

            Return objDALEmployee.GetAllforDDL()

        End Function

        Public Function GetEmpByCompEnt() As DataTable
            Return objDALEmployee.GetEmpByCompEnt(FK_CompanyId, FK_EntityId)
        End Function

        Public Function GetAllScheduleByCompany() As DataTable

            Return objDALEmployee.GetAllScheduleByCompany(_FK_CompanyId, _FK_EntityId)

        End Function

        Public Function GetByPK() As Employee

            Dim dr As DataRow
            dr = objDALEmployee.GetByPK(_EmployeeId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("EmployeeId")) Then
                    _EmployeeId = dr("EmployeeId")
                End If
                If Not IsDBNull(dr("EmployeeNo")) Then
                    _EmployeeNo = dr("EmployeeNo")
                End If
                If Not IsDBNull(dr("EmployeeCardNo")) Then
                    _EmployeeCardNo = dr("EmployeeCardNo")
                End If
                If Not IsDBNull(dr("FK_Status")) Then
                    _FK_Status = dr("FK_Status")
                End If
                If Not IsDBNull(dr("EmployeeName")) Then
                    _EmployeeName = dr("EmployeeName")
                End If
                If Not IsDBNull(dr("EmployeeArabicName")) Then
                    _EmployeeArabicName = dr("EmployeeArabicName")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("FK_EntityId")) Then
                    _FK_EntityId = dr("FK_EntityId")
                End If
                If Not IsDBNull(dr("EntityHierarchy")) Then
                    _EntityHierarchy = dr("EntityHierarchy")
                End If
                If Not IsDBNull(dr("Gender")) Then
                    _Gender = dr("Gender")
                End If
                If Not IsDBNull(dr("DOB")) Then
                    _DOB = dr("DOB")
                End If
                If Not IsDBNull(dr("Email")) Then
                    _Email = dr("Email")
                End If
                If Not IsDBNull(dr("FK_Nationality")) Then
                    _FK_Nationality = dr("FK_Nationality")
                End If
                If Not IsDBNull(dr("FK_Religion")) Then
                    _FK_Religion = dr("FK_Religion")
                End If
                If Not IsDBNull(dr("FK_MaritalStatus")) Then
                    _FK_MaritalStatus = dr("FK_MaritalStatus")
                End If
                If Not IsDBNull(dr("FK_WorkLocation")) Then
                    _FK_WorkLocation = dr("FK_WorkLocation")
                End If
                If Not IsDBNull(dr("FK_Grade")) Then
                    _FK_Grade = dr("FK_Grade")
                End If
                If Not IsDBNull(dr("FK_Designation")) Then
                    _FK_Designation = dr("FK_Designation")
                End If
                If Not IsDBNull(dr("FK_LogicalGroup")) Then
                    _FK_LogicalGroup = dr("FK_LogicalGroup")
                End If
                If Not IsDBNull(dr("AnnualLeaveBalance")) Then
                    _AnnualLeaveBalance = dr("AnnualLeaveBalance")
                End If
                If Not IsDBNull(dr("JoinDate")) Then
                    _JoinDate = dr("JoinDate")
                End If
                If Not IsDBNull(dr("IsTerminated")) Then
                    _IsTerminated = dr("IsTerminated")
                End If
                If Not IsDBNull(dr("TerminateDate")) Then
                    _TerminateDate = dr("TerminateDate")
                End If
                If Not IsDBNull(dr("Remarks")) Then
                    _Remarks = dr("Remarks")
                End If
                If Not IsDBNull(dr("EmpImagePath")) Then
                    _EmpImagePath = dr("EmpImagePath")
                End If
                If Not IsDBNull(dr("NationalId")) Then
                    _NationalId = dr("NationalId")
                End If
                If Not IsDBNull(dr("MobileNo")) Then
                    _MobileNo = dr("MobileNo")
                End If
                If Not IsDBNull(dr("CREATED_BY")) Then
                    _CREATED_BY = dr("CREATED_BY")
                End If
                If Not IsDBNull(dr("CREATED_DATE")) Then
                    _CREATED_DATE = dr("CREATED_DATE")
                End If
                If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                    _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
                End If
                If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                    _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
                End If
                If dr.Table.Columns.Contains("GT_VERIFYBY") Then
                    If Not IsDBNull(dr("GT_VERIFYBY")) Then
                        GT_VERIFYBY = dr("GT_VERIFYBY")
                    End If
                End If
                If Not IsDBNull(dr("PIN")) Then
                    _PIN = dr("PIN")
                End If
                If Not IsDBNull(dr("PayRollNumber")) Then
                    _PayRollNumber = dr("PayRollNumber")
                End If
                If Not IsDBNull(dr("FK_EmployeeTypeId")) Then
                    _FK_EmployeeTypeId = dr("FK_EmployeeTypeId")
                End If
                If Not IsDBNull(dr("ContractEndDate")) Then
                    _ContractEndDate = dr("ContractEndDate")
                End If
                If Not IsDBNull(dr("ExternalPartyName")) Then
                    _ExternalPartyName = dr("ExternalPartyName")
                End If
                If Not IsDBNull(dr("EnRoll_ID")) Then
                    _EnRoll_ID = dr("EnRoll_ID")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function GetMaxEmpNo() As Employee

            Dim dr As DataRow
            dr = objDALEmployee.GetMaxEmpNo(_InitialIndex, _FK_EmployeeTypeId)
            If Not dr Is Nothing Then

                If Not IsDBNull(dr("EmployeeNo")) Then
                    _EmployeeNo = dr("EmployeeNo")
                End If

            Else
                Return Nothing
            End If
        End Function

        Public Function GetByEmpId() As DataTable

            Dim dr As DataRow
            dr = objDALEmployee.GetByPK(_EmployeeId)

            If Not IsDBNull(dr("EmployeeId")) Then
                _EmployeeId = dr("EmployeeId")
            End If
            If Not IsDBNull(dr("EmployeeNo")) Then
                _EmployeeNo = dr("EmployeeNo")
            End If
            If Not IsDBNull(dr("FK_Status")) Then
                _FK_Status = dr("FK_Status")
            End If
            If Not IsDBNull(dr("EmployeeName")) Then
                _EmployeeName = dr("EmployeeName")
            End If
            If Not IsDBNull(dr("EmployeeArabicName")) Then
                _EmployeeArabicName = dr("EmployeeArabicName")
            End If
            If Not IsDBNull(dr("FK_CompanyId")) Then
                _FK_CompanyId = dr("FK_CompanyId")
            End If
            If Not IsDBNull(dr("FK_EntityId")) Then
                _FK_EntityId = dr("FK_EntityId")
            End If
            If Not IsDBNull(dr("EntityHierarchy")) Then
                _EntityHierarchy = dr("EntityHierarchy")
            End If
            If Not IsDBNull(dr("Gender")) Then
                _Gender = dr("Gender")
            End If
            If Not IsDBNull(dr("DOB")) Then
                _DOB = dr("DOB")
            End If
            If Not IsDBNull(dr("Email")) Then
                _Email = dr("Email")
            End If
            If Not IsDBNull(dr("FK_Nationality")) Then
                _FK_Nationality = dr("FK_Nationality")
            End If
            If Not IsDBNull(dr("FK_Religion")) Then
                _FK_Religion = dr("FK_Religion")
            End If
            If Not IsDBNull(dr("FK_MaritalStatus")) Then
                _FK_MaritalStatus = dr("FK_MaritalStatus")
            End If
            If Not IsDBNull(dr("FK_WorkLocation")) Then
                _FK_WorkLocation = dr("FK_WorkLocation")
            End If
            If Not IsDBNull(dr("FK_Grade")) Then
                _FK_Grade = dr("FK_Grade")
            End If
            If Not IsDBNull(dr("FK_Designation")) Then
                _FK_Designation = dr("FK_Designation")
            End If
            If Not IsDBNull(dr("FK_LogicalGroup")) Then
                _FK_LogicalGroup = dr("FK_LogicalGroup")
            End If
            If Not IsDBNull(dr("AnnualLeaveBalance")) Then
                _AnnualLeaveBalance = dr("AnnualLeaveBalance")
            End If
            If Not IsDBNull(dr("JoinDate")) Then
                _JoinDate = dr("JoinDate")
            End If
            If Not IsDBNull(dr("IsTerminated")) Then
                _IsTerminated = dr("IsTerminated")
            End If
            If Not IsDBNull(dr("TerminateDate")) Then
                _TerminateDate = dr("TerminateDate")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("EmpImagePath")) Then
                _EmpImagePath = dr("EmpImagePath")
            End If
            If Not IsDBNull(dr("NationalId")) Then
                _NationalId = dr("NationalId")
            End If
            If Not IsDBNull(dr("MobileNo")) Then
                _MobileNo = dr("MobileNo")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            Return dr.Table
        End Function

        Public Function AssignEmployeeLogicalGroup() As Integer

            Return objDALEmployee.AssignEmployeeLogicalGroup(_EmployeeId, _FK_LogicalGroup)

        End Function

        Public Function GetByLogicalGroup() As DataTable

            Return objDALEmployee.GetByLogicalGroup(_FK_LogicalGroup, _FK_EntityId)

        End Function

        Public Function GetByWorkLocation() As DataTable

            Return objDALEmployee.GetByWorkLocation(_FK_WorkLocation)

        End Function

        Public Function GetByEntityId() As DataTable

            Return objDALEmployee.GetByEntityId(_FK_CompanyId, _FK_EntityId)

        End Function

        Public Function GetAll_ByEmplyeeNo() As DataTable

            Return objDALEmployee.GetAll_ByEmplyeeNo(_EmployeeNo, _FK_CompanyId)

        End Function

        Public Function CheckCardNo() As DataTable

            Return objDALEmployee.CheckCardNo(_EmployeeCardNo)

        End Function

        Public Function GetRowByEmpNo() As Employee

            Dim dr As DataRow
            dr = objDALEmployee.GetRowByEmpNo(_EmployeeNo)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("EmployeeId")) Then
                    _EmployeeId = dr("EmployeeId")
                End If
                If Not IsDBNull(dr("EmployeeNo")) Then
                    _EmployeeNo = dr("EmployeeNo")
                End If
                If Not IsDBNull(dr("EmployeeCardNo")) Then
                    _EmployeeCardNo = dr("EmployeeCardNo")
                End If
                If Not IsDBNull(dr("FK_Status")) Then
                    _FK_Status = dr("FK_Status")
                End If
                If Not IsDBNull(dr("EmployeeName")) Then
                    _EmployeeName = dr("EmployeeName")
                End If
                If Not IsDBNull(dr("EmployeeArabicName")) Then
                    _EmployeeArabicName = dr("EmployeeArabicName")
                End If
                If Not IsDBNull(dr("FK_CompanyId")) Then
                    _FK_CompanyId = dr("FK_CompanyId")
                End If
                If Not IsDBNull(dr("FK_EntityId")) Then
                    _FK_EntityId = dr("FK_EntityId")
                End If
                If Not IsDBNull(dr("EntityHierarchy")) Then
                    _EntityHierarchy = dr("EntityHierarchy")
                End If
                If Not IsDBNull(dr("Gender")) Then
                    _Gender = dr("Gender")
                End If
                If Not IsDBNull(dr("DOB")) Then
                    _DOB = dr("DOB")
                End If
                If Not IsDBNull(dr("Email")) Then
                    _Email = dr("Email")
                End If
                If Not IsDBNull(dr("FK_Nationality")) Then
                    _FK_Nationality = dr("FK_Nationality")
                End If
                If Not IsDBNull(dr("FK_Religion")) Then
                    _FK_Religion = dr("FK_Religion")
                End If
                If Not IsDBNull(dr("FK_MaritalStatus")) Then
                    _FK_MaritalStatus = dr("FK_MaritalStatus")
                End If
                If Not IsDBNull(dr("FK_WorkLocation")) Then
                    _FK_WorkLocation = dr("FK_WorkLocation")
                End If
                If Not IsDBNull(dr("FK_Grade")) Then
                    _FK_Grade = dr("FK_Grade")
                End If
                If Not IsDBNull(dr("FK_Designation")) Then
                    _FK_Designation = dr("FK_Designation")
                End If
                If Not IsDBNull(dr("FK_LogicalGroup")) Then
                    _FK_LogicalGroup = dr("FK_LogicalGroup")
                End If
                If Not IsDBNull(dr("AnnualLeaveBalance")) Then
                    _AnnualLeaveBalance = dr("AnnualLeaveBalance")
                End If
                If Not IsDBNull(dr("JoinDate")) Then
                    _JoinDate = dr("JoinDate")
                End If
                If Not IsDBNull(dr("IsTerminated")) Then
                    _IsTerminated = dr("IsTerminated")
                End If
                If Not IsDBNull(dr("TerminateDate")) Then
                    _TerminateDate = dr("TerminateDate")
                End If
                If Not IsDBNull(dr("Remarks")) Then
                    _Remarks = dr("Remarks")
                End If
                If Not IsDBNull(dr("EmpImagePath")) Then
                    _EmpImagePath = dr("EmpImagePath")
                End If
                If Not IsDBNull(dr("NationalId")) Then
                    _NationalId = dr("NationalId")
                End If
                If Not IsDBNull(dr("MobileNo")) Then
                    _MobileNo = dr("MobileNo")
                End If
                If Not IsDBNull(dr("CREATED_BY")) Then
                    _CREATED_BY = dr("CREATED_BY")
                End If
                If Not IsDBNull(dr("CREATED_DATE")) Then
                    _CREATED_DATE = dr("CREATED_DATE")
                End If
                If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                    _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
                End If
                If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                    _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
                End If
                If dr.Table.Columns.Contains("GT_VERIFYBY") Then
                    If Not IsDBNull(dr("GT_VERIFYBY")) Then
                        GT_VERIFYBY = dr("GT_VERIFYBY")
                    End If
                End If
                If Not IsDBNull(dr("PIN")) Then
                    _PIN = dr("PIN")
                End If
                If Not IsDBNull(dr("PayRollNumber")) Then
                    _PayRollNumber = dr("PayRollNumber")
                End If
                If Not IsDBNull(dr("FK_EmployeeTypeId")) Then
                    _FK_EmployeeTypeId = dr("FK_EmployeeTypeId")
                End If
                If Not IsDBNull(dr("ContractEndDate")) Then
                    _ContractEndDate = dr("ContractEndDate")
                End If
                If Not IsDBNull(dr("ExternalPartyName")) Then
                    _ExternalPartyName = dr("ExternalPartyName")
                End If
                If Not IsDBNull(dr("EnRoll_ID")) Then
                    _EnRoll_ID = dr("EnRoll_ID")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function

#Region "Get Employee IDz"
        Public Function GetEmployeeIDz() As DataTable

            Return objDALEmployee.GetEmployeesByCompanyOrEntity(_FK_CompanyId, _FK_EntityId, _FilterType, _PageNo, _PageSize)

        End Function

        Public Function GetFormerEmployeeIDz() As DataTable 'ID: M01 || Date: 20-04-2023 || By: Yahia shalan || Description: New method to call the stored procedure that get the former employees.'

            Return objDALEmployee.GetFormerEmployeesByCompanyOrEntity(_FK_CompanyId, _FK_EntityId, _FilterType, _PageNo, _PageSize)

        End Function
        Public Function GetEmployee_ByStatus() As DataTable

            Return objDALEmployee.GetEmployeesByCompanyOrEntity_ByStatus(_FK_CompanyId, _FK_EntityId, _FilterType, _Status, _PageNo, _PageSize)

        End Function
#End Region

        Public Function GetEmpNo(ByVal emp_no As String) As Boolean
            Dim dr As DataRow
            dr = objDALEmployee.GetByPK(emp_no)
            If Not dr Is Nothing Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function GetEmpByEmpNo() As DataTable

            Return objDALEmployee.GetEmpByEmpNo(_EmployeeNo, _FK_CompanyId, _UserId)

        End Function
        Public Function GetEmployeeByManagerId() As DataTable

            Return objDALEmployee.GetEmployeeByManagerId(_FK_CompanyId, _FK_ManagerID, _FK_EntityId)

        End Function
        Public Function GetEmployeeByManagerIdForEntity() As DataTable

            Return objDALEmployee.GetEmployeeByManagerIdForEntity(_FK_CompanyId, _FK_ManagerID, _FK_EntityId)

        End Function
        Public Function GetEmployeeByManagerIdAdvanced() As DataTable

            Return objDALEmployee.GetEmployeeByManagerIdAdvanced(_FK_CompanyId, _FK_ManagerID)

        End Function
        Public Function GetEmployeeByManagerIdAdvancedByFilter() As DataTable

            Return objDALEmployee.GetEmployeeByManagerIdAdvancedByFilter(_FK_CompanyId, _FK_ManagerID, _FK_EntityId, _FK_Designation)

        End Function
#Region "SearchMethod"
        Public Function GetBySearchCriteria() As DataTable
            Return objDALEmployee.GetBySearchCriteria(_Type, _Value, _Oper)
        End Function
#End Region
#End Region

#Region "Extended Methods"
        Public Function GetAllInnerJoin() As DataTable

            Return objDALEmployee.GetAllInnerJoin()

        End Function
        Public Function FindExisting() As Boolean

            Return objDALEmployee.FindExisting(_EmployeeId)

        End Function

        Public Function EmpImagePath_Insert() As Integer
            Return objDALEmployee.EmpImagePath_Insert(_EmployeeId, _EmpImagePath)
        End Function
        Public Function GetEmpDetails() As DataTable

            Return objDALEmployee.GetEmpDetails(_FK_CompanyId, _FK_EntityId, _EmployeeId)

        End Function
        Public Function GetExist_Employee() As Integer
            Return objDALEmployee.GetExist_Employee(_EmployeeId)

        End Function
#End Region

    End Class
End Namespace