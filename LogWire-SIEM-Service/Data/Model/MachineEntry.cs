using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogWire.SIEM.Service.Data.Model
{
    public class MachineEntry
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}
