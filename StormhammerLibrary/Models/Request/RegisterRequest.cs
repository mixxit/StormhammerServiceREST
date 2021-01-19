using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Request
{
    public class RegisterRequest
    {
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
    }
}
