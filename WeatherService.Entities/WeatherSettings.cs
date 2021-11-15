using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherService.Entities
{
    [ExcludeFromCodeCoverage]
    public class WeatherSettings
    {
        public string? ApiKey { get; set; }
        public string? ApiUrl { get; set; }
        public string? ApiVersion { get; set; }
    }
}
