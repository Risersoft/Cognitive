Imports System.Net
Imports risersoft.shared.portable.Model

''' <summary>
''' Base Server Repository
''' </summary>
''' <typeparam name="T"></typeparam>
''' <typeparam name="TId"></typeparam>
''' <remarks></remarks>
Public Class ServerRepositoryBase(Of T As BaseInfo, TId)
    Inherits RepositoryBase(Of T, TId, Boolean, RSCallerInfo, HttpStatusCode)


    Protected Overrides Function GetStatus(IsError As Boolean) As HttpStatusCode
        If IsError Then Return HttpStatusCode.InternalServerError Else Return HttpStatusCode.OK
    End Function
    Protected Function GetServerEntity() As Object
        Dim strConn As String = ServiceAuthProvider.CalculateConnectionString(Me.User, "Task")
        Return New mxentdbEntities(strConn)
    End Function
End Class
