Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Definitions

    Public Class DALSemesters
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Semesters_Select As String = "Semesters_select"
        Private Semesters_Select_All As String = "Semesters_select_All"
        Private Semesters_Insert As String = "Semesters_Insert"
        Private Semesters_Update As String = "Semesters_Update"
        Private Semesters_Delete As String = "Semesters_Delete"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal SemesterName As String, ByVal SemesterArabicName As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Semesters_Insert, New SqlParameter("@SemesterName", SemesterName), _
               New SqlParameter("@SemesterArabicName", SemesterArabicName), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal SemesterId As Integer, ByVal SemesterName As String, ByVal SemesterArabicName As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Semesters_Update, New SqlParameter("@SemesterId", SemesterId), _
               New SqlParameter("@SemesterName", SemesterName), _
               New SqlParameter("@SemesterArabicName", SemesterArabicName), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal SemesterId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Semesters_Delete, New SqlParameter("@SemesterId", SemesterId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal SemesterId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Semesters_Select, New SqlParameter("@SemesterId", SemesterId)).Rows(0)
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
                objColl = objDac.GetDataTable(Semesters_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace