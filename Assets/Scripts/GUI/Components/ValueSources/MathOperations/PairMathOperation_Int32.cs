
using System.Collections.Generic;
using System;

namespace HistSi.ValueSources
{
    public sealed class PairMathOperation_Int32 : PairMathOperation<int>
    {
        protected sealed override  IList<Func<int, int, int>> Operations =>
            MathOperations.PairInt32.Operations;
    }
}
