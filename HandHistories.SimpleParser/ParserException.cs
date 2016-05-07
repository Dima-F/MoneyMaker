using System;
using System.Runtime.Serialization;

namespace HandHistories.SimpleParser
{
    [Serializable]
    public class ParserException:ApplicationException
    {
        public DateTime ErrorTime { get; set; }
        
        public ParserException() {}
        public ParserException(string message) : base(message) { }
        public ParserException(string message, Exception innerException) : base(message,innerException) { }
        public ParserException(SerializationInfo info, StreamingContext context) : base(info,context) { }

        public ParserException(string message, DateTime time) : base(message)
        {
            ErrorTime = time;
        }
    }
}
