namespace WeatherService.Business.Interfaces
{
    public interface ICityWeatherService
    {
        Task<WeatherDetails> GetWeatherForCityAsync(int cityid, WeatherSettings weatherSettings);
    }
}