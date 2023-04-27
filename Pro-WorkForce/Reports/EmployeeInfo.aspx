<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmployeeInfo.aspx.vb" Inherits="Reports_EmployeeInfo" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR2:CrystalReportViewer ID="crvCertificate" runat="server" AutoDataBind="True" SeparatePages="False"
            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False"
            HasToggleGroupTreeButton="False" meta:resourcekey="CRVResource1" />
    </div>
    </form>
</body>
</html>
