using CsvHelper.Configuration;
using System;
using System.Globalization;

namespace ParserService
{
    /// <summary>
    /// Maps and validates data.
    /// </summary>
    public sealed class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.Id).ConvertUsing(row =>
            {
                var value = row.GetField<string>(0);
                ValidateRequiredField(value);
                ValidateMaxLength(value, 50);

                return value;
            });
            Map(m => m.Amount).ConvertUsing(row =>
            {
                return decimal.Parse(row.GetField<string>(1), CultureInfo.InvariantCulture);
            });
            Map(m => m.Code).ConvertUsing(row =>
            {
                var value = row.GetField<string>(2);
                ValidateRequiredField(value);
                ValidateCode(value);

                return value;
            });
            Map(m => m.Date).ConvertUsing(row =>
            {
                return DateTime.ParseExact(row.GetField<string>(3), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            });
            Map(m => m.Status).ConvertUsing(row =>
            {
                var fieldValue = row.GetField<string>(4);

                if (!Enum.TryParse(fieldValue, out CsvStatus status))
                {
                    throw new ArgumentException("Invalid Status value");
                }
                
                return (int)status;
            }); ;
        }

        private void ValidateRequiredField(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Field is required");
        }

        private void ValidateMaxLength(string value, int maxLength)
        {
            if (value.Length > maxLength)
                throw new ArgumentException("Field is too long");
        }

        private void ValidateCode(string value)
        {
            if (!CurrencyCodes.Codes.Contains(value))
                throw new ArgumentException("Invalid Currency Code");
        }
    }
}
