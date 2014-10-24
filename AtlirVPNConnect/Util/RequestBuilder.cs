using System;
using System.IO;
using System.Net;
using System.Text;

namespace AtlirVPNConnect.Util
{
    public class RequestBuilder
    {
        public HttpWebRequest Req;
        public CookieContainer Container = new CookieContainer();
        private const string BaseUrl = "https://atlir.org/push/";

        private void Reset(string url)
        {
            Req = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            Req.CookieContainer = Container;
            Req.Method = "GET";
            Req.Accept = "*/*";
            Req.UserAgent = "Atlir Push Client";
            Req.ContentType = "application/json";
            Req.Timeout = 10000;
        }

        public string Post(string url, string data)
        {
            Reset(url);
            Req.Method = "POST";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            Req.ContentLength = buffer.Length;
            Stream stream = Req.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            return GetString();
        }

        public string Get(string url)
        {
            Reset(url);
            return GetString();
        }

        public string Get(string url, string data)
        {
            Reset(url + "?" + data);
            return GetString();
        }

        private string GetString()
        {
            try
            {
                HttpWebResponse res = (HttpWebResponse)Req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                string s = sr.ReadToEnd();
                res.Close();
                return s;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
