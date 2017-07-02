using Microsoft.VisualStudio.TestTools.UnitTesting;
using TakeAwayTech.Classes;

namespace TakeAwayTech.Tests
{
    [TestClass]
    public class AmountParserTests
    {
       
        [TestMethod]
        public void GetAmountInWordsTest_Should_Return_Valid_Amount_In_Words()
        {
            var amountParser = new AmountParser();
            var result = amountParser.GetAmountInWords(5523345.67);
            Assert.AreEqual(result, "FIVE MILLION AND FIVE TWENTY-THREE THOUSAND AND THREE HUNDRED AND FORTY-FIVE DOLLARS AND SIXTY-SEVEN CENTS");

        }

        [TestMethod]
        public void GetAmountInWordsTest_Should_Return_Valid_Amount_In_Words_When_Only_Digits()
        {
            var amountParser = new AmountParser();
            var result = amountParser.GetAmountInWords(11);
            Assert.AreEqual(result, "ELEVEN DOLLARS");

        }

        [TestMethod]
        public void GetAmountInWordsTest_Should_Return_Valid_Amount_In_Words_When_Only_Decimal()
        {
            var amountParser = new AmountParser();
            var result = amountParser.GetAmountInWords(.22);
            Assert.AreEqual(result, "TWENTY-TWO CENTS");

        }
    }
}