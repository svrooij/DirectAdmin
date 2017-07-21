using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectAdmin.Client
{
	/// <summary>
	/// Direct admin client exception.
	/// </summary>
    public class DirectAdminClientException : Exception
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="DirectAdmin.Client.DirectAdminClientException"/> class.
		/// </summary>
		/// <param name="responseCollection">Response collection.</param>
        public DirectAdminClientException(System.Collections.Specialized.NameValueCollection responseCollection)
            : base(responseCollection.AllKeys.Contains("text") ? responseCollection["text"] : "Unknown error")
        {
            if (responseCollection.Count > 0)
            {
                foreach (var key in responseCollection.AllKeys)
                {
                    var value = responseCollection[key];
                    if(!string.IsNullOrEmpty(key) && value != null)
                    Data.Add(key, value);
                }
            }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="DirectAdmin.Client.DirectAdminClientException"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
        public DirectAdminClientException(string message) : base(message) { }

        /// <summary>
        /// This property will be set, if the call is correct, but the feature may not be used with a demo account.
        /// </summary>
        public Boolean NotInDemo { get; set; }
    }
}
