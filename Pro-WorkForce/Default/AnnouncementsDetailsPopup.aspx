<%@ Page Title="Announcements Details" Language="VB" MasterPageFile="~/Default/EmptyMaster.master"
    AutoEventWireup="false" CodeFile="AnnouncementsDetailsPopup.aspx.vb" Inherits="Admin_AnnouncementDetailsPopup" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        table
        {
            width: 100% !important;
        }

        hr
        {
            border: 1px solid #eee;
        }

        br
        {
            display: none;
        }






        .Announcements ul
        {
            list-style: none;
            padding: 10px 0px;
            margin: 0px;
        }

        .Announcements h5
        {
            font-size: 19px;
        }

            .Announcements h5 img
            {
                padding-right: 10px;
                margin-top: -3px;
            }

        .Announcements ul li
        {
            list-style: none;
            padding: 0px 0px;
            margin: 10px 0px;
            display: block;
        }

            .Announcements ul li:nth-child(2) .date
            {
                background: #6f7e9d !important;
            }

            .Announcements ul li:nth-child(3) .date
            {
                background: #99a6c2 !important;
            }

            .Announcements ul li:nth-child(4) .date
            {
                background: #afb9d0 !important;
            }


        .Announcements h5
        {
            background: #4b4b4b;
            color: #fff;
            padding: 0px 12px;
            margin: -15px -25px 0px -25px;
            font-size: 19px;
            height: 50px;
            line-height: 50px;
        }

        .Announcements ul .date
        {
            display: inline-block;
            background: #ff6c60;
            width: 60px;
            height: 60px;
            border-radius: 100%;
            color: #fff;
            vertical-align: middle;
        }

        .Announcements .date span
        {
            display: block;
            text-align: center;
            font-size: 12px;
            margin: 0px;
        }

            .Announcements .date span:first-child
            {
                padding-top: 16px;
            }

        .Announcements .description
        {
            display: inline-block;
            padding-left: -12px;
            color: #888888;
            vertical-align: middle;
            width: 75%;
        }

            .Announcements .description b
            {
                color: #333;
            }

            .Announcements .description a
            {
                display: inline-block;
                height: 30px;
                overflow: hidden;
                font-size: 12px;
                position: relative;
                margin-top: 0px;
                text-decoration: none !important;
            }


                .Announcements .description a:hover
                {
                    color: #ff6c60;
                }

                .Announcements .description a:after
                {
                    content: '...';
                    background: #fff;
                    width: 20px;
                    height: 20px;
                    display: inline-block;
                    position: absolute;
                    right: 0px;
                    bottom: -4px;
                    font-size: 13px;
                }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="pnlAnnouncements" runat="server">
        <ContentTemplate>

            <div id="divAnnouncements" class="Announcements" style="display: block">


                <uc1:PageHeader ID="UserCtrlAnnouncementsDetails" HeaderText="Announcements Details" runat="server" />

                <ul>
                    <li>
                        <span class="date">
                            <asp:Label ID="lblMonth" runat="server"
                                meta:resourcekey="lblDateResource1"></asp:Label>

                            <asp:Label ID="lblDay" runat="server"
                                meta:resourcekey="lblDateResource1"></asp:Label>
                        </span>

                        <span class="description">
                            <b>
                                <asp:Label ID="lblEnglishTitle" runat="server" Text="English Title" meta:resourcekey="CodeResource1"></asp:Label>



                                <asp:Label ID="lblArabicTitle" runat="server" Text="Arabic Title" meta:resourcekey="CodeResource1"></asp:Label>
                            </b>
                            <div class="clear"></div>

                            <asp:Label ID="lblEnglishContent" runat="server" Text="English Content"
                                meta:resourcekey="lblEnglishContentResource1"></asp:Label>

                            <asp:Label ID="lblArabicContent" runat="server" Text="Arabic Content"
                                meta:resourcekey="lblArabicContentResource1"></asp:Label>

                        </span>
                    </li>
                </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

