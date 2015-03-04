using System.Web.Mvc;

namespace EShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Buy The Box.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Address.";

            return View();
        }
    }
}
