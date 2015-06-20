using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DirectAdmin.Client
{
	/// <summary>
	/// Direct admin client options.
	/// </summary>
    public class DirectAdminClientOptions
    {
		/// <summary>
		/// Gets or sets the name of the server.
		/// </summary>
		/// <value>The domainname of the directadmin server.</value>
        public String ServerName { get; set; }
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username, to connect to directadmin</value>
        public String Username { get; set; }
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <remarks>This can also be a more restricted API key, see http://help.directadmin.com/item.php?id=523</remarks>
		/// <value>The password, to connect to directadmin</value>
        public String Password { get; set; }
		/// <summary>
		/// Gets or sets the port, default port: 2222
		/// </summary>
		/// <value>The port, to connect to directadmin</value>
        public Int32 Port { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DirectAdmin.Client.DirectAdminClientOptions"/> use https.
		/// </summary>
		/// <value><c>true</c> if use https; otherwise, <c>false</c>.</value>
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
