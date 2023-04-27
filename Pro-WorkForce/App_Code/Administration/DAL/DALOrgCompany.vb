Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALOrgCompany
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OrgCompany_Select As String = "OrgCompany_select"
        Private OrgCompany_Select_All As String = "OrgCompany_select_All"
        Private OrgCompany_Insert As String = "OrgCompany_Insert"
        Private OrgCompany_Update As String = "OrgCompany_Update"
        Private OrgCompany_Logo_Update As String = "OrgCompany_Logo_Update"
        Private OrgCompany_Delete As String = "OrgCompany_Delete"
        Private OrgCompany_Select_AllForDDL As String = "OrgCompany_Select_AllForDDL"
        Private OrgCompany_Select_AllByParent As String = "OrgCompany_Select_AllByParent"
        Private OrgCompany_GetCompanyName As String = "OrgCompany_GetCompanyName"
        Private Get_Employees_By_OrgCompany As String = "Get_Employees_By_OrgCompany"
        Private OrgCompany_CheckChildOREntitytExists As String = "OrgCompany_CheckChildOREntitytExists"
        Private OrgCompany_Select_AllForDDL_ByUserId As String = "OrgCompany_Select_AllForDDL_ByUserId"
        Public intCompanyID As Integer
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ParentId As Integer, ByVal CompanyShortName As String, ByVal CompanyName As String, ByVal CompanyArabicName As String, ByVal Country As Integer, ByVal Address As String, ByVal PhoneNumber As String, ByVal Fax As String, ByVal URL As String, ByVal Logo As String, ByVal FK_HighestPost As Integer, ByVal FK_ManagerId As Long, ByVal FK_DefaultPolicyId As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer
            Dim sp1 As New SqlParameter("@CompanyID", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgCompany_Insert, New SqlParameter("@FK_ParentId", FK_ParentId), _
               New SqlParameter("@CompanyShortName", CompanyShortName), _
               New SqlParameter("@CompanyName", CompanyName), _
               New SqlParameter("@CompanyArabicName", CompanyArabicName), _
               New SqlParameter("@Country", Country), _
               New SqlParameter("@Address", Address), _
               New SqlParameter("@PhoneNumber", PhoneNumber), _
               New SqlParameter("@Fax", Fax), _
               New SqlParameter("@URL", URL), _
               New SqlParameter("@Logo", Logo), _
               New SqlParameter("@FK_HighestPost", FK_HighestPost), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_DefaultPolicyId", FK_DefaultPolicyId), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), sp1)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            intCompanyID = sp1.Value
            Return errNo

        End Function

        Public Function Update(ByVal CompanyId As Integer, ByVal FK_ParentId As Integer, ByVal CompanyShortName As String, ByVal CompanyName As String, ByVal CompanyArabicName As String, ByVal Country As Integer, ByVal Address As String, ByVal PhoneNumber As String, ByVal Fax As String, ByVal URL As String, ByVal Logo As String, ByVal FK_HighestPost As Integer, ByVal FK_ManagerId As Long, ByVal FK_DefaultPolicyId As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgCompany_Update, New SqlParameter("@CompanyId", CompanyId), _
               New SqlParameter("@FK_ParentId", FK_ParentId), _
               New SqlParameter("@CompanyShortName", CompanyShortName), _
               New SqlParameter("@CompanyName", CompanyName), _
               New SqlParameter("@CompanyArabicName", CompanyArabicName), _
               New SqlParameter("@Country", Country), _
               New SqlParameter("@Address", Address), _
               New SqlParameter("@PhoneNumber", PhoneNumber), _
               New SqlParameter("@Fax", Fax), _
               New SqlParameter("@URL", URL), _
               New SqlParameter("@Logo", Logo), _
               New SqlParameter("@FK_HighestPost", FK_HighestPost), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FK_DefaultPolicyId", FK_DefaultPolicyId), _
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

        Public Function UpdateLogo(ByVal CompanyId As Integer, ByVal Logo As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgCompany_Logo_Update, New SqlParameter("@CompanyId", CompanyId), _
               New SqlParameter("@Logo", Logo))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function


        Public Function Delete(ByVal CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgCompany_Delete, New SqlParameter("@CompanyId", CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal CompanyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OrgCompany_Select, New SqlParameter("@CompanyId", CompanyId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
        Public Function GetEmployeesByOrgCompany(ByVal intCompanyID As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Employees_By_OrgCompany, New SqlParameter("@CompanyId", intCompanyID))
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
                objColl = objDac.GetDataTable(OrgCompany_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllForDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgCompany_Select_AllForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllforddl_ByUserId(ByVal UserId As Integer, ByVal FilterType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgCompany_Select_AllForDDL_ByUserId, New SqlParameter("@UserId", UserId), New SqlParameter("@FilterType", FilterType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        'CheckChildOREntitytExistsForCompany
        Public Function CheckChildOREntitytExistsForCompany(ByVal intCompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Dim IntVal As Integer
            Try
                IntVal = objDac.GetSingleValue(Of Integer)(OrgCompany_CheckChildOREntitytExists, New SqlParameter("@CompanyId", intCompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return IntVal


        End Function
        Public Function GetAllchildsByParent(ByVal intFK_ParentId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgCompany_Select_AllByParent, New SqlParameter("@FK_ParentId", intFK_ParentId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetCompanyNameByID(ByVal CompanyId As Integer) As String

            objDac = DAC.getDAC()
            Dim objColl As String = ""
            Try
                objColl = objDac.GetSingleValue(Of String)(OrgCompany_GetCompanyName, New SqlParameter("@CompanyId", CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        '
#End Region


    End Class
End Namespace