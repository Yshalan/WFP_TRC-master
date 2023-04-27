<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OTReportViewer.aspx.vb" Inherits="Reports_RptViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
        <tr>
            <td align="center" style="border-left: 1px; border-right: 1px">
                <CR:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                    GroupTreeStyle-ShowLines="False" HasCrystalLogo="False"
                    HasToggleGroupTreeButton="False"   />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
