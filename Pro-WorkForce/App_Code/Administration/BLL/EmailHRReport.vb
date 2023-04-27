Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA.Admin

Public Class EmailHRReport

#Region "Class Variables"


Private  _ID As Long
Private  _ReportType As Integer
Private  _SendDate As DateTime
Private objDALEmailHRReport As DALEmailHRReport

#End Region

#Region "Public Properties"


Public Property ID() As Long
Set(ByVal value As Long )
_ID = value
End Set
Get
Return (_ID)
End Get
End Property


Public Property ReportType() As Integer
Set(ByVal value As Integer )
_ReportType = value
End Set
Get
Return (_ReportType)
End Get
End Property


Public Property SendDate() As DateTime
Set(ByVal value As DateTime )
_SendDate = value
End Set
Get
Return (_SendDate)
End Get
End Property

#End Region


#Region "Constructor"

Public Sub New()

objDALEmailHRReport = new DALEmailHRReport()

End Sub

#End Region

#region "Methods"

Public Function Add()  As Integer 

Return objDALEmailHRReport.Add( _ReportType, _SendDate)
End Function

Public Function Update() As Integer 

Return objDALEmailHRReport.Update( _ID, _ReportType, _SendDate)

End Function



Public Function Delete() As Integer 

Return objDALEmailHRReport.Delete( _ID)

End Function

public Function GetAll() As DataTable 

Return objDALEmailHRReport.GetAll()

End Function

public Function GetByPK() As EmailHRReport

Dim dr As DataRow
dr = objDALEmailHRReport.GetByPK( _ID)

If Not IsDBNull(dr("ID")) Then
_ID = dr("ID")
End If
If Not IsDBNull(dr("ReportType")) Then
_ReportType = dr("ReportType")
End If
If Not IsDBNull(dr("SendDate")) Then
_SendDate = dr("SendDate")
End If
Return Me
End Function

#End Region

 End Class
End namespace