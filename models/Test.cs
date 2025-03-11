using Microsoft.EntityFrameworkCore;
namespace PFE2024_QUIZZ_API.models

{
    public class Test
    {

 
            public int Id { get; set; }
            public required string Titre { get; set; }
            public required string Description { get; set; }
            public int Duree { get; set; }
            public required string Type { get; set; }
            public required string Etat { get; set; }
        

    }
}
