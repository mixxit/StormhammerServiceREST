using StormhammerLibrary.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models
{
    public class NetworkMessageEvent
    {

        public NetworkMessageEvent(string handlerName, string payload)
        {
            this.HandlerName = HandlerName;
            this.Payload = payload;
        }

        public string HandlerName { get; set; }
        public string User { get; set; }
        public string Payload { get; set; }
    }
}
