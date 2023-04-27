
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Security

Partial Class Default_MasterPageTheme2
    Inherits System.Web.UI.MasterPage
#Region "Class Variables"

    Private objEmployee As Employee
    Private objSYSUsers As SYSUsers
    Private objSYS_Users_Security As SYS_Users_Security
    Private objSYSForms As SYSForms

    Public textalign As String
    Protected dir As String
    Public Lang As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim FromLogin As Boolean = IIf(Page.Request.QueryString("FromLogin") Is Nothing, 0, Page.Request.QueryString("FromLogin"))

        Dim path As String = HttpContext.Current.Request.Url.PathAndQuery
        If path.Contains("/SelfServices/PermissionRequest.aspx?Id=1") Then ' ---Permission Request view button postback solution
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/NursingPermissionRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/StudyPermissionRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/ManualEntryRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/ManualEntryRequest_Remote.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/LeavesRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/UpdateTransactionsRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/employee/Permissions.aspx?EmployeeId=") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/employee/EmpLeave_New2.aspx?EmployeeId=") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/TaskManagement/Tasks_Details.aspx?ProjectId=") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/OvertimeRequest.aspx?Id=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        ElseIf path.Contains("/SelfServices/ScheduleVisit.aspx?IsAdmin=1") Then
            path = HttpContext.Current.Request.Url.AbsolutePath
        End If
        objSYS_Users_Security = New SYS_Users_Security()
        objSYSForms = New SYSForms
        objSYS_Users_Security.FK_UserId = SessionVariables.LoginUser.ID
        Dim words As String() = path.Split(New Char() {"/"c})
        Dim toRemove = "/" + words(1)
        Dim url As String = path.Replace(toRemove, "")
        Dim strForms As String = SessionVariables.LicenseDetails.FormIds
        Dim arrForms As ArrayList = SplitLicenseForms(strForms)
        Dim dt As DataTable
        Dim LicVerifiedurl As Boolean

        '------------Check if the form url in license forms-----------'
        If (Not url = "/Default/home.aspx") Then
            If (Not url = "/Default/home.aspx?FromLogin=1") Then
                'If (Not url.Contains("/home.aspx")) Then
                dt = objSYSForms.GetFormIDByFormPath_WithoutGroupId(url)
                Dim i As Integer
                For i = 0 To arrForms.Count - 1
                    For Each row In dt.Rows
                        If row("FormId").ToString = arrForms.Item(i) Then
                            LicVerifiedurl = True
                        End If
                    Next
                Next
                If LicVerifiedurl = False Then
                    Response.Redirect("~/Default/home.aspx")
                End If
            End If
        End If
        '------------Check if the form url in license forms-----------'

        '------------Check if the form url in user privilage-----------'
        If Not objSYS_Users_Security.VerifyIfUserHasRights(url) Then
            If (Not url = "/Default/home.aspx") Then
                If (Not url = "/Default/home.aspx?FromLogin=1") Then
                    'If (Not url.Contains("/home.aspx")) Then
                    Response.Redirect("~/Default/home.aspx")
                End If

            End If
        End If
        '------------Check if the form url in user privilage-----------'

        If SessionVariables.LoginUser Is Nothing Then
            Response.Redirect("~/default/Logout.aspx")
        End If


        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
            End If
        End If
    End Sub

    Private Sub Default_MasterPageTheme2_Load(sender As Object, e As EventArgs) Handles Me.Load

        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
            Lang = "en"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
                Lang = "en"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = "ar"
            End If
        End If

        FillUserInfo()

    End Sub

    Protected Sub lnkbtnLanguage_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkbtnLanguage.Click
        SessionVariables.CultureInfo = "ar-JO"
        Response.Redirect(Me.Request.Url.ToString(), True)
    End Sub

    Protected Sub lnkbtnLogOut_Click(sender As Object, e As EventArgs) Handles lnkbtnLogOut.Click
        Response.Redirect("../default/logout.aspx")
    End Sub

    Public Function SplitLicenseForms(ByVal LicenseForms As String) As ArrayList

        Dim s As String = LicenseForms
        Dim arrForms As New ArrayList
        For Each value As String In s.Split(","c)
            arrForms.Add(Convert.ToInt32(value))
        Next
        Return arrForms
    End Function

#End Region

#Region "Methods"

    Private Sub ApplyDefaultTheme()

        'objAPP_Settings = New APP_Settings
        'objAPP_Settings.GetByPK()

        'If objAPP_Settings.DefaultTheme = 1 Then
        '    head1.Controls.Remove(lnkGreenDesign)
        '    head1.Controls.Remove(lnkRedDesign)
        '    head1.Controls.Remove(lnkBlueDesign)
        '    head1.Controls.Remove(lnkVioletDesign)
        '    head1.Controls.Remove(lnkGoldDesign)
        '    head1.Controls.Remove(lnkADMDesign)

        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkRedDesign)
        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkADMDesign)

        'ElseIf objAPP_Settings.DefaultTheme = 2 Then
        '    head1.Controls.Remove(lnkDofDesign)
        '    head1.Controls.Remove(lnkRedDesign)
        '    head1.Controls.Remove(lnkBlueDesign)
        '    head1.Controls.Remove(lnkVioletDesign)
        '    head1.Controls.Remove(lnkGoldDesign)
        '    head1.Controls.Remove(lnkADMDesign)

        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkRedDesign)
        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkADMDesign)

        'ElseIf objAPP_Settings.DefaultTheme = 3 Then
        '    head1.Controls.Remove(lnkDofDesign)
        '    head1.Controls.Remove(lnkBlueDesign)
        '    head1.Controls.Remove(lnkGreenDesign)
        '    head1.Controls.Remove(lnkVioletDesign)
        '    head1.Controls.Remove(lnkGoldDesign)
        '    head1.Controls.Remove(lnkADMDesign)

        '    head1.Controls.Add(lnkRedDesign)
        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkADMDesign)

        'ElseIf objAPP_Settings.DefaultTheme = 4 Then
        '    head1.Controls.Remove(lnkDofDesign)
        '    head1.Controls.Remove(lnkGreenDesign)
        '    head1.Controls.Remove(lnkRedDesign)
        '    head1.Controls.Remove(lnkVioletDesign)
        '    head1.Controls.Remove(lnkGoldDesign)
        '    head1.Controls.Remove(lnkADMDesign)

        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkRedDesign)
        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkADMDesign)

        'ElseIf objAPP_Settings.DefaultTheme = 5 Then
        '    head1.Controls.Remove(lnkDofDesign)
        '    head1.Controls.Remove(lnkGreenDesign)
        '    head1.Controls.Remove(lnkRedDesign)
        '    head1.Controls.Remove(lnkBlueDesign)
        '    head1.Controls.Remove(lnkGoldDesign)
        '    head1.Controls.Remove(lnkADMDesign)

        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkRedDesign)
        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkADMDesign)

        'ElseIf objAPP_Settings.DefaultTheme = 6 Then
        '    head1.Controls.Remove(lnkDofDesign)
        '    head1.Controls.Remove(lnkGreenDesign)
        '    head1.Controls.Remove(lnkRedDesign)
        '    head1.Controls.Remove(lnkBlueDesign)
        '    head1.Controls.Remove(lnkVioletDesign)
        '    head1.Controls.Remove(lnkADMDesign)

        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkRedDesign)
        '    head1.Controls.Add(lnkADMDesign)

        'ElseIf objAPP_Settings.DefaultTheme = 7 Then
        '    head1.Controls.Remove(lnkDofDesign)
        '    head1.Controls.Remove(lnkGreenDesign)
        '    head1.Controls.Remove(lnkRedDesign)
        '    head1.Controls.Remove(lnkBlueDesign)
        '    head1.Controls.Remove(lnkVioletDesign)
        '    head1.Controls.Remove(lnkGoldDesign)

        '    head1.Controls.Add(lnkADMDesign)
        '    head1.Controls.Add(lnkGoldDesign)
        '    head1.Controls.Add(lnkVioletDesign)
        '    head1.Controls.Add(lnkBlueDesign)
        '    head1.Controls.Add(lnkGreenDesign)
        '    head1.Controls.Add(lnkDofDesign)
        '    head1.Controls.Add(lnkRedDesign)
        'End If

    End Sub

    Protected Sub FillUserInfo()
        objSYSUsers = New SYSUsers
        objEmployee = New Employee
        objSYSUsers.ID = SessionVariables.LoginUser.ID
        objSYSUsers.GetUser()
        If objSYSUsers.UserType = 1 Then
            With objSYSUsers
                '.ID = SessionVariables.LoginUser.ID
                '.GetUser()
                lblLoginUser.Text = .fullName
                lblLoginDate.Text = SessionVariables.LoginDate
            End With
        Else
            With objSYSUsers
                .ID = SessionVariables.LoginUser.ID
                .GetUser()
                lblLoginUser.Text = .fullName
                lblLoginDate.Text = SessionVariables.LoginDate
            End With
        End If
        'objEmployee.EmployeeId = objSYSUsers.FK_EmployeeId
        'objEmployee.GetByPK()
        'If Not objEmployee.EmpImagePath = Nothing Then
        '    Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(objEmployee.EmpImagePath))
        '    Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
        '    EmpImage.Src = imgSrc
        'Else
        '    If objEmployee.Gender = "f" Then
        '        Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(Server.MapPath("../Svassets/img/user.png")))
        '        Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
        '        EmpImage.Src = imgSrc
        '    Else
        '        Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(Server.MapPath("../Svassets/img/user.png")))
        '        Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
        '        EmpImage.Src = imgSrc
        '    End If
        'End If
    End Sub

#End Region

End Class

