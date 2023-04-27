Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALPermissionTypeOccurance
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private PermissionTypeOccurance_Select As String = "PermissionTypeOccurance_select"
        Private PermissionTypeOccurance_Select_All As String = "PermissionTypeOccurance_select_All"
        Private PermissionTypeOccurance_Insert As String = "PermissionTypeOccurance_Insert"
        Private PermissionTypeOccurance_Update As String = "PermissionTypeOccurance_Update"
        Private PermissionTypeOccurance_Delete As String = "PermissionTypeOccurance_Delete"
        Dim PermissionTypeOccurance_bulk_insert As String = "PermissionTypeOccurance_bulk_insert"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_PermId As Integer, ByVal FK_DurationId As Integer, ByVal MaximumOccur As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeOccurance_Insert, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_DurationId", FK_DurationId), _
               New SqlParameter("@MaximumOccur", MaximumOccur))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_PermId As Integer, ByVal FK_DurationId As Integer, ByVal MaximumOccur As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeOccurance_Update, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_DurationId", FK_DurationId), _
               New SqlParameter("@MaximumOccur", MaximumOccur))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_PermId As Integer, ByVal FK_DurationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeOccurance_Delete, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_DurationId", FK_DurationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_PermId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objRow As DataTable
            Try
                objRow = objDac.GetDataTable(PermissionTypeOccurance_Select, New SqlParameter("@FK_PermId", FK_PermId))
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
                objColl = objDac.GetDataTable(PermissionTypeOccurance_Select_All, Nothing)
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

                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypeOccurance_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@FK_PermId", PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region


    End Class
End Namespace