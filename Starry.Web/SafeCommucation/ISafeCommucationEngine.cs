using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Starry.Web.SafeCommucation
{
    public interface ISafeCommucationEngine
    {
        int EngineVersion { get; }
        string EngineName { get; }

        string GetResponse(ISafeWebRequest request);
        T GetResponse<T>(ISafeWebRequest request);

        T GetEntity<T>(HttpRequest request);
    }
}
