using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogWire.SIEM.Service.Data.Model
{

    [Table("events_auth")]
    public class AuthEventEntry
    {

        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MachineId { get; set; }
        public string EventType { get; set; }
        public DateTime EventTime { get; set; }

    }
}
