Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Namespace Helpers
    Public Module DateTimeExtensions
        ''' <summary>
        ''' Formats a date with the th, st, nd, or rd extenion.
        ''' </summary>
        ''' <remarks>
        ''' Hat-tip: http://stackoverflow.com/a/21926632/489433
        ''' </remarks>
        <System.Runtime.CompilerServices.Extension>
        Public Function ToStringWithSuffix(dateTime As DateTime, format As String, Optional suffixPlaceHolder As String = "$") As String
            If format.LastIndexOf("d", StringComparison.Ordinal) = -1 OrElse format.Count(Function(x) x = "d"c) > 2 Then
                Return dateTime.ToString(format)
            End If

            Dim suffix As String
            Select Case dateTime.Day
                Case 1, 21, 31
                    suffix = "st"
                    Exit Select
                Case 2, 22
                    suffix = "nd"
                    Exit Select
                Case 3, 23
                    suffix = "rd"
                    Exit Select
                Case Else
                    suffix = "th"
                    Exit Select
            End Select

            Dim formatWithSuffix = format.Insert(format.LastIndexOf("d", StringComparison.InvariantCultureIgnoreCase) + 1, suffixPlaceHolder)
            Dim [date] = dateTime.ToString(formatWithSuffix)

            Return [date].Replace(suffixPlaceHolder, suffix)
        End Function
    End Module
End Namespace
