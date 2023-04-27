Imports Microsoft.VisualBasic
Imports SmartV.Security
Imports System.Text

Namespace SmartV.UTILITIES

    Public Class AISMenu
        Private pName As String
        Private plink As String
        Private pSubMenu As AISMenu
        Private pNextMenu As AISMenu
        Private pIsItem As Boolean
        Private pIsUpperLevel As Boolean
        Private parentItemLevel As Integer = 0
        Private parentMenuLevel As Integer = 0
        Public Enum menusDirection
            rtl = 0
            ltr = 1
        End Enum

        Public Shared dir As menusDirection

        Public Property name() As String
            Get
                Return pName
            End Get
            Set(ByVal value As String)
                pName = value
            End Set
        End Property
        Public Property link() As String
            Get
                Return plink
            End Get
            Set(ByVal value As String)
                plink = value
            End Set
        End Property

        Public Property subMenu() As AISMenu
            Get
                Return pSubMenu
            End Get
            Set(ByVal value As AISMenu)
                pSubMenu = value
            End Set
        End Property

        Public Property nextMenu() As AISMenu
            Get
                Return pNextMenu
            End Get
            Set(ByVal value As AISMenu)
                pNextMenu = value
            End Set
        End Property



        Public Property isUpperLevel() As Boolean
            Get
                Return pIsUpperLevel
            End Get
            Set(ByVal value As Boolean)
                pIsUpperLevel = value
            End Set
        End Property



        Public Function buildRestOfMenu(ByVal isFirstLevel As Boolean, ByVal isFirstInMenu As Boolean, ByVal itemLevel As Integer, ByVal menuLevel As Integer, ByVal str As StringBuilder, Optional ByVal firstSeparator As Boolean = True) As String
            If SessionVariables.CultureInfo = "ar-JO" Then
                If Me.subMenu IsNot Nothing Then
                    str.AppendLine("<li><a href='" + Me.link + "' class='parent'><span style='text-align:right'>" + Me.name + "</span></a>")
                    str.AppendLine("<ul>")
                    Me.subMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                    str.AppendLine("</ul>")
                    str.AppendLine("</li>")
                Else
                    str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:right'>" + Me.name + "</span></a></li>")
                End If
                If Me.nextMenu IsNot Nothing Then
                    Me.nextMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                End If
            Else
                If Me.subMenu IsNot Nothing Then
                    str.AppendLine("<li><a href='" + Me.link + "' class='parent'><span style='text-align:left'>" + Me.name + "</span></a>")
                    str.AppendLine("<ul>")
                    Me.subMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                    str.AppendLine("</ul>")
                    str.AppendLine("</li>")
                Else
                    str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a></li>")
                End If
                If Me.nextMenu IsNot Nothing Then
                    Me.nextMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                End If
            End If

            Return str.ToString



        End Function


        Public Function getMenuString1() As String
            Dim str As StringBuilder = New StringBuilder()

            'added by sudheesh to change the direction of the menu items on 05-sep-2010

            If SessionVariables.CultureInfo = "en-US" Then
                str.AppendLine("<div id='menu' style='width:100%;'>")
                str.AppendLine("<ul class='menu'>")

                str.AppendLine("<li><a href='../Default/Default.aspx' class='parent'><span style='text-align:left'>Home</a></li>")
                ' str.AppendLine("<li><a href='../Default/LangTest.aspx' class='parent'><span style='text-align:left'>Home</a></li>")

                buildRestOfMenu(True, True, 0, 0, str)

                str.AppendLine("<li><a href='../Security/ChangePassword.aspx' class='parent'><span style='text-align:left'>Change Password</span></a></li>")
                str.AppendLine("<li><a href='../Default/SignOut.aspx' class='parent'><span style='text-align:left'>Log Out</span></a></li>")

                str.AppendLine("</ul></div>")

            Else
                str.AppendLine("<div id='menu1' style='width:100%;'>")
                str.AppendLine("<ul class='menu1'>")

                str.AppendLine("<li><a href='../Default/Default.aspx' class='parent'><span style='text-align:left'>الرئيسية</span></a></li>")
                'str.AppendLine("<li><a href='../Default/LangTest.aspx' class='parent'><span style='text-align:left'>الرئيسية</a></li>")

                buildRestOfMenu(True, True, 0, 0, str)

                str.AppendLine("<li><a href='../Security/ChangePassword.aspx' class='parent'><span style='text-align:left'>تغيير كلمة السر</span></a></li>")
                str.AppendLine("<li><a href='../Default/SignOut.aspx' class='parent'><span style='text-align:left'>خروج</span></a></li>")


                str.AppendLine("</ul></div>")
            End If
            ''''''''''''''''''''''''''''''''''''''End''''''''''''''''''''''''''''''''''''''
            Return str.ToString
        End Function


        Public Function buildRestOfMenuVertical(ByVal isFirstLevel As Boolean, ByVal isFirstInMenu As Boolean, ByVal itemLevel As Integer, ByVal menuLevel As Integer, ByVal str As StringBuilder, Optional ByVal firstSeparator As Boolean = True) As String
            If Me.subMenu IsNot Nothing Then

                str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a>")
                str.AppendLine("<ul>")
                Me.subMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                str.AppendLine("</ul>")
                str.AppendLine("</li>")
            Else
                str.AppendLine("<li><a class='qmparent' href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a></li>")
                'str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a></li>")
            End If
            If Me.nextMenu IsNot Nothing Then
                Me.nextMenu.buildRestOfMenuVertical(False, False, 1, 1, str, True)
            End If
            Return str.ToString
        End Function


        'Public Function SubitemMenuVertical(ByVal f As SECForm) As String
        '    Dim str As StringBuilder = New StringBuilder()
        '    str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a>")
        '    Return str.ToString
        'End Function

        Public Function getMenuStringVertical() As String
            Dim str As StringBuilder = New StringBuilder()
            str.AppendLine("<ul id='qm0' class='qmmc'>")
            buildRestOfMenuVertical(True, True, 0, 0, str)
            str.AppendLine("<li><a href='../FDON/ChangePassword.aspx'><span style='text-align:left'>:: Change Password</span></a></li>")
            str.AppendLine("<li><a href='../Default/LogOut.aspx'><span style='text-align:left'>:: Log Out</span></a></li>")
            str.AppendLine("</ul>")
            Return str.ToString
        End Function
        Public Function buildRestOfMenu_sys(ByVal isFirstLevel As Boolean, ByVal isFirstInMenu As Boolean, ByVal itemLevel As Integer, ByVal menuLevel As Integer, ByVal str As StringBuilder, Optional ByVal firstSeparator As Boolean = True) As String
            If SessionVariables.CultureInfo = "ar-JO" Then
                '    If Me.subMenu IsNot Nothing Then
                '        str.AppendLine("<li><a href='" + Me.link + "' class='parent'><span style='text-align:right'>" + Me.name + "</span></a>")
                '        str.AppendLine("<ul>")
                '        Me.subMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                '        str.AppendLine("</ul>")
                '        str.AppendLine("</li>")
                '    Else
                '        str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:right'>" + Me.name + "</span></a></li>")
                '    End If
                '    If Me.nextMenu IsNot Nothing Then
                '        Me.nextMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                '    End If
                'Else
                '    If Me.subMenu IsNot Nothing Then
                '        str.AppendLine("<li><a href='" + Me.link + "' class='parent'><span style='text-align:left'>" + Me.name + "</span></a>")
                '        str.AppendLine("<ul>")
                '        Me.subMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                '        str.AppendLine("</ul>")
                '        str.AppendLine("</li>")
                '    Else
                '        str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a></li>")
                '    End If
                '    If Me.nextMenu IsNot Nothing Then
                '        Me.nextMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                '    End If

                If Me.subMenu IsNot Nothing Then
                    str.AppendLine("<h3 class='headerbar'><a href='" + Me.link + "' class='parent'>" + Me.name + "</h3>")
                    str.AppendLine("<ul class='submenu'>")
                    Me.subMenu.buildRestOfMenu_sys(False, False, 1, 1, str, True)
                    str.AppendLine("</ul>")
                Else
                    str.AppendLine("<li><a href='" + Me.link + "'>" + Me.name + "</a></li>")
                End If
                If Me.nextMenu IsNot Nothing Then
                    Me.nextMenu.buildRestOfMenu_sys(False, False, 1, 1, str, True)
                End If
            Else
                If Me.subMenu IsNot Nothing Then

                    str.AppendLine("<h3 class='headerbar'><a href='" + Me.link + "' class='parent'>" + Me.name + "</a></h3>")
                    str.AppendLine("<ul class='submenu'>")
                    Me.subMenu.buildRestOfMenu_sys(False, False, 1, 1, str, True)

                    str.AppendLine("</ul>")
                Else

                    str.AppendLine("<li><a href='" + Me.link + "'>" + Me.name + "</a></li>")

                End If
                If Me.nextMenu IsNot Nothing Then
                    Me.nextMenu.buildRestOfMenu(False, False, 1, 1, str, True)
                End If
            End If

            Return str.ToString



        End Function


        Public Function getMenuString_sys(ByVal strs As String) As String
            Dim str As StringBuilder = New StringBuilder()

            'added by sudheesh to change the direction of the menu items on 05-sep-2010

            If SessionVariables.CultureInfo = "en-US" Then
                '    str.AppendLine("<div id='menu' style='width:100%;'>")
                '    str.AppendLine("<ul class='menu'>")

                '    str.AppendLine("<li><a href='../Default/Default.aspx' class='parent'><span style='text-align:left'>Home</a></li>")
                '    ' str.AppendLine("<li><a href='../Default/LangTest.aspx' class='parent'><span style='text-align:left'>Home</a></li>")

                '    buildRestOfMenu(True, True, 0, 0, str)

                '    str.AppendLine("<li><a href='../Security/ChangePassword.aspx' class='parent'><span style='text-align:left'>Change Password</span></a></li>")
                '    str.AppendLine("<li><a href='../Default/SignOut.aspx' class='parent'><span style='text-align:left'>Log Out</span></a></li>")

                '    str.AppendLine("</ul></div>")

                'Else
                '    str.AppendLine("<div id='menu1' style='width:100%;'>")
                '    str.AppendLine("<ul class='menu1'>")

                '    str.AppendLine("<li><a href='../Default/Default.aspx' class='parent'><span style='text-align:left'>الرئيسية</span></a></li>")
                '    'str.AppendLine("<li><a href='../Default/LangTest.aspx' class='parent'><span style='text-align:left'>الرئيسية</a></li>")

                '    buildRestOfMenu(True, True, 0, 0, str)

                '    str.AppendLine("<li><a href='../Security/ChangePassword.aspx' class='parent'><span style='text-align:left'>تغيير كلمة السر</span></a></li>")
                '    str.AppendLine("<li><a href='../Default/SignOut.aspx' class='parent'><span style='text-align:left'>خروج</span></a></li>")


                '    str.AppendLine("</ul></div>")
                str.AppendLine("<div id='menu' class='urbangreymenu' runat='server'>")
                'str.AppendLine("<ul class='menu'>")

                str.AppendLine("<h3 class='headerbars'><a href='../Default/Default.aspx'  >Home</a></h3>")
                ' str.AppendLine("<li><a href='../Default/LangTest.aspx' class='parent'><span style='text-align:left'>Home</a></li>")

                ' buildRestOfMenu(True, True, 0, 0, str)
                str.AppendLine(strs)
                str.AppendLine("<h3 class='headerbars'><a href='../Security/ChangePassword.aspx'  >Change Password</a></h3>")
                str.AppendLine("<h3 class='headerbars'><a href='../Default/SignOut.aspx'  >Log Out</a></h3>")

                'str.AppendLine("</ul></div>")
                str.AppendLine("</div>")

            Else
                str.AppendLine("<div id='menu' class='Ar_urbangreymenu' runat='server'>")
                'str.AppendLine("<ul class='menu1'>")

                str.AppendLine("<h3  class='Ar_headerbars'><a href='../Default/Default.aspx' >الرئيسية</a></h3>")
                'str.AppendLine("<li><a href='../Default/LangTest.aspx' class='parent'><span style='text-align:left'>الرئيسية</a></li>")

                'buildRestOfMenu(True, True, 0, 0, str)
                str.AppendLine(strs)

                str.AppendLine("<h3 class='Ar_headerbars'><a href='../Security/ChangePassword.aspx'  >تغيير كلمة السر</a></h3>")
                str.AppendLine("<h3 class='Ar_headerbars'><a href='../Default/SignOut.aspx' >خروج</a></h3>")


                'str.AppendLine("</ul></div>")
                str.AppendLine("</div>")

            End If
            ''''''''''''''''''''''''''''''''''''''End''''''''''''''''''''''''''''''''''''''
            Return str.ToString
        End Function

        Public Function getList(ByVal furl As String, ByVal fname As String, Optional ByVal langID As Integer = 0) As String
            Dim str As String
            If langID = 0 Then
                str = "<li><a href='" + furl + "'>" + fname + "</a></li>"
            Else
                str = "<li><a href='" + furl + "'>" + fname + "</a></li>"
            End If

            Return str.ToString
        End Function
        Public Function getUl(ByVal heads As String, ByVal strs As String, Optional ByVal langID As Integer = 0) As String
            Dim str As String
            If langID = 0 Then
                str = "<h3 class='headerbar'><a href='#' class='parent'>" + heads + "</a></h3><ul class='submenu'>" + strs + "</ul>"
            Else
                str = "<h3 class='Ar_headerbar'><a href='#' class='parent'>" + heads + "</a></h3><ul class='Ar_submenu'>" + strs + "</ul>"
            End If

            Return str.ToString
        End Function
        Public Function getUl(ByVal strs As String, Optional ByVal langID As Integer = 0) As String
            Dim str As String
            If langID = 0 Then
                str = "<div  class='urbangreymenu' runat='server'>" + strs + "</div>"
            Else
                str = "<div  class='Ar_urbangreymenu' runat='server'>" + strs + "</div>"
            End If

            Return str.ToString
        End Function

        Public Function buildRestOfMenuVertical_sys(ByVal isFirstLevel As Boolean, ByVal isFirstInMenu As Boolean, ByVal itemLevel As Integer, ByVal menuLevel As Integer, ByVal str As StringBuilder, Optional ByVal firstSeparator As Boolean = True) As String
            If Me.subMenu IsNot Nothing Then

                str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a>")
                str.AppendLine("<ul>")
                Me.subMenu.buildRestOfMenu_sys(False, False, 1, 1, str, True)
                str.AppendLine("</ul>")
                str.AppendLine("</li>")
            Else
                str.AppendLine("<li><a class='qmparent' href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a></li>")
                'str.AppendLine("<li><a href='" + Me.link + "'><span style='text-align:left'>" + Me.name + "</span></a></li>")
            End If
            If Me.nextMenu IsNot Nothing Then
                Me.nextMenu.buildRestOfMenuVertical_sys(False, False, 1, 1, str, True)
            End If
            Return str.ToString
        End Function

        Public Function getMenuStringVertical_sys() As String
            Dim str As StringBuilder = New StringBuilder()
            str.AppendLine("<ul id='qm0' class='qmmc'>")
            buildRestOfMenuVertical_sys(True, True, 0, 0, str)
            str.AppendLine("<li><a href='../FDON/ChangePassword.aspx'><span style='text-align:left'>:: Change Password</span></a></li>")
            str.AppendLine("<li><a href='../Default/LogOut.aspx'><span style='text-align:left'>:: Log Out</span></a></li>")
            str.AppendLine("</ul>")
            Return str.ToString
        End Function

    End Class

End Namespace
