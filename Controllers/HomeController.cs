using DBugr.Extensions;
using DBugr.Models;
using DBugr.Models.ViewModels;
using DBugr.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBugr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<BTUser> _userManager;
        private readonly IBTTicketService _ticketService;
        private readonly IBTCompanyInfoService _infoService;
        private readonly IBTProjectService _projectService;

        public HomeController(ILogger<HomeController> logger, UserManager<BTUser> userManager, IBTTicketService ticketService, IBTCompanyInfoService infoService, IBTProjectService projectService)
        {
            _userManager = userManager;
            _logger = logger;
            _ticketService = ticketService;
            _infoService = infoService;
            _projectService = projectService;
        }

        [Authorize]
        public  async Task<IActionResult> Dashboard()
        {
            DashboardViewModel model = new();

            string userId = _userManager.GetUserId(User);
            int companyId = User.Identity.GetCompanyId().Value;

            if (User.IsInRole("Admin"))
            {
                model.Projects = await _projectService.GetAllProjectsByCompany(companyId);

            }
            else
            {
                model.Projects = await _projectService.ListUserProjectsAsync(userId);
            }

            model.Tickets = await _ticketService.GetAllTicketsByCompanyAsync(companyId);
            model.Users = await _infoService.GetAllMembersAsync(companyId);

            return View(model);
        }

        public IActionResult Landing()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
