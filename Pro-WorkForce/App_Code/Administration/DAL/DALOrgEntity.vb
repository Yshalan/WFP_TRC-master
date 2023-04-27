Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Admin
    Public Class DALOrgEntity
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private OrgEntity_Select As String = "OrgEntity_select"
        Private OrgEntity_Select_All As String = "OrgEntity_select_All"
        Private OrgEntity_Insert As String = "OrgEntity_Insert"
        Private OrgEntity_Update As String = "OrgEntity_Update"
        Private OrgEnity_Select_AllByParent As String = "OrgEnity_Select_AllByParent"
        Private OrgEntity_Delete As String = "OrgEntity_Delete"
        Private OrgEntity_Select_All_ForDDL As String = "OrgEntity_Select_All_ForDDL"
        Private OrgEnity_Select_AllByCompany As String = "OrgEnity_Select_AllByCompany"
        Private OrgEntity_GetParentName As String = "OrgEntity_GetParentName"
        Private OrgEntity_CheckChildEntityexists As String = "OrgEntity_CheckChildEntityexists"
        Private OrgEntity_GetDefaultPolicyForFirstLevel As String = "OrgEntity_GetDefaultPolicyForFirstLevel"
        Private OrgEntity_Select_All_ForDDL_ByCompany As String = "OrgEntity_Select_All_ForDDL_ByCompany"
        Private Get_Employees_By_OrgEntity As String = "Get_Employees_By_OrgEntity"
        Private OrgEnity_Select_EntityAndChilds As String = "OrgEnity_Select_EntityAndChilds"
        Private OrgEnity_Select_AllByCompanyAndUserId As String = "OrgEnity_Select_AllByCompanyAndUserId"
        Public intEntityID As Integer = 0
        Private OrgEntity_GetParentManageId_ByEntityId As String = "OrgEntity_GetParentManageId_ByEntityId"
        Private OrgEntity_Get_ChildEntity_ManagerId As String = "OrgEntity_Get_ChildEntity_ManagerId"
        Private OrgEnity_Select_AllByCompanyIsManager As String = "OrgEnity_Select_AllByCompanyIsManager"
        Private OrgEnity_Select_AllByCompanyIsManagerForForce As String = "OrgEnity_Select_AllByCompanyIsManagerforForce"
        Private OrgEnity_Select_AllByManager As String = "OrgEnity_Select_AllByManager"
        Private OrgEntity_SelectAll_WithCompanyName As String = "OrgEntity_SelectAll_WithCompanyName"
        Private OrgEntity_Select_By_FK_ManagerId As String = "OrgEntity_Select_By_FK_ManagerId"
        Private OrgEntity_SelectAll_By_FK_ManagerId As String = "OrgEntity_SelectAll_By_FK_ManagerId"
        Private OrgEntity_Select_ByCompany_FK_LevelId As String = "OrgEntity_Select_ByCompany_FK_LevelId"
        Private OrgEnity_Select_AllByCompany_Chart As String = "OrgEnity_Select_AllByCompany_Chart"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub
#End Region

#Region "Methods"
        Public Function Add(ByVal EntityCode As String, ByVal FK_CompanyId As Integer, ByVal FK_ParentId As Integer, ByVal EntityName As String, ByVal EntityArabicName As String, ByVal FK_DefaultPolicyId As Integer, ByVal FK_LevelId As Integer, ByVal FK_ManagerId As Integer, ByVal FK_HighestPost As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer
            Dim sp1 As New SqlParameter("@EntityID", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgEntity_Insert, New SqlParameter("@EntityCode", EntityCode), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_ParentId", FK_ParentId), _
               New SqlParameter("@EntityName", EntityName), _
               New SqlParameter("@EntityArabicName", EntityArabicName), _
               New SqlParameter("@FK_DefaultPolicyId", FK_DefaultPolicyId), _
               New SqlParameter("@FK_LevelId", FK_LevelId), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HighestPost", FK_HighestPost), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), sp1)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            intEntityID = sp1.Value
            Return errNo

        End Function
        Public Function Update(ByVal EntityId As Integer, ByVal EntityCode As String, ByVal FK_CompanyId As Integer, ByVal FK_ParentId As Integer, ByVal EntityName As String, ByVal EntityArabicName As String, ByVal FK_DefaultPolicyId As Integer, ByVal FK_LevelId As Integer, ByVal FK_ManagerId As Integer, ByVal FK_HighestPost As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgEntity_Update, New SqlParameter("@EntityId", EntityId), _
               New SqlParameter("@EntityCode", EntityCode), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_ParentId", FK_ParentId), _
               New SqlParameter("@EntityName", EntityName), _
               New SqlParameter("@EntityArabicName", EntityArabicName), _
               New SqlParameter("@FK_DefaultPolicyId", FK_DefaultPolicyId), _
               New SqlParameter("@FK_LevelId", FK_LevelId), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_HighestPost", FK_HighestPost), _
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
        Public Function Delete(ByVal EntityId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgEntity_Delete, New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function GetByPK(ByVal EntityId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OrgEntity_Select, New SqlParameter("@EntityId", EntityId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
        'Get_Employees_By_OrgEntity
        Public Function GetEmployeesByOrgEntity(ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Employees_By_OrgEntity, New SqlParameter("@EntityId", EntityId))
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
                objColl = objDac.GetDataTable(OrgEntity_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_WithCompanyName(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEntity_SelectAll_WithCompanyName, New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function CheckChildEntityexists(ByVal intEntityId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim errNo As Integer = -1
            Try
                errNo = objDac.GetSingleValue(Of Integer)(OrgEntity_CheckChildEntityexists, New SqlParameter("@EntityId", intEntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo


        End Function
        Public Function GetAllForDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEntity_Select_All_ForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        'OrgEntity_Select_All_ForDDL_ByCompany
        Public Function SelectAllForDDLByCompany(ByVal intCompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEntity_Select_All_ForDDL_ByCompany, New SqlParameter("@CompanyId", intCompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllchildsByParent(ByVal intFK_ParentId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByParent, New SqlParameter("@FK_ParentId", intFK_ParentId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEntityAndChilds(ByVal intFK_ParentId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_EntityAndChilds, New SqlParameter("@EntityId", intFK_ParentId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEntityByCompany(ByVal intFK_Company As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByCompany, New SqlParameter("@FK_CompanyId", intFK_Company))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEntityByCompany_Chart(ByVal intFK_Company As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByCompany_Chart, New SqlParameter("@FK_CompanyId", intFK_Company))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEntityBy_CompanyandLevel(ByVal intFK_Company As Integer, ByVal FK_LevelId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEntity_Select_ByCompany_FK_LevelId, New SqlParameter("@FK_CompanyId", intFK_Company), _
                                              New SqlParameter("FK_LevelId", FK_LevelId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEntityByCompanyAndByUserId(ByVal intFK_Company As Integer, ByVal FK_UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByCompanyAndUserId, New SqlParameter("@FK_CompanyId", intFK_Company), New SqlParameter("@FK_UserId", FK_UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetParentNameByEntityID(ByVal EntityId As Integer) As String

            objDac = DAC.getDAC()
            Dim objColl As String = ""
            Try
                objColl = objDac.GetSingleValue(Of String)(OrgEntity_GetParentName, New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Function GetDefaultPolicy(ByVal CompanyID As Integer) As Integer

            objDac = DAC.getDAC()
            Dim RetVal As Integer
            Try
                RetVal = objDac.GetSingleValue(Of Integer)("OrgEntity_GetDefaultPolicyID", New SqlParameter("@CompanyID", CompanyID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return RetVal
        End Function

        Public Function GetParentManager_ByEntityId(ByVal EntityId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OrgEntity_GetParentManageId_ByEntityId, New SqlParameter("@EntityId", EntityId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetChildManager_ByEntityId(ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEntity_Get_ChildEntity_ManagerId, New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEntityByCompanyAndManger(ByVal FK_CompanyId As Integer, ByVal Fk_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByCompanyIsManager, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@Fk_ManagerId", Fk_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEntityByCompanyAndMangerforForce(ByVal FK_CompanyId As Integer, ByVal Fk_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByCompanyIsManagerForForce, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@Fk_ManagerId", Fk_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllEntityByManger(ByVal Fk_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEnity_Select_AllByManager, New SqlParameter("@Fk_ManagerId", Fk_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function Get_EntityBy_FK_MangerId(ByVal Fk_ManagerId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OrgEntity_Select_By_FK_ManagerId, New SqlParameter("@Fk_ManagerId", Fk_ManagerId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAllEntitysByManger(ByVal Fk_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgEntity_SelectAll_By_FK_ManagerId, New SqlParameter("@Fk_ManagerId", Fk_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace