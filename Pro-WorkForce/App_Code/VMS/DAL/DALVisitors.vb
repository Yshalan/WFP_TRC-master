Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace VMS

    Public Class DALVisitors
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Visitors_Select As String = "Visitors_select"
        Private Visitors_Select_All As String = "Visitors_select_All"
        Private Visitors_Select_AllVisitDetails As String = "Visitors_Select_AllVisitDetails"
        Private Visitors_Insert As String = "Visitors_Insert"
        Private Visitors_Update As String = "Visitors_Update"
        Private Visitors_Delete As String = "Visitors_Delete"
        Private Visitors_Delete_ByFK_VisitId As String = "Visitors_Delete_ByFK_VisitId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef VisitorId As Integer, ByVal VisitorName As String, ByVal VisitorArabicName As String, ByVal Nationality As String, ByVal IDNumber As String, ByVal Gender As Integer, ByVal EIDExpiryDate As DateTime, ByVal DOB As DateTime, ByVal OrganizationName As String, ByVal MobileNumber As String, ByVal Remarks As String, ByVal IsDeleted As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@VisitorId", SqlDbType.BigInt, 16, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, VisitorId)
                errNo = objDac.AddUpdateDeleteSPTrans(Visitors_Insert, sqlOut,
                 New SqlParameter("@VisitorArabicName", VisitorArabicName),
                 New SqlParameter("@Nationality", Nationality),
                 New SqlParameter("@IDNumber", IDNumber),
                 New SqlParameter("@Gender", Gender),
                 New SqlParameter("@OrganizationName", OrganizationName),
                 New SqlParameter("@MobileNumber", MobileNumber),
                 New SqlParameter("@Remarks", Remarks),
                 New SqlParameter("@IsDeleted", IsDeleted),
                 New SqlParameter("@CREATED_BY", CREATED_BY))
                'New SqlParameter("@VisitorName", VisitorName),)
                'New SqlParameter("@EIDExpiryDate", EIDExpiryDate),
                'New SqlParameter("@DOB", DOB),

                If Not IsDBNull(sqlOut.Value) Then
                    VisitorId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal VisitorId As Integer, ByVal VisitorName As String, ByVal VisitorArabicName As String, ByVal Nationality As String, ByVal IDNumber As String, ByVal Gender As Integer, ByVal EIDExpiryDate As DateTime, ByVal DOB As DateTime, ByVal OrganizationName As String, ByVal MobileNumber As String, ByVal Remarks As String, ByVal IsDeleted As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Visitors_Update, New SqlParameter("@VisitorId", VisitorId),
               New SqlParameter("@VisitorArabicName", VisitorArabicName),
               New SqlParameter("@Nationality", Nationality),
               New SqlParameter("@IDNumber", IDNumber),
               New SqlParameter("@Gender", Gender),
               New SqlParameter("@OrganizationName", OrganizationName),
               New SqlParameter("@MobileNumber", MobileNumber),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@IsDeleted", IsDeleted),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
                'New SqlParameter("@EIDExpiryDate", EIDExpiryDate),
                'New SqlParameter("@DOB", DOB),
                'New SqlParameter("@VisitorName", VisitorName),
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal VisitorId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Visitors_Delete, New SqlParameter("@VisitorId", VisitorId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal VisitorId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Visitors_Select, New SqlParameter("@VisitorId", VisitorId)).Rows(0)
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
                objColl = objDac.GetDataTable(Visitors_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllVisitDetails(ByVal EmployeeId As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Visitors_Select_AllVisitDetails, New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function DeleteByFK_VisitId(ByVal FK_VisitId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Visitors_Delete_ByFK_VisitId, New SqlParameter("@FK_VisitId", FK_VisitId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region


    End Class
End Namespace