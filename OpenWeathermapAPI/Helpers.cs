using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace OpenWeathermapAPI
{
    public class Helpers
    {

        public static Response SendRequest()
        {

            string uri = Const.uri;

            try
            {
                //DON'T REMOVE THE FOLLOWING COMMENTED CODE!!. It intended for debugging purposes via local proxy / sniffer tool
                //var httpClientHandler = new HttpClientHandler
                //{
                //    Proxy = new WebProxy("http://127.0.0.1:8081", false),
                //    UseProxy = true
                //};
                using (var httpClient = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    httpClient.Timeout = TimeSpan.FromMilliseconds(30000);
                    ServicePointManager.SecurityProtocol =
                        SecurityProtocolType.Tls12 |
                        SecurityProtocolType.Tls11 |
                        SecurityProtocolType.Tls;

                    var httpContent = new HttpRequestMessage(HttpMethod.Get,
                        new Uri(uri));
                    httpContent.Headers.Add("Accept", "application/json");

                    HttpResponseMessage response = httpClient.SendAsync(httpContent).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception();
                    }

                    Stream stream = response.Content.ReadAsStreamAsync().Result;
                    var sr = new StreamReader(stream);
                    var responseInfo = sr.ReadToEnd();
                    Response myojb = JsonConvert.DeserializeObject<Response>(responseInfo);
                    stream.Close();

                    return myojb;
                }
            }
            catch (WebException we)
            {
                using (WebResponse webResponse = we.Response)
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)webResponse;
                }
                throw;
            }
        }
    }
}
