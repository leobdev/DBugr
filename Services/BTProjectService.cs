using DBugr.Data;
using DBugr.Models;
using DBugr.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Services
{
    public class BTprojectService : IBTProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BTprojectService> _logger;
        private readonly IBTRolesService _rolesService;

        public BTprojectService (ApplicationDbContext context,
                                 ILogger<BTprojectService> logger,
                                 IBTRolesService roleService)
        {
            _context = context;
            _logger = logger;
            _rolesService = roleService;
        }

        public Task<bool> AddProjectManagerAsync(string userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddUserToProjectAsync(string userId, int projectId)
        {
            try
            {
                BTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if(user != null)
                {
                    Project project = await _context.Project.FirstOrDefaultAsync(p => p.Id == projectId);

                    if (!await IsUserOnProject(userId, projectId))
                    {
                        try
                        {
                            project.Members.Add(user);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    

                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"--Error-- Error adding user to project. --> { ex.Message}");
                return false;
            }
        }


        public Task<List<BTUser>> DevelopersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> GetAllProjectsByCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Project>> GetAllProjectsByPriority(int companyId, string priorityName)
        {
            int priorityId = await LookupProjectPriorityId(priorityName);
            return await _context.Project.Where(p => p.CompanyId == companyId && p.ProjectPriorityId == priorityId).ToListAsync();
        }

        public Task<List<Project>> GetArchivedProjectsByCompany(int companyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTUser>> GetMembersWithoutPMAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<BTUser> GetProjectManagerAsync(int projectId)
        {
            Project project = await _context.Project
                                     .Include(p => p.Members)
                                     .FirstOrDefaultAsync(u => u.Id == projectId);
            
            foreach (BTUser member in project?.Members)
            {
                if(await _rolesService.IsUserInRoleAsync(member, "ProjectManager"))
                {
                    return member;
                }          
            }
            return null;
        }

        public Task<List<BTUser>> GetProjectMembersByRoleAsync(int projectId, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserOnProject(string userId, int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> ListUserProjectsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProjectManagerAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveUserFromProjectAsync(string userId, int projectId)
        {
            try
            {
                BTUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);                               
                if (user != null)
                {
                    Project project = await _context.Project.FirstOrDefaultAsync(p => p.Id == projectId);
                    if (await IsUserOnProject(userId, projectId))
                    {
                        try
                        {
                            project.Members.Remove(user);
                            await _context.SaveChangesAsync();

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"---Error--- Error adding user to project --->{ex.Message }");
            }
        }

        public async Task RemoveUsersFromProjectByRoleAsync(string role, int projectId)
        {
            try
            {
                List<BTUser> members = await GetProjectMembersByRoleAsync(projectId, role);
                Project project = await _context.Project.FirstOrDefaultAsync(p => p.Id == projectId);

                foreach (BTUser btUser in members)
                    try
                    {
                        project.Members.Remove(btUser);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"---Error--- Error removing users from project ---> {ex.Message}");
            }
        }

        public Task<List<BTUser>> SubmittersOnProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BTUser>> UsersNotOnProjectAsync(int projectId, int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> LookupProjectPriorityId(string priorityName)
        {
            return (await _context.ProjectPriority.FirstOrDefaultAsync(p => p.Name == priorityName)).Id;
        }
    }

}
