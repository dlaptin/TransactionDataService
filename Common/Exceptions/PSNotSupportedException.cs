using System;

namespace Common.Exceptions
{
    public class PSNotSupportedException : Exception
    {
        public PSNotSupportedException()
        {
        }

        public PSNotSupportedException(string message)
            : base(message)
        {
        }

        public PSNotSupportedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
