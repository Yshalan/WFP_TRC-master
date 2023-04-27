Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.LookUp

    Public Class DALHoliday_EmployeeTypes
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Holiday_EmployeeTypes_Select As String = "Holiday_EmployeeTypes_select"
        Private Holiday_EmployeeTypes_Select_All As String = "Holiday_EmployeeTypes_select_All"
        Private Holiday_EmployeeTypes_Insert As String = "Holiday_EmployeeTypes_Insert"
        Private Holiday_EmployeeTypes_Update As String = "Holiday_EmployeeTypes_Update"
        Private Holiday_EmployeeTypes_Delete As String = "Holiday_EmployeeTypes_Delete"
        Private Emp_Type_Select_All_ForList As String = "Emp_Type_Select_All_ForList"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_HolidayId As Integer, ByVal FK_EmployeeTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_EmployeeTypes_Insert, New SqlParameter("@FK_HolidayId", FK_HolidayId), _
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_HolidayId As Integer, ByVal FK_EmployeeTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_EmployeeTypes_Update, New SqlParameter("@FK_HolidayId", FK_HolidayId), _
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_HolidayId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_EmployeeTypes_Delete, New SqlParameter("@FK_HolidayId", FK_HolidayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_HolidayId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objDataTable As DataTable
            Try
                objDataTable = objDac.GetDataTable(Holiday_EmployeeTypes_Select, New SqlParameter("@FK_HolidayId", FK_HolidayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objDataTable

        End Function

        Public Function GetAll_EmployeeType() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Type_Select_All_ForList, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Holiday_EmployeeTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region


    End Class
End Namespace