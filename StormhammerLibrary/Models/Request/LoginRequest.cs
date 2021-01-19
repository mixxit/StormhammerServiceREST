using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Request
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
