using System;
using System.Collections.Generic;
using System.Linq;

namespace KNearestNeighbours
{
    public class KNearestNeighbours
    {
        private IEnumerable<Iris> trainingSet;

        public KNearestNeighbours(IEnumerable<Iris> trainingSet)
        {
            this.trainingSet = trainingSet;
        }

        public void Solve(IEnumerable<Iris> testSet)
        {
            Console.Write("Provide a value for k: ");
            var k = int.Parse(Console.ReadLine());

            if (k > this.trainingSet.Count())
            {
                k = this.trainingSet.Count();
            }

            var predictions = testSet.Select(i => this.Predict(i, k));

            for (var i = 0; i < testSet.Count(); i++)
            {
                var iris = testSet.ElementAt(i);

                Console.WriteLine($"Iris {iris.Id} - Predicted: {predictions.ElementAt(i)} , Actual: {iris.Species}");
            }

            Console.WriteLine();

            var accuracy = testSet
                .Where((iris, index) => iris.Species == predictions.ElementAt(index))
                .Count() / (double)testSet.Count();

            Console.WriteLine($"Accuracy: {accuracy}");
        }

        private string Predict(Iris member, int k) =>
            this.trainingSet.OrderBy(member.GetEucledeanDistance)
                          .Take(k)
                          .GroupBy(i => i.Species)
                          .OrderByDescending(g => g.Count())
                          .ThenBy(g => g.Sum(i => member.GetEucledeanDistance(i)))
                          .First()
                          .Key;
    }
}
