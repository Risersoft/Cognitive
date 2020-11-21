Imports System
Imports CognitiveServiceRsMx.Helpers
Imports CognitiveServiceRsMx.Providers
Imports Newtonsoft.Json
Imports risersoft.app.mxform.bot.Helpers
Imports risersoft.app.mxform.bot.Providers

Namespace Models

    Public Class Menu
        <JsonProperty("week")>
        Public Property WeekNumber() As Integer

        <JsonProperty("day")>
        Public Property Day() As String
        <JsonProperty("primo")>
        Public Property Primo() As String

        <JsonProperty("secondo")>
        Public Property Secondo() As String
        <JsonProperty("contorno")>
        Public Property Contorno() As String

        <JsonProperty("dolce")>
        Public Property Dolce() As String

        Public Overloads Function ToString(dated As DateTime, Optional dateTimeProvider As IDateTimeProvider = Nothing) As String
            dateTimeProvider = If(dateTimeProvider, New DateTimeProvider())

            Dim verbTense As String
            If dated = dateTimeProvider.Now().[Date] Then
                verbTense = "is"
            ElseIf dated < dateTimeProvider.Now().[Date] Then
                verbTense = "was"
            Else
                verbTense = "will be"
            End If

            Return String.Format("On {0} {1}, the menu {2} {3}, then {4} with {5}, finishing with {6}", Day, dated.ToStringWithSuffix("d MMMM"), verbTense, Primo, Secondo, Contorno, Dolce)
        End Function
    End Class
End Namespace
