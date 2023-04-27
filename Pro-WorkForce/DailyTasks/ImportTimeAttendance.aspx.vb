Imports TA.Definitions
Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Security
Imports SmartV.UTILITIES.ProjectCommon
Imports System.IO
Imports SmartV.DB
Imports System.Data.SqlClient

Partial Class ImportTimeAttendance
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Religion As Emp_Religion
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

#End Region

#Region "PageEvents"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            Header1.HeaderText = ResourceManager.GetString("ImportTimeAttendance", CultureInfo)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If BrFromFile.HasFile Then

            Dim extention As String = Path.GetExtension(BrFromFile.PostedFile.FileName).Trim()
            Dim fileName As String = Guid.NewGuid.ToString()
            Dim fPath As String = String.Empty
            fPath = Server.MapPath("..\CompanyLogo\\" + fileName + extention)

            If File.Exists(fPath) Then
                File.Delete(fPath)
            End If

            BrFromFile.PostedFile.SaveAs(fPath)
            Dim objExcelFileManager = New ExcelFileManager()
            Try

                objExcelFileManager.Path = fPath
                Dim dsResult = objExcelFileManager.ReadExcelFile()
                For Each dt As DataTable In dsResult.Tables
                    Dim xmlResponse As String = GetXmlString(dt)
                    If (xmlResponse <> "<NewDataSet />") Then
                        Dim dac As DAC = dac.getDAC
                        dac.AddUpdateDeleteSPTrans("ExcelTimeAttendanceInsert", New SqlParameter("@filepath", fPath), New SqlParameter("@xmlstr", xmlResponse))
                    End If
                Next
                CtlCommon.ShowMessage(Page, "Imported Successfully", "success")
            Catch ex As Exception
                CtlCommon.ShowMessage(Page, ex.Message, "success")
            Finally
                objExcelFileManager.Dispose()
                objExcelFileManager = Nothing
            End Try


        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

#End Region

#Region "Methods"

    Public Function GetXmlString(dt As DataTable) As String
        Dim dsTemp As New DataSet
        Dim dtCopy = dt.Copy()
        dsTemp.Tables.Clear()
        dtCopy.TableName = "Result"
        dsTemp.Tables.Add(dtCopy)
        Return dsTemp.GetXml()
    End Function

#End Region

End Class