using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace KNearestNeighbours
{
    public class DataReader
    {
        private const string CsvPath = "./Iris.csv";

        public static IEnumerable<Iris> ReadCsv()
        {
            TextReader reader = new StreamReader(DataReader.CsvPath);

            var csvReader = new CsvReader(reader);
            var records = csvReader.GetRecords<Iris>().ToList();

            return records;
        }
    }
}
