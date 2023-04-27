Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.UTILITIES
Imports SmartV.DB



Namespace TA.Employees

    Public Class DALEmp_Cards
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Cards_AddToPrintQueue As String = "Emp_Cards_AddToPrintQueue"
        Private Emp_Cards_DeleteFromPrintQueue As String = "Emp_Cards_DeleteFromPrintQueue"
        Private Emp_Cards_GetInPrintQueue As String = "Emp_Cards_GetInPrintQueue"
        Private Emp_Cards_GetAllDesign As String = "Emp_Cards_GetAllDesign"
    
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region


#Region "Methods"

        Public Function AddToQueue(ByVal EmpId As Integer, ByVal CreatedBy As Integer, ByVal DesignId As Integer) As Integer

            objDac = DAC.getDAC()

            Try

                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Cards_AddToPrintQueue, _
               New SqlParameter("@EmpId", EmpId), _
               New SqlParameter("@UserId", CreatedBy), New SqlParameter("@DesignId", DesignId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            Return errNo

        End Function

        Public Function DeleteFromQueue(ByVal EmpId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Cards_DeleteFromPrintQueue, New SqlParameter("@EmpId", EmpId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetAllInQueue(ByVal CreatedBy As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Cards_GetInPrintQueue, New SqlParameter("@UserId", CreatedBy))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllCardsDesign(ByVal Lang As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Cards_GetAllDesign, New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region




    End Class
End Namespace