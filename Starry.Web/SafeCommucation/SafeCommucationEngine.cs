using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Starry.Web.SafeCommucation
{
    public class SafeCommucationEngine : ISafeCommucationEngine
    {
        private JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();
        public virtual int EngineVersion { get { return 0; } }
        public virtual string EngineName { get { return "None"; } }

        public virtual T GetResponse<T>(ISafeWebRequest request)
        {
            return this._javaScriptSerializer.Deserialize<T>(this.GetResponse(request));
        }

        public virtual string GetResponse(ISafeWebRequest request)
        {
            HttpWebRequest httpWebRequest = null;
            switch (request.Method)
            {
                case RequestMethod.GET: httpWebRequest = this.CreateRequestGET(request); break;
                case RequestMethod.POST: httpWebRequest = this.CreateRequestPOST(request); break;
            }
            httpWebRequest.Method = request.Method.ToString();
            httpWebRequest.Headers["EngineName"] = this.EngineName;
            httpWebRequest.Headers["EngineVersion"] = this.EngineVersion.ToString();

            var response = httpWebRequest.GetResponse();
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                return responseStream.ReadToEnd();
            }
        }

        private HttpWebRequest CreateRequestGET(ISafeWebRequest request)
        {
            var url = request.Url;
            if (request.Parameters != null)
            {
                var kvList = new List<string>();
                foreach (var propertyInfo in request.Parameters.GetType().GetProperties())
                {
                    var value = propertyInfo.GetValue(request.Parameters, null);
                    var param = string.Empty;
                    if (value != null)
                    {
                        param = HttpUtility.HtmlEncode(value.ToString());
                    }
                    kvList.Add(string.Format("{0}={1}", propertyInfo.Name, param));
                }
                if (kvList.Any())
                {
                    url += "?" + string.Join("&", kvList.ToArray());
                }
            }
            return HttpWebRequest.Create(url) as HttpWebRequest;
        }

        private HttpWebRequest CreateRequestPOST(ISafeWebRequest request)
        {
            var webRequest = HttpWebRequest.Create(request.Url) as HttpWebRequest;
            if (request.Parameters != null)
            {
                var objParameters = this._javaScriptSerializer.Serialize(request.Parameters);
                var bytes = Encoding.UTF8.GetBytes(objParameters);
                using (var requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
            }
            return webRequest;
        }

        public T GetEntity<T>(HttpRequest request)
        {
            var engineName = request.Headers["EngineName"];
            var engineVersion = request.Headers["EngineVersion"];
            var method = (request.HttpMethod ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException("Http Method must be set as POST or GET");
            }
            // TODO
            throw new NotImplementedException();
        }
    }
}
