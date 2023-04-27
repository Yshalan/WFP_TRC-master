Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.LookUp
Imports SmartV.UTILITIES
Imports SmartV.DB


Namespace TA.Definitions

    Public Class DALEmpMaritalStatus
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_MaritalStatus_Select As String = "Emp_MaritalStatus_select"
        Private Emp_MaritalStatus_Select_All As String = "Emp_MaritalStatus_select_All"
        Private Emp_MaritalStatus_Insert As String = "Emp_MaritalStatus_Insert"
        Private Emp_MaritalStatus_Update As String = "Emp_MaritalStatus_Update"
        Private Emp_MaritalStatus_Delete As String = "Emp_MaritalStatus_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef MaritalStatusId As Integer, ByVal MaritalStatusCode As String, ByVal MatitalStatusName As String, ByVal MaritalStatusArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MaritalStatusId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MaritalStatusId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MaritalStatus_Insert, sqlOut, New SqlParameter("@MaritalStatusCode", MaritalStatusCode), _
               New SqlParameter("@MatitalStatusName", MatitalStatusName), _
               New SqlParameter("@MaritalStatusArabicName", MaritalStatusArabicName))
                If errNo = 0 Then
                    MaritalStatusId = sqlOut.Value
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal MaritalStatusId As Integer, ByVal MaritalStatusCode As String, ByVal MatitalStatusName As String, ByVal MaritalStatusArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MaritalStatus_Update, New SqlParameter("@MaritalStatusId", MaritalStatusId), _
               New SqlParameter("@MaritalStatusCode", MaritalStatusCode), _
               New SqlParameter("@MatitalStatusName", MatitalStatusName), _
               New SqlParameter("@MaritalStatusArabicName", MaritalStatusArabicName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal MaritalStatusId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_MaritalStatus_Delete, New SqlParameter("@MaritalStatusId", MaritalStatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal MaritalStatusId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_MaritalStatus_Select, New SqlParameter("@MaritalStatusId", MaritalStatusId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_MaritalStatus_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace