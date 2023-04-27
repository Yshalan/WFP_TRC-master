Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.DailyTasks

    Public Class RECALC_REQUEST

#Region "Class Variables"

        Private _requestID As String
        Private _EMP_NO As Integer
        Private _VALID_FROM_NUM As Integer
        Private _IsCalculated As Boolean
        Private _UserId As Integer
        Private _FromDate As Integer
        Private _ToDate As Integer
        Private _CompanyId As Integer
        Private _EntityId As Integer
        Private _EmployeeId As Integer
        Private objDALRECALC_REQUEST As DALRECALC_REQUEST

#End Region

#Region "Public Properties"

        Public Property requestID() As String
            Set(ByVal value As String)
                _requestID = value
            End Set
            Get
                Return (_requestID)
            End Get
        End Property

        Public Property EMP_NO() As Integer
            Set(ByVal value As Integer)
                _EMP_NO = value
            End Set
            Get
                Return (_EMP_NO)
            End Get
        End Property

        Public Property VALID_FROM_NUM() As Integer
            Set(ByVal value As Integer)
                _VALID_FROM_NUM = value
            End Set
            Get
                Return (_VALID_FROM_NUM)
            End Get
        End Property

        Public Property IsCalculated() As Boolean
            Set(ByVal value As Boolean)
                _IsCalculated = value
            End Set
            Get
                Return (_IsCalculated)
            End Get
        End Property

        Public Property UserId() As Integer
            Get
                Return _UserId
            End Get
            Set(ByVal value As Integer)
                _UserId = value
            End Set
        End Property

        Public Property FromDate() As Integer
            Get
                Return _FromDate
            End Get
            Set(ByVal value As Integer)
                _FromDate = value
            End Set
        End Property

        Public Property ToDate() As Integer
            Get
                Return _ToDate
            End Get
            Set(ByVal value As Integer)
                _ToDate = value
            End Set
        End Property

        Public Property CompanyID() As Integer
            Get
                Return _CompanyId
            End Get
            Set(ByVal value As Integer)
                _CompanyId = value
            End Set
        End Property

        Public Property EntityID() As Integer
            Get
                Return _EntityId
            End Get
            Set(ByVal value As Integer)
                _EntityId = value
            End Set
        End Property
        Public Property EmployeeId() As Integer
            Get
                Return _EmployeeId
            End Get
            Set(ByVal value As Integer)
                _EmployeeId = value
            End Set
        End Property
#End Region

#Region "Constructor"

        Public Sub New()

            objDALRECALC_REQUEST = New DALRECALC_REQUEST()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALRECALC_REQUEST.Add(_EMP_NO, _VALID_FROM_NUM, _IsCalculated)
        End Function

        Public Function RECALCULATE() As Integer

            Return objDALRECALC_REQUEST.RECALCULATE_REQ(_EMP_NO, _VALID_FROM_NUM)

        End Function

        Public Function Update() As Integer

            Return objDALRECALC_REQUEST.Update(_requestID, _EMP_NO, _VALID_FROM_NUM, _IsCalculated)

        End Function

        Public Function Delete() As Integer

            Return objDALRECALC_REQUEST.Delete(_EMP_NO, _VALID_FROM_NUM)

        End Function

        Public Function GetAll() As DataTable

            Return objDALRECALC_REQUEST.GetAll()

        End Function

        Public Function GetByPK() As RECALC_REQUEST
            Dim dr As DataRow
            dr = objDALRECALC_REQUEST.GetByPK(_EMP_NO, _VALID_FROM_NUM)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("requestID")) Then
                    _requestID = dr("requestID")
                End If
                If Not IsDBNull(dr("EMP_NO")) Then
                    _EMP_NO = dr("EMP_NO")
                End If
                If Not IsDBNull(dr("IsCalculated")) Then
                    _IsCalculated = dr("IsCalculated")
                End If
                If Not IsDBNull(dr("VALID_FROM_NUM")) Then
                    _VALID_FROM_NUM = dr("VALID_FROM_NUM")
                End If
                Return Me
            Else
                Return Nothing
            End If
        End Function

        Public Function RecalculateTransactions_REQ() As Integer

            Return objDALRECALC_REQUEST.RecalculateTransactions_REQ(_UserId, _FromDate, _ToDate, _CompanyId, _EntityId, _EmployeeId)

        End Function


#End Region

    End Class
End Namespace