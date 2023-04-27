Imports Microsoft.VisualBasic
Namespace SmartV.Security.UI
    Public Class AISTabHeader
        Implements ITemplate

        Private chkText As String
        Private count As Integer
        Public pnl As Panel
        Public WithEvents chkBox As CheckBox
        Public lbl As Label

        Public Sub New()
            chkBox = New CheckBox
        End Sub

        Public Sub New(ByVal _count As Integer, ByVal _chkText As String)
            count = _count
            chkText = _chkText
            chkBox = New CheckBox
            chkBox.ID = String.Concat(chkText, count)
            chkBox.Text = chkText

        End Sub

        Public Sub InstantiateIn(ByVal container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
            chkBox = New CheckBox
            lbl = New Label
            chkBox.ID = String.Concat(chkText, count)
            lbl.Text = " " & chkText


            chkBox.AutoPostBack = True
            AddHandler chkBox.CheckedChanged, AddressOf chkDynamic_CheckedChanged
            container.Controls.Add(chkBox)

            container.Controls.Add(lbl)
        End Sub

        Protected Sub chkDynamic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If chkBox.Checked Then
                Dim t As AjaxControlToolkit.TabPanel = DirectCast(DirectCast(sender, System.Web.UI.WebControls.CheckBox).Parent.Parent, AjaxControlToolkit.TabPanel)
                For Each chList As Control In t.Controls
                    If TypeOf (chList) Is CheckBoxList Then
                        For i As Integer = 0 To DirectCast(chList, CheckBoxList).Items.Count - 1
                            DirectCast(chList, CheckBoxList).Items(i).Selected = True
                        Next
                    End If
                Next
            ElseIf Not chkBox.Checked Then
                Dim t As AjaxControlToolkit.TabPanel = DirectCast(DirectCast(sender, System.Web.UI.WebControls.CheckBox).Parent.Parent, AjaxControlToolkit.TabPanel)
                For Each chList As Control In t.Controls
                    If TypeOf (chList) Is CheckBoxList Then
                        For i As Integer = 0 To DirectCast(chList, CheckBoxList).Items.Count - 1
                            DirectCast(chList, CheckBoxList).Items(i).Selected = False
                        Next
                    End If
                Next
            End If
        End Sub
    End Class
End Namespace