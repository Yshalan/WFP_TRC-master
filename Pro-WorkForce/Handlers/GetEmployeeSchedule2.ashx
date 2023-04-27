<%@ WebHandler Language="VB" Class="GetEmployeeSchedule2" %>

Imports System
Imports System.Web
Imports System.Data
Imports TA.Employees
Imports System.IO

Imports SmartV.UTILITIES.ProjectCommon
Imports SmartV.UTILITIES
Imports System.Xml
Imports System.Web.UI.WebControls

Public Class GetEmployeeSchedule2 : Implements IHttpHandler, IRequiresSessionState
    Private objEmp_Shifts As Emp_Shifts
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

        Dim textReader As StreamReader = New StreamReader(context.Request.InputStream)
        Dim strScheduleJSON As String = textReader.ReadToEnd()
        Dim lstScheduleJSON As New List(Of ScheduleJSON)
		
        Dim serializer As New Script.Serialization.JavaScriptSerializer
        lstScheduleJSON = serializer.Deserialize(Of List(Of ScheduleJSON))(strScheduleJSON)
		
        Dim AffectedRows As String
        
        Dim objScheduleJSON As New ScheduleJSON
        Dim errNo As Integer
        errNo = objScheduleJSON.InsertUpdateDeleteSchedule(lstScheduleJSON, AffectedRows)
        Dim ValidationHrs As String = ""
        If errNo = -999 Then
            Dim dt As DataTable
            Dim EmployeeNo As String = ""
            Dim EmployeeName As String = ""
            Dim EmployeeArabicName As String = ""          
            Dim DayNo As String = ""
            Dim MinHours As String = ""
           
            dt = gets(lstScheduleJSON)
            If Not dt Is Nothing Then
                EmployeeName = dt.Rows(0)("EmployeeNo") + " - " + dt.Rows(0)("EmployeeName")
                EmployeeArabicName = dt.Rows(0)("EmployeeNo") + " - " + dt.Rows(0)("EmployeeArabicName")
                DayNo = dt.Rows(0)("DayNo")
                MinHours = dt.Rows(0)("MinHours")
            End If
            If SessionVariables.CultureInfo = "en-US" Then
                ValidationHrs = "Duration between Shifts exceed Allowed (" & MinHours & ") / Employee: " & EmployeeName & " in Day: " & DayNo
            Else
                ValidationHrs = "تجاوز الوقت المسموح به بين المناوبات " & MinHours & "  للموظف " & EmployeeArabicName & " في اليوم " & DayNo
                
            End If
            'MsgBox(ValidationHrs)
            'context.Response.Write("<script type='text/javascript'>alert('Hello, world');</script>")
            'Dim msgaaa = String.Format("<script language='javascript'>alert('" & ValidationHrs & "');</script>")
            'context.Response.Write(msgaaa)
            'context.Response.Write("<script type='text/javascript'>alert('" & ValidationHrs & "');</script>")
            'Return
        End If
       
        context.Response.Write("[")
        GetScheduleXML(context)
        Dim JSONData As String
    
            
            JSONData = ",[{""AffectedRows"":" + AffectedRows.ToString() + ", " + "}],[{""ValidationHrs"":" + """" + ValidationHrs.ToString() + """" + ", " + "}]"
   
     
        context.Response.Write(JSONData)
        context.Response.Write("]")
      
    End Sub
	
    Private Function gets(ByVal lstScheduleJSON As List(Of ScheduleJSON)) As DataTable
        Dim dt As DataTable
        
        objEmp_Shifts = New Emp_Shifts
        Dim ScheduleXml As New Xml
        Dim doc As New XmlDocument
        'Dim xmlDeclaration As XmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", "")
        'doc.InsertBefore(xmlDeclaration, doc.DocumentElement)
        Dim schedulesElement As XmlElement = doc.CreateElement("Schedules")
        doc.AppendChild(schedulesElement)

        For Each json As ScheduleJSON In lstScheduleJSON
            Dim scheduleElement As XmlElement = doc.CreateElement("Schedule")
            schedulesElement.AppendChild(scheduleElement)

            Dim EmpIdElement As XmlElement = doc.CreateElement("EmpId")
            EmpIdElement.InnerText = json.EmpId
            scheduleElement.AppendChild(EmpIdElement)

            Dim ShiftElement As XmlElement = doc.CreateElement("ShiftId")
            ShiftElement.InnerText = json.ShiftId
            scheduleElement.AppendChild(ShiftElement)

            Dim colorElement As XmlElement = doc.CreateElement("color")
            colorElement.InnerText = json.color
            scheduleElement.AppendChild(colorElement)

            Dim dayElement As XmlElement = doc.CreateElement("day")
            dayElement.InnerText = json.day
            scheduleElement.AppendChild(dayElement)

            Dim monthElement As XmlElement = doc.CreateElement("month")
            monthElement.InnerText = json.month
            scheduleElement.AppendChild(monthElement)

            Dim statusElement As XmlElement = doc.CreateElement("status")
            statusElement.InnerText = json.status
            scheduleElement.AppendChild(statusElement)

            Dim yearElement As XmlElement = doc.CreateElement("year")
            yearElement.InnerText = json.year
            scheduleElement.AppendChild(yearElement)

        Next
        ScheduleXml.Document = doc
        dt = objEmp_Shifts.GetValidtionMsg(ScheduleXml)
        Return dt
    End Function
    
        
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
		
        Dim scheduleId As Integer
        
        If Not context.Request.QueryString("schedule") Is Nothing Then
            If Not String.IsNullOrEmpty(context.Request.QueryString("schedule")) Then
                scheduleId = Convert.ToInt32(context.Request.QueryString("schedule"))
            End If
        End If
		
        Dim JSON As String
        Dim objEmp_WorkSchedule As New Emp_WorkSchedule()
        Dim objEmp_Shifts As New Emp_Shifts()
        Dim ManagerId As Integer = SmartV.UTILITIES.SessionVariables.LoginUser.FK_EmployeeId
        JSON = "[{""employees"":" + objEmp_WorkSchedule.GetAllEmployees_JSON2(Month, Year, scheduleId, ManagerId) + ", " + """schedule"":" + objEmp_Shifts.GetByDate_JSON(Year, Month) + "}]"
		
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