using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TakeAwayTech.Classes;
using TakeAwayTech.Controllers;

namespace TakeAwayTech.Tests
{
    [TestClass]
    public class AmountControllerTest
    {
        [TestMethod]
        public void Get_Should_Return_Valid_Amount_In_Words()
        {
            var mock = Substitute.For<IAmountParser>();

            mock.GetAmountInWords(435.45).Returns("four hundred and thirty-five dollars and fourty-five cents");

            var controller = new AmountController(mock);
            var result = controller.Get(435.45);
            Assert.AreEqual(result, "four hundred and thirty-five dollars and fourty-five cents");
        }
    }
}
