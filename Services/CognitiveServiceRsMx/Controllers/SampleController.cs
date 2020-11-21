using risersoft.shared;
using risersoft.shared.cloud;
using risersoft.shared.portable.Models.HR;
using risersoft.shared.portable.Models.Nav;
using risersoft.shared.web;
using risersoft.shared.web.Controllers;
using System.Net;
using System.Web.Http;
using System.Web.Http.Controllers;

/// <summary>
/// ESS Controller
/// </summary>
/// <remarks></remarks>
namespace CognitiveServiceRsMx.Controllers
{

    public class SampleController : ServerApiController<EmployeeInfo, int,EmployeeInfo, bool, HttpStatusCode, ISampleRepository>
    {

        public SampleController(ISampleRepository repository) : base(repository)
        {
        }
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            repository.WebController.CheckInitModel(new clsAppInfo { AppCode = "ess" }, true);
        }
        public IHttpActionResult ClearAuthInfo()
        {
            clsProcOutput oRet = AgentAuthProvider.ClearCache();
            if (oRet.Success)
                return Ok("Done");
            else
                return Ok(oRet.Message);

        }

    }
    //http://stackoverflow.com/questions/24080018/download-file-from-an-asp-net-web-api-method-using-angularjs
}