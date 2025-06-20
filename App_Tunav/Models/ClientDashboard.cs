namespace App_Tunav.Models
{
    public class ClientDashboard
    {
        public string ClientName { get; set; }
        public string Email { get; set; }
        public List<CompteViewModel> Comptes { get; set; }
    }
    public class CompteViewModel
    {
        public string Lien { get; set; }
        public string Login { get; set; }
    }
}
