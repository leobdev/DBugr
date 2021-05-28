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

        public Task<bool> IsUserInRoleAsync(BTUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> ListUserRolesAsync(BTUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveUserFromRoleAsync(BTUser user, string roles)
        {
            bool result = (await _userManager.RemoveFromRoleAsync(user, roles)).Succeeded;
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
