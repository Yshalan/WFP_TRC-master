Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Collections
Imports System.Collections.Generic
Imports System.Text
Imports CrystalDecisions
Imports CrystalDecisions.CrystalReports
Imports System.Globalization
Imports TA.Admin
Imports TA.OverTime

Partial Class Reports_RptViewer
    Inherits System.Web.UI.Page

    Private objAPP_Settings As APP_Settings
    Private Lang As CtlCommon.Lang
    Protected dir As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    'Dim cryRpt As New ReportDocument

    Public Shared RPTName As String
    Public Shared MainRPTDataSource As DataTable
    Public Shared SubRPTDataSource As DataTable
    Public Shared SubRPTDataSource1 As DataTable

    Public Shared SubRPTDataSource2 As DataTable
    Public Shared SubRPTDataSource3 As DataTable
    Public Shared SubRPTDataSource4 As DataTable
    Public Shared SubRPTDataSource5 As DataTable

    Public Shared Message As String = ""
    Public Shared period As String = ""

    Dim Nameval As NameValueCollection
    Private CRD As New ReportDocument
    Dim cryRpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
#Region "Properties"

    Public Property OverTimeMonth() As Integer
        Get
            Return ViewState("OverTimeMonth")
        End Get
        Set(ByVal value As Integer)
            ViewState("OverTimeMonth") = value
        End Set
    End Property
    Public Property OverTimeYear() As Integer
        Get
            Return ViewState("OverTimeYear")
        End Get
        Set(ByVal value As Integer)
            ViewState("OverTimeYear") = value
        End Set
    End Property
    Public Property EmployeeID() As Integer
        Get
            Return ViewState("EmployeeID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeID") = value
        End Set
    End Property
    Public Property ResponseStatus() As String
        Get
            Return ViewState("ResponseStatus")
        End Get
        Set(ByVal value As String)
            ViewState("ResponseStatus") = value
        End Set
    End Property
    Public Property ReportType() As Integer
        Get
            Return ViewState("ReportType")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReportType") = value
        End Set
    End Property
    Public Property ReportFormat() As String
        Get
            Return ViewState("ReportFormat")
        End Get
        Set(ByVal value As String)
            ViewState("ReportFormat") = value
        End Set
    End Property


#End Region

    Protected Sub Reports_RptViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then

                BindReport()
            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "ReportViewer.aspx Page:Reports_RptViewer_Load  " + ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Sub BindReport()
        Try


            Dim rptObj As New Emp_OverTime_Master
            'Dim objCert_Applications As New Cert_Applications
            Dim cryRpt As New ReportDocument

            ' cryRpt = ReportFactory.GetReport(cryRpt.GetType())

            If Not Request.QueryString("OverTimeMonth") Is Nothing Then
                OverTimeMonth = Request.QueryString("OverTimeMonth")
            End If
            If Not Request.QueryString("OvertimeYear") Is Nothing Then
                OverTimeYear = Request.QueryString("OvertimeYear")
            End If
            If Not Request.QueryString("EmployeeID") Is Nothing Then
                EmployeeID = Request.QueryString("EmployeeID")
            End If
            If Not Request.QueryString("ReportType") Is Nothing Then
                ReportType = Request.QueryString("ReportType")
            End If
            ' 

            If Not Page.Request.QueryString("ReportFormat") Is Nothing Then
                ReportFormat = Page.Request.QueryString("ReportFormat")
            End If



            Dim reportpath As String
            MainRPTDataSource = Nothing
            SubRPTDataSource = Nothing
            SubRPTDataSource1 = Nothing
            SubRPTDataSource2 = Nothing
            SubRPTDataSource3 = Nothing
            SubRPTDataSource4 = Nothing
            SubRPTDataSource5 = Nothing

            Dim Parameter1, Parameter1_Value, Parameter2, Parameter2_Value, Parameter3, Parameter3_Value, SubParameter1, SubParameter1_Value As String


            Select Case ReportType
                Case 1
                    RPTName = "rpt_Emp_OverTime_Details.rpt" ' 
                    With rptObj
                        .FK_EmployeeID = EmployeeID
                        .OverTimeMonth = OverTimeMonth
                        .OverTimeYear = OverTimeYear
                    End With
                    MainRPTDataSource = rptObj.GetOTDetailedReport()
                    'SubRPTDataSource = rptObj.Get_ECASProductDetails()
                    
            End Select

            If (Lang = CtlCommon.Lang.AR) Then
                RPTName = "Arabic/" + RPTName
            Else
                RPTName = "English/" + RPTName
            End If
            cryRpt.Load(Server.MapPath(RPTName))

            'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "reportpath " + reportpath.ToString(), MethodBase.GetCurrentMethod.Name)
            cryRpt.SetDataSource(MainRPTDataSource)

            If Not SubRPTDataSource Is Nothing Then
                cryRpt.Subreports(cryRpt.Subreports().Item(0).Name).SetDataSource(SubRPTDataSource)
            End If
            If Not SubRPTDataSource1 Is Nothing Then
                cryRpt.Subreports(cryRpt.Subreports().Item(1).Name).SetDataSource(SubRPTDataSource1)
            End If

            If Not SubRPTDataSource2 Is Nothing Then
                cryRpt.Subreports(cryRpt.Subreports().Item(2).Name).SetDataSource(SubRPTDataSource2)
            End If
            If Not SubRPTDataSource3 Is Nothing Then
                cryRpt.Subreports(cryRpt.Subreports().Item(3).Name).SetDataSource(SubRPTDataSource3)
            End If
            If Not SubRPTDataSource4 Is Nothing Then
                cryRpt.Subreports(cryRpt.Subreports().Item(4).Name).SetDataSource(SubRPTDataSource4)
            End If

            If Not SubRPTDataSource5 Is Nothing Then
                cryRpt.Subreports(cryRpt.Subreports().Item(5).Name).SetDataSource(SubRPTDataSource5)
            End If
            cryRpt.Refresh()


            CRV.ReportSource = cryRpt

            If Not Page.Request.QueryString("Parameter1") Is Nothing Then
                Parameter1 = Request.QueryString("Parameter1")
                Parameter1_Value = Request.QueryString("Parameter1_Value")
                If Len(Parameter1) > 0 Then cryRpt.SetParameterValue(Parameter1, Parameter1_Value)
            End If
            If Not Page.Request.QueryString("Parameter2") Is Nothing Then
                Parameter2 = Request.QueryString("Parameter2")
                Parameter2_Value = Request.QueryString("Parameter2_Value")
                If Len(Parameter2) > 0 Then cryRpt.SetParameterValue(Parameter2, Parameter2_Value)
            End If


            ''---------------------------------------------------------


            If ReportFormat <> "" Then
                Select Case ReportFormat
                    Case "Excel"
                        
                        cryRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, False, "Certificate")
                        Dim myExportOptions As ExportOptions
                        Dim myDiskFileDestinationOptions As DiskFileDestinationOptions

                        Dim myExportFile As String

                        Dim FileName, ReportTemp As String
                        ReportTemp = ConfigurationSettings.AppSettings("ReportPath").ToString
                        FileName = Now.Hour & Now.Minute & Now.Second & Now.Millisecond

                        myExportFile = Trim(ReportTemp) & Trim(FileName) + ".xls"
                        myDiskFileDestinationOptions = New DiskFileDestinationOptions

                        myDiskFileDestinationOptions.DiskFileName = myExportFile

                        myExportOptions = cryRpt.ExportOptions

                        myExportOptions.DestinationOptions = myDiskFileDestinationOptions
                        myExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
                        myExportOptions.ExportFormatType = ExportFormatType.Excel

                        cryRpt.Export()
                        Response.ClearContent()
                        Response.ClearHeaders()
                        Response.AddHeader("Content-Disposition", "inline;filename=" & FileName & ".xls")
                        Response.ContentType = "application/ms-excel"

                        Response.WriteFile(myExportFile)
                        Response.End()
                        Response.Flush()
                        Response.Close()

                        System.IO.File.Delete(myExportFile)

                        cryRpt.Close()
                        cryRpt.Dispose()
                        CRV.Dispose()
                        GC.Collect()
                        Exit Sub
                        'End Select

                    Case Else
                        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Certificate")
                End Select
            Else
                Select Case ReportType
                    Case 8, 9
                        cryRpt.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, False, "Certificate")
                    Case Else
                        cryRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, False, "Certificate")

                End Select
            End If

            cryRpt.Close()
            cryRpt.Dispose()
            CRV.Dispose()
            GC.Collect()

        Catch ex As Exception
            cryRpt.Close()
            cryRpt.Dispose()
            CRV.Dispose()
            GC.Collect()

            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "ReportViewer.aspx Page:BindReport  " + ex.ToString(), MethodBase.GetCurrentMethod.Name)


        Finally
            cryRpt.Close()
            cryRpt.Dispose()
            GC.Collect()
            CRV.Dispose()
            MainRPTDataSource.Dispose()

            If Not SubRPTDataSource Is Nothing Then
                SubRPTDataSource.Dispose()
            End If
            If Not SubRPTDataSource1 Is Nothing Then
                SubRPTDataSource1.Dispose()
            End If
            If Not SubRPTDataSource2 Is Nothing Then
                SubRPTDataSource2.Dispose()
            End If
            If Not SubRPTDataSource3 Is Nothing Then
                SubRPTDataSource3.Dispose()
            End If
            If Not SubRPTDataSource4 Is Nothing Then
                SubRPTDataSource4.Dispose()
            End If
            If Not SubRPTDataSource5 Is Nothing Then
                SubRPTDataSource5.Dispose()
            End If

        End Try
    End Sub


    Private Sub ExportExcel()
        '   This procedure is to export the report to Excel
        Try


        Catch ex As Exception
            cryRpt.Close()
            cryRpt.Dispose()
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "ReportViewer.aspx Page:BindReport  " + ex.ToString(), MethodBase.GetCurrentMethod.Name)
            ' Call WriteToErrLog("Error In  FN  Genrport    in CMS Report  :RPrCMSForms.apsx," & Err.Number & " ," & Err.Description & " ,")
            Response.Write(Err.Description)
        End Try

    End Sub

    Protected Sub CRV_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles CRV.Unload
        Try
            cryRpt.Dispose()
            cryRpt.Close()
            CRV.Dispose()
            cryRpt = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "CRV_Unload  " + ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
            ' Page.MasterPageFile = "~/default/ArabicMaster.master"
        Else
            Lang = CtlCommon.Lang.EN
            'Page.MasterPageFile = "~/default/NewMaster.master"
        End If

        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
        dir = GetPageDirection()
    End Sub
    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            Try
                If Page.IsPostBack Then

                    If cryRpt IsNot Nothing Then
                        If cryRpt.IsLoaded Then
                            cryRpt.Close()
                        End If
                        cryRpt.Dispose()
                    End If

                End If

            Catch ex As Exception

                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "Page_Unload  " + ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
            Finally
                GC.Collect()
            End Try

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), "Page_Unload  " + ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class
