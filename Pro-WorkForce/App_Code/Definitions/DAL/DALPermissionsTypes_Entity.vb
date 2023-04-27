Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALPermissionsTypes_Entity
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private PermissionsTypes_Entity_Select As String = "PermissionsTypes_Entity_select"
        Private PermissionsTypes_Entity_Select_All As String = "PermissionsTypes_Entity_select_All"
        Private PermissionsTypes_Entity_Insert As String = "PermissionsTypes_Entity_Insert"
        Private PermissionsTypes_Entity_Update As String = "PermissionsTypes_Entity_Update"
        Private PermissionsTypes_Entity_Delete As String = "PermissionsTypes_Entity_Delete"
        Private PermissionsTypes_Entity_Select_ByPermId As String = "PermissionsTypes_Entity_Select_ByPermId"
        Private PermissionsTypes_Entity_DeleteByPermId As String = "PermissionsTypes_Entity_DeleteByPermId"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_PermId As Long, ByVal FK_EntityId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Entity_Insert, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_PermId As Long, ByVal FK_EntityId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Entity_Update, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_PermId As Long, ByVal FK_EntityId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Entity_Delete, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteByPermId(ByVal FK_PermId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionsTypes_Entity_DeleteByPermId, New SqlParameter("@FK_PermId", FK_PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_PermId As Long, ByVal FK_EntityId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(PermissionsTypes_Entity_Select, New SqlParameter("@FK_PermId", FK_PermId), _
               New SqlParameter("@FK_EntityId", FK_EntityId)).Rows(0)
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
                objColl = objDac.GetDataTable(PermissionsTypes_Entity_Select_All, Nothing)
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
                objColl = objDac.GetDataTable(PermissionsTypes_Entity_Select_ByPermId, New SqlParameter("@FK_PermId", FK_PermId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace