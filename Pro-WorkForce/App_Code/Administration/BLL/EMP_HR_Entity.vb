Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Admin

    Public Class EMP_HR_Entity

#Region "Class Variables"


        Private _FK_HREmployeeId As Long
        Private _FK_Entity As Integer
        Private objDALEMP_HR_Entity As DALEMP_HR_Entity

#End Region

#Region "Public Properties"


        Public Property FK_HREmployeeId() As Long
            Set(ByVal value As Long)
                _FK_HREmployeeId = value
            End Set
            Get
                Return (_FK_HREmployeeId)
            End Get
        End Property


        Public Property FK_Entity() As Integer
            Set(ByVal value As Integer)
                _FK_Entity = value
            End Set
            Get
                Return (_FK_Entity)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEMP_HR_Entity = New DALEMP_HR_Entity()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEMP_HR_Entity.Add(_FK_HREmployeeId, _FK_Entity)
        End Function

        Public Function Update() As Integer

            Return objDALEMP_HR_Entity.Update(_FK_HREmployeeId, _FK_Entity)

        End Function

        Public Function Delete() As Integer

            Return objDALEMP_HR_Entity.Delete(_FK_HREmployeeId)

        End Function

        Public Function DeleteBy_FK_HREmployeeId() As Integer

            Return objDALEMP_HR_Entity.DeleteBy_FK_HREmployeeId(_FK_HREmployeeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEMP_HR_Entity.GetAll()

        End Function
        Public Function GetByHREmployeeId() As DataTable
            Return objDALEMP_HR_Entity.GetByHREmployeeId(_FK_HREmployeeId)
        End Function
        Public Function GetByPK() As EMP_HR_Entity

            Dim dr As DataRow
            dr = objDALEMP_HR_Entity.GetByPK(_FK_HREmployeeId)

            If Not IsDBNull(dr("FK_HREmployeeId")) Then
                _FK_HREmployeeId = dr("FK_HREmployeeId")
            End If
            If Not IsDBNull(dr("FK_Entity")) Then
                _FK_Entity = dr("FK_Entity")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace