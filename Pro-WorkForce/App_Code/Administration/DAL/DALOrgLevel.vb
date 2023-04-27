Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALOrgLevel
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private OrgLevel_Select As String = "OrgLevel_select"
        Private OrgLevel_Select_All As String = "OrgLevel_select_All"
        Private OrgLevel_Insert As String = "OrgLevel_Insert"
        Private OrgLevel_Update As String = "OrgLevel_Update"
        Private OrgLevel_Delete As String = "OrgLevel_Delete"
        Private OrgLevel_Select_All_By_Company As String = "OrgLevel_Select_All_By_Company"
        Private OrgLevel_Select_All_with_Company As String = "OrgLevel_Select_All_with_Company"
        Private OrgLevel_Get_AMax_LevelID As String = "OrgLevel_Get_AMax_LevelID"
        Private OrgLevel_bulk_insert As String = "OrgLevel_bulk_insert"
        Private OrgLevel_Select_All_By_CompanyAndLevel As String = "OrgLevel_Select_All_By_CompanyAndLevel"
        Private OrgLevel_CheckExistsInEntity As String = "OrgLevel_CheckExistsInEntity"
        Private OrgLevel_Select_All_Company As String = "OrgLevel_Select_All_Company"
        Private OrgLevel_Select_Hierarchy As String = "OrgLevel_Select_Hierarchy"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Properties"

        Private _ErrNo As Integer
        Public Property ErrorNo() As Integer
            Get
                Return _ErrNo
            End Get
            Set(ByVal value As Integer)
                _ErrNo = value
            End Set
        End Property

#End Region
#Region "Methods"
        Public Function Add_Bulk(ByVal xml As String, ByVal CompanyId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgLevel_bulk_insert, New SqlParameter("@xml", xml), New SqlParameter("@CompanyId", CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Add(ByVal FK_CompanyId As Integer, ByVal LevelName As String, ByVal LevelArabicName As String, ByRef LvlId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@LvlId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, LvlId)
                errNo = objDac.AddUpdateDeleteSPTrans(OrgLevel_Insert, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@LevelName", LevelName), _
               New SqlParameter("@LevelId", 0), _
               New SqlParameter("@LevelArabicName", LevelArabicName), sqlOut)
                LvlId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal LevelId As Integer, ByVal FK_CompanyId As Integer, ByVal LevelName As String, ByVal LevelArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgLevel_Update, New SqlParameter("@LevelId", LevelId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@LevelName", LevelName), _
              New SqlParameter("@LevelArabicName", LevelArabicName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function UpdateGrid(ByVal LevelId As Integer, ByVal FK_CompanyId As Integer, ByVal LevelName As String, ByVal LevelArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("OrgLevel_Update_Grid", New SqlParameter("@LevelId", LevelId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@LevelName", LevelName), _
              New SqlParameter("@LevelArabicName", LevelArabicName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        'CheckExistsInEntity
        Public Function CheckExistsInEntity(ByVal LevelId As Integer, ByVal intCompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.GetSingleValue(Of Integer)(OrgLevel_CheckExistsInEntity, New SqlParameter("@LevelId", LevelId), New SqlParameter("@FK_CompanyId", intCompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Delete(ByVal LevelId As Integer, ByVal FK_CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(OrgLevel_Delete, New SqlParameter("@LevelId", LevelId), New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal LevelId As Integer, ByVal FK_CompanyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                Dim dt As DataTable
                dt = objDac.GetDataTable(OrgLevel_Select, New SqlParameter("@LevelId", LevelId), New SqlParameter("@FK_CompanyId", FK_CompanyId))
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    objRow = dt.Rows(0)
                Else
                    ErrorNo = -1
                End If

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
                objColl = objDac.GetDataTable(OrgLevel_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Company() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgLevel_Select_All_Company, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        'GetLevelsByCompany
        Public Function GetLevelsByCompany(ByVal IntFK_CompanyId As Integer) As Integer
            objDac = DAC.getDAC()
            Dim intLevel As String
            Try
                intLevel = objDac.GetSingleValue(Of Integer)(OrgLevel_Get_AMax_LevelID, New SqlParameter("@FK_CompanyId", IntFK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return intLevel
        End Function
        Public Function GetAllByCompany(ByVal IntFK_CompanyId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgLevel_Select_All_By_Company, New SqlParameter("@FK_CompanyId", IntFK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
        Public Function GetAllByCompanyAndLevel(ByVal IntFK_CompanyId As Integer, ByVal intLevelId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgLevel_Select_All_By_CompanyAndLevel, New SqlParameter("@FK_CompanyId", IntFK_CompanyId), New SqlParameter("@LevelId", intLevelId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllGridData(ByVal IntFK_CompanyId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("OrgLevel_Select_All_Grid", New SqlParameter("@FK_CompanyId", IntFK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAll_With_Comapany() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgLevel_Select_All_with_Company, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetAll_ForHierarchy(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(OrgLevel_Select_Hierarchy, New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace