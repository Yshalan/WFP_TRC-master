Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Admin
    Public Class DALEmp_HR
        Inherits MGRBase

#Region "Class Variables"

        Dim Emp_HR_Select_All As String = "Emp_HR_Select_All"
        Dim Emp_HR_Select_ByPK As String = "Emp_HR_Select_ByPK"
        Dim Emp_HR_Insert As String = "Emp_HR_Insert"
        Dim Emp_HR_Update As String = "Emp_HR_Update"
        Dim Emp_HR_Delete As String = "Emp_HR_Delete"
        Dim Get_HR_Employee_With_Inner_Employee As String = "Get_HR_Employee_With_Inner_Employee"
        Dim Notifications_Select_All As String = "Notifications_Select_All"

#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function GetAll() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_HR_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetByPK(ByVal HREmployeeID As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_HR_Select_ByPK, New SqlParameter("@HREmployeeID", HREmployeeID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

        Public Function Add(ByVal HREmployeeID As Long, ByVal HRDesignation As Integer, ByVal IsSpecificEntity As Boolean, ByVal IsSpecificCompany As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Insert, _
               New SqlParameter("@HREmployeeID", HREmployeeID), _
               New SqlParameter("@HRDesignation", HRDesignation), _
                New SqlParameter("@IsSpecificEntity", IsSpecificEntity), _
                New SqlParameter("@IsSpecificCompany", IsSpecificCompany))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Update(ByVal HREmployeeID As Long, ByVal HRDesignation As Integer, ByVal IsSpecificEntity As Boolean, ByVal IsSpecificCompany As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Update, _
               New SqlParameter("@HREmployeeID", HREmployeeID), _
               New SqlParameter("@HRDesignation", HRDesignation), _
                New SqlParameter("@IsSpecificEntity", IsSpecificEntity), _
                New SqlParameter("@IsSpecificCompany", IsSpecificCompany))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Delete(ByVal HREmployeeID As Long) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Delete, _
               New SqlParameter("@HREmployeeID", HREmployeeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetHREmployeeWithInnerEmployees() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_HR_Employee_With_Inner_Employee, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetAllHRNotifications() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Notifications_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region

    End Class

End Namespace
