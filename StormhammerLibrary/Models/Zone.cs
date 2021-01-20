using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StormhammerLibrary.Models
{
    public class Zone
    {
        public long Id { get; set; }
        [StringLength(80)]
        public string Name { get; set; }
        public float SafeX { get; set; }
        public float SafeY { get; set; }
        public float SafeZ { get; set; }
    }
}
