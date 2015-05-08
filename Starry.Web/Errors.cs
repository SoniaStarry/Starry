using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web
{
    internal static class Errors
    {
        public static void ThrowRequestMethodException(string httpMethod)
        {
            if (httpMethod == null)
            {
                throw new RequestMethodException();
            }
        }
    }
}
