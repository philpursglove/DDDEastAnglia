using System.Web.Mvc;
using DDDEastAnglia.Helpers;
using DDDEastAnglia.Models;

namespace DDDEastAnglia.Controllers
{
    public class BagController : Controller
    {
        private readonly ITicketProvider ticketProvider;

        public BagController(ITicketProvider ticketProvider)
        {
            this.ticketProvider = ticketProvider ?? throw new System.ArgumentNullException(nameof(ticketProvider));
        }
        // GET
        public ActionResult Index()
        {
            if (ControllerContext.HttpContext.Session["ValidatedTicket"] != null && (bool)ControllerContext.HttpContext.Session["ValidatedTicket"])
            {
                return RedirectToAction("Contents");
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(BagIndexViewModel model)
        {
            if (ticketProvider.TicketIsForOurEvent(model.OrderNumber))
            {
                ControllerContext.HttpContext.Session["ValidatedTicket"] = true;
                return RedirectToAction("Contents");
            }
            return View("Index");
        }

        public ActionResult Contents()
        {
            if (!(bool)ControllerContext.HttpContext.Session["ValidatedTicket"] || ControllerContext.HttpContext.Session["ValidatedTicket"] == null)
            {
                return RedirectToAction("Index");
            }

            return View(new BagContentsViewModel());
        }
    }
}