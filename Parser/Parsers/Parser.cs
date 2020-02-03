using System;
using System.Collections.Generic;

namespace ParserService
{
    public abstract class Parser
    {
        public bool IsParseSuccess { get; set; }

        public List<string> ValidationMessages { get; set; }

        protected string FileName { get; set; }

        public Parser(string fileName)
        {
            FileName = fileName;
            IsParseSuccess = true;
            ValidationMessages = new List<string>();
        }

        public abstract IEnumerable<Transaction> Parse();
        
        protected void LogInvalidRecord(string record, Exception ex = null)
        {
            if (ex != null)
            {
                var message = string.Empty;

                if (ex.InnerException != null)
                {
                    message = ex.InnerException.Message;                    
                }
                else
                {
                    message = ex.Message;
                }

                ValidationMessages.Add($"Error: {message}. Raw record: {record}");
            }

            IsParseSuccess = false;
            // todo: perform log
        }
    }
}
