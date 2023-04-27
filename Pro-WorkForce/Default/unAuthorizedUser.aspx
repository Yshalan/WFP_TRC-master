<%@ Page Language="VB" AutoEventWireup="false" CodeFile="unAuthorizedUser.aspx.vb" Inherits="Default_unAuthorizedUseBased" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <center>
<table id="Table_01" width="801" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td colspan="3">
			<img src="../images/LoginPage/Smart-time-login_01.jpg" width="457" height="183" alt="image did not loaded"/></td>
		<td colspan="5">
			<img src="../images/LoginPage/Smart-time-login_02.jpg" width="343" height="183" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="183" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td colspan="8">
			<img src="../images/LoginPage/Smart-time-login_03.jpg" width="800" height="44" alt=""/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="44" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td>
			<img src="../images/LoginPage/Smart-time-login_04.jpg" width="233" height="40" alt="image did not loaded"/></td>
		<td rowspan="9">
			<img src="../images/LoginPage/Smart-time-login_05.jpg" width="188" height="332" alt="image did not loaded"/></td>
		<td colspan="6" rowspan="2">
		
            <asp:Label ID="lblUnauthorizedAccessMsg1" runat="server" 
            Text="Try to access unauthorized resourses or may be suspended for a reason."
            ForeColor="Red"
            ></asp:Label>
            <asp:Label ID="lblUnauthorizedAccessMsg2" runat="server" 
            ForeColor="Red"
            
                Text="You can register as a new user , reset your password or contact the admin" 
                style="direction: ltr"></asp:Label>
		
		
			<img src="../images/LoginPage/Smart-time-login_06.jpg" width="379" height="115" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="40" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td rowspan="4">
			<img src="../images/LoginPage/Smart-time-login_07.jpg" width="233" height="148" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="75" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td colspan="2">
			<img src="../images/LoginPage/Smart-time-login_08.jpg" width="107" height="30" alt="image did not loaded"/></td>
		<td colspan="3">
																											
            <asp:TextBox ID="txtUserName" style="height:27;width:168" runat="server"></asp:TextBox>
				 </td>
		<td>
			<img src="../images/LoginPage/Smart-time-login_10.jpg" width="103" height="30" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="30" alt=""/></td>
	</tr>
	<tr>
		<td colspan="6">
			<img src="../images/LoginPage/Smart-time-login_11.jpg" width="379" height="18" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="18" alt=""/></td>
	</tr>
	<tr>
		<td colspan="2" rowspan="2">
			<img src="../images/LoginPage/Smart-time-login_12.jpg" width="107" height="30" alt="image did not loaded"/></td>
		<td colspan="3" rowspan="2">
			<asp:TextBox ID="txtPassword" style="height:27;width:168" runat="server"/></td>
		<td rowspan="2">
			<img src="../images/LoginPage/Smart-time-login_14.jpg" width="103" height="30" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="25" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td rowspan="4">
			<img src="../images/LoginPage/Smart-time-login_15.jpg" width="233" height="144" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="5" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td colspan="6">
			<img src="../images/LoginPage/Smart-time-login_16.jpg" width="379" height="21" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="21" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td colspan="3">
			<img src="../images/LoginPage/Smart-time-login_17.jpg" width="145" height="31" alt="image did not loaded"/></td>
		<td>
			
			 
            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/LoginPage/Smart-time-login_18.jpg" 
            width="94" height="31"
            runat="server" />
			</td>
		<td colspan="2">
			<img src="../images/LoginPage/Smart-time-login_19.jpg" width="140" height="31" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="31" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td colspan="6">
            
		
<img src="../images/LoginPage/Smart-time-login_20.jpg" width="379" height="87" alt="image did not loaded"/>
			 </td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="1" height="87" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td colspan="8">
			<img src="../images/LoginPage/Smart-time-login_21.jpg" width="800" height="41" alt="image did not loaded"/></td>
		<td>
			<img src="~/images/LoginPage/spacer.gif" width="1" height="41" alt="image did not loaded"/></td>
	</tr>
	<tr>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="233" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="188" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="36" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="71" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="38" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="94" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="37" height="1" alt="image did not loaded"/></td>
		<td>
			<img src="../images/LoginPage/spacer.gif" width="103" height="1" alt="image did not loaded"/></td>
		<td></td>
	</tr>
</table>
</center>
 
    </form>
</body>
</html>
