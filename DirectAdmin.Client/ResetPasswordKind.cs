using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectAdmin.Client
{
    /// <summary>
    /// Use this to specify what kind of password need to be reset.
    /// </summary>
   
    [Flags]
    public enum ResetPasswordKind
    {
        /// <summary>Default value, will not work on newer versions of DA</summary>
        NotSpecified = 0,
        /// <summary>Reset everything</summary>
        All = 2 & 4 & 8,
        /// <summary>DirectAdmin account</summary>
        System = 2,
        /// <summary>FTP account</summary>
        Ftp = 4,
        /// <summary>Mysql database account</summary>
        Database = 8
    }
}
