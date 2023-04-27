Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV
Imports TA.Lookup
Imports SmartV.UTILITIES



Namespace TA_CustomerLicense

    Public Class DALCustomerLicense
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private CustomerLicense_Select As String = "CustomerLicense_select"
        Private CustomerLicense_Select_All As String = "CustomerLicense_select_All"
        Private CustomerLicense_Insert As String = "CustomerLicense_Insert"
        Private CustomerLicense_Update As String = "CustomerLicense_Update"
        Private CustomerLicense_Delete As String = "CustomerLicense_Delete"
        Private sp_GetFormsFromCustomerLicense As String = "sp_GetFormsFromCustomerLicense"
        Private sp_GetFormsModulesForLinks As String = "GetFormsModulesForLinks"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal CustomerShortName As String, ByVal CustomerName As String, ByVal CustomerArabicName As String, ByVal PhoneNumber As Long, ByVal CustomerCountry As String, ByVal CustomerCity As String, ByVal CustomerAddress As String, ByVal CustomerGPSCoordinates As String, ByVal NoOfUsers As String, ByVal NoOfReaders As String, ByVal NoOfEmployees As String, ByVal Projectmanager As String, ByVal ImplementationEngineer As String, ByVal SupportEngineer As String, ByVal IntegrationEngineer As String, ByVal ServerMacAddressKey As String, ByVal Package As Integer, ByVal StartDate As DateTime, ByVal SupportEndDate As DateTime, ByVal Forms As String, ByVal LicenseKey As String, ByVal CreatedBy As String, ByVal CreatedDate As DateTime, ByVal AlteredBy As String, ByVal AlteredDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CustomerLicense_Insert,
               New SqlParameter("@CustomerShortName", CustomerShortName), _
               New SqlParameter("@CustomerName", CustomerName), _
               New SqlParameter("@CustomerArabicName", CustomerArabicName), _
               New SqlParameter("@PhoneNumber", PhoneNumber), _
               New SqlParameter("@CustomerCountry", CustomerCountry), _
               New SqlParameter("@CustomerCity", CustomerCity), _
               New SqlParameter("@CustomerAddress", CustomerAddress), _
               New SqlParameter("@CustomerGPSCoordinates", CustomerGPSCoordinates), _
               New SqlParameter("@NoOfUsers", NoOfUsers), _
               New SqlParameter("@NoOfReaders", NoOfReaders), _
               New SqlParameter("@NoOfEmployees", NoOfEmployees), _
               New SqlParameter("@Projectmanager", Projectmanager), _
               New SqlParameter("@ImplementationEngineer", ImplementationEngineer), _
               New SqlParameter("@SupportEngineer", SupportEngineer), _
               New SqlParameter("@IntegrationEngineer", IntegrationEngineer), _
               New SqlParameter("@ServerMacAddressKey", ServerMacAddressKey), _
               New SqlParameter("@Package", Package), _
               New SqlParameter("@StartDate", StartDate), _
               New SqlParameter("@SupportEndDate", SupportEndDate), _
               New SqlParameter("@Forms", Forms), _
               New SqlParameter("@LicenseKey", LicenseKey), _
               New SqlParameter("@CreatedBy", CreatedBy), _
               New SqlParameter("@CreatedDate", CreatedDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal CustomerId As Integer, ByVal CustomerShortName As String, ByVal CustomerName As String, ByVal CustomerArabicName As String, ByVal PhoneNumber As Long, ByVal CustomerCountry As String, ByVal CustomerCity As String, ByVal CustomerAddress As String, ByVal CustomerGPSCoordinates As String, ByVal NoOfUsers As String, ByVal NoOfReaders As String, ByVal NoOfEmployees As String, ByVal Projectmanager As String, ByVal ImplementationEngineer As String, ByVal SupportEngineer As String, ByVal IntegrationEngineer As String, ByVal ServerMacAddressKey As String, ByVal Package As Integer, ByVal StartDate As DateTime, ByVal SupportEndDate As DateTime, ByVal Forms As String, ByVal LicenseKey As String, ByVal CreatedBy As String, ByVal CreatedDate As DateTime, ByVal AlteredBy As String, ByVal AlteredDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CustomerLicense_Update, New SqlParameter("@CustomerId", CustomerId), _
               New SqlParameter("@CustomerShortName", CustomerShortName), _
               New SqlParameter("@CustomerName", CustomerName), _
               New SqlParameter("@CustomerArabicName", CustomerArabicName), _
               New SqlParameter("@PhoneNumber", PhoneNumber), _
               New SqlParameter("@CustomerCity", CustomerCity), _
                 New SqlParameter("@CustomerCountry", CustomerCountry), _
               New SqlParameter("@CustomerAddress", CustomerAddress), _
               New SqlParameter("@CustomerGPSCoordinates", CustomerGPSCoordinates), _
               New SqlParameter("@NoOfUsers", NoOfUsers), _
               New SqlParameter("@NoOfReaders", NoOfReaders), _
               New SqlParameter("@NoOfEmployees", NoOfEmployees), _
               New SqlParameter("@Projectmanager", Projectmanager), _
               New SqlParameter("@ImplementationEngineer", ImplementationEngineer), _
               New SqlParameter("@SupportEngineer", SupportEngineer), _
               New SqlParameter("@IntegrationEngineer", IntegrationEngineer), _
               New SqlParameter("@ServerMacAddressKey", ServerMacAddressKey), _
               New SqlParameter("@Package", Package), _
               New SqlParameter("@StartDate", StartDate), _
               New SqlParameter("@SupportEndDate", SupportEndDate), _
               New SqlParameter("@Forms", Forms), _
               New SqlParameter("@LicenseKey", LicenseKey), _
               New SqlParameter("@AlteredBy", AlteredBy), _
               New SqlParameter("@AlteredDate", AlteredDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal CustomerId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CustomerLicense_Delete, New SqlParameter("@CustomerId", CustomerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal CustomerId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(CustomerLicense_Select, New SqlParameter("@CustomerId", CustomerId)).Rows(0)
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
                objColl = objDac.GetDataTable(CustomerLicense_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetFormsFromCustomerLicense(ByVal CustomerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(sp_GetFormsFromCustomerLicense, New SqlParameter("@CustomerId", CustomerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetFormsModulesForLinks(ByVal FormsString As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(sp_GetFormsModulesForLinks, New SqlParameter("@FormsString", FormsString))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



#End Region


    End Class
End Namespace