Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA_SchoolScheduling

    Public Class DALClassGrade
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private ClassGrade_Select As String = "ClassGrade_select"
        Private ClassGrade_Select_All As String = "ClassGrade_select_All"
        Private ClassGrade_Insert As String = "ClassGrade_Insert"
        Private ClassGrade_Update As String = "ClassGrade_Update"
        Private ClassGrade_Delete As String = "ClassGrade_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal ClassGradeName As String, ByVal ClassGradeNameAr As String, ByVal ClassGradeOrder As Integer, ByVal LAST_UPDATE_DATE As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ClassGrade_Insert, New SqlParameter("@ClassGradeName", ClassGradeName), _
               New SqlParameter("@ClassGradeNameAr", ClassGradeNameAr), _
               New SqlParameter("@ClassGradeOrder", ClassGradeOrder), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ClassGradeId As Integer, ByVal ClassGradeName As String, ByVal ClassGradeNameAr As String, ByVal ClassGradeOrder As Integer, ByVal LAST_UPDATE_DATE As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ClassGrade_Update, New SqlParameter("@ClassGradeId", ClassGradeId), _
               New SqlParameter("@ClassGradeName", ClassGradeName), _
               New SqlParameter("@ClassGradeNameAr", ClassGradeNameAr), _
               New SqlParameter("@ClassGradeOrder", ClassGradeOrder), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ClassGradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ClassGrade_Delete, New SqlParameter("@ClassGradeId", ClassGradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ClassGradeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ClassGrade_Select, New SqlParameter("@ClassGradeId", ClassGradeId)).Rows(0)
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
                objColl = objDac.GetDataTable(ClassGrade_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace