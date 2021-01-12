using System;
using System.Collections.Generic;
using System.Text;

namespace StormhammerLibrary.Models.Request
{
    public class CreateCharacterRequest
    {
        public long OwnerId { get; set; }
        public string Name { get; set; }
        public long MobClass { get; set; }
        public long MobRace { get; set; }
    }
}
