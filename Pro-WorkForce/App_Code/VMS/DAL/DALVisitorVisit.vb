Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace VMS

    Public Class DALVisitorVisit
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String

        Private VisitorVisit_Insert As String = "VisitorVisit_Insert"
        Private VisitorVisit_Delete As String = "VisitorVisit_Delete"
        Private VisitorVisits_Select_ByFK_VisitId As String = "VisitorVisits_Select_ByFK_VisitId"
        Private VisitorVisit_Delete_ByFK_VisitId As String = "VisitorVisit_Delete_ByFK_VisitId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_VisitId As Integer, ByVal FK_VisitorId As Integer) As Integer

            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(VisitorVisit_Insert,
               New SqlParameter("@FK_VisitId", FK_VisitId),
               New SqlParameter("@FK_VisitorId", FK_VisitorId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_VisitId As Integer, ByVal FK_VisitorId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(VisitorVisit_Delete,
               New SqlParameter("@FK_VisitId", FK_VisitId),
               New SqlParameter("@FK_VisitorId", FK_VisitorId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Get_ByFK_VisitId(ByVal FK_VisitId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(VisitorVisits_Select_ByFK_VisitId, New SqlParameter("@FK_VisitId", FK_VisitId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function DeleteByFK_VisitId(ByVal FK_VisitId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(VisitorVisit_Delete_ByFK_VisitId,
               New SqlParameter("@FK_VisitId", FK_VisitId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region


    End Class
End Namespace