Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.Visitors

    Public Class DALBlackListVisitors
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private BlackListVisitors_Select As String = "BlackListVisitors_select"
        Private BlackListVisitors_Select_All As String = "BlackListVisitors_select_All"
        Private BlackListVisitors_Insert As String = "BlackListVisitors_Insert"
        Private BlackListVisitors_Update As String = "BlackListVisitors_Update"
        Private BlackListVisitors_Delete As String = "BlackListVisitors_Delete"
        Private BlackListVisitors_Select_AllInner As String = "BlackListVisitors_Select_AllInner"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef BlacklistId As Long, ByVal IDNumber As String, ByVal VisitorName As String, ByVal VisitorArabicName As String, ByVal Nationality As Integer, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@BlacklistId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, BlacklistId)
                errNo = objDac.AddUpdateDeleteSPTrans(BlackListVisitors_Insert, sqlOut, New SqlParameter("@IDNumber", IDNumber),
               New SqlParameter("@VisitorName", VisitorName),
               New SqlParameter("@VisitorArabicName", VisitorArabicName),
               New SqlParameter("@Nationality", Nationality),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then BlacklistId = sqlOut.Value Else BlacklistId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal BlacklistId As Long, ByVal IDNumber As String, ByVal VisitorName As String, ByVal VisitorArabicName As String, ByVal Nationality As Integer, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BlackListVisitors_Update, New SqlParameter("@BlacklistId", BlacklistId),
               New SqlParameter("@IDNumber", IDNumber),
               New SqlParameter("@VisitorName", VisitorName),
               New SqlParameter("@VisitorArabicName", VisitorArabicName),
               New SqlParameter("@Nationality", Nationality),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal BlacklistId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BlackListVisitors_Delete, New SqlParameter("@BlacklistId", BlacklistId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal BlacklistId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(BlackListVisitors_Select, New SqlParameter("@BlacklistId", BlacklistId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll_Inner() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(BlackListVisitors_Select_AllInner, Nothing)
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
                objColl = objDac.GetDataTable(BlackListVisitors_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace