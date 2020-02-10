using System;
using System.Collections.Generic;
using System.Data.Linq;
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

        public void Add(List<Transaction> items)
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
            catch(DuplicateKeyException)
            {
                // todo: discuss if we should expect duplicate Transaction Ids in data received
                // and if such records should be discarded or updated
            }
        }

        public List<Transaction> Get(string code)
        {
            var db = new TransactionDB(connectionString);

            return (from t in db.Transaction
                    where t.Code.Equals(code)
                    select t).ToList();
        }

        public List<Transaction> Get(DateTime start, DateTime end)
        {
            var db = new TransactionDB(connectionString);

            return (from t in db.Transaction
                    where t.Date >= start.Date && t.Date <= end.Date
                    select t).ToList();
        }

        public List<Transaction> Get(int status)
        {
            var db = new TransactionDB(connectionString);

            return (from t in db.Transaction
                    where t.Status == status
                    select t).ToList();
        }
    }
}
