namespace WeatherService.Business.Services
{
    public class CityWeatherService : ICityWeatherService
    {
        private readonly ICityWeatherRepository _weatherRepository;
        public CityWeatherService(ICityWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public async Task<WeatherDetails> GetWeatherForCityAsync(int cityid, WeatherSettings weatherSettings)
        {
            return await  _weatherRepository.GetWeatherForCityAsync(cityid, weatherSettings);
        }
    }
}
