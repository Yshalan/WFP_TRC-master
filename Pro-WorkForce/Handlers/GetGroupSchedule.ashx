<%@ WebHandler Language="VB" Class="GetGroupSchedule" %>

Imports System
Imports System.Web
Imports System.Data
Imports TA.Employees
Imports System.IO
Imports TA.ScheduleGroups 
Imports SmartV.UTILITIES
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
            Else
                SaveScheduleXML(context)
				
            End If
        End If
    End Sub
	
    Private Sub SaveScheduleXML(ByVal context As HttpContext)
        Dim useridd As String = SessionVariables.LoginUser.ID
        Dim textReader As StreamReader = New StreamReader(context.Request.InputStream)
        Dim strScheduleJSON As String = textReader.ReadToEnd()
        Dim lstScheduleJSON As New List(Of ScheduleGroupJSON)
		
        Dim serializer As New Script.Serialization.JavaScriptSerializer
        lstScheduleJSON = serializer.Deserialize(Of List(Of ScheduleGroupJSON))(strScheduleJSON)
		
        Dim AffectedRows As Integer
        Dim objScheduleJSON As New ScheduleGroupJSON
        objScheduleJSON.InsertUpdateDeleteSchedule(lstScheduleJSON, AffectedRows, useridd)
		
        context.Response.Write("[")
        GetScheduleXML(context)
		
        Dim JSONData As String
        JSONData = ",[{""AffectedRows"":" + AffectedRows.ToString() + ", " + "}]"
        context.Response.Write(JSONData.ToString.Trim)
        context.Response.Write("]")
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
        Dim useridd As String = SessionVariables.LoginUser.ID
        
        'Dim scheduleId As Integer
        
        'If Not context.Request.QueryString("schedule") Is Nothing Then
        '    If Not String.IsNullOrEmpty(context.Request.QueryString("schedule")) Then
        '        scheduleId = Convert.ToInt32(context.Request.QueryString("schedule"))
        '    End If
        'End If
		
        Dim JSON As String
        'Dim objEmp_WorkSchedule As New Emp_WorkSchedule()
        'Dim objEmp_Shifts As New Emp_Shifts()
        Dim objSchGroups As New ScheduleGroups
        Dim obj_Shifts As New ScheduleGroup_Shifts
		
        JSON = "[{""Groups"":" + objSchGroups.GetAllGroups_JSON(Month, Year, useridd) + ", " + """schedule"":" + obj_Shifts.GetByDate_JSON(Year, Month) + "}]"
		
        context.Response.Write(JSON)
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