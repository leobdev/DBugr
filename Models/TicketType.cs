using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Models
{
    public class TicketType
    {
        //Primary key
        public int Id { get; set; }

        [DisplayName("Ticket Type")]
        public string Name { get; set; }
    }
}
