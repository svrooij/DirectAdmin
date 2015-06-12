using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DirectAdmin.Client
{
    public class DirectAdminClient : IDisposable
    {
        DirectAdminClientOptions options;
        HttpClient client;
        public DirectAdminClient(DirectAdminClientOptions options)
        {
            this.options = options;
            this.client = options.CreateHttpClient();
        }

        #region Url constants
        const string SetPasswordUrl = "CMD_API_USER_PASSWD";
        const string CreateUserUrl = "CMD_API_ACCOUNT_USER";
        #endregion

        #region Client methodes
        /// <summary>
        /// Reset a password of a certain user.
        /// </summary>
        /// <param name="username">Username of the user (you'll have to be an admin or a reseller)</param>
        /// <param name="password">The new password, make sure it meets the directadmin password requirements</param>
        public async Task ResetPasswordForUser(string username, string password)
        {
            var data = new List<KeyValuePair<string, string>>();
            data.Add(new KeyValuePair<string, string>("username", username));
            data.Add(new KeyValuePair<string, string>("passwd", password));
            data.Add(new KeyValuePair<string, string>("passwd2", password));

            var response = await client.PostAsync(SetPasswordUrl, new FormUrlEncodedContent(data));
            await CheckResponse(response);
        }
        /// <summary>
        /// Create a new hosting user
        /// </summary>
        /// <param name="userOptions">Request object with all the user options</param>
        public async Task CreateUser(RequestOptions.CreateUserOptions userOptions)
        {
            var response = await client.PostAsync(CreateUserUrl, new FormUrlEncodedContent(userOptions.GetRequestData()));
            await CheckResponse(response);
        }
        #endregion



        private async Task CheckResponse(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var responseCollection = System.Web.HttpUtility.ParseQueryString(responseString);
            if (responseCollection.AllKeys.Contains("error") && Convert.ToInt32(responseCollection["error"]) == 0)
                return;
            else
                throw new DirectAdminClientException(responseCollection);
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
            
        }
    }
}
