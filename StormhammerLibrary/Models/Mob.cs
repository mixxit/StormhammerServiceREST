using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StormhammerLibrary.Models
{
    public class Mob
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public long MobRaceId { get; set; }
        public long MobClassId { get; set; }
        public long? AccountId { get; set; }
        public long ZoneId { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
