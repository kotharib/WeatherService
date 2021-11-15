using WeatherService.Business.Interfaces;
using WeatherService.Business.Services;
using WeatherService.Entities;
using WeatherService.Repository.Interfaces;


namespace WeatherServiceBusiness.Tests
{
    public class WeatherServiceTests
    {
        private ICityWeatherService? _weatherService = null;
        private Mock<ICityWeatherRepository>? _mockWeatherRepository = null;

        [SetUp]
        public void Setup()
        {
            _mockWeatherRepository = new Mock<ICityWeatherRepository>();
            _weatherService = new CityWeatherService(_mockWeatherRepository.Object);
            var dummyWeatherDetails = new WeatherDetails()
            {
                Name = "Cairns",
                Weather = new Weather[]
                {
                    new Weather()
                    {
                        Id = 802,
                        Main = "Clouds",
                        Description = "scattered clouds",
                        Icon = "03n"
                    }
                }
            };
            _mockWeatherRepository.Setup(x => x.GetWeatherForCityAsync(It.IsAny<Int32>(),It.IsAny<WeatherSettings>())).Returns(Task.FromResult(dummyWeatherDetails));
        }

        [Test]
        public async Task WeatherService_WhenCalled_CallsTheRepositoryAndGetsData()
        {
            var result = await _weatherService!.GetWeatherForCityAsync(It.IsAny<Int32>(), It.IsAny<WeatherSettings>());
            _mockWeatherRepository!.Verify(x => x.GetWeatherForCityAsync(It.IsAny<Int32>(), It.IsAny<WeatherSettings>()));
            result.Should().NotBeNull();
        }
    }
}
