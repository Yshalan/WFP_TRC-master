Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALSlider_Images
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Slider_Images_Select As String = "Slider_Images_select"
        Private Slider_Images_Select_All As String = "Slider_Images_select_All"
        Private Slider_Images_Insert As String = "Slider_Images_Insert"
        Private Slider_Images_Update As String = "Slider_Images_Update"
        Private Slider_Images_Delete As String = "Slider_Images_Delete"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ImageId As Integer, ByVal ImageName As String, ByVal ImagePath As String, ByVal ImageOrder As Integer, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ImageId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ImageId)
                errNo = objDac.AddUpdateDeleteSPTrans(Slider_Images_Insert, sqlOut,
               New SqlParameter("@ImageName", ImageName),
               New SqlParameter("@ImagePath", ImagePath),
               New SqlParameter("@ImageOrder", ImageOrder),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then ImageId = sqlOut.Value Else ImageId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ImageId As Integer, ByVal ImageName As String, ByVal ImagePath As String, ByVal ImageOrder As Integer, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Slider_Images_Update, New SqlParameter("@ImageId", ImageId), _
               New SqlParameter("@ImageName", ImageName), _
               New SqlParameter("@ImagePath", ImagePath), _
               New SqlParameter("@ImageOrder", ImageOrder), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ImageId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Slider_Images_Delete, New SqlParameter("@ImageId", ImageId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ImageId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Slider_Images_Select, New SqlParameter("@ImageId", ImageId)).Rows(0)
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
                objColl = objDac.GetDataTable(Slider_Images_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace