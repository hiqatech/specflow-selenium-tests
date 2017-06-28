Imports System.ServiceModel
Imports System.Runtime.Serialization

<Serializable()>
Public Class SECGeneralException
    Inherits Exception

    Public Sub New(ByVal errorMessage As String)
        MyBase.New(errorMessage)

    End Sub

End Class
