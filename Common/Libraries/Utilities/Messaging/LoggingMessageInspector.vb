Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Dispatcher
Imports System.ServiceModel.Description
Imports System.ServiceModel.Configuration
Imports System.Configuration
Imports System.Xml
Imports Ipsi.Common.Utilities.Diagnostics
Imports Ipsi.Common.Utilities.EventLoggerService
Imports System.ComponentModel
Imports System.Threading.Tasks

Public Class LoggingMessageInspector
    Implements IClientMessageInspector
    Private ReadOnly _businessData As BaseMessageData
    Private startTask As Task = Nothing
    Public Sub New(ByRef BusinessData As BaseMessageData)
        Me._businessData = BusinessData
    End Sub

    Public Function BeforeSendRequest(ByRef request As Message, ByVal channel As IClientChannel) As Object Implements IClientMessageInspector.BeforeSendRequest
        Dim requestStartTime As DateTime = Now()
        Dim requestGUID As Guid = Guid.NewGuid()
        Try
            Dim mb As MessageBuffer = request.CreateBufferedCopy(Int32.MaxValue)
            'ServiceModel message can only be read/copies once so make a copy of it and reset the original
            request = mb.CreateMessage
            Dim requestCopy As Channels.Message = mb.CreateMessage
            startTask = Task.Run(Sub() CommitStartServiceCall(requestGUID, requestStartTime, requestCopy, channel.RemoteAddress.ToString))

        Catch ex As Exception
            Logger.GetLogger.WriteError("BeforeSendRequest>> Unable to log Request start details", ex, False)
            requestGUID = Nothing
        End Try
        If Not IsNothing(requestGUID) Then
            Return requestGUID
        Else
            Return Nothing
        End If
    End Function

    Private Sub CommitStartServiceCall(requestGUID As Guid, requestStartTime As DateTime, request As Message, serviceURL As String)
        Try
            Dim operation, servicerequest As String
            Dim reader As XmlDictionaryReader = request.GetReaderAtBodyContents()
            operation = reader.LocalName
            reader.GetType()
            servicerequest = reader.ReadOuterXml()

            'LGII001-730 -Load and search xelement for node name contained in list; strip namespace prefix and namespaces
            Dim policyNumber As String = String.Empty
            Dim policyNumberNames As New List(Of String)(New String() {"policynumber", "policynum", "policyno", "policy_number"})
            If policyNumberNames.Any(Function(b) servicerequest.ToLower().Contains(b.ToLower())) Then
                Dim requestElement As XElement = XElement.Parse(servicerequest)
                For Each name As String In policyNumberNames
                    Dim elements As IEnumerable(Of XElement) = requestElement.Descendants
                    Dim foundElement As XElement = elements.FirstOrDefault(Function(f) f.Name.LocalName.ToLower = name.ToLower)
                    If Not IsNothing(foundElement) Then
                        policyNumber = foundElement.Value
                        Exit For
                    End If
                Next
            End If
            reader.Close()
            Dim logProxy As New EventLoggerClient()
            Dim startRequest As StartServiceCallLogEntry
            Dim additionalInfo As New AdditionalInformation
            additionalInfo.Items = New List(Of AdditionalInformationItem)
            additionalInfo.Items.Add(New AdditionalInformationItem() With {.Key = "Database", .Value = _businessData.Database})
            additionalInfo.Items.Add(New AdditionalInformationItem() With {.Key = "Language", .Value = _businessData.Locale})
            additionalInfo.Items.Add(New AdditionalInformationItem() With {.Key = "Client", .Value = _businessData.Client})
            additionalInfo.Items.Add(New AdditionalInformationItem() With {.Key = "Agent", .Value = _businessData.Agent})
            additionalInfo.Items.Add(New AdditionalInformationItem() With {.Key = "Token", .Value = _businessData.Token})
            startRequest = New StartServiceCallLogEntry() With { _
                .Id = requestGUID, .Company = _businessData.Company, .Distribution = _businessData.Distribution, _
                .User = _businessData.CurrentUser, .PolicyNumber = policyNumber, .URL = serviceURL, .Operation = operation, _
                .Request = servicerequest, .StartTime = requestStartTime, .AdditionalInformation = additionalInfo}
            logProxy.LogStartServiceCall(startRequest)
        Catch ex As Exception
            Logger.GetLogger.WriteError("CommitStartServiceCall>> Unable to log Request start details", ex, False)
        End Try
    End Sub

    Public Sub AfterReceiveReply(ByRef Reply As Message, ByVal CorrelationState As Object) Implements IClientMessageInspector.AfterReceiveReply
        Try
            If Not IsNothing(CorrelationState) Then
                Dim requestGUID As Guid
                Dim replyCopy As Channels.Message
                Dim requestEndTime As DateTime = Now()
                requestGUID = CType(CorrelationState, Guid)
                Dim mb As MessageBuffer = Reply.CreateBufferedCopy(Int32.MaxValue)
                'ServiceModel message can only be read/copies once so make a copy of it and reset the original
                Reply = mb.CreateMessage
                replyCopy = mb.CreateMessage
                'DF - Run this as an async task
                Task.WaitAny(New Task() {startTask}, 5000)
                Task.Run(Sub() CommitEndServiceCall(requestGUID, requestEndTime, replyCopy))
            End If
        Catch ex As Exception
            Logger.GetLogger.WriteError("AfterReceiveReply>> Unable to log Request end details", ex, False)
        End Try
    End Sub

    Private Sub CommitEndServiceCall(requestGUID As Guid, requestEndTime As DateTime, response As Message)
        Dim servicersponse As String
        Try
            Dim reader As XmlReader = response.GetReaderAtBodyContents()
            servicersponse = reader.ReadOuterXml()
            Dim logProxy As New EventLoggerClient()
            Dim endRequest As New EndServiceCallLogEntry() With { _
                .Id = requestGUID, .EndTime = requestEndTime, .Response = servicersponse}
            logProxy.LogEndServiceCall(endRequest)
        Catch ex As Exception
            Logger.GetLogger.WriteError("CommitEndServiceCall>> Unable to log Request end details", ex, False)
        End Try
    End Sub
End Class

Public Class LoggingMessageInspectorCustomBehavior
    Implements IEndpointBehavior
    Dim _businessData As BaseMessageData = New BaseMessageData()

    Public Sub New()
    End Sub

    Public Sub New(ByRef businessData As BaseMessageData)
        Me._businessData = businessData
    End Sub

    Public Sub AddBindingParameters(ByVal endpoint As ServiceEndpoint, ByVal bindingParameters As BindingParameterCollection) Implements IEndpointBehavior.AddBindingParameters

    End Sub

    Public Sub ApplyClientBehavior(ByVal endpoint As ServiceEndpoint, ByVal clientRuntime As ClientRuntime) Implements IEndpointBehavior.ApplyClientBehavior
        'Add our custom message inspector here  
        clientRuntime.MessageInspectors.Add(New LoggingMessageInspector(_businessData))
    End Sub

    Public Sub ApplyDispatchBehavior(ByVal endpoint As ServiceEndpoint, ByVal endpointDispatcher As EndpointDispatcher) Implements IEndpointBehavior.ApplyDispatchBehavior

    End Sub

    Public Sub Validate(ByVal endpoint As ServiceEndpoint) Implements IEndpointBehavior.Validate

    End Sub
End Class

