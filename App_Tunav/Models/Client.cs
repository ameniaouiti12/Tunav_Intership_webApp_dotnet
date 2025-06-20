using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace App_Tunav.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Compte> Comptes { get; set; }
        public virtual ICollection<Reclamation> Reclamations { get; set; } // Ajoutez cette ligne

    }
    
       
}
