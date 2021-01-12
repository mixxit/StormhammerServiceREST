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
        public string OwnerId { get; set; }
    }
}
