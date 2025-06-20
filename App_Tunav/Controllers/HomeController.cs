using System.Diagnostics;
using App_Tunav.Data;
using App_Tunav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Tunav.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;

        public HomeController(ILogger<HomeController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _context.Clients
                .Include(c => c.Comptes)
                .ToListAsync();

            var dashboardData = clients.Select(client => new ClientDashboard
            {
                ClientName = client.Name,
                Email = client.Email,
                Comptes = client.Comptes.Select(compte => new CompteViewModel
                {
                    Lien = compte.Lien,
                    Login = compte.login
                }).ToList()
            }).ToList();

            return View(dashboardData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
