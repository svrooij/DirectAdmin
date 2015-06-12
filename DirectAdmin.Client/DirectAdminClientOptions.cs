using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DirectAdmin.Client
{
    public class DirectAdminClientOptions
    {
        public String ServerName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public Int32 Port { get; set; }
        public Boolean UseHttps { get; set; }

        internal HttpClient CreateHttpClient()
        {
            var client = new HttpClient(new HttpClientHandler());
            string credentials = String.Format("{0}:{1}", Username, Password);
            byte[] bytes = Encoding.ASCII.GetBytes(credentials);
            string base64 = Convert.ToBase64String(bytes);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("basic", base64);

            var uriBuilder = new UriBuilder(UseHttps ? "https" : "http", ServerName, Port);

            client.BaseAddress = uriBuilder.Uri;

            return client;
        }

    }
}
