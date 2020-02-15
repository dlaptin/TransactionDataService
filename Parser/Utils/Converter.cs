using Common.Dto;

namespace ParserService
{
    public static class Converter
    {
        public static TransactionDto ToDto(this CsvTransaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Code = transaction.Code,
                Date = transaction.Date,
                Status = transaction.Status
            };
        }

        public static TransactionDto ToDto(this TransactionsTransaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.id,
                Date = transaction.TransactionDate,
                Amount = transaction.PaymentDetails.Amount,
                Code = transaction.PaymentDetails.CurrencyCode,
                Status = (int)transaction.Status
            };
        }
    }
}
