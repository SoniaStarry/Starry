using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace Starry.Net.Validatable
{
    public class ValidatableHttpWebRequest
    {
        public ValidatableHttpWebRequest(string requestUriString)
        {
            this.httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
        }

        public ValidatableHttpWebRequest(Uri requestUri)
        {
            this.httpWebRequest = WebRequest.Create(requestUri) as HttpWebRequest;
        }

        private HttpWebRequest httpWebRequest;

        public string GetResponse()
        {
            var response = this.httpWebRequest.GetResponse() as HttpWebResponse;
            using (var responseStream = new StreamReader(response.GetResponseStream()))
            {
                return responseStream.ReadToEnd();
            }
        }

        public TEntity GetResponse<TEntity>() where TEntity : ValidatableEntity
        {
            return new JavaScriptSerializer().Deserialize<TEntity>(this.GetResponse());
        }
    }
}
