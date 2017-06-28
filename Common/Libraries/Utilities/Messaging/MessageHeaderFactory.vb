Option Strict On

Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Xml

Namespace Ipsi.Tools.Common.Messaging

    ''' <summary>
    ''' Provides a mechanism for creating message headers for SOAP messages.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class MessageHeaderFactory

        ''' <summary>
        ''' Creates a soap message header instance with the specified content, name and namespace.
        ''' </summary>
        ''' <param name="content">The content to be added to the soap message header.</param>
        ''' <param name="name">The name of the soap message header.</param>
        ''' <param name="namespace">The namespace of the soap message header.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Create(ByVal content As String, ByVal name As String, ByVal [namespace] As String) As MessageHeader

            If content Is Nothing Then
                Throw New ArgumentException("The 'content' parameter can't be null.")
            End If

            Dim header As MessageHeader(Of String) = New MessageHeader(Of String)(content)
            Dim untypedHeader As MessageHeader = header.GetUntypedHeader(name, [namespace])

            Return untypedHeader

        End Function

    End Class

End Namespace