using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectAdmin.Client.RequestOptions
{
	/// <summary>
	/// Create user options.
	/// </summary>
    public class CreateUserOptions
    {
		/// <summary>
		/// The Username of the new user.
		/// </summary>
		/// <remarks>Make sure it is between 4 and 10 characters.</remarks>
        public string Username { get; set; }
		/// <summary>
		/// The emailaddress of the new user
		/// </summary>
		/// <remarks>Used for password recovery and directadmin notifications.</remarks>
        public string Email { get; set; }
		/// <summary>
		/// The Password for the new user.
		/// </summary>
		/// <remarks>Make sure it complies to the password requirements! Used for FTP/DirectAdmin/Mysql.</remarks>
        public string Password { get; set; }
		/// <summary>
		/// The (default) domain for this user.
		/// </summary>
		/// <remarks>Make sure it doesn't voilate the check owner settings. And this should be not existing on the server.</remarks>
        public string Domain { get; set; }
		/// <summary>
		/// The name of the Reseller package the user is entitled to.
		/// </summary>
		/// <remarks>You should create this in the reseller panel.</remarks>
        public string PackageName { get; set; }
		/// <summary>
		/// The default ipaddress for this user.
		/// </summary>
		/// <remarks>Could be a shared address.</remarks>
        public string DefaultIpAddress { get; set; }
		/// <summary>
		/// Should the user be notified of the account creation?
		/// </summary>
		/// <value><c>true</c> if notify user; otherwise, <c>false</c>.</value>
        public bool NotifyUser { get; set; }
		/// <summary>
		/// The Username of an other admin/reseller.
		/// </summary>
		/// <value>The username owner.</value>
		/// <remarks>Use if you want to place this new user under an other admin/reseller then yourself.</remarks>
        public string UsernameOwner { get; set; }
        internal List<KeyValuePair<string, string>> GetRequestData()
        {
            var r = new List<KeyValuePair<string, string>>();
            r.Add(new KeyValuePair<string, string>("action", "create"));
            r.Add(new KeyValuePair<string, string>("add", "Submit"));
            r.Add(new KeyValuePair<string, string>("username", Username));
            r.Add(new KeyValuePair<string, string>("email", Email));
            r.Add(new KeyValuePair<string, string>("passwd", Password));
            r.Add(new KeyValuePair<string, string>("passwd2", Password));
            r.Add(new KeyValuePair<string, string>("domain", Domain));
            r.Add(new KeyValuePair<string, string>("package", PackageName));
            r.Add(new KeyValuePair<string, string>("ip", DefaultIpAddress));
            r.Add(new KeyValuePair<string, string>("notify", NotifyUser ? "yes" : "no"));
            if (!String.IsNullOrEmpty(UsernameOwner))
                r.Add(new KeyValuePair<string, string>("reseller", UsernameOwner));
            return r;
        }

    }
}
