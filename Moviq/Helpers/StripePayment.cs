using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

using Newtonsoft.Json;

namespace Moviq.Api
{
    public class StripePayment
    {
        
        static readonly string api_endpoint = "https://api.stripe.com/v1";
        static readonly string subscription_path = "{0}/customers/{1}/subscription";
        static readonly string user_agent = "Stripe .NET v1";
        static readonly Encoding encoding = Encoding.UTF8;
        ICredentials credential;

        public StripePayment(string api_key)
        {
            credential = new NetworkCredential(api_key, "");
            TimeoutSeconds = 80;
        }
        
        protected virtual WebRequest SetupRequest(string method, string url)
        {
            WebRequest req = (WebRequest)WebRequest.Create(url);
            req.Method = method;
            if (req is HttpWebRequest)
            {
                ((HttpWebRequest)req).UserAgent = user_agent;
            }
            req.Credentials = credential;
            req.PreAuthenticate = true;
            req.Timeout = TimeoutSeconds * 1000;
            if (method == "POST")
                req.ContentType = "application/x-www-form-urlencoded";
            return req;
        }


        public bool Charge(int amount_cents, string currency, string token, string description)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            return Charge(amount_cents, currency, null, token, description);
        }

        bool Charge(int amount_cents, string currency, string customer, string token, string description)
        {
            if (amount_cents < 0)
                throw new ArgumentOutOfRangeException("amount_cents", "Must be greater than or equal 0");
            if (String.IsNullOrEmpty(currency))
                throw new ArgumentNullException("currency");
            if (currency != "usd")
                throw new ArgumentException("The only supported currency is 'usd'");

            StringBuilder str = new StringBuilder();
            str.AppendFormat("amount={0}&", amount_cents);
            str.AppendFormat("currency={0}&", currency);
            str.AppendFormat("card={0}&", token);
            if (!String.IsNullOrEmpty(description))
            {
                str.AppendFormat("description={0}&", HttpUtility.UrlEncode(description));
            }

            string ep = String.Format("{0}/charges", api_endpoint);
            return DoRequest<StripeCharge>(ep, "POST", str.ToString());
        }

        protected virtual bool DoRequest<T>(string endpoint, string method = "GET", string body = null)
        {
            string json = DoRequest(endpoint, method, body);
            if (json != null) { 
                int startIndex = json.IndexOf("paid");
                int endIndex = json.IndexOf(",\n", startIndex);
                string paidInfo = json.Substring(startIndex+7, endIndex-startIndex-7);
                if (paidInfo.Contains("true"))
                    return true;
                else
                    return false;
            }
            return false;
//            return JsonConvert.DeserializeObject<T>(json);
        }

        protected virtual string DoRequest(string endpoint, string method, string body)
        {
            string result = null;
            WebRequest req = SetupRequest(method, endpoint);
            if (body != null)
            {
                byte[] bytes = encoding.GetBytes(body.ToString());
                req.ContentLength = bytes.Length;
                using (Stream st = req.GetRequestStream())
                {
                    st.Write(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (WebResponse resp = (WebResponse)req.GetResponse())
                {
                    result = GetResponseAsString(resp);
                }
            }
            catch (WebException wexc)
            {
                if (wexc.Response != null)
                {
                    string json_error = GetResponseAsString(wexc.Response);
                    HttpStatusCode status_code = HttpStatusCode.BadRequest;
                    HttpWebResponse resp = wexc.Response as HttpWebResponse;
                    if (resp != null)
                        status_code = resp.StatusCode;

                    return null;
                }
                throw;
            }
            return result;
        }

        static string GetResponseAsString(WebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public int TimeoutSeconds { get; set; }
    }
}