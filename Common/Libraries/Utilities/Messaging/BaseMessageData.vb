Public Class BaseMessageData

    Public Sub New()
        _Database = String.Empty
        _Distribution = String.Empty
        _CurrentUser = String.Empty
        _Locale = String.Empty
        _Agent = String.Empty
        _Company = String.Empty
        _Token = String.Empty
        _TimeOut = String.Empty
        _PolicyNumber = String.Empty
        _Fullname = String.Empty
        _Username = String.Empty
        _Client = String.Empty
        CurrentInstance = Me
    End Sub

    Public Sub New(ByVal database As String, ByVal distribution As String, ByVal company As String)
        _Database = database
        _Distribution = distribution
        _Company = company
        CurrentInstance = Me
    End Sub

    Public Sub New(ByVal database As String, ByVal distribution As String, ByVal currentUser As String, ByVal locale As String, ByVal agent As String, ByVal company As String, ByVal timeout As String, ByVal policyNumber As String, ByVal CurrentClientReference As String)
        _Database = database
        _Distribution = distribution
        _CurrentUser = currentUser
        _Locale = locale
        _Agent = agent
        _Company = company
        _TimeOut = timeout
        _PolicyNumber = policyNumber
        _Client = CurrentClientReference
        CurrentInstance = Me
    End Sub

    Public Sub New(ByVal database As String, ByVal distribution As String, ByVal currentUser As String, ByVal locale As String, ByVal agent As String, ByVal company As String, ByVal timeout As String, ByVal policyNumber As String, ByVal fullName As String, ByVal client As String, ByVal username As String)
        _Database = database
        _Distribution = distribution
        _CurrentUser = currentUser
        _Locale = locale
        _Agent = agent
        _Company = company
        _TimeOut = timeout
        _PolicyNumber = policyNumber
        _Fullname = fullName
        _Client = client
        _Username = username
        CurrentInstance = Me
    End Sub

    Public Shared ReadOnly Property Current() As BaseMessageData
        Get
            Return If(CurrentInstance, New BaseMessageData)
        End Get
    End Property

    Private Shared Property CurrentInstance As BaseMessageData
    Public Property Database() As String
    Public Property Agent() As String
    Public Property Distribution() As String
    Public Property CurrentUser() As String
    Public Property PolicyNumber() As String
    Public Property TimeOut() As String
    Public Property Token() As String
    Public Property Company() As String
    Public Property Locale() As String
    Public Property Fullname As String
    Public Property Username As String
    Public Property Client As String
    Public Property Indicator As String

End Class
