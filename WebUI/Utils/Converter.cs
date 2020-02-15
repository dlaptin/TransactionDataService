using Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models;

namespace WebUI.Utils
{
    public static class Converter
    {
        public static string ToUIString(this TransactionDto dto)
        {
            return new Transaction
            {
                Id = dto.Id,
                Payment = $"{dto.Amount.ToString()} {dto.Code}",
                Status = (Status)dto.Status
            }.ToString();
        }
    }
}