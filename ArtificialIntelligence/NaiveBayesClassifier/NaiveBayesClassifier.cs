using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NaiveBayesClassifier
{
    public class NaiveBayesClassifier
    {
        private readonly IDictionary<string, double> classNameProbabilities;
        private readonly IDictionary<string, IDictionary<string, IDictionary<Vote, double>>> attributeProbabilities;
        private readonly IEnumerable<PropertyInfo> attributesInfo;

        public NaiveBayesClassifier()
        {
            this.classNameProbabilities = new Dictionary<string, double>();
            this.attributeProbabilities = new Dictionary<string, IDictionary<string, IDictionary<Vote, double>>>();
            new[] { "republican", "democrat" }.ToList().ForEach(c =>
                  this.attributeProbabilities[c] = new Dictionary<string, IDictionary<Vote, double>>());

            this.attributesInfo = typeof(Congressman).GetProperties().Where(p => p.PropertyType == typeof(Vote));

            foreach (var attribute in this.attributesInfo)
            {
                this.attributeProbabilities["republican"][attribute.Name] = new Dictionary<Vote, double>();
                this.attributeProbabilities["democrat"][attribute.Name] = new Dictionary<Vote, double>();
            }
        }

        public void Fit(IEnumerable<Congressman> trainingSet)
        {
            var republicans = trainingSet.Where(c => c.ClassName == "republican").ToList();
            var democrats = trainingSet.Where(c => c.ClassName == "democrat").ToList();

            this.classNameProbabilities["republican"] =
                republicans.Count / (double)trainingSet.Count();
            this.classNameProbabilities["democrat"] =
                democrats.Count / (double)trainingSet.Count();

            foreach (var attribute in this.attributesInfo)
            {
                new [] {Vote.None, Vote.Yes, Vote.No}.ToList().ForEach(vote =>
                {
                    this.AssignProbability("republican", attribute.Name, vote, republicans);
                    this.AssignProbability("democrat", attribute.Name, vote, democrats);
                });
            }
        }

        public double Solve(IEnumerable<Congressman> testSet)
        {
            var predictions = testSet.Select(this.Predict);

            var accuracy = testSet
                               .Where((congressman, index) => congressman.ClassName == predictions.ElementAt(index))
                               .Count() / (double)testSet.Count();

            Console.WriteLine(accuracy);

            return accuracy;
        }

        private string Predict(Congressman member)
        {
            var republicanProbability = this.GetProbability("republican", member);
            var democratProbability = this.GetProbability("democrat", member);

            return democratProbability > republicanProbability ? "democrat" : "republican";
        }

        private double GetProbability(string party, Congressman member)
        {

            var probability = this.classNameProbabilities[party];

            this.attributesInfo.ToList().ForEach(a =>
            {
                probability *=
                    this.attributeProbabilities[party][a.Name][
                        (Vote)typeof(Congressman).GetProperty(a.Name).GetValue(member)];
            });

            return probability;
        }
 
        private void AssignProbability(string party, string attribute, Vote vote, IEnumerable<Congressman> congressmen)
        {
            this.attributeProbabilities[party][attribute][vote] =
                (congressmen.Count(c => (Vote)typeof(Congressman).GetProperty(attribute).GetValue(c) == vote) + 1) /
                ((double)congressmen.Count() + 1);
        }
    }
}
