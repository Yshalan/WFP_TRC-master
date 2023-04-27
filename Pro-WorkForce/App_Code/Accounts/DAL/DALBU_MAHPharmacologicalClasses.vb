Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp

Namespace TA.Accounts

    Public Class DALBU_MAHPharmacologicalClasses
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private BU_MAHPharmacologicalClasses_Select As String = "BU_MAHPharmacologicalClasses_select"
        Private BU_MAHPharmacologicalClasses_Select_All As String = "BU_MAHPharmacologicalClasses_select_All"
        Private BU_MAHPharmacologicalClasses_Insert As String = "BU_MAHPharmacologicalClasses_Insert"
        Private BU_MAHPharmacologicalClasses_Update As String = "BU_MAHPharmacologicalClasses_Update"
        Private BU_MAHPharmacologicalClasses_Delete As String = "BU_MAHPharmacologicalClasses_Delete"
        Private BU_MAHPharmacologicalClasses_Delete_By_AccountID As String = "BU_MAHPharmacologicalClasses_Delete_By_AccountID"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_AccountId As Integer, ByVal ClassName As String, ByVal Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHPharmacologicalClasses_Insert, New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@ClassName", ClassName), _
               New SqlParameter("@Remarks", Remarks))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PHClassId As Integer, ByVal FK_AccountId As Integer, ByVal ClassName As String, ByVal Remarks As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHPharmacologicalClasses_Update, New SqlParameter("@PHClassId", PHClassId), _
               New SqlParameter("@FK_AccountId", FK_AccountId), _
               New SqlParameter("@ClassName", ClassName), _
               New SqlParameter("@Remarks", Remarks))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PHClassId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHPharmacologicalClasses_Delete, New SqlParameter("@PHClassId", PHClassId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function DeleteByAccountID(ByVal FK_AccountID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(BU_MAHPharmacologicalClasses_Delete_By_AccountID, New SqlParameter("@FK_AccountID", FK_AccountID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PHClassId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(BU_MAHPharmacologicalClasses_Select, New SqlParameter("@PHClassId", PHClassId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll(ByVal FK_AccountID As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(BU_MAHPharmacologicalClasses_Select_All, New SqlParameter("@FK_AccountId", FK_AccountID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region


    End Class
End Namespace