Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALPermissionTypes_Entity
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private PermissionTypes_Entity_Select As String = "PermissionTypes_Entity_select"
        Private PermissionTypes_Entity_Select_All As String = "PermissionTypes_Entity_select_All"
        Private PermissionTypes_Entity_Insert As String = "PermissionTypes_Entity_Insert"
        Private PermissionTypes_Entity_Update As String = "PermissionTypes_Entity_Update"
        Private PermissionTypes_Entity_Delete As String = "PermissionTypes_Entity_Delete"
        Private PermissionTypes_Entity_GetByPermTypeId As String = "PermissionTypes_Entity_GetByPermTypeId"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EntityId As Integer, ByVal PermissionTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypes_Entity_Insert, New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@PermissionTypeId", PermissionTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EntityId As Integer, ByVal PermissionTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypes_Entity_Update, New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@PermissionTypeId", PermissionTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EntityId As Integer, ByVal PermissionTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(PermissionTypes_Entity_Delete, New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@PermissionTypeId", PermissionTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EntityId As Integer, ByVal PermissionTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(PermissionTypes_Entity_Select, New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@PermissionTypeId", PermissionTypeId)).Rows(0)
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
                objColl = objDac.GetDataTable(PermissionTypes_Entity_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByPermissionTypeId(ByVal PermissionTypeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(PermissionTypes_Entity_GetByPermTypeId, New SqlParameter("@PermissionTypeId", PermissionTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region

    End Class
End Namespace