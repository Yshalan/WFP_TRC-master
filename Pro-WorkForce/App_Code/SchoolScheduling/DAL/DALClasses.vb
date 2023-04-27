Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA_SchoolScheduling

    Public Class DALClasses
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Class_Select As String = "Class_select"
        Private Class_Select_All As String = "Class_select_All"
        Private Class_Insert As String = "Class_Insert"
        Private Class_Update As String = "Class_Update"
        Private Class_Delete As String = "Class_Delete"
        Private Class_Select_All_ByClassGrade As String = "Class_Select_All_ByClassGrade"
        Private Class_Select_All_ByClassGradeId As String = "Class_Select_All_ByClassGradeId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ClassGradeId As Integer, ByVal ClassName As String, ByVal ClassNameAr As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Class_Insert, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@ClassName", ClassName), _
               New SqlParameter("@ClassNameAr", ClassNameAr), _
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

        Public Function Update(ByVal ClassId As Integer, ByVal FK_ClassGradeId As Integer, ByVal ClassName As String, ByVal ClassNameAr As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Class_Update, New SqlParameter("@ClassId", ClassId), _
               New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@ClassName", ClassName), _
               New SqlParameter("@ClassNameAr", ClassNameAr), _
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

        Public Function Delete(ByVal ClassId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Class_Delete, New SqlParameter("@ClassId", ClassId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ClassId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Class_Select, New SqlParameter("@ClassId", ClassId)).Rows(0)
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
                objColl = objDac.GetDataTable(Class_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_ByClassGrade(ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Class_Select_All_ByClassGrade, New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetAll_ByClassGradeId(ByVal FK_ClassGradeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Class_Select_All_ByClassGradeId, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace