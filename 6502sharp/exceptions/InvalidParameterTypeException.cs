using System;

namespace _6502sharp
{
    public class InvalidParameterTypeException : Exception
    {
        public InvalidParameterTypeException() { }


        public InvalidParameterTypeException(string message)
            : base(message)
        {
        }

        public InvalidParameterTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
