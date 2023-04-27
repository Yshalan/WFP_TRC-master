
Imports Microsoft.VisualBasic
Namespace SmartV.Security.MENU
    Public Class SECModule
        Private pModuleID As Integer
        Private pModuleArabicName As String
        Private pModuleEnglishName As String
        Private pUpperImage As String
        Private pLowerImage As String
        Private pDefaultPAge As String


        Public Property moduleID() As Integer
            Get
                Return pModuleID
            End Get
            Set(ByVal value As Integer)
                pModuleID = value
            End Set
        End Property

        Public Property moduleArabicName() As String
            Get
                Return pModuleArabicName
            End Get
            Set(ByVal value As String)
                pModuleArabicName = value
            End Set
        End Property

        Public Property moduleEnglishName() As String
            Get
                Return pModuleEnglishName
            End Get
            Set(ByVal value As String)
                pModuleEnglishName = value
            End Set
        End Property

        Public Property UpperImage() As String
            Get
                Return pUpperImage
            End Get
            Set(ByVal value As String)
                pUpperImage = value
            End Set
        End Property

        Public Property LowerImage() As String
            Get
                Return pLowerImage
            End Get
            Set(ByVal value As String)
                pLowerImage = value
            End Set
        End Property

        Public Property defaultPage() As String
            Get
                Return pDefaultPAge
            End Get
            Set(ByVal value As String)
                pDefaultPAge = value
            End Set
        End Property

    End Class
End Namespace

