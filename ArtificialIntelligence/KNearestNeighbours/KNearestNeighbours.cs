using System.Collections.Generic;

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
            
        }
    }
}
