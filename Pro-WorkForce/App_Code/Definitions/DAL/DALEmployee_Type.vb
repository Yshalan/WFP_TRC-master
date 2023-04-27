Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALEmployee_Type
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Employee_Type_Select As String = "Employee_Type_select"
        Private Employee_Type_Select_All As String = "Employee_Type_select_All"
        Private Employee_Type_Insert As String = "Employee_Type_Insert"
        Private Employee_Type_Update As String = "Employee_Type_Update"
        Private Employee_Type_Delete As String = "Employee_Type_Delete"
        Private Employee_Type_GetInternal As String = "Employee_Type_GetInternal"
        Private Get_Initial_Index As String = "Get_Initial_Index"
        Private Emp_Type_Select_All_ForList As String = "Emp_Type_Select_All_ForList"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef EmployeeTypeId As Integer, ByVal TypeName_En As String, ByVal TypeName_Ar As String, ByVal IsInternaltype As Boolean, ByVal EmployeeNumberInitial As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@EmployeeTypeId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EmployeeTypeId)
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Type_Insert, sqlOut, New SqlParameter("@TypeName_En", TypeName_En), _
               New SqlParameter("@TypeName_Ar", TypeName_Ar), _
               New SqlParameter("@IsInternaltype", IsInternaltype), _
               New SqlParameter("@EmployeeNumberInitial", EmployeeNumberInitial))
                If errNo = 0 Then
                    EmployeeTypeId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EmployeeTypeId As Integer, ByVal TypeName_En As String, ByVal TypeName_Ar As String, ByVal IsInternaltype As Boolean, ByVal EmployeeNumberInitial As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Type_Update, New SqlParameter("@EmployeeTypeId", EmployeeTypeId), _
               New SqlParameter("@TypeName_En", TypeName_En), _
               New SqlParameter("@TypeName_Ar", TypeName_Ar), _
               New SqlParameter("@IsInternaltype", IsInternaltype), _
               New SqlParameter("@EmployeeNumberInitial", EmployeeNumberInitial))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EmployeeTypeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Type_Delete, New SqlParameter("@EmployeeTypeId", EmployeeTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmployeeTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Employee_Type_Select, New SqlParameter("@EmployeeTypeId", EmployeeTypeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Employee_Type_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_EmployeeType() As DataTable
            Dim objColl As DataTable = Nothing
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Emp_Type_Select_All_ForList, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetInternal(ByVal EmployeeTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Employee_Type_GetInternal, New SqlParameter("@EmployeeTypeId", EmployeeTypeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetInitialIndex(ByVal FK_EmployeeTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Get_Initial_Index, New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region


    End Class
End Namespace