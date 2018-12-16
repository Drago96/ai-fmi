using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace NaiveBayesClassifier
{
    public class DataReader
    {
        private const string CsvPath = "./Data.csv";

        public static IEnumerable<Congressman> ReadCsv()
        {
            TextReader reader = new StreamReader(DataReader.CsvPath);

            var csvReader = new CsvReader(reader);
            csvReader.Configuration.RegisterClassMap<CongressmanClassMap>();
            var records = csvReader.GetRecords<Congressman>().ToList();

            return records;
        }
    }
}
