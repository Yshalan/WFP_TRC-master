<%@ WebHandler Language="vb" Class="ShowImage" %>
 
Imports System
Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class ShowImage
    Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "image/jpeg"
        Dim strm As Stream = ShowEmpImage()
        Dim buffer As Byte() = New Byte(4095) {}
        Dim byteSeq As Integer = strm.Read(buffer, 0, 4096)
 
        Do While byteSeq > 0
            context.Response.OutputStream.Write(buffer, 0, byteSeq)
            byteSeq = strm.Read(buffer, 0, 4096)
        Loop
    End Sub
 
    Public Function ShowEmpImage() As Stream
        Dim SqlCnnStr As String = ConfigurationManager.ConnectionStrings("ConnStr").ConnectionString
        Dim SQLConn As New System.Data.SqlClient.SqlConnection(SqlCnnStr)
        Dim sql As String = "SELECT top 1 LogoImage FROM APP_Settings"
        Dim cmd As SqlCommand = New SqlCommand(sql, SQLConn)
        cmd.CommandType = CommandType.Text
        SQLConn.Open()
        Dim img As Object = cmd.ExecuteScalar()
        Try
            Return New MemoryStream(CType(img, Byte()))
        Catch
            Return Nothing
        Finally
            SQLConn.Close()
        End Try
    End Function
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
 
 
End Class
    'http://www.dotnetcurry.com/ShowArticle.aspx?ID=129