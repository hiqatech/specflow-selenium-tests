Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel.Channels
Imports System.Globalization

Public Class MessageInspectorCustomBehavior
    Implements IEndpointBehavior
    Dim _businessData As MessageData = New MessageData()

    Public Sub New(ByRef businessData As MessageData)
        Me._businessData = businessData
    End Sub

    Public Sub AddBindingParameters(ByVal endpoint As ServiceEndpoint, ByVal bindingParameters As BindingParameterCollection) Implements IEndpointBehavior.AddBindingParameters

    End Sub

    Public Sub ApplyClientBehavior(ByVal endpoint As ServiceEndpoint, ByVal clientRuntime As ClientRuntime) Implements IEndpointBehavior.ApplyClientBehavior
        'Add our custom message inspector here  
        clientRuntime.MessageInspectors.Add(New MessageInspector(_businessData))
    End Sub

    Public Sub ApplyDispatchBehavior(ByVal endpoint As ServiceEndpoint, ByVal endpointDispatcher As EndpointDispatcher) Implements IEndpointBehavior.ApplyDispatchBehavior

    End Sub

    Public Sub Validate(ByVal endpoint As ServiceEndpoint) Implements IEndpointBehavior.Validate

    End Sub

End Class

Public Class MessageData
    Public Shared Function GetMessageData(ByVal database As String, ByVal distribution As String, ByVal agent As String, ByVal company As String, ByVal client As String) As MessageData

        Return New MessageData(database, distribution, "LoadTest", GetLocale(), agent, company, 100000, String.Empty, String.Empty)

    End Function

    Public Sub New()
        _Database = String.Empty
        _Distribution = String.Empty
        _CurrentUser = String.Empty
        _Locale = String.Empty
        _Agent = String.Empty
        _Company = String.Empty
        '_Token = String.Empty
        _ServicingAgent = String.Empty
        _TimeOut = String.Empty
        _PolicyNumber = String.Empty
        Me.Client = String.Empty
    End Sub

    Public Sub New(ByVal database As String, ByVal distribution As String, ByVal currentUser As String, ByVal locale As String, ByVal agent As String, ByVal company As String, ByVal timeout As String, ByVal policyNumber As String, ByVal client As String)
        _Database = database
        _Distribution = distribution
        _CurrentUser = currentUser
        _Locale = locale
        _Agent = agent
        _Company = company
        _TimeOut = timeout
        _PolicyNumber = policyNumber
        Me.Client = client
    End Sub

    Public Shared Function GetLocale() As String
        Return CultureInfo.CurrentCulture.Name
    End Function

    Public Property Database() As String
    Public Property Agent() As String
    Public Property Distribution() As String
    Public Property CurrentUser() As String
    Public Property PolicyNumber() As String
    Public Property TimeOut() As String
    'Public Property Token() As String
    Public Property ServicingAgent() As String
    Public Property Company() As String
    Public Property Locale() As String
    Public Property Client() As String

End Class

Public Class MessageInspector
    Implements IClientMessageInspector
    Private ReadOnly _businessData As MessageData
    Public Sub New(ByRef BusinessData As MessageData)
        Me._businessData = BusinessData
    End Sub
    Public Sub AfterReceiveReply(ByRef Reply As Message, ByVal CorrelationState As Object) Implements IClientMessageInspector.AfterReceiveReply

    End Sub

    Public Function BeforeSendRequest(ByRef request As Message, ByVal channel As IClientChannel) As Object Implements IClientMessageInspector.BeforeSendRequest
        Using scope As New OperationContextScope(channel)
            'request.Headers.Add(CreateHeader(System.Threading.Thread.CurrentThread.CurrentCulture.Name, BusinessConstants.LOCALE_ID))
            request.Headers.Add(CreateHeader(_businessData.Database, "database")) 'BusinessConstants.DATABASE_ID))
            request.Headers.Add(CreateHeader(_businessData.Company, "company")) 'BusinessConstants.COMPANY_FIELDNAME))
            request.Headers.Add(CreateHeader(_businessData.Distribution, "distribution")) ' BusinessConstants.DISTRIBUTION_ID))
            'request.Headers.Add(CreateHeader(_businessData.Token, "token")) 'BusinessConstants.TOKEN))
            request.Headers.Add(CreateHeader(_businessData.CurrentUser, "user")) ' BusinessConstants.USER))
            request.Headers.Add(CreateHeader(_businessData.Locale, "language")) 'BusinessConstants.USER))
            request.Headers.Add(CreateHeader(_businessData.Agent, "agent")) 'BusinessConstants.USER))
            request.Headers.Add(CreateHeader(_businessData.Client, "client"))
            request.Headers.Add(CreateHeader(_businessData.ServicingAgent, "servicingagent")) 'BusinessConstants.SERVICINGAGENT))
        End Using
        Return Nothing
    End Function
    Private Function CreateHeader(ByVal value As String, ByVal name As String) As MessageHeader
        If IsNothing(value) Then
            value = String.Empty
        End If
        Dim mhValue As MessageHeader(Of String) = New MessageHeader(Of String)(value)
        Dim untypedValue As MessageHeader = mhValue.GetUntypedHeader(name, "http://web.ipsi.ie/headers/core/2008/03")
        Return untypedValue
    End Function
End Class
