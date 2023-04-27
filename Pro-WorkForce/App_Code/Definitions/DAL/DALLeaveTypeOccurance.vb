Imports Microsoft.VisualBasic
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Data

Namespace TA.Definitions
    Public Class DALLeaveTypeOccurance
        Inherits MGRBase

#Region "Constructor"
        Public Sub New()

        End Sub
#End Region

#Region "Class Variables"
        Private strConn As String
        Dim LeaveTypeOccurance_bulk_insert As String = "LeaveTypeOccurance_bulk_insert"
        Private LeaveTypeOccurance_Select As String = "LeaveTypeOccurance_select"
#End Region

#Region "Methods"
        Public Function Add_Bulk(ByVal xml As String, ByVal LeaveId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(LeaveTypeOccurance_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@FK_LeaveId", LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function GetByPK(ByVal FK_LeaveId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(LeaveTypeOccurance_Select, New SqlParameter("@FK_LeaveId", FK_LeaveId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByPKDT(ByVal FK_LeaveId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objRow As DataTable
            Try
                objRow = objDac.GetDataTable(LeaveTypeOccurance_Select, New SqlParameter("@FK_LeaveId", FK_LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
#End Region
    End Class
End Namespace
