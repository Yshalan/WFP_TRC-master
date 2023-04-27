Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp

Namespace TA.Accounts

    Public Class DALBU_Accounts_Contacts
        Inherits MGRBase



#Region "Class Variables"

       

        Private strConn As String
        Private BU_Accounts_Contacts_Select As String = "SEC_Users_Select"
        Private BU_Accounts_Contacts_Select_All As String = "SEC_Users_Select_All"


        Private BU_Accounts_Contacts_Insert As String = "SEC_Users_Insert"
        Private BU_Accounts_Contacts_Update As String = "SEC_Users_Update"
        Private BU_Accounts_Contacts_Delete As String = "BU_Accounts_Contacts_Delete"
        Private BU_Accounts_Contacts_ResetPassword As String = "BU_Accounts_Contacts_ResetPassword"
        Private BU_Accounts_Contacts_User_Update As String = "BU_Accounts_Contacts_User_Update"

        Private SEC_Users_GETLoginUserDetails As String = "Esub_GETLoginUserDetails"
        Private BU_Accounts_Contacts_ResetCredentials As String = "SEC_Users_ResetCredentials"

        Private BU_Accounts_Main_Contacts_SelectByAccountID As String = "BU_Accounts_Main_Contacts_SelectByAccountID"
        Private BU_Accounts_Contacts_Update_Profile As String = "BU_Accounts_Contacts_Update_Profile"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_AccountId As Integer, ByVal ContactName As String, ByVal ContactTitle As String, ByVal ContactFax As String, ByVal ContactTelephone As String, ByVal ContactMobile As String, ByVal ContactEmail As String, ByVal Remarks As String, ByVal HasCredintials As Boolean, ByVal UserName As String, ByVal Password As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String, ByVal PharmaLicNo As String, ByVal PharmaExpDate As String, ByVal IsApproved As Boolean, ByVal IsActive As Boolean, ByVal IsMainContact As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_Insert, New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@ContactName", ContactName), _
               New SqlParameter("@ContactTitle", ContactTitle), _
               New SqlParameter("@ContactFax", ContactFax), _
               New SqlParameter("@ContactTelephone", ContactTelephone), _
               New SqlParameter("@ContactMobile", ContactMobile), _
               New SqlParameter("@ContactEmail", ContactEmail), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@HasCredintials", HasCredintials), _
               New SqlParameter("@UserName", UserName), _
               New SqlParameter("@Password", Password), _
               New SqlParameter("@SecurityQuestion", SecurityQuestion), _
               New SqlParameter("@SecurityAnswer", SecurityAnswer), _
               New SqlParameter("@PharmaLicNo", PharmaLicNo), _
               New SqlParameter("@PharmaExpDate", PharmaExpDate), _
               New SqlParameter("@IsApproved", IsApproved), _
               New SqlParameter("@IsActive", IsActive), _
               New SqlParameter("@IsMainContact", IsMainContact))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ContactId As Integer, ByVal FK_AccountId As Integer, ByVal ContactName As String, ByVal ContactTitle As String, ByVal ContactFax As String, ByVal ContactTelephone As String, ByVal ContactMobile As String, ByVal ContactEmail As String, ByVal Remarks As String, ByVal HasCredintials As Boolean, ByVal UserName As String, ByVal Password As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String, ByVal PharmaLicNo As String, ByVal PharmaExpDate As String, ByVal IsApproved As Boolean, ByVal IsActive As Boolean, ByVal IsMainContact As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_Update, New SqlParameter("@ContactId", ContactId), _
               New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@ContactName", ContactName), _
               New SqlParameter("@ContactTitle", ContactTitle), _
               New SqlParameter("@ContactFax", ContactFax), _
               New SqlParameter("@ContactTelephone", ContactTelephone), _
               New SqlParameter("@ContactMobile", ContactMobile), _
               New SqlParameter("@ContactEmail", ContactEmail), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@HasCredintials", HasCredintials), _
               New SqlParameter("@UserName", UserName), _
               New SqlParameter("@Password", Password), _
               New SqlParameter("@SecurityQuestion", SecurityQuestion), _
               New SqlParameter("@SecurityAnswer", SecurityAnswer), _
               New SqlParameter("@PharmaLicNo", PharmaLicNo), _
               New SqlParameter("@PharmaExpDate", PharmaExpDate), _
               New SqlParameter("@IsApproved", IsApproved), _
               New SqlParameter("@IsActive", IsActive), _
               New SqlParameter("@IsMainContact", IsMainContact))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function UpdateProfile(ByVal ContactId As Integer, ByVal FK_AccountId As Integer, ByVal ContactName As String, ByVal ContactTitle As String, ByVal ContactFax As String, ByVal ContactTelephone As String, ByVal ContactMobile As String, ByVal ContactEmail As String, ByVal Remarks As String, ByVal Password As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_Update_Profile, New SqlParameter("@ContactId", ContactId), _
               New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@ContactName", ContactName), _
               New SqlParameter("@ContactTitle", ContactTitle), _
               New SqlParameter("@ContactFax", ContactFax), _
               New SqlParameter("@ContactTelephone", ContactTelephone), _
               New SqlParameter("@ContactMobile", ContactMobile), _
               New SqlParameter("@ContactEmail", ContactEmail), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@Password", Password), _
               New SqlParameter("@SecurityQuestion", SecurityQuestion), _
               New SqlParameter("@SecurityAnswer", SecurityAnswer))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function UpdateUser(ByVal ContactId As Integer, ByVal UserName As String, ByVal Password As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_User_Update, New SqlParameter("@ContactId", ContactId), _
               New SqlParameter("@UserName", UserName), _
               New SqlParameter("@Password", Password))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function UpdatePassword(ByVal ContactId As Integer, ByVal OldPassword As String, ByVal Password As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_ResetPassword, New SqlParameter("@ContactId", ContactId), _
               New SqlParameter("@OldPasword", OldPassword), _
               New SqlParameter("@Password", Password))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function ChangeAccountCredentials(ByVal ContactId As Integer, ByVal OldPassword As String, ByVal Password As String, ByVal SecQtn As String, ByVal SecAns As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_ResetCredentials, New SqlParameter("@ContactId", ContactId), _
               New SqlParameter("@OldPasword", OldPassword), _
               New SqlParameter("@SecurityQuestion", SecQtn), _
               New SqlParameter("@SecurityAnswer", SecAns), _
               New SqlParameter("@Password", Password))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ContactId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_Delete, New SqlParameter("@ContactId", ContactId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteByAccountID(ByVal FK_AccountID As Integer) As Integer

            Return 0
            'objDac = DAC.getDAC()
            'Try
            '    errNo = objDac.AddUpdateDeleteSPTrans(BU_Accounts_Contacts_Delete_By_AccountID, New SqlParameter("@FK_AccountID", FK_AccountID))
            'Catch ex As Exception
            '    errNo = -11
            '    CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            'End Try
            'Return errNo

        End Function

        Public Function GetByPK(ByVal ContactId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(BU_Accounts_Contacts_Select, New SqlParameter("@ContactId", ContactId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetMainContactByAccountID(ByVal AccountID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(BU_Accounts_Main_Contacts_SelectByAccountID, New SqlParameter("@FK_AccountId", AccountID)).Rows(0)
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
                objColl = objDac.GetDataTable(BU_Accounts_Contacts_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetLoginUser(ByVal LoginName As String, ByVal LoginPassword As String) As DataRow

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(SEC_Users_GETLoginUserDetails, New SqlParameter("@LoginName", LoginName), New SqlParameter("@LoginPassword", LoginPassword))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            If Not objColl Is Nothing Then
                If objColl.Rows.Count > 0 Then
                    Return objColl.Rows(0)
                End If
            End If
            Return Nothing
        End Function
        Public Function GetByAccountID(ByVal AccountID As Integer) As DataTable
            Dim dt As DataTable = Nothing
            Return dt

            'objDac = DAC.getDAC()
            'Dim objColl As DataTable = Nothing
            'Try
            '    objColl = objDac.GetDataTable(BU_Accounts_Contacts_SelectByAccountID, New SqlParameter("@FK_AccountId", AccountID))
            'Catch ex As Exception
            '    errNo = -11
            '    CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            'End Try
            'Return objColl


        End Function

#End Region


    End Class
End Namespace