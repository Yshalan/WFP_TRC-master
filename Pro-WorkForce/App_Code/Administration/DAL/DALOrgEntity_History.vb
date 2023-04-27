Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALOrgEntity_History
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OrgEntity_History_Select As String = "OrgEntity_History_select"
        Private OrgEntity_History_Select_All As String = "OrgEntity_History_select_All"
        Private OrgEntity_History_Insert As String = "OrgEntity_History_Insert"
        Private OrgEntity_History_Update As String = "OrgEntity_History_Update"
        Private OrgEntity_History_Delete As String = "OrgEntity_History_Delete"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EntityId As Integer, ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgEntity_History_Insert, New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EntityHistory_Id As Integer, ByVal FK_EntityId As Integer, ByVal FK_EmployeeId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgEntity_History_Update, New SqlParameter("@EntityHistory_Id", EntityHistory_Id), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EntityHistory_Id As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgEntity_History_Delete, New SqlParameter("@EntityHistory_Id", EntityHistory_Id))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EntityHistory_Id As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(OrgEntity_History_Select, New SqlParameter("@EntityHistory_Id", EntityHistory_Id)).Rows(0)
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
                objColl = objDac.GetDataTable(OrgEntity_History_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace