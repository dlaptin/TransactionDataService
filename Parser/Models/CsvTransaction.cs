using CsvHelper.Configuration.Attributes;
using System;

namespace ParserService
{
    public class CsvTransaction
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public decimal Amount { get; set; }

        [Index(2)]
        public string Code { get; set; }

        [Index(3)]
        public DateTime Date { get; set; }

        [Index(4)]
        public int Status { get; set; }
    }
}
