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
            if (ModelState.IsValid)
            {
                if (ticketProvider.TicketIsForOurEvent(model.OrderNumber))
                {
                    ControllerContext.HttpContext.Session["ValidatedTicket"] = true;
                    return RedirectToAction("Contents");
                }
                model.ErrorMessage = "Sorry, this is not a valid order for DDD East Anglia";
            }
            if (string.IsNullOrEmpty(model.OrderNumber))
            {
                model.ErrorMessage = "You must enter an order number";
            }
            return View("Index", model);
        }

        public ActionResult Contents()
        {
            if (ControllerContext.HttpContext.Session["ValidatedTicket"] == null || !(bool)ControllerContext.HttpContext.Session["ValidatedTicket"])
            {
                return RedirectToAction("Index");
            }

            return View(new BagContentsViewModel());
        }
    }
}