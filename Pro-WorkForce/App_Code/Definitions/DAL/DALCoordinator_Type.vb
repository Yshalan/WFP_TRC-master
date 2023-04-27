Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALCoordinator_Type
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Coordinator_Type_Select As String = "Coordinator_Type_select"
        Private Coordinator_Type_Select_All As String = "Coordinator_Type_select_All"
        Private Coordinator_Type_Insert As String = "Coordinator_Type_Insert"
        Private Coordinator_Type_Update As String = "Coordinator_Type_Update"
        Private Coordinator_Type_Delete As String = "Coordinator_Type_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef CoordinatorTypeId As Integer, ByVal CoordinatorShortName As String, ByVal CoordinatorTypeName As String, ByVal CoordinatorTypeArabicName As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@CoordinatorTypeId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, CoordinatorTypeId)
                errNo = objDac.AddUpdateDeleteSPTrans(Coordinator_Type_Insert, sqlOut, New SqlParameter("@CoordinatorShortName", CoordinatorShortName),
               New SqlParameter("@CoordinatorTypeName", CoordinatorTypeName),
               New SqlParameter("@CoordinatorTypeArabicName", CoordinatorTypeArabicName),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then CoordinatorTypeId = sqlOut.Value Else CoordinatorTypeId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal CoordinatorTypeId As Integer, ByVal CoordinatorShortName As String, ByVal CoordinatorTypeName As String, ByVal CoordinatorTypeArabicName As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Coordinator_Type_Update, New SqlParameter("@CoordinatorTypeId", CoordinatorTypeId), _
               New SqlParameter("@CoordinatorShortName", CoordinatorShortName), _
               New SqlParameter("@CoordinatorTypeName", CoordinatorTypeName), _
               New SqlParameter("@CoordinatorTypeArabicName", CoordinatorTypeArabicName), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal CoordinatorTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Coordinator_Type_Delete, New SqlParameter("@CoordinatorTypeId", CoordinatorTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal CoordinatorTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Coordinator_Type_Select, New SqlParameter("@CoordinatorTypeId", CoordinatorTypeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Coordinator_Type_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace