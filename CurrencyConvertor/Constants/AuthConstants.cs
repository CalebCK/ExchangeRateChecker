using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCMSAPI.Constants
{
    public static class AuthConstants
    {
        //in minutes
        public const int TokenLifeSpan = 60;
        public const string TokenProvider = "Default";
        public const string TokenPurpose = "RefreshToken";
    }
}
