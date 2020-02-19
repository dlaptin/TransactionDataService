using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using WebUI.Models;

namespace WebUI.Utils
{
    public static class Converter
    {
        public static string ToUIString(this List<TransactionDto> transactions)
        {
            var result = StringResources.NoResults;

            if (transactions.Count() > 0)
            {
                var items = transactions.Select(transaction => new Transaction
                {
                    Id = transaction.Id,
                    Payment = $"{transaction.Amount.ToString()} {transaction.Code}",
                    Status = (Status)transaction.Status
                }.ToString());

                result = string.Join(Environment.NewLine, items);
            }

            return result;
        }
    }
}