using System.Numerics;
using App_Tunav.Data;
using App_Tunav.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App_Tunav.Controllers
{
    public class CompteController : Controller
    {
        private readonly Context _context;

        public CompteController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Compte> objCompteList = _context.Comptes.Include(c => c.Client).ToList();
            return View(objCompteList);
        }

        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_context.Clients, "ClientId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compte compte)
        {
            
                _context.Comptes.Add(compte);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Account Created successfully!";
                ViewBag.Clients = new SelectList(_context.Clients, "ClientId", "Name", compte.ClientFK);
                return RedirectToAction("Index");
          
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Compte? compte = _context.Comptes.Find(id);
            if (compte == null)
            {
                return NotFound();
            }

            ViewBag.Clients = new SelectList(_context.Clients, "ClientId", "Name", compte.ClientFK);
            return View(compte);
        }
        [HttpPost]
        public IActionResult Edit(Compte obj)
        {

            _context.Comptes.Update(obj);
            _context.SaveChanges();
            TempData["success"] = "Account updated successfully!";
            ViewBag.Clients = new SelectList(_context.Clients, "ClientId", "Name", obj.ClientFK);
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Compte? compte = _context.Comptes.Find(id);
            if (compte == null)
            {
                return NotFound();
            }
            return View(compte);
        }

        // POST: Compte/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Compte? compte = _context.Comptes.Find(id);
            if (compte == null)
            {
                return NotFound();
            }
            _context.Comptes.Remove(compte);
            _context.SaveChanges();
            TempData["success"] = "Account Deleted successfully!";
            return RedirectToAction("Index");
        }



    }
}
