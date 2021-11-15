namespace WeatherService.Entities
{
    [ExcludeFromCodeCoverage]
    public class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }
}