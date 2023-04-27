Imports Microsoft.VisualBasic
Namespace TA.Security.UI
    Public Class AISListItem
        Private pMyItem As ListItem
        Private pFormID As Integer

        Public Sub New()
            pMyItem = New ListItem
        End Sub


        Public Property myItem() As ListItem
            Get
                Return pMyItem
            End Get
            Set(ByVal value As ListItem)
                pMyItem = value
            End Set
        End Property

        Public Property formID() As Integer
            Get
                Return pFormID
            End Get
            Set(ByVal value As Integer)
                pFormID = value
            End Set
        End Property

    End Class

End Namespace
