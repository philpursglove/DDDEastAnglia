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
            
        }

        [HttpPost]
        public ActionResult Edit(SponsorBagContent model)
        {
            if (ModelState.IsValid)
            {
                
            }
        }

        public ActionResult Delete(int id)
        {
            
        }
    }
}
