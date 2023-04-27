Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp

Namespace TA.Accounts

    Public Class DALBU_AccountsCOOInfo
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private BU_AccountsCOOInfo_Select As String = "BU_AccountsCOOInfo_select"
        Private BU_AccountsCOOInfo_Select_All As String = "BU_AccountsCOOInfo_select_All"
        Private BU_AccountsCOOInfo_Insert As String = "BU_AccountsCOOInfo_Insert"
        Private BU_AccountsCOOInfo_Update As String = "BU_AccountsCOOInfo_Update"
        Private BU_AccountsCOOInfo_Delete As String = "BU_AccountsCOOInfo_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_AccountId As Integer, ByVal EstablishmentDate As String, ByVal CapitalUSD As Double, ByVal TurnoverUSD As Double, ByVal MAHYear As Integer, ByVal LicenseAuthority As String, ByVal LicenseNo As String, ByVal LicenseIssueDate As String, ByVal LicenseExpiryDate As String, ByVal AuthorizedPersonResponsibilities As String, ByVal OtherDate1 As String, ByVal OtherInt1 As Integer, ByVal OtherBit1 As Boolean, ByVal OtherVar1 As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_AccountsCOOInfo_Insert, New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@EstablishmentDate", EstablishmentDate), _
               New SqlParameter("@CapitalUSD", CapitalUSD), _
               New SqlParameter("@TurnoverUSD", TurnoverUSD), _
               New SqlParameter("@MAHYear", MAHYear), _
               New SqlParameter("@LicenseAuthority", LicenseAuthority), _
               New SqlParameter("@LicenseNo", LicenseNo), _
               New SqlParameter("@LicenseIssueDate", LicenseIssueDate), _
               New SqlParameter("@LicenseExpiryDate", LicenseExpiryDate), _
               New SqlParameter("@AuthorizedPersonResponsibilities", AuthorizedPersonResponsibilities), _
               New SqlParameter("@OtherDate1", OtherDate1), _
               New SqlParameter("@OtherInt1", OtherInt1), _
               New SqlParameter("@OtherBit1", OtherBit1), _
               New SqlParameter("@OtherVar1", OtherVar1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_AccountId As Integer, ByVal EstablishmentDate As String, ByVal CapitalUSD As Double, ByVal TurnoverUSD As Double, ByVal MAHYear As Integer, ByVal LicenseAuthority As String, ByVal LicenseNo As String, ByVal LicenseIssueDate As String, ByVal LicenseExpiryDate As String, ByVal AuthorizedPersonResponsibilities As String, ByVal OtherDate1 As String, ByVal OtherInt1 As Integer, ByVal OtherBit1 As Boolean, ByVal OtherVar1 As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_AccountsCOOInfo_Update, New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@EstablishmentDate", EstablishmentDate), _
               New SqlParameter("@CapitalUSD", CapitalUSD), _
               New SqlParameter("@TurnoverUSD", TurnoverUSD), _
               New SqlParameter("@MAHYear", MAHYear), _
               New SqlParameter("@LicenseAuthority", LicenseAuthority), _
               New SqlParameter("@LicenseNo", LicenseNo), _
               New SqlParameter("@LicenseIssueDate", LicenseIssueDate), _
               New SqlParameter("@LicenseExpiryDate", LicenseExpiryDate), _
               New SqlParameter("@AuthorizedPersonResponsibilities", AuthorizedPersonResponsibilities), _
               New SqlParameter("@OtherDate1", OtherDate1), _
               New SqlParameter("@OtherInt1", OtherInt1), _
               New SqlParameter("@OtherBit1", OtherBit1), _
               New SqlParameter("@OtherVar1", OtherVar1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_AccountId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_AccountsCOOInfo_Delete, New SqlParameter("@FK_AccountId", FK_AccountId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_AccountId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                Dim dt As DataTable = objDac.GetDataTable(BU_AccountsCOOInfo_Select, New SqlParameter("@FK_AccountId", FK_AccountId))
                If dt.Rows.Count > 0 Then
                    objRow = dt.Rows(0)
                End If
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
                objColl = objDac.GetDataTable(BU_AccountsCOOInfo_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace