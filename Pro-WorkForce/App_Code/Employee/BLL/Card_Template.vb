Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Employees

    Public Class Card_Template

#Region "Class Variables"

        Private _TemplateId As Integer
        Private _TemplateName As String
        Private _TemplateArabicName As String
        Private _TemplateFilePath As String
        Private objDALCard_Template As DALCard_Template

#End Region

#Region "Public Properties"

        Public Property TemplateId() As Integer
            Set(ByVal value As Integer)
                _TemplateId = value
            End Set
            Get
                Return (_TemplateId)
            End Get
        End Property

        Public Property TemplateName() As String
            Set(ByVal value As String)
                _TemplateName = value
            End Set
            Get
                Return (_TemplateName)
            End Get
        End Property

        Public Property TemplateArabicName() As String
            Set(ByVal value As String)
                _TemplateArabicName = value
            End Set
            Get
                Return (_TemplateArabicName)
            End Get
        End Property

        Public Property TemplateFilePath() As String
            Set(ByVal value As String)
                _TemplateFilePath = value
            End Set
            Get
                Return (_TemplateFilePath)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALCard_Template = New DALCard_Template()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALCard_Template.Add(_TemplateId, _TemplateName, _TemplateArabicName, _TemplateFilePath)
            App_EventsLog.Insert_ToEventLog("Add", _TemplateId, "Card_Template", "Card Template(s)")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALCard_Template.Update(_TemplateId, _TemplateName, _TemplateArabicName, _TemplateFilePath)
            App_EventsLog.Insert_ToEventLog("Update", _TemplateId, "Card_Template", "Card Template(s)")
            Return rslt
        End Function

        Public Function Card_TemplatePath_Insert() As Integer

            Dim rslt As Integer = objDALCard_Template.Card_TemplatePath_Insert(_TemplateId, _TemplateFilePath)
            App_EventsLog.Insert_ToEventLog("Card_TemplatePath_Insert", _TemplateId, "Card_Template", "Card Template(s)")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALCard_Template.Delete(_TemplateId)
            App_EventsLog.Insert_ToEventLog("Delete", _TemplateId, "Card_Template", "Card Template(s)")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALCard_Template.GetAll()

        End Function

        Public Function GetByPK() As Card_Template

            Dim dr As DataRow
            dr = objDALCard_Template.GetByPK(_TemplateId)

            If Not IsDBNull(dr("TemplateId")) Then
                _TemplateId = dr("TemplateId")
            End If
            If Not IsDBNull(dr("TemplateName")) Then
                _TemplateName = dr("TemplateName")
            End If
            If Not IsDBNull(dr("TemplateArabicName")) Then
                _TemplateArabicName = dr("TemplateArabicName")
            End If
            If Not IsDBNull(dr("TemplateFilePath")) Then
                _TemplateFilePath = dr("TemplateFilePath")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace