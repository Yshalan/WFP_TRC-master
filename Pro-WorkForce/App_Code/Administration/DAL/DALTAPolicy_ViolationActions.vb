Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Admin
    Public Class DALTAPolicy_ViolationActions
        Inherits MGRBase
#Region "Class Variables"

        Private strConn As String
        Dim TAPolicy_ViolationActions_insert As String = "TAPolicy_ViolationActions_insert"
        Dim TAPolicy_ViolationActions_SelectAll As String = "TAPolicy_ViolationActions_SelectAll"
        Dim TAPolicy_ViolationActions_Select As String = "TAPolicy_ViolationActions_Select"
        Dim TAPolicy_ViolationActions_Update As String = "TAPolicy_ViolationActions_Update"
        Dim TAPolicy_ViolationActions_Delete As String = "TAPolicy_ViolationActions_Delete"
        Dim TAPolicy_ViolationActions_SelectAll_ForDDl As String = "TAPolicy_ViolationActions_SelectAll_ForDDl"
#End Region
#Region "Constructor"
        Public Sub New()

        End Sub
#End Region
#Region "Methods"

        Public Function Add(ByRef ActionId As Integer, ByVal ActionName As String, ByVal ActionArabicName As String, ByVal CREATED_BY As String) As Integer
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ActionId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ActionId)
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_ViolationActions_insert, sqlOut, _
               New SqlParameter("@ActionName", ActionName), _
               New SqlParameter("@ActionArabicName", ActionArabicName), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If Not IsDBNull(sqlOut.Value) Then
                    ActionId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetAll() As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(TAPolicy_ViolationActions_SelectAll, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function GetAllForDDL() As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(TAPolicy_ViolationActions_SelectAll_ForDDl, Nothing)

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function Update(ByVal ActionId As Integer, ByVal ActionName As String, ByVal ActionArabicName As String, ByVal LAST_UPDATE_BY As String) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_ViolationActions_Update, _
               New SqlParameter("@ActionId", ActionId), _
               New SqlParameter("@ActionName", ActionName), _
               New SqlParameter("@ActionArabicName", ActionArabicName), _
                             New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal ActionId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TAPolicy_ViolationActions_Delete, _
               New SqlParameter("@ActionId", ActionId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByPK(ByVal ActionId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TAPolicy_ViolationActions_Select, New SqlParameter("@ActionId", ActionId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function


#End Region
    End Class
End Namespace
