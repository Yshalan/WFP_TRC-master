Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Namespace TA.Reports
    Public Class RDLCReport




        Private _name As String


        Private objDALReports As DALRDLCReport

        Public Sub New()

            objDALReports = New DALRDLCReport()

        End Sub


        Public Property Name() As String
            Set(ByVal value As String)
                _name = value
            End Set
            Get
                Return (_name)
            End Get
        End Property



        'Public Property SECTION() As String
        '    Set(ByVal value As String)
        '        _SECTION = value
        '    End Set
        '    Get
        '        Return (_SECTION)
        '    End Get
        'End Property



        Public Function GetDynamicReports() As DataTable
            Return objDALReports.GetDynamicReports()
        End Function
        Public Function GetCountry() As DataTable
            Return objDALReports.GetCountry()
        End Function

        Public Function GetUAECity() As DataTable
            Return objDALReports.GetUAECity()
        End Function


        Public Function GetCurrency() As DataTable
            Return objDALReports.GetCurrency()
        End Function


        Public Function GetBodySystem() As DataTable
            Return objDALReports.GetBodySystem()
        End Function


        Public Function GetBodySubSystem() As DataTable
            Return objDALReports.GetBodySubSystem()
        End Function


        Public Function GetDosageForm() As DataTable
            Return objDALReports.GetDosageForm()
        End Function

        Public Function GetDosageSubForm() As DataTable
            Return objDALReports.GetDosageSubForm()
        End Function


        Public Function GetAnimal() As DataTable

            Return objDALReports.GetAnimal()

        End Function


        Public Function GetDispensingMode() As DataTable

            Return objDALReports.GetDispensingMode()

        End Function

        Public Function GetDispensingModeCOO() As DataTable

            Return objDALReports.GetDispensingModeCOO()

        End Function


        Public Function GetDocuments() As DataTable

            Return objDALReports.GetDocuments()

        End Function

        Public Function GetInQuantity() As DataTable
            Return objDALReports.GetInQuantity()
        End Function


        Public Function GetPackcolour() As DataTable

            Return objDALReports.GetPackcolour()

        End Function


        Public Function GetPackPresentationType() As DataTable

            Return objDALReports.GetPackPresentationType()

        End Function



        Public Function GetPackSizeUnit() As DataTable

            Return objDALReports.GetPackSizeUnit()

        End Function


        Public Function GetQuantityUnit() As DataTable

            Return objDALReports.GetQuantityUnit()

        End Function



        Public Function GetRouteofAdministration() As DataTable

            Return objDALReports.GetRouteofAdministration()

        End Function


        Public Function GetStorageCondition() As DataTable

            Return objDALReports.GetStorageCondition()

        End Function


        Public Function GetRemedyType() As DataTable

            Return objDALReports.GetRemedyType()

        End Function


        Public Function GetInactiveIngredientsFunctions() As DataTable

            Return objDALReports.GetInactiveIngredientsFunctions()

        End Function

        Public Function GetUAEPort() As DataTable

            Return objDALReports.GetUAEPort()

        End Function


        Public Function GetHealthInstitute() As DataTable

            Return objDALReports.GetHealthInstitute()

        End Function

        Public Function GetPriceUnit() As DataTable
            Return objDALReports.GetPriceUnit()
        End Function


        Public Function GetMAHActivitiesCOO() As DataTable

            Return objDALReports.GetMAHActivitiesCOO()

        End Function

        Public Function GetTooltip() As DataTable

            Return objDALReports.GetTooltip()

        End Function

        Public Function GetApplicationDocument() As DataTable

            Return objDALReports.GetApplicationDocument()

        End Function

        Public Function GetMDRegulatoryStatusRecords() As DataTable

            Return objDALReports.GetMDRegulatoryStatusRecords()

        End Function

        'Basic Information



        Public Function GetActiveIngradients() As DataTable

            Return objDALReports.GetActiveIngradients()

        End Function


        Public Function GetInActiveIngradients() As DataTable

            Return objDALReports.GetInActiveIngradients()

        End Function


        Public Function GetIngradiantTypes() As DataTable

            Return objDALReports.GetIngradiantTypes()

        End Function


        Public Function GetChemicalPrecursors() As DataTable

            Return objDALReports.GetChemicalPrecursors()

        End Function


        Public Function GetProductClass() As DataTable

            Return objDALReports.GetProductClass()

        End Function



        Public Function GetProductSubClass() As DataTable

            Return objDALReports.GetProductSubClass()

        End Function



        Public Function GetManufacturingFunctions() As DataTable
            Return objDALReports.GetManufacturingFunctions()
        End Function

        Public Function GetManufsiteActivityCategory() As DataTable
            Return objDALReports.GetManufsiteActivityCategory()
        End Function


        Public Function GetManufsiteActivity() As DataTable
            Return objDALReports.GetManufsiteActivity()
        End Function
        Public Function GetManufAssembly() As DataTable
            Return objDALReports.GetManufAssembly()
        End Function


        'Narcotic
        Public Function GetControlledMedicines() As DataTable
            Return objDALReports.GetControlledMedicines()
        End Function

        Public Function GetNarcoticMedicines() As DataTable
            Return objDALReports.GetNarcoticMedicines()
        End Function
        Public Function GetNarcoticQuataForm(ByVal QuataId As Integer) As DataTable
            Return objDALReports.GetNarcoticQuataForm(QuataId)
        End Function
        Public Function GetSubNarcoticQuataForm(ByVal QuataId As Integer) As DataTable
            Return objDALReports.GetSubNarcoticQuataForm(QuataId)
        End Function


        Public Function GetNarcoticYearlyReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetNarcoticYearlyReports(ReportId)
        End Function

        Public Function GetSubNarcoticYearlyReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetSubNarcoticYearlyReports(ReportId)
        End Function


        Public Function GetNarcoticQuaterlyReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetNarcoticQuaterlyReports(ReportId)
        End Function

        Public Function GetSubNarcoticQuaterlyReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetSubNarcoticQuaterlyReports(ReportId)
        End Function


        Public Function GetNarcoticMonthlyReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetNarcoticMonthlyReports(ReportId)
        End Function

        Public Function GetSubNarcoticMonthlyReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetSubNarcoticMonthlyReports(ReportId)
        End Function


        Public Function GetNarcoticScrapedReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetNarcoticScrapedReports(ReportId)
        End Function

        Public Function GetSubNarcoticScrapedReports(ByVal ReportId As Integer) As DataTable
            Return objDALReports.GetSubNarcoticScrapedReports(ReportId)
        End Function



        'Manufacturing site
        Public Function GetManufacturingSite(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetManufacturingSite(SiteId)
        End Function
        Public Function GetCompanyRegistration(ByVal AccountID As Integer, ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetCompanyRegistration(AccountID, SiteId)
        End Function
        Public Function GetCategoryOfProducts(ByVal AccountID As Integer) As DataTable
            Return objDALReports.GetCategoryOfProducts(AccountID)
        End Function
        Public Function GetLicensedActivities(ByVal AccountID As Integer) As DataTable
            Return objDALReports.GetLicensedActivities(AccountID)
        End Function
        Public Function GetMAHPharmacologicalClasses(ByVal AccountID As Integer) As DataTable
            Return objDALReports.GetMAHPharmacologicalClasses(AccountID)
        End Function
        Public Function GetManufacturingSiteActivities(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetManufacturingSiteActivities(SiteId)
        End Function
        Public Function GetManufacturingSiteOperations(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetManufacturingSiteOperations(SiteId)
        End Function
        Public Function GetManufacturingSiteOthercertificates(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetManufacturingSiteOthercertificates(SiteId)
        End Function
        'Evaluation
        Public Function GetEvaluationCommitee(ByVal FK_CommiteeId As Integer) As DataTable
            Return objDALReports.GetEvaluationCommitee(FK_CommiteeId)
        End Function



        'Esubmission
        Public Function GetAccountRequest(ByVal RequestId As Integer) As DataTable
            Return objDALReports.GetAccountRequest(RequestId)
        End Function




        'Filter

        Public Function GetDosageSubFormFilter(ByVal DosageFormId As Integer) As DataTable

            Return objDALReports.GetDosageSubFormFilter(DosageFormId)

        End Function


        Public Function GetUAEPortFiltered(ByVal UAECityId As Integer) As DataTable
            Return objDALReports.GetUAEPortFiltered(UAECityId)
        End Function

        Public Function GetFormulationQualityApproval(ByVal ApplicationId As Integer) As DataTable
            Return objDALReports.GetFormulationQualityApproval(ApplicationId)
        End Function

        Public Function GetDistributionOfSamples(ByVal ApplicationId As Integer) As DataTable
            Return objDALReports.GetDistributionOfSamples(ApplicationId)
        End Function

        Public Function GetSub_DistributionOfSamples(ByVal ApplicationId As Integer) As DataTable
            Return objDALReports.GetSub_DistributionOfSamples(ApplicationId)
        End Function

        Public Function GetCompanyRegistrationBySiteID(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetCompanyRegistrationBySiteID(SiteId)
        End Function


        Public Function GetCategoryOfProductsBySiteID(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetCategoryOfProductsBySiteID(SiteId)
        End Function
        Public Function GetLicensedActivitiesBySiteID(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetLicensedActivitiesBySiteID(SiteId)
        End Function

        Public Function GetMAHPharmacologicalClassesBySiteID(ByVal SiteId As Integer) As DataTable
            Return objDALReports.GetMAHPharmacologicalClassesBySiteID(SiteId)
        End Function

        'Chemical precursors
        Public Function GetCompanyApproved() As DataTable
            Return objDALReports.GetCompanyApproved()

        End Function

        Public Function GetCompanyById(ByVal CompanyId As Integer) As DataTable
            Return objDALReports.GetCompanyById(CompanyId)
        End Function

    End Class

End Namespace
