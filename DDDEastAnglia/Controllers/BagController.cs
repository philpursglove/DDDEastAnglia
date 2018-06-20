using System.Web.Mvc;

namespace DDDEastAnglia.Controllers
{
    public class BagController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}