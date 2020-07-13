using System.Collections.Generic;
using NUnit.Framework;
using Sampling;

namespace Test
{
    public class KFoldCrossValidationTest
    {
        List<string> smallSample;
        List<int> largeSample;

        [SetUp]
        public void Setup()
        {
            string[] data = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            smallSample = new List<string>(data);
            largeSample = new List<int>();
            for (var i = 0; i < 1000; i++)
            {
                largeSample.Add(i);
            }
        }

        [Test]
        public void TestSmallSample10Fold()
        {
            var kFoldCrossValidation = new KFoldCrossValidation<string>(smallSample, 10, 1);
            string[] expected1 = {"1"};
            Assert.AreEqual(expected1, kFoldCrossValidation.GetTestFold(0).ToArray());
        }

        [Test]
        public void TestSmallSample5Fold()
        {
            var kFoldCrossValidation = new KFoldCrossValidation<string>(smallSample, 5, 1);
            string[] expected2 = {"1", "2"};
            Assert.AreEqual(expected2, kFoldCrossValidation.GetTestFold(0).ToArray());
        }

        [Test]
        public void TestSmallSample2Fold()
        {
            var kFoldCrossValidation = new KFoldCrossValidation<string>(smallSample, 2, 1);
            string[] expected3 = {"1", "2", "3", "4", "5"};
            Assert.AreEqual(expected3, kFoldCrossValidation.GetTestFold(0).ToArray());
        }

        [Test]
        public void TestLargeSample10Fold()
        {
            var kFoldCrossValidation = new KFoldCrossValidation<int>(largeSample, 10, 1);
            for (var i = 0; i < 10; i++)
            {
                var items = new HashSet<int>();
                items.UnionWith(kFoldCrossValidation.GetTrainFold(i));
                items.UnionWith(kFoldCrossValidation.GetTestFold(i));
                Assert.AreEqual(100, kFoldCrossValidation.GetTestFold(i).Count);
                Assert.AreEqual(900, kFoldCrossValidation.GetTrainFold(i).Count);
                Assert.AreEqual(1000, items.Count);
            }
        }

        [Test]
        public void TestLargeSample5Fold()
        {
            var kFoldCrossValidation = new KFoldCrossValidation<int>(largeSample, 5, 1);
            for (var i = 0; i < 5; i++)
            {
                var items = new HashSet<int>();
                items.UnionWith(kFoldCrossValidation.GetTrainFold(i));
                items.UnionWith(kFoldCrossValidation.GetTestFold(i));
                Assert.AreEqual(200, kFoldCrossValidation.GetTestFold(i).Count);
                Assert.AreEqual(800, kFoldCrossValidation.GetTrainFold(i).Count);
                Assert.AreEqual(1000, items.Count);
            }
        }

        [Test]
        public void TestLargeSample2Fold()
        {
            var kFoldCrossValidation = new KFoldCrossValidation<int>(largeSample, 2, 1);
            for (var i = 0; i < 2; i++)
            {
                var items = new HashSet<int>();
                items.UnionWith(kFoldCrossValidation.GetTrainFold(i));
                items.UnionWith(kFoldCrossValidation.GetTestFold(i));
                Assert.AreEqual(500, kFoldCrossValidation.GetTestFold(i).Count);
                Assert.AreEqual(500, kFoldCrossValidation.GetTrainFold(i).Count);
                Assert.AreEqual(1000, items.Count);
            }
        }
    }
}