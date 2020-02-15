using DAL;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ParserService
{
    public class ParserService
    {
        /// <summary>
        /// Processes file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>validation messages if any</returns>
        public List<string> ProcessFile(string fileName)
        {
            var parser = GetParser(fileName);

            if (parser == null)
            {
                return new List<string>()
                {
                    "Unknown format"
                };
            }

            var transactions = parser.Parse();
            
            if (parser.ValidationMessages.Count == 0)
            {
                string connString = ConfigurationManager.AppSettings["connectionString"];

                new DataService(connString).Add(transactions);
            }

            return parser.ValidationMessages;
        }

        private Parser GetParser(string fileName)
        {
            Parser result = null;
            var extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".csv":
                    result = new CsvParser(fileName);
                    break;
                case ".xml":
                    result = new XmlParser(fileName);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
