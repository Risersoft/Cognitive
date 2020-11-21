using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using risersoft.shared.web;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartup(typeof(CognitiveServiceRsMx.Startup))]

namespace CognitiveServiceRsMx
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            //var OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            //app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            app.UseRSAuthClient();
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                AuthenticationType = OwinHelper.Bearer,
                AuthenticationMode = AuthenticationMode.Passive,
                Provider = new BearerOAuthProvider()
            });

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => { return true; });

        }
    }
}
