using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Starry
{
    public class StarryException : Exception
    {
        public StarryException() : base() { }
        public StarryException(string message) : base(message) { }
        protected StarryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public StarryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
