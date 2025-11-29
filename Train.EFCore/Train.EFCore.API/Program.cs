using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Train.EFCore.API.Data;
using Serilog;
using Train.EFCore.API.Repositories;
using Train.EFCore.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IBatchGenreService, BatchGenreService>();
builder.Services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();

builder.Services.AddControllers()
    // In order to work with enum text values in the UI
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var serilog = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog(serilog);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
    optionsBuilder
        .UseSqlServer(connectionString);
},
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var moviesContext = scope.ServiceProvider.GetRequiredService<MoviesContext>();
    var pendingMigrations = await moviesContext.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
        throw new Exception("Database is not fully migrated for MoviesContext.");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();