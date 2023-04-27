Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


namespace TA.Definitions

    Public Class DALPermissionsTypes_Company
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private PermissionsTypes_Company_Select As String = "PermissionsTypes_Company_select"
        Private PermissionsTypes_Company_Select_All As String = "PermissionsTypes_Company_select_All"
        Private PermissionsTypes_Company_Insert As String = "PermissionsTypes_Company_Insert"
        Private PermissionsTypes_Company_Update As String = "PermissionsTypes_Company_Update"
        Private PermissionsTypes_Company_Delete As String = "PermissionsTypes_Company_Delete"
        Private PermissionsTypes_Company_Select_ByPermId As String = "PermissionsTypes_Company_Select_ByPermId"
        Private PermissionsTypes_Company_DeleteByPermId As String = "PermissionsTypes_Company_DeleteByPermId"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_PermId As Long, ByVal FK_CompanyId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Company_Insert, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_PermId As Long, ByVal FK_CompanyId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Company_Update, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_PermId As Long, ByVal FK_CompanyId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Company_Delete, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteByPermId(ByVal FK_PermId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Company_DeleteByPermId, New SqlParameter("@FK_PermId", FK_PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_PermId As Long, ByVal FK_CompanyId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(PermissionsTypes_Company_Select, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId)).Rows(0)
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
                objColl = objDac.GetDataTable(PermissionsTypes_Company_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByPermId(ByVal FK_PermId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(PermissionsTypes_Company_Select_ByPermId, New SqlParameter("@FK_PermId", FK_PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region

    End Class
End Namespace