Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Reflection
Imports System.Data
Imports TA.OverTime
Partial Class OverTime_UserControls_UC_MyOTApplications
    Inherits System.Web.UI.UserControl
    Public Property SortDir() As String
        Get
            Return ViewState("SortDir")
        End Get
        Set(ByVal value As String)
            ViewState("SortDir") = value
        End Set
    End Property
    Public Property SortExep() As String
        Get
            Return ViewState("SortExep")
        End Get
        Set(ByVal value As String)
            ViewState("SortExep") = value
        End Set
    End Property
    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
        End Set
    End Property
    Public Property dtOTMyCurrent() As DataTable
        Get
            Return ViewState("dtOTMyCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtOTMyCurrent") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillMyAppsGrid()
        End If
    End Sub

    Private Sub FillMyAppsGrid()
        Try
            Dim obj As New Emp_OverTime_Master

            With obj
                .FK_EmployeeID = SessionVariables.LoginUser.FK_EmployeeId
            End With
            dtOTMyCurrent = obj.GetOTMyApplications()
            Dim dv As New DataView(dtOTMyCurrent)
            dv.Sort = SortExepression
            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()

            If Not dtOTMyCurrent Is Nothing Then
                If dtOTMyCurrent.Rows.Count > 0 Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCountOTMyApps.Text = "Search Result:" & dtOTMyCurrent.Rows.Count
                    Else
                        LbRowCountOTMyApps.Text = "نتيجة البحث " & dtOTMyCurrent.Rows.Count
                    End If
                Else
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCountOTMyApps.Text = "Search Result:" & 0
                    Else
                        LbRowCountOTMyApps.Text = "نتيجة البحث " & 0
                    End If
                End If
            Else
                If SessionVariables.CultureInfo = "en-US" Then
                    LbRowCountOTMyApps.Text = "Search Result:" & 0
                Else
                    LbRowCountOTMyApps.Text = "نتيجة البحث " & 0
                End If

            End If
            obj = Nothing

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
   
    
    Protected Sub gdvMyApplications_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gdvMyApplications.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtOTMyCurrent)

            dv.Sort = SortExepression
            SortExep = e.SortExpression
            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub gdvMyApplications_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdvMyApplications.PageIndexChanging
        Try
            gdvMyApplications.SelectedIndex = -1
            gdvMyApplications.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtOTMyCurrent)
            dv.Sort = SortExepression

            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
   
    
End Class
