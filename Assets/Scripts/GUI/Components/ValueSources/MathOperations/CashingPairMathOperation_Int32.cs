
using System;
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class CashingPairMathOperation_Int32 : CashingPairMathOperation<int>
    {
        protected sealed override IList<Func<int, int, int>> Operations =>
            MathOperations.PairInt32.Operations;
    }
}
