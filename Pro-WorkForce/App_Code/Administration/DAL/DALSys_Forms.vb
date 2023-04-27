Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.DB
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Forms

    Public Class DALSys_Forms
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Sys_Forms_Select As String = "Sys_Forms_select"
        Private Sys_Forms_Select_All As String = "Sys_Forms_select_All"
        Private Sys_Forms_Insert As String = "Sys_Forms_Insert"
        Private Sys_Forms_Update As String = "Sys_Forms_Update"
        Private Sys_Forms_Delete As String = "Sys_Forms_Delete"
        Private Sys_Forms_Select_All_ForEventLog As String = "Sys_Forms_Select_All_ForEventLog"
        Private sys_tables_select_CDC As String = "sys_tables_select_CDC"
        Private Sys_Forms_Select_All_ForEventLog_new = "Sys_Forms_Select_All_ForEventLog_new"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FormID As Integer, ByVal FormName As String, ByVal Desc_En As String, ByVal Desc_Ar As String, ByVal ModuleID As Integer, ByVal FormPath As String, ByVal FormOrder As Integer, ByVal ParentID As Integer, ByVal Visible As Integer, ByVal FormOnlinePath As String, ByVal FormOnlineOrder As Integer, ByVal ShowToEmp As Boolean, ByVal ShowToClient As Boolean, ByVal ShowToPersonal As Boolean, ByVal OnlineFormPath As String, ByVal DescOnline_En As String, ByVal DescOnline_Ar As String, ByVal Packages As String, ByVal AddBtnName As String, ByVal EditBtnName As String, ByVal DeleteBtnName As String, ByVal PrintBtnName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Sys_Forms_Insert, New SqlParameter("@FormID", FormID), _
               New SqlParameter("@FormName", FormName), _
               New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar), _
               New SqlParameter("@ModuleID", ModuleID), _
               New SqlParameter("@FormPath", FormPath), _
               New SqlParameter("@FormOrder", FormOrder), _
               New SqlParameter("@ParentID", ParentID), _
               New SqlParameter("@Visible", Visible), _
               New SqlParameter("@FormOnlinePath", FormOnlinePath), _
               New SqlParameter("@FormOnlineOrder", FormOnlineOrder), _
               New SqlParameter("@ShowToEmp", ShowToEmp), _
               New SqlParameter("@ShowToClient", ShowToClient), _
               New SqlParameter("@ShowToPersonal", ShowToPersonal), _
               New SqlParameter("@OnlineFormPath", OnlineFormPath), _
               New SqlParameter("@DescOnline_En", DescOnline_En), _
               New SqlParameter("@DescOnline_Ar", DescOnline_Ar), _
               New SqlParameter("@Packages", Packages), _
               New SqlParameter("@AddBtnName", AddBtnName), _
               New SqlParameter("@EditBtnName", EditBtnName), _
               New SqlParameter("@DeleteBtnName", DeleteBtnName), _
               New SqlParameter("@PrintBtnName", PrintBtnName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FormID As Integer, ByVal FormName As String, ByVal Desc_En As String, ByVal Desc_Ar As String, ByVal ModuleID As Integer, ByVal FormPath As String, ByVal FormOrder As Integer, ByVal ParentID As Integer, ByVal Visible As Integer, ByVal FormOnlinePath As String, ByVal FormOnlineOrder As Integer, ByVal ShowToEmp As Boolean, ByVal ShowToClient As Boolean, ByVal ShowToPersonal As Boolean, ByVal OnlineFormPath As String, ByVal DescOnline_En As String, ByVal DescOnline_Ar As String, ByVal Packages As String, ByVal AddBtnName As String, ByVal EditBtnName As String, ByVal DeleteBtnName As String, ByVal PrintBtnName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Sys_Forms_Update, New SqlParameter("@FormID", FormID), _
               New SqlParameter("@FormName", FormName), _
               New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar), _
               New SqlParameter("@ModuleID", ModuleID), _
               New SqlParameter("@FormPath", FormPath), _
               New SqlParameter("@FormOrder", FormOrder), _
               New SqlParameter("@ParentID", ParentID), _
               New SqlParameter("@Visible", Visible), _
               New SqlParameter("@FormOnlinePath", FormOnlinePath), _
               New SqlParameter("@FormOnlineOrder", FormOnlineOrder), _
               New SqlParameter("@ShowToEmp", ShowToEmp), _
               New SqlParameter("@ShowToClient", ShowToClient), _
               New SqlParameter("@ShowToPersonal", ShowToPersonal), _
               New SqlParameter("@OnlineFormPath", OnlineFormPath), _
               New SqlParameter("@DescOnline_En", DescOnline_En), _
               New SqlParameter("@DescOnline_Ar", DescOnline_Ar), _
               New SqlParameter("@Packages", Packages), _
               New SqlParameter("@AddBtnName", AddBtnName), _
               New SqlParameter("@EditBtnName", EditBtnName), _
               New SqlParameter("@DeleteBtnName", DeleteBtnName), _
               New SqlParameter("@PrintBtnName", PrintBtnName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FormID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Sys_Forms_Delete, New SqlParameter("@FormID", FormID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FormID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Sys_Forms_Select, New SqlParameter("@FormID", FormID)).Rows(0)
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
                objColl = objDac.GetDataTable(Sys_Forms_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_ForEventLog() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Sys_Forms_Select_All_ForEventLog_new, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_CDCTables() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(sys_tables_select_CDC, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace