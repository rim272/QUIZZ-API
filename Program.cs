using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PFE2024_QUIZZ_API.Data;

var builder = WebApplication.CreateBuilder(args);


var x = builder.Configuration.GetConnectionString("DefaultConnection");
// Ajouter la connexion SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les services nécessaires
builder.Services.AddControllers();  // ? Pour les API REST
builder.Services.AddRazorPages();  // ? Pour les Razor Pages
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurer le pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    {
        //app.UseSwagger();
        //app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();  // ? Ajoute le mapping des API REST

app.Run();
