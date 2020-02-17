using Common.Dto;
using System;
using System.Collections.Generic;

namespace DAL
{
    public interface IDataService
    {
        void Add(List<TransactionDto> items);

        void Update(List<Transaction> items);

        List<TransactionDto> Get(string code);

        List<TransactionDto> Get(DateTime start, DateTime end);

        List<TransactionDto> Get(int status);
    }
}
