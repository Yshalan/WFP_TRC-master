Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Reports

    Public Class DALDynamicReports
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private DynamicReports_Select As String = "DynamicReports_select"
        Private DynamicReports_Select_All As String = "DynamicReports_select_All"
        Private DynamicReports_Insert As String = "DynamicReports_Insert"
        Private DynamicReports_Update As String = "DynamicReports_Update"
        Private DynamicReports_Delete As String = "DynamicReports_Delete"
        Private DynamicReports_RetrieveViewDefinition As String = "DynamicReports_RetrieveViewDefinition"
        Private DynamicReports_ExecQuery As String = "DynamicReports_ExecQuery"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal ReportName As String, ByVal ViewName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(DynamicReports_Insert, New SqlParameter("@ReportName", ReportName), _
               New SqlParameter("@ViewName", ViewName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ReportId As Integer, ByVal ReportName As String, ByVal ViewName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(DynamicReports_Update, New SqlParameter("@ReportId", ReportId), _
               New SqlParameter("@ReportName", ReportName), _
               New SqlParameter("@ViewName", ViewName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ReportId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(DynamicReports_Delete, New SqlParameter("@ReportId", ReportId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ReportId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(DynamicReports_Select, New SqlParameter("@ReportId", ReportId)).Rows(0)
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
                objColl = objDac.GetDataTable(DynamicReports_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetViewDefinition(ByVal ViewName As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(DynamicReports_RetrieveViewDefinition, New SqlParameter("@ViewName", ViewName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function ExecSQLQuery(ByVal SQLQuery As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(DynamicReports_ExecQuery, New SqlParameter("@SQLQuery", SQLQuery))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace