Imports Microsoft.VisualBasic
Imports System.Data
Imports TA.Events

Namespace TA.Definitions
    Public Class LeaveTypeOccurance

#Region "Class Variables"
        Private _LeaveId As Integer
        Private _FK_PermId As Integer
        Private _FK_DurationId As Integer
        Private _MaximumOccur As Integer
#End Region

#Region "PROPERTIES"
        Public Property LeaveId() As Integer
            Set(ByVal value As Integer)
                _LeaveId = value
            End Set
            Get
                Return (_LeaveId)
            End Get
        End Property
#End Region

#Region "Class Variables"
        Private objDALLeaveTypeOccurance As DALLeaveTypeOccurance
#End Region

        Public Function Add_Bulk(ByVal DT As DataTable, ByVal LeaveId As Integer) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml
            objDALLeaveTypeOccurance = New DALLeaveTypeOccurance
            Dim rslt As Integer = objDALLeaveTypeOccurance.Add_Bulk(StrXml, LeaveId)
            App_EventsLog.Insert_ToEventLog("Add", _LeaveId, "LeaveTypeoccurance", "Define Type of Leaves")
            Return rslt
        End Function


        Public Function GetByPK() As DataTable

            Dim dtLeaves As DataTable
            objDALLeaveTypeOccurance = New DALLeaveTypeOccurance
            dtLeaves = objDALLeaveTypeOccurance.GetByPKDT(_LeaveId)

            Return dtLeaves
        End Function

    End Class
End Namespace
