using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Web.SafeCommucation
{
    public class SafeWebRequest : ISafeWebRequest
    {
        public SafeWebRequest(string url, RequestMethod method, object parameters)
        {
            this.Url = url;
            this.Method = method;
            this.Parameters = parameters;
        }

        public string Url { private set; get; }
        public RequestMethod Method { private set; get; }
        public object Parameters { private set; get; }
        public int TimeoutSeconds
        {
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("timeout seconds must greater than 0.");
                }
                this._timeoutSeconds = value;
            }
            get { return this._timeoutSeconds; }
        }
        private int _timeoutSeconds = 30;
    }
}
