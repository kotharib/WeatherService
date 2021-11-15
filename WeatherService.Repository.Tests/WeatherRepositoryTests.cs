using WeatherService.Entities;
using WeatherService.Repository;
using WeatherService.Repository.Interfaces;

namespace WeatherServiceRepository.Tests
{
    [TestFixture]
    public class WeatherRepositoryTests
    {
        private ICityWeatherRepository? weatherRepository;

        [SetUp]
        public void Setup()
        {
            weatherRepository = new CityWeatherRepository();
        }

        [Test]
        public async Task WeatherService_WhenCalled_ReturnsData()
        {
            var cityId = 2172797;
            var weatherSettings = new WeatherSettings()
            {
                ApiKey = "aa69195559bd4f88d79f9aadeb77a8f6",
                ApiUrl = "http://samples.openweathermap.org",
                ApiVersion = "2.5"
            };
            var weather = await weatherRepository!.GetWeatherForCityAsync(cityId, weatherSettings);

            weather.Should().NotBeNull();
        }
    }
}
