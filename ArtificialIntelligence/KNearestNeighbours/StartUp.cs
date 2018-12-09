using System;

namespace KNearestNeighbours
{
    class StartUp
    {
        static void Main()
        {
            var data = DataReader.ReadCsv();

            var (trainingSet, testSet) = TrainTest.Split(data);

            var kNearestNeighbours = new KNearestNeighbours(trainingSet);
            kNearestNeighbours.Solve(testSet);
        }
    }
}
