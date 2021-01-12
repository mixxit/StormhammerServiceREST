using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Request
{
    public class CreateCharacterRequest
    {
        public string Name { get; set; }
        public long MobClassId { get; set; }
        public long MobRaceId { get; set; }
    }
}
