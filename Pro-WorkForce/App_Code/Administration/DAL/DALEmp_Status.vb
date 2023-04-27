Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports SmartV.DB
Imports TA.LookUp
Imports SmartV.UTILITIES


Namespace TA.Admin

    Public Class DALEmp_Status
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Status_Select As String = "Emp_Status_select"
        Private Emp_Status_Select_All As String = "Emp_Status_select_All"
        Private Emp_Status_Insert As String = "Emp_Status_Insert"
        Private Emp_Status_Update As String = "Emp_Status_Update"
        Private Emp_Status_Delete As String = "Emp_Status_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef StatusId As Integer, ByVal statusCode As String, ByVal StatusName As String, ByVal StatusArabicName As String, ByVal StatusDescription As String, ByVal CosiderEmployeeActive As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@StatusId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, Statusid)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Status_Insert, sqlOut, New SqlParameter("@statusCode", statusCode), _
               New SqlParameter("@StatusName", StatusName), _
               New SqlParameter("@StatusArabicName", StatusArabicName), _
               New SqlParameter("@StatusDescription", StatusDescription), _
               New SqlParameter("@CosiderEmployeeActive", CosiderEmployeeActive))
                If errNo = 0 Then StatusId = sqlOut.Value Else StatusId = 0
                'StatusId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal StatusId As Integer, ByVal statusCode As String, ByVal StatusName As String, ByVal StatusArabicName As String, ByVal StatusDescription As String, ByVal CosiderEmployeeActive As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Status_Update, New SqlParameter("@StatusId", StatusId), _
               New SqlParameter("@statusCode", statusCode), _
               New SqlParameter("@StatusName", StatusName), _
               New SqlParameter("@StatusArabicName", StatusArabicName), _
               New SqlParameter("@StatusDescription", StatusDescription), _
               New SqlParameter("@CosiderEmployeeActive", CosiderEmployeeActive))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal StatusId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Status_Delete, New SqlParameter("@StatusId", StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal StatusId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Status_Select, New SqlParameter("@StatusId", StatusId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Status_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace