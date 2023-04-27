<%@ Page Title="Import Time Attendance" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="ImportTimeAttendance.aspx.vb" Inherits="ImportTimeAttendance"
    UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpReligion" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="Header1" runat="server" HeaderText="Import Time Attendance" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblExcelAttachment" runat="server" CssClass="Profiletitletxt" Text="Excel Attachment" meta:resourcekey="lblExcelAttachmentResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="BrFromFile" title="Select File" runat="server"
                                name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                Style="display: none;" type="file" meta:resourcekey="BrFromFileResource1" />
                        </span><span class="form-control"></span>
                    </div>
                    <asp:RegularExpressionValidator ID="revFileUploadValidation" Display="Dynamic" ValidationGroup="grpSave"
                        runat="server" ErrorMessage="Only Excel files are allowed!" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$"
                        ControlToValidate="BrFromFile" meta:resourcekey="revFileUploadValidationResource1"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="lblAttention" SkinID="Remark" runat="server"
                        Text="** Please Make Sure That Excel Document Formatted As Below" meta:resourcekey="lblAttentionResource1"></asp:Label>
                </div>
                <div class="col-md-6"></div>
            </div>
            <div class="responsivecustomtable">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDate" runat="server" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmployeeNo" runat="server" Text="EmployeeNo" meta:resourcekey="lblEmployeeNoResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblFirstInTime" runat="server" Text="FirstInTime" meta:resourcekey="lblFirstInTimeResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblLastOutTime" runat="server" Text="LastOutTime" meta:resourcekey="lblLastOutTimeResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblResult" runat="server" Text="Result" meta:resourcekey="lblResultResource1"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDateFormat" runat="server" Text="DateTime Format" meta:resourcekey="lblDateFormatResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmployeeNoFormat" runat="server" Text="Text Format" meta:resourcekey="lblEmployeeNoFormatResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblFirstInTimeFormat" runat="server" Text="Time Format (hh:mm)" meta:resourcekey="lblFirstInTimeFormatResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblLastOutTimeFormat" runat="server" Text="Time Format (hh:mm)" meta:resourcekey="lblLastOutTimeFormatResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblResultFormat" runat="server" Text="Text Format" meta:resourcekey="lblResultFormatResource1"></asp:Label>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnSave" runat="server" Text="Import" ValidationGroup="grpSave"
        CssClass="button" meta:resourcekey="btnSaveResource1" />
</asp:Content>
