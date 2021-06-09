using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Models.ViewModels
{
    public class AssignDeveloperViewModel
    {
        public SelectList Developer { get; set; }

        public string  DeveloperId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
