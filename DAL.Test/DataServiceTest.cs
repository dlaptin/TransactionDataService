using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DAL.Test
{
    [TestClass]
    public class DataServiceTest
    {
        // note: use test db
        private const string connString = @"Data Source=MEGREZ;Initial Catalog=TransactionDB;User ID=sa;Password=123asdQ!;
Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static DateTime dt1 = new DateTime(2019, 02, 20, 12, 33, 16);

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            CleanDb();

            var service = new DataService(connString);

            service.Add(new List<Transaction>()
            {
                new Transaction()
                {
                    Id = "foo1",
                    Amount = 11,
                    Code = "USD",
                    Date = dt1,
                    Status = 0
                },
                new Transaction()
                {
                    Id = "foo2",
                    Amount = 22,
                    Code = "EUR",
                    Date = dt1.AddDays(-10),
                    Status = 1
                },
                new Transaction()
                {
                    Id = "foo3",
                    Amount = 33,
                    Code = "USD",
                    Date = dt1.AddDays(10),
                    Status = 2
                }
            });
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            CleanDb();
        }

        [TestMethod]
        public void GetByCode_Success()
        {
            var service = new DataService(connString);

            var resultUsd = service.Get("USD");
            var resultEur = service.Get("EUR");
            var resultCad = service.Get("CAD");

            Assert.AreEqual(2, resultUsd.Count);
            Assert.AreEqual(1, resultEur.Count);
            Assert.AreEqual(0, resultCad.Count);
        }

        [TestMethod]
        public void GetByDateRange_Success()
        {
            var service = new DataService(connString);

            var result1 = service.Get(dt1.AddDays(-1), dt1.AddDays(1));
            var result2 = service.Get(dt1.AddDays(-11), dt1.AddDays(11));

            Assert.AreEqual(1, result1.Count);
            Assert.AreEqual(3, result2.Count);
        }

        [TestMethod]
        public void GetByStatus_Success()
        {
            var service = new DataService(connString);

            var result1 = service.Get(0);
            var result2 = service.Get(1);

            Assert.AreEqual(1, result1.Count);
            Assert.AreEqual(1, result2.Count);
        }

        private static void CleanDb()
        {
            var db = new TransactionDB(connString);

            db.ExecuteCommand("DELETE FROM [TransactionDB].[dbo].[Transaction]");
        }
    }
}
