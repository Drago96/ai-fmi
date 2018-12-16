using System;
using System.Collections.Generic;
using System.Linq;

namespace NaiveBayesClassifier
{
    public class TrainTest
    {
        private const int SetsCount = 10;

        public static IEnumerable<(IEnumerable<Congressman> training, IEnumerable<Congressman> test)> Split(IEnumerable<Congressman> data)
        {
            var trainSetSize = data.Count() / TrainTest.SetsCount;

            IEnumerable<(IEnumerable<Congressman> training, IEnumerable<Congressman> test)> result = new List<(IEnumerable<Congressman> training, IEnumerable<Congressman> test)>();

            for (int i = 0; i < TrainTest.SetsCount; i++)
            {
                var testSet = data.Skip(i * trainSetSize).Take(trainSetSize);
                var trainingSet = data.Except(testSet);

                result = result.Append((trainingSet, testSet));
            }

            return result;
        }
    }
}
