using System;

namespace LetsCode.Resistance.Domain
{
    public class TradeException : Exception
    {
        public TradeException()
        {
        }

        public TradeException(string message)
            : base(message)
        {
        }

        public TradeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}