using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StormhammerLibrary.Models
{
    public class Account
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public Guid ObjectId { get; set; }
    }
}
