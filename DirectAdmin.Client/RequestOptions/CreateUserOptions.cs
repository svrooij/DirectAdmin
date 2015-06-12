using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectAdmin.Client.RequestOptions
{
    public class CreateUserOptions
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string PackageName { get; set; }
        public string DefaultIpAddress { get; set; }
        public bool NotifyUser { get; set; }
        public string UsernameOwner { get; set; }
        public List<KeyValuePair<string, string>> GetRequestData()
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
