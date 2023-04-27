Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Definitions

    Public Class DALPermissionTypeDuration
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private PermissionTypeDuration_Select As String = "PermissionTypeDuration_select"
        Private PermissionTypeDuration_Select_All As String = "PermissionTypeDuration_select_All"
        Private PermissionTypeDuration_Insert As String = "PermissionTypeDuration_Insert"
        Private PermissionTypeDuration_Update As String = "PermissionTypeDuration_Update"
        Private PermissionTypeDuration_Delete As String = "PermissionTypeDuration_Delete"
        Dim PermissionTypeDuration_bulk_insert As String = "PermissionTypeDuration_bulk_insert"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_PermId As Integer, ByVal FK_DurationId As Integer, ByVal MaximumDuration As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeDuration_Insert, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_DurationId", FK_DurationId), _
               New SqlParameter("@MaximumDuration", MaximumDuration))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_PermId As Integer, ByVal FK_DurationId As Integer, ByVal MaximumDuration As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeDuration_Update, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_DurationId", FK_DurationId), _
               New SqlParameter("@MaximumDuration", MaximumDuration))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_PermId As Integer, ByVal FK_DurationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeDuration_Delete, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_DurationId", FK_DurationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_PermId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(PermissionTypeDuration_Select, New SqlParameter("@FK_PermId", FK_PermId))
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
                objColl = objDac.GetDataTable(PermissionTypeDuration_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Add_Bulk(ByVal xml As String, ByVal PermId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeDuration_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@FK_PermId", PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region


    End Class
End Namespace