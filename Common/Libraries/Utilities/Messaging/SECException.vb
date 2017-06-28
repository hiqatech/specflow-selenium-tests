Imports System.ServiceModel
Imports System.Runtime.Serialization

Public Class SECException
    Inherits Exception

    Public Const AccountFormatIncorrect_filiale As String = "*JMC*AR00003DatiRapportoSecOld.filiale"
    Public Const AccountFormatIncorrect_partita As String = "*JMC*AR00002DatiRapportoSecOld.partita"

    Public Const NoAccountFound As String = "ERRORE NON GESTITO: *HST*EX30000 - ZRAPPORTO NON TROVATO"

    Public Sub New(ByVal errorMessage As String, ByVal isMultipleException As Boolean)
        MyBase.New(errorMessage)

        Me.IsMultipleException = isMultipleException

    End Sub

    Public IsMultipleException As Boolean

End Class
