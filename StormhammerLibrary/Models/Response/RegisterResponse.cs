using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Response
{
    public class RegisterResponse
    {
        public bool succeeded { get; set; }
        public List<string> errors { get; set; }

    }
}
