using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Models.ViewModels
{
    public class ProjectMembersViewModel
    {
        public Project Projects { get; set; } = new();

        public MultiSelectList Users { get; set; } //populates list box

        public string[] SelectedUsers { get; set; } //receives selected users
    }
}
