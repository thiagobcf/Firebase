using Api.Data;
using Api.Services.Authentication;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionando serviços ao contêiner.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

FirebaseApp.Create(new AppOptions{
    Credential = GoogleCredential.FromFile("firebase.json")
});

// Registrando o serviço de autenticação
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((sp, httpClient) => {
    var configuration = sp.GetRequiredService<IConfiguration>();
    httpClient.BaseAddress = new Uri(configuration["Authentication:TokenUri"]!);
});

// Configurando a autenticação JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtOptions => {
        jwtOptions.Authority = builder.Configuration["Authentication:ValidIssuer"];
        jwtOptions.Audience = builder.Configuration["Authentication:Audience"];
        jwtOptions.TokenValidationParameters.ValidIssuer = builder.Configuration["Authentication:ValidIssuer"];
    });

// Adicionando serviços de autorização
builder.Services.AddAuthorization();

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.LogTo(Console.WriteLine, new [] {
        DbLoggerCategory.Database.Command.Name
    }, LogLevel.Information).EnableSensitiveDataLogging();
    
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase"));
});

var app = builder.Build();

// Configurando o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
