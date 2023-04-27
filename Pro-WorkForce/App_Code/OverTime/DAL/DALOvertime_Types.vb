Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.OverTime

    Public Class DALOvertime_Types
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Overtime_Types_Select As String = "Overtime_Types_select"
        Private Overtime_Types_Select_All As String = "Overtime_Types_select_All"
        Private Overtime_Types_Insert As String = "Overtime_Types_Insert"
        Private Overtime_Types_Update As String = "Overtime_Types_Update"
        Private Overtime_Types_Delete As String = "Overtime_Types_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal OvertimeTypeName As String, ByVal OvertimeTypeArabicName As String, ByVal OvertimeRate As Double, ByVal CompensateToLeave As Boolean, ByVal FK_LeaveTypeId As Integer, ByVal MustRequested As Boolean, ByVal CREATED_BY As String, ByVal OvertimeCalculationConsideration As Integer, ByVal OvertimeChangeValue As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Overtime_Types_Insert, New SqlParameter("@OvertimeTypeName", OvertimeTypeName),
               New SqlParameter("@OvertimeTypeArabicName", OvertimeTypeArabicName),
               New SqlParameter("@OvertimeRate", OvertimeRate),
               New SqlParameter("@CompensateToLeave", CompensateToLeave),
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId),
               New SqlParameter("@MustRequested", MustRequested),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@OvertimeCalculationConsideration", OvertimeCalculationConsideration),
               New SqlParameter("@OvertimeChangeValue", OvertimeChangeValue))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal OvertimeTypeId As Integer, ByVal OvertimeTypeName As String, ByVal OvertimeTypeArabicName As String, ByVal OvertimeRate As Double, ByVal CompensateToLeave As Boolean, ByVal FK_LeaveTypeId As Integer, ByVal MustRequested As Boolean, ByVal LAST_UPDATE_BY As String, ByVal OvertimeCalculationConsideration As Integer, ByVal OvertimeChangeValue As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Overtime_Types_Update, New SqlParameter("@OvertimeTypeId", OvertimeTypeId),
               New SqlParameter("@OvertimeTypeName", OvertimeTypeName),
               New SqlParameter("@OvertimeTypeArabicName", OvertimeTypeArabicName),
               New SqlParameter("@OvertimeRate", OvertimeRate),
               New SqlParameter("@CompensateToLeave", CompensateToLeave),
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId),
               New SqlParameter("@MustRequested", MustRequested),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@OvertimeCalculationConsideration", OvertimeCalculationConsideration),
               New SqlParameter("@OvertimeChangeValue", OvertimeChangeValue))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal OvertimeTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Overtime_Types_Delete, New SqlParameter("@OvertimeTypeId", OvertimeTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal OvertimeTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Overtime_Types_Select, New SqlParameter("@OvertimeTypeId", OvertimeTypeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Overtime_Types_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace