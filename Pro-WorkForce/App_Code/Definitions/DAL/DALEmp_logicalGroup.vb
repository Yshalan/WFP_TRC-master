Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp


Namespace TA.Definitions

    Public Class DALEmp_logicalGroup
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_logicalGroup_Select As String = "Emp_logicalGroup_select"
        Private Emp_logicalGroup_Select_All As String = "Emp_logicalGroup_select_All"
        Private Emp_logicalGroup_Insert As String = "Emp_logicalGroup_Insert"
        Private Emp_logicalGroup_Update As String = "Emp_logicalGroup_Update"
        Private Emp_logicalGroup_Delete As String = "Emp_logicalGroup_Delete"
        Private Emp_logicalGroup_Select_All_By_CompanyId As String = "Emp_logicalGroup_Select_All_By_CompanyId"
        Private Emp_logicalGroup_Select_All_By_CompanyIdAndUserId As String = "Emp_logicalGroup_Select_All_By_CompanyIdAndUserId"
        Private Events_Select_All_ForDDL As String = "Events_Select_All_ForDDL"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef GroupId As Integer, ByVal GroupName As String, ByVal GroupArabicName As String, ByVal FK_TAPolicyId As Integer, ByVal Active As Boolean, ByVal AllowPunchOutSideLocation As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@GroupId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, GroupId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_logicalGroup_Insert, sqlOut, New SqlParameter("@GroupName", GroupName),
               New SqlParameter("@GroupArabicName", GroupArabicName),
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId),
               New SqlParameter("@Active", Active),
               New SqlParameter("@CREATED_BY", 1),
               New SqlParameter("@LAST_UPDATE_BY", 1),
               New SqlParameter("@AllowPunchOutSideLocation", AllowPunchOutSideLocation))
                If errNo = 0 Then
                    GroupId = sqlOut.Value
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal GroupId As Integer, ByVal GroupName As String, ByVal GroupArabicName As String, ByVal FK_TAPolicyId As Integer, ByVal Active As Boolean, ByVal AllowPunchOutSideLocation As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_logicalGroup_Update, New SqlParameter("@GroupId", GroupId),
               New SqlParameter("@GroupName", GroupName),
               New SqlParameter("@GroupArabicName", GroupArabicName),
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId),
               New SqlParameter("@Active", Active),
               New SqlParameter("@LAST_UPDATE_BY", 2),
               New SqlParameter("@AllowPunchOutSideLocation", AllowPunchOutSideLocation))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal GroupId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_logicalGroup_Delete, New SqlParameter("@GroupId", GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal GroupId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_logicalGroup_Select, New SqlParameter("@GroupId", GroupId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_logicalGroup_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByCompany(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_logicalGroup_Select_All_By_CompanyId, New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByCompanyAndUserId(ByVal FK_CompanyId As Integer, ByVal FK_UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_logicalGroup_Select_All_By_CompanyIdAndUserId, New SqlParameter("@FK_CompanyId", FK_CompanyId), New SqlParameter("@FK_UserId", FK_UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_ForDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Events_Select_All_ForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace