using System.Security.Claims;
using App_Tunav.Data;
using App_Tunav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_Tunav.Controllers
{
    public class ClientController : Controller
    {
        private readonly Context _context;

        public ClientController(Context context)
        {
            _context = context;
        }

     

        // Affiche le formulaire de création de transaction
        public IActionResult CreateReclamation()
        {
            return View();
        }

        // Ajoute une nouvelle transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReclamation(Reclamation Reclamation)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.Name;
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == userId);

                if (client != null)
                {
                   Reclamation.ClientId = client.ClientId; // Associe le ClientId à la transaction
                    Reclamation.Client = null; // Ne pas valider la navigation property Client

                    _context.Reclamations.Add(Reclamation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Reclamations)); // Redirige vers la liste des transactions
                }
                else
                {
                    ModelState.AddModelError("", "Client not found.");
                }
            }
            return View(Reclamation);
        }

        // Affiche la liste des transactions du client
        public async Task<IActionResult> Reclamations()
        {
            var userId = User.Identity.Name;
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == userId);

            if (client != null)
            {
                var transactions = await _context.Reclamations
                    .Where(t => t.ClientId == client.ClientId)
                    .ToListAsync();

                return View(Reclamations);
            }

            return View(new List<Reclamation>());
        }
        public IActionResult Index()
        {
            List<Client> objClientList = _context.Clients.ToList();
            return View(objClientList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client obj)
        {  
            
            _context.Clients.Add(obj);
            _context.SaveChanges();
            TempData["success"] = "Client Created successfully";
            return RedirectToAction("Index");
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Client? client = _context.Clients.Find(id); 
            Client? client1 = _context.Clients.FirstOrDefault(u=>u.ClientId==id);
            Client? client3 = _context.Clients.Where(u=>u.ClientId==id).FirstOrDefault();
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client obj)
        {
          
                _context.Clients.Update(obj);
                _context.SaveChanges();
            TempData["success"] = "Client updated successfully!";
            return RedirectToAction("Index");
           
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Client? clientF = _context.Clients.Find(id);
            if (clientF == null)
            {
                return NotFound();
            }
            return View(clientF);
        }

        // Méthode pour supprimer le client et rediriger vers la liste
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }
            _context.Clients.Remove(client);
            _context.SaveChanges();
            TempData["success"] = "Client Deleted successfully";
            return RedirectToAction("Index");
        }

         public IActionResult Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Récupérer l'ID du client depuis les revendications

            if (int.TryParse(userId, out var clientId))
            {
                var client = _context.Clients.Find(clientId);

                if (client != null)
                {
                    return View(client); // Afficher les informations du client dans la vue Dashboard
                }
            }

            return RedirectToAction("Login", "Account");
        }

        // Affiche le formulaire pour modifier les informations du compte
        public IActionResult EditProfile()
        {
            var userId = User.Identity.Name;
            var client = _context.Clients.FirstOrDefault(c => c.Email == userId);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost]
        public IActionResult EditProfile(Client model)
        {
            if (ModelState.IsValid)
            {
                var client = _context.Clients.FirstOrDefault(c => c.ClientId == model.ClientId);
                if (client != null)
                {
                    client.Name = model.Name;
                    client.Email = model.Email;
                    // Mettez à jour d'autres propriétés si nécessaire

                    _context.Update(client);
                    _context.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
            }
            return View(model);
        }

     

      
    }
}

