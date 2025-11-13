using System.Text.Json.Serialization;
using Train.EFCore.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    // In order to work with enum text values in the UI
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MoviesContext>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var moviesContext = scope.ServiceProvider.GetRequiredService<MoviesContext>();
moviesContext.Database.EnsureDeleted();
moviesContext.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();