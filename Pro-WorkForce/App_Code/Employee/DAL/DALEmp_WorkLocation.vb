Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES



Namespace TA.Employees

    Public Class DALEmp_WorkLocation
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_WorkLocation_Select As String = "Emp_WorkLocation_select"
        Private Emp_WorkLocation_Select_All As String = "Emp_WorkLocation_select_All"
        Private Emp_WorkLocation_Insert As String = "Emp_WorkLocation_Insert"
        Private Emp_WorkLocation_Update As String = "Emp_WorkLocation_Update"
        Private Emp_WorkLocation_Delete As String = "Emp_WorkLocation_Delete"
        Private Emp_WorkLocation_Insert_Update As String = "Emp_WorkLocation_Insert_Update"
        Private Emp_WorkLocation_bulk_insert As String = "Emp_WorkLocation_bulk_insert"
        Private Emp_WorkLocation_GetAllByCompany As String = "Emp_WorkLocation_GetAllByCompany"
        Private Emp_WorkLocation_GetAllByCompanyAndUserId As String = "Emp_WorkLocation_GetAllByCompanyAndUserId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region


#Region "Extended Class Variables"


        Private EmpWorkLocation_GetAllPolicy As String = "Emp_WorkLocation_GetAllPolicy"
        Private Emp_WorkLocation_GetAllPolicyByCompany As String = "Emp_WorkLocation_GetAllPolicyByCompany"
#End Region

#Region "Methods"
        Public Function Add_Bulk(ByVal xml As String, ByVal CompanyId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@CompanyId", CompanyId), New SqlParameter("@CREATED_BY", 1), _
               New SqlParameter("@LAST_UPDATE_BY", 1))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Add(ByRef WorkLocationId As Integer, ByVal FK_CompanyId As Integer, ByVal WorkLocationCode As String, ByVal WorkLocationName As String,
                            ByVal WorkLocationArabicName As String, ByVal FK_TAPolicyId As Integer, ByVal Active As Boolean, ByVal GPSCoordinates As String,
                            ByVal CREATED_BY As String, ByVal Radius As Integer, ByVal HasMobilePunch As Boolean, ByVal AllowedMobileWorkLocation As String,
                            ByVal MustPunchPhysical As Boolean, ByVal MobilePunchConsiderDuration As String, ByVal SecondPunchRadius As Integer, ByVal OutPunchRadius As Integer, ByVal mustPunchTwoTimes As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@WorkLocationId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, WorkLocationId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Insert, sqlOut, New SqlParameter("@FK_CompanyId", FK_CompanyId),
               New SqlParameter("@WorkLocationCode", WorkLocationCode),
               New SqlParameter("@WorkLocationName", WorkLocationName),
               New SqlParameter("@WorkLocationArabicName", WorkLocationArabicName),
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId),
               New SqlParameter("@Active", Active),
               New SqlParameter("@GPSCoordinates", GPSCoordinates),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@Radius", IIf(Radius = 0, DBNull.Value, Radius)),
               New SqlParameter("@HasMobilePunch", HasMobilePunch),
               New SqlParameter("@AllowedMobileWorkLocation", AllowedMobileWorkLocation),
               New SqlParameter("@MustPunchPhysical", MustPunchPhysical),
               New SqlParameter("@MobilePunchConsiderDuration", MobilePunchConsiderDuration),
               New SqlParameter("@SecondPunchRadius", IIf(SecondPunchRadius = 0, DBNull.Value, SecondPunchRadius)),
               New SqlParameter("@OutPunchRadius", IIf(OutPunchRadius = 0, DBNull.Value, OutPunchRadius)),
               New SqlParameter("@mustPunchTwoTimes", mustPunchTwoTimes)
               )
                If Not IsDBNull(sqlOut.Value) Then
                    WorkLocationId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_CompanyId As Integer, ByVal WorkLocationId As Integer, ByVal WorkLocationCode As String,
                               ByVal WorkLocationName As String, ByVal WorkLocationArabicName As String, ByVal FK_TAPolicyId As Integer,
                               ByVal Active As Boolean, ByVal CREATED_BY As String, ByVal LAST_UPDATE_BY As String, ByVal GPSCoordinates As String,
                               ByVal Radius As Integer, ByVal HasMobilePunch As Boolean, ByVal AllowedMobileWorkLocation As String,
                               ByVal MustPunchPhysical As Boolean, ByVal MobilePunchConsiderDuration As String, ByVal SecondPunchRadius As Integer, ByVal OutPunchRadius As Integer, ByVal mustPunchTwoTimes As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Update, New SqlParameter("@FK_CompanyId", FK_CompanyId),
               New SqlParameter("@WorkLocationId", WorkLocationId),
               New SqlParameter("@WorkLocationCode", WorkLocationCode),
               New SqlParameter("@WorkLocationName", WorkLocationName),
               New SqlParameter("@WorkLocationArabicName", WorkLocationArabicName),
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId),
               New SqlParameter("@Active", Active),
               New SqlParameter("@CREATED_BY", IIf(CREATED_BY Is Nothing, "admin", CREATED_BY)),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@GPSCoordinates", GPSCoordinates),
               New SqlParameter("@Radius", IIf(Radius = 0, DBNull.Value, Radius)),
               New SqlParameter("@HasMobilePunch", HasMobilePunch),
               New SqlParameter("@AllowedMobileWorkLocation", AllowedMobileWorkLocation),
               New SqlParameter("@MustPunchPhysical", MustPunchPhysical),
               New SqlParameter("@MobilePunchConsiderDuration", MobilePunchConsiderDuration),
               New SqlParameter("@SecondPunchRadius", IIf(SecondPunchRadius = 0, DBNull.Value, SecondPunchRadius)),
               New SqlParameter("@OutPunchRadius", IIf(OutPunchRadius = 0, DBNull.Value, OutPunchRadius)),
               New SqlParameter("@mustPunchTwoTimes", mustPunchTwoTimes)
              )
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Add_Advanced(ByVal FK_CompanyId As Integer, ByVal WorkLocationId As Integer, ByVal WorkLocationCode As String, _
                                     ByVal WorkLocationName As String, ByVal WorkLocationArabicName As String, ByVal FK_TAPolicyId As Integer, _
                                     ByVal Active As Boolean, ByVal CREATED_BY As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Insert_Update, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@WorkLocationId", WorkLocationId), _
               New SqlParameter("@WorkLocationCode", WorkLocationCode), _
               New SqlParameter("@WorkLocationName", WorkLocationName), _
               New SqlParameter("@WorkLocationArabicName", WorkLocationArabicName), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@Active", Active), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal WorkLocationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkLocation_Delete, New SqlParameter("@WorkLocationId", WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal WorkLocationId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_WorkLocation_Select, New SqlParameter("@WorkLocationId", WorkLocationId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_WorkLocation_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllByCompany(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_GetAllByCompany, New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllByCompanyAndUserId(ByVal FK_CompanyId As Integer, ByVal FK_UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_GetAllByCompanyAndUserId, New SqlParameter("@FK_CompanyId", FK_CompanyId), New SqlParameter("@FK_UserId", FK_UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllWorkGrid(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("Emp_WorkLocation_Select_GridData", New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


#Region "Extended Method"
        Public Function GetAllPolicyByCompany(ByVal intCompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_WorkLocation_GetAllPolicyByCompany, New SqlParameter("@CompanyId", intCompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllPolicy() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmpWorkLocation_GetAllPolicy, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


#End Region


    End Class
End Namespace