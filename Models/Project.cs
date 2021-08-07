using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Models
{
    public class Project
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Start Date")]
        public DateTimeOffset StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTimeOffset EndDate { get; set; }

        [DisplayName("Priority")]
        public int? ProjectPriorityId {get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        //[MaxFileSize (1024 * 1024)]
        //[AllowedExtensions(new string[] {".jbg", ".png"})]

        public IFormFile ImageFormFile { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        public byte[] ImageFileData { get; set; }

        [DisplayName("File Extention")]
        public string ImageContentType { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }

        
        //Navigation Properties
        public virtual Company Company { get; set; }

        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();

        public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();





    }
}
