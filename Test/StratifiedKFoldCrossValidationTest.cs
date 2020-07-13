using System.Collections.Generic;
using NUnit.Framework;
using Sampling;

namespace Test
{
    public class StratifiedKFoldCrossValidationTest
    {
        List<string>[] smallSample;
        List<int>[] largeSample;

        [SetUp]
        public void Setup()
        {
            string[] class1Data = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
            var inputClass1 = new List<string>(class1Data);
            string[] class2Data =
            {
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"
            };
            var inputClass2 = new List<string>(class2Data);
            smallSample = new List<string>[2];
            smallSample[0] = inputClass1;
            smallSample[1] = inputClass2;
            var class1 = new List<int>();
            for (var i = 0; i < 1000; i++)
            {
                class1.Add(i);
            }

            var class2 = new List<int>();
            for (var i = 0; i < 3000; i++)
            {
                class2.Add(1000 + i);
            }

            var class3 = new List<int>();
            for (var i = 0; i < 5000; i++)
            {
                class3.Add(4000 + i);
            }

            largeSample = new List<int>[3];
            largeSample[0] = class1;
            largeSample[1] = class2;
            largeSample[2] = class3;
        }

        [Test]
        public void TestSmallSample10Fold()
        {
            var stratifiedKFoldCrossValidation =
                new StratifiedKFoldCrossValidation<string>(smallSample, 10, 1);
            string[] expected1 = {"1", "11", "12"};
            Assert.AreEqual(expected1, stratifiedKFoldCrossValidation.GetTestFold(0).ToArray());
        }

        [Test]
        public void TestSmallSample5Fold()
        {
            var stratifiedKFoldCrossValidation =
                new StratifiedKFoldCrossValidation<string>(smallSample, 5, 1);
            string[] expected2 = {"1", "2", "11", "12", "13", "14"};
            Assert.AreEqual(expected2, stratifiedKFoldCrossValidation.GetTestFold(0).ToArray());
        }

        [Test]
        public void TestSmallSample2Fold()
        {
            var stratifiedKFoldCrossValidation =
                new StratifiedKFoldCrossValidation<string>(smallSample, 2, 1);
            string[] expected3 = {"1", "2", "3", "4", "5", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"};
            Assert.AreEqual(expected3, stratifiedKFoldCrossValidation.GetTestFold(0).ToArray());
        }

        [Test]
        public void TestLargeSample10Fold()
        {
            var stratifiedKFoldCrossValidation =
                new StratifiedKFoldCrossValidation<int>(largeSample, 10, 1);
            for (var i = 0; i < 10; i++)
            {
                var items = new HashSet<int>();
                var trainFold = stratifiedKFoldCrossValidation.GetTrainFold(i);
                var testFold = stratifiedKFoldCrossValidation.GetTestFold(i);
                items.UnionWith(trainFold);
                items.UnionWith(testFold);
                Assert.AreEqual(900, testFold.Count);
                Assert.AreEqual(8100, trainFold.Count);
                Assert.AreEqual(9000, items.Count);
                var trainCounts = new int[3];
                foreach (int integer in trainFold) {
                    if (integer < 1000)
                    {
                        trainCounts[0]++;
                    }
                    else
                    {
                        if (integer < 4000)
                        {
                            trainCounts[1]++;
                        }
                        else
                        {
                            trainCounts[2]++;
                        }
                    }
                }
                Assert.AreEqual(900, trainCounts[0]);
                Assert.AreEqual(2700, trainCounts[1]);
                Assert.AreEqual(4500, trainCounts[2]);
                var testCounts = new int[3];
                foreach (int integer in testFold) {
                    if (integer < 1000)
                    {
                        testCounts[0]++;
                    }
                    else
                    {
                        if (integer < 4000)
                        {
                            testCounts[1]++;
                        }
                        else
                        {
                            testCounts[2]++;
                        }
                    }
                }
                Assert.AreEqual(100, testCounts[0]);
                Assert.AreEqual(300, testCounts[1]);
                Assert.AreEqual(500, testCounts[2]);
            }
        }

        [Test]

        public void TestLargeSample5Fold()
        {
            var stratifiedKFoldCrossValidation =
                new StratifiedKFoldCrossValidation<int>(largeSample, 5, 1);
            for (var i = 0; i < 5; i++)
            {
                var items = new HashSet<int>();
                var trainFold = stratifiedKFoldCrossValidation.GetTrainFold(i);
                var testFold = stratifiedKFoldCrossValidation.GetTestFold(i);
                items.UnionWith(trainFold);
                items.UnionWith(testFold);
                Assert.AreEqual(1800, testFold.Count);
                Assert.AreEqual(7200, trainFold.Count);
                Assert.AreEqual(9000, items.Count);
                var trainCounts = new int[3];
                foreach (int integer in trainFold) {
                    if (integer < 1000)
                    {
                        trainCounts[0]++;
                    }
                    else
                    {
                        if (integer < 4000)
                        {
                            trainCounts[1]++;
                        }
                        else
                        {
                            trainCounts[2]++;
                        }
                    }
                }
                Assert.AreEqual(800, trainCounts[0]);
                Assert.AreEqual(2400, trainCounts[1]);
                Assert.AreEqual(4000, trainCounts[2]);
                var testCounts = new int[3];
                foreach (int integer in testFold) {
                    if (integer < 1000)
                    {
                        testCounts[0]++;
                    }
                    else
                    {
                        if (integer < 4000)
                        {
                            testCounts[1]++;
                        }
                        else
                        {
                            testCounts[2]++;
                        }
                    }
                }
                Assert.AreEqual(200, testCounts[0]);
                Assert.AreEqual(600, testCounts[1]);
                Assert.AreEqual(1000, testCounts[2]);
            }
        }

        [Test]

        public void TestLargeSample2Fold()
        {
            var stratifiedKFoldCrossValidation =
                new StratifiedKFoldCrossValidation<int>(largeSample, 2, 1);
            for (var i = 0; i < 2; i++)
            {
                var items = new HashSet<int>();
                var trainFold = stratifiedKFoldCrossValidation.GetTrainFold(i);
                var testFold = stratifiedKFoldCrossValidation.GetTestFold(i);
                items.UnionWith(trainFold);
                items.UnionWith(testFold);
                Assert.AreEqual(4500, testFold.Count);
                Assert.AreEqual(4500, trainFold.Count);
                Assert.AreEqual(9000, items.Count);
                var trainCounts = new int[3];
                foreach (int integer in trainFold) {
                    if (integer < 1000)
                    {
                        trainCounts[0]++;
                    }
                    else
                    {
                        if (integer < 4000)
                        {
                            trainCounts[1]++;
                        }
                        else
                        {
                            trainCounts[2]++;
                        }
                    }
                }
                Assert.AreEqual(500, trainCounts[0]);
                Assert.AreEqual(1500, trainCounts[1]);
                Assert.AreEqual(2500, trainCounts[2]);
                var testCounts = new int[3];
                foreach (int integer in testFold) {
                    if (integer < 1000)
                    {
                        testCounts[0]++;
                    }
                    else
                    {
                        if (integer < 4000)
                        {
                            testCounts[1]++;
                        }
                        else
                        {
                            testCounts[2]++;
                        }
                    }
                }
                Assert.AreEqual(500, testCounts[0]);
                Assert.AreEqual(1500, testCounts[1]);
                Assert.AreEqual(2500, testCounts[2]);
            }
        }
    }
}