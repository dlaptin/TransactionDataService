using Common.Exceptions;
using System.IO;

namespace ParserService
{
    public class ParserService
    {
        public Parser GetParser(string fileName)
        {
            Parser result = null;
            var extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case "csv":
                    break;
                case "xml":
                    break;
                default:
                    throw new PSNotSupportedException("Unknown format");
            }

            return result;
        }
    }
}
