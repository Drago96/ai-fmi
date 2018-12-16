using System;
using System.Collections.Generic;
using System.Linq;

namespace NaiveBayesClassifier
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var data = DataReader.ReadCsv();

            var dataSets = TrainTest.Split(data);

            var accuracies = new List<double>();

            dataSets.ToList().ForEach(sets =>
            {
                var model = new NaiveBayesClassifier();
                model.Fit(sets.training);
                var accuracy = model.Solve(sets.test);

                accuracies.Add(accuracy);
            });

            Console.WriteLine();
            Console.WriteLine($"Average accuracy: {accuracies.Sum() / (double)accuracies.Count}");
        }
    }
}
