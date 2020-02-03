using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ParserService
{
    public class CsvParser : Parser
    {
        public CsvParser(string fileName) : base(fileName) { }

        public override IEnumerable<Transaction> Parse()
        {
            IsParseSuccess = true;

            using (var reader = new StreamReader(FileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<TransactionMap>();

                var result = new List<Transaction>();
                var isRecordBad = false;

                csv.Configuration.BadDataFound = context =>
                {
                    isRecordBad = true;
                    LogInvalidRecord(context.RawRecord);
                };
                while (csv.Read())
                {
                    if (!isRecordBad)
                    {
                        try
                        {
                            var record = csv.GetRecord<Transaction>();
                            result.Add(record);
                        }
                        catch (CsvHelperException ex)
                        {
                            LogInvalidRecord(csv.Context.RawRecord, ex);
                        }
                    }

                    isRecordBad = false;
                }

                return result;
            }
        }
    }
}
