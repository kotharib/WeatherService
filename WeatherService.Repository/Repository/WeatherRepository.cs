using Newtonsoft.Json;
namespace WeatherService.Repository
{
    public class CityWeatherRepository : ICityWeatherRepository
    {
        public async Task<WeatherDetails> GetWeatherForCityAsync(int cityid, WeatherSettings weatherSettings)
        {
            WeatherDetails? weatherDetails = null;

            using (var client = new HttpClient())
            {
                    client.BaseAddress = new Uri(weatherSettings.ApiUrl!);
                    var response = await client.GetAsync($"/data/{weatherSettings.ApiVersion}/weather?id={cityid}&appid={weatherSettings.ApiKey}");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    weatherDetails = JsonConvert.DeserializeObject<WeatherDetails>(stringResult);
            }

            return weatherDetails!;
        }
    }
}