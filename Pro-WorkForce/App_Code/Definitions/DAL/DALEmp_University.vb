Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.Definitions

    Public Class DALEmp_University
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_University_Select As String = "Emp_University_select"
        Private Emp_University_Select_All As String = "Emp_University_select_All"
        Private Emp_University_Insert As String = "Emp_University_Insert"
        Private Emp_University_Update As String = "Emp_University_Update"
        Private Emp_University_Delete As String = "Emp_University_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef UniversityId As Integer, ByVal UniversityShortName As String, ByVal UniversityName As String, ByVal UniversityArabicName As String, ByVal Address As String, ByVal PhoneNo As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@UniversityId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, UniversityId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_University_Insert, sqlOut, New SqlParameter("@UniversityShortName", UniversityShortName),
               New SqlParameter("@UniversityName", UniversityName),
               New SqlParameter("@UniversityArabicName", UniversityArabicName),
               New SqlParameter("@Address", Address),
               New SqlParameter("@PhoneNo", PhoneNo),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then UniversityId = sqlOut.Value Else UniversityId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal UniversityId As Integer, ByVal UniversityShortName As String, ByVal UniversityName As String, ByVal UniversityArabicName As String, ByVal Address As String, ByVal PhoneNo As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_University_Update, New SqlParameter("@UniversityId", UniversityId),
               New SqlParameter("@UniversityShortName", UniversityShortName),
               New SqlParameter("@UniversityName", UniversityName),
               New SqlParameter("@UniversityArabicName", UniversityArabicName),
               New SqlParameter("@Address", Address),
               New SqlParameter("@PhoneNo", PhoneNo),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal UniversityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_University_Delete, New SqlParameter("@UniversityId", UniversityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal UniversityId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_University_Select, New SqlParameter("@UniversityId", UniversityId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_University_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace