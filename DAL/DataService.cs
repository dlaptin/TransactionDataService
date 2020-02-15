using Common.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DataService
    {
        private readonly string connectionString;

        public DataService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(List<TransactionDto> items)
        {
            Add(items.Select(t => t.ToModel()).ToList());
        }

        public void Update(List<Transaction> items)
        {
            throw new NotImplementedException();
        }

        public List<TransactionDto> Get(string code)
        {
            return GetInternal(code).Select(t => t.ToDto()).ToList();
        }

        public List<TransactionDto> Get(DateTime start, DateTime end)
        {
            return GetInternal(start, end).Select(t => t.ToDto()).ToList();
        }

        public List<TransactionDto> Get(int status)
        {
            return GetInternal(status).Select(t => t.ToDto()).ToList();
        }

        private void Add(List<Transaction> items)
        {
            var db = new TransactionDB(connectionString);

            foreach (var item in items)
            {
                db.Transaction.InsertOnSubmit(item);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (SqlException)
            {
                // todo: perform log
            }
        }

        private List<Transaction> GetInternal(string code)
        {
            var db = new TransactionDB(connectionString);

            return (from t in db.Transaction
                    where t.Code.Equals(code)
                    select t).ToList();
        }

        private List<Transaction> GetInternal(DateTime start, DateTime end)
        {
            var db = new TransactionDB(connectionString);

            return (from t in db.Transaction
                    where t.Date >= start.Date && t.Date <= end.Date
                    select t).ToList();
        }

        private List<Transaction> GetInternal(int status)
        {
            var db = new TransactionDB(connectionString);

            return (from t in db.Transaction
                    where t.Status == status
                    select t).ToList();
        }
    }
}
