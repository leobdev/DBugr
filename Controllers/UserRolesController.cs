using DBugr.Data;
using DBugr.Models;
using DBugr.Models.Enums;
using DBugr.Models.ViewModels;
using DBugr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Controllers
{
    [Authorize(Roles="Admin")]
    public class UserRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTRolesService _rolesService;
        
        public UserRolesController(ApplicationDbContext context,
                                   UserManager<BTUser> userManager,
                                   IBTRolesService rolesService)
        {
            _context = context;
            //_userManager = userManager;
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles()
        {
            List<ManageUserRolesViewModel> member = new();

            //ToDo: Company Users
            List<BTUser> users = _context.Users.ToList();

            foreach(var user in users)
            {
                ManageUserRolesViewModel vm = new();
                vm.BTUser = user;
                var selected = await _rolesService.ListUserRolesAsync(user);
                vm.Roles = new MultiSelectList(_context.Roles, "Name", "Name", selected);
                member.Add(vm);
            }

            return View(member);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(ManageUserRolesViewModel member)
        {
            //var id = member.BTUser.Id;
            var selected = member.SelectedRoles;
            
            BTUser user = _context.Users.Find(member.BTUser.Id);
            //Get user's current roles
            IEnumerable<string> roles = await _rolesService.ListUserRolesAsync(user);
            //Remove teh current roles
            bool result = await _rolesService.RemoveUserFromRolesAsync(user,roles);
            if(!result) { throw new Exception("Didnt work"); }
            

            string userRole = selected.FirstOrDefault();

            if(Enum.TryParse(userRole, out Roles roleValue))
            {
                await _rolesService.AddUserToRoleAsync(user, userRole);
                return RedirectToAction("ManageUserRoles");
            }

            return RedirectToAction("ManageUserRoles");
        }
    }
}
