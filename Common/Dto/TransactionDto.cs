using System;

namespace Common.Dto
{
    public class TransactionDto
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string Code { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }
    }
}
