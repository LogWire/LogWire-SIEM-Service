using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWire.SIEM.Service.Messages
{
    public class AgentAuthMessage
    {
        public string EventType { get; set; }
        public string MachineName { get; set; }
        public string MachineIp { get; set; }
        public string Username { get; set; }
        public string EventDate { get; set; }
        
    }
}
