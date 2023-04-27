Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

namespace TA.LookUp

Public Class Holiday_logicalGroup

#Region "Class Variables"


Private  _FK_HolidayId As Integer
Private  _FK_LogicalGroupId As Integer
Private objDALHoliday_logicalGroup As DALHoliday_logicalGroup

#End Region

#Region "Public Properties"


Public Property FK_HolidayId() As Integer
Set(ByVal value As Integer )
_FK_HolidayId = value
End Set
Get
Return (_FK_HolidayId)
End Get
End Property


Public Property FK_LogicalGroupId() As Integer
Set(ByVal value As Integer )
_FK_LogicalGroupId = value
End Set
Get
Return (_FK_LogicalGroupId)
End Get
End Property

#End Region


#Region "Constructor"

Public Sub New()

objDALHoliday_logicalGroup = new DALHoliday_logicalGroup()

End Sub

#End Region

#region "Methods"

Public Function Add()  As Integer 

Return objDALHoliday_logicalGroup.Add( _FK_HolidayId, _FK_LogicalGroupId)
End Function

Public Function Update() As Integer 

Return objDALHoliday_logicalGroup.Update( _FK_HolidayId, _FK_LogicalGroupId)

End Function



Public Function Delete() As Integer 

            Return objDALHoliday_logicalGroup.Delete(_FK_HolidayId)

End Function

public Function GetAll() As DataTable 

Return objDALHoliday_logicalGroup.GetAll()

End Function

        Public Function GetByPK() As DataTable
            Return objDALHoliday_logicalGroup.GetByPK(_FK_HolidayId)
            'Dim dr As DataRow
            'dr = objDALHoliday_logicalGroup.GetByPK( _FK_HolidayId, _FK_LogicalGroupId)

            'If Not IsDBNull(dr("FK_HolidayId")) Then
            '_FK_HolidayId = dr("FK_HolidayId")
            'End If
            'If Not IsDBNull(dr("FK_LogicalGroupId")) Then
            '_FK_LogicalGroupId = dr("FK_LogicalGroupId")
            'End If
            'Return Me
        End Function

#End Region

        Public Function GetAll_LogicalGroup() As DataTable
            Return objDALHoliday_logicalGroup.GetAll_LogicalGroup()
        End Function

 End Class
End namespace