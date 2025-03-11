namespace PFE2024_QUIZZ_API.models
{
    public class AnalyseIA
    {
        public int Id { get; set; }
        public required string Resultats { get; set; }
        public required string Recommandations { get; set; }
        public int TestId { get; set; }
        public Test? Test { get; set; }
    }
}
