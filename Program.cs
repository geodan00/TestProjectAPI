using System.Reflection.Metadata;
using System.Text;
using TestGeodanApi.Interfaces;
using TestGeodanApi.Models;
using TestGeodanApi.Services;
using static System.Collections.Specialized.BitVector32;


// Local variable;
Parameters listeParametres;
IConfigurationSection section;
byte[] connexionString;
string clientUrl;

var builder = WebApplication.CreateBuilder(args);

// Obtention des paramètres de l'application. 
section = builder.Configuration.GetSection(nameof(Parameters));
builder.Services.Configure<Parameters>(section);
listeParametres = section.Get<Parameters>();
connexionString = Encoding.ASCII.GetBytes(listeParametres.ConnexionString);
clientUrl = listeParametres.clientUrl;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.WithOrigins(clientUrl).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<IPerson, SPerson>();
builder.Services.AddScoped<ISector, SSector>();
builder.Services.AddScoped<IUsers, SUsers>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// API Documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TEST GEODAN BACKEND (API)",
        Version = "v1",
        Description = "Backend part for the test Task",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Géodan Nzissié",
            Email = "geodannzissie@gmail.com",
        }
    }); ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    
    app.UseSwaggerUI(c =>
    {
        var prefix = string.IsNullOrEmpty(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "TEST GEODAN APIs");
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
