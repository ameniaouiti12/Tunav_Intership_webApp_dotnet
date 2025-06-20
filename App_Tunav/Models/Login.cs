using System.ComponentModel.DataAnnotations;

namespace App_Tunav.Models
{
    public class Login
    {
        [Required]
        public string login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
