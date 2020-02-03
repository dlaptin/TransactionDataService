using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserService;

namespace Parser.Test
{
    [TestClass]
    public class CsvParserTest
    {
        [TestMethod]
        public void ValidData_ParseSuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\Transactions.csv");
            var date = new DateTime(2019, 02, 20, 12, 33, 16);

            var item = parser.Parse().First();
            
            Assert.IsTrue(parser.IsParseSuccess);
            Assert.AreEqual("Invoice0000001", item.Id);
            Assert.AreEqual(date, item.Date);
            Assert.AreEqual(1000, item.Amount);
            Assert.AreEqual("USD", item.Code);
            Assert.AreEqual(0, item.Status);
        }

        [TestMethod]
        public void AmountNotDecimal_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\AmountNotDecimal.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void ExtraSpaces_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\ExtraSpaces.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void InvalidDateFormat_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\InvalidDateFormat.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void InvalidStatus_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\InvalidStatus.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void ExtraData_ParseSuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\ExtraData.csv");

            parser.Parse();

            Assert.IsTrue(parser.IsParseSuccess);
        }

        [TestMethod]
        public void MissingFiled_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\MissingFiled.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void TextTooLong_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\TextTooLong.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void InvalidCode_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\InvalidCode.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }

        [TestMethod]
        public void EmptyField_ParseUnsuccessfull()
        {
            var parser = new CsvParser("..\\..\\TestData\\EmptyId.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);

            parser = new CsvParser("..\\..\\TestData\\EmptyAmount.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);

            parser = new CsvParser("..\\..\\TestData\\EmptyCode.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);

            parser = new CsvParser("..\\..\\TestData\\EmptyDate.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);

            parser = new CsvParser("..\\..\\TestData\\EmptyStatus.csv");

            parser.Parse();

            Assert.IsFalse(parser.IsParseSuccess);
        }
    }
}
