using CsvHelper;
using CsvHelper.Configuration;

namespace WeatherService.Common
{
    public class CsvFileHelper : ICsvFileHelper
    {
        public List<T> GetRecords<T>(StreamReader streamReader,  CsvSettings csvSettings)
        {
            CsvConfiguration csvConfiguration = new(System.Globalization.CultureInfo.CurrentCulture)
            {
                Delimiter = csvSettings.Delimiter,
                MissingFieldFound = null
            };
            List<T> records = new();
            using (var csv = new CsvReader(streamReader, csvConfiguration))
            {
                while (csv.Read())
                {
                    T record = csv.GetRecord<T>();
                    records.Add(record);
                }
            }
            return records;
        }

        public void WriteRecords<T>(string csvFilePath, IEnumerable<T> records, CsvSettings csvSettings)
        {
            CsvConfiguration csvConfiguration = new(System.Globalization.CultureInfo.CurrentCulture)
            {
                Delimiter = csvSettings.Delimiter,
                MissingFieldFound = null
            };

            using (var writer = new StreamWriter(csvFilePath))
            {
                using (var csv = new CsvWriter(writer, csvConfiguration))
                {
                    csv.WriteRecords(records);
                }
            }
        }
    }
}
