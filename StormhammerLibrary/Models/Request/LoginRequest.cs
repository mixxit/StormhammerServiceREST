using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Request
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UniqueId { get; set; }
    }
}
