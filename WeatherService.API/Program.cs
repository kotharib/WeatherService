var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.Configure<WeatherSettings>(builder.Configuration.GetSection("WeatherSettings"));
builder.Services.Configure<CsvSettings>(builder.Configuration.GetSection("CsvSettings"));
builder.Services.AddTransient<ICityWeatherService, CityWeatherService>();
builder.Services.AddTransient<ICityWeatherRepository, CityWeatherRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
