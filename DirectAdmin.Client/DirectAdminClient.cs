using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DirectAdmin.Client
{
	/// <summary>
	/// Direct admin client.
	/// </summary>
    public class DirectAdminClient : IDisposable
    {
        DirectAdminClientOptions options;
        HttpClient client;
		/// <summary>
		/// Initializes a new instance of the <see cref="DirectAdmin.Client.DirectAdminClient"/> class.
		/// </summary>
		/// <param name="options">Client options <see cref="DirectAdmin.Client.DirectAdminClientOptions"/></param>
        public DirectAdminClient(DirectAdminClientOptions options)
        {
            this.options = options;
            this.client = options.CreateHttpClient();
        }

        #region Url constants
        const string SetPasswordUrl = "CMD_API_USER_PASSWD";
        const string CreateUserUrl = "CMD_API_ACCOUNT_USER";
        const string ShowAllUsersUrl = "CMD_API_SHOW_ALL_USERS";
        const string SelectUsersUrl = "CMD_API_SELECT_USERS";
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
        /// Delete an user account
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <remarks>Only reseller/admin accounts can call this action</remarks>
        public async Task DeleteUser(string username)
        {
            var data = new List<KeyValuePair<string, string>>();
            data.Add(new KeyValuePair<string, string>("confirmed", "Confirm"));
            data.Add(new KeyValuePair<string, string>("delete", "yes"));
            data.Add(new KeyValuePair<string, string>("select0", username));

            var response = await client.PostAsync(SelectUsersUrl, new FormUrlEncodedContent(data));
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


		/// <summary>
		/// Checks the response.
		/// </summary>
		/// <param name="response">Response.</param>
		/// <remarks>The DirectAdmin API is not a really REST API. So we have to do some tricks to check for errors.</remarks>
        private async Task CheckResponse(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString == "list[]=not&list[]=available&list[]=in&list[]=the&list[]=demo" || responseString.Contains("That feature has been disabled for the demo"))
                throw new DirectAdminClientException("Not available in the demo") { NotInDemo = true };
            
            var responseCollection = System.Web.HttpUtility.ParseQueryString(responseString);
            if (responseCollection.AllKeys.Contains("error") && Convert.ToInt32(responseCollection["error"]) == 0)
                return;
            else
                throw new DirectAdminClientException(responseCollection);
        }

		/// <summary>
		/// Releases all resource used by the <see cref="DirectAdmin.Client.DirectAdminClient"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="DirectAdmin.Client.DirectAdminClient"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="DirectAdmin.Client.DirectAdminClient"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DirectAdmin.Client.DirectAdminClient"/> so the garbage collector can reclaim the memory that the
		/// <see cref="DirectAdmin.Client.DirectAdminClient"/> was occupying.</remarks>
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
