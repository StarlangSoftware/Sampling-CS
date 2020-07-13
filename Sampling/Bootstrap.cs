using System;
using System.Collections.Generic;

namespace Sampling
{
    public class Bootstrap<T>
    {
        private readonly List<T> _instanceList;
        
        /**
         * <summary>A constructor of {@link Bootstrap} class which takes a sample an array of instances and a seed number, then creates a bootstrap
         * sample using this seed as random number.</summary>
         *
         * <param name="instanceList"> Original sample</param>
         * <param name="seed">Random number to create bootstrap sample</param>
         */
        public Bootstrap(List<T> instanceList, int seed){
            var rand = new Random(seed);
            var n = instanceList.Count;
            this._instanceList = new List<T>();
            for (var i = 0; i < n; i++){
                this._instanceList.Add(instanceList[rand.Next(n)]);
            }
        }

        /**
         * <summary>getSample returns the produced bootstrap sample.</summary>
         *
         * <returns>Produced bootstrap sample</returns>
         */
        public List<T> GetSample(){
            return _instanceList;
        }

    }
}