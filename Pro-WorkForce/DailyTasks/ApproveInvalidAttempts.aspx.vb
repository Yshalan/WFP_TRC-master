Imports TA.DailyTasks
Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES

Partial Class DailyTasks_ApproveInvalidAttempts
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objINVALID_ATTEMPTS As INVALID_ATTEMPTS
    Private objEmployee As Employee
    Private objEmpMove As Emp_Move
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objREADER_KEYS As READER_KEYS
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Enum TransactionStatus
        Rejected = 0
        Approved = 1
    End Enum

    Public Property TransactionId() As Integer
        Get
            Return ViewState("TransactionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("TransactionId") = value
        End Set
    End Property

    Public Property FileFullPath() As String
        Get
            Return ViewState("FileFullPath")
        End Get
        Set(ByVal value As String)
            ViewState("FileFullPath") = value
        End Set
    End Property

    Public Property tbl_id() As Long
        Get
            Return ViewState("tbl_id")
        End Get
        Set(ByVal value As Long)
            ViewState("tbl_id") = value
        End Set
    End Property

#End Region

#Region "Page Events"

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

    Protected Sub DailyTasks_ApproveInvalidAttempts_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("ApproveInvalidAttempts")
        End If
    End Sub

    Protected Sub dgrdInvalidAttempts_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdInvalidAttempts.NeedDataSource
        objINVALID_ATTEMPTS = New INVALID_ATTEMPTS
        Dim dt As DataTable
        dt = objINVALID_ATTEMPTS.GetAll_Invalid
        dgrdInvalidAttempts.DataSource = dt
    End Sub

    Protected Sub dgrdTAPolicy_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdInvalidAttempts.Skin))
    End Function

    Protected Sub dgrdInvalidAttempts_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdInvalidAttempts.SelectedIndexChanged
        objINVALID_ATTEMPTS = New INVALID_ATTEMPTS
        TransactionId = Convert.ToInt32(DirectCast(dgrdInvalidAttempts.SelectedItems(0), GridDataItem).GetDataKeyValue("TransactionId").ToString())

        FillControls()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If TransactionId = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("InvalidTrans_Select"))
        Else
            If rblDecision.SelectedValue = 1 Then
                ApproveTransactionDecision()
            Else
                RejectTransactionDecision()
            End If
        End If
        
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        objEmployee = New Employee
        objINVALID_ATTEMPTS = New INVALID_ATTEMPTS

        With objINVALID_ATTEMPTS
            .Id = TransactionId
            .Get_ReasonName_ByPK()
            lblDateVal.Text = .M_Date.ToShortDateString
            lblTimeVal.Text = .M_Time.ToString("HH:mm")
            lblTypeVal.Text = IIf(Lang = CtlCommon.Lang.AR, .ReasonArabicName, .ReasonName)
            img_ReaderImage.ImageUrl = "data:image/jpg;base64," & .EMP_IMAGE() & ""
       
        End With

        With objEmployee
            .EmployeeId = objINVALID_ATTEMPTS.FK_EmployeeId
            .GetByPK()
            lblEmployeeNoVal.Text = .EmployeeNo
            lblEmployeeNameVal.Text = .EmployeeName

            If .EmpImagePath <> Nothing Then
                FileFullPath = .EmpImagePath
                Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(.EmpImagePath))
                Dim imgSrc As String = String.Format("data:image/gif;base64,{0}", imgURL)
                img_EmpImage.ImageUrl = imgSrc
            Else
                If .Gender = "f" Then
                    img_EmpImage.ImageUrl = "~/images/no_photo_female.jpg"
                Else
                    img_EmpImage.ImageUrl = "~/images/nophoto.jpg"
                End If

            End If

        End With



    End Sub

    Private Sub FillGrid()
        objINVALID_ATTEMPTS = New INVALID_ATTEMPTS
        Dim dt As DataTable
        dt = objINVALID_ATTEMPTS.GetAll_Invalid
        dgrdInvalidAttempts.DataSource = dt
        dgrdInvalidAttempts.DataBind()
    End Sub

    Public Function ConvertImageFiletoBytes(ByVal ImageFilePath As String) As Byte()
        Dim _tempByte() As Byte = Nothing
        If ImageFilePath <> Nothing Then
            If String.IsNullOrEmpty(ImageFilePath) = True Then
                Throw New ArgumentNullException("Image File Name Cannot be Null or Empty", "ImageFilePath")
                Return Nothing
            End If
        End If
        Try
            Dim _fileInfo As New IO.FileInfo(ImageFilePath)
            Dim _NumBytes As Long = _fileInfo.Length
            Dim _FStream As New IO.FileStream(ImageFilePath, IO.FileMode.Open, IO.FileAccess.Read)
            Dim _BinaryReader As New IO.BinaryReader(_FStream)
            _tempByte = _BinaryReader.ReadBytes(Convert.ToInt32(_NumBytes))
            _fileInfo = Nothing
            _NumBytes = 0
            _FStream.Close()
            _FStream.Dispose()
            _BinaryReader.Close()
            Return _tempByte
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Private Sub ClearAll()
        lblEmployeeNoVal.Text = String.Empty
        lblEmployeeNameVal.Text = String.Empty
        lblDateVal.Text = String.Empty
        lblTimeVal.Text = String.Empty
        lblTypeVal.Text = String.Empty
        rblDecision.ClearSelection()
        img_EmpImage.ImageUrl = ""
        img_ReaderImage.ImageUrl = ""
        TransactionId = 0
    End Sub

    Private Sub ApproveTransactionDecision()
        objEmpMove = New Emp_Move
        objRECALC_REQUEST = New RECALC_REQUEST
        objREADER_KEYS = New READER_KEYS
        objEmployee = New Employee
        objINVALID_ATTEMPTS = New INVALID_ATTEMPTS

        Dim temp_date As Date
        Dim temp_str_date As String
        Dim formats As String() = New String() {"HHmm"}
        Dim temp_time As DateTime
        Dim temp_str_time As String
        Dim err As Integer

        With objINVALID_ATTEMPTS
            .Id = TransactionId
            .GetByPK()
            .TransactionStatus = TransactionStatus.Approved
            .UpdateStatus()
        End With


        objEmpMove.FK_EmployeeId = objINVALID_ATTEMPTS.FK_EmployeeId
        objEmpMove.Reader = objINVALID_ATTEMPTS.Reader
        objEmpMove.Status = 1
        objEmpMove.IsManual = False
        objEmpMove.CREATED_BY = SessionVariables.LoginUser.ID
        objREADER_KEYS.READER_KEY = objINVALID_ATTEMPTS.Reason
        objREADER_KEYS.GetByPK()

        objEmpMove.FK_ReasonId = objREADER_KEYS.CHANGE_TO
        objEmpMove.Type = objREADER_KEYS.Type

        objEmpMove.MoveDate = objINVALID_ATTEMPTS.M_Date
        temp_date = objINVALID_ATTEMPTS.M_Date
        temp_str_date = DateToString(temp_date)
        objEmpMove.M_DATE_NUM = temp_str_date
        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(temp_str_date)
        objRECALC_REQUEST.EMP_NO = objINVALID_ATTEMPTS.FK_EmployeeId
        temp_time = objINVALID_ATTEMPTS.M_Time
        objEmpMove.MoveTime = temp_time
        temp_str_time = temp_time.Minute + temp_time.Hour * 60
        objEmpMove.M_TIME_NUM = temp_str_time
        objEmpMove.IsFromInvalid = True

        If tbl_id = 0 Then
            err = objEmpMove.Add()
        Else
            objEmpMove.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            objEmpMove.MoveId = tbl_id
            err = objEmpMove.Update()
        End If
        If err = 0 Then
            Dim err2 As Integer
            Dim count As Integer
            While count < 5
                err2 = objRECALC_REQUEST.RECALCULATE()
                If err2 = 0 Then
                    Exit While
                End If
                count += 1
            End While
        End If

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
            ClearAll()
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
        End If

    End Sub

    Private Sub RejectTransactionDecision()
        objINVALID_ATTEMPTS = New INVALID_ATTEMPTS
        Dim err As Integer = -1

        With objINVALID_ATTEMPTS
            .Id = TransactionId
            .TransactionStatus = TransactionStatus.Approved
            err = .UpdateStatus()
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
            ClearAll()
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
        End If
    End Sub

#End Region

End Class
