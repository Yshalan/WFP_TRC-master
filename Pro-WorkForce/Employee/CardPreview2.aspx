<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CardPreview2.aspx.vb" Inherits="Employee_CardPreview2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML>
	<HEAD>
		<title>IDCardPreview</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" rightMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" border="0" cellSpacing="0" cellPadding="0" width="100%" align="center"
				height="100%">
				<TR>
					<TD align="center" id="eSmartPrint" valign="middle">
						<OBJECT id="eSmartPrint" classid="clsid:A32998DC-E53D-469F-A2B0-94CFF90AB7CE">
							<PARAM NAME="_ExtentX" VALUE="20849">
							<PARAM NAME="_ExtentY" VALUE="15901">
						</OBJECT>
					</TD>
				</TR>
				<TR>
					<TD align="center" height="1">
						<input id="hidCabFile" value="CAB/SmartID.zip" size="1" type="hidden" name="hidCabFile" runat="server"/>
						<input id="txtAppPath" size="1" type="hidden" name="txtAppPath" runat="server"/>
						<input id="txtLoginUserName" size="1" type="hidden" name="txtLoginUserName" runat="server"/>
						<input id="txtDBType" size="1" type="hidden" name="txtDBType" runat="server"/>
						<input id="txtServerName" size="1" type="hidden" name="txtServerName" runat="server"/>
						<input id="txtDBName" size="1" type="hidden" name="txtDBName" runat="server"/>
						<input id="txtUserName" size="1" type="hidden" name="txtUserName" runat="server"/>
						<input id="txtPassword" size="1" type="hidden" name="txtPassword" runat="server"/>
                        <input id="hidUser" type="hidden" runat="server"/>
                        <input id="hidSessionID" type="hidden" runat="server"/>
					</TD>
				</TR>
			</TABLE>
		</form>
	<script language=javascript>

    
    </script>
		<script language="vbscript">

			Sub window_OnLoad()
			Dim CtrlLoad
				If Document.Form1.eSmartPrint.CheckSVKey=0 Then
					msgbox "Invalid Registration Code, please contact Smart Vision", vbInformation, "   SmartTime 3000"				
					Exit Sub
				End If

				CtrlLoad=Document.Form1.eSmartPrint.ConnectToDatabase (Document.Form1.txtServerName.value,Document.Form1.txtDBName.value,Document.Form1.txtUserName.value,Document.Form1.txtPassword.value,Document.Form1.txtDBType.value,Document.Form1.txtLoginUserName.Value)
				If CtrlLoad=0 Then
					msgbox "Please Contact your Administrator to register the required Controls", vbInformation, "   SmartTime 3000"
					Exit Sub
				End If
		    End Sub

		</script>
	
	</body>
</HTML>