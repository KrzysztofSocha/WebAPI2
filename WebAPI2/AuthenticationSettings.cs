using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtEspireDays { get; set; }
    }
}
