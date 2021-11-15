using WeatherService.API.Controllers;
using WeatherService.Business.Interfaces;
using WeatherService.Common;
using WeatherService.Entities;

namespace WeatherServiceAPI.Tests
{
    public class WeatherDataControllerTests
    {
        private readonly WeatherDataController _cityWeatherController;
        private readonly Mock<ICityWeatherService> _mockCityWeatherService;

        public WeatherDataControllerTests()
        {
            _mockCityWeatherService = new Mock<ICityWeatherService>();
            WeatherSettings weatherSettings = new WeatherSettings() { ApiUrl = "some url", ApiKey = "some id" };
            CsvSettings csvSettings = new CsvSettings() { Delimiter = "," };
            IOptions<WeatherSettings> weatherOptions = Options.Create(weatherSettings);
            IOptions<CsvSettings> csvOptions = Options.Create(csvSettings);
            _cityWeatherController = new WeatherDataController(_mockCityWeatherService.Object, weatherOptions, csvOptions);
        }

        [Fact]
        public void When_GetWeatherForCitiesCalled_Returns_Data()
        {
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

            _mockCityWeatherService.Setup(x => x.GetWeatherForCityAsync(It.IsAny<Int32>(), It.IsAny<WeatherSettings>()))
                                   .Returns(Task.FromResult(dummyWeatherDetails));
            var formFile = new Mock<IFormFile>();
            StringBuilder sbContent = new StringBuilder();
            sbContent.AppendLine("CityId,CityName");
            sbContent.AppendLine("2643741,City of London");
            sbContent.AppendLine("5128581,New York");
            sbContent.AppendLine("1273294,Delhi");
            sbContent.AppendLine("1275339,Mumbai");

            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sbContent.ToString());
            writer.Flush();
            ms.Position = 0;

            formFile.Setup(_ => _.OpenReadStream()).Returns(ms);
            formFile.Setup(_ => _.FileName).Returns(fileName);
            formFile.Setup(_ => _.Length).Returns(ms.Length);

            var file = formFile.Object;
            var result = _cityWeatherController.GetWeatherForCitiesAsync(file);

            result.Should().NotBeNull();
        }


        [Fact]
        public async Task When_GetWeatherForCitiesCalledWithNoFile_Returns_BadRequest()
        {
            var result = await _cityWeatherController.GetWeatherForCitiesAsync(null);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task When_GetWeatherForCitiesCalledWithEmptyFile_Returns_BadRequest()
        {
            var formFile = new Mock<IFormFile>();
            StringBuilder sbContent = new StringBuilder();
            sbContent.Append(string.Empty);
            var fileName = "test.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(sbContent.ToString());
            writer.Flush();
            ms.Position = 0;
            formFile.Setup(_ => _.OpenReadStream()).Returns(ms);
            formFile.Setup(_ => _.FileName).Returns(fileName);
            formFile.Setup(_ => _.Length).Returns(ms.Length);
            var file = formFile.Object;
            var result = await _cityWeatherController.GetWeatherForCitiesAsync(file);
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
