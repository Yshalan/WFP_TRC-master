Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.DailyTasks

    Public Class DALEmp_Allowed_GPSCoordinates
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_Allowed_GPSCoordinates_Select As String = "Emp_Allowed_GPSCoordinates_select"
        Private Emp_Allowed_GPSCoordinates_Select_All As String = "Emp_Allowed_GPSCoordinates_select_All"
        Private Emp_Allowed_GPSCoordinates_Insert As String = "Emp_Allowed_GPSCoordinates_Insert"
        Private Emp_Allowed_GPSCoordinates_Update As String = "Emp_Allowed_GPSCoordinates_Update"
        Private Emp_Allowed_GPSCoordinates_Delete As String = "Emp_Allowed_GPSCoordinates_Delete"
        Private Emp_Allowed_GPSCoordinates_Select_All_Inner As String = "Emp_Allowed_GPSCoordinates_Select_All_Inner"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef AllowedGPSId As Long, ByVal FK_EmployeeId As Long, ByVal LocationName As String, ByVal LocationArabicName As String, ByVal GPS_Coordinates As String, ByVal Radius As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@AllowedGPSId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, AllowedGPSId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Allowed_GPSCoordinates_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@LocationName", LocationName),
               New SqlParameter("@LocationArabicName", LocationArabicName),
               New SqlParameter("@GPS_Coordinates", GPS_Coordinates),
               New SqlParameter("@Radius", Radius),
               New SqlParameter("@FromDate", FromDate),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)),
               New SqlParameter("@IsTemporary", IsTemporary),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then AllowedGPSId = sqlOut.Value Else AllowedGPSId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal AllowedGPSId As Long, ByVal FK_EmployeeId As Long, ByVal LocationName As String, ByVal LocationArabicName As String, ByVal GPS_Coordinates As String, ByVal Radius As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Allowed_GPSCoordinates_Update, New SqlParameter("@AllowedGPSId", AllowedGPSId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@LocationName", LocationName),
               New SqlParameter("@LocationArabicName", LocationArabicName),
               New SqlParameter("@GPS_Coordinates", GPS_Coordinates),
               New SqlParameter("@Radius", Radius),
               New SqlParameter("@FromDate", FromDate),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)),
               New SqlParameter("@IsTemporary", IsTemporary),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal AllowedGPSId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Allowed_GPSCoordinates_Delete, New SqlParameter("@AllowedGPSId", AllowedGPSId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal AllowedGPSId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Allowed_GPSCoordinates_Select, New SqlParameter("@AllowedGPSId", AllowedGPSId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Allowed_GPSCoordinates_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Inner() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Allowed_GPSCoordinates_Select_All_Inner, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace