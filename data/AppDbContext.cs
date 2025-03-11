using Azure;
using Microsoft.EntityFrameworkCore;
using PFE2024_QUIZZ_API.models;
using PFE2024_QUIZZ_API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PFE2024_QUIZZ_API.Data
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Reponse> Reponses { get; set; }
        public DbSet<Tentative> Tentatives { get; set; }
        public DbSet<AnalyseIA> AnalysesIA { get; set; }
        public DbSet<Surveillance> Surveillances { get; set; }
        

      

    }
}

