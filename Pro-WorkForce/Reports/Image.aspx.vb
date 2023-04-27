Imports TA.Reports
Imports System.Data
Partial Class Reports_Image
    Inherits System.Web.UI.Page

    Private objReport As Report
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents myImage As System.Web.UI.HtmlControls.HtmlImage
    'Protected WithEvents mainDiv As System.Web.UI.HtmlControls.HtmlGenericControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            objReport = New Report
            'If Not Page.IsPostBack Then
            If Not Request.QueryString("Id") Is Nothing Then
                Dim id As String = Request.QueryString("ID").ToString
                Dim intId As Integer
                Dim str As String
                'Dim DBCmd As OleDb.OleDbCommand
                If IsNumeric(id) Then
                    id = id.Replace(",", "")
                    id = id.Replace(".00", "")
                    intId = Convert.ToInt32(id)
                End If
                Dim dr As DataRow
                objReport.ID = intId
                dr = objReport.Get_Invalid_EmployeeImg()
                str = dr(0).ToString
                'DBCmd = New OleDb.OleDbCommand("SELECT EMP_IMAGE From INVALID_ATTEMPTS Where ID=" & intId, DBConn)
                'If OpenDBConnection() <> "" Then Exit Sub
                'Dim DBdr As OleDb.OleDbDataReader
                'DBdr = DBCmd.ExecuteReader()

                'If DBdr.Read = True Then
                '    If Not DBdr("EMP_IMAGE") Is Nothing Then
                '        str = DBdr("EMP_IMAGE")
                '    End If
                'End If

                'Dim imgURL As String = Convert.ToBase64String(ConvertImageFiletoBytes(str))
                'Dim imgSrc As String = String.Format("data:/image/gif;base64,{0}", imgURL)

                'myImage.Src = imgSrc
                Dim innerHtml As String = "<img alt='photo' id='imgPhoto' src='" & "data:image/jpg;base64," & str & "' />"
                'mainDiv.InnerHtml = innerHtml
                ' Dim innerHtml As String = str
                Response.Write(innerHtml)

                'DBdr.Close()

                'DBCmd.Dispose()

            End If
            'End If
        Catch ex As Exception

        End Try

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

End Class
