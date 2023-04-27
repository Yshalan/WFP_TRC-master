Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp


Namespace TA_Announcements

    Public Class DALAssignEmployee
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Add_AssignedEmployee As String = "Add_AssignedEmployee"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"
        Public Function Add(ByVal AssignedEmployee As Integer, ByVal FK_EmployeeId As Integer, ByVal LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(Add_AssignedEmployee,
                            New SqlParameter("@AssignedEmployee", AssignedEmployee),
                            New SqlParameter("@LeaveId", LeaveId),
                            New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function


#End Region


    End Class
End Namespace