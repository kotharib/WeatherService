using WeatherService.Entities;

namespace WeatherService.Repository.Interfaces
{
    public interface ICityWeatherRepository
    {
        Task<WeatherDetails> GetWeatherForCityAsync(int cityid, WeatherSettings weatherSettings);
    }
}
