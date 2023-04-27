Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Definitions

    Public Class DALEmp_Nationality
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Nationality_Select As String = "Emp_Nationality_select"
        Private Emp_Nationality_Select_All As String = "Emp_Nationality_select_All"
        Private Emp_Nationality_Insert As String = "Emp_Nationality_Insert"
        Private Emp_Nationality_Update As String = "Emp_Nationality_Update"
        Private Emp_Nationality_Delete As String = "Emp_Nationality_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef NationalityId As Integer, ByVal NationalityCode As String, ByVal NationalityName As String, ByVal NationalityArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@NationalityId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, NationalityId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Nationality_Insert, sqlOut, New SqlParameter("@NationalityCode", NationalityCode), _
               New SqlParameter("@NationalityName", NationalityName), _
               New SqlParameter("@NationalityArabicName", NationalityArabicName))
                If errNo = 0 Then
                    NationalityId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal NationalityId As Integer, ByVal NationalityCode As String, ByVal NationalityName As String, ByVal NationalityArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Nationality_Update, New SqlParameter("@NationalityId", NationalityId), _
               New SqlParameter("@NationalityCode", NationalityCode), _
               New SqlParameter("@NationalityName", NationalityName), _
               New SqlParameter("@NationalityArabicName", NationalityArabicName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal NationalityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Nationality_Delete, New SqlParameter("@NationalityId", NationalityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal NationalityId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Nationality_Select, New SqlParameter("@NationalityId", NationalityId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Nationality_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace