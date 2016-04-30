using System;

namespace RdfMapperNet.Exceptions
{
    public class RdfMapperException : Exception
    {
        public RdfMapperException()
        {
        }

        public RdfMapperException(string message) : base(message)
        {
        }
    }
}
