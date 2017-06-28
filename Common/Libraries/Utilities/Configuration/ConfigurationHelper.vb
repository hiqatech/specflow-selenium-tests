Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.ServiceModel
Imports Ipsi.Common.Utilities
Imports Ipsi.Tools.Common

Public Class ConfigurationHelper

    Public Shared Function GetProxyFactory(ByVal EndPointUrl As String, Optional ByVal IgnoreSecurity As Boolean = False) As Runtime.DynamicProxyFactory
        If IgnoreSecurity Then
            ServicePointManager.CheckCertificateRevocationList = False
            ServicePointManager.ServerCertificateValidationCallback = New Security.RemoteCertificateValidationCallback(AddressOf IgnoreSSLCertificate)
        End If

        Return New Runtime.DynamicProxyFactory(EndPointUrl)
    End Function

    Public Shared Function IgnoreSSLCertificate(ByVal Sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal policyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Shared Function GetProxy(ByVal CurrentService As ConfigurationsConfigurationService, Optional ByVal IgnoreSecurity As Boolean = False, Optional MessageData As BaseMessageData = Nothing) As Object
        Dim dynamicProxy = Nothing
        Dim currentBinding As New BasicHttpBinding("BasicHttpBinding")

        currentBinding.MaxReceivedMessageSize = Integer.MaxValue

        If CurrentService.Secure Then
            currentBinding.Security.Mode = BasicHttpSecurityMode.Transport
        End If

        If IgnoreSecurity Then
            ServicePointManager.CheckCertificateRevocationList = False
            ServicePointManager.ServerCertificateValidationCallback = New Security.RemoteCertificateValidationCallback(AddressOf IgnoreSSLCertificate)
        End If

        Dim dynamicProxyFactory As Runtime.DynamicProxyFactory = GetProxyFactory(CurrentService.Endpoint, IgnoreSecurity)

        Dim serviceEndpoint As New Description.ServiceEndpoint(New Description.ContractDescription(CurrentService.Interface),
                                                                            currentBinding,
                                                                            New EndpointAddress(CurrentService.Endpoint))


        'serviceEndpoint.EndpointBehaviors.Add(New Ipsi.Common.Utilities.MessageInspectorCustomBehavior(MessageData))


        dynamicProxy = dynamicProxyFactory.CreateProxy(serviceEndpoint)

        dynamicProxy.proxy.Endpoint.Behaviors.Add(New Ipsi.Common.Utilities.MessageInspectorCustomBehavior(MessageData))


        Return dynamicProxy
    End Function

    Public Shared Function GetCompanyInfo(ByVal configs As Configurations, ByVal configurationsFilePath As String) As Dictionary(Of String, String)
        Dim _currentConfigs As Configurations = Nothing
        Dim resultList As New Dictionary(Of String, String)

        If configs Is Nothing Then
            _currentConfigs = Load(configurationsFilePath)
        Else
            _currentConfigs = configs
        End If

        For Each config As ConfigurationsConfiguration In _currentConfigs.Configuration
            resultList.Add(config.Code, config.Name)
        Next

        Return resultList

    End Function

    Public Shared Function Load(ByVal configurationsFilePath As String) As Configurations
        Dim _currentConfigs As Configurations = Nothing

        If File.Exists(configurationsFilePath) Then
            _currentConfigs = Configurations.LoadFromFile(configurationsFilePath)
        End If

        Return _currentConfigs

    End Function

End Class
