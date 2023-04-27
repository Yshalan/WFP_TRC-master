Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Admin

    Public Class DALCardTypes
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private CardTypes_Select As String = "CardTypes_select"
        Private CardTypes_Select_All As String = "CardTypes_select_All"
        Private CardTypes_Insert As String = "CardTypes_Insert"
        Private CardTypes_Update As String = "CardTypes_Update"
        Private CardTypes_Delete As String = "CardTypes_Delete"
        Private Get_Card_Template_By_Designation As String = "Get_Card_Template_By_Designation"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef CardTypeId As Integer, ByVal CardTypeEn As String, ByVal CardTypeAr As String, ByVal Fk_TemplateId As Integer, ByVal CardApproval As Integer, ByVal CardRequestManagerLevelRequired As Integer, ByVal CreatedBY As String, ByVal CreatedDate As DateTime, ByVal LastUpdatedBy As String, ByVal LastUpdatedDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Dim sqlOut = New SqlParameter("@CardTypeId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.[Default], CardTypeId)
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CardTypes_Insert, sqlOut, _
               New SqlParameter("@CardTypeEn", CardTypeEn), _
               New SqlParameter("@CardTypeAr", CardTypeAr), _
               New SqlParameter("@Fk_TemplateId", Fk_TemplateId), _
               New SqlParameter("@CardApproval", CardApproval), _
               New SqlParameter("@CardRequestManagerLevelRequired", CardRequestManagerLevelRequired), _
               New SqlParameter("@CreatedBY", CreatedBY), _
               New SqlParameter("@LastUpdatedBy", LastUpdatedBy))
                CardTypeId = Convert.ToInt32(sqlOut.Value)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal CardTypeId As Integer, ByVal CardTypeEn As String, ByVal CardTypeAr As String, ByVal Fk_TemplateId As Integer, ByVal CardApproval As Integer, ByVal CardRequestManagerLevelRequired As Integer, ByVal CreatedBY As String, ByVal CreatedDate As DateTime, ByVal LastUpdatedBy As String, ByVal LastUpdatedDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CardTypes_Update, New SqlParameter("@CardTypeId", CardTypeId), _
               New SqlParameter("@CardTypeEn", CardTypeEn), _
               New SqlParameter("@CardTypeAr", CardTypeAr), _
               New SqlParameter("@Fk_TemplateId", Fk_TemplateId), _
               New SqlParameter("@CardApproval", CardApproval), _
               New SqlParameter("@CardRequestManagerLevelRequired", CardRequestManagerLevelRequired), _
               New SqlParameter("@LastUpdatedBy", LastUpdatedBy))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal CardTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CardTypes_Delete, New SqlParameter("@CardTypeId", CardTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal CardTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(CardTypes_Select, New SqlParameter("@CardTypeId", CardTypeId)).Rows(0)
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
                objColl = objDac.GetDataTable(CardTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllByDesignation(ByVal Fk_designation As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Card_Template_By_Designation, New SqlParameter("@DesignationID", Fk_designation))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace
