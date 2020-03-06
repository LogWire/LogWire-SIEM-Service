using System;
using System.ComponentModel.DataAnnotations;

namespace LogWire.SIEM.Service.Data.Model
{
    public class SIEMUserEntry
    {

        public Guid Id { get; set; }

        [Key]
        public string Username { get; set; }

    }
}
