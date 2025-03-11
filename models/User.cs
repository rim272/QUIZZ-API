namespace PFE2024_QUIZZ_API.Models
{
	public class User
	{
		public int Id { get; set; }  // Clé primaire
		public required string nom { get; set; } // Nom d'utilisateur
		public required string Email { get; set; } // Adresse email
		public required string mdp { get; set; } // Mot de passe
        public required string Role { get; set; } // admin, candidat, formateur
    }
}
