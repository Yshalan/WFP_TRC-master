Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Reports

    Public Class DALRDLCReport
        Inherits MGRBase



        Private strConn As String
        'admin

        Private DynamicReports_Select_All As String = "DynamicReports_Select_All"

        Private RPT_lkpCountry_Select_All As String = "RPT_lkpCountry_Select_All"
        Private lkpUAECity_Select_All As String = "lkpUAECity_Select_All"
        Private lkpCurrency_Select_All As String = "lkpCurrency_Select_All"
        Private LkpProductBNFClassmain_Select_All As String = "LkpProductBNFClassmain_Select_All"
        Private RPT_LkpProductBNFClassSub_Select_All As String = "RPT_LkpProductBNFClassSub_Select_All"

        Private lkpDosageForm_Select_All As String = "lkpDosageForm_Select_All"
        Private RPTlkpDosageSubForm_Select_All As String = "RPTlkpDosageSubForm_Select_All"
        Private lkpAnimal_Select_All As String = "lkpAnimal_Select_All"
        Private lkpDispensingMode_Select_All As String = "lkpDispensingMode_Select_All"
        Private lkpDispensingModeCOO_Select_All As String = "lkpDispensingModeCOO_Select_All"

        Private lkpDocuments_Select_All As String = "lkpDocuments_Select_All"
        Private lkpInQuantity_select_All As String = "lkpInQuantity_select_All"
        Private LkpPackcolour_Select_All As String = "LkpPackcolour_Select_All"
        Private lkpPackPresentationType_Select_All As String = "lkpPackPresentationType_Select_All"
        Private lkpPackSizeUnit_Select_All As String = "lkpPackSizeUnit_Select_All"


        Private lkpQuantityUnit_Select_All As String = "lkpQuantityUnit_Select_All"
        Private lkpRouteofAdministration_Select_All As String = "lkpRouteofAdministration_Select_All"
        Private lkpStorageCondition_Select_All As String = "lkpStorageCondition_Select_All"
        Private lkpRemedyType_Select_All As String = "lkpRemedyType_Select_All"
        Private lkpInactiveIngredientsFunctions_Select_All As String = "lkpInactiveIngredientsFunctions_Select_All"


        Private RPTlkpUAEPort_Select_All As String = "RPTlkpUAEPort_Select_All"
        Private RPT_lkpHealthInstitute_Select_All As String = "RPT_lkpHealthInstitute_Select_All"
        Private lkpPriceUnit_Select_All As String = "lkpPriceUnit_Select_All"
        Private lkpMAHActivitiesCOO_Select_All As String = "lkpMAHActivitiesCOO_Select_All"
        Private Sys_Tooltip_Select_All As String = "Sys_Tooltip_Select_All"

        Private Doc_ApplicationType_Select_All As String = "Doc_ApplicationType_Select_All"
        Private RPT_lkpMDRegulatoryStatusRecords_Select_All As String = "RPT_lkpMDRegulatoryStatusRecords_Select_All"


        'Basic Information

        Private RPT_ActiveIngradients_Select_All As String = "RPT_ActiveIngradients_Select_All"
        Private RPT_InactiveIngrediants_Select_All As String = "RPT_InactiveIngrediants_Select_All"
        Private lkpIngradiantTypes_Select_All As String = "lkpIngradiantTypes_Select_All"
        Private RPT_BU_Chemical_Precursors_Select_All As String = "RPT_BU_Chemical_Precursors_Select_All"
        Private RPT_LkpProductClass_Select_All_1 As String = "RPT_LkpProductClass_Select_All_1"
        Private RPT_LkpProductSubClass_GetAll As String = "RPT_LkpProductSubClass_GetAll"


        Private lkpDosageSubForm_Select_All_ByDosageFormId As String = "lkpDosageSubForm_Select_All_ByDosageFormId"
        Private lkpUAEPort_Select_All_ByUAECityId As String = "lkpUAEPort_Select_All_ByUAECityId"



        Private lkpManufacturingFunctions_Select_All As String = "lkpManufacturingFunctions_Select_All"
        Private RPT_lkpManfsiteActivityCategory_selectAll As String = " RPT_lkpManfsiteActivityCategory_selectAll"
        Private lkpManufsiteActivity_Select_All_With_Category As String = "lkpManufsiteActivity_Select_All_With_Category"
        Private RPT_lkpManufAssembly_Select_All As String = "RPT_lkpManufAssembly_Select_All"

        'Narcotics
        Private RPT_BU_ControlledMedicines_SelectAll As String = "RPT_BU_ControlledMedicines_SelectAll"
        Private BU_NarcoticsMedicines_Select_All As String = "BU_NarcoticsMedicines_Select_All"
        Private BU_NArcoticQuataForm_Select As String = "BU_NArcoticQuataForm_Select"
        Private RPT_BU_Sub_NarcoticQuataForm_SelectByQuataId As String = "RPT_BU_Sub_NarcoticQuataForm_SelectByQuataId"
        Private BU_NarcoticYearlyReport_Select As String = "BU_NarcoticYearlyReport_Select"

        Private RPT_BU_Sub_NarcoticYearlyReport_Select_ByReportId As String = "RPT_BU_Sub_NarcoticYearlyReport_Select_ByReportId"
        Private BU_NarcoticQuarterlyReport_Select As String = "BU_NarcoticQuarterlyReport_Select"
        Private RPT_BU_Sub_NarcoticQuarterlyReport_SelectByReportId As String = "RPT_BU_Sub_NarcoticQuarterlyReport_SelectByReportId"
        Private BU_NarcoticNeedScrapedReport_Select As String = "BU_NarcoticNeedScrapedReport_Select"
        Private RPT_BU_NarcoticNeedScrapedReportDrugs_Select_All As String = "RPT_BU_NarcoticNeedScrapedReportDrugs_Select_All"

        Private BU_NarcoticMonthlyReportCDA_CDB_Select As String = "BU_NarcoticMonthlyReportCDA_CDB_Select"
        Private RPT_BU_Sub_NarcoticMonthlyReportCDA_CDB_Select_ByReportID As String = "RPT_BU_Sub_NarcoticMonthlyReportCDA_CDB_Select_ByReportID"

        'Evaluation

        Private RPT__EvaluationCommitteeMembers As String = "RPT__EvaluationCommitteeMembers"

        'Esubmission

        Private RPT_Esub_AccountRequest As String = "RPT_Esub_AccountRequest"


        'DAS
        Private BU_DAS_Application_By_ApplicationID_ForReports As String = "BU_DAS_Application_By_ApplicationID_ForReports"
        Private BU_DAS_Application_By_ApplicationID_For_Analysis As String = "BU_DAS_Application_By_ApplicationID_For_Analysis"

        Private RPT_CompanyRegistrationBySiteID As String = "RPT_CompanyRegistrationBySiteID"
        Private RPT_CategoryOfProductsDetailsBySiteid As String = "RPT_CategoryOfProductsDetailsBySiteid"
        Private RPT_LicensedActivitiesDetailsBySiteID As String = "RPT_LicensedActivitiesDetailsBySiteID"
        Private RPT_MAHPharmacologicalClassesBySiteID As String = "RPT_MAHPharmacologicalClassesBySiteID"
        
        'ChemicalPrecursors
        Private RPT_Esub_ChemicalPrecursorsCompany_Approved As String = "RPT_Esub_ChemicalPrecursorsCompany_Approved"
        Private RPT_Esub_ChemicalPrecursorsCompanyByID As String = "RPT_Esub_ChemicalPrecursorsCompanyByID"
        Private BU_DAS_Application_SubRpt_By_ApplicationID_For_Analysis As String = "BU_DAS_Application_SubRpt_By_ApplicationID_For_Analysis"






        Public Sub New()



        End Sub
        'Admin
        Public Function GetDynamicReports() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(DynamicReports_Select_All)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCountry() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_lkpCountry_Select_All)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetUAECity() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable

            Try
                objColl = objDac.GetDataTable(lkpUAECity_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetCurrency() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpCurrency_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetBodySystem() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(LkpProductBNFClassmain_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetBodySubSystem() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_LkpProductBNFClassSub_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function




        Public Function GetDosageForm() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpDosageForm_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetDosageSubForm() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPTlkpDosageSubForm_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAnimal() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpAnimal_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function




        Public Function GetDispensingMode() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpDispensingMode_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetDispensingModeCOO() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpDispensingModeCOO_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetDocuments() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpDocuments_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetInQuantity() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpInQuantity_select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetPackcolour() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(LkpPackcolour_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetPackPresentationType() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpPackPresentationType_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetPackSizeUnit() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpPackSizeUnit_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetQuantityUnit() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpQuantityUnit_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetRouteofAdministration() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpRouteofAdministration_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetStorageCondition() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpStorageCondition_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetRemedyType() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpRemedyType_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetInactiveIngredientsFunctions() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpInactiveIngredientsFunctions_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetUAEPort() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPTlkpUAEPort_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetHealthInstitute() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_lkpHealthInstitute_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetPriceUnit() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpPriceUnit_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetMAHActivitiesCOO() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpMAHActivitiesCOO_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetTooltip() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Sys_Tooltip_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetApplicationDocument() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Doc_ApplicationType_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetMDRegulatoryStatusRecords() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_lkpMDRegulatoryStatusRecords_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        'BasicInformation


        Public Function GetActiveIngradients() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_ActiveIngradients_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetInActiveIngradients() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_InactiveIngrediants_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetIngradiantTypes() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpIngradiantTypes_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetChemicalPrecursors() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_BU_Chemical_Precursors_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function




        Public Function GetProductClass() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_LkpProductClass_Select_All_1)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function





        Public Function GetProductSubClass() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_LkpProductSubClass_GetAll)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function




        Public Function GetManufacturingFunctions() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpManufacturingFunctions_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetManufsiteActivityCategory() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_lkpManfsiteActivityCategory_selectAll)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetManufsiteActivity() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(lkpManufsiteActivity_Select_All_With_Category)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetManufAssembly() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_lkpManufAssembly_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        'Narcotics
        Public Function GetNarcoticMedicines() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(BU_NarcoticsMedicines_Select_All)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetControlledMedicines() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RPT_BU_ControlledMedicines_SelectAll)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetNarcoticQuataForm(ByVal QuataId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_NArcoticQuataForm_Select", New SqlParameter("@QuataId", QuataId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetSubNarcoticQuataForm(ByVal QuataId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_Sub_NarcoticQuataForm_SelectByQuataId", New SqlParameter("@QuataId", QuataId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetNarcoticYearlyReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_NarcoticYearlyReport_Select", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSubNarcoticYearlyReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_Sub_NarcoticYearlyReport_Select_ByReportId", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetNarcoticQuaterlyReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_NarcoticQuarterlyReport_Select", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSubNarcoticQuaterlyReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_Sub_NarcoticQuarterlyReport_SelectByReportId", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetNarcoticMonthlyReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_NarcoticMonthlyReportCDA_CDB_Select", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSubNarcoticMonthlyReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_Sub_NarcoticMonthlyReportCDA_CDB_Select_ByReportID", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function




        Public Function GetNarcoticScrapedReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_NarcoticNeedScrapedReport_Select", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetSubNarcoticScrapedReports(ByVal ReportId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_NarcoticNeedScrapedReportDrugs_Select_All", New SqlParameter("@ReportId", ReportId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function







        'ManufacturingSite
        Public Function GetManufacturingSiteOthercertificates(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_ManufSitesOtherCertifates", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetManufacturingSiteOperations(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_ManufacturingSitesOperations", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetManufacturingSiteActivities(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_ManufsitesActivitiesDetails", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetManufacturingSite(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_BU_ManufacturingSites", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCompanyRegistration(ByVal AccountId As Integer, ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_CompanyRegistration", New SqlParameter("@AccountId", AccountId), New SqlParameter("@SiteId", SiteId))
                '@SiteId
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetCompanyRegistrationBySiteID(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_CompanyRegistrationBySiteID", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetCategoryOfProducts(ByVal AccountId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_CategoryOfProductsDetails", New SqlParameter("@AccountId", AccountId))
                '@SiteId
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetLicensedActivities(ByVal AccountId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_LicensedActivitiesDetails", New SqlParameter("@AccountId", AccountId))
                '@SiteId
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetMAHPharmacologicalClasses(ByVal AccountId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_MAHPharmacologicalClasses", New SqlParameter("@AccountId", AccountId))
                '@SiteId
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        'Evaluation

        Public Function GetEvaluationCommitee(ByVal FK_CommiteeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT__EvaluationCommitteeMembers", New SqlParameter("@FK_CommiteeId", FK_CommiteeId))
                '@SiteId
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        'Esubmission
        Public Function GetAccountRequest(ByVal RequestId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_Esub_AccountRequest", New SqlParameter("@RequestId", RequestId))
                '@SiteId
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        'Filter


        Public Function GetDosageSubFormFilter(ByVal DosageFormId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("lkpDosageSubForm_Select_All_ByDosageFormId", New SqlParameter("@DosageFormId", DosageFormId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetUAEPortFiltered(ByVal UAECityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("lkpUAEPort_Select_All_ByUAECityId", New SqlParameter("@UAECityId", UAECityId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFormulationQualityApproval(ByVal ApplicationId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_DAS_Application_By_ApplicationID_ForReports", New SqlParameter("@ApplicationId", ApplicationId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function



        Public Function GetDistributionOfSamples(ByVal ApplicationId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_DAS_Application_By_ApplicationID_For_Analysis", New SqlParameter("@ApplicationId", ApplicationId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetSub_DistributionOfSamples(ByVal ApplicationId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("BU_DAS_Application_SubRpt_By_ApplicationID_For_Analysis", New SqlParameter("@ApplicationId", ApplicationId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetCategoryOfProductsBySiteID(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_CategoryOfProductsDetailsBySiteid", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function



        Public Function GetLicensedActivitiesBySiteID(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_LicensedActivitiesDetailsBySiteID", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function


        Public Function GetMAHPharmacologicalClassesBySiteID(ByVal SiteId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_MAHPharmacologicalClassesBySiteID", New SqlParameter("@SiteId", SiteId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        'Chemical Precursors


        Public Function GetCompanyApproved() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_Esub_ChemicalPrecursorsCompany_Approved")

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function


        Public Function GetCompanyById(ByVal CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("RPT_Esub_ChemicalPrecursorsCompanyByID", New SqlParameter("@CompanyId", CompanyId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function


    End Class

End Namespace
