using System;
using System.Collections.Generic;

namespace Sampling
{
    public class KFoldCrossValidation<T> : CrossValidation<T>
    {
        private readonly List<T> _instanceList;
        private readonly int _n;

        public static void Shuffle(List<T> list, Random random)  
        {  
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = random.Next(n + 1);  
                var value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
        
        /**
         * <summary>A constructor of {@link KFoldCrossValidation} class which takes a sample as an array of instances, a K (K in K-fold cross-validation) and a seed number,
         * then shuffles the original sample using this seed as random number.</summary>
         *
         * <param name="instanceList">Original sample</param>
         * <param name="k">K in K-fold cross-validation</param>
         * <param name="seed">Random number to create K-fold sample(s)</param>
         */
        public KFoldCrossValidation(List<T> instanceList, int k, int seed){
            this._instanceList = instanceList;
            Shuffle(this._instanceList, new Random(seed));
            _n = instanceList.Count;
            this.K = k;
        }

        /**
         * <summary>getTrainFold returns the k'th train fold in K-fold cross-validation.</summary>
         *
         * <param name="k">index for the k'th train fold of the K-fold cross-validation</param>
         * <returns>Produced training sample</returns>
         */
        public override List<T> GetTrainFold(int k)
        {
            var trainFold = new List<T>();
            for (var i = 0; i < k * _n / K; i++){
                trainFold.Add(_instanceList[i]);
            }
            for (var i = (k + 1) * _n / K; i < _n; i++){
                trainFold.Add(_instanceList[i]);
            }
            return trainFold;
        }

        /**
         * <summary>getTestFold returns the k'th test fold in K-fold cross-validation.</summary>
         *
         * <param name="k">index for the k'th test fold of the K-fold cross-validation</param>
         * <returns>Produced testing sample</returns>
         */
        public override List<T> GetTestFold(int k)
        {
            var testFold = new List<T>();
            for (var i = k * _n / K; i < (k + 1) * _n / K; i++){
                testFold.Add(_instanceList[i]);
            }
            return testFold;
        }
    }
}