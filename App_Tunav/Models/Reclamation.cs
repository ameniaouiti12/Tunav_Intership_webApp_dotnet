using System.ComponentModel.DataAnnotations;



namespace App_Tunav.Models
{
    public class Reclamation
    {
        public int ReclamationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The Lien field is required.")]
        public string lien { get; set; }
        public string Description { get; set; }

        // Clé étrangère pour le client
        public int ClientId { get; set; }

        // Navigation property pour le client
        public Client? Client { get; set; }
       
     
    }


}
