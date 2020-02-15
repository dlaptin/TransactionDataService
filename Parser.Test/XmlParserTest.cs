using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserService;

namespace Parser.Test
{
    [TestClass]
    public class XmlParserTest
    {
        [TestMethod]
        public void ValidData_ParseSuccessfull()
        {
            var parser = new XmlParser("..\\..\\TestData\\Transactions.xml");

            var items = parser.Parse();
            var item = items.First();
            var date = new DateTime(2019, 01, 23, 1, 45, 10);
            
            Assert.AreEqual("Inv00001", item.Id);
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(200, item.Amount);
            Assert.AreEqual("USD", item.Code);
            Assert.AreEqual(2, item.Status);
        }

        [TestMethod]
        public void InvalidDateFormat_ParseUnsuccessfull()
        {
            var parser = new XmlParser("..\\..\\TestData\\InvalidDateFormat.xml");

            parser.Parse();

            Assert.IsTrue(parser.ValidationMessages.Count > 0);
        }
    }
}
