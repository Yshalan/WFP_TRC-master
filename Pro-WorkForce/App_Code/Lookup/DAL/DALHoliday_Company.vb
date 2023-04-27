Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES

Namespace TA.Lookup
    Public Class DALHoliday_Company
        Inherits MGRBase
#Region "Class Variables"
        Private strConn As String
        Dim Holiday_Company_insert As String = "Holiday_Company_insert"
        Dim Holiday_Company_SelectAll As String = "Holiday_Company_SelectAll"
        Dim Holiday_Company_Select As String = "Holiday_Company_Select"
        Dim Holiday_Company_Update As String = "Holiday_Company_Update"
        Dim Holiday_Company_Delete As String = "Holiday_Company_Delete"
        Dim OrgCompany_Select_AllForDDL As String = "OrgCompany_Select_AllForDDL"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"

        Public Function Add(ByVal HolidayId As Integer, ByVal CompanyId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Company_insert, _
               New SqlParameter("@HolidayId", HolidayId), _
               New SqlParameter("@CompanyId", CompanyId))
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
                objColl = objDac.GetDataTable(Holiday_Company_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function GetAll_OrgCompany() As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(OrgCompany_Select_AllForDDL, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update(ByVal HolidayId As Integer, ByVal CompanyId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Company_Update, _
               New SqlParameter("@HolidayId", HolidayId), _
               New SqlParameter("@CompanyId", CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal HolidayId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_Company_Delete, _
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
                objColl = objDac.GetDataTable(Holiday_Company_Select, New SqlParameter("@HolidayId", HolidayId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function


#End Region
    End Class
End Namespace