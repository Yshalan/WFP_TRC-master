Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace VMS

    Public Class DALVisitInfo
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private VisitInfo_Select As String = "VisitInfo_select"
        Private VisitInfo_Select_All As String = "VisitInfo_select_All"
        Private VisitInfo_Insert As String = "VisitInfo_Insert"
        Private VisitInfo_Update As String = "VisitInfo_Update"
        Private VisitInfo_Delete As String = "VisitInfo_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef VisitId As Integer, ByVal FK_DepartmentId As Integer, ByVal FK_EmployeeId As Integer, ByVal ReasonOfVisit As String, ByVal ExpectedCheckInTime As DateTime, ByVal ExpectedCheckOutTime As DateTime, ByVal Remarks As String, ByVal IsDeleted As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@VisitId", SqlDbType.BigInt, 16, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, VisitId)
                errNo = objDac.AddUpdateDeleteSPTrans(VisitInfo_Insert, sqlOut,
               New SqlParameter("@FK_DepartmentId", FK_DepartmentId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@ReasonOfVisit", ReasonOfVisit),
               New SqlParameter("@ExpectedCheckInTime", ExpectedCheckInTime),
               New SqlParameter("@ExpectedCheckOutTime", ExpectedCheckOutTime),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@IsDeleted", IsDeleted),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If Not IsDBNull(sqlOut.Value) Then
                    VisitId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal VisitId As Integer, ByVal FK_DepartmentId As Integer, ByVal FK_EmployeeId As Integer, ByVal ReasonOfVisit As String, ByVal ExpectedCheckInTime As DateTime, ByVal ExpectedCheckOutTime As DateTime, ByVal Remarks As String, ByVal IsDeleted As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(VisitInfo_Update, New SqlParameter("@VisitId", VisitId),
               New SqlParameter("@FK_DepartmentId", FK_DepartmentId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@ReasonOfVisit", ReasonOfVisit),
               New SqlParameter("@ExpectedCheckInTime", ExpectedCheckInTime),
               New SqlParameter("@ExpectedCheckOutTime", ExpectedCheckOutTime),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@IsDeleted", IsDeleted),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal VisitId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(VisitInfo_Delete, New SqlParameter("@VisitId", VisitId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal VisitId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(VisitInfo_Select, New SqlParameter("@VisitId", VisitId)).Rows(0)
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
                objColl = objDac.GetDataTable(VisitInfo_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace