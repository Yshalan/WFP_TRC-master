
Partial Class UserColntrols_PageHeader
    Inherits System.Web.UI.UserControl

#Region "Properties"

    Public WriteOnly Property HeaderText() As String

        Set(ByVal value As String)
            LbHeader.Text = value
        End Set
    End Property

#End Region

   
End Class
