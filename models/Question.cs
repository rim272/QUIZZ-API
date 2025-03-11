namespace PFE2024_QUIZZ_API.models
{
    public class Question
    {
        public int Id { get; set; }
        public required string Texte { get; set; }
        public string? Type { get; set; }
        public int NiveauDifficulte { get; set; }
        public int TestId { get; set; }
        public Test? Test { get; set; } 
    }
}
