namespace PFE2024_QUIZZ_API.models
{
    public class Reponse
    {
        public int Id { get; set; }
        public required string Texte { get; set; }
        public bool EstCorrecte { get; set; }
        public int QuestionId { get; set; }
        public  Question? Question { get; set; }
    }
}
