using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;

namespace WeatherService.Common
{
    public interface ICsvFileHelper
    {
        List<T> GetRecords<T>(StreamReader streamReader, CsvSettings csvSettings);
        void WriteRecords<T>(string csvFilePath, IEnumerable<T> records, CsvSettings csvSettings);
    }
}