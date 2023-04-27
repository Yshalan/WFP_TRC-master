Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.ScheduleGroups

    Public Class LeaveSubstitute

#Region "Class Variables"


        Private _SubstituteId As Integer
        Private _FK_EmployeeId As Integer
        Private _LeaveDate As DateTime
        Private _FK_ShiftId As Integer
        Private _SubstituteDate As DateTime
        Private _IsConfirmed As Boolean
        Private _ConfirmSubstituteDate As DateTime
        Private _Confirmed_By As String
        Private _ModifiedDate As DateTime
        Private _UserId As Integer
        Private objDALLeaveSubstitute As DALLeaveSubstitute

#End Region

#Region "Public Properties"


        Public Property SubstituteId() As Integer
            Set(ByVal value As Integer)
                _SubstituteId = value
            End Set
            Get
                Return (_SubstituteId)
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


        Public Property LeaveDate() As DateTime
            Set(ByVal value As DateTime)
                _LeaveDate = value
            End Set
            Get
                Return (_LeaveDate)
            End Get
        End Property


        Public Property FK_ShiftId() As Integer
            Set(ByVal value As Integer)
                _FK_ShiftId = value
            End Set
            Get
                Return (_FK_ShiftId)
            End Get
        End Property


        Public Property SubstituteDate() As DateTime
            Set(ByVal value As DateTime)
                _SubstituteDate = value
            End Set
            Get
                Return (_SubstituteDate)
            End Get
        End Property


        Public Property IsConfirmed() As Boolean
            Set(ByVal value As Boolean)
                _IsConfirmed = value
            End Set
            Get
                Return (_IsConfirmed)
            End Get
        End Property


        Public Property ConfirmSubstituteDate() As DateTime
            Set(ByVal value As DateTime)
                _ConfirmSubstituteDate = value
            End Set
            Get
                Return (_ConfirmSubstituteDate)
            End Get
        End Property


        Public Property Confirmed_By() As String
            Set(ByVal value As String)
                _Confirmed_By = value
            End Set
            Get
                Return (_Confirmed_By)
            End Get
        End Property


        Public Property ModifiedDate() As DateTime
            Set(ByVal value As DateTime)
                _ModifiedDate = value
            End Set
            Get
                Return (_ModifiedDate)
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
#End Region


#Region "Constructor"

        Public Sub New()

            objDALLeaveSubstitute = New DALLeaveSubstitute()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALLeaveSubstitute.Add(_FK_EmployeeId, _LeaveDate, _FK_ShiftId, _SubstituteDate, _IsConfirmed, _ConfirmSubstituteDate, _Confirmed_By, _ModifiedDate)
        End Function

        Public Function Update() As Integer

            Return objDALLeaveSubstitute.Update(_SubstituteId, _IsConfirmed, _ConfirmSubstituteDate, _Confirmed_By)

        End Function



        Public Function Delete() As Integer

            Return objDALLeaveSubstitute.Delete(_SubstituteId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALLeaveSubstitute.GetAll()

        End Function

        Public Function GetAll_Pending() As DataTable

            Return objDALLeaveSubstitute.GetAll_Pending(_UserId)

        End Function

        Public Function GetAll_Confirmed() As DataTable

            Return objDALLeaveSubstitute.GetAll_Confirmed(_UserId)

        End Function

        Public Function Get_ByEmployeeId() As DataTable

            Return objDALLeaveSubstitute.Get_ByEmployeeId(_FK_EmployeeId)

        End Function

        Public Function GetByPK() As LeaveSubstitute

            Dim dr As DataRow
            dr = objDALLeaveSubstitute.GetByPK(_SubstituteId)

            If Not IsDBNull(dr("SubstituteId")) Then
                _SubstituteId = dr("SubstituteId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("LeaveDate")) Then
                _LeaveDate = dr("LeaveDate")
            End If
            If Not IsDBNull(dr("FK_ShiftId")) Then
                _FK_ShiftId = dr("FK_ShiftId")
            End If
            If Not IsDBNull(dr("SubstituteDate")) Then
                _SubstituteDate = dr("SubstituteDate")
            End If
            If Not IsDBNull(dr("IsConfirmed")) Then
                _IsConfirmed = dr("IsConfirmed")
            End If
            If Not IsDBNull(dr("ConfirmSubstituteDate")) Then
                _ConfirmSubstituteDate = dr("ConfirmSubstituteDate")
            End If
            If Not IsDBNull(dr("Confirmed_By")) Then
                _Confirmed_By = dr("Confirmed_By")
            End If
            If Not IsDBNull(dr("ModifiedDate")) Then
                _ModifiedDate = dr("ModifiedDate")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace