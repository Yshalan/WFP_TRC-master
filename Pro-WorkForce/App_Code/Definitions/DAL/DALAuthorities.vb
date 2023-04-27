Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALAuthorities
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Authorities_Select As String = "Authorities_select"
        Private Authorities_Select_All As String = "Authorities_select_All"
        Private Authorities_Insert As String = "Authorities_Insert"
        Private Authorities_Update As String = "Authorities_Update"
        Private Authorities_Delete As String = "Authorities_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef AuthorityId As Integer, ByVal AuthorityCode As String, ByVal AuthorityName As String, ByVal AuthorityArabicName As String, ByVal Active As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@AuthorityId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, AuthorityId)
                errNo = objDac.AddUpdateDeleteSPTrans(Authorities_Insert, sqlOut, New SqlParameter("@AuthorityCode", AuthorityCode), _
               New SqlParameter("@AuthorityName", AuthorityName), _
               New SqlParameter("@AuthorityArabicName", AuthorityArabicName), _
               New SqlParameter("@Active", Active), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                If errNo = 0 Then
                    AuthorityId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal AuthorityId As Integer, ByVal AuthorityCode As String, ByVal AuthorityName As String, ByVal AuthorityArabicName As String, ByVal Active As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Authorities_Update, New SqlParameter("@AuthorityId", AuthorityId), _
               New SqlParameter("@AuthorityCode", AuthorityCode), _
               New SqlParameter("@AuthorityName", AuthorityName), _
               New SqlParameter("@AuthorityArabicName", AuthorityArabicName), _
               New SqlParameter("@Active", Active), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal AuthorityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Authorities_Delete, New SqlParameter("@AuthorityId", AuthorityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal AuthorityId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Authorities_Select, New SqlParameter("@AuthorityId", AuthorityId)).Rows(0)
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
                objColl = objDac.GetDataTable(Authorities_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace