Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.DailyTasks

    Public Class Monthly_Deduction

#Region "Class Variables"


        Private _Year As String
        Private _Month As String
        Private _FK_EmployeeId As Integer
        Private _Delay As DateTime
        Private _EarlyOut As DateTime
        Private _Delay_Count As Integer
        Private _EarlyOut_Count As Integer
        Private _Absent_Count As Integer
        Private _MissingIN_Count As Integer
        Private _MissingOut_Count As Integer
        Private _Uncomplete50WHRS As Integer
        Private _IsApproved As Boolean
        Private _Apporved_By As String
        Private _Approval_Date As DateTime
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private objDALMonthly_Deduction As DALMonthly_Deduction

#End Region

#Region "Public Properties"

        Public Property Year() As String
            Set(ByVal value As String)
                _Year = value
            End Set
            Get
                Return (_Year)
            End Get
        End Property

        Public Property Month() As String
            Set(ByVal value As String)
                _Month = value
            End Set
            Get
                Return (_Month)
            End Get
        End Property

        Public Property FK_EmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property

        Public Property Delay() As DateTime
            Set(ByVal value As DateTime)
                _Delay = value
            End Set
            Get
                Return (_Delay)
            End Get
        End Property

        Public Property EarlyOut() As DateTime
            Set(ByVal value As DateTime)
                _EarlyOut = value
            End Set
            Get
                Return (_EarlyOut)
            End Get
        End Property

        Public Property Delay_Count() As Integer
            Set(ByVal value As Integer)
                _Delay_Count = value
            End Set
            Get
                Return (_Delay_Count)
            End Get
        End Property

        Public Property EarlyOut_Count() As Integer
            Set(ByVal value As Integer)
                _EarlyOut_Count = value
            End Set
            Get
                Return (_EarlyOut_Count)
            End Get
        End Property

        Public Property Absent_Count() As Integer
            Set(ByVal value As Integer)
                _Absent_Count = value
            End Set
            Get
                Return (_Absent_Count)
            End Get
        End Property

        Public Property MissingIN_Count() As Integer
            Set(ByVal value As Integer)
                _MissingIN_Count = value
            End Set
            Get
                Return (_MissingIN_Count)
            End Get
        End Property

        Public Property MissingOut_Count() As Integer
            Set(ByVal value As Integer)
                _MissingOut_Count = value
            End Set
            Get
                Return (_MissingOut_Count)
            End Get
        End Property

        Public Property Uncomplete50WHRS() As Integer
            Set(ByVal value As Integer)
                _Uncomplete50WHRS = value
            End Set
            Get
                Return (_Uncomplete50WHRS)
            End Get
        End Property

        Public Property IsApproved() As Boolean
            Set(ByVal value As Boolean)
                _IsApproved = value
            End Set
            Get
                Return (_IsApproved)
            End Get
        End Property

        Public Property Apporved_By() As String
            Set(ByVal value As String)
                _Apporved_By = value
            End Set
            Get
                Return (_Apporved_By)
            End Get
        End Property

        Public Property Approval_Date() As DateTime
            Set(ByVal value As DateTime)
                _Approval_Date = value
            End Set
            Get
                Return (_Approval_Date)
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
#End Region


#Region "Constructor"

        Public Sub New()

            objDALMonthly_Deduction = New DALMonthly_Deduction()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALMonthly_Deduction.Add(_Year, _Month, _FK_EmployeeId, _Delay, _EarlyOut, _Delay_Count, _EarlyOut_Count, _Absent_Count, _MissingIN_Count, _MissingOut_Count, _Uncomplete50WHRS, _IsApproved, _Apporved_By, _Approval_Date)
        End Function

        Public Function Update() As Integer

            Return objDALMonthly_Deduction.Update(_Year, _Month, _FK_EmployeeId, _Delay, _EarlyOut, _Delay_Count, _EarlyOut_Count, _Absent_Count, _MissingIN_Count, _MissingOut_Count, _Uncomplete50WHRS, _IsApproved, _Apporved_By, _Approval_Date)

        End Function

        Public Function Update_Approval() As Integer

            Return objDALMonthly_Deduction.Update_Approval(_Year, _Month, _FK_EmployeeId, _Apporved_By)

        End Function

        Public Function Delete() As Integer

            Return objDALMonthly_Deduction.Delete(_Year, _Month, _FK_EmployeeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALMonthly_Deduction.GetAll()

        End Function

        Public Function GetAll_Inner() As DataTable

            Return objDALMonthly_Deduction.GetAll_Inner(_FK_CompanyId, FK_EntityId, _Year, _Month)

        End Function

        Public Function GetByPK() As Monthly_Deduction

            Dim dr As DataRow
            dr = objDALMonthly_Deduction.GetByPK(_Year, _Month, _FK_EmployeeId)

            If Not IsDBNull(dr("Year")) Then
                _Year = dr("Year")
            End If
            If Not IsDBNull(dr("Month")) Then
                _Month = dr("Month")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("Delay")) Then
                _Delay = dr("Delay")
            End If
            If Not IsDBNull(dr("EarlyOut")) Then
                _EarlyOut = dr("EarlyOut")
            End If
            If Not IsDBNull(dr("Delay_Count")) Then
                _Delay_Count = dr("Delay_Count")
            End If
            If Not IsDBNull(dr("EarlyOut_Count")) Then
                _EarlyOut_Count = dr("EarlyOut_Count")
            End If
            If Not IsDBNull(dr("Absent_Count")) Then
                _Absent_Count = dr("Absent_Count")
            End If
            If Not IsDBNull(dr("MissingIN_Count")) Then
                _MissingIN_Count = dr("MissingIN_Count")
            End If
            If Not IsDBNull(dr("MissingOut_Count")) Then
                _MissingOut_Count = dr("MissingOut_Count")
            End If
            If Not IsDBNull(dr("Uncomplete50WHRS")) Then
                _Uncomplete50WHRS = dr("Uncomplete50WHRS")
            End If
            If Not IsDBNull(dr("IsApproved")) Then
                _IsApproved = dr("IsApproved")
            End If
            If Not IsDBNull(dr("Apporved_By")) Then
                _Apporved_By = dr("Apporved_By")
            End If
            If Not IsDBNull(dr("Approval_Date")) Then
                _Approval_Date = dr("Approval_Date")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace