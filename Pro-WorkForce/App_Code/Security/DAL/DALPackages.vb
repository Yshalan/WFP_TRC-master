Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV
Imports TA.Lookup
Imports SmartV.UTILITIES


Namespace TA_Packages

    Public Class DALPackages
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Packages_Select As String = "Packages_select"
        Private Packages_Select_All As String = "Packages_select_All"
        Private Packages_Insert As String = "Packages_Insert"
        Private Packages_Update As String = "Packages_Update"
        Private Packages_Delete As String = "Packages_Delete"
        Private sp_GetFormsFromPackage As String = "sp_GetFormsFromPackage"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal PackageId As Integer, ByVal PackageName As String, ByVal Forms As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Packages_Insert,
               New SqlParameter("@PackageName", PackageName), _
               New SqlParameter("@Forms", Forms))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PackageId As Integer, ByVal PackageName As String, ByVal Forms As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Packages_Update, New SqlParameter("@PackageId", PackageId), _
               New SqlParameter("@PackageName", PackageName), _
               New SqlParameter("@Forms", Forms))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PackageId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Packages_Delete, New SqlParameter("@PackageId", PackageId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PackageId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Packages_Select, New SqlParameter("@PackageId", PackageId)).Rows(0)
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
                objColl = objDac.GetDataTable(Packages_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

    

#End Region


    End Class
End Namespace