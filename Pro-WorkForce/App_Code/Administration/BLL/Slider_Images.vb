Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class Slider_Images

#Region "Class Variables"


        Private _ImageId As Integer
        Private _ImageName As String
        Private _ImagePath As String
        Private _ImageOrder As Integer
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime

        Private objDALSlider_Images As DALSlider_Images

#End Region

#Region "Public Properties"

        Public Property ImageId() As Integer
            Set(ByVal value As Integer)
                _ImageId = value
            End Set
            Get
                Return (_ImageId)
            End Get
        End Property

        Public Property ImageName() As String
            Set(ByVal value As String)
                _ImageName = value
            End Set
            Get
                Return (_ImageName)
            End Get
        End Property

        Public Property ImagePath() As String
            Set(ByVal value As String)
                _ImagePath = value
            End Set
            Get
                Return (_ImagePath)
            End Get
        End Property

        Public Property ImageOrder() As Integer
            Set(ByVal value As Integer)
                _ImageOrder = value
            End Set
            Get
                Return (_ImageOrder)
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

            objDALSlider_Images = New DALSlider_Images()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Dim rslt As Integer = objDALSlider_Images.Add(_ImageId, _ImageName, _ImagePath, _ImageOrder, _CREATED_BY)
            App_EventsLog.Insert_ToEventLog("Add", _ImageId, "Slider_Images", "Slider Announcements")
            Return rslt
        End Function

        Public Function Update() As Integer

            Dim rslt As Integer = objDALSlider_Images.Update(_ImageId, _ImageName, _ImagePath, _ImageOrder, _LAST_UPDATE_BY)
            App_EventsLog.Insert_ToEventLog("Update", _ImageId, "Slider_Images", "Slider Announcements")
            Return rslt
        End Function

        Public Function Delete() As Integer

            Dim rslt As Integer = objDALSlider_Images.Delete(_ImageId)
            App_EventsLog.Insert_ToEventLog("Delete", _ImageId, "Slider_Images", "Slider Announcements")
            Return rslt
        End Function

        Public Function GetAll() As DataTable

            Return objDALSlider_Images.GetAll()

        End Function

        Public Function GetByPK() As Slider_Images

            Dim dr As DataRow
            dr = objDALSlider_Images.GetByPK(_ImageId)

            If Not IsDBNull(dr("ImageId")) Then
                _ImageId = dr("ImageId")
            End If
            If Not IsDBNull(dr("ImageName")) Then
                _ImageName = dr("ImageName")
            End If
            If Not IsDBNull(dr("ImagePath")) Then
                _ImagePath = dr("ImagePath")
            End If
            If Not IsDBNull(dr("ImageOrder")) Then
                _ImageOrder = dr("ImageOrder")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace