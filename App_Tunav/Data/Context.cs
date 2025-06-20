using App_Tunav.Models;
using Microsoft.EntityFrameworkCore;

namespace App_Tunav.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Compte> Comptes { get; set; }
      
        public DbSet<Reclamation> Reclamations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data for Clients
            modelBuilder.Entity<Client>().HasData(
                new Client { ClientId = 2233, Name = "Ameni", Email= "ameni@gmail.com" },
                new Client { ClientId = 1233, Name = "Arwa", Email = "arwa@gmail.com" },
                new Client { ClientId = 2433, Name = "Oussema", Email = "oussema@example.com" }
            );

            // Seed data for Comptes with valid ClientFK
            modelBuilder.Entity<Compte>().HasData(
                 new Compte { CompteId = 1, ClientFK = 2233, login = "ameni@gmail.com", password = "password1", Lien = "GPS1" },
    new Compte { CompteId = 2, ClientFK = 1233, login = "arwa@gmail.com", password = "password2", Lien = "GPS1" },
    new Compte { CompteId = 3, ClientFK = 2433, login = "oussema@gmail.com", password = "password3", Lien = "GPS1" },
                // Remove or correct these entries to ensure they reference valid ClientFK
                // new Compte { CompteId = 4, ClientFK = 2436, login = "ahmed@gmail.com", password = "password4" }, // Invalid ClientFK
                // new Compte { CompteId = 5, ClientFK = 2435, login = "Asma@gmail.com", password = "password5" }, // Invalid ClientFK
                // new Compte { CompteId = 7, ClientFK = 2435, login = "Asma@gmail.com", password = "password7" }, // Invalid ClientFK
                new Compte { CompteId = 6, ClientFK = 2233, login = "ameni@gmail.com", password = "password6", Lien = "GPS1" }
            );

            // Configure relationships and constraints
            modelBuilder.Entity<Compte>()
                .HasOne(c => c.Client)
                .WithMany(cl => cl.Comptes)
                .HasForeignKey(c => c.ClientFK)
                .OnDelete(DeleteBehavior.Cascade);

        
            base.OnModelCreating(modelBuilder);
        }
    }
}
