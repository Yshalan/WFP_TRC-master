<%@ WebHandler Language="VB" Class="GetGroupSchedule" %>

Imports System
Imports System.Web
Imports System.Data
Imports TA.Employees
Imports System.IO
Imports TA.ScheduleGroups 
Imports SmartV.UTILITIES
Imports TA.Definitions

Public Class GetGroupSchedule : Implements IHttpHandler, IRequiresSessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If context.Request.RequestType = "GET" Then
            If (Not ValidateUser()) Then
                context.Response.Write("401")
            Else
                GetScheduleXML(context)
            End If
        End If
        If (context.Request.RequestType = "POST") Then
            context.Response.ContentType = "text/plain"
            If (Not ValidateUser()) Then
                context.Response.Write("401")
          End If
        End If
    End Sub
	
    ''' <summary>
    ''' GetScheduleXML
    ''' </summary>
    ''' <param name="context"></param>
    ''' <remarks></remarks>
    Private Sub GetScheduleXML(ByVal context As HttpContext)
        context.Response.ContentType = "application/json"			
        Dim Year As Integer
        Dim Month As Integer
        Year = Convert.ToInt32(context.Request.QueryString("year"))
        Month = Convert.ToInt32(context.Request.QueryString("month"))
        Dim JSONDetails As String
        Dim obj_workSchedule As New WorkSchedule_Shifts
        JSONDetails = "[{""leaves"":" + obj_workSchedule.GetEmployeeLeaveDetails_JSON(Year, Month) + "}]"
        context.Response.Write(JSONDetails.ToString.Trim)
    End Sub
	
    Private Function ValidateUser() As Boolean
        Return True
    End Function

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class