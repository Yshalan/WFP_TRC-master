Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Security

    Public Class DALSYS_Users_Security
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private SYS_Users_Security_Select As String = "SYS_Users_Security_select"
        Private SYS_Users_Security_Select_All As String = "SYS_Users_Security_select_All"
        Private SYS_Users_Security_Insert As String = "SYS_Users_Security_Insert"
        Private SYS_Users_Security_Update As String = "SYS_Users_Security_Update"
        Private SYS_Users_Security_Delete As String = "SYS_Users_Security_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_UserId As Integer, ByVal FK_SecurityLevel As Integer, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                If (FK_CompanyId <> 0 And FK_EntityId <> 0) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Insert, New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@FK_SecurityLevel", FK_SecurityLevel), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_EntityId", FK_EntityId))
                ElseIf (FK_CompanyId <> 0 And FK_EntityId = 0) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Insert, New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@FK_SecurityLevel", FK_SecurityLevel), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
                ElseIf (FK_CompanyId = 0 And FK_EntityId = 0) Then

                    errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Insert, New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@FK_SecurityLevel", FK_SecurityLevel))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_UserId As Integer, ByVal FK_SecurityLevel As Integer, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                If (FK_CompanyId <> 0 And FK_EntityId <> 0) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Update, New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@FK_SecurityLevel", FK_SecurityLevel), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_EntityId", FK_EntityId))
                ElseIf (FK_CompanyId <> 0 And FK_EntityId = 0) Then
                    errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Update, New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@FK_SecurityLevel", FK_SecurityLevel), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
                ElseIf (FK_CompanyId = 0 And FK_EntityId = 0) Then

                    errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Update, New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@FK_SecurityLevel", FK_SecurityLevel))
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_UserId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(SYS_Users_Security_Delete, New SqlParameter("@FK_UserId", FK_UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_UserId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(SYS_Users_Security_Select, New SqlParameter("@FK_UserId", FK_UserId)).Rows(0)
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
                objColl = objDac.GetDataTable(SYS_Users_Security_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function VerifyIfUserHasRights(UserId As Integer, Url As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("SysForms_VerifyUserHasRights", New SqlParameter("@UserId", UserId), New SqlParameter("@Url", Url))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region


    End Class
End Namespace