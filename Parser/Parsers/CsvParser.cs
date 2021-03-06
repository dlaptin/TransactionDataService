﻿using Common.Dto;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ParserService
{
    public class CsvParser : Parser
    {
        public CsvParser(string fileName) : base(fileName) { }

        public override List<TransactionDto> Parse()
        {
            IsParseSuccess = true;

            using (var reader = new StreamReader(FileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<TransactionMap>();

                var result = new List<CsvTransaction>();
                var isRecordBad = false;

                csv.Configuration.BadDataFound = context =>
                {
                    isRecordBad = true;
                    LogInvalidRecord(context.RawRecord.Trim());
                };
                while (csv.Read())
                {
                    if (!isRecordBad)
                    {
                        try
                        {
                            var record = csv.GetRecord<CsvTransaction>();
                            result.Add(record);
                        }
                        catch (CsvHelperException ex)
                        {
                            LogInvalidRecord(csv.Context.RawRecord.Trim(), ex);
                        }
                    }

                    isRecordBad = false;
                }

                return result.Select(t => t.ToDto()).ToList();
            }
        }
    }
}
