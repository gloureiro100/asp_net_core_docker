namespace docker_texvn_api;

public class WeatherForecast
{
  public int TemperatureC { get; set; }

  public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

  public string? Summary { get; set; }

  public Guid Id { get; set; }

  private static readonly string[] Summaries = new[]
  {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
  };

  public static WeatherForecast Create()
  {
    return new WeatherForecast
    {
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = Summaries[Random.Shared.Next(Summaries.Length)],
      Id = Guid.NewGuid()
    };
  }
}
