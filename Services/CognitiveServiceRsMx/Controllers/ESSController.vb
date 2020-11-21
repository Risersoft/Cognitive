Imports risersoft.shared.portable.Model
Imports System.Web.Http
Imports risersoft.shared.portable.Models.HR
Imports risersoft.shared
Imports System.Net.Http
Imports System.Net
Imports risersoft.shared.portable.Models
Imports System.Web.Http.Controllers
Imports risersoft.shared.portable.Models.Nav

''' <summary>
''' ESS Controller
''' </summary>
''' <remarks></remarks>
<RoutePrefix("api/ESS")>
Public Class ESSController
    Inherits ServerApiController(Of EmployeeInfo, Integer, Boolean, HttpStatusCode, IESSRepository)

    Public Sub New(repository As IESSRepository)
        MyBase.New(repository)
    End Sub
    Protected Overrides Sub Initialize(controllerContext As HttpControllerContext)
        MyBase.Initialize(controllerContext)
        repository.WebController.CheckInitModel(New clsAppInfo With {.AppCode = "ess"},
                                                EnumLoadAppStuff.acDataAndSecurity)
    End Sub
    <HttpGet>
    <Route("Clear")>
    Public Function ClearAuthInfo() As IHttpActionResult
        Dim oRet As clsProcOutput = ServiceAuthProvider.ClearCache
        If oRet.Success Then Return Ok("Done") Else Return Ok(oRet.Message)

    End Function

End Class
'http://stackoverflow.com/questions/24080018/download-file-from-an-asp-net-web-api-method-using-angularjs
