Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES

Namespace TA.Lookup
    Public Class DALHoliday_WorkLocation
        Inherits MGRBase
#Region "Class Variables"
        Private strConn As String
        Dim Holiday_WorkLocation_insert As String = "Holiday_WorkLocation_insert"
        Dim Holiday_WorkLocation_SelectAll As String = "Holiday_WorkLocation_SelectAll"
        Dim Holiday_WorkLocation_Select As String = "Holiday_WorkLocation_Select"
        Dim Holiday_WorkLocation_Update As String = "Holiday_WorkLocation_Update"
        Dim Holiday_WorkLocation_Delete As String = "Holiday_WorkLocation_Delete"
        Dim Emp_WorkLocation_Select_All_ForList As String = "Emp_WorkLocation_Select_All_ForList"
        Dim Emp_WorkLocation_Select_Specific As String = "Emp_WorkLocation_Select_Specific"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"

        Public Function Add(ByVal HolidayId As Integer, ByVal WorkLocationId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_WorkLocation_insert, _
               New SqlParameter("@HolidayId", HolidayId), _
               New SqlParameter("@WorkLocationId", WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetAll() As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Holiday_WorkLocation_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function GetAll_WorkLocations() As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_Select_All_ForList, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetSpecific_WorkLocations(ByVal CompaniesIds As String) As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_Select_Specific, New SqlParameter("@CompaniesIds", CompaniesIds))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update(ByVal HolidayId As Integer, ByVal WorkLocationId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_WorkLocation_Update, _
               New SqlParameter("@HolidayId", HolidayId), _
               New SqlParameter("@WorkLocationId", WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal HolidayId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_WorkLocation_Delete, _
               New SqlParameter("@HolidayId", HolidayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByPK(ByVal HolidayId As Integer) As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Holiday_WorkLocation_Select, New SqlParameter("@HolidayId", HolidayId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function


#End Region
    End Class
End Namespace