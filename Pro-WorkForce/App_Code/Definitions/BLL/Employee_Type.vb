Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Definitions

    Public Class Employee_Type

#Region "Class Variables"


        Private _EmployeeTypeId As Integer
        Private _TypeName_En As String
        Private _TypeName_Ar As String
        Private _IsInternaltype As Boolean
        Private _EmployeeNumberInitial As String
        Private objDALEmployee_Type As DALEmployee_Type
        Private _InitialIndex As Integer

#End Region

#Region "Public Properties"


        Public Property EmployeeTypeId() As Integer
            Set(ByVal value As Integer)
                _EmployeeTypeId = value
            End Set
            Get
                Return (_EmployeeTypeId)
            End Get
        End Property


        Public Property TypeName_En() As String
            Set(ByVal value As String)
                _TypeName_En = value
            End Set
            Get
                Return (_TypeName_En)
            End Get
        End Property


        Public Property TypeName_Ar() As String
            Set(ByVal value As String)
                _TypeName_Ar = value
            End Set
            Get
                Return (_TypeName_Ar)
            End Get
        End Property


        Public Property IsInternaltype() As Boolean
            Set(ByVal value As Boolean)
                _IsInternaltype = value
            End Set
            Get
                Return (_IsInternaltype)
            End Get
        End Property

        Public Property EmployeeNumberInitial() As String
            Set(ByVal value As String)
                _EmployeeNumberInitial = value
            End Set
            Get
                Return (_EmployeeNumberInitial)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmployee_Type = New DALEmployee_Type()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALEmployee_Type.Add(_EmployeeTypeId, _TypeName_En, _TypeName_Ar, _IsInternaltype, _EmployeeNumberInitial)
            App_EventsLog.Insert_ToEventLog("Add", _EmployeeTypeId, "Employee_Type", "Define Employee Type")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALEmployee_Type.Update(_EmployeeTypeId, _TypeName_En, _TypeName_Ar, _IsInternaltype, _EmployeeNumberInitial)
            App_EventsLog.Insert_ToEventLog("Edit", _EmployeeTypeId, "Employee_Type", "Define Employee Type")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALEmployee_Type.Delete(_EmployeeTypeId)
            App_EventsLog.Insert_ToEventLog("Delete", _EmployeeTypeId, "Employee_Type", "Define Employee Type")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALEmployee_Type.GetAll()

        End Function

        Public Function GetAll_EmployeeType()
            Return objDALEmployee_Type.GetAll_EmployeeType()
        End Function

        Public Function GetByPK() As Employee_Type

            Dim dr As DataRow
            dr = objDALEmployee_Type.GetByPK(_EmployeeTypeId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("EmployeeTypeId")) Then
                _EmployeeTypeId = dr("EmployeeTypeId")
            End If
            If Not IsDBNull(dr("TypeName_En")) Then
                _TypeName_En = dr("TypeName_En")
            End If
            If Not IsDBNull(dr("TypeName_Ar")) Then
                _TypeName_Ar = dr("TypeName_Ar")
            End If
            If Not IsDBNull(dr("IsInternaltype")) Then
                _IsInternaltype = dr("IsInternaltype")
            End If
            If Not IsDBNull(dr("EmployeeNumberInitial")) Then
                _EmployeeNumberInitial = dr("EmployeeNumberInitial")
            End If
            Return Me
        End Function

        Public Function GetInternal() As Employee_Type

            Dim dr As DataRow
            dr = objDALEmployee_Type.GetInternal(_EmployeeTypeId)
            If Not IsDBNull(dr("IsInternaltype")) Then
                _IsInternaltype = dr("IsInternaltype")
            End If
            Return Me
        End Function

        Public Function GetInitialIndex() As Employee_Type

            Dim dr As DataRow
            dr = objDALEmployee_Type.GetInitialIndex(_EmployeeTypeId)
            If Not dr Is Nothing Then
                If Not IsDBNull(dr("InitialIndex")) Then
                    _InitialIndex = dr("InitialIndex")
                End If
            Else
                Return Nothing
            End If
        End Function

#End Region

    End Class
End Namespace