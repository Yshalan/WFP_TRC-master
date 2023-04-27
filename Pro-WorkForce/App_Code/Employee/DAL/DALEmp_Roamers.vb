Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALEmp_Roamers
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Roamers_Select As String = "Emp_Roamers_select"
        Private Emp_Roamers_Select_All As String = "Emp_Roamers_select_All"
        Private Emp_Roamers_Insert As String = "Emp_Roamers_Insert"
        Private Emp_Roamers_Update As String = "Emp_Roamers_Update"
        Private Emp_Roamers_Delete As String = "Emp_Roamers_Delete"
        Private Roamers_Get_Emp_Details As String = "Emp_Roamers_Get_Emp_Details"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Function Get_Emp_Roamers_Details() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Roamers_Get_Emp_Details, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Add(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Roamers_Insert, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@IsTemporary", IsTemporary), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal RoamerId As Integer, ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Roamers_Update, New SqlParameter("@RoamerId", RoamerId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@IsTemporary", IsTemporary), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal RoamerId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Roamers_Delete, New SqlParameter("@RoamerId", RoamerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal RoamerId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Roamers_Select, New SqlParameter("@RoamerId", RoamerId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Roamers_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace