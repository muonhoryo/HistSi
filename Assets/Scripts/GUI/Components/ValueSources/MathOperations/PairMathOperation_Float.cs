
using System;
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class PairMathOperation_Float : PairMathOperation<float> 
    {
        protected sealed override IList<Func<float, float, float>> Operations =>
            MathOperations.PairFloat.Operations;
    }
}
