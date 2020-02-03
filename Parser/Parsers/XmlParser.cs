using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ParserService
{
    public class XmlParser : Parser
    {
        public XmlParser(string fileName) : base(fileName) { }

        public override IEnumerable<Transaction> Parse()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Transactions));
            Transactions transactions;
            
            using (FileStream fs = File.OpenRead(FileName))
            {
                transactions = (Transactions)serializer.Deserialize(fs);
            }

            return from item in transactions.Transaction
                   select new Transaction
                   {
                       Id = item.id,
                       Date = item.TransactionDate,
                       Amount = item.PaymentDetails.Amount,
                       Code = item.PaymentDetails.CurrencyCode,
                       Status = (int)item.Status
                   };
        }
    }
}
