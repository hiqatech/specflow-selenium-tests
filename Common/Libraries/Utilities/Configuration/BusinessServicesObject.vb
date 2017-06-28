Imports System.Reflection
Imports System.Xml
Imports Ipsi.Common.Utilities
Imports Ipsi.Tools.Common


Public Module BusinessServicesObject
    'Private _proxy
    'Private _endPointAdded As Boolean = False
    'Private _currentConfiguation As ConfigurationsConfiguration
    'Private _configurations As Configurations

    Public Function GetServiceMethods(ByVal serviceName As String, services As List(Of ConfigurationsConfigurationService), proxy As Object) As SortedDictionary(Of String, String)

        Dim typesInUse As New List(Of Type)
        Dim dicSortedOperations As New SortedDictionary(Of String, String)()

        Dim currentService As New ConfigurationsConfigurationService
        For Each service As ConfigurationsConfigurationService In services
            If service.Name = serviceName Then
                currentService = service
            End If
        Next

        For Each serviceContract In ConfigurationHelper.GetProxyFactory(currentService.Endpoint, False).Contracts
            For Each serviceOperation In serviceContract.Operations
                If Not String.IsNullOrEmpty(serviceOperation.Name) Then

                    dicSortedOperations.Add(serviceOperation.Name,
                                           currentService.Class + "." + serviceOperation.Name)
                End If
            Next
        Next

        typesInUse.Add(proxy.GetType())

        Return dicSortedOperations

    End Function

    Public Function GetServiceParameters(ByVal serviceName As String, ByVal serviceMethod As String, services As List(Of ConfigurationsConfigurationService), proxy As Object) As ParameterInfo()

        If IsNothing(proxy) Then
            proxy = SetProxy(serviceName, services)
        End If

        Dim method As MethodInfo = proxy.GetType().GetMethod(serviceMethod)

        Dim parameters As ParameterInfo() = method.GetParameters()

        Return parameters

    End Function

    Public Function SetProxy(ByVal serviceName As String, services As List(Of ConfigurationsConfigurationService), Optional MessageData As BaseMessageData = Nothing) As Object
        Dim typesInUse As New List(Of Type)
        Dim dicSortedOperations As New SortedDictionary(Of String, String)()

        Dim currentService As New ConfigurationsConfigurationService
        For Each service As ConfigurationsConfigurationService In services
            If service.Name = serviceName Then
                currentService = service
            End If
        Next

        'If Not MessageData Is Nothing Then
        '    'DirectCast(DirectCast(dynamicProxy, Runtime.DynamicProxy).obj, BusinessServiceClient).Endpoint
        '    dynamicProxy.Endpoint.Behaviors.Add(New Ipsi.Common.Utilities.MessageInspectorCustomBehavior(MessageData))
        'End If

        Dim dynamicProxy = ConfigurationHelper.GetProxy(currentService, True, MessageData)

        Return dynamicProxy.proxy

    End Function

    Public Function PopulateParamObj(ByVal obj As Object, paramTypeFullName As String, ByVal node As XmlElement) As Object
        Dim properties As PropertyInfo() = obj.GetType.GetProperties

        For Each prop As PropertyInfo In properties

            If Not IgnoreType(prop) Then
                Dim propNodeName As String = String.Empty
                If Not String.IsNullOrEmpty(paramTypeFullName) Then
                    propNodeName = String.Format("*[local-name()='{0}']/*[local-name()='{1}']", paramTypeFullName, prop.Name)
                Else
                    propNodeName = String.Format("*[local-name()='{0}']", prop.Name)
                End If

                Dim propNode As XmlElement = node.SelectSingleNode(propNodeName)
                If Not IsNothing(propNode) Then
                    If Not String.IsNullOrEmpty(propNode.InnerText) Then
                        Dim convertionType As Type

                        If IsNullableType(prop.PropertyType) Then
                            convertionType = Nullable.GetUnderlyingType(prop.PropertyType)
                        Else
                            convertionType = prop.PropertyType
                        End If

                        If convertionType.IsEnum Then
                            prop.SetValue(obj, [Enum].Parse(convertionType, propNode.InnerText), Nothing)
                        ElseIf convertionType.IsArray Then

                            Dim subElementType As Type = convertionType.GetElementType()

                            Dim subNodes As XmlNodeList = propNode.SelectNodes(String.Format("*[local-name()='{0}']", subElementType.Name))

                            Dim subObjArray = Array.CreateInstance(subElementType, subNodes.Count)

                            Dim arrayCount As Integer = 0
                            For Each subNode As XmlElement In subNodes
                                Dim subObj As Object = Activator.CreateInstance(subElementType)
                                subObjArray(arrayCount) = PopulateParamObj(subObj, String.Empty, subNode)
                                arrayCount += 1
                            Next

                            prop.SetValue(obj, subObjArray, Nothing)
                        ElseIf convertionType.IsClass And (convertionType.Namespace Is Nothing OrElse Not convertionType.Namespace.StartsWith("System")) Then
                            Dim subObj As Object = Activator.CreateInstance(convertionType)
                            prop.SetValue(obj, PopulateParamObj(subObj, String.Empty, propNode), Nothing)
                        Else
                            prop.SetValue(obj, Convert.ChangeType(propNode.InnerText, convertionType), Nothing)
                        End If

                        'Now we need to see if there is a specified item for this prop and set it to true
                        Dim specifiedProp As PropertyInfo = properties.SingleOrDefault(Function(c) c.Name = String.Concat(prop.Name, "Specified"))
                        If Not IsNothing(specifiedProp) Then
                            specifiedProp.SetValue(obj, True, Nothing)
                        End If

                    End If
                End If

            End If

        Next

        Return obj

    End Function


    Private Function IgnoreType(ByVal t As PropertyInfo) As Boolean

        Return t.PropertyType.FullName = "System.Boolean" And Strings.Right(t.Name, 9) = "Specified"

    End Function

    Private Function IsNullableType(ByVal type As Type)

        Return (type.IsGenericType) AndAlso (type.GetGenericTypeDefinition() Is GetType(Nullable(Of )))

    End Function

End Module
