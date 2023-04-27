Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALEmp_WorkLocation_Beacon
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Emp_WorkLocation_Beacon_Select As String = "Emp_WorkLocation_Beacon_select"
        Private Emp_WorkLocation_Beacon_Select_All As String = "Emp_WorkLocation_Beacon_select_All"
        Private Emp_WorkLocation_Beacon_Insert As String = "Emp_WorkLocation_Beacon_Insert"
        Private Emp_WorkLocation_Beacon_Update As String = "Emp_WorkLocation_Beacon_Update"
        Private Emp_WorkLocation_Beacon_Delete As String = "Emp_WorkLocation_Beacon_Delete"
        Private Emp_WorkLocation_Beacon_Select_ByFK_WorkLocationId As String = "Emp_WorkLocation_Beacon_Select_ByFK_WorkLocationId"
        Private Emp_WorkLocation_Beacon_bulk_insert As String = "Emp_WorkLocation_Beacon_bulk_insert"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal BeaconId As Integer, ByVal BeaconNo As String, ByVal BeaconDesc As String, ByVal BeaconExpiryDate As DateTime, ByVal BeaconTransType As String, ByVal FK_WorkLocationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@BeaconId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, BeaconId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Beacon_Insert, sqlOut, New SqlParameter("@BeaconNo", BeaconNo), _
               New SqlParameter("@BeaconDesc", BeaconDesc), _
               New SqlParameter("@BeaconExpiryDate", BeaconExpiryDate), _
               New SqlParameter("@BeaconTransType", BeaconTransType), _
               New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId))
                If Not IsDBNull(sqlOut.Value) Then
                    BeaconId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Add_Bulk(ByVal xml As String, ByVal FK_WorkLocationId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Beacon_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal BeaconId As Integer, ByVal BeaconNo As String, ByVal BeaconDesc As String, ByVal BeaconExpiryDate As DateTime, ByVal BeaconTransType As String, ByVal FK_WorkLocationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Beacon_Update, New SqlParameter("@BeaconId", BeaconId), _
               New SqlParameter("@BeaconNo", BeaconNo), _
               New SqlParameter("@BeaconDesc", BeaconDesc), _
               New SqlParameter("@BeaconExpiryDate", BeaconExpiryDate), _
               New SqlParameter("@BeaconTransType", BeaconTransType), _
               New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal BeaconId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Beacon_Delete, New SqlParameter("@BeaconId", BeaconId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal BeaconId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_WorkLocation_Beacon_Select, New SqlParameter("@BeaconId", BeaconId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_Beacon_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByFK_WorkLocationId(ByVal FK_WorkLocationId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_Beacon_Select_ByFK_WorkLocationId, New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region

    End Class
End Namespace