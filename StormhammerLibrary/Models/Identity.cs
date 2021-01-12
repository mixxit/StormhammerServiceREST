using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models
{
    public class Identity
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SessionId { get; set; }
    }
}
