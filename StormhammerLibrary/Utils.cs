using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary
{
    public class Utils
    {
        public static T Convert<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
