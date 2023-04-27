Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Lookup

    Public Class Emp_GradeLeavesTypes

#Region "Class Variables"


        Private _FK_LeaveId As Integer
        Private _FK_GradeId As Integer
        Private objDALEmp_GradeLeavesTypes As DALEmp_GradeLeavesTypes

#End Region

#Region "Public Properties"


        Public Property FK_LeaveId() As Integer
            Set(ByVal value As Integer)
                _FK_LeaveId = value
            End Set
            Get
                Return (_FK_LeaveId)
            End Get
        End Property


        Public Property FK_GradeId() As Integer
            Set(ByVal value As Integer)
                _FK_GradeId = value
            End Set
            Get
                Return (_FK_GradeId)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_GradeLeavesTypes = New DALEmp_GradeLeavesTypes()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_GradeLeavesTypes.Add(_FK_LeaveId, _FK_GradeId)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_GradeLeavesTypes.Update(_FK_LeaveId, _FK_GradeId)

        End Function

        Public Function Delete() As Integer

            Return objDALEmp_GradeLeavesTypes.Delete(_FK_LeaveId, _FK_GradeId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_GradeLeavesTypes.GetAll()

        End Function

        Public Function GetByPK() As Emp_GradeLeavesTypes

            Dim dr As DataRow
            dr = objDALEmp_GradeLeavesTypes.GetByPK(_FK_LeaveId, _FK_GradeId)

            If Not IsDBNull(dr("FK_LeaveId")) Then
                _FK_LeaveId = dr("FK_LeaveId")
            End If
            If Not IsDBNull(dr("FK_GradeId")) Then
                _FK_GradeId = dr("FK_GradeId")
            End If
            Return Me
        End Function

        Public Function DeleteByFk() As Integer
            Return objDALEmp_GradeLeavesTypes.DeleteByFk(_FK_GradeId)
        End Function

        Public Function SelectByFk() As DataTable
            Return objDALEmp_GradeLeavesTypes.GetAllByFk(_FK_GradeId)
        End Function

        Public Function GetAllByFK_LeaveId() As DataTable
            Return objDALEmp_GradeLeavesTypes.GetAllByFK_LeaveId(_FK_LeaveId)
        End Function

        Public Function GetAllInnerByFK_LeaveId() As DataTable
            Return objDALEmp_GradeLeavesTypes.GetAllInnerByFK_LeaveId(_FK_LeaveId)
        End Function

        Public Function DeleteFK_LeaveId() As Integer
            Return objDALEmp_GradeLeavesTypes.DeleteFK_LeaveId(_FK_LeaveId)
        End Function

        Public Function Add_Bulk(ByVal DT As DataTable, ByVal LeaveId As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml
            objDALEmp_GradeLeavesTypes = New DALEmp_GradeLeavesTypes
            Dim rslt As Integer = objDALEmp_GradeLeavesTypes.Add_Bulk(StrXml, LeaveId)
            App_EventsLog.Insert_ToEventLog("Add", LeaveId, "LeaveTypeoccurance", "Define Type of Leaves")
            Return rslt
        End Function

#End Region

    End Class
End Namespace