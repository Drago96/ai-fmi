using System;

namespace KNearestNeighbours
{
    public class Iris
    {
        public int Id { get; set; }

        public double SepalLengthCm { get; set; }

        public double SepalWidthCm { get; set; }

        public double PetalLengthCm { get; set; }

        public double PetalWidthCm { get; set; }

        public string Species { get; set; }

        public double GetEucledeanDistance(Iris other) =>
            Math.Sqrt(Math.Pow(this.SepalLengthCm - other.SepalLengthCm, 2) +
                Math.Pow(this.SepalWidthCm - other.SepalWidthCm, 2) +
                Math.Pow(this.PetalLengthCm - other.PetalLengthCm, 2) +
                Math.Pow(this.PetalWidthCm - other.PetalWidthCm, 2));
    }
}
