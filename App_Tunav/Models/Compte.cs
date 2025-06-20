using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace App_Tunav.Models
{
    public class Compte
    {
        [Key]
        public int CompteId { get; set; }

        [Required]
        public string login { get; set; }

        [Required]
        public string password { get; set; }

        public string Lien  { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("Client")]
        [Required]
        public int ClientFK { get; set; }

      
    }

}

