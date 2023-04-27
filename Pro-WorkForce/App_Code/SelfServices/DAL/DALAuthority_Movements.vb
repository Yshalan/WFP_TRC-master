Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA_AuthorityMovements

    Public Class DALAuthority_Movements
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Authority_Movements_Select As String = "Authority_Movements_select"
        Private Authority_Movements_Select_All As String = "Authority_Movements_select_All"
        Private Authority_Movements_Insert As String = "Authority_Movements_Insert"
        Private Authority_Movements_Update As String = "Authority_Movements_Update"
        Private Authority_Movements_Delete As String = "Authority_Movements_Delete"
        Private Authority_Movements_GetAllByEmployeeId As String = "Authority_Movements_GetAllByEmployeeId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_AuthoritId As Integer, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal Type As String, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal IP_Address As String, ByVal Domain As String, ByVal PCName As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Authority_Movements_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_AuthoritId", FK_AuthoritId), _
               New SqlParameter("@MoveDate", MoveDate), _
               New SqlParameter("@MoveTime", MoveTime), _
               New SqlParameter("@Type", Type), _
               New SqlParameter("@FK_ReasonId", FK_ReasonId), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IP_Address", IP_Address), _
               New SqlParameter("@Domain", Domain), _
               New SqlParameter("@PCName", PCName), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal AuthorityMoveId As Long, ByVal FK_EmployeeId As Long, ByVal FK_AuthoritId As Integer, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal Type As String, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal IP_Address As String, ByVal Domain As String, ByVal PCName As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Authority_Movements_Update, New SqlParameter("@AuthorityMoveId", AuthorityMoveId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_AuthoritId", FK_AuthoritId), _
               New SqlParameter("@MoveDate", MoveDate), _
               New SqlParameter("@MoveTime", MoveTime), _
               New SqlParameter("@Type", Type), _
               New SqlParameter("@FK_ReasonId", FK_ReasonId), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@IP_Address", IP_Address), _
               New SqlParameter("@Domain", Domain), _
               New SqlParameter("@PCName", PCName), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal AuthorityMoveId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Authority_Movements_Delete, New SqlParameter("@AuthorityMoveId", AuthorityMoveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal AuthorityMoveId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Authority_Movements_Select, New SqlParameter("@AuthorityMoveId", AuthorityMoveId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Authority_Movements_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByEmployeeId(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Authority_Movements_GetAllByEmployeeId, New SqlParameter("@EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace