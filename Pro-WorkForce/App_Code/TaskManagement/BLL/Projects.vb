Imports System.Data
Imports TA.Events

Namespace TA.TaskManagement

    Public Class Projects

#Region "Class Variables"

        Private _ProjectId As Long
        Private _ProjectName As String
        Private _ProjectArabicName As String
        Private _Project_Description As String
        Private _Project_ArabicDescription As String
        Private _PlannedStartDate As DateTime
        Private _PlannedEndDate As DateTime
        Private _ActualStartDate As DateTime
        Private _ActualEndDate As DateTime
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private objDALProjects As DALProjects

#End Region

#Region "Public Properties"


        Public Property ProjectId() As Long
            Set(ByVal value As Long)
                _ProjectId = value
            End Set
            Get
                Return (_ProjectId)
            End Get
        End Property


        Public Property ProjectName() As String
            Set(ByVal value As String)
                _ProjectName = value
            End Set
            Get
                Return (_ProjectName)
            End Get
        End Property


        Public Property ProjectArabicName() As String
            Set(ByVal value As String)
                _ProjectArabicName = value
            End Set
            Get
                Return (_ProjectArabicName)
            End Get
        End Property


        Public Property Project_Description() As String
            Set(ByVal value As String)
                _Project_Description = value
            End Set
            Get
                Return (_Project_Description)
            End Get
        End Property


        Public Property Project_ArabicDescription() As String
            Set(ByVal value As String)
                _Project_ArabicDescription = value
            End Set
            Get
                Return (_Project_ArabicDescription)
            End Get
        End Property


        Public Property PlannedStartDate() As DateTime
            Set(ByVal value As DateTime)
                _PlannedStartDate = value
            End Set
            Get
                Return (_PlannedStartDate)
            End Get
        End Property


        Public Property PlannedEndDate() As DateTime
            Set(ByVal value As DateTime)
                _PlannedEndDate = value
            End Set
            Get
                Return (_PlannedEndDate)
            End Get
        End Property


        Public Property ActualStartDate() As DateTime
            Set(ByVal value As DateTime)
                _ActualStartDate = value
            End Set
            Get
                Return (_ActualStartDate)
            End Get
        End Property


        Public Property ActualEndDate() As DateTime
            Set(ByVal value As DateTime)
                _ActualEndDate = value
            End Set
            Get
                Return (_ActualEndDate)
            End Get
        End Property


        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property


        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property


        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property


        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALProjects = New DALProjects()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALProjects.Add(_ProjectId, _ProjectName, _ProjectArabicName, _Project_Description, _Project_ArabicDescription, _PlannedStartDate, _PlannedEndDate, _ActualStartDate, _ActualEndDate, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _ProjectId, "Projects", "Define Projects")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALProjects.Update(_ProjectId, _ProjectName, _ProjectArabicName, _Project_Description, _Project_ArabicDescription, _PlannedStartDate, _PlannedEndDate, _ActualStartDate, _ActualEndDate, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _ProjectId, "Projects", "Define Projects")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALProjects.Delete(_ProjectId)
            App_EventsLog.Insert_ToEventLog("Delete", _ProjectId, "Projects", "Define Projects")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALProjects.GetAll()

        End Function

        Public Function GetByPK() As Projects

            Dim dr As DataRow
            dr = objDALProjects.GetByPK(_ProjectId)

            If Not IsDBNull(dr("ProjectId")) Then
                _ProjectId = dr("ProjectId")
            End If
            If Not IsDBNull(dr("ProjectName")) Then
                _ProjectName = dr("ProjectName")
            End If
            If Not IsDBNull(dr("ProjectArabicName")) Then
                _ProjectArabicName = dr("ProjectArabicName")
            End If
            If Not IsDBNull(dr("Project_Description")) Then
                _Project_Description = dr("Project_Description")
            End If
            If Not IsDBNull(dr("Project_ArabicDescription")) Then
                _Project_ArabicDescription = dr("Project_ArabicDescription")
            End If
            If Not IsDBNull(dr("PlannedStartDate")) Then
                _PlannedStartDate = dr("PlannedStartDate")
            End If
            If Not IsDBNull(dr("PlannedEndDate")) Then
                _PlannedEndDate = dr("PlannedEndDate")
            End If
            If Not IsDBNull(dr("ActualStartDate")) Then
                _ActualStartDate = dr("ActualStartDate")
            End If
            If Not IsDBNull(dr("ActualEndDate")) Then
                _ActualEndDate = dr("ActualEndDate")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace