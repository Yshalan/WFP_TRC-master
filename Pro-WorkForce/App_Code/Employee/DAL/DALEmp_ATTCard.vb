Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Employees

    Public Class DALEmp_ATTCard
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_ATTCard_Select As String = "Emp_ATTCard_select"
        Private Emp_ATTCard_Select_All As String = "Emp_ATTCard_select_All"
        Private Emp_ATTCard_Insert As String = "Emp_ATTCard_Insert"
        Private Emp_ATTCard_Update As String = "Emp_ATTCard_Update"
        Private Emp_ATTCard_Delete As String = "Emp_ATTCard_Delete"
        Private GetEmp_ATTCardDetails As String = "GetEmp_ATTCardDetails"
        Private CheckActive_EmpCard As String = "CheckActive_EmpCard"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal CardId As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Active As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_ATTCard_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@CardId", CardId), _
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@Active", Active), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Long, ByVal CardId As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Active As Boolean, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_ATTCard_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@CardId", CardId), _
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, DBNull.Value, FromDate)), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
               New SqlParameter("@Active", Active), _
               New SqlParameter("@LAST_UPDATE_BY", IIf(LAST_UPDATE_BY Is Nothing, DBNull.Value, LAST_UPDATE_BY)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Long, ByVal CardId As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_ATTCard_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@CardId", CardId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Long, ByVal CardId As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_ATTCard_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@CardId", CardId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_ATTCard_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmp_ATTCardsDetails(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetEmp_ATTCardDetails, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetActiveCard(ByVal FK_EmployeeId As Integer) As Integer
            objDac = DAC.getDAC()
            Dim objColl As Integer
            Try
                objColl = objDac.AddUpdateDeleteSP(CheckActive_EmpCard, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace