Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports TA.Security.UI

Namespace SmartV.Security.UI
    Public Class AISTabPanel
        Inherits AjaxControlToolkit.TabPanel

        Private pModuleID As Integer
        Private pItems As List(Of AISListItem)

        Public Sub New()
            pItems = New List(Of AISListItem)
        End Sub
        Public Property moduleID() As Integer
            Get
                Return pModuleID
            End Get
            Set(ByVal value As Integer)
                pModuleID = value
            End Set
        End Property

        Public Property items() As List(Of AISListItem)
            Get
                Return pItems
            End Get
            Set(ByVal value As List(Of AISListItem))
                pItems = value
            End Set
        End Property

    End Class
End Namespace
