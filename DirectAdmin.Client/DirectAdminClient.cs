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
        const string ShowAllUsersUrl = "CMD_API_SHOW_ALL_USERS";
        #endregion

        #region Client methodes
        /// <summary>
        /// Reset a password of a certain user.
        /// </summary>
		/// <remarks>Make sure you can access CMD_API_USER_PASSWD http://www.directadmin.com/features.php?id=736 </remarks>
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
		/// <remarks>Make sure you can access CMD_API_ACCOUNT_USER</remarks>
        /// <param name="userOptions">Request object with all the user options</param>
        public async Task CreateUser(RequestOptions.CreateUserOptions userOptions)
        {
            var response = await client.PostAsync(CreateUserUrl, new FormUrlEncodedContent(userOptions.GetRequestData()));
            await CheckResponse(response);
        }
        /// <summary>
        /// Fetch a list of all users
		/// </summary>
		/// <remarks>Make sure you can access CMD_API_SHOW_ALL_USERS</remarks>
        /// <returns>List of usernames</returns>
        public async Task<List<string>> ListUsers()
        {
            var result = new List<string>();
            var responseString = await client.GetStringAsync(ShowAllUsersUrl);
            // responseString looks like list[]=user1&list[]=user2

            if (responseString.ToLower().Contains("html"))
                throw new DirectAdminClientException("Wrong password or not enough rights");

            foreach (var item in responseString.Split('&'))
            {
                var splitted = item.Split('=');
                if (splitted.Length == 2)
                    result.Add(splitted[1]);
            }

            return result;
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
			options = null;
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
            
        }
    }
}
