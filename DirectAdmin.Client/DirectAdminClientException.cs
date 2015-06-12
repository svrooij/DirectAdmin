using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectAdmin.Client
{
    public class DirectAdminClientException : Exception
    {
        public DirectAdminClientException(System.Collections.Specialized.NameValueCollection responseCollection)
            : base(responseCollection.AllKeys.Contains("text") ? responseCollection["text"] : "Unknown error")
        {
           foreach(var key in responseCollection.AllKeys){
               Data.Add(key, responseCollection[key]);
           }
        }

        public DirectAdminClientException(string message) : base(message) { }
    }
}
