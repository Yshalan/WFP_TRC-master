Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Forms

    Public Class DALEvents_Groups
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Events_Groups_Select As String = "Events_Groups_select"
        Private Events_Groups_Select_All As String = "Events_Groups_select_All"
        Private Events_Groups_Insert As String = "Events_Groups_Insert"
        Private Events_Groups_Update As String = "Events_Groups_Update"
        Private Events_Groups_Delete As String = "Events_Groups_Delete"
        Private Events_Groups_SelectAllDetails As String = "Events_Groups_SelectAllDetails"
        Private Events_Select_All_ForDDL As String = "Events_Select_All_ForDDL"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EventId As Integer, ByVal FK_GroupId As Integer, ByVal NumberOfEmployees As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Groups_Insert, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_GroupId", FK_GroupId), _
               New SqlParameter("@NumberOfEmployees", NumberOfEmployees))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EventId As Integer, ByVal FK_GroupId As Integer, ByVal NumberOfEmployees As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Groups_Update, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_GroupId", FK_GroupId), _
               New SqlParameter("@NumberOfEmployees", NumberOfEmployees))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EventId As Integer, ByVal FK_GroupId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Groups_Delete, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_GroupId", FK_GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EventId As Integer, ByVal FK_GroupId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Events_Groups_Select, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_GroupId", FK_GroupId)).Rows(0)
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
                objColl = objDac.GetDataTable(Events_Groups_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_Details(ByVal EventId As Integer) As DataTable ', ByVal GroupId As Integer

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Events_Groups_SelectAllDetails, New SqlParameter("@EventId", EventId)) ', _
                'New SqlParameter("@GroupId", GroupId)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
      
#End Region


    End Class
End Namespace