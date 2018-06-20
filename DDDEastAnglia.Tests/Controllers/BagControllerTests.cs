using System.Web.Mvc;
using DDDEastAnglia.Controllers;
using DDDEastAnglia.Models;
using NUnit.Framework;

namespace DDDEastAnglia.Tests.Controllers
{

    [TestFixture]
    public class BagControllerTests
    {
        [Test]
        public void Order_Number_Not_For_DDDEA_Returns_Same_View()
        {
            BagController controller = new BagController();
            BagIndexViewModel model = new BagIndexViewModel();
            var result = controller.Index(model);
            Assert.That(((ViewResult)result).ViewName, Is.EqualTo("Index"));
        }
    }
}