using System.Web.Mvc;
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

        [SetUp]
        public void Setup()
        {
            ticketProvider = Substitute.For<ITicketProvider>();
        }

        [Test]
        public void Order_Number_Not_For_DDDEA_Returns_Same_View()
        {
            ticketProvider.TicketIsForOurEvent("1234567890").Returns(false);
            BagController controller = new BagController(ticketProvider);
            BagIndexViewModel model = new BagIndexViewModel {OrderNumber = "1234567890"};
            var result = controller.Index(model);
            Assert.That(((ViewResult)result).ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Order_Number_For_DDDEA_Returns_Contents_View()
        {
            ticketProvider.TicketIsForOurEvent("1234567890").Returns(true);
            BagController controller = new BagController(ticketProvider);
            BagIndexViewModel model = new BagIndexViewModel { OrderNumber = "1234567890" };
            var result = controller.Index(model);
            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }
    }
}