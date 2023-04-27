Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp

Namespace TA.Accounts

    Public Class DALBU_Accounts_Master
        Inherits MGRBase
        Dim FK_AccountId As Integer


#Region "Class Variables"
        Private strConn As String
        Private BU_Accounts_Master_Select As String = "BU_Accounts_Master_select"
        Private BU_Accounts_Master_Select_All As String = "BU_Accounts_Master_select_All"
        Private BU_Accounts_Master_Insert As String = "BU_Accounts_Master_Insert"
        Private BU_Accounts_Master_Update As String = "BU_Accounts_Master_Update"
        Private BU_Accounts_Master_Delete As String = "BU_Accounts_Master_Delete"
        Private BU_Accounts_Master_SelectByType As String = "BU_Accounts_Master_SelectByType"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal AccountNo As String, ByVal FK_AccountTypeId As Integer, ByVal AccountName As String, ByVal FK_CountryId As Integer, ByVal City As String, ByVal Address As String, ByVal PoBox As String, ByVal Telephone As String, ByVal Fax As String, ByVal Email As String, ByVal Website As String, ByVal Remarks As String, ByVal MOHLicense As Boolean, ByVal MOHLicenseNo As String, ByVal MOHLicenseIssueDate As String, ByVal MOHLicenseExpiryDate As String, ByVal MOERegNo As String, ByVal MOERegDate As String, ByVal OldAgentNo As Integer, ByVal MikeNewOldAgentNo As Integer, ByVal IsActive As Boolean, ByVal IsUAEMAH As Boolean, ByVal IsImporter As Boolean, ByVal IsDistributer As Boolean, ByVal AccountID As Integer) As String
            objDac = DAC.getDAC()
            Dim strErrNo As String = ""
            Try
                Dim SP1 As SqlParameter = New SqlParameter("@AccountId", SqlDbType.Int, 8, ParameterDirection.Output, True, 0, 0, "", DataRowVersion.Default, 0)
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Master_Insert, New SqlParameter("@AccountNo", AccountNo), _
                New SqlParameter("@FK_AccountTypeId", FK_AccountTypeId), _
                New SqlParameter("@AccountName", AccountName), _
                New SqlParameter("@FK_CountryId", FK_CountryId), _
                New SqlParameter("@City", City), _
                New SqlParameter("@Address", Address), _
                New SqlParameter("@PoBox", PoBox), _
                New SqlParameter("@Telephone", Telephone), _
                New SqlParameter("@Fax", Fax), _
                New SqlParameter("@Email", Email), _
                New SqlParameter("@Website", Website), _
                New SqlParameter("@Remarks", Remarks), _
                New SqlParameter("@MOHLicense", MOHLicense), _
                New SqlParameter("@MOHLicenseNo", MOHLicenseNo), _
                New SqlParameter("@MOHLicenseIssueDate", MOHLicenseIssueDate), _
                New SqlParameter("@MOHLicenseExpiryDate", MOHLicenseExpiryDate), _
                New SqlParameter("@MOERegNo", MOERegNo), _
                New SqlParameter("@MOERegDate", MOERegDate), _
                New SqlParameter("@OldAgentNo", OldAgentNo), _
                New SqlParameter("@MikeNewOldAgentNo", MikeNewOldAgentNo), _
                New SqlParameter("@IsActive", IsActive), _
                New SqlParameter("@IsUAEMAH", IsUAEMAH), _
                New SqlParameter("@IsImporter", IsImporter), _
                New SqlParameter("@IsDistributer", IsDistributer), SP1)
                Dim objBU_Accounts_Contacts As New BU_Accounts_Master
                FK_AccountId = SP1.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Dim strError As String = errNo.ToString() + "|" + FK_AccountId.ToString()
            Return strError

        End Function

        Public Function Update(ByVal AccountId As Integer, ByVal AccountNo As String, ByVal FK_AccountTypeId As Integer, ByVal AccountName As String, ByVal FK_CountryId As Integer, ByVal City As String, ByVal Address As String, ByVal PoBox As String, ByVal Telephone As String, ByVal Fax As String, ByVal Email As String, ByVal Website As String, ByVal Remarks As String, ByVal MOHLicense As Boolean, ByVal MOHLicenseNo As String, ByVal MOHLicenseIssueDate As String, ByVal MOHLicenseExpiryDate As String, ByVal MOERegNo As String, ByVal MOERegDate As String, ByVal OldAgentNo As Integer, ByVal MikeNewOldAgentNo As Integer, ByVal IsActive As Boolean, ByVal IsUAEMAH As Boolean, ByVal IsImporter As Boolean, ByVal IsDistributer As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Master_Update, New SqlParameter("@AccountId", AccountId), _
               New SqlParameter("@AccountNo", AccountNo), _
               New SqlParameter("@FK_AccountTypeId", FK_AccountTypeId), _
               New SqlParameter("@AccountName", AccountName), _
               New SqlParameter("@FK_CountryId", FK_CountryId), _
               New SqlParameter("@City", City), _
               New SqlParameter("@Address", Address), _
               New SqlParameter("@PoBox", PoBox), _
               New SqlParameter("@Telephone", Telephone), _
               New SqlParameter("@Fax", Fax), _
               New SqlParameter("@Email", Email), _
               New SqlParameter("@Website", Website), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@MOHLicense", MOHLicense), _
               New SqlParameter("@MOHLicenseNo", MOHLicenseNo), _
               New SqlParameter("@MOHLicenseIssueDate", MOHLicenseIssueDate), _
               New SqlParameter("@MOHLicenseExpiryDate", MOHLicenseExpiryDate), _
               New SqlParameter("@MOERegNo", MOERegNo), _
               New SqlParameter("@MOERegDate", MOERegDate), _
               New SqlParameter("@OldAgentNo", OldAgentNo), _
               New SqlParameter("@MikeNewOldAgentNo", MikeNewOldAgentNo), _
               New SqlParameter("@IsActive", IsActive), _
               New SqlParameter("@IsUAEMAH", IsUAEMAH), _
               New SqlParameter("@IsImporter", IsImporter), _
               New SqlParameter("@IsDistributer", IsDistributer))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal AccountId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Master_Delete, New SqlParameter("@AccountId", AccountId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal AccountId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(BU_Accounts_Master_Select, New SqlParameter("@AccountId", AccountId)).Rows(0)
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
                objColl = objDac.GetDataTable(BU_Accounts_Master_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllByType(ByVal AccType As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(BU_Accounts_Master_SelectByType, New SqlParameter("@FK_AccountTypeId", AccType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace