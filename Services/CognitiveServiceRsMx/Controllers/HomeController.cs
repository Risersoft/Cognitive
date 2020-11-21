using risersoft.shared.web;
using System.Web.Mvc;

namespace CognitiveServiceRsMx.Controllers
{
    public class HomeController : clsMvcControllerBase
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Parent()
        {
            return Redirect("http://www.risersoft.com");
        }

        public ActionResult Explore()
        {
            return Redirect("http://www.risersoft.com/Cognitive");
        }

        public ActionResult BuyApp()
        {
            var portal = this.Host.Portal.ToString();
            return Redirect(portal + "/account/create?license=buy&product=CognitiveServices");
        }

        public ActionResult TryApp()
        {
            var portal = this.Host.Portal.ToString();
            return Redirect(portal + "/account/create?license=buy&product=CognitiveServices");
        }
    }
}