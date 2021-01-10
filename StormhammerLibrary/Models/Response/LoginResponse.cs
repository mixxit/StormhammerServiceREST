using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Response
{
    public class LoginResponse
    {
        public string SessionId { get; set; }
        public bool LoggedIn { get; set; }
    }
}
