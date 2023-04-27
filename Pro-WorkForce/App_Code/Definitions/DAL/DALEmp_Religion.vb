Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.UTILITIES
Imports SmartV.DB



Namespace TA.Definitions

    Public Class DALEmp_Religion
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Religion_Select As String = "Emp_Religion_select"
        Private Emp_Religion_Select_All As String = "Emp_Religion_select_All"
        Private Emp_Religion_Insert As String = "Emp_Religion_Insert"
        Private Emp_Religion_Update As String = "Emp_Religion_Update"
        Private Emp_Religion_Delete As String = "Emp_Religion_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ReligionId As Integer, ByVal ReligionCode As String, ByVal ReligionName As String, ByVal ReligionArabicName As String, ByVal Active As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ReligionId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ReligionId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Religion_Insert, sqlOut, New SqlParameter("@ReligionCode", ReligionCode), _
               New SqlParameter("@ReligionName", ReligionName), _
               New SqlParameter("@ReligionArabicName", ReligionArabicName), _
               New SqlParameter("@Active", Active))
                If errNo = 0 Then
                    ReligionId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ReligionId As Integer, ByVal ReligionCode As String, ByVal ReligionName As String, ByVal ReligionArabicName As String, ByVal Active As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Religion_Update, New SqlParameter("@ReligionId", ReligionId), _
               New SqlParameter("@ReligionCode", ReligionCode), _
               New SqlParameter("@ReligionName", ReligionName), _
               New SqlParameter("@ReligionArabicName", ReligionArabicName), _
               New SqlParameter("@Active", Active))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ReligionId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Religion_Delete, New SqlParameter("@ReligionId", ReligionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ReligionId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Religion_Select, New SqlParameter("@ReligionId", ReligionId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Religion_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace