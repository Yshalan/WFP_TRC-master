Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES

Namespace TA.Lookup
    Public Class DALHoliday_Religion
        Inherits MGRBase
#Region "Class Variables"
        Private strConn As String
        Dim Holiday_Religion_insert As String = "Holiday_Religion_insert"
        Dim Holiday_Religion_SelectAll As String = "Holiday_Religion_SelectAll"
        Dim Holiday_Religion_Select As String = "Holiday_Religion_Select"
        Dim Holiday_Religion_Update As String = "Holiday_Religion_Update"
        Dim Holiday_Religion_Delete As String = "Holiday_Religion_Delete"
        Dim Emp_Religion_Select_All_ForList As String = "Emp_Religion_Select_All_ForList"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"

        Public Function Add(ByVal HolidayId As Integer, ByVal ReligionId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Religion_insert, _
               New SqlParameter("@HolidayId", HolidayId), _
               New SqlParameter("@ReligionId", ReligionId))
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
                objColl = objDac.GetDataTable(Holiday_Religion_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function GetAll_Religion() As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Emp_Religion_Select_All_ForList, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function Update(ByVal HolidayId As Integer, ByVal ReligionId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Religion_Update, _
               New SqlParameter("@HolidayId", HolidayId), _
               New SqlParameter("@ReligionId", ReligionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal HolidayId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Religion_Delete, _
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
                objColl = objDac.GetDataTable(Holiday_Religion_Select, New SqlParameter("@HolidayId", HolidayId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function


#End Region
    End Class
End Namespace