Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.ScheduleGroups

    Public Class DALLeaveSubstitute
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private LeaveSubstitute_Select As String = "LeaveSubstitute_select"
        Private LeaveSubstitute_Select_All As String = "LeaveSubstitute_select_All"
        Private LeaveSubstitute_Insert As String = "LeaveSubstitute_Insert"
        Private LeaveSubstitute_Update As String = "LeaveSubstitute_Update"
        Private LeaveSubstitute_Delete As String = "LeaveSubstitute_Delete"
        Private LeaveSubstitute_Select_All_Pending As String = "LeaveSubstitute_Select_All_Pending"
        Private LeaveSubstitute_Select_All_Confirmed As String = "LeaveSubstitute_Select_All_Confirmed"
        Private LeaveSubstitute_SelectByFK_EmployeeId As String = "LeaveSubstitute_SelectByFK_EmployeeId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Integer, ByVal LeaveDate As DateTime, ByVal FK_ShiftId As Integer, ByVal SubstituteDate As DateTime, ByVal IsConfirmed As Boolean, ByVal ConfirmSubstituteDate As DateTime, ByVal Confirmed_By As String, ByVal ModifiedDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LeaveSubstitute_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@LeaveDate", LeaveDate), _
               New SqlParameter("@FK_ShiftId", FK_ShiftId), _
               New SqlParameter("@SubstituteDate", SubstituteDate), _
               New SqlParameter("@IsConfirmed", IsConfirmed), _
               New SqlParameter("@ConfirmSubstituteDate", ConfirmSubstituteDate), _
               New SqlParameter("@Confirmed_By", Confirmed_By), _
               New SqlParameter("@ModifiedDate", ModifiedDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal SubstituteId As Integer, ByVal IsConfirmed As Boolean, ByVal ConfirmSubstituteDate As DateTime, ByVal Confirmed_By As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LeaveSubstitute_Update, New SqlParameter("@SubstituteId", SubstituteId), _
               New SqlParameter("@IsConfirmed", IsConfirmed), _
               New SqlParameter("@ConfirmSubstituteDate", ConfirmSubstituteDate), _
               New SqlParameter("@Confirmed_By", Confirmed_By))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal SubstituteId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(LeaveSubstitute_Delete, New SqlParameter("@SubstituteId", SubstituteId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal SubstituteId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(LeaveSubstitute_Select, New SqlParameter("@SubstituteId", SubstituteId)).Rows(0)
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
                objColl = objDac.GetDataTable(LeaveSubstitute_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Pending(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(LeaveSubstitute_Select_All_Pending, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Confirmed(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(LeaveSubstitute_Select_All_Confirmed, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_ByEmployeeId(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(LeaveSubstitute_SelectByFK_EmployeeId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace