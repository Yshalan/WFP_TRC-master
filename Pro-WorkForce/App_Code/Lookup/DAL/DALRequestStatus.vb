Imports Microsoft.VisualBasic
Imports ST.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports ST.UTILITIES
Imports SmartV.UTILITIES
Imports SmartV.DB


Namespace TA.Lookup

    Public Class DALRequestStatus
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private RequestStatus_Select As String = "RequestStatus_select"
        Private RequestStatus_Select_All As String = "RequestStatus_select_All"
        Private RequestStatus_Insert As String = "RequestStatus_Insert"
        Private RequestStatus_Update As String = "RequestStatus_Update"
        Private RequestStatus_Delete As String = "RequestStatus_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal StatusId As Integer, ByVal StatusName As String, ByVal StatusNameArabic As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RequestStatus_Insert, New SqlParameter("@StatusId", StatusId), _
               New SqlParameter("@StatusName", StatusName), _
               New SqlParameter("@StatusNameArabic", StatusNameArabic))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal StatusId As Integer, ByVal StatusName As String, ByVal StatusNameArabic As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RequestStatus_Update, New SqlParameter("@StatusId", StatusId), _
               New SqlParameter("@StatusName", StatusName), _
               New SqlParameter("@StatusNameArabic", StatusNameArabic))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal StatusId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RequestStatus_Delete, New SqlParameter("@StatusId", StatusId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal StatusId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(RequestStatus_Select, New SqlParameter("@StatusId", StatusId)).Rows(0)
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
                objColl = objDac.GetDataTable(RequestStatus_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace