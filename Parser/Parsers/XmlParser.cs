using Common.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ParserService
{
    public class XmlParser : Parser
    {
        public XmlParser(string fileName) : base(fileName) { }

        public override List<TransactionDto> Parse()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Transactions));
            Transactions transactions = null;

            try
            {
                using (FileStream fs = File.OpenRead(FileName))
                {
                    transactions = (Transactions)serializer.Deserialize(fs);
                }
            }
            catch (InvalidOperationException ex)
            {
                // todo: implement validation for Transaction Id and Currency Code
                LogInvalidRecord(null, ex);
            }

            return transactions?.Transaction.Select(t => t.ToDto()).ToList();
        }
    }
}
