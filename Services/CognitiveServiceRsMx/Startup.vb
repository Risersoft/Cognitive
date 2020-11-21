Imports Owin
Imports Microsoft.Owin
Imports System.Web.Http
Imports Microsoft.Owin.Security.OAuth
Imports Microsoft.Owin.Security
Imports System.Threading.Tasks
Imports System.Security.Claims
Imports Microsoft.Owin.Security.DataHandler
Imports System.Net
Imports System.Net.Security
Imports risersoft.shared
Imports risersoft.shared.portable.Model
Imports System.Security.Cryptography.X509Certificates

<Assembly: OwinStartup(GetType(Startup))>

Partial Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        Dim config = ConfigureWebApi()
        config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always

        config.EnableCors(New Cors.EnableCorsAttribute("*", "*", "*"))

        Dim OAuthBearerOptions = New OAuthBearerAuthenticationOptions()
        app.UseOAuthBearerAuthentication(OAuthBearerOptions)

        app.UseWebApi(config)
        myApp = New clsWebApiApp(New clsExtendWebAppMxEnt)
        ServicePointManager.ServerCertificateValidationCallback = New RemoteCertificateValidationCallback(Function(sender As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors)
                                                                                                              Return True
                                                                                                          End Function)

    End Sub

    Public Function ConfigureWebApi() As HttpConfiguration
        Dim config = New HttpConfiguration()

        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            "DefaultApi",
            "api/{controller}/{id}",
            New With {.id = RouteParameter.Optional}
        )

        UnityConfig.RegisterComponents(config)

        Return config
    End Function
End Class

