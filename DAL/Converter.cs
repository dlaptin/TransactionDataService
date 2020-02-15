using Common.Dto;

namespace DAL
{
    public static class Converter
    {
        public static Transaction ToModel(this TransactionDto dto)
        {
            return new Transaction
            {
                Id = dto.Id,
                Amount = dto.Amount,
                Code = dto.Code,
                Date = dto.Date,
                Status = dto.Status
            };
        }

        public static TransactionDto ToDto(this Transaction transaction)
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
    }
}
