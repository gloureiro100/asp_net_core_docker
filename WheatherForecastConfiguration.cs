using docker_texvn_api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class WheatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
  public void Configure(EntityTypeBuilder<WeatherForecast> builder)
  {
    builder.ToTable("previsoes");

    builder.HasKey(e => e.Id);

    builder.Property(p => p.Summary)
    .IsRequired();

    builder.Property(p => p.TemperatureC)
    .IsRequired();

    builder.Property(p => p.Summary)
    .IsRequired();
  }
}