Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Xml
Imports SmartV.UTILITIES

Namespace TA.Employees
    ''' <summary>
    ''' This class helps us to serialize JSON data and convert it to usefull list of business object
    ''' for example: refere to ~\Handlers\GetEmployeeSchedule.ashx , Line:37
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ScheduleJSON

        ''' <summary>
        ''' to pass table of Emp_Shifts after user changes to save into the database, use only with SQL2008R2 and later
        ''' </summary>
        ''' <param name="ScheduleTable">table with columns (EmpId, ShiftId, color, day, month, status, year) status values: 0-NONE, 1-ADD, 2-UPDATE, 3-DELETE</param>
        ''' <param name="AffectedRows">output parameters, total affected rows in database</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsertUpdateDeleteSchedule_passTable(ByVal ScheduleTable As DataTable, ByRef AffectedRows As Integer) As Integer
            Dim objDALEmp_Shifts As New DALEmp_Shifts
            Return objDALEmp_Shifts.InsertUpdateDeleteSchedule_passTable(ScheduleTable, AffectedRows)
        End Function

        ''' <summary>
        ''' to pass table of Emp_Shifts after user changes to save into the database.
        ''' </summary>
        ''' <param name="lstScheduleJSON">table with columns (EmpId, ShiftId, color, day, month, status, year) status values: 0-NONE, 1-ADD, 2-UPDATE, 3-DELETE</param>
        ''' <param name="AffectedRows">output parameters, total affected rows in database</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsertUpdateDeleteSchedule(ByVal lstScheduleJSON As List(Of ScheduleJSON), ByRef AffectedRows As Integer) As Integer
            Dim objDALEmp_Shifts As New DALEmp_Shifts

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

                Dim Createdby As XmlElement = doc.CreateElement("createdby")
                Createdby.InnerText = SessionVariables.LoginUser.ID
                scheduleElement.AppendChild(Createdby)

            Next
            ScheduleXml.Document = doc
            Return objDALEmp_Shifts.InsertUpdateDeleteSchedule(ScheduleXml, AffectedRows)
        End Function

        Public Function InsertUpdateDeleteScheduleApprove(ByVal lstScheduleJSON As List(Of ScheduleJSON), ByRef AffectedRows As Integer) As Integer
            Dim objDALEmp_Shifts As New DALEmp_Shifts

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

                Dim Createdby As XmlElement = doc.CreateElement("createdby")
                Createdby.InnerText = SessionVariables.LoginUser.ID
                scheduleElement.AppendChild(Createdby)

            Next
            ScheduleXml.Document = doc
            Return objDALEmp_Shifts.InsertUpdateDeleteScheduleApprove(ScheduleXml, AffectedRows)
        End Function

        Public Function InsertUpdateDeleteScheduleRequest(ByVal lstScheduleJSON As List(Of ScheduleJSON), ByRef AffectedRows As Integer) As Integer
            Dim objDALEmp_Shifts As New DALEmp_Shifts

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

                Dim FK_StatusIdElement As XmlElement = doc.CreateElement("FK_StatusId")
                FK_StatusIdElement.InnerText = json.FK_StatusId
                scheduleElement.AppendChild(FK_StatusIdElement)

                Dim CreatedbyElement As XmlElement = doc.CreateElement("createdby")
                CreatedbyElement.InnerText = SessionVariables.LoginUser.ID
                scheduleElement.AppendChild(CreatedbyElement)

            Next
            ScheduleXml.Document = doc
            Return objDALEmp_Shifts.InsertUpdateDeleteScheduleRequest(ScheduleXml, AffectedRows)
        End Function
        Private _EmpId As Integer
        Private _ShiftId As Integer
        Private _color As String
        Private _day As Integer
        Private _month As Integer
        Private _status As Integer
        Private _year As Integer
        Private _FK_StatusId As Integer
        Private _CreatedBy As String

        Public Property EmpId() As Integer
            Get
                Return _EmpId
            End Get
            Set(ByVal value As Integer)
                _EmpId = value
            End Set
        End Property
        Public Property ShiftId() As Integer
            Get
                Return _ShiftId
            End Get
            Set(ByVal value As Integer)
                _ShiftId = value
            End Set
        End Property
        Public Property color() As String
            Get
                Return _color
            End Get
            Set(ByVal value As String)
                _color = value
            End Set
        End Property
        Public Property day() As Integer
            Get
                Return _day
            End Get
            Set(ByVal value As Integer)
                _day = value
            End Set
        End Property
        Public Property month() As Integer
            Get
                Return _month
            End Get
            Set(ByVal value As Integer)
                _month = value
            End Set
        End Property
        Public Property status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property
        Public Property year() As Integer
            Get
                Return _year
            End Get
            Set(ByVal value As Integer)
                _year = value
            End Set
        End Property
        Public Property FK_StatusId() As Integer
            Get
                Return _FK_StatusId
            End Get
            Set(ByVal value As Integer)
                _FK_StatusId = value
            End Set
        End Property
        Public Property CreatedBy() As String
            Get
                Return _CreatedBy
            End Get
            Set(ByVal value As String)
                _CreatedBy = value
            End Set
        End Property
    End Class
End Namespace
