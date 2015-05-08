using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.SafeCommucation
{
    public interface ISafeWebRequest
    {
        string Url { get; }
        RequestMethod Method { get; }
        object Parameters { get; }
        int TimeoutSeconds { get; }
    }
}
