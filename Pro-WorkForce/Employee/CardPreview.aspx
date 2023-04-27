<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CardPreview.aspx.vb" Inherits="Employee_CardPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>IDCardPreview</title>

</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" border="0" cellspacing="0" cellpadding="0" width="100%" align="center"
            height="100%">
            <tr>
                <td align="center" id="eSmartPrint222" valign="middle">

                    <object id="<%Response.Write(ConfigurationManager.AppSettings("CardPrintingID"))%>" classid="<%Response.Write(ConfigurationManager.AppSettings("CardPrintingClass"))%>">
                        <param name="_ExtentX" value="20849">
                        <param name="_ExtentY" value="15901">
                    </object>
                </td>
            </tr>
            <tr>
                <td align="center" height="1">
                    <input id="hidCabFile" value="CAB/SmartID.zip" size="1" type="hidden" name="hidCabFile" runat="server" />
                    <input id="txtAppPath" size="1" type="hidden" name="txtAppPath" runat="server" />
                    <input id="txtLoginUserName" size="1" type="hidden" name="txtLoginUserName" runat="server" />
                    <input id="txtDBType" size="1" type="hidden" name="txtDBType" runat="server" />
                    <input id="txtServerName" size="1" type="hidden" name="txtServerName" runat="server" />
                    <input id="txtDBName" size="1" type="hidden" name="txtDBName" runat="server" />
                    <input id="txtUserName" size="1" type="hidden" name="txtUserName" runat="server" />
                    <input id="txtPassword" size="1" type="hidden" name="txtPassword" runat="server" />
                    <input id="hidUser" type="hidden" runat="server" />
                    <input id="hidSessionID" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
    </form>




    <script type="text/javascript" language="javascript">

          document.onreadystatechange = function(){
                 if(document.readyState === 'complete'){
                     InitSmartPrint();
                 }
            }
          function InitSmartPrint() {

            var CtrlLoad;

            //if (document.Form1.eSmartPrint.CheckSVKey() == 0) {
            var SmartObj = document.getElementById("eSmartPrint");
            if (SmartObj.CheckSVKey() == 0) {
                ShowMessage("Invalid Registration Code, please contact Smart Vision");
                return;
            }



            CtrlLoad = SmartObj.ConnectToDatabase(document.Form1.txtServerName.value, document.Form1.txtDBName.value, document.Form1.txtUserName.value, document.Form1.txtPassword.value, document.Form1.txtDBType.value, document.Form1.txtLoginUserName.value);
            //CtrlLoad = SmartObj.ConnectToDatabase("192.168.4.104", "ADHA", "sa", "s@2012", "SQL", "33");


            if (CtrlLoad == 0) {
                alter("Please Contact your Administrator to register the required Controls");
                return;
            }
        }

      //InitSmartPrint();

    </script>

</body>
</html>
