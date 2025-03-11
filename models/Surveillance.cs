namespace PFE2024_QUIZZ_API.models
{
    public class Surveillance
    {
        public int Id { get; set; }
        public bool ComportementSuspect { get; set; }
        public required string CaptureEcran { get; set; }
        public int TentativeId { get; set; }
        public Tentative? Tentative { get; set; }
    }
}

