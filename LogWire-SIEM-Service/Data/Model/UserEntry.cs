using System;
using System.ComponentModel.DataAnnotations;

namespace LogWire.SIEM.Service.Data.Model
{
    public class UserEntry
    {

        [Key]
        public string Username { get; set; }
        public Guid Id { get; set; }

    }
}
