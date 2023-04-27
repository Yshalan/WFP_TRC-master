Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA_SchoolScheduling

    Public Class DALschoolSchedule
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private schoolSchedule_Select As String = "schoolSchedule_select"
        Private schoolSchedule_Select_All As String = "schoolSchedule_select_All"
        Private schoolSchedule_Insert As String = "schoolSchedule_Insert"
        Private schoolSchedule_Update As String = "schoolSchedule_Update"
        Private schoolSchedule_Delete As String = "schoolSchedule_Delete"
        Private schoolSchedule_CheckIfEmpty As String = "schoolSchedule_CheckIfEmpty"
        Private WeekDays_Select_All_SchoolSchedule As String = "WeekDays_Select_All_SchoolSchedule"
        Private schoolSchedule_FillClassScheduleByClassId As String = "schoolSchedule_FillClassScheduleByClassId"
        Private schoolSchedule_Select_All_ForGrid As String = "schoolSchedule_Select_All_ForGrid"
        Private schoolSchedule_FillClassScheduleByTeacherId As String = "schoolSchedule_FillClassScheduleByTeacherId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal DayId As Integer, ByVal lesson As Integer, ByVal FK_ClassId As Integer, ByVal FK_CourseId As Integer, ByVal FK_TeacherId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(schoolSchedule_Insert, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@lesson", lesson), _
               New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
               New SqlParameter("@FK_TeacherId", FK_TeacherId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal DayId As Integer, ByVal lesson As Integer, ByVal FK_ClassId As Integer, ByVal FK_CourseId As Integer, ByVal FK_TeacherId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(schoolSchedule_Update, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@lesson", lesson), _
               New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
               New SqlParameter("@FK_TeacherId", FK_TeacherId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal DayId As Integer, ByVal lesson As Integer, ByVal FK_ClassId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(schoolSchedule_Delete, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@lesson", lesson), _
               New SqlParameter("@FK_ClassId", FK_ClassId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal DayId As Integer, ByVal lesson As Integer, ByVal FK_ClassId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(schoolSchedule_Select, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@lesson", lesson), _
               New SqlParameter("@FK_ClassId", FK_ClassId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
        Public Function CheckIfEmpty(ByVal DayId As Integer, ByVal lesson As Integer, ByVal FK_ClassId As Integer, ByVal FK_CourseId As Integer, ByVal FK_TeacherID As Integer, ByVal AllowSequential As Boolean, ByVal DistributedBreak As Boolean, ByVal Maxlesson As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            try
                objColl = objDac.GetDataTable(schoolSchedule_CheckIfEmpty, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@lesson", lesson), _
               New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
                 New SqlParameter("@FK_TeacherId", FK_TeacherID), _
                New SqlParameter("@AllowSequential", AllowSequential), _
                 New SqlParameter("@DistributedBreak", DistributedBreak), _
                  New SqlParameter("@Maxlesson", Maxlesson))


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
                objColl = objDac.GetDataTable(schoolSchedule_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
      
       

        Public Function GetAll_ForGrid() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(schoolSchedule_Select_All_ForGrid, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_WeekDays() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(WeekDays_Select_All_SchoolSchedule, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Fill_ByClassId(ByVal FK_ClassId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(schoolSchedule_FillClassScheduleByClassId, New SqlParameter("@FK_ClassId", FK_ClassId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Fill_ByTeacherId(ByVal FK_TeacherId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(schoolSchedule_FillClassScheduleByTeacherId, New SqlParameter("@FK_TeacherId", FK_TeacherId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace