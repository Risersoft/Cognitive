Imports risersoft.shared.portable.Model
Imports System.Web.Http
Imports risersoft.shared.portable.Models.HR
Imports risersoft.shared
Imports System.Net.Http
Imports System.Net
Imports risersoft.shared.portable.Models
Imports System.Web.Http.Controllers
Imports risersoft.shared.portable.Models.Nav
Imports risersoft.shared.portable

''' <summary>
''' ESS Controller
''' </summary>
''' <remarks></remarks>
<RoutePrefix("api/NAV")>
Public Class NavController
    Inherits ServerApiController(Of clsMobileViewModel, String, Boolean, HttpStatusCode, INavRepository)

    Public Sub New(repository As INavRepository)
        MyBase.New(repository)
    End Sub
    Protected Overrides Sub Initialize(controllerContext As HttpControllerContext)
        MyBase.Initialize(controllerContext)

    End Sub

    Public Function GenerateAppModel(AppInfo As clsAppInfo, GetAppStuff As EnumLoadAppStuff, RelateLoginPerson As String, FYPPComp As Boolean, CommonMenuResourceName As String) As IHttpActionResult
    End Function

    Public Function TryManualTransition(AppInfo As clsAppInfo, vwState As clsMobileViewState, WFKey As String, frmIDX As String, FromStateID As Integer, ToStateID As Integer, ProcDoesWork As Boolean) As clsProcOutput
    End Function

    Public Function DirectoryService() As Dictionary(Of String, String)
    End Function
    Public Function GenerateFilterModel(AppInfo As clsAppInfo, fKey As String, vwState As clsMobileViewState) As clsMobileViewModel
    End Function

    <HttpPost>
    <Route("DetailView")>
    Public Function GenerateDetailView(Params As clsMobileNavParams) As IHttpActionResult

    End Function
    <HttpPost>
    <Route("ListView")>
    Public Function GenerateListView(Params As clsMobileNavParams) As IHttpActionResult
        If repository.WebController.CheckInitModel(Params.Info,
                                               EnumLoadAppStuff.acDataAndSecurity) Then
            Dim result = repository.GenerateView(Params)
            Return Ok(result)
        End If
    End Function

End Class
'http://stackoverflow.com/questions/24080018/download-file-from-an-asp-net-web-api-method-using-angularjs
