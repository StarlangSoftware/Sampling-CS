using System.Collections.Generic;

namespace Sampling
{
    public abstract class CrossValidation<T>
    {
        protected int K;

        public abstract List<T> GetTrainFold(int k);
        public abstract List<T> GetTestFold(int k);
    }
}