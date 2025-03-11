using PFE2024_QUIZZ_API.Models;

namespace PFE2024_QUIZZ_API.models
{
    public class Tentative
    {
        public int Id { get; set; }
        public float ScoreObtenu { get; set; }
        public DateTime Date { get; set; }
        public int CandidatId { get; set; }
        public  User? Candidat { get; set; }
    }
}
