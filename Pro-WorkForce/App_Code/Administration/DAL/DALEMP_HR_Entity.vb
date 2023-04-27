Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALEMP_HR_Entity
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private EMP_HR_Entity_Select As String = "EMP_HR_Entity_select"
        Private EMP_HR_Entity_Select_All As String = "EMP_HR_Entity_select_All"
        Private EMP_HR_Entity_Insert As String = "EMP_HR_Entity_Insert"
        Private EMP_HR_Entity_Update As String = "EMP_HR_Entity_Update"
        Private EMP_HR_Entity_Delete As String = "EMP_HR_Entity_Delete"
        Private EMP_HR_Entity_DeleteBy_FK_HREmployeeId As String = "EMP_HR_Entity_DeleteBy_FK_HREmployeeId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_HREmployeeId As Long, ByVal FK_Entity As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EMP_HR_Entity_Insert, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_Entity", FK_Entity))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_HREmployeeId As Long, ByVal FK_Entity As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EMP_HR_Entity_Update, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_Entity", FK_Entity))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_HREmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EMP_HR_Entity_Delete, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteBy_FK_HREmployeeId(ByVal FK_HREmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EMP_HR_Entity_DeleteBy_FK_HREmployeeId, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByHREmployeeId(ByVal FK_HREmployeeId As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EMP_HR_Entity_Select, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetByPK(ByVal FK_HREmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(EMP_HR_Entity_Select, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId)).Rows(0)
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
                objColl = objDac.GetDataTable(EMP_HR_Entity_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace