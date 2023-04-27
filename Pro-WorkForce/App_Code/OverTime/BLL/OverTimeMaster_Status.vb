Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA.OverTime

Public Class OverTimeMaster_Status

#Region "Class Variables"


Private  _StatusID As Integer
Private  _Desc_En As String
Private  _Desc_Ar As String
Private objDALOverTimeMaster_Status As DALOverTimeMaster_Status

#End Region

#Region "Public Properties"


Public Property StatusID() As Integer
Set(ByVal value As Integer )
_StatusID = value
End Set
Get
Return (_StatusID)
End Get
End Property


Public Property Desc_En() As String
Set(ByVal value As String )
_Desc_En = value
End Set
Get
Return (_Desc_En)
End Get
End Property


Public Property Desc_Ar() As String
Set(ByVal value As String )
_Desc_Ar = value
End Set
Get
Return (_Desc_Ar)
End Get
End Property

#End Region


#Region "Constructor"

Public Sub New()

objDALOverTimeMaster_Status = new DALOverTimeMaster_Status()

End Sub

#End Region

#region "Methods"

Public Function Add()  As Integer 

Return objDALOverTimeMaster_Status.Add( _Desc_En, _Desc_Ar)
End Function

Public Function Update() As Integer 

Return objDALOverTimeMaster_Status.Update( _StatusID, _Desc_En, _Desc_Ar)

End Function



Public Function Delete() As Integer 

Return objDALOverTimeMaster_Status.Delete( _StatusID)

End Function

public Function GetAll() As DataTable 

Return objDALOverTimeMaster_Status.GetAll()

End Function

public Function GetByPK() As OverTimeMaster_Status

Dim dr As DataRow
dr = objDALOverTimeMaster_Status.GetByPK( _StatusID)

If Not IsDBNull(dr("StatusID")) Then
_StatusID = dr("StatusID")
End If
If Not IsDBNull(dr("Desc_En")) Then
_Desc_En = dr("Desc_En")
End If
If Not IsDBNull(dr("Desc_Ar")) Then
_Desc_Ar = dr("Desc_Ar")
End If
Return Me
End Function

#End Region

 End Class
End namespace