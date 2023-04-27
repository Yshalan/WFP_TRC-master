Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA_Packages

Public Class Packages

#Region "Class Variables"


Private  _PackageId As Integer
Private  _PackageName As String
Private  _Forms As String
Private objDALPackages As DALPackages

#End Region

#Region "Public Properties"


Public Property PackageId() As Integer
Set(ByVal value As Integer )
_PackageId = value
End Set
Get
Return (_PackageId)
End Get
End Property


Public Property PackageName() As String
Set(ByVal value As String )
_PackageName = value
End Set
Get
Return (_PackageName)
End Get
End Property


Public Property Forms() As String
Set(ByVal value As String )
_Forms = value
End Set
Get
Return (_Forms)
End Get
End Property

#End Region


#Region "Constructor"

Public Sub New()

objDALPackages = new DALPackages()

End Sub

#End Region

#region "Methods"

Public Function Add()  As Integer 

Return objDALPackages.Add( _PackageId, _PackageName, _Forms)
End Function

Public Function Update() As Integer 

Return objDALPackages.Update( _PackageId, _PackageName, _Forms)

End Function



Public Function Delete() As Integer 

Return objDALPackages.Delete( _PackageId)

End Function

public Function GetAll() As DataTable 

Return objDALPackages.GetAll()

End Function

public Function GetByPK() As Packages

Dim dr As DataRow
dr = objDALPackages.GetByPK( _PackageId)

If Not IsDBNull(dr("PackageId")) Then
_PackageId = dr("PackageId")
End If
If Not IsDBNull(dr("PackageName")) Then
_PackageName = dr("PackageName")
End If
If Not IsDBNull(dr("Forms")) Then
_Forms = dr("Forms")
End If
Return Me
        End Function


#End Region

 End Class
End namespace