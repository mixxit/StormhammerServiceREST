using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StormhammerLibrary.Models
{
    public class MobClass
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
