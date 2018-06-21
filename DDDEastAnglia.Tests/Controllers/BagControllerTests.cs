using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DDDEastAnglia.Controllers;
using DDDEastAnglia.Helpers;
using DDDEastAnglia.Models;
using NSubstitute;
using NUnit.Framework;

namespace DDDEastAnglia.Tests.Controllers
{
    [TestFixture]
    public class BagControllerTests
    {
        private ITicketProvider ticketProvider;
        private HttpContextBase contextBase;
        private HttpSessionStateBase sessionStateBase;

        [SetUp]
        public void Setup()
        {
            ticketProvider = Substitute.For<ITicketProvider>();
            contextBase = Substitute.For<HttpContextBase>();
            sessionStateBase = Substitute.For<HttpSessionStateBase>();
            sessionStateBase["ValidatedTicket"].Returns(false);
            contextBase.Session.Returns(sessionStateBase);
        }

        [Test]
        public void Order_Number_Not_For_DDDEA_Returns_Same_View()
        {
            ticketProvider.TicketIsForOurEvent("1234567890").Returns(false);
            BagController controller = new BagController(ticketProvider);
            ControllerContext controllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.ControllerContext = controllerContext;
            BagIndexViewModel model = new BagIndexViewModel {OrderNumber = "1234567890"};
            var result = controller.Index(model);
            Assert.That(((ViewResult)result).ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Order_Number_For_DDDEA_Returns_Contents_View()
        {
            ticketProvider.TicketIsForOurEvent("1234567890").Returns(true);
            BagController controller = new BagController(ticketProvider);
            ControllerContext controllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.ControllerContext = controllerContext;
            BagIndexViewModel model = new BagIndexViewModel { OrderNumber = "1234567890" };
            var result = controller.Index(model);
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }
        
        [Test]
        public void Contents_View_Returns_BagContents()
        {
            sessionStateBase["ValidatedTicket"].Returns(true);
            BagController controller = new BagController(ticketProvider);
            ControllerContext controllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.ControllerContext = controllerContext;
            ViewResult result = (ViewResult)controller.Contents();
            Assert.That(result.Model, Is.TypeOf<BagContentsViewModel>());
        }

        [Test]
        public void When_The_User_Has_Already_Validated_Their_Ticket_Pass_Them_To_The_Contents_Action()
        {
            sessionStateBase["ValidatedTicket"].Returns(true);
            BagController controller = new BagController(ticketProvider);
            ControllerContext controllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.ControllerContext = controllerContext;
            var result = controller.Index();
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void When_The_User_Has_Not_Validated_Their_Ticket_Return_Them_To_The_Index_View()
        {
            sessionStateBase["ValidatedTicket"].Returns(false);
            BagController controller = new BagController(ticketProvider);
            ControllerContext controllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.ControllerContext = controllerContext;
            var result = controller.Contents();
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Order_Number_For_DDDEA_Sets_The_SessionState()
        {
            ticketProvider.TicketIsForOurEvent("1234567890").Returns(true);
            BagController controller = new BagController(ticketProvider);
            ControllerContext controllerContext = new ControllerContext(contextBase, new RouteData(), controller);
            controller.ControllerContext = controllerContext;
            BagIndexViewModel model = new BagIndexViewModel { OrderNumber = "1234567890" };
            controller.Index(model);
            Assert.That(sessionStateBase["ValidatedTicket"], Is.True);
        }

    }
}