﻿using System.Web.Mvc;
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
            return View("Index");
        }

        [HttpPost]
        public ActionResult Index(BagIndexViewModel model)
        {
            if (ticketProvider.TicketIsForOurEvent(model.OrderNumber))
            {
                return RedirectToAction("Contents");
            }
            return View("Index");
        }

        public ActionResult Contents()
        {
            return View();
        }
    }
}