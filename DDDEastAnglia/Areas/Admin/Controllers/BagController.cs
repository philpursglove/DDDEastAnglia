using System.Web.Mvc;
using DDDEastAnglia.Models;

namespace DDDEastAnglia.Areas.Admin.Controllers
{
    public class BagController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(SponsorBagContent model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(SponsorBagContent model)
        {
            
        }
    }
}
