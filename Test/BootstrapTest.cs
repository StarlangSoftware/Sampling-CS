using System.Collections.Generic;
using NUnit.Framework;
using Sampling;

namespace Test
{
    public class BootstrapTest
    {
        List<string> smallSample;
        List<int> largeSample;
        
        [SetUp]
        public void Setup()
        {
            string[] data = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            smallSample = new List<string>(data);
            largeSample = new List<int>();
            for (var i = 0; i < 1000000; i++){
                largeSample.Add(i);
            }
        }

        [Test]
        public void TestSmallSample() {
            var bootstrap = new Bootstrap<string>(smallSample, 1);
            string[] sample = {"3", "2", "5", "8", "7", "5", "4", "10", "2", "7"};
            Assert.AreEqual(sample, bootstrap.GetSample().ToArray());
        }

        [Test]
        public void TestLargeSample() {
            var bootstrap = new Bootstrap<int>(largeSample, 1);
            var sample = bootstrap.GetSample();
            var set = new HashSet<int>();
            set.UnionWith(sample);
            Assert.AreEqual(set.Count / 1000000.0, 0.632, 0.001);
        }

    }
}