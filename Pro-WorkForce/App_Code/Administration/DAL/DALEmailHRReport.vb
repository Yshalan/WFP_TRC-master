Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


namespace TA.Admin

Public Class DALEmailHRReport
Inherits MGRBase



#Region "Class Variables"
private strConn As String
Private EmailHRReport_Select As String = "EmailHRReport_select" 
Private EmailHRReport_Select_All As String = "EmailHRReport_select_All" 
Private EmailHRReport_Insert As String = "EmailHRReport_Insert" 
private EmailHRReport_Update As String = "EmailHRReport_Update" 
private EmailHRReport_Delete As String = "EmailHRReport_Delete" 
#End Region
#Region "Constructor"
Public Sub New()



End Sub

#End Region

#Region "Methods"

Public Function Add(ByVal ReportType As Integer ,ByVal SendDate As DateTime ) As Integer

objDac = DAC.getDAC()
Try
 errNo = objDac.AddUpdateDeleteSPTrans(EmailHRReport_Insert,New SqlParameter("@ReportType",ReportType) , _
New SqlParameter("@SendDate",SendDate))
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return errNo

End Function

Public Function Update(ByVal ID As Long ,ByVal ReportType As Integer ,ByVal SendDate As DateTime ) As Integer

objDac = DAC.getDAC()
Try
 errNo = objDac.AddUpdateDeleteSPTrans(EmailHRReport_Update,New SqlParameter("@ID",ID) , _
New SqlParameter("@ReportType",ReportType) , _
New SqlParameter("@SendDate",SendDate))
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return errNo

End Function

Public Function Delete(ByVal ID As Long ) As Integer

objDac = DAC.getDAC()
Try
 errNo = objDac.AddUpdateDeleteSPTrans(EmailHRReport_Delete,New SqlParameter("@ID",ID))
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return errNo

End Function

Public Function GetByPK(ByVal ID As Long ) As DataRow

objDac = DAC.getDAC()
Dim objRow As DataRow
Try
 objRow = objDac.GetDataTable(EmailHRReport_Select,New SqlParameter("@ID",ID)).Rows(0)
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
 objColl = objDac.GetDataTable(EmailHRReport_Select_All, Nothing)
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return objColl


End Function

#End Region 


 End Class
End Namespace