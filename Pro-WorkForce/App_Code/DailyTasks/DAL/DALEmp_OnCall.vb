Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA_Emp_OnCall

    Public Class DALEmp_OnCall
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OnCall_Select As String = "OnCall_select"
        Private OnCall_Select_All As String = "OnCall_select_All"
        Private OnCall_Insert As String = "OnCall_Insert"
        Private OnCall_Update As String = "OnCall_Update"
        Private OnCall_Delete As String = "OnCall_Delete"
        Private OnCall_Select_AllbyFilter As String = "OnCall_Select_AllbyFilter"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Integer, ByVal DutyDate As Date, ByVal FromHome As Boolean, ByVal FromTime As Integer, ByVal ToTime As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OnCall_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@DutyDate", DutyDate), _
               New SqlParameter("@FromHome", FromHome), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal OnCallId As Integer, ByVal FK_EmployeeId As Integer, ByVal DutyDate As Date, ByVal FromHome As Boolean, ByVal FromTime As Integer, ByVal ToTime As Integer, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OnCall_Update, New SqlParameter("@OnCallId", OnCallId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@DutyDate", DutyDate), _
               New SqlParameter("@FromHome", FromHome), _
               New SqlParameter("@FromTime", FromTime), _
               New SqlParameter("@ToTime", ToTime), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal OnCallId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OnCall_Delete, New SqlParameter("@OnCallId", OnCallId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal OnCallId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OnCall_Select, New SqlParameter("@OnCallId", OnCallId)).Rows(0)
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
                objColl = objDac.GetDataTable(OnCall_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllbyFilter(ByVal FK_ManagerId As Integer, ByVal Fk_EntityId As Integer, ByVal FK_DesginationId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OnCall_Select_AllbyFilter, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@Fk_EntityId", Fk_EntityId), _
                                              New SqlParameter("@FK_DesginationId", FK_DesginationId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace