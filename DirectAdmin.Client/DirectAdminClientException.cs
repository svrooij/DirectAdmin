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
           foreach(var key in responseCollection.AllKeys){
               Data.Add(key, responseCollection[key]);
           }
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="DirectAdmin.Client.DirectAdminClientException"/> class.
		/// </summary>
		/// <param name="message">Message.</param>
        public DirectAdminClientException(string message) : base(message) { }
    }
}
