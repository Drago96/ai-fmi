using System;
using System.Collections.Generic;
using System.Linq;

namespace KNearestNeighbours
{
    public class TrainTest
    {
        private const int TestSetSize = 20;

        public static (IEnumerable<Iris> training, IEnumerable<Iris> test) Split(IEnumerable<Iris> data)
        {
            var dataRandomized = data.OrderBy(i => Guid.NewGuid()).ToList();

            var testSet = dataRandomized.Take(TestSetSize).ToList();
            var trainingSet = dataRandomized.Skip(TestSetSize).ToList();

            return (trainingSet, testSet);
        }
    }
}
