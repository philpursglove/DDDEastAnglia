using System.Web.Mvc;
using DDDEastAnglia.Models;

namespace DDDEastAnglia.Controllers
{
    public class BagController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(BagIndexViewModel model)
        {
            return View("Index");
        }
    }
}