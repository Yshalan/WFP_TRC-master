Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA_AuthorityMovements

    Public Class Authority_Movements

#Region "Class Variables"


        Private _AuthorityMoveId As Long
        Private _FK_EmployeeId As Long
        Private _FK_AuthoritId As Integer
        Private _MoveDate As DateTime
        Private _MoveTime As DateTime
        Private _Type As String
        Private _FK_ReasonId As Integer
        Private _Remarks As String
        Private _IP_Address As String
        Private _Domain As String
        Private _PCName As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALAuthority_Movements As DALAuthority_Movements

#End Region

#Region "Public Properties"


        Public Property AuthorityMoveId() As Long
            Set(ByVal value As Long)
                _AuthorityMoveId = value
            End Set
            Get
                Return (_AuthorityMoveId)
            End Get
        End Property


        Public Property FK_EmployeeId() As Long
            Set(ByVal value As Long)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property


        Public Property FK_AuthoritId() As Integer
            Set(ByVal value As Integer)
                _FK_AuthoritId = value
            End Set
            Get
                Return (_FK_AuthoritId)
            End Get
        End Property


        Public Property MoveDate() As DateTime
            Set(ByVal value As DateTime)
                _MoveDate = value
            End Set
            Get
                Return (_MoveDate)
            End Get
        End Property


        Public Property MoveTime() As DateTime
            Set(ByVal value As DateTime)
                _MoveTime = value
            End Set
            Get
                Return (_MoveTime)
            End Get
        End Property


        Public Property Type() As String
            Set(ByVal value As String)
                _Type = value
            End Set
            Get
                Return (_Type)
            End Get
        End Property


        Public Property FK_ReasonId() As Integer
            Set(ByVal value As Integer)
                _FK_ReasonId = value
            End Set
            Get
                Return (_FK_ReasonId)
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


        Public Property IP_Address() As String
            Set(ByVal value As String)
                _IP_Address = value
            End Set
            Get
                Return (_IP_Address)
            End Get
        End Property


        Public Property Domain() As String
            Set(ByVal value As String)
                _Domain = value
            End Set
            Get
                Return (_Domain)
            End Get
        End Property


        Public Property PCName() As String
            Set(ByVal value As String)
                _PCName = value
            End Set
            Get
                Return (_PCName)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALAuthority_Movements = New DALAuthority_Movements()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALAuthority_Movements.Add(_FK_EmployeeId, _FK_AuthoritId, _MoveDate, _MoveTime, _Type, _FK_ReasonId, _Remarks, _IP_Address, _Domain, _PCName, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
        End Function

        Public Function Update() As Integer

            Return objDALAuthority_Movements.Update(_AuthorityMoveId, _FK_EmployeeId, _FK_AuthoritId, _MoveDate, _MoveTime, _Type, _FK_ReasonId, _Remarks, _IP_Address, _Domain, _PCName, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)

        End Function



        Public Function Delete() As Integer

            Return objDALAuthority_Movements.Delete(_AuthorityMoveId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALAuthority_Movements.GetAll()

        End Function
        Public Function GetAllByEmployeeId() As DataTable

            Return objDALAuthority_Movements.GetAllByEmployeeId(_FK_EmployeeId)

        End Function

        Public Function GetByPK() As Authority_Movements

            Dim dr As DataRow
            dr = objDALAuthority_Movements.GetByPK(_AuthorityMoveId)

            If Not IsDBNull(dr("AuthorityMoveId")) Then
                _AuthorityMoveId = dr("AuthorityMoveId")
            End If
            If Not IsDBNull(dr("FK_EmployeeId")) Then
                _FK_EmployeeId = dr("FK_EmployeeId")
            End If
            If Not IsDBNull(dr("FK_AuthoritId")) Then
                _FK_AuthoritId = dr("FK_AuthoritId")
            End If
            If Not IsDBNull(dr("MoveDate")) Then
                _MoveDate = dr("MoveDate")
            End If
            If Not IsDBNull(dr("MoveTime")) Then
                _MoveTime = dr("MoveTime")
            End If
            If Not IsDBNull(dr("Type")) Then
                _Type = dr("Type")
            End If
            If Not IsDBNull(dr("FK_ReasonId")) Then
                _FK_ReasonId = dr("FK_ReasonId")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            If Not IsDBNull(dr("IP_Address")) Then
                _IP_Address = dr("IP_Address")
            End If
            If Not IsDBNull(dr("Domain")) Then
                _Domain = dr("Domain")
            End If
            If Not IsDBNull(dr("PCName")) Then
                _PCName = dr("PCName")
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
            Return Me
        End Function

#End Region

    End Class
End Namespace