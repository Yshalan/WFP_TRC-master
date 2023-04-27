Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB


Namespace TA.Lookup

    Public Class DALHoliday_logicalGroup
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Holiday_logicalGroup_Select As String = "Holiday_logicalGroup_select"
        Private Holiday_logicalGroup_Select_All As String = "Holiday_logicalGroup_select_All"
        Private Holiday_logicalGroup_Insert As String = "Holiday_logicalGroup_Insert"
        Private Holiday_logicalGroup_Update As String = "Holiday_logicalGroup_Update"
        Private Holiday_logicalGroup_Delete As String = "Holiday_logicalGroup_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_HolidayId As Integer, ByVal FK_LogicalGroupId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_logicalGroup_Insert, New SqlParameter("@FK_HolidayId", FK_HolidayId), _
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_HolidayId As Integer, ByVal FK_LogicalGroupId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_logicalGroup_Update, New SqlParameter("@FK_HolidayId", FK_HolidayId), _
               New SqlParameter("@FK_LogicalGroupId", FK_LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_HolidayId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Holiday_logicalGroup_Delete, New SqlParameter("@FK_HolidayId", FK_HolidayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_HolidayId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objRow As DataTable
            Try
                objRow = objDac.GetDataTable(Holiday_logicalGroup_Select, New SqlParameter("@FK_HolidayId", FK_HolidayId))
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
                objColl = objDac.GetDataTable(Holiday_logicalGroup_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAll_LogicalGroup() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable("Emp_LogicalGroup_Select_All_ForList", Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
#End Region




    End Class
End Namespace