Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Employees

    Public Class Emp_WorkLocation_Beacon

#Region "Class Variables"

        Private _BeaconId As Integer
        Private _BeaconNo As String
        Private _BeaconDesc As String
        Private _BeaconExpiryDate As DateTime
        Private _BeaconTransType As String
        Private _FK_WorkLocationId As Integer
        Private objDALEmp_WorkLocation_Beacon As DALEmp_WorkLocation_Beacon

#End Region

#Region "Public Properties"

        Public Property BeaconId() As Integer
            Set(ByVal value As Integer)
                _BeaconId = value
            End Set
            Get
                Return (_BeaconId)
            End Get
        End Property

        Public Property BeaconNo() As String
            Set(ByVal value As String)
                _BeaconNo = value
            End Set
            Get
                Return (_BeaconNo)
            End Get
        End Property

        Public Property BeaconDesc() As String
            Set(ByVal value As String)
                _BeaconDesc = value
            End Set
            Get
                Return (_BeaconDesc)
            End Get
        End Property

        Public Property BeaconExpiryDate() As DateTime
            Set(ByVal value As DateTime)
                _BeaconExpiryDate = value
            End Set
            Get
                Return (_BeaconExpiryDate)
            End Get
        End Property

        Public Property BeaconTransType() As String
            Set(ByVal value As String)
                _BeaconTransType = value
            End Set
            Get
                Return (_BeaconTransType)
            End Get
        End Property

        Public Property FK_WorkLocationId() As Integer
            Set(ByVal value As Integer)
                _FK_WorkLocationId = value
            End Set
            Get
                Return (_FK_WorkLocationId)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALEmp_WorkLocation_Beacon = New DALEmp_WorkLocation_Beacon()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_WorkLocation_Beacon.Add(_BeaconId, _BeaconNo, _BeaconDesc, _BeaconExpiryDate, _BeaconTransType, _FK_WorkLocationId)
        End Function

        Public Function Add_Bulk(ByVal DT As DataTable) As Integer
            Dim DS As New DataSet("DS")
            DT.TableName = "DT"
            DS.Tables.Add(DT)
            Dim StrXml As String = DS.GetXml

            Dim rslt As Integer = objDALEmp_WorkLocation_Beacon.Add_Bulk(StrXml, _FK_WorkLocationId)

            Return rslt
        End Function


        Public Function Update() As Integer

            Return objDALEmp_WorkLocation_Beacon.Update(_BeaconId, _BeaconNo, _BeaconDesc, _BeaconExpiryDate, _BeaconTransType, _FK_WorkLocationId)

        End Function

        Public Function Delete() As Integer

            Return objDALEmp_WorkLocation_Beacon.Delete(_BeaconId)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_WorkLocation_Beacon.GetAll()

        End Function

        Public Function GetByFK_WorkLocationId() As DataTable

            Return objDALEmp_WorkLocation_Beacon.GetByFK_WorkLocationId(_FK_WorkLocationId)

        End Function
        Public Function GetByPK() As Emp_WorkLocation_Beacon

            Dim dr As DataRow
            dr = objDALEmp_WorkLocation_Beacon.GetByPK(_BeaconId)

            If Not IsDBNull(dr("BeaconId")) Then
                _BeaconId = dr("BeaconId")
            End If
            If Not IsDBNull(dr("BeaconNo")) Then
                _BeaconNo = dr("BeaconNo")
            End If
            If Not IsDBNull(dr("BeaconDesc")) Then
                _BeaconDesc = dr("BeaconDesc")
            End If
            If Not IsDBNull(dr("BeaconExpiryDate")) Then
                _BeaconExpiryDate = dr("BeaconExpiryDate")
            End If
            If Not IsDBNull(dr("BeaconTransType")) Then
                _BeaconTransType = dr("BeaconTransType")
            End If
            If Not IsDBNull(dr("FK_WorkLocationId")) Then
                _FK_WorkLocationId = dr("FK_WorkLocationId")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace