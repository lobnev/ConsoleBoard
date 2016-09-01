using System;

namespace ConsoleBoard.Exceptions
{
    public class DrawException : Exception
    {
        public DrawException()
        {
        }
        public DrawException(string message) : base(message)
        {
        }
    }
}