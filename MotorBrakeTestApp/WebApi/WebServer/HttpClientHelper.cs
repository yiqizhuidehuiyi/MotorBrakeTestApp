using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.WebServer
{
    public static class HttpClientHelper
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(HttpClientHelper));
        /// <summary>
        /// 要连接的Uri
        /// </summary>
        public static string ClientUri { get; set; }
        /// <summary>
        /// 记录返回的Token
        /// </summary>
        public static string TokenValue { get; set; }

        static HttpClientHelper()
        {
            if (string.IsNullOrEmpty(ClientUri))
            {

                ClientUri = "http://81.70.205.37:8889";
                //我们的接口
                //产线上用的
                ClientUri = "http://10.9.112.106:9990/login";
                ClientUri = "http://10.9.112.106:9990";
                //我们的接口
                //测试上用的
                //string url = "http://10.9.24.18:9090/login";
            }
        }

        public static string HttpPost(string actionAndMethod, string body)
        {
            Encoding encoding = Encoding.UTF8;
            Uri myUri = new Uri(ClientUri + actionAndMethod);
            log.Debug($"HttpPost {ClientUri}{actionAndMethod}");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myUri);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("authorization", "Bearer " + TokenValue);
            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="actionAndMethod"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string HttpGet(string actionAndMethod, Dictionary<string, object> query = null)
        {
            var myUri = ClientUri + actionAndMethod;

            if (query != null)
            {
                var list = new List<string>();
                foreach (var item in query)
                {
                    list.Add($"{item.Key}={item.Value}");
                }
                myUri += "?" + string.Join("&", list);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(myUri);
            request.Method = "GET";
            request.ContentType = "application/json";

            request.Headers.Add("authorization", "Bearer " + TokenValue);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
