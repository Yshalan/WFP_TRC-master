Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Forms

    Public Class DALEvents_Employees
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Events_Employees_Select As String = "Events_Employees_select"
        Private Events_Employees_Select_All As String = "Events_Employees_select_All"
        Private Events_Employees_Insert As String = "Events_Employees_Insert"
        Private Events_Employees_Update As String = "Events_Employees_Update"
        Private Events_Employees_Delete As String = "Events_Employees_Delete"
        Private Get_EventGroups_ForDDL As String = "Get_EventGroups_ForDDL"
        Private Events_Employees_SelectAllDetails As String = "Events_Employees_SelectAllDetails"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EventId As Integer, ByVal FK_EmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Insert, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EventId As Integer, ByVal FK_EmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Update, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EventId As Integer, ByVal FK_EmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Delete, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EventId As Integer, ByVal FK_EmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Events_Employees_Select, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Events_Employees_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function Get_EventGroups(ByVal EventId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_EventGroups_ForDDL, New SqlParameter("@EventId", EventId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function Get_All_Details(ByVal EventId As Integer, ByVal GroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Events_Employees_SelectAllDetails, New SqlParameter("@EventId", EventId), _
                                              New SqlParameter("@GroupId", GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace