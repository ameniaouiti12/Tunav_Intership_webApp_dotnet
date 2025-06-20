using System.Security.Claims;
using App_Tunav.Data;
using App_Tunav.Models;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace App_Tunav.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly Context _context;

        public AuthentificationController(Context context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(App_Tunav.Models.Login model)
        {
            if (ModelState.IsValid)
            {
                // Recherche du compte dans la base de données
                var account = _context.Comptes.SingleOrDefault(c => c.login == model.login && c.password == model.Password);

                if (account != null)
                {
                    // Trouver le client associé² 
                    var client = _context.Clients.SingleOrDefault(c => c.ClientId == account.ClientFK);

                    if (client != null)
                    {
                        // Créer les revendications pour l'utilisateur
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, client.Email), // Utilisation de l'email comme identifiant
                    new Claim(ClaimTypes.NameIdentifier, client.ClientId.ToString())
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        // Authentifier l'utilisateur
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        // Rediriger vers la page d'accueil ou l'espace personnel du client
                        return RedirectToAction("Dashboard", "Client"); // Redirection vers le Dashboard du client
                    }
                }
                // Ajouter une erreur si l'authentification échoue
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Déconnecter l'utilisateur en supprimant les cookies d'authentification
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Rediriger vers la page d'accueil ou une autre page
            return RedirectToAction("Index", "Home"); // Ou "Dashboard", "Client", etc.
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Comptes.SingleOrDefault(c => c.login == model.Email);
                if (account != null)
                {
                    // Generate password reset token (for simplicity, use a GUID)
                    var token = System.Guid.NewGuid().ToString();

                    // Save the token in a database or send via email to the user
                    // For now, assume the token is sent via email and proceed
                    // TODO: Send email with the token (you'll need to implement this part)

                    return RedirectToAction("ResetPassword", new { token = token, email = model.Email });
                }
                ModelState.AddModelError("", "Email not found.");
            }
            return View(model);
        }

        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Comptes.SingleOrDefault(c => c.login == model.Email);
                if (account != null)
                {
                    // Validate the token (for simplicity, assume the token is valid)
                    // Update the password
                    account.password = model.NewPassword;
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                ModelState.AddModelError("", "Invalid token or email.");
            }
            return View(model);
        }
    }
}

