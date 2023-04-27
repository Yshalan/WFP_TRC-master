Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Definitions
    Public Class DALHoliday
        Inherits MGRBase
#Region "Class Variables"
        Private strConn As String
        Dim Holiday_insert As String = "Holiday_insert"
        Dim Holiday_SelectAll As String = "Holiday_Select_All"
        Dim Emp_Get_Holidays As String = "Emp_Get_Holidays"
        Dim Holiday_Select As String = "Holiday_Select"
        Dim Holiday_Update As String = "Holiday_Update"
        Dim Holiday_Delete As String = "Holiday_Delete"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"

        Public Function Add(ByRef HolidayId As Integer, ByVal HolidayName As String, ByVal HolidayArabicName As String, ByVal isYearlyFixed As Boolean, ByVal StartDay As Integer, ByVal StartMonth As Integer, ByVal StartYear As Integer, ByVal EndDay As Integer, ByVal EndMonth As Integer, ByVal EndYear As Integer, ByVal IsCompanyApplicable As Boolean, ByVal IsWorkLocation As Boolean, ByVal IsReligion As Boolean, ByVal CREATED_BY As String, ByVal IsEmployeeType As Boolean, ByVal IsLogicalGroup As Boolean, ByVal IsReligionRelated As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@HolidayId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, HolidayId)
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_insert,
                New SqlParameter("@HolidayName", HolidayName),
               New SqlParameter("@HolidayArabicName", HolidayArabicName),
               New SqlParameter("@isYearlyFixed", isYearlyFixed),
               New SqlParameter("@StartDay", StartDay),
               New SqlParameter("@StartMonth", StartMonth),
               New SqlParameter("@StartYear", StartYear),
               New SqlParameter("@EndDay", EndDay),
               New SqlParameter("@EndMonth", EndMonth),
               New SqlParameter("@EndYear", EndYear),
               New SqlParameter("@IsCompanyApplicable", IsCompanyApplicable),
               New SqlParameter("@IsWorkLocation", IsWorkLocation),
               New SqlParameter("@IsReligion", IsReligion),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@IsEmployeeType", IsEmployeeType),
               New SqlParameter("@IsLogicalGroup", IsLogicalGroup),
               New SqlParameter("@IsReligionRelated", IsReligionRelated), sqlOut)
                If errNo = 0 Then HolidayId = sqlOut.Value Else HolidayId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetAll() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Holiday_SelectAll, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Emp_GetHolidays(ByVal EmployeeId As Integer, ByVal HolidayDate As Date) As Integer
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Get_Holidays, New SqlParameter("@EmployeeId", EmployeeId), _
               New SqlParameter("@DayDate", HolidayDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            If (objColl IsNot Nothing And objColl.Rows.Count > 0) Then
                Return objColl.Rows(0)(0)
            Else
                Return 0
            End If
            Return 0
        End Function

        Public Function Update(ByVal HolidayId As Integer, ByVal HolidayName As String, ByVal HolidayArabicName As String, ByVal isYearlyFixed As Boolean, ByVal StartDay As Integer, ByVal StartMonth As Integer, ByVal StartYear As Integer, ByVal EndDay As Integer, ByVal EndMonth As Integer, ByVal EndYear As Integer, ByVal IsCompanyApplicable As Boolean, ByVal IsWorkLocation As Boolean, ByVal IsReligion As Boolean, ByVal LAST_UPDATE_BY As String, ByVal IsEmployeeType As Boolean, ByVal IsLogicalGroup As Boolean, ByVal IsReligionRelated As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Update,
               New SqlParameter("@HolidayId", HolidayId),
               New SqlParameter("@HolidayName", HolidayName),
               New SqlParameter("@HolidayArabicName", HolidayArabicName),
               New SqlParameter("@isYearlyFixed", isYearlyFixed),
               New SqlParameter("@StartDay", StartDay),
               New SqlParameter("@StartMonth", StartMonth),
               New SqlParameter("@StartYear", StartYear),
               New SqlParameter("@EndDay", EndDay),
               New SqlParameter("@EndMonth", EndMonth),
               New SqlParameter("@EndYear", EndYear),
               New SqlParameter("@IsCompanyApplicable", IsCompanyApplicable),
               New SqlParameter("@IsWorkLocation", IsWorkLocation),
               New SqlParameter("@IsReligion", IsReligion),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@IsEmployeeType", IsEmployeeType),
               New SqlParameter("@IsLogicalGroup", IsLogicalGroup),
               New SqlParameter("@IsReligionRelated", IsReligionRelated))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal HolidayId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Delete, _
               New SqlParameter("@HolidayId", HolidayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByPK(ByVal HolidayId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Holiday_Select, New SqlParameter("@HolidayId", HolidayId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function


#End Region
    End Class
End Namespace