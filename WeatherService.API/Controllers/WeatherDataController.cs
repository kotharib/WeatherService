namespace WeatherService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherDataController : ControllerBase
    {
        private readonly ICityWeatherService _cityWeatherService;
        private readonly IOptions<WeatherSettings> _weatherSettings;
        private readonly IOptions<CsvSettings> _csvSettings;

        public WeatherDataController(ICityWeatherService cityWeatherService, IOptions<WeatherSettings> weatherSettings, IOptions<CsvSettings> csvSettings)
        {
            _cityWeatherService = cityWeatherService;
            _weatherSettings = weatherSettings;
            _csvSettings = csvSettings;
        }

        /// <summary>
        /// This method will accept the file as a input and get the weather for the cities. The result will be written into a separate csv file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("getweatherforcities")]
        public async Task<IActionResult> GetWeatherForCitiesAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            ICsvFileHelper csvHelper = new CsvFileHelper();
            IEnumerable<WeatherRecord> weatherRecords;
            List<WeatherDetails> weatherDetails = new();
            string strMessage;
            try
            {
                //Get the list of cities
                var reader = new StreamReader(file.OpenReadStream());
                weatherRecords = csvHelper.GetRecords<WeatherRecord>(reader, _csvSettings.Value!);

                //Get the weather for the cities
                foreach (var wr in weatherRecords)
                {
                    var weather = await _cityWeatherService.GetWeatherForCityAsync(wr.CityId, _weatherSettings.Value!);
                    weatherDetails.Add(weather);
                }

                //write the weather to file
                //Appending the Datetime.Ticks to ensure unique file name. Guid can also be used instead.
                string csvFilepath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Weather_" + DateTime.Now.Ticks.ToString() + ".csv");
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Files")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Files"));
                }
                csvHelper.WriteRecords<WeatherDetails>(csvFilepath, weatherDetails, _csvSettings.Value!);
                strMessage = "Successfully written to " + csvFilepath;
            }
            catch (Exception ex)
            {
                strMessage = "Operation failed with error:" + ex.Message;
            }
            return Ok(strMessage);
        }
    }
}
