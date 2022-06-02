using docker_texvn_api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// var dbConnectionString = builder.Configuration.GetConnectionString("Postgres");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
// (options =>
// {
//   options.UseNpgsql(dbConnectionString);
// });

var app = builder.Build();

await CreateDBMigrations(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseAuthorization();


// app.MapGet("/", () => "Está Funcionando!!!");

// app.MapGet("/v1/wheatherforecast", async (AppDbContext _context) =>
app.MapGet("/getall", async (AppDbContext _context) =>
{
  return await _context.Previsoes.ToListAsync();
});

app.MapGet("/create", async (AppDbContext _context) =>
{
  var result = WeatherForecast.Create();

  await _context.Previsoes.AddAsync(result);
  await _context.SaveChangesAsync();

  return result;
});

app.MapControllers();

app.Run();

async Task CreateDBMigrations(IServiceProvider services)
{
  using var db = services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
  await db.Database.EnsureCreatedAsync();
  await db.Database.MigrateAsync();
}


public class AppDbContext : DbContext
{


  public DbSet<WeatherForecast> Previsoes { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("weatherforecast");
    modelBuilder.ApplyConfiguration(new WheatherForecastConfiguration());
  }

  protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("User ID=postgres;Password=mypassword;Host=postgres;Port=5432;Database=weatherforecast;Pooling=true;");
}