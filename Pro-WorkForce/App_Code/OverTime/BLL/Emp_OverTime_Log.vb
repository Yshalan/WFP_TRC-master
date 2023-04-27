Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.OverTime

    Public Class Emp_OverTime_Log

#Region "Class Variables"


        Private _LogID As Long
        Private _FK_OT_MasterID As Long
        Private _FK_ActionEmployeeID As Long
        Private _FK_OTDecisionID As Integer
        Private _ActionDate As DateTime
        Private _Remarks As String

        Private objDALEmp_OverTime_Log As DALEmp_OverTime_Log

#End Region

#Region "Public Properties"

        Public Property Remarks() As String
            Set(ByVal value As String)
                _Remarks = value
            End Set
            Get
                Return (_Remarks)
            End Get
        End Property
        Public Property LogID() As Long
            Set(ByVal value As Long)
                _LogID = value
            End Set
            Get
                Return (_LogID)
            End Get
        End Property


        Public Property FK_OT_MasterID() As Long
            Set(ByVal value As Long)
                _FK_OT_MasterID = value
            End Set
            Get
                Return (_FK_OT_MasterID)
            End Get
        End Property


        Public Property FK_ActionEmployeeID() As Long
            Set(ByVal value As Long)
                _FK_ActionEmployeeID = value
            End Set
            Get
                Return (_FK_ActionEmployeeID)
            End Get
        End Property


        Public Property FK_OTDecisionID() As Integer
            Set(ByVal value As Integer)
                _FK_OTDecisionID = value
            End Set
            Get
                Return (_FK_OTDecisionID)
            End Get
        End Property


        Public Property ActionDate() As DateTime
            Set(ByVal value As DateTime)
                _ActionDate = value
            End Set
            Get
                Return (_ActionDate)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_OverTime_Log = New DALEmp_OverTime_Log()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_OverTime_Log.Add(_FK_OT_MasterID, _FK_ActionEmployeeID, _FK_OTDecisionID, _ActionDate, _Remarks)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_OverTime_Log.Update(_LogID, _FK_OT_MasterID, _FK_ActionEmployeeID, _FK_OTDecisionID, _ActionDate, _Remarks)

        End Function



        Public Function Delete() As Integer

            Return objDALEmp_OverTime_Log.Delete(_LogID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_OverTime_Log.GetAll()

        End Function

        Public Function GetByPK() As Emp_OverTime_Log

            Dim dr As DataRow
            dr = objDALEmp_OverTime_Log.GetByPK(_LogID)

            If Not IsDBNull(dr("LogID")) Then
                _LogID = dr("LogID")
            End If
            If Not IsDBNull(dr("FK_OT_MasterID")) Then
                _FK_OT_MasterID = dr("FK_OT_MasterID")
            End If
            If Not IsDBNull(dr("FK_ActionEmployeeID")) Then
                _FK_ActionEmployeeID = dr("FK_ActionEmployeeID")
            End If
            If Not IsDBNull(dr("FK_OTDecisionID")) Then
                _FK_OTDecisionID = dr("FK_OTDecisionID")
            End If
            If Not IsDBNull(dr("ActionDate")) Then
                _ActionDate = dr("ActionDate")
            End If
            If Not IsDBNull(dr("Remarks")) Then
                _Remarks = dr("Remarks")
            End If
            Return Me
        End Function
        Public Function CheckIfExists() As Boolean

            Return objDALEmp_OverTime_Log.CheckIfExists(_FK_ActionEmployeeID)

        End Function
#End Region

    End Class
End Namespace