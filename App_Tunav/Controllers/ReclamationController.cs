using App_Tunav.Data;
using Microsoft.AspNetCore.Mvc;
using App_Tunav.Models;
using Microsoft.EntityFrameworkCore;
using AppReclamation = App_Tunav.Models.Reclamation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App_Tunav.Controllers
{
    public class ReclamationController : Controller
    {
        private readonly Context _context;

        public ReclamationController(Context context)
        {
            _context = context;
        }

        // GET: Reclamation/Index
        public async Task<IActionResult> Reclamations()
        {
            var userId = User.Identity.Name;
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == userId);

            if (client != null)
            {
                var reclamations = await _context.Reclamations
                    .Where(t => t.ClientId == client.ClientId)
                    .ToListAsync();

                return View(reclamations);
            }

            return NotFound();
        }

        // GET: Reclamation/CreateReclamation
        public async Task<IActionResult> CreateReclamation()
        {
            var userId = User.Identity.Name;
            var client = await _context.Clients.Include(c => c.Comptes)
                                               .FirstOrDefaultAsync(c => c.Email == userId);

            if (client != null)
            {
                var liens = client.Comptes.Select(c => c.Lien).ToList();
                ViewBag.Liens = new SelectList(liens);
                return View();
            }

            return NotFound();
        }

        // POST: Reclamation/CreateReclamation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReclamation(Reclamation reclamation)
        {
            if (ModelState.IsValid)
            {
                var userEmail = User.Identity.Name;
                var currentClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == userEmail);

                if (currentClient != null)
                {
                    reclamation.ClientId = currentClient.ClientId;
                    _context.Reclamations.Add(reclamation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Reclamations));
                }
                else
                {
                    ModelState.AddModelError("", "Client not found.");
                }
            }

            // Repopulate the ViewBag.Liens if validation fails
            var userEmailForViewBag = User.Identity.Name;
            var clientForViewBag = await _context.Clients.Include(c => c.Comptes)
                                                         .FirstOrDefaultAsync(c => c.Email == userEmailForViewBag);
            if (clientForViewBag != null)
            {
                var liens = clientForViewBag.Comptes.Select(c => c.Lien).ToList();
                ViewBag.Liens = new SelectList(liens);
            }

            return View(reclamation);
        }
    }

}
