using System;
using System.Collections.Generic;

namespace Sampling
{
    public class StratifiedKFoldCrossValidation<T> : CrossValidation<T>
    {
        private readonly List<T>[] _instanceLists;
        private readonly int[] _n;

        /**
         * <summary>A constructor of {@link StratifiedKFoldCrossValidation} class which takes as set of class samples as an array of array of instances, a K (K in K-fold cross-validation) and a seed number,
         * then shuffles each class sample using the seed number.</summary>
         *
         * <param name="instanceLists">Original class samples. Each element of the this array is a sample only from one class.</param>
         * <param name="K">K in K-fold cross-validation</param>
         * <param name="seed">Random number to create K-fold sample(s)</param>
         */
        public StratifiedKFoldCrossValidation(List<T>[] instanceLists, int K, int seed){
            this._instanceLists = instanceLists;
            _n = new int[instanceLists.Length];
            for (var i = 0; i < instanceLists.Length; i++){
                KFoldCrossValidation<T>.Shuffle(_instanceLists[i], new Random(seed));
                _n[i] = instanceLists[i].Count;
            }
            this.K = K;
        }

        /**
         * <summary>getTrainFold returns the k'th train fold in K-fold stratified cross-validation.</summary>
         *
         * <param name="k">index for the k'th train fold of the K-fold stratified cross-validation</param>
         * <returns>Produced training sample</returns>
         */
        public override List<T> GetTrainFold(int k)
        {
            var trainFold = new List<T>();
            for (var i = 0; i < _n.Length; i++){
                for (var j = 0; j < k * _n[i] / K; j++){
                    trainFold.Add(_instanceLists[i][j]);
                }
                for (var j = (k + 1) * _n[i] / K; j < _n[i]; j++){
                    trainFold.Add(_instanceLists[i][j]);
                }
            }
            return trainFold;
        }

        /**
         * <summary>getTestFold returns the k'th test fold in K-fold stratified cross-validation.</summary>
         *
         * <param name="k">index for the k'th test fold of the K-fold stratified cross-validation</param>
         * <returns>Produced testing sample</returns>
         */
        public override List<T> GetTestFold(int k)
        {
            var testFold = new List<T>();
            for (var i = 0; i < _n.Length; i++){
                for (var j = k * _n[i] / K; j < (k + 1) * _n[i] / K; j++){
                    testFold.Add(_instanceLists[i][j]);
                }
            }
            return testFold;
        }
    }
}