using DBugr.Data;
using DBugr.Models;
using DBugr.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Services
{
    public class BTRolesService : IBTRolesService
    {

        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTCompanyInfoService _infoService;

        public BTRolesService(ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<BTUser> userManager,
            IBTCompanyInfoService infoService)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _infoService = infoService;
        }

        public async Task<bool> AddUserToRoleAsync(BTUser user, string roleName)
        {
            bool result = (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
            return result;
        }

        public async Task<string> GetroleNameByIdAsync(string roleId)
        {
            IdentityRole role = _context.Roles.Find(roleId);
            string result = await _roleManager.GetRoleNameAsync(role);
            return await _roleManager.GetRoleNameAsync(role);
        }

        public async Task<bool> IsUserInRoleAsync(BTUser user, string roleName)
        {
            bool result = await _userManager.IsInRoleAsync(user, roleName);
            return result;
        }

        public async Task<IEnumerable<string>> ListUserRolesAsync(BTUser user)
        {
            IEnumerable<string> result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<bool> RemoveUserFromRoleAsync(BTUser user, string role)
        {
            bool result = (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;
            return result;
        }

        public async Task<bool> RemoveUserFromRolesAsync(BTUser user, IEnumerable<string> roles)
        {
            bool result = (await _userManager.RemoveFromRolesAsync(user, roles)).Succeeded;
            return result;
        }

        public async Task<List<BTUser>> UsersNotInRoleAsync(string roleName, int companyId)
        {
            List<BTUser> usersNotInRole = new();
            try
            {
                //Modify for multi tenants
                foreach (BTUser user in await _infoService.GetAllMembersAsync(companyId))
                {
                    if (!await IsUserInRoleAsync(user, roleName))
                    {
                        usersNotInRole.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

            return usersNotInRole;
        }
    }
}
